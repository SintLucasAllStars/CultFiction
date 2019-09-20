using UnityEngine;

[CreateAssetMenu(fileName = "OrderItem", menuName = "Game/OrderItem", order = 1)]
public class OrderItem : ScriptableObject
{
    [SerializeField]
    protected FoodName foodName;
    public FoodName FoodName => foodName;

    [SerializeField]
    protected Sprite _sprite;
    public Sprite Sprite => _sprite;
}

public enum FoodName
{
    Red_Tea,
    Sliced_Coconut,
    Green_Tea
}