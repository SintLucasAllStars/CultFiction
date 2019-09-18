using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextMover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadLevel());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, 2 * Time.deltaTime, 0));
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(2);
        }
    }

    IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(30);
        SceneManager.LoadScene(2);
    }
}
