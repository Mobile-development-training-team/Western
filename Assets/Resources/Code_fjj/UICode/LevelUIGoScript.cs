using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUIGoScript : MonoBehaviour
{
    public void Click()
    {
        SceneManager.LoadScene("LevelSelectScene");
    }
}
