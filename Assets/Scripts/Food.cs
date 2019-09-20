using UnityEngine;

[CreateAssetMenu(fileName = "Food", menuName = "Game/Food", order = 1)]
public class Food : OrderItem
{
    [SerializeField]
    protected int _price;
    public int Price => _price;
}