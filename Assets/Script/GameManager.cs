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
            currentSceneIndex = getCurrentSceneIndex();
            levelId = PlayerPrefs.GetInt(PlayerPrefs.GetString("level"), 1);
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
            PlayerPrefs.SetInt(PlayerPrefs.GetString("level"), levelId);
        }
        SceneManager.LoadScene(currentSceneIndex);
    }
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
            gameUIController.ShowWinEnd();
            if (currentSceneIndex == levelId)
            {
                levelId++;
                PlayerPrefs.SetInt(PlayerPrefs.GetString("level"), levelId);
            }
            else if (currentSceneIndex > levelId)
            {
                levelId = currentSceneIndex;
                PlayerPrefs.SetInt(PlayerPrefs.GetString("level"), levelId);
            }

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
}
