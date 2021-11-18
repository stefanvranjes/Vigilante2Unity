using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballistic2 : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_4CAC4
    public override uint UpdateW(int arg1, VigObject arg2)
    {
        uint uVar1;

        if (arg1 == 5)
        {
            FUN_30C20();
            vAnim = null;
            uVar1 = 0xffffffff;
        }
        else
            uVar1 = 0;

        return uVar1;
    }
}
