using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class Interactable : MonoBehaviour
    {
        /*
        private void Update()
        {
            if (isActive)
            {
                float d = Vector3.Distance(player.transform.position, transform.position);
                if (d <= radius)
                {
                    Interact();
                }
            }
        }
        */


        public virtual void Interact()
        {

        }
    }
}