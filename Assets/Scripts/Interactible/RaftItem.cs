using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftItem : Item {
	//properties
	private int part;

	//constructor)
	public RaftItem(string name,int weight,int part) : base(name,weight)
	{
		this.part = part;
	}

	//methods
	public int GetValue(){
		return part;
	}

}
