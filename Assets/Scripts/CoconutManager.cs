using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoconutManager : MonoBehaviour
{
    public GameObject nut;
    public static int score = 0;

    public AudioClip throwSound;
    AudioSource asc;

    public Text scoreBox;
    string baseText = "Score: ";

    float power = 400f;
    float cooldown = 1.4f;
    bool canFire = true;

    private void Start()
    {
        scoreBox.text = baseText + score;
        asc = FindObjectOfType<AudioSource>();
        asc.clip = throwSound;
    }

    private void Update()
    {
        int oldScore = int.Parse(scoreBox.text.Remove(0, baseText.Length));
        if (oldScore != score)
        {
            scoreBox.text = baseText + score;
        }
        if (Input.GetMouseButton(0) && canFire)
        {
            Vector3 startPos = FindObjectOfType<Camera>().gameObject.transform.position;
            startPos.y -= .7f;
            GameObject n = Instantiate(nut, startPos, Quaternion.identity);

            asc.Play();

            Vector3 m = Input.mousePosition;
            Vector3 k = Camera.main.ScreenToWorldPoint(new Vector3(m.x, m.y, 1));
            float camDistance = (float)Mathf.Sqrt((Mathf.Pow(k.y, 2.0f) + Mathf.Pow(6, 2.0f)));
            Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(m.x, m.y, camDistance));

            p.z = -p.z;
            p = p.normalized;

            Rigidbody rb = n.GetComponent<Rigidbody>();
            rb.AddForce(p * power);
            canFire = false;
            StartCoroutine(Cooldown(cooldown));

            FloatScript fs = n.GetComponent<FloatScript>();
            if (fs.isFloating())
            {
                Destroy(fs, 3f);
                Destroy(n.GetComponent<Rigidbody>(), 3f);
                Destroy(n.GetComponent<Collider>(), 3f);
            }
            else Destroy(n, 12f);
        }
    }

    IEnumerator Cooldown(float down)
    {
        yield return new WaitForSeconds(down);
        canFire = true;
    }
}