using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GridSpace : MonoBehaviour
{
  public int listID;
  public int xAxis;
  public int yAxis;
  public int zAxis;

  public GameObject worldGridSpace;
  
  public GridSpace(int x, int z)
  {
    //Default
    xAxis = 0;
    yAxis = 0;
    zAxis = 0;
    //
    xAxis = x;
    zAxis = z;

  }
  public GridSpace(int x, int y, int z)
  {
    //Default
    xAxis = 0;
    yAxis = 0;
    zAxis = 0;
    //
    xAxis = x;
    yAxis = y;
    zAxis = z;
  }

  public Vector2 PositionV2()
  {
    return new Vector2(xAxis,zAxis);
  }
  
  public Vector3 PositionV3()
  {
    return new Vector3(xAxis,yAxis,zAxis);
  }
}

