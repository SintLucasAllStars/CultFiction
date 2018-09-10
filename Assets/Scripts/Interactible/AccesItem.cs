using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class AccesItem : Item 
{
	//properties
	public int doorID;

	//constructor)
	public AccesItem(string name,int weight,int doorID) : base(name,weight)
	{
		this.doorID = doorID;

	}

	//methods
	public bool OpensDoor(int id)
	{
		return doorID == id;
	}
}
