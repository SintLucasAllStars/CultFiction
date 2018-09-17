using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentScript : MonoBehaviour {

    public void StartFadingEnvironment () {

        for (int i = 0; i < transform.childCount;i++)
            transform.GetChild(i).GetComponent<FadeScript>().StartFade();

        Destroy(this);
    }
}
