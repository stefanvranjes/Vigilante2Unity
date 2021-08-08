using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_453E0
    public override uint UpdateW(int arg1, int arg2)
    {
        int iVar10;
        uint uVar11;

        switch (arg1)
        {
            case 0:
                iVar10 = FUN_42330(arg2);

                if (iVar10 < 1)
                    return 0;

                goto default;
            default:
                uVar11 = 0;
                break;
        }

        return uVar11;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        long lVar4;
        long lVar5;
        int iVar7;
        VigObject oVar7;
        int iVar10;
        VigTransform tVar10;
        uint uVar11;
        int iVar12;
        VigObject oVar13;
        Vector3Int local_30;
        uint local_20;
        int local_1c;

        switch (arg1)
        {
            case 0:
                if (((Vehicle)arg2).target != null)
                {
                    oVar13 = child2;
                    tVar10 = GameManager.instance.FUN_2CDF4(this);
                    oVar7 = ((Vehicle)arg2).target;
                    local_30 = new Vector3Int(
                        oVar7.screen.x - tVar10.position.x,
                        oVar7.screen.y - tVar10.position.y,
                        oVar7.screen.z - tVar10.position.z);
                    local_30 = Utilities.FUN_2426C(tVar10.rotation, 
                        new Matrix2x4(local_30.x, local_30.y, local_30.z, 0));
                    lVar4 = Utilities.Ratan2(local_30.x, local_30.z);
                    local_20 = (uint)((long)local_30.z * local_30.z);
                    local_1c = (int)((ulong)((long)local_30.z * local_30.z) >> 32);
                    uVar11 = (uint)((long)local_30.x * local_30.x) + local_20;
                    iVar10 = Utilities.FUN_2ABC4(uVar11, (int)((ulong)((long)local_30.x * local_30.x) >>
                                                            32) + local_1c + (uVar11 < local_20 ? 1 : 0));
                    local_20 = (uint)((long)local_30.y * 0x6000);
                    local_1c = (int)((ulong)((long)local_30.y * 0x6000) >> 32);
                    iVar7 = (int)Utilities.Divdi3((int)local_20, local_1c, iVar10, iVar10 >> 31);
                    lVar5 = Utilities.Ratan2(iVar7 - (iVar10 * 0x38) / 0xc000, 0x6000);
                    iVar7 = (int)lVar5 * -0x100000 >> 20;
                    iVar10 = 0x100;

                    if (iVar10 < 0x100)
                        iVar10 = iVar7;

                    iVar7 = -0x80;

                    if (-0x80 < iVar10)
                        iVar7 = iVar10;

                    iVar10 = (((int)lVar4 << 20) >> 20) - oVar13.vr.y;

                    if (iVar10 < 0)
                        iVar10 += 3;

                    oVar13.vr.y += iVar10 >> 2;
                    iVar10 = (short)iVar7 - oVar13.vr.x;

                    if (iVar10 < 0)
                        iVar10 += 3;

                    oVar13.vr.x += iVar10 >> 2;
                    oVar13.ApplyTransformation();
                    return 0;
                }
                goto default;
            default:
                uVar11 = 0;
                break;
            case 1:
                maxHalfHealth = 12;
                flags |= 0x4000;
                goto default;
            case 12:
                iVar12 = 75;

                if (((Vehicle)arg2).doubleDamage != 0)
                    iVar12 = 150;

                FUN_45218(arg2, 175, (ushort)iVar12);
                uVar11 = 60;
                break;
            case 13:
                iVar10 = Utilities.FUN_29F6C(arg2.screen, ((Vehicle)arg2).target.screen);
                uVar11 = 0;

                if (iVar10 < 0x3e8000)
                    uVar11 = (uint)(arg2.physics1.W < 4577 ? 1 : 0);

                break;
        }

        return uVar11;
    }

    private VigObject FUN_45218(VigObject param1, short param2, ushort param3)
    {
        int iVar5;
        VigObject oVar6;
        Ballistic ppcVar2;
        Cannonball ppcVar3;
        Vector3Int auStack32;

        oVar6 = child2;
        ppcVar2 = LevelManager.instance.xobfList[19].ini.FUN_2C17C(3, typeof(Ballistic), 8) as Ballistic;
        ppcVar3 = LevelManager.instance.FUN_42408(param1, oVar6, (ushort)param2, typeof(Cannonball), ppcVar2) as Cannonball;
        ppcVar3.flags = 0x20000094;
        ppcVar3.maxHalfHealth = param3;
        ppcVar3.FUN_305FC();
        ppcVar3.physics2.M2 = 60;
        iVar5 = param1.physics1.X;

        if (iVar5 < 0)
            iVar5 += 127;

        ppcVar3.physics1.Z = (iVar5 >> 7) + ppcVar3.vTransform.rotation.V02 * 6;
        iVar5 = param1.physics1.Y;

        if (iVar5 < 0)
            iVar5 += 127;

        ppcVar3.physics1.W = (iVar5 >> 7) + ppcVar3.vTransform.rotation.V12 * 6;
        iVar5 = param1.physics1.Z;

        if (iVar5 < 0)
            iVar5 += 127;

        ppcVar3.physics2.X = (iVar5 >> 7) + ppcVar3.vTransform.rotation.V22 * 6;
        ppcVar2.FUN_30BF0();
        auStack32 = Utilities.FUN_24094(oVar6.vTransform.rotation, GameManager.DAT_A3C);
        param1.FUN_2B1FC(auStack32, screen);
        //sound
        maxHalfHealth--;

        if (maxHalfHealth == 0)
            FUN_3A368();

        return ppcVar3;
    }
}
