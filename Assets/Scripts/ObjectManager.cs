using System.Collections.Generic;

public enum ObjectMode
{
    Normal,
    BuildMode
}

public class ObjectManager : Singleton<ObjectManager>
{
    private List<ObjectLocation> _objectLocations = new List<ObjectLocation>();

    private PhysicalShopItem _object;
    public PhysicalShopItem Object => _object;

    private ObjectMode _buildMode = ObjectMode.Normal;

    private void Start() => _objectLocations = new List<ObjectLocation>(FindObjectsOfType<ObjectLocation>());

    public void PlaceObject(PhysicalShopItem obj)
    {
        SetMode(ObjectMode.BuildMode);
        _object = obj;
        ShowLocations(true, obj.ShopObject.ObjectType);
    }

    public bool CanPlaceObject(RestaurantObject shopObject)
    {
        if (_objectLocations.Find(x => x.ObjectType == shopObject.ObjectType))
            return true;
        else
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
                break;
            case ObjectMode.BuildMode:
                break;
        }
    }

    public bool InObjectMode() => _buildMode == ObjectMode.BuildMode;
}