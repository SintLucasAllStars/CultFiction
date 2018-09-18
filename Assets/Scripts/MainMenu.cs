using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject canvasMenu;
    public Animator anim;
    public GameObject bommmm;

    public void ButtonPlay ()
    {
        canvasMenu.SetActive(false);
        anim.SetBool("play", true);
        bommmm.SetActive(true);
        StartCoroutine(waitForLevelsLoading());
    }

    IEnumerator waitForLevelsLoading  ()
    {
        yield return new WaitForSeconds(5);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
