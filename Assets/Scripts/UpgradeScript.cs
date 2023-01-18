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
    public GameObject Player;

    private Camera cam;
    private Vector2 screenBoundary;
    private float screenWidth;
    private float screenHeight;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");

        cam = Camera.main;
        screenBoundary = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.transform.position.z));
        screenWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x;
        screenHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y;
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

    private void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBoundary.x * -1 + screenWidth, screenBoundary.x - screenWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBoundary.y * -1 + screenHeight, screenBoundary.y - screenWidth);
        transform.position = viewPos;
    }

    public void PlayerUpgrade()
    {
        PlayerScript playerscript = Player.GetComponent<PlayerScript>();
        //if-else or use switch case to select the upgrade.
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
