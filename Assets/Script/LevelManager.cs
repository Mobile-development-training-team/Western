using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject[] LevelButton;
    public GameObject[] LockImage;
    private int levelId;
    void Start()
    {
        //上次退出游戏时保存的游戏关卡ID，如果第一次进入默认为1
        levelId = PlayerPrefs.GetInt(PlayerPrefs.GetString("level"), 1);//返回Level的值，如果不存在就返回默认值1
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
            GameManager.INSTANCE.LoadTargetScene(1);
        }
    }
    public void LoadGameScene02()
    {
        if (levelId >= 2)
        {
            GameManager.INSTANCE.LoadTargetScene(2);
        }
    }
    public void LoadGameScene03()
    {
        if (levelId >= 3)
        {
            GameManager.INSTANCE.LoadTargetScene(3);
        }
    }
    public void LoadGameScene04()
    {

    }
    public void LoadGameScene05()
    {

    }
    public void LoadGameScene06()
    {

    }
    public void LoadGameScene07()
    {

    }
    public void LoadGameScene08()
    {

    }

    public void LoadMainScene()
    {
        GameManager.INSTANCE.LoadMainScene();
    }
    private void  Reset()
    {
      // PlayerPrefs.SetInt(PlayerPrefs.GetString("level"),1);
      PlayerPrefs.DeleteKey("level");
      //print(PlayerPrefs.GetInt(PlayerPrefs.GetString("level")));
      print(PlayerPrefs.HasKey("level"));
    }
}
