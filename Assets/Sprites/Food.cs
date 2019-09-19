using UnityEngine;

[CreateAssetMenu(fileName = "Food", menuName = "Game/Food", order = 1)]
public class Food : ScriptableObject
{
    [SerializeField]
    protected FoodName foodName;
    public FoodName FoodName => foodName;

    [SerializeField]
    protected Sprite _sprite;
    public Sprite Sprite => _sprite;

    [SerializeField]
    protected int _price;
    public int Price => _price;
}

public enum FoodName
{
    Coconut,
    Sliced_Coconut,
    Drink
}