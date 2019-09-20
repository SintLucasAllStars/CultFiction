using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Interactable : MonoBehaviour
{
    protected void OnMouseDown() => Interact();

    protected virtual bool Interact()
    {
        if (IsPointerOverUIObject())
            return false;

        SoundEffectManager.Instance.PlaySound(SoundEffectName.Select);
        return true;
    }

    protected bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
