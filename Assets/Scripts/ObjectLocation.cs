using UnityEngine;

public class ObjectLocation : ObjectContainer
{
    public override void OnDrop(GameObject obj)
    {
        base.OnDrop(obj);

        _occupiedBy = Instantiate(obj, this.transform);

        KitchenManager.Instance.FindMachines();
    }
}
