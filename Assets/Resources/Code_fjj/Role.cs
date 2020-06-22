using System;
using System.Collections.Generic;

/// <summary>
/// In Role.cs d
/// </summary>
public struct LevelIndex
{
    public int Level;
    public int ExpLimit;
    public Attribute attribute;

    public static int FindExpLimit(LevelIndex[] index, int level)
    {
        for (int i = 0; i < index.Length; i++)
        {
            if (index[i].Level == level)
            {
                return index[i].ExpLimit;
            }
        }
        return index[0].ExpLimit;
    }
    public static Attribute FindAttribute(LevelIndex[] index, int level)
    {
        for (int i = 0; i < index.Length; i++)
        {
            if (index[i].Level == level)
            {
                return index[i].attribute;
            }
        }
        return index[0].attribute;
    }
}

/// <summary>
/// In Role.cs
/// </summary>
public class RoleAttribute
{
    public RoleAttribute(string name, int level, int exp, int expLimit, Attribute atr)
    {
        Name = name;
        attribute = atr;
        Level = level;
        Exp = exp;
        ExpLimit = expLimit;
    }

    public string GetName()
    {
        return Name;
    }
    public int GetAttack()
    {
        return attribute.Attack;
    }
    public int GetHealthPointLimit()
    {
        return attribute.HealthPointLimit;
    }
    public int GetDefence()
    {
        return attribute.Defence;
    }
    public Attribute GetAttribute()
    {
        return attribute;
    }
    public int GetLevel()
    {
        return Level;
    }
    public int GetExp()
    {
        return Exp;
    }
    public int GetExpLimit()
    {
        return ExpLimit;
    }

    public void ExpUP(int exp)
    {
        Exp += exp;
        LevelUp();
    }
    public bool LevelUp()
    {
        if (Exp < ExpLimit)
        {
            return false;
        }
        if (Level == DataManager.levelIndex.Length)
        {
            return false;
        }
        Exp = Exp - ExpLimit;
        Level++;
        ExpLimit = DataManager.levelIndex[Level - 1].ExpLimit;
        attribute = DataManager.levelIndex[Level - 1].attribute;
        return true;
    }

    public Attribute GetRoleAttribute(Attribute equipmentAttribute, Attribute skillAttribute)
    {
        Attribute a = new Attribute();
        a = attribute + equipmentAttribute + skillAttribute;
        return a;
    }

    public void SetName(string name)
    {
        Name = name;
    }
    public void SetAttack(int atk)
    {
        attribute.Attack = atk;
    }
    public void SetHealthPointLimit(int hp)
    {
        attribute.HealthPointLimit = hp;
    }
    public void SetDefence(int def)
    {
        attribute.Defence = def;
    }
    public void SetAttribute(Attribute a)
    {
        attribute = a;
    }
    public void SetLevel(int level)
    {
        Level = level;
    }
    public void SetExp(int exp)
    {
        Exp = exp;
    }

    private string Name;
    private Attribute attribute;
    private int Level;
    private int Exp;
    private int ExpLimit;
}

/// <summary>
/// In Role.cs
/// </summary>
public class RoleEquipment
{
    public RoleEquipment(BagItem[] equipmentList, bool[] equipmentListBool, int slayCount, int rareEarthCount, int skillDebris)
    {
        for (int i = 0; i < equipmentList.Length; i++)
        {
            EquipmentList[i] = equipmentList[i];
            EquipmentListBool[i] = equipmentListBool[i];
        }
        SlayCount = slayCount;
        RareEarthCount = rareEarthCount;
        SkillDebris = skillDebris;
    }

    public ReserveAttribute GetEquipmentListReserveAttribute()
    {
        ReserveAttribute Buf = ReserveAttribute.StandardReserveAttribute();
        for (int i = 0; i < 4; i++)
        {
            if (EquipmentListBool[i])
            {
                Buf = Buf + EquipmentList[i].GetEquipmentReserveAttribute();
            }
        }
        return Buf;
    }

    public bool MainWeaponLevelUp()
    {
        if (HadMainWeapon())
        {
            return EquipmentList[0].LevelUP();
        }
        else
        {
            return false;
        }
    }
    public bool AlternateWeaponLevelUp()
    {
        if (HadAlternateWeapon())
        {
            return EquipmentList[1].LevelUP();
        }
        else
        {
            return false;
        }
    }
    public bool CuirassLevelUp()
    {
        if (HadCuirass())
        {
            return EquipmentList[2].LevelUP();
        }
        else
        {
            return false;
        }
    }
    public bool HelmLevelUp()
    {
        if (HadHelm())
        {
            return EquipmentList[3].LevelUP();
        }
        else
        {
            return false;
        }
    }

