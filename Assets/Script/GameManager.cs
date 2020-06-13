using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;
using  LeoLuz.PlugAndPlayJoystick;
public class GameManager:MonoBehaviour
{
    public  static GameManager INSTANCE;
    
    public GameObject LevelImage;
    public Text LevelText;
    public Text GameoverText;
    public GameObject GameOverImage;

    public int currentSceneIndex;

    int currentlives = 3;//主角初始化有3条命

    private GameManager() { }

    /*
    public static GameManager getInstance()
    {
        if (INSTANCE == null)
        {
            //INSTANCE = new GameManager();
            Debug.Log("you shouldn'ts get there");
        }
        return INSTANCE;
    }
    */

    public void Awake()
    {
        if (INSTANCE == null)
        {
            INSTANCE = this;
            currentSceneIndex = 0;
        }
        else if (INSTANCE != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);//不摧毁控制器
    }
    /*
    IEnumerator Start()
    {
        //SetUpUI(1);
        LevelImage.SetActive(true);
        yield return new WaitForSeconds(2);
        //Wait();
        LevelImage.SetActive(false);
        GameOverImage.SetActive(false);
        // EnemiesManager01.Instance.generatorEnemiesWave();
    }
    */
    public void  GameOver(bool judge)
    {
        //Debug.Log("GameOver!");
        //Invoke("LoadStartScene", 2f);
        CloseUI();
        GameOverImage.SetActive(true);
        if (!judge)
        {
            //主角死亡
            //if 复活
            //do something
            //else if 重开此关卡
            //Invoke("LoadCurScene", 2f);
            //else 结束关卡返回起始界面
            Invoke("LoadStartScene", 2f);
        }
        else
        {
            //关卡获胜
            currentSceneIndex = getLevelIndex();
            if (currentSceneIndex == 3)
            {
                //全部通关
                //if 重开此关卡
                //Invoke("LoadCurScene", 2f);
                //else 结束关卡返回起始界面
                Invoke("LoadStartScene", 2f);
            }
            else
            {
                //if 进入下一关
                Invoke("LoadNextScene", 2f);
                //else if 重开此关卡
                //Invoke("LoadCurScene", 2f);
                //else 结束关卡返回起始界面
                //Invoke("LoadStartScene", 2f);
            }
        }
    }
    /*
    public void Destroyself()
    {
        //Destroy(gameObject);//游戏结束强行摧毁所有东西，重新开始
        if (IsInvoking("Destroyself"))
        {
            CancelInvoke("Destroyself");
        }
        LoadStartScene();
    }
    */
    public void LoadStartScene()
    {
        GameOverImage.SetActive(false);
        currentSceneIndex = 0;
        SceneManager.LoadScene(0);
    }
    public void LoadNextScene()
    {
        //int curSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //Debug.Log(curSceneIndex);
        /*
        if (currentSceneIndex == 3)
        { 
            //GameOver();
            currentSceneIndex = 0;
            //return;
        }
        */
        GameOverImage.SetActive(false);
        SceneManager.LoadScene((currentSceneIndex + 1) % 4);
        if (currentSceneIndex + 1 > PlayerPrefs.GetInt("level"))
        {
            PlayerPrefs.SetInt(PlayerPrefs.GetString("level"), currentSceneIndex + 1);
        }
        SetUpUI(currentSceneIndex + 1);
        Invoke("CloseUI",2f);

    }
    public void LoadCurScene()
    {
        GameOverImage.SetActive(false);
        int curSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(curSceneIndex);
    }
     public void QuitGame() {
        Application.Quit();
        Debug.Log("退出游戏");
    }
    public int getLevelIndex() { 
        return SceneManager.GetActiveScene().buildIndex; ;
    }
    //level config
    public void SetUpUI(int nxtIndex)
    {
        //修改text文本内容
        LevelText.text = "Level " + nxtIndex;
        LevelImage.SetActive(true);
    }
    public void CloseUI()
    {
        LevelImage.SetActive(false);
    }
    public void SetUpScene()
    {

    }
}
