using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : Singleton<ObjectManager>
{
    private List<ObjectLocation> _objectLocations = new List<ObjectLocation>();

    private GameObject _object;
    public GameObject Object => _object;

    private void Start() => _objectLocations = new List<ObjectLocation>(FindObjectsOfType<ObjectLocation>());

    public void PlaceObject(GameObject obj)
    {
        _object = obj;
        ShowLocations(true);
    }

    public bool CanPlaceObject(ShopObject shopObject)
    {
        if (_objectLocations.Find(x => x.ObjectType == shopObject.ObjectType))
            return true;
        else
            return false;
    }

    public void ShowLocations(bool enabled)
    {
        for (int i = 0; i < _objectLocations.Count; i++)
            if (!_objectLocations[i].IsUsed) _objectLocations[i].Enable(enabled);
    }
}
