using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Reset();

        //LoadLevel();
        
    }

    // Update is called once per frame
    void Update()
    {
       // Reset();
        LoadLevel();
    }
    private void LoadLevel()
    {
        //上次退出游戏时保存的游戏关卡ID，如果第一次进入默认为1
        int levelId = PlayerPrefs.GetInt(PlayerPrefs.GetString("level"),1);//返回Level的值，如果不存在就返回默认值1
        //向所有子物体上的LevelItem脚本中的Init方法传值
        for (int i = 0; i < transform.childCount; i++)
        {
            
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
      // PlayerPrefs.SetInt(PlayerPrefs.GetString("level"),1);
      PlayerPrefs.DeleteKey("level");
      //print(PlayerPrefs.GetInt(PlayerPrefs.GetString("level")));
      print(PlayerPrefs.HasKey("level"));
    }
}
