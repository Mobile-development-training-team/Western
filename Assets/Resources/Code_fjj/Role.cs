using System;
using System.Collections.Generic;

//等级属性结构体，用于从csv读入、储存等级属性
public struct LevelIndex
{
    public int Level;  //等级
    public int ExpLimit;  //对应等级的经验值上限
    public Attribute attribute;  //对应属性

    //查找特定等级对应的特定等级的经验值上限
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
    //查找特定等级对应的特定属性
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

//角色属性类
public class RoleAttribute
{
    //获取角色姓名、等级、经验值、经验值上限、属性，进行初始化
    public RoleAttribute(string name, int level, int exp, int expLimit, Attribute atr)
    {
        Name = name;
        attribute = atr;
        Level = level;
        Exp = exp;
        ExpLimit = expLimit;
    }

    public string GetName()  //获得人物姓名
    {
        return Name;
    }
    public int GetAttack()  //获得人物攻击
    {
        return attribute.Attack;
    }
    public int GetHealthPointLimit()  //获得人物生命(生命上限)
    {
        return attribute.HealthPointLimit;
    }
    public int GetDefence()  //获得人物防御
    {
        return attribute.Defence;
    }
    public Attribute GetAttribute()  //获得人物属性
    {
        return attribute;
    }
    public int GetLevel()  //获得人物等级
    {
        return Level;
    }
    public int GetExp()  //获得人物经验
    {
        return Exp;
    }
    public int GetExpLimit()  //获得人物经验上限
    {
        return ExpLimit;
    }

    public void ExpUP(int exp)  //增加人物经验
    {
        Exp += exp;
        LevelUp();
    }
    public bool LevelUp()  //人物升级
    {
        if (Exp < ExpLimit)  //当经验值不足以升级时
        {
            return false;
        }
        if (Level == DataManager.levelIndex.Length)  //当人物满级时
        {
            return false;
        }
        //提升等级直至经验值不足或满级
        while (!(Exp < ExpLimit || Level == DataManager.levelIndex.Length))
        {
            Exp = Exp - ExpLimit;
            Level++;
            ExpLimit = DataManager.levelIndex[Level - 1].ExpLimit;
            attribute = DataManager.levelIndex[Level - 1].attribute;
        }
        return true;
    }

    //获取人物加成后属性[另类计算方式，已弃用]
    public Attribute GetRoleAttribute(Attribute equipmentAttribute, Attribute skillAttribute)
    {
        Attribute a = new Attribute();
        a = attribute + equipmentAttribute + skillAttribute;
        return a;
    }

    public void SetName(string name)  //设置人物姓名
    {
        Name = name;
    }
    public void SetAttack(int atk)  //设置人物攻击
    {
        attribute.Attack = atk;
    }
    public void SetHealthPointLimit(int hp)  //设置人物生命(生命上限)
    {
        attribute.HealthPointLimit = hp;
    }
    public void SetDefence(int def)  //设置人物防御
    {
        attribute.Defence = def;
    }
    public void SetAttribute(Attribute a)  //设置人物属性
    {
        attribute = a;
    }
    public void SetLevel(int level)  //设置人物等级
    {
        Level = level;
    }
    public void SetExp(int exp)  //设置人物经验值
    {
        Exp = exp;
    }

    private string Name;  //人物名称
    private Attribute attribute;  //人物属性
    private int Level;  //人物等级
    private int Exp;  //人物经验值
    private int ExpLimit;  //人物经验值上限
}

//角色装备类
public class RoleEquipment
{
    //获去装备列表，是否已装备、剩余必杀次数、拥有稀土数量、拥有技能碎片数量，进行初始化
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

    //获取人物装备的总属性加成
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

    public bool MainWeaponLevelUp()  //人物主武器升级
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
    public bool AlternateWeaponLevelUp()  //人物副武器升级
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
    public bool CuirassLevelUp()  //人物护甲升级
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
    public bool HelmLevelUp()  //人物头盔升级
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

