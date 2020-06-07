using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBallController : MonoBehaviour
{
    private Attack attack;

    void OnEnable()
    {
        attack = new Attack();
        attack.mTeam = 1;
        Invoke("SaveMagicBall", 1);       //1秒后回收魔法球
    }
    //超出射程回收魔法球
    void SaveMagicBall()
    {
        ObjectPool.GetInstant().SaveObj(transform.gameObject);
    }
    void Update()
    {
        transform.Translate(transform.forward * 6 * Time.deltaTime, Space.World);
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
            if (IsInvoking("SaveMagicBall"))
            {
                CancelInvoke("SaveMagicBall");
            }
        }*/
        Life otherLife = other.gameObject.GetComponent<Life>();
        if (otherLife != null)
        {
            attack.attack(otherLife);
        }
        ObjectPool.GetInstant().SaveObj(transform.gameObject);
        if (IsInvoking("SaveMagicBall"))
        {
            CancelInvoke("SaveMagicBall");
        }
    }
}
