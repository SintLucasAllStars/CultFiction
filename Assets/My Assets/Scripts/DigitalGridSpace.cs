using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// this class is for creating a list with a certain size and coordinates for when a world space grid is instantiated 
public class DigitalGridSpace
{
  public int xAxis;
  public int yAxis;
  public int zAxis;

  private void Awake()
  {
    //Default
    xAxis = 0;
    yAxis = 0;
    zAxis = 0;
  }

  public DigitalGridSpace(int x, int z)
  {
    xAxis = x;
    zAxis = z;

  }
  public DigitalGridSpace(int x, int y, int z)
  {
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

