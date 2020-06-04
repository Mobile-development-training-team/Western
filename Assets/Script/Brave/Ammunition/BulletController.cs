using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    void OnEnable()
    {
        Invoke("SaveBullet", 3);       //3秒后回收子弹
    }
    //超出射程回收子弹
    void SaveBullet()
    {
        ObjectPool.GetInstant().SaveObj(transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * 10 * Time.deltaTime,Space.World);
    }
    //击中目标
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            Animator enemyAnimator = other.transform.GetComponent<Animator>();
            enemyAnimator.SetTrigger("GetHit");
            ObjectPool.GetInstant().SaveObj(transform.gameObject);
            if (IsInvoking("SaveBullet"))
            {
                CancelInvoke("SaveBullet");
            }
        }
    }
}
