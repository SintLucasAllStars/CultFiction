using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
	//properties
	private string name;
	private int weight;

	public string Name
	{
		get
		{
			return name;
		}
	}

	public int Weight
	{
		get
		{
			return weight;
		}
	}

	//contructor
	public Item(string name,int weight)
	{
		this.name = name;
		this.weight = weight;
	}

	//methods
}
