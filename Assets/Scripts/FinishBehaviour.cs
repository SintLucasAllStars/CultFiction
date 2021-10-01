using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Spaakschip")
        {
            GameManager.instance.SetTimer(false);
            StartCoroutine("LevelSwitch");
        }
    }

    IEnumerator LevelSwitch()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }
}
