
// Blend two profiles based on camera entered into  trigger

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LB_LightingTrigger_Direct : MonoBehaviour {

	MobileColorGrading2 current;
	MobileColorGrading2 temp;


	[Tooltip("Target camera tag for trigger enter and exit")]
	public string cameraTag = "MainCamera";
	[Tooltip("Blend lerp speed * Time.deltaTime")]
	public float blendSpeed = 10f;
	[Tooltip("Update time duration. used for optimization")]
	public float blendDuration = 3f;

	[Header("Options")]
	[Range(0.3f,1)]
	public float Exposure = 0;
	[Range(1,2)]
	public float Contrast = 1f;
	[Range(0.3f,1)]
	public float Gamma = 1f;
	[Range(1,0)]
	public float Saturation = 1f;
	[Range(0,0.5f)]
	public float vignetteIntensity = 0;
	[Range(-100,100)]
	public float R = 0;
	[Range(-100,100)]
	public float G = 0;
	[Range(-100,100)]
	public float B = 0;

	void Start () {
		current = GameObject.FindGameObjectWithTag(cameraTag).GetComponent<MobileColorGrading2> ();
		temp = new MobileColorGrading2 ();
		temp.Exposure = current.Exposure;
		temp.Contrast = current.Contrast;
		temp.Gamma = current.Gamma;
		temp.Saturation = current.Saturation;
		temp.vignetteIntensity = current.vignetteIntensity;
		temp.R = current.R;
		temp.G = current.G;
		temp.B = current.B;
	}
	
	bool isChanging;
	bool isUpdating;

	void Update () {
		if (!isUpdating)
			return;
		
		if (isChanging) {
			current.Exposure = Mathf.Lerp (current.Exposure, Exposure, Time.deltaTime * blendSpeed);
			current.Contrast = Mathf.Lerp (current.Contrast, Contrast, Time.deltaTime * blendSpeed);
			current.Gamma = Mathf.Lerp (current.Gamma, Gamma, Time.deltaTime * blendSpeed);
			current.Saturation = Mathf.Lerp (current.Saturation, Saturation, Time.deltaTime * blendSpeed);
			current.vignetteIntensity = Mathf.Lerp (current.vignetteIntensity, vignetteIntensity, Time.deltaTime * blendSpeed);
			current.R = Mathf.Lerp (current.R, R, Time.deltaTime * blendSpeed);
			current.G = Mathf.Lerp (current.G, G, Time.deltaTime * blendSpeed);
			current.B = Mathf.Lerp (current.B, B, Time.deltaTime * blendSpeed);
		} else {
			current.Exposure = Mathf.Lerp (current.Exposure, temp.Exposure, Time.deltaTime * blendSpeed);
			current.Contrast = Mathf.Lerp (current.Contrast, temp.Contrast, Time.deltaTime * blendSpeed);
			current.Gamma = Mathf.Lerp (current.Gamma, temp.Gamma, Time.deltaTime * blendSpeed);
			current.Saturation = Mathf.Lerp (current.Saturation, temp.Saturation, Time.deltaTime * blendSpeed);
			current.vignetteIntensity = Mathf.Lerp (current.vignetteIntensity, temp.vignetteIntensity, Time.deltaTime * blendSpeed);
			current.R = Mathf.Lerp (current.R, temp.R, Time.deltaTime * blendSpeed);
			current.G = Mathf.Lerp (current.G, temp.G, Time.deltaTime * blendSpeed);
			current.B = Mathf.Lerp (current.B, temp.B, Time.deltaTime * blendSpeed);
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == cameraTag){
			StopCoroutine ("StopUpdating");
			StartCoroutine ("StopUpdating");
			isChanging = true;
		}
	}

	void OnTriggerExit(Collider col)
	{
		if (col.tag == cameraTag)
		{
			StopCoroutine ("StopUpdating");
			StartCoroutine ("StopUpdating");
			isChanging = false;
		}
	}

	// Stop update function after passing blennd duration in seconds    
	IEnumerator StopUpdating()
	{
		isUpdating = true;
		yield return new WaitForSeconds (blendDuration);
		isUpdating = false;
	}
}