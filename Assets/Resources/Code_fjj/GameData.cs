using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



/// <summary>
/// 用于将csv文件中的数据转化为游戏内的可使用数据
/// </summary>
public class GameData : MonoBehaviour
{
    private Attribute attribute_Buf;  //角色属性缓存，用于判断角色属性(GameScript.GameRoleAttribute)是否需要更新
    private ReserveAttribute reserveAttribute_Buf;  //角色属性加成缓存，用于判断角色属性加成是否需要更新

    //获取StreamingAssets下文件的路径，未使用到
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
    //获得Application.persistentDataPath下文件的路径
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

    //储存角色技能数据为csv
    public void SkillDataSave()
    {
        using (CsvFileWriter writer = new CsvFileWriter(GetPersistentFilePath("Data/SkillData.csv")))
        {
            CsvRow skillRow = new CsvRow();  //初始化表头
            skillRow.Add("名称");
            skillRow.Add("介绍");
            skillRow.Add("等级");
            skillRow.Add("等级上限");
            skillRow.Add("图标路径");
            writer.WriteRow(skillRow);
            for (int i = 0; i < DataManager.SkillData.Length; i++)  //写出数据
            {
                CsvRow row = new CsvRow();
                row.Add(DataManager.SkillData[i].GetName());  //技能姓名
                row.Add(DataManager.SkillData[i].GetMessage());  //技能简介
                row.Add(DataManager.SkillData[i].GetLevel().ToString());  //技能等级
                row.Add(DataManager.SkillData[i].GetLevelLimit().ToString());  //技能等级上限
                row.Add(DataManager.SkillData[i].GetImagePath());  //技能图标路径
                writer.WriteRow(row);
            }
        }
    }
    //储存角色数值为csv
    public void RoleSave()
    {
        using (CsvFileWriter writer = new CsvFileWriter(GetPersistentFilePath("Data/Role.csv")))
        {
            CsvRow roleRow = new CsvRow();  //初始化表头
            roleRow.Add("角色名称");
            roleRow.Add("等级");
            roleRow.Add("经验");
            roleRow.Add("主武器ID");
            roleRow.Add("主武器等级");
            roleRow.Add("副武器ID");
            roleRow.Add("副武器等级");
            roleRow.Add("护甲ID");
            roleRow.Add("护甲等级");
            roleRow.Add("头盔ID");
            roleRow.Add("头盔等级");
            roleRow.Add("剩余必杀次数");
            roleRow.Add("稀土");
            roleRow.Add("技能碎片");
            writer.WriteRow(roleRow);
            CsvRow row = new CsvRow();  //目前未设置多角色，不使用for语句，写出
            row.Add(DataManager.roleAttribute.GetName());  //角色名称
            row.Add(DataManager.roleAttribute.GetLevel().ToString());  //角色等级
            row.Add(DataManager.roleAttribute.GetExp().ToString());  //角色经验

            if (DataManager.roleEquipment.HadMainWeapon())  //当角色拥有装备时写出，否则为空
            {
                row.Add(DataManager.roleEquipment.GetMainWeapon().item.GetID().ToString());  //主武器ID
                row.Add(DataManager.roleEquipment.GetMainWeapon().level.ToString());  //主武器等级
            }
            else
            {
                row.Add("");
                row.Add("");
            }
            if (DataManager.roleEquipment.HadAlternateWeapon())  //同主武器，不做赘述
            {
                row.Add(DataManager.roleEquipment.GetAlternateWeapon().item.GetID().ToString());
                row.Add(DataManager.roleEquipment.GetAlternateWeapon().level.ToString());
            }
            else
            {
                row.Add("");
                row.Add("");
            }
            if (DataManager.roleEquipment.HadCuirass())
            {
                row.Add(DataManager.roleEquipment.GetCuirass().item.GetID().ToString());
                row.Add(DataManager.roleEquipment.GetCuirass().level.ToString());
            }
            else
            {
                row.Add("");
                row.Add("");
            }
            if (DataManager.roleEquipment.HadHelm())
            {
                row.Add(DataManager.roleEquipment.GetHelm().item.GetID().ToString());
                row.Add(DataManager.roleEquipment.GetHelm().level.ToString());
            }
            else
            {
                row.Add("");
                row.Add("");
            }

            row.Add(DataManager.roleEquipment.GetSlayCount().ToString());  //剩余必杀次数
            row.Add(DataManager.roleEquipment.GetRareEarthCount().ToString());  //拥有稀土数量
            row.Add(DataManager.roleEquipment.GetSkillDebris().ToString());  //拥有技能碎片数量
            writer.WriteRow(row);
        }
    }
    //储存角色背包数据为csv
    public void BagSave()
    {
        using (CsvFileWriter writer = new CsvFileWriter(GetPersistentFilePath("Data/Bag.csv")))
        {
            CsvRow bagRow = new CsvRow();  //初始化表头
            bagRow.Add("ID");
            bagRow.Add("等级");
            bagRow.Add("数量");
            writer.WriteRow(bagRow);
            for (int i = 0; i < DataManager.bag.GetItemBag().Count; i++)  //写出数据
            {
                CsvRow row = new CsvRow();
                row.Add(DataManager.bag.GetItemBag()[i].item.GetID().ToString());  //背包物品ID
                row.Add(DataManager.bag.GetItemBag()[i].level.ToString());  //背包物品等级
                row.Add(DataManager.bag.GetItemBag()[i].count.ToString());  //背包物品数量
                writer.WriteRow(row);
            }
        }
    }

