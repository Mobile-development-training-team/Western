using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

/// <summary>
/// 用于迁移游戏数据到设备本地
/// </summary>
public class GameInitScript : MonoBehaviour
{
    //以下对象储存用于在更换装备时对角色模型进行更新
    public GameObject MainWeaponModel1;  //导入、储存主武器1的模型
    public GameObject MainWeaponModel2;  //导入、储存主武器2的模型
    public GameObject MainWeaponModel3;  //导入、储存主武器3的模型
    public GameObject MainWeaponModel4;  //导入、储存主武器4的模型

    public GameObject AlternateWeaponModel1;  //导入、储存副武器1的模型
    public GameObject AlternateWeaponModel2;  //导入、储存副武器2的模型
    public GameObject AlternateWeaponModel3;  //导入、储存副武器3的模型
    public GameObject AlternateWeaponModel4;  //导入、储存副武器4的模型

    public GameObject CuirassModel1;  //导入、储存护甲1的模型
    public GameObject CuirassModel2;  //导入、储存护甲2的模型
    public GameObject CuirassModel3;  //导入、储存护甲3的模型
    public GameObject CuirassModel4;  //导入、储存护甲4的模型

    public GameObject HelmModel1;  //导入、储存头盔1的模型
    public GameObject HelmModel2;  //导入、储存头盔2的模型
    public GameObject HelmModel3;  //导入、储存头盔3的模型
    public GameObject HelmModel4;  //导入、储存头盔4的模型

    private void Awake()
    {
        GameDataInit();  //对游戏数据进行迁移
    }

    private void GameDataInit()
    {
        //对游戏是否已经迁移进行判断
        if (File.Exists(GetPersistentFilePath("Data/Role.csv")))  //已迁移
        {
            DataEnable();  //不做数据迁移，直接激活数据读入脚本及界面显示
        }
        else  //未迁移
        {
            Directory.CreateDirectory(GetPersistentFilePath("Data"));  //创建Data文件夹
            Directory.CreateDirectory(GetPersistentFilePath("UI"));  //创建UI文件夹
            StartCoroutine(DataMigration());  //对数据进行迁移
            Invoke("DataEnable", 1.2f);  //延迟数据读入脚本及界面显示的激活
        }
    }

    //数据迁移预处理
    private IEnumerator DataMigration()
    {
        string filePath = GetStreamFilePath("MigrationList.txt");  //迁移文件列表的路径
        UnityWebRequest file = UnityWebRequest.Get(filePath);  //获取迁移文件列表
        yield return file.SendWebRequest();
        if (file.isDone)  //当获取后
        {
            byte[] array = Encoding.UTF8.GetBytes(file.downloadHandler.text);  //获取文件流
            MemoryStream sBuf = new MemoryStream(array);
            StreamReader reader = new StreamReader(sBuf);
            string Buf;
            Buf = reader.ReadLine();
            while (Buf != null)  //根据列表提供路径
            {
                StartCoroutine(DataMigrationHelp(Buf));  //迁移文件
                Buf = reader.ReadLine();
            }
        }
    }
    //数据迁移
    private IEnumerator DataMigrationHelp(string path)
    {
        string filePath = GetStreamFilePath(path);  //迁移文件的路径
        UnityWebRequest file = UnityWebRequest.Get(filePath);  //获取迁移文件
        yield return file.SendWebRequest();
        string savePath = GetPersistentFilePath(path);  //储存文件的路径
        FileInfo t = new FileInfo(savePath);
        FileStream fs = t.Create();  //创建文件
        fs.Close();
        fs.Dispose();
        if (file.isDone)
        {
            File.WriteAllBytes(savePath, file.downloadHandler.data);  //写入文件
        }
    }

    //激活数据读入脚本及界面显示
    private void DataEnable()
    {
        GetComponent<Canvas>().enabled = false;  //取消“加载中”界面的显示
        if (!GameScript.IfInit)  //判断是否首次激活此脚本
        {
            GameLoad();  //如是，进行数据读入；如不是，则不重复进行数据读入
        }
        GameModelLoad();  //更新模型加载
        transform.parent.Find("GameData").gameObject.SetActive(true);  //激活数据读入脚本
        transform.parent.Find("NewMainUI").gameObject.SetActive(true);  //激活界面显示
    }

    private string GetStreamFilePath(string FileName)  //获取StreamingAssets下文件的路径
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
    private string GetPersistentFilePath(string FileName)  //获取Application.persistentDataPath下文件的路径
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

