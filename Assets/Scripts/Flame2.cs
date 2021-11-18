using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame2 : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_480F8
    public override uint UpdateW(int arg1, int arg2)
    {
        bool bVar1;
        int iVar2;

        if (arg1 == 0)
        {
            vTransform.position.x += physics1.Z;
            vTransform.position.y += physics1.W;
            vTransform.position.z += physics2.X;
            iVar2 = physics1.Z * 31;

            if (iVar2 < 0)
                iVar2 += 31;

            physics1.Z = iVar2 >> 5;
            iVar2 = physics2.X * 31;

            if (iVar2 < 0)
                iVar2 += 31;

            physics2.X = iVar2 >> 5;
            bVar1 = GameManager.instance.DAT_DA0 <= vTransform.position.z;
            physics1.W -= 16;

            if ((bVar1 || vTransform.position.y <= GameManager.instance.DAT_DB0) &&
                vTransform.position.y <= GameManager.instance.terrain.FUN_1B750((uint)vTransform.position.x, (uint)vTransform.position.z))
                return 0;
        }
        else
        {
            if (arg1 != 5)
                return 0;
        }

        FUN_4EE8C(GameManager.instance.interObjs);
        return 0xffffffff;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        if (arg1 != 5)
            return 0;

        FUN_4EE8C(GameManager.instance.interObjs);
        return 0xffffffff;
    }
}
