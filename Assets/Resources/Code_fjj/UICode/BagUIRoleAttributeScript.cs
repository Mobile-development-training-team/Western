using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagUIRoleAttributeScript : MonoBehaviour
{
    private Attribute Buf;

    void Start()
    {
        SelfUpdate();
    }

    void Update()
    {
        if (!Attribute.AttributeCompare(Buf, GameScript.GameRoleAttribute))
        {
            SelfUpdate();
        }
    }

    private void SelfUpdate()
    {
        Buf = GameScript.GameRoleAttribute;
        transform.Find("ATK").Find("Text").GetComponent<Text>().text = "攻击:" + Buf.Attack.ToString();
        transform.Find("HP").Find("Text").GetComponent<Text>().text = "生命:" + Buf.HealthPointLimit.ToString();
        transform.Find("DEF").Find("Text").GetComponent<Text>().text = "防御:" + Buf.Defence.ToString();
    }
}
