using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking; // allows download from local or online servers

public class DataManager : MonoBehaviour
{
    public string serverURL = "http://localhost/UXG2176/GetJson.php";

    /*public ServerData serverData;*/

    public void GetData(System.Action onComplete)
    {
        StartCoroutine(WaitForRequest(serverURL, onComplete));
    }

    // UnityWebRequest to get the data, works with coroutine
    IEnumerator WaitForRequest(string url, System.Action onComplete)
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(url);

        yield return webRequest.SendWebRequest();

        // To check if the webRequest is a sucess
        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            string jsonString = webRequest.downloadHandler.text;
            Debug.Log("webRequest Result: " + jsonString);

            // process data
            /*serverData = JsonUtility.FromJson<ServerData>(jsonString);*/
            ProcessGameData(jsonString);

            // to run onComplete after GameData has been process
            onComplete?.Invoke();
        }
        else
        {
            Debug.LogError("webRequest Error: " + webRequest.error);
        }
    }

    public void ProcessGameData(string jsonString)
    {
        ServerData serverData = JsonUtility.FromJson<ServerData>(jsonString);

        GameData gameData = Game.GetGameData();

        List<Level> levelList = new List<Level>();
        foreach (RefLevel refLevel in serverData.RefLevel)
        {
            levelList.Add(new Level(refLevel.id, refLevel.levelName,refLevel.maxTime, refLevel.spawnInterval, refLevel.spawnMin, refLevel.spawnMax, refLevel.upgradeInterval,
                refLevel.upgradeCount, refLevel.startMinSize, refLevel.minSizeUpFrequency, refLevel.sizeUpValue, refLevel.startMaxSizeInterval, refLevel.maxSizeUpFrequency,
                refLevel.startMinSpeed, refLevel.minSpeedUpFrequency, refLevel.startMaxSpeed, refLevel.maxSpeedUpFrequency, refLevel.speedUpValue));
        }
        gameData.SetLevelList(levelList);

        List<Upgrade> upgradeList = new List<Upgrade>();
        foreach (RefUpgrade refUpgrade in serverData.RefUpgrade)
        {
            // rarity convert strings to enum
            Rarity rarity = (Rarity)System.Enum.Parse(typeof(Rarity), refUpgrade.rarity);
            UpgradeType upgradeType = (UpgradeType)System.Enum.Parse(typeof(UpgradeType), refUpgrade.upgradeType);
            upgradeList.Add(new Upgrade(refUpgrade.id, refUpgrade.name, refUpgrade.shortName, rarity, refUpgrade.appearChance, upgradeType, refUpgrade.upgradeValue));
        }
        gameData.SetUpgradeList(upgradeList);

        List<Debuff> debuffList = new List<Debuff>();
        foreach (RefDebuff refDebuff in serverData.RefDebuff)
        {
            // rarity convert strings to enum
            DebuffRarity rarity = (DebuffRarity)System.Enum.Parse(typeof(DebuffRarity), refDebuff.rarity);
            DebuffType debuffType = (DebuffType)System.Enum.Parse(typeof(DebuffType), refDebuff.debuffType);
            debuffList.Add(new Debuff(refDebuff.id, refDebuff.name, refDebuff.shortName, refDebuff.appearChance, rarity, debuffType, refDebuff.debuffValue, refDebuff.debuffTime));
        }
        gameData.SetDebuffList(debuffList);
    }
}
