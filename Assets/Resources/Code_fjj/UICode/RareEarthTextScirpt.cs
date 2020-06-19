using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RareEarthTextScirpt : MonoBehaviour
{
    private int Buf;
    void Start()
    {
        SelfUpdate();
    }

    private void SelfUpdate()
    {
        Buf = DataManager.roleEquipment.GetRareEarthCount();
        GetComponent<Text>().text = "稀土：" + Buf.ToString();
    }

    void Update()
    {
        if (Buf != DataManager.roleEquipment.GetRareEarthCount())
        {
            SelfUpdate();
        }
    }
}
