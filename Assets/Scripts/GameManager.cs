using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private bool _isOpened;

    public bool IsOpened => _isOpened;

    [SerializeField]
    private GameObject _customer = null;

    private Coroutine _customerCoroutine;

    private float _spawnDelay;

    private float _waitingTime;
    public float WaitTime => _waitingTime;

    public void Open()
    {
        if (ObjectManager.Instance.GetChairs().Length == 0)
        {
            ErrorMessage.Instance.AnnounceError("Please place a table for customers to sit on");
            return;
        }

        if (ObjectManager.Instance.GetMachines().Length == 0)
        {
            ErrorMessage.Instance.AnnounceError("Please place a machine for making food");
            return;
        }

        _isOpened = true;
        _customerCoroutine = StartCoroutine(CustomerRoutine());
    }

    private void Update()
    {
        if (_isOpened)
        {
            _spawnDelay = Time.time;
            _waitingTime = 5 + (_spawnDelay - 30) * -0.06f;
        }
    }

    private IEnumerator CustomerRoutine()
    {
        float spawnDelay = 17 + (_spawnDelay - 30) * -0.07f;
        yield return new WaitForSeconds(spawnDelay);
        if(FindObjectsOfType<Customer>().Length < 12)
        {
            Instantiate(_customer, transform.position, transform.rotation);
        }
        StartCoroutine(CustomerRoutine());
    }
}