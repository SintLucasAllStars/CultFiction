using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderStats
{
    public string name;
    public int image;

    public int damageMin;
    public int damage;
    public int damageMax;

    public float rangeMin;
    public float range;
    public float rangeMax;

    public float fireRateMin;
    public float fireRate;
    public float fireRateMax;

    public int price;
    public int upgradePrice;
    public int upgrades;

    public DefenderStats()
    {

    }

    public DefenderStats(string name, int image, int damageMin, int damageMax, float rangeMin, float rangeMax, float fireRateMin, float fireRateMax, int price, int upgradePrice,int upgrades)
    {
        this.name = name;
        this.image = image;

        this.damageMin = damageMin;
        this.damage = damageMin;
        this.damageMax = damageMax;

        this.rangeMin = rangeMin;
        this.range = rangeMin;
        this.rangeMax = rangeMax;

        this.fireRateMin = fireRateMin;
        this.fireRate = fireRateMin;
        this.fireRateMax = fireRateMax;

        this.price = price;
        this.upgradePrice = upgradePrice;
        this.upgrades = upgrades;
    }

    public int DamageAfterUpgrade()
    {
        int addOn = (damageMax - damageMin) / upgrades;
        int next = damage + addOn;
        return next < damageMax ? next : damage ;
    }

    public float FireRateAfterUpgrade()
    {
        float addOn = (fireRateMax - fireRateMin) / upgrades;
        float next = fireRate + addOn;
        return next < fireRateMax ? next : fireRate;
    }
    public float RangeAfterUpgrade()
    {
        float addOn = (rangeMax - rangeMin) / upgrades;
        float next = range + addOn;
        return next < rangeMax ? next : range;
    }

}
