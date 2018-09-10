using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneController : MonoBehaviour, Iinteractable
{
    public bool pickedUpPhone;
    public bool ringing;

    private GameManager manager;
    private GameObject dialogBox;
    private Text dialogText;

    private List<Caller> callers;
    private Caller currentCaller;

	void Start ()
    {
        pickedUpPhone = false;
        ringing = false;
        manager = FindObjectOfType<GameManager>();
        dialogBox = GameObject.Find("DialogBox");
        dialogText = GameObject.Find("DialogText").GetComponent<Text>();
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
            Debug.Log("sold some weed");
        }
        else
        {
            Debug.Log("denied weed");
        }

        if(callers.Count <= 0)
        {
            manager.EndGame(true, "No more buyers left!");
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

    private IEnumerator PhoneCoroutine()
    {
        currentCaller = callers[Random.Range(0, callers.Count)];
        callers.Remove(currentCaller);

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
