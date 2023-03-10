using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    private List<Level> levelList;
    private List<Upgrade> upgradeList;
    private List<Debuff> debuffList;

    public List<Level> GetLevelList()
    {
        return levelList;
    }

    public Level GetLevelByRefId(string aId)
    {
        return levelList.Find(x => x.GetId() == aId);
    }

    public void SetLevelList(List<Level> aList)
    {
        levelList = aList;
    }

    public List<Upgrade> GetUpgradeList()
    {
        return upgradeList;
    }

    public void SetUpgradeList(List<Upgrade> aList)
    {
        upgradeList = aList;
    }

    public List<Debuff> GetDebuffList()
    {
        return debuffList;
    }

    public void SetDebuffList(List<Debuff> aList)
    {
        debuffList = aList;
    }
}