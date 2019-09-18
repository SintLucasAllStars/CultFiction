using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : Singleton<Shop>
{
    private Animator _animator;

    [SerializeField]
    private ShopItem[] _shopItems;

    [SerializeField]
    public GameObject _shopItemPrefab;

    [SerializeField]
    private Transform _shopContainer;

    private List<ItemPrefab> _currentItemsContainers = new List<ItemPrefab>();

    private bool _isOpened;

    private void Start() => _animator = GetComponent<Animator>();

    private Dictionary<string, bool> _boughtItems = new Dictionary<string, bool>();

    public void ToggleShop()
    {
        if (_isOpened)
            CloseShop();
        else
            OpenShop();
    }

    public void CloseShop()
    {
        _isOpened = false;
        _animator.SetBool("IsOpened", _isOpened);
        for (int i = 0; i < _currentItemsContainers.Count; i++)
        {
            Destroy(_currentItemsContainers[i].gameObject);
        }
        _currentItemsContainers.Clear();
    }

    public void OpenShop()
    {
        _isOpened = true;
        _animator.SetBool("IsOpened", _isOpened);
        for (int i = 0; i < _shopItems.Length; i++)
        {
            if (CanBuyUpgrade(_shopItems[i].PostCondition, _shopItems[i].PreReq))
            {
                ItemPrefab prefab = Instantiate(_shopItemPrefab, _shopContainer).GetComponent<ItemPrefab>();
                prefab.InitShopItem(_shopItems[i]);
                _currentItemsContainers.Add(prefab);
            }
        }
    }

    public bool CanBuyUpgrade(string postReq, string preReq)
    {
        //If the post req is in here, it cannot be bought again.
        if (_boughtItems.ContainsKey(postReq) && !string.IsNullOrEmpty(postReq))
            return false;

        //If the preReq is empty then it can be bought.
        if (string.IsNullOrEmpty(preReq))
            return true;

        //If it does contain the preReq it means they can either be bought or not bought depending on the value in the dictionary (refunds?)
        if (_boughtItems.ContainsKey(preReq))
            return _boughtItems[preReq];

        return false;
    }

    public void Purchase(ShopItem item)
    {
        GameManager.Instance.UpdateMoney(-item.Price);
        _boughtItems[item.ItemName] = true;
    }
}
