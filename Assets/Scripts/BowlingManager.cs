using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BowlingManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;

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

    private float _timeTillNextBall = 2;
    private float _audioStart = 1;

    private int _maxballs = 2;
    private int _ballNumber;
    private int _ballsThrown = 0;
    private int _lastpinns;

    private bool _isBowling;
    private bool _firstPinn;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _bowlingBall != null)
        {
            _bowlingBall.Throw();
            _ballsThrown++;
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
                if (!_firstPinn)
                {
                    _audioSource.Play();
                    _audioSource.time = _audioStart;
                    _firstPinn = true;
                }
            }
        }
        _scoreText.text = hitPins + "";
        if (hitPins == _pins.Count)
        {
            Invoke("EndGame", _timeTillNextBall);

            if (_ballsThrown == 1)
            {
                _scoreText.text = "STRIKE";
                return;
            }
            _scoreText.text = "SPARE";
        }
    }

    private void SpawnBall()
    {
        _bowlingBall = Instantiate(_throwing, _spawnPosition, transform.rotation);
        _ballNumber++;
    }

    private void EndGame()
    {
        SceneManager.LoadScene(0);
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
