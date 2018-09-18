using UnityEngine;

namespace Inventory.Item
{
    //Weapons & Tools
    //Sword
    //Shovel
    //Pickaxe
    //Axe

    [CreateAssetMenu(fileName = "New Weapon/Tools", menuName = "Inventory/WeaponTools", order = 2)]
    public class Weapon : Equipment
    {
        public double damage;

    }


}