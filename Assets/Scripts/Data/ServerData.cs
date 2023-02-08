using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ServerData
{
    public List<RefLevel> RefLevel;
    public List<RefUpgrade> RefUpgrade;
    public List<RefDebuff> RefDebuff;
}

[System.Serializable]
public class RefLevel
{
    public string id;
    public string levelName;
    public float maxTime;
    public float spawnInterval;
    public int spawnMin;
    public int spawnMax;
    public float upgradeInterval;
    public int upgradeCount;
    public float startMinSize;
    public float minSizeUpFrequency;
    public float sizeUpValue;
    public int startMaxSizeInterval;
    public float maxSizeUpFrequency;
    public float startMinSpeed;
    public float minSpeedUpFrequency;
    public float startMaxSpeed;
    public float maxSpeedUpFrequency;
    public float speedUpValue;
}

[System.Serializable]
public class RefUpgrade
{
    public string id;
    public string name;
    public string shortName;
    public string rarity;
    public int appearChance;
    public string upgradeType;
    public float upgradeValue;
}

[System.Serializable]
public class RefDebuff
{
    public string id;
    public string name;
    public string shortName;
    public string rarity;
    public int appearChance;
    public string debuffType;
    public float debuffValue;
    public float debuffTime;
}