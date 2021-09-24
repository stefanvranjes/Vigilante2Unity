using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineDr : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_47B5C
    public override uint UpdateW(int arg1, int arg2)
    {
        short sVar2;
        int iVar3;
        Vehicle vVar3;
        uint uVar7;
        ushort uVar8;
        VigObject oVar10;

        switch (arg1)
        {
            case 0:
                FUN_42330(arg2);
                uVar7 = 0;
                break;
            default:
                uVar7 = 0;
                break;
            case 10:
                arg2 &= 0xfff;

                if (arg2 == 0x132)
                {
                    if (maxHalfHealth < 2)
                        return 0xffffffff;

                    vVar3 = Utilities.FUN_2CD78(this) as Vehicle;
                    oVar10 = FUN_479DC(vVar3, 195, typeof(Brearhug));
                    iVar3 = Utilities.FUN_29E84(new Vector3Int(
                        oVar10.vCollider.reader.ReadInt32(16), 
                        oVar10.vCollider.reader.ReadInt32(20), 
                        oVar10.vCollider.reader.ReadInt32(24)));
                    oVar10.DAT_58 = iVar3;
                    oVar10.type = 3;
                    oVar10.flags |= 0x40000000;
                    sVar2 = (short)(maxHalfHealth - 2);
                }
                else
                {
                    if (arg2 != 0x134)
                    {
                        if (arg2 != 0x133)
                            return 0;

                        if (maxHalfHealth < 2)
                            return 0xffffffff;

                        vVar3 = Utilities.FUN_2CD78(this) as Vehicle;
                        oVar10 = FUN_479DC(vVar3, 214, typeof(Hovermine));
                        oVar10.child2.transform.parent = oVar10.transform;
                        iVar3 = Utilities.FUN_29E84(new Vector3Int(
                            oVar10.vCollider.reader.ReadInt32(16),
                            oVar10.vCollider.reader.ReadInt32(20),
                            oVar10.vCollider.reader.ReadInt32(24)));
                        oVar10.DAT_58 = iVar3;
                        oVar10.flags |= 0x40000000;
                        sVar2 = (short)(maxHalfHealth - 2);
                    }
                    else
                    {
                        if (maxHalfHealth < 2)
                            return 0xffffffff;

                        vVar3 = Utilities.FUN_2CD78(this) as Vehicle;
                        oVar10 = FUN_479DC(vVar3, 191, typeof(CactusPatch));
                        iVar3 = Utilities.FUN_29E84(new Vector3Int(
                            oVar10.vCollider.reader.ReadInt32(16),
                            oVar10.vCollider.reader.ReadInt32(20),
                            oVar10.vCollider.reader.ReadInt32(24)));
                        oVar10.DAT_58 = iVar3;
                        oVar10.type = 3;
                        uVar8 = 6;

                        if (maxHalfHealth < 6)
                            uVar8 = maxHalfHealth;

                        oVar10.maxHalfHealth = uVar8;
                        sVar2 = (short)(maxHalfHealth - uVar8);
                    }
                }

                maxHalfHealth = (ushort)sVar2;
                uVar7 = 120;

                if (sVar2 == 0)
                {
                    FUN_3A368();
                    uVar7 = 120;
                }

                break;
        }

        return uVar7;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        short sVar2;
        uint uVar4;
        long lVar5;
        int iVar6;
        uint uVar7;
        int iVar9;
        int iVar10;
        VigObject oVar10;
        Vector3Int local_18;
        Vector3Int local_8;

        switch (arg1)
        {
            case 0:
                FUN_42330(arg2);
                uVar7 = 0;
                break;
            case 1:
                maxHalfHealth = 6;
                goto default;
            default:
                uVar7 = 0;
                break;
            case 12:
                FUN_479DC((Vehicle)arg2, 182, typeof(Mine));
                maxHalfHealth--;
                uVar7 = 60;

                if (maxHalfHealth == 0)
                {
                    FUN_3A368();
                    uVar7 = 60;
                }

                break;
            case 13:
                uVar4 = GameManager.FUN_2AC5C();

                if ((uVar4 & 0x3ff) == 0 ||
                    ((uVar4 & 0xff) == 0 &&
                    (sbyte)LevelManager.instance.FUN_35778(arg2.vTransform.position.x, arg2.vTransform.position.z) == -128))
                    uVar7 = 1;
                else
                {
                    uVar7 = 0;

                    if ((uVar4 & 15) == 0)
                    {
                        oVar10 = ((Vehicle)arg2).target;
                        local_18 = Utilities.FUN_24304(arg2.vTransform, oVar10.vTransform.position);

                        if (local_18.z < 0 && -0x96000 < local_18.z)
                        {
                            lVar5 = Utilities.Ratan2(local_18.x, local_18.z);
                            iVar6 = (int)(lVar5 << 20) >> 20;

                            if (iVar6 < 0)
                                iVar6 = -iVar6;

                            if (0x6aa < iVar6)
                            {
                                Utilities.FUN_2A168(out local_8, oVar10.vTransform.position, arg2.vTransform.position);
                                iVar6 = oVar10.physics1.X;

                                if (iVar6 < 0)
                                    iVar6 += 127;

                                iVar9 = oVar10.physics1.Y;

                                if (iVar9 < 0)
                                    iVar9 += 127;

                                iVar10 = oVar10.physics1.Z;

                                if (iVar10 < 0)
                                    iVar10 += 127;

                                iVar10 = (iVar6 >> 7) * local_8.x + (iVar9 >> 7) * local_8.y +
                                         (iVar10 >> 7) * local_8.z;

                                if (iVar10 < 0)
                                    iVar10 += 4095;

                                uVar7 = (uint)(-local_18.z < (iVar10 >> 12) * 60 ? 1 : 0);
                            }
                        }
                    }
                }

                break;
        }

        return uVar7;
    }

    private VigObject FUN_479DC(Vehicle param1, short param2, Type param3)
    {
        VigObject puVar1;
        int iVar2;
        ushort uVar3;
        int iVar4;
        uint uVar4;
        int iVar5;

        puVar1 = LevelManager.instance.FUN_42408(param1, this, (ushort)param2, param3, null);
        uVar4 = 0x20000080;

        if (puVar1.vAnim != null)
            uVar4 = 0x20000084;

        puVar1.flags = uVar4;
        uVar3 = 150;

        if (param1.doubleDamage != 0)
            uVar3 = 300;

        puVar1.maxHalfHealth = uVar3;
        puVar1.FUN_305FC();
        puVar1.FUN_2D1DC();
        puVar1.physics2.M2 = 0;
        iVar5 = param1.physics1.X;

        if (iVar5 < 0)
            iVar5 += 127;

        iVar2 = puVar1.vTransform.rotation.V01;

        if (iVar2 < 0)
            iVar2 += 3;

        puVar1.physics1.Z = (iVar5 >> 7) - (iVar2 >> 2);
        iVar5 = param1.physics1.Y;

        if (iVar5 < 0)
            iVar5 += 127;

        iVar2 = puVar1.vTransform.rotation.V11;

        if (iVar2 < 0)
            iVar2 += 3;

        puVar1.physics1.W = (iVar5 >> 7) - (iVar2 >> 2);
        iVar5 = param1.physics1.Z;

        if (iVar5 < 0)
            iVar5 += 127;

        iVar2 = puVar1.vTransform.rotation.V21;

        if (iVar2 < 0)
            iVar2 += 3;

        puVar1.physics2.X = (iVar5 >> 7) - (iVar2 >> 2);
        param1.FUN_2B1FC(GameManager.DAT_A68, screen);
        iVar4 = GameManager.instance.FUN_1DD9C();
        GameManager.instance.FUN_1E580(iVar4, GameManager.instance.DAT_C2C, 54, puVar1.screen);
        return puVar1;
    }
}
