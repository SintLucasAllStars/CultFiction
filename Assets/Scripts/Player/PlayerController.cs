using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    //Movement
    [Header("Movement")]
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float jumpSpeed;

    public bool grounded = true;
    Rigidbody rb;

    //Mouse
    [Header("Mouse")]
    [SerializeField] Vector2 sensitivity;
    [SerializeField] Vector2 clamp;
    [SerializeField] float range = 5;

    //Weapon
    [Header("Weapon")]
    [SerializeField] Weapon currentWeapon;
    [SerializeField] Weapon secondaryWeapon;

    [Header("Player")]
    const int maxHealth = 100;
    int health = 100;

    bool lockMouse = true;
    new Transform camera;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camera = transform.GetChild(0);
    }

    void Update()
    {
        MouseRotation();
        CheckInteraction();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float speed = (!Input.GetKey(KeyCode.LeftShift)) ? walkSpeed : runSpeed;

        float horizontal = Input.GetAxis("Horizontal") * speed;
        float vertical = Input.GetAxis("Vertical") * speed;
        transform.position += (transform.forward * vertical + transform.right * horizontal) * Time.deltaTime;

        if(!grounded)
            IsGrounded();
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

    public void Damage(int amount)
    {
        health -= amount;
        if(health <= 0)
            Debug.Log("Dead");
    }


    void IsGrounded()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, -transform.up, out hit, 1))
        {
            grounded = true;
        }
    }

    void Jump()
    {
        Debug.Log("Jump");
        grounded = false;
        rb.AddForce(transform.up * jumpSpeed, ForceMode.VelocityChange);
    }

    void MouseRotation()
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

    void MouseLock()
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

    Quaternion ClampRotationAroundXAxis(Quaternion q)
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


    void CheckInteraction()
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
