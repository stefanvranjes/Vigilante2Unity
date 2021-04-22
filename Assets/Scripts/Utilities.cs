﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities
{
    public static byte[] DAT_10AE0 =
    {
        0, 1, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5,
        5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 6, 6, 6, 6,
        6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6,
        6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 7, 7, 7, 7, 7, 7, 7, 7,
        7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
        7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
        7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
        7, 7, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8,
        8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8,
        8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8,
        8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8,
        8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8,
        8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8,
        8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8,
        8, 8, 8, 8
    };

    public static int FUN_29E84(Vector3Int phy)
    {
        Vector2Int v2 = FUN_2ACD0(phy, phy); //r2, r3

        int iVar3 = v2.y >> 0; //r4
        int iVar4 = v2.y >> 0x1f; //r5
        iVar3 = LeadingZeros(iVar3);

        int iVar5 = 35 - iVar3 >> 1; //r16
        iVar3 = iVar5 << 1;
        int iVar6 = iVar3 << 0x1a; //r6
        int iVar7 = 0; //r8

        if (iVar6 < 0)
        {
            iVar7 = v2.y >> 0x1f;
        }
        else
        {
            iVar7 = (int)((uint)v2.x >> iVar3);

            if (iVar6 != 0)
            {
                iVar6 = v2.y << (iVar3 * -1);
                iVar7 |= iVar6;
            }
        }

        iVar4 = v2.y >> iVar3;

        return (int)(SquareRoot(iVar7) << iVar5);
    }

    public static Vector2Int FUN_2ACD0(Vector3Int v3a, Vector3Int v3b)
    {
        long lVar1 = (long)v3a.x * v3b.x;
        long lVar2 = (long)v3a.y * v3b.y;
        long lVar3 = (long)v3a.z * v3b.z;

        Vector2Int v2 = new Vector2Int();
        v2.x = (int)lVar1 + (int)lVar2;
        v2.y = (int)(lVar1 >> 0x20) + (int)(lVar2 >> 0x20);
        v2.y += (uint)v2.x < (uint)lVar2 ? 1 : 0;
        v2.x += (int)lVar3;
        v2.y += (int)(lVar3 >> 0x20);
        v2.y += (uint)v2.x < (uint)lVar3 ? 1 : 0;

        return v2;
    }

    public static uint FUN_2AC5C()
    {
        uint uVar1 = GameManager.DAT_63A64; //r8
        uint uVar2 = (uint)(byte)GameManager.DAT_63A68 << 31; //r9
        GameManager.DAT_63A68 = (byte)uVar1;
        uint uVar3 = (uVar1 >> 1) + uVar2;
        uVar1 = uVar1 << 12;
        uVar3 = uVar3 ^ uVar1;
        uVar1 = uVar3 >> 20;
        uVar3 = uVar3 ^ uVar1;
        GameManager.DAT_63A64 = uVar3;
        return uVar3 & 0x7FFF;
    }

    public static int FUN_2A27C(Matrix3x3 m33)
    {
        return Ratan2(m33.V02, m33.V22) << 16 >> 16;
    }

    public static void FUN_2CC48(VigObject obj1, VigObject obj2)
    {
        if (obj1.child2 != null)
        {
            obj1 = obj1.child2;

            while (obj1.child != null)
                obj1 = obj1.child;

            obj1.child = obj2;
            obj2.parent = obj1;
        }
        else
        {
            obj1.child2 = obj2;
            obj2.parent = obj1;
        }
    }

    public static Vector3Int FUN_2426C(Matrix3x3 m33, Matrix2x3 m23)
    {
        FUN_243B4(m33);

        int iVar1 = m23.X; //r8
        int iVar2 = m23.Y; //r9
        int iVar3 = m23.Z; //r10
        int iVar4 = iVar1 >> 15; //r11
        int iVar5 = iVar2 >> 15; //r12
        int iVar6 = iVar3 >> 15; //r13
        Coprocessor.accumulator.ir1 = (short)iVar4;
        Coprocessor.accumulator.ir2 = (short)iVar5;
        Coprocessor.accumulator.ir3 = (short)iVar6;

        iVar1 &= 0x7FFF;
        iVar2 &= 0x7FFF;
        iVar3 &= 0x7FFF;
        Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.IR, _MVMVA_TRANSLATION_VECTOR.None, 0, false);

        iVar4 = iVar2 << 16 | iVar1;
        Coprocessor.vector0.vx0 = (short)(iVar4 & 0xFFFF);
        Coprocessor.vector0.vy0 = (short)(iVar4 >> 16);
        Coprocessor.vector0.vz0 = (short)iVar3;

        iVar4 = Coprocessor.mathsAccumulator.mac1;
        iVar5 = Coprocessor.mathsAccumulator.mac2;
        iVar6 = Coprocessor.mathsAccumulator.mac3;
        Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.V0, _MVMVA_TRANSLATION_VECTOR.None, 12, false);

        iVar4 = iVar4 << 3;
        iVar5 = iVar5 << 3;
        iVar6 = iVar6 << 3;
        iVar1 = Coprocessor.mathsAccumulator.mac1 + iVar4;
        iVar2 = Coprocessor.mathsAccumulator.mac2 + iVar5;
        iVar3 = Coprocessor.mathsAccumulator.mac3 + iVar6;

        return new Vector3Int(iVar1, iVar2, iVar3);
    }

    public static void FUN_243B4(Matrix3x3 m33)
    {
        int iVar1 = m33.GetValue32(0) & 0xFFFF; //r8
        int iVar2 = m33.GetValue32(0) - iVar1; //r2
        int iVar3 = m33.GetValue32(1) & 0xFFFF; //r9
        int iVar4 = m33.GetValue32(1) - iVar3; //r3
        iVar1 |= iVar4;
        Coprocessor.rotationMatrix.rt11 = (short)(iVar1 & 0xFFFF);
        Coprocessor.rotationMatrix.rt12 = (short)(iVar1 >> 16);

        iVar1 = m33.GetValue32(2) & 0xFFFF;
        iVar4 = m33.GetValue32(2) - iVar1;
        iVar3 |= iVar4;
        Coprocessor.rotationMatrix.rt31 = (short)(iVar3 & 0xFFFF);
        Coprocessor.rotationMatrix.rt32 = (short)(iVar3 >> 16);

        iVar3 = m33.GetValue32(3) & 0xFFFF;
        iVar4 = m33.GetValue32(3) - iVar3;
        iVar2 |= iVar3;
        iVar4 |= iVar1;
        Coprocessor.rotationMatrix.rt13 = (short)(iVar2 & 0xFFFF);
        Coprocessor.rotationMatrix.rt21 = (short)(iVar2 >> 16);
        Coprocessor.rotationMatrix.rt22 = (short)(iVar4 & 0xFFFF);
        Coprocessor.rotationMatrix.rt23 = (short)(iVar4 >> 16);
        Coprocessor.rotationMatrix.rt33 = (short)m33.GetValue32(4);
    }

    public static Vector3Int FUN_24304(Matrix3x3 rot, Vector3Int pos, Vector3Int v)
    {
        FUN_243B4(rot);

        int iVar1 = v.x - pos.x;
        int iVar2 = v.y - pos.y;
        int iVar3 = v.z - pos.z;
        Coprocessor.accumulator.ir1 = (short)(iVar1 >> 15);
        Coprocessor.accumulator.ir2 = (short)(iVar2 >> 15);
        Coprocessor.accumulator.ir3 = (short)(iVar3 >> 15);
        Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.IR, _MVMVA_TRANSLATION_VECTOR.None, 0, false);

        int mac1_1 = Coprocessor.mathsAccumulator.mac1 << 3;
        int mac2_1 = Coprocessor.mathsAccumulator.mac2 << 3;
        int mac3_1 = Coprocessor.mathsAccumulator.mac3 << 3;

        iVar1 &= 0x7FFF;
        iVar2 &= 0x7FFF;
        iVar3 &= 0x7FFF;
        Coprocessor.vector0.vx0 = (short)iVar1;
        Coprocessor.vector0.vy0 = (short)iVar2;
        Coprocessor.vector0.vz0 = (short)iVar3;
        Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.V0, _MVMVA_TRANSLATION_VECTOR.None, 12, false);

        int mac1_2 = Coprocessor.mathsAccumulator.mac1 + mac1_1;
        int mac2_2 = Coprocessor.mathsAccumulator.mac2 + mac2_1;
        int mac3_2 = Coprocessor.mathsAccumulator.mac3 + mac3_1;

        return new Vector3Int(mac1_2, mac2_2, mac3_2);
    }

    public static Vector3Int FUN_24148(Matrix3x3 rot, Vector3Int pos, Vector3Int v)
    {
        Coprocessor.rotationMatrix.rt11 = rot.V00;
        Coprocessor.rotationMatrix.rt12 = rot.V01;
        Coprocessor.rotationMatrix.rt13 = rot.V02;
        Coprocessor.rotationMatrix.rt21 = rot.V10;
        Coprocessor.rotationMatrix.rt22 = rot.V11;
        Coprocessor.rotationMatrix.rt23 = rot.V12;
        Coprocessor.rotationMatrix.rt31 = rot.V20;
        Coprocessor.rotationMatrix.rt32 = rot.V21;
        Coprocessor.rotationMatrix.rt33 = rot.V22;
        Coprocessor.accumulator.ir1 = (short)(v.x >> 15);
        Coprocessor.accumulator.ir2 = (short)(v.y >> 15);
        Coprocessor.accumulator.ir3 = (short)(v.z >> 15);
        Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.IR, _MVMVA_TRANSLATION_VECTOR.None, 0, false);

        int mac1_1 = Coprocessor.mathsAccumulator.mac1 << 3;
        int mac2_1 = Coprocessor.mathsAccumulator.mac2 << 3;
        int mac3_1 = Coprocessor.mathsAccumulator.mac3 << 3;

        Coprocessor.translationVector._trx = pos.x;
        Coprocessor.translationVector._try = pos.y;
        Coprocessor.translationVector._trz = pos.z;
        Coprocessor.accumulator.ir1 = (short)(v.x & 0x7fff);
        Coprocessor.accumulator.ir2 = (short)(v.y & 0x7fff);
        Coprocessor.accumulator.ir3 = (short)(v.z & 0x7fff);
        Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.IR, _MVMVA_TRANSLATION_VECTOR.TR, 12, false);

        int mac1_2 = Coprocessor.mathsAccumulator.mac1 + mac1_1;
        int mac2_2 = Coprocessor.mathsAccumulator.mac2 + mac2_1;
        int mac3_2 = Coprocessor.mathsAccumulator.mac3 + mac3_1;

        return new Vector3Int(mac1_2, mac2_2, mac3_2);
    }

    public static Vector3Int FUN_24210(Matrix3x3 rot, Vector3Int normal)
    {
        FUN_243B4(rot);

        Coprocessor.vector0.vx0 = (short)normal.x;
        Coprocessor.vector0.vy0 = (short)normal.y;
        Coprocessor.vector0.vz0 = (short)normal.z;
        Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.V0, _MVMVA_TRANSLATION_VECTOR.None, 12, false);

        return new Vector3Int(Coprocessor.accumulator.ir1, Coprocessor.accumulator.ir2, Coprocessor.accumulator.ir3);
    }

    public static Vector3Int FUN_24094(Matrix3x3 rot, Vector3Int v)
    {
        Coprocessor.rotationMatrix.rt11 = rot.V00;
        Coprocessor.rotationMatrix.rt12 = rot.V01;
        Coprocessor.rotationMatrix.rt13 = rot.V02;
        Coprocessor.rotationMatrix.rt21 = rot.V10;
        Coprocessor.rotationMatrix.rt22 = rot.V11;
        Coprocessor.rotationMatrix.rt23 = rot.V12;
        Coprocessor.rotationMatrix.rt31 = rot.V20;
        Coprocessor.rotationMatrix.rt32 = rot.V21;
        Coprocessor.rotationMatrix.rt33 = rot.V22;
        Coprocessor.accumulator.ir1 = (short)(v.x >> 15);
        Coprocessor.accumulator.ir2 = (short)(v.y >> 15);
        Coprocessor.accumulator.ir3 = (short)(v.z >> 15);
        Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.IR, _MVMVA_TRANSLATION_VECTOR.None, 0, false);

        int mac1_1 = Coprocessor.mathsAccumulator.mac1 << 3;
        int mac2_1 = Coprocessor.mathsAccumulator.mac2 << 3;
        int mac3_1 = Coprocessor.mathsAccumulator.mac3 << 3;

        Coprocessor.vector0.vx0 = (short)(v.x & 0x7FFF);
        Coprocessor.vector0.vy0 = (short)(v.y & 0x7FFF);
        Coprocessor.vector0.vz0 = (short)(v.z & 0x7FFF);
        Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.V0, _MVMVA_TRANSLATION_VECTOR.None, 12, false);

        int mac1_2 = Coprocessor.mathsAccumulator.mac1 + mac1_1;
        int mac2_2 = Coprocessor.mathsAccumulator.mac2 + mac2_1;
        int mac3_2 = Coprocessor.mathsAccumulator.mac3 + mac3_1;

        return new Vector3Int(mac1_2, mac2_2, mac3_2);
    }

    public static int LeadingZeros(int x)
    {
        if ((x & 0x80000000) != 0)
            x ^= -1;

        if (x == 0) return 32;

        const int numIntBits = sizeof(int) * 8; //compile time constant
                                                //do the smearing
        x |= x >> 1;
        x |= x >> 2;
        x |= x >> 4;
        x |= x >> 8;
        x |= x >> 16;
        //count the ones
        x -= x >> 1 & 0x55555555;
        x = (x >> 2 & 0x33333333) + (x & 0x33333333);
        x = (x >> 4) + x & 0x0f0f0f0f;
        x += x >> 8;
        x += x >> 16;
        return numIntBits - (x & 0x0000003f); //subtract # of 1s from 32
    }

    //FUN_597BC
    public static long SquareRoot (long a)
    {
        int iVar1 = LeadingZeros((int)a); //r2
        int iVar6 = 0; //r12

        if (iVar1 != 0x20)
        {
            int iVar2 = iVar1 & 1; //r8
            int iVar3 = iVar1 & -2; //r10
            int iVar4 = 0x1f - iVar3 >> 1; //r9
            int iVar5 = iVar3 - 24; //r11

            if (iVar5 < 0)
                iVar6 = (int)a >> 24 - iVar3;
            else
                iVar6 = (int)a << iVar5;

            iVar6 -= 64;
            iVar6 = iVar6 << 1;
            int iVar7 = GameManager.SQRT[iVar6 / 2] << iVar4;

            return (uint)iVar7 >> 12;
        }

        return 0;
    }

    public static int Ratan2(int y, int x)
    {
        bool bVar1 = x < 0;

        if (bVar1)
            x = -x;

        bool bVar2 = y < 0;

        if (bVar2)
            y = -y;

        int iVar3 = 0;
        int iVar4 = 0;

        if (x != 0 || y != 0)
        {
            if (y < x)
            {
                iVar3 = x >> 10;

                if ((y & 0x7fe00000U) == 0)
                {
                    if (x == 0)
                        return 0; //exception 0x1C00

                    if (x == -1 && (y & 0x3fffffU) == 0x200000)
                        return 0; //exception 0x1800

                    y = (y << 10) / x;
                    iVar4 = GameManager.DAT_69C90[(y << 1) / 2];

                    if (bVar1)
                        iVar4 = 2048 - iVar4;

                    if (bVar2)
                        iVar4 = -iVar4;

                    return iVar4;
                }

                if (iVar3 == 0)
                    return 0; //exception 0x1C00

                if (iVar3 == -1 && (uint)y == 0x80000000)
                    return 0; //exception 0x1800

                y = y / iVar3;
                iVar4 = GameManager.DAT_69C90[(y << 1) / 2];

                if (bVar1)
                    iVar4 = 2048 - iVar4;

                if (bVar2)
                    iVar4 = -iVar4;

                return iVar4;
            }

            iVar3 = y >> 10;

            if ((x & 0x7fe00000U) != 0)
            {
                if (iVar3 == 0)
                    return 0; //exception 0x1C00

                if (iVar3 == -1 && (uint)x == 0x80000000)
                    return 0; //exception 0x1800

                y = x / iVar3;
                iVar4 = 1024 - GameManager.DAT_69C90[(y << 1) / 2];

                if (bVar1)
                    iVar4 = 2048 - iVar4;

                if (bVar2)
                    iVar4 = -iVar4;

                return iVar4;
            }

            if (y == 0)
                return 0; //exception 0x1C00

            if (y == -1 && (uint)(x << 10) == 0x80000000)
                return 0; //exception 0x1800

            y = (x << 10) / y;
            iVar4 = 1024 - GameManager.DAT_69C90[(y << 1) / 2];

            if (bVar1)
                iVar4 = 2048 - iVar4;

            if (bVar2)
                iVar4 = -iVar4;

            return iVar4;
        }

        return iVar3;
    }

    //FUN_545A4
    public static long Divdi3(int param1, int param2, int param3, int param4)
    {
        int iVar1 = 0; //r18

        if (param2 < 0)
        {
            iVar1 = -1;
            param1 = -param1;
            param2 = -(param1 != 0 ? 1 : 0) - param2;
        }

        if (param4 < 0)
        {
            iVar1 = ~iVar1;
            param4 = -(-param3 != 0 ? 1 : 0) - param4;
            param3 = -param3;
        }

        long lVar2 = (long)Udivmoddi4((uint)param1, (uint)param2, (uint)param3, (uint)param4, null);

        if (iVar1 != 0)
            lVar2 = -lVar2;

        return lVar2;
    }

    public static ulong Udivmoddi4(uint param1, uint param2, uint param3, uint param4, uint[] param5)
    {
        //param1 it t3
        bool bVar1;
        ulong lVar2;
        uint uVar3;
        int iVar4;
        uint uVar5;
        uint uVar6;
        uint uVar7;
        uint uVar8;
        uint uVar9;
        uint uVar10;
        uint uVar11;
        uint uVar12;
        uint uVar13;
        uint t4;
        uint t5;

        if (param4 == 0)
        {
            if (param2 < param3)
            {
                if (0xffff < param3)
                {
                    iVar4 = 24;

                    if (param3 < 0x1000000)
                        iVar4 = 16;
                }
                else
                {
                    iVar4 = (param3 < 256 ? 1 : 0) ^ 1;
                    iVar4 = iVar4 << 3;
                }

                uVar3 = (uint)(32 - (DAT_10AE0[param3 >> iVar4] + iVar4));
                t5 = uVar3;

                if (uVar3 != 0)
                {
                    param3 = param3 << (int)(uVar3 & 31);
                    param2 = param2 << (int)(uVar3 & 31) | param1 >> (int)(32 - uVar3 & 31);
                    param1 = param1 << (int)(uVar3 & 31);
                }

                uVar3 = param3 >> 16;
                uVar8 = param2 / uVar3;

                if (uVar3 == 0)
                    return 0; //exception 0x1C00

                uVar9 = uVar8 * (param3 & 0xffff);
                uVar5 = param2 % uVar3 << 16 | param1 >> 16;
                uVar6 = uVar8;

                if (uVar5 < uVar9)
                {
                    uVar5 = uVar5 + param3;
                    uVar6 = uVar8 - 1;

                    if (param3 <= uVar5 && uVar5 < uVar9)
                    {
                        uVar6 = uVar8 - 2;
                        uVar5 += param3;
                    }
                }

                uVar8 = (uVar5 - uVar9) / uVar3;

                if (uVar3 == 0)
                    return 0; //exception 0x1C00

                uVar10 = uVar8 * (param3 & 0xffff);
                uVar9 = (uVar5 - uVar9) % uVar3 << 16 | param1 & 0xffff;
                uVar5 = uVar8;

                if (uVar9 < uVar10)
                {
                    uVar9 += param3;
                    uVar5 = uVar8 - 1;

                    if (param3 <= uVar9 && uVar9 < uVar10)
                        uVar5 = uVar8 - 2;
                }

                if (param5 != null)
                {
                    param5[0] = param1 >> (int)t5;
                    param5[1] = 0;
                }

                return (uVar6 << 16) | uVar5;
            }

            bVar1 = 0xffff < param3;

            if (param3 == 0)
            {
                if (param4 == 0) //can't divide by zero
                    return 0; //exception 0x1C00

                param3 = 1 / param4;
                bVar1 = 0xffff < param3;
            }

            if (!bVar1)
            {
                iVar4 = (param3 < 256 ? 1 : 0) ^ 1;
                iVar4 = iVar4 << 3;
            }
            else
            {
                iVar4 = 24;

                if (param3 < 0x1000000)
                    iVar4 = 16;
            }

            uVar3 = (uint)(32 - (DAT_10AE0[param3 >> iVar4] + iVar4));
            t5 = uVar3;

            if (uVar3 == 0)
            {
                uVar3 = param2;
                uVar10 = param3;
                t4 = 1;
            }
            else
            {
                param3 = param3 << (int)(uVar3 & 31);
                uVar8 = param2 >> (int)(32 - uVar3 & 31);
                uVar6 = param2 << (int)(uVar3 & 31) | param1 >> (int)(32 - uVar3 & 31);
                uVar5 = param3 >> 16;

                if (uVar5 == 0)
                    return 0; //exception 0x1C00

                uVar11 = uVar8 / uVar5; //a3
                uVar9 = uVar11 * (param3 & 0xffff);
                uVar8 = uVar8 % uVar5 << 16 | uVar6 >> 16;
                param1 = param1 << (int)(uVar3 & 31);

                if (uVar8 < uVar9)
                {
                    uVar8 += param3;
                    uVar11 -= 1;

                    if (param3 <= uVar8 && uVar8 < uVar9)
                    {
                        uVar8 += param3;
                        uVar11 -= 1;
                    }
                }

                if (uVar5 == 0)
                    return 0; ///exception 0x1C00

                uVar12 = (uVar8 - uVar9) / uVar5; //a2
                uVar10 = uVar12 * (param3 & 0xffff);
                uVar3 = (uVar8 - uVar9) % uVar5 << 16 | uVar6 & 0xffff;
                uVar8 = uVar11 << 16; //v0

                if (uVar3 < uVar10)
                {
                    uVar8 = uVar11 << 16;
                    uVar3 += param3;
                    uVar12 -= 1;

                    if (param3 <= uVar3 && uVar3 < uVar10)
                    {
                        uVar3 += param3;
                        uVar12 -= 1;
                    }
                }

                t4 = uVar8 | uVar12;
            }

            uVar8 = param3 >> 16;
            uVar6 = (uVar3 - uVar10) / uVar8;

            if (uVar8 == 0)
                return 0; //exception 0x1C00

            uVar9 = uVar6 * (param3 * 0xffff);
            uVar3 = (uVar3 - uVar10) % uVar8 << 16 | param1 >> 16;
            uVar5 = uVar6;

            if (uVar3 < uVar9)
            {
                uVar3 += param3;
                uVar5 = uVar6 - 1;

                if (param3 <= uVar3 && uVar3 < uVar9)
                {
                    uVar5 = uVar6 - 2;
                    uVar3 = uVar3 + param3;
                }
            }

            uVar6 = (uVar3 - uVar9) / uVar8;

            if (uVar8 == 0)
                return 0; //exception 0x1C00

            uVar10 = uVar6 * (param3 & 0xffff);
            uVar8 = (uVar3 - uVar9) % uVar8 << 16 | param1 & 0xffff;
            uVar3 = uVar6;

            if (uVar8 < uVar10)
            {
                uVar8 += param3;
                uVar3 = uVar6 - 1;

                if (param3 <= uVar8 && uVar8 < uVar10)
                    uVar3 = uVar6 - 2;
            }

            uVar3 = uVar5 << 16 | uVar3;

            if (param5 != null)
            {
                param5[0] = uVar8 - uVar10 >> (int)t5;
                param5[1] = 0;
            }

            return (ulong)t4 << 32 | uVar3;
        }
        else
        {
            uVar3 = 0;

            if (param2 < param4)
            {
                if (param5 != null)
                {
                    param5[0] = param1;
                    param5[1] = param2;
                }

                return 0;
            }
            else
            {
                if (param4 < 0x10000)
                {
                    iVar4 = (param4 < 256 ? 1 : 0) ^ 1;
                    iVar4 = iVar4 << 3;
                }
                else
                {
                    iVar4 = 24;

                    if (param4 < 0x1000000)
                        iVar4 = 16;
                }

                uVar8 = (uint)(32 - (DAT_10AE0[param3 >> iVar4] + iVar4));
                uVar6 = 32 - uVar8;
                t5 = uVar8;

                if (uVar8 == 0)
                {
                    if (param4 < param2 || param3 <= param1)
                    {
                        uVar3 = 1;
                        uVar8 = param1 - param3;
                        uVar6 = (param2 - param4) - (uint)(param1 < uVar3 ? 1 : 0);
                    }
                    else
                    {
                        uVar3 = 0;
                    }

                    if (param5 != null)
                    {
                        param5[0] = uVar8;
                        param5[1] = uVar6;
                    }

                    return uVar3;
                }
                else
                {
                    uVar9 = param4 << (int)(uVar8 & 31) | param3 >> (int)(uVar6 & 31);
                    param3 = param3 << (int)(uVar8 & 31);
                    uVar3 = param2 >> (int)(uVar6 & 31);
                    uVar5 = param2 << (int)(uVar8 & 31) | param1 >> (int)(uVar6 & 31);
                    uVar10 = uVar9 >> 16;
                    uVar11 = uVar3 / uVar10;

                    if (uVar10 == 0)
                        return 0; //exception 0x1C00

                    uVar12 = uVar11 * (uVar9 & 0xffff);
                    uVar3 = uVar3 % uVar10 << 16 | uVar5 >> 16;
                    param1 = param1 << (int)(uVar8 & 31);
                    uVar7 = uVar11;

                    if (uVar3 < uVar12)
                    {
                        uVar3 += uVar9;
                        uVar7 = uVar11 - 1;

                        if (uVar9 <= uVar3 && uVar3 < uVar12)
                        {
                            uVar7 = uVar11 - 2;
                            uVar3 += uVar9;
                        }
                    }

                    uVar11 = (uVar3 - uVar12) / uVar10;

                    if (uVar10 == 0)
                        return 0; //exception 0x1C00

                    uVar13 = uVar11 * (uVar9 & 0xffff);
                    uVar5 = (uVar3 - uVar12) % uVar10 << 16 | uVar5 & 0xffff;
                    uVar3 = uVar11;

                    if (uVar5 < uVar13)
                    {
                        uVar5 += uVar9;
                        uVar3 = uVar11 - 1;

                        if (uVar9 <= uVar5 && uVar5 < uVar13)
                        {
                            uVar3 = uVar11 - 2;
                            uVar5 += uVar9;
                        }
                    }

                    uVar3 = uVar7 << 16 | uVar3;
                    uVar5 -= uVar13;
                    lVar2 = (ulong)uVar3 * param3;
                    uVar10 = (uint)lVar2;
                    uVar11 = (uint)(lVar2 >> 32);
                    param3 = uVar10 - param3;

                    if (uVar5 < uVar11 || (uVar11 == uVar5 && param1 < uVar10))
                    {
                        uVar3 -= 1;
                        uVar11 = (uVar11 - uVar9) - (uint)(uVar10 < param3 ? 1 : 0);
                        uVar10 = param3;
                    }

                    if (param5 != null)
                    {
                        uVar5 = (uVar5 - uVar11) - (uint)(param1 < param1 - uVar10 ? 1 : 0);
                        param5[0] = uVar5 << (int)(uVar6 & 31) | param1 - uVar10 >> (int)(uVar8 & 31);
                        param5[1] = uVar5 >> (int)(uVar8 & 31);
                    }
                }
            }
        }

        return uVar3;
    }

    //FUN_59FBC
    public static Vector3Int ApplyMatrixSV(Matrix3x3 m33, Vector3Int v3)
    {
        Coprocessor.rotationMatrix.rt11 = m33.V00;
        Coprocessor.rotationMatrix.rt12 = m33.V01;
        Coprocessor.rotationMatrix.rt13 = m33.V02;
        Coprocessor.rotationMatrix.rt21 = m33.V10;
        Coprocessor.rotationMatrix.rt22 = m33.V11;
        Coprocessor.rotationMatrix.rt23 = m33.V12;
        Coprocessor.rotationMatrix.rt31 = m33.V20;
        Coprocessor.rotationMatrix.rt32 = m33.V21;
        Coprocessor.rotationMatrix.rt33 = m33.V22;
        Coprocessor.vector0.vx0 = (short)v3.x;
        Coprocessor.vector0.vy0 = (short)v3.y;
        Coprocessor.vector0.vz0 = (short)v3.z;
        Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.V0, _MVMVA_TRANSLATION_VECTOR.None, 12, false);

        return new Vector3Int(Coprocessor.accumulator.ir1, Coprocessor.accumulator.ir2, Coprocessor.accumulator.ir3);
    }

    public static Matrix3x3 RotMatrixYXZ(Vector3Int r)
    {
        Matrix3x3 m33 = new Matrix3x3();
        int z = r.z >> 31; //r11
        int xy = (r.y << 16) + r.x; //r12
        int index = ((r.z + z ^ z) << 2 & 0x3FFC) / 2;
        int iVar1 = (GameManager.DAT_65C90[index + 1] << 16) | (ushort)GameManager.DAT_65C90[index]; //r6
        int iVar2 = xy >> 16; //r8
        int iVar3 = iVar2 >> 31; //r10
        index = ((iVar2 + iVar3 ^ iVar3) << 2 & 0x3FFC) / 2;
        int iVar4 = (GameManager.DAT_65C90[index + 1] << 16) | (ushort)GameManager.DAT_65C90[index]; //r5
        iVar2 = xy << 16 >> 16;
        int iVar5 = iVar2 >> 31; //r9
        index = ((iVar2 + iVar5 ^ iVar5) << 2 & 0x3FFC) / 2;
        int iVar6 = (GameManager.DAT_65C90[index + 1] << 16) | (ushort)GameManager.DAT_65C90[index]; //r4
        iVar1 = iVar1 >> 16 << 16 | (int)((uint)((iVar1 << 16) + z ^ z) >> 16);
        iVar4 = iVar4 >> 16 << 16 | (int)((uint)((iVar4 << 16) + iVar3 ^ iVar3) >> 16);
        iVar6 = iVar6 >> 16 << 16 | (int)((uint)((iVar6 << 16) + iVar5 ^ iVar5) >> 16);
        iVar2 = iVar4 >> 16;
        Coprocessor.accumulator.ir0 = (short)iVar2;
        int iVar7 = iVar6 << 16 >> 16; //r7
        Coprocessor.accumulator.ir1 = (short)iVar7;
        int iVar8 = iVar1 << 16 >> 16; //r3
        Coprocessor.accumulator.ir2 = (short)iVar8;
        Coprocessor.accumulator.ir3 = (short)(iVar1 >> 16);
        Coprocessor.ExecuteGPF(12, false);
        m33.SetValue32(4, (iVar6 >> 16) * iVar2 >> 12);
        iVar2 = Coprocessor.accumulator.ir1;
        iVar5 = Coprocessor.accumulator.ir2;
        iVar3 = Coprocessor.accumulator.ir3;
        int iVar9 = iVar4 << 16 >> 16; //r14
        Coprocessor.accumulator.ir0 = (short)iVar9;
        Coprocessor.accumulator.ir1 = (short)iVar7;
        Coprocessor.accumulator.ir2 = (short)iVar8;
        Coprocessor.accumulator.ir3 = (short)(iVar1 >> 16);
        Coprocessor.ExecuteGPF(12, false);
        int iVar10 = Coprocessor.accumulator.ir1; //r11
        int iVar11 = Coprocessor.accumulator.ir2; //r12
        int iVar12 = Coprocessor.accumulator.ir3; //r13
        Coprocessor.accumulator.ir0 = (short)(iVar1 >> 16);
        Coprocessor.accumulator.ir1 = (short)(iVar6 >> 16);
        Coprocessor.accumulator.ir2 = (short)iVar10;
        Coprocessor.accumulator.ir3 = (short)iVar2;
        Coprocessor.ExecuteGPF(12, false);
        int iVar13 = iVar6 >> 16; //r1
        int iVar14 = iVar13 * iVar9; //lo
        iVar6 = Coprocessor.accumulator.ir1;
        iVar4 = Coprocessor.accumulator.ir2;
        iVar1 = Coprocessor.accumulator.ir3;
        Coprocessor.accumulator.ir0 = (short)iVar8;
        Coprocessor.accumulator.ir1 = (short)iVar13;
        Coprocessor.accumulator.ir2 = (short)iVar10;
        Coprocessor.accumulator.ir3 = (short)iVar2;
        Coprocessor.ExecuteGPF(12, false);
        iVar7 = -iVar7 << 16;
        iVar6 = iVar6 & 0xFFFF | iVar7;
        m33.SetValue32(2, iVar6);
        iVar13 = Coprocessor.accumulator.ir1;
        iVar9 = Coprocessor.accumulator.ir2;
        int iVar15 = Coprocessor.accumulator.ir3; //r15
        iVar5 = iVar4 - iVar5 << 16;
        iVar3 = iVar3 + iVar9 & 0xFFFF;
        iVar5 |= iVar3;
        m33.SetValue32(0, iVar5);
        iVar14 = iVar14 >> 12 & 0xFFFF;
        iVar13 = iVar13 << 16 | iVar14;
        m33.SetValue32(1, iVar13);
        iVar12 = iVar15 - iVar12 & 0xFFFF;
        iVar11 = iVar1 + iVar11 << 16 | iVar13;
        m33.SetValue32(3, iVar11);
        return m33;
    }

    public static Vector3Int VectorNormalSS(Vector3Int n1)
    {
        int normal_x = n1.x;
        int normal_y = n1.y;
        int normal_z = n1.z;

        return MSC02_OBJ_100(new Vector3Int(normal_x, normal_y, normal_z));
    }

    //FUN_59A0C
    public static Matrix3x3 MatrixNormal(Matrix3x3 m33)
    {
        Matrix3x3 n = new Matrix3x3();

        short rt11 = Coprocessor.rotationMatrix.rt11;
        short rt12 = Coprocessor.rotationMatrix.rt12;
        short rt22 = Coprocessor.rotationMatrix.rt22;
        short rt23 = Coprocessor.rotationMatrix.rt23;
        short rt33 = Coprocessor.rotationMatrix.rt33;

        Coprocessor.rotationMatrix.rt11 = m33.V00;
        Coprocessor.rotationMatrix.rt12 = (short)(m33.V00 >> 16);
        Coprocessor.rotationMatrix.rt22 = m33.V01;
        Coprocessor.rotationMatrix.rt23 = (short)(m33.V01 >> 16);
        Coprocessor.rotationMatrix.rt33 = m33.V02;
        Coprocessor.accumulator.ir3 = m33.V12;
        Coprocessor.accumulator.ir1 = m33.V10;
        Coprocessor.accumulator.ir2 = m33.V11;
        Coprocessor.ExecuteOP(12, false);

        int mac1_1 = Coprocessor.mathsAccumulator.mac1;
        int mac2_1 = Coprocessor.mathsAccumulator.mac2;
        int mac3_1 = Coprocessor.mathsAccumulator.mac3;

        Coprocessor.rotationMatrix.rt11 = m33.V10;
        Coprocessor.rotationMatrix.rt12 = (short)(m33.V10 >> 16);
        Coprocessor.rotationMatrix.rt22 = m33.V11;
        Coprocessor.rotationMatrix.rt23 = (short)(m33.V11 >> 16);
        Coprocessor.rotationMatrix.rt33 = m33.V12;
        Coprocessor.ExecuteOP(12, false);

        int mac1_2 = Coprocessor.mathsAccumulator.mac1;
        int mac2_2 = Coprocessor.mathsAccumulator.mac2;
        int mac3_2 = Coprocessor.mathsAccumulator.mac3;

        Coprocessor.vector0.vx0 = m33.V10;
        Coprocessor.vector0.vy0 = (short)(m33.V10 >> 16);
        Coprocessor.vector0.vz0 = m33.V11;
        Coprocessor.vector1.vx1 = m33.V12;
        Coprocessor.vector1.vy1 = (short)(m33.V12 >> 16);
        Coprocessor.rotationMatrix.rt11 = rt11;
        Coprocessor.rotationMatrix.rt12 = rt12;
        Coprocessor.rotationMatrix.rt22 = rt22;
        Coprocessor.rotationMatrix.rt23 = rt23;
        Coprocessor.rotationMatrix.rt33 = rt33;

        Vector3Int v1 = MSC02_OBJ_100(new Vector3Int(mac1_2, mac2_2, mac3_2));
        n.V00 = (short)v1.x;
        n.V01 = (short)v1.y;
        n.V02 = (short)v1.z;

        Vector3Int v2 = MSC02_OBJ_100(new Vector3Int
            (Coprocessor.vector0.vy0 << 16 | (ushort)Coprocessor.vector0.vx0, Coprocessor.vector0.vz0, 
            Coprocessor.vector1.vy1 << 16 | (ushort)Coprocessor.vector1.vx1));
        n.V10 = (short)v2.x;
        n.V11 = (short)v2.y;
        n.V12 = (short)v2.z;

        Vector3Int v3 = MSC02_OBJ_100(new Vector3Int(mac1_1, mac2_1, mac3_1));
        n.V20 = (short)v3.x;
        n.V21 = (short)v3.y;
        n.V22 = (short)v3.z;

        return n;
    }

    public static Vector3Int MSC02_OBJ_100(Vector3Int normal)
    {
        Coprocessor.accumulator.ir1 = (short)normal.x;
        Coprocessor.accumulator.ir2 = (short)normal.y;
        Coprocessor.accumulator.ir3 = (short)normal.z;
        Coprocessor.ExecuteSQR(0, true);
        int mac1 = Coprocessor.mathsAccumulator.mac1; //r11
        int mac2 = Coprocessor.mathsAccumulator.mac2; //r12
        int mac3 = Coprocessor.mathsAccumulator.mac3; //r13
        int sum = mac1 + mac2 + mac3; //r2
        int zeroCount = LeadingZeros(sum) & -2; //not sure if accurate; r3
        int iVar1 = 31 - zeroCount >> 1; //r14
        int iVar2 = zeroCount - 24; //r11
        int iVar3; //r12

        if (iVar2 < 0)
        {
            iVar2 = 24 - zeroCount;
            iVar3 = sum >> iVar2;
        }
        else
            iVar3 = sum << iVar2;

        iVar3 = iVar3 - 64 << 1;
        int iVar4 = GameManager.UNK4[iVar3 / 2]; //r13
        Coprocessor.accumulator.ir0 = (short)iVar4;
        Coprocessor.accumulator.ir1 = (short)normal.x;
        Coprocessor.accumulator.ir2 = (short)normal.y;
        Coprocessor.accumulator.ir3 = (short)normal.z;
        Coprocessor.ExecuteGPF(0, false);
        mac1 = Coprocessor.mathsAccumulator.mac1 >> iVar1;
        mac2 = Coprocessor.mathsAccumulator.mac2 >> iVar1;
        mac3 = Coprocessor.mathsAccumulator.mac3 >> iVar1;

        return new Vector3Int(mac1, mac2, mac3);
    }

    public static float MoveDecimal(int value, int space)
    {
        return (float)(value / Math.Pow(10, space));
    }

    public static float MoveDecimal(long value, int space)
    {
        return (float)(value / Math.Pow(10, space));
    }
}