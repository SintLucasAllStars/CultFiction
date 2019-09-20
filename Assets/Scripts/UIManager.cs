using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public GameObject inGameUI;
    public GameObject menuUI;
    public GameObject lvlCompleteUI;

    void Start() {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            inGameUI.SetActive(false);
            menuUI.SetActive(true);
        }
        else
        {
            inGameUI.SetActive(true);
            lvlCompleteUI.SetActive(false);
            menuUI.SetActive(false);
        }
    }

    void Update() {
        if (lvlCompleteUI != null) // Incorrect, I know.
        {
            if (GameManager.instance.isComplete)
            {
                lvlCompleteUI.SetActive(true);
            }
        }
    }

}