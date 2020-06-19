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
            if (def.mDef >= atk.mAtk)
            {
                return 0;
            }

            if (def.mShield > atk.mAtk)
            {
                def.mShield -= atk.mAtk;
                return 0;
            }
            else
            {
                int dmg = (int)(atk.mAtk - def.mShield - def.mDef + 0.5);
                def.mShield = 0;
                return (float)dmg;
            }

        }
        return 0f;
    }
}
