using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Recoil
{
    int fire;
    float multiplier = .1f;

    [Header("Recoil Aim")]
    [SerializeField] Vector2 recoilAimX;
    [SerializeField] Vector2 recoilAimY;

    [Header("Recoil Hip")]
    [SerializeField] Vector2 recoilHipX;
    [SerializeField] Vector2 recoilHipY;

    public Vector3 GetRecoil(Vector3 forward, bool isAiming)
    {
        fire++;
        forward.x += ((isAiming) ? Random.Range(recoilAimX.x, recoilAimX.y) : Random.Range(recoilHipX.x, recoilHipX.y)) * (multiplier * fire);
        forward.y += ((isAiming) ? Random.Range(recoilAimY.x, recoilAimY.y) : Random.Range(recoilHipY.x, recoilHipY.y)) * (multiplier * fire);
        return forward;
    }

    public void ResetHits()
    {
        fire = 0;
    }
}