    //储存抽奖系统数据为csv
    public void LottertySave()
    {
        using (CsvFileWriter writer = new CsvFileWriter(GetPersistentFilePath("Data/Lotterty.csv")))
        {
            CsvRow lottertyRow = new CsvRow();  //初始化表头
            lottertyRow.Add("奖池");
            lottertyRow.Add("时");
            lottertyRow.Add("分");
            lottertyRow.Add("秒");
            writer.WriteRow(lottertyRow);
            CsvRow normalRow = new CsvRow();  //写出低级抽奖数据
            normalRow.Add("低级");
            normalRow.Add(GameScript.NormalPool.TimeOfDay.Hours.ToString());  //低级抽奖所需等待的时
            normalRow.Add(GameScript.NormalPool.TimeOfDay.Minutes.ToString());  //低级抽奖所需等待的分
            normalRow.Add(GameScript.NormalPool.TimeOfDay.Seconds.ToString());  //低级抽奖所需等待的秒
            writer.WriteRow(normalRow);
            CsvRow goodRow = new CsvRow();  //写出高级抽奖数据
            goodRow.Add("高级");
            goodRow.Add(GameScript.GoodPool.TimeOfDay.Hours.ToString());  //高级抽奖所需等待的时
            goodRow.Add(GameScript.GoodPool.TimeOfDay.Minutes.ToString());  //高级抽奖所需等待的分
            goodRow.Add(GameScript.GoodPool.TimeOfDay.Seconds.ToString());  //高级抽奖所需等待的秒
            writer.WriteRow(goodRow);
        }
    }

    //储存关卡数据为csv
    public void LevelOutputSave()
    {
        using (CsvFileWriter writer = new CsvFileWriter(GetPersistentFilePath("Data/LevelOutput.csv")))
        {
            CsvRow levelOutputRow = new CsvRow();  //初始化表头
            levelOutputRow.Add("关卡");
            levelOutputRow.Add("是否首通");
            levelOutputRow.Add("碎片");
            levelOutputRow.Add("碎片数量");
            levelOutputRow.Add("稀土数量");
            levelOutputRow.Add("经验值");
            levelOutputRow.Add("首通碎片");
            levelOutputRow.Add("首通碎片数量");
            levelOutputRow.Add("首通稀土数量");
            levelOutputRow.Add("首通经验值");
            levelOutputRow.Add("技能碎片数量");
            levelOutputRow.Add("首通技能碎片数量");
            writer.WriteRow(levelOutputRow);
            for (int i = 0; i < GameScript.LevelOutput.Count; i++)  //写出数据
            {
                CsvRow row = new CsvRow();
                row.Add(GameScript.LevelOutput[i].GetSaveLevel());  //关卡序号[以下使用函数详见Chip.cs]
                row.Add(GameScript.LevelOutput[i].GetSaveFON());  //是否首通
                row.Add(GameScript.LevelOutput[i].GetSaveChipType());  //非首通装备碎片品质
                row.Add(GameScript.LevelOutput[i].GetSaveChipNum());  //非首通装备碎片数量
                row.Add(GameScript.LevelOutput[i].GetSaveRareNum());  //非首通稀土数量
                row.Add(GameScript.LevelOutput[i].GetSaveExp());  //经验数量
                row.Add(GameScript.LevelOutput[i].GetSaveChipTypeF());  //首通装备碎片品质
                row.Add(GameScript.LevelOutput[i].GetSaveChipNumF());  //首通装备碎片数量
                row.Add(GameScript.LevelOutput[i].GetSaveRareNumF());  //首通稀土数量
                row.Add(GameScript.LevelOutput[i].GetSaveExpF());  //首通经验值
                row.Add(GameScript.LevelOutput[i].GetSaveSDNum());  //非首通技能碎片数量
                row.Add(GameScript.LevelOutput[i].GetSaveSDNumF());  //首通技能碎片数量
                writer.WriteRow(row);
            }
        }
    }

