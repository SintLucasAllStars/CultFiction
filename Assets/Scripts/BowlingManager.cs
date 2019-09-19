using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BowlingManager : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    [SerializeField]    
    private Transform _bowlingPov;

    [SerializeField]
    private Throwing _throwing;

    [SerializeField]
    private Vector3 _spawnPosition;

    [SerializeField]
    private List<GameObject> _pins;

    [SerializeField]
    private Text _scoreText;

    private Throwing _bowlingBall;

    private float _timeTillNextBall = 1;
    private int _maxballs = 2;
    private int _ballNumber;

    private bool _isBowling;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _bowlingBall != null)
        {
            _bowlingBall.Throw();
        }

        HitPins();

        if (_isBowling)
        {
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, _bowlingPov.position, 10 * Time.deltaTime);
            _camera.transform.rotation = Quaternion.Lerp(_camera.transform.rotation, _bowlingPov.rotation, 10 * Time.deltaTime);
        }
    }

    public void StartBowling(float power, float throwingAngle, float rotationSpeed)
    {
        _throwing._power = power;
        _throwing._throwingAngle = throwingAngle;
        _throwing._rotationSpeed = rotationSpeed;
        _isBowling = true;
        SpawnBall();
    }

    void HitPins()
    {
        int hitPins = 0;

        for (int i = 0; i < _pins.Count; i++)
        {
            if (_pins[i].transform.eulerAngles.x > 45 && _pins[i].transform.eulerAngles.x < 315 || _pins[i].transform.eulerAngles.z > 45 && _pins[i].transform.eulerAngles.z < 315)
            {
                hitPins++;
            }
        }
        _scoreText.text = hitPins + "";
    }

    private void SpawnBall()
    {
        _bowlingBall = Instantiate(_throwing, _spawnPosition, transform.rotation);
        _ballNumber++;
    }

    private void EndGame()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Throwing ball = other.GetComponent<Throwing>();
        if (ball != null)
        {
            Destroy(ball);
            if (_ballNumber != _maxballs)
            {

                Invoke("SpawnBall", _timeTillNextBall);
            }
            else
            {
                Invoke("EndGame", _timeTillNextBall);
            }
        }
    }
}
