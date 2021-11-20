using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftGate : Destructible
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

    //FUN_1D70 (OLYMPIC.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        OLYMPIC oVar1;

        if (arg1 == 1)
        {
            oVar1 = (OLYMPIC)LevelManager.instance.level;
            OLYMPIC.FUN_CCC(oVar1.DAT_8C, oVar1.pole2M, this);
        }

        return base.UpdateW(arg1, arg2);
    }
}
