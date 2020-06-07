using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager01 : MonoBehaviour
{
    public static EnemiesManager01 Instance;
    private float waveTimer = 5f;   //每波间隔时间
    private int waveNum = 0;        //敌人波数

    GameObject brave;

    void Awake()
    {
        brave = GameObject.FindGameObjectWithTag("brave");
    }

    // Update is called once per frame
    void Update()
    {
        waveTimer -= Time.deltaTime;
        if (waveTimer <= 0)
        {
            generatorEnemiesWave();
            waveTimer = 5f + (float)waveNum*2;
            waveNum = (waveNum + 1) % 3;
        }
    }

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
        ObjectPool.GetInstant().GetObj("2Hand-Sword-Enemy", new Vector3(brave.transform.position[0] - 13, brave.transform.position[1] + 2, brave.transform.position[2]), new Quaternion());
        ObjectPool.GetInstant().GetObj("TwoHandsSwordEnemy", new Vector3(brave.transform.position[0] + 13, brave.transform.position[1] + 2, brave.transform.position[2]), new Quaternion());
    }

    private void wave3()
    {
        ObjectPool.GetInstant().GetObj("2Hand-Sword-Enemy", new Vector3(brave.transform.position[0] - 13, brave.transform.position[1] + 2, brave.transform.position[2]), new Quaternion());
        ObjectPool.GetInstant().GetObj("TwoHandsSwordEnemy", new Vector3(brave.transform.position[0] - 6, brave.transform.position[1] + 2, brave.transform.position[2]), new Quaternion());
        ObjectPool.GetInstant().GetObj("2Hand-Sword-Enemy", new Vector3(brave.transform.position[0] + 13, brave.transform.position[1] + 2, brave.transform.position[2]), new Quaternion());
    }

}
