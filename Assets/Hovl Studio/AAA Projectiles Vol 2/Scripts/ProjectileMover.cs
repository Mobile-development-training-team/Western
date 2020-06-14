using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMover : MonoBehaviour
{
    public float speed = 15f;
    public float hitOffset = 0f;
    public bool UseFirePointRotation;
    public Vector3 rotationOffset = new Vector3(0, 0, 0);
    public GameObject hit;
    public GameObject flash;
    private Rigidbody rb;
    public GameObject[] Detached;

    /*
    public Attack attack;
    public float atk = 20f;
    public float flyTime = 3f;

    void OnEnable()
    {
        attack = new Attack();
        attack.mTeam = 1;
        attack.mAtk = atk;
        Invoke("SaveMagic", flyTime);       //3秒后回收子弹
    }

    //超出射程回收子弹
    void SaveMagic()
    {
        ObjectPool.GetInstant().SaveObj(transform.gameObject);
    }
    */
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        if (flash != null)
        {
            var flashInstance = Instantiate(flash, transform.position, Quaternion.identity);
            //var flashInstance = ObjectPool.GetInstant().GetObj("MagicFlash01", transform.position, Quaternion.identity);
            flashInstance.transform.forward = gameObject.transform.forward;
            var flashPs = flashInstance.GetComponent<ParticleSystem>();
            if (flashPs != null)
            {
                Destroy(flashInstance, flashPs.main.duration);
                //ObjectPool.GetInstant().SaveObj(flashInstance);
            }
            else
            {
                var flashPsParts = flashInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(flashInstance, flashPsParts.main.duration);
                //ObjectPool.GetInstant().SaveObj(flashInstance);
            }
        }
        Destroy(gameObject,5);
	}

    void FixedUpdate ()
    {
		if (speed != 0)
        {
            rb.velocity = transform.forward * speed;
            //transform.position += transform.forward * (speed * Time.deltaTime);         
        }
	}

    //https ://docs.unity3d.com/ScriptReference/Rigidbody.OnCollisionEnter.html
    void OnCollisionEnter(Collision collision)
    {
        /*
        Life otherLife = collision.gameObject.GetComponent<Life>();
        if (otherLife != null)
        {
            attack.attack(otherLife);
        }*/
        //Lock all axes movement and rotation
        rb.constraints = RigidbodyConstraints.FreezeAll;
        speed = 0;

        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point + contact.normal * hitOffset;

        if (hit != null)
        {
            var hitInstance = Instantiate(hit, pos, rot);
            //var hitInstance = ObjectPool.GetInstant().GetObj("MagicHit01", pos, rot);
            if (UseFirePointRotation) { hitInstance.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 180f, 0); }
            else if (rotationOffset != Vector3.zero) { hitInstance.transform.rotation = Quaternion.Euler(rotationOffset); }
            else { hitInstance.transform.LookAt(contact.point + contact.normal); }

            var hitPs = hitInstance.GetComponent<ParticleSystem>();
            if (hitPs != null)
            {
                Destroy(hitInstance, hitPs.main.duration);
                //ObjectPool.GetInstant().SaveObj(hitInstance);
            }
            else
            {
                var hitPsParts = hitInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitInstance, hitPsParts.main.duration);
                //ObjectPool.GetInstant().SaveObj(hitInstance);
            }
        }
        foreach (var detachedPrefab in Detached)
        {
            if (detachedPrefab != null)
            {
                detachedPrefab.transform.parent = null;
            }
        }
        Destroy(gameObject);
        //ObjectPool.GetInstant().SaveObj(transform.gameObject);
        

    }
}
