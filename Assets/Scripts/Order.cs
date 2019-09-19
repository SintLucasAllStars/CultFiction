using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : TriggerInteractable
{
    private Food _currentOrder;
    public Food CurrentOrder => _currentOrder;

    [SerializeField]
    private SpriteRenderer _speechBubble = null, _foodImage = null;

    protected override bool Interact()
    {
        if (!base.Interact())
            return false;

        if (Player.Instance.OrderProcessor.HasOrder)
        {
            ErrorMessage.Instance.AnnounceError("Please finish the current order first");
            return false;
        }

        Player.Instance.OrderProcessor.SetFood(_currentOrder);
        Player.Instance.OrderProcessor.SetMode(OrderMode.Fetching);
        Enable(false);
        return true;
    }

    public void SetRandomFoodType()
    {
        List<Food> orderableFood = KitchenManager.Instance.Foods;
        if (orderableFood.Count == 0)
            return;

        _currentOrder = orderableFood[Random.Range(0, orderableFood.Count)];
        _foodImage.sprite = _currentOrder.Sprite;
        Enable(true);
    }

    protected override void Enable(bool enabled)
    {
        base.Enable(enabled);
        _speechBubble.enabled = enabled;
        _foodImage.enabled = enabled;
    }
}