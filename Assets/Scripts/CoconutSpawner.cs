using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoconutSpawner : MonoBehaviour
{
    public GameObject coconutPrefab;
    private Vector3 spawnPos;
    public static GameObject mainCamera;
    private GameObject coconut;
    public Text statCount;
    public Text waterCount;
    private float cooldownTimer;

    private int huskAmount;
    private int coconutAmount;
    private int totalCoconutAmount;
    private int coconutWater;

    private AudioSource throwSound;
    public CameraMovement cameraMovement;
    bool runOnce;
    bool camDone;
    public GameObject stonePrefab;
    public GameObject stonePrefabInstance;
    public GameObject nutPrefab;
    public GameObject nutPrefabInstance;
    public RectTransform barFill;

    public GameObject endCanvas;
    public GameObject gameCanvas;
    public GameObject inGameMenuCanvas;
    public Text statBoxText;
    public Text resultText;

    void Start()
    {
        spawnPos = new Vector3(-7.86f, 1.76f, -6.54f);
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        
        huskAmount = 16;
        coconutAmount = 0;
        coconutWater = 0;

        throwSound = GetComponent<AudioSource>();
        runOnce = false;
        camDone = false;
        UpdateStats();
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;
        GetInput();
        MoveCamera();
        ShowEndScreen();
    }

    void GetInput()
    {
        if (cameraMovement.blockInput)
        {
            cooldownTimer = 4f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && huskAmount > 0 && cooldownTimer <= 0 && !cameraMovement.blockInput)
        {
            huskAmount--;
            UpdateStats();
            cooldownTimer = 0.3f;
            coconut = Instantiate(coconutPrefab, spawnPos, transform.rotation);
            coconut.GetComponent<CoconutBehaviour>().cS = this;
            throwSound.Play();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && gameCanvas.activeSelf && !inGameMenuCanvas.activeSelf)
        {
            inGameMenuCanvas.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && inGameMenuCanvas.activeSelf)
        {
            inGameMenuCanvas.SetActive(false);
        }
    }

    void UpdateStats()
    {
        statCount.text = "Husks: " + huskAmount + "/16 \nCoconuts: " + coconutAmount;
        waterCount.text = "Coconut Water:\n" + coconutWater + "/2000ML";
        barFill.sizeDelta = new Vector2(50, Mathf.Clamp((350f / 2000f) * coconutWater,0f,350f));
    }

    public void AddCoconutCount()
    {
        coconutAmount++;
        totalCoconutAmount++;
        UpdateStats();
    }

    void MoveCamera()
    {
        
        if (huskAmount <= 0 && !camDone)
        {      
            cameraMovement.blockInput = true;

            if (!runOnce)
            {
                //mainCamera.transform.eulerAngles = new Vector3(0, mainCamera.transform.eulerAngles.y, 0);
                runOnce = true;
            }
            
            if (mainCamera.transform.eulerAngles.y <= 90)
            {
                mainCamera.transform.Rotate(new Vector3(0, 0.1f, 0));
            }
            
            if (mainCamera.transform.eulerAngles.y >= 90)
            {
                cameraMovement.camAngle = 90;
                cameraMovement.blockInput = false;
                camDone = true;
                StartCocoMilk();
            }
        }
    }

    void StartCocoMilk()
    {
        StoneBehaviour stn;
        stonePrefabInstance = Instantiate(stonePrefab);
        stn = stonePrefabInstance.GetComponent<StoneBehaviour>();
        stn.cS = this;
        if (coconutAmount > 0)
        {
            nutPrefabInstance = Instantiate(nutPrefab);
            nutPrefabInstance.GetComponent<NutBehaviour>().sB = stn;
        }
    }

    void ShowEndScreen()
    {
        if (huskAmount <= 0 && coconutAmount <= 0)
        {
            gameCanvas.SetActive(false);
            endCanvas.SetActive(true);

            if (coconutWater >= 2000)
            {
                resultText.text = "You're staying hydrated!";
            }
            else if (coconutWater >= 1900 && coconutWater <= 1999)
            {
                resultText.text = "You've got barely enough water! Try getting more!";
            }
            else if (coconutWater <= 1899)
            {
                resultText.text = "You're dehydrated!";
            }

            if (coconutWater >= 2000)
            {
                statBoxText.text = "You've collected " + totalCoconutAmount + " coconuts.\nThese gave you " + coconutWater + "ML of coconut water.\nYou have " + (coconutWater-2000) + "ML more than needed.";
            }
            else if (coconutWater <= 1999)
            {
                statBoxText.text = "You've collected " + totalCoconutAmount + " coconuts.\nThese gave you " + coconutWater + "ML of coconut water.\nYou have " + (2000-coconutWater) + "ML less than needed.";
            }
            
        }
    }

    public int GetCoconutAmount()
    {
        return coconutAmount;
    }

    public void SetCoconutAmount()
    {
        coconutAmount--;
        UpdateStats();
    }

    public void AddCoconutWater(int amount)
    {
        coconutWater += amount;
    }
}
