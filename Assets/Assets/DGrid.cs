using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DGrid : MonoBehaviour
{
    public GameObject gridCubePrefab;
    [SerializeField] public List<GridSpace> digitalGrid;
    
    // Start is called before the first frame update
    void Start()
    {
        digitalGrid = new List<GridSpace>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            CreateDigitalGrid(20,20);

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < digitalGrid.Count; i++)
            {
                var debug = digitalGrid[i];
                Debug.Log(debug.xAxis +"-"+ debug.zAxis);    
            }

            CreateWorldSpaceGrid();
        }
    }

    void CreateDigitalGrid(int xSize, int zSize)
    {
        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < zSize; j++)
            {
                digitalGrid.Add(new GridSpace(j, i));
            }
        }
    }
    
    
    void CreateDigitalGrid(int xSize, int ysize , int zSize)
    {
      
        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < zSize; j++)
            {
                for (int k = 0; k < ysize; k++)
                {
                    digitalGrid.Add(new GridSpace(k, j, i));

                }
            }
        } 
    }
    

    void CreateWorldSpaceGrid()
    {
        for (int i = 0; i < digitalGrid.Count; i++)
        { 
            GridSpace dGridSpaceScript = digitalGrid[i];

            GameObject space = Instantiate(gridCubePrefab, dGridSpaceScript.PositionV3(), Quaternion.identity);
            space.GetComponent<GridSpace>().xAxis = dGridSpaceScript.xAxis;
            space.GetComponent<GridSpace>().yAxis = dGridSpaceScript.yAxis;
            space.GetComponent<GridSpace>().zAxis = dGridSpaceScript.zAxis;
            space.GetComponent<GridSpace>().listID = i;
            space.GetComponent<GridSpace>().worldGridSpace = space;
        }
    }
   
}
