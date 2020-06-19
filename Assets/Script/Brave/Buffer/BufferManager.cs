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

    void Start()
    {
        brave = transform.GetComponent<BraveController>();
        tempLife = brave.getLife();
    }

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
                loadBuffer("heal-sphere");
            }
        }
    }
    int bufferIndex = Random.Range(0, 6);
    public void getBuffer()
    {
        //int bufferIndex = Random.Range(0, 6);
        if (bufferIndex == 6)
        {
            bufferIndex = 0;
        }


        if (bufferIndex == 0)
        {
            //黑暗权能
            float tempAtk = brave.getCurrAtk() * 1.3f;
            brave.setAtk(tempAtk);
            loadBuffer("fist-target-large");
        }
        else if (bufferIndex == 1)
        {
            //血气旺盛
            tempLife.mHp += tempLife.MAXHP * 0.3f;
            tempLife.MAXHP *= 1.3f;
            loadBuffer("heal-target-large");
        }
        else if (bufferIndex == 2)
        {
            //钢筋铁骨
            if (tempLife.mDef >= 99999)
            {
                tempLife.mDef += (tempLife.mDef - 99999) * 0.3f;
            }
            else
            {
                tempLife.mDef *= 1.3f;
            }
            loadBuffer("armor-increase-target-large");
        }
        else if (bufferIndex == 3)
        {
            //圣洁之力
            tempLife.mShield += tempLife.MAXHP * 0.2f;
            //特效
        }
        else if (bufferIndex == 4)
        {
            //天堂谐音
            buffer_03 = true;
            tempLife.mHp += tempLife.MAXHP * 0.15f;
            if (tempLife.mHp > tempLife.MAXHP)
            {
                tempLife.mHp = tempLife.MAXHP;
            }
            loadBuffer("heal-sphere");
        }
        else if (bufferIndex == 5)
        {
            //一刀入魂
            buffer_04 = true;
            loadBuffer("armor-decrease-target-large");
        }
        bufferIndex++;
    }

    
    public void Use_Buffer_04(Attack attack,Life otherLife)
    {
        if (buffer_04 && judgePercent(10f))
        {
            Attack bufferAttack = new Attack();
            bufferAttack.mAtk = attack.mAtk * 4f;
            bufferAttack.mTeam = attack.mTeam;
            bufferAttack.attack(otherLife);
            loadBuffer("armor-decrease-target-large");
        }
        else
        {
            attack.attack(otherLife);
        }
    }

    public void loadBuffer(string resourceName)
    {
        GameObject buffer = ObjectPool.GetInstant().loadResource<GameObject>(resourceName);
        buffer = Instantiate(buffer);
        buffer.transform.position = new Vector3(transform.position[0], transform.position[1] + 0.7f, transform.position[2]);
        buffer.SetActive(true);
        buffer.transform.parent = brave.transform;
        Destroy(buffer, 5f);
    }

    //概率判断函数
    public static bool judgePercent(float percent)
    {
        return Random.Range(0, 100) <= percent;
    }
}
