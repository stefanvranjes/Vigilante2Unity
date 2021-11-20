using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContBuilding : Destructible
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override uint OnCollision(HitDetection hit)
    {
        return base.OnCollision(hit);
    }

    //FUN_312C (NUCLEAR.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        VigObject oVar1;

        if (arg1 == 1)
        {
            oVar1 = child2;

            while (oVar1 != null)
            {
                oVar1.maxHalfHealth = 75;
                oVar1 = oVar1.child;
            }
        }

        return base.UpdateW(arg1, arg2);
    }
}
