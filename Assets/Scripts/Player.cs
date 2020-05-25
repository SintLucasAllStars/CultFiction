using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Camera cam;
    private Rigidbody2D rb;
    private Animator anim;
    public GameObject bullet;
    public GameObject ReloadSprite;

    public Gun gun;

    private int health = 10;
    public float speed = 3.5f;
    private float cameraFollowSpeed = 5.0f;

    private float horizontal, vertical;
    private Vector2 moveDir;
    private Vector2 mousePos;
    private Vector2 lookDir;
    private float angle;

    private float myDeltaTime;
    private bool isReloading;

    public delegate void OnAction(int health, int ammo, int ammoInClip);
    public static OnAction OnChange;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        myDeltaTime = Time.time;
        ReloadSprite.SetActive(false);
        cam = Camera.main;
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(horizontal, vertical) * speed;
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        lookDir = mousePos - rb.position;
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        Rotate();

        if (Input.GetKey(KeyCode.Mouse0) && Time.time > myDeltaTime && !isReloading)
        {
            if(gun.ammoInClip > 0)
            {
                Shoot();
                myDeltaTime = Time.time + gun.fireRate;
            }
            else
            {
                if(gun.ammo > 0)
                {
                    isReloading = true;
                    StartCoroutine(Reload());
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.R) && gun.ammoInClip < gun.clipCapacity && gun.ammo > 0)
        {
            isReloading = true;
            StartCoroutine(Reload());
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDir;
    }

    private void Rotate()
    {
        int parameterValue = GetRotation(angle);
        anim.SetFloat("lookDir", parameterValue);
    }

    private void Shoot()
    {
        Vector2 firePos = rb.position + (lookDir.normalized * 0.55f);

        GameObject go = Instantiate(gun.bullet, firePos, Quaternion.Euler(0, 0, angle));
        go.tag = "PlayerBullet";
        go.GetComponent<Rigidbody2D>().AddForce(lookDir.normalized * gun.projectileSpeed, ForceMode2D.Impulse);
        gun.ammoInClip--;
        OnChange?.Invoke(health, gun.ammo, gun.ammoInClip);
    }

    private IEnumerator Reload()
    {
        ReloadSprite.SetActive(true);
        yield return new WaitForSeconds(gun.reloadTime);
        int amountToReload = gun.clipCapacity - gun.ammoInClip;
        if(gun.ammo < amountToReload)
        {
            gun.ammo = 0;
            gun.ammoInClip += gun.ammo;
        }
        else
        {
            gun.ammo -= amountToReload;
            gun.ammoInClip += amountToReload;
        }
        OnChange?.Invoke(health, gun.ammo, gun.ammoInClip);
        isReloading = false;
        ReloadSprite.SetActive(false);
    }

    private void LateUpdate()
    {
        Vector3 camOffset = transform.position + (-Vector3.forward * 5.0f);
        cam.transform.position = Vector3.Lerp(cam.transform.position, camOffset, Time.deltaTime * cameraFollowSpeed);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        DropAmmo drop = col.gameObject.GetComponent<DropAmmo>();
        Debug.Log(drop);
        if (drop != null)
        {
            int dropAmount = drop.PickUp();
            if(gun.ammo + dropAmount > gun.totalAmmo)
            {
                gun.ammo = gun.totalAmmo;
            }
            else
            {
                gun.ammo += dropAmount;
            }
        }
        OnChange?.Invoke(health, gun.ammo, gun.ammoInClip);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("EnemyBullet"))
        {
            health--;
            OnChange?.Invoke(health, gun.ammo, gun.ammoInClip);

            if (health < 1)
            {
                GameOver();
            }
        }
    }

    private void GameOver()
    {
        //load GameOver Scene
    }

    private int GetRotation(float angle)
    {
        if (angle < 23 && angle >= -23)
        {
            //North
            return 1;
        }
        else if (angle < -23 && angle >= -68)
        {
            //North East
            return 2;
        }
        else if (angle < -68 && angle >= -113)
        {
            //East
            return 3;
        }
        else if (angle < -113 && angle >= -158)
        {
            //South East
            return 4;
        }
        else if (angle < -158 && angle >= -203)
        {
            //South
            return 5;
        }
        else if (angle < -203 && angle >= -248)
        {
            //South West
            return 6;
        }
        else if (angle < -248 || angle >= 68)
        {
            //West
            return 7;
        }
        else if (angle < 68 && angle >= 23)
        {
            //North West
            return 8;
        }
        else
        {
            Debug.LogError("Angle: " + angle + " none of the directions compatible!!");
            return 1;
        }
    }
}