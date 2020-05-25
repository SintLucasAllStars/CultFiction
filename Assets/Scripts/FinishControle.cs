using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishControle : MonoBehaviour
{
    private GameObject Target;
    public float distance;

    private void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Target");
    }
    private void Update()
    {
         distance = Vector3.Distance(gameObject.transform.position, Target.transform.position);
        if(distance < 3)
        {
            SceneManager.LoadScene("MainScene");
        }
    }


}
