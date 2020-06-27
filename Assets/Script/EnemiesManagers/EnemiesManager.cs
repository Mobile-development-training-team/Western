using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class EnemiesManager : MonoBehaviour
{
    protected GameManager gameManager;
    protected GameUIController gameUIController;
    //public static EnemiesManager01 Instance;
    protected float waveTimer = 5f;               //每波间隔时间

    protected GameManager.LevelData thisLevel;
    protected int totalWaveNum;
    protected int waveNum = 0;                    //敌人波数
    //private int totalGeneratedEnemies = 50;     //总共敌人的数量
    protected int curDisplayedEnemies = 0;        //目前场景中的敌人数量
    protected int curWaveStillHaveEnemies = 0;        //当前波敌人剩余总数

    protected GameObject brave;

    void Awake()
    {
        //Instance = this;
        if (GameManager.INSTANCE == null)
        {
            return;
        }
        gameManager = GameManager.INSTANCE;
        gameManager.setEnemiesManager(this);
        setTotalWaveNum();
    }
    void Start()
    {
        //brave.GetComponent<BufferManager>().getBuffer();
        brave = gameManager.getBrave().gameObject;
        gameUIController = gameManager.getUIController();
        gameUIController.SetCurrentEnemyWavesText(waveNum, totalWaveNum);
        gameUIController.SetCurrentEnemyNumText(curDisplayedEnemies);
    }
    void Update()
    {
        if (waveNum == totalWaveNum && curWaveStillHaveEnemies == 0)
        {
            gameManager.GameOver(true);
            return;
        }
        if (waveTimer > 0)
        {
            waveTimer -= Time.deltaTime;
            if (waveTimer <= 0)
            {
                generatorEnemiesWave();
                gameUIController.SetCurrentEnemyWavesText(waveNum, totalWaveNum);
            }
        }
        else
        {
            if (curWaveStillHaveEnemies == 0)
            {
                getBuffer();
                waveTimer = 5f;
            }
        }
    }
    public void EnemiesDestory()
    {
        curDisplayedEnemies--;
        curWaveStillHaveEnemies--;
        gameUIController.SetCurrentEnemyNumText(curDisplayedEnemies);
    }
    protected void generatorEnemiesWave()
    {
        switch (waveNum)
        {
            case 0:
                wave1();
                break;
            case 1:
                wave2();
                break;
            case 2:
                wave3();
                break;
            case 3:
                wave4();
                break;
            case 4:
                wave5();
                break;
            case 5:
                wave6();
                break;
            case 6:
                wave7();
                break;
            case 7:
                wave8();
                break;
            case 8:
                wave9();
                break;
            case 9:
                wave10();
                break;
            case 10:
                wave11();
                break;
            case 11:
                wave12();
                break;
            case 12:
                wave13();
                break;
            case 13:
                wave14();
                break;
            case 14:
                wave15();
                break;
            default:
                break;
        }
        waveNum++;
    }
    /*
    protected IEnumerator generatorEnemy(int enemyIndex, float distance, float waitingTime ,float MaxHP,float Def,float Atk)
    {
        yield return new WaitForSeconds(waitingTime);
        if (enemyIndex == 0)
        {
            ObjectPool.GetInstant().GetEnemy("TwoHandsSwordEnemy", new Vector3(brave.transform.position[0] + distance, brave.transform.position[1] + 2, brave.transform.position[2]), new Quaternion(),new GameManager.EnemyData(1,1,0,8,1,10000,0,10000));
            //GameObject theEnemy = ObjectPool.GetInstant().GetObj("TwoHandsSwordEnemy", new Vector3(brave.transform.position[0] + distance, brave.transform.position[1] + 2, brave.transform.position[2]), new Quaternion());
            //SetEnemyAttribute(theEnemy, MaxHP, Def, Atk);
            //StartCoroutine(SetEnemyAttribute(theEnemy, MaxHP, Def, Atk));
        }
        if (enemyIndex == 1)
        {
            GameObject theEnemy = ObjectPool.GetInstant().GetObj("BowEnemy", new Vector3(brave.transform.position[0] + distance, brave.transform.position[1] + 2, brave.transform.position[2]), new Quaternion());
            theEnemy.GetComponent<EnemyController>().mLife.MAXHP = MaxHP;
            theEnemy.GetComponent<EnemyController>().mLife.mHp = MaxHP;
            theEnemy.GetComponent<EnemyController>().mLife.mDef = Def;
            theEnemy.GetComponent<EnemyController>().attack.mAtk = Atk;
        }
        if (enemyIndex == 2)
        {
            GameObject theEnemy = ObjectPool.GetInstant().GetObj("MagicWandEnemy", new Vector3(brave.transform.position[0] + distance, brave.transform.position[1] + 2, brave.transform.position[2]), new Quaternion());
            theEnemy.GetComponent<EnemyController>().mLife.MAXHP = MaxHP;
            theEnemy.GetComponent<EnemyController>().mLife.mHp = MaxHP;
            theEnemy.GetComponent<EnemyController>().mLife.mDef = Def;
            theEnemy.GetComponent<EnemyController>().attack.mAtk = Atk;
        }
        if (enemyIndex == 3)
        {
            GameObject theEnemy = ObjectPool.GetInstant().GetObj("AxeEnemy", new Vector3(brave.transform.position[0] + distance, brave.transform.position[1] + 2, brave.transform.position[2]), new Quaternion());
            theEnemy.GetComponent<EnemyController>().mLife.MAXHP = MaxHP;
            theEnemy.GetComponent<EnemyController>().mLife.mHp = MaxHP;
            theEnemy.GetComponent<EnemyController>().mLife.mDef = Def;
            theEnemy.GetComponent<EnemyController>().attack.mAtk = Atk;
        }
        if (enemyIndex == 4)
        {
            ObjectPool.GetInstant().GetObj("HammerEnemy", new Vector3(brave.transform.position[0] + distance, brave.transform.position[1] + 2, brave.transform.position[2]), new Quaternion()).GetComponent<EnemyController>().setAttribte(MaxHP, Def, Atk);
        }
        curDisplayedEnemies++;
        gameUIController.SetCurrentEnemyNumText(curDisplayedEnemies);
    }
    */
    protected IEnumerator generatorEnemy(GameManager.EnemyData enemy)
    {
        yield return new WaitForSeconds(enemy.WaitTime);
        if (enemy.EnemyIndex == 0)
        {
            ObjectPool.GetInstant().GetObj("TwoHandsSwordEnemy", new Vector3(brave.transform.position[0] + enemy.Distance, brave.transform.position[1] + 2, brave.transform.position[2]), new Quaternion()).GetComponent<EnemyController>().setAttribte(enemy.MaxHP, enemy.Def, enemy.Atk);
        }
        if (enemy.EnemyIndex == 1)
        {
            ObjectPool.GetInstant().GetObj("BowEnemy", new Vector3(brave.transform.position[0] + enemy.Distance, brave.transform.position[1] + 2, brave.transform.position[2]), Quaternion.identity).GetComponent<EnemyController>().setAttribte(enemy.MaxHP, enemy.Def, enemy.Atk);
        }
        if (enemy.EnemyIndex == 2)
        {
            ObjectPool.GetInstant().GetObj("MagicWandEnemy", new Vector3(brave.transform.position[0] + enemy.Distance, brave.transform.position[1] + 2, brave.transform.position[2]), new Quaternion()).GetComponent<EnemyController>().setAttribte(enemy.MaxHP, enemy.Def, enemy.Atk);
        }
        if (enemy.EnemyIndex == 3)
        {
            ObjectPool.GetInstant().GetObj("AxeEnemy", new Vector3(brave.transform.position[0] + enemy.Distance, brave.transform.position[1] + 2, brave.transform.position[2]), new Quaternion()).GetComponent<EnemyController>().setAttribte(enemy.MaxHP, enemy.Def, enemy.Atk);
        }
        if (enemy.EnemyIndex == 4)
        {
            ObjectPool.GetInstant().GetObj("HammerEnemy", new Vector3(brave.transform.position[0] + enemy.Distance, brave.transform.position[1] + 2, brave.transform.position[2]), new Quaternion()).GetComponent<EnemyController>().setAttribte(enemy.MaxHP, enemy.Def, enemy.Atk);
        }
        curDisplayedEnemies++;
        gameUIController.SetCurrentEnemyNumText(curDisplayedEnemies);
    }
    abstract public void getBuffer();
    abstract protected void setTotalWaveNum();


    protected void wave1()
    {
        //StartCoroutine(generatorEnemy(0,8,1,40,0,1000));
        //ObjectPool.GetInstant().GetObj("TwoHandsSwordEnemy", new Vector3(brave.transform.position[0] + 5, brave.transform.position[1] + 2, brave.transform.position[2]), new Quaternion()).GetComponent<EnemyController>().setAttribte(50, 0, 50);
        //curWaveStillHaveEnemies = 1;
        int enemiesCount = thisLevel.Waves[0].Enemies.Count;
        if (enemiesCount <= 0)
        {
            return;
        }
        curWaveStillHaveEnemies = enemiesCount;
        GameManager.EnemyData enemy;
        for (int i = 0; i < enemiesCount; i++)
        {
            enemy = thisLevel.Waves[0].Enemies[i];
            StartCoroutine(generatorEnemy(enemy));
        }

    }

    protected void wave2()
    {
        int enemiesCount = thisLevel.Waves[1].Enemies.Count;
        if (enemiesCount <= 0)
        {
            return;
        }
        curWaveStillHaveEnemies = enemiesCount;
        GameManager.EnemyData enemy;
        for (int i = 0; i < enemiesCount; i++)
        {
            enemy = thisLevel.Waves[1].Enemies[i];
            StartCoroutine(generatorEnemy(enemy));
        }
    }

    protected void wave3()
    {
        int enemiesCount = thisLevel.Waves[2].Enemies.Count;
        if (enemiesCount <= 0)
        {
            return;
        }
        curWaveStillHaveEnemies = enemiesCount;
        GameManager.EnemyData enemy;
        for (int i = 0; i < enemiesCount; i++)
        {
            enemy = thisLevel.Waves[2].Enemies[i];
            StartCoroutine(generatorEnemy(enemy));
        }
    }
    protected void wave4()
    {
        int enemiesCount = thisLevel.Waves[3].Enemies.Count;
        if (enemiesCount <= 0)
        {
            return;
        }
        curWaveStillHaveEnemies = enemiesCount;
        GameManager.EnemyData enemy;
        for (int i = 0; i < enemiesCount; i++)
        {
            enemy = thisLevel.Waves[3].Enemies[i];
            StartCoroutine(generatorEnemy(enemy));
        }
    }
    protected void wave5()
    {
        int enemiesCount = thisLevel.Waves[4].Enemies.Count;
        if (enemiesCount <= 0)
        {
            return;
        }
        curWaveStillHaveEnemies = enemiesCount;
        GameManager.EnemyData enemy;
        for (int i = 0; i < enemiesCount; i++)
        {
            enemy = thisLevel.Waves[4].Enemies[i];
            StartCoroutine(generatorEnemy(enemy));
        }
    }
    protected void wave6()
    {
        int enemiesCount = thisLevel.Waves[5].Enemies.Count;
        if (enemiesCount <= 0)
        {
            return;
        }
        curWaveStillHaveEnemies = enemiesCount;
        GameManager.EnemyData enemy;
        for (int i = 0; i < enemiesCount; i++)
        {
            enemy = thisLevel.Waves[5].Enemies[i];
            StartCoroutine(generatorEnemy(enemy));
        }
    }
    protected void wave7()
    {
        int enemiesCount = thisLevel.Waves[6].Enemies.Count;
        if (enemiesCount <= 0)
        {
            return;
        }
        curWaveStillHaveEnemies = enemiesCount;
        GameManager.EnemyData enemy;
        for (int i = 0; i < enemiesCount; i++)
        {
            enemy = thisLevel.Waves[6].Enemies[i];
            StartCoroutine(generatorEnemy(enemy));
        }
    }
    protected void wave8()
    {
        int enemiesCount = thisLevel.Waves[7].Enemies.Count;
        if (enemiesCount <= 0)
        {
            return;
        }
        curWaveStillHaveEnemies = enemiesCount;
        GameManager.EnemyData enemy;
        for (int i = 0; i < enemiesCount; i++)
        {
            enemy = thisLevel.Waves[7].Enemies[i];
            StartCoroutine(generatorEnemy(enemy));
        }
    }
    protected void wave9()
    {
        int enemiesCount = thisLevel.Waves[8].Enemies.Count;
        if (enemiesCount <= 0)
        {
            return;
        }
        curWaveStillHaveEnemies = enemiesCount;
        GameManager.EnemyData enemy;
        for (int i = 0; i < enemiesCount; i++)
        {
            enemy = thisLevel.Waves[8].Enemies[i];
            StartCoroutine(generatorEnemy(enemy));
        }
    }
    protected void wave10()
    {
        int enemiesCount = thisLevel.Waves[9].Enemies.Count;
        if (enemiesCount <= 0)
        {
            return;
        }
        curWaveStillHaveEnemies = enemiesCount;
        GameManager.EnemyData enemy;
        for (int i = 0; i < enemiesCount; i++)
        {
            enemy = thisLevel.Waves[9].Enemies[i];
            StartCoroutine(generatorEnemy(enemy));
        }
    }
    protected void wave11()
    {
        int enemiesCount = thisLevel.Waves[10].Enemies.Count;
        if (enemiesCount <= 0)
        {
            return;
        }
        curWaveStillHaveEnemies = enemiesCount;
        GameManager.EnemyData enemy;
        for (int i = 0; i < enemiesCount; i++)
        {
            enemy = thisLevel.Waves[10].Enemies[i];
            StartCoroutine(generatorEnemy(enemy));
        }
    }
    protected void wave12()
    {
        int enemiesCount = thisLevel.Waves[11].Enemies.Count;
        if (enemiesCount <= 0)
        {
            return;
        }
        curWaveStillHaveEnemies = enemiesCount;
        GameManager.EnemyData enemy;
        for (int i = 0; i < enemiesCount; i++)
        {
            enemy = thisLevel.Waves[11].Enemies[i];
            StartCoroutine(generatorEnemy(enemy));
        }
    }
    protected void wave13()
    {
        int enemiesCount = thisLevel.Waves[12].Enemies.Count;
        if (enemiesCount <= 0)
        {
            return;
        }
        curWaveStillHaveEnemies = enemiesCount;
        GameManager.EnemyData enemy;
        for (int i = 0; i < enemiesCount; i++)
        {
            enemy = thisLevel.Waves[12].Enemies[i];
            StartCoroutine(generatorEnemy(enemy));
        }
    }
    protected void wave14()
    {
        int enemiesCount = thisLevel.Waves[13].Enemies.Count;
        if (enemiesCount <= 0)
        {
            return;
        }
        curWaveStillHaveEnemies = enemiesCount;
        GameManager.EnemyData enemy;
        for (int i = 0; i < enemiesCount; i++)
        {
            enemy = thisLevel.Waves[13].Enemies[i];
            StartCoroutine(generatorEnemy(enemy));
        }
    }
    protected void wave15()
    {
        int enemiesCount = thisLevel.Waves[14].Enemies.Count;
        if (enemiesCount <= 0)
        {
            return;
        }
        curWaveStillHaveEnemies = enemiesCount;
        GameManager.EnemyData enemy;
        for (int i = 0; i < enemiesCount; i++)
        {
            enemy = thisLevel.Waves[14].Enemies[i];
            StartCoroutine(generatorEnemy(enemy));
        }
    }
    public double LevelProcess()
    {
        return (double)(waveNum - 1) / totalWaveNum;
    }
    /*
    abstract protected void wave1();
    abstract protected void wave2();
    abstract protected void wave3();
    abstract protected void wave4();
    abstract protected void wave5();
    abstract protected void wave6();
    abstract protected void wave7();
    abstract protected void wave8();
    abstract protected void wave9();
    abstract protected void wave10();
    abstract protected void wave11();
    abstract protected void wave12();
    abstract protected void wave13();
    abstract protected void wave14();
    abstract protected void wave15();
     * */
}
