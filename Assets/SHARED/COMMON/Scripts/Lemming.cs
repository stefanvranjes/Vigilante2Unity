using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lemming : VigObject
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
        Ballistic puVar7;
        uint uVar8;
        int iVar15;

        GameManager.instance.FUN_2F798(this, hit);

        if (hit.self.type == 2)
        {
            puVar7 = vData.ini.FUN_2C17C(2, typeof(Ballistic), 8) as Ballistic;
            puVar7.flags = 0x24;
            puVar7.screen = screen;
            puVar7.vTransform.rotation = Utilities.FUN_2A5EC(hit.normal1);
            iVar15 = GameManager.instance.FUN_1DD9C();
            GameManager.instance.FUN_1E580(iVar15, GameManager.instance.DAT_C2C, 65, puVar7.screen);
            puVar7.FUN_305FC();
            tags = 1;
            uVar8 = flags | 0x22;
            flags = uVar8;
            GameManager.instance.FUN_30CB0(this, 75);
            return 0;
        }

        if (hit.self.type == 3)
            return 0;

        flags |= 0x100;
        return 0;
    }

    //FUN_258 (ELGUERRO.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        short sVar1;
        ushort uVar2;
        long lVar3;
        VigObject oVar4;
        Lemming3 puVar5;
        int iVar6;
        VigObject oVar6;
        VigTransform tVar7;
        Lemming2 puVar7;
        uint uVar8;
        VigObject piVar9;
        VigTuple ppiVar11;
        List<VigTuple> ppiVar12;
        uint uVar13;
        int iVar14;
        ConfigContainer ccVar14;
        Vehicle vVar14;
        int iVar15;
        VigObject oVar15;
        int iVar16;
        VigObject oVar16;
        int iVar20;
        int iVar21;
        Vector3Int local_20;
        Vector3Int auStack16;

        switch (arg1)
        {
            case 0:
                if (tags == 0)
                {
                    sVar1 = physics2.M3;

                    if (physics2.M3 != 0)
                    {
                        physics2.M3 = (short)(sVar1 - 1);

                        if (sVar1 != 1)
                            return 0;

                        oVar15 = Utilities.FUN_2CD78(this);
                        oVar6 = Utilities.FUN_2CD78(oVar15);
                        vVar14 = Utilities.FUN_2CD78(oVar6) as Vehicle;

                        if (vVar14 != null)
                        {
                            oVar4 = vData.ini.FUN_2C17C(1, typeof(VigObject), 8);
                            tVar7 = GameManager.instance.FUN_2CDF4(this);
                            vTransform = tVar7;
                            screen = vTransform.position;
                            id = vVar14.id;
                            oVar16 = vVar14.target;

                            if (vVar14.target == null)
                                oVar16 = vVar14;

                            DAT_84 = oVar16;
                            iVar16 = Utilities.FUN_29F6C(vTransform.position, oVar16.vTransform.position);

                            if (iVar16 < 0)
                                iVar16 += 255;

                            iVar16 >>= 8;
                            iVar21 = 0x1000;

                            if (4095 < iVar16)
                            {
                                iVar21 = 0x2000;

                                if (iVar16 < 0x2001)
                                    iVar21 = iVar16;
                            }

                            iVar16 = vVar14.physics1.X;

                            if (iVar16 < 0)
                                iVar16 += 127;

                            iVar20 = vTransform.rotation.V02 * iVar21;

                            if (iVar20 < 0)
                                iVar20 += 4095;

                            physics1.Z = (iVar16 >> 7) + (iVar20 >> 12);
                            iVar16 = vVar14.physics1.Y;

                            if (iVar16 < 0)
                                iVar16 += 127;

                            iVar20 = vTransform.rotation.V12 * iVar21;

                            if (iVar20 < 0)
                                iVar20 += 4095;

                            physics1.W = (iVar16 >> 7) + (iVar20 >> 12);
                            iVar14 = vVar14.physics1.Z;

                            if (iVar14 < 0)
                                iVar14 += 127;

                            iVar21 = vTransform.rotation.V22 * iVar21;

                            if (iVar21 < 0)
                                iVar21 += 4095;

                            physics2.X = (iVar14 >> 7) + (iVar21 >> 12);
                            FUN_2CCBC();
                            transform.parent = null;
                            oVar4.vTransform = GameManager.FUN_2A39C();
                            ccVar14 = FUN_2C5F4(0x8000);
                            oVar4.vTransform.position = ccVar14.v3_1;
                            Utilities.FUN_2CC9C(this, oVar4);
                            Utilities.ParentChildren(this, this);
                            FUN_30BF0();
                            GameManager.instance.FUN_30080(GameManager.instance.worldObjs, this);

                            if (oVar6.maxHalfHealth == 0)
                                oVar6.FUN_3A368();

                            iVar15 = GameManager.instance.FUN_1DD9C();
                            GameManager.instance.FUN_1E580(iVar15, oVar6.vData.sndList, 3, vTransform.position);
                            return 0;
                        }

                        oVar15 = FUN_2CCBC();
                        GameManager.instance.FUN_308C4(oVar15);
                        return 0xffffffff;
                    }

                    screen.x += physics1.Z;
                    screen.y += physics1.W;
                    screen.z += physics2.X;
                    vTransform.position = screen;
                    oVar6 = DAT_84;
                    physics1.W += 42;
                    local_20 = new Vector3Int();
                    local_20.x = oVar6.vTransform.position.x - screen.x;
                    local_20.y = oVar6.vTransform.position.y - screen.y;
                    local_20.z = oVar6.vTransform.position.z - screen.z;
                    lVar3 = (long)physics1.W * physics1.W;
                    uVar8 = (uint)local_20.y * 112;
                    uVar13 = (uint)lVar3 + uVar8;
                    iVar6 = (int)((ulong)lVar3 >> 32) + ((int)uVar8 >> 31) + (uVar13 < uVar8 ? 1 : 0);

                    if (-1 < iVar6)
                    {
                        iVar14 = Utilities.FUN_2ABC4(uVar13, iVar6);
                        iVar14 -= physics1.W;
                        iVar6 = iVar14;

                        if (iVar14 < 0)
                            iVar6 = -iVar14;

                        if (0x100 < iVar6)
                        {
                            iVar6 = (local_20.x * 56) / iVar14 - physics1.Z;

                            if (iVar6 < 0)
                                iVar6 += 15;

                            physics1.Z += iVar6 >> 4;
                            iVar6 = (local_20.z * 56) / iVar14 - physics2.X;

                            if (iVar6 < 0)
                                iVar6 += 15;

                            physics2.X += iVar6 >> 4;
                        }
                    }

                    local_20 = new Vector3Int(physics1.Z, physics1.W, physics2.X);
                    Utilities.FUN_29FC8(local_20, out auStack16);
                    vTransform.rotation = Utilities.FUN_2A724(auStack16);
                    iVar6 = GameManager.instance.terrain.FUN_1B750((uint)vTransform.position.x, (uint)vTransform.position.z);

                    if (vTransform.position.y <= iVar6)
                    {
                        if (physics1.W < 1)
                        {
                            uVar2 = (ushort)physics2.M2;
                            physics2.M2 = (short)(uVar2 + 1);

                            if ((uVar2 & 15) == 0)
                            {
                                ppiVar12 = GameManager.instance.worldObjs;

                                for (int i = 0; i < ppiVar12.Count; i++)
                                {
                                    ppiVar11 = ppiVar12[i];
                                    piVar9 = ppiVar11.vObject;

                                    if (piVar9.type == 2 && (piVar9.flags & 0x4000000U) == 0 && piVar9.maxHalfHealth != 0)
                                    {
                                        piVar9.flags |= 0x8000000;
                                        piVar9 = ppiVar11.vObject;
                                        piVar9.screen = screen;
                                    }
                                }
                            }
                        }
                        else
                        {
                            ppiVar12 = GameManager.instance.worldObjs;

                            for (int i = 0; i < ppiVar12.Count; i++)
                            {
                                ppiVar11 = ppiVar12[i];
                                piVar9 = ppiVar11.vObject;

                                if (piVar9.type == 2 && (piVar9.flags & 0x4000000U) == 0 && piVar9.maxHalfHealth != 0)
                                {
                                    piVar9.flags |= 0x8000000;
                                    piVar9 = ppiVar11.vObject;
                                    piVar9.screen = DAT_84.vTransform.position;
                                }
                            }
                        }

                        if ((physics2.M2 & 7) != 0)
                            return 0;

                        puVar7 = LevelManager.instance.xobfList[19].ini.FUN_2C17C(7, typeof(Lemming2), 8) as Lemming2;
                        Utilities.ParentChildren(puVar7, puVar7);
                        puVar7.type = 4;
                        puVar7.flags = 0x4B4;
                        puVar7.screen = screen;
                        puVar7.vr.z = physics2.M2 * 96;
                        iVar6 = vTransform.rotation.V02 * -341;

                        if (iVar6 < 0)
                            iVar6 += 4095;

                        puVar7.physics1.Z = iVar6 >> 12;
                        iVar6 = vTransform.rotation.V12 * -341;

                        if (iVar6 < 0)
                            iVar6 += 4095;

                        puVar7.physics1.W = iVar6 >> 12;
                        iVar6 = vTransform.rotation.V22 * -341;

                        if (iVar6 < 0)
                            iVar6 += 4095;

                        puVar7.physics2.X = iVar6 >> 12;
                        puVar7.FUN_3066C();
                        return 0;
                    }

                    puVar5 = vData.ini.FUN_2C17C(2, typeof(Lemming3), 8, typeof(VigChild)) as Lemming3;
                    Utilities.ParentChildren(puVar5, puVar5);
                    puVar5.type = 8;
                    puVar5.flags = 0x60000004;
                    puVar5.id = id;
                    puVar5.screen = screen;
                    puVar5.maxHalfHealth = 4;
                    puVar5.DAT_80 = DAT_80;
                    puVar5.FUN_2D114(puVar5.screen, ref puVar5.vTransform);
                    //FUN_4E414
                    iVar15 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E580(iVar15, GameManager.instance.DAT_C2C, 65, puVar5.screen);
                    puVar5.FUN_305FC();
                    tags = 1;
                    uVar8 = flags | 2;
                    flags = uVar8;
                    GameManager.instance.FUN_30CB0(this, 75);
                    return 0;
                }

                if (tags != 1)
                    return 0;

                ppiVar12 = GameManager.instance.worldObjs;
                
                for (int i = 0; i < ppiVar12.Count; i++)
                {
                    ppiVar11 = ppiVar12[i];
                    piVar9 = ppiVar11.vObject;

                    if (piVar9.type == 2 && (piVar9.flags & 0x4000000U) == 0 && piVar9.maxHalfHealth != 0)
                    {
                        piVar9.flags |= 0x8000000;
                        piVar9 = ppiVar11.vObject;
                        piVar9.screen = DAT_84.vTransform.position;
                    }
                }

                break;
            case 2:
                GameManager.instance.FUN_309A0(this);
                GameManager.instance.DAT_1084--;
                return 0xffffffff;
            case 4:
                ppiVar12 = GameManager.instance.worldObjs;

                for (int i = 0; i < ppiVar12.Count; i++)
                {
                    ppiVar11 = ppiVar12[i];
                    piVar9 = ppiVar11.vObject;

                    if (piVar9.type == 2 && (piVar9.flags & 0x4000000U) == 0 && piVar9.maxHalfHealth != 0)
                        piVar9.flags &= 0xf7ffffff;
                }

                break;
        }

        return 0;
    }
}
