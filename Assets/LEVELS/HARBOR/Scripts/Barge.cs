using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barge : VigObject
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
        VigObject oVar9;

        oVar9 = hit.self;

        if (oVar9.type != 2)
        {
            if (oVar9.type != 8)
                return 0;

            FUN_32B90(oVar9.maxHalfHealth);
            return 0;
        }

        if (DAT_8C != 0)
            FUN_33798(hit, DAT_84_2 * 2);

        oVar9.PDAT_74 = this;
        return 0;
    }

    public RSEG_DB DAT_80_2; //0x80
    public int DAT_84_2; //0x84
    public int DAT_88; //0x88
    public int DAT_8C; //0x8C

    //FUN_1FA0 (HARBOR.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        sbyte sVar1;
        sbyte sVar2;
        int iVar3;
        JUNC_DB jVar3;
        int iVar4;
        RSEG_DB piVar5;
        VigObject oVar6;
        DrawBridge ppcVar7;
        uint uVar8;
        long lVar8;
        RSEG_DB rVar8;
        ConfigContainer ccVar8;
        CraneLarge ppcVar8;
        int iVar9;
        JUNC_DB jVar9;
        Vector3Int local_38;
        Vector3Int local_28;

        switch (arg1)
        {
            case 0:
                sVar2 = tags;

                if (sVar2 == 2)
                {
                    iVar3 = DAT_84_2 + 19;
                    iVar9 = 4577;

                    if (iVar3 < 4577)
                        iVar9 = iVar3;

                    DAT_84_2 = iVar9;
                }
                else
                {
                    if (sVar2 < 3)
                    {
                        if (sVar2 == 1)
                        {
                            if (DAT_88 == 0)
                                iVar9 = DAT_8C;
                            else
                                iVar9 = 0x10000000 - DAT_8C;

                            if (iVar9 < 0)
                                iVar9 += 0xffff;

                            iVar9 = (iVar9 >> 16) * 4577;

                            if (iVar9 < 0)
                                iVar9 += 0xffff;

                            iVar3 = 305;

                            if (305 < iVar9 >> 12)
                                iVar3 = iVar9 >> 12;

                            DAT_84_2= iVar3;
                        }
                    }
                    else
                    {
                        if (sVar2 == 4)
                        {
                            if (DAT_88 == 0)
                                iVar9 = DAT_8C;
                            else
                                iVar9 = 0x10000000 - DAT_8C;

                            if (iVar9 < 0)
                                iVar9 += 0xffff;

                            iVar9 = (iVar9 >> 16) * 4577;

                            if (iVar9 < 0)
                                iVar9 += 0xffff;

                            iVar3 = 305;

                            if (305 < iVar9 >> 12)
                                iVar3 = iVar9 >> 12;

                            DAT_84_2 = iVar3;
                        }
                        else if (sVar2 == 5)
                        {
                            iVar3 = DAT_84_2 + 19;
                            iVar9 = 4577;

                            if (iVar3 < 4577)
                                iVar9 = iVar3;

                            DAT_84_2 = iVar9;
                        }
                    }
                }

                iVar9 = DAT_8C;

                if (iVar9 < 0)
                    iVar9 += 0xffff;

                DAT_80_2.FUN_285E4(iVar9 >> 16, ref vTransform.position, out local_38);
                vTransform.position.y = GameManager.instance.DAT_DB0;

                if (DAT_88 == 0)
                {
                    local_38.x = -local_38.x;
                    local_38.z = -local_38.z;
                }

                lVar8 = Utilities.VectorNormal2(local_38, out local_28);
                vTransform.rotation.V22 = (short)local_28.z;
                vTransform.rotation.V00 = (short)local_28.z;
                vTransform.rotation.V02 = (short)local_28.x;
                vTransform.rotation.V20 = (short)-local_28.x;
                iVar9 = (int)Utilities.SquareRoot(lVar8);

                if (DAT_88 == 0)
                    iVar3 = DAT_84_2 * -0x10000;
                else
                    iVar3 = DAT_84_2 << 16;

                iVar4 = DAT_8C + iVar3 / iVar9;
                DAT_8C = iVar4;

                if ((uint)iVar4 < 0x10000001) goto LAB_2244;

                jVar9 = DAT_80_2.DAT_00[DAT_88];
                piVar5 = jVar9.DAT_1C[0];

                if (piVar5 == DAT_80_2)
                    piVar5 = jVar9.DAT_1C[1];

                DAT_80_2 = piVar5;
                jVar3 = piVar5.DAT_00[0];
                DAT_88 = jVar3 == jVar9 ? 1 : 0;
                DAT_8C = -((jVar3 == jVar9 ? 1 : 0) == 0 ? 1 : 0) & 0x10000000;

                if ((jVar9.DAT_10 & 0x40) == 0) goto LAB_2244;

                sVar1 = (sbyte)jVar9.DAT_12;
                tags = sVar1;

                switch (sVar1)
                {
                    case 2:
                        uVar8 = 120;
                        goto LAB_2284;
                    case 3:
                        ppcVar7 = (DrawBridge)GameManager.instance.FUN_318D0(50);

                        if (ppcVar7.GetType().IsSubclassOf(typeof(VigObject)))
                            ppcVar7.UpdateW(10, 0);

                        break;
                    case 5:
                        uVar8 = 121;
                        LAB_2284:
                        ppcVar8 = (CraneLarge)GameManager.instance.FUN_318D0((int)uVar8);

                        if (ppcVar8.GetType().IsSubclassOf(typeof(VigObject)))
                            ppcVar8.UpdateW(20, this);
                    
                        FUN_30BA8();
                        break;
                }

                LAB_2244:
                uVar8 = 0;

                if (arg2 != 0)
                {
                    uVar8 = GameManager.instance.FUN_1E7A8(vTransform.position);
                    GameManager.instance.FUN_1E2C8(DAT_18, uVar8);
                    uVar8 = 0;
                }

                break;
            case 1:
                rVar8 = LevelManager.instance.FUN_518DC(screen, 16);
                DAT_80_2 = rVar8;
                DAT_88 = 1;
                iVar9 = rVar8.FUN_51334(screen);
                DAT_8C = iVar9 << 16;
                DAT_84_2 = 6103;
                flags |= 0x180;
                ccVar8 = FUN_2C5F4(0x8000);
                oVar6 = FUN_1F64();
                Utilities.FUN_2CA94(this, ccVar8, oVar6);
                ccVar8 = FUN_2C5F4(0x8002);
                oVar6 = FUN_1F64();
                Utilities.FUN_2CA94(this, ccVar8, oVar6);
                Utilities.ParentChildren(this, this);
                sVar2 = (sbyte)GameManager.instance.FUN_1DD9C();
                DAT_18 = sVar2;
                GameManager.instance.FUN_1E098(sVar2, vData.sndList, 3, 0, true);
                uVar8 = 0;
                break;
            case 2:
                uVar8 = 121;

                if (tags == 2)
                {
                    ppcVar7 = (DrawBridge)GameManager.instance.FUN_318D0(49);

                    if (ppcVar7.GetType().IsSubclassOf(typeof(VigObject)))
                        ppcVar7.UpdateW(10, 0);

                    uVar8 = 120;
                }

                ppcVar8 = (CraneLarge)GameManager.instance.FUN_318D0((int)uVar8);

                if (ppcVar8.GetType().IsSubclassOf(typeof(VigObject)))
                    ppcVar8.UpdateW(20, null);

                FUN_30B78();
                uVar8 = 0;
                break;
            default:
                uVar8 = 0;
                break;
            case 4:
                GameManager.instance.FUN_1DE78(DAT_18);
                ppcVar8 = (CraneLarge)GameManager.instance.FUN_318D0(120);

                if (ppcVar8.GetType().IsSubclassOf(typeof(VigObject)))
                    ppcVar8.UpdateW(20, null);

                ppcVar8 = (CraneLarge)GameManager.instance.FUN_318D0(121);

                if (ppcVar8.GetType().IsSubclassOf(typeof(VigObject)))
                    ppcVar8.UpdateW(20, null);

                goto default;
            case 8:
                FUN_32B90((uint)arg2);
                uVar8 = 0;
                break;
            case 9:
                uVar8 = 0;

                if (arg2 != 0)
                {
                    GameManager.instance.FUN_309A0(this);
                    uVar8 = 0xffffffff;
                }

                break;
        }

        return uVar8;
    }

    public static VigObject FUN_1F64()
    {
        VigObject oVar1;

        oVar1 = LevelManager.instance.xobfList[42].ini.FUN_2C17C(2, typeof(VigObject), 0);
        oVar1.vLOD = oVar1.vMesh;
        return oVar1;
    }
}
