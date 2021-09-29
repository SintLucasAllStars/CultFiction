using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NutBehaviour : MonoBehaviour
{
    public Animation anim;
    public StoneBehaviour sB;
    private bool playOnce;
    private ParticleSystem pS;

    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
        playOnce = false;
        pS = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        float waitD = sB.waitDelay;
        if (waitD >= 2.4f && !sB.noNuts)
        {
            anim.Play("TiltNut");
            pS.Play();
        }
        else if (sB.noNuts)
        {
            if (!playOnce)
            {
                anim.Play("TiltNutFinal");
                playOnce = true;
            }
            if (!anim.isPlaying)
            {
                Destroy(this.gameObject);
            }
        }    
    }
}
