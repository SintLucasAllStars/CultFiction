using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class WeaponUI : MonoBehaviour
{
    [Header("Player Info")]
    [SerializeField] Text ammoText;
    [SerializeField] Text gunText;
    [SerializeField] Text score;
    [SerializeField] Text round;

    [Header("Health Player")]
    [SerializeField] Image healthImge;
    [SerializeField] float fadeSpeed;


    private void Start()
    {
        Gamemanager.ToggleSpawn += UpdateRound;    
    }

    public void UpdateRound(bool start)
    {
        if(start)
            round.text = Gamemanager.instance.GetRound().ToString();
    }

    public void UpdateAmmo(int currentAmmo, int ammoStockpile)
    {
        ammoText.text = currentAmmo + "/" + ammoStockpile;
        if(score.text != null || Gamemanager.instance != null)
            score.text = Gamemanager.instance.GetScore().ToString();
    }

    public void UpdateWeapon(Weapon weapon)
    {
        gunText.text = weapon.name;
        UpdateAmmo(weapon.ammo, weapon.ammoStockPile);
    }

    public void SetNewHealth(float health, float maxHealth)
    {
        Color color = healthImge.color;
        color.a = (maxHealth - health) / 100f;

        healthImge.color = color;
    }

    IEnumerator HealthDown(float currentTarget)
    {
        Color color = healthImge.color;

        for(float i = color.a; i <= currentTarget; i += Time.deltaTime / fadeSpeed)
        {
            color.a = i;
            healthImge.color = color;
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator HealthUp(float currentTarget)
    {
        Color color = healthImge.color;

        for(float i = color.a; i >= currentTarget; i -= Time.deltaTime / fadeSpeed)
        {
            color.a = i;
            healthImge.color = color;
            yield return new WaitForEndOfFrame();
        }
    }

}
