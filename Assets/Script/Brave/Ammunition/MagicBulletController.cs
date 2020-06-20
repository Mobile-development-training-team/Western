using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBulletController : MonoBehaviour
{
    public GameObject hit;
    public GameObject flash;
    private Rigidbody rb;
    
    private Attack attack;
    public float atk = 20f;
    public float speed = 10f;
    public float flyTime = 1f;

    void OnEnable()
    {
        attack = new Attack();
        attack.mTeam = 1;
        attack.mAtk = atk;
        Invoke("SaveMagic", flyTime);       //3秒后回收子弹


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
    
    /*
    void FixedUpdate()
    {
        if (speed != 0)
        {
            rb.velocity = transform.forward * speed;     
        }
    }
    */
    void OnCollisionEnter(Collision collision)
    {
        
        Life otherLife = collision.gameObject.GetComponent<Life>();
        if (otherLife != null)
        {
            attack.attack(otherLife);
        }
        Hit(collision);
        ObjectPool.GetInstant().SaveObj(transform.gameObject);
        if (IsInvoking("SaveMagic"))
        {
            CancelInvoke("SaveMagic");
        }
    }

    public void Hit(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point + contact.normal;

        if (hit != null)
        {
            var hitInstance = Instantiate(hit, pos, rot);
            hitInstance.transform.LookAt(contact.point + contact.normal);
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
        /*
        ObjectPool.GetInstant().SaveObj(transform.gameObject);
        if (IsInvoking("SaveBullet"))
        {
            CancelInvoke("SaveBullet");
        }
        */
    }
    public void Hit(Collider collider)
    {
        //ContactPoint contact = collider.contacts[0];
        //Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        //Vector3 pos = contact.point + contact.normal;
        

        if (hit != null)
        {
            var hitInstance = Instantiate(hit, transform.position, Quaternion.identity);
            //hitInstance.transform.LookAt(contact.point + contact.normal);
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
