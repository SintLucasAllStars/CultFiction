using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Death : MonoBehaviour
{
   public int Health;
   public GameObject expPrefab;
   


   private void Start()
   {
      Health = 5;
      
      
   }
   private void OnCollisionEnter(Collision other)
   {
      if (other.gameObject.CompareTag("PlayerBullet"))
      {
         Health--;
         if (Health == 0 || Health < 0)
         {
            Dies();
         }
      }
   }

   private void Dies()
   {
      Instantiate (expPrefab, this.transform.position, Quaternion.identity);
      Singleton.brain.addScore(amt:1);
      SoundOnPlayer.brain.PlaySound();
      Destroy(this.gameObject);
      
      
   }
}
