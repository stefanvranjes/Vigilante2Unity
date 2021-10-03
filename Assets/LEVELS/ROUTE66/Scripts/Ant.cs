using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //0xE0-EC (ROUTE66.DLL)
    private static int DAT_E0, DAT_E8, DAT_EC = 0;
    private static int DAT_E4 = 0x1000;

    public VigObject DAT_8C; //0x8C
    public VigObject DAT_90; //0x90

    public static VigObject OnInitialize(XOBF_DB arg1, int arg2, uint arg3)
    {
        return arg1.ini.FUN_2C17C_2((ushort)arg2, typeof(Ant), arg3);
    }

    public override uint OnCollision(HitDetection hit)
    {
        VigObject oVar4;
        Vehicle vVar4;
        int iVar6;
        Vector3Int local_40;
        Vector3Int local_30;
        Vector3Int local_20;

        oVar4 = hit.self;

        if (oVar4.type == 2)
        {
            vVar4 = (Vehicle)oVar4;
            Utilities.FUN_2A168(out local_40, vTransform.position, vVar4.vTransform.position);
            local_30 = new Vector3Int();
            local_30.y = -0xE0000;
            iVar6 = local_40.x * 0x1400;
            local_30.x = local_40.x << 5;
            local_30.z = local_40.z << 5;

            if (iVar6 < 0)
                iVar6 += 4095;

            local_20 = new Vector3Int();
            local_20.x = vVar4.vTransform.position.x + (iVar6 >> 12);
            local_20.y = vVar4.vTransform.position.y;
            iVar6 = local_40.y * 0x1400;

            if (iVar6 < 0)
                iVar6 += 4095;

            local_20.z = vVar4.vTransform.position.z + (iVar6 >> 12);
            vVar4.FUN_2B370(local_30, local_20);

            if (tags != 0)
                tags = 0;

            flags |= 0x20;
            return 0;
        }

        if (oVar4.type != 8)
            return 0;

        return UpdateW(8, oVar4.maxHalfHealth);
    }

    //FUN_221C (ROUTE66.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        sbyte sVar1;
        ushort uVar2;
        short sVar3;
        int iVar4;
        VigObject oVar4;
        int iVar5;
        uint uVar5;
        VigMesh mVar5;
        VigShadow sVar5;
        int iVar6;
        VigObject oVar6;
        VigObject piVar7;
        uint uVar8;
        List<VigTuple> ppiVar10;
        VigTuple ppiVar11;
        uint uVar12;
        int iVar13;
        VigObject piVar13;
        Vector2Int v2Var14;
        Vector3Int local_40;

        switch (arg1)
        {
            case 0:
                if (physics1.X < 0x10000)
                {
                    iVar4 = physics1.X + physics1.Y;
                    oVar6 = child2;
                    physics1.X = iVar4;

                    if (iVar4 < 0)
                        iVar4 += 15;

                    sVar3 = (short)(iVar4 >> 4);
                    oVar6.vTransform.rotation.V22 = sVar3;
                    oVar6.vTransform.rotation.V00 = sVar3;
                    oVar6.vTransform.rotation.V11 = sVar3;
                }

                if (arg2 != 0)
                {
                    uVar5 = GameManager.instance.FUN_1E7A8(vTransform.position);
                    GameManager.instance.FUN_1E2C8(DAT_18, uVar5);
                }

                sVar1 = tags;

                if (sVar1 == 1)
                {
                    if (DAT_90.maxHalfHealth == 0)
                        tags = 0;

                    iVar4 = DAT_90.vTransform.position.x - vTransform.position.x;
                    iVar6 = DAT_90.vTransform.position.z - vTransform.position.z;
                    FUN_1CC8(iVar4, iVar6);
                    FUN_1F60(DAT_90.vTransform.position);

                    if (iVar4 < 0)
                        iVar4 = -iVar4;

                    if (iVar4 < 0xc8000)
                    {
                        if (iVar6 < 0)
                            iVar6 = -iVar6;

                        if (iVar6 < 0xc8000)
                        {
                            tags = 2;
                            GameManager.instance.FUN_30CB0(this, 0);
                            return 0;
                        }
                    }

                    if (iVar4 < 0x11940001) //unaff_s3 an error in the original code?
                    {
                        iVar4 = physics2.Y - vTransform.position.x;
                        iVar6 = physics2.W - vTransform.position.z;

                        if (iVar4 < 0)
                            iVar4 = -iVar4;

                        if (0x22ffff < iVar4)
                            return 0;

                        if (iVar6 < 0)
                            iVar6 = -iVar6;

                        if (0x22ffff < iVar6)
                            return 0;
                    }

                    tags = 0;
                    return 0;
                }

                if (1 < sVar1)
                {
                    if (4 < sVar1)
                        return 0;

                    FUN_1CC8(DAT_90.vTransform.position.x - vTransform.position.x,
                             DAT_90.vTransform.position.z - vTransform.position.z);
                    FUN_1F60(DAT_90.vTransform.position);
                }

                if (sVar1 != 0)
                    return 0;

                iVar4 = physics1.Y - vTransform.position.x;
                iVar6 = physics1.W - vTransform.position.z;
                FUN_1CC8(iVar4, iVar6);
                FUN_1F60(new Vector3Int(physics1.Y, physics1.Z, physics1.W));

                if (iVar4 < 0)
                    iVar4 = -iVar4;

                if (0x77fff < iVar4)
                    return 0;

                if (iVar6 < 0)
                    iVar6 = -iVar6;

                if (0x77fff < iVar6)
                    return 0;

                piVar13 = null;
                uVar12 = 0x7fff0000;

                if (GameManager.instance.worldObjs != null)
                {
                    ppiVar10 = GameManager.instance.worldObjs;

                    for (int i = 0; i < ppiVar10.Count; i++)
                    {
                        ppiVar11 = ppiVar10[i];
                        piVar7 = ppiVar11.vObject;

                        if (piVar7.type == 2 && piVar7.maxHalfHealth != 0)
                        {
                            local_40 = new Vector3Int();
                            local_40.x = piVar7.screen.x - screen.x;
                            local_40.y = piVar7.screen.y - screen.y;
                            local_40.z = piVar7.screen.z - screen.z;
                            v2Var14 = Utilities.FUN_2A1C0(local_40);
                            uVar8 = (uint)v2Var14.x >> 16 | (uint)v2Var14.y << 16;

                            if ((int)uVar8 < (int)uVar12)
                            {
                                piVar13 = ppiVar11.vObject;
                                uVar12 = uVar8;
                            }
                        }
                    }
                }

                DAT_90 = piVar13;

                if (piVar13 == null)
                    return 0;

                break;
            case 1:
                FUN_2D1DC();
                GameObject obj = new GameObject();
                mVar5 = LevelManager.instance.xobfList[18].FUN_2CB74(obj, 93, true);
                iVar6 = DAT_58;
                iVar4 = iVar6;

                if (iVar6 < 0)
                    iVar4 = iVar6 + 7;

                if (iVar6 < 0)
                    iVar6 += 3;

                sVar5 = Utilities.FUN_4C44C(mVar5, iVar4 >> 3, iVar6 >> 2, obj);
                vShadow = sVar5;
                maxHalfHealth = 300;
                physics1.Z = 50;
                flags |= 8;
                return 0;
            case 2:
                sVar1 = tags;

                if (sVar1 < 2)
                    return 0;

                if (sVar1 < 4)
                {
                    if ((flags & 0x1000000) == 0)
                        FUN_1A60();

                    tags++;
                    iVar4 = (int)GameManager.FUN_2AC5C();

                    if (tags == 4)
                        iVar4 += 240;
                    else
                        iVar4 += 42;

                    GameManager.instance.FUN_30CB0(this, iVar4);
                    return 0;
                }

                if (sVar1 != 4)
                    return 0;

                break;
            case 8:
                oVar4 = Utilities.FUN_2CDB0(this);

                if (oVar4 == null)
                    oVar4 = this;

                uVar2 = oVar4.maxHalfHealth;
                iVar13 = uVar2;

                if (arg2 < uVar2)
                    iVar13 = arg2;

                oVar4.maxHalfHealth = (ushort)(uVar2 - iVar13);
                iVar6 = oVar4.physics1.Z - iVar13;
                oVar4.physics1.Z = iVar6;

                if (0 < iVar6)
                    return 0;

                if ((oVar4.flags & 0x1000000) != 0)
                    return 0;

                oVar4.physics1.Z = 50;
                iVar5 = GameManager.instance.FUN_1DD9C();
                GameManager.instance.FUN_1E580(iVar5, oVar4.vData.sndList, 3, oVar4.vTransform.position);
                oVar4.FUN_2C124(17);
                oVar4.flags |= 0x1000000;
                oVar6 = oVar4.child2.child2.child2;

                while (oVar6 != null && oVar6.id != 1)
                    oVar6 = oVar6.child;

                ((Ant)oVar4).DAT_8C = oVar6;
                ((Body)oVar6.child2).state = _BODY_TYPE.Ant;
                iVar6 = oVar4.physics1.X;

                if (iVar6 < 0x10000)
                {
                    oVar4 = oVar4.child2;

                    if (iVar6 < 0)
                        iVar6 += 15;

                    sVar3 = (short)(iVar6 >> 4);
                    oVar4.vTransform.rotation.V22 = sVar3;
                    oVar4.vTransform.rotation.V11 = sVar3;
                    oVar4.vTransform.rotation.V00 = sVar3;
                    return 0;
                }

                return 0;
            default:
                return 0;
            case 9:
                if (arg2 != 0)
                {
                    GameManager.instance.FUN_309A0(this);

                    if (DAT_18 != 0)
                    {
                        GameManager.instance.FUN_1DE78(DAT_18);
                        DAT_18 = 0;
                    }

                    FUN_134C();
                    return 0;
                }

                return 0;
        }

        tags = 1;
        return 0;
    }

    private Ant2 FUN_134C()
    {
        Ant2 ppcVar1;
        int iVar2;
        VigObject oVar2;
        Vector3Int local_10;
        int local_4;

        local_10 = new Vector3Int(DAT_E0, DAT_E4, DAT_E8);
        local_4 = DAT_EC;
        ppcVar1 = LevelManager.instance.xobfList[42].ini.FUN_2C17C(12, typeof(Ant2), 0) as Ant2;

        if (ppcVar1 == null)
            return null;

        oVar2 = GameManager.instance.FUN_30250(GameManager.instance.DAT_1078, 49);

        if (oVar2 == null)
            return null;

        ppcVar1.screen = oVar2.screen;
        ppcVar1.id = 1000;
        ppcVar1.maxHalfHealth = 100;
        ppcVar1.DAT_19 = 1;
        ppcVar1.state = _ANT2_TYPE.Ant2;
        ppcVar1.flags |= 0x100;
        local_10 = Utilities.VectorNormal(local_10);
        ppcVar1.vTransform.position.x = ppcVar1.screen.x + local_10.x * -1750;
        ppcVar1.vTransform.position.y = ppcVar1.screen.y + local_10.y * -1750;
        ppcVar1.vTransform.position.z = ppcVar1.screen.z + local_10.z * -1750;
        iVar2 = local_10.x * 30517;

        if (iVar2 < 0)
            iVar2 += 4095;

        ppcVar1.physics1.X = (iVar2 >> 12) << 7;
        iVar2 = local_10.y * 30517;

        if (iVar2 < 0)
            iVar2 += 4095;

        ppcVar1.physics1.Y = (iVar2 >> 12) << 7;
        iVar2 = local_10.z * 30517;

        if (iVar2 < 0)
            iVar2 += 4095;

        ppcVar1.physics1.Z = (iVar2 >> 12) << 7;
        ppcVar1.physics2.Z = 32640;
        ppcVar1.FUN_2D1DC();
        ppcVar1.ApplyRotationMatrix();
        ppcVar1.FUN_305FC();
        GameManager.instance.FUN_30CB0(ppcVar1, 360);
        return ppcVar1;
    }

    //FUN_1A60 (ROUTE66.DLL)
    private Laser FUN_1A60()
    {
        short sVar1;
        int iVar2;
        ConfigContainer ccVar2;
        Laser ppcVar3;
        Ballistic puVar4;
        int iVar5;
        int iVar6;
        VigObject oVar6;
        VigObject oVar7;
        Vector3Int local_28;
        Vector3Int local_18;

        oVar7 = DAT_8C.child2;
        ccVar2 = oVar7.FUN_2C5F4(0x8000);
        ppcVar3 = vData.ini.FUN_2C17C(18, typeof(Laser), 8) as Laser;
        puVar4 = vData.ini.FUN_2C17C(19, typeof(Ballistic), 8) as Ballistic;
        sVar1 = id;
        ppcVar3.type = 8;
        ppcVar3.id = sVar1;
        ppcVar3.vTransform = GameManager.instance.FUN_2CEAC(oVar7, ccVar2);
        ppcVar3.screen = ppcVar3.vTransform.position;
        oVar6 = DAT_90;
        local_28 = new Vector3Int();
        local_28.x = oVar6.screen.x - ppcVar3.screen.x;
        local_28.y = oVar6.screen.y - ppcVar3.screen.y;
        local_28.z = oVar6.screen.z - ppcVar3.screen.z;
        Utilities.FUN_29FC8(local_28, out local_18);
        iVar6 = -local_18.x;

        if (0 < local_18.x)
            iVar6 += 3;

        iVar5 = ppcVar3.vTransform.rotation.V02;

        if (iVar5 < 0)
            iVar5 += 3;

        local_18.x = local_18.x + (iVar6 >> 2) + (iVar5 >> 2);
        iVar6 = -local_18.y;

        if (0 < local_18.y)
            iVar6 += 3;

        iVar5 = ppcVar3.vTransform.rotation.V12;

        if (iVar5 < 0)
            iVar5 += 3;

        local_18.y = local_18.y + (iVar6 >> 2) + (iVar5 >> 2);
        iVar6 = -local_18.z;

        if (0 < local_18.z)
            iVar6 += 3;

        iVar5 = ppcVar3.vTransform.rotation.V22;

        if (iVar5 < 0)
            iVar5 += 3;

        local_18.z = local_18.z + (iVar6 >> 2) + (iVar5 >> 2);
        local_18 = Utilities.VectorNormal(local_18);
        ppcVar3.vTransform.rotation = Utilities.FUN_2A724(local_18);
        Utilities.FUN_2CA94(oVar7, ccVar2, puVar4);
        Utilities.ParentChildren(oVar7, oVar7);
        ppcVar3.flags = 0x84;
        ppcVar3.maxHalfHealth = 100;
        ppcVar3.physics2.M3 = 2;
        ppcVar3.physics2.M2 = 8;
        ppcVar3.FUN_305FC();
        puVar4.flags = 0x10;
        iVar2 = GameManager.instance.FUN_1DD9C();
        GameManager.instance.FUN_1E5D4(iVar2, vData.sndList, 1, vTransform.position);
        return ppcVar3;
    }

    //FUN_1CC8 (ROUTE66.DLL)
    private int FUN_1CC8(int param1, int param2)
    {
        int iVar1;
        int iVar2;
        int iVar3;
        int iVar4;
        Vector3Int local_18;
        Vector3Int local_10;
        Vector3Int local_8;

        iVar4 = 5340;

        if (tags < 2)
            iVar4 = 6103;

        iVar1 = -1;

        if ((flags & 0x1000000) == 0)
        {
            iVar2 = Utilities.Ratan2(param1, param2);
            iVar3 = (int)((iVar2 - (ushort)vr.y) * 0x100000) >> 20;
            iVar2 = iVar3;

            if (iVar3 < 0)
                iVar2 = -iVar3;

            if (2 < iVar2)
            {
                iVar2 = -22;

                if (-23 < iVar3)
                {
                    iVar2 = 22;

                    if (iVar3 < 23)
                        iVar2 = iVar3;
                }

                iVar3 = iVar2;

                if (iVar2 < 0)
                    iVar3 = -iVar2;

                vr.y += iVar2;
                iVar4 -= (iVar3 * 3051) / 22;
            }

            local_8 = new Vector3Int();
            local_8.x = GameManager.DAT_65C90[(vr.y & 0xfff) * 2];
            iVar2 = local_8.x * iVar4;
            local_8.z = GameManager.DAT_65C90[(vr.y & 0xfff) * 2 + 1];

            if (iVar2 < 0)
                iVar2 += 4095;

            vTransform.position.x += iVar2 >> 12;
            iVar4 = local_8.z * iVar4;

            if (iVar4 < 0)
                iVar4 += 4095;

            iVar4 = vTransform.position.z + (iVar4 >> 12);
            vTransform.position.z = iVar4;
            local_10 = GameManager.instance.terrain.FUN_1BB50(vTransform.position.x, iVar4);
            local_10 = Utilities.VectorNormal(local_10);
            local_8.y = -(local_8.x * local_10.x + local_8.z * local_10.z) / local_10.y;
            local_18 = Utilities.FUN_2A1E0(local_10, local_8);
            vTransform.rotation.V00 = (short)-local_18.x;
            vTransform.rotation.V10 = (short)-local_18.y;
            vTransform.rotation.V20 = (short)-local_18.z;
            vTransform.rotation.V01 = (short)-local_10.x;
            vTransform.rotation.V11 = (short)-local_10.y;
            vTransform.rotation.V21 = (short)-local_10.z;
            vTransform.rotation.V02 = (short)local_8.x;
            vTransform.rotation.V12 = (short)local_8.y;
            vTransform.rotation.V22 = (short)local_8.z;
            iVar1 = GameManager.instance.terrain.FUN_1B750((uint)vTransform.position.x, (uint)vTransform.position.z);
            vTransform.position.y = iVar1;
            iVar1 = 0;
        }

        return iVar1;
    }

    //FUN_1F60 (ROUTE66.DLL)
    private void FUN_1F60(Vector3Int param1)
    {
        short sVar1;
        int iVar2;
        VigTransform tVar2;
        int iVar3;
        int iVar4;
        uint uVar5;
        int iVar6;
        VigObject oVar7;
        Vector3Int local_18;
        uint local_8;
        int local_4;

        oVar7 = DAT_8C;
        tVar2 = GameManager.instance.FUN_2CDF4(oVar7);
        local_18 = new Vector3Int();
        local_18.x = param1.x - tVar2.position.x;
        local_18.y = param1.y - tVar2.position.y;
        local_18.z = param1.z - tVar2.position.z;
        local_18 = Utilities.FUN_2426C(tVar2.rotation, 
            new Matrix2x4(local_18.x, local_18.y, local_18.z, 0));
        sVar1 = (short)Utilities.Ratan2(local_18.x, local_18.z);
        local_8 = (uint)((long)local_18.z * local_18.z);
        local_4 = (int)((ulong)((long)local_18.z * local_18.z) >> 32);
        uVar5 = (uint)((long)local_18.x * local_18.x) + local_8;
        iVar3 = Utilities.FUN_2ABC4(uVar5, (int)((ulong)((long)local_18.x * local_18.x) >> 32) 
                                    + local_4 + (uVar5 < local_8 ? 1 : 0));
        iVar2 = Utilities.Ratan2(local_18.y, iVar3);
        iVar4 = (iVar2 << 20) >> 20;
        iVar2 = -128;

        if (-129 < iVar4)
        {
            iVar2 = 256;

            if (iVar4 < 257)
                iVar2 = iVar4;
        }

        iVar6 = sVar1 - vr.y;
        iVar4 = iVar6;

        if (iVar6 < 0)
            iVar4 = -iVar6;

        if (11 < iVar4)
        {
            if (iVar6 < 0)
                iVar6 += 3;

            oVar7.vr.y += iVar6 >> 2;
        }

        iVar2 -= oVar7.vr.x;
        iVar4 = iVar2;

        if (iVar2 < 0)
            iVar4 = -iVar2;

        if (11 < iVar4)
        {
            if (iVar2 < 0)
                iVar2 += 3;

            oVar7.vr.x += iVar2 >> 2;
        }

        oVar7.ApplyTransformation();
    }
}
