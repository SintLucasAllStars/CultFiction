using UnityEngine;

public enum OrderMode
{
    None,
    Fetching,
    Bringing
}

public class OrderProcessor : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _foodSprite, _speechBalloon, _speechBalloonFoodSprite;

    protected Food _currentFood;
    public Food CurrentFood => _currentFood;

    public bool IsHolding => CurrentFood;

    private OrderMode _orderMode = OrderMode.None;
    public OrderMode OrderMode => _orderMode;

    public bool HasOrder => OrderMode != OrderMode.None;

    private void Start() => SetMode(OrderMode.None);

    public void SetMode(OrderMode mode)
    {
        _orderMode = mode;
        switch (mode)
        {
            case OrderMode.None:
                _speechBalloon.enabled = false;
                _speechBalloonFoodSprite.sprite = null;
                _foodSprite.sprite = null;
                break;
            case OrderMode.Fetching:
                _speechBalloon.enabled = true;
                _speechBalloonFoodSprite.sprite = CurrentFood.Sprite;
                _foodSprite.sprite = null;
                break;
            case OrderMode.Bringing:
                _speechBalloon.enabled = false;
                _speechBalloonFoodSprite.sprite = null;
                _foodSprite.sprite = CurrentFood.Sprite;
                break;
        }
    }

    public void SetFood(Food newFood) => _currentFood = newFood;
}