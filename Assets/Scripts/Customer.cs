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

    private IEnumerator Wait(CustomerMode mode)
    {
        yield return new WaitForSeconds(Random.Range(8, 9));
        SetMode(mode);
    }

    public void SetMode(CustomerMode mode)
    {
        _customerMode = mode;
        switch (mode)
        {
            case CustomerMode.ChoosingMeal:
                StartCoroutine(Wait(CustomerMode.WaitingForOrder));
                break;
            case CustomerMode.WaitingForOrder:
                _order.SetRandomFoodType();
                break;
            case CustomerMode.Eating:
                StartCoroutine(Wait(CustomerMode.WaitingToPay));
                break;
            case CustomerMode.WaitingToPay:
                Money m = Instantiate(_money, transform.position, transform.rotation).GetComponent<Money>();
                m.Init(_order.CurrentOrder.Price);
                Destroy(this.gameObject);
                break;
        }
    }
}