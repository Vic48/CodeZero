using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private GameController gameController;

    //enemy life
    private float lifetime;

    //increase or decrease timer
    private float timerAdd;
    private float timerDmg;

    private float enemySize;

    //add enemySpeed and initialize in GameController
    private float enemySpeed;

    //timer for enemy to change direction
    private float timeLeft;
    private Vector2 direction;
    public float addTime = 2f; //add back time to timer

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
        timeLeft -= Time.deltaTime;

        if (lifetime <= 0)
        {
            DestroyEnemy();
        }

        if (timeLeft <= 0)
        {
            direction = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
            timeLeft += addTime;
        }

    }

    private void FixedUpdate()
    {
        rb.AddForce(direction * enemySpeed);
    }

    public void Initialize(GameController gameController, float timerAdd, float timerDmg, float lifetime, float enemySize, float enemySpeed)
    {
        this.gameController = gameController;

        this.timerAdd = timerAdd;
        this.timerDmg = timerDmg;
        this.lifetime = lifetime;

        this.enemySize = enemySize;
        this.enemySpeed = enemySpeed;
    }

    public void DestroyEnemy()
    {
        gameController.RemoveEnemy(this.gameObject);
        gameController.TakeAwayEnemy(this.gameObject);
        

        //Destroy(this.gameObject);
        // need to be add into the object pool 

    }

    public float GetTimerAdd() => timerAdd;
    public float GetTimerDmg() => timerDmg;
    public float GetEnemySize() => enemySize;
}
