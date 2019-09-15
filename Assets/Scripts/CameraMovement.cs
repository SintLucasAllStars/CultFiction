using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float _lerpSpeed = 0.0f;

    [SerializeField]
    private float _xOffset = 0.0f;

    [SerializeField]
    private float _yOffset = 0.0f;

    [SerializeField]
    private float _zOffset = 0.0f;

    private GameObject _target;

    // Start is called before the first frame update
    void Start()
    {
        _target = FindObjectOfType<Player>().gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetMovementPos = new Vector3(_target.transform.position.x + _xOffset, _target.transform.position.y + _yOffset, _target.transform.position.z + _zOffset);
        transform.position = Vector3.MoveTowards(transform.position, targetMovementPos, _lerpSpeed * Time.deltaTime);
    }
}
