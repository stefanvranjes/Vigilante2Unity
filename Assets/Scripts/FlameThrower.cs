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
        int iVar16;

        switch (arg1)
        {
            case 0:
                iVar16 = FUN_42330(arg2);

                if (iVar16 < 1)
                    return 0;

                if (id != 0)
                    return 0;

                break;
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
            case 1:
                maxHalfHealth = 20;
                return 0;
            case 12:
                if (DAT_18 == 0)
                {
                    //sound
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
