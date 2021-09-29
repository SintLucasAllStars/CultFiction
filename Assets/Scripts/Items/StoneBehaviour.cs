using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBehaviour : MonoBehaviour
{
    public Animation anim;
    private int turnCount;
    public float waitDelay;
    public CoconutSpawner cS;
    public bool noNuts;
    private AudioSource squishSound;

    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
        turnCount = 0;
        anim.Play("FlyInStone");
        waitDelay = 1.1f;
        noNuts = false;
        squishSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (waitDelay >= 0f)
        {
            waitDelay -= Time.deltaTime;
        }

        if (cS.GetCoconutAmount() > 0)
        {
            TurnStone();
        }
        else if (cS.GetCoconutAmount() <= 0)
        {
            noNuts = true;
        }

    }

    void TurnStone()
    {
        if (Input.GetMouseButtonDown(0) && !anim.isPlaying && waitDelay <= 0.1f)
        {
            anim.Play("TurnRockAnim");
            transform.Translate(0f, 0f, -0.005f);
            turnCount++;
            squishSound.volume = 0.05f * turnCount;
            squishSound.pitch = 1 - 0.01f * turnCount;
            squishSound.Play();
        }

        if (turnCount == 5)
        {
            if (!anim.isPlaying)
            {
                transform.position = new Vector3(-7.4f, 1.81f, -6.62f);
                turnCount = 0;
                cS.AddCoconutWater(Random.Range(200, 300));
                cS.SetCoconutAmount();
                waitDelay = 2.5f;
            }
        }
    }
}
