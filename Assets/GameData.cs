using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameData : MonoBehaviour
{
    private Attribute attribute_Buf;
    private ReserveAttribute reserveAttribute_Buf;

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

    public void SkillDataSave()
    {
        using (CsvFileWriter writer = new CsvFileWriter(GetPersistentFilePath("Data/SkillData.csv")))
        {
            CsvRow skillRow = new CsvRow();
            skillRow.Add("名称");
            skillRow.Add("介绍");
            skillRow.Add("等级");
            skillRow.Add("等级上限");
            skillRow.Add("图标路径");
            writer.WriteRow(skillRow);
            for (int i = 0; i < DataManager.SkillData.Length; i++)
            {
                CsvRow row = new CsvRow();
                row.Add(DataManager.SkillData[i].GetName());
                row.Add(DataManager.SkillData[i].GetMessage());
                row.Add(DataManager.SkillData[i].GetLevel().ToString());
                row.Add(DataManager.SkillData[i].GetLevelLimit().ToString());
                row.Add(DataManager.SkillData[i].GetImagePath());
                writer.WriteRow(row);
            }
        }
    }
    public void RoleSave()
    {
        using (CsvFileWriter writer = new CsvFileWriter(GetPersistentFilePath("Data/Role.csv")))
        {
            CsvRow roleRow = new CsvRow();
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
            CsvRow row = new CsvRow();
            row.Add(DataManager.roleAttribute.GetName());
            row.Add(DataManager.roleAttribute.GetLevel().ToString());
            row.Add(DataManager.roleAttribute.GetExp().ToString());
            if (DataManager.roleEquipment.HadMainWeapon())
            {
                row.Add(DataManager.roleEquipment.GetMainWeapon().item.GetID().ToString());
                row.Add(DataManager.roleEquipment.GetMainWeapon().level.ToString());
            }
            else
            {
                row.Add("");
                row.Add("");
            }
            if (DataManager.roleEquipment.HadAlternateWeapon())
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
            row.Add(DataManager.roleEquipment.GetSlayCount().ToString());
            row.Add(DataManager.roleEquipment.GetRareEarthCount().ToString());
            row.Add(DataManager.roleEquipment.GetSkillDebris().ToString());
            writer.WriteRow(row);
        }
    }
    public void BagSave()
    {
        using (CsvFileWriter writer = new CsvFileWriter(GetPersistentFilePath("Data/Bag.csv")))
        {
            CsvRow bagRow = new CsvRow();
            bagRow.Add("ID");
            bagRow.Add("等级");
            bagRow.Add("数量");
            writer.WriteRow(bagRow);
            for (int i = 0; i < DataManager.bag.GetItemBag().Count; i++)
            {
                CsvRow row = new CsvRow();
                row.Add(DataManager.bag.GetItemBag()[i].item.GetID().ToString());
                row.Add(DataManager.bag.GetItemBag()[i].level.ToString());
                row.Add(DataManager.bag.GetItemBag()[i].count.ToString());
                writer.WriteRow(row);
            }
        }
    }

    public void LottertySave()
    {
        using (CsvFileWriter writer = new CsvFileWriter(GetPersistentFilePath("Data/Lotterty.csv")))
        {
            CsvRow lottertyRow = new CsvRow();
            lottertyRow.Add("奖池");
            lottertyRow.Add("时");
            lottertyRow.Add("分");
            lottertyRow.Add("秒");
            writer.WriteRow(lottertyRow);
            CsvRow normalRow = new CsvRow();
            normalRow.Add("低级");
            normalRow.Add(GameScript.NormalPool.TimeOfDay.Hours.ToString());
            normalRow.Add(GameScript.NormalPool.TimeOfDay.Minutes.ToString());
            normalRow.Add(GameScript.NormalPool.TimeOfDay.Seconds.ToString());
            writer.WriteRow(normalRow);
            CsvRow goodRow = new CsvRow();
            goodRow.Add("高级");
            goodRow.Add(GameScript.GoodPool.TimeOfDay.Hours.ToString());
            goodRow.Add(GameScript.GoodPool.TimeOfDay.Minutes.ToString());
            goodRow.Add(GameScript.GoodPool.TimeOfDay.Seconds.ToString());
            writer.WriteRow(goodRow);
        }
    }
    public void LevelOutputSave()
    {
        using (CsvFileWriter writer = new CsvFileWriter(GetPersistentFilePath("Data/LevelOutput.csv")))
        {
            CsvRow levelOutputRow = new CsvRow();
            levelOutputRow.Add("关卡");
            levelOutputRow.Add("是否首通");
            levelOutputRow.Add("碎片");
            levelOutputRow.Add("碎片数量");
            levelOutputRow.Add("稀土数量");
            levelOutputRow.Add("经验值");
            levelOutputRow.Add("首通碎片");
            levelOutputRow.Add("首通碎片数量");
            levelOutputRow.Add("首通稀土数量");
            writer.WriteRow(levelOutputRow);
            for (int i = 0; i < GameScript.LevelOutput.Count; i++)
            {
                CsvRow row = new CsvRow();
                row.Add(GameScript.LevelOutput[i].GetSaveLevel());
                row.Add(GameScript.LevelOutput[i].GetSaveFON());
                row.Add(GameScript.LevelOutput[i].GetSaveChipType());
                row.Add(GameScript.LevelOutput[i].GetSaveChipNum());
                row.Add(GameScript.LevelOutput[i].GetSaveRareNum());
                row.Add(GameScript.LevelOutput[i].GetSaveExp());
                row.Add(GameScript.LevelOutput[i].GetSaveChipTypeF());
                row.Add(GameScript.LevelOutput[i].GetSaveChipNumF());
                row.Add(GameScript.LevelOutput[i].GetSaveRareNumF());
                writer.WriteRow(row);
            }
        }
    }

    public void ExitTimeSave()
    {
        using (CsvFileWriter writer = new CsvFileWriter(GetPersistentFilePath("Data/ExitTime.csv")))
        {
            CsvRow ExitTimeRow = new CsvRow();
            ExitTimeRow.Add(GameScript.DisableTime.ToString());
            writer.WriteRow(ExitTimeRow);
        }
    }

    public void VITSave()
    {
        using (CsvFileWriter writer = new CsvFileWriter(GetPersistentFilePath("Data/VIT.csv")))
        {
            CsvRow VITRow = new CsvRow();
            VITRow.Add("体力");
            VITRow.Add("每体力恢复所需分钟");
            writer.WriteRow(VITRow);
            CsvRow row = new CsvRow();
            row.Add(GameScript.VIT.ToString());
            row.Add(GameScript.PerVITMinute.ToString());
            writer.WriteRow(row);
        }
    }

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

    private void OnEnable()
    {
        SelfUpdate();
        if (GameScript.IfInit)
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
                while (Buf.Minutes >= GameScript.PerVITMinute)
                {
                    if (GameScript.VIT == 999)
                    {
                        break;
                    }
                    GameScript.VIT++;
                    Buf = Buf.Add(DateTime.Now.Date.AddMinutes(GameScript.PerVITMinute).TimeOfDay.Negate());
                }
                if (Buf > GameScript.VITCounter.TimeOfDay)
                {
                    if (GameScript.VIT == 999)
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
        GameScript.IfInit = true;
        GameSave();
    }

    private void Update()
    {
        Skill[] BuffSkillList = new Skill[1];
        BuffSkillList[0] = DataManager.SkillData[13];
        if (!ReserveAttribute.ReserveAttributeCompare(reserveAttribute_Buf, DataManager.roleEquipment.GetEquipmentListReserveAttribute() + Skill.GetSkillListAttributer(BuffSkillList)))
        {
            SelfUpdate();
            return;
        }
        if (!Attribute.AttributeCompare(attribute_Buf, DataManager.roleAttribute.GetAttribute()))
        {
            SelfUpdate();
            return;
        }
    }

    public void SelfUpdate()
    {
        reserveAttribute_Buf = DataManager.roleEquipment.GetEquipmentListReserveAttribute();
        Skill[] BuffSkillList = new Skill[1];
        BuffSkillList[0] = DataManager.SkillData[13];
        reserveAttribute_Buf = reserveAttribute_Buf + Skill.GetSkillListAttributer(BuffSkillList);
        attribute_Buf = DataManager.roleAttribute.GetAttribute();
        GameScript.GameRoleAttribute = ReserveAttribute.ReserverToService(reserveAttribute_Buf, attribute_Buf);
    }

    private void OnDisable()
    {
        GameScript.DisableTime = DateTime.Now;
        GameSave();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            GameScript.DisableTime = DateTime.Now;
            GameSave();
        }
        else
        {
            if (GameScript.IfInit)
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
                    while (Buf.Minutes >= GameScript.PerVITMinute)
                    {
                        if (GameScript.VIT == 999)
                        {
                            break;
                        }
                        GameScript.VIT++;
                        Buf = Buf.Add(DateTime.Now.Date.AddMinutes(GameScript.PerVITMinute).TimeOfDay.Negate());
                    }
                    if (Buf > GameScript.VITCounter.TimeOfDay)
                    {
                        if (GameScript.VIT == 999)
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
