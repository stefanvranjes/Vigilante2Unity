using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_430F8
    public override uint UpdateW(int arg1, VigObject arg2)
    {
        short sVar1;
        int iVar2;
        Matrix3x3 m;

        if (arg1 == 2)
        {
            //...
        }
        else
        {
            if (2 < arg1)
            {
                if (arg1 != 3)
                    return 0;
            }

            if (arg1 != 0)
                return 0;

            m = vTransform.rotation;
            screen.x += physics1.Z;
            screen.y += physics1.W;
            screen.z += physics2.X;
            vTransform.position = screen;
            FUN_24700(0, 0, 512);

            if ((physics2.M2 & 31) == 0)
                vTransform.rotation = Utilities.MatrixNormal(m);

            sVar1 = physics2.M2;
            physics2.M2--;

            if (sVar1 == 1)
            {
                GameManager.instance.FUN_309A0(this);
                return 0xffffffff;
            }

            iVar2 = GameManager.instance.terrain.FUN_1B750((uint)screen.x, (uint)screen.z);

            if (iVar2 <= screen.y)
            {
                //sound
                LevelManager.instance.FUN_4DE54(screen, 33);
                LevelManager.instance.FUN_309C8(this, 1);
                return 0xffffffff;
            }
        }

        return 0;
    }
}
