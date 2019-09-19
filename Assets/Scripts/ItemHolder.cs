using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    protected Food _currentFood;
    public Food CurrentFood => _currentFood;

    [SerializeField]
    protected SpriteRenderer _itemSpriteRenderer;

    public bool IsHolding => CurrentFood;

    public void SetFood(Food newFood)
    {
        Enable(true);
        _currentFood = newFood;
        _itemSpriteRenderer.sprite = CurrentFood.Sprite;
    }

    public void ResetFood()
    {
        Enable(false);
        _currentFood = null;
        _itemSpriteRenderer.sprite = null;
    }

    protected virtual void Enable(bool enabled) => _itemSpriteRenderer.enabled = enabled;
}
