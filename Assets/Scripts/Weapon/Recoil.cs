using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{

    float recoil;
    bool isRecoiling;
    Vector2 maxRecoil = Vector2.zero;
    float speed;

    Quaternion zero = Quaternion.Euler(0, 0, 0);

    // Use this for initialization
    public void Setup(Weapon currentWeapon)
    {
        maxRecoil = currentWeapon.maxRecoil;
        speed = currentWeapon.recoilSpeed;

    }

    public void AddRecoil(float time)
    {
        if(isRecoiling)
        {
            recoil += time;
        }
        else
        {
            recoil = Time.time + time;
            isRecoiling = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(recoil < Time.time)
            isRecoiling = false;

        RecoilWeapon();
    }

    void RecoilWeapon()
    {
        if(isRecoiling)
        {

            Quaternion maxRecoilRotation = Quaternion.Euler(-maxRecoil.x, maxRecoil.y, 0f);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, maxRecoilRotation, Time.deltaTime * speed);
        }
        else if(Quaternion.identity != zero)
        {
            transform.localRotation = Quaternion.Slerp(zero, Quaternion.identity, Time.deltaTime * speed / 2);
        }
    }
}
