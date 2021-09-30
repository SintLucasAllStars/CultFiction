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

    void Start()
    {
        spawnPos = new Vector3(-7.86f, 1.76f, -6.54f);
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        
        huskAmount = 20;
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
    }

    void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && huskAmount > 0 && cooldownTimer <= 0)
        {
            huskAmount--;
            UpdateStats();
            cooldownTimer = 0.3f;
            coconut = Instantiate(coconutPrefab, spawnPos, transform.rotation);
            coconut.GetComponent<CoconutBehaviour>().cS = this;
            throwSound.Play();
        }
    }

    void UpdateStats()
    {
        statCount.text = "Husks: " + huskAmount + "/20 \nCoconuts: " + coconutAmount;
        waterCount.text = "Coconut Water:\n" + coconutWater + "/2000ML";
        barFill.sizeDelta = new Vector2(50, Mathf.Clamp((350f / 2000f) * coconutWater,0f,350f));
    }

    public void AddCoconutCount()
    {
        coconutAmount++;
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
