using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DancingManager : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private BowlingManager _bowlingManager;

    [SerializeField]
    private GameObject _shakingCanvas;

    [SerializeField]
    private List<Arrow> _arrowPrefabs;

    [SerializeField]
    private Transform _spawnPosition;

    [SerializeField]
    private Transform _correctPosition;

    [SerializeField]
    private int _numberOfDances;

    [SerializeField]
    private float _maxRange;

    [SerializeField]
    private float _arrowRate;

    private List<Arrow> _arrows = new List<Arrow>();

    private float _points;
    private float _arrowsPast;
    private float _rotationModifier = 10;
    private float _powerModifier = 5;

    private int _power = 0;

    private bool _isShaking = true;
    private bool _onLeft;
    private bool _isFinished;

    private Arrow _closestArrow = null;

    private void Start()
    {
        _animator.SetInteger("DanceNumber1", Random.Range(0, _numberOfDances - 1));
        _animator.SetInteger("DanceNumber2", Random.Range(0, _numberOfDances - 1));
        Invoke("ShakingEnd", 5);
    }

    private void ShakingEnd()
    {
        _shakingCanvas.SetActive(false);
        _animator.SetTrigger("DoneShaking");
        _isShaking = false;
        AddArrow();
    }

    private void AddArrow()
    {
        _arrows.Add(Instantiate(_arrowPrefabs[Random.Range(0, _arrowPrefabs.Count)], _spawnPosition));
        if (!_isFinished)
        {
            Invoke("AddArrow", 1 / _arrowRate);
            _points -= _maxRange;
            _arrowsPast++;
        }
    }

    private void ClosestArrow(DirectionsEnum directionsEnum)
    {
        float smallestDistance = 0;

        for (int i = 0; i < _arrows.Count; i++)
        {
            var currentDistance = Vector3.Distance(_arrows[i].transform.position, _correctPosition.position);
            if (currentDistance < _maxRange && (_closestArrow == null || smallestDistance > currentDistance))
            {
                _closestArrow = _arrows[i];
                smallestDistance = currentDistance;
            }
        }

        if (_closestArrow != null)
        {
            if (_closestArrow._direction == directionsEnum)
            {
                _points += _maxRange - smallestDistance;
            }
            Destroy(_closestArrow.gameObject);
            _arrows.Remove(_closestArrow);
        }
    }

    public void Finish()
    {
        if (!_isFinished)
        {
            _isFinished = true;
            _points = _points / _arrowsPast;
            _bowlingManager.StartBowling(_power * _powerModifier, 90, _points / _rotationModifier);
        }
    }

    void Update()
    {
        if (_isShaking)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && !_onLeft)
            {
                _onLeft = true;
                _power++;
            }

            if (Input.GetKeyDown(KeyCode.RightArrow) && _onLeft)
            {
                _onLeft = false;
                _power++;
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ClosestArrow(DirectionsEnum.Up);
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ClosestArrow(DirectionsEnum.Down);
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ClosestArrow(DirectionsEnum.Left);
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ClosestArrow(DirectionsEnum.Right);
        }
    }
}
