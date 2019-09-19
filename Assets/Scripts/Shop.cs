﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : Singleton<Shop>
{
    private Animator _animator = null;

    [SerializeField]
    public GameObject _shopItemPrefab = null;

    [SerializeField]
    private ShopItem[] _shopItems = null;

    [SerializeField]
    private Transform _shopContainer = null;

    private List<ItemPrefab> _currentItemsContainers = new List<ItemPrefab>();

    private bool _isOpened = false;

    private Dictionary<string, bool> _boughtItems = new Dictionary<string, bool>();

    private void Start() => _animator = GetComponent<Animator>();

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

    public bool CanBuyUpgrade(string[] postReq, string[] preReq)
    {
        bool canBuy = false;

        if (preReq.Length == 0)
            canBuy = true;

        if (postReq.Length == 0)
            canBuy = true;

        //If the post req is in here, it cannot be bought again.
        for (int i = 0; i < postReq.Length; i++)
        {
            if (_boughtItems.ContainsKey(postReq[i]) && !string.IsNullOrEmpty(postReq[i]))
                return false;
            else
                canBuy = true;
        }

        for (int i = 0; i < preReq.Length; i++)
        {
            if (!_boughtItems.ContainsKey(preReq[i]))
                return false;

            //If the preReq is empty then it can be bought.
            if (string.IsNullOrEmpty(preReq[i]))
                canBuy = true;

            //If it does contain the preReq it means they can either be bought or not bought depending on the value in the dictionary (refunds?)
            if (_boughtItems.ContainsKey(preReq[i]))
                canBuy = _boughtItems[preReq[i]];
        }

        return canBuy;
    }

    public void Purchase(ShopItem item)
    {
        BankAccount.Instance.RemoveMoney(item.Price);
        _boughtItems[item.ItemName] = true;
        SoundEffectManager.Instance.PlaySound(SoundEffectName.PurchaseItem);
    }
}
