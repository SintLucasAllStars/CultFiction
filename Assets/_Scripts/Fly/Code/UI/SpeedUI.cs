
using UnityEngine;
using UnityEngine.UI;

public class SpeedUI : MonoBehaviour
{
    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        if (text != null && Ship.PlayerShip != null)
        {
            text.text = string.Format("THR: {0}\nSPD: {1}", (Ship.PlayerShip.Throttle * 100.0f).ToString("000"), Ship.PlayerShip.Velocity.magnitude.ToString("000"));
        }
    }
}