    public bool SetMainWeapon(BagItem mainWeapon)
    {
        if (mainWeapon.item.GetEquipmentType() == EquipmentType.MainWeapon)
        {
            EquipmentList[0] = mainWeapon;
            EquipmentListBool[0] = true;
            return true;
        }
        return false;
    }
    public void SetMainWeaponNull()
    {
        EquipmentListBool[0] = false;
    }
    public bool SetAlternateWeapon(BagItem alternateWeapon)
    {
        if (alternateWeapon.item.GetEquipmentType() == EquipmentType.AlternateWeapon)
        {
            EquipmentList[1] = alternateWeapon;
            EquipmentListBool[1] = true;
            return true;
        }
        return false;
    }
    public void SetAlternateWeaponNull()
    {
        EquipmentListBool[1] = false;
    }
    public bool SetCuirass(BagItem cuirass)
    {
        if (cuirass.item.GetEquipmentType() == EquipmentType.Cuirass)
        {
            EquipmentList[2] = cuirass;
            EquipmentListBool[2] = true;
            return true;
        }
        return false;
    }
    public void SetCuirassNull()
    {
        EquipmentListBool[2] = false;
    }
    public bool SetHelm(BagItem helm)
    {
        if (helm.item.GetEquipmentType() == EquipmentType.Helm)
        {
            EquipmentList[3] = helm;
            EquipmentListBool[3] = true;
            return true;
        }
        return false;
    }
    public void SetHelmNull()
    {
        EquipmentListBool[3] = false;
    }
    public void SetSlayCount(int slayCount)
    {
        SlayCount = slayCount;
    }
    public void SetRareEarthCount(int rareEarthCount)
    {
        RareEarthCount = rareEarthCount;
    }
    public void SetSkillDebris(int skillDeberis)
    {
        SkillDebris = skillDeberis;
    }

    public bool HadMainWeapon()
    {
        return EquipmentListBool[0];
    }
    public bool HadAlternateWeapon()
    {
        return EquipmentListBool[1];
    }
    public bool HadCuirass()
    {
        return EquipmentListBool[2];
    }
    public bool HadHelm()
    {
        return EquipmentListBool[3];
    }
    public BagItem GetMainWeapon()
    {
        return EquipmentList[0];
    }
    public BagItem GetAlternateWeapon()
    {
        return EquipmentList[1];
    }
    public BagItem GetCuirass()
    {
        return EquipmentList[2];
    }
    public BagItem GetHelm()
    {
        return EquipmentList[3];
    }
    public int GetSlayCount()
    {
        return SlayCount;
    }
    public int GetRareEarthCount()
    {
        return RareEarthCount;
    }
    public int GetSkillDebris()
    {
        return SkillDebris;
    }

    private BagItem[] EquipmentList = new BagItem[4];
    private bool[] EquipmentListBool = new bool[4];
    private int SlayCount;
    private int RareEarthCount;
    private int SkillDebris;
}

public struct SkillLevelIndex
{
    public string name;
    public int level;
    public ReserveAttribute reserveAttribute;
    public double coolDown;
    public double duration;
}
public struct SkillDataPlus
{
    public ReserveAttribute reserveAttribute;
    public double coolDown;
    public double duration;
}
public struct SkillLevelUpIndex
{
    public string Name;
    public int LearnCount;
    public int UpCount;
}
/// <summary>
/// In Role.cs
/// </summary>
public class Skill
{
    public Skill(string name, string message, int level, int levelLimit, ReserveAttribute[] attributeList, double[] coolDwonList, double[] durationList, string imagePath)
    {
        Name = name;
        Message = message;
        Level = level;
        LevelLimit = levelLimit;
        ReserveAttributeList = attributeList;
        if (Level != 0)
        {
            SkillReserveAttribute = ReserveAttributeList[Level - 1];
        }
        else
        {
            SkillReserveAttribute = ReserveAttribute.StandardReserveAttribute();
        }
        CoolDownList = coolDwonList;
        DurationList = durationList;
        ImagePath = imagePath;
    }

