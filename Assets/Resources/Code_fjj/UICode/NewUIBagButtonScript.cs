using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewUIBagButtonScript : MonoBehaviour
{
    public GameObject HomeButton1;
    public GameObject HomeButton2;
    public GameObject SkillButton;
    public GameObject SkillUI;
    public GameObject BagUI;

    public void Click()
    {
        HomeButton1.GetComponent<Canvas>().enabled = false;
        HomeButton2.GetComponent<Canvas>().enabled = true;
        SkillButton.GetComponent<Canvas>().enabled = true;
        SkillUI.GetComponent<Canvas>().enabled = false;
        BagUI.GetComponent<Canvas>().enabled = true;
        gameObject.GetComponent<Canvas>().enabled = false;
    }
}
