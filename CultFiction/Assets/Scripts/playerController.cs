using UnityEngine;

public class playerController : MonoBehaviour
{

    public Animation[] animations;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            fireRay();
        }
    }

    void fireRay()
    {
        Ray myray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(myray, out hit, Mathf.Infinity))
        {
            if (checkTag(hit.transform.tag, "hitarea"))
            {
                Debug.Log("hitarea: has been hit");
                // PlayAnimation(animations[0]);
            }
            return;
        }

    }

    bool checkTag(string objectTag, string tag)
    {
        bool isHit;
        return isHit = objectTag == tag ? true : false;
    }

    void PlayAnimation(Animation anim)
    {
        anim.Play();
    }
}