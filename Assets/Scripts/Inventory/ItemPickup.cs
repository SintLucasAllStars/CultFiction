using UnityEngine;

namespace Inventory
{
    public class ItemPickup : Interactable
    {
        public Item.Item item;
        public override void Interact()
        {
            base.Interact();
            Pickup();
        }

        private void Pickup()
        {
            Debug.Log(item.name + " try Pick up");
        }

    }
}