using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    public static DataManager dataManager;
    public static Attribute GameRoleAttribute;
    public static DateTime NormalPool;
    public static DateTime GoodPool;

    public static List<Chip> LevelOutput;
    private static Item[] NormalDebrisPool;
    private static Item[] ExcellentDebrisPool;
    private static Item[] RareDebrisPool;
    private static Item[] EpicDebrisPool;

    private void PoolInit()
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

    public static void LevelPass(int level)
    {
        if (level >= LevelOutput.Count)
        {
            return;
        }
        System.Random rA = new System.Random();
        switch (LevelOutput[level].GetChipQuality())
        {
            case Quality.Normal:
                DataManager.bag.AddItem(NormalDebrisPool[rA.Next()%4], LevelOutput[level].GetChipNum());
                break;
            case Quality.Excellent:
                DataManager.bag.AddItem(ExcellentDebrisPool[rA.Next() % 4], LevelOutput[level].GetChipNum());
                break;
            case Quality.Rare:
                DataManager.bag.AddItem(RareDebrisPool[rA.Next() % 4], LevelOutput[level].GetChipNum());
                break;
            case Quality.Epic:
                DataManager.bag.AddItem(EpicDebrisPool[rA.Next() % 4], LevelOutput[level].GetChipNum());
                break;
        }
        DataManager.roleEquipment.SetRareEarthCount(DataManager.roleEquipment.GetRareEarthCount() + LevelOutput[level].GetRareEarthNum());
        DataManager.roleAttribute.ExpUP(LevelOutput[level].GetExp());
    }

    private Attribute attribute_Buf;
    private ReserveAttribute reserveAttribute_Buf;

    public static Sprite GetSprite(string path)
    {
        string texturePath;
        if (Application.platform == RuntimePlatform.Android)
        {
            texturePath = Application.persistentDataPath + '/' + path;
        }
        else
        {
            texturePath = "file://" + Application.persistentDataPath + '/' + path;
        }
        FileStream fs = new FileStream(texturePath, FileMode.Open, FileAccess.Read);
        fs.Seek(0, SeekOrigin.Begin);
        byte[] pBytes = new byte[fs.Length];
        fs.Read(pBytes, 0, (int)fs.Length);
        fs.Close();
        fs.Dispose();
        fs = null;
        Texture2D pBuf = new Texture2D(64, 64);
        pBuf.LoadImage(pBytes);
        return Sprite.Create(pBuf, new Rect(0, 0, pBuf.width, pBuf.height), Vector2.zero);
    }

    public static Sprite QualityNormal;
    public static Sprite QualityExcellent;
    public static Sprite QualityRare;
    public static Sprite QualityEpic;
    public static Sprite RareEarthSprite;
    public static Sprite SkillDebrisSprite;

    public static Sprite GetQualitySprite(Quality q)
    {
        switch (q)
        {
            case Quality.Normal:
                return QualityNormal;
            case Quality.Excellent:
                return QualityExcellent;
            case Quality.Rare:
                return QualityRare;
            case Quality.Epic:
                return QualityEpic;
            default:
                return QualityNormal;
        }
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

    public static Quality GetEquipmentQuality(string s)
    {
        switch (s)
        {
            case "普通":
                return Quality.Normal;
            case "精良":
                return Quality.Excellent;
            case "稀有":
                return Quality.Rare;
            case "史诗":
                return Quality.Epic;
        }
        return Quality.Normal;
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
                Item a = new Item(int.Parse(row[0]), row[1], row[2], equipmentOrNot, new ReserveAttribute { AddATK = int.Parse(row[4]), PlusATK = double.Parse(row[5]), AddHP = int.Parse(row[6]), PlusHP = double.Parse(row[7]), AddDEF = int.Parse(row[8]), PlusDEF = double.Parse(row[9]) }, eT, GetEquipmentQuality(row[10]), row[11]);
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
        NormalPool = Buf.Date;
        GoodPool = Buf.Date;
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
                    NormalPool = NormalPool.AddHours(double.Parse(row[1]));
                    NormalPool = NormalPool.AddMinutes(double.Parse(row[2]));
                    NormalPool = NormalPool.AddSeconds(double.Parse(row[3]));
                    continue;
                }
                if (row[0] == "高级")
                {
                    GoodPool = GoodPool.AddHours(double.Parse(row[1]));
                    GoodPool = GoodPool.AddMinutes(double.Parse(row[2]));
                    GoodPool = GoodPool.AddSeconds(double.Parse(row[3]));
                    continue;
                }
            }
        }
    }
    public void LevelOutputLoad()
    {
        using (CsvFileReader reader = new CsvFileReader(GetPersistentFilePath("Data/LevelOutput.csv")))
        {
            LevelOutput = new List<Chip>();
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
                        LevelOutput.Add(new Chip(int.Parse(row[0]), true, Quality.Normal, 0, int.Parse(row[4]), int.Parse(row[5]), GetEquipmentQuality(row[6]), int.Parse(row[7]), int.Parse(row[8])));
                    }
                    else
                    {
                        LevelOutput.Add(new Chip(int.Parse(row[0]), true, GetEquipmentQuality(row[2]), int.Parse(row[3]), int.Parse(row[4]), int.Parse(row[5]), GetEquipmentQuality(row[6]), int.Parse(row[7]), int.Parse(row[8])));
                    }
                }
                else
                {
                    if (row[2] == "")
                    {
                        LevelOutput.Add(new Chip(int.Parse(row[0]), false, Quality.Normal, 0, int.Parse(row[4]), int.Parse(row[5]), GetEquipmentQuality(row[6]), int.Parse(row[7]), int.Parse(row[8])));
                    }
                    else
                    {
                        LevelOutput.Add(new Chip(int.Parse(row[0]), false, GetEquipmentQuality(row[2]), int.Parse(row[3]), int.Parse(row[4]), int.Parse(row[5]), GetEquipmentQuality(row[6]), int.Parse(row[7]), int.Parse(row[8])));
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
        SkillDataLoad();
        RoleLoad();
        BagLoad();
        LottertyLoad();
        LevelOutputLoad();
    }

    public void SkillDataSave()
    {
        using (CsvFileWriter writer = new CsvFileWriter(GetPersistentFilePath("Data/SkillData.csv")))
        {
            CsvRow skillRow = new CsvRow();
            skillRow.Add("名称");
            skillRow.Add("介绍");
            skillRow.Add("等级");
            skillRow.Add("等级上限");
            skillRow.Add("图标路径");
            writer.WriteRow(skillRow);
            for (int i = 0; i < DataManager.SkillData.Length; i++)
            {
                CsvRow row = new CsvRow();
                row.Add(DataManager.SkillData[i].GetName());
                row.Add(DataManager.SkillData[i].GetMessage());
                row.Add(DataManager.SkillData[i].GetLevel().ToString());
                row.Add(DataManager.SkillData[i].GetLevelLimit().ToString());
                row.Add(DataManager.SkillData[i].GetImagePath());
                writer.WriteRow(row);
            }
        }
    }
    public void RoleSave()
    {
        using (CsvFileWriter writer = new CsvFileWriter(GetPersistentFilePath("Data/Role.csv")))
        {
            CsvRow roleRow = new CsvRow();
            roleRow.Add("角色名称");
            roleRow.Add("等级");
            roleRow.Add("经验");
            roleRow.Add("主武器ID");
            roleRow.Add("主武器等级");
            roleRow.Add("副武器ID");
            roleRow.Add("副武器等级");
            roleRow.Add("护甲ID");
            roleRow.Add("护甲等级");
            roleRow.Add("头盔ID");
            roleRow.Add("头盔等级");
            roleRow.Add("剩余必杀次数");
            roleRow.Add("稀土");
            roleRow.Add("技能碎片");
            writer.WriteRow(roleRow);
            CsvRow row = new CsvRow();
            row.Add(DataManager.roleAttribute.GetName());
            row.Add(DataManager.roleAttribute.GetLevel().ToString());
            row.Add(DataManager.roleAttribute.GetExp().ToString());
            if (DataManager.roleEquipment.HadMainWeapon())
            {
                row.Add(DataManager.roleEquipment.GetMainWeapon().item.GetID().ToString());
                row.Add(DataManager.roleEquipment.GetMainWeapon().level.ToString());
            }
            else
            {
                row.Add("");
                row.Add("");
            }
            if (DataManager.roleEquipment.HadAlternateWeapon())
            {
                row.Add(DataManager.roleEquipment.GetAlternateWeapon().item.GetID().ToString());
                row.Add(DataManager.roleEquipment.GetAlternateWeapon().level.ToString());
            }
            else
            {
                row.Add("");
                row.Add("");
            }
            if (DataManager.roleEquipment.HadCuirass())
            {
                row.Add(DataManager.roleEquipment.GetCuirass().item.GetID().ToString());
                row.Add(DataManager.roleEquipment.GetCuirass().level.ToString());
            }
            else
            {
                row.Add("");
                row.Add("");
            }
            if (DataManager.roleEquipment.HadHelm())
            {
                row.Add(DataManager.roleEquipment.GetHelm().item.GetID().ToString());
                row.Add(DataManager.roleEquipment.GetHelm().level.ToString());
            }
            else
            {
                row.Add("");
                row.Add("");
            }
            row.Add(DataManager.roleEquipment.GetSlayCount().ToString());
            row.Add(DataManager.roleEquipment.GetRareEarthCount().ToString());
            row.Add(DataManager.roleEquipment.GetSkillDebris().ToString());
            writer.WriteRow(row);
        }
    }
    public void BagSave()
    {
        using (CsvFileWriter writer = new CsvFileWriter(GetPersistentFilePath("Data/Bag.csv")))
        {
            CsvRow bagRow = new CsvRow();
            bagRow.Add("ID");
            bagRow.Add("等级");
            bagRow.Add("数量");
            writer.WriteRow(bagRow);
            for (int i = 0; i < DataManager.bag.GetItemBag().Count; i++)
            {
                CsvRow row = new CsvRow();
                row.Add(DataManager.bag.GetItemBag()[i].item.GetID().ToString());
                row.Add(DataManager.bag.GetItemBag()[i].level.ToString());
                row.Add(DataManager.bag.GetItemBag()[i].count.ToString());
                writer.WriteRow(row);
            }
        }
    }
    public void LottertySave()
    {
        using (CsvFileWriter writer = new CsvFileWriter(GetPersistentFilePath("Data/Lotterty.csv")))
        {
            CsvRow lottertyRow = new CsvRow();
            lottertyRow.Add("奖池");
            lottertyRow.Add("时");
            lottertyRow.Add("分");
            lottertyRow.Add("秒");
            writer.WriteRow(lottertyRow);
            CsvRow normalRow = new CsvRow();
            normalRow.Add("低级");
            normalRow.Add(NormalPool.TimeOfDay.Hours.ToString());
            normalRow.Add(NormalPool.TimeOfDay.Minutes.ToString());
            normalRow.Add(NormalPool.TimeOfDay.Seconds.ToString());
            writer.WriteRow(normalRow);
            CsvRow goodRow = new CsvRow();
            goodRow.Add("高级");
            goodRow.Add(GoodPool.TimeOfDay.Hours.ToString());
            goodRow.Add(GoodPool.TimeOfDay.Minutes.ToString());
            goodRow.Add(GoodPool.TimeOfDay.Seconds.ToString());
            writer.WriteRow(goodRow);
        }
    }
    public void LevelOutputSave()
    {
        using (CsvFileWriter writer = new CsvFileWriter(GetPersistentFilePath("Data/LevelOutput.csv")))
        {
            CsvRow levelOutputRow = new CsvRow();
            levelOutputRow.Add("关卡");
            levelOutputRow.Add("是否首通");
            levelOutputRow.Add("碎片");
            levelOutputRow.Add("碎片数量");
            levelOutputRow.Add("稀土数量");
            levelOutputRow.Add("经验值");
            levelOutputRow.Add("首通碎片");
            levelOutputRow.Add("首通碎片数量");
            levelOutputRow.Add("首通稀土数量");
            writer.WriteRow(levelOutputRow);
            for (int i = 0; i < LevelOutput.Count; i++)
            {
                CsvRow row = new CsvRow();
                row.Add(LevelOutput[i].GetSaveLevel());
                row.Add(LevelOutput[i].GetSaveFON());
                row.Add(LevelOutput[i].GetSaveChipType());
                row.Add(LevelOutput[i].GetSaveChipNum());
                row.Add(LevelOutput[i].GetSaveRareNum());
                row.Add(LevelOutput[i].GetSaveExp());
                row.Add(LevelOutput[i].GetSaveChipTypeF());
                row.Add(LevelOutput[i].GetSaveChipNumF());
                row.Add(LevelOutput[i].GetSaveRareNumF());
                writer.WriteRow(row);
            }
        }
    }

    public void GameSave()
    {
        SkillDataSave();
        RoleSave();
        BagSave();
        LottertySave();
        LevelOutputSave();
    }

    private void OnEnable()
    {
        dataManager = new DataManager();
        GameLoad();
        QualityNormal = GetSprite("UI/Normal.png");
        QualityExcellent = GetSprite("UI/Excellent.png");
        QualityRare = GetSprite("UI/Rare.png");
        QualityEpic = GetSprite("UI/Epic.png");
        RareEarthSprite = GetSprite("UI/RareEarth.png");
        SkillDebrisSprite = GetSprite("UI/SkillDebris.png");
        PoolInit();
        SelfUpdate();
    }

    private void Update()
    {
        Skill[] BuffSkillList = new Skill[1];
        BuffSkillList[0] = DataManager.SkillData[13];
        if (!ReserveAttribute.ReserveAttributeCompare(reserveAttribute_Buf, DataManager.roleEquipment.GetEquipmentListReserveAttribute() + Skill.GetSkillListAttributer(BuffSkillList)))
        {
            SelfUpdate();
            return;
        }
        if (!Attribute.AttributeCompare(attribute_Buf, DataManager.roleAttribute.GetAttribute()))
        {
            SelfUpdate();
            return;
        }
    }

    public void SelfUpdate()
    {
        reserveAttribute_Buf = DataManager.roleEquipment.GetEquipmentListReserveAttribute();
        Skill[] BuffSkillList = new Skill[1];
        BuffSkillList[0] = DataManager.SkillData[13];
        reserveAttribute_Buf = reserveAttribute_Buf + Skill.GetSkillListAttributer(BuffSkillList);
        attribute_Buf = DataManager.roleAttribute.GetAttribute();
        GameRoleAttribute = ReserveAttribute.ReserverToService(reserveAttribute_Buf, attribute_Buf);
    }

    private void OnDisable()
    {
        GameSave();
    }

    private void OnApplicationPause(bool pause)
    {
        GameSave();
    }
}
