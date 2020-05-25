using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{

    [Header("Audio")]
    public AudioClip[] sounds;
    public AudioSource audioSource;

    private bool canPlaySound = true;
    

    // Update is called once per frame
    void Update()
    {
        if (canPlaySound)
        {
            canPlaySound = false;
            PlayRandom();
            StartCoroutine("SoundDelay");
        }
    }


    void PlayRandom()
    {
        audioSource.clip = sounds[Random.Range(0, sounds.Length)];
        audioSource.Play();
    }

    IEnumerator SoundDelay()
    {
        yield return new WaitForSeconds(5);
        canPlaySound = true;
    }
}
