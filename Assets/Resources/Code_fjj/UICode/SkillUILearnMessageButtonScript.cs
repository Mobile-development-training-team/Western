using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUILearnMessageButtonScript : MonoBehaviour
{
    public static int pastIndex = new int();

    public void Click()
    {
        if (DataManager.skillLevelUpIndex[pastIndex].Count <= DataManager.roleEquipment.GetSkillDebris())
        {
            DataManager.roleEquipment.SetSkillDebris(DataManager.roleEquipment.GetSkillDebris() - DataManager.skillLevelUpIndex[pastIndex].Count);
            DataManager.SkillData[pastIndex + 12].LevelUP();
            transform.parent.parent.Find("List").GetChild(pastIndex).Find("Message").Find("Lv").GetComponent<Text>().text = DataManager.SkillData[pastIndex + 12].GetLevel().ToString() + '/' + DataManager.SkillData[pastIndex + 12].GetLevelLimit().ToString();
            transform.parent.GetComponent<Canvas>().enabled = false;
        }
    }
}
