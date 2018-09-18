using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{

    public static UiManager instance;
    public Sprite[] defendersImg;

    int newDefenderIndex = 0;
    [SerializeField]
    private Text wave;
    [SerializeField]
    private Text nextWave;
    [SerializeField]
    private Text points;

    [SerializeField]
    private Text defenderName;
    [SerializeField]
    private Image defenderImage;
    [SerializeField]
    private GameObject leftArrow;
    [SerializeField]
    private GameObject rightArrow;


    [SerializeField]
    private Image damageSlider;
    [SerializeField]
    private Text damageUpgrade;

    [SerializeField]
    private Image fireRateSlider;
    [SerializeField]
    private Text fireRateUpgrade;
    [SerializeField]
    private Image rangeSlider;
    [SerializeField]
    private Text rangeUpgrade;

    [SerializeField]
    private Text priceText;

    public int nextWaveTime = 10;
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
        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateUI()
    {

        wave.text = "Wave :\n" + Gamecontroller.instance.wave;
        points.text = "Points :\n" + Gamecontroller.instance.points;
        Defender selectedDefender = Gamecontroller.instance.SelectedDefender();
        DefenderStats newDefender = null;
        if (selectedDefender != null)
        {
            newDefender = selectedDefender.stats;
            leftArrow.SetActive(false);
            rightArrow.SetActive(false);
            damageUpgrade.transform.parent.gameObject.SetActive(newDefender.DamageAfterUpgrade() != newDefender.damage);
            fireRateUpgrade.transform.parent.gameObject.SetActive(newDefender.FireRateAfterUpgrade() != newDefender.fireRate);
            rangeUpgrade.transform.parent.gameObject.SetActive(newDefender.RangeAfterUpgrade() != newDefender.range);

            priceText.text = "Sell:\n" + selectedDefender.stats.price;

        }
        else
        {
            leftArrow.SetActive(true);
            rightArrow.SetActive(true);
            damageUpgrade.transform.parent.gameObject.SetActive(false);
            fireRateUpgrade.transform.parent.gameObject.SetActive(false);
            rangeUpgrade.transform.parent.gameObject.SetActive(false);

            newDefender = Gamecontroller.instance.defenders[newDefenderIndex];
            priceText.text = "buy:\n" + newDefender.price;

        }

        defenderName.text = newDefender.name;
        defenderImage.sprite = UiManager.instance.defendersImg[newDefender.image];

        

        float currentDamage = Percentage(Gamecontroller.instance.maxDamage, newDefender.damage);
        float nextDamage = Percentage(Gamecontroller.instance.maxDamage, newDefender.DamageAfterUpgrade());
        damageSlider.fillAmount = currentDamage;
        damageSlider.transform.GetChild(0).GetComponent<Image>().fillAmount = nextDamage;
        damageUpgrade.text = "+ " + newDefender.upgradePrice;

        float currentFireRate = Percentage(Gamecontroller.instance.maxFireRate, newDefender.fireRate);
        float nextFireRate = Percentage(Gamecontroller.instance.maxFireRate, newDefender.FireRateAfterUpgrade());
        fireRateSlider.fillAmount = currentFireRate;
        fireRateSlider.transform.GetChild(0).GetComponent<Image>().fillAmount = nextFireRate;
        fireRateUpgrade.text = "+ " + newDefender.upgradePrice;


        float currentRange = Percentage(Gamecontroller.instance.maxRange, newDefender.range);
        float nextFireRange = Percentage(Gamecontroller.instance.maxRange, newDefender.RangeAfterUpgrade());
        rangeSlider.fillAmount = currentRange;
        rangeSlider.transform.GetChild(0).GetComponent<Image>().fillAmount = nextFireRange;
        rangeUpgrade.text = "+ " + newDefender.upgradePrice;

    }



    float Percentage (float max, float current)
    {
        return 1 / max  * current;
    }

    public void NextDefender(bool left)
    {
        int plus = 0;

        if (newDefenderIndex > 0 && left)
        {
            plus = -1;

        }
        else if (newDefenderIndex < Gamecontroller.instance.defenders.Count - 1 && !left)
        {
            plus = +1;
        }

        newDefenderIndex += plus;

        UpdateUI();

    }

    public void Buy()
    {
        Defender selectedDefender = Gamecontroller.instance.SelectedDefender();
        if (selectedDefender == null)
        {

            Gamecontroller.instance.BuyDefender(newDefenderIndex);
        }
        else
        {
            selectedDefender.Sell();
            UpdateUI();
        }
    }

    public IEnumerator Timer()
    {
        while (true)
        {
            nextWave.text = "Next Wave :\n" + nextWaveTime;
            yield return new WaitForSeconds(1);
            if (nextWaveTime > 0) { nextWaveTime--; }
        }
    }

}
