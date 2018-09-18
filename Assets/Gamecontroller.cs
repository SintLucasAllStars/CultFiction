using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamecontroller : MonoBehaviour
{

    public static Gamecontroller instance;
    public List<Vector2> vainPosition;
    private int lives;
    public int wave = 1;
    public int points = 100;

    [SerializeField]
    private Transform cloneParrent;
    [SerializeField]
    private GameObject defenderPrefab;
    public List<Defender> placedDefenders;
    public List<DefenderStats> defenders;
    [SerializeField]
    private GameObject heroinPrefab;
    public List<Heroin> heroin;


    [SerializeField]
    private int defaultEnemies = 4;
    [SerializeField]
    private float enemiesFactor = 1.25f;
    [SerializeField]
    private float defaultWaitTime = .1f;
    [SerializeField]
    private float waitTimeFactor = -0.5f;

    public float maxDamage, maxFireRate, maxRange;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Use this for initialization
    void Start()
    {
        defenders = new List<DefenderStats>();
        defenders.Add(new DefenderStats("Vincent", 0, 50, 100, 50, 150, 2, 6, 100, 300, 4));
        defenders.Add(new DefenderStats("lance", 1, 20, 40, 100, 200, 6, 15, 150, 190, 20));

        heroin = new List<Heroin>();

        UiManager.instance.UpdateUI();
        StartCoroutine(Wave());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Wave()
    {
        int enemies = (int)Factor(defaultEnemies, enemiesFactor);
        float waitTime = Factor(defaultWaitTime, waitTimeFactor);

        float defautWaitTime = 10f;
        yield return new WaitForSeconds(defautWaitTime);
        UiManager.instance.nextWaveTime = (int)(defautWaitTime + enemies * waitTime);

        int enemiesSpawned = 0;
        while (enemiesSpawned < enemies)
        {

            yield return new WaitForSeconds(waitTime);
            enemiesSpawned++;
            heroin.Add(Instantiate<Heroin>(heroinPrefab.GetComponent<Heroin>(), cloneParrent));

        }
        wave++;
        StartCoroutine(Wave());
    }

    public float Factor(float defaultF, float factor)
    {
        return defaultF * Mathf.Pow(wave, factor);
    }

    public void SubtractLife()
    {
        lives--;
    }

    public void BuyDefender(int i)
    {
        DefenderStats defender = defenders[i];
        if (defender.price <= points)
        {
            points -= defender.price;
            placedDefenders.Add(Instantiate<Defender>(defenderPrefab.GetComponent<Defender>(), cloneParrent));
            placedDefenders[placedDefenders.Count - 1].stats = defender;
            UiManager.instance.UpdateUI();
        }
    }

    public void UpgradeDamage()
    {
        DefenderStats defender = SelectedDefender().stats;
        if (defender.upgradePrice <= points)
        {
            points -= defender.upgradePrice;
            defender.damage = defender.DamageAfterUpgrade();
        }
        UiManager.instance.UpdateUI();
    }
    public void UpgradeFireRate()
    {
        DefenderStats defender = SelectedDefender().stats;
        if (defender.upgradePrice <= points)
        {
            points -= defender.upgradePrice;
            defender.fireRate = defender.FireRateAfterUpgrade();
        }
        UiManager.instance.UpdateUI();

    }
    public void UpgradeRange()
    {
        DefenderStats defender = SelectedDefender().stats;
        if (defender.upgradePrice <= points)
        {
            points -= defender.upgradePrice;
            defender.range = defender.RangeAfterUpgrade();
        }
        UiManager.instance.UpdateUI();

    }

    public Defender SelectedDefender()
    {
        Defender selectedDefender = null;
        for (int i = 0; i < placedDefenders.Count; i++)
        {
            selectedDefender = placedDefenders[i].selected ? placedDefenders[i] : null;
            if (selectedDefender != null)
            {
                break;

            }
        }
        return selectedDefender;

    }

    public void DeselectAllDefenders()
    {
        for (int i = 0; i < placedDefenders.Count; i++)
        {
            placedDefenders[i].selected = false;
        }
        UiManager.instance.UpdateUI();
    }
}
