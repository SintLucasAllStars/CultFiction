using UnityEngine;

public class gameController : MonoBehaviour
{
    #region Singleton

    public static gameController instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion

    [SerializeField] private GameObject patient;
    private patientController pController;
    public bool isPhoneClicked = false;
    private int score;

    void Start()
    {
        SpawnPatient();
        videoController.instance.StartVideoHandler();
        Debug.Log("1");
    }

    public void SpawnPatient()
    {
        try
        {
            var p = Instantiate(patient, new Vector3(-16.04f, 0, -0.65f), Quaternion.identity);
            pController = p.GetComponent<patientController>();
            pController.InitPatient();
        }
        catch (MissingComponentException m)
        {
            Debug.LogError(m);
            throw;
        }
    }

    public void PatientHit()
    {
        pController.RemovePatient();
        score += 100;
        uiController.instance.ShowScore(score);
    }
}