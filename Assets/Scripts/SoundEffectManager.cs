using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundEffectName
{
    PurchaseItem,
    Button,
    Error,
    Money,
    Select,
    GameOver
}

[RequireComponent(typeof(AudioSource))]
public class SoundEffectManager : Singleton<SoundEffectManager>
{
    private AudioSource _audioSource = null;

    [SerializeField]
    private List<SoundEffect> _soundEffects = new List<SoundEffect>();

    private void Start() => _audioSource = GetComponent<AudioSource>();

    public void PlaySound(SoundEffectName soundEffect) => _audioSource.PlayOneShot(_soundEffects.Find(x => x.SoundEffectName == soundEffect).AudioClip);
}

[System.Serializable]
public class SoundEffect
{
    [SerializeField]
    private SoundEffectName _soundEffectName = SoundEffectName.GameOver;
    public SoundEffectName SoundEffectName => _soundEffectName;

    [SerializeField]
    private AudioClip _audioClip = null;
    public AudioClip AudioClip => _audioClip;
}