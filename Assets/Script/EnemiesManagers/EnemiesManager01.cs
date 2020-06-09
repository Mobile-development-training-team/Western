using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager01 : MonoBehaviour
{
    public static EnemiesManager01 Instance;
    private float waveTimer = 5f;   //每波间隔时间
    private int waveNum = 0;        //敌人波数
    private float wave1Time = 10f;  //第一波敌人到达时间
    private float wave2Time = 20f;  //第二波敌人到达时间
    private float wave3Time = 30f;  //第三波敌人到达时间

    GameObject brave;

    void Awake()
    {
        brave = GameObject.FindGameObjectWithTag("brave");
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
            }
        }
        if (wave2Time > 0)
        {
            wave2Time -= Time.deltaTime;
            if (wave2Time < 0)
            {
                wave2();
            }
        }
        if (wave3Time > 0)
        {
            wave3Time -= Time.deltaTime;
            if (wave3Time < 0)
            {
                wave3();
            }
        }
    }

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

    private void wave1()
    {
        ObjectPool.GetInstant().GetObj("TwoHandsSwordEnemy", new Vector3(brave.transform.position[0] - 13, brave.transform.position[1] + 2, brave.transform.position[2]), new Quaternion());
    }

    private void wave2()
    {
        ObjectPool.GetInstant().GetObj("BowEnemy", new Vector3(brave.transform.position[0] - 13, brave.transform.position[1] + 2, brave.transform.position[2]), new Quaternion());
        ObjectPool.GetInstant().GetObj("TwoHandsSwordEnemy", new Vector3(brave.transform.position[0] + 13, brave.transform.position[1] + 2, brave.transform.position[2]), new Quaternion());
    }

    private void wave3()
    {
        ObjectPool.GetInstant().GetObj("BowEnemy", new Vector3(brave.transform.position[0] - 13, brave.transform.position[1] + 2, brave.transform.position[2]), new Quaternion());
        ObjectPool.GetInstant().GetObj("TwoHandsSwordEnemy", new Vector3(brave.transform.position[0] - 6, brave.transform.position[1] + 2, brave.transform.position[2]), new Quaternion());
        ObjectPool.GetInstant().GetObj("BowEnemy", new Vector3(brave.transform.position[0] + 13, brave.transform.position[1] + 2, brave.transform.position[2]), new Quaternion());
    }

}
