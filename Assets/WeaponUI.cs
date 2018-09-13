using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class WeaponUI : MonoBehaviour
{
    [Header("Player Info")]
    [SerializeField]
    Text ammoText;
    [SerializeField] Text gunText;
    [SerializeField] Text score;

    [Header("Health Player")]
    [SerializeField]
    Image healthImge;
    [SerializeField] float fadeSpeed;

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

    //public void SetNewHealth(float hits, float maxHits, bool healthDown)
    //{
    //    float a = 1 - (hits / maxHits);
    //    if(healthDown)
    //    {
    //        StartCoroutine(HealthDown(a));
    //    }
    //    else
    //    {
    //        StartCoroutine(HealthDown(a));
    //    }
    //    Debug.Log(a + " " + healthDown + " " + hits + " " + maxHits);
    //}

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
