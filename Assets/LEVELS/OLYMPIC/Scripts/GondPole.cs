using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GondPole : Destructible
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

    //FUN_1ADC (OLYMPIC.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        OLYMPIC oVar1;

        if (arg1 == 1)
        {
            oVar1 = (OLYMPIC)LevelManager.instance.level;
            OLYMPIC.FUN_CCC(oVar1.DAT_80_2, oVar1.pole1M, this);
        }

        return base.UpdateW(arg1, arg2);
    }
}
