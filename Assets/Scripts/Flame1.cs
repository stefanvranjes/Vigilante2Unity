using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame1 : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_47EE4
    public override uint UpdateW(int arg1, int arg2)
    {
        bool bVar1;
        int iVar4;

        if (arg1 != 3)
        {
            if (arg1 < 4)
            {
                if (arg1 != 0)
                    return 0;

                vTransform.position.x += physics1.Z;
                vTransform.position.y += physics1.W;
                vTransform.position.z += physics2.X;
                iVar4 = physics1.Z * 31;

                if (iVar4 < 0)
                    iVar4 += 31;

                physics1.Z = iVar4 >> 5;
                iVar4 = physics2.X * 31;

                if (iVar4 < 0)
                    iVar4 += 31;

                physics2.X = iVar4 >> 5;
                bVar1 = GameManager.instance.DAT_DA0 <= vTransform.position.z;
                physics1.W -= 16;

                if ((bVar1 || vTransform.position.y <= GameManager.instance.DAT_DB0) &&
                    vTransform.position.y <= GameManager.instance.terrain.FUN_1B750((uint)vTransform.position.x, (uint)vTransform.position.z))
                    return 0;

                if (tags != 0)
                {
                    LevelManager.instance.FUN_4DE54(vTransform.position, 138);
                    //sound
                    LevelManager.instance.FUN_309C8(this, 1);
                    return 0xffffffff;
                }
            }
            else
            {
                if (arg1 != 5)
                    return 0;
            }
        }

        GameManager.instance.FUN_309A0(this);
        return 0xffffffff;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        if (arg1 != 5)
            return 0;

        GameManager.instance.FUN_309A0(this);
        return 0xffffffff;
    }
}
