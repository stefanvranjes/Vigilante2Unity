using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkLift : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public static VigObject OnInitialize(XOBF_DB arg1, int arg2, uint arg3)
    {
        Dictionary<int, Type> dict = new Dictionary<int, Type>();
        dict.Add(921, typeof(ForkLift2));
        dict.Add(919, typeof(ForkLift2));
        dict.Add(913, typeof(ForkLift2));
        return arg1.ini.FUN_2C17C((ushort)arg2, typeof(ForkLift), arg3, dict);
    }

    public override uint OnCollision(HitDetection hit)
    {
        short sVar1;
        int iVar2;
        uint uVar3;
        ushort uVar4;
        int iVar5;
        VigObject oVar5;
        VigObject oVar9;
        int iVar10;

        oVar9 = hit.self;

        if (oVar9.type == 2)
        {
            iVar5 = (int)FUN_33798(hit, physics1.W);

            if (iVar5 != 0)
            {
                iVar2 = GameManager.instance.FUN_1DD9C();
                GameManager.instance.FUN_1E580(iVar2, GameManager.instance.DAT_C2C, 7, oVar9.vTransform.position);
                return 0;
            }

            return 0;
        }

        if (oVar9.type != 8)
            return 0;

        iVar10 = oVar9.maxHalfHealth;
        uVar4 = maxHalfHealth;
        oVar9 = null;

        if (uVar4 < iVar10)
        {
            oVar5 = child2;
            maxHalfHealth = maxFullHealth;

            while (oVar5 != null)
            {
                if (3 < oVar5.id && (oVar9 == null || oVar5.id < oVar9.id))
                    oVar9 = oVar5;

                oVar5 = oVar5.child;
            }

            if (oVar9 != null)
            {
                uVar3 = GameManager.FUN_2AC5C();
                oVar9.FUN_306FC();
                oVar9.FUN_2CCBC();
                sVar1 = id;
                oVar9.type = 10;
                oVar9.id = sVar1;
                oVar9.flags |= 0x80;
                oVar9 = Utilities.FUN_52188(oVar9, typeof(ForkLift2));
                ((ForkLift2)oVar9).state = _FORKLIFT2_TYPE.Type1;

                if ((uVar3 & 1) == 0)
                    iVar2 = -0x5F580;
                else
                    iVar2 = 0x5F580;

                oVar9.physics1.X = iVar2;
                oVar9.physics1.Y = -0x2FA80;
                oVar9.physics1.Z = 0;
                oVar9.physics2.X = 0;
                oVar9.physics2.Y = 0;

                if ((uVar3 & 1) == 0)
                    iVar2 = -0x80000;
                else
                    iVar2 = 0x80000;

                oVar9.physics2.Z = iVar2;
                oVar9.DAT_A0 = new Vector3Int(0x10, 0x20, 0x40);
                oVar9.vTransform = Utilities.CompMatrixLV(vTransform, oVar9.vTransform);
                Vector3Int v3 = Utilities.FUN_24094(vTransform.rotation, new Vector3Int(oVar9.physics1.X, oVar9.physics1.Y, oVar9.physics1.Z));
                oVar9.physics1.X = v3.x;
                oVar9.physics1.Y = v3.y;
                oVar9.physics1.Z = v3.z;
                oVar9.FUN_305FC();
                GameManager.instance.FUN_30CB0(oVar9, 1800);
                return 0;
            }

            FUN_4DC94();
            return 0;
        }

        maxHalfHealth = (ushort)(uVar4 - iVar10);
        return 0;
    }

    public RSEG_DB DAT_80_2; //0x80

    //FUN_2814 (NUCLEAR.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        long lVar2;
        RSEG_DB rVar2;
        int iVar3;
        uint uVar3;
        ushort uVar4;
        int iVar5;
        VigObject oVar5;
        RSEG_DB piVar6;
        VigObject ppcVar7;
        int iVar9;
        VigObject oVar9;
        JUNC_DB jVar9;

        switch (arg1)
        {
            case 0:
                iVar9 = physics1.Z;

                if (iVar9 < 0)
                    iVar9 += 0xffff;

                lVar2 = DAT_80_2.FUN_5105C(iVar9 >> 16, physics1.Y != 0, ref vTransform);
                iVar9 = (int)Utilities.SquareRoot(lVar2);

                if (physics1.Y == 0)
                    iVar5 = physics1.W * -0x10000;
                else
                    iVar5 = physics1.W << 16;

                iVar3 = physics1.Z + iVar5 / iVar9;
                physics1.Z = iVar3;

                if ((uint)iVar3 < 0x10000001)
                    return 0;

                jVar9 = DAT_80_2.DAT_00[physics1.Y];

                if (jVar9.DAT_11 != 1)
                {
                    piVar6 = jVar9.DAT_1C[jVar9.DAT_1C[0] == DAT_80_2 ? 1 : 0];
                    DAT_80_2 = piVar6;
                    iVar3 = piVar6.DAT_00[0] == jVar9 ? 1 : 0;
                    physics1.Y = iVar3;
                    physics1.Z = -(iVar3 == 0 ? 1 : 0) & 0x10000000;
                    return 0;
                }

                goto LAB_2BD8;
            case 2:
                uVar3 = GameManager.FUN_2AC5C();
                oVar9 = GameManager.instance.FUN_318D0((int)(uVar3 & 1) + 49);
                screen = oVar9.screen;
                flags &= 0xffffffdd;
                Dictionary<int, Type> dict = new Dictionary<int, Type>();
                dict.Add(921, typeof(ForkLift2));
                dict.Add(919, typeof(ForkLift2));
                dict.Add(913, typeof(ForkLift2));
                FUN_2C124(25, dict);
                Utilities.ParentChildren(this, this);
                FUN_30B78();
                goto case 1;
            case 1:
                rVar2 = LevelManager.instance.FUN_518DC(screen, 16);
                DAT_80_2 = rVar2;
                iVar9 = rVar2.FUN_51334(screen);
                ppcVar7 = child2;
                physics1.Z = iVar9 << 16;
                physics1.Y = 1;
                physics1.W = 4577;
                maxHalfHealth = 50;
                maxFullHealth = 50;
                flags |= 0x188;

                while (ppcVar7 != null)
                {
                    if (3 < ppcVar7.id)
                        ((ForkLift2)ppcVar7).state = _FORKLIFT2_TYPE.Type1;

                    ppcVar7 = ppcVar7.child;
                }

                break;
            default:
                break;
            case 8:
                uVar4 = maxHalfHealth;
                oVar9 = null;

                if (uVar4 < arg2)
                {
                    oVar5 = child2;
                    maxHalfHealth = maxFullHealth;

                    while (oVar5 != null)
                    {
                        if (3 < oVar5.id && (oVar9 == null || oVar5.id < oVar9.id))
                            oVar9 = oVar5;

                        oVar5 = oVar5.child;
                    }

                    if (oVar9 != null)
                    {
                        oVar9.FUN_2CD04();
                        oVar9.FUN_305FC();
                        oVar9.FUN_4DC94();
                        return 0;
                    }

                    FUN_4DC94();
                    return 0;
                }

                maxHalfHealth = (ushort)(uVar4 - arg2);
                break;
            case 9:
                if (arg2 == 0)
                    return 0;

                LAB_2BD8:
                flags |= 0x22;
                FUN_30BA8();
                GameManager.instance.FUN_30CB0(this, 300);
                return 0;
        }

        return 0;
    }
}
