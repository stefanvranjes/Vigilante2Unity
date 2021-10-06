using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutShop : Destructible
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
        return base.OnCollision(hit);
    }

    //FUN_4DE8 (ROUTE66.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        short sVar1;
        uint uVar2;
        VigObject oVar2;
        Donut ppcVar3;
        int iVar4;
        int iVar5;
        uint uVar6;

        if (arg1 == 9)
        {
            uVar2 = 0;

            if (arg2 == 0)
            {
                oVar2 = child2.FUN_2CD04();
                ppcVar3 = Utilities.FUN_52188(oVar2, typeof(Donut)) as Donut;
                uVar6 = (uint)((long)ppcVar3.DAT_58 * 25734);
                iVar5 = (int)((ulong)((long)ppcVar3.DAT_58 * 25734) >> 32);
                ppcVar3.flags = 0x80;
                sVar1 = id;
                ppcVar3.maxHalfHealth = 300;
                ppcVar3.id = sVar1;
                iVar4 = FUN_50B0(uVar6, iVar5, 0, 0);

                if (iVar4 < 1)
                {
                    uVar6 += 4095;
                    iVar5 += (uVar6 < 4095 ? 1 : 0);
                }

                ppcVar3.physics2.Y = (int)(uVar6 >> 12 | (uint)iVar5 << 20);
                ppcVar3.vr = Utilities.FUN_2A2E0(ppcVar3.vTransform.rotation);
                iVar4 = ppcVar3.vTransform.rotation.V02 * 4577;

                if (iVar4 < 0)
                    iVar4 += 4095;

                ppcVar3.physics1.X = iVar4 >> 12;
                ppcVar3.physics1.Y = -3051;
                iVar4 = ppcVar3.vTransform.rotation.V22 * 4577;

                if (iVar4 < 0)
                    iVar4 += 4095;

                ppcVar3.physics1.Z = iVar4 >> 12;
                ppcVar3.FUN_305FC();
                uVar2 = 0;
            }
        }
        else
            uVar2 = base.UpdateW(arg1, arg2);

        return uVar2;
    }

    private static int FUN_50B0(uint param1, int param2, uint param3, int param4)
    {
        int iVar1;

        iVar1 = 0;

        if (param4 <= param2)
        {
            iVar1 = 2;

            if (param2 <= param4)
            {
                if (param1 < param3)
                    iVar1 = 0;
                else
                {
                    iVar1 = 2;

                    if (param1 <= param3)
                        iVar1 = 1;
                }
            }
        }

        return iVar1;
    }
}
