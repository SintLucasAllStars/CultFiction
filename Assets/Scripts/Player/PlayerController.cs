using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    //Movement
    [Header("Movement")]
    [SerializeField]
    float walkSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float jumpSpeed;

    public bool grounded = true;
    private Rigidbody rb;

    //Mouse
    [Header("Mouse")]
    [SerializeField] Vector2 sensitivity;
    [SerializeField] Vector2 clamp;
    [SerializeField] float range = 5;

    //Weapon
    [Header("Weapon")]
    [SerializeField] Weapon currentWeapon;
    [SerializeField] Weapon secondaryWeapon;

    private bool lockMouse = true;
    private new Transform camera;

    // Use this for initialization
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        camera = transform.GetChild(0);
    }

    private void Update()
    {
        MouseRotation();
        CheckInteraction();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float speed = (!Input.GetKey(KeyCode.LeftShift)) ? walkSpeed : runSpeed;

        float horizontal = Input.GetAxis("Horizontal") * speed;
        float vertical = Input.GetAxis("Vertical") * speed;
        transform.position += (transform.forward * vertical + transform.right * horizontal) * Time.deltaTime;

        if(!grounded)
            CheckGroundedStatus();
        else if(Input.GetKey(KeyCode.Space))
            Jump();

        if(Input.GetMouseButtonDown(0))
        {
            if(currentWeapon != null)
                currentWeapon.Fire();
        }
        if(Input.GetKey(KeyCode.R))
        {
            if(currentWeapon != null)
                currentWeapon.Reload();
        }
    }

    private void CheckGroundedStatus()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, -transform.up, out hit, 1))
        {
            grounded = true;
        }
    }

    private void Jump()
    {
        Debug.Log("Jump");
        grounded = false;
        rb.AddForce(transform.up * jumpSpeed, ForceMode.VelocityChange);
    }

    private void MouseRotation()
    {
        MouseLock();
        if(!lockMouse)
            return;

        float xRotation = Input.GetAxis("Mouse Y") * sensitivity.x;
        float yRotation = Input.GetAxis("Mouse X") * sensitivity.y;

        transform.localRotation *= Quaternion.Euler(0, yRotation, 0);
        camera.localRotation *= Quaternion.Euler(-xRotation, 0, 0);
        camera.localRotation = ClampRotationAroundXAxis(camera.localRotation);
    }

    private void MouseLock()
    {
        if(lockMouse)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        angleX = Mathf.Clamp(angleX, clamp.x, clamp.y);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }


    private void CheckInteraction()
    {
        RaycastHit hit;
        if(Physics.Raycast(camera.position, camera.forward, out hit, range) && hit.collider.CompareTag("Interactable"))
        {
            if(Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
            {
                hit.collider.GetComponent<IInteractable>().Interact();
            }
        }
    }

    void SwapWeapon()
    {
        Weapon weapon = currentWeapon;

        currentWeapon = secondaryWeapon;
        currentWeapon.Toggle(true);

        secondaryWeapon = weapon;
        weapon.Toggle(false);
    }
}
