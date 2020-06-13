using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float range = 3f;    //敌人攻击范围

    private Life mLife;
    private Attack attack;
    private LifeCallback callback;

    private Animator mAnimator;
    private Collider mcollider;
    private Rigidbody mrigidbody;
    private GameObject EnemyWeapon;
    private GameObject brave;

    private bool walking = false;
    private bool death = false;
    private bool beDoingSomethings = false;
    private bool meetBrave = false;
    private float deathTime = 5f;
    private float time1 = 3f;

    class LifeCallback : Life.LifeCallback
    {
        private EnemyController enemy;
        public LifeCallback(EnemyController enemy)
        {
            this.enemy = enemy;
        }
        public void onHurted()
        {
            enemy.GetHit();
        }
        public void onDead()
        {
            enemy.Death();
        }
    }

    void Awake()
    {
        attack = new Attack();
        mLife = GetComponent<Life>();
        callback = new LifeCallback(this);
        mLife.registerCallback(callback);
        attack.mTeam = mLife.mTeam;

        mAnimator = GetComponent<Animator>();
        mcollider = GetComponent<CapsuleCollider>();
        mrigidbody = GetComponent<Rigidbody>();
        brave = GameObject.FindGameObjectWithTag("brave");
        foreach (Transform child in gameObject.GetComponentsInChildren<Transform>())
        {
            if (child.name.Equals("EnemyWeapon"))
            {
                EnemyWeapon = child.gameObject;
            }
        }
    }

    void OnEnable()
    {
        EnemyWeapon.GetComponent<BoxCollider>().enabled = false;
        mcollider.enabled = true;
        mrigidbody.isKinematic = false;
        mAnimator.SetBool("Walking", false);
        mAnimator.SetBool("Death", false); 
        walking = false;
        death = false;
        beDoingSomethings = false;
        meetBrave = false;
        deathTime = 5f;
        time1 = 3f;

        mLife.mHp = mLife.MAXHP;
        mLife.hasHp = true;
    }

    void Update()
    {
        ///////////////////////////////<3秒后死亡>
        /*
        time1 -= Time.deltaTime;
        if (time1 <= 0)
        {
            death = true;
        }
         * */
        ///////////////////////////////<3秒后死亡/>

        ///////////////////////////////<掉出场景外回收敌人(保险)>
        if (transform.position[1] < -100)
        {
            Death();
        }
        ///////////////////////////////<掉出场景外回收敌人(保险)/>

        if (death)
        {
            mAnimator.SetBool("Death", death);
            mcollider.enabled = false;
            mrigidbody.isKinematic = true;
            EnemyWeapon.GetComponent<BoxCollider>().enabled = false;

            ///////////////////////////////<3秒后回收尸体>
            deathTime -= Time.deltaTime;
            if (deathTime <= 0)
            {
                transform.gameObject.SetActive(false);
                ObjectPool.GetInstant().SaveObj(transform.gameObject);
                deathTime = 3f;
            }
            ///////////////////////////////<3秒后回收尸体/>

            return;
        }

        ///////////////////////////////<勇者是否在攻击范围内>
        if ((brave.transform.position.x - transform.position.x) > -range && (brave.transform.position.x - transform.position.x) < range)
        {
            meetBrave = true;
        }
        else
        {
            meetBrave = false;
        }
        ///////////////////////////////<勇者是否在攻击范围内/>

        if (meetBrave)
        {
            beDoingSomethings = true;
            Attack();
        }

        walk(new Vector3(brave.transform.position.x - transform.position.x, 0, 0));
    }

    ////////////////////////////////////////////////////////////////////<控制运动状态>
    public void walk(Vector3 vector)
    {
        //将角色旋转至指定方向
        transform.rotation = Quaternion.LookRotation(vector);
        //transform.LookAt(vector.position[1]);
        //行走
        walking = true;
        mAnimator.SetBool("Walking", walking);

    }
    public void idle()
    {
        //站立
        walking = false;
        mAnimator.SetBool("Walking", walking);
    }
    ////////////////////////////////////////////////////////////////////<控制运动状态/>

    ////////////////////////////////////////////////////////////////////<控制动画>
    public void Attack()
    {
        mAnimator.SetTrigger("Attacking");
    }
    public void GetHit()
    {
        if (!death)
            mAnimator.SetTrigger("GetHit");
    }
    public void Death()
    {
        death = true;
        EnemiesManager01.Instance.EnemiesDestory();
        GameObject.Find("Main Camera").GetComponent<ShakeCamera>().isShake = true;
    }
    ////////////////////////////////////////////////////////////////////<控制动画/>

    ////////////////////////////////////////////////////////////////////<动画的回调函数>
    public void startHit()
    {
        EnemyWeapon.GetComponent<BoxCollider>().enabled = true;
    }
    public void endHit()
    {
        EnemyWeapon.GetComponent<BoxCollider>().enabled = false;
        beDoingSomethings = false;
    }
    public void startBowShooting()
    {
        ObjectPool.GetInstant().GetObj("EnemyArrow", EnemyWeapon.transform.position, transform.localRotation);
        EnemyWeapon.SetActive(false);
    }
    public void endBowShooting()
    {
        EnemyWeapon.SetActive(true);
    }
    ////////////////////////////////////////////////////////////////////<动画的回调函数/>




    ////////////////////////////////////////////////////////////////////<碰撞检测及处理>
    private void OnCollisionEnter(Collision collision)
    {
        //碰到地面
        if (collision.gameObject.name.Equals("Plane"))
        {
            return;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name.Equals("Plane"))
        {
            return;
        }
    }
    private void OnTriggerEnter(Collider other)
    {    
        if (beDoingSomethings)
        {
            Life otherLife = other.gameObject.GetComponent<Life>();
            if (otherLife != null)
            {
                attack.attack(otherLife);
            }
        }
    }
    ////////////////////////////////////////////////////////////////////<碰撞检测及处理/>

}
