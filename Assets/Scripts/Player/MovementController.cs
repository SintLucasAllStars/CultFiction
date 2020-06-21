using UnityEngine;

public class MovementController : MonoBehaviour
{
    public Transform playerModel;
    public float sprintSpeed;
    public float walkSpeed;
    public float movementSpeed;

    [SerializeField] private Camera _mainCam;

    private void Update()
    {
        PlayerMovementControls();
    }


    private void PlayerMovementControls()
    {
        if (CursorLockMode.None == Cursor.lockState) return;
        var moveDir = Vector3.zero;
        
        moveDir += CheckKey(KeyCode.W, _mainCam.transform.forward);
        moveDir -= CheckKey(KeyCode.S, _mainCam.transform.forward);
        moveDir -= CheckKey(KeyCode.A, _mainCam.transform.right);
        moveDir += CheckKey(KeyCode.D, _mainCam.transform.right);

        movementSpeed = 0;
        if (moveDir != Vector3.zero) movementSpeed = walkSpeed;

        if (Input.GetKey(KeyCode.LeftShift) && moveDir != Vector3.zero)
            movementSpeed = sprintSpeed;

        moveDir.y = 0;

        transform.Translate(movementSpeed * Time.deltaTime * moveDir.normalized, Space.World);
        transform.rotation =
            Quaternion.LookRotation(Vector3.Lerp(playerModel.forward, moveDir.normalized, Time.deltaTime * 10));
    }

    private Vector3 CheckKey(KeyCode key, Vector3 dir)
    {
        if (Input.GetKey(key))
        {
            return dir;
        }
        return Vector3.zero;
    }
}