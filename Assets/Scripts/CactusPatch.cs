using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusPatch : Mine
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_476D0
    public override uint UpdateW(int arg1, int arg2)
    {
        bool bVar1;
        int iVar2;

        if (arg1 == 2)
        {
            id = 0;

            if (DAT_80.maxHalfHealth == 0)
            {
                flags &= 0xdfffffff;
                return 0;
            }
        }
        else
        {
            if (arg1 != 0)
                return 0;

            if (tags != 0)
            {
                iVar2 = GameManager.DAT_65C90[(physics2.M2 & 0xfff) * 2] * 0x1400;

                if (iVar2 < 0)
                    iVar2 += 4095;

                vTransform.position.y = GameManager.instance.DAT_DB0 + (iVar2 >> 12);
                physics2.M2 += 34;
                return 0;
            }

            iVar2 = FUN_46D70();

            if (iVar2 == 0)
                return 0;

            tags = 1;
            bVar1 = GameManager.instance.DAT_DA0 <= vTransform.position.z;
            physics2.M2 = 0;

            if (bVar1 || vTransform.position.y < GameManager.instance.DAT_DB0)
                FUN_30BA8();

            flags |= 0x100;
        }

        GameManager.instance.FUN_30CB0(this, 120);
        return 0;
    }
}
