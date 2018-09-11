using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneController : MonoBehaviour, Iinteractable
{
    public int amountOfRings;

    public bool pickedUpPhone;
    public bool ringing;

    public GameManager manager;
    public GameObject dialogBox;
    public Text dialogText;

    private List<Caller> callers;
    private Caller currentCaller;

	void Start ()
    {
        pickedUpPhone = false;
        ringing = false;
        dialogBox.SetActive(false);
        GetCallers();
        StartCoroutine(PhoneCoroutine());
	}

    public void OnClick()
    {
        if (ringing)
        {
            pickedUpPhone = true;
            OpenDialog();
        }
    }

    public void OpenDialog()
    {
        dialogBox.SetActive(true);
        dialogText.text = currentCaller.dialog;
    }

    public void CloseDialog(bool accepted)
    {
        pickedUpPhone = false;
        dialogBox.SetActive(false);

        if (accepted)
        {
            if (currentCaller.isNarc)
            {
                manager.EndGame(false, "Sold to a narc you dummy!");
                return;
            }
            manager.Money += currentCaller.payment;
        }
        else
        {
            Debug.Log("denied weed");
            if (!currentCaller.isNarc)
            {
                manager.ClientsDenied++;
            }
        }

        callers.Remove(currentCaller);

        if (callers.Count <= 0)
        {
            manager.EndGame(true, "No more callers left!");
            return;
        }

        StartCoroutine(PhoneCoroutine());
    }

    private void Ring()
    {
        Debug.Log("RING!");
    }

    private void GetCallers()
    {
        callers = new List<Caller>();
        TextAsset[] textAssets = Resources.LoadAll<TextAsset>("Callers");
        foreach(TextAsset asset in textAssets)
        {
            callers.Add(JsonUtility.FromJson<Caller>(asset.text));
        }
    }

    public void StopCoroutines()
    {
        StopAllCoroutines();
    }

    private IEnumerator PhoneCoroutine()
    {
        currentCaller = callers[Random.Range(0, callers.Count)];

        yield return new WaitForSeconds(3f);

        ringing = true;

        // start ringing phone, user has a chance to pick up phone for the duration of this loop
        int timesRang = 0;
        while(!pickedUpPhone)
        {
            // if user did not pick up the phone, denied client will be added to manager and coroutine will be called again
            if (timesRang >= amountOfRings)
            {
                ringing = false;
                manager.ClientsDenied++;
                Debug.Log("Didn't pick up phone");

                if(manager.GameRunning)
                    StartCoroutine(PhoneCoroutine());

                yield break;
            }

            timesRang++;
            Ring();

            Debug.Log("test");
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
