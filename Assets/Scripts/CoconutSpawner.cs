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
    private float cooldownTimer;
    private int huskAmount;
    private int coconutAmount;
    private AudioSource throwSound;

    void Start()
    {
        spawnPos = new Vector3(-7.86f, 1.76f, -6.54f);
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        huskAmount = 20;
        coconutAmount = 0;
        throwSound = GetComponent<AudioSource>();
        UpdateStats();
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && huskAmount > 0 && cooldownTimer <= 0)
        {
            huskAmount--;
            UpdateStats();
            cooldownTimer = 0.3f;
            coconut = Instantiate(coconutPrefab, spawnPos, transform.rotation);
            coconut.GetComponent<CoconutBehaviour>().coconutSpawner = this;
            throwSound.Play();
        }
    }

    void UpdateStats()
    {
        statCount.text = "Husks: " + (huskAmount) + "/20 \nCoconuts: " + (coconutAmount);
    }

    public void AddCoconutCount()
    {
        coconutAmount++;
        UpdateStats();
    }
}
