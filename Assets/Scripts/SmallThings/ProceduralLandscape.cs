using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProceduralLandscape : MonoBehaviour {
    public int terrainSize =10;
    public float terrainGain =10f;
    public bool perlin;
    public float perlinScale=1;

    public float[,] GetFloatArray()
    {
        float[,] heights = new float[terrainSize, terrainSize];
        float perlinSeed = Random.Range(0f, 100000f);
        for (int x=0; x<terrainSize; x++)
        {
            for(int z = 0; z < terrainSize; z++)
            {
                float y = 0f;
                if (perlin)
                {
                    y = Mathf.PerlinNoise(perlinSeed+x / perlinScale,perlinSeed+z / perlinScale);
                    y *= terrainGain;
                }
                else
                {
                    y = Random.Range(0.0f, terrainGain);
                }
                heights[x, z] = y;
            }
        }
        return heights;
    }

    public Vector3[] GetVector3s()
    {
        Vector3[] positions = new Vector3[terrainSize*terrainSize];
        float perlinSeed = Random.Range(0f, 100000f);
        for (int x = 0; x < terrainSize; x++)
        {
            for (int z = 0; z < terrainSize; z++)
            {
                float y = 0.0f;
                if (perlin)
                {
                    y = Mathf.PerlinNoise(perlinSeed+x/perlinScale,perlinSeed+z/perlinScale);
                    y *= terrainGain;
                }
                else
                {
                    y = Random.Range(0.0f, terrainGain);
                }
                
                int index = x + (z*terrainSize);
                positions[index] = new Vector3(x, y, z);
            }
        }
                return positions;

    }
    public virtual void Generate()
    {

    }
	// Use this for initialization
	void Start () {
        Generate();
	}
	
}
