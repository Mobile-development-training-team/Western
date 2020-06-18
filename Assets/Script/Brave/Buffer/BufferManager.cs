using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BufferManager : MonoBehaviour
{
    private BraveController brave;
    Life tempLife;

    private bool buffer_03 = false;
    private float buffer_03_coolTime = 30f;
    private float buffer_03_timer = 30f;
    private bool buffer_04 = false;

    // Start is called before the first frame update
    void Start()
    {
        brave = transform.GetComponent<BraveController>();
        tempLife = brave.getLife();
    }

    // Update is called once per frame
    void Update()
    {
        //冷却+触发型技能实现
        if (buffer_03)
        {
            buffer_03_timer -= Time.deltaTime;
            if (buffer_03_timer <= 0)
            {
                tempLife.mHp += tempLife.MAXHP * 0.15f;
                if (tempLife.mHp > tempLife.MAXHP)
                {
                    tempLife.mHp = tempLife.MAXHP;
                }
                buffer_03_timer = buffer_03_coolTime;
            }
        }
    }

    public void getBuffer()
    {
        int bufferIndex = Random.Range(0, 4);
        if (bufferIndex == 0)
        {
            //黑暗权能
            float tempAtk = brave.getCurrAtk() * 1.4f;
            brave.setAtk(tempAtk);
        }
        else if (bufferIndex == 1)
        {
            //血气旺盛
            tempLife.MAXHP *= 1.4f;
            tempLife.mHp *= 1.4f;
        }
        else if (bufferIndex == 2)
        {
            //钢筋铁骨
            tempLife.mDef *= 1.4f;
        }
        else if (bufferIndex == 3)
        {
            //天堂谐音
            buffer_03 = true;
        }
        else if (bufferIndex == 4)
        {
            //一刀入魂
            buffer_04 = true;
        }
    }

    
    public void Use_Buffer_04(Attack attack,Life otherLife)
    {
        if (buffer_04 && judgePercent(6f))
        {
            Attack bufferAttack = new Attack();
            bufferAttack.mAtk = attack.mAtk * 4f;
            bufferAttack.mTeam = attack.mTeam;
            bufferAttack.attack(otherLife);
        }
        else
        {
            attack.attack(otherLife);
        }
    }

    //概率判断函数
    public static bool judgePercent(float percent)
    {
        return Random.Range(0, 100) <= percent;
    }
}
