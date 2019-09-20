using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chonk : MonoBehaviour
{
    [SerializeField]
    private float _chonkSpeed;

    private void Update()
    {
        transform.position += Vector3.back * _chonkSpeed * Time.deltaTime;
    }
}
