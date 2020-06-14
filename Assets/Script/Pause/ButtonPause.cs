using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPause : MonoBehaviour
{
    // Start is called before the first frame update
 public GameObject ingameMenu;

    public void OnPause()//点击“暂停”时执行此方法
    {
        Time.timeScale = 0;
        ingameMenu.SetActive(true);
    }

    public void OnResume()//点击“回到游戏”时执行此方法
    {
        Time.timeScale = 1f;
        ingameMenu.SetActive(false);
    }

    public void OnRestart()//点击“重新开始”时执行此方法
    {
         ingameMenu.SetActive(false);
        //Loading Scene0
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}