    //设置人物主武器
    public bool SetMainWeapon(BagItem mainWeapon)
    {
        //判断新装备是否主武器
        if (mainWeapon.item.GetEquipmentType() == EquipmentType.MainWeapon)
        {
            EquipmentList[0] = mainWeapon;  //更新人物主武器
            EquipmentListBool[0] = true;  //更新已装备
            return true;
        }
        return false;
    }
    //设置人物未装备主武器
    public void SetMainWeaponNull()
    {
        EquipmentListBool[0] = false;  //更新未装备
    }
    //设置人物副武器
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
    //设置人物未装备副武器
    public void SetAlternateWeaponNull()
    {
        EquipmentListBool[1] = false;
    }
    //设置人物护甲
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
    //设置人物未装备护甲
    public void SetCuirassNull()
    {
        EquipmentListBool[2] = false;
    }
    //设置人物头盔
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
    //设置人物未装备头盔
    public void SetHelmNull()
    {
        EquipmentListBool[3] = false;
    }

    //设置剩余必杀次数
    public void SetSlayCount(int slayCount)
    {
        SlayCount = slayCount;
    }
    //设置拥有稀土数量
    public void SetRareEarthCount(int rareEarthCount)
    {
        RareEarthCount = rareEarthCount;
    }
    //设置拥有技能碎片数量
    public void SetSkillDebris(int skillDeberis)
    {
        SkillDebris = skillDeberis;
    }

    public bool HadMainWeapon()  //判断是否穿戴主武器
    {
        return EquipmentListBool[0];
    }
    public bool HadAlternateWeapon()  //判断是否穿戴副武器
    {
        return EquipmentListBool[1];
    }
    public bool HadCuirass()  //判断是否穿戴护甲
    {
        return EquipmentListBool[2];
    }
    public bool HadHelm()  //判断是否穿戴头盔
    {
        return EquipmentListBool[3];
    }
    public BagItem GetMainWeapon()  //获取人物主武器
    {
        return EquipmentList[0];
    }
    public BagItem GetAlternateWeapon()  //获取人物副武器
    {
        return EquipmentList[1];
    }
    public BagItem GetCuirass()  //获取人物护甲
    {
        return EquipmentList[2];
    }
    public BagItem GetHelm()  //获取人物头盔
    {
        return EquipmentList[3];
    }
    public int GetSlayCount()  //获取剩余必杀次数
    {
        return SlayCount;
    }
    public int GetRareEarthCount()  //获取拥有稀土数量
    {
        return RareEarthCount;
    }
    public int GetSkillDebris()  //获取技能碎片数量
    {
        return SkillDebris;
    }

    private BagItem[] EquipmentList = new BagItem[4];  //人物装备
    private bool[] EquipmentListBool = new bool[4];  //人物装备与否
    private int SlayCount;  //剩余必杀次数
    private int RareEarthCount;  //拥有稀土数量
    private int SkillDebris;  //拥有技能碎片数量
}

//技能等级属性结构体
public struct SkillLevelIndex
{
    public string name;  //技能名称
    public int level;  //技能等级
    public ReserveAttribute reserveAttribute;  //技能对应等级的对应属性加成
    public double coolDown;  //技能对应等级的对应冷却时间
    public double duration;  //技能对应等级的对应持续时间
}
//技能参数补丁结构体
public struct SkillDataPlus
{
    public ReserveAttribute reserveAttribute;  //技能属性加成
    public double coolDown;  //技能冷却时间
    public double duration;  //技能持续时间
}
//技能升级消耗结构体
public struct SkillLevelUpIndex
{
    public string Name;  //技能名称
    public int LearnCount;  //学习技能消耗
    public int UpCount;  //升级技能消耗
}
//技能类
public class Skill
{
    //获取技能名称、技能简介、技能等级、技能等级上限、技能属性加成列表、技能冷却时间列表、技能持续时间列表，进行初始化
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

