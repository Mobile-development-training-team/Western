using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    GameManager gameManager;
    EnemiesManager enemiesManager;

    public float range = 0f;    //敌人攻击范围
    public float atk = 20f;

    private Life mLife;
    public Attack attack;
    private LifeCallback callback;

    private Animator mAnimator;
    private Collider mcollider;
    private Rigidbody mrigidbody;
    private GameObject EnemyWeapon;
    private GameObject ShootingPoint;
    private GameObject brave;

    private bool walking = false;
    private bool death = false;
    private bool beDoingSomethings = false;
    private bool meetBrave = false;
    private float deathTime = 5f;
    private float time1 = 3f;
    public bool atAir = false;

    public GameObject mhit;

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
        //Debug.Log("Enemy awake");
        attack = new Attack();
        /*
        if (attack == null)
        {
            Debug.Log("in the awake,attack is null");
        }
        else
        {
            Debug.Log("in the awake,attack is not null");
        }
        */
        mAnimator = GetComponent<Animator>();
        mcollider = GetComponent<CapsuleCollider>();
        mrigidbody = GetComponent<Rigidbody>();
        foreach (Transform child in gameObject.GetComponentsInChildren<Transform>())
        {
            if (child.name.Equals("EnemyWeapon"))
            {
                EnemyWeapon = child.gameObject;
            }
            if (child.name.Equals("ShootingPoint"))
            {
                ShootingPoint = child.gameObject;
            }
        }
        //Debug.Log("Enemy awake finished");
    }

    void OnEnable()
    {
        //Debug.Log("Enemy enable");
        mLife = GetComponent<Life>();
        callback = new LifeCallback(this);
        mLife.registerCallback(callback);
        gameManager = GameManager.INSTANCE;
        enemiesManager = gameManager.getEnemiesManager();
        brave = gameManager.getBrave().gameObject;

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
        attack.mTeam = mLife.mTeam;
        attack.mAtk = atk;
        //Debug.Log("enable atk=" + atk);
        mLife.hasHp = true;
        /*
        if (attack == null)
        {
            Debug.Log("in the enable,attack is null");
        }
        else
        {
            Debug.Log("in the enable,attack is not null");
        }
        Debug.Log("Enemy enable finished");
         * */
    }

    void Update()
    {
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


        ///////////////////////////////<身位高于勇者即在天空>
        if (transform.position[1] > brave.transform.position[1])
        {
            atAir = true;
        }
        else
        {
            atAir = false;
        }
        if (atAir)
        {
            idle();
            return;
        }
        ///////////////////////////////<身位高于勇者即在天空/>

        ///////////////////////////////<掉出场景外回收敌人(保险)>
        if (transform.position[1] < -100)
        {
            //Death();
            transform.position = new Vector3(brave.transform.position[0] + 20f, brave.transform.position[1] + 2, brave.transform.position[2]);
        }
        ///////////////////////////////<掉出场景外回收敌人(保险)/>
        if (brave.GetComponent<BraveController>().isDead())
        {
            idle();
            return;
        }
        else
        {
            walk(new Vector3(brave.transform.position.x - transform.position.x, 0, 0));
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
        //Debug.Log("attacking atk=" + atk);
        mAnimator.SetTrigger("Attacking");
    }
    public void GetHit()
    {
        if (!death)
            mAnimator.SetTrigger("GetHit");
    }
    public void Death()
    {
        if (!death)
        {
            death = true;
            enemiesManager.EnemiesDestory();
            GameObject.Find("Main Camera").GetComponent<ShakeCamera>().isShake = true;
        }
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
        ObjectPool.GetInstant().GetObj("EnemyArrow", EnemyWeapon.transform.position, transform.localRotation).gameObject.GetComponent<EnemyArrowController>().atk = attack.mAtk;
        EnemyWeapon.SetActive(false);
    }
    public void endBowShooting()
    {
        EnemyWeapon.SetActive(true);
    }
    public void startBigMagic()
    {
        ObjectPool.GetInstant().GetObj("EnemyMagic01", ShootingPoint.transform.position, transform.localRotation).gameObject.GetComponent<EnemyMagicBulletController>().atk = attack.mAtk;
    }
    public void endBigMagic()
    {
    }
    ////////////////////////////////////////////////////////////////////<动画的回调函数/>




    ////////////////////////////////////////////////////////////////////<碰撞检测及处理>
    private void OnCollisionEnter(Collision collision)
    {
        //碰到地面
        if (collision.gameObject.name.Equals("Plane"))
        {
            atAir = false;
            return;
        }
        else if(atAir)
        {
            transform.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(5, 5, 0), ForceMode.Impulse);
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
                if (otherLife.mHp > 0 && otherLife.mTeam != attack.mTeam)
                {
                    Hit(other);
                }
            }
        }
    }
    ////////////////////////////////////////////////////////////////////<碰撞检测及处理/>

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

    public void setAttribte(float HP, float Def, float ATK)
    {
        //Debug.Log("try to setAttribute");
        if (mLife == null)
        {
            mLife = new Life();
        }
        mLife.MAXHP = HP;
        mLife.mHp = mLife.MAXHP;
        mLife.mDef = Def;
        mLife.mTeam = 2;
        if (attack == null)
        {
            attack = new Attack();
        }
        attack.mAtk = ATK;
        attack.mTeam = mLife.mTeam;
        /*
        mLife.MAXHP = HP;
        mLife.mHp = mLife.MAXHP;
        mLife.mDef = Def;
        atk = ATK;
        attack.mAtk = ATK;
         * */
        //Debug.Log("setattribute atk=" + atk);
    }

}
