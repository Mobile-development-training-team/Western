using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFireController : MonoBehaviour
{
    private Attack attack;
    public float atk = 20f;
    public float speed = 20f;
    public float flyTime = 0.5f;
    public GameObject mhit;
    public GameObject collider;

    void OnEnable()
    {
        collider.transform.position = transform.position;
        attack = new Attack();
        attack.mTeam = 1;
        attack.mAtk = atk;
        Invoke("SaveGroundFire", flyTime);       //flyTime秒后回收地火
    }
    //超出射程回收地火
    void SaveGroundFire()
    {
        ObjectPool.GetInstant().SaveObj(transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        collider.transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
    }
    //击中目标
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Hit object" + other.gameObject.name);
        if (other.gameObject.tag.Equals("Enemy"))
        {
            //Debug.Log("Hit enemy");
            Life otherLife = other.gameObject.GetComponent<Life>();
            if (otherLife != null)
            {
                if (otherLife.mHp > 0 && otherLife.mTeam != attack.mTeam)
                {
                    Hit(other);
                }
                attack.attack(otherLife);
                //GameUIController.AddRythmCount(3f);
            }
        }
    }
    ////////////////////////////////////////////////////////////////////<攻击特效>
    public void Hit(Collider collider)
    {
        if (mhit != null)
        {
            Vector3 pos = collider.transform.position;
            var hitInstance = Instantiate(mhit, new Vector3(pos[0], pos[1] + 1f , pos[2]), Quaternion.identity);
            var hitPs = hitInstance.GetComponent<ParticleSystem>();
            if (hitPs != null)
            {
                Destroy(hitInstance, 3f);
            }
        }
    }
    ////////////////////////////////////////////////////////////////////<攻击特效/>
}
