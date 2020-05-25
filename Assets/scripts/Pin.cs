using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pin : MonoBehaviour
{
    public float speed = 2f;
    private Vector3 targetLocation;
    private WaveManager WaveManager;
    public DestroyPin DestroyPin;

    // Start is called before the first frame update
    void Start()
    {
        WaveManager = FindObjectOfType<WaveManager>();
        targetLocation = GameObject.FindGameObjectWithTag("LoseCollision").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = Vector3.MoveTowards//(transform.position, speed = Time.deltaTime);
        transform.position += Vector3.back * Time.deltaTime * speed;

        if (DestroyPin.pinDestroy == true)
        {
            Destroy(this);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "LoseCollision")
        {
            Debug.Log("losehit");
            SceneManager.LoadScene("GameOver");
        }
    }
}