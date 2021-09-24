using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class Cannonball : VigObject
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
        VigTuple ppiVar2;
        List<VigTuple> ppiVar3;
        sbyte sVar4;
        int iVar5;
        VigObject oVar5;
        int iVar6;
        uint uVar6;
        Ballistic ppcVar7;
        int iVar8;
        VigObject oVar8;
        Vehicle vVar8;
        int iVar11;
        VigObject oVar12;
        int iVar13;
        int iVar14;
        VigObject oVar14;
        Vector3Int local_40;
        Vector3Int local_30;
        Vector3Int local_20;

        sVar4 = tags;

        if (sVar4 == 1)
        {
            if (hit.object2.type != 3)
            {
                ppcVar7 = LevelManager.instance.xobfList[19].ini.FUN_2C17C(140, typeof(Ballistic), 8) as Ballistic;
                ppcVar7.flags = 0x34;
                ppcVar7.screen = screen;
                ppcVar7.FUN_3066C();
                LevelManager.instance.FUN_4DE54(screen, 42);
                iVar6 = GameManager.instance.FUN_1DD9C();
                GameManager.instance.FUN_1E628(iVar6, GameManager.instance.DAT_C2C, 64, vTransform.position);
                oVar5 = hit.self;

                if (oVar5.type == 2)
                {
                    local_40 = new Vector3Int(physics1.Z * 96, physics1.W * 96, physics2.X * 96);
                    oVar5.FUN_2B370(local_40, vTransform.position);
                    oVar5.physics2.Z <<= 4;
                    oVar5.physics1.Y = -0x47800;
                    oVar5.physics2.X <<= 4;

                    if (oVar5.id < 0)
                        GameManager.instance.FUN_15B00(~oVar5.id, 255, 8, 32);
                }

                GameManager.instance.FUN_309A0(this);
                return 0xffffffff;
            }
        }
        else
        {
            if (sVar4 < 2)
            {
                if (sVar4 != 0)
                    return 0;

                uVar6 = FUN_42638(hit, 37, 66);
                return uVar6;
            }

            if (sVar4 != 2)
                return 0;

            iVar5 = 66;

            if (hit.object2.type != 3)
            {
                oVar12 = null;
                oVar8 = hit.self;
                iVar11 = 0x1388000;

                if (oVar8.type == 2)
                {
                    vVar8 = (Vehicle)oVar8;
                    iVar13 = (maxHalfHealth << 11) / vVar8.DAT_A6;
                    iVar14 = physics1.Z * iVar13;
                    maxHalfHealth = (ushort)(maxHalfHealth * 3 >> 2);
                    local_30 = new Vector3Int();

                    if (iVar14 < -0x80000)
                        local_30.x = -0x80000;
                    else
                    {
                        local_30.x = 0x80000;

                        if (iVar14 < 0x80001)
                            local_30.x = iVar14;
                    }

                    iVar14 = physics1.W * iVar13;
                    local_30.y = -0x80000;

                    if (-0x80001 < iVar14)
                    {
                        local_30.y = 0x80000;

                        if (iVar14 < 0x80001)
                            local_30.y = iVar14;
                    }

                    iVar14 = physics2.X * iVar13;
                    local_30.z = -0x80000;

                    if (-0x80001 < iVar14)
                    {
                        local_30.z = 0x80000;

                        if (iVar14 < 0x80001)
                            local_30.z = iVar14;
                    }

                    vVar8.FUN_2B370(local_30, vTransform.position);

                    if (vVar8.id < 0)
                        GameManager.instance.FUN_15B00(~vVar8.id, 255, 2, 128);

                    if (vVar8.shield != 0)
                        iVar5 = -1;
                }

                if (-1 < iVar5)
                {
                    iVar6 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E628(iVar6, GameManager.instance.DAT_C2C, iVar5, vTransform.position);
                }

                id = hit.object2.id;
                iVar13 = DAT_80.id;

                if (iVar13 < 0)
                    sVar4 = GameManager.instance.DAT_1128[~iVar13];
                else
                    sVar4 = -1;

                ppiVar3 = GameManager.instance.worldObjs;

                for (int i = 0; i < ppiVar3.Count; i++)
                {
                    ppiVar2 = ppiVar3[i];
                    oVar14 = ppiVar2.vObject;
                    oVar5 = oVar12;
                    iVar8 = iVar11;

                    if (oVar14.type != 2 || (oVar14.flags & 0x4000) == 0)
                    {
                        oVar12 = oVar5;
                        iVar11 = iVar8;
                    }
                    else if ((iVar13 = oVar14.id) != id &&
                        (0 < iVar13 || sVar4 != GameManager.instance.DAT_1128[~iVar13]))
                    {
                        iVar8 = Utilities.FUN_29F6C(oVar14.vTransform.position, vTransform.position);
                        oVar5 = oVar14;

                        if (iVar8 < iVar11)
                        {
                            oVar12 = oVar5;
                            iVar11 = iVar8;
                        }
                    }
                }

                if (oVar12 != null)
                {
                    local_20 = new Vector3Int(
                        oVar12.screen.x - vTransform.position.x,
                        oVar12.screen.y - vTransform.position.y,
                        oVar12.screen.z - vTransform.position.z);
                    Utilities.FUN_2A098(local_20, out local_20);
                    physics1.Z = local_20.x * 6;
                    physics1.W = local_20.y * 6;
                    physics2.X = local_20.z * 6;
                }
            }
        }

        uVar6 = 0;
        return uVar6;
    }

    //FUN_44AF0
    public override uint UpdateW(int arg1, int arg2)
    {
        int iVar5;
        int iVar6;
        uint uVar6;
        int iVar12;
        Vector3Int v0;
        Vector3Int local_48;

        if (arg1 == 2)
        {
            LevelManager.instance.FUN_4DE54(screen, 39);
            iVar6 = GameManager.instance.FUN_1DD9C();
            GameManager.instance.FUN_1E628(iVar6, GameManager.instance.DAT_C2C, 66, screen);
            LevelManager.instance.FUN_309C8(this, 1);
            uVar6 = 0xffffffff;
        }
        else
        {
            if (arg1 < 3)
            {
                if (arg1 != 0)
                    return 0;

                screen.x += physics1.Z;
                screen.y += physics1.W;
                screen.z += physics2.X;
                vTransform.position = screen;
                physics1.W += 56;
                iVar5 = GameManager.instance.terrain.FUN_1B750((uint)screen.x, (uint)screen.z);

                if (screen.y <= iVar5)
                    return 0;

                if (tags != 2)
                {
                    screen.y = iVar5;
                    LevelManager.instance.FUN_4DE54(screen, 39);
                    iVar6 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E628(iVar6, GameManager.instance.DAT_C2C, 66, screen);
                    LevelManager.instance.FUN_309C8(this, 1);
                    uVar6 = 0xffffffff;
                }
                else
                {
                    v0 = GameManager.instance.terrain.FUN_1B998((uint)screen.x, (uint)screen.z);
                    local_48 = Utilities.VectorNormal(v0);
                    screen.y = iVar5;
                    iVar5 = local_48.x * physics1.Z +
                            local_48.y * physics1.W +
                            local_48.z * physics2.X;

                    if (iVar5 < 0)
                        iVar5 += 2047;

                    iVar5 >>= 11;

                    if (-1 < iVar5)
                        return 0;

                    iVar12 = iVar5 * local_48.x;

                    if (iVar12 < 0)
                        iVar12 += 4095;

                    physics1.Z -= iVar12 >> 12;
                    iVar12 = iVar5 * local_48.y;

                    if (iVar12 < 0)
                        iVar12 += 4095;

                    physics1.W -= iVar12 >> 12;
                    iVar5 *= local_48.z;

                    if (iVar5 < 0)
                        iVar5 += 4095;

                    physics2.X -= iVar5 >> 12;
                }
            }
            else
            {
                if (arg1 != 3)
                    return 0;
            }

            uVar6 = 0;
        }

        return uVar6;
    }
}
