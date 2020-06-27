using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewUIHomeButtonScript : MonoBehaviour
{
    public GameObject SkillButton;
    public GameObject BagButton;
    public GameObject SkillUI;
    public GameObject BagUI;

    public void Click()
    {
        SkillButton.GetComponent<Canvas>().enabled = true;
        BagButton.GetComponent<Canvas>().enabled = true;
        SkillUI.GetComponent<Canvas>().enabled = false;
        BagUI.GetComponent<Canvas>().enabled = false;
        gameObject.GetComponent<Canvas>().enabled = false;
    }
}
