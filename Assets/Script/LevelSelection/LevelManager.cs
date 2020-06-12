using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.GetInt("level", 1);
        //LoadLevel();
        //Reset();
    }

    // Update is called once per frame
    void Update()
    {
        LoadLevel();
    }
    private void LoadLevel()
    {
        //上次退出游戏时保存的游戏关卡ID，如果第一次进入默认为1
        //向所有子物体上的LevelItem脚本中的Init方法传值
        for (int i = 0; i < transform.childCount; i++)
        {
            int levelId = PlayerPrefs.GetInt(PlayerPrefs.GetString("level"));
            if (i + 1 > levelId)
            {
                //没通关的关卡
                transform.GetChild(i).GetComponent<LevelSelection>().Init(i + 1,true);
            }
            else
            { 
                //通关的关卡
                transform.GetChild(i).GetComponent<LevelSelection>().Init(i + 1,false);
            }
        }
    }

    private void  Reset()
    {
       PlayerPrefs.SetInt(PlayerPrefs.GetString("level"),1);
    }
}
