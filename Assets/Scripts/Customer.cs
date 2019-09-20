using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum CustomerMode
{
    WaitingInLine,
    ChoosingMeal,
    WaitingForOrder,
    Eating,
    WaitingToPay
}

public class Customer : Interactable
{
    [SerializeField]
    private GameObject _money = null;

    private CustomerMode _customerMode = CustomerMode.WaitingInLine;

    private Order _order;

    private Coroutine _currentCoroutine;

    private void Start()
    {
        SetMode(CustomerMode.WaitingInLine);
        _order = GetComponentInChildren<Order>();
    }

    protected override bool Interact()
    {
        if (!base.Interact())
            return false;

        switch (_customerMode)
        {
            case CustomerMode.WaitingInLine:

                if (ObjectManager.Instance.InObjectMode())
                {
                    ErrorMessage.Instance.AnnounceError("Please place your object before buying another one");
                    return false;
                }
                if (ObjectManager.Instance.CanPlaceObject(ObjectType.Customer))
                    ObjectManager.Instance.ReplaceObject(this.gameObject, ObjectType.Customer);

                break;
            case CustomerMode.ChoosingMeal:

                ErrorMessage.Instance.AnnounceError("This customer is currently looking through the menu");

                break;
            case CustomerMode.WaitingForOrder:

                if(Player.Instance.OrderProcessor.IsHolding && Player.Instance.OrderProcessor.OrderMode == OrderMode.Bringing)
                {
                    Player.Instance.OrderProcessor.SetMode(OrderMode.None);
                    SetMode(CustomerMode.Eating);
                }

                break;
        }

        return true;
    }

    private IEnumerator SetModeTimed(CustomerMode mode)
    {
        yield return new WaitForSeconds(Random.Range(8, 9));
        SetMode(mode);
    }

    public void SetMode(CustomerMode mode)
    {
        _customerMode = mode;
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);
        switch (mode)
        {
            case CustomerMode.WaitingInLine:
                _currentCoroutine = StartCoroutine(DecreaseHappiness(10f));
                break;
            case CustomerMode.ChoosingMeal:
                _currentCoroutine = StartCoroutine(SetModeTimed(CustomerMode.WaitingForOrder));
                break;
            case CustomerMode.WaitingForOrder:
                _order.SetRandomFoodType();
                _currentCoroutine = StartCoroutine(DecreaseHappiness(10f));
                break;
            case CustomerMode.Eating:
                _currentCoroutine = StartCoroutine(SetModeTimed(CustomerMode.WaitingToPay));
                break;
            case CustomerMode.WaitingToPay:
                Money m = Instantiate(_money, transform.position, transform.rotation).GetComponent<Money>();
                m.Init(_order.CurrentOrder.Price);
                Destroy(this.gameObject);
                break;
        }
    }

    private IEnumerator DecreaseHappiness(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        HappinessMeter.Instance.Decrease();
        _currentCoroutine = StartCoroutine(DecreaseHappiness(GameManager.Instance.WaitTime));
    }
}