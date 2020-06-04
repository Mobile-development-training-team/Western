using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager01 : MonoBehaviour
{
    private float waveTimer = 3f;   //每波间隔时间
    private int waveNum = 0;        //敌人波数

    // Update is called once per frame
    void Update()
    {
        waveTimer -= Time.deltaTime;
        if (waveTimer <= 0)
        {
            generatorEnemiesWave();
            waveTimer = 3f + (float)waveNum;
            waveNum = (waveNum + 1) % 3;
        }
    }

    private void generatorEnemiesWave()
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
        ObjectPool.GetInstant().GetObj("TwoHandsSwordEnemy", new Vector3(-13, -7, -2), new Quaternion());
    }

    private void wave2()
    {
        ObjectPool.GetInstant().GetObj("2Hand-Sword-Enemy", new Vector3(-13, -7, -2), new Quaternion());
        ObjectPool.GetInstant().GetObj("TwoHandsSwordEnemy", new Vector3(13, -7, -2), new Quaternion());
    }

    private void wave3()
    {
        ObjectPool.GetInstant().GetObj("2Hand-Sword-Enemy", new Vector3(-13, -7, -2), new Quaternion());
        ObjectPool.GetInstant().GetObj("TwoHandsSwordEnemy", new Vector3(0, -7, -2), new Quaternion());
        ObjectPool.GetInstant().GetObj("2Hand-Sword-Enemy", new Vector3(13, -7, -2), new Quaternion());
    }

}
