using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

/// <summary>
/// Class to store one CSV row
/// </summary>
public class CsvRow : List<string>
{
    public string LineText { get; set; }
}

/// <summary>
/// Class to write data to a CSV file
/// </summary>
public class CsvFileWriter : StreamWriter
{
    // Override
    public CsvFileWriter(Stream stream)
        : base(stream)
    {
    }
    public CsvFileWriter(string filename)
        : base(filename)
    {
    }

    /// <summary>
    /// Writes a single row to a CSV file.
    /// </summary>
    /// <param name="row">The row to be written</param>
    public void WriteRow(CsvRow row)
    {
        StringBuilder builder = new StringBuilder();
        bool firstColumn = true;
        foreach (string value in row)
        {
            // Add separator if this isn't the first value
            if (!firstColumn)
                builder.Append(',');
            // Implement special handling for values that contain comma or quote
            // Enclose in quotes and double up any double quotes
            if (value.IndexOfAny(new char[] { '"', ',' }) != -1)
                builder.AppendFormat("\"{0}\"", value.Replace("\"", "\"\""));
            else
                builder.Append(value);
            firstColumn = false;
        }
        row.LineText = builder.ToString();
        WriteLine(row.LineText);
    }
}

/// <summary>
/// Class to read data from a CSV file
/// </summary>
public class CsvFileReader : StreamReader
{
    public CsvFileReader(Stream stream)
        : base(stream)
    {
    }

    public CsvFileReader(string filename)
        : base(filename)
    {
    }

    /// <summary>
    /// Reads a row of data from a CSV file
    /// </summary>
    /// <param name="row"></param>
    /// <returns></returns>
    public bool ReadRow(CsvRow row)
    {
        row.LineText = ReadLine();
        if (String.IsNullOrEmpty(row.LineText))
            return false;

        int pos = 0;
        int rows = 0;

        while (pos < row.LineText.Length)
        {
            string value;

            // Special handling for quoted field
            if (row.LineText[pos] == '"')
            {
                // Skip initial quote
                pos++;

                // Parse quoted value
                int start = pos;
                while (pos < row.LineText.Length)
                {
                    // Test for quote character
                    if (row.LineText[pos] == '"')
                    {
                        // Found one
                        pos++;

                        // If two quotes together, keep one
                        // Otherwise, indicates end of value
                        if (pos >= row.LineText.Length || row.LineText[pos] != '"')
                        {
                            pos--;
                            break;
                        }
                    }
                    pos++;
                }
                value = row.LineText.Substring(start, pos - start);
                value = value.Replace("\"\"", "\"");
            }
            else
            {
                // Parse unquoted value
                int start = pos;
                while (pos < row.LineText.Length && row.LineText[pos] != ',')
                    pos++;
                value = row.LineText.Substring(start, pos - start);
            }

            // Add field to list
            if (rows < row.Count)
                row[rows] = value;
            else
                row.Add(value);
            rows++;

            // Eat up to and including next comma
            while (pos < row.LineText.Length && row.LineText[pos] != ',')
                pos++;
            if (pos < row.LineText.Length)
                pos++;
        }
        // Delete any unused items
        while (row.Count > rows)
            row.RemoveAt(rows);

        // Return true if any columns read
        return (row.Count > 0);
    }
}

public class DataManager{
    //Index
    public static Item[] GameItemIndex;
    public static Equipment[] GameEquipmentIndex;
    public static LevelIndex[] levelIndex;
    public static SkillLevelIndex[] skillLevelIndex;
    public static SkillLevelUpIndex[] skillLevelUpIndex;
    public static EquipmentMakeCell[] EquipmentMakeList;
    private List<string> EquipmentData;

    //Use in unity
    public static RoleAttribute roleAttribute;
    public static RoleEquipment roleEquipment;
    public static Skill[] SkillData;
    public static SkillLearn skillLearn;
    public static Bag bag;
    public static EquipmentMake equipmentMake;

