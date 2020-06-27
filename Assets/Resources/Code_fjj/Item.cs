using System.Collections.Generic;
using UnityEngine;

//属性加成结构体
public struct ReserveAttribute
{
    public int AddATK;  //攻击加算参数
    public double PlusATK;  //攻击乘算参数
    public int AddHP;  //生命加算参数
    public double PlusHP;  //生命乘算参数
    public int AddDEF;  //防御加算参数
    public double PlusDEF;  //防御乘算参数

    //将技能属性加成转化为特定string，用于辅助技能属性显示
    public string SkillReseveAttributeToString(Skill skill)
    {
        string Buf = "";
        //以下判断区分不同等级下技能的不同显示
        if (skill.GetLevel() == 0)  //当技能等级为0
        {
            ReserveAttribute nBuf = skill.GetNextAttribute();
            if (nBuf.AddATK > 0)  //当存在技能加算
            {
                Buf += "升级:(攻击+" + nBuf.AddATK.ToString() + ')';
            }
            if (nBuf.PlusATK > 0.000001)  //当存在技能乘算
            {
                Buf += "升级:(攻击+" + (nBuf.PlusATK * 100).ToString() + "%)";
            }
            if (nBuf.AddHP > 0)  //当存在生命加算
            {
                Buf += "升级:(生命+" + nBuf.AddHP.ToString() + ')';
            }
            if (nBuf.PlusHP > 0.000001)  //当存在生命乘算
            {
                Buf += "升级:(生命+" + (nBuf.PlusHP * 100).ToString() + "%)";
            }
            if (nBuf.AddDEF > 0)  //当存在防御加算
            {
                Buf += "升级:(防御+" + nBuf.AddDEF.ToString() + ')';
            }
            if (nBuf.PlusDEF > 0.000001)  //当存在防御乘算
            {
                Buf += "升级:(防御+" + (nBuf.PlusDEF * 100).ToString() + "%)";
            }
            if (skill.GetNextDuration() > 0.000001)  //当存在持续时间
            {
                Buf += "升级:(持续时间:" + skill.GetNextDuration().ToString() + "秒)";
            }
        }
        if (skill.GetLevel() < skill.GetLevelLimit()) //当技能未满级[技能等级为0时此判断不输出]
        {
            if (AddATK > 0)
            {
                Buf += "攻击：+" + AddATK.ToString() + " 升级:(+" + skill.GetNextAttribute().AddATK.ToString() + ')';
            }
            if (PlusATK > 0.000001)
            {
                Buf += "攻击：+" + (PlusATK * 100).ToString() + "% 升级:(+" + (skill.GetNextAttribute().PlusATK * 100).ToString() + "%)";
            }
            if (AddHP > 0)
            {
                Buf += "生命：+" + AddHP.ToString() + " 升级:(+" + skill.GetNextAttribute().AddHP.ToString() + ')';
            }
            if (PlusHP > 0.000001)
            {
                Buf += "生命：+" + (PlusHP * 100).ToString() + "% 升级:(+" + (skill.GetNextAttribute().PlusHP * 100).ToString() + "%)";
            }
            if (AddDEF > 0)
            {
                Buf += "防御：+" + AddDEF.ToString() + " 升级:(+" + skill.GetNextAttribute().AddDEF.ToString() + ')';
            }
            if (PlusDEF > 0.000001)
            {
                Buf += "防御：+" + (PlusDEF * 100).ToString() + "% 升级:(+" + (skill.GetNextAttribute().PlusDEF * 100).ToString() + "%)";
            }
            if (skill.GetDuration() > 0.000001)
            {
                Buf += "持续时间:" + skill.GetDuration().ToString() + "秒 升级:(" + skill.GetNextDuration().ToString() + "秒)";
            }
        }
        else  //当技能满级
        {
            if (AddATK > 0)
            {
                Buf += "攻击：+" + AddATK.ToString();
            }
            if (PlusATK > 0.000001)
            {
                Buf += "攻击：+" + (PlusATK * 100).ToString() + '%';
            }
            if (AddHP > 0)
            {
                Buf += "生命：+" + AddHP.ToString();
            }
            if (PlusHP > 0.000001)
            {
                Buf += "生命：+" + (PlusHP * 100).ToString() + '%';
            }
            if (AddDEF > 0)
            {
                Buf += "防御：+" + AddDEF.ToString();
            }
            if (PlusDEF > 0.000001)
            {
                Buf += "防御：+" + (PlusDEF * 100).ToString() + '%';
            }
            if (skill.GetDuration() > 0.000001)
            {
                Buf += "持续时间:" + skill.GetDuration().ToString() + "秒";
            }
        }
        return Buf;
    }

