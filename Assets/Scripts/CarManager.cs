using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarManager : MonoBehaviour
{
    public static CarManager instance;

    public carStates carState;
    private carStates lastCarState;
    AudioSource audio;
    Rigidbody rg;
    public AudioClip[] carSounds;

    public void Awake()
    {
        instance = this;
        DontDestroyOnLoad(instance);
    }
    void Start()
    {
        audio = GetComponent<AudioSource>();
        rg = GetComponent<Rigidbody>();
        audio.Play();
    }

    void Update()
    {
        audio.pitch = map(rg.velocity.magnitude, 0, 16, 0.8f, 2.3f);
        audio.clip = carSounds[(int)carState];
        if(carState != lastCarState)
        {
            lastCarState = carState;
            audio.Play();
        }
    }

    float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
}
