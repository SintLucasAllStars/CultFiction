using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchScript : MonoBehaviour {
	public Light light;
	public float minLight;
	public float maxLight;
	public float flikkerSpeed = 0.1f;
	float timer;
	// Use this for initialization
	void Start () {
		if (light == null)
		{
			GetComponent<Light> ();
		}
		StartCoroutine (SetLight ());
	}
	IEnumerator SetLight(){
		while (true)
		{
			if (timer+0.1f < Time.time) {
				light.intensity = Random.Range (minLight, maxLight);


				timer = Time.time;
			}
			if (flikkerSpeed < 0.01f)
			{
				StopAllCoroutines ();
			}
			yield return new WaitForSeconds (flikkerSpeed);
		}
	}
}
