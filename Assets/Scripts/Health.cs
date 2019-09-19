using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{

    public Image[] lives;
    private int liveCount = 3;
    private bool isHit;

    public GameObject bloodScreen;
    private float duration = 1.0f;
    private float duration1 = 2.0f;

    public Color textureColor;
    public Color textureColor2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(liveCount < 0)
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("GameOver");
        }

        if(isHit == true)
        {
            textureColor = bloodScreen.GetComponent<Image>().color;
            textureColor.a = Mathf.PingPong(Time.time, duration) / duration;
            bloodScreen.GetComponent<Image>().color = textureColor;
        }

        if(isHit == false)
        {
            bloodScreen.GetComponent<Image>().color = textureColor2;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Trap" && isHit == false)
        {
            StartCoroutine(HeathCountdown());
            isHit = true;
        }
    }

    IEnumerator HeathCountdown()
    {
        Destroy(lives[liveCount--]);
        yield return new WaitForSeconds(1f);
        isHit = false;
    }
}
