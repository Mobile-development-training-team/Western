using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUICellScript : MonoBehaviour
{
    void Start()
    {
        Skill Buf = DataManager.SkillData[transform.GetSiblingIndex() + 12];
        transform.Find("Index").Find("Icon").GetComponent<Image>().sprite = GameScript.GetSprite(Buf.GetImagePath());
        transform.Find("Message").Find("Name").GetComponent<Text>().text = Buf.GetName();
        transform.Find("Message").Find("Lv").GetComponent<Text>().text = Buf.GetLevel().ToString() + '/' + Buf.GetLevelLimit();
        transform.Find("Message").Find("Text").GetComponent<Text>().text = Buf.GetMessage() + '\n' + Buf.GetAttibute().SkillReseveAttributeToString(Buf);
    }

}
