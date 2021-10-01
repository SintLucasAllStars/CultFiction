using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] public int Hp;
    public int numhearts;

    public Image[] hearts;
    public Sprite fullheart;
    public Sprite emptyyheart;
    private float invTimer;

    void Update()
    {
        if (invTimer >= -1) {
            invTimer -= Time.deltaTime;
        }

        //hp controller and hearts display
        if (Hp > numhearts)
        {
            Hp = numhearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {

            if (i < Hp)
            {
                hearts[i].sprite = fullheart;
            }
            else
            {
                hearts[i].sprite = emptyyheart;
            }

            if (i < numhearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Enemy") && invTimer <= 0)
        {
            invTimer = 2;
            Hp--;
        }

        if (hit.gameObject.CompareTag("Water") && invTimer <= 0)
        {
            transform.position = new Vector3(162, 24, 120);
            Hp--;
        }
    }

    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.CompareTag("Water") && invTimer <= 0)
        {
            transform.position = new Vector3(162, 24, 120);
            Hp--;
        }
    }
}
