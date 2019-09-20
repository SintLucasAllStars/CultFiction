using UnityEngine;

public class patientController : extendedFunctions
{
    [SerializeField]
    private enum State
    {
        spawned,
        onTheTable,
        lived,
        died
    }

    [SerializeField] private State state;

    public GameObject radiusObject;
    public float radiusOption;
    public patient _patient;

    /// <summary>
    /// 0 = comming in
    /// 1 = dead or alive
    /// </summary>
    public int target = 0;

    private float speed = 0.15f;

    void Update()
    {
        transform.position = Vector3.MoveTowards(this.transform.position, _patient.positions[target], speed);
    }

    public void InitPatient()
    {
        _patient = new patient(this.gameObject, radiusObject, Random.Range(2, 4));
        state = State.spawned;
        Wait(5f, () => { state = State.onTheTable; });
        Debug.Log(state);
    }

    public void RemovePatient()
    {
        state = State.lived;
        target = 1;
        Wait(5f, () =>
        {
            gameController.instance.SpawnPatient();
            Destroy(this.gameObject);
        });
    }
}