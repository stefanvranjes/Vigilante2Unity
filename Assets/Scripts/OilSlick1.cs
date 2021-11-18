using System;
using System.Collections.Generic;
using UnityEngine;

public class OilSlick1 : VigObject
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
        int iVar5;
        int iVar8;

        GameManager.instance.FUN_2F798(this, hit);
        iVar5 = physics1.X * hit.normal1.x +
                physics1.Y * hit.normal1.y +
                physics1.Z * hit.normal1.z;

        if (iVar5 < 0)
            iVar5 += 2047;

        iVar5 >>= 11;

        if (-1 < iVar5)
            return 0;

        iVar8 = iVar5 * hit.normal1.x;

        if (iVar8 < 0)
            iVar8 += 4095;

        physics1.X -= iVar8 >> 12;
        iVar8 = iVar5 * hit.normal1.y;

        if (iVar8 < 0)
            iVar8 += 4095;

        physics1.Y -= iVar8 >> 12;
        iVar5 = iVar5 * hit.normal1.z;

        if (iVar5 < 0)
            iVar5 += 4095;

        physics1.Z -= iVar5 >> 12;
        return 0;
    }

    //FUN_48AF4
    public override uint UpdateW(int arg1, int arg2)
    {
        short sVar1;
        VigObject ppcVar2;
        int iVar3;
        uint uVar4;
        int iVar5;
        int iVar6;
        int iVar7;
        int iVar8;
        Vector3Int local_20;
        Vector3Int local_10;

        if (arg1 == 1)
        {
            iVar5 = DAT_58 * 0x3243;
            physics2.Z = DAT_58;

            if (iVar5 < 0)
                iVar5 += 4095;

            physics1.M6 = 0;
            physics2.M2 = (short)(0x1000000 / (iVar5 >> 12));
            physics1.M7 = 0;
            physics2.M0 = 0;
        }
        else
        {
            if (arg1 == 0)
            {
                local_20 = new Vector3Int(
                    vTransform.position.x,
                    vTransform.position.y + physics2.Z,
                    vTransform.position.z);
                local_10 = new Vector3Int();
                iVar5 = FUN_2CFBC(local_20, ref local_10);

                if (iVar5 < local_20.y + 0x800)
                {
                    if (local_10.y < -0xf33)
                    {
                        ppcVar2 = LevelManager.instance.xobfList[19].ini.FUN_2C17C(218, typeof(OilSlick2), 8, typeof(VigChild));
                        Utilities.ParentChildren(ppcVar2, ppcVar2);
                        FUN_2D114(vTransform.position, ref ppcVar2.vTransform);
                        iVar3 = GameManager.instance.FUN_1DD9C();
                        GameManager.instance.FUN_1E14C(iVar3, GameManager.instance.DAT_C2C, 61);
                        ppcVar2.flags = 0x4;
                        sVar1 = id;
                        ppcVar2.type = 3;
                        ppcVar2.id = sVar1;
                        ppcVar2.maxHalfHealth = 100;
                        ppcVar2.tags = 10;
                        ppcVar2.FUN_2D1DC();
                        ppcVar2.FUN_305FC();
                        GameManager.instance.FUN_309A0(this);
                        return 0xffffffff;
                    }

                    iVar6 = local_10.x;
                    iVar7 = physics1.X;
                    iVar8 = iVar7 * iVar6 + physics1.Y * local_10.y +
                            physics1.Z * local_10.z;

                    if (iVar8 < 0)
                        iVar8 += 2047;

                    iVar8 >>= 11;

                    if (iVar8 < 0)
                    {
                        iVar6 = iVar8 * iVar6;

                        if (iVar6 < 0)
                            iVar6 += 4095;

                        physics1.X = iVar7 - (iVar6 >> 12);
                        iVar6 = iVar8 * local_10.y;

                        if (iVar6 < 0)
                            iVar6 += 4095;

                        physics1.Y = (physics1.Y - (iVar6 >> 12)) / 2;
                        iVar8 = iVar8 * local_10.z;

                        if (iVar8 < 0)
                            iVar8 += 4095;

                        physics1.Z -= iVar8 >> 12;
                        vTransform.position.y = iVar5 - physics2.Z;
                    }
                    else
                    {
                        iVar6 *= 90;

                        if (iVar6 < 0)
                            iVar6 += 4095;

                        physics1.X = iVar7 + (iVar6 >> 12);
                        iVar5 = local_10.z * 90;

                        if (iVar5 < 0)
                            iVar5 += 4095;

                        physics1.Z += iVar5 >> 12;
                    }

                    iVar5 = -physics1.Z * (ushort)physics2.M2;

                    if (iVar5 < 0)
                        iVar5 += 4095;

                    iVar8 = physics1.X * (ushort)physics2.M2;
                    physics1.M6 = (short)(iVar5 >> 12);

                    if (iVar8 < 0)
                        iVar8 += 4095;

                    physics2.M0 = (short)(iVar5 >> 12);
                }
                else
                    physics1.Y += 90;

                vTransform.rotation = Utilities.FUN_2ADB0(vTransform.rotation, 
                    new Vector3Int(physics1.M6, physics1.M7, physics2.M0));
                vTransform.position.y += physics1.Y;
                vTransform.position.x += physics1.X;
                uVar4 = (uint)GameManager.instance.DAT_28 - DAT_19;
                vTransform.position.z += physics1.Z;

                if ((uVar4 & 15) == 0)
                {
                    vTransform.rotation = Utilities.MatrixNormal(vTransform.rotation);
                    return 0;
                }
            }
        }

        return 0;
    }
}