    //技能升级
    public bool LevelUP()
    {
        if (Level < LevelLimit)  //如未满级
        {
            Level++;  //提升等级
            SkillReserveAttribute = ReserveAttributeList[Level - 1];  //更新技能属性加成
            SkillCoolDown = CoolDownList[Level - 1];  //更新技能冷却时间
            SkillDuration = DurationList[Level - 1];  //更新技能持续时间
            return true;
        }
        return false;
    }
    //技能降级，目前未使用
    public bool LevelDown()
    {
        if (Level > 0)  //如等级不为0
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
    //洗点，目前未使用
    public void LevelClear()
    {
        SkillReserveAttribute = ReserveAttribute.StandardReserveAttribute();
        SkillCoolDown = 0;
        SkillDuration = 0;
        Level = 0;
    }
    //获取技能列表的总属性加成，目前未使用
    public static ReserveAttribute GetSkillListAttributer(Skill[] skillList)
    {
        ReserveAttribute a = ReserveAttribute.StandardReserveAttribute();
        for (int i = 0; i < skillList.Length; i++)
        {
            a = a + skillList[i].GetAttibute();
        }
        return a;
    }

    public string GetName()  //获得技能名称
    {
        return Name;
    }
    public string GetMessage()  //获得技能简介
    {
        return Message;
    }
    public ReserveAttribute GetAttibute()  //获得技能属性加成
    {
        return SkillReserveAttribute;
    }
    public double GetCoolDown()  //获得技能冷却时间
    {
        return SkillCoolDown;
    }
    public double GetDuration()  //获得技能持续时间
    {
        return SkillDuration;
    }
    public int GetLevel()  //获得技能等级
    {
        return Level;
    }
    public int GetLevelLimit()  //获得技能等级上限
    {
        return LevelLimit;
    }
    public ReserveAttribute GetNextAttribute()  //获取技能下一级对应的属性加成
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
    public double GetNextDuration()  //获取技能下一级对应的持续时间
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
    public string GetImagePath()  //获取技能图标路径
    {
        return ImagePath;
    }

    private string Name;  //技能名称
    private string Message;  //技能简介
    private ReserveAttribute SkillReserveAttribute;  //现技能属性加成
    private double SkillCoolDown;  //现技能冷却时间
    private double SkillDuration;  //现技能持续时间
    private int Level;  //技能等级
    private int LevelLimit;  //技能等级上限
    private ReserveAttribute[] ReserveAttributeList;  //技能属性加成列表[对应等级]
    private double[] CoolDownList;  //技能冷却时间列表
    private double[] DurationList;  //技能持续时间列表
    private string ImagePath;  //技能图标路径
}

//背包物品结构体
public struct BagItem
{
    public Item item;  //物品
    public int level;  //物品等级
    public int count;  //物品数量

