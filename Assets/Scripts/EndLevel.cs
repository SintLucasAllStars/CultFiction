using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour, IInteractable {

    [SerializeField] int scoreRequired;
    [SerializeField] int roundRequired;

    public void Interact(PlayerController controller)
    {
        if(Gamemanager.instance.GetScore() >= scoreRequired)
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);

        Debug.Log("You broke");
    }

}
