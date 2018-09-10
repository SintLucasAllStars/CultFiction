using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneController : MonoBehaviour, Iinteractable
{
    public bool pickedUpPhone;
    public bool ringing;
    private GameObject dialogBox;

	void Start ()
    {
        pickedUpPhone = false;
        ringing = false;
        dialogBox = GameObject.Find("DialogBox");
        dialogBox.SetActive(false);
        StartCoroutine(PhoneCoroutine());
	}

    public void OnClick()
    {
        if (ringing)
        {
            pickedUpPhone = true;
            dialogBox.SetActive(true);
        }
    }

    public void CloseDialog(bool accepted)
    {
        pickedUpPhone = false;
        dialogBox.SetActive(false);

        if (accepted)
        {
            Debug.Log("sold some weed");
        }
        else
        {
            Debug.Log("denied weed");
        }

        StartCoroutine(PhoneCoroutine());
    }

    private void Ring()
    {
        Debug.Log("RING!");
    }

    private IEnumerator PhoneCoroutine()
    {
        yield return new WaitForSeconds(3f);

        ringing = true;

        while (!pickedUpPhone)
        {
            Ring();
            yield return new WaitForSeconds(2f);
        }

        ringing = false;
    }
}
