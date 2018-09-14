using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class JointController : MonoBehaviour, Iinteractable
{
    public PhoneController phone;
    public Text highText;
    public int maxLevel;

    public bool _active;
    private int _level;
    private Coroutine currentTimerCoroutine;
    private Coroutine currentHitCoroutine;

    public bool Active
    {
        get
        {
            return _active;
        }
    }

	void Start ()
    {
        _active = false;
        _level = 0;
        highText.text = "highness level: " + _level;
	}

    public void OnClick()
    {
        if (_level < maxLevel)
            Hit();
    }

    private void Hit()
    {
        if (phone.PickedUpPhone)
        {
            phone.currentDialog = ScrambleText(phone.unalteredCurrentDialog);
        }

        _level += 1;
        highText.text = "highness level: " + _level;
        if(currentTimerCoroutine != null)
            StopCoroutine(currentTimerCoroutine);

        currentTimerCoroutine = StartCoroutine(TimerCoroutine());
    }

    public string ScrambleText(string input)
    {
        string[] words = input.Split(' ');
        int numberToScramble = Mathf.CeilToInt((float)words.Length / (7 + (_level - 0) * (0 - 6) / (6 - 0)));

        Debug.Log(numberToScramble + " words will be scrambled");

        List<int> indexesToScramble = new List<int>();

        for(int i = 0; i < numberToScramble; i++)
        {
            int randomIndex = Random.Range(0, words.Length);

            while (indexesToScramble.Contains(randomIndex))
            {
                randomIndex = Random.Range(0, words.Length);
            }

            indexesToScramble.Add(randomIndex);

            words[randomIndex] = Shuffle(words[randomIndex]);
        }

        return words.Aggregate((x, y) => x + " " + y);
    }

    public string Shuffle(string str)
    {
        char[] array = str.ToCharArray();
        System.Random rng = new System.Random();
        int n = array.Length;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            var value = array[k];
            array[k] = array[n];
            array[n] = value;
        }
        return new string(array);
    }

    private IEnumerator TimerCoroutine()
    {
        _active = true;

        yield return new WaitForSeconds(3f);
        if(_level > 0)
        {
            _level--;
            currentTimerCoroutine = StartCoroutine(TimerCoroutine());
        }
        highText.text = "highness level: " + _level;

        if (_level <= 0)
            _active = false;
    }
}
