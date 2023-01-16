using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeScript : MonoBehaviour
{
    // put details of upgrades inside
    private GameController gameController;

    //upgrade life
    private float lifetime;

    //increase or decrease timer
    private float timerAdd;
    private float timerDmg;

    private float upgradeSize;

    //add upgradeSpeed and initialize in GameController
    private float upgradeSpeed;

    //timer for upgrade to change direction
    private float timeLeft;
    private Vector2 direction;
    public float addTime = 2f; //add back time to timer

    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        lifetime -= Time.deltaTime;
        timeLeft -= Time.deltaTime;

        if (lifetime <= 0)
        {
            DestroyUpgrade();
        }

        if (timeLeft <= 0)
        {
            direction = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
            timeLeft += addTime;
        }

    }

    private void FixedUpdate()
    {
        rb.AddForce(direction * upgradeSpeed);
    }

    public void Initialize(GameController gameController, float timerAdd, float timerDmg, float lifetime, float upgradeSize, float upgradeSpeed)
    {
        this.gameController = gameController;

        this.timerAdd = timerAdd;
        this.timerDmg = timerDmg;
        this.lifetime = lifetime;

        this.upgradeSize = upgradeSize;
        this.upgradeSpeed = upgradeSpeed;
    }
    public void DestroyUpgrade()
    {
        gameController.RemoveUpgrade(this.gameObject);

        Destroy(this.gameObject);
    }

    public float GetTimerAdd() => timerAdd;
    public float GetTimerDmg() => timerDmg;
    public float GetUpgradeSize() => upgradeSize;
}
