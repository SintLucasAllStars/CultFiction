using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreManagement : MonoBehaviour
{
    public static ScoreManagement Instance;

    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Text _highScoreText;

    [SerializeField]
    private GameObject _canvas;

    private int _score;
    private int _highscore;

    private void Start()
    {
        if (ScoreManagement.Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(this.gameObject);
            ScoreManagement.Instance._score = 0;
            ScoreManagement.Instance._scoreText.text = "Score: " + _score;
        }
    }

    public void ToggleUI()
    {
        StartCoroutine(ShowUI());
    }

    private IEnumerator ShowUI()
    {
        yield return new WaitForEndOfFrame();
        _canvas.SetActive(!_canvas.activeInHierarchy);
    }
     
    public void CheckScore()
    {
        _score++;
        if (_highscore < _score)
        {
            _highscore = _score;
            _highScoreText.text = "Highscore: " + _highscore;
        }
        _scoreText.text = "Score: " + _score;
    }
}
