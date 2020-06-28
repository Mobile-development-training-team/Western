using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScript
{
    public static GameObject[] EquipmentModel;  //角色装备模型
    public static GameObject[] MainWeaponModelIndex;  //游戏主武器模型
    public static GameObject[] AlternateWeaponModelIndex;  //游戏副武器模型
    public static GameObject[] CuirassModelIndex;  //游戏护甲模型
    public static GameObject[] HelmModelIndex;  //游戏头盔模型

    public static DataManager dataManager;  //游戏数据管理类
    public static Attribute GameRoleAttribute;  //角色属性，用于与战斗对接
    public static DateTime NormalPool;  //低级抽奖计时
    public static DateTime GoodPool;  //高级抽奖计时

    public static List<Chip> LevelOutput;  //关卡产出列表
    public static Item[] NormalDebrisPool;  //普通装备碎片池
    public static Item[] ExcellentDebrisPool;  //精良装备碎片池
    public static Item[] RareDebrisPool;  //稀有装备碎片池
    public static Item[] EpicDebrisPool;  //史诗装备碎片池

    //关卡产出结果的保存，用于在通关时显示产出
    public struct PassResult
    {
        public BagItem Chip;
        public int RareEarth;
        public int Exp;
        public int SkillDebris;
    }
    //当通关时，获得对应关卡产出，参数中level为关卡序号-1
    public static PassResult LevelPass(int level)
    {
        PassResult prBuf = new PassResult();
        prBuf.Chip = new BagItem();
        prBuf.Chip.item = new Item();
        System.Random rA = new System.Random();
        int rANext = rA.Next() % 4;
        int cnBuf = LevelOutput[level].GetChipNum();  //获取装备碎片数量
        if (cnBuf > 0)
        {
            switch (LevelOutput[level].GetChipQuality())  //加入到背包中,并储存装备碎片到结果中
            {
                case Quality.Normal:
                    DataManager.bag.AddItem(NormalDebrisPool[rANext], cnBuf);
                    prBuf.Chip.item = NormalDebrisPool[rANext];  //储存种类
                    prBuf.Chip.count = cnBuf;  //储存数量
                    break;
                case Quality.Excellent:
                    DataManager.bag.AddItem(ExcellentDebrisPool[rANext], cnBuf);
                    prBuf.Chip.item = ExcellentDebrisPool[rANext];
                    prBuf.Chip.count = cnBuf;
                    break;
                case Quality.Rare:
                    DataManager.bag.AddItem(RareDebrisPool[rANext], cnBuf);
                    prBuf.Chip.item = RareDebrisPool[rANext];
                    prBuf.Chip.count = cnBuf;
                    break;
                case Quality.Epic:
                    DataManager.bag.AddItem(EpicDebrisPool[rANext], cnBuf);
                    prBuf.Chip.item = EpicDebrisPool[rANext];
                    prBuf.Chip.count = cnBuf;
                    break;
            }
        }
        //获得稀土，对拥有稀土数量进行更新，并储存结果
        int rareBuf = LevelOutput[level].GetRareEarthNum();
        DataManager.roleEquipment.SetRareEarthCount(DataManager.roleEquipment.GetRareEarthCount() + rareBuf);
        prBuf.RareEarth = rareBuf;
        //获得经验，对经验值进行更新，并储存结果
        int expBuf = LevelOutput[level].GetExp();
        DataManager.roleAttribute.ExpUP(expBuf);
        prBuf.Exp = expBuf;
        //获得技能碎片，对拥有技能碎片数量进行更新，并储存结果
        int dbBuf = LevelOutput[level].GetSkillDebrisNum();
        DataManager.roleEquipment.SetSkillDebris(DataManager.roleEquipment.GetSkillDebris() + dbBuf);
        prBuf.SkillDebris = dbBuf;
        //更新首通标示，即已通关过，不再是首通
        LevelOutput[level].SetFirstOrNot(false);
        //返回产出结果
        return prBuf;
    }
    //当未能通关时，获得对应关卡产出，参数中course为通关进程
    public static PassResult LevelNotPass(int level, double course)
    {
        PassResult prBuf = new PassResult();
        prBuf.Chip = new BagItem();
        prBuf.Chip.item = new Item();
        System.Random rA = new System.Random();
        int rANext = rA.Next() % 4;
        // 产出固定为非首通，并根据通关进程进行产出给予
        int cnBuf = (int)(LevelOutput[level].GetNotPassChipNum() * course);
        if (cnBuf > 0)
        {
            switch (LevelOutput[level].GetNotPassChipQuality())
            {
                case Quality.Normal:
                    DataManager.bag.AddItem(NormalDebrisPool[rANext], cnBuf);
                    prBuf.Chip.item = NormalDebrisPool[rANext];
                    prBuf.Chip.count = cnBuf;
                    break;
                case Quality.Excellent:
                    DataManager.bag.AddItem(ExcellentDebrisPool[rANext], cnBuf);
                    prBuf.Chip.item = ExcellentDebrisPool[rANext];
                    prBuf.Chip.count = cnBuf;
                    break;
                case Quality.Rare:
                    DataManager.bag.AddItem(RareDebrisPool[rANext], cnBuf);
                    prBuf.Chip.item = RareDebrisPool[rANext];
                    prBuf.Chip.count = cnBuf;
                    break;
                case Quality.Epic:
                    DataManager.bag.AddItem(EpicDebrisPool[rANext], cnBuf);
                    prBuf.Chip.item = EpicDebrisPool[rANext];
                    prBuf.Chip.count = cnBuf;
                    break;
            }
        }
        int rareBuf = (int)(LevelOutput[level].GetNotPassRareEarthNum() * course);
        DataManager.roleEquipment.SetRareEarthCount(DataManager.roleEquipment.GetRareEarthCount() + rareBuf);
        prBuf.RareEarth = rareBuf;
        int expBuf = (int)(LevelOutput[level].GetNotPassExp() * course);
        DataManager.roleAttribute.ExpUP(expBuf);
        prBuf.Exp = expBuf;
        int dbBuf = (int)(LevelOutput[level].GetNotPassSkillDebrisNum() * course);
        DataManager.roleEquipment.SetSkillDebris(DataManager.roleEquipment.GetSkillDebris() + dbBuf);
        prBuf.SkillDebris = dbBuf;
        return prBuf;
    }

    public static DateTime DisableTime;  //离开游戏(主界面)时间
    public static bool IfInit = false;  //数据是否初始化

    public static int VIT;  //体力值
    public static double PerVITMinute;  //体力恢复所需分钟
    public static DateTime VITCounter;  //体力计时器

    //用于将本地文件中的png转化为Sprite
    public static Sprite GetSprite(string path)
    {
        string texturePath;  //获得文件位置
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
        byte[] pBytes = new byte[fs.Length];  //读取文件流
        fs.Read(pBytes, 0, (int)fs.Length);
        fs.Close();
        fs.Dispose();
        fs = null;
        Texture2D pBuf = new Texture2D(64, 64);  //转化流为Texture2D
        pBuf.LoadImage(pBytes);
        //将Texture2D转化为Sprite
        return Sprite.Create(pBuf, new Rect(0, 0, pBuf.width, pBuf.height), Vector2.zero);
    }

    public static Sprite QualityNormal;  //普通品质对应Sprite
    public static Sprite QualityExcellent;  //精良品质对应Sprite
    public static Sprite QualityRare;  //稀有品质对应Sprite
    public static Sprite QualityEpic;  //史诗品质对应Sprite
    public static Sprite SlaySprite;  //必杀图标对应Sprite
    public static Sprite RareEarthSprite;  //稀土对应Sprite
    public static Sprite SkillDebrisSprite;  //技能碎片对应Sprite

    //根据Quality，即物品品质获得对应品质Sprite
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

    //用于读入数据、初始化QualityNormal等品质Sprite的辅助函数
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

    //减少体力，并做能否减少的判断，用于与关卡选择对接
    public static bool ReduceVIT(int vit)
    {
        if (VIT == 24)
        {
            VIT -= vit;
            VITScript.ReStart = true;
            return true;
        }
        if (VIT >= vit)
        {
            VIT -= vit;
            return true;
        }
        else
        {
            return false;
        }
    }
    
}
