using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyLightningController : MonoBehaviour
{
    private Attack attack1;
    private Attack attack2;
    private Attack attack3;
    private Attack attack4;
    public float speed = 30f;
    public float flyTime = 3f;
    public GameObject mhit1;
    public GameObject mhit1_1;
    public GameObject mhit2;
    public GameObject mhit3;
    public GameObject mhit4;

    void OnEnable()
    {
        attack1 = new Attack();
        attack1.mTeam = 1;
        attack1.mAtk = 5f;
        attack2 = new Attack();
        attack2.mTeam = 1;
        attack2.mAtk = 10f;
        attack3 = new Attack();
        attack3.mTeam = 1;
        attack3.mAtk = 15f;
        attack4 = new Attack();
        attack4.mTeam = 1;
        attack4.mAtk = 99999f;
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
            attack1.attack(otherLife);
            GameUIController.AddRythmCount(3f);
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
            attack2.attack(otherLife);
            GameUIController.AddRythmCount(3f);
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
            attack3.attack(otherLife);
            GameUIController.AddRythmCount(3f);
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
            attack4.attack(otherLife);
            GameUIController.AddRythmCount(3f);
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
