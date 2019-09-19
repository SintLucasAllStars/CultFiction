using TMPro;
using UnityEngine;

public class ErrorMessage : Singleton<ErrorMessage>
{
    private TextMeshProUGUI _text;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void AnnounceError(string error)
    {
        _animator.SetTrigger("Appear");
        SoundEffectManager.Instance.PlaySound(SoundEffectName.Error);
        _text.text = error;
    }
}
