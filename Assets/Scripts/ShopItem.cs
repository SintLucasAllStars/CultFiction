using UnityEngine;

[CreateAssetMenu(fileName = "ShopItem", menuName = "Game/ShopItem", order = 1)]
public class ShopItem : ScriptableObject
{
    [SerializeField]
    protected string _itemName, _preReq, _postCondition;
    public string ItemName => _itemName;
    public string PreReq => _preReq;
    public string PostCondition => _postCondition;

    [SerializeField]
    protected Sprite _sprite;
    public Sprite Sprite => _sprite;

    [SerializeField]
    protected int _price;
    public int Price => _price;
}