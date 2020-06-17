using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LeoLuz.PropertyAttributes;

public class SkillManager : MonoBehaviour
{
    public GameObject magicCircle;
    public GameObject magicCircleBack;

    public bool skill_01 = false;
    private float skill_01_coolTime = 3f;
    private float skill_01_atk = 10f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Use_Skill_01(bool needBack)
    {
        if (skill_01)
        {
            ObjectPool.GetInstant().GetObj("SlashWaveBlue", magicCircle.transform.position, transform.localRotation).GetComponent<SlashWaveBlueController>().atk = skill_01_atk;
            if(needBack)
                ObjectPool.GetInstant().GetObj("SlashWaveBlue", magicCircleBack.transform.position, magicCircleBack.transform.localRotation).GetComponent<SlashWaveBlueController>().atk = skill_01_atk;
        }
    }
}
