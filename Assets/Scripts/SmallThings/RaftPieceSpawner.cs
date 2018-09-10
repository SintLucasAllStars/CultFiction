using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftPieceSpawner : MonoBehaviour {
	public  GameObject[] raftItems;
	TerrainLandscape terrain;

	public LayerMask thingsToGroundWith;
	int size;
	int seed;
	float[,] heights;
	// Use this for initialization
	void Start () {
		terrain = FindObjectOfType<TerrainLandscape> ();
		size = terrain.size;
		Invoke ("LateStart", 1);

	}
	void LateStart(){
		terrain = FindObjectOfType<TerrainLandscape> ();
		heights = terrain.GetFloatArrayExtern ();
		for (int x = 0; x < raftItems.Length; x++)
		{
				RaycastHit hit;
				Vector3 startRayPoint = new Vector3 (Random.Range(-((size-20)/2),(size-20)/2), 50, Random.Range(-((size-20)/2),(size-20)/2));
					Ray rayDown = new Ray (startRayPoint, -transform.up);
					Debug.DrawRay (startRayPoint, -transform.up, Color.yellow, 50, false);
					if (Physics.Raycast (rayDown, out hit, 80, thingsToGroundWith))
					{
						Instantiate (raftItems [x], hit.point + Vector3.up, Quaternion.identity);
					}
				

		}
	}
}
