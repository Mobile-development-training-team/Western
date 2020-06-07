using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack
{
    public int mTeam = 1;
    public float mAtk = 20f;

    public void attack(Life life)
    {
        if (mTeam != life.mTeam)
        {
            life.beAttacked(this);
        }
    }
}
