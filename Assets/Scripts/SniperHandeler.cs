using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperHandeler : MonoBehaviour
{
    public bool isDucked, shotIsShot;
    private Coroutine shot;

    public AudioSource sniperShot, bulletPassing;

    private void Update()
    {
        if (shot is null)
        {
           shot = StartCoroutine(shoot());
        }        
    }

    private IEnumerator shoot()
    {
        var shottime = Random.Range(2.5f, 3.5f);
        var _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();        
        
        if (!isDucked)
        {
            //Wait for X amount otherwise you will get shot at to quick.
            yield return new WaitForSecondsRealtime(Random.Range(4, 8));

            Debug.Log("shot sound");
            //Play the shoot sound.
            sniperShot.Play();
            shotIsShot = true;
            yield return new WaitForSecondsRealtime(shottime);

            //This checks of the player is still ducked after the shot is fired.
            if (!isDucked)
            {
                //Plays the getting shot sound FX.
                var _player = GameObject.Find("Player").GetComponent<AudioSource>();
                _player.Play();

                //Shoots the player and kills him.
                Debug.Log("Shot");
                _gameManager.PlayerDeath();

                //Disables the sniper script after you died.
                var sniperHandeler = gameObject.GetComponent<SniperHandeler>().enabled = false;
            }

        }
        else
        {
            if (shotIsShot)
            {
                Debug.Log("cancel shot");

                //Plays an bullet passing sound FX.
                yield return new WaitForSecondsRealtime(shottime);
                bulletPassing.Play();
            }

            shotIsShot = false;

        }

        shot = null;
    }
}
