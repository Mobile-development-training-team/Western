using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoodLottertyScript : MonoBehaviour
{
    private DateTime Check;

    private void Start()
    {
        Check = GameScript.GoodPool.Date;
        InvokeRepeating("TimeGo", 0, 1);
    }

    private void TimeGo()
    {
        GameScript.GoodPool = GameScript.GoodPool.AddSeconds(-1);
        if (GameScript.GoodPool < Check)
        {
            CancelInvoke();
            return;
        }
        transform.Find("Text").GetComponent<Text>().text = "高级抽奖" + '\n' + '\n' + GameScript.GoodPool.TimeOfDay.ToString().Substring(0, 8);
    }

    public void Click()
    {
        if (GameScript.GoodPool < Check)
        {
            LottertyMessageScript.NormalOrGood = true;
            transform.parent.parent.Find("LottertyMessage").GetComponent<Canvas>().enabled = true;
        }
    }
}
