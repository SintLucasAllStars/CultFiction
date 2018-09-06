using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoconutManager : MonoBehaviour
{

    public GameObject nut;
    float power = 100f;
    float cooldown = 1.5f;
    bool canFire = true;

    private void Update()
    {
        if (Input.GetMouseButton(0) && canFire)
        {
            GameObject n = Instantiate(nut, FindObjectOfType<Camera>().gameObject.transform.position, Quaternion.identity);
            Rigidbody rb = n.GetComponent<Rigidbody>();
            Debug.Log(Camera.main.ViewportToWorldPoint(Input.mousePosition));
            rb.AddForce((-Camera.main.ViewportToWorldPoint(Input.mousePosition * 10)) * power);
            canFire = false;
            StartCoroutine(Cooldown(cooldown));
            Destroy(n, 3f);

        }
    }

    IEnumerator Cooldown(float down)
    {
        yield return new WaitForSeconds(down);
        canFire = true;
    }
}
