using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneController : MonoBehaviour, Iinteractable
{
    public AudioClip phoneRing;
    public AudioClip phonePickup;
    public AudioClip dialogBeep;
    public AudioClip acceptSound;
    public AudioClip denySound;
    public AudioClip notPickUpSound;
    public AudioSource phoneAudio;

    public int amountOfRings;
    public int maxAmountOfSecondsDialog;
    public float dialogWriteSpeed;

    // cached version of unaltered dialog for use of external text scramblers to make sure text wont get overscrambled
    public string unalteredCurrentDialog;
    public string currentDialog;
    public bool scrambledText;

    public JointController textManipulator;
    public GameManager manager;
    public GameObject dialogBox;
    public Text dialogText;
    public Text dialogTimer;

    private bool pickedUpPhone;
    private bool ringing;
    private List<Caller> callers;
    private Caller currentCaller;

    private Coroutine currentPhoneCoroutine;
    private Coroutine currentDialogTimerCoroutine;
    private Coroutine currentDialogWriterCoroutine;

    public bool PickedUpPhone
    {
        get
        {
            return pickedUpPhone;
        }
    }

	void Start ()
    {
        scrambledText = false;
        pickedUpPhone = false;
        ringing = false;
        dialogBox.SetActive(false);
        GetCallers();
        currentPhoneCoroutine = StartCoroutine(PhoneCoroutine());
	}

    public void OnClick()
    {
        if (ringing && !pickedUpPhone)
        {
            OpenDialog();
        }
    }

    public void OpenDialog()
    {
        StopCoroutine(currentPhoneCoroutine);
        phoneAudio.Stop();
        ringing = false;
        pickedUpPhone = true;
        phoneAudio.PlayOneShot(phonePickup);
        dialogBox.SetActive(true);

        unalteredCurrentDialog = currentCaller.dialog;
        currentDialog = textManipulator.Active ? textManipulator.ScrambleText(unalteredCurrentDialog) : unalteredCurrentDialog;

        currentDialogWriterCoroutine = StartCoroutine(DialogWriterCoroutine());
        currentDialogTimerCoroutine = StartCoroutine(DialogTimerCoroutine());
    }

    public void CloseDialog(bool accepted)
    {
        StopCoroutine(currentDialogWriterCoroutine);
        StopCoroutine(currentDialogTimerCoroutine);

        phoneAudio.Stop();
        pickedUpPhone = false;
        dialogBox.SetActive(false);

        if (accepted)
        {
            phoneAudio.PlayOneShot(acceptSound);
            if (currentCaller.isNarc)
            {
                manager.EndGame(false, "Sold to a narc you dummy!");
                return;
            }
            manager.Money += currentCaller.payment;
        }
        else
        {
            phoneAudio.PlayOneShot(denySound);
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

        phoneAudio.PlayOneShot(notPickUpSound);

        if(manager.GameRunning)
            currentPhoneCoroutine = StartCoroutine(PhoneCoroutine());
    }

    private void Ring()
    {
        phoneAudio.Stop();
        phoneAudio.PlayOneShot(phoneRing);
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

        for (int timesRang = 0; timesRang < amountOfRings; timesRang++)
        {
            Ring();
            yield return new WaitForSeconds(4f);
        }

        ringing = false;
        phoneAudio.PlayOneShot(notPickUpSound);
        manager.ClientsDenied++;

        if (manager.GameRunning)
            currentPhoneCoroutine = StartCoroutine(PhoneCoroutine());
    }

    private IEnumerator DialogTimerCoroutine()
    {
        UpdateDialogTimer(0);
        yield return new WaitForSeconds(1f);

        for (int time = 0; time < maxAmountOfSecondsDialog; time++)
        {
            UpdateDialogTimer(time + 1);
            yield return new WaitForSeconds(1f);
        }

        CloseDialog(false);
    }

    private IEnumerator DialogWriterCoroutine()
    {
        dialogText.text = "";
        for(int i = 0; i < currentDialog.Length; i++)
        {
            phoneAudio.PlayOneShot(dialogBeep);
            dialogText.text += currentDialog[i];
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
