using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject mouth;

    public int maxHealth = 3;
    public int currHealth = 3;

    public int maxFood = 100;
    public int currFood = 100;

    public int loseFoodPerSecond = 1;

    [Space(5)]

    public float shakeDuration;
    public float shakeMagnitude;

    [Space(5)]

    public GameObject[] hearts = new GameObject[3];
    public Slider foodSlider;

    [Header("SoundFX")]
    AudioSource audioSource;

    public AudioClip hurtSound;
    public AudioClip eatSound;

    DragRigidbody m_dragRigidbody;
    FoodManager m_foodManager;

    private void Start()
    {
        m_dragRigidbody = FindObjectOfType<DragRigidbody>();
        m_foodManager = FindObjectOfType<FoodManager>();
        audioSource = GetComponent<AudioSource>();

        StartCoroutine(LoseFoodOverTime());
    }

    public void UpdateFood(int foodPoints)
    {
        if (currFood + foodPoints < maxFood)
            currFood += foodPoints;
        else
            currFood = maxFood;
        
        if(foodPoints > 0)
            audioSource.PlayOneShot(eatSound);

        foodSlider.value = currFood;
        // player died if currFood hit 0 or lower
        if(currFood <= 0)
            PlayerDie();
    }

    public void UpdateHealth(int damage)
    {
        if (currHealth - damage > 0)
        {
            currHealth -= damage;
            
        }
        else
        {
            // player died
            currHealth = 0;

            // Change scene
            PlayerDie();
        }

        if (currHealth >= 0)
            hearts[currHealth].SetActive(false);

        m_foodManager.ExplodeForceOnFood();

        audioSource.PlayOneShot(hurtSound);
        StartCoroutine(Shake(shakeDuration, shakeMagnitude));
    }

    private void Update()
    {
        if (m_dragRigidbody.coroutineIsRunning)
            mouth.SetActive(true);
        else
            mouth.SetActive(false);
    }

    private void PlayerDie()
    {
        StartCoroutine(Shake(shakeDuration * 10, shakeMagnitude * 10));
        // play some crashing sounds
        SceneManager.LoadSceneAsync("GameEnd");
    }

    IEnumerator LoseFoodOverTime()
    {
        yield return new WaitForSeconds(loseFoodPerSecond);
        UpdateFood(-1);
        StartCoroutine(LoseFoodOverTime());
    }


    IEnumerator Shake(float duration, float magnitude)
    {
        float elapsed = 0.0f;

        Transform cameraTrans = Camera.main.transform;
        Vector3 originalCamPos = cameraTrans.position;

        while (elapsed < duration)
        {

            elapsed += Time.deltaTime;

            float percentComplete = elapsed / duration;
            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

            // map value to [-1, 1]
            float x = Random.value * 2.0f - 1.0f;
            float y = Random.value * 2.0f - 1.0f;
            x *= magnitude * damper;
            y *= magnitude * damper;

            x += cameraTrans.position.x;
            y += cameraTrans.position.y;

            cameraTrans.position = new Vector3(x, y, cameraTrans.position.z);

            yield return null;
        }

        cameraTrans.localPosition = new Vector3(4, 5, cameraTrans.position.z);
    }
}