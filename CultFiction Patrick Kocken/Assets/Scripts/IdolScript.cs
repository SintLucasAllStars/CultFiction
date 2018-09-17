using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdolScript : MonoBehaviour {

    [SerializeField] private GameObject _rubble;
    [SerializeField] private BoxCollider _finish;


    private void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.CompareTag("Player")){
                Destroy(transform.GetChild(0).gameObject);
            _rubble.SetActive(true);
            _finish.enabled = true;
            GetComponent<Animation>().enabled = true;

        }
	}
}
