using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyPin : MonoBehaviour
{

    public bool pinDestroy = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            pinDestroy = true;
            Debug.Log("destroy");
            Destroy(transform.parent.gameObject, 3f);
        }

        if (collision.gameObject.tag == "LoseCollision")
        {
            Debug.Log("losehit");
            SceneManager.LoadScene("GameOver");
        }
    }
}
