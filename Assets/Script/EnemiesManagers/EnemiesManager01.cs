using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager01 : MonoBehaviour
{
    public static EnemiesManager01 Instance;
    //private float waveTimer = 5f;   //每波间隔时间（测试用）
    private int waveNum = 0;        //敌人波数
    private float wave1Time = 10f;  //第一波敌人到达时间
    private float wave2Time = 20f;  //第二波敌人到达时间
    private float wave3Time = 30f;  //第三波敌人到达时间
    [SerializeField]
    private int totalGeneratedEnemies = 6;//总共敌人的数量
    //private int curGeneratedEnemies = 0;//目前产生的敌人的数量
    [SerializeField]
    private int curDisplayedEnemies = 0 ;//目前场景中的敌人数量
    //private int curDestroyedEnemies = 0;//目前摧毁的敌人的数量

    public GameObject[] Enemies;

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


    private void wave1()
    {
        ObjectPool.GetInstant().GetObj("TwoHandsSwordEnemy", new Vector3(brave.transform.position[0] - 13, brave.transform.position[1] + 2, brave.transform.position[2]), new Quaternion());
        curDisplayedEnemies++;
    }

    private void wave2()
    {
        ObjectPool.GetInstant().GetObj("BowEnemy", new Vector3(brave.transform.position[0] - 13, brave.transform.position[1] + 2, brave.transform.position[2]), new Quaternion());
        curDisplayedEnemies++;
        ObjectPool.GetInstant().GetObj("TwoHandsSwordEnemy", new Vector3(brave.transform.position[0] + 13, brave.transform.position[1] + 2, brave.transform.position[2]), new Quaternion());
        curDisplayedEnemies++;
    }

    private void wave3()
    {
        ObjectPool.GetInstant().GetObj("BowEnemy", new Vector3(brave.transform.position[0] - 13, brave.transform.position[1] + 2, brave.transform.position[2]), new Quaternion());
        curDisplayedEnemies++;
        ObjectPool.GetInstant().GetObj("TwoHandsSwordEnemy", new Vector3(brave.transform.position[0] - 6, brave.transform.position[1] + 2, brave.transform.position[2]), new Quaternion());
        curDisplayedEnemies++;
        ObjectPool.GetInstant().GetObj("BowEnemy", new Vector3(brave.transform.position[0] + 13, brave.transform.position[1] + 2, brave.transform.position[2]), new Quaternion());
        curDisplayedEnemies++;
    }

    
    public void EnemiesDestory()
    {
        curDisplayedEnemies--;
        totalGeneratedEnemies--;
        //如果敌人被摧毁的数量大于等于产生的总量则进入下一关
        if (totalGeneratedEnemies <= 0)
        {
            GameManager.INSTANCE.GameOver(true);
        }
    }
}
