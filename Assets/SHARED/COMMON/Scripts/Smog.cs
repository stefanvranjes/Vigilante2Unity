using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smog : VigObject
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
        int iVar1;
        int iVar2;

        if ((flags & 0x1000000) == 0 && hit.self.type == 2)
        {
            flags |= 0x1000020;
            iVar1 = GameManager.instance.FUN_1DD9C();
            GameManager.instance.FUN_1E628(iVar1, GameManager.instance.DAT_C2C, 33, screen);
            iVar2 = (int)GameManager.FUN_2AC5C();

            if (iVar2 * 5 >> 15 == 0)
                ((Vehicle)hit.self).FUN_39BC4();
        }

        return 0;
    }

    //FUN_60 (BUS.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        if (arg1 == 0)
        {
            screen.x += physics1.Z;
            screen.y += physics1.W;
            screen.z += physics2.X;
            physics1.Z = physics1.Z * 3968 >> 12;
            physics1.W = physics1.W * 3968 >> 12;
            physics2.X = physics2.X * 3968 >> 12;
            vr.z += physics2.M3;

            if (arg2 == 0)
                return 0;
            else
                ApplyTransformation();
        }

        return 0;
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
