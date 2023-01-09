using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Objects")]
    public Camera mainCamera;
    public Object playerObj;
    public Object enemyObj;

    [Header("Game")]
    public float timerMax = 60f;
    public float timerAdd = 5f;
    public float timerDmg = 1f;
    public Image timerBar;
    
    [Header("Enemy")]
    public float spawnInterval = 2f;
    public int spawnMin = 3;
    public int spawnMax = 10;
    public float enemyLifetime = 10f;
    public float enemySizeMin = 0.7f;
    public int enemySizeMaxInterval = 2; //0.7+0.2+0.2=1.1 biggest circle size possible
    public float sizeInterval = 0.2f;

    private float spawnTimer = 0;

    //timer that decreases over time - not actual gameplay time
    private float currTimer = 0;

    //create another timer to keep track of gameplay time so can increase enemy size and movement speed

    private GameObject player;

    private List<GameObject> activeEnemyList = new List<GameObject>();
    private int enemyIndex = 0;

    private Vector2 viewportZero, viewportOne;

    // Start is called before the first frame update
    void Start()
    {
        viewportZero = mainCamera.ViewportToWorldPoint(Vector2.zero);
        viewportOne = mainCamera.ViewportToWorldPoint(Vector2.one);

        //spawns player
        player = Instantiate(playerObj, Vector2.zero, Quaternion.identity, this.transform) as GameObject;
        player.GetComponent<PlayerScript>().Intialize(mainCamera, this);

        currTimer = timerMax;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        currTimer -= Time.deltaTime;

        if (activeEnemyList.Count < spawnMin || spawnTimer > spawnInterval && activeEnemyList.Count < spawnMax)
        {
            //spawn enemy
            enemyIndex++;

            Vector2 randomPos = GetRandomOnScreenPos();

            GameObject enemy = Instantiate(enemyObj, randomPos, Quaternion.identity, this.transform) as GameObject;

            //each enemy has diff number
            enemy.name = "Enemy_" + enemyIndex;

            int randSize = Random.Range(0, enemySizeMaxInterval + 1);

            float enemySize = enemySizeMin + ((float)randSize * sizeInterval);
            enemy.transform.localScale = new Vector2(enemySize, enemySize);

            //initializing data in EnemyScript
            enemy.GetComponent<EnemyScript>().Initialize(this, timerAdd, timerDmg, enemyLifetime, enemySize);

            //add active enemy to list
            activeEnemyList.Add(enemy);

            //reset timer
            spawnTimer = 0;
        }

        UpdateTimerBar();
    }

    private Vector2 GetRandomOnScreenPos()
    {
        //get random position
        Vector2 randomPos = new Vector2(Random.Range(viewportZero.x, viewportOne.x), Random.Range(viewportZero.y, viewportOne.y));

        return randomPos;
    }

    //minor task: change colour
    public void AddTimer(float aValue)
    {
        currTimer += aValue;

        //fix timer so it doesnt go negative/exceed timerMax
        currTimer = Mathf.Clamp(currTimer, 0, timerMax);

        UpdateTimerBar();
    }

    public void MinusTimer(float aValue)
    {
        currTimer -= aValue;

        //fix timer so it doesnt go negative/exceed timerMax
        currTimer = Mathf.Clamp(currTimer, 0, timerMax);

        UpdateTimerBar();
    }

    public void UpdateTimerBar()
    {
        timerBar.fillAmount = currTimer / timerMax;

        //if timerBar = 0, show game over screen

    }

    public void RemoveEnemy(GameObject enemyGO)
    {
        activeEnemyList.Remove(enemyGO);
    }
}
