using UnityEngine;
using UnityEngine.UI;

/**
 * Simple material swapper.
 * 
 * @author J.C. Wichman - Inner Drive Studios
 */
public class MaterialControlPanel : MonoBehaviour {

	[SerializeField] private GameObject target;
	[SerializeField] private Text materialText;
	[SerializeField] private Material[] materials;
	private MeshRenderer[] renderers;
	private int _currentMaterialIndex = 0;
	

	private void Awake()
	{
		renderers = target.GetComponentsInChildren<MeshRenderer>();
	}

	private void Update()
	{
		//update the material info if required
		if (materialText != null)
		{
			if (materials.Length == 0)
			{
				materialText.text = "-";
			}
			else
			{
				if (_currentMaterialIndex >= 0)
				{
					materialText.text = "" + (_currentMaterialIndex + 1) + "/" + materials.Length;
				}
				else
				{
					materialText.text = "R";
				}
			}
		}
	}

	public void NextMaterial()
	{
		if (materials.Length == 0) return;
		_currentMaterialIndex = (_currentMaterialIndex + 1) % materials.Length;
		updateMaterial();
	}

	public void PreviousMaterial()
	{
		if (materials.Length == 0) return;
		_currentMaterialIndex = (_currentMaterialIndex - 1 + materials.Length) % materials.Length;
		updateMaterial();
	}

	private void updateMaterial()
	{
		for (int i = 0; i < renderers.Length; i++)
		{
			renderers[i].sharedMaterial = 
				materials[_currentMaterialIndex>-1?_currentMaterialIndex:Random.Range(0, materials.Length)];
		}
	}

}
