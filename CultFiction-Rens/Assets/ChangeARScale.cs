using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeARScale : MonoBehaviour
{

    public GameObject arSessionOrigin;
    public GameObject Content;
    private Slider _slider;

    private void Start()
    {
        _slider = GetComponent<Slider>();
    }

    public void ChangeScale(int value)
    {
        arSessionOrigin.transform.localScale = Vector3.one * _slider.value;
        Content.transform.localScale = Vector3.one / _slider.value;
    }
}
