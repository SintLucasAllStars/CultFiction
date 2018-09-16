using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Iinteractable
{
    void OnClick();
    IEnumerator ActionCoroutine(Transform target);
}
