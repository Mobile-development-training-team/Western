using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager03 : EnemiesManager
{
    protected override void setTotalWaveNum()
    {
        totalWaveNum = 3;
    }
    public override void getBuffer()
    {
        int bufferIndex = 0;
        switch (waveNum)
        {
            case 2:
                bufferIndex = brave.GetComponent<BufferManager>().getBuffer();
                gameUIController.GetBuff(0, bufferIndex);
                break;
            case 4:
                bufferIndex = brave.GetComponent<BufferManager>().getBuffer();
                gameUIController.GetBuff(1, bufferIndex);
                break;
            case 6:
                bufferIndex = brave.GetComponent<BufferManager>().getBuffer();
                gameUIController.GetBuff(2, bufferIndex);
                break;
            case 8:
                bufferIndex = brave.GetComponent<BufferManager>().getBuffer();
                gameUIController.GetBuff(3, bufferIndex);
                break;
            case 10:
                bufferIndex = brave.GetComponent<BufferManager>().getBuffer();
                gameUIController.GetBuff(4, bufferIndex);
                break;
            default:
                break;
        }
    }
    protected override void wave1()
    {
        StartCoroutine(generatorEnemy(0, -8, 1f));
        StartCoroutine(generatorEnemy(0, -8, 2f));
        StartCoroutine(generatorEnemy(0, -8, 3f));
        StartCoroutine(generatorEnemy(0, -8, 4f));
        curWaveStillHaveEnemies = 4;
    }
    protected override void wave2()
    {
        StartCoroutine(generatorEnemy(1, -8, 1f));
        StartCoroutine(generatorEnemy(1, -8, 2f));
        StartCoroutine(generatorEnemy(2, -8, 3f));
        StartCoroutine(generatorEnemy(2, -8, 1f));
        StartCoroutine(generatorEnemy(0, -8, 2f));
        StartCoroutine(generatorEnemy(0, -8, 4f));
        curWaveStillHaveEnemies = 6;
    }
    protected override void wave3()
    {
        StartCoroutine(generatorEnemy(0, -8, 1f));
        StartCoroutine(generatorEnemy(0, -8, 2f));
        StartCoroutine(generatorEnemy(1, -8, 1f));
        StartCoroutine(generatorEnemy(1, -8, 2f));
        StartCoroutine(generatorEnemy(2, -8, 3f));
        StartCoroutine(generatorEnemy(2, -8, 4f));
        curWaveStillHaveEnemies = 6;
    }
    protected override void wave4()
    {
    }
    protected override void wave5()
    {
    }
    protected override void wave6()
    {
    }
    protected override void wave7()
    {
    }
    protected override void wave8()
    {
    }
    protected override void wave9()
    {
    }
    protected override void wave10()
    {
    }
    protected override void wave11()
    {
    }
    protected override void wave12()
    {
    }
    protected override void wave13()
    {
    }
    protected override void wave14()
    {
    }
    protected override void wave15()
    {
    }
}
