using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalShopItem : ShopItem
{
    [SerializeField]
    private ShopObject _shopObject = new ShopObject();
    public ShopObject ShopObject => _shopObject;
}

[System.Serializable]
public class ShopObject
{
    [SerializeField]
    private GameObject _object;
    public GameObject Object => _object;

    [SerializeField]
    private ObjectType _objectType;
    public ObjectType ObjectType => _objectType;
}