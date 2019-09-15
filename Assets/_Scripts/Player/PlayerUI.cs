using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Transform thisShip;
    [SerializeField] float shipRotationX;
    [SerializeField] float shipRotationZ;

    [Header("UI Info")]
    public bool alarm;
    public Text rotYText;
    public Text rotZText;
    public Text targetText;
    public Text reloadText;
    public Animator verticalBar;
    public Animator horizontalBar;
    public GameObject warning;
    
    private PlayerFlyController playerRef;
    private PlayerCombat playerCombatRef;

    private void Start()
    {
        playerRef = GameObject.FindObjectOfType<PlayerFlyController>();
        playerCombatRef = GameObject.FindObjectOfType<PlayerCombat>();
    }

    void Update()
    {
        DisplayInfo();
        SideBarAnimations();
        ActivateAlarm();
    }

    void DisplayInfo()
    {
        rotYText.text = thisShip.eulerAngles.y.ToString();
        rotZText.text = thisShip.eulerAngles.z.ToString();
    }

    void SideBarAnimations()
    {
        verticalBar.speed = Mathf.Abs(Input.GetAxis("Horizontal"));
        horizontalBar.speed = Mathf.Abs(Input.GetAxis("Vertical"));
    }

    public void ActivateAlarm()
    {
        if (playerRef.rb.velocity != Vector3.zero && alarm == false)
        {
            alarm = true;
            warning.SetActive(true);
        }
    }

    public void DisableAlarm()
    {
        alarm = false;
        warning.SetActive(false);
    }

    public void StartReloadTimer()
    {
        StartCoroutine(ReloadTimer());
    }

    IEnumerator ReloadTimer()
    {
        reloadText.text = "Reloading";
        yield return new WaitForSeconds(5);
        reloadText.text = "Missiles Ready";
        playerCombatRef.reloading = false;
    }

}
