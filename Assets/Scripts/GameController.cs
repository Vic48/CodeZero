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
    public Object upgradeObj;

    [Header("Game")]
    public float timerMax = 60f;
    public float timerAdd = 5f;
    public float timerDmg = 1f;
    public Image timerBar;

    [Header("Upgrade")]
    public float upgradeInterval = 2f;
    public int upgradeCount = 3;
    public int upgradeSpawnMin = 3;
    public int upgradeSpawnMax = 10;
    public float upgradeLifetime = 10f;
    public float upgradeSizeMin = 0.7f;
    public int upgradeSizeMaxInterval = 2;
    public float upgradeSizeInterval = 0.2f;

    private float upgradeSpawnTimer = 0;



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

    public List<GameObject> activeUpgrades = new List<GameObject>();
    private int upgradeIndex = 0;

    //object pooling
    private List<GameObject> enemyObjectPool = new List<GameObject>();

    public GameObject GetEnemyObject(string aObjName, System.Action<GameObject> onLoaded)
    {
        // create an empty object to return
        GameObject emptyObj = null;

        if (aObjName.Contains ("Enemy_") && enemyObjectPool.Count != 0)
        {
            emptyObj = enemyObjectPool[0];
            //set active == take the item out 
            onLoaded.Invoke(emptyObj);
            // remove the item after its being taken out
            enemyObjectPool.Remove(emptyObj);
        }
        else
        {
            //spawn enemy/initialize
            emptyObj = Instantiate(enemyObj, GetRandomOnScreenPos(), Quaternion.identity, this.transform) as GameObject;
            emptyObj.name = "Enemy_" + enemyIndex;
            //set active
            onLoaded?.Invoke(emptyObj);
        }
        emptyObj.SetActive(true);
        return emptyObj;
    }

    private Vector2 viewportZero, viewportOne;

    private bool isGameStart = false;

    public int digit;
    // which level I want to play
    // public string levelId;

    // Start is called before the first frame update
    void Start()
    {
        // GetComponent only works if the two scripts are on the same object
        // StartGame to only start running after I have gotten my data
        this.GetComponent<DataManager>().GetData(StartGame); // this works only if the function has no input parameters
    }

    public void StartGame()
    {
        viewportZero = mainCamera.ViewportToWorldPoint(Vector2.zero);
        viewportOne = mainCamera.ViewportToWorldPoint(Vector2.one);

        //spawns player
        player = Instantiate(playerObj, Vector2.zero, Quaternion.identity, this.transform) as GameObject;
        player.GetComponent<PlayerScript>().Intialize(mainCamera, this);

        Level currLevel = Game.GetGameData().GetLevelByRefId("1");

        currTimer = Game.GetGameData().GetLevelByRefId("1").GetMaxTime();

        spawnMin = currLevel.GetSpawnMin();
        spawnMax = currLevel.GetSpawnMax();

        isGameStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameStart) return;

        spawnTimer += Time.deltaTime;
        currTimer -= Time.deltaTime;

        if (activeEnemyList.Count < spawnMin || spawnTimer > spawnInterval && activeEnemyList.Count < spawnMax)
        {
            //spawn enemy
            enemyIndex++;

            Vector2 randomPos = GetRandomOnScreenPos();

            GameObject enemy = GetEnemyObject("Enemy_", (enemyObj) =>
            {
                enemyObj.name = "Enemy_" + enemyIndex;
                enemyObj.transform.position = randomPos;
            });

            //GameObject enemy = Instantiate(enemyObj, randomPos, Quaternion.identity, this.transform) as GameObject;

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

        // upgrades
        upgradeSpawnTimer += Time.deltaTime;

        if (activeUpgrades.Count < upgradeSpawnMin || ((upgradeSpawnTimer > upgradeInterval) && (activeUpgrades.Count < upgradeSpawnMax)))
        {
            //spawn upgrade
            upgradeIndex++;

            Vector2 randomPos = GetRandomOnScreenPos();

            GameObject upgrade = Instantiate(upgradeObj, randomPos, Quaternion.identity, this.transform) as GameObject;

            
            digit = 0;
            foreach(Upgrade a in Game.GetGameData().GetUpgradeList())
            {
                digit += a.GetAppearChance();
            }

            bool loopCheck = true;
            int generatedNum = Random.Range(0, digit + 1);
            int loopNum = 0;
            while (loopCheck)
            {
                generatedNum -= Game.GetGameData().GetUpgradeList()[loopNum].GetAppearChance();
                if (generatedNum <= 0)
                {
                    loopCheck = false;
                }
                else
                {
                    loopNum += 1;
                }
            }
            
            upgrade.GetComponentInChildren<TextMesh>().text = Game.GetGameData().GetUpgradeList()[loopNum].GetShortName();
      
            //each enemy has diff number
            upgrade.name = "Upgrade_" + Game.GetGameData().GetUpgradeList()[loopNum].GetName() + "_" + upgradeIndex;

            int randSize = Random.Range(0, upgradeSizeMaxInterval + 1);

            float upgradeSize = upgradeSizeMin + ((float)randSize * upgradeSizeInterval);
            upgrade.transform.localScale = new Vector2(upgradeSize, upgradeSize); 

            //initializing data in EnemyScript
            upgrade.GetComponent<UpgradeScript>().Initialize(this, timerAdd, timerDmg, upgradeLifetime, upgradeSize);

            //add active enemy to list
            activeUpgrades.Add(upgrade);

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

    public void RemoveUpgrade(GameObject upgradeGO)
    {
        activeUpgrades.Remove(upgradeGO);
    }
}
