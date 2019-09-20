using System;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public Animation[] animations;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            FireRay();
    }

    void FireRay()
    {
        Ray myray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(myray, out hit, Mathf.Infinity))
        {
            if (CheckTag(hit.transform.tag, "hitarea"))
            {
                gameController.instance.PatientHit();
                Destroy(hit.transform.gameObject);
                return;
            }
        }
    }

    bool CheckTag(string objectTag, string tag)
    {
        bool isHit;
        return isHit = objectTag == tag ? true : false;
    }
}