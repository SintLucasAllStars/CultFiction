using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
{
    Table,
    Machine
}

public class ObjectLocation : MonoBehaviour
{
    [SerializeField]
    private ObjectType _objectType;
    public ObjectType ObjectType => _objectType;

    [SerializeField]
    private SpriteRenderer _location;

    private GameObject _object;

    private bool _isUsed;
    public bool IsUsed => _isUsed;

    private void Start() => Enable(false);

    public void OnDrop(GameObject obj)
    {
        _isUsed = true;
        _object = Instantiate(obj, this.transform);
        Enable(false);
        ObjectManager.Instance.ShowLocations(false);
    }

    private void OnMouseDown() => OnDrop(ObjectManager.Instance.Object);

    public void Enable(bool enabled) => _location.gameObject.SetActive(enabled);
}
