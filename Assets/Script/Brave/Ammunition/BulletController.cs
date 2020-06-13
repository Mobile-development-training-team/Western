using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Attack attack;
    public float atk = 20f;
    public float speed = 10f;
    public float flyTime = 3f;

    void OnEnable()
    {
        attack = new Attack();
        attack.mTeam = 1;
        attack.mAtk = atk;
        Invoke("SaveBullet", flyTime);       //3秒后回收子弹
    }
    //超出射程回收子弹
    void SaveBullet()
    {
        ObjectPool.GetInstant().SaveObj(transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
    }
    //击中目标
    private void OnTriggerEnter(Collider other)
    {
        /*
        if (other.gameObject.tag.Equals("Enemy"))
        {
            Animator enemyAnimator = other.transform.GetComponent<Animator>();
            enemyAnimator.SetTrigger("GetHit");
            ObjectPool.GetInstant().SaveObj(transform.gameObject);
            if (IsInvoking("SaveBullet"))
            {
                CancelInvoke("SaveBullet");
            }
        }*/
        Life otherLife = other.gameObject.GetComponent<Life>();
        if (otherLife != null)
        {
            attack.attack(otherLife);
        }
        ObjectPool.GetInstant().SaveObj(transform.gameObject);
        if (IsInvoking("SaveBullet"))
        {
            CancelInvoke("SaveBullet");
        }
    }
}
