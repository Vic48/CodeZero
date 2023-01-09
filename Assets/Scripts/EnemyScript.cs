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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;

        if (lifetime <= 0)
        {
            DestroyEnemy();
        }
    }

    public void Initialize(GameController gameController, float timerAdd, float timerDmg, float lifetime, float enemySize)
    {
        this.gameController = gameController;

        this.timerAdd = timerAdd;
        this.timerDmg = timerDmg;
        this.lifetime = lifetime;

        this.enemySize = enemySize;
    }

    public void DestroyEnemy()
    {
        gameController.RemoveEnemy(this.gameObject);

        Destroy(this.gameObject);
    }

    public float GetTimerAdd() => timerAdd;
    public float GetTimerDmg() => timerDmg;
    public float GetEnemySize() => enemySize;
}
