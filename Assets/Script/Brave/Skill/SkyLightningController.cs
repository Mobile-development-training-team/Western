using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyLightningController : MonoBehaviour
{
    //private Attack attack1;
    //private Attack attack2;
    //private Attack attack3;
    //private Attack attack4;

    private Attack attack;
    public float atk;
    public float speed = 30f;
    public float flyTime = 3f;
    public GameObject mhit1;
    public GameObject mhit1_1;
    public GameObject mhit2;
    public GameObject mhit3;
    public GameObject mhit4;

    void OnEnable()
    {
        attack = new Attack();
        attack.mTeam = 1;
        attack.mAtk = atk;
        Invoke("SaveLightning", flyTime);       //flyTime秒后回收天雷
    }
    //超出射程回收天雷
    void SaveLightning()
    {
        ObjectPool.GetInstant().SaveObj(transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
    }
    //击中目标
    Life theOtherLife;
    Vector3 theOtherPos;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("brave"))
        {
            return;
        }
        if (other.gameObject.tag.Equals("Enemy"))
        {
            Life otherLife = other.gameObject.GetComponent<Life>();
            Hit1(otherLife, other.transform.position);
            StartCoroutine(Hit2(otherLife, other.transform.position));
            StartCoroutine(Hit3(otherLife, other.transform.position));
            StartCoroutine(Hit4(otherLife, other.transform.position));
        }
    }
    ////////////////////////////////////////////////////////////////////<攻击特效>
    public void Hit1(Life otherLife,Vector3 pos)
    {
        if (otherLife != null)
        {
            attack.mAtk = atk * 0.3f;
            attack.attack(otherLife);
            //GameUIController.AddRythmCount(3f);
        }
        if (mhit1 != null&&mhit1_1!=null)
        {
            var hitInstance1 = Instantiate(mhit1, new Vector3(pos[0], pos[1] + 1f, pos[2]), Quaternion.identity);
            var hitInstance1_1 = Instantiate(mhit1_1, new Vector3(pos[0], pos[1] + 1f, pos[2]), Quaternion.identity);
            var hitPs = hitInstance1.GetComponent<ParticleSystem>();
            if (hitPs != null)
            {
                Destroy(hitInstance1, 1.2f);
            }
            Destroy(hitInstance1_1, 1.2f);
        }
    }
    IEnumerator Hit2(Life otherLife, Vector3 pos)
    {
        yield return new WaitForSeconds(0.4f);
        if (otherLife != null)
        {
            attack.mAtk = atk * 0.6f;
            attack.attack(otherLife);
            //GameUIController.AddRythmCount(3f);
        }
        if (mhit2 != null)
        {
            var hitInstance2 = Instantiate(mhit2, new Vector3(pos[0], pos[1] + 1f, pos[2]), Quaternion.identity);
            var hitPs = hitInstance2.GetComponent<ParticleSystem>();
            if (hitPs != null)
            {
                Destroy(hitInstance2, 1.2f);
            }
        }
    }
    IEnumerator Hit3(Life otherLife, Vector3 pos)
    {
        yield return new WaitForSeconds(0.8f);
        if (otherLife != null)
        {
            attack.mAtk = atk * 0.9f;
            attack.attack(otherLife);
            //GameUIController.AddRythmCount(3f);
        }
        if (mhit3 != null)
        {
            var hitInstance3 = Instantiate(mhit3, new Vector3(pos[0], pos[1] + 1f, pos[2]), Quaternion.identity);
            var hitPs = hitInstance3.GetComponent<ParticleSystem>();
            if (hitPs != null)
            {
                Destroy(hitInstance3, 1.2f);
            }
        }
    }
    IEnumerator Hit4(Life otherLife, Vector3 pos)
    {
        yield return new WaitForSeconds(1.2f);
        if (otherLife != null)
        {
            //attack.mAtk = atk * 1.2f;
            attack.mAtk = 99999f;
            attack.attack(otherLife);
            //GameUIController.AddRythmCount(3f);
        }
        if (mhit3 != null)
        {
            var hitInstance3 = Instantiate(mhit4, new Vector3(pos[0], pos[1] + 1f, pos[2]), Quaternion.identity);
            var hitPs = hitInstance3.GetComponent<ParticleSystem>();
            if (hitPs != null)
            {
                Destroy(hitInstance3, 1.2f);
            }
        }
    }
    /*
    public void Hit4(Vector3 pos)
    {
        if (mhit4 != null)
        {
            var hitInstance4 = Instantiate(mhit4, pos, Quaternion.identity);
            if (hitInstance4 == null)
                Debug.Log("hitInstance4 is null");
            var hitPs = hitInstance4.GetComponent<ParticleSystem>();
            if (hitPs != null)
            {
                Destroy(hitInstance4, hitPs.main.duration);
            }
        }
    }
     * */
    ////////////////////////////////////////////////////////////////////<攻击特效/>
}
