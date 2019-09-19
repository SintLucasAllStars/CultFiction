using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : TriggerInteractable
{
    [SerializeField]
    private FoodName _foodType = FoodName.Coconut;
    public FoodName FoodType => _foodType;

    new private void Start()
    {
        _collider = GetComponent<Collider2D>();
        Enable(true);
    }

    protected override bool Interact()
    {
        if (!base.Interact())
            return false;
        if (!Player.Instance.OrderProcessor.IsHolding)
            return false;

        if(Player.Instance.OrderProcessor.CurrentFood.FoodName != _foodType)
        {
            ErrorMessage.Instance.AnnounceError("You need a different machine for that");
            return false;
        }

        Player.Instance.OrderProcessor.SetMode(OrderMode.Bringing);
        return true;
    }
}
