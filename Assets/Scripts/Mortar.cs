using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortar : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_466C0
    public override uint UpdateW(int arg1, int arg2)
    {
        ushort uVar1;
        uint uVar3;
        Vehicle vVar2;
        Shell sVar4;
        uint uVar5;

        switch (arg1)
        {
            case 0:
                FUN_42330(arg2);
                uVar5 = 0;
                break;
            default:
                uVar5 = 0;
                break;
            case 10:
                arg2 &= 0xfff;

                if (arg2 == 0x222)
                {
                    if (maxHalfHealth < 2)
                        return 0xffffffff;

                    vVar2 = Utilities.FUN_2CD78(this) as Vehicle;
                    sVar4 = FUN_46480(vVar2, 203, 15, 37);
                    sVar4.tags = 1;
                    uVar5 = sVar4.flags | 0x40000000;
                    sVar4.flags = uVar5;
                    uVar1 = (ushort)(maxHalfHealth - 2);
                    goto LAB_468B4;
                }

                if (arg2 != 0x224)
                {
                    if (arg2 != 0x223)
                        return 0;

                    if (maxHalfHealth < 2)
                        return 0xffffffff;

                    vVar2 = Utilities.FUN_2CD78(this) as Vehicle;
                    sVar4 = FUN_46480(vVar2, 217, 24, 75);
                    sVar4.tags = 3;
                    uVar5 = sVar4.flags | 0x40000020;
                    sVar4.flags = uVar5;
                    uVar1 = (ushort)(maxHalfHealth - 2);
                    goto LAB_468B4;
                }

                if (maxHalfHealth < 2)
                    return 0xffffffff;

                vVar2 = Utilities.FUN_2CD78(this) as Vehicle;
                uVar5 = 5;

                if (maxHalfHealth < 5)
                    uVar5 = maxHalfHealth;

                sVar4 = FUN_46480(vVar2, 204, 19, (ushort)(uVar5 * 75));
                sVar4.tags = 2;
                sVar4.flags |= 0x40000020;
                uVar5 = (uint)maxHalfHealth - 5;
                uVar3 = 0;

                if (0 < (int)uVar5)
                    uVar3 = uVar5;

                maxHalfHealth = (ushort)uVar3;
                uVar3 &= 0xffff;
                goto LAB_468BC;
                LAB_468B4:
                maxHalfHealth = uVar1;
                uVar3 = uVar1;
                LAB_468BC:
                uVar5 = 120;

                if (uVar3 == 0)
                {
                    FUN_3A368();
                    uVar5 = 120;
                }

                break;
        }

        return uVar5;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        ushort uVar1;
        uint uVar2;
        uint uVar3;
        int iVar4;
        uint uVar5;

        switch (arg1)
        {
            case 0:
                FUN_42330(arg2);
                uVar5 = 0;
                break;
            case 1:
                maxHalfHealth = 10;
                flags |= 0x4000;
                goto default;
            default:
                uVar5 = 0;
                break;
            case 12:
                if (((Vehicle)arg2).doubleDamage == 0)
                    uVar2 = 75;
                else
                    uVar2 = 150;

                FUN_46480((Vehicle)arg2, 183, 6, (ushort)uVar2);
                uVar1 = (ushort)(maxHalfHealth - 1);
                LAB_468B4:
                maxHalfHealth = uVar1;
                uVar3 = uVar1;
                LAB_468BC:
                uVar5 = 120;

                if (uVar3 == 0)
                {
                    FUN_3A368();
                    uVar5 = 120;
                }

                break;
            case 13:
                iVar4 = Utilities.FUN_29F6C(arg2.screen, ((Vehicle)arg2).target.screen);
                uVar5 = (uint)(0x1f3ffe < (uint)(iVar4 - 0x1f4001 ^ 1) ? 1 : 0);
                break;
        }

        return uVar5;
    }

    private Shell FUN_46480(Vehicle param1, ushort param2, short param3, ushort param4)
    {
        sbyte sVar1;
        Ballistic ppcVar2;
        Shell ppcVar3;
        int iVar4;
        VigObject pcVar5;
        VigObject oVar6;
        int iVar7;
        int iVar8;
        Vector3Int auStack48;

        oVar6 = child2;
        ppcVar2 = LevelManager.instance.xobfList[19].ini.FUN_2C17C(8, typeof(Ballistic), 8) as Ballistic;
        ppcVar3 = LevelManager.instance.FUN_42408(param1, oVar6, param2, typeof(Shell), ppcVar2) as Shell;
        ppcVar3.DAT_1A = param3;
        ppcVar3.flags = 0x20000490;
        ppcVar3.maxHalfHealth = param4;
        ppcVar3.FUN_305FC();
        ppcVar3.physics2.M2 = 0;
        pcVar5 = param1.target;

        if (param1.target == null)
            pcVar5 = param1;

        ppcVar3.DAT_84 = pcVar5;
        iVar4 = Utilities.FUN_29F6C(ppcVar3.vTransform.position, pcVar5.vTransform.position);
        iVar4 >>= 9;
        iVar8 = 0x1000;

        if (4095 < iVar4)
        {
            iVar8 = 0x2000;

            if (iVar4 < 0x2001)
                iVar8 = iVar4;
        }

        iVar4 = param1.physics1.X;

        if (iVar4 < 0)
            iVar4 += 127;

        iVar7 = ppcVar3.vTransform.rotation.V01 * iVar8;

        if (iVar7 < 0)
            iVar7 += 4095;

        ppcVar3.physics1.Z = (iVar4 >> 7) - (iVar7 >> 12);
        iVar4 = param1.physics1.Y;

        if (iVar4 < 0)
            iVar4 += 127;

        iVar7 = ppcVar3.vTransform.rotation.V11 * iVar8;

        if (iVar7 < 0)
            iVar7 += 4095;

        ppcVar3.physics1.W = (iVar4 >> 7) - (iVar7 >> 12);
        iVar4 = param1.physics1.Z;

        if (iVar4 < 0)
            iVar4 += 127;

        iVar8 = ppcVar3.vTransform.rotation.V21 * iVar8;

        if (iVar8 < 0)
            iVar8 += 4095;

        ppcVar3.physics2.X = (iVar4 >> 7) - (iVar8 >> 12);
        ppcVar3.vTransform = GameManager.FUN_2A39C();
        ppcVar2.flags |= 0x410;
        ppcVar2.FUN_30BF0();
        auStack48 = Utilities.FUN_24094(oVar6.vTransform.rotation, GameManager.DAT_A4C);
        param1.FUN_2B1FC(auStack48, screen);
        sVar1 = (sbyte)GameManager.instance.FUN_1DD9C();
        ppcVar3.DAT_18 = sVar1;
        GameManager.instance.FUN_1E580(sVar1, GameManager.instance.DAT_C2C, 57, ppcVar3.screen);
        return ppcVar3;
    }
}