    //对游戏物品目录进行读入，即对DataManager.GameItemIndex[]进行赋值
    public void GameItemIndexLoad()
    {
        using (CsvFileReader reader = new CsvFileReader(GetPersistentFilePath("Data/GameItemIndex.csv")))  //从csv文件中读取数据
        {
            List<Item> buf = new List<Item>();  //用于暂存转化的读入内容
            CsvRow row = new CsvRow();
            bool first = true;  //判断是否表头
            while (reader.ReadRow(row))
            {
                if (first)  //去除表头
                {
                    first = false;
                    continue;
                }
                if (row[0] == "")  //判断结尾
                {
                    break;
                }
                bool equipmentOrNot;
                if (row[3] == "装备碎片")  //判断物品为装备还是装备碎片
                {
                    equipmentOrNot = false;  //不是装备
                }
                else
                {
                    equipmentOrNot = true;  //是装备
                }
                EquipmentType eT = EquipmentType.MainWeapon;  //储存读入的装备类型
                switch (row[3])
                {
                    case "主武器":
                        eT = EquipmentType.MainWeapon;  //是主武器
                        break;
                    case "副武器":
                        eT = EquipmentType.AlternateWeapon;  //是副武器
                        break;
                    case "护甲":
                        eT = EquipmentType.Cuirass;  //是护甲
                        break;
                    case "头盔":
                        eT = EquipmentType.Helm;  //是头盔
                        break;
                }
                //将读入的数据转化为Item类
                Item a = new Item(int.Parse(row[0]), row[1], row[2], equipmentOrNot, new ReserveAttribute { AddATK = int.Parse(row[4]), PlusATK = double.Parse(row[5]), AddHP = int.Parse(row[6]), PlusHP = double.Parse(row[7]), AddDEF = int.Parse(row[8]), PlusDEF = double.Parse(row[9]) }, eT, GameScript.GetEquipmentQuality(row[10]), row[11]);
                //添加至缓存中
                buf.Add(a);
            }
            DataManager.GameItemIndex = new Item[buf.Count];  //为DataManager.GameItemIndex[]申请内存空间
            for (int i = 0; i < DataManager.GameItemIndex.Length; i++)  //进行赋值
            {
                DataManager.GameItemIndex[i] = buf[i];
            }
        }
    }
    //对角色等级属性目录进行读入，即对DataManager.levelIndex[]进行赋值
    public void LevelIndexLoad()
    {
        using (CsvFileReader reader = new CsvFileReader(GetPersistentFilePath("Data/LevelIndex.csv")))
        {
            List<LevelIndex> buf = new List<LevelIndex>();
            CsvRow row = new CsvRow();
            bool first = true;
            while (reader.ReadRow(row))
            {
                if (first)
                {
                    first = false;
                    continue;
                }
                if (row[0] == "")
                {
                    break;
                }
                //将读入内容转化为LevelIndex结构体
                LevelIndex a = new LevelIndex { Level = int.Parse(row[0]), ExpLimit = int.Parse(row[1]), attribute = new Attribute { Attack = int.Parse(row[2]), HealthPointLimit = int.Parse(row[3]), Defence = int.Parse(row[4]) } };
                buf.Add(a);
            }
            DataManager.levelIndex = new LevelIndex[buf.Count];
            for (int i = 0; i < DataManager.levelIndex.Length; i++)
            {
                DataManager.levelIndex[i] = buf[i];
            }
        }
    }
    //对角色技能等级属性目录进行读入，即对DataManager.skillLevelIndex[]进行赋值
    public void SkillLevelIndexLoad()
    {
        using (CsvFileReader reader = new CsvFileReader(GetPersistentFilePath("Data/SkillLevelIndex.csv")))
        {
            List<SkillLevelIndex> buf = new List<SkillLevelIndex>();
            CsvRow row = new CsvRow();
            bool first = true;
            while (reader.ReadRow(row))
            {
                if (first)
                {
                    first = false;
                    continue;
                }
                if (row[0] == "")
                {
                    break;
                }
                //将读入内容转化为SkillLevelIndex结构体
                SkillLevelIndex a = new SkillLevelIndex { name = row[0], level = int.Parse(row[1]), reserveAttribute = new ReserveAttribute { AddATK = int.Parse(row[2]), PlusATK = double.Parse(row[3]), AddHP = int.Parse(row[4]), PlusHP = double.Parse(row[5]), AddDEF = int.Parse(row[6]), PlusDEF = double.Parse(row[7]) }, coolDown = double.Parse(row[8]), duration = double.Parse(row[9]) };
                buf.Add(a);
            }
            DataManager.skillLevelIndex = new SkillLevelIndex[buf.Count];
            for (int i = 0; i < DataManager.skillLevelIndex.Length; i++)
            {
                DataManager.skillLevelIndex[i] = buf[i];
            }
        }
    }
    //对角色技能升级消耗目录进行读入，即对DataManager.skillLevelUpIndex[]进行赋值
    public void SkillLevelUpIndexLoad()
    {
        using (CsvFileReader reader = new CsvFileReader(GetPersistentFilePath("Data/SkillLevelUpIndex.csv")))
        {
            List<SkillLevelUpIndex> buf = new List<SkillLevelUpIndex>();
            CsvRow row = new CsvRow();
            bool first = true;
            while (reader.ReadRow(row))
            {
                if (first)
                {
                    first = false;
                    continue;
                }
                if (row[0] == "")
                {
                    break;
                }
                //将读入的数据转化为SkillLevelUpIndex结构体
                SkillLevelUpIndex a = new SkillLevelUpIndex { Name = row[0], LearnCount = int.Parse(row[1]), UpCount = int.Parse(row[2]) };
                buf.Add(a);
            }
            DataManager.skillLevelUpIndex = new SkillLevelUpIndex[buf.Count];
            for (int i = 0; i < DataManager.skillLevelUpIndex.Length; i++)
            {
                DataManager.skillLevelUpIndex[i] = buf[i];
            }
        }
    }

