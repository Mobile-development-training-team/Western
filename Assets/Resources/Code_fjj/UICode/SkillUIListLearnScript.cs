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
            transform.Find("Count").GetComponent<Text>().text = 'x' + DataManager.skillLevelUpIndex[transform.parent.GetSiblingIndex()].LearnCount.ToString();
        }
        else
        {
            transform.Find("Count").GetComponent<Text>().text = 'x' + DataManager.skillLevelUpIndex[transform.parent.GetSiblingIndex()].UpCount.ToString();
        }
    }

    public void Click()
    {
        int Index = transform.parent.GetSiblingIndex();
        Skill Buf = DataManager.SkillData[Index + 3];
        if (Buf.GetLevel() < Buf.GetLevelLimit())
        {
            if (Buf.GetLevel() == 0)
            {
                if (DataManager.skillLevelUpIndex[Index].LearnCount <= DataManager.roleEquipment.GetSkillDebris())
                {
                    DataManager.roleEquipment.SetSkillDebris(DataManager.roleEquipment.GetSkillDebris() - DataManager.skillLevelUpIndex[Index].LearnCount);
                    DataManager.SkillData[Index + 3].LevelUP();
                    transform.parent.Find("Lv").GetComponent<Text>().text = "等级:" + DataManager.SkillData[Index + 3].GetLevel().ToString() + '/' + DataManager.SkillData[Index + 3].GetLevelLimit().ToString();
                    transform.parent.Find("Message").GetComponent<Text>().text = DataManager.SkillData[Index + 3].GetMessage() + '\n' + DataManager.SkillData[Index + 3].GetAttibute().SkillReseveAttributeToString(DataManager.SkillData[Index + 3]);
                    transform.Find("Count").GetComponent<Text>().text = 'x' + DataManager.skillLevelUpIndex[Index].UpCount.ToString();
                }
            }
            else
            {
                if (DataManager.skillLevelUpIndex[Index].UpCount <= DataManager.roleEquipment.GetSkillDebris())
                {
                    DataManager.roleEquipment.SetSkillDebris(DataManager.roleEquipment.GetSkillDebris() - DataManager.skillLevelUpIndex[Index].UpCount);
                    DataManager.SkillData[Index + 3].LevelUP();
                    transform.parent.Find("Lv").GetComponent<Text>().text = "等级:" + DataManager.SkillData[Index + 3].GetLevel().ToString() + '/' + DataManager.SkillData[Index + 3].GetLevelLimit().ToString();
                    transform.parent.Find("Message").GetComponent<Text>().text = DataManager.SkillData[Index + 3].GetMessage() + '\n' + DataManager.SkillData[Index + 3].GetAttibute().SkillReseveAttributeToString(DataManager.SkillData[Index + 3]);
                }
            }
        }
    }
}
