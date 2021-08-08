using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketL : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_4351C
    public override uint UpdateW(int arg1, int arg2)
    {
        uint uVar9;

        switch (arg1)
        {
            case 0:
                FUN_42330(arg2);
                uVar9 = 0;
                break;
            default:
                uVar9 = 0;
                break;
        }

        return uVar9;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        short sVar4;
        long lVar7;
        int iVar8;
        uint uVar9;
        Vector3Int local_28;

        switch (arg1)
        {
            case 1:
                maxHalfHealth = 10;
                goto default;
            default:
                uVar9 = 0;
                break;
            case 12:
                FUN_4336C((Vehicle)arg2, 174);
                sVar4 = (short)(maxHalfHealth - 1);
                LAB_437B0:
                maxHalfHealth = (ushort)sVar4;
                uVar9 = 30;

                if (sVar4 == 0)
                {
                    FUN_3A368();
                    uVar9 = 30;
                }

                break;
            case 13:
                local_28 = Utilities.FUN_24304(arg2.vTransform, ((Vehicle)arg2).target.vTransform.position);
                uVar9 = 0;

                if (local_28.z < 0x1f4000)
                {
                    lVar7 = Utilities.Ratan2(local_28.x, local_28.z);
                    iVar8 = (int)(lVar7 << 20) >> 20;

                    if (iVar8 < 0)
                        iVar8 = -iVar8;

                    uVar9 = (uint)(iVar8 < 113 ? 1 : 0);
                }

                break;
        }

        return uVar9;
    }

    private VigObject FUN_4336C(Vehicle param1, int param2)
    {
        Rocket ppcVar1;
        int iVar2;
        Smoke1 oVar2;
        ushort uVar4;

        ppcVar1 = LevelManager.instance.FUN_42408(param1, this, (ushort)param2, typeof(Rocket), null) as Rocket;

        if (1 < (uint)param2 - 212)
        {
            oVar2 = LevelManager.instance.FUN_4DE54(ppcVar1.screen, 1);
            oVar2.flags &= 0xffffffef;
            oVar2.vTransform = ppcVar1.vTransform;
        }

        ppcVar1.flags = 0x20000084;
        uVar4 = 80;

        if (param1.doubleDamage != 0)
            uVar4 = 160;

        ppcVar1.maxHalfHealth = uVar4;
        ppcVar1.FUN_305FC();
        iVar2 = param1.physics1.X;

        if (iVar2 < 0)
            iVar2 += 127;

        ppcVar1.physics1.Z = (iVar2 >> 7) + ppcVar1.vTransform.rotation.V02 * 4;
        iVar2 = param1.physics1.Y;

        if (iVar2 < 0)
            iVar2 += 127;

        ppcVar1.physics1.W = (iVar2 >> 7) + ppcVar1.vTransform.rotation.V12 * 4;
        iVar2 = param1.physics1.Z;

        if (iVar2 < 0)
            iVar2 += 127;

        ppcVar1.physics2.X = (iVar2 >> 7) + ppcVar1.vTransform.rotation.V22 * 4;
        ppcVar1.physics2.M2 = 240;
        //sound
        param1.FUN_2B1FC(GameManager.DAT_A30, screen);
        return ppcVar1;
    }
}
