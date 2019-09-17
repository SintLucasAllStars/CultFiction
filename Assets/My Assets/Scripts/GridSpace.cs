using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpace : MonoBehaviour
{
    public int spaceID;
    public int spaceX;
    public int spaceY;
    public int spaceZ;

    //private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
       // gm = GameObject.Find("Game Managers and debug").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetValuesWhenInstantiated(int x,int y,int z, int ID)
    {
        
        // id used for movement calculation
        spaceID = ID;
        spaceX = x;
        spaceY = y;
        spaceZ = z;
    }
    
}
