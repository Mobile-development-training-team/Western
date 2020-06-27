using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalLottertyScript : MonoBehaviour
{
    private DateTime Check;

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

        Check = GameScript.NormalPool.Date;
        InvokeRepeating("TimeGo", 0, 1);
    }

    private void Update()
    {
        if (LotteryMessageButtonScript.HadLotteryMessage && !LottertyMessageScript.NormalOrGood)
        {
            Rebuild();
            LotteryMessageButtonScript.HadLotteryMessage = false;
        }
    }

    private void Rebuild()
    {
        GameScript.NormalPool = Check.Date;
        GameScript.NormalPool = GameScript.NormalPool.AddHours(4);
        InvokeRepeating("TimeGo", 0, 1);
    }

    private void TimeGo()
    {
        GameScript.NormalPool = GameScript.NormalPool.AddSeconds(-1);
        if (GameScript.NormalPool < Check)
        {
            CancelInvoke();
            GameScript.NormalPool = Check.Date;
            return;
        }
        transform.Find("Text").GetComponent<Text>().text = GameScript.NormalPool.TimeOfDay.ToString().Substring(0, 8);
    }

    public void Click()
    {
        if (GameScript.NormalPool == Check)
        {
            LottertyMessageScript.NormalOrGood = false;
            RunNormalLotterty();
            transform.parent.parent.parent.parent.Find("LottertyMessage").GetComponent<Canvas>().enabled = true;
        }
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

            GameObject gBuf = transform.parent.parent.parent.parent.Find("LottertyMessage").Find("Board").Find("Gift").gameObject;

            gBuf.transform.Find("Gift1").Find("Quality").GetComponent<Image>().sprite = GameScript.QualityEpic;
            gBuf.transform.Find("Gift1").Find("Icon").GetComponent<Image>().sprite = GameScript.GetSprite(EpicDebrisPool[indexNum].GetImagePath());
            gBuf.transform.Find("Gift1").Find("Count").GetComponent<Text>().text = 'x' + equipmentDebris.ToString();

            gBuf.transform.Find("Gift2").Find("Quality").GetComponent<Image>().sprite = GameScript.QualityExcellent;
            gBuf.transform.Find("Gift2").Find("Icon").GetComponent<Image>().sprite = GameScript.RareEarthSprite;
            gBuf.transform.Find("Gift2").Find("Count").GetComponent<Text>().text = 'x' + rareEarth.ToString();

            gBuf.transform.Find("Gift3").Find("Quality").GetComponent<Image>().sprite = GameScript.QualityExcellent;
            gBuf.transform.Find("Gift3").Find("Icon").GetComponent<Image>().sprite = GameScript.SkillDebrisSprite;
            gBuf.transform.Find("Gift3").Find("Count").GetComponent<Text>().text = 'x' + skillDebris.ToString();
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

            GameObject gBuf = transform.parent.parent.parent.parent.Find("LottertyMessage").Find("Board").Find("Gift").gameObject;

            gBuf.transform.Find("Gift1").Find("Quality").GetComponent<Image>().sprite = GameScript.QualityRare;
            gBuf.transform.Find("Gift1").Find("Icon").GetComponent<Image>().sprite = GameScript.GetSprite(RareDebrisPool[indexNum].GetImagePath());
            gBuf.transform.Find("Gift1").Find("Count").GetComponent<Text>().text = 'x' + equipmentDebris.ToString();

            gBuf.transform.Find("Gift2").Find("Quality").GetComponent<Image>().sprite = GameScript.QualityExcellent;
            gBuf.transform.Find("Gift2").Find("Icon").GetComponent<Image>().sprite = GameScript.RareEarthSprite;
            gBuf.transform.Find("Gift2").Find("Count").GetComponent<Text>().text = 'x' + rareEarth.ToString();

            gBuf.transform.Find("Gift3").Find("Quality").GetComponent<Image>().sprite = GameScript.QualityExcellent;
            gBuf.transform.Find("Gift3").Find("Icon").GetComponent<Image>().sprite = GameScript.SkillDebrisSprite;
            gBuf.transform.Find("Gift3").Find("Count").GetComponent<Text>().text = 'x' + skillDebris.ToString();
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

            GameObject gBuf = transform.parent.parent.parent.parent.Find("LottertyMessage").Find("Board").Find("Gift").gameObject;

            gBuf.transform.Find("Gift1").Find("Quality").GetComponent<Image>().sprite = GameScript.QualityExcellent;
            gBuf.transform.Find("Gift1").Find("Icon").GetComponent<Image>().sprite = GameScript.GetSprite(ExcellentDebrisPool[indexNum].GetImagePath());
            gBuf.transform.Find("Gift1").Find("Count").GetComponent<Text>().text = 'x' + equipmentDebris.ToString();

            gBuf.transform.Find("Gift2").Find("Quality").GetComponent<Image>().sprite = GameScript.QualityExcellent;
            gBuf.transform.Find("Gift2").Find("Icon").GetComponent<Image>().sprite = GameScript.RareEarthSprite;
            gBuf.transform.Find("Gift2").Find("Count").GetComponent<Text>().text = 'x' + rareEarth.ToString();

            gBuf.transform.Find("Gift3").Find("Quality").GetComponent<Image>().sprite = GameScript.QualityExcellent;
            gBuf.transform.Find("Gift3").Find("Icon").GetComponent<Image>().sprite = GameScript.SkillDebrisSprite;
            gBuf.transform.Find("Gift3").Find("Count").GetComponent<Text>().text = 'x' + skillDebris.ToString();
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

            GameObject gBuf = transform.parent.parent.parent.parent.Find("LottertyMessage").Find("Board").Find("Gift").gameObject;

            gBuf.transform.Find("Gift1").Find("Quality").GetComponent<Image>().sprite = GameScript.QualityNormal;
            gBuf.transform.Find("Gift1").Find("Icon").GetComponent<Image>().sprite = GameScript.GetSprite(NormalDebrisPool[indexNum].GetImagePath());
            gBuf.transform.Find("Gift1").Find("Count").GetComponent<Text>().text = 'x' + equipmentDebris.ToString();

            gBuf.transform.Find("Gift2").Find("Quality").GetComponent<Image>().sprite = GameScript.QualityExcellent;
            gBuf.transform.Find("Gift2").Find("Icon").GetComponent<Image>().sprite = GameScript.RareEarthSprite;
            gBuf.transform.Find("Gift2").Find("Count").GetComponent<Text>().text = 'x' + rareEarth.ToString();

            gBuf.transform.Find("Gift3").Find("Quality").GetComponent<Image>().sprite = GameScript.QualityExcellent;
            gBuf.transform.Find("Gift3").Find("Icon").GetComponent<Image>().sprite = GameScript.SkillDebrisSprite;
            gBuf.transform.Find("Gift3").Find("Count").GetComponent<Text>().text = 'x' + skillDebris.ToString();
        }
        LotteryMessageButtonScript.HadLotteryMessage = true;
    }
}
