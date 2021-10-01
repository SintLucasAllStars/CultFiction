using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameManager _gm;

    private float _delayTime = .5f;

    private bool _forward = true;
    private bool _delay;

    void Start()
    {
        _gm = GameManager.gameManager;
    }

    // Update is called once per frame
    void Update()
    {
        if (_gm.gameStarted) MoveForward();
        
        if (Input.GetButtonDown("Jump") && !_delay && _gm.gameStarted)
        {
            _forward = !_forward;
            _gm.source.PlayOneShot(_gm.move, .1f);
            if (float.Parse(string.Join("", _gm.data.score)) < 30)
            {
                StartCoroutine(CreateDelay());
            }
        }

        if (transform.position.y < -1) die();
    }

    IEnumerator CreateDelay()
    {
        _delay = true;
        yield return new WaitForSeconds(_delayTime);
        _delay = false;
    }

    void MoveForward()
    {
        transform.position += (_forward? Vector3.forward * float.Parse(_gm.data.StringListToFloat(_gm.data.speed)) : Vector3.left * float.Parse(_gm.data.StringListToFloat(_gm.data.speed)) ) * Time.deltaTime;
    }

    void die()
    {
        _gm.source.PlayOneShot(_gm.death, 1);
        _gm.gameStarted = false;
        gameObject.transform.position = Vector3.up;
        _forward = true;
        _gm.DeathScreen();
    }
}
