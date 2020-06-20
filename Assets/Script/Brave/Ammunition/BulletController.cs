using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Attack attack;
    public float atk = 20f;
    public float speed = 10f;
    public float flyTime = 3f;
    public GameObject mhit;

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
            GameUIController.AddRythmCount(3f);
        }
        Hit(other);
        ObjectPool.GetInstant().SaveObj(transform.gameObject);
        if (IsInvoking("SaveBullet"))
        {
            CancelInvoke("SaveBullet");
        }
    }
    ////////////////////////////////////////////////////////////////////<攻击特效>
    public void Hit(Collider collider)
    {
        if (mhit != null)
        {
            Vector3 pos = collider.transform.position;
            var hitInstance = Instantiate(mhit, new Vector3(pos[0], pos[1] + 1f, pos[2]), Quaternion.identity);
            var hitPs = hitInstance.GetComponent<ParticleSystem>();
            if (hitPs != null)
            {
                Destroy(hitInstance, hitPs.main.duration);
            }
            else
            {
                var hitPsParts = hitInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitInstance, hitPsParts.main.duration);
            }
        }
    }
    ////////////////////////////////////////////////////////////////////<攻击特效/>
}
