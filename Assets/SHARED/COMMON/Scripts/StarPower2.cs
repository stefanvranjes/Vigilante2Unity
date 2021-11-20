using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPower2 : VigObject
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
        uint uVar7;
        VigObject oVar12;
        Vector3Int local_60;
        HitDetection auStack72;

        oVar12 = hit.self;
        uVar7 = GameManager.FUN_2AC5C();

        if ((uVar7 & 31) != 0)
            return 0;

        auStack72 = new HitDetection(null);
        GameManager.instance.FUN_2FB70(this, hit, auStack72);
        local_60 = Utilities.FUN_24148(oVar12.vTransform, auStack72.position);
        UIManager.instance.FUN_4E414(local_60, new Color32(0x80, 0x00, 0x00, 8));
        LevelManager.instance.FUN_4DE54(local_60, 143);

        if (oVar12.type != 2)
            return 0;

        if (-1 < oVar12.id)
            return 0;

        GameManager.instance.FUN_15ADC(~oVar12.id, 32);
        return 0;
    }

    public VigObject DAT_94;

    //FUN_124 (VERTIGO.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        sbyte sVar1;
        Vector3Int piVar2;
        int iVar3;
        VigObject oVar3;
        VigObject oVar4;
        ConfigContainer ccVar4;
        StarPower3 puVar5;
        uint uVar6;
        int iVar7;
        uint uVar7;
        int iVar8;
        VigObject oVar8;
        ConfigContainer ccVar8;
        int iVar9;
        VigTerrain puVar10;
        int iVar11;
        VigObject oVar11;
        int iVar12;
        VigObject oVar12;
        Vector3Int local_60;
        Vector3Int local_58;

        switch (arg1)
        {
            case 0:
                if (tags == 0)
                {
                    piVar2 = GameManager.instance.FUN_2CE50(DAT_94);
                    iVar11 = physics1.Z;
                    iVar12 = iVar11;

                    if (iVar11 < 0)
                        iVar12 = iVar11 + 63;

                    iVar9 = piVar2.x - vTransform.position.x;

                    if (iVar9 < 0)
                        iVar9 += 255;

                    iVar11 = (iVar11 - (iVar12 >> 6)) + (iVar9 >> 8);
                    iVar12 = -9155;

                    if (-9156 < iVar11)
                    {
                        iVar12 = 9155;

                        if (iVar11 < 9156)
                            iVar12 = iVar11;
                    }

                    iVar11 = physics1.W;
                    physics1.Z = iVar12;
                    iVar12 = iVar11;

                    if (iVar11 < 0)
                        iVar12 = iVar11 + 63;

                    iVar9 = piVar2.y - vTransform.position.y;

                    if (iVar9 < 0)
                        iVar9 += 255;

                    iVar11 = (iVar11 - (iVar12 >> 6)) + (iVar9 >> 8);
                    iVar12 = -9155;

                    if (-9156 < iVar11)
                    {
                        iVar12 = 9155;

                        if (iVar11 < 9156)
                            iVar12 = iVar11;
                    }

                    iVar11 = physics2.X;
                    physics1.W = iVar12;
                    iVar12 = iVar11;

                    if (iVar11 < 0)
                        iVar12 = iVar11 + 63;

                    iVar9 = piVar2.z - vTransform.position.z;

                    if (iVar9 < 0)
                        iVar9 += 255;

                    iVar11 = (iVar11 - (iVar12 >> 6)) + (iVar9 >> 8);
                    iVar12 = -9155;

                    if (-9156 < iVar11)
                    {
                        iVar12 = 9155;

                        if (iVar11 < 9156)
                            iVar12 = iVar11;
                    }

                    physics2.X = iVar12;
                    vTransform.position.x += physics1.Z;
                    vTransform.position.y += physics1.W;
                    vTransform.position.z += physics2.X;
                    vr.x += 22;
                    vr.y += 34;

                    if (arg2 != 0)
                    {
                        ApplyRotationMatrix();
                        return 0;
                    }
                }
                else
                {
                    iVar12 = DAT_84.screen.x - vTransform.position.x;
                    iVar11 = DAT_84.screen.z - vTransform.position.z;
                    iVar9 = iVar12;

                    if (iVar12 < 0)
                        iVar9 = iVar12 + 7;

                    iVar9 >>= 3;
                    iVar3 = -9155;

                    if (-9156 < iVar9)
                    {
                        iVar3 = 9155;

                        if (iVar9 < 9156)
                            iVar3 = iVar9;
                    }

                    physics1.Z = iVar3;
                    iVar9 = iVar11;

                    if (iVar11 < 0)
                        iVar9 = iVar11 + 7;

                    iVar9 >>= 3;
                    iVar3 = -9155;

                    if (-9156 < iVar9)
                    {
                        iVar3 = 9155;

                        if (iVar9 < 9156)
                            iVar3 = iVar9;
                    }

                    physics2.X = iVar3;
                    vTransform.position.x += physics1.Z;
                    vTransform.position.z += physics2.X;
                    iVar9 = GameManager.instance.terrain.FUN_1B750((uint)vTransform.position.x, (uint)vTransform.position.z);
                    iVar3 = iVar9 - (vTransform.position.y + 0x30000);

                    if (iVar3 < 0)
                        iVar3 += 15;

                    vTransform.position.y += iVar3 >> 4;

                    if (arg2 != 0)
                        ApplyRotationMatrix();

                    sVar1 = tags;

                    if (sVar1 == 2)
                    {
                        if (arg2 != 0)
                        {
                            oVar3 = PDAT_74;
                            oVar3.vTransform.position.x = vTransform.position.x;
                            oVar3.vTransform.position.y = iVar9;
                            oVar3.vTransform.position.z = vTransform.position.z;
                            local_60 = GameManager.instance.terrain.FUN_1BB50(vTransform.position.x, vTransform.position.z);
                            oVar3.vTransform.rotation.V22 = 0x1000;
                            oVar3.vTransform.rotation.V00 = 0x1000;
                            oVar3.vTransform.rotation.V21 = 0;
                            oVar3.vTransform.rotation.V20 = 0;
                            oVar3.vTransform.rotation.V11 = 0;
                            oVar3.vTransform.rotation.V02 = 0;
                            oVar3.vTransform.rotation.V01 = 0;
                            oVar3.vTransform.rotation.V10 = (short)((local_60.x * -0x1000) / local_60.y);
                            oVar3.vTransform.rotation.V12 = (short)((local_60.z * -0x1000) / local_60.y);
                        }

                        sVar1 = (sbyte)(DAT_19 - 1);
                        DAT_19 = (byte)sVar1;

                        if (sVar1 != 0)
                        {
                            if (iVar12 < 0)
                                iVar12 = -iVar12;

                            if (0xffff < iVar12)
                                return 0;

                            if (iVar11 < 0)
                                iVar11 = -iVar11;

                            if (0xffff < iVar11)
                                return 0;
                        }

                        GameManager.instance.FUN_1E14C(DAT_18, vData.sndList, 4);
                        GameManager.instance.FUN_309A0(PDAT_74);
                        oVar8 = child2.FUN_2CCBC();
                        GameManager.instance.FUN_307CC(oVar8);
                        ccVar8 = FUN_2C5F4(0x8000);
                        oVar4 = vData.ini.FUN_2C17C(4, typeof(VigObject), 8);
                        Utilities.FUN_2CB04(this, ccVar8, oVar4);
                        Utilities.ParentChildren(this, this);
                        tags = 3;
                        GameManager.instance.FUN_30CB0(this, 12);
                        return 0;
                    }

                    if (sVar1 < 3)
                    {
                        if (sVar1 != 1)
                            return 0;

                        iVar11 = vr.x;
                        iVar12 = iVar11;

                        if (iVar11 < 0)
                            iVar12 = iVar11 + 15;

                        if (iVar12 >> 4 == 0)
                        {
                            if (iVar11 < 0)
                                iVar7 = iVar11 + 1;
                            else
                                iVar7 = iVar11 - 1;
                        }
                        else
                            iVar7 = iVar11 - (iVar12 >> 4);

                        vr.x = iVar7;

                        if ((iVar7 & 0xffff) != 0)
                            return 0;

                        oVar8 = vData.ini.FUN_2C17C(3, typeof(VigObject), 8);
                        oVar12 = vData.ini.FUN_2C17C(2, typeof(VigObject), 8);
                        Utilities.ParentChildren(oVar12, oVar12);
                        ccVar4 = FUN_2C5F4(0x8000);
                        Utilities.FUN_2CB04(this, ccVar4, oVar8);
                        Utilities.ParentChildren(this, this);
                        tags = 2;
                        DAT_19 = 95;
                        PDAT_74 = oVar12;
                        oVar12.flags = 4;
                        oVar12.FUN_305FC();
                        return 0;
                    }

                    if (sVar1 < 6)
                    {
                        if (sVar1 < 4)
                            return 0;

                        oVar12 = child2;

                        if (vTransform.position.z < GameManager.instance.DAT_DA0 && 
                            GameManager.instance.DAT_DB0 < iVar9)
                        {
                            if (tags == 5)
                            {
                                tags = 4;
                                GameManager.instance.FUN_309A0(oVar12.PDAT_74);
                                oVar12.PDAT_74 = null;
                            }

                            sVar1 = (sbyte)(DAT_19 - 1);
                            DAT_19 = (byte)sVar1;

                            if (sVar1 != -1)
                                return 0;

                            local_58 = new Vector3Int();
                            local_58.x = vTransform.position.x;
                            local_58.y = GameManager.instance.DAT_DB0;
                            local_58.z = vTransform.position.z;
                            LevelManager.instance.FUN_4DE54(local_58, 138);
                            iVar8 = GameManager.instance.FUN_1DD9C();
                            GameManager.instance.FUN_1E628(iVar8, GameManager.instance.DAT_C2C, 70, local_58);
                        }
                        else
                        {
                            if (tags == 4)
                            {
                                oVar11 = vData.ini.FUN_2C17C(5, typeof(VigObject), 8);
                                Utilities.ParentChildren(oVar11, oVar11);
                                oVar11.flags = 0x24;
                                oVar11.FUN_305FC();
                                oVar12.PDAT_74 = oVar11;
                                tags = 5;
                            }
                            else
                                oVar11 = oVar12.PDAT_74;

                            local_60 = GameManager.instance.terrain.FUN_1B998((uint)vTransform.position.x, (uint)vTransform.position.z);
                            local_60 = Utilities.VectorNormal(local_60);
                            oVar11.vTransform.rotation = Utilities.FUN_2A5EC(local_60);
                            oVar11.vTransform.position.x = vTransform.position.x;
                            oVar11.vTransform.position.y = iVar9;
                            oVar11.vTransform.position.z = vTransform.position.z;
                            iVar12 = vTransform.position.x;

                            if (iVar12 < 0)
                                iVar12 += 0xffff;

                            iVar9 = vTransform.position.z;

                            if (iVar9 < 0)
                                iVar9 += 0xffff;

                            puVar10 = GameManager.instance.terrain;
                            puVar10.vertices[puVar10.chunks[(((uint)(iVar9 >> 16) >> 6) * 4 + ((uint)(iVar12 >> 16) >> 6) * 128) / 4] * 4096 +
                                            ((iVar12 >> 16 & 63) * 128 + (iVar9 >> 16 & 63) * 2) / 2] &= 0x7ff;
                            sVar1 = (sbyte)(DAT_19 - 1);
                            DAT_19 = (byte)sVar1;

                            if (sVar1 != -1)
                                return 0;

                            flags |= 0x20000000;
                            uVar7 = GameManager.FUN_2AC5C();

                            if ((uVar7 & 1) != 0)
                            {
                                puVar5 = LevelManager.instance.xobfList[19].ini.FUN_2C17C(152, typeof(StarPower3), 8) as StarPower3;
                                uVar7 = GameManager.FUN_2AC5C();
                                uVar6 = GameManager.FUN_2AC5C();
                                iVar12 = ((int)(uVar6 & 0xff) - 128 & 0xfff) * 2;
                                iVar3 = ((int)(uVar6 & 0xff) - 128 & 0xfff) * 2;
                                iVar9 = GameManager.DAT_65C90[iVar12] * GameManager.DAT_65C90[iVar3 + 1];

                                if (iVar9 < 0)
                                    iVar9 += 4095;

                                iVar12 = -GameManager.DAT_65C90[iVar12 + 1] * GameManager.DAT_65C90[iVar3 + 1];

                                if (iVar12 < 0)
                                    iVar12 += 4095;

                                local_58 = new Vector3Int();
                                local_58.x = iVar9 >> 12;
                                local_58.y = iVar12 >> 12;
                                local_58.z = -GameManager.DAT_65C90[iVar3];
                                local_58 = Utilities.ApplyMatrixSV(oVar11.vTransform.rotation, local_58);
                                puVar5.vTransform.rotation = Utilities.FUN_2A724(local_58);
                                puVar5.vTransform.position = oVar11.vTransform.position;
                                puVar5.flags = 0xa4;
                                puVar5.FUN_305FC();
                            }
                        }

                        iVar12 = (int)GameManager.FUN_2AC5C();
                        DAT_19 = (byte)((iVar12 * 5 >> 15) + 5);
                    }
                }

                break;
            case 2:
                tags = 4;
                flags = flags & 0xffffffdf | 0x800;
                FUN_2D1DC();
                return 0;
            case 4:
                if (tags == 2)
                {
                    oVar12 = PDAT_74;
                    GameManager.instance.FUN_309A0(oVar12);
                }
                else
                {
                    if (tags == 5 && child2 != null)
                    {
                        oVar12 = child2.PDAT_74;

                        if (oVar12 != null)
                            GameManager.instance.FUN_309A0(oVar12);
                    }
                }

                GameManager.instance.FUN_1DE78(DAT_18);
                break;
        }

        return 0;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        VigObject oVar8;
        VigObject oVar12;

        switch (arg1)
        {
            case 5:
                if (tags < 3)
                    return 0;

                oVar12 = child2;

                if (tags == 5)
                {
                    GameManager.instance.FUN_309A0(oVar12.PDAT_74);
                    oVar12.PDAT_74 = null;
                }

                GameManager.instance.FUN_1DE78(DAT_18);
                DAT_18 = 0;
                GameManager.instance.DAT_1084--;

                if (DAT_94.maxHalfHealth == 0)
                    DAT_94.FUN_3A368();
                else
                {
                    oVar8 = oVar12.FUN_2CCBC();
                    GameManager.instance.FUN_307CC(oVar8);
                    tags = 0;
                    flags |= 0x20;
                }

                return 0xfffffffe;
        }

        return 0;
    }
}
