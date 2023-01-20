using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
    public float timerMax;
    public float timerAdd = 5f;
    public float timerDmg = 1f;

    [Header("Timer")]
    public Image timerBar;
    public Text timerCountDown;
    public float elaspedTime = 0f;
    public Color PoposedColor;

    [Header("Upgrade")]
    public float upgradeInterval;
    public int upgradeCount;
    public int upgradeSpawnMin;
    public int upgradeSpawnMax;
    public float upgradeLifetime = 5f; //DO NOT CHANGE
    public float upgradeSizeMin;
    public int upgradeSizeMaxInterval;
    public float upgradeSizeInterval;

    private float upgradeSpawnTimer = 0;

    [Header("Enemy")]
    public float spawnInterval;
    public int spawnMin;
    public int spawnMax;
    public float enemyLifetime = 10f; //DO NOT CHANGE
    public float enemySizeMin;
    public int enemySizeMaxInterval; //0.7+0.2+0.2=1.1 biggest circle size possible
    public float sizeInterval;
    public float enemyMinSpeed;
    public float enemyMaxSpeed;
    public float enemyMinSizeUpFreq;
    public float enemyMaxSizeUpFreq;
    public float enemyMinSpeedFreq;
    public float enemyMaxSpeedFreq;
    public float enemySpeedUpVal;

    public int enemyPoolSize = 10;
    private float spawnTimer = 0;

    //timer that decreases over time - not actual gameplay time
    private float currTimer = 0;

    //keep track of gameplay time so can increase enemy size and movement speed
    private float gameTime = 0;

    [Header("HUD")]
    public int circlesDestroyed;
    public Text circle_gone;
    //public GameObject gameOver; //game over panel
    //public GameObject pauseMenu;
    public Text levelText;
    public int levelNum;
    public float surviveTimer;
    public Text timesurviveText;

    private GameObject player;
    public Buttons buttonScript;

    private List<GameObject> activeEnemyList = new List<GameObject>();
    private int enemyIndex = 0;

    public List<GameObject> activeUpgrades = new List<GameObject>();
    private int upgradeIndex = 0;

    //-------------------   Object Pooling -----------------
    private List<GameObject> enemyObjectPool = new List<GameObject>();

    public Level levelScript;
    public bool pausedGame;

    public GameObject GetEnemyObject(string aObjName, System.Action<GameObject> onLoaded)
    {
        // create an empty object to return
        GameObject emptyObj = null;

        if (enemyObjectPool.Count != 0)
        {
            for (int i = 0; i < enemyObjectPool.Count; i++)
            {
                // if the i in the pool is not active in hierarchy
                if (!enemyObjectPool[i].activeInHierarchy)
                {
                    emptyObj = enemyObjectPool[i];

                    //ADD in 
                    activeEnemyList.Add(emptyObj);
                    onLoaded.Invoke(emptyObj);
                    // Set active
                    emptyObj.SetActive(true);
                    return emptyObj;
                }
            }
        }
        return emptyObj;
        //else
        //{
        //    //spawn enemy/initialize
        //    emptyObj = Instantiate(enemyObj, GetRandomOnScreenPos(), Quaternion.identity, this.transform) as GameObject;
        //    emptyObj.name = "Enemy_" + enemyIndex;
        //    //set active
        //    onLoaded?.Invoke(emptyObj);
        //}
        //emptyObj.SetActive(true);
        //return emptyObj;
    }

    public void TakeAwayEnemy(GameObject emptyObj)
    {
        // remove the emptyObj from enemy list
        activeEnemyList.Remove(emptyObj);
        //add to the object pool
        //enemyObjectPool.Add(emptyObj);
        //set from enemy list to false
        emptyObj.SetActive(false);
    }

    private Vector2 viewportZero, viewportOne;

    private bool isGameStart = false;

    public int digit;
    // which level I want to play
    public string levelId;

    // Start is called before the first frame update
    void Start()
    {
        // GetComponent only works if the two scripts are on the same object
        // StartGame to only start running after I have gotten my data
        this.GetComponent<DataManager>().GetData(StartGame); // this works only if the function has no input parameters
        Time.timeScale = 1f;
    }

    public void StartGame()
    {
        FindObjectOfType<AudioManager>().StopPlaying("MenuBGM");
        FindObjectOfType<AudioManager>().Play("GameBGM");
        viewportZero = mainCamera.ViewportToWorldPoint(Vector2.zero);
        viewportOne = mainCamera.ViewportToWorldPoint(Vector2.one);

        //spawns player
        player = Instantiate(playerObj, Vector2.zero, Quaternion.identity, this.transform) as GameObject;
        player.GetComponent<PlayerScript>().Intialize(mainCamera, this);

        levelId = PlayerPrefs.GetString("levelId");
        Level currLevel = Game.GetGameData().GetLevelByRefId(levelId);

        //currTimer = Game.GetGameData().GetLevelByRefId(levelId).GetMaxTime();

        spawnMin = currLevel.GetSpawnMin();
        spawnMax = currLevel.GetSpawnMax();

        //set the initial color for the bar
        PoposedColor = Color.white;

        surviveTimer = 0f;

        isGameStart = true;

        levelText.text = levelNum.ToString("Level: "+ currLevel.GetLevelName());


        enemObjectPool();

        //-------------------   ENEMY -----------------

        spawnInterval = currLevel.GetSpawnInterval();
        enemySizeMin = currLevel.GetStartMinSize();
        enemySizeMaxInterval = currLevel.GetStartMaxSizeInterval();
        sizeInterval = currLevel.GetSizeUpValue();        
        enemyMinSpeed = currLevel.GetStartMinSpeed();
        enemyMaxSpeed = currLevel.GetStartMaxSpeed();

        // TODO
        //number of seconds between min size spawned enemies increases by sizeUpValue
        enemyMinSizeUpFreq = currLevel.GetMinSizeUpFrequency();
        //Number of seconds between each time the maximum size interval of spawned enemies increases by 1
        enemyMaxSizeUpFreq = currLevel.GetMaxSizeUpFrequency();
        enemyMinSpeedFreq = currLevel.GetMinSpeedUpFrequency();
        enemyMaxSpeedFreq = currLevel.GetMaxSpeedUpFrequency();
        enemySpeedUpVal = currLevel.GetSpeedUpValue();

        //-------------------   UPGRADE -----------------

        upgradeInterval = currLevel.GetUpgradeInterval();
        upgradeCount = currLevel.GetUpgradeCount();
        upgradeSpawnMin = currLevel.GetSpawnMin();
        upgradeSpawnMax = currLevel.GetSpawnMax();
        upgradeSizeMin = currLevel.GetStartMinSize();
        upgradeSizeMaxInterval = currLevel.GetStartMaxSizeInterval();
        upgradeSizeInterval = currLevel.GetSizeUpValue();

        //-------------------   TIMER -----------------

        timerMax = currLevel.GetMaxTime();
        currTimer = timerMax;

        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(levelId+ " Timermax: " + timerMax);
        if (!isGameStart) return;

        spawnTimer += Time.deltaTime;
        currTimer -= Time.deltaTime;

        //-------------------   HUD -----------------
        // timer text countdown
        currTimer = Mathf.Clamp(currTimer, 0, timerMax);
        int seconds = Mathf.FloorToInt(currTimer % 60F);
        timerCountDown.text = seconds.ToString("0");

        //survive timer
        surviveTimer += Time.deltaTime;
        timesurviveText.text = "Player Survived: " + Mathf.RoundToInt(surviveTimer).ToString();

        //circles destroyed
        circle_gone.text = circlesDestroyed.ToString("Score: " + "0");

        //-------------------   ENEMY -----------------

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

            float enemySpeed = enemyMinSpeed;

            //initializing data in EnemyScript
            enemy.GetComponent<EnemyScript>().Initialize(this, timerAdd, timerDmg, enemyLifetime, enemySize, enemySpeed);

            //add active enemy to list
            activeEnemyList.Add(enemy);

            //reset timer
            spawnTimer = 0;


            //30 seconds later
            if (currTimer <= enemyMinSizeUpFreq)
            {
                //random size up value
                int sizeUpFrequency = Random.Range(0, enemySizeMaxInterval + 1);
                float enemyIncSize = (enemySizeMin + sizeInterval) + ((float)sizeUpFrequency * sizeInterval);
                //Debug.Log(enemyIncSize);
                enemy.transform.localScale = new Vector2(enemyIncSize, enemyIncSize);

                float enemyIncSpeed = enemyMinSpeed * enemyMinSpeedFreq;
                //Debug.Log(enemyIncSpeed);
                enemy.GetComponent<EnemyScript>().Initialize(this, timerAdd, timerDmg, enemyLifetime, enemySize, enemyIncSpeed);

                activeEnemyList.Add(enemy);
            }

        }

        //-------------------   UPGRADES -----------------

        upgradeSpawnTimer += Time.deltaTime;

        if (activeUpgrades.Count < upgradeCount)
        {
            //spawn upgrade
            upgradeIndex++;

            Vector2 randomPos = GetRandomOnScreenPos();

            GameObject upgrade = Instantiate(upgradeObj, randomPos, Quaternion.identity, this.transform) as GameObject;


            digit = 0;
            foreach (Upgrade a in Game.GetGameData().GetUpgradeList())
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

            //each upgrade has diff number
            upgrade.name = "Upgrade_" + Game.GetGameData().GetUpgradeList()[loopNum].GetName() + "_" + upgradeIndex;
            upgrade.GetComponent<UpgradeScript>().thisRarity = Game.GetGameData().GetUpgradeList()[loopNum].GetRarity();
            upgrade.GetComponent<UpgradeScript>().thisUpgradeType = Game.GetGameData().GetUpgradeList()[loopNum].GetUpgradeType();
            upgrade.GetComponent<UpgradeScript>().thisUpgradeValue = Game.GetGameData().GetUpgradeList()[loopNum].GetUpgradeValue();

            int randSize = Random.Range(0, upgradeSizeMaxInterval + 1);

            float upgradeSize = upgradeSizeMin + ((float)randSize * upgradeSizeInterval);
            upgrade.transform.localScale = new Vector2(upgradeSize, upgradeSize);

            float upgradeSpeed = enemyMinSpeed;

            //initializing data in UpgradeScript
            upgrade.GetComponent<UpgradeScript>().Initialize(this, timerAdd, timerDmg, upgradeLifetime, upgradeSize, upgradeSpeed);

            //add active upgrade to list
            activeUpgrades.Add(upgrade);

            //reset timer
            spawnTimer = 0;
        }

        UpdateTimerBar();

        //set the color back to initial color
        elaspedTime = elaspedTime + Time.deltaTime;

        if (elaspedTime >= 2f)
        {
            elaspedTime = 0;
            PoposedColor = Color.white;
        }

    }

    public void enemObjectPool()
    {
        // check the pool size
        for (int i = 0; i < enemyPoolSize; i++)
        {
            GameObject enemy = Instantiate(enemyObj, this.gameObject.transform.position, Quaternion.identity, this.transform) as GameObject;
            enemy.SetActive(false);
            enemyObjectPool.Add(enemy);
        }
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
        PoposedColor = Color.green;
        UpdateTimerBar();
    }

    public void MinusTimer(float aValue)
    {
        currTimer -= aValue;
        //fix timer so it doesnt go negative/exceed timerMax
        currTimer = Mathf.Clamp(currTimer, 0, timerMax);
        PoposedColor = Color.red;
        UpdateTimerBar();
    }

    public void UpdateTimerBar()
    {
        timerBar.fillAmount = currTimer / timerMax;
        timerBar.color = Color.Lerp(timerBar.color, PoposedColor, 1f);
        //if timerBar = 0, show game over screen
        if (timerBar.fillAmount == 0)
        {
            GameOver();
        }
    }

    // add the removed enemy back to the object pool
    public void RemoveEnemy(GameObject enemyGO)
    {
        activeEnemyList.Remove(enemyGO);
        enemyObjectPool.Add(enemyGO);
    }

    public void RemoveUpgrade(GameObject upgradeGO)
    {
        activeUpgrades.Remove(upgradeGO);
    }

    public void GameOver()
    {
        FindObjectOfType<AudioManager>().StopPlaying("GameBGM");
        FindObjectOfType<AudioManager>().Play("GameOver");
        Time.timeScale = 0f;
        buttonScript.gameOver.SetActive(true);
    }
}
