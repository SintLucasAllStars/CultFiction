using UnityEngine;

[System.Serializable]
public class patient
{
    public GameObject person;
    public float raduis;
    public GameObject raduisObject;

    public Vector3[] positions = new Vector3[2] {
        new Vector3(0, 0, 0),
        new Vector3(-16.04f, 0, -0.65f)
    };

    public patient(GameObject _person, GameObject _raduisObject, float _raduis)
    {
        this.person = _person;
        this.raduis = _raduis;
        this.raduisObject = _raduisObject;
        setRaduis();
    }

    public void setRaduis()
    {
        float size = raduis / 2;
        Debug.Log(size);
        raduisObject.transform.localScale = new Vector3(size, 0.1f, size);
    }
}