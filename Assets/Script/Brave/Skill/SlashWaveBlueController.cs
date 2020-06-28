using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashWaveBlueController : MonoBehaviour
{
    private Attack attack;
    public float atk = 20f;
    public float speed = 30f;
    public float flyTime = 0.2f;
    public GameObject mhit;

    void OnEnable()
    {
        attack = new Attack();
        attack.mTeam = 1;
        attack.mAtk = atk;
        Invoke("SaveWave", flyTime);       //flyTime秒后回收剑气
    }
    //超出射程回收剑气
    void SaveWave()
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
        if (other.gameObject.tag.Equals("Enemy"))
        {
            Life otherLife = other.gameObject.GetComponent<Life>();
            if (otherLife != null)
            {
                if (otherLife.mHp > 0 && otherLife.mTeam != attack.mTeam)
                {
                    Hit(other);
                }
                attack.attack(otherLife);
            }
            /*
            ObjectPool.GetInstant().SaveObj(transform.gameObject);
            if (IsInvoking("SaveWave"))
            {
                CancelInvoke("SaveWave");
            }
            */
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
