using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuildManager : MonoBehaviour
{
    public DGrid dGridScript;
    public GameObject playerGridCubePrefab;
    public GameObject aiGridCubePrefab;
    public List<GameObject> worldSpaceGrid;
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
            DigitalGridSpace dGridSpaceValuesInstance = dGridScript.dGrid[i];
            GameObject cube;
            int test = dGridScript.dGrid.Count/2;
            if (i < test)
            {
                cube = playerGridCubePrefab;
            }
            else
            {
                cube = aiGridCubePrefab;
            }

            GameObject space = Instantiate(cube, dGridSpaceValuesInstance.PositionV3(), Quaternion.identity);
            space.GetComponent<GridSpace>().SetValuesWhenInstantiated(dGridSpaceValuesInstance.xAxis,dGridSpaceValuesInstance.yAxis,dGridSpaceValuesInstance.zAxis, i, true);
            worldSpaceGrid.Add(space);
            //may not need this
            //space.GetComponent<GridSpace>().worldGridSpace = space;
        }
    }
}
