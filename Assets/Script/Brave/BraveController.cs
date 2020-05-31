using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BraveController : MonoBehaviour
{
    private Animator mAnimator;
    private GameObject RightHand;
    private GameObject THandSword;
    private GameObject Gun;
    private GameObject laser;
    private GameObject muzzle;

    private bool beDoingSomethings = false;
    private bool walking = false;
    private bool running = false;
    private bool death = false;
    private int AttackIndex = 0;
    private int WeaponIndex = 0;
    private int Ammunition = 10;
    private float horizontal = 0;
    private float vertical = 0;
    private bool LeftWalking = false;
    private bool RightWalking = false;
    private bool LeftRunning = false;
    private bool RightRunning = false;
    private bool X = false;
    private bool Y = false;
    private bool Z = false;

    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
        mAnimator.SetInteger("AttackIndex", 0);
        mAnimator.SetInteger("WeaponIndex", 0);
        mAnimator.SetInteger("Ammunition", 10);
        mAnimator.SetBool("Walking", false);
        mAnimator.SetBool("Running", false);
        mAnimator.SetBool("Death", false);

        RightHand = GameObject.FindGameObjectWithTag("RightHand");
        THandSword = RightHand.transform.Find("2Hand-Sword Variant").gameObject;
        Gun = RightHand.transform.Find("2Hand-Rifle").gameObject;
        laser = Gun.transform.Find("laser").gameObject;
        muzzle = Gun.transform.Find("muzzle").gameObject;

        THandSword.SetActive(true);
        THandSword.GetComponent<BoxCollider>().enabled = false;

        Gun.SetActive(false);
        laser.SetActive(false);
    }

    // Update is called once per frame
    ////////////////////////////////////////////////////////////////////<监听UI>
    void Update()
    {
        //键盘输入版本（废弃）

        /*
        //horizontal = Input.GetAxis("Horizontal");
        //vertical = Input.GetAxis("Vertical");
        walking = false;
        running = false;

        //算出方向向量
        Vector3 direction = new Vector3(horizontal, 0, vertical);

        if (direction != Vector3.zero)
        {
            //行走
            walking = true;
            mAnimator.SetBool("Walking", walking);
            AttackIndex = 0;

            //将角色旋转至指定方向
            transform.rotation = Quaternion.LookRotation(direction);

            //加速
            if (Input.GetKey(KeyCode.Z))
            {
                //Z加速
                running = true;
                mAnimator.SetBool("Running", running);
            }
            else
            {
                //普通速度
                running = false;
                mAnimator.SetBool("Running", running);
            }

        }
        else
        {
            //站立
            walking = false;
            mAnimator.SetBool("Walking", walking);
            AttackIndex = 0;
        }
        */
        /*if (!beDoingSomethings)
        {
            //双手剑平A
            if (WeaponIndex == 0 && Input.GetKeyDown(KeyCode.X))
            {
                beDoingSomethings = true;
                mAnimator.SetInteger("AttackIndex", AttackIndex);
                mAnimator.SetTrigger("Attacking");
                AttackIndex = (AttackIndex + 1) % 4;
            }
            //双手剑技能
            if (WeaponIndex == 0 && Input.GetKeyDown(KeyCode.C))
            {
                beDoingSomethings = true;
                mAnimator.SetTrigger("Skill01");
            }
            //枪支射击
            if (WeaponIndex == 1 && Input.GetKeyDown(KeyCode.X))
            {
                beDoingSomethings = true;
                if (Ammunition > 0)
                {
                    mAnimator.SetTrigger("Shooting");
                }
                else
                {
                    mAnimator.SetTrigger("Reload");
                }
            }
            //枪支换弹
            if (WeaponIndex == 1 && Input.GetKeyDown(KeyCode.C))
            {
                beDoingSomethings = true;
                mAnimator.SetTrigger("Reload");
            }
            //换武器
            if (Input.GetKeyDown(KeyCode.V))
            {
                beDoingSomethings = true;
                mAnimator.SetInteger("WeaponIndex", WeaponIndex);
                mAnimator.SetTrigger("ChangeWeapon");
            }
        }*/
    }

    void OnGUI()
    {
        //按钮输入版本

        walking = false;
        running = false;

        LeftWalking = GUI.RepeatButton(new Rect(0, 0, 100, 30), "LeftWalk");
        RightWalking = GUI.RepeatButton(new Rect(100, 0, 100, 30), "RightWalk");
        LeftRunning = GUI.RepeatButton(new Rect(0, 30, 100, 30), "LeftRun");
        RightRunning = GUI.RepeatButton(new Rect(100, 30, 100, 30), "RightRun");
        X = GUI.RepeatButton(new Rect(0, 60, 100, 30), "X");
        Y = GUI.RepeatButton(new Rect(0, 90, 100, 30), "Y");
        Z = GUI.RepeatButton(new Rect(0, 120, 100, 30), "Z");

        if (LeftWalking)
        {
            walk(new Vector3(-1, 0, 0));
        }
        else if(RightWalking)
        {
            walk(new Vector3(1, 0, 0));
        }
        else if (LeftRunning)
        {
            run(new Vector3(-1, 0, 0));
        }
        else if(RightRunning)
        {
            run(new Vector3(1, 0, 0));
        }
        else
        {
            idle();
        }
        if (!beDoingSomethings)
        {
            if (X)
            {
                XButton();
            }
            if (Y)
            {
                YButton();
            }
            if (Z)
            {
                ZButton();
            }
        }
    }
    ////////////////////////////////////////////////////////////////////<监听UI/>

    ////////////////////////////////////////////////////////////////////<控制运动状态>
    public void walk(Vector3 vector)
    {
        //将角色旋转至指定方向
        transform.rotation = Quaternion.LookRotation(vector);
        //行走
        AttackIndex = 0;
        walking = true;
        mAnimator.SetBool("Walking", walking);

    }
    public void run(Vector3 vector)
    {
        //将角色旋转至指定方向
        transform.rotation = Quaternion.LookRotation(vector);
        //奔跑
        AttackIndex = 0;
        walking = true;
        running = true;
        mAnimator.SetBool("Walking", walking);
        mAnimator.SetBool("Running", running);

    }
    public void idle() 
    {
        //站立
        walking = false;
        running = false;
        mAnimator.SetBool("Walking", walking);
        mAnimator.SetBool("Running", running);
    }
    ////////////////////////////////////////////////////////////////////<控制运动状态/>

    ////////////////////////////////////////////////////////////////////<按钮事件>
    public void XButton() 
    {
        beDoingSomethings = true;
        mAnimator.SetBool("beDoingSomethings", beDoingSomethings);
        if (WeaponIndex == 0)
        {
            TwoHandSwordAttack();
        }
        else if (WeaponIndex == 1)
        {
            TwoHandRifleShoot();
        }
        else
        {
            beDoingSomethings = false;
            mAnimator.SetBool("beDoingSomethings", beDoingSomethings);
        }
    }
    public void YButton() 
    {
        beDoingSomethings = true;
        mAnimator.SetBool("beDoingSomethings", beDoingSomethings);
        if (WeaponIndex == 0)
        {
            TwoHandSwordSkill();
        }
        else if (WeaponIndex == 1)
        {
            TwoHandRifleReload();
        }
        else
        {
            beDoingSomethings = false;
            mAnimator.SetBool("beDoingSomethings", beDoingSomethings);
        }
    }
    public void ZButton() 
    {
        beDoingSomethings = true;
        mAnimator.SetBool("beDoingSomethings", beDoingSomethings);
        ChangeWeapon();
    }
    ////////////////////////////////////////////////////////////////////<按钮事件/>


    ////////////////////////////////////////////////////////////////////<控制动画>
    public void TwoHandSwordAttack() 
    {
        mAnimator.SetInteger("AttackIndex", AttackIndex);
        mAnimator.SetTrigger("Attacking");
        AttackIndex = (AttackIndex + 1) % 4;
    }
    public void TwoHandSwordSkill() 
    {
        mAnimator.SetTrigger("Skill01");
    }
    public void TwoHandRifleShoot() 
    {
        if (Ammunition > 0)
        {
            mAnimator.SetTrigger("Shooting");
        }
        else
        {
            mAnimator.SetTrigger("Reload");
        }
    }
    public void TwoHandRifleReload() 
    {
        mAnimator.SetTrigger("Reload");
    }
    public void ChangeWeapon()
    {
        mAnimator.SetInteger("WeaponIndex", WeaponIndex);       //WeaponIndex是当前武器序号
        mAnimator.SetTrigger("ChangeWeapon");
    }
    ////////////////////////////////////////////////////////////////////<控制动画/>


    ////////////////////////////////////////////////////////////////////<动画的回调函数>
    public void startHit()
    {
        THandSword.GetComponent<BoxCollider>().enabled = true;
    }
    public void endHit()
    {
        THandSword.GetComponent<BoxCollider>().enabled = false;
        beDoingSomethings = false;
        mAnimator.SetBool("beDoingSomethings", beDoingSomethings);
    }

    public void startShooting()
    {
        //laser.SetActive(true);
        ObjectPool.GetInstant().GetObj("Bullet", muzzle.transform.position, transform.localRotation);
        
    }
    public void endShooting()
    {
        laser.SetActive(false);
        Ammunition = Ammunition - 1;
        mAnimator.SetInteger("Ammunition", Ammunition);
        beDoingSomethings = false;
        mAnimator.SetBool("beDoingSomethings", beDoingSomethings);
    }
    public void startReload()
    {

    }
    public void endReload()
    {
        mAnimator.SetInteger("Ammunition", 10);
        Ammunition = 10;
        beDoingSomethings = false;
        mAnimator.SetBool("beDoingSomethings", beDoingSomethings);
    }
    public void startChangeWeapon()
    {
        if (WeaponIndex == 0)
        {
            THandSword.SetActive(false);
        }
        else if (WeaponIndex == 1)
        {
            Gun.SetActive(false);
        }
        else
        {

        }
    }
    public void endChangeWeapon()
    {
        WeaponIndex = (WeaponIndex + 1) % 2;
        mAnimator.SetInteger("WeaponIndex", WeaponIndex);
        if (WeaponIndex == 0)
        {
            THandSword.SetActive(true);
        }
        else if (WeaponIndex == 1)
        {
            Gun.SetActive(true);
        }
        else
        {

        }
        beDoingSomethings = false;
        mAnimator.SetBool("beDoingSomethings", beDoingSomethings);
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
    private void OnTriggerEnter(Collider other)
    {
        if (beDoingSomethings)
        {
            if (other.gameObject.tag.Equals("Enemy"))
            {
                Animator enemyAnimator = other.transform.GetComponent<Animator>();
                enemyAnimator.SetTrigger("GetHit");
            }
        }

    }
    ////////////////////////////////////////////////////////////////////<碰撞检测及处理/>

    ////////////////////////////////////////////////////////////////////<回归初始状态>
    public void clearStatus()
    {
        beDoingSomethings = false;
        AttackIndex = 0;
    }
    ////////////////////////////////////////////////////////////////////<回归初始状态/>

}
