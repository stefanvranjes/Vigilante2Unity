using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invasion2 : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_5A0 (CARAVLLE.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        short sVar1;
        uint uVar2;

        if (arg1 == 0)
        {
            sVar1 = physics2.M3;

            if (physics2.M3 == 0)
            {
                vTransform.position.x += physics1.Z;
                vTransform.position.y += physics1.W;
                vTransform.position.z += physics2.X;
                uVar2 = 0;
            }
            else
            {
                physics2.M3 = (short)(sVar1 - 1);
                uVar2 = 0;

                if (sVar1 == 1)
                {
                    flags &= 0xfffffffd;
                    FUN_30BF0();
                    uVar2 = 0;
                }
            }
        }
        else
            uVar2 = 0;

        return uVar2;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        if (arg1 == 5)
        {
            GameManager.instance.FUN_309A0(this);
            return 0xffffffff;
        }

        return 0;
    }
}
