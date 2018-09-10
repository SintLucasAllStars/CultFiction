using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeLandscape : ProceduralLandscape {

    public GameObject prefab;
    public override void Generate()
    {
        
        Vector3[] posistions = GetVector3s();

        for( int i =0; i< posistions.Length; i++)
        {
            Instantiate(prefab, posistions[i], Quaternion.identity,transform);
        }
    }
}

