using System.Collections.Generic;
using UnityEngine;

public enum ObjectMode
{
    Normal,
    BuildMode
}

public class ObjectManager : Singleton<ObjectManager>
{
    private List<ObjectContainer> _objectLocations = new List<ObjectContainer>();

    private GameObject _object;
    public GameObject Object => _object;

    private ObjectMode _buildMode = ObjectMode.Normal;

    private void Start() => FindLocations();

    public void FindLocations() => _objectLocations = new List<ObjectContainer>(FindObjectsOfType<ObjectContainer>());

    public void PlaceObject(RestaurantObject obj)
    {
        SetMode(ObjectMode.BuildMode);
        _object = obj.Object;
        ShowLocations(true, obj.ObjectType);
    }

    public void ReplaceObject(GameObject obj, ObjectType type)
    {
        SetMode(ObjectMode.BuildMode);
        _object = obj;
        ShowLocations(true, type);
    }

    public bool CanPlaceObject(ObjectType objectType)
    {
        for (int i = 0; i < _objectLocations.Count; i++)
        {
            if (_objectLocations[i].ObjectType == objectType && !_objectLocations[i].IsUsed)
                return true;
        }
        return false;
    }

    public void ShowLocations(bool enabled, ObjectType objectType)
    {
        for (int i = 0; i < _objectLocations.Count; i++)
            if (!_objectLocations[i].IsUsed && _objectLocations[i].ObjectType == objectType) _objectLocations[i].Enable(enabled);
    }

    public void ShowAllLocations(bool enabled)
    {
        for (int i = 0; i < _objectLocations.Count; i++)
            if (!_objectLocations[i].IsUsed) _objectLocations[i].Enable(enabled);
    }

    public void SetMode(ObjectMode mode)
    {
        _buildMode = mode;
        switch (mode)
        {
            case ObjectMode.Normal:
                _object = null;
                ShowAllLocations(false);
                break;
            case ObjectMode.BuildMode:
                break;
        }
    }

    public bool InObjectMode() => _buildMode == ObjectMode.BuildMode;

    public void StopObjectMode() => SetMode(ObjectMode.Normal);

    public Chair[] GetChairs() => FindObjectsOfType<Chair>();

    public Machine[] GetMachines() => FindObjectsOfType<Machine>();
}