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

    [SerializeField]
    private Vector3 _gameCameraRot = Vector3.zero;

    [SerializeField]
    private Vector3 _menuCameraRot = Vector3.zero;

    [SerializeField]
    private Vector3 _menuCameraPos = Vector3.zero;

    private GameObject _target;

    void FixedUpdate()
    {
        if (GameManager.Instance.GameIsRunnning)
        {
            if (_target != null)
            {
                float targetX = _target.transform.position.x + _xOffset;
                float targetY = _target.transform.position.y + _yOffset;
                float targetZ = _target.transform.position.z + _zOffset;
                Vector3 targetMovementPos = new Vector3(targetX, targetY, targetZ);
                transform.position = Vector3.MoveTowards(transform.position, targetMovementPos, _lerpSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(_gameCameraRot), _lerpSpeed * Time.deltaTime);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _menuCameraPos, _lerpSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(_menuCameraRot), _lerpSpeed * Time.deltaTime);
        }
    }

    public void SetNewTarget(GameObject player)
    {
        _target = player;
    }
}
