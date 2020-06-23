using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMagicBulletController : MonoBehaviour
{
    public GameObject hit;
    public GameObject flash;
    private Rigidbody rb;

    private Attack attack;
    public float atk = 20f;
    public float speed = 5f;
    public float flyTime = 1f;

    void OnEnable()
    {
        attack = new Attack();
        attack.mTeam = 2;
        attack.mAtk = atk;
        Invoke("SaveMagic", flyTime);       //1秒后回收子弹


        rb = GetComponent<Rigidbody>();
        if (flash != null)
        {
            var flashInstance = Instantiate(flash, transform.position, Quaternion.identity);
            flashInstance.transform.forward = gameObject.transform.forward;
            var flashPs = flashInstance.GetComponent<ParticleSystem>();
            if (flashPs != null)
            {
                Destroy(flashInstance, flashPs.main.duration);
            }
            else
            {
                var flashPsParts = flashInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(flashInstance, flashPsParts.main.duration);
            }
        }

    }

    //超出射程回收子弹
    void SaveMagic()
    {
        ObjectPool.GetInstant().SaveObj(transform.gameObject);
    }

    void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
    }
    //击中目标
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("brave"))
        {
            Life otherLife = other.gameObject.GetComponent<Life>();
            if (otherLife != null)
            {
                attack.attack(otherLife);
            }
            Hit(other);
            ObjectPool.GetInstant().SaveObj(transform.gameObject);
            if (IsInvoking("SaveMagic"))
            {
                CancelInvoke("SaveMagic");
            }
        }
    }
    public void Hit(Collider collider)
    {
        if (hit != null)
        {
            var hitInstance = Instantiate(hit, transform.position, Quaternion.identity);
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
}
