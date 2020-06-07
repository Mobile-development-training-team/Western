using System;
using System.Collections.Generic;

/// <summary>
/// In Role.cs
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
    public int GetCritValue()
    {
        return attribute.CritValue;
    }
    public double GetCritRatio()
    {
        return attribute.CritRatio;
    }
    public int GetLeech()
    {
        return attribute.Leech;
    }
    public int GetStun()
    {
        return attribute.Stun;
    }
    public double GetCoolDownRatio()
    {
        return attribute.CoolDownRatio;
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
    public void SetCritValue(int critV)
    {
        attribute.CritValue = critV;
    }
    public void SetCritRatio(double critR)
    {
        attribute.CritRatio = critR;
    }
    public void SetLeech(int leech)
    {
        attribute.Leech = leech;
    }
    public void SetStun(int stun)
    {
        attribute.Stun = stun;
    }
    public void SetCoolDownRatio(double cdr)
    {
        attribute.CoolDownRatio = cdr;
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
    public void SetExpLimit(int expLimit)
    {
        ExpLimit = expLimit;
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
    public RoleEquipment(Equipment[] roleEquipment)
    {
        for (int i = 0; i < roleEquipment.Length; i++)
        {
            if (roleEquipment[i] == null)
                continue;
            switch ((int)roleEquipment[i].GetEquipmentType())
            {
                case (int)EquipmentType.MainWeapon:
                    EquipmentList[0] = roleEquipment[i];
                    break;
                case (int)EquipmentType.AlternateWeapon:
                    EquipmentList[1] = roleEquipment[i];
                    break;
                case (int)EquipmentType.Helm:
                    EquipmentList[2] = roleEquipment[i];
                    break;
                case (int)EquipmentType.Cuirass:
                    EquipmentList[3] = roleEquipment[i];
                    break;
                case (int)EquipmentType.Cuish:
                    EquipmentList[4] = roleEquipment[i];
                    break;
                case (int)EquipmentType.Necklace:
                    EquipmentList[5] = roleEquipment[i];
                    break;
                case (int)EquipmentType.Ring:
                    EquipmentList[6] = roleEquipment[i];
                    break;
            }
        }
    }

    public bool SetMainWeapon(Equipment mainWeapon)
    {
        if (mainWeapon.GetEquipmentType() != EquipmentType.MainWeapon)
        {
            return false;
        }
        EquipmentList[0] = mainWeapon;
        return true;
    }
    public bool SetAlternate(Equipment alternateWeapon)
    {
        if (alternateWeapon.GetEquipmentType() != EquipmentType.AlternateWeapon)
        {
            return false;
        }
        EquipmentList[1] = alternateWeapon;
        return true;
    }
    public bool SetHelm(Equipment helm)
    {
        if (helm.GetEquipmentType() != EquipmentType.Helm)
        {
            return false;
        }
        EquipmentList[2] = helm;
        return true;
    }
    public bool SetCuirass(Equipment cuirass)
    {
        if (cuirass.GetEquipmentType() != EquipmentType.Cuirass)
        {
            return false;
        }
        EquipmentList[3] = cuirass;
        return true;
    }
    public bool SetCuish(Equipment cuish)
    {
        if (cuish.GetEquipmentType() != EquipmentType.Cuish)
        {
            return false;
        }
        EquipmentList[4] = cuish;
        return true;
    }
    public bool SetNecklace(Equipment necklace)
    {
        if (necklace.GetEquipmentType() != EquipmentType.Necklace)
        {
            return false;
        }
        EquipmentList[5] = necklace;
        return true;
    }
    public bool SetRing(Equipment ring)
    {
        if (ring.GetEquipmentType() != EquipmentType.Ring)
        {
            return false;
        }
        EquipmentList[6] = ring;
        return true;
    }

    public Equipment GetMainWeapon()
    {
        return EquipmentList[0];
    }
    public Equipment GetAlternateWeapon()
    {
        return EquipmentList[1];
    }
    public Equipment GetHelm()
    {
        return EquipmentList[2];
    }
    public Equipment GetCuirass()
    {
        return EquipmentList[3];
    }
    public Equipment GetCuish()
    {
        return EquipmentList[4];
    }
    public Equipment GetNecklace()
    {
        return EquipmentList[5];
    }
    public Equipment GetRing()
    {
        return EquipmentList[6];
    }
    public Equipment[] GetEquipmentList()
    {
        return EquipmentList;
    }

    private Equipment[] EquipmentList = new Equipment[7];
}

/// <summary>
/// In Role.cs
/// </summary>
public struct SkillLevelIndex
{
    public string name;
    public int level;
    public ReserveAttribute reserveAttribute;
}
/// <summary>
/// In Role.cs
/// </summary>
public struct SkillLevelUpIndex
{
    public string Name;
    public int ToLevel;
    public List<BagItem> UpItemList;
}
/// <summary>
/// In Role.cs
/// </summary>
public class Skill
{
    public Skill(string name, string message,int level, int levelLimit, ReserveAttribute[] attributeList, string imagePath)
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
        ImagePath = imagePath;
    }
    /*
    public void SetAttribute(Attribute attribute)
    {
        SkillAttribute = attribute;
    }
    public void SetLevel(int level)
    {
        Level = level;
    }
    public void SetLevelLimit(int levelLimit)
    {
        LevelLimit = levelLimit;
    }
    public void SetAttributeList(Attribute[] attributeList)
    {
        AttributeList = attributeList;
    }
    */
    public bool LevelUP()
    {
        if (Level < LevelLimit)
        {
            Level++;
            SkillReserveAttribute = ReserveAttributeList[Level - 1];
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
            }
            else
            {
                SkillReserveAttribute = ReserveAttributeList[Level - 1];
            }
            return true;
        }
        return false;
    }
    public void LevelClear()
    {
        SkillReserveAttribute = ReserveAttribute.StandardReserveAttribute();
        Level = 0;
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
    public string GetImagePath()
    {
        return ImagePath;
    }

    private string Name;
    private string Message;
    private ReserveAttribute SkillReserveAttribute;
    private int Level;
    private int LevelLimit;
    private ReserveAttribute[] ReserveAttributeList;
    private string ImagePath;
}
/// <summary>
/// In Role.cs
/// </summary>
public struct SkillLearn
{
    public Skill[] SkillList;
    public BagItem[] BagItemList;
}

/// <summary>
/// In Role.cs
/// </summary>
public struct BagItem
{
    public Item item;
    public int count;
}
/// <summary>
/// In Role.cs
/// </summary>
public struct BagEquipment
{
    public Equipment equipment;
    public DateTime ID;
}
/// <summary>
/// In Role.cs
/// </summary>
public class Bag
{
    public Bag(int gold, List<BagItem> itemBag, List<BagEquipment> equipmentBag)
    {
        Gold = gold;
        ItemBag = itemBag;
        EquipmentBag = equipmentBag;
    }

    public void SetGold(int gold)
    {
        Gold = gold;
    }
    public void AddItem(Item newItem, int itemCount = 1)
    {
        for (int i = 0; i < ItemBag.Count; i++)
        {
            if (newItem.GetID() == ItemBag[i].item.GetID())
            {
                ItemBag[i] = new BagItem { item = ItemBag[i].item, count = ItemBag[i].count + itemCount };
                return;
            }
        }
        BagItem buf = new BagItem { item = newItem, count = itemCount };
        ItemBag.Add(buf);
        ItemSort();
    }
    public bool ReduceItem(Item cutItem, int itemCount = 1)
    {
        for (int i = 0; i < ItemBag.Count; i++)
        {
            if (cutItem.GetID() == ItemBag[i].item.GetID())
            {
                if (ItemBag[i].count >= itemCount)
                {
                    ItemBag[i] = new BagItem { item = ItemBag[i].item, count = ItemBag[i].count - itemCount };
                    if (ItemBag[i].count == 0)
                    {
                        ItemBag.RemoveAt(i);
                    }
                    return true;
                }
                return false;
            }
        }
        return false;
    }
    public void AddEquipment(Equipment newEquipment)
    {
        BagEquipment buf = new BagEquipment { equipment = newEquipment, ID = DateTime.Now };
        EquipmentBag.Add(buf);
        EquipmentSort();
    }
    public bool ReduceEquipment(BagEquipment cutEquipment)
    {
        for (int i = 0; i < EquipmentBag.Count; i++)
        {
            if (cutEquipment.ID == EquipmentBag[i].ID && cutEquipment.equipment.GetID() == EquipmentBag[i].equipment.GetID())
            {
                EquipmentBag.RemoveAt(i);
                return true;
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
                        BagItem Buf = new BagItem { item = ItemBag[j].item, count = ItemBag[j].count };
                        ItemBag[j] = ItemBag[j + 1];
                        ItemBag[j + 1] = Buf;
                    }
                }
            }
        }
    }
    public void EquipmentSort()
    {
        if (EquipmentBag.Count > 1)
        {
            for (int i = 0; i < EquipmentBag.Count - 1; i++)
            {
                for (int j = 0; j < EquipmentBag.Count - 1 - i; j++)
                {
                    if (EquipmentBag[j].equipment.GetID() > EquipmentBag[j + 1].equipment.GetID())
                    {
                        BagEquipment Buf = new BagEquipment { equipment = EquipmentBag[j].equipment, ID = EquipmentBag[j].ID };
                        EquipmentBag[j] = EquipmentBag[j + 1];
                        EquipmentBag[j + 1] = Buf;
                    }
                    if (EquipmentBag[j].equipment.GetID() == EquipmentBag[j + 1].equipment.GetID())
                    {
                        if (EquipmentBag[j].ID > EquipmentBag[j + 1].ID)
                        {
                            BagEquipment Buf = new BagEquipment { equipment = EquipmentBag[j].equipment, ID = EquipmentBag[j].ID };
                            EquipmentBag[j] = EquipmentBag[j + 1];
                            EquipmentBag[j + 1] = Buf;
                        }
                    }
                }
            }
        }
    }

    public int GetGold()
    {
        return Gold;
    }
    public List<BagItem> GetItemBag()
    {
        return ItemBag;
    }
    public List<BagEquipment> GetEquipmentBag()
    {
        return EquipmentBag;
    }

    private int Gold;
    private List<BagItem> ItemBag;
    private List<BagEquipment> EquipmentBag;
}