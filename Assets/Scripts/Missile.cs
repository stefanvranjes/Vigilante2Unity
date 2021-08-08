using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_441D4
    public override uint UpdateW(int arg1, VigObject arg2)
    {
        uint uVar1;
        uint uVar2;

        if (arg1 < 4)
        {
            if (arg1 != 0)
                return 0;

            physics1.W += 56;
            screen.x += physics1.Z;
            screen.y += physics1.W;
            screen.z += physics2.X;
            vTransform.position = screen;

            if ((flags & 0x1000000) != 0)
            {
                physics1.Z = physics1.Z * 3968 >> 12;
                physics1.W = physics1.W * 3968 >> 12;
                physics2.X = physics2.X * 3968 >> 12;
            }
        }
        else
        {
            if (arg1 == 4)
                DAT_84.flags &= 0xf7ffffff;
            else
            {
                if (arg1 != 5)
                    return 0;

                GameManager.instance.FUN_1FEB8(vMesh);
                GameManager.instance.FUN_2C4B4(child2);
                uVar2 = 186;

                if ((flags & 0x1000000) != 0)
                    uVar2 = 198;

                FUN_2C344(vData, (ushort)uVar2, 8);
                uVar1 = flags;
                flags |= 0x84;

                //...
                physics2.M2 = 480;
            }
        }

        uVar2 = 0;
        return uVar2;
    }
}
