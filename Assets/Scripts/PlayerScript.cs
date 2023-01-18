using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Camera mainCamera;
    private GameController gameController;

    [Header("Variables")]
    //default speed of player
    public float playerSpeed = 5f;
    public float dashDistance = 1f; //enemies with diameter lesser than 1 will get destroyed
    public float dashCooldown = 0.3f; //DO NOT CHANGE
    public float damageCooldown = 0.5f; //DO NOT CHANGE

    [Header("Dash")]
    //reference to DashCircle and DashLine
    public SpriteRenderer dashCircle;
    public LineRenderer dashLine;

    //check if dashCircle and dashLine are shown
    private bool isDashMode;

    //keeps track of cooldown between each time player uses Dash
    private float dashTimer = 0;

    //gives player cooldown so they dont take damage continuously
    private float damageTimer = 0;

    //public GameObject target;
    //public GameObject arrowPointer;

    //Renderer rd;

    // Start is called before the first frame update
    public void Intialize(Camera mainCamera, GameController gameController)
    {
        //find camera
        this.mainCamera = mainCamera;

        this.gameController = gameController;
    }

    private void Start()
    {
        //rd = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //player movement
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 moveDir = new Vector3(moveX, moveY, 0);
        this.transform.position += moveDir * playerSpeed * Time.deltaTime;

        //dash
        if (dashTimer > 0)
        {
            dashTimer -= Time.deltaTime;
        }

        if (damageTimer > 0)
        {
            damageTimer -= Time.deltaTime;
            this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
        }
        else
        {
            this.GetComponent<SpriteRenderer>().color = Color.white;
        }

        //check if player release left mouse button
        if (Input.GetKeyUp(KeyCode.Mouse0) && isDashMode)
        {
            //start cooldown
            dashTimer = dashCooldown;

            //dash collision
            RaycastHit2D[] hitList = Physics2D.RaycastAll(this.transform.position, dashCircle.transform.position - this.transform.position, dashDistance + 1f);

            //check list
            foreach(RaycastHit2D hit in hitList)
            {
                if (hit.collider.GetComponent<UpgradeScript>() != null)
                {
                    //handle upgrades here
                    UpgradeScript upgradeScript = hit.collider.GetComponent<UpgradeScript>();

                    if (upgradeScript.GetUpgradeSize() <= dashDistance)
                    {
                        //add upgrade to player
                        // upgradeScript.addUpgrade();

                        upgradeScript.DestroyUpgrade();
                    }
                    else
                    {
                        //take damage
                        gameController.MinusTimer(upgradeScript.GetTimerDmg());

                        damageTimer = damageCooldown;
                    }
                }
                    //if hit enemy
                if (hit.collider.GetComponent<EnemyScript>() != null)
                {
                    //destroy
                    EnemyScript enemyScript = hit.collider.GetComponent<EnemyScript>();

                    //if enemy size is smaller than dash distance
                    if (enemyScript.GetEnemySize() <= dashDistance)
                    {
                        //add time to timer - can add multiplier/more value GetTimerAdd * [smth]
                        gameController.AddTimer(enemyScript.GetTimerAdd());

                        enemyScript.DestroyEnemy();
                    }
                    else
                    {
                        //take damage
                        gameController.MinusTimer(enemyScript.GetTimerDmg());

                        damageTimer = damageCooldown;
                    }

                }
            }

            //move player to new position
            this.transform.position = dashCircle.transform.position;
        }

        // checks if dashTimer = 0 and left mouse button is clicked
        if (Input.GetKey(KeyCode.Mouse0) && dashTimer <= 0)
        {
            isDashMode = true;
        }
        else
        {
            isDashMode = false;
        }
        UpdateDashDisplay();

        //tracking player
        //if (rd.isVisible == false)
        //{
        //    if(arrowPointer.activeSelf == false)
        //    {
        //        arrowPointer.SetActive(true);
        //    }

        //    Vector2 direction = target.transform.position - transform.position;

        //    RaycastHit2D ray = Physics2D.Raycast(transform.position, direction);

        //    if (ray.collider != null)
        //    {
        //        arrowPointer.transform.position = ray.point;
        //    }
        //}
        //else
        //{
        //    if (arrowPointer.activeSelf == true)
        //    {
        //        arrowPointer.SetActive(false);
        //    }
        //}

    }

    private void UpdateDashDisplay()
    {
        //enable dashLine and dashCircle if isDashMode is on
        dashCircle.enabled = isDashMode;
        dashLine.enabled = isDashMode;

        //locate mouse direction for player to dash
        Vector2 dashTargetDir = mainCamera.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
        dashCircle.transform.position = (Vector2)this.transform.position + dashTargetDir.normalized * (dashDistance + 1f);

        //start point sets at where player is at
        dashLine.SetPosition(0, this.transform.position);

        //end point sets at where dashCircle is at
        dashLine.SetPosition(1, dashCircle.transform.position);
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        //if timer = 0 and touching enemy
        if (damageTimer <= 0 && collision.GetComponent<EnemyScript>() != null)
        {
            EnemyScript enemyScript = collision.GetComponent<EnemyScript>();
            gameController.MinusTimer(enemyScript.GetTimerDmg());

            damageTimer = damageCooldown;
        }

        //if timer = 0 and touching upgrade
        if (damageTimer <= 0 && collision.GetComponent<UpgradeScript>() != null)
        {
            UpgradeScript upgradeScript = collision.GetComponent<UpgradeScript>();
            gameController.MinusTimer(upgradeScript.GetTimerDmg());

            damageTimer = damageCooldown;
        }
    }
}
