using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Block : MonoBehaviour {

     public Vector3 pos1; 
     public Vector3 pos2; 
     public float speed = 1.0f;
 
     void Update() {
         transform.position = Vector3.Lerp (pos1, pos2, Mathf.PingPong(Time.time*speed, 1.0f));
     }
 } 
