using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
    private float Coins;
    public GameObject Temple;

    public TempleBehavior templeBehaviour;
    public GameObject coinsText;

    void Update()
    {
        //Debug.Log(score);
        coinsText.GetComponent<Text>().text = "Coins: " + Coins.ToString();

        if (templeBehaviour.duckCount >= 2) Temple.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Pickup"))
        {
            templeBehaviour.duckCount++;
            Destroy(collider.gameObject);
        }

        if (collider.gameObject.CompareTag("Coin"))
        {
            Coins++;
            Destroy(collider.gameObject);
        }
    }
}
