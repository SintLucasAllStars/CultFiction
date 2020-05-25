using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject[] buttonsObjects;
    public float waitTime;

    public GameObject mouth;

    FoodManager m_foodManager;

    private void Start()
    {
        m_foodManager = FindObjectOfType<FoodManager>();

        StartCoroutine(spawnButtons());
    }

    IEnumerator spawnButtons()
    {
        for (int i = 0; i < buttonsObjects.Length; i++)
        {
            yield return new WaitForSeconds(waitTime);
            Instantiate(buttonsObjects[i], m_foodManager.GetRandomBorders(), (Quaternion.Euler(0, 180, 0)));
        }
        mouth.SetActive(true);
    }
}
