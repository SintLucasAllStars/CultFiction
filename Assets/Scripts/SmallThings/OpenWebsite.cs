using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWebsite : MonoBehaviour,IExtraFunction {
	public string link;

	public void ExtraFunction(){
		Application.OpenURL (link);
	}
}
