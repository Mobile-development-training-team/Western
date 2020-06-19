using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUICellScript : MonoBehaviour
{
    void Start()
    {
        Skill Buf = DataManager.SkillData[transform.GetSiblingIndex() + 12];
        transform.Find("Index").Find("Text").GetComponent<Text>().text = (transform.GetSiblingIndex() + 1).ToString();
        transform.Find("Message").Find("Name").GetComponent<Text>().text = Buf.GetName();
        transform.Find("Message").Find("Lv").GetComponent<Text>().text = Buf.GetLevel().ToString() + '/' + Buf.GetLevelLimit();
        transform.Find("Message").Find("Text").GetComponent<Text>().text = Buf.GetMessage();
    }

}
