using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack
{
    public int mTeam = 1;
    public float mAtk = 20f;

    public float attack(Life life)
    {
        if (mTeam != life.mTeam)
        {
            return life.beAttacked(this);
        }
        else
        {
            return -1;
        }
    }
}
