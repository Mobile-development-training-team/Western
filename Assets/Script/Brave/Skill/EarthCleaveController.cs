using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthCleaveController : MonoBehaviour
{
    /*
    private Attack attack;
    public float atk = 20f;
    public float speed = 20f;
    public float flyTime = 0.5f;
    public GameObject mhit;
    public GameObject collider;
    */
    void OnEnable()
    {
        /*
        collider.transform.position = transform.position;
        attack = new Attack();
        attack.mTeam = 1;
        attack.mAtk = atk;
        */
        Invoke("SaveEarthCleave", 1f);       //flyTime秒后回收地刺
    }
    //回收地刺
    void SaveEarthCleave()
    {
        ObjectPool.GetInstant().SaveObj(transform.gameObject);
    }

    //击中目标
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            /*
            Life otherLife = other.gameObject.GetComponent<Life>();
            if (otherLife != null)
            {
                if (otherLife.mHp > 0 && otherLife.mTeam != attack.mTeam)
                {
                    Hit(other);
                }
                attack.attack(otherLife);
                GameUIController.AddRythmCount(3f);
            }
            */
            other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0,10, 0), ForceMode.Impulse);
            //GameUIController.AddRythmCount(2f);
        }
    }
}
