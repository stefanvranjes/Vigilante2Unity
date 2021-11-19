using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trailer2 : Destructible
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
        return FUN_440(hit);
    }

    public Vector3Int DAT_A8; //0xA8
    public Vector3Int DAT_B4; //0xB4
    public Vehicle DAT_C0; //0xC0
    public Wheel[] DAT_C4 = new Wheel[2]; //0xC4

    public override uint UpdateW(int arg1, int arg2)
    {
        uint uVar1;
        Vehicle vVar2;

        switch (arg1)
        {
            case 0:
                FUN_484();
                uVar1 = 0;
                break;
            default:
                uVar1 = 0;
                break;
            case 2:
                FUN_4DC94();
                uVar1 = 0;
                break;
            case 8:
                FUN_32B90((uint)arg2);
                uVar1 = 0;
                break;
            case 9:
                uVar1 = 0;

                if (arg2 != 0)
                {
                    vVar2 = DAT_C0;

                    if (vVar2 != null)
                        vVar2.DAT_A6 -= DAT_A6;

                    GameManager.instance.FUN_309A0(this);
                    uVar1 = 0xffffffff;
                }

                break;
        }

        return uVar1;
    }

    private uint FUN_440(HitDetection param1)
    {
        if (DAT_C0 == null)
        {
            FUN_32CF0(param1);
            return 0;
        }
        else
            return (uint)DAT_C0.FUN_3B424(this, param1);
    }

    private void FUN_484()
    {
        bool bVar2;
        long lVar3;
        long lVar4;
        long lVar5;
        uint uVar7;
        int iVar8;
        uint uVar9;
        uint uVar10;
        int iVar11;
        Wheel wVar11;
        uint uVar12;
        int iVar13;
        uint uVar14;
        uint uVar16;
        int iVar17;
        BufferedBinaryReader brVar17;
        int iVar18;
        Wheel wVar18;
        Vehicle vVar18;
        uint uVar19;
        uint uVar22;
        Vector3Int local_168;
        Vector3Int local_158;
        Vector3Int local_128;
        Vector3Int local_118;
        Vector3Int local_100;
        Vector3Int local_f0;
        Vector3Int local_e0;
        Vector3Int local_d0;
        Vector3Int local_c0;
        Vector3Int local_b0;
        Vector3Int local_a0;
        Vector3Int local_80;
        Vector3Int local_70;
        Vector3Int local_68;
        Vector3Int local_58;
        TileData local_48;
        uint local_28;
        int local_24;
        int local_20;
        VigTransform auStack328;
        Vector3Int auStack272;
        Vector3Int auStack144;

        vVar18 = DAT_C0;

        if (vVar18 == null)
        {
            vCollider.reader.Seek(4, SeekOrigin.Current);
            FUN_2B4F8(vCollider.reader);
            vCollider.reader.Seek(-4, SeekOrigin.Current);
            physics1.X = physics1.X * 4032 >> 12;
            physics1.Y = physics1.Y * 4032 >> 12;
            physics1.Z = physics1.Z * 4032 >> 12;
        }
        else
        {
            if (((vVar18.flags | flags) & 0x4000000) == 0)
            {
                if ((vVar18.flags & 0x4000) != 0)
                {
                    auStack328 = FUN_2AEAC();
                    auStack272 = Utilities.FUN_24148(vVar18.vTransform, DAT_B4);
                    local_100 = Utilities.FUN_24304(vTransform, auStack272);
                    local_f0 = Utilities.FUN_24148(auStack328, DAT_A8);
                    local_f0.x = (local_100.x - DAT_A8.x) * 128 - local_f0.x;

                    if (local_f0.x < 0)
                        local_f0.x += 7;

                    local_158 = new Vector3Int();
                    local_158.x = local_f0.x >> 3;
                    local_f0.y = (local_100.y - DAT_A8.y) * 128 - local_f0.y;

                    if (local_f0.y < 0)
                        local_f0.y += 7;

                    local_158.y = local_f0.y >> 3;
                    local_f0.z = (local_100.z - DAT_A8.z) * 128 - local_f0.z;

                    if (local_f0.z < 0)
                        local_f0.z += 7;

                    local_158.z = local_f0.z >> 3;
                    iVar17 = local_158.x;

                    if (local_158.x < 0)
                        iVar17 = -local_158.x;

                    local_e0 = local_158;

                    if (iVar17 < 0xee681)
                    {
                        iVar17 = local_158.y;

                        if (local_158.y < 0)
                            iVar17 = -local_158.y;

                        if (iVar17 < 0xee681)
                        {
                            iVar17 = local_158.z;

                            if (local_158.z < 0)
                                iVar17 = -local_158.z;

                            if (iVar17 < 0xee681 && vVar18.wheelsType != _WHEELS.Air &&
                                vVar18.wheelsType != _WHEELS.Sea)
                            {
                                local_158 = local_e0;
                                Coprocessor.rotationMatrix.rt11 = (short)(DAT_A8.x >> 3);
                                Coprocessor.rotationMatrix.rt12 = (short)(DAT_A8.x >> 3 >> 16);
                                Coprocessor.rotationMatrix.rt22 = (short)(DAT_A8.y >> 3);
                                Coprocessor.rotationMatrix.rt23 = (short)(DAT_A8.y >> 3 >> 16);
                                Coprocessor.rotationMatrix.rt33 = (short)(DAT_A8.z >> 3);
                                local_f0.x >>= 6;
                                iVar17 = -0x8000;

                                if (-0x8001 < local_f0.x)
                                {
                                    iVar17 = 0x7fff;

                                    if (local_f0.x < 0x8000)
                                        iVar17 = local_f0.x;
                                }

                                local_f0.y >>= 6;
                                iVar8 = -0x8000;

                                if (-0x8001 < local_f0.y)
                                {
                                    iVar8 = 0x7fff;

                                    if (local_f0.y < 0x8000)
                                        iVar8 = local_f0.y;
                                }

                                local_f0.z >>= 6;
                                iVar13 = -0x8000;

                                if (-0x8001 < local_f0.z)
                                {
                                    iVar13 = 0x7fff;

                                    if (local_f0.z < 0x8000)
                                        iVar13 = local_f0.z;
                                }

                                Coprocessor.accumulator.ir1 = (short)iVar17;
                                Coprocessor.accumulator.ir2 = (short)iVar8;
                                Coprocessor.accumulator.ir3 = (short)iVar13;
                                Coprocessor.ExecuteOP(12, false);
                                local_168 = new Vector3Int(
                                    Coprocessor.mathsAccumulator.mac1,
                                    Coprocessor.mathsAccumulator.mac2,
                                    Coprocessor.mathsAccumulator.mac3);
                                local_d0 = Utilities.FUN_24094(vTransform.rotation, local_e0);
                                local_d0.x = -local_d0.x;
                                local_d0.y = -local_d0.y;
                                local_d0.z = -local_d0.z;
                                vVar18.FUN_2B370(local_d0, auStack272);
                                local_118 = new Vector3Int();
                                local_118.x = vVar18.vTransform.rotation.V02;
                                local_118.y = vVar18.vTransform.rotation.V12;
                                local_118.z = vVar18.vTransform.rotation.V22;
                                local_118 = Utilities.FUN_24238(vTransform.rotation, local_118);

                                if (local_118.z < 0)
                                    vVar18.physics2.Y += local_118.x * -4;

                                uVar10 = 0;

                                if (vTransform.rotation.V11 < 0)
                                {
                                    brVar17 = vCollider.reader;
                                    uVar7 = 0;

                                    do
                                    {
                                        local_c0 = new Vector3Int();

                                        if (uVar7 == 0)
                                            local_c0.x = brVar17.ReadInt32(4);
                                        else
                                            local_c0.x = brVar17.ReadInt32(16);

                                        if ((uVar10 & 4) == 0)
                                            local_c0.y = brVar17.ReadInt32(8);
                                        else
                                            local_c0.y = brVar17.ReadInt32(20);

                                        if ((uVar10 & 2) == 0)
                                            local_c0.z = brVar17.ReadInt32(12);
                                        else
                                            local_c0.z = brVar17.ReadInt32(24);

                                        local_c0 = Utilities.FUN_24148(vVar18.vTransform, local_c0);
                                        iVar8 = FUN_2CFBC(local_c0);

                                        if (0 < local_c0.y - iVar8)
                                        {
                                            iVar13 = -physics1.X;

                                            if (0 < physics1.X)
                                                iVar13 += 3;

                                            iVar13 >>= 2;
                                            local_b0 = new Vector3Int();

                                            if (iVar13 < -2880)
                                                local_b0.x = -2880;
                                            else
                                            {
                                                local_b0.x = 2880;

                                                if (iVar13 < 2881)
                                                    local_b0.x = iVar13;
                                            }

                                            iVar13 = -physics1.Z;

                                            if (0 < physics1.Z)
                                                iVar13 += 3;

                                            iVar13 >>= 2;

                                            if (iVar13 < -2880)
                                                local_b0.z = -2880;
                                            else
                                            {
                                                local_b0.z = 2880;

                                                if (iVar13 < 2881)
                                                    local_b0.z = iVar13;
                                            }

                                            local_b0.y = -(local_c0.y - iVar8);

                                            if (0 < vVar18.physics1.Y)
                                                local_b0.y -= vVar18.physics1.Y >> 2;

                                            Coprocessor.rotationMatrix.rt11 = (short)(vTransform.position.x >> 3);
                                            Coprocessor.rotationMatrix.rt12 = (short)(vTransform.position.x >> 3 >> 16);
                                            Coprocessor.rotationMatrix.rt22 = (short)(vTransform.position.y >> 3);
                                            Coprocessor.rotationMatrix.rt23 = (short)(vTransform.position.y >> 3 >> 16);
                                            Coprocessor.rotationMatrix.rt33 = (short)(vTransform.position.z >> 3);
                                            Coprocessor.accumulator.ir1 = (short)(local_b0.x >> 3);
                                            Coprocessor.accumulator.ir2 = (short)(local_b0.y >> 3);
                                            Coprocessor.accumulator.ir3 = (short)(local_b0.z >> 3);
                                            Coprocessor.ExecuteOP(12, false);
                                            local_158.x += local_b0.x;
                                            local_158.y += local_b0.y;
                                            local_158.z += local_b0.z;
                                            iVar8 = Coprocessor.mathsAccumulator.mac1;
                                            local_168.x += iVar8;
                                            iVar8 = Coprocessor.mathsAccumulator.mac2;
                                            local_168.y += iVar8;
                                            iVar8 = Coprocessor.mathsAccumulator.mac3;
                                            local_168.z += iVar8;
                                        }

                                        uVar10++;
                                        uVar7 = uVar10 & 1;
                                    } while ((int)uVar10 < 8);

                                    local_168 = Utilities.FUN_2426C(vVar18.vTransform.rotation,
                                        new Matrix2x4(local_168.x, local_168.y, local_168.z, 0));
                                    iVar17 = 0;

                                    do
                                    {
                                        wVar11 = DAT_C4[iVar17];
                                        iVar13 = wVar11.physics2.Z;
                                        wVar11.screen.y = wVar11.physics1.Y;
                                        iVar8 = iVar13;

                                        if (iVar13 < 0)
                                            iVar8 = iVar13 + 63;

                                        iVar13 -= iVar8 >> 6;
                                        wVar11.physics2.Z = iVar13;

                                        if (wVar11.physics2.Y != 0)
                                        {
                                            if (iVar13 < 0)
                                                iVar13 += 4095;

                                            iVar8 = (iVar13 >> 12) * wVar11.physics2.Y;

                                            if (iVar8 < 0)
                                                iVar8 += 0x7ffff;

                                            wVar11.vr.x -= iVar8 >> 19;
                                        }

                                        wVar11.ApplyTransformation();
                                        iVar17++;
                                    } while (iVar17 < 2);
                                }
                                else
                                {
                                    local_70 = new Vector3Int();
                                    local_a0 = new Vector3Int();

                                    do
                                    {
                                        wVar18 = DAT_C4[uVar10];
                                        local_a0.x = wVar18.screen.x;
                                        local_a0.y = wVar18.screen.y + wVar18.physics2.X;
                                        local_a0.z = wVar18.screen.z;
                                        local_68 = Utilities.FUN_24148(auStack328, local_a0);
                                        auStack144 = Utilities.FUN_24148(vTransform, local_a0);
                                        auStack144.y = FUN_2CFBC(auStack144, ref local_70, out local_48);
                                        local_80 = Utilities.FUN_24304(vTransform, auStack144);
                                        local_80.y -= wVar18.physics2.X;

                                        if (local_80.y < wVar18.physics1.Y)
                                        {
                                            uVar19 = (uint)(local_70.x << 16 >> 16);
                                            uVar7 = (uint)physics1.X;
                                            lVar3 = (long)((ulong)uVar19 * uVar7);
                                            uVar16 = (uint)(local_70.y << 16 >> 16);
                                            uVar9 = (uint)physics1.Y;
                                            lVar4 = (long)((ulong)uVar16 * uVar9);
                                            uVar22 = (uint)lVar4;
                                            uVar12 = (uint)(local_70.z << 16 >> 16);
                                            uVar14 = (uint)physics1.Z;
                                            lVar5 = (long)((ulong)uVar12 * uVar14);
                                            local_28 = (uint)lVar5;
                                            local_20 = (int)uVar19 * ((int)uVar7 >> 31);
                                            local_24 = (int)((ulong)lVar5 >> 32) + (int)uVar12 * ((int)uVar14 >> 31) +
                                                       (int)uVar14 * ((int)((uint)local_70.z << 16) >> 31);
                                            uVar14 = (uint)lVar3 + uVar22;
                                            uVar12 = uVar14 + local_28;
                                            iVar8 = (int)((ulong)lVar3 >> 32) + local_20 +
                                                    (int)uVar7 * ((int)((uint)local_70.x << 16) >> 31) +
                                                    (int)((ulong)lVar4 >> 32) + (int)uVar16 * ((int)uVar9 >> 31) +
                                                    (int)uVar9 * ((int)((uint)local_70.y << 16) >> 31) +
                                                    (uVar14 < uVar22 ? 1 : 0) + local_24 + (uVar12 < local_28 ? 1 : 0);
                                            iVar17 = FUN_1BC0(uVar12, iVar8, 0, 0);

                                            if (iVar17 < 1)
                                            {
                                                uVar12 += 0x7fff;
                                                iVar8 += (uVar12 < 0x7fff ? 1 : 0);
                                            }

                                            uVar7 = uVar12 >> 15 | (uint)iVar8 << 17;
                                            local_58 = Utilities.FUN_24210(vTransform.rotation, local_70);
                                            iVar17 = -local_58.x * (int)uVar7;

                                            if (iVar17 < 0)
                                                iVar17 += 4095;

                                            iVar8 = 0;

                                            if (local_a0.x - local_80.x < 0)
                                                iVar8 = local_a0.x - local_80.x;

                                            iVar13 = -local_58.z * (int)uVar7;

                                            if (iVar13 < 0)
                                                iVar13 += 4095;

                                            local_b0 = new Vector3Int();
                                            local_b0.z = 0;

                                            if (local_a0.z - local_80.z < 0)
                                                local_b0.z = local_a0.z - local_80.z;

                                            local_b0.z = (iVar13 >> 12) - local_b0.z;
                                            iVar13 = wVar18.physics1.X;

                                            if (wVar18.physics1.X < local_80.y)
                                                iVar13 = local_80.y;

                                            if (wVar18.physics1.X < local_80.y || wVar18.screen.y < local_80.y)
                                            {
                                                local_b0.y = (local_80.y - wVar18.screen.y) * wVar18.physics1.M7;

                                                if (local_b0.y < 0)
                                                    local_b0.y += 31;

                                                local_b0.y >>= 5;
                                            }
                                            else
                                                local_b0.y = (local_80.y - wVar18.screen.y) * 16;

                                            local_b0.y = ((wVar18.physics1.Y - iVar13) * wVar18.physics1.M6 * 128) / local_58.y + local_b0.y;
                                            wVar18.screen.y = local_80.y;

                                            if (local_48 == null || local_48.DAT_10[0] == 0)
                                                iVar13 = local_b0.y * -2;
                                            else
                                            {
                                                iVar11 = -local_b0.y * (0x100 - local_48.DAT_10[0]);
                                                iVar13 = iVar11 >> 7;

                                                if (iVar11 < 0)
                                                    iVar13 = iVar11 + 127 >> 7;
                                            }

                                            iVar11 = local_68.x;

                                            if (local_68.x < 0)
                                                iVar11 = local_68.x + 31;

                                            local_b0.x = -(iVar11 >> 5);

                                            if (iVar11 >> 5 < 1)
                                                bVar2 = iVar13 < local_b0.x;
                                            else
                                            {
                                                iVar13 = -iVar13;
                                                bVar2 = local_b0.x < iVar13;
                                            }

                                            if (bVar2)
                                                local_b0.x = iVar13;

                                            local_b0.x = ((iVar17 >> 12) - iVar8) + local_b0.x;
                                            Coprocessor.rotationMatrix.rt11 = (short)(local_a0.x >> 3);
                                            Coprocessor.rotationMatrix.rt12 = (short)(local_a0.x >> 3 >> 16);
                                            Coprocessor.rotationMatrix.rt22 = (short)(local_a0.y >> 3);
                                            Coprocessor.rotationMatrix.rt23 = (short)(local_a0.y >> 3 >> 16);
                                            Coprocessor.rotationMatrix.rt33 = (short)(local_a0.z >> 3);
                                            iVar17 = local_b0.x >> 3;

                                            if (iVar17 < -0x8000)
                                                iVar8 = -0x8000;
                                            else
                                            {
                                                iVar8 = 0x7fff;

                                                if (iVar17 < 0x8000)
                                                    iVar8 = iVar17;
                                            }

                                            iVar17 = local_b0.y >> 3;

                                            if (iVar17 < -0x8000)
                                                iVar13 = -0x8000;
                                            else
                                            {
                                                iVar13 = 0x7fff;

                                                if (iVar17 < 0x8000)
                                                    iVar13 = iVar17;
                                            }

                                            iVar17 = local_b0.z >> 3;

                                            if (iVar17 < -0x8000)
                                                iVar11 = -0x8000;
                                            else
                                            {
                                                iVar11 = 0x7fff;

                                                if (iVar17 < 0x8000)
                                                    iVar11 = iVar17;
                                            }

                                            Coprocessor.accumulator.ir1 = (short)iVar8;
                                            Coprocessor.accumulator.ir2 = (short)iVar13;
                                            Coprocessor.accumulator.ir3 = (short)iVar11;
                                            Coprocessor.ExecuteOP(12, false);
                                            local_158.x += local_b0.x;
                                            local_158.y += local_b0.y;
                                            local_158.z += local_b0.z;
                                            iVar17 = Coprocessor.mathsAccumulator.mac1;
                                            local_168.x += iVar17;
                                            iVar17 = Coprocessor.mathsAccumulator.mac2;
                                            local_168.y += iVar17;
                                            iVar17 = Coprocessor.mathsAccumulator.mac3;
                                            local_168.z += iVar17;
                                        }
                                        else
                                            wVar18.screen.y = wVar18.physics1.Y;

                                        iVar17 = local_68.z * wVar18.physics2.Y;
                                        wVar18.physics2.Z = local_68.z;

                                        if (iVar17 < 0)
                                            iVar17 += 0x7ffff;

                                        wVar18.vr.x -= iVar17 >> 19;
                                        uVar10++;
                                        wVar18.ApplyTransformation();
                                    } while ((int)uVar10 < 2);

                                    local_158 = Utilities.FUN_24094(vTransform.rotation, local_158);
                                }

                                local_158.y += GameManager.instance.gravityFactor;
                                FUN_2AFF8(local_158, local_168);
                                iVar17 = physics2.X;
                                iVar18 = iVar17;

                                if (iVar17 < 0)
                                    iVar18 = iVar17 + 31;

                                iVar8 = physics2.Y;
                                physics2.X = iVar17 - (iVar18 >> 5);
                                iVar18 = iVar8;

                                if (iVar8 < 0)
                                    iVar18 = iVar8 + 31;

                                iVar17 = physics2.Z;
                                physics2.Y = iVar8 - (iVar18 >> 5);
                                iVar18 = iVar17;

                                if (iVar17 < 0)
                                    iVar18 = iVar17 + 31;

                                physics2.Z = iVar17 - (iVar18 >> 5);
                                return;
                            }
                        }
                    }
                }

                DAT_C0 = null;
                type = 4;
                id = 0;
                GameManager.instance.FUN_30CB0(this, 300);
                vVar18.DAT_A6 -= DAT_A6;
            }
            else
            {
                flags ^= 0x4000000;
                local_128 = new Vector3Int();
                local_128.x = DAT_B4.x - DAT_A8.x;
                local_128.y = DAT_B4.y - DAT_A8.y;
                local_128.z = DAT_B4.z - DAT_A8.z;
                vTransform = vVar18.vTransform;
                vTransform.position = Utilities.FUN_24148(vVar18.vTransform, local_128);
                physics1.X = vVar18.physics1.X;
                physics1.Y = vVar18.physics1.Y;
                physics1.Z = vVar18.physics1.Z;
                physics2.X = vVar18.physics2.X;
                physics2.Y = vVar18.physics2.Y;
                physics2.Z = vVar18.physics2.Z;
                uVar10 = flags;

                if (((uVar10 ^ vVar18.flags) & 0x4000000) != 0)
                {
                    flags = uVar10 ^ 0x4000000;
                    uVar10 = flags;
                }

                if (((uVar10 ^ vVar18.flags) & 2) != 0)
                    flags = uVar10 ^ 2;
            }
        }
    }

    //FUN_1BC0 (TRUCK.DLL)
    private int FUN_1BC0(uint param1, int param2, uint param3, int param4)
    {
        int iVar1;

        iVar1 = 0;

        if (param4 <= param2)
        {
            iVar1 = 2;

            if (param2 <= param4)
            {
                if (param1 < param3)
                    iVar1 = 0;
                else
                {
                    iVar1 = 2;

                    if (param1 <= param3)
                        iVar1 = 1;
                }
            }
        }

        return iVar1;
    }
}