    //储存游戏结束时间为csv，用于辅助抽奖系统、体力系统
    public void ExitTimeSave()
    {
        using (CsvFileWriter writer = new CsvFileWriter(GetPersistentFilePath("Data/ExitTime.csv")))
        {
            CsvRow ExitTimeRow = new CsvRow();  //直接写出结束时间
            ExitTimeRow.Add(GameScript.DisableTime.ToString());
            writer.WriteRow(ExitTimeRow);
        }
    }

    //储存体力数据为csv
    public void VITSave()
    {
        using (CsvFileWriter writer = new CsvFileWriter(GetPersistentFilePath("Data/VIT.csv")))
        {
            CsvRow VITRow = new CsvRow();  //初始化表头
            VITRow.Add("体力");
            VITRow.Add("每体力恢复所需分钟");
            writer.WriteRow(VITRow);
            CsvRow row = new CsvRow();
            row.Add(GameScript.VIT.ToString());  //拥有体力
            row.Add(GameScript.PerVITMinute.ToString());  //体力恢复速度(每体力恢复所需分钟)
            writer.WriteRow(row);
        }
    }

    //用于储存的函数的集成
    public void GameSave()
    {
        SkillDataSave();
        RoleSave();
        BagSave();
        LottertySave();
        LevelOutputSave();
        ExitTimeSave();
        VITSave();
    }

    //当脚本被激活时执行如下
    private void OnEnable()
    {
        SelfUpdate();  //更新检测及更新
        if (GameScript.IfInit)  //如果不是首次加载主界面
        {
            //根据离开主界面的时间与当前时间的差更新抽奖计时
            if ((DateTime.Now - GameScript.DisableTime) > GameScript.NormalPool.TimeOfDay)
            {
                GameScript.NormalPool = GameScript.NormalPool.Date;
            }
            else
            {
                GameScript.NormalPool = GameScript.NormalPool.AddHours(-(DateTime.Now - GameScript.DisableTime).Hours);
                GameScript.NormalPool = GameScript.NormalPool.AddMinutes(-(DateTime.Now - GameScript.DisableTime).Minutes);
                GameScript.NormalPool = GameScript.NormalPool.AddSeconds(-(DateTime.Now - GameScript.DisableTime).Seconds);
            }
            if ((DateTime.Now - GameScript.DisableTime) > GameScript.GoodPool.TimeOfDay)
            {
                GameScript.GoodPool = GameScript.GoodPool.Date;
            }
            else
            {
                GameScript.GoodPool = GameScript.GoodPool.AddHours(-(DateTime.Now - GameScript.DisableTime).Hours);
                GameScript.GoodPool = GameScript.GoodPool.AddMinutes(-(DateTime.Now - GameScript.DisableTime).Minutes);
                GameScript.GoodPool = GameScript.GoodPool.AddSeconds(-(DateTime.Now - GameScript.DisableTime).Seconds);
            }

            //根据离开主界面的时间与当前时间的差更新体力计时
            if ((DateTime.Now - GameScript.DisableTime) > GameScript.VITCounter.TimeOfDay)
            {
                TimeSpan Buf = DateTime.Now - GameScript.DisableTime;
                TimeSpan PerVIT = DateTime.Now.Date.AddMinutes(GameScript.PerVITMinute).TimeOfDay;
                while (Buf >= PerVIT)
                {
                    if (GameScript.VIT == 24)
                    {
                        break;
                    }
                    GameScript.VIT++;
                    Buf = Buf.Add(PerVIT.Negate());
                }
                if (Buf > GameScript.VITCounter.TimeOfDay)
                {
                    if (GameScript.VIT == 24)
                    {
                    }
                    else
                    {
                        GameScript.VIT++;
                        Buf = Buf.Add(GameScript.VITCounter.TimeOfDay.Negate());
                        GameScript.VITCounter = GameScript.VITCounter.Date.AddMinutes(GameScript.PerVITMinute).Add(Buf.Negate());
                    }
                }
                else
                {
                    GameScript.VITCounter = GameScript.VITCounter.Add(Buf.Negate());
                }
            }
            else
            {
                GameScript.VITCounter = GameScript.VITCounter.Add((DateTime.Now - GameScript.DisableTime).Negate());
            }
        }
        GameScript.IfInit = true;  //初始化为false,首次加载后设为true
        GameSave();  //对游戏数据进行一次储存，保证数据储存的时效性
    }

