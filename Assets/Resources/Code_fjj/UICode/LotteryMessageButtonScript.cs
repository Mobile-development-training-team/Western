using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LotteryMessageButtonScript : MonoBehaviour
{
    public static bool HadLotteryMessage = false;

    private Item[] NormalDebrisPool;
    private Item[] ExcellentDebrisPool;
    private Item[] RareDebrisPool;
    private Item[] EpicDebrisPool;

    private void Start()
    {
        NormalDebrisPool = new Item[4];
        ExcellentDebrisPool = new Item[4];
        RareDebrisPool = new Item[4];
        EpicDebrisPool = new Item[4];
        NormalDebrisPool[0] = DataManager.GameItemIndex[1];
        NormalDebrisPool[1] = DataManager.GameItemIndex[3];
        NormalDebrisPool[2] = DataManager.GameItemIndex[17];
        NormalDebrisPool[3] = DataManager.GameItemIndex[25];

        ExcellentDebrisPool[0] = DataManager.GameItemIndex[5];
        ExcellentDebrisPool[1] = DataManager.GameItemIndex[7];
        ExcellentDebrisPool[2] = DataManager.GameItemIndex[19];
        ExcellentDebrisPool[3] = DataManager.GameItemIndex[27];

        RareDebrisPool[0] = DataManager.GameItemIndex[9];
        RareDebrisPool[1] = DataManager.GameItemIndex[11];
        RareDebrisPool[2] = DataManager.GameItemIndex[21];
        RareDebrisPool[3] = DataManager.GameItemIndex[29];

        EpicDebrisPool[0] = DataManager.GameItemIndex[13];
        EpicDebrisPool[1] = DataManager.GameItemIndex[15];
        EpicDebrisPool[2] = DataManager.GameItemIndex[23];
        EpicDebrisPool[3] = DataManager.GameItemIndex[31];
    }

    public void Click()
    {
        if (LottertyMessageScript.NormalOrGood)
        {
            RunGoodLotterty();
        }
        else
        {
            RunNormalLotterty();
        }
        HadLotteryMessage = true;
        transform.parent.GetComponent<Canvas>().enabled = false;
        transform.parent.parent.Find("Done").GetComponent<Canvas>().enabled = true;
    }

    private void RunNormalLotterty()
    {
        System.Random ra = new System.Random();
        int raNum = ra.Next() % 100;
        if (raNum < 5)
        {
            int equipmentDebris = 5;
            int rareEarth = 80;
            int skillDebris = 25;
            int indexNum = ra.Next() % 4;
            DataManager.bag.AddItem(EpicDebrisPool[indexNum], equipmentDebris);
            DataManager.roleEquipment.SetRareEarthCount(DataManager.roleEquipment.GetRareEarthCount() + rareEarth);
            DataManager.roleEquipment.SetSkillDebris(DataManager.roleEquipment.GetSkillDebris() + skillDebris);
            transform.parent.parent.Find("Done").Find("Material").Find("Equipment").GetComponent<Image>().sprite = GameScript.QualityEpic;
            transform.parent.parent.Find("Done").Find("Material").Find("Equipment").Find("Icon").GetComponent<Image>().sprite = GameScript.GetSprite(EpicDebrisPool[indexNum].GetImagePath());
            transform.parent.parent.Find("Done").Find("Material").Find("Equipment").Find("Text").GetComponent<Text>().text = 'x' + equipmentDebris.ToString();

            transform.parent.parent.Find("Done").Find("Material").Find("Rare-earth").GetComponent<Image>().sprite = GameScript.QualityExcellent;
            transform.parent.parent.Find("Done").Find("Material").Find("Rare-earth").Find("Icon").GetComponent<Image>().sprite = GameScript.RareEarthSprite;
            transform.parent.parent.Find("Done").Find("Material").Find("Rare-earth").Find("Text").GetComponent<Text>().text = 'x' + rareEarth.ToString();

            transform.parent.parent.Find("Done").Find("Material").Find("Skill").GetComponent<Image>().sprite = GameScript.QualityExcellent;
            transform.parent.parent.Find("Done").Find("Material").Find("Skill").Find("Icon").GetComponent<Image>().sprite = GameScript.SkillDebrisSprite;
            transform.parent.parent.Find("Done").Find("Material").Find("Skill").Find("Text").GetComponent<Text>().text = 'x' + skillDebris.ToString();
        }
        else if (raNum < 20)
        {
            int equipmentDebris = 5;
            int rareEarth = 70;
            int skillDebris = 20;
            int indexNum = ra.Next() % 4;
            DataManager.bag.AddItem(RareDebrisPool[indexNum], equipmentDebris);
            DataManager.roleEquipment.SetRareEarthCount(DataManager.roleEquipment.GetRareEarthCount() + rareEarth);
            DataManager.roleEquipment.SetSkillDebris(DataManager.roleEquipment.GetSkillDebris() + skillDebris);
            transform.parent.parent.Find("Done").Find("Material").Find("Equipment").GetComponent<Image>().sprite = GameScript.QualityRare;
            transform.parent.parent.Find("Done").Find("Material").Find("Equipment").Find("Icon").GetComponent<Image>().sprite = GameScript.GetSprite(RareDebrisPool[indexNum].GetImagePath());
            transform.parent.parent.Find("Done").Find("Material").Find("Equipment").Find("Text").GetComponent<Text>().text = 'x' + equipmentDebris.ToString();

            transform.parent.parent.Find("Done").Find("Material").Find("Rare-earth").GetComponent<Image>().sprite = GameScript.QualityExcellent;
            transform.parent.parent.Find("Done").Find("Material").Find("Rare-earth").Find("Icon").GetComponent<Image>().sprite = GameScript.RareEarthSprite;
            transform.parent.parent.Find("Done").Find("Material").Find("Rare-earth").Find("Text").GetComponent<Text>().text = 'x' + rareEarth.ToString();

            transform.parent.parent.Find("Done").Find("Material").Find("Skill").GetComponent<Image>().sprite = GameScript.QualityExcellent;
            transform.parent.parent.Find("Done").Find("Material").Find("Skill").Find("Icon").GetComponent<Image>().sprite = GameScript.SkillDebrisSprite;
            transform.parent.parent.Find("Done").Find("Material").Find("Skill").Find("Text").GetComponent<Text>().text = 'x' + skillDebris.ToString();
        }
        else if (raNum < 50)
        {
            int equipmentDebris = 10;
            int rareEarth = 60;
            int skillDebris = 15;
            int indexNum = ra.Next() % 4;
            DataManager.bag.AddItem(ExcellentDebrisPool[indexNum], equipmentDebris);
            DataManager.roleEquipment.SetRareEarthCount(DataManager.roleEquipment.GetRareEarthCount() + rareEarth);
            DataManager.roleEquipment.SetSkillDebris(DataManager.roleEquipment.GetSkillDebris() + skillDebris);
            transform.parent.parent.Find("Done").Find("Material").Find("Equipment").GetComponent<Image>().sprite = GameScript.QualityExcellent;
            transform.parent.parent.Find("Done").Find("Material").Find("Equipment").Find("Icon").GetComponent<Image>().sprite = GameScript.GetSprite(ExcellentDebrisPool[indexNum].GetImagePath());
            transform.parent.parent.Find("Done").Find("Material").Find("Equipment").Find("Text").GetComponent<Text>().text = 'x' + equipmentDebris.ToString();

            transform.parent.parent.Find("Done").Find("Material").Find("Rare-earth").GetComponent<Image>().sprite = GameScript.QualityExcellent;
            transform.parent.parent.Find("Done").Find("Material").Find("Rare-earth").Find("Icon").GetComponent<Image>().sprite = GameScript.RareEarthSprite;
            transform.parent.parent.Find("Done").Find("Material").Find("Rare-earth").Find("Text").GetComponent<Text>().text = 'x' + rareEarth.ToString();

            transform.parent.parent.Find("Done").Find("Material").Find("Skill").GetComponent<Image>().sprite = GameScript.QualityExcellent;
            transform.parent.parent.Find("Done").Find("Material").Find("Skill").Find("Icon").GetComponent<Image>().sprite = GameScript.SkillDebrisSprite;
            transform.parent.parent.Find("Done").Find("Material").Find("Skill").Find("Text").GetComponent<Text>().text = 'x' + skillDebris.ToString();
        }
        else
        {
            int equipmentDebris = 15;
            int rareEarth = 40;
            int skillDebris = 10;
            int indexNum = ra.Next() % 4;
            DataManager.bag.AddItem(NormalDebrisPool[indexNum], equipmentDebris);
            DataManager.roleEquipment.SetRareEarthCount(DataManager.roleEquipment.GetRareEarthCount() + rareEarth);
            DataManager.roleEquipment.SetSkillDebris(DataManager.roleEquipment.GetSkillDebris() + skillDebris);
            transform.parent.parent.Find("Done").Find("Material").Find("Equipment").GetComponent<Image>().sprite = GameScript.QualityNormal;
            transform.parent.parent.Find("Done").Find("Material").Find("Equipment").Find("Icon").GetComponent<Image>().sprite = GameScript.GetSprite(NormalDebrisPool[indexNum].GetImagePath());
            transform.parent.parent.Find("Done").Find("Material").Find("Equipment").Find("Text").GetComponent<Text>().text = 'x' + equipmentDebris.ToString();

            transform.parent.parent.Find("Done").Find("Material").Find("Rare-earth").GetComponent<Image>().sprite = GameScript.QualityExcellent;
            transform.parent.parent.Find("Done").Find("Material").Find("Rare-earth").Find("Icon").GetComponent<Image>().sprite = GameScript.RareEarthSprite;
            transform.parent.parent.Find("Done").Find("Material").Find("Rare-earth").Find("Text").GetComponent<Text>().text = 'x' + rareEarth.ToString();

            transform.parent.parent.Find("Done").Find("Material").Find("Skill").GetComponent<Image>().sprite = GameScript.QualityExcellent;
            transform.parent.parent.Find("Done").Find("Material").Find("Skill").Find("Icon").GetComponent<Image>().sprite = GameScript.SkillDebrisSprite;
            transform.parent.parent.Find("Done").Find("Material").Find("Skill").Find("Text").GetComponent<Text>().text = 'x' + skillDebris.ToString();
        }
        
    }
    private void RunGoodLotterty()
    {
        System.Random ra = new System.Random();
        int raNum = ra.Next() % 100;
        if (raNum < 5)
        {
            int equipmentDebris = 5;
            int rareEarth = 100;
            int slayCount = 4;
            int indexNum = ra.Next() % 4;
            DataManager.bag.AddItem(EpicDebrisPool[indexNum], equipmentDebris);
            DataManager.roleEquipment.SetRareEarthCount(DataManager.roleEquipment.GetRareEarthCount() + rareEarth);
            DataManager.roleEquipment.SetSlayCount(DataManager.roleEquipment.GetSlayCount() + slayCount);
            transform.parent.parent.Find("Done").Find("Material").Find("Equipment").GetComponent<Image>().sprite = GameScript.QualityEpic;
            transform.parent.parent.Find("Done").Find("Material").Find("Equipment").Find("Icon").GetComponent<Image>().sprite = GameScript.GetSprite(EpicDebrisPool[indexNum].GetImagePath());
            transform.parent.parent.Find("Done").Find("Material").Find("Equipment").Find("Text").GetComponent<Text>().text = 'x' + equipmentDebris.ToString();

            transform.parent.parent.Find("Done").Find("Material").Find("Rare-earth").GetComponent<Image>().sprite = GameScript.QualityExcellent;
            transform.parent.parent.Find("Done").Find("Material").Find("Rare-earth").Find("Icon").GetComponent<Image>().sprite = GameScript.RareEarthSprite;
            transform.parent.parent.Find("Done").Find("Material").Find("Rare-earth").Find("Text").GetComponent<Text>().text = 'x' + rareEarth.ToString();

            transform.parent.parent.Find("Done").Find("Material").Find("Skill").GetComponent<Image>().sprite = GameScript.QualityRare;
            transform.parent.parent.Find("Done").Find("Material").Find("Skill").Find("Icon").GetComponent<Image>().sprite = GameScript.GetSprite(DataManager.SkillData[0].GetImagePath());
            transform.parent.parent.Find("Done").Find("Material").Find("Skill").Find("Text").GetComponent<Text>().text = 'x' + slayCount.ToString();
        }
        else if (raNum < 20)
        {
            int equipmentDebris = 5;
            int rareEarth = 90;
            int slayCount = 3;
            int indexNum = ra.Next() % 4;
            DataManager.bag.AddItem(RareDebrisPool[indexNum], equipmentDebris);
            DataManager.roleEquipment.SetRareEarthCount(DataManager.roleEquipment.GetRareEarthCount() + rareEarth);
            DataManager.roleEquipment.SetSlayCount(DataManager.roleEquipment.GetSlayCount() + slayCount);
            transform.parent.parent.Find("Done").Find("Material").Find("Equipment").GetComponent<Image>().sprite = GameScript.QualityRare;
            transform.parent.parent.Find("Done").Find("Material").Find("Equipment").Find("Icon").GetComponent<Image>().sprite = GameScript.GetSprite(RareDebrisPool[indexNum].GetImagePath());
            transform.parent.parent.Find("Done").Find("Material").Find("Equipment").Find("Text").GetComponent<Text>().text = 'x' + equipmentDebris.ToString();

            transform.parent.parent.Find("Done").Find("Material").Find("Rare-earth").GetComponent<Image>().sprite = GameScript.QualityExcellent;
            transform.parent.parent.Find("Done").Find("Material").Find("Rare-earth").Find("Icon").GetComponent<Image>().sprite = GameScript.RareEarthSprite;
            transform.parent.parent.Find("Done").Find("Material").Find("Rare-earth").Find("Text").GetComponent<Text>().text = 'x' + rareEarth.ToString();

            transform.parent.parent.Find("Done").Find("Material").Find("Skill").GetComponent<Image>().sprite = GameScript.QualityRare;
            transform.parent.parent.Find("Done").Find("Material").Find("Skill").Find("Icon").GetComponent<Image>().sprite = GameScript.GetSprite(DataManager.SkillData[0].GetImagePath());
            transform.parent.parent.Find("Done").Find("Material").Find("Skill").Find("Text").GetComponent<Text>().text = 'x' + slayCount.ToString();
        }
        else if (raNum < 50)
        {
            int equipmentDebris = 12;
            int rareEarth = 70;
            int slayCount = 2;
            int indexNum = ra.Next() % 4;
            DataManager.bag.AddItem(ExcellentDebrisPool[indexNum], equipmentDebris);
            DataManager.roleEquipment.SetRareEarthCount(DataManager.roleEquipment.GetRareEarthCount() + rareEarth);
            DataManager.roleEquipment.SetSlayCount(DataManager.roleEquipment.GetSlayCount() + slayCount);
            transform.parent.parent.Find("Done").Find("Material").Find("Equipment").GetComponent<Image>().sprite = GameScript.QualityExcellent;
            transform.parent.parent.Find("Done").Find("Material").Find("Equipment").Find("Icon").GetComponent<Image>().sprite = GameScript.GetSprite(ExcellentDebrisPool[indexNum].GetImagePath());
            transform.parent.parent.Find("Done").Find("Material").Find("Equipment").Find("Text").GetComponent<Text>().text = 'x' + equipmentDebris.ToString();

            transform.parent.parent.Find("Done").Find("Material").Find("Rare-earth").GetComponent<Image>().sprite = GameScript.QualityExcellent;
            transform.parent.parent.Find("Done").Find("Material").Find("Rare-earth").Find("Icon").GetComponent<Image>().sprite = GameScript.RareEarthSprite;
            transform.parent.parent.Find("Done").Find("Material").Find("Rare-earth").Find("Text").GetComponent<Text>().text = 'x' + rareEarth.ToString();

            transform.parent.parent.Find("Done").Find("Material").Find("Skill").GetComponent<Image>().sprite = GameScript.QualityRare;
            transform.parent.parent.Find("Done").Find("Material").Find("Skill").Find("Icon").GetComponent<Image>().sprite = GameScript.GetSprite(DataManager.SkillData[0].GetImagePath());
            transform.parent.parent.Find("Done").Find("Material").Find("Skill").Find("Text").GetComponent<Text>().text = 'x' + slayCount.ToString();
        }
        else
        {
            int equipmentDebris = 10;
            int rareEarth = 50;
            int slayCount = 1;
            int indexNum = ra.Next() % 4;
            DataManager.bag.AddItem(ExcellentDebrisPool[indexNum], equipmentDebris);
            DataManager.roleEquipment.SetRareEarthCount(DataManager.roleEquipment.GetRareEarthCount() + rareEarth);
            DataManager.roleEquipment.SetSlayCount(DataManager.roleEquipment.GetSlayCount() + slayCount);
            transform.parent.parent.Find("Done").Find("Material").Find("Equipment").GetComponent<Image>().sprite = GameScript.QualityExcellent;
            transform.parent.parent.Find("Done").Find("Material").Find("Equipment").Find("Icon").GetComponent<Image>().sprite = GameScript.GetSprite(ExcellentDebrisPool[indexNum].GetImagePath());
            transform.parent.parent.Find("Done").Find("Material").Find("Equipment").Find("Text").GetComponent<Text>().text = 'x' + equipmentDebris.ToString();

            transform.parent.parent.Find("Done").Find("Material").Find("Rare-earth").GetComponent<Image>().sprite = GameScript.QualityExcellent;
            transform.parent.parent.Find("Done").Find("Material").Find("Rare-earth").Find("Icon").GetComponent<Image>().sprite = GameScript.RareEarthSprite;
            transform.parent.parent.Find("Done").Find("Material").Find("Rare-earth").Find("Text").GetComponent<Text>().text = 'x' + rareEarth.ToString();

            transform.parent.parent.Find("Done").Find("Material").Find("Skill").GetComponent<Image>().sprite = GameScript.QualityRare;
            transform.parent.parent.Find("Done").Find("Material").Find("Skill").Find("Icon").GetComponent<Image>().sprite = GameScript.GetSprite(DataManager.SkillData[0].GetImagePath());
            transform.parent.parent.Find("Done").Find("Material").Find("Skill").Find("Text").GetComponent<Text>().text = 'x' + slayCount.ToString();
        }
    }
        
}