    //对角色技能数据进行读入，即对DataManager.SkillData[]进行赋值
    public void SkillDataLoad()
    {
        using (CsvFileReader reader = new CsvFileReader(GetPersistentFilePath("Data/SkillData.csv")))
        {
            List<Skill> buf = new List<Skill>();
            CsvRow row = new CsvRow();
            bool first = true;
            while (reader.ReadRow(row))
            {
                if (first)
                {
                    first = false;
                    continue;
                }
                if (row[0] == "")
                {
                    break;
                }
                //冷却时间与持续时间为后续加入内容，故SkillDataPlus结构体为补丁性质
                List<SkillDataPlus> sdp_buf = new List<SkillDataPlus>();
                for (int i = 0; i < DataManager.skillLevelIndex.Length; i++)
                {
                    if (DataManager.skillLevelIndex[i].name == row[0])
                    {
                        sdp_buf.Add(new SkillDataPlus { reserveAttribute = DataManager.skillLevelIndex[i].reserveAttribute, coolDown = DataManager.skillLevelIndex[i].coolDown, duration = DataManager.skillLevelIndex[i].duration });
                    }
                }
                ReserveAttribute[] ra_Array = new ReserveAttribute[sdp_buf.Count];
                double[] cd_array = new double[sdp_buf.Count];
                double[] dr_array = new double[sdp_buf.Count];
                for (int i = 0; i < sdp_buf.Count; i++)
                {
                    ra_Array[i] = sdp_buf[i].reserveAttribute;
                    cd_array[i] = sdp_buf[i].coolDown;
                    dr_array[i] = sdp_buf[i].duration;
                }
                //将读入的内容转化为Skill类
                Skill a = new Skill(row[0], row[1], int.Parse(row[2]), int.Parse(row[3]), ra_Array, cd_array, dr_array, row[4]);
                buf.Add(a);
            }
            DataManager.SkillData = new Skill[buf.Count];
            for (int i = 0; i < DataManager.SkillData.Length; i++)
            {
                DataManager.SkillData[i] = buf[i];
            }
        }
    }
    //对角色数据进行读入，即对DataManager.roleAttribute、DataManager.roleEquipment进行赋值
    public void RoleLoad()
    {
        using (CsvFileReader reader = new CsvFileReader(GetPersistentFilePath("Data/Role.csv")))
        {
            CsvRow row = new CsvRow();
            bool first = true;
            while (reader.ReadRow(row))
            {
                if (first)
                {
                    first = false;
                    continue;
                }
                if (row[0] == "")
                {
                    break;
                }
                //将读入数据转化为RoleAttribute类并赋值给DataManager.roleAttribute
                DataManager.roleAttribute = new RoleAttribute(row[0], int.Parse(row[1]), int.Parse(row[2]), LevelIndex.FindExpLimit(DataManager.levelIndex, int.Parse(row[1])), LevelIndex.FindAttribute(DataManager.levelIndex, int.Parse(row[1])));
                BagItem[] e_buf = new BagItem[4];
                bool[] eb_buf = new bool[4];
                for (int i = 0; i < 8; i += 2)
                {
                    if (row[i + 3] == "")
                    {
                        e_buf[i / 2] = new BagItem();
                        eb_buf[i / 2] = false;
                        continue;
                    }
                    e_buf[i / 2].item = new Item();
                    e_buf[i / 2].item.FindItem(DataManager.GameItemIndex, int.Parse(row[i + 3]));
                    e_buf[i / 2].level = int.Parse(row[i + 4]);
                    e_buf[i / 2].count = 1;
                    eb_buf[i / 2] = true;
                }
                //将读入数据转化为RoleEquipment类并赋值给DataManager.roleEquipment
                DataManager.roleEquipment = new RoleEquipment(e_buf, eb_buf, int.Parse(row[11]), int.Parse(row[12]), int.Parse(row[13]));
            }
        }
    }
    //对角色背包数据进行读入，即对DataManager.bag进行赋值
    public void BagLoad()
    {
        using (CsvFileReader reader = new CsvFileReader(GetPersistentFilePath("Data/Bag.csv")))
        {
            List<BagItem> buf = new List<BagItem>();
            CsvRow row = new CsvRow();
            bool first = true;
            while (reader.ReadRow(row))
            {
                if (first)
                {
                    first = false;
                    continue;
                }
                if (row[0] == "")
                {
                    break;
                }
                //将读入的内容转化为BagItem结构体
                BagItem addBuf = new BagItem();
                addBuf.item = new Item();
                addBuf.item.FindItem(DataManager.GameItemIndex, int.Parse(row[0]));
                addBuf.level = int.Parse(row[1]);
                addBuf.count = int.Parse(row[2]);
                buf.Add(addBuf);
            }
            //赋值
            DataManager.bag = new Bag(buf);
        }
    }

