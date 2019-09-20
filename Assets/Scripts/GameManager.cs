using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private bool _isOpened;

    public bool IsOpened => _isOpened;

    [SerializeField]
    private GameObject _customer = null;

    private Coroutine _customerCoroutine;

    private float _time;

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

    private IEnumerator CustomerRoutine()
    {
        yield return new WaitForSeconds(Random.Range(15, 20));
        Instantiate(_customer, transform.position, transform.rotation);
        StartCoroutine(CustomerRoutine());
    }
}