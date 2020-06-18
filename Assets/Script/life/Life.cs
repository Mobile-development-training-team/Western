using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    public int mTeam = 1;
    public float mHp = 100f;
    public float MAXHP = 100f;
    public float mDef = 0f;

    private GameObject mHpBar;
    private Slider slider;
    private GameObject HpCanvas;

    public bool hasHp = true;
    void Awake()
    {
        if (hasHp)
        {
            HpCanvas = GameObject.Find("HpCanvas");
            mHpBar = ObjectPool.GetInstant().GetObj("HpBar", new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 0f));
            mHpBar = Instantiate(mHpBar);
            slider = mHpBar.GetComponent<Slider>();
            mHpBar.transform.parent = HpCanvas.transform;
        }
        //Debug.Log(mHpBar.transform.position);
    }
    /*
    void Start()
    {
        if (hasHp)
        {
            HpCanvas = GameObject.Find("HpCanvas");
            mHpBar = ObjectPool.GetInstant().GetObj("HpBar",new Vector3(0f,0f,0f),new Quaternion(0f,0f,0f,0f));
            mHpBar = Instantiate(mHpBar);
            slider = mHpBar.GetComponent<Slider>();
            mHpBar.transform.parent = HpCanvas.transform;
        }
        Debug.Log(mHpBar.transform.position);
    }
    */
    void OnEnable()
    {
        mHpBar.SetActive(true);
        mHp = MAXHP;
    }
    void Update()
    {
        if (hasHp)
        {
            mHpBar.SetActive(true);
            mHpBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2);
            slider.value = mHp / MAXHP;
            if (slider.value <= 0f)
            {
                mHpBar.SetActive(false);
                hasHp = false;
            }
        }
    }
    
    public float beAttacked(Attack attack)
    {
        if (hasHp)
        {
            float dmg = DamageCalculate.calDam(attack,this);

            if (dmg <= 0)
            {
                return 0;
            }

            if (mHp <= dmg)
            {
                dmg = mHp;
                mHp = 0f;
                if (mCallback != null)
                {
                    mCallback.onDead();
                }
            }
            else
            {
                mHp -= dmg;
                if (mCallback != null)
                {
                    mCallback.onHurted();
                }
            }
            GameObject HPLabel = ObjectPool.GetInstant().GetObj("HPParticle", new Vector3(transform.position[0], transform.position[1] + 2.5f, transform.position[2]), Quaternion.identity).transform.Find("HPLabel").gameObject;
            HPLabel.GetComponent<TextMesh>().text = "-" + dmg;
            HPLabel.GetComponent<MeshRenderer>().sortingOrder = 10;
            return dmg;
        }
        else
        {
            return 0;
        }
    }

    private LifeCallback mCallback;
    public void registerCallback(LifeCallback callback)
    {
        this.mCallback = callback;
    }
    public void unRegisterCallback()
    {
        this.mCallback = null;
    }
    public interface LifeCallback
    {
        void onHurted();
        void onDead();
    }
}
