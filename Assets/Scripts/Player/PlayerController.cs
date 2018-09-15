using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    Rigidbody rb;

    //Mouse
    [Header("Mouse")]
    [SerializeField]
    Vector2 sensitivity;
    [SerializeField] Vector2 clamp;
    [SerializeField] float range = 5;

    //Weapon
    [Header("Weapon")]
    public Weapon currentWeapon;
    public Weapon secondaryWeapon;
    [SerializeField] Recoil recoil;
    [SerializeField] WeaponUI weaponUI;

    [Header("Player")]
    const float maxHealth = 100f;
    float currentHeatlh = maxHealth;
    [SerializeField] float regenDelay;
    [SerializeField] float regenSpeed;
    float currentTimeTillRegen;
    bool isRegen = false;

    bool lockMouse = true;
    new Transform camera;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camera = Camera.main.gameObject.transform;
        recoil.Setup(currentWeapon);
        weaponUI.UpdateWeapon(currentWeapon);
    }

    void Update()
    {
        MouseRotation();
        CheckInteraction();

        if(currentHeatlh < maxHealth && !isRegen)
        {
            CheckRegen();
        }

        if(currentWeapon != null)
        {
            if(currentWeapon.isAutomatic)
            {
                if(Input.GetMouseButton(0))
                {
                    currentWeapon.Fire();
                    weaponUI.UpdateAmmo(currentWeapon.ammo, currentWeapon.ammoStockPile);
                    //recoil.AddRecoil(currentWeapon.recoilTime);
                }
            }
            else
            {
                if(Input.GetMouseButtonDown(0))
                {
                    currentWeapon.Fire();
                    weaponUI.UpdateAmmo(currentWeapon.ammo, currentWeapon.ammoStockPile);
                    //recoil.AddRecoil(currentWeapon.recoilTime);
                }
            }

        }
        if(Input.GetKey(KeyCode.R))
        {
            if(currentWeapon != null)
                currentWeapon.Reload();

            weaponUI.UpdateAmmo(currentWeapon.ammo, currentWeapon.ammoStockPile);
        }

        if(Input.GetMouseButtonDown(1))
        {
            Debug.Log("Down");
            currentWeapon.Aim(true);
        }

        if(Input.GetMouseButtonUp(1))
        {
            Debug.Log("Up");
            currentWeapon.Aim(false);
        }


        float scrollWheel = Input.GetAxis("Mouse ScrollWheel"); ;
        if(scrollWheel > 0 || scrollWheel < 0)
        {
            SwapWeapon();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float speed = (!Input.GetKey(KeyCode.LeftShift)) ? walkSpeed : runSpeed;

        Vector2 axis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed;
        transform.position += (transform.forward * axis.y + transform.right * axis.x) * Time.deltaTime;

        if(!grounded)
            IsGrounded();
        else if(Input.GetKey(KeyCode.Space))
            Jump();
    }

    public void Damage(float damage)
    {
        currentHeatlh -= damage;
        if(currentHeatlh < 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        Debug.Log(currentHeatlh);

        isRegen = false;
        currentTimeTillRegen = Time.time + regenDelay;

        weaponUI.SetNewHealth(currentHeatlh, maxHealth);
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
        grounded = false;
        rb.AddForce(transform.up * jumpSpeed, ForceMode.VelocityChange);
    }

    void MouseRotation()
    {
        MouseLock();
        if(!lockMouse)
            return;

        Vector2 rotation = new Vector2(Input.GetAxis("Mouse Y") * sensitivity.x, Input.GetAxis("Mouse X") * sensitivity.y);

        transform.localRotation *= Quaternion.Euler(0, rotation.y, 0);
        camera.localRotation *= Quaternion.Euler(-rotation.x, 0, 0);
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
                hit.collider.GetComponent<IInteractable>().Interact(this);
            }
        }
    }

    void SwapWeapon()
    {
        if(currentWeapon == null || secondaryWeapon == null)
            return;

        currentWeapon.Aim(false);

        Weapon weapon = currentWeapon;

        currentWeapon = secondaryWeapon;
        currentWeapon.Toggle(true);

        secondaryWeapon = weapon;
        weapon.Toggle(false);
        recoil.Setup(currentWeapon);
        weaponUI.UpdateWeapon(currentWeapon);
        currentWeapon.Aim(false);

    }

    void CheckRegen()
    {
        if(Time.time > currentTimeTillRegen)
        {
            StartCoroutine(RegenToFullHealth());
            Debug.Log("Regen");
        }
    }

    IEnumerator RegenToFullHealth()
    {
        isRegen = true;
        while(currentHeatlh < maxHealth)
        {
            currentHeatlh = Mathf.Clamp(currentHeatlh + Time.deltaTime / regenSpeed, 0, maxHealth);
            weaponUI.SetNewHealth(currentHeatlh, maxHealth);
            yield return new WaitForEndOfFrame();
        }
    }



}
