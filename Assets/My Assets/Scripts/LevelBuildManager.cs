﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuildManager : MonoBehaviour
{
    public DGrid dGridScript;
    public GameObject gridCubePrefab;
    // Start is called before the first frame update
    void Start()
    {
        dGridScript = GameObject.Find("Game Managers and debug").GetComponent<DGrid>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < dGridScript.dGrid.Count; i++)
            {
                var debug = dGridScript.dGrid[i];
                Debug.Log(debug.xAxis + "-" + debug.zAxis);
            }

        }
    }
    
    public void CreateWorldSpaceGrid()
    {
        
        
        
        
        
        for (int i = 0; i < dGridScript.dGrid.Count; i++)
        {
            GridSpace dGridSpaceScript = dGridScript.dGrid[i];

            GameObject space = Instantiate(gridCubePrefab, dGridSpaceScript.PositionV3(), Quaternion.identity);
            space.GetComponent<GridSpace>().xAxis = dGridSpaceScript.xAxis;
            space.GetComponent<GridSpace>().yAxis = dGridSpaceScript.yAxis;
            space.GetComponent<GridSpace>().zAxis = dGridSpaceScript.zAxis;
            space.GetComponent<GridSpace>().dListID = i;
            space.GetComponent<GridSpace>().worldGridSpace = space;
        }
    }
}
