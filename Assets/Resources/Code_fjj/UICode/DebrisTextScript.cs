using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebrisTextScript : MonoBehaviour
{
    private int Buf;
    void Start()
    {
        SelfUpdate();
    }

    private void SelfUpdate()
    {
        Buf = DataManager.roleEquipment.GetSkillDebris();
        GetComponent<Text>().text = "技能碎片：" + Buf.ToString();
    }

    void Update()
    {
        if (Buf != DataManager.roleEquipment.GetSkillDebris())
        {
            SelfUpdate();
        }
    }
}
