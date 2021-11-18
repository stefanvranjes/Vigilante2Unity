using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : VigObject
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
        FUN_32CF0(hit);
        return 0;
    }

    //FUN_32D34
    public override uint UpdateW(int arg1, int arg2)
    {
        if (arg1 == 8)
            FUN_32B90((uint)arg2);

        return 0;
    }

    public bool FUN_32CF0(HitDetection param1)
    {
        bool bVar1;
        bool bVar2;

        bVar2 = false;

        if (param1.self.type == 8)
        {
            bVar1 = param1.object1.FUN_32B90(param1.self.maxHalfHealth);
            bVar2 = bVar1 != false;
        }

        return bVar2;
    }
}
