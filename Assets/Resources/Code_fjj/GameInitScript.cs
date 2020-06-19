using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public class GameInitScript : MonoBehaviour
{
    private void Awake()
    {
        GameDataInit();
    }

    private void GameDataInit()
    {
        if (File.Exists(GetPersistentFilePath("Data/Role.csv")))
        {
            DataEnable();
        }
        else
        {
            Directory.CreateDirectory(GetPersistentFilePath("Data"));
            Directory.CreateDirectory(GetPersistentFilePath("UI"));
            StartCoroutine(DataMigration());
            Invoke("DataEnable", 1.2f);
        }
    }

    private IEnumerator DataMigration()
    {
        string filePath = GetStreamFilePath("MigrationList.txt");
        UnityWebRequest file = UnityWebRequest.Get(filePath);
        yield return file.SendWebRequest();
        if (file.isDone)
        {
            byte[] array = Encoding.UTF8.GetBytes(file.downloadHandler.text);
            MemoryStream sBuf = new MemoryStream(array);
            StreamReader reader = new StreamReader(sBuf);
            string Buf;
            Buf = reader.ReadLine();
            while (Buf != null)
            {
                StartCoroutine(DataMigrationHelp(Buf));
                Buf = reader.ReadLine();
            }
        }
    }
    private IEnumerator DataMigrationHelp(string path)
    {
        string filePath = GetStreamFilePath(path);
        UnityWebRequest file = UnityWebRequest.Get(filePath);
        yield return file.SendWebRequest();
        string savePath = GetPersistentFilePath(path);
        FileInfo t = new FileInfo(savePath);
        FileStream fs = t.Create();
        fs.Close();
        fs.Dispose();
        if (file.isDone)
        {
            File.WriteAllBytes(savePath, file.downloadHandler.data);
        }
    }

    private void DataEnable()
    {
        GetComponent<Canvas>().enabled = false;
        if (!GameScript.IfInit)
        {
            GameLoad();
        }
        transform.parent.Find("GameData").gameObject.SetActive(true);
        transform.parent.Find("MainUI").gameObject.SetActive(true);
    }

    private string GetStreamFilePath(string FileName)
    {
        string filePath;
        if (Application.platform == RuntimePlatform.Android)
        {
            filePath = Application.streamingAssetsPath + '/' + FileName;
        }
        else
        {
            filePath = "file://" + Application.streamingAssetsPath + '/' + FileName;
        }
        return filePath;
    }
    private string GetPersistentFilePath(string FileName)
    {
        string filePath;
        if (Application.platform == RuntimePlatform.Android)
        {
            filePath = Application.persistentDataPath + '/' + FileName;
        }
        else
        {
            filePath = "file://" + Application.persistentDataPath + '/' + FileName;
        }
        return filePath;
    }

    public void GameItemIndexLoad()
    {
        using (CsvFileReader reader = new CsvFileReader(GetPersistentFilePath("Data/GameItemIndex.csv")))
        {
            List<Item> buf = new List<Item>();
            CsvRow row = new CsvRow();
            bool first = true;
            while (reader.ReadRow(row))
            {
                if (first)
                {
                    first = false;
                    continue;
                }
                if (row[0] == "")
                {
                    break;
                }
                bool equipmentOrNot;
                if (row[3] == "装备碎片")
                {
                    equipmentOrNot = false;
                }
                else
                {
                    equipmentOrNot = true;
                }
                EquipmentType eT = EquipmentType.MainWeapon;
                switch (row[3])
                {
                    case "主武器":
                        eT = EquipmentType.MainWeapon;
                        break;
                    case "副武器":
                        eT = EquipmentType.AlternateWeapon;
                        break;
                    case "护甲":
                        eT = EquipmentType.Cuirass;
                        break;
                    case "头盔":
                        eT = EquipmentType.Helm;
                        break;
                }
                Item a = new Item(int.Parse(row[0]), row[1], row[2], equipmentOrNot, new ReserveAttribute { AddATK = int.Parse(row[4]), PlusATK = double.Parse(row[5]), AddHP = int.Parse(row[6]), PlusHP = double.Parse(row[7]), AddDEF = int.Parse(row[8]), PlusDEF = double.Parse(row[9]) }, eT, GameScript.GetEquipmentQuality(row[10]), row[11]);
                buf.Add(a);
            }
            DataManager.GameItemIndex = new Item[buf.Count];
            for (int i = 0; i < DataManager.GameItemIndex.Length; i++)
            {
                DataManager.GameItemIndex[i] = buf[i];
            }
        }
    }
    public void LevelIndexLoad()
    {
        using (CsvFileReader reader = new CsvFileReader(GetPersistentFilePath("Data/LevelIndex.csv")))
        {
            List<LevelIndex> buf = new List<LevelIndex>();
            CsvRow row = new CsvRow();
            bool first = true;
            while (reader.ReadRow(row))
            {
                if (first)
                {
                    first = false;
                    continue;
                }
                if (row[0] == "")
                {
                    break;
                }
                LevelIndex a = new LevelIndex { Level = int.Parse(row[0]), ExpLimit = int.Parse(row[1]), attribute = new Attribute { Attack = int.Parse(row[2]), HealthPointLimit = int.Parse(row[3]), Defence = int.Parse(row[4]) } };
                buf.Add(a);
            }
            DataManager.levelIndex = new LevelIndex[buf.Count];
            for (int i = 0; i < DataManager.levelIndex.Length; i++)
            {
                DataManager.levelIndex[i] = buf[i];
            }
        }
    }
    public void SkillLevelIndexLoad()
    {
        using (CsvFileReader reader = new CsvFileReader(GetPersistentFilePath("Data/SkillLevelIndex.csv")))
        {
            List<SkillLevelIndex> buf = new List<SkillLevelIndex>();
            CsvRow row = new CsvRow();
            bool first = true;
            while (reader.ReadRow(row))
            {
                if (first)
                {
                    first = false;
                    continue;
                }
                if (row[0] == "")
                {
                    break;
                }
                SkillLevelIndex a = new SkillLevelIndex { name = row[0], level = int.Parse(row[1]), reserveAttribute = new ReserveAttribute { AddATK = int.Parse(row[2]), PlusATK = double.Parse(row[3]), AddHP = int.Parse(row[4]), PlusHP = double.Parse(row[5]), AddDEF = int.Parse(row[6]), PlusDEF = double.Parse(row[7]) }, coolDown = double.Parse(row[8]), duration = double.Parse(row[9]) };
                buf.Add(a);
            }
            DataManager.skillLevelIndex = new SkillLevelIndex[buf.Count];
            for (int i = 0; i < DataManager.skillLevelIndex.Length; i++)
            {
                DataManager.skillLevelIndex[i] = buf[i];
            }
        }
    }
    public void SkillLevelUpIndexLoad()
    {
        using (CsvFileReader reader = new CsvFileReader(GetPersistentFilePath("Data/SkillLevelUpIndex.csv")))
        {
            List<SkillLevelUpIndex> buf = new List<SkillLevelUpIndex>();
            CsvRow row = new CsvRow();
            bool first = true;
            while (reader.ReadRow(row))
            {
                if (first)
                {
                    first = false;
                    continue;
                }
                if (row[0] == "")
                {
                    break;
                }
                SkillLevelUpIndex a = new SkillLevelUpIndex { Name = row[0], Count = int.Parse(row[1]) };
                buf.Add(a);
            }
            DataManager.skillLevelUpIndex = new SkillLevelUpIndex[buf.Count];
            for (int i = 0; i < DataManager.skillLevelUpIndex.Length; i++)
            {
                DataManager.skillLevelUpIndex[i] = buf[i];
            }
        }
    }

    public void SkillDataLoad()
    {
        using (CsvFileReader reader = new CsvFileReader(GetPersistentFilePath("Data/SkillData.csv")))
        {
            List<Skill> buf = new List<Skill>();
            CsvRow row = new CsvRow();
            bool first = true;
            while (reader.ReadRow(row))
            {
                if (first)
                {
                    first = false;
                    continue;
                }
                if (row[0] == "")
                {
                    break;
                }
                List<SkillDataPlus> sdp_buf = new List<SkillDataPlus>();
                for (int i = 0; i < DataManager.skillLevelIndex.Length; i++)
                {
                    if (DataManager.skillLevelIndex[i].name == row[0])
                    {
                        sdp_buf.Add(new SkillDataPlus { reserveAttribute = DataManager.skillLevelIndex[i].reserveAttribute, coolDown = DataManager.skillLevelIndex[i].coolDown, duration = DataManager.skillLevelIndex[i].duration });
                    }
                }
                ReserveAttribute[] ra_Array = new ReserveAttribute[sdp_buf.Count];
                double[] cd_array = new double[sdp_buf.Count];
                double[] dr_array = new double[sdp_buf.Count];
                for (int i = 0; i < sdp_buf.Count; i++)
                {
                    ra_Array[i] = sdp_buf[i].reserveAttribute;
                    cd_array[i] = sdp_buf[i].coolDown;
                    dr_array[i] = sdp_buf[i].duration;
                }
                Skill a = new Skill(row[0], row[1], int.Parse(row[2]), int.Parse(row[3]), ra_Array, cd_array, dr_array, row[4]);
                buf.Add(a);
            }
            DataManager.SkillData = new Skill[buf.Count];
            for (int i = 0; i < DataManager.SkillData.Length; i++)
            {
                DataManager.SkillData[i] = buf[i];
            }
        }
    }
    public void RoleLoad()
    {
        using (CsvFileReader reader = new CsvFileReader(GetPersistentFilePath("Data/Role.csv")))
        {
            CsvRow row = new CsvRow();
            bool first = true;
            while (reader.ReadRow(row))
            {
                if (first)
                {
                    first = false;
                    continue;
                }
                if (row[0] == "")
                {
                    break;
                }
                DataManager.roleAttribute = new RoleAttribute(row[0], int.Parse(row[1]), int.Parse(row[2]), LevelIndex.FindExpLimit(DataManager.levelIndex, int.Parse(row[1])), LevelIndex.FindAttribute(DataManager.levelIndex, int.Parse(row[1])));
                BagItem[] e_buf = new BagItem[4];
                bool[] eb_buf = new bool[4];
                for (int i = 0; i < 8; i += 2)
                {
                    if (row[i + 3] == "")
                    {
                        e_buf[i / 2] = new BagItem();
                        eb_buf[i / 2] = false;
                        continue;
                    }
                    e_buf[i / 2].item = new Item();
                    e_buf[i / 2].item.FindItem(DataManager.GameItemIndex, int.Parse(row[i + 3]));
                    e_buf[i / 2].level = int.Parse(row[i + 4]);
                    e_buf[i / 2].count = 1;
                    eb_buf[i / 2] = true;
                }
                DataManager.roleEquipment = new RoleEquipment(e_buf, eb_buf, int.Parse(row[11]), int.Parse(row[12]), int.Parse(row[13]));
            }
        }
    }
    public void BagLoad()
    {
        using (CsvFileReader reader = new CsvFileReader(GetPersistentFilePath("Data/Bag.csv")))
        {
            List<BagItem> buf = new List<BagItem>();
            CsvRow row = new CsvRow();
            bool first = true;
            while (reader.ReadRow(row))
            {
                if (first)
                {
                    first = false;
                    continue;
                }
                if (row[0] == "")
                {
                    break;
                }
                BagItem addBuf = new BagItem();
                addBuf.item = new Item();
                addBuf.item.FindItem(DataManager.GameItemIndex, int.Parse(row[0]));
                addBuf.level = int.Parse(row[1]);
                addBuf.count = int.Parse(row[2]);
                buf.Add(addBuf);
            }
            DataManager.bag = new Bag(buf);
        }
    }

    public void LottertyLoad()
    {
        DateTime Buf = DateTime.Now;
        GameScript.NormalPool = Buf.Date;
        GameScript.GoodPool = Buf.Date;
        using (CsvFileReader reader = new CsvFileReader(GetPersistentFilePath("Data/Lotterty.csv")))
        {
            CsvRow row = new CsvRow();
            bool first = true;
            while (reader.ReadRow(row))
            {
                if (first)
                {
                    first = false;
                    continue;
                }
                if (row[0] == "")
                {
                    break;
                }
                if (row[0] == "低级")
                {
                    GameScript.NormalPool = GameScript.NormalPool.AddHours(double.Parse(row[1]));
                    GameScript.NormalPool = GameScript.NormalPool.AddMinutes(double.Parse(row[2]));
                    GameScript.NormalPool = GameScript.NormalPool.AddSeconds(double.Parse(row[3]));
                    continue;
                }
                if (row[0] == "高级")
                {
                    GameScript.GoodPool = GameScript.GoodPool.AddHours(double.Parse(row[1]));
                    GameScript.GoodPool = GameScript.GoodPool.AddMinutes(double.Parse(row[2]));
                    GameScript.GoodPool = GameScript.GoodPool.AddSeconds(double.Parse(row[3]));
                    continue;
                }
            }
        }
    }
    public void LevelOutputLoad()
    {
        using (CsvFileReader reader = new CsvFileReader(GetPersistentFilePath("Data/LevelOutput.csv")))
        {
            GameScript.LevelOutput = new List<Chip>();
            CsvRow row = new CsvRow();
            bool first = true;
            while (reader.ReadRow(row))
            {
                if (first)
                {
                    first = false;
                    continue;
                }
                if (row[0] == "")
                {
                    break;
                }
                if (row[1] == "是")
                {
                    if (row[2] == "")
                    {
                        GameScript.LevelOutput.Add(new Chip(int.Parse(row[0]), true, Quality.Normal, 0, int.Parse(row[4]), int.Parse(row[5]), GameScript.GetEquipmentQuality(row[6]), int.Parse(row[7]), int.Parse(row[8])));
                    }
                    else
                    {
                        GameScript.LevelOutput.Add(new Chip(int.Parse(row[0]), true, GameScript.GetEquipmentQuality(row[2]), int.Parse(row[3]), int.Parse(row[4]), int.Parse(row[5]), GameScript.GetEquipmentQuality(row[6]), int.Parse(row[7]), int.Parse(row[8])));
                    }
                }
                else
                {
                    if (row[2] == "")
                    {
                        GameScript.LevelOutput.Add(new Chip(int.Parse(row[0]), false, Quality.Normal, 0, int.Parse(row[4]), int.Parse(row[5]), GameScript.GetEquipmentQuality(row[6]), int.Parse(row[7]), int.Parse(row[8])));
                    }
                    else
                    {
                        GameScript.LevelOutput.Add(new Chip(int.Parse(row[0]), false, GameScript.GetEquipmentQuality(row[2]), int.Parse(row[3]), int.Parse(row[4]), int.Parse(row[5]), GameScript.GetEquipmentQuality(row[6]), int.Parse(row[7]), int.Parse(row[8])));
                    }
                }

            }
        }
    }

    public void GameLoad()
    {
        GameItemIndexLoad();
        LevelIndexLoad();
        SkillLevelIndexLoad();
        SkillLevelUpIndexLoad();
        LottertyLoad();
        SkillDataLoad();
        RoleLoad();
        BagLoad();
        LevelOutputLoad();

        GameScript.QualityNormal = GameScript.GetSprite("UI/Normal.png");
        GameScript.QualityExcellent = GameScript.GetSprite("UI/Excellent.png");
        GameScript.QualityRare = GameScript.GetSprite("UI/Rare.png");
        GameScript.QualityEpic = GameScript.GetSprite("UI/Epic.png");
        GameScript.RareEarthSprite = GameScript.GetSprite("UI/RareEarth.png");
        GameScript.SkillDebrisSprite = GameScript.GetSprite("UI/SkillDebris.png");
        PoolInit();
    }

    private void PoolInit()
    {
        GameScript.NormalDebrisPool = new Item[4];
        GameScript.ExcellentDebrisPool = new Item[4];
        GameScript.RareDebrisPool = new Item[4];
        GameScript.EpicDebrisPool = new Item[4];
        GameScript.NormalDebrisPool[0] = DataManager.GameItemIndex[1];
        GameScript.NormalDebrisPool[1] = DataManager.GameItemIndex[3];
        GameScript.NormalDebrisPool[2] = DataManager.GameItemIndex[17];
        GameScript.NormalDebrisPool[3] = DataManager.GameItemIndex[25];

        GameScript.ExcellentDebrisPool[0] = DataManager.GameItemIndex[5];
        GameScript.ExcellentDebrisPool[1] = DataManager.GameItemIndex[7];
        GameScript.ExcellentDebrisPool[2] = DataManager.GameItemIndex[19];
        GameScript.ExcellentDebrisPool[3] = DataManager.GameItemIndex[27];

        GameScript.RareDebrisPool[0] = DataManager.GameItemIndex[9];
        GameScript.RareDebrisPool[1] = DataManager.GameItemIndex[11];
        GameScript.RareDebrisPool[2] = DataManager.GameItemIndex[21];
        GameScript.RareDebrisPool[3] = DataManager.GameItemIndex[29];

        GameScript.EpicDebrisPool[0] = DataManager.GameItemIndex[13];
        GameScript.EpicDebrisPool[1] = DataManager.GameItemIndex[15];
        GameScript.EpicDebrisPool[2] = DataManager.GameItemIndex[23];
        GameScript.EpicDebrisPool[3] = DataManager.GameItemIndex[31];
    }

}
