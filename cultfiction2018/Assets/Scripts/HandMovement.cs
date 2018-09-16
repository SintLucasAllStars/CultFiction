using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMovement : MonoBehaviour
{
    public float Speed = 10.0f;
    public float RotationSpeed = 10.0f;

    public float Drunkness = 100f;
    private float _randomSinX = .0f;
    private float _randomsSinY = .0f;

    public Vector2 Xclamp;
    public Vector2 Yclamp;

    private int _boundary = 1;
    private int _width;
    private int _height;

    private float _cameraOffsetX;
    private float _cameraOffsetY;
    private float _cameraOffsetZ;


    // Use this for initialization
    void Start()
    {
        _width = Screen.width;
        _height = Screen.height;
        SetCameraOffsets();
        StartCoroutine(DrunkNumber());
    }

    private void SetCameraOffsets()
    {
        _cameraOffsetX = transform.position.x - Camera.main.transform.position.x;
        _cameraOffsetY = Camera.main.transform.position.y - transform.position.y;
        _cameraOffsetZ = transform.position.z - Camera.main.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.UIactive())
        {
            //MouseMode();
            KeyBoardMode();
        }
 
    }

    private void KeyBoardMode()
    {
        transform.position = transform.position + new Vector3(Input.GetAxis("Vertical"), 0, -Input.GetAxis("Horizontal") * Speed * Time.deltaTime);
        SinCalculations();
        transform.position = ClampVector();
        transform.eulerAngles = RotationEuler();
        MoveCamera();
    }


    private void MouseMode()
    {
        if (Input.mousePosition.x > _width - _boundary)
        {
            transform.position -= Xvector();
        }

        if (Input.mousePosition.x < 0 + _boundary)
        {
            transform.position -= Xvector();
        }

        if (Input.mousePosition.y > _height - _boundary)
        {
            transform.position -= Yvector();
        }

        if (Input.mousePosition.y < 0 + _boundary)
        {
            transform.position -= Yvector();
        }

        transform.position = ClampVector();

        transform.eulerAngles = RotationEuler();
        MoveCamera();
    }

    private void MoveCamera()
    {
        Camera.main.transform.position = new Vector3
        (
            transform.position.x - _cameraOffsetX,
            transform.position.y + _cameraOffsetY,
            transform.position.z - _cameraOffsetZ
        );
    }

    private Vector3 RotationEuler()
    {
        return transform.eulerAngles + new Vector3(0, 0, Mathf.Sin(Time.realtimeSinceStartup) /10);
    }

    private float ScrollWheelAxis()
    {
        return Input.GetAxis("Mouse ScrollWheel");
    }


    private Vector3 Yvector()
    {
        return new Vector3(Input.GetAxis("Mouse Y") * Time.deltaTime * Speed, 0, 0);
    }

    private Vector3 Xvector()
    {
        return new Vector3(0, 0, Input.GetAxis("Mouse X") * Time.deltaTime * Speed);
    }

    private void SinCalculations()
    {


        float xSin = Mathf.Sin(_randomSinX * Drunkness) * Time.deltaTime;
        float zCos = Mathf.Sin(_randomsSinY * Drunkness) * Time.deltaTime;
        transform.position = transform.position + new Vector3(xSin, 0, zCos);
    }

    private IEnumerator DrunkNumber()
    {
        while(true)
        {
            _randomSinX = Random.Range(-0.1f, 0.1f);
            _randomsSinY = Random.Range(-0.1f, 0.1f);
            yield return new  WaitForSeconds(Random.Range(.25f,.75f));
        }
    }


    private Vector3 ClampVector()
    {
        return new Vector3
        (
            Mathf.Clamp(transform.position.x, Xclamp.x, Xclamp.y),
            transform.position.y,
            Mathf.Clamp(transform.position.z, Yclamp.x, Yclamp.y)
        );
    }
}