    //将属性加成转化为特定string，用于辅助装备属性显示
    public string ReserveAttributeToString(bool MAXorNot)
    {
        string Buf = "";
        Buf += "属性：\n";
        if (MAXorNot)  //判断装备满级与否，如满级
        {
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
        }
        else  //如不满级
        {
            if (AddATK > 0)
            {
                Buf += "攻击：+" + AddATK.ToString() + "\n升级:(+" + ((int)(AddATK * 0.1)).ToString() + ')'+ '\n';
            }
            if (PlusATK > 0.000001)
            {
                Buf += "攻击：+" + (PlusATK * 100).ToString() + "%\n升级:(+" + (PlusATK * 100 * 0.1).ToString() + "%)\n";
            }
            if (AddHP > 0)
            {
                Buf += "生命：+" + AddHP.ToString() + "\n升级:(+" + ((int)(AddHP * 0.1)).ToString() + ')' + '\n';
            }
            if (PlusHP > 0.000001)
            {
                Buf += "生命：+" + (PlusHP * 100).ToString() + "%\n升级:(+" + (PlusATK * 100 * 0.1).ToString() + "%)\n";
            }
            if (AddDEF > 0)
            {
                Buf += "防御：+" + AddDEF.ToString() + "\n升级:(+" + ((int)(AddDEF * 0.1)).ToString() + ')' + '\n';
            }
            if (PlusDEF > 0.000001)
            {
                Buf += "防御：+" + (PlusDEF * 100).ToString() + "%\n升级:(+" + (PlusDEF * 100 * 0.1).ToString() + "%)\n";
            }
        }
        return Buf;
    }

    //将攻击、生命、防御转化为string，用于辅助新UI属性显示
    public string AtkToString()
    {
        string Buf = "";
        if (AddATK > 0)
        {
            Buf += '+' + AddATK.ToString();
            if (PlusATK > 0.000001)
            {
                Buf = Buf + " +" + (PlusATK * 100).ToString() + '%';
                return Buf;
            }
            return Buf;
        }
        if (PlusATK > 0.000001)
        {
            Buf = Buf + '+' + (PlusATK * 100).ToString() + '%';
        }
        return Buf;
    }
    public string HpToString()
    {
        string Buf = "";
        if (AddHP > 0)
        {
            Buf += '+' + AddHP.ToString();
            if (PlusHP > 0.000001)
            {
                Buf = Buf + " +" + (PlusHP * 100).ToString() + '%';
                return Buf;
            }
            return Buf;
        }
        if (PlusHP > 0.000001)
        {
            Buf = Buf + '+' + (PlusHP * 100).ToString() + '%';
        }
        return Buf;
    }
    public string DefToString()
    {
        string Buf = "";
        if (AddDEF > 0)
        {
            Buf += '+' + AddDEF.ToString();
            if (PlusDEF > 0.000001)
            {
                Buf = Buf + " +" + (PlusDEF * 100).ToString() + '%';
                return Buf;
            }
            return Buf;
        }
        if (PlusDEF > 0.000001)
        {
            Buf = Buf + '+' + (PlusDEF * 100).ToString() + '%';
        }
        return Buf;
    }

    //获取ReserveAttribute[]的总和，是辅助函数
    public static ReserveAttribute GrossReserveAttribute(ReserveAttribute[] reserveAttributeList)
    {
        ReserveAttribute buf = ReserveAttribute.StandardReserveAttribute();
        for (int i = 0; i < reserveAttributeList.Length; i++)
        {
            buf = buf + reserveAttributeList[i];
        }
        return buf;
    }

