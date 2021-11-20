using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aligator : VigObject
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
        int iVar8;
        VigObject oVar8;
        Vehicle vVar8;
        int iVar9;
        VigObject oVar9;
        Vector3Int local_70;
        Vector3Int local_60;
        HitDetection auStack80;
        Vector3Int auStack24;

        oVar8 = hit.self;

        if (oVar8.type == 2)
        {
            vVar8 = (Vehicle)oVar8;

            if (DAT_1A == 27)
            {
                GameManager.instance.FUN_2F798(this, hit);
                local_70 = Utilities.FUN_24148(vTransform, hit.position);
                LevelManager.instance.FUN_4DE54(local_70, 142);
                iVar9 = GameManager.instance.FUN_1DD9C();
                GameManager.instance.FUN_1E580(iVar9, GameManager.instance.DAT_C2C, 24, local_70);
                iVar9 = 25;

                if (vVar8.id < 0)
                    iVar9 = 100;

                vVar8.FUN_3A064(iVar9, local_70, true);
                flags |= 0x20;
                local_60 = new Vector3Int(0, -4587, 0);
                local_70.x = local_70.x / 2 + hit.self.vTransform.position.x / 2;
                local_70.y = local_70.y / 2 + hit.self.vTransform.position.y / 2;
                local_70.z = local_70.z / 2 + hit.self.vTransform.position.z / 2;
                vVar8.FUN_2B370(local_60, local_70);
                return 0;
            }

            if (tags != 1)
                return 0;

            if (DAT_1A != 26)
                return 0;

            auStack80 = new HitDetection(null);
            GameManager.instance.FUN_2FB70(this, hit, auStack80);
            auStack24 = Utilities.FUN_24238(vTransform.rotation, auStack80.normal1);

            if (0x800 < auStack24.z)
            {
                iVar9 = GameManager.instance.FUN_1DD9C();
                GameManager.instance.FUN_1E628(iVar9, vData.sndList, 5, vTransform.position);
                flags |= 0x20;
                FUN_2C124(27);
                Utilities.ParentChildren(this, this);
                GameManager.instance.FUN_30CB0(this, 20);
                return 0;
            }

            return 0;
        }

        if (oVar8.type == 8 && tags == 0)
        {
            tags = 2;
            iVar8 = (int)GameManager.FUN_2AC5C();
            oVar9 = GameManager.instance.FUN_30250(GameManager.instance.DAT_1078, (iVar8 * 5 >> 15) + 1200);
            DAT_80 = oVar9;
            FUN_30B78();
            FUN_30BF0();
            return 0;
        }

        if (oVar8.id != 100)
            return 0;

        if (tags != 1)
            return 0;

        tags = 2;
        iVar8 = (int)GameManager.FUN_2AC5C();
        oVar9 = GameManager.instance.FUN_30250(GameManager.instance.DAT_1078, (iVar8 * 5 >> 15) + 1200);
        DAT_80 = oVar9;
        return 0;
    }

    //FUN_1698 (BAYOU.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        sbyte sVar1;
        short sVar2;
        bool bVar3;
        VigTuple ppiVar5;
        List<VigTuple> ppiVar6;
        short sVar7;
        int iVar8;
        VigObject oVar8;
        Particle1 pVar8;
        VigObject oVar9;
        int iVar10;
        VigTuple2 tVar10;
        uint uVar11;
        int iVar12;
        int iVar13;
        VigObject oVar13;
        int iVar14;
        uint uVar14;
        Vector3Int local_78;
        Vector3Int local_80;
        Vector3Int local_88;

        switch (arg1)
        {
            case 0:
                iVar8 = DAT_80.vTransform.position.x - vTransform.position.x;
                iVar13 = DAT_80.vTransform.position.z - vTransform.position.z;
                iVar10 = Utilities.Ratan2(iVar8, iVar13);
                iVar12 = ((iVar10 - (ushort)vr.y) * 0x100000) >> 20;
                iVar10 = -22;

                if (-23 < iVar12)
                {
                    iVar10 = 22;

                    if (iVar12 < 23)
                        iVar10 = iVar12;
                }

                iVar14 = (ushort)vr.y + iVar10;
                vr.y = (short)iVar14;
                local_78 = new Vector3Int();
                local_78.x = GameManager.DAT_65C90[(iVar14 & 0xfff) * 2];
                local_78.z = GameManager.DAT_65C90[(vr.y & 0xfff) * 2 + 1];

                if (DAT_1A == 25)
                {
                    iVar10 = local_78.x * 3051;

                    if (iVar10 < 0)
                        iVar10 += 4095;

                    vTransform.position.x += iVar10 >> 12;
                    iVar10 = local_78.z * 3051;
                }
                else
                {
                    iVar10 = local_78.x * 4577;

                    if (iVar10 < 0)
                        iVar10 += 4095;

                    vTransform.position.x += iVar10 >> 12;
                    iVar10 = local_78.z * 4577;
                }

                if (iVar10 < 0)
                    iVar10 += 4095;

                vTransform.position.z += iVar10 >> 12;
                local_80 = GameManager.instance.terrain.FUN_1BB50(vTransform.position.x, vTransform.position.z);
                local_80 = Utilities.VectorNormal(local_80);
                local_78.y = -(local_78.x * local_80.x + local_78.z * local_80.z) / local_80.y;
                local_88 = Utilities.FUN_2A1E0(local_80, local_78);
                vTransform.rotation.V00 = (short)-local_88.x;
                vTransform.rotation.V10 = (short)-local_88.y;
                vTransform.rotation.V20 = (short)-local_88.z;
                vTransform.rotation.V01 = (short)-local_80.x;
                vTransform.rotation.V11 = (short)-local_80.y;
                vTransform.rotation.V21 = (short)-local_80.z;
                vTransform.rotation.V02 = (short)local_78.x;
                vTransform.rotation.V12 = (short)local_78.y;
                vTransform.rotation.V22 = (short)local_78.z;
                iVar10 = GameManager.instance.terrain.FUN_1B750((uint)vTransform.position.x, (uint)vTransform.position.z);

                if (GameManager.instance.DAT_DB0 < iVar10)
                {
                    if (1 < (ushort)DAT_1A - 26U)
                    {
                        FUN_2C124(26);
                        Utilities.ParentChildren(this, this);
                        flags |= 0x10000;
                    }

                    if ((GameManager.instance.DAT_28 - DAT_19 & 3) == 0)
                    {
                        pVar8 = LevelManager.instance.FUN_4DE54(vTransform.position, 146);
                        sVar7 = (short)GameManager.FUN_2AC5C();
                        pVar8.vr.y = sVar7;
                        pVar8.flags &= 0xffffffef;
                        pVar8.ApplyTransformation();
                    }

                    vTransform.position.y = GameManager.instance.DAT_DB0;
                }
                else
                {
                    if (DAT_1A != 25)
                    {
                        if (tags == 1 && DAT_1A == 26)
                        {
                            tags = 2;
                            iVar12 = (int)GameManager.FUN_2AC5C();
                            oVar9 = GameManager.instance.FUN_30250(GameManager.instance.DAT_1078, (iVar12 * 5 >> 15) + 1200);
                            DAT_80 = oVar9;
                            FUN_30C68();
                        }

                        FUN_2C124(25);
                        flags &= 0xfffeffff;
                    }

                    if (1 < tags)
                    {
                        if (iVar8 < 0)
                            iVar8 = -iVar8;

                        if (iVar8 < 0x10000)
                        {
                            if (iVar13 < 0)
                                iVar13 = -iVar13;

                            if (iVar13 < 0x10000)
                            {
                                FUN_2C124(24);
                                FUN_30C20();
                                FUN_30BA8();
                                GameManager.instance.FUN_30CB0(this, 60);
                                Utilities.ParentChildren(this, this);
                                tags = 0;
                            }
                        }
                    }

                    vTransform.position.y = iVar10;
                }

                break;
            case 1:
                if (arg2 == 0)
                {
                    flags |= 0x100;
                    GameManager.instance.FUN_30CB0(this, 60);
                    return 0;
                }

                return 0;
            case 2:
                sVar2 = DAT_1A;
                oVar8 = null;

                if (sVar2 == 27)
                {
                    flags &= 0xffffffdf;
                    return 0;
                }

                uVar14 = 0xffffffff;
                bVar3 = false;

                if ((LevelManager.instance.level.flags & 0x1000000) != 0)
                {
                    if (sVar2 == 24)
                    {
                        if (GameManager.instance.DAT_DB0 < vTransform.position.y)
                            bVar3 = true;
                    }
                    else
                    {
                        if (sVar2 != 25)
                            bVar3 = true;
                    }
                }

                ppiVar6 = GameManager.instance.worldObjs;

                for (int i = 0; i < ppiVar6.Count; i++)
                {
                    ppiVar5 = ppiVar6[i];
                    oVar13 = ppiVar5.vObject;

                    if (oVar13.type == 2 && oVar13.maxHalfHealth != 0)
                    {
                        if (bVar3) goto LAB_1EEC;

                        tVar10 = GameManager.instance.FUN_2FF3C((uint)oVar13.vTransform.position.x, (uint)oVar13.vTransform.position.z);

                        if (tVar10 == null || tVar10.id != 1) continue;

                        LAB_1EEC:
                        uVar11 = (uint)Utilities.FUN_29F6C(vTransform.position, oVar13.screen);

                        if (uVar11 < uVar14)
                        {
                            oVar8 = oVar13;
                            uVar14 = uVar11;
                        }
                    }
                }

                sVar1 = tags;

                if (sVar1 != 1)
                {
                    if (1 < sVar1)
                    {
                        if (sVar1 != 2)
                            return 0;

                        GameManager.instance.FUN_30CB0(this, 60);

                        if (oVar8 != null)
                        {
                            tags = 1;
                            DAT_80 = oVar8;
                            return 0;
                        }

                        return 0;
                    }

                    if (sVar1 != 0)
                        return 0;

                    if (oVar8 != null)
                    {
                        tags = 1;
                        DAT_80 = oVar8;
                        FUN_30B78();
                        FUN_30BF0();
                    }

                    GameManager.instance.FUN_30CB0(this, 60);
                    return 0;
                }

                if (oVar8 != null)
                {
                    DAT_80 = oVar8;
                    GameManager.instance.FUN_30CB0(this, 60);
                    return 0;
                }

                tags = 2;
                iVar8 = (int)GameManager.FUN_2AC5C();
                oVar9 = GameManager.instance.FUN_30250(GameManager.instance.DAT_1078, (iVar8 * 5 >> 15) + 1200);
                DAT_80 = oVar9;
                break;
        }

        return 0;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        int iVar8;
        VigObject oVar9;

        switch (arg1)
        {
            case 5:
                if (DAT_1A != 27)
                    return 0;

                FUN_2C124(26);
                Utilities.ParentChildren(this, this);

                if ((flags & 0x20) != 0)
                {
                    flags &= 0xffffffdf;
                    tags = 2;
                    iVar8 = (int)GameManager.FUN_2AC5C();
                    oVar9 = GameManager.instance.FUN_30250(GameManager.instance.DAT_1078, (iVar8 * 5 >> 15) + 1200);
                    DAT_80 = oVar9;
                    GameManager.instance.FUN_30CB0(this, 120);
                    return 0;
                }

                break;
        }

        return 0;
    }
}
