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
    //嗜血封魔
    ///////////////////////////////////////<skill01>
    public bool select_skill_01 = true;
    public float skill_01_num = 0.08f;
    //概率型技能实现
    private float skill_01_percent = 10f;
    ///////////////////////////////////////<skill01/>

    //剑气
    ///////////////////////////////////////<skill02>
    public bool select_skill_02 = true;
    public float skill_02_atk = 10f;
    public float skill_02_num = 0.5f;
    //概率型技能实现
    private float skill_02_percent = 50f;
    ///////////////////////////////////////<skill02/>

    //血战天虹
    ///////////////////////////////////////<skill04>
    public bool select_skill_04 = true;
    public float skill_04_num = 0.2f;
    private float skill_04_percent = 10f;
    private bool use_skill_04 = false;
    private GameObject skill_04_buff;
    ///////////////////////////////////////<skill04/>

    //神之庇护
    ///////////////////////////////////////<skill05>
    public bool select_skill_05 = true;
    //public float skill_05_atk = 10f;
    //冷却+持续型技能实现
    private bool skill_05 = false;
    private float skill_05_coolTime = 30f;
    private float skill_05_stayedTime = 5f;
    private float skill_05_timer = 30f;
    ///////////////////////////////////////<skill05/>

    void Start()
    {
        brave = transform.GetComponent<BraveController>();
        mLife = brave.getLife();
    }

    void Update()
    {
        if (select_skill_04)
        {
            if (use_skill_04)
            {
                if (mLife.mHp >= mLife.MAXHP * 0.5f)
                {
                    brave.setAtk(brave.getCurrAtk() + brave.getBaseAtk() * skill_04_num);
                    DisappearBuff04();
                    use_skill_04 = false;
                }
            }
            else
            {
                if (mLife.mHp < mLife.MAXHP * 0.5f)
                {
                    brave.setAtk(brave.getCurrAtk() - brave.getBaseAtk() * skill_04_num);
                    ShowBuff04();
                    use_skill_04 = true;
                }
            }
        }
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
                    mLife.mDef -= 99999;
                }
                else
                {
                    skill_05 = true;
                    skill_05_timer = skill_05_stayedTime;
                    mLife.mDef += 99999;
                    loadBuffer("armor-increase-buff", skill_05_stayedTime);
                    /*
                    GameObject buffer = ObjectPool.GetInstant().loadResource<GameObject>("armor-increase-buff");
                    buffer = Instantiate(buffer);
                    buffer.transform.position = new Vector3(transform.position[0], transform.position[1] + 0.7f, transform.position[2]);
                    buffer.SetActive(true);
                    buffer.transform.parent = brave.transform;
                    Destroy(buffer, skill_05_stayedTime);
                    */
                }
            }
        }
    }

    public void Use_Skill_01()
    {
        //概率型技能实现
        if (select_skill_01)
        {
            if (judgePercent(skill_01_percent))
            {
                mLife.mHp += mLife.MAXHP * skill_01_num;
                if (mLife.mHp > mLife.MAXHP)
                {
                    mLife.mHp = mLife.MAXHP;
                }
                loadBuffer("damage-increase-target-large-additive", 5f);
            }
        }

    }

    public void Use_Skill_02(bool needBack)
    {
        //概率型技能实现
        if (select_skill_02)
        {
            if (judgePercent(skill_02_percent))
            {
                ObjectPool.GetInstant().GetObj("SlashWaveBlue", magicCircle.transform.position, transform.localRotation).GetComponent<SlashWaveBlueController>().atk = brave.getCurrAtk() * skill_01_num;
                if (needBack)
                    ObjectPool.GetInstant().GetObj("SlashWaveBlue", magicCircleBack.transform.position, magicCircleBack.transform.rotation).GetComponent<SlashWaveBlueController>().atk = brave.getCurrAtk() * skill_01_num;
            }
        }
    }


    public void loadBuffer(string resourceName,float stayedTime)
    {
        GameObject buffer = ObjectPool.GetInstant().loadResource<GameObject>(resourceName);
        buffer = Instantiate(buffer);
        buffer.transform.position = new Vector3(transform.position[0], transform.position[1] + 0.7f, transform.position[2]);
        buffer.SetActive(true);
        buffer.transform.parent = brave.transform;
        Destroy(buffer, stayedTime);
    }

    public void ShowBuff04()
    {
        use_skill_04 = true;
        skill_04_buff = ObjectPool.GetInstant().loadResource<GameObject>("fist-buff");
        skill_04_buff = Instantiate(skill_04_buff);
        skill_04_buff.transform.position = new Vector3(transform.position[0], transform.position[1] + 0.7f, transform.position[2]);
        skill_04_buff.SetActive(true);
        skill_04_buff.transform.parent = brave.transform;
    }
    public void DisappearBuff04()
    {
        use_skill_04 = false;
        if (skill_04_buff != null)
        {
            Destroy(skill_04_buff);
        }
    }


    //概率判断函数
    public static bool judgePercent(float percent)
    {
        return Random.Range(0, 100) <= percent;
    }

}
