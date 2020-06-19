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

        private bool beDoingSomethings = false;
        private bool walking = false;
        private bool running = false;
        private bool atAir = false;
        private bool death = false;
        private bool reviving = false;
        private int AttackIndex = 0;
        private int WeaponIndex = 0;
        private int Ammunition = 10;
        private float horizontal = 0;
        private float vertical = 0;
        private bool canUse_secondAttack = false;
        private float secondAttack_coolTime = 20f;

        public int mainWeaponIndex = 0;
        public int secondaryWeaponIndex = 1;
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

        private float deathTime = 3f;
        private int deaths = 0;//主角死亡次数

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
            }
            public void onDead()
            {
                //if (!brave.death && !brave.reviving && !brave.atAir)
                brave.Death();
            }
        }
        
        void Awake()
        {
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
            //自定义测试数据
            mLife.mHp = mLife.MAXHP;
            baseAtk = 20f;
            currAtk = baseAtk;
            attack.mAtk = currAtk;
            //从外面拿数据
            /*
            mLife.MAXHP = GameScript.GameRoleAttribute.HealthPointLimit;
            mLife.mHp = mLife.MAXHP;
            mLife.mDef = GameScript.GameRoleAttribute.Defence;
            baseAtk = GameScript.GameRoleAttribute.Attack;
            currAtk = baseAtk;
            attack.mAtk = currAtk;
            */
            RightHand = GameObject.FindGameObjectWithTag("RightHand");
            //THandSword = RightHand.transform.Find("2Hand-Sword Variant").gameObject;
            //Gun = RightHand.transform.Find("2Hand-Rifle").gameObject;
            //Wand = RightHand.transform.Find("Wand").gameObject;
            //Crossbow = RightHand.transform.Find("Crossbow").gameObject;
            muzzle = Gun.transform.Find("muzzle").gameObject;
            crossBowMuzzle = CrossBow.transform.Find("CrossbowMuzzle").gameObject;
            magicCircle = transform.Find("MagicCircle").gameObject;

            THandSword.SetActive(false);
            Gun.SetActive(false);
            Wand.SetActive(false);
            CrossBow.SetActive(false);
            THandSword.GetComponent<BoxCollider>().enabled = false;
            muzzle.SetActive(true);
            crossBowMuzzle.SetActive(true);
            magicCircle.SetActive(true);

            ArmdeMyselfe();
            usingMainWeapon = true;
        }

        public void ArmdeMyselfe()
        {
            if (mainWeaponIndex == 0)
            {
                mainWeapon = THandSword;
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
            else
            {
                secondaryWeapon = mainWeapon;
                secondaryWeaponIndex = mainWeaponIndex;
            }
            mainWeapon.SetActive(true);
        }

        // Update is called once per frame
        ////////////////////////////////////////////////////////////////////<监听UI>
        void Update()
        {
            if (death)
            {
                ///////////////////////////////<3秒后复活>
                
                deathTime -= Time.deltaTime;
                if (deathTime <= 0)
                {
                    Revive();
                    deathTime = 3f;
                }
                
                ///////////////////////////////<3秒后复活/>
                return;
            }
            if (secondAttack_coolTime > 0)
            {
                secondAttack_coolTime -= Time.deltaTime;
                if (secondAttack_coolTime <= 0)
                {
                    canUse_secondAttack = true;
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
            //transform.Translate(Vector3.forward * 1 * Time.deltaTime);

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

            if (!beDoingSomethings && !death && !reviving&&!atAir)
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
                else
                {
                    beDoingSomethings = false;
                    mAnimator.SetBool("beDoingSomethings", beDoingSomethings);
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
            if (!canUse_secondAttack)
            {
                return;
            }

            if (!beDoingSomethings && !death && !reviving&&!atAir)
            {
                secondAttack_coolTime = 20f;
                canUse_secondAttack = false;
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
                else if (WeaponIndex == 2)
                {
                    MagicWandSkill();
                }
                else
                {
                    beDoingSomethings = false;
                    mAnimator.SetBool("beDoingSomethings", beDoingSomethings);
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

            if (!beDoingSomethings&&!death&&!reviving&&!atAir)
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
        public void MagicWandShoot()
        {
            mAnimator.SetTrigger("Attacking");
        }
        public void MagicWandSkill()
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
                //暂定主角有10条命。如果死了10次就游戏结束
                if (deaths >= 10)
                {
                    GameManager.INSTANCE.GameOver(false);
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
            skillManager.Use_Skill_01();
            skillManager.Use_Skill_02(false);
        }
        public void skillLooping()
        {
            skillManager.Use_Skill_01();
            skillManager.Use_Skill_02(true);
        }
        public void endHit()
        {
            //THandSword.GetComponent<BoxCollider>().enabled = false;
            mainWeapon.GetComponent<BoxCollider>().enabled = false;
            beDoingSomethings = false;
            mAnimator.SetBool("beDoingSomethings", beDoingSomethings);
        }
        public void startShooting()
        {
            //ObjectPool.GetInstant().GetObj("Bullet", muzzle.transform.position, transform.localRotation);
            ObjectPool.GetInstant().GetObj("BraveCrossBowArrow", crossBowMuzzle.transform.position, transform.localRotation);
        }
        public void endShooting()
        {
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
        public void startMagic()
        {
            magicCircle.SetActive(true);
            ObjectPool.GetInstant().GetObj("Magic01", magicCircle.transform.position, transform.localRotation);
            //Instantiate(magic, magicCircle.transform.position, magicCircle.transform.rotation);
        }
        public void endMagic()
        {
            magicCircle.SetActive(false);
            beDoingSomethings = false;
            mAnimator.SetBool("beDoingSomethings", beDoingSomethings);
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

            ObjectPool.GetInstant().GetObj("Magic01", new Vector3(pos[0], pos[1] + 0.5f, pos[2]), transform.localRotation);
            ObjectPool.GetInstant().GetObj("Magic01", pos, transform.localRotation);
            ObjectPool.GetInstant().GetObj("Magic01", new Vector3(pos[0], pos[1] - 0.5f, pos[2]), transform.localRotation);
            
        }
        public void endBigMagic()
        {
            magicCircle.SetActive(false);
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
            beDoingSomethings = false;
            mAnimator.SetBool("beDoingSomethings", beDoingSomethings);

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
            beDoingSomethings = false;
            reviving = false;
            mLife.mHp = mLife.MAXHP;
            mLife.hasHp = true;
            endChangeWeapon();
            mAnimator.SetBool("beDoingSomethings", beDoingSomethings);
        }
        public void startJump()
        {
            this.GetComponent<Rigidbody>().AddForce(new Vector3(0, 7, 0), ForceMode.Impulse);
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
        }
        ////////////////////////////////////////////////////////////////////<攻击特效/>


        ////////////////////////////////////////////////////////////////////<技能和buff相关>
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
        ////////////////////////////////////////////////////////////////////<技能和buff相关/>
    }

//}
