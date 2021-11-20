using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellGate3 : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_8A0 (HALFTRAK.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        uint uVar1;
        VigObject oVar1;

        if (arg1 == 2)
        {
            oVar1 = FUN_2CCBC();
            GameManager.instance.FUN_307CC(oVar1);
            uVar1 = 0xffffffff;
        }
        else
            uVar1 = 0;

        return uVar1;
    }
}
