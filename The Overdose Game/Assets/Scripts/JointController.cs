using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class JointController : MonoBehaviour, Iinteractable
{
    public AudioClip smoke;
    public AudioSource jointAudio;
    public GameManager manager;
    public Transform animationTarget;
    public PhoneController phone;
    public int maxLevel;

    public bool active { get; private set; }
    private Coroutine currentTimerCoroutine;
    private Coroutine currentActionCoroutine;

	void Start ()
    {
        active = false;
        manager.HighLevel = 0;
	}

    // Interface implemented method for handling clicks
    public void OnClick()
    {
        if (manager.HighLevel < maxLevel && currentActionCoroutine == null)
        {
            currentActionCoroutine = StartCoroutine(ActionCoroutine(animationTarget));
        }
    }

    // Method for interacting with object, will also scramble text if user is currently on phone
    private void Hit()
    {
        if (phone.pickedUpPhone)
        {
            phone.currentDialog = ScrambleText(phone.unalteredCurrentDialog);
        }

        manager.HighLevel++;
        manager.MoneyMultiplier = manager.HighLevel;
        if(currentTimerCoroutine != null)
            StopCoroutine(currentTimerCoroutine);

        currentTimerCoroutine = StartCoroutine(TimerCoroutine());
    }

    public string ScrambleText(string input)
    {
        string[] words = input.Split(' ');
        int numberToScramble = Mathf.CeilToInt((float)words.Length / (7 + (manager.HighLevel - 0) * (0 - 6) / (6 - 0)));
        List<int> indexesToScramble = new List<int>();

        for(int i = 0; i < words.Length; i++)
        {
            indexesToScramble.Add(i);
        }

        indexesToScramble.Shuffle();

        for(int i = 0; i < numberToScramble; i++)
        {
            int index = indexesToScramble[i];
            List<char> scrambledWord = words[index].ToList();
            scrambledWord.Shuffle();
            words[index] = new string(scrambledWord.ToArray());
        }

        return words.Aggregate((x, y) => x + " " + y);
    }

    // Coroutine which slowly decreases highness level back to 0
    private IEnumerator TimerCoroutine()
    {
        active = true;

        yield return new WaitForSeconds(6f);
        if(manager.HighLevel > 0)
        {
            manager.HighLevel--;
            manager.MoneyMultiplier = manager.HighLevel;
            currentTimerCoroutine = StartCoroutine(TimerCoroutine());
        }

        if (manager.HighLevel <= 0)
            active = false;
    }

    // Interface implemented coroutine for animations and actions for interaction
    public IEnumerator ActionCoroutine(Transform target)
    {
        jointAudio.PlayOneShot(smoke);
        Vector3 previousPosition = transform.position;
        Quaternion previousRotation = transform.rotation;

        while(Vector3.Distance(transform.position, target.position) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, 15 * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, 15 * Time.deltaTime);
            yield return null;
        }

        Hit();
        yield return new WaitForSeconds(0.5f);

        while (Vector3.Distance(transform.position, previousPosition) > 0.001f)
        {
            transform.position = Vector3.Lerp(transform.position, previousPosition, 15 * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, previousRotation, 15 * Time.deltaTime);
            yield return null;
        }

        currentActionCoroutine = null;
    }
}

public static class Extentions
{
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        System.Random rnd = new System.Random();
        while (n > 1)
        {
            int k = (rnd.Next(0, n) % n);
            n--;
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