    //基于属性Attribute b,计算叠加属性加成ReserveAttribute a后的属性
    public static Attribute ReserverToService(ReserveAttribute a, Attribute b)
    {
        Attribute c = new Attribute();
        //计算公式为：属性 = (原属性 + 属性加算) * (1 + 属性乘算)
        c.Attack = (int)((a.AddATK + b.Attack) * (1 + a.PlusATK));
        c.HealthPointLimit = (int)((a.AddHP + b.HealthPointLimit) * (1 + a.PlusHP));
        c.Defence = (int)((a.AddDEF + b.Defence) * (1 + a.PlusHP));
        return c;
    }
    //基于属性Attribute b,计算属性加成ReserveAttribute a换算后的属性，用于计算技能输出
    public static Attribute SkillReserveToServiece(ReserveAttribute a, Attribute b)
    {
        Attribute c = new Attribute();
        //计算公式为：属性 = (原属性 + 属性加算) * 属性乘算
        c.Attack = (int)((a.AddATK + b.Attack) * a.PlusATK);
        c.HealthPointLimit = (int)((a.AddHP + b.HealthPointLimit) * a.PlusHP);
        c.Defence = (int)((a.AddDEF + b.Defence) * a.PlusHP);
        return c;
    }

    //返回零值的标准ReserveAttribute
    public static ReserveAttribute StandardReserveAttribute()
    {
        ReserveAttribute NewOne = new ReserveAttribute { AddATK = 0, PlusATK = 0, AddHP = 0, PlusHP = 0, AddDEF = 0, PlusDEF = 0 };
        return NewOne;
    }
    //用于比较ReserveAttribute a/b是否相等
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

    //以下为对ReserveAttribute的运算符重载
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

//属性结构体
public struct Attribute
{
    public int Attack;  //攻击
    public int HealthPointLimit;  //生命
    public int Defence;  //防御

    //用于比较Attribute a/b是否相等
    public static bool AttributeCompare(Attribute a, Attribute b)
    {
        if (a.Attack != b.Attack) { return false; }
        if (a.HealthPointLimit != b.HealthPointLimit) { return false; }
        if (a.Defence != b.Defence) { return false; }
        return true;
    }

    //以下为对Attribute的运算符重载
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

//装备类型枚举
public enum EquipmentType
{
    MainWeapon,  //主武器
    AlternateWeapon,  //副武器
    Cuirass,  //护甲
    Helm  //头盔
}

//物品品质枚举
public enum Quality
{
    Normal,  //普通
    Excellent,  //精良
    Rare,  //稀有
    Epic  //史诗
}

//物品类
public class Item
{
    public Item()
    {
    }
    //获取物品ID、名称、简介、装备与否、属性加成、装备种类、物品品质、物品图标路径，进行初始化
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
    //从另一个Item拷贝
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
    //根据Item列表及序号进行初始化
    public Item(Item[] itemList, int id)
    {
        FindItem(itemList, id);
    }

    //将自己初始化为Item列表中的特定Item
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

    public int GetID()  //获取物品ID
    {
        return ID;
    }
    public string GetName()  //获取物品名称
    {
        return Name;
    }
    public string GetMessage()  //获取物品简介
    {
        return Message;
    }
    public bool GetEquipmentOrNot()  //获取是否装备
    {
        return EquipmentOrNot;
    }
    public ReserveAttribute GetReserveAttribute()  //获取属性加成
    {
        return reserveAttribute;
    }
    public EquipmentType GetEquipmentType()  //获取装备种类
    {
        return equipmentType;
    }
    public Quality GetQuality()  //获取物品品质
    {
        return quality;
    }
    public string GetImagePath()  //获取物品图标路径，用于读取Sprite
    {
        return ImagePath;
    }

    private int ID;  //物品ID
    private string Name;  //物品名称
    private string Message;  //物品简介
    private bool EquipmentOrNot;  //是否是装备
    private EquipmentType equipmentType;  //装备种类
    private ReserveAttribute reserveAttribute;  //属性加成
    private Quality quality;  //物品品质
    private string ImagePath;  //物品图标路径
}
