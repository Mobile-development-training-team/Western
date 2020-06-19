using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalLottertyScript : MonoBehaviour
{
    private DateTime Check;

    private void Start()
    {
        Check = GameScript.NormalPool.Date;
        InvokeRepeating("TimeGo", 0, 1);
    }

    private void Update()
    {
        if (LotteryMessageButtonScript.HadLotteryMessage)
        {
            Rebuild();
            LotteryMessageButtonScript.HadLotteryMessage = false;
        }
    }

    private void Rebuild()
    {
        GameScript.NormalPool = GameScript.NormalPool.AddSeconds(10);
        InvokeRepeating("TimeGo", 0, 1);
    }

    private void TimeGo()
    {
        GameScript.NormalPool = GameScript.NormalPool.AddSeconds(-1);
        if (GameScript.NormalPool < Check)
        {
            CancelInvoke();
            GameScript.NormalPool = Check.Date;
            return;
        }
        transform.Find("Text").GetComponent<Text>().text = "低级抽奖" + '\n' + '\n' + GameScript.NormalPool.TimeOfDay.ToString().Substring(0, 8);
    }

    public void Click()
    {
        if (GameScript.NormalPool == Check)
        {
            LottertyMessageScript.NormalOrGood = false;
            transform.parent.parent.Find("LottertyMessage").GetComponent<Canvas>().enabled = true;
        }
    }
}
