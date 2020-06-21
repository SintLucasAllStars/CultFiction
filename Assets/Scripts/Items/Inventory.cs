using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Text ammoText;
    public List<Image> images;
    public Sprite defaultImage;
    private readonly List<PickupableItemTag> _itemList = new List<PickupableItemTag>();

    public int ammo;
    private MovementController _movement;

    public bool HasItem(string name)
    {
        foreach (PickupableItemTag item in _itemList)
        {
            if (item.name == name)
                return true;
        }

        return false;
    }

    public void AddItem(PickupableItemTag pickupableItem)
    {
        _itemList.Add(pickupableItem);
        UpdateItemList();
    }

    private void Start()
    {
        ammo = 50;
    }

    public void RemoveAmmo()
    {
        ammo -= 1;
        ammoText.text = "Ammo: " + ammo;
    }

    public void AddAmmo(int amount)
    {
        ammo += amount;
        ammoText.text = "Ammo: " + ammo;
    }

    private void UpdateItemList()
    {
        for (var i = 0; i < images.Count; i++)
        {
            images[i].sprite = _itemList.Count >= i+1 ? _itemList[i].icon : defaultImage;        
        }
    }
}