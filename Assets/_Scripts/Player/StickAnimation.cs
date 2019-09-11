using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickAnimation : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        SetAxis();
    }

    void SetAxis()
    {
        anim.SetFloat("Vertical", Input.GetAxis("Vertical"));
        anim.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
    }

}