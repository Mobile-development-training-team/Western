using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LeoLuz.PropertyAttributes;
using UnityEngine.SceneManagement;

/*
namespace LeoLuz.PlugAndPlayJoystick
{
*/
    public class BraveController : MonoBehaviour
    {
        public GameManager gameManager;
        GameUIController gameUIController;

        private Animator mAnimator;
        private SkillManager skillManager;
        private BufferManager bufferManager;
        private GameObject RightHand;
        public GameObject THandSword;
        public GameObject Gun;
        private GameObject muzzle;
        public GameObject CrossBow;
        private GameObject crossBowMuzzle;
        public GameObject Wand;
        private GameObject magicCircle;
        private GameObject magicCircleBack;
        public GameObject Axe;
        public GameObject Bow;
        public GameObject BowArrow;
        public GameObject Spear;

        public GameObject EarthSlam;
        public GameObject EarthSphereBlast;

        private bool beDoingSomethings = false;
        private bool walking = false;
        private bool running = false;
        private bool atAir = false;
        private bool death = false;
        private bool reviving = false;
        private bool dashFall = false;
        //private float dashCoolTime = 1.5f;
        private int AttackIndex = 0;
        private int WeaponIndex = 0;
        private int Ammunition = 10;
        private float horizontal = 0;
        private float vertical = 0;

        public int mainWeaponIndex = 0;
        public int secondaryWeaponIndex = 0;
        private bool usingMainWeapon = true;
        private GameObject mainWeapon;
        private GameObject secondaryWeapon;
        private Attack mainWeaponAttack;
        private Attack secondaryWeaponAttack;

        private Life mLife;
        private Attack attack;
        private float baseAtk;
        private float currAtk;
        public GameObject mhit;
        //public float mBlock;
        public bool Blocking;
        public bool BlockBroken;

        private float groundYPos = 0;
        private float hight = 0;

        private float deathTime = 3f;
        private int deaths = 0;//主角死亡次数
        private float InvincibleTime = 0;   //复活后无敌时间3秒

        //(GUI按钮控制版本-废弃)
        /*
        private bool LeftWalking = false;
        private bool RightWalking = false;
        private bool LeftRunning = false;
        private bool RightRunning = false;
        private bool X = false;
        private bool Y = false;
        private bool Z = false;
        */

        class LifeCallback : Life.LifeCallback
        {
            private BraveController brave;
            public LifeCallback(BraveController brave)
            {
                this.brave = brave;
            }
            public void onHurted()
            {
                //if(!brave.death&&!brave.reviving&&!brave.atAir)
                brave.GetHit();
                //GameUIController.SubtractRythmCount(20f);
            }
            public void onDead()
            {
                //if (!brave.death && !brave.reviving && !brave.atAir)
                brave.Death();
                //GameUIController.SubtractRythmCount(999f);
            }
        }
        
        void Awake()
        {
            if (GameManager.INSTANCE == null)
            {
                return;
            }
            gameManager = GameManager.INSTANCE;
            gameManager.setBrave(this);

            mAnimator = GetComponent<Animator>();
            mAnimator.SetInteger("AttackIndex", 0);
            mAnimator.SetInteger("WeaponIndex", 0);
            mAnimator.SetInteger("Ammunition", 10);
            mAnimator.SetBool("Walking", false);
            mAnimator.SetBool("Running", false);
            mAnimator.SetBool("beDoingSomethings", false);
            mAnimator.SetBool("atAir", false);
            mAnimator.SetBool("Death", false);

            skillManager = GetComponent<SkillManager>();
            bufferManager = GetComponent<BufferManager>();

            mLife = GetComponent<Life>();
            LifeCallback callback = new LifeCallback(this);
            mLife.registerCallback(callback);
            attack = new Attack();
            mLife.hasHp = true;
            attack.mTeam = mLife.mTeam;

            deaths = 0;
            Blocking = false;
            BlockBroken = false;

            //自定义测试数据
            /*
            mLife.mHp = mLife.MAXHP;
            baseAtk = 20f;
            currAtk = baseAtk;
            attack.mAtk = currAtk;
            skillManager.skill_00_num = 3;
            */
            //从外面拿数据
            mLife.MAXHP = GameScript.GameRoleAttribute.HealthPointLimit;
            mLife.mHp = mLife.MAXHP;
            mLife.mDef = GameScript.GameRoleAttribute.Defence;
            baseAtk = GameScript.GameRoleAttribute.Attack;
            currAtk = baseAtk;
            attack.mAtk = currAtk;

            RightHand = GameObject.FindGameObjectWithTag("RightHand");
            muzzle = Gun.transform.Find("muzzle").gameObject;
            crossBowMuzzle = CrossBow.transform.Find("CrossbowMuzzle").gameObject;
            magicCircle = transform.Find("MagicCircle").gameObject;
            magicCircleBack = transform.Find("MagicCircleBack").gameObject;

            THandSword.SetActive(false);
            Gun.SetActive(false);
            Wand.SetActive(false);
            CrossBow.SetActive(false);
            Axe.SetActive(false);
            Spear.SetActive(false);
            Bow.SetActive(false);
            BowArrow.SetActive(false);
            THandSword.GetComponent<BoxCollider>().enabled = false;
            muzzle.SetActive(true);
            crossBowMuzzle.SetActive(true);
            magicCircle.SetActive(true);
            magicCircleBack.SetActive(true);
            Axe.GetComponent<BoxCollider>().enabled = false;
            Spear.GetComponent<BoxCollider>().enabled = false;
            BowArrow.GetComponent<BoxCollider>().enabled = false;
            SelectWeapon();
            ArmdeMyselfe();
            usingMainWeapon = true;
        }

        public void SelectWeapon()
        {
            if (DataManager.roleEquipment.HadMainWeapon())
            {
                switch (DataManager.roleEquipment.GetMainWeapon().item.GetQuality())
                {
                    case Quality.Normal:
                        mainWeaponIndex = 0;
                        break;
                    case Quality.Excellent:
                        mainWeaponIndex = 3;
                        break;
                    case Quality.Rare:
                        mainWeaponIndex = 4;
                        break;
                    case Quality.Epic:
                        mainWeaponIndex = 0;
                        break;
                    default:
                        mainWeaponIndex = 0;
                        break;
                }
            }
            else
            {
                mainWeaponIndex = 0;
            }
            if (DataManager.roleEquipment.HadAlternateWeapon())
            {
                switch (DataManager.roleEquipment.GetAlternateWeapon().item.GetQuality())
                {
                    case Quality.Normal:
                        secondaryWeaponIndex = 5;
                        break;
                    case Quality.Excellent:
                        secondaryWeaponIndex = 1;
                        break;
                    case Quality.Rare:
                        secondaryWeaponIndex = 2;
                        break;
                    case Quality.Epic:
                        secondaryWeaponIndex = 2;
                        break;
                    default:
                        secondaryWeaponIndex = 0;
                        break;
                }
            }
            else
            {
                secondaryWeaponIndex = 0;
            }
        }

        public void ArmdeMyselfe()
        {
            if (mainWeaponIndex == 0)
            {
                mainWeapon = THandSword;
            }
            else if (mainWeaponIndex == 3)
            {
                mainWeapon = Axe;
            }
            else if (mainWeaponIndex == 4)
            {
                mainWeapon = Spear;
            }
            else
            {
                mainWeapon = THandSword;
                mainWeaponIndex = 0;
            }

            if (secondaryWeaponIndex == 1)
            {
                //secondaryWeapon = Gun;
                secondaryWeapon = CrossBow;
            }
            else if (secondaryWeaponIndex == 2)
            {
                secondaryWeapon = Wand;
            }
            else if (secondaryWeaponIndex == 5)
            {
                secondaryWeapon = Bow;
            }
            else
            {
                secondaryWeapon = mainWeapon;
                secondaryWeaponIndex = mainWeaponIndex;
            }
            WeaponIndex = mainWeaponIndex;
            mAnimator.SetInteger("WeaponIndex", WeaponIndex);
            //mainWeapon.SetActive(true);
        }
        void Start()
        {
            gameUIController = gameManager.getUIController();
            gameUIController.SetSkillNumText(skillManager.getSkill_00_Num());
        }
        ////////////////////////////////////////////////////////////////////<监听UI>
        void Update()
        {
            if (death)
            {
                ///////////////////////////////<3秒后复活>
                if (deaths < 3)       //3条命
                {
                    deathTime -= Time.deltaTime;
                    if (deathTime <= 0)
                    {
                        Revive();
                        deathTime = 3f;
                    }
                }
                ///////////////////////////////<3秒后复活/>
                return;
            }
            if (InvincibleTime > 0)
            {
                InvincibleTime -= Time.deltaTime;
                if (InvincibleTime <= 0)
                {
                    mLife.mShield = 0;
                }
            }
            //手柄输入版本
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            //Debug.Log("horizontal is " + horizontal);
            if (horizontal != 0)
            {
                //将角色旋转至指定方向
                //transform.rotation = Quaternion.LookRotation(direction);
                //transform.Translate(Vector3.forward * horizontal *horizontal*10* Time.deltaTime);
                if (horizontal > -0.5 && horizontal < 0.5)
                {
                    walk(new Vector3(horizontal, 0, 0));
                }
                else
                {
                    run(new Vector3(horizontal, 0, 0));
                }
                /*
                if (walking && running && !beDoingSomethings)
                {
                    dashCoolTime -= Time.deltaTime;
                }
                else
                {
                    dashCoolTime = 1.5f;
                }
                */
                if (!atAir)
                {
                    /*
                    if (horizontal > -0.5 && horizontal < 0.5)
                    {
                        walk(new Vector3(horizontal, 0, 0));
                    }
                    else
                    {
                        run(new Vector3(horizontal, 0, 0));
                    }
                    */
                }
                else
                {
                    //将角色旋转至指定方向
                    transform.rotation = Quaternion.LookRotation(new Vector3(horizontal,0,0));
                    transform.Translate(Vector3.forward *Mathf.Abs(horizontal)* 5 * Time.deltaTime);
                }
            }
            else
            {
                idle();
            }
            hight = transform.position[1] - groundYPos;
            //手柄向上抬控制跳跃
            if (vertical > 0.5)
            {
                if (!beDoingSomethings && !atAir)
                {
                    beDoingSomethings = true;
                    mAnimator.SetBool("beDoingSomethings", beDoingSomethings);
                    Jump();
                }
            }
            if (!Blocking)
            {
                BlockBroken = false;
            }
            //手柄向下拉
            if (vertical < -0.95)
            {
                //Debug.Log("vertical="+vertical);
                if (!death)
                {
                    if (atAir)
                    {
                        //快速下降
                        if (hight>2 && mLife.mAp > 20f)        //&&mLife.mAp>20f
                        {
                            this.GetComponent<Rigidbody>().AddForce(new Vector3(0, -1000, 0), ForceMode.Impulse);
                            mLife.mAp -= 20f;       //mLife.mAp-=20f
                            dashFall = true;
                            Blocking = true;
                        }
                    }
                    else
                    {
                        //防御
                        if (mLife.mAp >= 20f && !BlockBroken)       //mBlock >= mLife.MAXHP * 0.2f
                        {
                            if (!Blocking)
                            {
                                mLife.mBlock = mLife.MAXHP * 0.2f;
                                Blocking = true;
                                //beDoingSomethings = true;
                                //mAnimator.SetBool("beDoingSomethings", beDoingSomethings);
                            }
                            else
                            {

                            }
                        }
                        else
                        {
                            //Blocking = false;
                        }
                    }
                }
            }
            else
            {
                Blocking = false;
                mLife.mBlock = 0;
            }
            
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

        //private bool quitButton = false;
        void OnGUI()
        {
            /*
            //退出场景按钮（暂用）
            quitButton = GUI.Button(new Rect(1180, 0, 100, 30), "退出关卡");
            if (quitButton)
            {
                SceneManager.LoadScene(0);
            }
            */
            //GUI按钮输入版本（废弃）
            /*
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
            else if (RightWalking)
            {
                walk(new Vector3(1, 0, 0));
            }
            else if (LeftRunning)
            {
                run(new Vector3(-1, 0, 0));
            }
            else if (RightRunning)
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
            */
        }
        ////////////////////////////////////////////////////////////////////<监听UI/>

        ////////////////////////////////////////////////////////////////////<控制运动状态>
        public void walk(Vector3 vector)
        {
            //将角色旋转至指定方向
            transform.rotation = Quaternion.LookRotation(vector);
            //行走
            AttackIndex = 0;
            mAnimator.SetInteger("AttackIndex", AttackIndex);
            walking = true;
            running = false;
            mAnimator.SetBool("Walking", walking);
            mAnimator.SetBool("Running", running);
            //transform.Translate(Vector3.forward * 1 * Time.deltaTime);
        }
        public void run(Vector3 vector)
        {
            //将角色旋转至指定方向
            transform.rotation = Quaternion.LookRotation(vector);
            //奔跑
            AttackIndex = 0;
            mAnimator.SetInteger("AttackIndex", AttackIndex);
            walking = true;
            running = true;
            mAnimator.SetBool("Walking", walking);
            mAnimator.SetBool("Running", running);
            /*
            if (dashCoolTime <= 0 && mLife.mAp > 0f)      //mLife.mAp>0f
            {
                transform.Translate(Vector3.forward * 2 * Time.deltaTime);
                mLife.mAp -= Time.deltaTime * 10f;//mLife.mAp-=Time.dealtime*7f
            }
            */
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
            /*
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
            */

            if (!beDoingSomethings && !death && !reviving && !atAir && !Blocking) 
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
                else if (WeaponIndex == 2)
                {
                    MagicWandShoot();
                }
                else if (WeaponIndex == 3)
                {
                    AxeAttack();
                }
                else if (WeaponIndex == 4)
                {
                    SpearAttack();
                }
                else if (WeaponIndex == 5)
                {
                    BowShoot();
                }
            }

        }
        public void YButton()
        {
            /*
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
            */
            if (!beDoingSomethings && !death && !reviving && !atAir && mLife.mAp > 30f && !Blocking)      
            {
                beDoingSomethings = true;
                mAnimator.SetBool("beDoingSomethings", beDoingSomethings);
                AttackIndex = 0;
                mAnimator.SetInteger("AttackIndex", AttackIndex);   
                mLife.mAp -= 30f;
                if (WeaponIndex == 0)
                {
                    TwoHandSwordSkill();
                }
                else if (WeaponIndex == 1)
                {
                    TwoHandRifleReload();
                }
                else if (WeaponIndex == 2)
                {
                    MagicWandSkill();
                }
                else if (WeaponIndex == 3)
                {
                    AxeSkill();
                }
                else if (WeaponIndex == 4)
                {
                    SpearSkill();
                }
                else if (WeaponIndex == 5)
                {
                    BowSkill();
                }
            }
        }
        public void ZButton()
        {
            /*
            beDoingSomethings = true;
            mAnimator.SetBool("beDoingSomethings", beDoingSomethings);
            ChangeWeapon();
            */

            if (!beDoingSomethings && !death && !reviving && !atAir && !Blocking)      
            {
                beDoingSomethings = true;
                mAnimator.SetBool("beDoingSomethings", beDoingSomethings);
                ChangeWeapon();
            }
        }
        public void WButton()
        {
            /*
            if (!beDoingSomethings && !atAir)
            {
                beDoingSomethings = true;
                mAnimator.SetBool("beDoingSomethings", beDoingSomethings);
                Jump();
            }
            */
            if (!death)      //&& mLife.mAp >= 50f
            {
                skillManager.Use_Skill_00();
            }
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
        public void AxeAttack()
        {
            mAnimator.SetInteger("AttackIndex", AttackIndex);
            mAnimator.SetTrigger("Attacking");
            AttackIndex = (AttackIndex + 1) % 4;
        }
        public void AxeSkill()
        {
            mAnimator.SetTrigger("Skill01");
        }
        public void SpearAttack()
        {
            mAnimator.SetInteger("AttackIndex", AttackIndex);
            mAnimator.SetTrigger("Attacking");
            AttackIndex = (AttackIndex + 1) % 4;
        }
        public void SpearSkill()
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
                    mLife.mAp -= 30f;
                    mAnimator.SetTrigger("Reload");
            }
        }
        public void TwoHandRifleReload()
        {
            mAnimator.SetTrigger("Reload");
        }
        public void MagicWandShoot()
        {
            mAnimator.SetTrigger("Attacking");
        }
        public void MagicWandSkill()
        {
            mAnimator.SetTrigger("Skill01");
        }
        public void BowShoot()
        {
            mAnimator.SetTrigger("Attacking");
        }
        public void BowSkill()
        {
            mAnimator.SetTrigger("Skill01");
        }
        public void ChangeWeapon()
        {
            mAnimator.SetInteger("WeaponIndex", WeaponIndex);       //WeaponIndex是当前武器序号
            mAnimator.SetTrigger("ChangeWeapon");
        }
        public void Jump()
        {
            mAnimator.SetTrigger("Jump");
        }
        public void Death()
        {
            if (!death)
            {
                beDoingSomethings = true;
                mainWeapon.GetComponent<BoxCollider>().enabled = false;
                mAnimator.SetBool("beDoingSomethings", beDoingSomethings);
                death = true;
                mAnimator.SetBool("Death", death);
                Hit(transform.GetComponent<CapsuleCollider>());
                deaths++;
                gameUIController.SetLifeNumText(3-deaths);
                //暂定主角有3条命。如果死了3次就游戏结束
                if (deaths >= 3)
                {
                    gameManager.GameOver(false);
                }
            }
        }
        public void Revive()
        {
            mAnimator.SetTrigger("Revive");
            death = false;
            reviving = true;
            mAnimator.SetBool("Death", death);
        }
        public void GetHit()
        {
            if (reviving || death || atAir || beDoingSomethings || running)
                return;
            else
            {
                mAnimator.SetTrigger("GetHit");
            }
        }
        ////////////////////////////////////////////////////////////////////<控制动画/>


        ////////////////////////////////////////////////////////////////////<动画的回调函数>
        public void startHit()
        {
            //THandSword.GetComponent<BoxCollider>().enabled = true;
            mainWeapon.GetComponent<BoxCollider>().enabled = true;
            //ObjectPool.GetInstant().GetObj("SlashWaveBlue", magicCircle.transform.position, transform.localRotation);
            skillManager.Use_Skill_02(false);
            skillManager.Use_Skill_03(false);
        }
        public void skillLooping()
        {
            skillManager.Use_Skill_02(true);
            skillManager.Use_Skill_03(true);
        }
        public void endHit()
        {
            //THandSword.GetComponent<BoxCollider>().enabled = false;
            mainWeapon.GetComponent<BoxCollider>().enabled = false;
            //if (!Blocking)
            //{
                beDoingSomethings = false;
                mAnimator.SetBool("beDoingSomethings", beDoingSomethings);
            //}
        }
        public void startShooting()
        {
            //ObjectPool.GetInstant().GetObj("Bullet", muzzle.transform.position, transform.localRotation);
            ObjectPool.GetInstant().GetObj("BraveCrossBowArrow", crossBowMuzzle.transform.position, transform.localRotation).GetComponent<CrossBowArrowController>().atk = currAtk;
        }
        public void endShooting()
        {
            Ammunition = Ammunition - 1;
            mAnimator.SetInteger("Ammunition", Ammunition);
            //if (!Blocking)
            //{
                beDoingSomethings = false;
                mAnimator.SetBool("beDoingSomethings", beDoingSomethings);
            //}
        }
        public void startReload()
        {

        }
        public void endReload()
        {
            mAnimator.SetInteger("Ammunition", 10);
            Ammunition = 10;
            //if (!Blocking)
            //{
                beDoingSomethings = false;
                mAnimator.SetBool("beDoingSomethings", beDoingSomethings);
            //}
        }
        public void startMagic()
        {
            magicCircle.SetActive(true);
            ObjectPool.GetInstant().GetObj("Magic01", magicCircle.transform.position, transform.localRotation).GetComponent<MagicBulletController>().atk = currAtk;
            //Instantiate(magic, magicCircle.transform.position, magicCircle.transform.rotation);
        }
        public void endMagic()
        {
            //magicCircle.SetActive(false);
            //if (!Blocking)
            //{
                beDoingSomethings = false;
                mAnimator.SetBool("beDoingSomethings", beDoingSomethings);
            //}
        }
        public void startBigMagic()
        {
            magicCircle.SetActive(true);
            Vector3 pos = magicCircle.transform.position;
            /*
            Instantiate(magic, new Vector3(pos[0], pos[1] + 0.5f, pos[2]), magicCircle.transform.rotation);
            Instantiate(magic, magicCircle.transform.position, magicCircle.transform.rotation);
            Instantiate(magic, new Vector3(pos[0], pos[1] - 0.5f, pos[2]), magicCircle.transform.rotation);
            */

            ObjectPool.GetInstant().GetObj("Magic01", new Vector3(pos[0], pos[1] + 0.5f, pos[2]), transform.localRotation).GetComponent<MagicBulletController>().atk = currAtk;
            ObjectPool.GetInstant().GetObj("Magic01", pos, transform.localRotation).GetComponent<MagicBulletController>().atk = currAtk;
            ObjectPool.GetInstant().GetObj("Magic01", new Vector3(pos[0], pos[1] - 0.5f, pos[2]), transform.localRotation).GetComponent<MagicBulletController>().atk = currAtk;
            
        }
        public void endBigMagic()
        {
            //magicCircle.SetActive(false);
            //if (!Blocking)
            //{
                beDoingSomethings = false;
                mAnimator.SetBool("beDoingSomethings", beDoingSomethings);
            //}
        }
        public void startBowShooting()
        {
            ObjectPool.GetInstant().GetObj("BraveArrow", BowArrow.transform.position, transform.localRotation).gameObject.GetComponent<BowArrowController>().atk = attack.mAtk;
            BowArrow.SetActive(false);
        }
        public void endBowShooting()
        {
            BowArrow.SetActive(true);
            beDoingSomethings = false;
            mAnimator.SetBool("beDoingSomethings", beDoingSomethings);
        }
        public void startBowSkill()
        {
            ObjectPool.GetInstant().GetObj("BraveArrow", new Vector3(BowArrow.transform.position[0], BowArrow.transform.position[1] + 0.3f, BowArrow.transform.position[2]), transform.localRotation).gameObject.GetComponent<BowArrowController>().atk = attack.mAtk;
            ObjectPool.GetInstant().GetObj("BraveArrow", BowArrow.transform.position, transform.localRotation).gameObject.GetComponent<BowArrowController>().atk = attack.mAtk;
            ObjectPool.GetInstant().GetObj("BraveArrow", new Vector3(BowArrow.transform.position[0], BowArrow.transform.position[1] - 0.3f, BowArrow.transform.position[2]), transform.localRotation).gameObject.GetComponent<BowArrowController>().atk = attack.mAtk;
            BowArrow.SetActive(false);
        }
        public void endBowSkill()
        {
            BowArrow.SetActive(true);
            beDoingSomethings = false;
            mAnimator.SetBool("beDoingSomethings", beDoingSomethings);
        }
        public void startChangeWeapon()
        {
            
            if (usingMainWeapon)
            {
                mainWeapon.SetActive(false);
                WeaponIndex = secondaryWeaponIndex;
                mAnimator.SetInteger("WeaponIndex", WeaponIndex);
                usingMainWeapon = false;
            }
            else
            {
                secondaryWeapon.SetActive(false);
                BowArrow.SetActive(false);
                WeaponIndex = mainWeaponIndex;
                mAnimator.SetInteger("WeaponIndex", WeaponIndex);
                usingMainWeapon = true;
            }
            
            /*
            if (WeaponIndex == 0)
            {
                THandSword.SetActive(false);
            }
            else if (WeaponIndex == 1)
            {
                Gun.SetActive(false);
            }
            else if (WeaponIndex == 2)
            {
                Wand.SetActive(false);
            }
            else
            {

            }
            WeaponIndex = (WeaponIndex + 1) % 3;
            mAnimator.SetInteger("WeaponIndex", WeaponIndex);
            */
        }
        public void endChangeWeapon()
        {
            
            if (usingMainWeapon)
            {
                mainWeapon.SetActive(true);
            }
            else
            {
                secondaryWeapon.SetActive(true);
                if (WeaponIndex == 5)
                {
                    BowArrow.SetActive(true);
                }
            }
            

            /*
            WeaponIndex = (WeaponIndex + 1) % 3;
            mAnimator.SetInteger("WeaponIndex", WeaponIndex);
            */
            /*
            if (WeaponIndex == 0)
            {
                THandSword.SetActive(true);
            }
            else if (WeaponIndex == 1)
            {
                Gun.SetActive(true);
            }
            else if (WeaponIndex == 2)
            {
                Wand.SetActive(true);
            }
            else
            {

            }
            */
            //if (!Blocking)
            //{
                beDoingSomethings = false;
                mAnimator.SetBool("beDoingSomethings", beDoingSomethings);
            //}

        }
        public void startDeath()
        {
        }
        public void endDeath()
        {
        }
        public void startRevive()
        {
        }
        public void endRevive()
        {
            reviving = false;
            mLife.mShield = 9999999f;
            InvincibleTime = 3f;
            mLife.mHp = mLife.MAXHP;
            mLife.hasHp = true;
            endChangeWeapon();
            //if (!Blocking)
            //{
                beDoingSomethings = false;
                mAnimator.SetBool("beDoingSomethings", beDoingSomethings);
            //}
        }
        public void startJump()
        {
            this.GetComponent<Rigidbody>().AddForce(new Vector3(0, 350, 0), ForceMode.Impulse);
        }
        public void AtAir()
        {
            atAir = true;
            mAnimator.SetBool("atAir", atAir);
        }
        public void endJump()
        {
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
                groundYPos = transform.position[1];
                hight = 0;
                if (atAir&&dashFall&&!death)
                {
                    if (EarthSlam != null)
                    {
                        var Instance = Instantiate(EarthSlam, transform.position, Quaternion.identity);
                        var Ps = Instance.GetComponent<ParticleSystem>();
                        if (Ps != null)
                        {
                            Destroy(Instance, Ps.main.duration);
                        }
                    }
                    if (EarthSphereBlast != null)
                    {
                        var Instance = Instantiate(EarthSphereBlast, transform.position, Quaternion.identity);
                        var Ps = Instance.GetComponent<ParticleSystem>();
                        if (Ps != null)
                        {
                            Destroy(Instance, Ps.main.duration);
                        }
                    }

                    ObjectPool.GetInstant().GetObj("EarthCleave", new Vector3(magicCircle.transform.position[0], magicCircle.transform.position[1] - 0.777f, magicCircle.transform.position[2]), transform.localRotation);
                    ObjectPool.GetInstant().GetObj("EarthCleave", new Vector3(magicCircleBack.transform.position[0], magicCircleBack.transform.position[1] - 0.777f, magicCircleBack.transform.position[2]), magicCircleBack.transform.rotation);
                }
                dashFall = false;
                
                atAir = false;
                mAnimator.SetBool("atAir", atAir);
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
                    //attack.attack(otherLife);
                    bufferManager.Use_Buffer_04(attack,otherLife);
                    skillManager.Use_Skill_01();
                    Hit(other);
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
            //GameUIController.AddRythmCount(3f);
        }
        ////////////////////////////////////////////////////////////////////<攻击特效/>


        ////////////////////////////////////////////////////////////////////<获取与设定勇者数据>
        public void setAtk(float setatk)
        {
            currAtk = setatk;
            attack.mAtk = currAtk;
        }
        public float getBaseAtk()
        {
            return baseAtk;
        }
        public float getCurrAtk()
        {
            return currAtk;
        }
        public Life getLife()
        {
            return mLife;
        }
        public int getSkillNum()
        {
            return skillManager.getSkill_00_Num();
        }
        public SkillManager getSkillManager()
        {
            return skillManager;
        }
        public BufferManager getBufferManager()
        {
            return bufferManager;
        }
        public bool isDead()
        {
            return death || reviving;
        }
        ////////////////////////////////////////////////////////////////////<获取与设定勇者数据/>

    }

//}
