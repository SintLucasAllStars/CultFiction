using UnityEngine;

public class patient
{
    public GameObject person;
    public float radius;

    public Vector3[] positions = new Vector3[2] {
        new Vector3(2.87f, 0.5f,-7f),
        new Vector3(-16.04f, 0, -0.65f)
    };

    public patient(GameObject _person, float _radius)
    {
        this.person = _person;
        this.radius = _radius;
    }
}