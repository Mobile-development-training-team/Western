using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject[] LevelButton;
    public GameObject[] LockImage;
    private int levelId;
    GameManager gameManager;

    void Awake()
    {
        gameManager = GameManager.INSTANCE;
        gameManager.setLevelManager(this);
    }
    void Start()
    {
        //上次退出游戏时保存的游戏关卡ID，如果第一次进入默认为1
        //levelId = PlayerPrefs.GetInt(PlayerPrefs.GetString("level"), 1);//返回Level的值，如果不存在就返回默认值1
        levelId = gameManager.getLevelID();
        LoadLevel();
    }
    private void LoadLevel()
    {
        for (int i = 0; i < 8; i++)
        {
            if (i + 1 <= levelId)
            {
                LockImage[i].transform.gameObject.SetActive(false);
            }
            else
            {
                LockImage[i].transform.gameObject.SetActive(true);
            }
        }
    }
    public void LoadGameScene01()
    {
        if (levelId >= 1)
        {
            if (GameScript.ReduceVIT(4))
            {
                GameManager.INSTANCE.LoadTargetScene(1);
            }
        }
    }
    public void LoadGameScene02()
    {
        if (levelId >= 2)
        {
            if (GameScript.ReduceVIT(4))
            {
                gameManager.LoadTargetScene(2);
            }
        }
    }
    public void LoadGameScene03()
    {
        if (levelId >= 3)
        {
            if (GameScript.ReduceVIT(4))
            {
                gameManager.LoadTargetScene(3);
            }
        }
    }
    public void LoadGameScene04()
    {
        if (levelId >= 4)
        {
            if (GameScript.ReduceVIT(4))
            {
                gameManager.LoadTargetScene(4);
            }
        }
    }
    public void LoadGameScene05()
    {
        if (levelId >= 5)
        {
            if (GameScript.ReduceVIT(4))
            {
                gameManager.LoadTargetScene(5);
            }
        }
    }
    public void LoadGameScene06()
    {
        if (levelId >= 6)
        {
            if (GameScript.ReduceVIT(4))
            {
                gameManager.LoadTargetScene(6);
            }
        }
    }
    public void LoadGameScene07()
    {
        if (levelId >= 7)
        {
            if (GameScript.ReduceVIT(4))
            {
                gameManager.LoadTargetScene(7);
            }
        }
    }
    public void LoadGameScene08()
    {
        if (levelId >= 8)
        {
            if (GameScript.ReduceVIT(4))
            {
                gameManager.LoadTargetScene(8);
            }
        }
    }
    public void LoadMainScene()
    {
        gameManager.LoadMainScene();
    }
}
