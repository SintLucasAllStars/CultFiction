using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : ObjectContainer
{
    public override void OnDrop(GameObject obj)
    {
        _occupiedBy = obj;

        //Set position and rotation of the customer to chair.
        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation;


        obj.GetComponent<Customer>().SetMode(CustomerMode.ChoosingMeal);
        base.OnDrop(obj);
    }
}
