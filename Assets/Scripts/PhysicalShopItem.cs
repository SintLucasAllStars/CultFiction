using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalShopItem : ShopItem
{
    [SerializeField]
    private RestaurantObject _shopObject = new RestaurantObject();
    public RestaurantObject ShopObject => _shopObject;
}

[System.Serializable]
public class RestaurantObject
{
    [SerializeField]
    private GameObject _object = null;
    public GameObject Object => _object;

    [SerializeField]
    private ObjectType _objectType = ObjectType.Table;
    public ObjectType ObjectType => _objectType;
}