using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tantrum : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //0x20 (WUNDER.DLL)
    private static Vector3Int DAT_20 = new Vector3Int(0, 0, -0x8000);

    //FUN_2FC (WUNDER.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        uint uVar2;
        int iVar6;
        uint uVar7;

        switch (arg1)
        {
            case 0:
                iVar6 = FUN_42330(arg2);
                uVar7 = 0;
                break;
            default:
                uVar7 = 0;
                break;
            case 11:
                vr.x = 0;
                uVar2 = flags & 0xfdffffff;
                flags = uVar2;
                uVar7 = 0;
                break;
        }

        return uVar7;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        short sVar1;
        uint uVar2;
        Ballistic puVar3;
        Tantrum2 puVar4;
        Throwaway puVar5;
        int iVar6;
        VigTransform tVar6;
        VigObject oVar6;
        int iVar7;
        uint uVar7;
        ConfigContainer ccVar7;
        int iVar8;
        VigObject oVar8;
        int iVar9;
        VigObject oVar10;
        Vector3Int local_30;
        Vector3Int auStack32;

        switch (arg1)
        {
            case 0:
                iVar6 = FUN_42330(arg2);
                uVar7 = 0;

                if (0 < iVar6 && ((Vehicle)arg2).target != null)
                {
                    oVar10 = child2;
                    tVar6 = GameManager.instance.FUN_2CDF4(this);
                    oVar8 = ((Vehicle)arg2).target;
                    local_30 = new Vector3Int();
                    local_30.x = oVar8.screen.x - tVar6.position.x;
                    local_30.y = oVar8.screen.y - tVar6.position.y;
                    local_30.z = oVar8.screen.z - tVar6.position.z;
                    local_30 = Utilities.FUN_2426C(tVar6.rotation, new Matrix2x4
                        (local_30.x, local_30.y, local_30.z, 0));
                    iVar8 = Utilities.Ratan2(local_30.x, local_30.z);
                    iVar6 = Utilities.Ratan2(local_30.y, local_30.z);
                    iVar9 = iVar6 * -0x100000 >> 20;
                    iVar6 = 0x100;

                    if (iVar9 < 0x100)
                        iVar6 = iVar9;

                    iVar9 = -128;

                    if (-128 < iVar6)
                        iVar9 = iVar6;

                    oVar10.vr.y += (((iVar8 << 20) >> 20) - oVar10.vr.y) / 2;
                    oVar10.vr.x += (iVar9 - oVar10.vr.x) / 2;
                    oVar10.ApplyTransformation();
                    uVar7 = 0;

                    if (id != 0)
                    {
                        oVar10.child2.vr.z += 42;
                        oVar10.child2.ApplyTransformation();
                        uVar7 = 0;
                    }
                }

                break;
            case 1:
                maxHalfHealth = 50;
                uVar2 = flags | 0x4000;
                flags = uVar2;
                uVar7 = 0;
                break;
            default:
                uVar7 = 0;
                break;
            case 11:
                vr.x = 0;
                uVar2 = flags & 0xfdffffff;
                flags = uVar2;
                uVar7 = 0;
                break;
            case 12:
                oVar6 = child2;
                puVar3 = LevelManager.instance.xobfList[19].ini.FUN_2C17C(9, typeof(Ballistic), 8) as Ballistic;
                Utilities.ParentChildren(puVar3, puVar3);
                puVar4 = LevelManager.instance.FUN_42408(arg2, oVar6, 2, typeof(Tantrum2), puVar3) as Tantrum2;
                puVar5 = vData.ini.FUN_2C17C(1, typeof(Throwaway), 0) as Throwaway;
                uVar7 = 0x280;

                if (vr.x == 0)
                    uVar7 = 0x60000280;

                puVar4.maxHalfHealth = 20;
                puVar4.flags = uVar7;
                puVar4.FUN_305FC();
                iVar8 = arg2.physics1.X;

                if (iVar8 < 0)
                    iVar8 += 127;

                puVar4.physics1.Z = (iVar8 >> 7) + puVar4.vTransform.rotation.V02 * 6;
                iVar8 = arg2.physics1.Y;

                if (iVar8 < 0)
                    iVar8 += 127;

                puVar4.physics1.W = (iVar8 >> 7) + puVar4.vTransform.rotation.V12 * 6;
                iVar8 = arg2.physics1.Z;

                if (iVar8 < 0)
                    iVar8 += 127;

                puVar4.physics2.X = (iVar8 >> 7) + puVar4.vTransform.rotation.V22 * 6;
                puVar4.physics2.M2 = 180;

                if ((arg2.flags & 4) == 0)
                    puVar3.FUN_30BF0();

                ccVar7 = oVar6.FUN_2C5F4(0x8001);
                puVar5.vTransform = GameManager.instance.FUN_2CEAC(oVar6, ccVar7);
                puVar5.type = 4;
                puVar5.flags = 0xa0;
                iVar8 = puVar5.vTransform.rotation.V02;
                puVar5.state = _THROWAWAY_TYPE.Unspawnable;
                puVar5.vCollider = null;

                if (iVar8 < 0)
                    iVar8 += 3;

                iVar9 = puVar5.vTransform.rotation.V12;
                puVar5.physics1.Z = iVar8 >> 2;

                if (iVar9 < 0)
                    iVar9 += 3;

                iVar8 = puVar5.vTransform.rotation.V22;
                puVar5.physics1.W = iVar9 >> 2;

                if (iVar8 < 0)
                    iVar8 += 3;

                puVar5.physics2.X = iVar8 >> 2;
                puVar5.DAT_87 = 2;
                puVar5.FUN_305FC();
                auStack32 = Utilities.FUN_24094(oVar6.vTransform.rotation, DAT_20);
                arg2.FUN_2B1FC(auStack32, screen);

                if ((flags & 0x2000000) == 0)
                {
                    flags |= 0x2000000;
                    iVar7 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E188(iVar7, vData.sndList, 2);
                }

                iVar7 = GameManager.instance.FUN_1DD9C();
                GameManager.instance.FUN_1E628(iVar7, vData.sndList, 3, puVar4.screen);
                sVar1 = (short)(maxHalfHealth - 1);
                maxHalfHealth = (ushort)sVar1;

                if (sVar1 == 0)
                    FUN_3A368();

                if (arg2.id < 0)
                {
                    uVar7 = 12;
                    flags |= 0x4000000;
                    goto LAB_794;
                }

                sVar1 = (short)(vr.x + 1);
                vr.x = sVar1;

                if (sVar1 < 8)
                {
                    uVar7 = 12;
                    flags |= 0x4000000;
                }
                else
                {
                    uVar7 = 180;
                    flags &= 0xfbffffff;
                }

                LAB_794:
                break;
            case 13:
                uVar7 = 0;

                if (vr.x < 8)
                {
                    if (vr.x == 0)
                    {
                        if (GameManager.instance.DAT_1084 != 0)
                            return 0;

                        iVar6 = Utilities.FUN_29F6C(arg2.screen, ((Vehicle)arg2).target.screen);

                        if (0x31fff < iVar6)
                            return 0;

                        if (3050 < arg2.physics1.W)
                            return 0;
                    }

                    uVar7 = 1;
                }

                break;
        }

        return uVar7;
    }
}
