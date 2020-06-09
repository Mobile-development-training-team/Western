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
        //yield return new WaitForSeconds(2f);
        //StartCoroutine(Wait());
        LoadStartScene();
        

    }
    public void LoadNextScene()
    {
        int curSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(curSceneIndex + 1);
        SetUpUI (curSceneIndex +1);
    }
    public void LoadCurScene()
    {
        int curSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(curSceneIndex);
    }
    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
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
        LevelImage.SetActive(true);

    }

  // Start is called before the first frame update
    IEnumerator  Start()
    {
        SetUpUI(1);
        yield return new WaitForSeconds(2);
        //Wait();
         LevelImage.SetActive(false);
         GameOverImage.SetActive(false);
        // EnemiesManager01.Instance.generatorEnemiesWave();
    }

    public void SetUpScene()
    {

    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(4);
    }




}
