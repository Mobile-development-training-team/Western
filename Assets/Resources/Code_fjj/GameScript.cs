using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScript
{
    public static DataManager dataManager;
    public static Attribute GameRoleAttribute;
    public static DateTime NormalPool;
    public static DateTime GoodPool;

    public static List<Chip> LevelOutput;
    public static Item[] NormalDebrisPool;
    public static Item[] ExcellentDebrisPool;
    public static Item[] RareDebrisPool;
    public static Item[] EpicDebrisPool;

    public static DateTime DisableTime;
    public static bool IfInit = false;

    public static Sprite GetSprite(string path)
    {
        string texturePath;
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
        byte[] pBytes = new byte[fs.Length];
        fs.Read(pBytes, 0, (int)fs.Length);
        fs.Close();
        fs.Dispose();
        fs = null;
        Texture2D pBuf = new Texture2D(64, 64);
        pBuf.LoadImage(pBytes);
        return Sprite.Create(pBuf, new Rect(0, 0, pBuf.width, pBuf.height), Vector2.zero);
    }

    public static Sprite QualityNormal;
    public static Sprite QualityExcellent;
    public static Sprite QualityRare;
    public static Sprite QualityEpic;
    public static Sprite RareEarthSprite;
    public static Sprite SkillDebrisSprite;

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


    
}
