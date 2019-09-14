using UnityEngine;

public class patient
{
    public GameObject person;
    public float raduis;
    public GameObject raduisObject;

    public static float[] positions = new float[2] { 10, 30 };

    public patient(GameObject _person, float _raduis)
    {
        this.person = _person;
        this.raduis = _raduis;
        this.raduisObject = person.transform.GetChild(0).gameObject;
    }

    public void setRaduis()
    {
        float size = raduis / 2;
        raduisObject.transform.localScale = new Vector3(size, size, size);
    }
}