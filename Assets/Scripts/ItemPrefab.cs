using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemPrefab : MonoBehaviour
{
    private ShopItem _shopItem;

    [SerializeField]
    private TextMeshProUGUI _priceText = null;

    [SerializeField]
    private Image _itemImage = null;

    public void InitShopItem(ShopItem item)
    {
        _shopItem = item;
        _priceText.text = _shopItem.Price.ToString();
        _itemImage.sprite = _shopItem.Sprite;
    }

    public void Purchase()
    {
        if (!BankAccount.Instance.HasEnoughMoney(_shopItem.Price))
            return;

        if(_shopItem is PhysicalShopItem)
        {
            PhysicalShopItem item = (PhysicalShopItem)_shopItem;
            if (!ObjectManager.Instance.CanPlaceObject(item.ShopObject.ObjectType))
            {
                ErrorMessage.Instance.AnnounceError("There is no more space left for this object");
                return;
            }

            ObjectManager.Instance.PlaceObject(item.ShopObject);
        }
        else if(_shopItem is FoodShopItem)
        {
            FoodShopItem item = (FoodShopItem)_shopItem;
            KitchenManager.Instance.AddFood(item.ShopObject);
        }

        Shop.Instance.CloseShop();
        Shop.Instance.Purchase(_shopItem);
    }
}
