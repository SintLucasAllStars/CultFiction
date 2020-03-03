using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Death : MonoBehaviour
{
    public AudioSource deathSound;
    
    // Start is called before the first frame update
    void Start()
    {
        deathSound = GetComponent<AudioSource> ();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.R)){
            SceneManager.LoadScene("SampleScene");
        }
        if(Input.GetKey(KeyCode.Q)){
            SceneManager.LoadScene("MainMenu");
        }
    }

IEnumerator ChangeScene(int index,float delay = 0.13f)
     {
          yield return new WaitForSeconds(delay);
          SceneManager.LoadScene("Restart_Scene");
     }

     void OnCollisionEnter2D(Collision2D coll)
{
    if (coll.gameObject.tag == "Player")
	{
        Debug.Log("Death");
        deathSound.Play();
        StartCoroutine(ChangeScene(2));
        }
    }
}
     
  

