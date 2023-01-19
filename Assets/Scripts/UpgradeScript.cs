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

    private const float moveInterval = 1f; //interval between each direction change
    private float moveTimer = moveInterval; //movement timer
    private Vector2 moveDir = new Vector2(); //direction

    public UpgradeType thisUpgradeType;
    public Color RarityColor = new Color (1,1,1,1);
    public Rarity thisRarity;
    public float thisUpgradeValue;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
        cam = Camera.main;
        screenBoundary = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.transform.position.z));
        screenWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x;
        screenHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y;
        checkUpgradeRarity();
    }

    void Update()
    {
        lifetime -= Time.deltaTime;
        timeLeft -= Time.deltaTime;

        GetComponent<SpriteRenderer>().color = RarityColor;


        if (lifetime <= 0)
        {
            DestroyUpgrade();
        }

        if (timeLeft <= 0)
        {
            float time = Time.deltaTime;

            MoveUpdate(time);
        }

    }

    private void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBoundary.x * -1 + screenWidth, screenBoundary.x - screenWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBoundary.y * -1 + screenHeight, screenBoundary.y - screenWidth);
        transform.position = viewPos;
    }

    public Vector2 MoveVector(float time, Vector2 selfPos, Vector2 viewportZero, Vector2 viewportOne)
    {
        moveTimer += time;

        //check direction
        if (moveTimer > moveInterval)
        {
            moveTimer -= moveInterval;

            moveDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));

            //adjust direction if hit boundary
            if (selfPos.x + (moveDir.normalized.x * moveInterval) > viewportOne.x) moveDir.x = -Mathf.Abs(moveDir.x);
            if (selfPos.x + (moveDir.normalized.x * moveInterval) < viewportZero.y) moveDir.x = Mathf.Abs(moveDir.x);
            if (selfPos.y + (moveDir.normalized.y * moveInterval) > viewportOne.y) moveDir.y = -Mathf.Abs(moveDir.y);
            if (selfPos.y + (moveDir.normalized.y * moveInterval) < viewportZero.y) moveDir.y = Mathf.Abs(moveDir.y);
        }

        return moveDir.normalized * time;
    }

    public void MoveUpdate(float time
        )
    {
        Vector2 viewportZero = cam.ViewportToWorldPoint(Vector2.zero);
        Vector2 viewportOne = cam.ViewportToWorldPoint(Vector2.one);

        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        Vector2 moveVector = Vector2.zero;
        moveVector = MoveVector(time, transform.position, viewportZero, viewportOne);


        this.transform.Translate(moveVector);
    }

    public void checkUpgradeRarity()
    {
        if(thisRarity == Rarity.COMMON)
        {
            RarityColor =Color.blue;
        }
        else
        {
            RarityColor = Color.yellow;
        }
    }

    public void PlayerUpgrade()
    {
        PlayerScript playerscript = Player.GetComponent<PlayerScript>();
        //TODO: if-else or use switch case to select the upgrade.
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
