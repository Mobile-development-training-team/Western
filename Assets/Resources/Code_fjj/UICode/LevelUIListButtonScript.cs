using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUIListButtonScript : MonoBehaviour
{
    public void Click()
    {
        LevelSelectedScript.LevelChosen = transform.GetSiblingIndex();
        GameObject.Find("MainUI").transform.Find("SelectBar").GetComponent<Canvas>().enabled = false;
        GameObject.Find("MainUI").GetComponent<Canvas>().enabled = false;
        GameObject.Find("SceneUI").GetComponent<Canvas>().enabled = true;
    }
}
