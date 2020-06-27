using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagUIBackgroundTransfromScript : MonoBehaviour
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
        transform.Find("ATK").Find("Text").GetComponent<Text>().text = Buf.Attack.ToString();
        transform.Find("HP").Find("Text").GetComponent<Text>().text = Buf.HealthPointLimit.ToString();
        transform.Find("DEF").Find("Text").GetComponent<Text>().text = Buf.Defence.ToString();
    }
}
