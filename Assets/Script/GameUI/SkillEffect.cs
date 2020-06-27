using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class SkillEffect : MonoBehaviour
{
    public float CD_Time = 20f;//技能冷却时间
    //public Image bg;
    public Image Mask;
    private bool ButtonSwitch = false;
    private Button ThisButton;
    private float Times = 0f;//累加器
     private void Awake()
        {
            ThisButton = this.GetComponent<Button>();            
        }


    // Start is called before the first frame update
    void Start()
    {
        Mask.gameObject.SetActive(false);
        /*Times += Time.deltaTime;
            Mask.fillAmount = 1-Times/CD_Time;
            if (Times >= CD_Time)
                {
                    ButtonSwitch = false;
                    Mask.fillAmount = 0;
                    Times = 0f;
                    ThisButton.interactable = true;
                }*/
        //Times = CD_Time;
    }

    // Update is called once per frame
    void Update()
    {
        if(ButtonSwitch)//在技能冷却时间
        {
            Mask.gameObject.SetActive(true);
            Times += Time.deltaTime;
            Mask.fillAmount = 1-Times/CD_Time;
            if (Times >= CD_Time)
                {
                    ButtonSwitch = false;
                    Mask.fillAmount = 0;
                    Times = 0f;
                    ThisButton.interactable = true;
                }
        }
    }

    public void SkillTimeStarts()//按钮的注册方法
        {
            ButtonSwitch = true;
            ThisButton.interactable = false;
        }


}
