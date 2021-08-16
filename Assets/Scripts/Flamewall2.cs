using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamewall2 : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_485C4
    public override uint UpdateW(int arg1, int arg2)
    {
        uint uVar1;

        if (arg1 < 4)
        {
            uVar1 = 0;

            if (arg1 == 2)
                id = 0;
        }
        else
            uVar1 = 0;

        return uVar1;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        uint uVar1;

        uVar1 = 0;

        if (arg1 == 5)
        {
            GameManager.instance.FUN_309A0(this);
            uVar1 = 0xffffffff;
        }

        return uVar1;
    }
}
