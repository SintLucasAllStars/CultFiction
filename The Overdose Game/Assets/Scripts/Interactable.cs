using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public Vector3 originalScale;
    private Coroutine lerpCoroutine;

    public virtual void OnMouseEnter()
    {
        if (lerpCoroutine != null)
            StopCoroutine(lerpCoroutine);
        lerpCoroutine = StartCoroutine(LerpScaleCoroutine(originalScale + new Vector3(10, 10, 10)));
    }

    public virtual void OnMouseExit()
    {
        if (lerpCoroutine != null)
            StopCoroutine(lerpCoroutine);
        lerpCoroutine = StartCoroutine(LerpScaleCoroutine(originalScale));
    }

    public virtual void OnMouseDown()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            OnClick();
        }
    }

    // Component specific behaviour of derived class goes here
    public abstract void OnClick();

    // Component specific behaviour and animation of derived class go here
    public abstract IEnumerator ActionCoroutine(Transform target);

    // Default implementation of object highlighting using scale
    private IEnumerator LerpScaleCoroutine(Vector3 scale)
    {
        while (Vector3.Distance(transform.localScale, scale) > 0.01f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, scale, 10 * Time.deltaTime);
            yield return null;
        }

        transform.localScale = scale;
    }
}
