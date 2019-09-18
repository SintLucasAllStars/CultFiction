using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int amountOfTurrets;
    public int amountOfShields;
    public List<ShipTurret> turrets = new List<ShipTurret>();
    public List<ShipShieldGen> shields = new List<ShipShieldGen>();

    void Start()
    {
        turrets.AddRange(GameObject.FindObjectsOfType<ShipTurret>());
        shields.AddRange(GameObject.FindObjectsOfType<ShipShieldGen>());

        amountOfTurrets = turrets.Count;
        amountOfShields = shields.Count;
    }
    
    void Update()
    {
            foreach (ShipTurret i in turrets)
            {
                i.GetComponentInChildren<ForceShield>().gameObject.SetActive(false);
            }
    }

    void ShieldDestroyed()
    {
        amountOfShields -= 1;
        if (amountOfShields < 1)
        {
            foreach (ShipTurret i in turrets)
            {
                i.GetComponentInChildren<ForceShield>().gameObject.SetActive(false);
            }
        }
    }

    void TurretDestroyed()
    {
        amountOfTurrets -= 1;
        if (amountOfTurrets < 1)
        {
            Debug.Log("Ship Nutrualized");
        }
    }

}
