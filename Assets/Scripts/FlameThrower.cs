using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_48FB4
    public override uint UpdateW(int arg1, int arg2)
    {
        byte bVar1;
        bool bVar2;
        sbyte sVar3;
        ushort uVar4;
        short sVar5;
        ushort uVar5;
        short sVar6;
        ushort uVar7;
        Vehicle pcVar8;
        VigTransform pMVar9;
        VigObject ppcVar10;
        Type pcVar11;
        VigObject ppcVar12;
        int iVar13;
        ConfigContainer ccVar13;
        int iVar14;
        int iVar16;
        ConfigContainer ccVar16;
        Vehicle vVar16;
        ushort uVar18;
        uint uVar19;
        Vector3Int local_30;
        Vector3Int local_38;
        Vector3Int local_28;
        Vector3Int local_20;

        switch (arg1)
        {
            case 0:
                iVar16 = FUN_42330(arg2);

                if (iVar16 < 1)
                    return 0;

                if (id != 0)
                    return 0;

                break;
            case 10:
                arg2 &= 0xfff;

                if (arg2 == 0x314)
                {
                    if (maxHalfHealth < 10)
                        return 0xffffffff;

                    pcVar8 = Utilities.FUN_2CD78(this) as Vehicle;
                    pMVar9 = GameManager.instance.FUN_2CDF4(this);
                    ccVar16 = FUN_2C5F4(0x8000);
                    ppcVar10 = LevelManager.instance.xobfList[19].ini.FUN_2C17C(3, typeof(Ballistic), 8);
                    ppcVar12 = LevelManager.instance.xobfList[19].ini.FUN_2C17C(110, typeof(Brimstone), 8);
                    local_30 = new Vector3Int(0, 0, 0);
                    local_30.y = -0x100;
                    local_30.z = 0x1000;
                    Utilities.FUN_2CA94(this, ccVar16, ppcVar10);
                    ppcVar10.FUN_30BF0();
                    ppcVar12.vTransform.position = Utilities.FUN_24148(pMVar9, ccVar16.v3_1);
                    ppcVar12.vTransform.rotation = new Matrix3x3()
                    {
                        V00 = 0x1000,
                        V01 = 0,
                        V02 = 0,
                        V10 = 0,
                        V11 = 0x1000,
                        V12 = 0,
                        V20 = 0,
                        V21 = 0,
                        V22 = 0x1000
                    };
                    iVar13 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E14C(iVar13, GameManager.instance.DAT_C2C, 60);
                    ppcVar12.flags = 0x60000694;
                    sVar5 = pcVar8.id;
                    ppcVar12.type = 8;
                    ppcVar12.maxHalfHealth = 100;
                    ppcVar12.DAT_80 = pcVar8;
                    ppcVar12.physics2.M3 = 2;
                    ppcVar12.id = sVar5;
                    local_30 = Utilities.ApplyMatrixSV(pMVar9.rotation, local_30);
                    iVar16 = pcVar8.physics1.X;

                    if (iVar16 < 0)
                        iVar16 += 127;

                    ppcVar12.physics1.Z = (iVar16 >> 7) + local_30.x * 4;
                    iVar16 = pcVar8.physics1.Y;

                    if (iVar16 < 0)
                        iVar16 += 127;

                    ppcVar12.physics1.W = (iVar16 >> 7) + local_30.y * 4;
                    iVar16 = pcVar8.physics1.Z;

                    if (iVar16 < 0)
                        iVar16 += 127;

                    ppcVar12.physics2.X = (iVar16 >> 7) + local_30.z * 4;
                    ppcVar12.FUN_305FC();
                    sVar6 = (short)(maxHalfHealth - 10);
                }
                else
                {
                    if (arg2 == 0x312)
                    {
                        if (maxHalfHealth < 2)
                            return 0xffffffff;

                        pcVar8 = Utilities.FUN_2CD78(this) as Vehicle;
                        ccVar13 = FUN_2C5F4(0x8000);
                        uVar19 = 0;
                        uVar4 = 0xfe00;
                        ppcVar10 = LevelManager.instance.xobfList[19].ini.FUN_2C17C(20, typeof(Ballistic), 8);
                        local_30 = new Vector3Int(0, 0, 0);
                        local_30.y = 0x100;
                        Utilities.FUN_2CA94(this, ccVar13, ppcVar10);
                        ppcVar10.FUN_30BF0();
                        GameManager.instance.FUN_2CF00(out local_20, this, ccVar13);
                        iVar13 = GameManager.instance.FUN_1DD9C();
                        GameManager.instance.FUN_1E14C(iVar13, GameManager.instance.DAT_C2C, 60);

                        while (true)
                        {
                            ppcVar10 = LevelManager.instance.xobfList[19].ini.FUN_2C17C(113, typeof(Flamewall1), 8);
                            ppcVar10.flags = 0x600006b4;
                            sVar5 = pcVar8.id;
                            ppcVar10.type = 8;
                            ppcVar10.maxHalfHealth = 100;
                            ppcVar10.DAT_80 = pcVar8;
                            ppcVar10.id = sVar5;
                            ppcVar10.vTransform.position = local_20;

                            if ((uVar19 & 1) == 0)
                                local_30.x = (short)uVar4;
                            else
                                local_30.x = (short)uVar19 * 0x400 - 0x200;

                            local_28 = Utilities.ApplyMatrixSV(pcVar8.vTransform.rotation, local_30);
                            iVar16 = pcVar8.physics1.X;

                            if (iVar16 < 0)
                                iVar16 += 127;

                            ppcVar10.physics1.Z = (iVar16 >> 7) + local_28.x;
                            iVar16 = pcVar8.physics1.Y;

                            if (iVar16 < 0)
                                iVar16 += 127;

                            ppcVar10.physics1.W = (iVar16 >> 7) + local_28.y;
                            iVar16 = pcVar8.physics1.Z;

                            if (iVar16 < 0)
                                iVar16 += 127;

                            ppcVar10.physics2.X = (iVar16 >> 7) + local_28.z;
                            ppcVar10.FUN_305FC();
                            uVar7 = (ushort)(maxHalfHealth - 2);
                            maxHalfHealth = uVar7;
                            uVar19++;

                            if (uVar7 < 2) break;

                            uVar4 -= 0x400;

                            if (7 < (int)uVar19)
                                return 120;
                        }

                        if (uVar7 != 0)
                            return 120;

                        goto LAB_497B8;
                    }

                    if (arg2 != 0x313)
                        return 0;

                    if (maxHalfHealth < 5)
                        return 0xffffffff;

                    vVar16 = Utilities.FUN_2CD78(this) as Vehicle;
                    ccVar13 = FUN_2C5F4(0x8000);
                    ppcVar10 = LevelManager.instance.xobfList[19].ini.FUN_2C17C(222, typeof(OilSlick1), 8);
                    Utilities.ParentChildren(ppcVar10, ppcVar10);
                    ppcVar10.vTransform = GameManager.instance.FUN_2CEAC(this, ccVar13);
                    iVar13 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E14C(iVar13, GameManager.instance.DAT_C2C, 60);
                    ppcVar10.flags = 0xAC;
                    sVar5 = vVar16.id;
                    ppcVar10.maxHalfHealth = 100;
                    ppcVar10.id = sVar5;
                    iVar14 = vVar16.physics1.X;

                    if (iVar14 < 0)
                        iVar14 += 255;

                    ppcVar10.physics1.X = iVar14 >> 8;
                    iVar14 = vVar16.physics1.Y;

                    if (iVar14 < 0)
                        iVar14 += 255;

                    ppcVar10.physics1.Y = iVar14 >> 8;
                    iVar16 = vVar16.physics1.Z;

                    if (iVar16 < 0)
                        iVar16 += 255;

                    ppcVar10.physics1.Z = iVar16 >> 8;
                    ppcVar10.FUN_2D1DC();
                    ppcVar10.FUN_4C9C8();
                    ppcVar10.FUN_305FC();
                    sVar6 = (short)(maxHalfHealth - 5);
                }

                maxHalfHealth = (ushort)sVar6;

                if (sVar6 != 0)
                    return 120;

                LAB_497B8:
                FUN_3A368();
                return 120;
            case 2:
                pcVar8 = (Vehicle)Utilities.FUN_2CD78(this);
                pMVar9 = GameManager.instance.FUN_2CDF4(this);
                ccVar16 = FUN_2C5F4(0x8000);
                bVar1 = DAT_19;
                bVar2 = (bVar1 & 3) != 0;

                if (bVar2)
                    pcVar11 = typeof(Flame2);
                else
                    pcVar11 = typeof(Flame1);

                ppcVar10 = LevelManager.instance.xobfList[19].ini.FUN_2C17C(111, pcVar11, 8);
                Utilities.ParentChildren(ppcVar10, ppcVar10);
                local_30 = new Vector3Int(0, 0, 0);
                local_38 = new Vector3Int(0, 0, 0);
                uVar4 = (ushort)GameManager.FUN_2AC5C();
                local_30.x = (short)((uVar4 & 0x1ff) - 0x100);
                uVar4 = (ushort)GameManager.FUN_2AC5C();
                uVar18 = 20;
                local_30.y = (short)((uVar4 & 0x1ff) - 0x100);
                local_30.z = 0x1000;
                local_38 = local_30;
                sVar6 = id;
                ppcVar10.flags = 0x284;
                ppcVar10.tags = (sbyte)(sVar6 == 0 ? 1 : 0);
                uVar5 = (ushort)pcVar8.id;
                ppcVar10.type = 8;
                ppcVar10.id = (short)uVar5;

                if (pcVar8.doubleDamage != 0)
                    uVar18 = 40;

                ppcVar10.maxHalfHealth = uVar18;
                uVar5 = (ushort)GameManager.FUN_2AC5C();
                ppcVar10.vr.z = uVar5;
                ppcVar10.child2.flags = 0x410;

                if (pcVar11.Equals(typeof(Flame1)))
                    ((Flame1)ppcVar10).DAT_80 = pcVar8;
                else
                    ((Flame2)ppcVar10).DAT_80 = pcVar8;

                ppcVar10.ApplyTransformation();
                ppcVar10.vTransform.position = Utilities.FUN_24148(pMVar9, ccVar16.v3_1);
                local_38 = Utilities.ApplyMatrixSV(pMVar9.rotation, local_38);
                iVar16 = pcVar8.physics1.X;

                if (iVar16 < 0)
                    iVar16 += 127;

                ppcVar10.physics1.Z = ((iVar16 >> 7) + local_38.x * 2);
                iVar16 = pcVar8.physics1.Y;

                if (iVar16 < 0)
                    iVar16 += 127;

                ppcVar10.physics1.W = ((iVar16 >> 7) + local_38.y * 2);
                iVar16 = pcVar8.physics1.Z;

                if (iVar16 < 0)
                    iVar16 += 127;

                ppcVar10.physics2.X = ((iVar16 >> 7) + local_38.z * 2);

                if (bVar2)
                {
                    ppcVar10.FUN_4EDFC();
                    GameManager.instance.FUN_30080(GameManager.instance.DAT_1088, ppcVar10);
                }
                else
                    ppcVar10.FUN_305FC();

                DAT_19--;

                if (DAT_19 == 0)
                {
                    if (maxHalfHealth == 0)
                        FUN_3A368();
                }
                else
                    GameManager.instance.FUN_30CB0(this, 2);

                return (uint)(DAT_19 + 1) * 2;
        }

        return 0;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        byte bVar1;
        bool bVar2;
        sbyte sVar3;
        ushort uVar4;
        ushort uVar5;
        short sVar6;
        Vehicle pcVar8;
        VigTransform pMVar9;
        VigObject ppcVar10;
        Type pcVar11;
        long lVar15;
        int iVar16;
        VigObject oVar16;
        ConfigContainer ccVar16;
        byte bVar17;
        ushort uVar18;
        Vector3Int local_30;
        Vector3Int local_38;

        switch (arg1)
        {
            case 0:
                iVar16 = FUN_42330(arg2);

                if (iVar16 < 1)
                    return 0;

                if (id != 0)
                    return 0;

                break;
            case 1:
                maxHalfHealth = 20;
                return 0;
            case 12:
                if (DAT_18 == 0)
                {
                    sVar3 = (sbyte)GameManager.instance.FUN_1DD9C();
                    DAT_18 = sVar3;
                    GameManager.instance.FUN_1E14C(sVar3, GameManager.instance.DAT_C2C, 59);
                }

                maxHalfHealth--;
                oVar16 = Utilities.FUN_2CD78(this);
                bVar17 = 12;

                if (oVar16.id < 0)
                    bVar17 = 4;

                DAT_19 = bVar17;
                goto case 2;
            case 2:
                pcVar8 = (Vehicle)Utilities.FUN_2CD78(this);
                pMVar9 = GameManager.instance.FUN_2CDF4(this);
                ccVar16 = FUN_2C5F4(0x8000);
                bVar1 = DAT_19;
                bVar2 = (bVar1 & 3) != 0;

                if (bVar2)
                    pcVar11 = typeof(Flame2);
                else
                    pcVar11 = typeof(Flame1);

                ppcVar10 = LevelManager.instance.xobfList[19].ini.FUN_2C17C(111, pcVar11, 8);
                Utilities.ParentChildren(ppcVar10, ppcVar10);
                local_30 = new Vector3Int(0, 0, 0);
                local_38 = new Vector3Int(0, 0, 0);
                uVar4 = (ushort)GameManager.FUN_2AC5C();
                local_30.x = (short)((uVar4 & 0x1ff) - 0x100);
                uVar4 = (ushort)GameManager.FUN_2AC5C();
                uVar18 = 20;
                local_30.y = (short)((uVar4 & 0x1ff) - 0x100);
                local_30.z = 0x1000;
                local_38 = local_30;
                sVar6 = id;
                ppcVar10.flags = 0x284;
                ppcVar10.tags = (sbyte)(sVar6 == 0 ? 1 : 0);
                uVar5 = (ushort)pcVar8.id;
                ppcVar10.type = 8;
                ppcVar10.id = (short)uVar5;

                if (pcVar8.doubleDamage != 0)
                    uVar18 = 40;

                ppcVar10.maxHalfHealth = uVar18;
                uVar5 = (ushort)GameManager.FUN_2AC5C();
                ppcVar10.vr.z = uVar5;
                ppcVar10.child2.flags = 0x410;

                if (pcVar11.Equals(typeof(Flame1)))
                    ((Flame1)ppcVar10).DAT_80 = pcVar8;
                else
                    ((Flame2)ppcVar10).DAT_80 = pcVar8;

                ppcVar10.ApplyTransformation();
                ppcVar10.vTransform.position = Utilities.FUN_24148(pMVar9, ccVar16.v3_1);
                local_38 = Utilities.ApplyMatrixSV(pMVar9.rotation, local_38);
                iVar16 = pcVar8.physics1.X;

                if (iVar16 < 0)
                    iVar16 += 127;

                ppcVar10.physics1.Z = ((iVar16 >> 7) + local_38.x * 2);
                iVar16 = pcVar8.physics1.Y;

                if (iVar16 < 0)
                    iVar16 += 127;

                ppcVar10.physics1.W = ((iVar16 >> 7) + local_38.y * 2);
                iVar16 = pcVar8.physics1.Z;

                if (iVar16 < 0)
                    iVar16 += 127;

                ppcVar10.physics2.X = ((iVar16 >> 7) + local_38.z * 2);

                if (bVar2)
                {
                    ppcVar10.FUN_4EDFC();
                    GameManager.instance.FUN_30080(GameManager.instance.DAT_1088, ppcVar10);
                }
                else
                    ppcVar10.FUN_305FC();

                DAT_19--;

                if (DAT_19 == 0)
                {
                    if (maxHalfHealth == 0)
                        FUN_3A368();
                }
                else
                    GameManager.instance.FUN_30CB0(this, 2);

                return (uint)(DAT_19 + 1) * 2;
            case 13:
                local_30 = Utilities.FUN_24304(arg2.vTransform, ((Vehicle)arg2).target.vTransform.position);

                if (0x3bfff < local_30.z)
                    return 0;

                lVar15 = Utilities.Ratan2(local_30.x, local_30.z);
                iVar16 = (int)(lVar15 << 20) >> 20;

                if (iVar16 < 0)
                    iVar16 = -iVar16;

                return (uint)(iVar16 < 113 ? 1 : 0);
        }

        return 0;
    }
}
