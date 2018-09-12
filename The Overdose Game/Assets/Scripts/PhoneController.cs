using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneController : MonoBehaviour, Iinteractable
{
    public int amountOfRings;
    public int maxAmountOfSecondsDialog;
    public float dialogWriteSpeed;

    public bool pickedUpPhone;
    public bool ringing;

    public GameManager manager;
    public GameObject dialogBox;
    public Text dialogText;
    public Text dialogTimer;

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
        Debug.Log("test");
        if (ringing)
        {
            pickedUpPhone = true;
            OpenDialog();
        }
    }

    public void OpenDialog()
    {
        dialogBox.SetActive(true);
        StartCoroutine(DialogWriterCoroutine(currentCaller.dialog));
        StartCoroutine(DialogTimerCoroutine());
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

    private void UpdateDialogTimer(int secondsElapsed)
    {
        dialogTimer.text = secondsElapsed + "/" + maxAmountOfSecondsDialog;
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

            yield return new WaitForSeconds(2f);
        }

        ringing = false;
    }

    private IEnumerator DialogTimerCoroutine()
    {
        int time = 0;
        UpdateDialogTimer(time);
        yield return new WaitForSeconds(1f);

        while (pickedUpPhone)
        {
            if(time >= maxAmountOfSecondsDialog)
            {
                CloseDialog(false);
            }

            time++;
            UpdateDialogTimer(time);
            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator DialogWriterCoroutine(string dialog)
    {
        dialogText.text = "";
        for(int i = 0; i < dialog.Length; i++)
        {
            dialogText.text += dialog[i];
            yield return new WaitForSeconds(dialogWriteSpeed);
        }
    }
}

public struct Caller
{
    public bool isNarc;
    public int payment;
    public string dialog;
}
