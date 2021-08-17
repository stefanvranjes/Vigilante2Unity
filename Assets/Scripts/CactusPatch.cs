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

    public override uint OnCollision(HitDetection hit)
    {
        int iVar2;
        Mine ppcVar4;
        int iVar5;
        uint uVar6;

        if (hit.self.type == 2)
        {
            if (tags == 0)
                return 0;

            LevelManager.instance.FUN_4DF20(vTransform.position, 13, 2048);
            //FUN_4E414
            iVar2 = 0;
            //sound

            if (maxHalfHealth != 0)
            {
                do
                {
                    ppcVar4 = vData.ini.FUN_2C17C(182, typeof(Mine), 0) as Mine;
                    ppcVar4.type = 8;
                    ppcVar4.id = hit.self.id;
                    ppcVar4.screen = screen;
                    uVar6 = flags;
                    ppcVar4.maxHalfHealth = 150;
                    ppcVar4.physics2.M2 = 0;
                    ppcVar4.flags = uVar6 & 0x60000000 | 0x80;
                    iVar5 = (int)GameManager.FUN_2AC5C();
                    ppcVar4.physics1.Z = (iVar5 * 7629 >> 15) - 3814;
                    ppcVar4.physics1.W = -3051;
                    iVar5 = (int)GameManager.FUN_2AC5C();
                    ppcVar4.physics2.X = (iVar5 * 7629 >> 15) - 3814;
                    ppcVar4.DAT_80 = DAT_80;
                    ppcVar4.FUN_305FC();
                    iVar2++;
                } while (iVar2 < maxHalfHealth);
            }

            GameManager.instance.FUN_309A0(this);
            return 0xffffffff;
        }

        return 0;
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
