using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewUISkillButtonScript : MonoBehaviour
{
    public GameObject HomeButton1;
    public GameObject HomeButton2;
    public GameObject BagButton;
    public GameObject SkillUI;
    public GameObject BagUI;

    public void Click()
    {
        HomeButton1.GetComponent<Canvas>().enabled = true;
        HomeButton2.GetComponent<Canvas>().enabled = false;
        BagButton.GetComponent<Canvas>().enabled = true;
        SkillUI.GetComponent<Canvas>().enabled = true;
        BagUI.GetComponent<Canvas>().enabled = false;
        gameObject.GetComponent<Canvas>().enabled = false;
    }
}
