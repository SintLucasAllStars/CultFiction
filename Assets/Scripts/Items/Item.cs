using System;
using UnityEngine;

namespace Inventory.Item
{
    //[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item", order = 1)]
    public abstract class Item : ScriptableObject
    {
        //// OBJECT NAME & ID ////
        public string itemName = "Item";
        public UInt16 itemId = 0;

        //// OBJECT UI ////
        [TextArea]
        public string itemDescription;
        public Texture2D itemIcon;

        ///// ITEM  ////
        public int itemWeight;
        

        public abstract void Use(int index);
    }
}