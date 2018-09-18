using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory {

    public class InventoryManagement : MonoBehaviour
    {
        #region Singleton
        public static InventoryManagement instance;

        void Awake()
        {
            instance = this;
        }
        #endregion

        public int slotSize
        {
            get; private set;
        }

        //public List<Item.Item> items = new List<Item.Item>();

        //public delegate void intValueDelegate(int value);
        //public intValueDelegate OnSlotSizeChangedCallback;


        public void SlotChangedSize(int value)
        {
            slotSize = value;
        }

        private void Start()
        {

        }


    }


    [System.Serializable]
    public class ItemInInventory
    {
        // Op handL
        public GameObject placement;
        // Weeg 3 kg
        public int placementWear;

        public ItemInInventory (GameObject placement, int placementWear)
        {
            this.placement = placement;
            this.placementWear = placementWear;
        }
    }
}