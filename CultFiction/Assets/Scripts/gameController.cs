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
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip[] clips;
    private int score;

    void Start()
    {
        SpawnPatient();
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

    public void NextAudioClip(int clipIndex)
    {
        source.clip = clips[clipIndex];
        source.Play();
    }
}