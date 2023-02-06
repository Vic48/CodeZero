using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event
{
    private string id;
    private string name;
    private int appearChance;

    public Event(string id, string name, int appearChance)
    {
        this.id = id;
        this.name = name;
        this.appearChance = appearChance;
    }

    public string GetId() => id;
    public string GetName() => name;
    public int GetAppearChance() => appearChance;
}

