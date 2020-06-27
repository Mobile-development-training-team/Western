using System;


/// <summary>
/// 用于储存关卡产出信息
/// </summary>
public class Chip
{
    private int level;  //关卡序号
    private bool firstOrNot;  //是否首通
    private Quality chipType;  //非首通获得的装备碎片品质
    private int chipNum;  //非首通获得的装备碎片数量
    private Quality chipType_first;  //首通获得的装备碎片品质
    private int chipNum_first;  //首通获得的装备碎片数量
    private int rareEarthNum;  //非首通获得的稀土数量
    private int rareEarthNum_first;  //首通获得的稀土数量
    private int exp;  //获得的经验
    private int exp_first;
    private int skillDebrisNum;  //非首通获得的技能碎片数量
    private int skillDebrisNum_first;  //首通获得的技能碎片数量

    //初始化，参数依次为关卡序号，首通与否，装备碎片品质，装备碎片数量，稀土数量，经验数量，首通装备碎片品质，首通装备碎片数量，首通稀土数量，技能碎片数量，首通技能碎片数量
    public Chip(int l, bool fON, Quality cT, int cN, int rEN, int e, Quality cTF, int cNF, int rEF, int eXF, int sDN, int sDF)
    {
        level = l;
        firstOrNot = fON;
        chipType = cT;
        chipNum = cN;
        rareEarthNum = rEN;
        exp = e;
        chipType_first = cTF;
        chipNum_first = cNF;
        rareEarthNum_first = rEF;
        exp_first = eXF;
        skillDebrisNum = sDN;
        skillDebrisNum_first = sDF;
    }

    //设置是否首通
    public void SetFirstOrNot(bool b)
    {
        firstOrNot = b;
    }

    //获得装备碎片品质[区分是否首通]
    public Quality GetChipQuality()
    {
        if (firstOrNot)
        {
            return chipType_first;
        }
        else
        {
            return chipType;
        }  
    }
    //获得装备碎片品质[未通关，固定为非首通]
    public Quality GetNotPassChipQuality()
    {
        return chipType;
    }

    //获得装备碎片数量[区分是否首通]
    public int GetChipNum()
    {
        Random random = new Random();
        int min = 95, max = 105;  //浮动参数，使每次通关获取的收益有浮动
        float factor = random.Next(min, max) / (float)100;
        if (firstOrNot)
        {
            return chipNum_first;
        }
        else
        {
            return (int)(chipNum * factor);
        }
    }
    //获得装备碎片数量[未通关，固定为非首通]
    public int GetNotPassChipNum()
    {
        Random random = new Random();
        int min = 95, max = 105;
        float factor = random.Next(min, max) / (float)100;
        return (int)(chipNum * factor);
    }

    //获得稀土数量[区分是否首通]
    public int GetRareEarthNum()
    {
        Random random = new Random();
        int min = 95, max = 105;
        float factor = random.Next(min, max) / (float)100;
        if (firstOrNot)
        {
            return rareEarthNum_first;
        }
        else
        {
            return (int)(rareEarthNum * factor);
        }
    }
    //获得稀土数量[未通关，固定为非首通]
    public int GetNotPassRareEarthNum()
    {
        Random random = new Random();
        int min = 95, max = 105;
        float factor = random.Next(min, max) / (float)100;
        return (int)(rareEarthNum * factor);
    }

    //获得技能碎片数量[区分是否首通]
    public int GetSkillDebrisNum()
    {
        Random random = new Random();
        int min = 95, max = 105;
        float factor = random.Next(min, max) / (float)100;
        if (firstOrNot)
        {
            return skillDebrisNum_first;
        }
        else
        {
            return (int)(skillDebrisNum * factor);
        }
    }
    //获得技能碎片数量[未通关，固定为非首通]
    public int GetNotPassSkillDebrisNum()
    {
        Random random = new Random();
        int min = 95, max = 105;
        float factor = random.Next(min, max) / (float)100;
        return (int)(skillDebrisNum * factor);
    }

    //获得经验值
    public int GetExp()
    {
        if (firstOrNot)
        {
            return exp_first;
        }
        else
        {
            return exp;
        }
    }
    //获得经验值[未通关，固定为非首通]
    public int GetNotPassExp()
    {
        return exp;
    }

    //以下函数用于辅助储存关卡信息，仅在写出csv时使用
    public string GetSaveLevel()
    {
        return level.ToString();
    }
    public string GetSaveFON()
    {
        if (firstOrNot)
        {
            return "是";
        }
        else
        {
            return "否";
        }
    }
    public string GetSaveChipType()
    {
        if (chipNum == 0)
        {
            return "";
        }
        switch (chipType)
        {
            case Quality.Normal:
                return "普通";
            case Quality.Excellent:
                return "精良";
            case Quality.Rare:
                return "稀有";
            case Quality.Epic:
                return "史诗";
        }
        return "";
    }
    public string GetSaveChipNum()
    {
        return chipNum.ToString();
    }
    public string GetSaveRareNum()
    {
        return rareEarthNum.ToString();
    }
    public string GetSaveExp()
    {
        return exp.ToString();
    }
    public string GetSaveChipTypeF()
    {
        if (chipNum_first == 0)
        {
            return "";
        }
        switch (chipType_first)
        {
            case Quality.Normal:
                return "普通";
            case Quality.Excellent:
                return "精良";
            case Quality.Rare:
                return "稀有";
            case Quality.Epic:
                return "史诗";
        }
        return "";
    }
    public string GetSaveChipNumF()
    {
        return chipNum_first.ToString();
    }
    public string GetSaveRareNumF()
    {
        return rareEarthNum_first.ToString();
    }
    public string GetSaveExpF()
    {
        return exp_first.ToString();
    }
    public string GetSaveSDNum()
    {
        return skillDebrisNum.ToString();
    }
    public string GetSaveSDNumF()
    {
        return skillDebrisNum_first.ToString();
    }
    public bool GetFirstOrNot()
    {
        return firstOrNot;
    }
}
