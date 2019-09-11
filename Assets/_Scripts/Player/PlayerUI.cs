using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Transform thisShip;
    public float shipRotationX;
    public float shipRotationZ;

    [Header("UI Info")]
    public Text rotYText;
    public Text rotZText;
    public Animator verticalBar;
    public Animator horizontalBar;

    void Update()
    {
        DisplayInfo();
        SideBarAnimations();
    }

    void DisplayInfo()
    {
        rotYText.text = thisShip.eulerAngles.y.ToString();
        rotZText.text = thisShip.eulerAngles.z.ToString();
    }

    void SideBarAnimations()
    {
        verticalBar.speed = Input.GetAxis("Vertical");
        horizontalBar.speed = Input.GetAxis("Horizontal");
    }

}
