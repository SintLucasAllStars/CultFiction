using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
    private float score = 0;
    private float Coins;

    public GameObject coinsText;

    void Update()
    {
        //Debug.Log(score);
        coinsText.GetComponent<Text>().text = "Coins: " + Coins.ToString();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Pickup"))
        {
            score++;
            Destroy(collider.gameObject);
        }

        if (collider.gameObject.CompareTag("Coin"))
        {
            Coins++;
            Destroy(collider.gameObject);
        }
    }
}
