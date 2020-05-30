using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    /*
    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }

    public GameObject LoadObj(string objName)
    {
        GameObject currentPrefab = Resources.Load<GameObject>("Prefabs/" + objName);
        return Instantiate(currentPrefab);
    }
    */

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
