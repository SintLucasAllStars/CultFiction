using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementScript : MonoBehaviour {

    [SerializeField] private float _speed;

    private Transform _playerTransform;

    private void Start () {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
	private void Update () {

        float newCameraPosition = Vector3.MoveTowards(transform.position, _playerTransform.position, _speed * Time.deltaTime).z;
        transform.position = new Vector3(transform.position.x, transform.position.y,newCameraPosition);
    }
}
