using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Saw_Rotation : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.Rotate (new Vector3 (0f, 0f, 3f));
    }
}
