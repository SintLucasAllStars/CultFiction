using UnityEngine;

[CreateAssetMenu(fileName = "ShopItem_Food", menuName = "Game/ShopItem_Food", order = 1)]
public class FoodShopItem : ShopItem
{
    [SerializeField]
    private Food _shopObject = null;
    public Food ShopObject => _shopObject;
}
