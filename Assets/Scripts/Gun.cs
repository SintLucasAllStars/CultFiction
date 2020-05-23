using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class Gun : ScriptableObject
{
    public GameObject bullet;
    public string gunName;
    public int totalAmmo;
    public int ammo;
    public int clipCapacity;
    public int ammoInClip;
    public float fireRate;
    public float reloadTime;
    public int damage;
}
