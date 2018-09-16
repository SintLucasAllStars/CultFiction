using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneController : Interactable
{
    public AudioClip phoneRing;
    public AudioClip phonePickup;
    public AudioClip dialogBeep;
    public AudioClip acceptSound;
    public AudioClip denySound;
    public AudioClip notPickUpSound;
    public AudioClip dialogTimerSound;
    public AudioSource phoneAudio;
    public int amountOfRings;
    public int maxAmountOfSecondsDialog;
    public float dialogWriteSpeed;
    public Transform animationTarget;
    public Transform phoneHorn;
    public JointController textManipulator;
    public GameManager manager;
    public GameObject dialogBox;
    public Text dialogText;
    public Image dialogTimer;

    public string unalteredCurrentDialog { get; private set; }
    public string currentDialog { get; set; }
    public bool pickedUpPhone { get; private set; }
    private bool ringing;
    private List<Caller> callers;
    private Caller currentCaller;

    private Coroutine currentPhoneCoroutine;
    private Coroutine currentDialogTimerCoroutine;
    private Coroutine currentDialogWriterCoroutine;

	void Start ()
    {
        pickedUpPhone = false;
        ringing = false;
        dialogBox.SetActive(false);
        originalScale = transform.localScale;
        GetCallers();
        currentPhoneCoroutine = StartCoroutine(PhoneCoroutine());
	}

    public override void OnClick()
    {
        if (ringing && !pickedUpPhone)
        {
            StartCoroutine(ActionCoroutine(animationTarget));
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
        currentDialog = textManipulator.active ? textManipulator.ScrambleText(unalteredCurrentDialog) : unalteredCurrentDialog;

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
            manager.Money += Mathf.CeilToInt(currentCaller.payment);
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
            manager.EndGame(false, "No more clients left!");
            return;
        }

        phoneAudio.PlayOneShot(notPickUpSound);

        if(manager.gameRunning)
            currentPhoneCoroutine = StartCoroutine(PhoneCoroutine());
    }

    private void Ring()
    {
        phoneAudio.Stop();
        phoneAudio.PlayOneShot(phoneRing);
    }

    private void UpdateDialogTimer(int secondsElapsed)
    {
        dialogTimer.fillAmount = (float)1 / maxAmountOfSecondsDialog * secondsElapsed;
        phoneAudio.PlayOneShot(dialogTimerSound);
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
        yield return new WaitForSeconds(6f);

        ringing = true;

        for (int timesRang = 0; timesRang < amountOfRings; timesRang++)
        {
            Ring();
            yield return new WaitForSeconds(4f);
        }

        ringing = false;
        phoneAudio.PlayOneShot(notPickUpSound);
        manager.ClientsDenied++;

        if (manager.gameRunning)
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

    public override IEnumerator ActionCoroutine(Transform target)
    {
        OpenDialog();

        Vector3 previousPosition = phoneHorn.position;
        Quaternion previousRotation = phoneHorn.rotation;

        while (Vector3.Distance(phoneHorn.position, target.position) > 0.001f)
        {
            phoneHorn.position = Vector3.Lerp(phoneHorn.position, target.position, 15 * Time.deltaTime);
            phoneHorn.rotation = Quaternion.Lerp(phoneHorn.rotation, target.rotation, 15 * Time.deltaTime);
            yield return null;
        }

        while (pickedUpPhone)
        {
            yield return null;
        }

        while (Vector3.Distance(phoneHorn.position, previousPosition) > 0.001f)
        {
            phoneHorn.position = Vector3.Lerp(phoneHorn.position, previousPosition, 15 * Time.deltaTime);
            phoneHorn.rotation = Quaternion.Lerp(phoneHorn.rotation, previousRotation, 15 * Time.deltaTime);
            yield return null;
        }
    }
}

public struct Caller
{
    public bool isNarc;
    public int payment;
    public string dialog;
}
