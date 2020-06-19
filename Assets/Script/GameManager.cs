using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;
//using  LeoLuz.PlugAndPlayJoystick;
public class GameManager:MonoBehaviour
{
    public  static GameManager INSTANCE;
    public int currentSceneIndex;
    private GameUIController gameUIController;
    private EnemiesManager01 enemiesManager;

    private GameManager() { }

    public void Awake()
    {
        if (INSTANCE == null)
        {
            INSTANCE = this;
            currentSceneIndex = getCurrentSceneIndex();
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
    public void LoadTargetScene(int targetSceneIndex)
    {
        currentSceneIndex = targetSceneIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    public void LoadGameSelectScene()
    {
        LoadTargetScene(4);
    }
    public void LoadNextScene()
    {
        if (currentSceneIndex == 4)
        {
            return;
        }
        currentSceneIndex += 1;
        if (currentSceneIndex > PlayerPrefs.GetInt("level"))
        {
            PlayerPrefs.SetInt(PlayerPrefs.GetString("level"), currentSceneIndex );
        }
        SceneManager.LoadScene(currentSceneIndex);
    }
    public void LoadCurScene()
    {
        int curSceneIndex = getCurrentSceneIndex();
        SceneManager.LoadScene(curSceneIndex);
    }
     public void LoadMainScene() {
         SceneManager.LoadScene("MainScene_fjj");
         Destroy(gameObject);
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
    public void setEnemiesManager(EnemiesManager01 em)
    {
        enemiesManager = em;
    }
    ///////////////////////////////////////////////////////////////<关卡控制相关/>
}
