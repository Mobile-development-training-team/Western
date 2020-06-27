using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VITScript : MonoBehaviour
{
    private DateTime check;
    public static bool ReStart = false;

    private void Start()
    {
        check = GameScript.VITCounter.Date;
        InvokeRepeating("VITTimeGo", 0, 1);
    }

    private void Update()
    {
        if (ReStart)
        {
            ReStart = false;
            InvokeRepeating("VITTimeGo", 0, 1);
        }
    }

    private void VITTimeGo()
    {
        if (GameScript.VIT < 24)
        {
            GameScript.VITCounter = GameScript.VITCounter.AddSeconds(-1);
            if (GameScript.VITCounter < check)
            {
                GameScript.VIT++;
                GameScript.VITCounter = check.Date;
                GameScript.VITCounter = GameScript.VITCounter.AddMinutes(GameScript.PerVITMinute);
                if (GameScript.VIT == 24)
                {
                    GetComponent<Text>().text = GameScript.VITCounter.TimeOfDay.ToString().Substring(0, 8);
                    CancelInvoke();
                    return;
                }
            }
            GetComponent<Text>().text = GameScript.VITCounter.TimeOfDay.ToString().Substring(0, 8);
        }
        else
        {
            GameScript.VITCounter = check.Date;
            GameScript.VITCounter = GameScript.VITCounter.AddMinutes(GameScript.PerVITMinute);
            GetComponent<Text>().text = GameScript.VITCounter.TimeOfDay.ToString().Substring(0, 8);
            CancelInvoke();
        }
    }
}
