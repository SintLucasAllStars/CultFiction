using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HappinessMeter : Singleton<HappinessMeter>
{
    private float _happiness = 1000;

    [SerializeField]
    private Image _fillImage = null;

    private void Start() => Mathf.Clamp(_happiness, 0f, 1000f);

    public void Decrease()
    {
        _happiness--;
        _fillImage.fillAmount = _happiness / 1000;
        if (_happiness < 0f)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void Increase(float happiness)
    {
        _happiness += happiness;
        _fillImage.fillAmount = _happiness / 1000;
    }
}
