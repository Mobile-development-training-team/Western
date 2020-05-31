using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager
{
    private static GameManager INSTANCE;

    private GameManager() { }

    public static GameManager getInstance()
    {
        if (INSTANCE == null)
        {
            INSTANCE = new GameManager();
        }
        return INSTANCE;
    }

}