    public bool LevelUP()
    {
        if (Level < LevelLimit)
        {
            Level++;
            SkillReserveAttribute = ReserveAttributeList[Level - 1];
            SkillCoolDown = CoolDownList[Level - 1];
            SkillDuration = DurationList[Level - 1];
            return true;
        }
        return false;
    }
    public bool LevelDown()
    {
        if (Level > 0)
        {
            Level--;
            if (Level == 0)
            {
                SkillReserveAttribute = ReserveAttribute.StandardReserveAttribute();
                SkillCoolDown = 0;
                SkillDuration = 0;
            }
            else
            {
                SkillReserveAttribute = ReserveAttributeList[Level - 1];
                SkillCoolDown = CoolDownList[Level - 1];
                SkillDuration = DurationList[Level - 1];
            }
            return true;
        }
        return false;
    }
    public void LevelClear()
    {
        SkillReserveAttribute = ReserveAttribute.StandardReserveAttribute();
        SkillCoolDown = 0;
        SkillDuration = 0;
        Level = 0;
    }
    public static ReserveAttribute GetSkillListAttributer(Skill[] skillList)
    {
        ReserveAttribute a = ReserveAttribute.StandardReserveAttribute();
        for (int i = 0; i < skillList.Length; i++)
        {
            a = a + skillList[i].GetAttibute();
        }
        return a;
    }

    public string GetName()
    {
        return Name;
    }
    public string GetMessage()
    {
        return Message;
    }
    public ReserveAttribute GetAttibute()
    {
        return SkillReserveAttribute;
    }
    public double GetCoolDown()
    {
        return SkillCoolDown;
    }
    public double GetDuration()
    {
        return SkillDuration;
    }
    public int GetLevel()
    {
        return Level;
    }
    public int GetLevelLimit()
    {
        return LevelLimit;
    }
    public ReserveAttribute GetNextAttribute()
    {
        if (Level < LevelLimit)
        {
            return ReserveAttributeList[Level];
        }
        else
        {
            return ReserveAttributeList[Level - 1];
        }
    }
    public double GetNextDuration()
    {
        if (Level < LevelLimit)
        {
            return DurationList[Level];
        }
        else
        {
            return DurationList[Level - 1];
        }
    }
    public string GetImagePath()
    {
        return ImagePath;
    }

    private string Name;
    private string Message;
    private ReserveAttribute SkillReserveAttribute;
    private double SkillCoolDown;
    private double SkillDuration;
    private int Level;
    private int LevelLimit;
    private ReserveAttribute[] ReserveAttributeList;
    private double[] CoolDownList;
    private double[] DurationList;
    private string ImagePath;
}
public struct SkillLearn
{
    public Skill SkillList;
    public int Count;
}

public struct BagItem
{
    public Item item;
    public int level;
    public int count;

    public bool LevelUP()
    {
        if (level < 20)
        {
            level++;
            return true;
        }
        return false;
    }

    public int CountChange(int change)
    {
        count += change;
        if (count < 0)
        {
            count = 0;
        }
        if (count > 999)
        {
            count = 999;
        }
        return count;
    }

    public int GetIntensifyDebris()
    {
        return (2 * level + 3);
    }

    public int GetIntensifyRareEarth()
    {
        int q;
        switch (item.GetQuality())
        {
            case Quality.Normal:
                q = 1;
                break;
            case Quality.Excellent:
                q = 2;
                break;
            case Quality.Rare:
                q = 3;
                break;
            case Quality.Epic:
                q = 4;
                break;
            default:
                q = 1;
                break;
        }
        return (level * (q + 2) + 5);
    }

    public int GetResolveRareEarth()
    {
        switch (item.GetQuality())
        {
            case Quality.Normal:
                return 50;
            case Quality.Excellent:
                return 100;
            case Quality.Rare:
                return 150;
            default:
                return 50;
        }
    }

    public ReserveAttribute GetEquipmentReserveAttribute()
    {
        ReserveAttribute Buf = new ReserveAttribute();
        Buf.AddATK = item.GetReserveAttribute().AddATK;
        Buf.PlusATK = item.GetReserveAttribute().PlusATK;
        Buf.AddHP = item.GetReserveAttribute().AddHP;
        Buf.PlusHP = item.GetReserveAttribute().PlusHP;
        Buf.AddDEF = item.GetReserveAttribute().AddDEF;
        Buf.PlusDEF = item.GetReserveAttribute().PlusDEF;
        for (int i = 0; i < level; i++)
        {
            Buf = Buf * 1.1;
        }
        return Buf;
    }
}
/// <summary>
/// In Role.cs
/// </summary>
public class Bag
{
    public Bag(List<BagItem> itemBag)
    {
        ItemBag = itemBag;
    }

