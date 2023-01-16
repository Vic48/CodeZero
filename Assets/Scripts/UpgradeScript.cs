using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeScript : MonoBehaviour
{
    // put details of upgrades inside
    private GameController gameController;

    //enemy life
    private float lifetime;

    //increase or decrease timer
    private float timerAdd;
    private float timerDmg;

    private float upgradeSize;
    void Update()
    {
        lifetime -= Time.deltaTime;

        if (lifetime <= 0)
        {
            DestroyUpgrade();
        }
    }
    public void Initialize(GameController gameController, float timerAdd, float timerDmg, float lifetime, float upgradeSize)
    {
        this.gameController = gameController;

        this.timerAdd = timerAdd;
        this.timerDmg = timerDmg;
        this.lifetime = lifetime;

        this.upgradeSize = upgradeSize;
    }
    public void DestroyUpgrade()
    {
        gameController.RemoveUpgrade(this.gameObject);

        Destroy(this.gameObject);
    }
}
