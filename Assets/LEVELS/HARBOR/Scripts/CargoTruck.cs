using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoTruck : VigObject
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
        int iVar3;
        int iVar6;
        int iVar9;
        VigObject oVar9;

        oVar9 = hit.self;

        if (oVar9.type != 2)
        {
            if (oVar9.type == 8)
            {
                FUN_32B90(oVar9.maxHalfHealth);
                return 0;
            }

            return 0;
        }

        iVar3 = (int)FUN_33798(hit, DAT_8C);

        if (iVar3 == 0)
            return 0;

        iVar6 = GameManager.instance.FUN_1DD9C();
        GameManager.instance.FUN_1E580(iVar6, GameManager.instance.DAT_C2C, 7, oVar9.vTransform.position);

        if (tags != 12)
            return 0;

        iVar3 = DAT_8C - 1525;
        iVar9 = 0;

        if (0 < iVar3)
            iVar9 = iVar3;

        DAT_8C = iVar9;

        if (iVar9 != 0)
            return 0;

        //FUN_326B0
        FUN_30BA8();
        return 0;
    }

    public RSEG_DB DAT_88; //0x88
    public int DAT_8C; //0x8C
    public int DAT_90; //0x90
    public int DAT_94; //0x94

    //FUN_24CC (HARBOR.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        sbyte sVar1;
        VigObject oVar2;
        int iVar3;
        RSEG_DB unaff_s3;
        CraneLarge ppcVar4;
        int iVar6;
        RSEG_DB rVar6;
        long lVar6;
        VigObject oVar6;
        ConfigContainer ccVar6;
        RSEG_DB piVar7;
        JUNC_DB jVar8;
        int iVar9;
        JUNC_DB jVar9;
        Vector3Int local_50;
        Vector3Int local_40;
        Vector3Int local_38;
        Vector3Int local_30;

        switch (arg1)
        {
            case 0:
                break;
            case 1:
                rVar6 = LevelManager.instance.FUN_518DC(screen, id - 100);
                DAT_88 = rVar6;
                DAT_90 = 1;
                iVar9 = rVar6.FUN_51334(screen);
                DAT_94 = iVar9 << 16;
                DAT_8C = 6103;
                flags |= 0x88;
                return 0;
            case 2:
                iVar6 = 120;

                if (id == 117)
                    iVar6 = 121;

                ppcVar4 = (CraneLarge)GameManager.instance.FUN_318D0(iVar6);

                if (ppcVar4.GetType().IsSubclassOf(typeof(VigObject)))
                    ppcVar4.UpdateW(21, null);
                
                FUN_30B78();
                return 0;
            case 4:
                iVar6 = 120;

                if (id == 117)
                    iVar6 = 121;

                ppcVar4 = (CraneLarge)GameManager.instance.FUN_318D0(iVar6);

                if (!ppcVar4.GetType().IsSubclassOf(typeof(VigObject)))
                    return 0;

                ppcVar4.UpdateW(21, null);
                return 0;
            default:
                return 0;
            case 8:
                FUN_32B90((uint)arg2);
                return 0;
            case 9:
                if (arg2 != 0)
                {
                    GameManager.instance.FUN_309A0(this);
                    return 0;
                }

                return 0;
        }

        switch(((byte)tags - 1) * 0x1000000 >> 24)
        {
            case 0:
            case 8:
                iVar9 = DAT_8C + 33;
                goto LAB_261C;
            case 1:
            case 4:
                if (DAT_84 == null) goto case 12;

                if (DAT_84.tags == 0) goto case 5;

                if (DAT_90 == 0)
                    iVar9 = DAT_94;
                else
                    iVar9 = 0x10000000 - DAT_94;

                if (iVar9 < 0)
                    iVar9 += 0xffff;

                iVar9 = (iVar9 >> 16) * 4577;

                if (iVar9 < 0)
                    iVar9 += 4095;

                goto LAB_2694;
            case 2:
            case 5:
                if (DAT_84 == null || DAT_84.tags == 0)
                    goto case 12;

                break;
            case 6:
            case 7:
                if (DAT_90 == 0)
                    iVar9 = DAT_94;
                else
                    iVar9 = 0x10000000 - DAT_94;

                if (iVar9 < 0)
                    iVar9 += 0xffff;

                iVar9 = (iVar9 >> 16) * 4577;

                if (iVar9 < 0)
                    iVar9 += 4095;

                LAB_2694:
                iVar3 = 305;

                if (305 < iVar9 >> 12)
                    iVar3 = iVar9 >> 12;

                DAT_8C = iVar3;
                break;
            case 11:
                iVar9 = DAT_8C + 3;
                goto LAB_261C;
            case 12:
                iVar9 = DAT_8C + 33;
                LAB_261C:
                iVar3 = 6103;

                if (iVar9 < 6103)
                    iVar3 = iVar9;

                DAT_8C = iVar3;
                break;
        }

        iVar9 = DAT_94;
        iVar6 = vTransform.position.y;

        if (iVar9 < 0)
            iVar9 += 0xffff;

        DAT_88.FUN_285E4(iVar9 >> 16, ref vTransform.position, out local_50);
        vTransform.position.y = iVar6;
        local_38 = new Vector3Int();
        iVar6 = FUN_2CFBC(vTransform.position, ref local_38);
        vTransform.position.y = iVar6;

        if (DAT_90 == 0)
        {
            local_50.x = -local_50.x;
            local_50.z = -local_50.z;
        }

        local_50.y = -(local_50.x * local_38.x + local_50.z * local_38.z) / local_38.y;
        lVar6 = Utilities.VectorNormal2(local_50, out local_30);
        iVar9 = (int)Utilities.SquareRoot(lVar6);

        if (DAT_90 == 0)
            iVar3 = DAT_8C * -0x10000;
        else
            iVar3 = DAT_8C << 16;

        DAT_94 += iVar3 / iVar9;
        local_40 = Utilities.FUN_2A1E0(local_38, local_30);
        vTransform.rotation.V00 = (short)-local_40.x;
        vTransform.rotation.V10 = (short)-local_40.y;
        vTransform.rotation.V20 = (short)-local_40.z;
        vTransform.rotation.V01 = (short)-local_38.x;
        vTransform.rotation.V11 = (short)-local_38.y;
        vTransform.rotation.V22 = (short)-local_38.z;
        vTransform.rotation.V02 = (short)local_30.x;
        vTransform.rotation.V12 = (short)local_30.y;
        vTransform.rotation.V22 = (short)local_30.z;

        if ((uint)DAT_94 < 0x10000001)
            return 0;

        piVar7 = DAT_88;
        jVar8 = piVar7.DAT_00[DAT_90];
        iVar3 = 0;
        unaff_s3 = null; //not in the original code

        if (jVar8.DAT_11 != 0)
        {
            do
            {
                unaff_s3 = jVar8.DAT_1C[iVar3];

                if (unaff_s3 != piVar7 && unaff_s3.DAT_08 == piVar7.DAT_08) break;

                iVar3++;
            } while (iVar3 < jVar8.DAT_11);
        }

        DAT_88 = unaff_s3;
        jVar9 = unaff_s3.DAT_00[0];
        DAT_90 = jVar9 == jVar8 ? 1 : 0;
        DAT_94 = ((jVar9 == jVar8 ? 1 : 0) == 0 ? 1 : 0) << 28;

        if ((jVar8.DAT_10 & 0x40) == 0)
            return 0;

        sVar1 = (sbyte)jVar8.DAT_12;
        tags = sVar1;

        switch (sVar1)
        {
            case 1:
                FUN_30BA8();
                GameManager.instance.FUN_30CB0(this, 600);
                iVar6 = 120;

                if (id == 117)
                    iVar6 = 121;

                ppcVar4 = (CraneLarge)GameManager.instance.FUN_318D0(iVar6);

                if (ppcVar4.GetType().IsSubclassOf(typeof(VigObject)))
                    ppcVar4.UpdateW(21, this);

                break;
            case 2:
                iVar6 = 49;
                goto LAB_29EC;
            case 3:
            case 6:
                if (DAT_84 != null)
                {
                    if (DAT_84.tags != 0)
                    {
                        DAT_8C = 0;
                        return 0;
                    }

                    return 0;
                }

                break;
            case 4:
                if (DAT_80 == null)
                {
                    ccVar6 = FUN_2C5F4(0x8000);
                    oVar2 = Barge.FUN_1F64();
                    DAT_80 = oVar2;
                    Utilities.FUN_2CB04(this, ccVar6, oVar2);
                    Utilities.ParentChildren(this, this);
                    return 0;
                }

                return 0;
            case 5:
                iVar6 = 50;
                LAB_29EC:
                oVar6 = GameManager.instance.FUN_318D0(iVar6);
                DAT_84 = oVar6;
                break;
            case 9:
                FUN_30BA8();
                GameManager.instance.FUN_30CB0(this, 180);
                return 0;
            case 10:
                if (DAT_80 == null)
                    return 0;

                oVar6 = DAT_80.FUN_2CCBC();
                GameManager.instance.FUN_307CC(oVar6);
                DAT_80 = null;
                break;
        }

        return 0;
    }
}
