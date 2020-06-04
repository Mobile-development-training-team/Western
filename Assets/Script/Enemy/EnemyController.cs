using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
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

    void Awake()
    {
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
    }

    // Update is called once per frame
    void Update()
    {
        ///////////////////////////////<3秒后死亡>
        time1 -= Time.deltaTime;
        if (time1 <= 0)
        {
            death = true;
        }
        ///////////////////////////////<3秒后死亡/>

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
                ObjectPool.GetInstant().SaveObj(transform.gameObject);
                deathTime = 3f;
            }
            ///////////////////////////////<3秒后回收尸体/>

            return;
        }

        ///////////////////////////////<勇者是否在攻击范围内>
        if ((brave.transform.position.x - transform.position.x) > -2 && (brave.transform.position.x - transform.position.x) < 2)
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
            if (other.gameObject.tag.Equals("brave"))
            {
                other.gameObject.SendMessage("GetHit");
                //Animator braveAnimator = other.transform.GetComponent<Animator>();
                //braveAnimator.SetTrigger("GetHit");
            }
        }

    }
    ////////////////////////////////////////////////////////////////////<碰撞检测及处理/>

}
