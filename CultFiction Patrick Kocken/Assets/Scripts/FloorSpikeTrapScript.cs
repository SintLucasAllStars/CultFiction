using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpikeTrapScript : MonoBehaviour {

    private IEnumerator Start()
    {
        BoxCollider floorSpikeTrapCollider = GetComponent<BoxCollider>();
        Animator floorSpikeTrapAnimator = GetComponent<Animator>();
        
        while (true)
        {
            floorSpikeTrapAnimator.SetBool("isReady", false);
            yield return new WaitForSecondsRealtime(4);
            floorSpikeTrapAnimator.SetBool("isReady", true);
            yield return new WaitForSecondsRealtime(4);
        }
    }
}
