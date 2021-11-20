using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Donut : VigObject
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
        int iVar3;
        VigObject oVar3;
        Vehicle vVar3;
        int iVar4;
        int iVar5;
        uint uVar6;
        int iVar8;
        Vector3Int local_58;
        HitDetection auStack56;
        Vector3Int auStack72;

        oVar3 = hit.self;

        if (oVar3.type == 8)
        {
            FUN_32B90(oVar3.maxHalfHealth);
            return 0;
        }

        if (oVar3.type == 2)
        {
            vVar3 = (Vehicle)oVar3;
            GameManager.instance.FUN_2F798(this, hit);
            auStack72 = Utilities.FUN_24148(vTransform, hit.position);
            local_58 = new Vector3Int();
            local_58.x = (vVar3.vTransform.position.x - vTransform.position.x) * 8;
            local_58.y = (vVar3.vTransform.position.y - vTransform.position.y) * 8;
            local_58.z = (vVar3.vTransform.position.z - vTransform.position.z) * 8;
            vVar3.FUN_2B370(local_58, auStack72);
            iVar5 = GameManager.instance.FUN_1DD9C();
            uVar6 = GameManager.FUN_2AC5C();
            iVar8 = 25;

            if ((uVar6 & 1) != 0)
                iVar8 = 24;

            GameManager.instance.FUN_1E5D4(iVar5, GameManager.instance.DAT_C2C, iVar8, auStack72);
            UIManager.instance.FUN_4E414(auStack72, new Color32(0x80, 0x80, 0x80, 8));
            LevelManager.instance.FUN_4DE54(auStack72, 142);
            vVar3.FUN_3A064(-3, auStack72, true);
            return 0;
        }

        auStack56 = new HitDetection(null);
        GameManager.instance.FUN_2FB70(oVar3, hit, auStack56);
        iVar3 = physics1.X * auStack56.normal1.x + physics1.Y * auStack56.normal1.y +
                physics1.Z * auStack56.normal1.z;

        if (0 < iVar3)
        {
            iVar4 = (iVar3 >> 12) * 2;
            iVar3 = iVar4 * auStack56.normal1.x;

            if (iVar3 < 0)
                iVar3 += 4095;

            physics1.X -= iVar3 >> 12;
            iVar3 = iVar4 * auStack56.normal1.y;

            if (iVar3 < 0)
                iVar3 += 4095;

            physics1.Y -= iVar3 >> 12;
            iVar4 = iVar4 * auStack56.normal1.z;

            if (iVar4 < 0)
                iVar4 += 4095;

            physics1.Z -= iVar4 >> 12;
        }

        return 0;
    }

    //FUN_4864 (ROUTE66.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        sbyte sVar1;
        int iVar2;
        int iVar3;
        int iVar4;
        int iVar7;
        int iVar9;
        Vector3Int local_78;
        Vector3Int local_68;

        if (arg1 < 4)
        {
            if (arg1 != 0)
                return 0;

            local_78 = new Vector3Int();
            local_78.x = vTransform.position.x;
            local_78.y = vTransform.position.y + DAT_58;
            local_78.z = vTransform.position.z;
            local_68 = new Vector3Int();
            iVar3 = FUN_2CFBC(local_78, ref local_68);

            if (iVar3 < local_78.y + 0x800)
            {
                iVar7 = local_68.x;
                iVar9 = physics1.X;
                iVar4 = iVar9 * iVar7 + physics1.Y * local_68.y +
                        physics1.Z * local_68.z;

                if (iVar4 < 0)
                    iVar4 += 2047;

                iVar4 >>= 11;

                if (iVar4 < 0)
                {
                    iVar7 = iVar4 * iVar7;

                    if (iVar7 < 0)
                        iVar7 += 4095;

                    physics1.X = iVar9 - (iVar7 >> 12);
                    iVar7 = iVar4 * local_68.y;

                    if (iVar7 < 0)
                        iVar7 += 4095;

                    physics1.Y = (physics1.Y - (iVar7 >> 12)) / 2;
                    iVar4 *= local_68.z;

                    if (iVar4 < 0)
                        iVar4 += 4095;

                    physics1.Z -= iVar4 >> 12;
                    vTransform.position.y = iVar3 - DAT_58;
                }
                else
                {
                    iVar7 *= 90;

                    if (iVar7 < 0)
                        iVar7 += 4095;

                    physics1.X = iVar9 + (iVar7 >> 12);
                    iVar3 = local_68.z * 90;

                    if (iVar3 < 0)
                        iVar3 += 4095;

                    physics1.Z += iVar3 >> 12;
                }

                iVar2 = Utilities.Ratan2(physics1.X, physics1.Z);
                vr.y = iVar2;
                iVar3 = (int)Utilities.SquareRoot(physics1.X * physics1.X +
                                                  physics1.Z * physics1.Z);

                if (physics2.Y == 0)
                    physics1.M6 = (short)((iVar3 << 12) / -1); //not in the original code
                else
                    physics1.M6 = (short)((iVar3 << 12) / physics2.Y);

                if (iVar3 < 457)
                {
                    FUN_4DC94();
                    return 0xffffffff;
                }
            }
            else
                physics1.Y += 90;

            vr.x -= physics1.M6;
            vTransform.position.x += physics1.X;
            vTransform.position.y += physics1.Y;
            vTransform.position.z += physics1.Z;

            if (arg2 != 0)
            {
                iVar3 = (int)GameManager.instance.FUN_1E7A8(vTransform.position);

                if (iVar3 == 0)
                {
                    GameManager.instance.FUN_1DE78(DAT_18);
                    DAT_18 = 0;
                }
                else
                {
                    if (DAT_18 == 0)
                    {
                        sVar1 = (sbyte)GameManager.instance.FUN_1DD9C();
                        DAT_18 = sVar1;
                        GameManager.instance.FUN_1E098(sVar1, LevelManager.instance.xobfList[42].sndList, 7, (uint)iVar3, true);
                    }
                    else
                        GameManager.instance.FUN_1E2C8(DAT_18, (uint)iVar3);
                }

                ApplyRotationMatrix();
                return 0;
            }
        }
        else
        {
            if (arg1 != 4)
            {
                if (arg1 != 9)
                    return 0;

                if (arg2 == 0)
                    return 0;

                GameManager.instance.FUN_309A0(this);
                return 0xffffffff;
            }

            GameManager.instance.FUN_1DE78(DAT_18);
        }

        return 0;
    }
}
