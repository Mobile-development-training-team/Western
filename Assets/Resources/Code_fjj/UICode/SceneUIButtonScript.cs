using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneUIButtonScript : MonoBehaviour
{
    public void Click()
    {
        GameObject.Find("SceneUI").GetComponent<Canvas>().enabled = false;
        GameObject.Find("MainUI").GetComponent<Canvas>().enabled = true;
        GameObject.Find("MainUI").transform.Find("SelectBar").GetComponent<Canvas>().enabled = true;
    }
}
