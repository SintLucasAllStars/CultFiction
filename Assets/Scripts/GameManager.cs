using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject platform;
    [SerializeField]
    private int startPlatformLength;
    [SerializeField]
    private List<string> childrenNames;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI countDownText;
    [SerializeField]
    private TextMeshProUGUI deathScoreText;
    [SerializeField]
    private GameObject _scorebord;
    [SerializeField]
    private GameObject _deathScreen;
    [SerializeField]
    private GameObject _menu;
    [SerializeField] 
    private GameObject _tutorial;

    private GameObject previousPlatform;
    private List<GameObject> allPlatforms = new List<GameObject>();
    private bool previousAnimDirection;
    private bool previousDirection;
    public bool gameStarted;

    private int amountLeft;
    private int amountRight;

    private System.Random rnd = new System.Random();

    public static GameManager gameManager;
    
    public PlayerData data;
    
    private bool scoreDelay;

    private void Awake()
    {
        if (gameManager == null) gameManager = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        _menu.GetComponentInChildren<Canvas>().enabled = true;
        GeneratePlatform(Vector3.zero - Vector3.zero, false);
    }

    private void Update()
    {
        if (scoreDelay && gameStarted)
        {
            scoreDelay = !scoreDelay;
            StartCoroutine(SetScoreDelay());
            data.score = data.FloatToStringList(float.Parse(string.Join("", data.score)) + 1);
            scoreText.text = string.Join("", data.score);
            data.speed = data.FloatToStringList(float.Parse(string.Join("", data.speed)) + .1f);
        }
    }

    IEnumerator SpawnPlatform()
    {
        //Generate the first platform with a 2 grid offset;
        previousPlatform = GeneratePlatform(Vector3.zero - Vector3.forward * 3 * 2, false);
        
        allPlatforms.Add(previousPlatform);
        
        //Create the next platforms
        for (int i = 0; i < startPlatformLength; i++)
        {
            Transform platformChild = extractPositionChild(previousPlatform, previousDirection);
            
            //Generate next platform
            previousPlatform = i > 3? GenerateAnimatedPlatform(platformChild.position, previousAnimDirection? "Left":"Right"):GeneratePlatform(platformChild.position, false);
            allPlatforms.Add(previousPlatform);
            if(i > 3) yield return new WaitForSeconds(3/4);
        }
    }
    
    IEnumerator SetScoreDelay()
    {
        yield return new WaitForSeconds(4/float.Parse(string.Join("", data.speed)));
        scoreDelay = !scoreDelay;
    }

    IEnumerator StartGame()
    {
        data = new PlayerData(2);
        StartCoroutine(SpawnPlatform());
        scoreText.text = "0";
        countDownText.text = "3";
        yield return new WaitForSeconds(1);
        countDownText.text = "2";
        yield return new WaitForSeconds(1);
        countDownText.text = "1";
        yield return new WaitForSeconds(1);
        countDownText.text = "";
        gameStarted = true;
        scoreDelay = true;
    }

    public void RestartGame()
    {
        foreach (var ap in allPlatforms) Destroy(ap);
        if (allPlatforms.Count > 0) allPlatforms.Clear();
        previousDirection = false;
        deathScoreText.text = "Your score: ";
        _deathScreen.GetComponentInChildren<Canvas>().enabled = false;
        _scorebord.GetComponentInChildren<Canvas>().enabled = true;
        StartCoroutine(StartGame());
    }

    public void StartGameFromMenu()
    {
        _menu.GetComponentInChildren<Canvas>().enabled = false;
        _scorebord.GetComponentInChildren<Canvas>().enabled = true;
        StartCoroutine(StartGame());
    }

    public void GoToMenu()
    {
        _deathScreen.GetComponentInChildren<Canvas>().enabled = false;
        _tutorial.GetComponentInChildren<Canvas>().enabled = false;
        _menu.GetComponentInChildren<Canvas>().enabled = true;
    }

    public void GoToTutorial()
    {
        _menu.GetComponentInChildren<Canvas>().enabled = false;
        _tutorial.GetComponentInChildren<Canvas>().enabled = true;
    }

    public void DeathScreen()
    {
        _scorebord.GetComponentInChildren<Canvas>().enabled = false;
        _deathScreen.GetComponentInChildren<Canvas>().enabled = true;
        deathScoreText.text += string.Join("", data.getScore());
    }

    public void CollisionCaller()
    {
        if (amountLeft == 0 && amountRight == 0)
        {
            previousDirection = !previousDirection;
            if (previousDirection) amountLeft = rnd.Next(1, 10);
            else amountRight = rnd.Next(1, 10);
        }

        if (amountLeft > 0)
        {
            previousPlatform = GenerateAnimatedPlatform(extractPositionChild(previousPlatform, previousDirection).position, previousAnimDirection? "Left":"Right");
            allPlatforms.Add(previousPlatform);
            amountLeft--;
        }

        if (amountRight > 0)
        {
            previousPlatform = GenerateAnimatedPlatform(extractPositionChild(previousPlatform, previousDirection).position, previousAnimDirection? "Left":"Right");
            allPlatforms.Add(previousPlatform);
            amountRight--;
        }

        if (allPlatforms.Count > 15)
        {
            Destroy(allPlatforms[0]);
            allPlatforms.RemoveAt(0);
        }
    }

    Transform extractPositionChild(GameObject originPlatform, bool direction)
    {
        //Create a temp variable to save a transform
        Transform platformChild = originPlatform.transform;

        //Loop until you have the right child
        foreach (var childName in childrenNames.Append(direction? "Left":"Right"))
        {
            //Get the child transform
            platformChild = platformChild.transform.Find(childName);
        }

        return platformChild;
    }

    GameObject GeneratePlatform(Vector3 position, bool enableCollider)
    {
        GameObject gp = Instantiate(platform, position, Quaternion.identity);
        gp.transform.Find("Collider").gameObject.SetActive(enableCollider);
        return gp;
    }

    GameObject GenerateAnimatedPlatform(Vector3 position, string side)
    {
        //Start animation
        GameObject ap = GeneratePlatform(position, true);
        Animator apa = ap.GetComponent<Animator>();
        apa.SetFloat(side, float.Parse(string.Join("", data.speed)) * .5f);
        apa.Play($"PlatformAnimation{side}");
        previousAnimDirection = !previousAnimDirection;
        return ap;
    }
}
