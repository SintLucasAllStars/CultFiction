using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    protected void OnMouseDown() => Interact();

    protected virtual bool Interact()
    {
        SoundEffectManager.Instance.PlaySound(SoundEffectName.Select);
        return true;
    }
}
