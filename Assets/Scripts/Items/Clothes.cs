using UnityEngine;

namespace Inventory.Item
{
    [CreateAssetMenu(fileName ="New Clothes", menuName = "Inventory/Clothes", order = 3)]
    public class Clothes : Equipment
    {
        public EquipmentClothesSlot clothesSlot;

        public int armor;

    }

    public enum EquipmentClothesSlot { Head, Chest, Legs, Feet}

}