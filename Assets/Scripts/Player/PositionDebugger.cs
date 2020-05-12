using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PositionDebugger : MonoBehaviour
{
    private Text height;
    private readonly float yPos;
            
    private Text position;
    private readonly float xPos;
    private readonly float zPos;

    private void Awake()
    {
        height = GameObject.Find("Y").GetComponent<Text>();
        position = GameObject.Find("X, Z").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        DebugPosition(height, position);
    }

    void DebugPosition(Text y, Text xz)
    {
        y.text = transform.position.y.ToString();
        xz.text = transform.position.x.ToString() + ", " + transform.position.z.ToString();
    }
}
