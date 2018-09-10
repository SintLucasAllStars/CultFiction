using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBoat : MonoBehaviour {
	public List<GameObject> rafts =  new List<GameObject>();
	public LayerMask ignoreLayer;
	public GameObject endScreen;
	// Use this for initialization
	void Start () {
		endScreen.SetActive (false);
		
	}
	
	// Update is called once per frame
	void Update () {
		if (rafts.Count == 3)
		{
			Debug.Log ("complete!");
			endScreen.SetActive (true);
		}
	}
	void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Raft"))
		{
			rafts.Add (other.gameObject);
		}
	}
	void OnTriggerExit(Collider other){
		if (other.CompareTag ("Raft"))
		{
			rafts.Remove (other.gameObject);
		}
	}
	public void Exit(){
		Application.Quit ();

	}
}
