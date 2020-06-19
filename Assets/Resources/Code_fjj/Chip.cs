using System;

public class Chip
{
    private int level;
    private bool firstOrNot;
    private Quality chipType;
    private int chipNum;
    private Quality chipType_first;
    private int chipNum_first;
    private int rareEarthNum;
    private int rareEarthNum_first;
    private int exp;

    public Chip(int l, bool fON, Quality cT, int cN, int rEN, int e, Quality cTF, int cNF, int rEF)
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
    }

    public void SetFirstOrNot(bool b)
    {
        firstOrNot = b;
    }

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
 
    public int GetChipNum()
    {
        Random random = new Random();
        int min = 95, max = 105;
        float factor = random.Next(min, max) / (float)100;
        if (firstOrNot)
        {
            return (int)(chipNum_first * factor);
        }
        else
        {
            return (int)(chipNum * factor);
        }
    }

    public int GetRareEarthNum()
    {
        Random random = new Random();
        int min = 95, max = 105;
        float factor = random.Next(min, max) / (float)100;
        if (firstOrNot)
        {
            return (int)(rareEarthNum_first * factor);
        }
        else
        {
            return (int)(rareEarthNum * factor);
        }
    }

    public int GetExp()
    {
        return exp;
    }

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
}
