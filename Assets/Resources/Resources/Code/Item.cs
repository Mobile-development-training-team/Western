using System.Collections.Generic;

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
    public int AddCritValue;
    public double PlusCritValue;
    public double AddCritRatio;
    public double PlusCritRatio;
    public int AddLeech;
    public double PlusLeech;
    public int AddStun;
    public double PlusStun;
    public double AddCoolDownRatio;
    public double PlusCoolDownRatio;

    public static Attribute ReserverToService(ReserveAttribute a, Attribute b)
    {
        Attribute c = new Attribute();
        c.Attack = (int)((a.AddATK + b.Attack) * (1 + a.PlusATK));
        c.HealthPointLimit = (int)((a.AddHP + b.HealthPointLimit) * (1 + a.PlusHP));
        c.Defence = (int)((a.AddDEF + b.Defence) * (1 + a.PlusHP));
        c.CritValue = (int)((a.AddCritValue + b.CritValue) * (1 + a.PlusCritValue));
        c.CritRatio = (a.AddCritRatio + b.CritRatio) * (1 + a.PlusCritRatio);
        c.Leech = (int)((a.AddLeech + b.Leech) * (1 + a.PlusLeech));
        c.Stun = (int)((a.AddStun + b.Stun) * (1 + a.PlusStun));
        c.CoolDownRatio = (a.AddCoolDownRatio + b.CoolDownRatio) * (1 + a.PlusCoolDownRatio);
        return c;
    }

    public static ReserveAttribute StandardReserveAttribute()
    {
        ReserveAttribute NewOne = new ReserveAttribute{ AddATK = 0, PlusATK = 1,AddHP = 0,PlusHP = 1,AddDEF = 0,PlusDEF = 1, AddCritValue = 0, PlusCritValue = 1,AddCritRatio = 0,PlusCritRatio = 1,AddLeech = 0, PlusLeech = 1, AddStun = 0, PlusStun = 1, AddCoolDownRatio = 0, PlusCoolDownRatio = 1};
        return NewOne;
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
    public int CritValue;
    public double CritRatio;
    public int Leech;
    public int Stun;
    public double CoolDownRatio;

    public static Attribute operator +(Attribute a, Attribute b)
    {
        Attribute c = new Attribute();
        c.Attack = a.Attack + b.Attack;
        c.HealthPointLimit = a.HealthPointLimit + b.HealthPointLimit;
        c.Defence = a.Defence + b.Defence;
        c.CritValue = a.CritValue + b.CritValue;
        c.CritRatio = a.CritRatio + b.CritRatio;
        c.Leech = a.Leech + b.Leech;
        c.Stun = a.Stun + b.Stun;
        c.CoolDownRatio = a.CoolDownRatio + b.CoolDownRatio;
        return c;
    }
    public static Attribute operator -(Attribute a, Attribute b)
    {
        Attribute c = new Attribute();
        c.Attack = a.Attack - b.Attack;
        c.HealthPointLimit = a.HealthPointLimit - b.HealthPointLimit;
        c.Defence = a.Defence - b.Defence;
        c.CritValue = a.CritValue - b.CritValue;
        c.CritRatio = a.CritRatio - b.CritRatio;
        c.Leech = a.Leech - b.Leech;
        c.Stun = a.Stun - b.Stun;
        c.CoolDownRatio = a.CoolDownRatio - b.CoolDownRatio;
        return c;
    }
    public static Attribute operator *(Attribute a, int b)
    {
        Attribute c = new Attribute();
        c.Attack = a.Attack * b;
        c.HealthPointLimit = a.HealthPointLimit * b;
        c.Defence = a.Defence * b;
        c.CritValue = a.CritValue * b;
        c.CritRatio = a.CritRatio * b;
        c.Leech = a.Leech * b;
        c.Stun = a.Stun * b;
        c.CoolDownRatio = a.CoolDownRatio * b;
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
        c.CritValue = (int)(a.CritValue * b);
        c.CritRatio = a.CritRatio * b;
        c.Leech = (int)(a.Leech * b);
        c.Stun = (int)(a.Stun * b);
        c.CoolDownRatio = a.CoolDownRatio * b;
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
    Helm,
    Cuirass,
    Cuish,
    Necklace,
    Ring
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
    public Item(int id, string name, string message, string imagePath)
    {
        ID = id;
        Name = name;
        Message = message;
        ImagePath = imagePath;
    }
    public Item(Item copy)
    {
        ID = copy.ID;
        Name = copy.Name;
        Message = copy.Message;
        ImagePath = copy.ImagePath;
    }

    public static Item FindItem(Item[] itemList, int id)
    {
        for (int i = 0; i < itemList.Length; i++)
        {
            if (itemList[i].GetID() == id)
            {
                return itemList[i];
            }
        }
        return itemList[0];
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
    public string GetImagePath()
    {
        return ImagePath;
    }

    private int ID;
    private string Name;
    private string Message;
    private string ImagePath;
}

/// <summary>
/// In Item.cs
/// </summary>
//public struct EquipmentIndexCell
//{
//    public int id;
//    public string name;
//    public string message;
//    public string imagePath;
//    public EquipmentType equipmentType;
//    public ReserveAttribute reserveAttribute;
//    public Quality quality;
//}
/// <summary>
/// In Item.cs
/// </summary>
public class Equipment : Item
{
    public Equipment(int id, string name, string message, string imagePath, EquipmentType equipmentType, ReserveAttribute reserveAttribute, Quality quality) :base(id,name,message,imagePath)
    {
        Type = equipmentType;
        EquipmentReserveAttribute = reserveAttribute;
        EquipmentQuality = quality;
        //switch (EquipmentQuality)
        //{
        //    case Quality.Normal:
        //        EquipmentAttribute = EquipmentAttribute * 1;
        //        break;
        //    case Quality.Excellent:
        //        EquipmentAttribute = EquipmentAttribute * 1.1;
        //        break;
        //    case Quality.Rare:
        //        EquipmentAttribute = EquipmentAttribute * 1.3;
        //        break;
        //    case Quality.Epic:
        //        EquipmentAttribute = EquipmentAttribute * 1.6;
        //        break;
        //}
    }

    public void SetAttribute(ReserveAttribute reserveAttribute)
    {
        EquipmentReserveAttribute = reserveAttribute;
    }
    public void SetQuality(Quality quality)
    {
        EquipmentQuality = quality;
    }

    public EquipmentType GetEquipmentType()
    {
        return Type;
    }
    public ReserveAttribute GetAttribute()
    {
        return EquipmentReserveAttribute;
    }
    public Quality GetQuality()
    {
        return EquipmentQuality;
    }

    private EquipmentType Type;
    private ReserveAttribute EquipmentReserveAttribute;
    private Quality EquipmentQuality;
}

/// <summary>
/// In Item.cs
/// </summary>
public struct EquipmentMakeCell
{
    public int EquipmentID;
    public List<MakeMaterial> makeMatrial;
}
/// <summary>
/// In Item.cs
/// </summary>
public struct MakeMaterial
{
    public int ItemID;
    public int count;
}
/// <summary>
/// In Item.cs
/// </summary>
public class EquipmentMake
{
    public EquipmentMake(int[] equipmentIDList, List<MakeMaterial>[] makeMatrialList)
    {
        EquipmentIDList = equipmentIDList;
        MakeMatrialList = makeMatrialList;
    }

    public EquipmentMakeCell GetEquipmentMakeCell(int equipmentID)
    {
        for (int i = 0; i < EquipmentIDList.Length; i++)
        {
            if (equipmentID == EquipmentIDList[i])
            {
                EquipmentMakeCell fBuf = new EquipmentMakeCell { EquipmentID = EquipmentIDList[i], makeMatrial = MakeMatrialList[i] };
                return fBuf;
            }
        }
        EquipmentMakeCell sBuf = new EquipmentMakeCell { EquipmentID = EquipmentIDList[0], makeMatrial = MakeMatrialList[0] };
        return sBuf;
    }

    private int[] EquipmentIDList;
    private List<MakeMaterial>[] MakeMatrialList;
}