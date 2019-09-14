using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DGrid : MonoBehaviour
{
    [SerializeField] public List<GridSpace> dGrid;

    // Start is called before the first frame update
    void Start()
    {
        dGrid = new List<GridSpace>();
    }

 

    public void CreateDigitalGrid(int xSize, int zSize)
    {
        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < zSize; j++)
            {
                dGrid.Add(new GridSpace(j, i));
            }
        }
    }

    public void CreateDigitalGrid(int xSize, int ysize, int zSize)
    {
        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < zSize; j++)
            {
                for (int k = 0; k < ysize; k++)
                {
                    dGrid.Add(new GridSpace(k, j, i));
                }
            }
        }
    }
}
