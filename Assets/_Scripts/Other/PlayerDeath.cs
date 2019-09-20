using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using  UnityEngine.UI;
public class PlayerDeath : MonoBehaviour
{
    public int Health;
    public Text healthText;
    private void Start()
    {
        Health = 100;
      
      
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Health--;
            healthText.text = "Health" + Health;
            if (Health == 0 || Health < 0)
            {
                Dies();
            }
        }
    }

    private void Dies()
    {
        SceneManager.LoadScene("Dies");

    }
}
