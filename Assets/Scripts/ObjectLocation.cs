using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
{
    Table,
    Machine
}

public class ObjectLocation : IInteractable
{
    [SerializeField]
    private ObjectType _objectType = ObjectType.Table;
    public ObjectType ObjectType => _objectType;

    [SerializeField]
    private SpriteRenderer _location = null;

    private GameObject _object = null;

    private bool _isUsed = false;
    public bool IsUsed => _isUsed;

    private void Start() => Enable(false);

    public void OnDrop(GameObject obj)
    {
        _isUsed = true;

        _object = Instantiate(obj, this.transform);

        Enable(false);

        ObjectManager.Instance.ShowAllLocations(false);
        ObjectManager.Instance.SetMode(ObjectMode.Normal);
    }

    public void Enable(bool enabled) => _location.gameObject.SetActive(enabled);

    private void OnMouseDown() => Interact();

    protected override void Interact()
    {
        if (!ObjectManager.Instance.InObjectMode())
            return;

        OnDrop(ObjectManager.Instance.Object.ShopObject.Object);
    }
}
