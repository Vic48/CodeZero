using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
    private string id;
    private string levelName;
    private float maxTime;
    private float spawnInterval;
    private int spawnMin;
    private int spawnMax;
    private float upgradeInterval;
    private int upgradeCount;
    private float startMinSize;
    private float minSizeUpFrequency;
    private float sizeUpValue;
    private int startMaxSizeInterval;
    private float maxSizeUpFrequency;
    private float startMinSpeed;
    private float minSpeedUpFrequency;
    private float startMaxSpeed;
    private float maxSpeedUpFrequency;
    private float speedUpValue;


    public Level(string id, string levelName, float maxTime, float spawnInterval, int spawnMin, int spawnMax, float upgradeInterval, int upgradeCount, float startMinSize, float minSizeUpFrequency, float sizeUpValue, 
        int startMaxSizeInterval, float maxSizeUpFrequency, float startMinSpeed, float minSpeedUpFrequency, float startMaxSpeed, float maxSpeedUpFrequency, float speedUpValue)
    {
        this.id = id;
        this.levelName = levelName;
        this.maxTime = maxTime;
        this.spawnInterval = spawnInterval;
        this.spawnMin = spawnMin;
        this.spawnMax = spawnMax;
        this.upgradeInterval = upgradeInterval;
        this.upgradeCount = upgradeCount;
        this.startMinSize = startMinSize;
        this.minSizeUpFrequency = minSizeUpFrequency;
        this.sizeUpValue = sizeUpValue;
        this.startMaxSizeInterval = startMaxSizeInterval;
        this.maxSizeUpFrequency = maxSizeUpFrequency;
        this.startMinSpeed = startMinSpeed;
        this.minSpeedUpFrequency = minSpeedUpFrequency;
        this.startMaxSpeed = startMaxSpeed;
        this.maxSpeedUpFrequency = maxSpeedUpFrequency;
        this.speedUpValue = speedUpValue;
    }

    public string GetId() => id;

    public string GetLevelName() => levelName;

    public float GetMaxTime() => maxTime;

    public float GetSpawnInterval() => spawnInterval;

    public int GetSpawnMin() => spawnMin;

    public int GetSpawnMax() => spawnMax;

    public float GetUpgradeInterval() => upgradeInterval;

    public int GetUpgradeCount() => upgradeCount;

    public float GetStartMinSize() => startMinSize;

    public float GetMinSizeUpFrequency() => minSpeedUpFrequency;

    public float GetSizeUpValue() => sizeUpValue;

    public int GetStartMaxSizeInterval() => startMaxSizeInterval;

    public float GetMaxSizeUpFrequency() => maxSizeUpFrequency;

    public float GetStartMinSpeed() => startMinSpeed;

    public float GetMinSpeedUpFrequency() => minSpeedUpFrequency;

    public float GetStartMaxSpeed() => startMaxSpeed;

    public float GetMaxSpeedUpFrequency() => maxSpeedUpFrequency;

    public float GetSpeedUpValue() => speedUpValue;   

}
