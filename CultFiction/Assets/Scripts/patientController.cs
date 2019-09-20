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
    public patient _patient;

    /// <summary>
    /// 0 = comming in
    /// 1 = dead or alive
    /// </summary>
    public int target = 0;

    private float speed = 0.15f;

    void Awake()
    {
        Debug.Log("HERE");
        _patient = new patient(this.gameObject, Random.Range(2, 4));
        state = State.spawned;
        Wait(5f, () => { state = State.onTheTable; });
    }
    void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, _patient.positions[target], speed);
    }
}