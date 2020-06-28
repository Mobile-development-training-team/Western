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
                float dmg1 = atk.mAtk - def.mShield;
                def.mShield = 0;
                if (def.mDef >= dmg1)
                {
                    return 0;
                }
                else
                {
                    if (def.mBlock > dmg1)
                    {
                        def.mBlock -= dmg1;
                        return 0;
                    }
                    else
                    {
                        int dmg2 = (int)(dmg1 - def.mBlock - def.mDef + 0.5f);
                        def.mBlock = 0;
                        return (float)dmg2;
                    }
                }
                /*
                if (dmg1 > def.mBlock)
                {
                    int dmg2 = (int)(dmg1 - def.mBlock - def.mDef + 0.5);
                    def.mBlock = 0;
                    return (float)dmg2;
                }
                else
                {
                    def.mBlock -= dmg1;
                    return 0;
                }
                */
                //int dmg = (int)(atk.mAtk - def.mShield - def.mDef + 0.5);
                //def.mShield = 0;
                //return (float)dmg;
            }

        }
        return 0f;
    }
}
