using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUICellScript : MonoBehaviour
{
    void Start()
    {
        Skill Buf = DataManager.SkillData[transform.GetSiblingIndex() + 3];
        transform.Find("Field").Find("Icon").GetComponent<Image>().sprite = GameScript.GetSprite(Buf.GetImagePath());
        transform.Find("Name").GetComponent<Text>().text = Buf.GetName();
        transform.Find("Lv").GetComponent<Text>().text = "等级:" + Buf.GetLevel().ToString() + '/' + Buf.GetLevelLimit();
        transform.Find("Message").GetComponent<Text>().text = Buf.GetMessage() + '\n' + Buf.GetAttibute().SkillReseveAttributeToString(Buf);
    }

}
