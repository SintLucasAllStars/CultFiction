using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubbleScript : MonoBehaviour {

    [SerializeField] float _speed;

    private void Update () {
        transform.position += Vector3.back * (_speed * Time.deltaTime);
    }

	private void OnTriggerEnter(Collider other)
	{
			if(other.gameObject.CompareTag("Player"))
            GameController.instance.EndGame();
    }
}