    //物品升级[仅用于装备]
    public bool LevelUP()
    {
        if (level < 20)  //如未满级
        {
            level++;
            return true;
        }
        return false;
    }
    //获取更新后的物品数量
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
    //获取装备升级所需的装备碎片数量
    public int GetIntensifyDebris()
    {
        //计算公式为：所需装备碎片数量 = (2 * 装备等级) + 3
        return (2 * level + 3);
    }
    //获取装备升级所需的稀土数量
    public int GetIntensifyRareEarth()
    {
        int q;  //品质系数
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
        //计算公式为：所需稀土数量 = (装备等级 * (品质系数 + 2) + 5)
        return (level * (q + 2) + 5);
    }
    //获取装备分解获得的稀土数量
    public int GetResolveRareEarth()
    {
        //根据品质获得不同稀土数量
        switch (item.GetQuality())
        {
            case Quality.Normal:
                return 100;
            case Quality.Excellent:
                return 200;
            case Quality.Rare:
                return 300;
            default:
                return 0;
        }
    }
    //获取装备属性加成
    public ReserveAttribute GetEquipmentReserveAttribute()
    {
        ReserveAttribute Buf = new ReserveAttribute();
        Buf.AddATK = item.GetReserveAttribute().AddATK;
        Buf.PlusATK = item.GetReserveAttribute().PlusATK;
        Buf.AddHP = item.GetReserveAttribute().AddHP;
        Buf.PlusHP = item.GetReserveAttribute().PlusHP;
        Buf.AddDEF = item.GetReserveAttribute().AddDEF;
        Buf.PlusDEF = item.GetReserveAttribute().PlusDEF;
        for (int i = 0; i < level; i++)  //计算装备等级对属性加成的提升
        {
            Buf = Buf * 1.1;
        }
        return Buf;
    }
}
//背包类
public class Bag
{
    //获取BagItem结构体列表，进行初始化
    public Bag(List<BagItem> itemBag)
    {
        ItemBag = itemBag;
    }
    //在背包中查找物体
    public int FindBagItem(BagItem b)
    {
        //如查找到背包物品，则返回在背包中的位置
        for (int i = 0; i < ItemBag.Count; i++)
        {
            if (b.item.GetID() == ItemBag[i].item.GetID() && b.level == ItemBag[i].level)
            {
                return i;
            }
        }
        //如未查找到，返回-1
        return -1;
    }
    //背包物品升级[仅用于装备]
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
    //更换背包特定位置的背包物品[用于装备切换]
    public void ChangeBagItem(BagItem b, int index)
    {
        ItemBag[index] = b;
    }
    //增加物品到背包中
    public void AddItem(Item newItem, int itemCount = 1)
    {
        for (int i = 0; i < ItemBag.Count; i++)  //查找背包中是否有新物品，如有
        {
            if (newItem.GetID() == ItemBag[i].item.GetID())  //如有
            {
                if (ItemBag[i].item.GetEquipmentOrNot())  //判断新物品是否为装备，如是
                {
                    for (int j = 0; j < itemCount; j++)  //增加itemCount件装备[装备不互相叠加]
                    {
                        BagItem addBuf = new BagItem();
                        addBuf.item = new Item();
                        addBuf.item.FindItem(DataManager.GameItemIndex, newItem.GetID());
                        addBuf.level = 0;
                        addBuf.count = 1;
                        ItemBag.Add(addBuf);
                    }
                }
                else  //如不是
                {
                    BagItem addBuf = new BagItem();
                    addBuf.item = new Item();
                    addBuf.item = ItemBag[i].item;
                    addBuf.level = ItemBag[i].level;
                    addBuf.count = ItemBag[i].CountChange(itemCount);  //更新物品数量[物品可叠加]
                    ItemBag[i] = addBuf;
                }
                return;
            }
        }
        //如背包中无新物品，直接添加
        BagItem Buf = new BagItem();
        Buf.item = new Item();
        Buf.item.FindItem(DataManager.GameItemIndex, newItem.GetID());
        Buf.level = 0;
        Buf.count = itemCount;
        ItemBag.Add(Buf);
        //ItemSort();
    }
    //添加背包物品到背包中[主要用于添加装备]
    public void AddBagItem(BagItem newBagItem)
    {
        //逻辑与AddItem()相同
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
    //减少背包中背包物品的数量
    public bool ReduceItem(BagItem cutItem, int itemCount = 1)
    {
        for (int i = 0; i < ItemBag.Count; i++)  //在背包中查找该物品
        {
            if (cutItem.item.GetID() == ItemBag[i].item.GetID())  //如有该物品
            {
                if (ItemBag[i].item.GetEquipmentOrNot())  //如该物品是装备
                {
                    if (cutItem.level == ItemBag[i].level)  //如背包中装备与该装备等级相同，即两者等价
                    {
                        ItemBag.RemoveAt(i);  //将背包总装备删除(减去)
                        return true;  //减少成功
                    }
                }
                else  //如该物品不是装备
                {
                    if (ItemBag[i].count >= itemCount)  //如背包中物品数量大于或等于减少数量
                    {
                        //可减少
                        ItemBag[i] = new BagItem { item = ItemBag[i].item, level = ItemBag[i].level, count = ItemBag[i].count - itemCount };
                        if (ItemBag[i].count == 0)  //物品数量为0时从背包中剔除
                        {
                            ItemBag.RemoveAt(i);
                        }
                        return true;  //减少成功
                    }
                }
            }
        }
        //如查找不到该物品
        return false;  //减少失败
    }
    //排序背包物品
    public void ItemSort()
    {
        if (ItemBag.Count > 1)  //当存在多个物品时
        {
            for (int i = 0; i < ItemBag.Count - 1; i++)
            {
                for (int j = 0; j < ItemBag.Count - 1 - i; j++)
                {
                    if (ItemBag[j].item.GetID() > ItemBag[j + 1].item.GetID())  //根据物品ID进行升序排序
                    {
                        BagItem Buf = new BagItem { item = ItemBag[j].item, level = ItemBag[i].level, count = ItemBag[j].count };
                        ItemBag[j] = ItemBag[j + 1];
                        ItemBag[j + 1] = Buf;
                    }
                }
            }
        }
    }
    //获得背包物品列表
    public List<BagItem> GetItemBag()
    {
        return ItemBag;
    }

    private List<BagItem> ItemBag;  //背包物品列表
}