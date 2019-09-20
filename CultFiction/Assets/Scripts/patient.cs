using UnityEngine;

[System.Serializable]
public class patient
{
    public GameObject person;
    public float radius;
    public GameObject radiusObject;

    public Vector3[] positions = new Vector3[2] {
        new Vector3(0, 0, 0),
        new Vector3(-16.04f, 0, -0.65f)
    };

    public patient(GameObject _person, GameObject _radiusObject, float _radius)
    {
        this.person = _person;
        this.radius = _radius;
        this.radiusObject = _radiusObject;
        setRadius();
    }

    public void setRadius()
    {
        float size = radius / 2;
        Debug.Log(size);
        radiusObject.transform.localScale = new Vector3(size, 0.1f, size);
    }
}