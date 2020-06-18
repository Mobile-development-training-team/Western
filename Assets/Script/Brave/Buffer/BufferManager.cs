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
                loadBuffer("heal-sphere");
                /*
                GameObject buffer = ObjectPool.GetInstant().loadResource<GameObject>("heal-sphere");
                buffer = Instantiate(buffer);
                buffer.transform.position = new Vector3(transform.position[0], transform.position[1] + 0.7f, transform.position[2]);
                buffer.SetActive(true);
                buffer.transform.parent = brave.transform;
                Destroy(buffer, 5f);
                 * */
            }
        }
    }

    public void getBuffer()
    {
        float bufferIndex = Random.Range(0, 5);
        if (bufferIndex == 0)
        {
            //黑暗权能
            float tempAtk = brave.getCurrAtk() * 1.4f;
            brave.setAtk(tempAtk);
            loadBuffer("fist-target-large");
            /*
            //GameObject buffer = ObjectPool.GetInstant().GetObj("fist-buff", new Vector3(transform.position[0], transform.position[1] + 0.7f, transform.position[2]), brave.transform.localRotation);
            GameObject buffer = ObjectPool.GetInstant().loadResource<GameObject>("fist-buff");
            buffer = Instantiate(buffer);
            buffer.transform.position = new Vector3(transform.position[0], transform.position[1] + 0.7f, transform.position[2]);
            buffer.SetActive(true);
            buffer.transform.parent = brave.transform;
            Destroy(buffer, 5f);
             * */
        }
        else if (bufferIndex == 1)
        {
            //血气旺盛
            tempLife.MAXHP *= 1.4f;
            tempLife.mHp *= 1.4f;
            loadBuffer("heal-target-large");
            /*
            //GameObject buffer = ObjectPool.GetInstant().GetObj("heal-target-large", new Vector3(transform.position[0], transform.position[1] + 0.7f, transform.position[2]), brave.transform.localRotation);
            GameObject buffer = ObjectPool.GetInstant().loadResource<GameObject>("heal-target-large");
            buffer = Instantiate(buffer);
            buffer.transform.position = new Vector3(transform.position[0], transform.position[1] + 0.7f, transform.position[2]);
            buffer.SetActive(true);
            buffer.transform.parent = brave.transform;
            Destroy(buffer, 5f);
             * */
        }
        else if (bufferIndex == 2)
        {
            //钢筋铁骨
            tempLife.mDef *= 1.4f;
            loadBuffer("armor-increase-target-large");
            /*
            //GameObject buffer = ObjectPool.GetInstant().GetObj("armor-increase-buff", new Vector3(transform.position[0], transform.position[1] + 0.7f, transform.position[2]), brave.transform.localRotation);
            GameObject buffer = ObjectPool.GetInstant().loadResource<GameObject>("armor-increase-buff");
            buffer = Instantiate(buffer);
            buffer.transform.position = new Vector3(transform.position[0], transform.position[1] + 0.7f, transform.position[2]);
            buffer.SetActive(true);
            buffer.transform.parent = brave.transform;
            Destroy(buffer, 5f); ;
             * */
        }
        else if (bufferIndex == 3)
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
        else if (bufferIndex == 4)
        {
            //一刀入魂
            buffer_04 = true;
            loadBuffer("armor-decrease-target-large");
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
