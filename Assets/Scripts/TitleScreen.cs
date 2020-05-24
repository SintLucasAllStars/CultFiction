using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{

    [SerializeField] private Text m_CreditsText;
    [SerializeField] private Button m_CreditsButton;
    [SerializeField] private Button m_StartButton;
    [SerializeField] private Button m_BackButton;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LevelManager.instance.LevelChange("Quit");
        }
    }

    public void StartGame()
    {
        LevelManager.instance.LevelChange("Game");
    }

    public void Credits()
    {
        m_CreditsText.gameObject.SetActive(true);
        m_BackButton.gameObject.SetActive(true);
        m_StartButton.gameObject.SetActive(false);
        m_CreditsButton.gameObject.SetActive(false);
    }

    public void Title()
    {
        m_CreditsText.gameObject.SetActive(false);
        m_BackButton.gameObject.SetActive(false);
        m_StartButton.gameObject.SetActive(true);
        m_CreditsButton.gameObject.SetActive(true);
    }
}
