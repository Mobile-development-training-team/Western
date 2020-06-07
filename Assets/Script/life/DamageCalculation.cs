using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCalculate
{
    static public Boolean friendlyFire = false;

    static public float calDam(Attack atk,Life def)
    {
        if ((atk.mTeam != def.mTeam) || friendlyFire == true)
        {
            return atk.mAtk - def.mDef;
        }
        return 0f;
    }
}
