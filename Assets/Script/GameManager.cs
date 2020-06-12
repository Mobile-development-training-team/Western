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

    int currentlives = 3;//主角初始化有3条命
    private GameManager() { }

    public static GameManager getInstance()
    {
        if (INSTANCE == null)
        {
            //INSTANCE = new GameManager();
            Debug.Log("you shouldn'ts get there");
        }
        return INSTANCE;
    }

    public void Awake(){
     if (INSTANCE == null)
        {
            INSTANCE = this;
        }
        else if (INSTANCE != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);//不摧毁控制器
    }


    public void  GameOver()
    {
        Debug.Log("GameOver!");
        GameOverImage.SetActive(true);
        Invoke("Destroyself",2f);
        //yield return new WaitForSeconds(2f);
        //StartCoroutine(Wait());


    }

    void Destroyself()
    {
        Destroy(gameObject);//游戏结束强行摧毁所有东西，重新开始
        LoadStartScene();
        
    }
    public void LoadNextScene()
    {
        int curSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //Debug.Log(curSceneIndex);
        if( curSceneIndex == 3)
        { 
            GameOver();
            return;
        }
        SceneManager.LoadScene(curSceneIndex + 1);
        if(curSceneIndex+1>PlayerPrefs.GetInt("level"))
        {
            PlayerPrefs.SetInt(PlayerPrefs.GetString("level"),curSceneIndex + 1);
        }
        SetUpUI(curSceneIndex + 1);
        Invoke("CloseUI",2f);

    }
    public void LoadCurScene()
    {
        int curSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(curSceneIndex);
    }
    public void LoadStartScene()
    {
        SceneManager.LoadScene(4);
    }
     public void QuitGame() {
        Application.Quit();
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

  // Start is called before the first frame update
    IEnumerator  Start()
    {
        //SetUpUI(1);
        LevelImage.SetActive(true);
        yield return new WaitForSeconds(2);
        //Wait();
        LevelImage.SetActive(false);
        GameOverImage.SetActive(false);
        // EnemiesManager01.Instance.generatorEnemiesWave();
    }

    public void SetUpScene()
    {

    }



}
