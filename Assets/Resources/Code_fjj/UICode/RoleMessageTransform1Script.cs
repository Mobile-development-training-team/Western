using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoleMessageTransform1Script : MonoBehaviour
{
    private int Lv;
    private int Exp;
    private int ExpLimit;


    private void Start()
    {
        SelfUpdate();
    }

    private void Update()
    {
        if (Exp != DataManager.roleAttribute.GetExp())
        {
            SelfUpdate();
            return;
        }
        if (Lv != DataManager.roleAttribute.GetLevel())
        {
            SelfUpdate();
            return;
        }
    }

    private void SelfUpdate()
    {
        transform.Find("Name").GetComponent<Text>().text = DataManager.roleAttribute.GetName();
        Lv = DataManager.roleAttribute.GetLevel();
        Exp = DataManager.roleAttribute.GetExp();
        ExpLimit = DataManager.roleAttribute.GetExpLimit();
        transform.Find("Lv").Find("Text").GetComponent<Text>().text = Lv.ToString();
        transform.Find("Bar").Find("ExpBar_forward").GetComponent<Image>().fillAmount = (float)Exp / (float)ExpLimit;
        transform.Find("Bar").Find("Exp").GetComponent<Text>().text = Exp.ToString() + '/' + ExpLimit.ToString();
    }
}
