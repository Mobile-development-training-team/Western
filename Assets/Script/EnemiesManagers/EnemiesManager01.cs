using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager01 : MonoBehaviour
{
    public static EnemiesManager01 Instance;
    private float waveTimer = 5f;   //每波间隔时间（测试用）
    private int waveNum = 0;        //敌人波数
    //private float wave1Time = 10f;  //第一波敌人到达时间
    //private float wave2Time = 20f;  //第二波敌人到达时间
    //private float wave3Time = 30f;  //第三波敌人到达时间
    //[SerializeField]
    private int totalGeneratedEnemies = 50;//总共敌人的数量
    //private int curGeneratedEnemies = 0;//目前产生的敌人的数量
    //[SerializeField]
    private int curDisplayedEnemies = 0 ;//目前场景中的敌人数量
    //private int curDestroyedEnemies = 0;//目前摧毁的敌人的数量

    //public GameObject[] Enemies;

    GameObject brave;

    void Awake()
    {
        brave = GameObject.FindGameObjectWithTag("brave");
        if (Instance== null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
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
                brave.GetComponent<BufferManager>().getBuffer();
                waveTimer = 5f;
            }
        }
        //测试用，循环生成多波敌人
        /*
        waveTimer -= Time.deltaTime;
        if (waveTimer <= 0)
        {
            generatorEnemiesWave();
            waveTimer = 5f + (float)waveNum*2;
            waveNum = (waveNum + 1) % 3;
        }
         * */

        /*
        if (wave1Time>0)
        {
            wave1Time -= Time.deltaTime;
            if (wave1Time < 0)
            {
                wave1();
                waveNum = 1;
                //curDisplayedEnemies = ObjectPool.GetInstant().GetAINum();
                //Console.WriteLine("curDestroyedEnemies "+curDestroyedEnemies);
            }
        }
        if (wave2Time > 0)
        {
            wave2Time -= Time.deltaTime;
            if (wave2Time < 0)
            {
                wave2();
                waveNum = 2;
                //curDisplayedEnemies = ObjectPool.GetInstant().GetAINum();
            }
        }
        if (wave3Time > 0)
        {
            wave3Time -= Time.deltaTime;
            if (wave3Time < 0)
            {
                wave3();
                waveNum = 3;
                //curDisplayedEnemies = ObjectPool.GetInstant().GetAINum();
            }
        }
        */
    }

    /*
    //测试用
    public  void generatorEnemiesWave()
    {
        if (waveNum == 0)
        {
            wave1();
        }
        if (waveNum == 1)
        {
            wave2();
        }
        if (waveNum == 2)
        {
            wave3();
        }
    }
    */
    public void generatorEnemiesWave()
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
        Debug.Log("EnemiesManager totalFenerateEnemies = " + totalGeneratedEnemies);
        //如果敌人被摧毁的数量大于等于产生的总量则进入下一关
        if (totalGeneratedEnemies <= 0)
        {
            GameManager.INSTANCE.GameOver(true);
        }
    }
}
