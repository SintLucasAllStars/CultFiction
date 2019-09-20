using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    public Text scoreText;
    public GameObject explanation;
    
    private void Start()
    {
        GameManager gameManager = GameManager.GetGameManager();

        if (gameManager.HasJustFinished())
        {
            Destroy(explanation);
            
            scoreText.text = $"Score: {gameManager.GetLastScore()}" +
                             '\n' +
                             $"HighScore: {gameManager.GetHighScore()}";
        }else if (gameManager.HasPlayedBefore())
        {
            scoreText.text = $"HighScore: {gameManager.GetHighScore()}";
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("MainScene");
        }
    }
    
}