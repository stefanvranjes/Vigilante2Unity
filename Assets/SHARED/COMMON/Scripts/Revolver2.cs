using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver2 : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_474 (TBOLT.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        short sVar1;

        if (arg1 == 0)
        {
            vr.z += maxHalfHealth * 2;

            if (arg2 != 0)
                ApplyTransformation();

            sVar1 = (short)(maxHalfHealth - 1);
            maxHalfHealth = (ushort)sVar1;

            if (sVar1 == 0)
                FUN_30BA8();
        }

        return 0;
    }
}
