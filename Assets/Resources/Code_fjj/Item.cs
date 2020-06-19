using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// In Item.cs
/// </summary>
public struct ReserveAttribute
{
    public int AddATK;
    public double PlusATK;
    public int AddHP;
    public double PlusHP;
    public int AddDEF;
    public double PlusDEF;

    public string ReserveAttributeToString()
    {
        string Buf = "";
        Buf += "属性：\n";
        if (AddATK > 0)
        {
            Buf += "攻击：+" + AddATK.ToString() + '\n';
        }
        if (PlusATK > 0.000001)
        {
            Buf += "攻击：+" + (PlusATK * 100).ToString() + "%\n";
        }
        if (AddHP > 0)
        {
            Buf += "生命：+" + AddHP.ToString() + '\n';
        }
        if (PlusHP > 0.000001)
        {
            Buf += "生命：+" + (PlusHP * 100).ToString() + "%\n";
        }
        if (AddDEF > 0)
        {
            Buf += "防御：+" + AddDEF.ToString() + '\n';
        }
        if (PlusDEF > 0.000001)
        {
            Buf += "防御：+" + (PlusDEF * 100).ToString() + "%\n";
        }
        return Buf;
    }

    public static ReserveAttribute GrossReserveAttribute(ReserveAttribute[] reserveAttributeList)
    {
        ReserveAttribute buf = ReserveAttribute.StandardReserveAttribute();
        for (int i = 0; i < reserveAttributeList.Length; i++)
        {
            buf = buf + reserveAttributeList[i];
        }
        return buf;
    }

    public static Attribute ReserverToService(ReserveAttribute a, Attribute b)
    {
        Attribute c = new Attribute();
        c.Attack = (int)((a.AddATK + b.Attack) * (1 + a.PlusATK));
        c.HealthPointLimit = (int)((a.AddHP + b.HealthPointLimit) * (1 + a.PlusHP));
        c.Defence = (int)((a.AddDEF + b.Defence) * (1 + a.PlusHP));
        return c;
    }

    public static ReserveAttribute StandardReserveAttribute()
    {
        ReserveAttribute NewOne = new ReserveAttribute { AddATK = 0, PlusATK = 0, AddHP = 0, PlusHP = 0, AddDEF = 0, PlusDEF = 0 };
        return NewOne;
    }
    public static bool ReserveAttributeCompare(ReserveAttribute a, ReserveAttribute b)
    {
        if (a.AddATK != b.AddATK) { return false; }
        if (a.PlusATK != b.PlusATK) { return false; }
        if (a.AddHP != b.AddHP) { return false; }
        if (a.PlusHP != b.PlusHP) { return false; }
        if (a.AddDEF != b.AddDEF) { return false; }
        if (a.PlusDEF != b.PlusDEF) { return false; }
        return true;
    }


    public static ReserveAttribute operator +(ReserveAttribute a, ReserveAttribute b)
    {
        ReserveAttribute c = new ReserveAttribute();
        c.AddATK = a.AddATK + b.AddATK;
        c.PlusATK = a.PlusATK + b.PlusATK;
        c.AddHP = a.AddHP + b.AddHP;
        c.PlusHP = a.PlusHP + b.PlusHP;
        c.AddDEF = a.AddDEF + b.AddDEF;
        c.PlusDEF = a.PlusDEF + b.PlusDEF;
        return c;
    }

    public static ReserveAttribute operator *(ReserveAttribute a, int b)
    {
        ReserveAttribute c = new ReserveAttribute();
        c.AddATK = a.AddATK * b;
        c.PlusATK = a.PlusATK * b;
        c.AddHP = a.AddHP * b;
        c.PlusHP = a.PlusHP * b;
        c.AddDEF = a.AddDEF * b;
        c.PlusDEF = a.PlusDEF * b;
        return c;
    }
    public static ReserveAttribute operator *(int a, ReserveAttribute b)
    {
        return b * a;
    }

    public static ReserveAttribute operator *(ReserveAttribute a, double b)
    {
        ReserveAttribute c = new ReserveAttribute();
        c.AddATK = (int)(a.AddATK * b);
        c.PlusATK = a.PlusATK * b;
        c.AddHP = (int)(a.AddHP * b);
        c.PlusHP = a.PlusHP * b;
        c.AddDEF = (int)(a.AddDEF * b);
        c.PlusDEF = a.PlusDEF * b;
        return c;
    }
    public static ReserveAttribute operator *(double a, ReserveAttribute b)
    {
        return b * a;
    }
}
/// <summary>
/// In Item.cs
/// </summary>
public struct Attribute
{
    public int Attack;
    public int HealthPointLimit;
    public int Defence;