    //每帧进行检测
    private void Update()
    {
        //下方注释为局外Buff类型技能的储存，由于并未设计局外Buff类型技能，故废除
        //Skill[] BuffSkillList = new Skill[1];
        //BuffSkillList[0] = DataManager.SkillData[13];

        //根据角色属性加成缓存与实时的角色属性加成进行比较，当不相同时对缓存进行更新，进而对GameScript.GameRoleAttribute进行更新
        if (!ReserveAttribute.ReserveAttributeCompare(reserveAttribute_Buf, DataManager.roleEquipment.GetEquipmentListReserveAttribute() + ReserveAttribute.StandardReserveAttribute()))//Skill.GetSkillListAttributer(BuffSkillList)))
        {
            SelfUpdate();
            return;
        }
        //根据角色属性缓存与实时的角色属性进行比较，当不相同时对缓存进行更新，进而对GameScript.GameRoleAttribute进行更新
        if (!Attribute.AttributeCompare(attribute_Buf, DataManager.roleAttribute.GetAttribute()))
        {
            SelfUpdate();
            return;
        }
    }

    //自我更新
    public void SelfUpdate()
    {
        reserveAttribute_Buf = DataManager.roleEquipment.GetEquipmentListReserveAttribute();  //暂存角色装备属性加成

        //以下注释内容原因同上，无局外Buff类型技能，故不计入技能属性加成
        //Skill[] BuffSkillList = new Skill[1];
        //BuffSkillList[0] = DataManager.SkillData[13];

        reserveAttribute_Buf = reserveAttribute_Buf + ReserveAttribute.StandardReserveAttribute();  //初始化或更新角色属性加成缓存  //Skill.GetSkillListAttributer(BuffSkillList);
        attribute_Buf = DataManager.roleAttribute.GetAttribute();  //初始化或更新角色属性缓存
        GameScript.GameRoleAttribute = ReserveAttribute.ReserverToService(reserveAttribute_Buf, attribute_Buf);  //更新GameScript.GameRoleAttribute,即更新角色属性
    }

    //当脚本取消激活时
    private void OnDisable()
    {
        GameScript.DisableTime = DateTime.Now;  //更新离开时间
        GameSave();  //储存游戏数据
    }

    //当游戏暂停(切入后台/离开游戏)时
    private void OnApplicationPause(bool pause)
    {
        if (pause)  //当离开游戏
        {
            GameScript.DisableTime = DateTime.Now;  //更新离开时间
            GameSave();  //储存游戏数据
        }
        else  //未结束游戏，将游戏重新切回前台
        {
            if (GameScript.IfInit)  //与OnEnable()中相同，更新抽奖计时、体力计时
            {
                if ((DateTime.Now - GameScript.DisableTime) > GameScript.NormalPool.TimeOfDay)
                {
                    GameScript.NormalPool = GameScript.NormalPool.Date;
                }
                else
                {
                    GameScript.NormalPool = GameScript.NormalPool.AddHours(-(DateTime.Now - GameScript.DisableTime).Hours);
                    GameScript.NormalPool = GameScript.NormalPool.AddMinutes(-(DateTime.Now - GameScript.DisableTime).Minutes);
                    GameScript.NormalPool = GameScript.NormalPool.AddSeconds(-(DateTime.Now - GameScript.DisableTime).Seconds);
                }
                if ((DateTime.Now - GameScript.DisableTime) > GameScript.GoodPool.TimeOfDay)
                {
                    GameScript.GoodPool = GameScript.GoodPool.Date;
                }
                else
                {
                    GameScript.GoodPool = GameScript.GoodPool.AddHours(-(DateTime.Now - GameScript.DisableTime).Hours);
                    GameScript.GoodPool = GameScript.GoodPool.AddMinutes(-(DateTime.Now - GameScript.DisableTime).Minutes);
                    GameScript.GoodPool = GameScript.GoodPool.AddSeconds(-(DateTime.Now - GameScript.DisableTime).Seconds);
                }

                if ((DateTime.Now - GameScript.DisableTime) > GameScript.VITCounter.TimeOfDay)
                {
                    TimeSpan Buf = DateTime.Now - GameScript.DisableTime;
                    TimeSpan PerVIT = DateTime.Now.Date.AddMinutes(GameScript.PerVITMinute).TimeOfDay;
                    while (Buf >= PerVIT)
                    {
                        if (GameScript.VIT == 24)
                        {
                            break;
                        }
                        GameScript.VIT++;
                        Buf = Buf.Add(PerVIT.Negate());
                    }
                    if (Buf > GameScript.VITCounter.TimeOfDay)
                    {
                        if (GameScript.VIT == 24)
                        {
                        }
                        else
                        {
                            GameScript.VIT++;
                            Buf = Buf.Add(GameScript.VITCounter.TimeOfDay.Negate());
                            GameScript.VITCounter = GameScript.VITCounter.Date.AddMinutes(GameScript.PerVITMinute).Add(Buf.Negate());
                        }
                    }
                    else
                    {
                        GameScript.VITCounter = GameScript.VITCounter.Add(Buf.Negate());
                    }
                }
                else
                {
                    GameScript.VITCounter = GameScript.VITCounter.Add((DateTime.Now - GameScript.DisableTime).Negate());
                }
            }
        }
    }
}
