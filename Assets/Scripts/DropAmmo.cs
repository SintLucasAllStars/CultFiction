using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropAmmo : MonoBehaviour
{
    [SerializeField]
    private int ammo;

    private void Start()
    {
        ammo = Random.Range(8, 36);
    }

    public int PickUp()
    {
        Destroy(gameObject, Time.deltaTime);
        return ammo;
    }
}
