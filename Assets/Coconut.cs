using UnityEngine;

public class Coconut : MonoBehaviour
{
    private bool _gravity;

    private void OnCollisionEnter(Collision coll)
    {
        if (!_gravity && coll.gameObject.CompareTag("Stone"))
        {
            // drop
            _gravity = true;
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.useGravity = true;
        }
    }
    
}