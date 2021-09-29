using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneBehaviour : MonoBehaviour
{
    public float FlightSpeed;
    public float dodgeRange = 10f;

    public GameObject propellor;

    private Rigidbody _PlaneRb;

    private void Start()
    {
        _PlaneRb = gameObject.AddComponent<Rigidbody>();
        _PlaneRb.useGravity = false;
        _PlaneRb.constraints = RigidbodyConstraints.FreezeAll;
    }

    // Update is called once per frame
    void Update()
    {
        FlyForward();

        if (transform.position.z < -10)
        {
            Destroy(gameObject);
        }
    }

    private void FlyForward()
    {
        transform.Translate(transform.forward * FlightSpeed * Time.deltaTime, Space.World);
        propellor.transform.Rotate(0, 0, (36 * FlightSpeed) * Time.deltaTime);

        Collider[] otherplanes = Physics.OverlapSphere(transform.position, dodgeRange);
        foreach (Collider item in otherplanes)
        {
            if (item.CompareTag("AirPlane") && item != this.gameObject)
            {
                MoveOutOfTheWay(item.gameObject.transform);
            }
        }

        if (!_PlaneRb.useGravity)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -15, 15), Mathf.Clamp(transform.position.y, 17.5f, 25), transform.position.z);
        }

    }

    public void Crash()
    {
        print("crash");
        _PlaneRb.useGravity = true;
        _PlaneRb.constraints = RigidbodyConstraints.None;
        _PlaneRb.AddForce(transform.forward * FlightSpeed, ForceMode.Impulse);
        _PlaneRb.AddTorque(new Vector3(Random.Range(-360, 360), Random.Range(-360, 360), Random.Range(-360, 360)));
    }

    private void MoveOutOfTheWay(Transform otherPlane)
    {
        Vector3 direction = transform.position - otherPlane.position;
        direction = new Vector3(-direction.x, direction.y, -direction.z).normalized;
        transform.Translate(direction * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, dodgeRange);
    }
}
