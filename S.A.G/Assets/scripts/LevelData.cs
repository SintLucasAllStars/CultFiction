using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class LevelData
{
    //public int level;
    public int waveAmount;
    public int planesPerWave;
    public float timeBetweenWaves;
    public float timeBetweenPlanes;

    public LevelData(int level, int waveAmount, int planesPerWave, float timeBetweenWaves = 0, float timeBetweenPlanes = 0)
    {
        //this.level = level;
        this.waveAmount = waveAmount;
        this.planesPerWave = planesPerWave;
        this.timeBetweenWaves = timeBetweenWaves;
        this.timeBetweenPlanes = timeBetweenPlanes;
    }
}
