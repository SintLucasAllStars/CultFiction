using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneController : MonoBehaviour, Iinteractable
{
    public bool pickedUpPhone;
    public bool ringing;
    private GameObject dialogBox;
    private List<Caller> callers;

	void Start ()
    {
        pickedUpPhone = false;
        ringing = false;
        dialogBox = GameObject.Find("DialogBox");
        dialogBox.SetActive(false);
        GetCallers();
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

    private void GetCallers()
    {
        TextAsset test = Resources.Load<TextAsset>("Callers/test");
        Caller testCaller = JsonUtility.FromJson<Caller>(test.text);
        Debug.Log(testCaller.dialog);
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

public struct Caller
{
    public bool isNarc;
    public int payment;
    public string dialog;
}
