using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUILearnMessageButtonScript : MonoBehaviour
{
    public static int pastIndex = new int();

    public void Click()
    {
        if (DataManager.SkillData[pastIndex + 3].GetLevel() == 0)
        {
            if (DataManager.skillLevelUpIndex[pastIndex].LearnCount <= DataManager.roleEquipment.GetSkillDebris())
            {
                DataManager.roleEquipment.SetSkillDebris(DataManager.roleEquipment.GetSkillDebris() - DataManager.skillLevelUpIndex[pastIndex].LearnCount);
                DataManager.SkillData[pastIndex + 3].LevelUP();
                transform.parent.parent.Find("List").GetChild(pastIndex).Find("Message").Find("Lv").GetComponent<Text>().text = DataManager.SkillData[pastIndex + 3].GetLevel().ToString() + '/' + DataManager.SkillData[pastIndex + 3].GetLevelLimit().ToString();
                transform.parent.parent.Find("List").GetChild(pastIndex).Find("Message").Find("Text").GetComponent<Text>().text = DataManager.SkillData[pastIndex + 3].GetMessage() + '\n' + DataManager.SkillData[pastIndex + 3].GetAttibute().SkillReseveAttributeToString(DataManager.SkillData[pastIndex + 3]);
                transform.parent.GetComponent<Canvas>().enabled = false;
                transform.parent.parent.Find("List").GetChild(pastIndex).Find("Learn").Find("Text").GetComponent<Text>().text = "学习\n技能碎片:" + DataManager.skillLevelUpIndex[pastIndex].UpCount.ToString();
            }
        }
        else
        {
            if (DataManager.skillLevelUpIndex[pastIndex].UpCount <= DataManager.roleEquipment.GetSkillDebris())
            {
                DataManager.roleEquipment.SetSkillDebris(DataManager.roleEquipment.GetSkillDebris() - DataManager.skillLevelUpIndex[pastIndex].UpCount);
                DataManager.SkillData[pastIndex + 3].LevelUP();
                transform.parent.parent.Find("List").GetChild(pastIndex).Find("Message").Find("Lv").GetComponent<Text>().text = DataManager.SkillData[pastIndex + 3].GetLevel().ToString() + '/' + DataManager.SkillData[pastIndex + 3].GetLevelLimit().ToString();
                transform.parent.parent.Find("List").GetChild(pastIndex).Find("Message").Find("Text").GetComponent<Text>().text = DataManager.SkillData[pastIndex + 3].GetMessage() + '\n' + DataManager.SkillData[pastIndex + 3].GetAttibute().SkillReseveAttributeToString(DataManager.SkillData[pastIndex + 3]);
                transform.parent.GetComponent<Canvas>().enabled = false;
            }
        }
    }
}
