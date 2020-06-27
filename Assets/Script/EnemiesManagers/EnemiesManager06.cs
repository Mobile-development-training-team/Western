using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager06 : EnemiesManager
{
    protected override void setTotalWaveNum()
    {
        thisLevel = GameManager.LevelDatas[5];
        totalWaveNum = thisLevel.Waves.Count;
        if (totalWaveNum > 14)
        {
            totalWaveNum = 14;
        }
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
}
