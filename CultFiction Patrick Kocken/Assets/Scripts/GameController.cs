using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : MonoBehaviour {

    public static GameController instance;

    public UIScript UI;
    public LightingScript Lighting;
    public EnvironmentScript Environment;
    public CameraMovementScript CameraMovement;
    public MouseScript Mouse;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        UI.StartFade(false);
    }
	public void StartGame(){

        UI.IntroScroll();
        Environment.StartFadingEnvironment();
        CameraMovement.enabled = true;
        Lighting.StartChangingIntensity(3.75f, 0);
    }
    public void EndGame(){
        Mouse.enabled = false;
        UI.StartFade(true);
    }
    public void StartDestroyingTemple(){
        
    }


}
