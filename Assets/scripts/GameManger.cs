using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManger : MonoBehaviour {
    [HideInInspector]
    public int Score;
    [HideInInspector]
    public float speed;
    public Text scoreText;
    public GameObject deathText;
    public GameObject pipe;
    private bool gameStopped;
    private AudioSource audioSource;

    private void Start() {
        speed = 1;
        StartCoroutine(PipeLoop());
        scoreText.text = $"{Score}";
        audioSource = GetComponent<AudioSource>();
    }

    public void ScoreIncrement() {
        Score++;
        speed *= 1.02f;
        scoreText.text = $"{Score}";
    }

    public void DeathState() {
        Time.timeScale = 0;
        gameStopped = true;
        deathText.gameObject.SetActive(true);
        audioSource.Play();
    }

    private void Update() {
        if (gameStopped && Input.GetButtonDown("Jump")) {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.Q))
            Application.Quit();
    }

    IEnumerator PipeLoop() {
        while (true) {
            Instantiate(pipe, transform.position + new Vector3(11, Random.Range(-2.5f,2.5f)), Quaternion.identity, transform);
            yield return new WaitForSeconds(1/speed);
        }
    }
}