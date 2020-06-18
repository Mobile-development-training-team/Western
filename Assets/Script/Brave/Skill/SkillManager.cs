using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LeoLuz.PropertyAttributes;

public class SkillManager : MonoBehaviour
{
    public GameObject magicCircle;
    public GameObject magicCircleBack;
    private BraveController brave;
    private Life mLife;

    ///////////////////////////////////////<skill01>
    public bool select_skill_01 = true;
    public float skill_01_atk = 10f;
    //冷却+持续型技能实现
    /*
    private bool skill_01 = false;
    private float skill_01_coolTime = 10f;
    private float skill_01_stayedTime = 3f;
    private float skill_01_timer = 3f;
    */
    //概率型技能实现
    private float skill_01_percent = 100f;
    ///////////////////////////////////////<skill01/>

    ///////////////////////////////////////<skill05>
    public bool select_skill_05 = true;
    //public float skill_05_atk = 10f;
    //冷却+持续型技能实现
    private bool skill_05 = false;
    private float skill_05_coolTime = 30f;
    private float skill_05_stayedTime = 5f;
    private float skill_05_timer = 30f;
    private float preDef = 0;
    ///////////////////////////////////////<skill05/>

    void Start()
    {
        brave = transform.GetComponent<BraveController>();
        mLife = brave.getLife();
        preDef = mLife.mDef;
    }

    void Update()
    {
        //冷却+持续型技能实现
        /*
        skill_01_timer-=Time.deltaTime;
        if (skill_01_timer <= 0)
        {
            if (skill_01)
            {
                skill_01 = false;
                skill_01_timer = skill_01_coolTime;
            }
            else
            {
                skill_01 = true;
                skill_01_timer = skill_01_stayedTime;
            }
        }
        */
        //冷却+持续型技能实现
        if (select_skill_05)
        {
            skill_05_timer -= Time.deltaTime;
            if (skill_05_timer <= 0)
            {
                if (skill_05)
                {
                    skill_05 = false;
                    skill_05_timer = skill_05_coolTime;
                    mLife.mDef = preDef;
                }
                else
                {
                    skill_05 = true;
                    skill_05_timer = skill_05_stayedTime;
                    mLife.mDef += 99999;
                }
            }
        }
    }

    public void Use_Skill_01(bool needBack)
    {
        //概率型技能实现
        if (select_skill_01)
        {
            if (judgePercent(skill_01_percent))
            {
                ObjectPool.GetInstant().GetObj("SlashWaveBlue", magicCircle.transform.position, transform.localRotation).GetComponent<SlashWaveBlueController>().atk = skill_01_atk;
                if (needBack)
                    ObjectPool.GetInstant().GetObj("SlashWaveBlue", magicCircleBack.transform.position, magicCircleBack.transform.rotation).GetComponent<SlashWaveBlueController>().atk = skill_01_atk;
            }
            //冷却+持续型技能实现
            /*
            if (skill_01)
            {
                ObjectPool.GetInstant().GetObj("SlashWaveBlue", magicCircle.transform.position, transform.localRotation).GetComponent<SlashWaveBlueController>().atk = skill_01_atk;
                if(needBack)
                    ObjectPool.GetInstant().GetObj("SlashWaveBlue", magicCircleBack.transform.position, magicCircleBack.transform.rotation).GetComponent<SlashWaveBlueController>().atk = skill_01_atk;
            }
            */
        }
    }

    public void judgeSkills()
    {
        if (select_skill_01)
        {
            //Use_Skill_01();
        }
    }



    //概率判断函数
    public static bool judgePercent(float percent)
    {
        return Random.Range(0, 100) <= percent;
    }

}
