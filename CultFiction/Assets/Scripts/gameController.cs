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
    [SerializeField] private Animation animation;
    private int score;
    private int amountOfHits;

    void Start()
    {
        //SpawnPatient();
        videoController.instance.StartVideoHandler();
    }

    public void SpawnPatient()
    {
        try
        {
            var p = Instantiate(patient, new Vector3(-16.04f, 0, -0.65f), Quaternion.identity);
            pController = p.transform.GetChild(0).GetComponent<patientController>();
        }
        catch (MissingComponentException m)
        {
            Debug.LogError(m);
            throw;
        }
    }

    public void PatientHit()
    {
        amountOfHits++;

        if (amountOfHits > 5)
        {
            pController.target = 1;
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(2);
        }
    }

    public void SecondPart()
    {
        animation.Play();
        SpawnPatient();
    }
}