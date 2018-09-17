using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public float BlastRange;
    [HideInInspector]
    public GameController gc;
    [HideInInspector]
    public WaveSpawner ws;

    public GameObject Gun;
    void Start () {
        ws = FindObjectOfType<WaveSpawner>();
        gc = FindObjectOfType<GameController>();
    }
	void Update () {
        Visuals();
        OnShoot();
    }
    void Visuals()
    {
      

        Vector3 dif = Camera.main.ScreenToWorldPoint(Input.mousePosition) - Gun.transform.position;
        dif.Normalize();

        float rot_z = Mathf.Atan2(dif.y, dif.x) * Mathf.Rad2Deg;
        Gun.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }
    void OnShoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            gc.SpawnAnimation(new Vector3(mousePos.x+0.28f,mousePos.y-0.3f),1);
            for (int i = 0; i < ws.airFighters.Count; i++)
            {
          
                if ((mousePos.x - BlastRange) < ws.airFighters[i].transform.position.x && (mousePos.x + BlastRange) > ws.airFighters[i].transform.position.x)
                {
                    if ((mousePos.y - BlastRange) < ws.airFighters[i].transform.position.y && (mousePos.y + BlastRange) > ws.airFighters[i].transform.position.y)
                    {
                        ws.airFighters[i].GetComponent<GunShip>().KillAirFighter(mousePos);
                    }
                }

            }

            //killing them with the gun is to easy

           // sm.KillGroundTroops(new Vector3(mousePos.x,mousePos.y,0), 0.3f,0);
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            ws.KillGroundTroops(Vector3.zero, 10, 0);
        }
    }
}
