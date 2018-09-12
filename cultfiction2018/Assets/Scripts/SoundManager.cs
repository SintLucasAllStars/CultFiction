using System.Collections;
using System.Collections.Generic;
using MathExt;
using Tools;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioClip[] SplatSounds;
    public AudioClip[] BloodSounds;
    public AudioClip[] Heartbeats;
    public AudioClip[] Screams;
    public AudioClip[] SyringeSounds;
    public AudioClip[] SyringeHittingSounds;

    private AudioSource _audioSource;
    public AudioSource HeartBeatAudioSource;
    
    private int _heartBeatLevel;

    protected override void Awake()
    {
        base.Awake();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        PlayHeartBeat();
        StartCoroutine(PlayScreems());
    }

    public void PlaySplatSounds()
    {
        PlayAudioClip(SplatSounds.GetRandom_Array());
    }

    public void PlayBloodSounds()
    {
        PlayAudioClip(BloodSounds.GetRandom_Array());
    }

    public void PlayScreams()
    {
        PlayAudioClip(Screams.GetRandom_Array());
    }

    public void IncreaseHeartBeat()
    {
        _heartBeatLevel++;
        PlayHeartBeat();
    }
    
    public void PlaySyringeSounds()
    {
        PlayAudioClip(SyringeSounds.GetRandom_Array());
    }

    public void PlaySyringeHitSound()
    {
        PlayAudioClip(SyringeHittingSounds.GetRandom_Array());
    }

    private void PlayHeartBeat()
    {
        HeartBeatAudioSource.clip = Heartbeats[_heartBeatLevel];
        HeartBeatAudioSource.loop = true;
        HeartBeatAudioSource.Play();
    }

    private IEnumerator PlayScreems()
    {
        while (true)
        {
            yield return  new WaitForSeconds(Random.Range(10f, 20f));
            PlayAudioClip(Screams.GetRandom_Array());
        }
    }



    private void PlayAudioClip(AudioClip audioClip)
    {
        _audioSource.PlayOneShot(audioClip);
    }


}
