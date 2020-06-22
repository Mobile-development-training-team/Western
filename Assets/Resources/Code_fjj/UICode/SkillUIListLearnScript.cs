using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUIListLearnScript : MonoBehaviour
{
    private void Start()
    {
        if (DataManager.SkillData[transform.parent.GetSiblingIndex() + 3].GetLevel() == 0)
        {
            transform.Find("Text").GetComponent<Text>().text = "学习\n技能碎片:" + DataManager.skillLevelUpIndex[transform.parent.GetSiblingIndex()].LearnCount.ToString();
        }
        else
        {
            transform.Find("Text").GetComponent<Text>().text = "学习\n技能碎片:" + DataManager.skillLevelUpIndex[transform.parent.GetSiblingIndex()].UpCount.ToString();
        }
    }

    public void Click()
    {
        Skill Buf = DataManager.SkillData[transform.parent.GetSiblingIndex() + 3];
        if (Buf.GetLevel() < Buf.GetLevelLimit())
        {
            SkillUILearnMessageButtonScript.pastIndex = transform.parent.GetSiblingIndex();
            GameObject gBuf = transform.parent.parent.parent.Find("LearnMessage").gameObject;
            gBuf.transform.Find("Name").GetComponent<Text>().text = Buf.GetName() + " Lv" + (Buf.GetLevel() + 1).ToString();
            if (DataManager.SkillData[transform.parent.GetSiblingIndex() + 3].GetLevel() == 0)
            {
                gBuf.transform.Find("Debris").GetComponent<Text>().text = "消耗技能碎片：" + DataManager.skillLevelUpIndex[SkillUILearnMessageButtonScript.pastIndex].LearnCount.ToString();
            }
            else
            {
                gBuf.transform.Find("Debris").GetComponent<Text>().text = "消耗技能碎片：" + DataManager.skillLevelUpIndex[SkillUILearnMessageButtonScript.pastIndex].UpCount.ToString();
            }
            
            transform.parent.parent.parent.Find("LearnMessage").GetComponent<Canvas>().enabled = true;
        }
    }
}
