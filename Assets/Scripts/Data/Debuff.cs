using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuff
{
    private string id;
    private string name;
    private string shortName;
    private DebuffRarity debuffRarity;
    private int appearChance;
    private DebuffType debuffType;
    private float debuffValue;
    private float debuffTime;

    public Debuff(string id, string name, string shortName, int appearChance, DebuffRarity debuffRarity, DebuffType debuffType, float debuffValue, float debuffTime)
    {
        this.id = id;
        this.name = name;
        this.shortName = shortName;
        this.appearChance = appearChance;
        this.debuffRarity = debuffRarity;
        this.debuffType = debuffType;
        this.debuffValue = debuffValue;
        this.debuffTime = debuffTime;
    }

    public string GetId() => id;
    public string GetName() => name;
    public string GetShortName() => shortName;
    public int GetAppearChance() => appearChance;
    public DebuffRarity GetDebuffRarity() => debuffRarity;
    public DebuffType GetDebuffType() => debuffType;
    public float GetDebuffValue() => debuffValue;
    public float GetDebuffTime() => debuffTime;
}

public enum DebuffRarity
{
    COMMON,
    RARE
}

public enum DebuffType
{
    SPEED,
    LINE,
    DEF,
}
