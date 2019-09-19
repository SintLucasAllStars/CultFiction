using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [Header("World Values")]
    public int amountOfTurrets;
    public int amountOfShields;
    public List<ShipTurret> turrets = new List<ShipTurret>();
    public List<ShipShieldGen> shields = new List<ShipShieldGen>();

    [Header("UI")]
    public Text currentShields;
    public Text maxShields;
    public Text currentTurrets;
    public Text maxTurrets;

    private Animator anim;

    void Start()
    {

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        anim = GetComponent<Animator>();

        turrets.AddRange(GameObject.FindObjectsOfType<ShipTurret>());
        shields.AddRange(GameObject.FindObjectsOfType<ShipShieldGen>());
        
        amountOfTurrets = turrets.Count;
        amountOfShields = shields.Count;

        maxTurrets.text = amountOfTurrets.ToString();
        maxShields.text = amountOfShields.ToString();

        UpdateCanvas();
    }

    public void ShieldDestroyed()
    {
        amountOfShields -= 1;
        if (amountOfShields < 1)
        {
            foreach (ShipTurret i in turrets)
            {
                i.GetComponentInChildren<ForceShield>().gameObject.SetActive(false);
            }
        }
        UpdateCanvas();
    }

    public void TurretDestroyed()
    {
        amountOfTurrets -= 1;
        if (amountOfTurrets < 1)
        {
            Debug.Log("Ship Nutrualized");
        }
        UpdateCanvas();
    }

    void UpdateCanvas()
    {
        currentTurrets.text = amountOfTurrets.ToString();
        currentShields.text = amountOfShields.ToString();
    }

    public void Win()
    {
        if (amountOfTurrets == 0)
        {
            anim.Play("Win");
        }
    }

    public void Lose()
    {
        anim.Play("Lose");
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(10);
        Retry();
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
