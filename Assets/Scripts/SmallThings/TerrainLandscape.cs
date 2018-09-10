using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainLandscape : ProceduralLandscape {
     Terrain t;
	public BuildingPlacement Level;
    bool set;
	float percentage;
	public float smooth = 100;
	public int size = 30;
	float highest;
	public float[,] store;
    public override void Generate()
    {
		percentage = 1.0f / smooth;
        float[,] heights = GetFloatArray();
		store =  new float[terrainSize,terrainSize];
        t = GetComponent<Terrain>();
		MakeCoast (heights);
		foreach (float f in heights)
		{
			if (f > highest)
			{
				highest = f;
			}
		}
	MakeSpaceForPrison (heights);
        t.terrainData.SetHeights(0, 0, heights);
		GetComponent<TextureGenerator> ().Texturize ();
		float y = Level.GetPos ();
		Level.gameObject.transform.position = new Vector3 (Level.gameObject.transform.position.x, y-10.8f, Level.gameObject.transform.position.z);

    }

	void MakeCoast(float[,] heights){
		for(int i = 0; i <smooth; i++)
		{
			for (int p = 0 ; p <  t.terrainData.alphamapHeight+1; p++)
			{
				heights [i, p] = heights [i, p] * (i / smooth);
				heights [p, i] = heights [p,i] * (i / smooth);
				heights [t.terrainData.alphamapHeight-i, p] = heights [t.terrainData.alphamapHeight-i, p] * (i / smooth);
				heights [p, t.terrainData.alphamapHeight-i] = heights [p,t.terrainData.alphamapHeight-i] * (i / smooth);
			}
		}

	}
	public float[,] GetFloatArrayExtern(){
		float[,] heights = GetFloatArray();
		return heights;
	}
	void MakeSpaceForPrison(float[,] heights){
		for (int x = 0; x < heights.GetLength (0); x++)
		{
			for (int z = 0; z < heights.GetLength (1); z++)
			{
				store [x, z] = heights [x, z];
			}
		}
		for(int i = terrainSize/2; i <(terrainSize/2)+size*3; i++)
		{
			for (int p = terrainSize/2 ; p <  (terrainSize/2)+size*3; p++)
			{
				heights [i, p] = highest;
			}
		}
		for(int i = terrainSize/2; i <terrainSize/2 +size*3; i++)
		{
			for (int p = terrainSize/2 ; p <  terrainSize/2+size; p++)
			{
				if (heights [i, p] * (p - (terrainSize / 2)) / size > store [i, p])
				{
					heights [i, p] = heights [i, p] * (p - (terrainSize / 2)) / size;
				}
				else
				{
					heights [i, p] = store [i, p];
				}
				if (heights [p, i] * (p - (terrainSize / 2)) / size > store [p, i])
				{
					heights [p, i] = heights [p, i] * (p - (terrainSize / 2)) / size;
				}else
				{
					heights [p, i] = store [p, i];
				}
				if (heights [terrainSize + size * 3 - p, i] * (p - (terrainSize / 2)) / size > store [terrainSize + size * 3 - p, i])
				{
					heights [terrainSize + size * 3 - p, i] = heights [(terrainSize) + size * 3 - p, i] * (p - (terrainSize / 2)) / size;
				}
				else
				{
					heights [terrainSize + size * 3 - p, i] = store [terrainSize + size * 3 - p, i];
				}
				if (heights [i, terrainSize + size * 3 - p] * (p - (terrainSize / 2)) / size > store [i, terrainSize + size * 3 - p])
				{
					heights [i, terrainSize + size * 3 - p] = heights [i, terrainSize + size * 3 - p] * (p - (terrainSize / 2)) / size;
				}
				else
				{
					heights [i, terrainSize + size * 3 - p] = store [i, terrainSize + size * 3 - p];
				}
			}
		}
	}

}
