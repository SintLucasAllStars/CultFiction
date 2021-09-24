using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperHandeler : MonoBehaviour
{
    public bool isDucked = true;

    private void Update()
    {
        if (isDucked)
        {
            //cancel the shot.
        }
        else if (!isDucked)
        {
            //Shoot the shot.
        }
    }
}
