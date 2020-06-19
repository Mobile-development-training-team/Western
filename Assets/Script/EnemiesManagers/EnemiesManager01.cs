using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager01 : MonoBehaviour
{
    public static EnemiesManager01 Instance;
    private GameManager gameManager;
    private float waveTimer = 5f;   //每波间隔时间（测试用）
    private int waveNum = 0;        //敌人波数
    private int totalGeneratedEnemies = 50;//总共敌人的数量
    private int curDisplayedEnemies = 0 ;//目前场景中的敌人数量

    GameObject brave;

    void Awake()
    {
        brave = GameObject.FindGameObjectWithTag("brave");
        gameManager = GameManager.INSTANCE;
        gameManager.setEnemiesManager(this);
        Instance = this;
    }
    void Start()
    {
        //brave.GetComponent<BufferManager>().getBuffer();
    }
    void Update()
    {
        if (waveTimer > 0)
        {
            waveTimer -= Time.deltaTime;
            if (waveTimer <= 0)
            {
                generatorEnemiesWave();
            }
        }
        else
        {
            if (curDisplayedEnemies == 0)
            {
                if (waveNum % 2 == 0)
                {
                    brave.GetComponent<BufferManager>().getBuffer();
                }
                waveTimer = 5f;
            }
        }
    }
    private void generatorEnemiesWave()
    {
        if (waveNum == 0)
        {
            wave1();
        }
        else if (waveNum == 1)
        {
            wave2();
        }
        else if (waveNum == 2)
        {
            wave3();
        }
        else if (waveNum == 3)
        {
            wave4();
        }
        else if (waveNum == 4)
        {
            wave5();
        }
        else if (waveNum == 5)
        {
            wave6();
        }
        else if (waveNum == 6)
        {
            wave7();
        }
        else if (waveNum == 7)
        {
            wave8();
        }
        else if (waveNum == 8)
        {
            wave9();
        }
        else if (waveNum == 9)
        {
            wave10();
        }
        else
        {

        }
    }

    private void wave1()
    {
        generatorEnemy(0, -7);
        generatorEnemy(0, 7);
        generatorEnemy(0, -10);
        generatorEnemy(1, 12);
        generatorEnemy(1, -14);
        waveNum++;
    }

    private void wave2()
    {
        generatorEnemy(0, -7);
        generatorEnemy(0, 7);
        generatorEnemy(0, -10);
        generatorEnemy(1, 12);
        generatorEnemy(1, -14);
        waveNum++;
    }

    private void wave3()
    {
        generatorEnemy(0, -7);
        generatorEnemy(0, 7);
        generatorEnemy(0, -10);
        generatorEnemy(1, 12);
        generatorEnemy(1, -14);
        waveNum++;
    }
    private void wave4()
    {
        generatorEnemy(0, -7);
        generatorEnemy(0, 7);
        generatorEnemy(0, -10);
        generatorEnemy(1, 12);
        generatorEnemy(1, -14);
        waveNum++;
    }
    private void wave5()
    {
        generatorEnemy(0, -7);
        generatorEnemy(0, 7);
        generatorEnemy(0, -10);
        generatorEnemy(1, 12);
        generatorEnemy(1, -14);
        waveNum++;
    }
    private void wave6()
    {
        generatorEnemy(0, -7);
        generatorEnemy(0, 7);
        generatorEnemy(0, -10);
        generatorEnemy(1, 12);
        generatorEnemy(1, -14);
        waveNum++;
    }
    private void wave7()
    {
        generatorEnemy(0, -7);
        generatorEnemy(0, 7);
        generatorEnemy(0, -10);
        generatorEnemy(1, 12);
        generatorEnemy(1, -14);
        waveNum++;
    }
    private void wave8()
    {
        generatorEnemy(0, -7);
        generatorEnemy(0, 7);
        generatorEnemy(0, -10);
        generatorEnemy(1, 12);
        generatorEnemy(1, -14);
        waveNum++;
    }
    private void wave9()
    {
        generatorEnemy(0, -7);
        generatorEnemy(0, 7);
        generatorEnemy(0, -10);
        generatorEnemy(1, 12);
        generatorEnemy(1, -14);
        waveNum++;
    }
    private void wave10()
    {
        generatorEnemy(0, -7);
        generatorEnemy(0, 7);
        generatorEnemy(0, -10);
        generatorEnemy(1, 12);
        generatorEnemy(1, -14);
        waveNum++;
    }

    private void generatorEnemy(int enemyIndex, float distance)
    {
        if (enemyIndex == 0)
        {
            ObjectPool.GetInstant().GetObj("TwoHandsSwordEnemy", new Vector3(brave.transform.position[0] + distance, brave.transform.position[1] + 2, brave.transform.position[2]), new Quaternion());
        }
        if (enemyIndex == 1)
        {
            ObjectPool.GetInstant().GetObj("BowEnemy", new Vector3(brave.transform.position[0] + distance, brave.transform.position[1] + 2, brave.transform.position[2]), new Quaternion());
        }
        curDisplayedEnemies++;
    }
    
    public void EnemiesDestory()
    {
        curDisplayedEnemies--;
        totalGeneratedEnemies--;
        //Debug.Log("EnemiesManager totalFenerateEnemies = " + totalGeneratedEnemies);
        if (totalGeneratedEnemies <= 0)
        {
            GameManager.INSTANCE.GameOver(true);
        }
    }
}