    //对抽奖计时数据进行读入，即对GameScript.NormalPool、GameScript.GoodPool进行赋值
    public void LottertyLoad()
    {
        DateTime Buf = DateTime.Now;
        GameScript.NormalPool = Buf.Date;
        GameScript.GoodPool = Buf.Date;
        using (CsvFileReader reader = new CsvFileReader(GetPersistentFilePath("Data/Lotterty.csv")))
        {
            CsvRow row = new CsvRow();
            bool first = true;
            while (reader.ReadRow(row))
            {
                if (first)
                {
                    first = false;
                    continue;
                }
                if (row[0] == "")
                {
                    break;
                }
                if (row[0] == "低级")  //赋值时、分、秒
                {
                    GameScript.NormalPool = GameScript.NormalPool.AddHours(double.Parse(row[1]));
                    GameScript.NormalPool = GameScript.NormalPool.AddMinutes(double.Parse(row[2]));
                    GameScript.NormalPool = GameScript.NormalPool.AddSeconds(double.Parse(row[3]));
                    continue;
                }
                if (row[0] == "高级")  //赋值时、分、秒
                {
                    GameScript.GoodPool = GameScript.GoodPool.AddHours(double.Parse(row[1]));
                    GameScript.GoodPool = GameScript.GoodPool.AddMinutes(double.Parse(row[2]));
                    GameScript.GoodPool = GameScript.GoodPool.AddSeconds(double.Parse(row[3]));
                    continue;
                }
            }
        }
    }
    //对关卡产出进行读入，即对GameScript.LevelOutput[]进行赋值
    public void LevelOutputLoad()
    {
        using (CsvFileReader reader = new CsvFileReader(GetPersistentFilePath("Data/LevelOutput.csv")))
        {
            GameScript.LevelOutput = new List<Chip>();
            CsvRow row = new CsvRow();
            bool first = true;
            while (reader.ReadRow(row))
            {
                if (first)
                {
                    first = false;
                    continue;
                }
                if (row[0] == "")
                {
                    break;
                }
                if (row[1] == "是")
                {
                    if (row[2] == "")
                    {
                        GameScript.LevelOutput.Add(new Chip(int.Parse(row[0]), true, Quality.Normal, 0, int.Parse(row[4]), int.Parse(row[5]), GameScript.GetEquipmentQuality(row[6]), int.Parse(row[7]), int.Parse(row[8]), int.Parse(row[9]), int.Parse(row[10]), int.Parse(row[11])));
                    }
                    else
                    {
                        GameScript.LevelOutput.Add(new Chip(int.Parse(row[0]), true, GameScript.GetEquipmentQuality(row[2]), int.Parse(row[3]), int.Parse(row[4]), int.Parse(row[5]), GameScript.GetEquipmentQuality(row[6]), int.Parse(row[7]), int.Parse(row[8]), int.Parse(row[9]), int.Parse(row[10]), int.Parse(row[11])));
                    }
                }
                else
                {
                    if (row[2] == "")
                    {
                        GameScript.LevelOutput.Add(new Chip(int.Parse(row[0]), false, Quality.Normal, 0, int.Parse(row[4]), int.Parse(row[5]), GameScript.GetEquipmentQuality(row[6]), int.Parse(row[7]), int.Parse(row[8]), int.Parse(row[9]), int.Parse(row[10]), int.Parse(row[11])));
                    }
                    else
                    {
                        GameScript.LevelOutput.Add(new Chip(int.Parse(row[0]), false, GameScript.GetEquipmentQuality(row[2]), int.Parse(row[3]), int.Parse(row[4]), int.Parse(row[5]), GameScript.GetEquipmentQuality(row[6]), int.Parse(row[7]), int.Parse(row[8]), int.Parse(row[9]), int.Parse(row[10]), int.Parse(row[11])));
                    }
                }

            }
        }
    }
    //对人物模型在场景中进行读入，即对GameScript.MainWeaponModelIndex[]等进行赋值
    public void GameModelLoad()
    {
        //申请内存空间
        GameScript.EquipmentModel = new GameObject[4];
        GameScript.MainWeaponModelIndex = new GameObject[4];
        GameScript.AlternateWeaponModelIndex = new GameObject[4];
        GameScript.CuirassModelIndex = new GameObject[4];
        GameScript.HelmModelIndex = new GameObject[4];

        //导入场景对象，进行赋值
        GameScript.MainWeaponModelIndex[0] = MainWeaponModel1;
        GameScript.MainWeaponModelIndex[1] = MainWeaponModel2;
        GameScript.MainWeaponModelIndex[2] = MainWeaponModel3;
        GameScript.MainWeaponModelIndex[3] = MainWeaponModel4;

        GameScript.AlternateWeaponModelIndex[0] = AlternateWeaponModel1;
        GameScript.AlternateWeaponModelIndex[1] = AlternateWeaponModel2;
        GameScript.AlternateWeaponModelIndex[2] = AlternateWeaponModel3;
        GameScript.AlternateWeaponModelIndex[3] = AlternateWeaponModel4;

        GameScript.CuirassModelIndex[0] = CuirassModel1;
        GameScript.CuirassModelIndex[1] = CuirassModel2;
        GameScript.CuirassModelIndex[2] = CuirassModel3;
        GameScript.CuirassModelIndex[3] = CuirassModel4;

        GameScript.HelmModelIndex[0] = HelmModel1;
        GameScript.HelmModelIndex[1] = HelmModel2;
        GameScript.HelmModelIndex[2] = HelmModel3;
        GameScript.HelmModelIndex[3] = HelmModel4;

        if (DataManager.roleEquipment.HadMainWeapon())
        {
            GameScript.EquipmentModel[0] = GameScript.MainWeaponModelIndex[(int)DataManager.roleEquipment.GetMainWeapon().item.GetQuality()];
        }
        else
        {
            GameScript.EquipmentModel[0] = null;
        }

        if (DataManager.roleEquipment.HadAlternateWeapon())
        {
            GameScript.EquipmentModel[1] = GameScript.AlternateWeaponModelIndex[(int)DataManager.roleEquipment.GetAlternateWeapon().item.GetQuality()];
        }
        else
        {
            GameScript.EquipmentModel[1] = null;
        }

        if (DataManager.roleEquipment.HadCuirass())
        {
            GameScript.EquipmentModel[2] = GameScript.CuirassModelIndex[(int)DataManager.roleEquipment.GetCuirass().item.GetQuality()];
        }
        else
        {
            GameScript.EquipmentModel[2] = null;
        }

        if (DataManager.roleEquipment.HadHelm())
        {
            GameScript.EquipmentModel[3] = GameScript.HelmModelIndex[(int)DataManager.roleEquipment.GetHelm().item.GetQuality()];
        }
        else
        {
            GameScript.EquipmentModel[3] = null;
        }

        for (int i = 0; i < 4; i++)
        {
            if (GameScript.EquipmentModel[i] != null)
            {
                if (i == 1)
                {
                    if (GameScript.EquipmentModel[0] != null)
                    {
                        continue;
                    }
                }
                GameScript.EquipmentModel[i].SetActive(true);
            }
        }
    }
    //对离开游戏时间进行读入，即对GameScript.DisableTime进行赋值
    public void ExitTimeLoad()
    {
        using (CsvFileReader reader = new CsvFileReader(GetPersistentFilePath("Data/ExitTime.csv")))
        {
            CsvRow row = new CsvRow();
            while (reader.ReadRow(row))
            {
                if (row[0] == "")
                {
                    GameScript.DisableTime = DateTime.Now;
                    break;
                }
                GameScript.DisableTime = DateTime.Parse(row[0]);
            }
        }
    }
    //对体力系统数据进行读入，即对GameScript.VIT、GameScript.PerVITMinute进行赋值，并做计时器初始化
    public void VITLoad()
    {
        using (CsvFileReader reader = new CsvFileReader(GetPersistentFilePath("Data/VIT.csv")))
        {
            CsvRow row = new CsvRow();
            bool first = true;
            while (reader.ReadRow(row))
            {
                if (first)
                {
                    first = false;
                    continue;
                }
                if (row[0] == "")
                {
                    break;
                }
                GameScript.VIT = int.Parse(row[0]);
                GameScript.PerVITMinute = double.Parse(row[1]);
                //初始化计时器
                GameScript.VITCounter = DateTime.Now.Date.AddMinutes(GameScript.PerVITMinute);
            }
        }
    }
    //对数据读入进行集成，并对全局高频sprite进行赋值，同时初始化装备碎片池用于计算关卡产出
    public void GameLoad()
    {
        GameItemIndexLoad();
        LevelIndexLoad();
        SkillLevelIndexLoad();
        SkillLevelUpIndexLoad();
        LottertyLoad();
        ExitTimeLoad();
        SkillDataLoad();
        RoleLoad();
        BagLoad();
        LevelOutputLoad();
        VITLoad();

        GameScript.QualityNormal = GameScript.GetSprite("UI/Normal.png");
        GameScript.QualityExcellent = GameScript.GetSprite("UI/Excellent.png");
        GameScript.QualityRare = GameScript.GetSprite("UI/Rare.png");
        GameScript.QualityEpic = GameScript.GetSprite("UI/Epic.png");
        GameScript.SlaySprite = GameScript.GetSprite("UI/Slay.png");
        GameScript.RareEarthSprite = GameScript.GetSprite("UI/RareEarth.png");
        GameScript.SkillDebrisSprite = GameScript.GetSprite("UI/SkillDebris.png");
        PoolInit();
    }
    //初始化装备碎片池，即对GameScript.NormalDebrisPool[]等进行赋值
    private void PoolInit()
    {
        GameScript.NormalDebrisPool = new Item[4];
        GameScript.ExcellentDebrisPool = new Item[4];
        GameScript.RareDebrisPool = new Item[4];
        GameScript.EpicDebrisPool = new Item[4];
        GameScript.NormalDebrisPool[0] = DataManager.GameItemIndex[1];
        GameScript.NormalDebrisPool[1] = DataManager.GameItemIndex[3];
        GameScript.NormalDebrisPool[2] = DataManager.GameItemIndex[17];
        GameScript.NormalDebrisPool[3] = DataManager.GameItemIndex[25];

        GameScript.ExcellentDebrisPool[0] = DataManager.GameItemIndex[5];
        GameScript.ExcellentDebrisPool[1] = DataManager.GameItemIndex[7];
        GameScript.ExcellentDebrisPool[2] = DataManager.GameItemIndex[19];
        GameScript.ExcellentDebrisPool[3] = DataManager.GameItemIndex[27];

        GameScript.RareDebrisPool[0] = DataManager.GameItemIndex[9];
        GameScript.RareDebrisPool[1] = DataManager.GameItemIndex[11];
        GameScript.RareDebrisPool[2] = DataManager.GameItemIndex[21];
        GameScript.RareDebrisPool[3] = DataManager.GameItemIndex[29];

        GameScript.EpicDebrisPool[0] = DataManager.GameItemIndex[13];
        GameScript.EpicDebrisPool[1] = DataManager.GameItemIndex[15];
        GameScript.EpicDebrisPool[2] = DataManager.GameItemIndex[23];
        GameScript.EpicDebrisPool[3] = DataManager.GameItemIndex[31];
    }

}
