using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _maxY;
    [SerializeField] private float _minY;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _cam;
    [Range(0.01f, 10.0f)] [SerializeField] private float _sensetivity;
    [Range(0.01f, 1.0f)] [SerializeField] private float _smoothing;

    private Vector3 _rotation;
    [SerializeField] private float _maxDistance;
    [SerializeField] private GameObject _shootingTarget;
    [SerializeField] private Camera _shootingCamera;
    [SerializeField] private float _gunAnimationSpeed;
    [SerializeField] private Image _crosshair;

    public enum CameraState
    {
        IDLE,
        ON_WAY_TO_TARGET,
        AT_TARGET,
        ON_WAY_BACK,
    }

    public CameraState currentState;

    private void Start()
    {
        currentState = CameraState.IDLE;
        _shootingCamera.gameObject.SetActive(false);
        _crosshair.enabled = false;
    }

    private void Update()
    {
        ShootControlls();
    }

    private void ShootControlls()
    {
        if (Input.GetMouseButton(1) && currentState == CameraState.IDLE)
        {
            StartCoroutine(MoveCameraToTarget());
        }
        else if (currentState == CameraState.AT_TARGET && !Input.GetMouseButton(1))
        {
            StartCoroutine(MoveCameraBack());
        }
    }

    private IEnumerator MoveCameraToTarget()
    {
        _cam.gameObject.SetActive(false);
        _shootingCamera.gameObject.SetActive(true);
        currentState = CameraState.ON_WAY_TO_TARGET;
        _crosshair.enabled = true;
        while (_shootingCamera.transform.position != _shootingTarget.transform.position)
        {
            _shootingCamera.transform.position = Vector3.MoveTowards(_shootingCamera.transform.position,
                _shootingTarget.transform.position,
                _gunAnimationSpeed * Time.deltaTime);
            yield return null;
        }

        currentState = CameraState.AT_TARGET;
    }

    private IEnumerator MoveCameraBack()
    {
        currentState = CameraState.ON_WAY_BACK;
        while (_shootingCamera.transform.position != _cam.transform.position)
        {
            _shootingCamera.transform.position = Vector3.MoveTowards(_shootingCamera.transform.position,
                _cam.transform.position,
                _gunAnimationSpeed * Time.deltaTime);
            yield return null;
        }

        _crosshair.enabled = false;
        _cam.gameObject.SetActive(true);
        _shootingCamera.gameObject.SetActive(false);
        currentState = CameraState.IDLE;
    }

    private void CheckCollision()
    {
        RaycastHit hit;
        Vector3 playerPos = new Vector3(_player.position.x, _player.position.y + 2, _player.position.z);
        Debug.DrawRay(playerPos, _cam.position - playerPos);
        int layerMask = LayerMask.GetMask("Walls");
        if (Physics.Raycast(playerPos, _cam.position - playerPos, out hit, _maxDistance, layerMask))
        {
            _cam.position = Vector3.Lerp(hit.point, playerPos, 1 * Time.deltaTime);
        }
    }

    private void LateUpdate()
    {
        MouseControlls();
    }

    private void MouseControlls()
    {
        if (CursorLockMode.None == Cursor.lockState) return;
        transform.position = _player.position;

        var newRotation = new Vector3(Input.GetAxis("Mouse Y") * -1, Input.GetAxis("Mouse X")) * _sensetivity;
        _rotation.x = Mathf.Clamp(_rotation.x, _minY, _maxY);

        transform.rotation = Quaternion.Euler(Vector3.Slerp(_rotation, newRotation, Time.deltaTime * _smoothing));
        _rotation += newRotation;
    }
}