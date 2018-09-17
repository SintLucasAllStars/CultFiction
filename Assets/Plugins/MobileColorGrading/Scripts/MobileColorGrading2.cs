// Color Grading image ffects
// Made by ALIyerEdon
// aliyeredon@gmail.com
// All rights reserved
//    2017   - Unity 5.6 - us compitibale with all unity versions even unity 4 (is not tested)    

using UnityEngine;
using System.Collections;

public enum ToneMapping{
	ACES,None
}

[ExecuteInEditMode]
[RequireComponent (typeof(Camera))]
[AddComponentMenu ("Lighting Box/Mobile Color Grading2")]
[ImageEffectAllowedInSceneView]
public class MobileColorGrading2 : MonoBehaviour {

	[Header("Tone Mapping")]
	// Tonemapping type - ACES Filmic or None
	public ToneMapping toneMapping = ToneMapping.ACES;

	public Material colorMaterial;
	public  Shader shader;

	[Header("Image Settings")]
	[Range(0.3f,1)]
	public float Exposure = 0.3f;

	[Range(1,2)]
	public float Contrast = 1.14f;

	[Range(1,0)]
	public float Saturation = 0f;

	[Range(0.3f,1)]
	public float Gamma = 0.4f;

	[Header("Vignette")]
	[Range(0,0.5f)]
	public float vignetteIntensity = 0f;

	[Header("Color Balance")]
	[Range(-100,100)]
	public float R;
	[Range(-100,100)]
	public float G;
	[Range(-100,100)]
	public float B;

	void OnEnable()
	{
		if (colorMaterial == null) {
			shader = Shader.Find ("Hidden/ALIyerEdon/ColorGrading");
			colorMaterial = Resources.Load ("ColorGrading") as Material;
			colorMaterial.shader = shader;
		}
	}

	void Start ()
	{
		shader = Shader.Find ("Hidden/ALIyerEdon/ColorGrading");
		colorMaterial = Resources.Load ("ColorGrading") as Material;
		colorMaterial.shader = shader;
	}

	// Postprocess the image
	void OnRenderImage (RenderTexture source, RenderTexture destination)
	{

		colorMaterial.SetVector ("_Color", new Vector4(R/1000,G/1000,B/1000,1f));
		colorMaterial.SetFloat ("_Contrast", Contrast);
		colorMaterial.SetFloat ("_Gamma", Gamma);
		colorMaterial.SetFloat ("_Exposure", Exposure);
		colorMaterial.SetFloat ("_VignetteIntensity",vignetteIntensity);
		colorMaterial.SetFloat ("_Saturation", Saturation);

		if (Saturation > 0) {
			colorMaterial.DisableKeyword ("SaturN_OFF");
			colorMaterial.EnableKeyword ("SaturN_ON");
		} else {
			colorMaterial.DisableKeyword ("SaturN_ON");
			colorMaterial.EnableKeyword ("SaturN_OFF");
		}
		
		if (vignetteIntensity != 0) {
			colorMaterial.DisableKeyword ("Vignette_OFF");
			colorMaterial.EnableKeyword ("Vignette_ON");
		} else {
			colorMaterial.DisableKeyword ("Vignette_ON");
			colorMaterial.EnableKeyword ("Vignette_OFF");
		}

		if (toneMapping == ToneMapping.ACES){
			colorMaterial.DisableKeyword ("ACES_OFF");
			colorMaterial.EnableKeyword ("ACES_ON");
		}
		if (toneMapping == ToneMapping.None) {

			colorMaterial.DisableKeyword ("ACES_ON");
			colorMaterial.EnableKeyword ("ACES_OFF");
		}
		Graphics.Blit (source, destination, colorMaterial);

	}

}