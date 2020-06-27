using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;
//using  LeoLuz.PlugAndPlayJoystick;
public class GameManager:MonoBehaviour
{
    public static GameManager INSTANCE;
    private LevelManager levelManager;
    private GameUIController gameUIController;
    private EnemiesManager enemiesManager;
    private BraveController brave;

    private int currentSceneIndex;
    private int levelId;

    private GameManager() { }

    public void Awake()
    {
        if (INSTANCE == null)
        {
            INSTANCE = this;
            //ResetLevel();
            currentSceneIndex = getCurrentSceneIndex();
            //levelId = PlayerPrefs.GetInt(PlayerPrefs.GetString("level"), 1);
            levelId = PlayerPrefs.GetInt("level", 1);
            EnemiesLoad();
            //Debug.Log("LevelDatas.Count=" + LevelDatas.Count + "//////LevelDatas[0].Waves.Count=" + LevelDatas[0].Waves.Count);
        }
        else if (INSTANCE != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);//不摧毁控制器
    }

    ///////////////////////////////////////////////////////////////<页面跳转相关>
    public int getCurrentSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex; ;
    }
    public int getLevelID()
    {
        return levelId;
    }
    public void ResetLevel()
    {
        PlayerPrefs.DeleteKey("level");
    }
    public void LoadTargetScene(int targetSceneIndex)
    {
        currentSceneIndex = targetSceneIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    public void LoadGameSelectScene()
    {
        LoadTargetScene(9);
    }
    /*
    public void LoadNextScene()
    {
        if (currentSceneIndex == 9)
        {
            return;
        }
        currentSceneIndex += 1;
        if (currentSceneIndex > levelId)
        {
            levelId = currentSceneIndex;
            //PlayerPrefs.SetInt(PlayerPrefs.GetString("level"), levelId);
            PlayerPrefs.SetInt("level", levelId);
        }
        SceneManager.LoadScene(currentSceneIndex);
    }
    */
    public void LoadCurScene()
    {
        int curSceneIndex = getCurrentSceneIndex();
        SceneManager.LoadScene(curSceneIndex);
    }
    public void LoadMainScene() 
    {
         SceneManager.LoadScene("MainScene_fjj");
         //Destroy(gameObject);
    }
    ///////////////////////////////////////////////////////////////<页面跳转相关/>

    ///////////////////////////////////////////////////////////////<关卡控制相关>
    public void GameOver(bool judge)
    {
        if (!judge)
        {
            //主角死亡
            //if 复活
            //do something
            //else if 重开此关卡
            //Invoke("LoadCurScene", 2f);
            //else 结束关卡返回关卡选择界面
            //Invoke("LoadGameSelectScene", 2f);
            gameUIController.ShowDeathEnd();
        }
        else
        {
            //关卡获胜
            if (currentSceneIndex == levelId)
            {
                levelId++;
                //PlayerPrefs.SetInt(PlayerPrefs.GetString("level"), levelId);
                PlayerPrefs.SetInt("level", levelId);
            }
                /*
            else if (currentSceneIndex > levelId && currentSceneIndex < 9) 
            {
                levelId = currentSceneIndex;
                //PlayerPrefs.SetInt(PlayerPrefs.GetString("level"), levelId);
                PlayerPrefs.SetInt("level", levelId);
            }*/
            gameUIController.ShowWinEnd();
            /*
            currentSceneIndex = getCurrentSceneIndex();
            if (currentSceneIndex == 3)
            {
                //全部通关
                //if 重开此关卡
                //Invoke("LoadCurScene", 2f);
                //else 结束关卡返回关卡选择界面
                Invoke("LoadGameSelectScene", 2f);
            }
            else
            {
                //if 进入下一关
                Invoke("LoadNextScene", 2f);
                //else if 重开此关卡
                //Invoke("LoadCurScene", 2f);
                //else 结束关卡返回关卡选择界面
                //Invoke("LoadGameSelectScene", 2f);
            }
            */
        }
    }
    public void setUIController(GameUIController uic)
    {
        gameUIController = uic;
    }
    public GameUIController getUIController()
    {
        return gameUIController;
    }
    public void setEnemiesManager(EnemiesManager em)
    {
        enemiesManager = em;
    }
    public EnemiesManager getEnemiesManager()
    {
        return enemiesManager;
    }
    public void setLevelManager(LevelManager lm)
    {
        levelManager = lm;
    }
    public LevelManager getLevelManager()
    {
        return levelManager;
    }
    public void setBrave(BraveController br)
    {
        brave = br;
    }
    public BraveController getBrave()
    {
        return brave;
    }
    ///////////////////////////////////////////////////////////////<关卡控制相关/>



    ////////////////////////////////////////////////////////////////<尝试读取CSV文件>
    //public static Dictionary<int , List<WaveData>> LevelDatas;
    public static List<LevelData> LevelDatas;
    public void EnemiesLoad()
    {
        using (CsvFileReader reader = new CsvFileReader(GetPersistentFilePath("Data/Enemies.csv")))
        {
            LevelDatas = new List<LevelData>();
            //List<EnemyData> enemiesList = new List<EnemyData>();
            int levelIndex;
            int waveIndex;
            int enemyIndex;
            float distance;
            float waitTime;
            float maxHP;
            float def;
            float atk;
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
                else
                {
                    levelIndex = int.Parse(row[0]);
                    waveIndex = int.Parse(row[1]);
                    enemyIndex = int.Parse(row[2]);
                    distance = float.Parse(row[3]);
                    waitTime = float.Parse(row[4]);
                    maxHP = float.Parse(row[5]);
                    def = float.Parse(row[6]);
                    atk = float.Parse(row[7]);
                    //Debug.Log("new enemy "+levelIndex+"////"+waveIndex+"////"+enemyIndex+"////"+distance+"////"+waitTime+"////"+maxHP+"////"+def+"////"+atk);
                    //enemiesList.Add(new EnemyData(levelIndex,waveIndex,enemyIndex,distance,waitTime,maxHP,def,atk));
                    if (LevelDatas.Count < levelIndex)
                    {
                        for (int i = 0; i < levelIndex - LevelDatas.Count; i++)
                        {
                            LevelDatas.Add(new LevelData());
                        }
                    }
                    if (LevelDatas[levelIndex-1].Waves.Count < waveIndex)
                    {
                        for (int i = 0; i < waveIndex - LevelDatas[levelIndex - 1].Waves.Count; i++)
                        {
                            LevelDatas[levelIndex - 1].Waves.Add(new WaveData());
                        }
                    }
                    LevelDatas[levelIndex - 1].Waves[waveIndex - 1].Enemies.Add(new EnemyData(levelIndex, waveIndex, enemyIndex, distance, waitTime, maxHP, def, atk));
                }
            }
            //Debug.Log("enemiesList.Count=" + enemiesList.Count);
            //InitLevelDatas(enemiesList);
        }
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
    public class LevelData
    {
        public List<WaveData> Waves;
        public LevelData()
        {
            Waves = new List<WaveData>();
        }
    }
    public class WaveData
    {
        public List<EnemyData> Enemies;
        public WaveData()
        {
            Enemies = new List<EnemyData>();
        }
    }
    public class EnemyData
    {
        public int LevelIndex;
        public int WaveIndex;
        public int EnemyIndex;
        public float Distance;
        public float WaitTime;
        public float MaxHP;
        public float Def;
        public float Atk;

        public EnemyData(int li, int wi, int ei, float di, float wt, float mh, float df, float atk)
        {
            LevelIndex = li;
            WaveIndex = wi;
            EnemyIndex = ei;
            Distance = di;
            WaitTime = wt;
            MaxHP = mh;
            Def = df;
            Atk = atk;
        }
    }
}
