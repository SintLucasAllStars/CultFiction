using System;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private Transform shootPoint;
    private int mistakes;
    public int Mistakes
    {
        get
        {
            return mistakes;
        }
        set
        {
            if (value > 3)
            {
                NoLivesLeft();
            }
            else
            {
                mistakes = value;
            }
        }
    }

    public Animation[] animations;


    void Start()
    {
        shootPoint = this.transform.GetChild(0).transform;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            fireRay();
    }

    void fireRay()
    {
        Ray myray = new Ray(shootPoint.position, shootPoint.transform.position);
        RaycastHit hit;

        Debug.DrawRay(shootPoint.position, shootPoint.transform.position, Color.blue);

        if (Physics.Raycast(myray, out hit, Mathf.Infinity))
        {
            if (checkTag(hit.transform.tag, "hitarea"))
            {
                gameController.instance.PatientHit();
                return;
            }
            else if (checkTag(hit.transform.tag, "phone"))
            {
                // gameController.instance call clip
                return;
            }

        }
    }

    bool checkTag(string objectTag, string tag)
    {
        bool isHit;
        return isHit = objectTag == tag ? true : false;
    }

    private void NoLivesLeft()
    {
        //using it like this so that I don't need to imported over the whole script
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    void PlayAnimation(Animation anim)
    {
        anim.Play();
    }
}