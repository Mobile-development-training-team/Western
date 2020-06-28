using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectedScript : MonoBehaviour
{
    public static int LevelChosen = -1;

    public void Click()
    {
        transform.parent.Find("Skill").Find("SkillUI").GetComponent<Canvas>().enabled = false;
        transform.parent.Find("Level").Find("LevelUI").GetComponent<Canvas>().enabled = true;
        transform.parent.Find("Bag").Find("BagUI").GetComponent<Canvas>().enabled = false;
    }
}