    /// <summary>
    /// Init when game start
    /// </summary>
    public void GamItemIndexLoad()
    {
        using (CsvFileReader reader = new CsvFileReader("Assets/Resources/Data/ItemIndex.csv"))
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
                Item a = new Item(int.Parse(row[0]), row[1], row[2], row[3]);
                buf.Add(a);
            }
            for (int i = 0; i < buf.Count; i++)
            {
                Debug.Log(buf[i].GetID().ToString() + " " + buf[i].GetName() + " " + buf[i].GetMessage() + " " + buf[i].GetImagePath() + '\n');
            }
            GameItemIndex = new Item[buf.Count];
            for (int i = 0; i < GameItemIndex.Length; i++)
            {
                GameItemIndex[i] = buf[i];
            }
        }
    }

    public static EquipmentType GetEquipmentType(string s)
    {
        switch (s)
        {
            case "MainWeapon":
                return EquipmentType.MainWeapon;
            case "AlternateWeapon":
                return EquipmentType.AlternateWeapon;
            case "Helm":
                return EquipmentType.Helm;
            case "Cuirass":
                return EquipmentType.Cuirass;
            case "Cuish":
                return EquipmentType.Cuish;
            case "Necklace":
                return EquipmentType.Necklace;
            case "Ring":
                return EquipmentType.Ring;
        }
        return EquipmentType.MainWeapon;
    }
    public static Quality GetEquipmentQuality(string s)
    {
        switch (s)
        {
            case "Normal":
                return Quality.Normal;
            case "Excellent":
                return Quality.Excellent;
            case "Rare":
                return Quality.Rare;
            case "Epic":
                return Quality.Epic;
        }
        return Quality.Normal;
    }
    public void GameEquipmentIndexLoad()
    {
        using (CsvFileReader reader = new CsvFileReader("Assets/Resources/Data/EquipmentIndex.csv"))
        {
            List<Equipment> buf = new List<Equipment>();
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
                Equipment a = new Equipment(int.Parse(row[0]), row[1], row[2], row[3], GetEquipmentType(row[4]), new ReserveAttribute { AddATK = int.Parse(row[5]), PlusATK = double.Parse(row[6]), AddHP = int.Parse(row[7]), PlusHP = double.Parse(row[8]), AddDEF = int.Parse(row[9]), PlusDEF = double.Parse(row[10]), AddCritValue = int.Parse(row[11]), PlusCritValue = double.Parse(row[12]), AddCritRatio = double.Parse(row[13]), PlusCritRatio = double.Parse(row[14]), AddLeech = int.Parse(row[15]), PlusLeech = double.Parse(row[16]), AddStun = int.Parse(row[17]), PlusStun = double.Parse(row[18]), AddCoolDownRatio = double.Parse(row[19]), PlusCoolDownRatio = double.Parse(row[20]) }, GetEquipmentQuality(row[21]) );
                buf.Add(a);
            }
            for (int i = 0; i < buf.Count; i++)
            {
                //Only show ATK
                Debug.Log(buf[i].GetID().ToString() + ' ' + buf[i].GetName() + ' ' + buf[i].GetEquipmentType() + ' ' + buf[i].GetImagePath() + ' ' + buf[i].GetMessage() + ' ' + buf[i].GetQuality() + ' ' + buf[i].GetAttribute().AddATK.ToString() + ' ' + buf[i].GetAttribute().PlusATK.ToString());
            }
            GameEquipmentIndex = new Equipment[buf.Count];
            for (int i = 0; i < GameEquipmentIndex.Length; i++)
            {
                GameEquipmentIndex[i] = buf[i];
            }
        }
    }

    public void LevelIndexLoad()
    {
        using (CsvFileReader reader = new CsvFileReader("Assets/Resources/Data/LevelIndex.csv"))
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
                LevelIndex a = new LevelIndex { Level = int.Parse(row[0]), ExpLimit = int.Parse(row[1]), attribute = new Attribute { Attack = int.Parse(row[2]), HealthPointLimit = int.Parse(row[3]), Defence = int.Parse(row[4]), CritValue = int.Parse(row[5]), CritRatio = double.Parse(row[6]), Leech = int.Parse(row[7]), Stun = int.Parse(row[8]), CoolDownRatio = double.Parse(row[9]) } };
                buf.Add(a);
                for (int i = 0; i < buf.Count; i++)
                {
                    //Only show ATK
                    Debug.Log(buf[i].Level.ToString() + ' ' + buf[i].ExpLimit.ToString() + ' ' + buf[i].attribute.Attack.ToString());
                }
                levelIndex = new LevelIndex[buf.Count];
                for (int i = 0; i < levelIndex.Length; i++)
                {
                    levelIndex[i] = buf[i];
                }
            }
        }
    }

    public void SkillLevelIndexLoad()
    {
        using (CsvFileReader reader = new CsvFileReader("Assets/Resources/Data/SkillLevelIndex.csv"))
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
                SkillLevelIndex a = new SkillLevelIndex { name = row[0], level = int.Parse(row[1]), reserveAttribute = new ReserveAttribute { AddATK = int.Parse(row[2]), PlusATK = double.Parse(row[3]), AddHP = int.Parse(row[4]), PlusHP = double.Parse(row[5]), AddDEF = int.Parse(row[6]), PlusDEF = double.Parse(row[7]), AddCritValue = int.Parse(row[8]), PlusCritValue = double.Parse(row[9]), AddCritRatio = double.Parse(row[10]), PlusCritRatio = double.Parse(row[11]), AddLeech = int.Parse(row[12]), PlusLeech = double.Parse(row[13]), AddStun = int.Parse(row[14]), PlusStun = double.Parse(row[15]), AddCoolDownRatio = double.Parse(row[16]), PlusCoolDownRatio = double.Parse(row[17]) } };
                buf.Add(a);
            }
            skillLevelIndex = new SkillLevelIndex[buf.Count];
            for (int i = 0; i < skillLevelIndex.Length; i++)
            {
                skillLevelIndex[i] = buf[i];
            }
        }
    }
    public void SkillLevelUpIndexLoad()
    {
        using (CsvFileReader reader = new CsvFileReader("Assets/Resources/Data/SkillLevelUpIndex.csv"))
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
                List<BagItem> bi_buf = new List<BagItem>();
                for (int i = 2; i < row.Count; i += 2)
                {
                    if (row[i] == "")
                    {
                        break;
                    }
                    bi_buf.Add(new BagItem { item = Item.FindItem(GameItemIndex, int.Parse(row[i])), count = int.Parse(row[i+1]) });
                }
                SkillLevelUpIndex a = new SkillLevelUpIndex { Name = row[0], ToLevel = int.Parse(row[1]), UpItemList = bi_buf };
                buf.Add(a);
            }
            skillLevelUpIndex = new SkillLevelUpIndex[buf.Count];
            for (int i = 0; i < skillLevelUpIndex.Length; i++)
            {
                skillLevelUpIndex[i] = buf[i];
            }
        }
    }

    public void EquipmentMakeListLoad()
    {
        using (CsvFileReader reader = new CsvFileReader("Assets/Resources/Data/EquipmentMakeIndex.csv"))
        {
            List<EquipmentMakeCell> buf = new List<EquipmentMakeCell>();
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
                List<MakeMaterial> mm_buf = new List<MakeMaterial>();
                for (int i = 2; i < row.Count; i += 2)
                {
                    if (row[i] == "")
                    {
                        break;
                    }
                    mm_buf.Add(new MakeMaterial { ItemID = int.Parse(row[i]), count = int.Parse(row[i+1]) });
                }
                EquipmentMakeCell a = new EquipmentMakeCell { EquipmentID = int.Parse(row[0]), makeMatrial = mm_buf };
                buf.Add(a);
            }
            int[] id_Array = new int[buf.Count];
            List<MakeMaterial>[] mmL_Array = new List<MakeMaterial>[buf.Count];
            for (int i = 0; i < buf.Count; i++)
            {
                id_Array[i] = buf[i].EquipmentID;
                mmL_Array[i] = buf[i].makeMatrial;
            }
            equipmentMake = new EquipmentMake(id_Array, mmL_Array);
        }
    }

    public void SkillDataLoad()
    {
        using (CsvFileReader reader = new CsvFileReader("Assets/Resources/Data/SkillData.csv"))
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
                List<ReserveAttribute> ra_buf = new List<ReserveAttribute>();
                for (int i = 0; i < skillLevelIndex.Length; i++)
                {
                    if (skillLevelIndex[i].name == row[0])
                    {
                        ra_buf.Add(skillLevelIndex[i].reserveAttribute);
                    }
                }
                ReserveAttribute[] ra_Array = new ReserveAttribute[ra_buf.Count];
                for (int i = 0; i < ra_Array.Length; i++)
                {
                    ra_Array[i] = ra_buf[i];
                }
                Skill a = new Skill(row[0],row[1],int.Parse(row[2]),int.Parse(row[3]), ra_Array, row[4]);
                buf.Add(a);
            }
            SkillData = new Skill[buf.Count];
            for (int i = 0; i < SkillData.Length; i++)
            {
                SkillData[i] = buf[i];
            }
        }
    }
    public void SkillDataSave()
    {
        using (CsvFileWriter writer = new CsvFileWriter("Assets/Resources/Data/SkillData.csv"))
        {
            CsvRow skillRow = new CsvRow();
            skillRow.Add("Name");
            skillRow.Add("Message");
            skillRow.Add("Level");
            skillRow.Add("LevelLimit");
            skillRow.Add("ImagPath");
            writer.WriteRow(skillRow);
            for (int i = 0; i < SkillData.Length; i++)
            {
                CsvRow row = new CsvRow();
                row.Add(SkillData[i].GetName());
                row.Add(SkillData[i].GetMessage());
                row.Add(SkillData[i].GetLevel().ToString());
                row.Add(SkillData[i].GetLevelLimit().ToString());
                row.Add(SkillData[i].GetImagePath());
                writer.WriteRow(row);
            }
        }
    }

    public void RoleLoad()
    {
        int g_buf = new int();
        using (CsvFileReader reader = new CsvFileReader("Assets/Resources/Data/Role.csv"))
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
                roleAttribute = new RoleAttribute(row[0], int.Parse(row[1]), int.Parse(row[2]), LevelIndex.FindExpLimit(levelIndex, int.Parse(row[1])), LevelIndex.FindAttribute(levelIndex, int.Parse(row[1])));
                Equipment[] e_buf = new Equipment[7];
                for (int i = 0; i < 7; i++)
                {
                    if (row[i+3] == "")
                    {
                        e_buf[i] = null;
                        continue;
                    }
                    e_buf[i] = (Equipment)Item.FindItem(GameEquipmentIndex, int.Parse(row[i+3]));
                }
                roleEquipment = new RoleEquipment(e_buf);
                g_buf = int.Parse(row[10]);
            }
        }
        List<BagItem> rbi_Buf = new List<BagItem>();
        using (CsvFileReader reader = new CsvFileReader("Assets/Resources/Data/ItemBag.csv"))
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
                buf.Add(new BagItem { item = Item.FindItem(GameItemIndex, int.Parse(row[0])), count = int.Parse(row[1]) });
            }
            rbi_Buf = buf; 
        }
        using (CsvFileReader reader = new CsvFileReader("Assets/Resources/Data/EquipmentBag.csv"))
        {
            List<BagEquipment> buf = new List<BagEquipment>();
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
                buf.Add(new BagEquipment { equipment = (Equipment)Item.FindItem(GameEquipmentIndex ,int.Parse(row[0])), ID = DateTime.Parse(row[1]) });
                bag = new Bag(g_buf, rbi_Buf, buf);
            }
        }
    }
    public void RoleSave()
    {
        using (CsvFileWriter writer = new CsvFileWriter("Assets/Resources/Data/Role.csv"))
        {
            CsvRow roleRow = new CsvRow();
            roleRow.Add("Name");
            roleRow.Add("Level");
            roleRow.Add("Exp");
            roleRow.Add("MainWeaponID");
            roleRow.Add("AlternateWeaponID");
            roleRow.Add("HelmID");
            roleRow.Add("CuirassID");
            roleRow.Add("CuishID");
            roleRow.Add("NecklaceID");
            roleRow.Add("RingID");
            roleRow.Add("Gold");
            writer.WriteRow(roleRow);
            for (int i = 0; i < 2; i++)
            {
                CsvRow row = new CsvRow();
                row.Add(roleAttribute.GetName());
                row.Add(roleAttribute.GetLevel().ToString());
                row.Add(roleAttribute.GetExp().ToString());
                Equipment[] buf = roleEquipment.GetEquipmentList();
                for(int j = 0; j < buf.Length; j++)
                {
                    row.Add(buf[j].GetID().ToString());
                }
                row.Add(bag.GetGold().ToString());
                writer.WriteRow(row);
            }
        }
        using (CsvFileWriter writer = new CsvFileWriter("Assets/Resources/Data/ItemBag.csv"))
        {
            CsvRow itemBagRow = new CsvRow();
            itemBagRow.Add("ItemID");
            itemBagRow.Add("Count");
            writer.WriteRow(itemBagRow);
            List<BagItem> buf = bag.GetItemBag();
            for (int i = 0; i < buf.Count; i++)
            {
                CsvRow row = new CsvRow();
                row.Add(buf[i].item.GetID().ToString());
                row.Add(buf[i].count.ToString());
                writer.WriteRow(row);
            }
        }
        using (CsvFileWriter writer = new CsvFileWriter("Assets/Resources/Data/EquipmentBag.csv"))
        {
            CsvRow equipmentBagRow = new CsvRow();
            equipmentBagRow.Add("EquipmentID");
            equipmentBagRow.Add("ID");
            List<BagEquipment> buf = bag.GetEquipmentBag();
            for (int i = 0; i < buf.Count; i++)
            {
                CsvRow row = new CsvRow();
                row.Add(buf[i].equipment.GetID().ToString());
                row.Add(buf[i].ID.ToString());
                writer.WriteRow(row);
            }
        }
    }

}