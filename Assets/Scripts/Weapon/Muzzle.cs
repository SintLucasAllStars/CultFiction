using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muzzle : MonoBehaviour
{

    [SerializeField] float fadeTime;

    Light ligthFlash;

    Material material;
    Color color;


    // Use this for initialization
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        ligthFlash = GetComponent<Light>();
        color = material.GetColor("_TintColor");
    }

    public void Enable()
    {
        StartCoroutine(MuzzleFlash());
    }

    IEnumerator MuzzleFlash()
    {
        ligthFlash.enabled = true;
        float elapsedTime = 0.0f;
        color.a = 1;
        while(elapsedTime < fadeTime)
        {
            yield return new WaitForEndOfFrame();
            elapsedTime += Time.deltaTime;
            color.a = 1.0f - Mathf.Clamp01(elapsedTime / fadeTime);
            material.SetColor("_TintColor", color);
        }
        ligthFlash.enabled = false;
    }

}
