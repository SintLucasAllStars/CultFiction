using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextMover : MonoBehaviour
{
    public int timeToLoad;
    public int scene;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadLevel());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, 2 * Time.deltaTime, 0));
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(scene);
        }
    }

    IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(timeToLoad);
        SceneManager.LoadScene(scene);
    }
}