    public static bool AttributeCompare(Attribute a, Attribute b)
    {
        if (a.Attack != b.Attack) { return false; }
        if (a.HealthPointLimit != b.HealthPointLimit) { return false; }
        if (a.Defence != b.Defence) { return false; }
        return true;
    }

    public static Attribute operator +(Attribute a, Attribute b)
    {
        Attribute c = new Attribute();
        c.Attack = a.Attack + b.Attack;
        c.HealthPointLimit = a.HealthPointLimit + b.HealthPointLimit;
        c.Defence = a.Defence + b.Defence;
        return c;
    }
    public static Attribute operator -(Attribute a, Attribute b)
    {
        Attribute c = new Attribute();
        c.Attack = a.Attack - b.Attack;
        c.HealthPointLimit = a.HealthPointLimit - b.HealthPointLimit;
        c.Defence = a.Defence - b.Defence;
        return c;
    }
    public static Attribute operator *(Attribute a, int b)
    {
        Attribute c = new Attribute();
        c.Attack = a.Attack * b;
        c.HealthPointLimit = a.HealthPointLimit * b;
        c.Defence = a.Defence * b;
        return c;
    }
    public static Attribute operator *(int a, Attribute b)
    {
        return b * a;
    }
    public static Attribute operator *(Attribute a, double b)
    {
        Attribute c = new Attribute();
        c.Attack = (int)(a.Attack * b);
        c.HealthPointLimit = (int)(a.HealthPointLimit * b);
        c.Defence = (int)(a.Defence * b);
        return c;
    }
    public static Attribute operator *(double a, Attribute b)
    {
        return b * a;
    }
}

/// <summary>
/// In Item.cs
/// </summary>
public enum EquipmentType
{
    MainWeapon,
    AlternateWeapon,
    Cuirass,
    Helm
}

/// <summary>
/// In Item.cs
/// </summary>
public enum Quality
{
    Normal,
    Excellent,
    Rare,
    Epic
}

/// <summary>
/// In Item.cs
/// </summary>
public class Item
{
    public Item()
    {
    }
    public Item(int id, string name, string message, bool equipmentOrNot, ReserveAttribute rA, EquipmentType eT, Quality q, string imagePath)
    {
        ID = id;
        Name = name;
        Message = message;
        EquipmentOrNot = equipmentOrNot;
        reserveAttribute = rA;
        equipmentType = eT;
        quality = q;
        ImagePath = imagePath;
    }
    public Item(Item copy)
    {
        ID = copy.ID;
        Name = copy.Name;
        Message = copy.Message;
        EquipmentOrNot = copy.EquipmentOrNot;
        reserveAttribute = copy.reserveAttribute;
        equipmentType = copy.equipmentType;
        quality = copy.quality;
        ImagePath = copy.ImagePath;
    }
    public Item(Item[] itemList, int id)
    {
        FindItem(itemList, id);
    }

    public void FindItem(Item[] itemList, int id)
    {
        for (int i = 0; i < itemList.Length; i++)
        {
            if (itemList[i].ID == id)
            {
                ID = itemList[i].ID;
                Name = itemList[i].Name;
                Message = itemList[i].Message;
                EquipmentOrNot = itemList[i].EquipmentOrNot;
                reserveAttribute = itemList[i].reserveAttribute;
                equipmentType = itemList[i].equipmentType;
                quality = itemList[i].quality;
                ImagePath = itemList[i].ImagePath;
                break;
            }
        }
    }

    public int GetID()
    {
        return ID;
    }
    public string GetName()
    {
        return Name;
    }
    public string GetMessage()
    {
        return Message;
    }
    public bool GetEquipmentOrNot()
    {
        return EquipmentOrNot;
    }
    public ReserveAttribute GetReserveAttribute()
    {
        return reserveAttribute;
    }
    public EquipmentType GetEquipmentType()
    {
        return equipmentType;
    }
    public Quality GetQuality()
    {
        return quality;
    }
    public string GetImagePath()
    {
        return ImagePath;
    }

    private int ID;
    private string Name;
    private string Message;
    private bool EquipmentOrNot;
    private EquipmentType equipmentType;
    private ReserveAttribute reserveAttribute;
    private Quality quality;
    private string ImagePath;
}
