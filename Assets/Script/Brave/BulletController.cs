using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void destoryy()
    {
        ObjectPool.GetInstant().SaveObj(transform.gameObject);
    }
    void OnEnable()
    {
        Invoke("destoryy", 10);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * 10 * Time.deltaTime,Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            Animator enemyAnimator = other.transform.GetComponent<Animator>();
            enemyAnimator.SetTrigger("GetHit");
            ObjectPool.GetInstant().SaveObj(transform.gameObject);
        }
    }
}
