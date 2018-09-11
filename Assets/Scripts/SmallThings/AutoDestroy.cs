using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour {
	[SerializeField]
	float timeToWaitTillDestroy = 2;

	// Use this for initialization
	void Start () {
		StartCoroutine(Timer());
	}
	IEnumerator Timer(){
		yield return new WaitForSeconds(timeToWaitTillDestroy);
		Destroy(this.gameObject);
	}
}
