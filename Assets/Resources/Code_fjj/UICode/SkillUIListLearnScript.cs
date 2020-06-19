using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUIListLearnScript : MonoBehaviour
{
    public void Click()
    {
        Skill Buf = DataManager.SkillData[transform.parent.GetSiblingIndex() + 12];
        if (Buf.GetLevel() < Buf.GetLevelLimit())
        {
            SkillUILearnMessageButtonScript.pastIndex = transform.parent.GetSiblingIndex();
            GameObject gBuf = transform.parent.parent.parent.Find("LearnMessage").gameObject;
            gBuf.transform.Find("Name").GetComponent<Text>().text = Buf.GetName() + " Lv" + (Buf.GetLevel() + 1).ToString();
            gBuf.transform.Find("Debris").GetComponent<Text>().text = "消耗技能碎片：" + DataManager.skillLevelUpIndex[SkillUILearnMessageButtonScript.pastIndex].Count.ToString();
            transform.parent.parent.parent.Find("LearnMessage").GetComponent<Canvas>().enabled = true;
        }
    }
}
