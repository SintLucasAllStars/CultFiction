using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnPlayer : MonoBehaviour
{
    public static SoundOnPlayer brain;
    public AudioClip impact;
    AudioSource audioSource;

    void Awake()
    {
        if(SoundOnPlayer.brain == null)
        {
            SoundOnPlayer.brain = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        audioSource.PlayOneShot(impact, 0.7F);
    }
}
