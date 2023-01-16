using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade
{
    private string id;
    private string name;
    private string shortName;
    private Rarity rarity;
    private int appearChance;
    private UpgradeType upgradeType;
    private float upgradeValue;

    public Upgrade(string id, string name, string shortName, Rarity rarity, int appearChance, UpgradeType upgradeType, float upgradeValue)
    {
        this.id = id;
        this.name = name;
        this.shortName = shortName;
        this.rarity = rarity;
        this.appearChance = appearChance;
        this.upgradeType = upgradeType;
        this.upgradeValue = upgradeValue;
    }

    public string GetId => id;
    public string GetName => name;
    public string GetShortName => shortName;
    public Rarity GetRarity => rarity;
    public int GetAppearChance => appearChance;
    public UpgradeType GetUpgradeType() => upgradeType;
    public float GetUpgradeValue() => upgradeValue;
}

public enum Rarity
{
    COMMON,
    RARE
}

public enum UpgradeType
{
    SPEED,
    LINE,
    DEF_MULT,
    TIME_MULT
}