    public int FindBagItem(BagItem b)
    {
        for (int i = 0; i < ItemBag.Count; i++)
        {
            if (b.item.GetID() == ItemBag[i].item.GetID() && b.level == ItemBag[i].level)
            {
                return i;
            }
        }
        return -1;
    }
    public void BagItemLevelUp(int i)
    {
        BagItem Buf = new BagItem();
        Buf.item = new Item();
        Buf.item = ItemBag[i].item;
        Buf.level = ItemBag[i].level;
        Buf.count = ItemBag[i].count;
        Buf.LevelUP();
        ItemBag[i] = Buf;
    }
    public void ChangeBagItem(BagItem b, int index)
    {
        ItemBag[index] = b;
    }
    public void AddItem(Item newItem, int itemCount = 1)
    {
        for (int i = 0; i < ItemBag.Count; i++)
        {
            if (newItem.GetID() == ItemBag[i].item.GetID())
            {
                if (ItemBag[i].item.GetEquipmentOrNot())
                {
                    for (int j = 0; j < itemCount; j++)
                    {
                        BagItem addBuf = new BagItem();
                        addBuf.item = new Item();
                        addBuf.item.FindItem(DataManager.GameItemIndex, newItem.GetID());
                        addBuf.level = 0;
                        addBuf.count = 1;
                        ItemBag.Add(addBuf);
                    }
                }
                else
                {
                    BagItem addBuf = new BagItem();
                    addBuf.item = new Item();
                    addBuf.item = ItemBag[i].item;
                    addBuf.level = ItemBag[i].level;
                    addBuf.count = ItemBag[i].CountChange(itemCount);
                    ItemBag[i] = addBuf;
                }
                return;
            }
        }
        BagItem Buf = new BagItem();
        Buf.item = new Item();
        Buf.item.FindItem(DataManager.GameItemIndex, newItem.GetID());
        Buf.level = 0;
        Buf.count = itemCount;
        ItemBag.Add(Buf);
        //ItemSort();
    }
    public void AddBagItem(BagItem newBagItem)
    {
        for (int i = 0; i < ItemBag.Count; i++)
        {
            if (newBagItem.item.GetID() == ItemBag[i].item.GetID())
            {
                if (ItemBag[i].item.GetEquipmentOrNot())
                {
                    ItemBag.Add(newBagItem);
                }
                else
                {
                    BagItem addBuf = new BagItem();
                    addBuf.item = new Item();
                    addBuf.item = ItemBag[i].item;
                    addBuf.level = ItemBag[i].level;
                    addBuf.count = ItemBag[i].CountChange(newBagItem.count);
                    ItemBag[i] = addBuf;
                }
                return;
            }
        }
        ItemBag.Add(newBagItem);
    }
    public bool ReduceItem(BagItem cutItem, int itemCount = 1)
    {
        for (int i = 0; i < ItemBag.Count; i++)
        {
            if (cutItem.item.GetID() == ItemBag[i].item.GetID())
            {
                if (ItemBag[i].item.GetEquipmentOrNot())
                {
                    if (cutItem.level == ItemBag[i].level)
                    {
                        ItemBag.RemoveAt(i);
                        return true;
                    }
                }
                else
                {
                    if (ItemBag[i].count >= itemCount)
                    {
                        ItemBag[i] = new BagItem { item = ItemBag[i].item, level = ItemBag[i].level, count = ItemBag[i].count - itemCount };
                        if (ItemBag[i].count == 0)
                        {
                            ItemBag.RemoveAt(i);
                        }
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public void ItemSort()
    {
        if (ItemBag.Count > 1)
        {
            for (int i = 0; i < ItemBag.Count - 1; i++)
            {
                for (int j = 0; j < ItemBag.Count - 1 - i; j++)
                {
                    if (ItemBag[j].item.GetID() > ItemBag[j + 1].item.GetID())
                    {
                        BagItem Buf = new BagItem { item = ItemBag[j].item, level = ItemBag[i].level, count = ItemBag[j].count };
                        ItemBag[j] = ItemBag[j + 1];
                        ItemBag[j + 1] = Buf;
                    }
                }
            }
        }
    }

    public List<BagItem> GetItemBag()
    {
        return ItemBag;
    }

    private List<BagItem> ItemBag;
}