using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


public static class Utilities
{
    public static ushort DAT_10390 = 0;

    //0x80010084
    public static KeyValuePair<string, Type>[][] commonTypes =
    {
        //0x80010000
        new KeyValuePair<string, Type>[]
        {
            new KeyValuePair<string, Type>("SunLensFlare", typeof(SunLensFlare)),
            new KeyValuePair<string, Type>("PU_WeaponUpgrade", typeof(Pickup)),
            new KeyValuePair<string, Type>("PU_RadarJammer", typeof(Pickup)),
            new KeyValuePair<string, Type>("PU_Shield", typeof(Pickup)),
            new KeyValuePair<string, Type>("PU_Health", typeof(Pickup)),
            new KeyValuePair<string, Type>("I_RocktL", typeof(Pickup)),
            new KeyValuePair<string, Type>("I_MisslL", typeof(Pickup)),
            new KeyValuePair<string, Type>("I_Cannon", typeof(Pickup)),
            new KeyValuePair<string, Type>("I_Mortar", typeof(Pickup)),
            new KeyValuePair<string, Type>("I_MineDr", typeof(Pickup)),
            new KeyValuePair<string, Type>("I_FlThrower", typeof(Pickup)),
            new KeyValuePair<string, Type>("I_Special", typeof(Pickup)),
            new KeyValuePair<string, Type>("I_Surprise", typeof(Pickup)),
            new KeyValuePair<string, Type>("I_Hover", typeof(Pickup)),
            new KeyValuePair<string, Type>("I_Float", typeof(Pickup)),
            new KeyValuePair<string, Type>("I_Ski", typeof(Pickup))
        }
    };

    public static sbyte[] DAT_106E8 =
    {
        -1, -1, 2, 3, 4, 5, 6, 7, 8, 8, 9, 9
    };

    //0x800107A0
    public static KeyValuePair<int, Type>[] weaponTypes =
    {
        new KeyValuePair<int, Type>(-1, typeof(MGun)),
        new KeyValuePair<int, Type>(172, typeof(RocketL)),
        new KeyValuePair<int, Type>(184, typeof(MissileL)),
        new KeyValuePair<int, Type>(176, typeof(Cannon)),
        new KeyValuePair<int, Type>(179, typeof(Mortar)),
        new KeyValuePair<int, Type>(208, typeof(MineDr)),
        new KeyValuePair<int, Type>(206, typeof(FlameThrower))
    };

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

    public static uint FUN_14A54()
    {
        uint uVar1;

        uVar1 = (uint)DateTime.Now.Millisecond;
        return uVar1 | (uint)DAT_10390 << 16;
    }

    public static Type FUN_14DAC(KeyValuePair<string, Type>[] pairs, string cmp)
    {
        for (int i = 0; i < pairs.Length; i++)
            if (String.Compare(pairs[i].Key, cmp) == 0)
                return pairs[i].Value;

        return null;
    }

    public static Type FUN_14E1C(int i, string cmp)
    {
        return FUN_14DAC(commonTypes[i], cmp);
    }

    public static VigObject FUN_31D30(Type param1, XOBF_DB param2, short param3, uint param4)
    {
        if (param1.Equals(typeof(Destructible)) || param1.Equals(typeof(Pickup))
            || param1.Equals(typeof(RocketL)) || param1.Equals(typeof(MissileL)) 
            || param1.Equals(typeof(Cannon)) || param1.Equals(typeof(Mortar)) 
            || param1.Equals(typeof(MineDr)) || param1.Equals(typeof(FlameThrower))
            || param1.Equals(typeof(MGun)))
        {
            if (param2 == null || param3 == -1)
            {
                GameObject obj = new GameObject();
                VigObject comp = obj.AddComponent(param1) as VigObject;
                comp.vData = param2;
                return comp;
            }
            else
                return param2.ini.FUN_2C17C((ushort)param3, param1, param4);
        }
        else
            return null;
    }

    public static VigObject FUN_31D30(_VEHICLE_INIT param1, XOBF_DB param2, ushort param3, uint param4)
    {
        VigObject oVar1;

        if (param1 != null)
        {
            oVar1 = param1(param2, param3);

            if (oVar1 != null)
                return oVar1;

            return null;
        }

        return null;
    }

    public static int FUN_2E5B0(BoundingBox bbox, VigTransform t1, Radius radius, VigTransform t2)
    {
        long lVar1;
        long lVar2;
        long lVar3;
        uint uVar4;
        uint uVar5;
        uint uVar6;
        int iVar7;
        uint uVar8;
        uint uVar9;
        uint uVar10;
        uint uVar11;
        uint uVar12;
        uint uVar13;
        uint uVar14;
        uint uVar15;
        uint uVar16;
        uint uVar17;
        int iVar18;
        int iVar19;
        int iVar20;
        Vector3Int local_38;

        local_38 = ApplyMatrixSV(t2.rotation, radius.matrixSV);
        uVar4 = (ushort)local_38.x;
        uVar13 = (uint)((int)uVar4 << 16 >> 16);
        uVar8 = (uint)((t1.position.x + (bbox.min.x + bbox.max.x) / 2) - t2.position.x);
        uVar5 = (ushort)local_38.y;
        lVar1 = (long)((ulong)uVar13 * uVar8);
        uVar14 = (uint)((int)uVar5 << 16 >> 16);
        uVar9 = (uint)((t1.position.y + (bbox.min.y + bbox.max.y) / 2) - t2.position.y);
        uVar6 = (ushort)local_38.z;
        lVar2 = (long)((ulong)uVar14 * uVar9);
        uVar16 = (uint)lVar2;
        uVar15 = (uint)((int)uVar6 << 16 >> 16);
        uVar10 = (uint)((t1.position.z + (bbox.min.z + bbox.max.z) / 2) - t2.position.z);
        lVar3 = (long)((ulong)uVar15 * uVar10);
        uVar17 = (uint)lVar3;
        uVar11 = (uint)lVar1 + uVar16;
        uVar12 = uVar11 + uVar17;
        iVar7 = radius.contactOffset;
        local_38 = FUN_24238(t1.rotation, local_38);
        iVar18 = local_38.x * (bbox.max.x - bbox.min.x);
        iVar19 = local_38.y * (bbox.max.y - bbox.min.y);
        iVar20 = local_38.z * (bbox.max.z - bbox.min.z);

        if (iVar18 < 0)
            iVar18 = -iVar18;

        if (iVar19 < 0)
            iVar19 = -iVar19;

        if (iVar20 < 0)
            iVar20 = -iVar20;

        iVar20 = iVar18 + iVar19 + iVar20;

        if (iVar20 < 0)
            iVar20 += 0x1fff;

        return ((int)(uVar12 >> 12 |
                (uint)((int)((ulong)lVar1 >> 32) + (int)uVar13 * ((int)uVar8 >> 31) +
                (int)uVar8 * ((int)(uVar4 << 16) >> 31) +
                (int)((ulong)lVar2 >> 32) + (int)uVar14 * ((int)uVar9 >> 31) +
                (int)uVar9 * ((int)(uVar5 << 16) >> 31) + (uVar11 < uVar16 ? 1 : 0) +
                (int)((ulong)lVar3 >> 32) + (int)uVar15 * ((int)uVar10 >> 31) +
                (int)uVar10 * ((int)(uVar6 << 16) >> 31) + (uVar12 < uVar17 ? 1 : 0)) *
                0x100000) - iVar7) - (iVar20 >> 13);
    }

    public static uint FUN_2E2E8(BoundingBox param1, VigTransform param2, Radius param3, VigTransform param4)
    {
        long lVar1;
        long lVar2;
        long lVar3;
        uint uVar4;
        uint uVar5;
        uint uVar6;
        uint uVar7;
        uint uVar8;
        int iVar9;
        uint uVar10;
        int iVar11;
        uint uVar12;
        uint uVar13;
        uint uVar14;
        uint uVar15;
        Vector3Int local_48;
        Vector3Int local_40;
        int local_38;
        VigTransform local_30;

        local_30 = param2;
        local_48 = ApplyMatrixSV(param4.rotation, param3.matrixSV);
        uVar10 = (uint)(local_48.x << 16 >> 16);
        uVar4 = (uint)(local_30.position.x + (param1.min.x + param1.max.x) / 2 - param4.position.x);
        lVar1 = (long)((ulong)uVar10 * uVar4);
        uVar12 = (uint)(local_48.y << 16 >> 16);
        uVar5 = (uint)(local_30.position.y + (param1.min.y + param1.max.y) / 2 - param4.position.y);
        lVar2 = (long)((ulong)uVar12 * uVar5);
        uVar14 = (uint)lVar2;
        uVar13 = (uint)(local_48.z << 16 >> 16);
        uVar6 = (uint)(local_30.position.z + (param1.min.z + param1.max.z) / 2 - param4.position.z);
        lVar3 = (long)((ulong)uVar13 * uVar6);
        uVar15 = (uint)lVar3;
        uVar7 = (uint)lVar1 + uVar14;
        uVar8 = uVar7 + uVar15;
        iVar9 = (int)((ulong)lVar1 >> 32) + (int)uVar10 * ((int)uVar4 >> 31) +
                (int)uVar4 * (local_48.x << 16 >> 31) +
                (int)((ulong)lVar2 >> 32) + (int)uVar12 * ((int)uVar5 >> 31) +
                (int)uVar5 * (local_48.y << 16 >> 31) + (uVar7 < uVar14 ? 1 : 0) +
                (int)((ulong)lVar3 >> 32) + (int)uVar13 * ((int)uVar6 >> 31) +
                (int)uVar6 * (local_48.z << 16 >> 31) + (uVar8 < uVar15 ? 1 : 0);
        iVar11 = (int)(uVar8 >> 12 | (uint)(iVar9 * 0x100000)) - param3.contactOffset;

        if (iVar11 < 0)
            uVar4 = 1;
        else
        {
            local_40 = FUN_24210(local_30.rotation, local_48);
            local_40.x *= param1.max.x - param1.min.x;
            local_40.y *= param1.max.y - param1.min.y;
            local_40.z *= param1.max.z - param1.min.z;

            if (local_40.x < 0)
                local_40.x = -local_40.x;

            if (local_40.y < 0)
                local_40.y = -local_40.y;

            if (local_40.z < 0)
                local_40.z = -local_40.z;

            local_38 = local_40.x + local_40.y + local_40.z;

            if (local_38 < 0)
                local_38 += 0x1fff;

            uVar4 = (uint)(iVar11 - (local_38 >> 13)) >> 31;
        }

        return uVar4;
    }

    public static bool FUN_281FC(BoundingBox param1, VigTransform param2, BoundingBox param3, VigTransform param4)
    {
        int iVar1;
        int iVar2;
        int iVar3;
        int iVar4;
        int iVar5;
        uint uVar6;
        uint uVar7;
        int iVar8;
        uint uVar9;
        uint uVar10;
        int iVar11;
        int iVar12;
        int iVar13;
        int iVar14;
        int iVar15;
        int iVar16;
        uint uVar17;
        int iVar18;
        int iVar19;
        int iVar20;

        uVar6 = (ushort)param2.rotation.V00;
        uVar10 = (ushort)param2.rotation.V02;
        Coprocessor.rotationMatrix.rt11 = (short)uVar6;
        Coprocessor.rotationMatrix.rt12 = param2.rotation.V10;
        uVar7 = (ushort)param2.rotation.V11;
        Coprocessor.rotationMatrix.rt31 = (short)uVar10;
        Coprocessor.rotationMatrix.rt32 = param2.rotation.V12;
        uVar10 = (ushort)param2.rotation.V20;
        Coprocessor.rotationMatrix.rt13 = (short)uVar10;
        Coprocessor.rotationMatrix.rt21 = param2.rotation.V01;
        Coprocessor.rotationMatrix.rt22 = (short)uVar7;
        Coprocessor.rotationMatrix.rt23 = param2.rotation.V21;
        Coprocessor.rotationMatrix.rt33 = param2.rotation.V22;
        Coprocessor.accumulator.ir1 = (short)((param4.position.x - param2.position.x) >> 15);
        Coprocessor.accumulator.ir2 = (short)((param4.position.y - param2.position.y) >> 15);
        Coprocessor.accumulator.ir3 = (short)((param4.position.z - param2.position.z) >> 15);
        Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.IR, _MVMVA_TRANSLATION_VECTOR.None, 0, false);
        Coprocessor.vector0.vx0 = (short)(param4.position.x - param2.position.x & 0x7fff);
        Coprocessor.vector0.vy0 = (short)(param4.position.y - param2.position.y & 0x7fff);
        Coprocessor.vector0.vz0 = (short)(param4.position.z - param2.position.z & 0x7fff);
        iVar3 = Coprocessor.mathsAccumulator.mac1;
        iVar4 = Coprocessor.mathsAccumulator.mac2;
        iVar8 = Coprocessor.mathsAccumulator.mac3;
        Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.V0, _MVMVA_TRANSLATION_VECTOR.None, 12, false);
        iVar1 = Coprocessor.mathsAccumulator.mac1;
        iVar2 = Coprocessor.mathsAccumulator.mac2;
        iVar20 = Coprocessor.mathsAccumulator.mac3;
        Coprocessor.vector0.vx0 = param4.rotation.V00;
        Coprocessor.vector0.vy0 = param4.rotation.V10;
        Coprocessor.vector0.vz0 = param4.rotation.V20;
        Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.V0, _MVMVA_TRANSLATION_VECTOR.None, 12, false);
        uVar7 = (ushort)Coprocessor.accumulator.ir1;
        iVar12 = Coprocessor.accumulator.ir2;
        uVar10 = (ushort)Coprocessor.accumulator.ir3;
        Coprocessor.vector0.vx0 = param4.rotation.V01;
        Coprocessor.vector0.vy0 = param4.rotation.V11;
        Coprocessor.vector0.vz0 = param4.rotation.V21;
        Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.V0, _MVMVA_TRANSLATION_VECTOR.None, 12, false);
        iVar15 = Coprocessor.accumulator.ir1;
        uVar17 = (ushort)Coprocessor.accumulator.ir2;
        iVar19 = Coprocessor.accumulator.ir3;
        Coprocessor.vector0.vx0 = param4.rotation.V02;
        Coprocessor.vector0.vy0 = param4.rotation.V12;
        Coprocessor.vector0.vz0 = param4.rotation.V22;
        Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.V0, _MVMVA_TRANSLATION_VECTOR.None, 12, false);
        uVar6 = (ushort)Coprocessor.accumulator.ir1;
        iVar5 = Coprocessor.accumulator.ir2;
        uVar9 = (ushort)Coprocessor.accumulator.ir3;
        Coprocessor.rotationMatrix.rt11 = (short)uVar7;
        Coprocessor.rotationMatrix.rt12 = (short)iVar15;
        Coprocessor.rotationMatrix.rt31 = (short)uVar10;
        Coprocessor.rotationMatrix.rt32 = (short)iVar19;
        Coprocessor.rotationMatrix.rt13 = (short)uVar6;
        Coprocessor.rotationMatrix.rt21 = (short)iVar12;
        Coprocessor.rotationMatrix.rt22 = (short)uVar17;
        Coprocessor.rotationMatrix.rt23 = (short)iVar5;
        Coprocessor.rotationMatrix.rt33 = (short)uVar9;
        iVar5 = param3.min.x + param3.max.x;
        iVar12 = param3.min.y + param3.max.y;
        iVar15 = param3.min.z + param3.max.z;
        iVar19 = param3.max.x - param3.min.x;
        iVar11 = param3.max.y - param3.min.y;
        iVar13 = param3.max.z - param3.min.z;
        Coprocessor.accumulator.ir1 = (short)(iVar5 >> 16);
        Coprocessor.accumulator.ir2 = (short)(iVar12 >> 16);
        Coprocessor.accumulator.ir3 = (short)(iVar15 >> 16);
        Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.IR, _MVMVA_TRANSLATION_VECTOR.None, 0, false);
        Coprocessor.vector0.vx0 = (short)(iVar5 >> 1 & 0x7fff);
        Coprocessor.vector0.vy0 = (short)(iVar12 >> 1 & 0x7fff);
        Coprocessor.vector0.vz0 = (short)(iVar15 >> 1 & 0x7fff);
        iVar5 = Coprocessor.mathsAccumulator.mac1;
        iVar12 = Coprocessor.mathsAccumulator.mac2;
        iVar15 = Coprocessor.mathsAccumulator.mac3;
        Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.V0, _MVMVA_TRANSLATION_VECTOR.None, 12, false);
        iVar14 = Coprocessor.mathsAccumulator.mac1;
        iVar16 = Coprocessor.mathsAccumulator.mac2;
        iVar18 = Coprocessor.mathsAccumulator.mac3;
        iVar14 = iVar14 + iVar5 * 8 + iVar1 + iVar3 * 8;
        iVar16 = iVar16 + iVar12 * 8 + iVar2 + iVar4 * 8;
        iVar8 = iVar18 + iVar15 * 8 + iVar20 + iVar8 * 8;
        uVar6 = (uint)(ushort)Coprocessor.rotationMatrix.rt12 << 16 | (ushort)Coprocessor.rotationMatrix.rt11;
        uVar10 = (uVar6 & 0x80008000) >> 15;
        uVar7 = (uint)(ushort)Coprocessor.rotationMatrix.rt21 << 16 | (ushort)Coprocessor.rotationMatrix.rt13;
        uint cop2r32 = uVar10 + (uVar6 ^ (uVar6 & 0x80008000) * 2 - uVar10);
        Coprocessor.rotationMatrix.rt11 = (short)cop2r32;
        Coprocessor.rotationMatrix.rt12 = (short)(cop2r32 >> 16);
        uVar10 = (uVar7 & 0x80008000) >> 15;
        uVar6 = (uint)(ushort)Coprocessor.rotationMatrix.rt23 << 16 | (ushort)Coprocessor.rotationMatrix.rt22;
        uint cop2r33 = uVar10 + (uVar7 ^ (uVar7 & 0x80008000) * 2 - uVar10);
        Coprocessor.rotationMatrix.rt13 = (short)cop2r33;
        Coprocessor.rotationMatrix.rt21 = (short)(cop2r33 >> 16);
        uVar10 = (uVar6 & 0x80008000) >> 15;
        uVar7 = (uint)(ushort)Coprocessor.rotationMatrix.rt32 << 16 | (ushort)Coprocessor.rotationMatrix.rt31;
        uint cop2r34 = uVar10 + (uVar6 ^ (uVar6 & 0x80008000) * 2 - uVar10);
        Coprocessor.rotationMatrix.rt22 = (short)cop2r34;
        Coprocessor.rotationMatrix.rt23 = (short)(cop2r34 >> 16);
        uVar10 = (uVar7 & 0x80008000) >> 15;
        uVar6 = (ushort)Coprocessor.rotationMatrix.rt33;
        uint cop2r35 = uVar10 + (uVar7 ^ (uVar7 & 0x80008000) * 2 - uVar10);
        Coprocessor.rotationMatrix.rt31 = (short)cop2r35;
        Coprocessor.rotationMatrix.rt32 = (short)(cop2r35 >> 16);
        uVar7 = (uint)((int)(uVar6 << 16) >> 31);
        Coprocessor.rotationMatrix.rt33 = (short)((uVar6 ^ uVar7) - uVar7);
        Coprocessor.accumulator.ir1 = (short)(iVar19 >> 16);
        Coprocessor.accumulator.ir2 = (short)(iVar11 >> 16);
        Coprocessor.accumulator.ir3 = (short)(iVar13 >> 16);
        Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.IR, _MVMVA_TRANSLATION_VECTOR.None, 0, false);
        Coprocessor.vector0.vx0 = (short)(iVar19 >> 1 & 0x7fff);
        Coprocessor.vector0.vy0 = (short)(iVar11 >> 1 & 0x7fff);
        Coprocessor.vector0.vz0 = (short)(iVar13 >> 1 & 0x7fff);
        iVar1 = Coprocessor.mathsAccumulator.mac1;
        iVar2 = Coprocessor.mathsAccumulator.mac2;
        iVar3 = Coprocessor.mathsAccumulator.mac3;
        Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.V0, _MVMVA_TRANSLATION_VECTOR.None, 12, false);
        iVar5 = Coprocessor.mathsAccumulator.mac1;
        iVar4 = Coprocessor.mathsAccumulator.mac2;
        iVar12 = Coprocessor.mathsAccumulator.mac3;
        iVar5 = iVar5 + iVar1 * 8;
        iVar4 = iVar4 + iVar2 * 8;
        iVar12 = iVar12 + iVar3 * 8;

        if (-1 < (iVar14 + iVar5) - param1.min.x && -1 < (iVar16 + iVar4) - param1.min.y && 
            -1 < (iVar8 + iVar12) - param1.min.z)
        {
            if ((iVar14 - iVar5) - param1.max.x < 1 && (iVar16 - iVar4) - param1.max.y < 1)
                return iVar8 - iVar12 < param1.max.z;
        }

        return false;
    }

    public static int FUN_29A9C(List<VigTuple> tuple)
    {
        return tuple.Count;
    }

    public static int FUN_29C5C(Vector3Int v1, Vector3Int v2)
    {
        int iVar1;
        int iVar2;
        int iVar3;

        iVar1 = v1.x * v2.x;
        iVar2 = v1.y * v2.y;
        iVar3 = v1.z * v2.z;

        return iVar1 + iVar2 + iVar3;
    }

    public static int FUN_29DDC(Vector3Int v3)
    {
        Coprocessor.accumulator.ir1 = (short)v3.x;
        Coprocessor.accumulator.ir2 = (short)v3.y;
        Coprocessor.accumulator.ir3 = (short)v3.z;
        Coprocessor.ExecuteSQR(0, true);

        return Coprocessor.mathsAccumulator.mac1 +
               Coprocessor.mathsAccumulator.mac2 +
               Coprocessor.mathsAccumulator.mac3;
    }

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

    public static int FUN_29F6C(Vector3Int v1, Vector3Int v2)
    {
        return FUN_29E84(new Vector3Int
            (v1.x - v2.x, v1.y - v2.y, v1.z - v2.z));
    }

    public static int FUN_29FC8(Vector3Int vin, out Vector3Int vout)
    {
        int iVar1;
        int iVar2;
        uint uVar3;

        iVar1 = FUN_29E84(vin);

        if (iVar1 == 0)
            vout = new Vector3Int(0, 0, 0);
        else
        {
            iVar2 = LeadingZeros(iVar1);
            uVar3 = 12;

            if (iVar2 - 1 < 12)
                uVar3 = (uint)iVar2 - 1;

            iVar2 = iVar1 >> (int)(12 - uVar3 & 31);
            vout = new Vector3Int(
                (vin.x << (int)(uVar3 & 31)) / iVar2,
                (vin.y << (int)(uVar3 & 31)) / iVar2,
                (vin.z << (int)(uVar3 & 31)) / iVar2);
        }

        return iVar1;
    }

    public static int FUN_2A098(Vector3Int vin, out Vector3Int vout)
    {
        int iVar1;
        int iVar2;
        uint uVar3;

        iVar1 = FUN_29E84(vin);

        if (iVar1 == 0)
            vout = new Vector3Int(0, 0, 0);
        else
        {
            iVar2 = LeadingZeros(iVar1);
            uVar3 = 12;

            if (iVar2 - 1 < 12)
                uVar3 = (uint)iVar2 - 1;

            iVar2 = iVar1 >> (int)(12 - uVar3 & 31);
            vout = new Vector3Int(
                (vin.x << (int)(uVar3 & 31)) / iVar2,
                (vin.y << (int)(uVar3 & 31)) / iVar2,
                (vin.z << (int)(uVar3 & 31)) / iVar2);
        }

        return iVar1;
    }

    public static int FUN_2A168(out Vector3Int vout, Vector3Int v1, Vector3Int v2)
    {
        Vector3Int local_8;

        local_8 = new Vector3Int
            (v1.x - v2.x,
             v1.y - v2.y,
             v1.z - v2.z);
        return FUN_29FC8(local_8, out vout);
    }

    public static long FUN_2AD3C(Vector3Int v3, Vector3Int sv3)
    {
        long lVar1;
        long lVar2;
        long lVar3;
        int iVar4;
        int iVar5;

        lVar1 = (long)v3.x * sv3.x;
        lVar2 = (long)v3.y * sv3.y;
        lVar3 = (long)v3.z * sv3.z;
        iVar4 = (int)lVar1 + (int)lVar2;
        iVar5 = (int)(lVar1 >> 32) + (int)(lVar2 >> 32)
                + ((uint)iVar4 < (uint)lVar2 ? 1 : 0);
        iVar4 += (int)lVar3;
        iVar5 += (int)(lVar3 >> 32) + ((uint)iVar4 < (uint)lVar3 ? 1 : 0);
        return (long)iVar5 << 32 | (uint)iVar4;
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

    public static int FUN_2ABC4(uint param1, int param2)
    {
        int iVar1;
        long lVar2;
        uint uVar3;

        iVar1 = LeadingZeros(param2);
        uVar3 = (uint)(35 - iVar1 >> 1);

        if ((int)(uVar3 << 27) < 0)
            param1 = (uint)(param2 >> (int)(uVar3 * 2 & 31));
        else
        {
            param1 = param1 >> (int)(uVar3 * 2 & 31);

            if (uVar3 << 27 != 0)
                param1 = param1 | (uint)param2 << (int)(uVar3 * -2 & 31);
        }

        lVar2 = SquareRoot(param1);
        return (int)lVar2 << (int)(uVar3 & 31);
    }

    public static Matrix3x3 FUN_2A4A4(Matrix3x3 m33)
    {
        long lVar1;
        int iVar2;
        int iVar3;
        int iVar4;
        int iVar5;

        lVar1 = SquareRoot(m33.V00 * m33.V00 + m33.V20 * m33.V20);
        iVar4 = m33.V00 * (0x1000000 / (int)lVar1);

        if (iVar4 < 0)
            iVar4 += 4095;

        iVar5 = m33.V20 * (0x1000000 / (int)lVar1);
        iVar4 = iVar4 >> 12;

        if (iVar5 < 0)
            iVar5 += 4095;

        if (m33.V11 < 0)
            iVar4 = -iVar4;

        m33.V22 = (short)lVar1;
        iVar2 = iVar4 * m33.V10 - iVar5 * m33.V12;
        m33.V20 = 0;

        if (iVar2 < 0)
            iVar2 += 4095;

        iVar3 = iVar5 * m33.V00 - iVar5 * m33.V02;
        m33.V10 = (short)(iVar2 >> 12);

        if (iVar3 < 0)
            iVar3 += 4095;

        iVar4 = iVar4 * m33.V00 - iVar5 * m33.V02;
        m33.V12 = (short)(iVar3 >> 12);

        if (iVar4 < 0)
            iVar4 += 4095;

        m33.V00 = (short)(iVar4 >> 12);
        m33.V02 = 0;

        return m33;
    }

    public static VigTransform FUN_2A3EC(VigTransform transform)
    {
        int iVar1;
        VigTransform tout;

        tout.rotation = TransposeMatrix(transform.rotation);
        tout.padding = 0;
        tout.position = FUN_24094(tout.rotation, transform.position);
        iVar1 = tout.position.z;
        tout.position.x = -tout.position.x;
        tout.position.z = -iVar1;
        tout.position.y = -tout.position.y;

        return tout;
    }

    public static short FUN_2A248(Matrix3x3 m33)
    {
        return (short)-Ratan2(m33.V12, m33.V22);
    }

    public static short FUN_2A27C(Matrix3x3 m33)
    {
        return (short)Ratan2(m33.V02, m33.V22);
    }

    public static int FUN_2A2AC(Matrix3x3 m33)
    {
        return -Ratan2(m33.V10, m33.V11) << 16 >> 16;
    }

    public static Matrix3x3 FUN_2A5EC(Vector3Int sv)
    {
        Vector3Int local_18;
        Vector3Int local_8;

        local_8 = new Vector3Int(-sv.x, -sv.y, -sv.z);
        
        if (sv.x == 0 && sv.y == 0)
            local_18 = new Vector3Int(0x1000, 0, 0);
        else
        {
            local_18 = new Vector3Int(-sv.y, sv.x, 0);
            local_18 = Utilities.VectorNormal(local_18);
        }

        Coprocessor.rotationMatrix.rt11 = (short)local_18.x;
        Coprocessor.rotationMatrix.rt12 = (short)(local_18.x >> 16);
        Coprocessor.rotationMatrix.rt22 = (short)local_18.y;
        Coprocessor.rotationMatrix.rt23 = (short)(local_18.y >> 16);
        Coprocessor.rotationMatrix.rt33 = (short)local_18.z;
        Coprocessor.accumulator.ir1 = (short)local_8.x;
        Coprocessor.accumulator.ir2 = (short)local_8.y;
        Coprocessor.accumulator.ir3 = (short)local_8.z;
        Coprocessor.ExecuteOP(12, false);
        return new Matrix3x3()
        {
            V00 = (short)local_18.x,
            V10 = (short)local_18.y,
            V20 = (short)local_18.z,
            V01 = (short)local_8.x,
            V11 = (short)local_8.y,
            V21 = (short)local_8.z,
            V02 = (short)Coprocessor.mathsAccumulator.mac1,
            V12 = (short)Coprocessor.mathsAccumulator.mac2,
            V22 = (short)Coprocessor.mathsAccumulator.mac3
        };
    }

    public static Matrix3x3 FUN_2A724(Vector3Int sv)
    {
        short sVar1;

        Vector3Int local_18;
        short local_8;
        short local_4;

        local_8 = (short)sv.x;
        local_4 = (short)sv.y;
        sVar1 = (short)sv.z;
        local_18 = new Vector3Int();
        local_18.x = sv.z;

        if (local_18.x == 0)
        {
            if (sv.x == 0)
            {
                local_18.x = 0x1000;
                local_18.y = 0;
                local_18.z = 0;
                goto LAB_2A7AC;
            }

            local_18.x = sv.z;
        }

        local_18.y = 0;
        local_18.z = -sv.x;
        local_18 = VectorNormal(local_18);
        LAB_2A7AC:
        Coprocessor.rotationMatrix.rt11 = local_8;
        Coprocessor.rotationMatrix.rt12 = (short)(local_8 >> 16);
        Coprocessor.rotationMatrix.rt22 = local_4;
        Coprocessor.rotationMatrix.rt23 = (short)(local_4 >> 16);
        Coprocessor.rotationMatrix.rt33 = sVar1;
        Coprocessor.accumulator.ir3 = (short)local_18.z;
        Coprocessor.accumulator.ir1 = (short)local_18.x;
        Coprocessor.accumulator.ir2 = (short)local_18.y;
        Coprocessor.ExecuteOP(12, false);

        return new Matrix3x3()
        {
            V00 = (short)local_18.x,
            V10 = (short)local_18.y,
            V20 = (short)local_18.z,
            V02 = local_8,
            V12 = local_4,
            V22 = sVar1,
            V01 = (short)Coprocessor.mathsAccumulator.mac1,
            V11 = (short)Coprocessor.mathsAccumulator.mac2,
            V21 = (short)Coprocessor.mathsAccumulator.mac3
        };
    }

    public static Matrix3x3 FUN_2ADB0(Matrix3x3 m33, Vector3Int v3)
    {
        Matrix3x3 local_18;

        local_18 = new Matrix3x3()
        {
            V22 = 0x1000,
            V11 = 0x1000,
            V00 = 0x1000,
            V21 = (short)v3.x,
            V12 = (short)-v3.x,
            V02 = (short)v3.y,
            V20 = (short)-v3.y,
            V10 = (short)v3.z,
            V01 = (short)-v3.z
        };

        return FUN_247C4(local_18, m33);
    }

    public static VigTransform FUN_2C77C(ConfigContainer conf)
    {
        VigTransform transf = new VigTransform();
        transf.rotation = RotMatrixYXZ_gte(conf.v3_2);
        transf.position.x = conf.v3_1.x;
        transf.position.y = conf.v3_1.y;
        transf.position.z = conf.v3_1.z;
        return transf;
    }

    public static void FUN_2CA94(VigObject obj1, ConfigContainer cont, VigObject obj2)
    {
        obj2.vr = cont.v3_2;
        obj2.screen = cont.v3_1;
        obj2.ApplyTransformation();
        FUN_2CC48(obj1, obj2);
    }

    public static void FUN_2CC48(VigObject obj1, VigObject obj2)
    {
        VigObject oVar1;
        VigObject oVar2;

        oVar1 = obj1.child2;

        if (oVar1 != null)
        {
            oVar2 = oVar1.child;

            while (oVar2 != null)
            {
                oVar1 = oVar1.child;
                oVar2 = oVar1.child;
            }

            oVar1.child = obj2;
            obj2.parent = oVar1;
            return;
        }

        obj1.child2 = obj2;
        obj2.parent = obj1;
    }

    public static void ParentChildren(VigObject obj, VigObject parent)
    {
        VigObject child2 = obj.child2;

        if (child2 != null)
        {
            child2.transform.parent = parent.transform;
            ParentChildren(child2, parent);
        }

        VigObject child = obj.child;

        if (child != null)
        {
            child.transform.parent = parent.transform;
            ParentChildren(child, parent);
        }
    }

    public static void ParentChildren2(VigObject obj, VigObject parent)
    {
        VigObject child2 = obj.child2;

        if (child2 != null)
        {
            child2.transform.parent = obj.transform;
            ParentChildren2(child2, obj);
        }

        VigObject child = obj.child;
        
        if (child != null)
        {
            child.transform.parent = parent.transform;
            ParentChildren2(child, parent);
        }
    }

    public static void ResetMesh(VigObject obj)
    {
        VigMesh mesh;
        VigMesh mLOD;

        mesh = obj.vMesh;
        mLOD = obj.vLOD;

        if (mesh != null)
            mesh.ClearMeshData();

        if (mLOD != null)
            mLOD.ClearMeshData();

        VigObject child2 = obj.child2;

        if (child2 != null)
        {
            ResetMesh(child2);
        }

        VigObject child = obj.child;

        if (child != null)
        {
            ResetMesh(child);
        }
    }

    public static void FUN_2CC9C(VigObject obj1, VigObject obj2)
    {
        VigObject oVar1;

        oVar1 = obj1.child2;
        obj1.child2 = obj2;
        obj2.parent = obj1;
        obj2.child = oVar1;

        if (oVar1 != null)
            oVar1.parent = obj2;
    }

    public static VigObject FUN_2CD78(VigObject obj)
    {
        VigObject oVar1;
        VigObject oVar2;

        oVar2 = obj.parent;
        oVar1 = oVar2;

        while(oVar1 != null && oVar1.child2 != obj)
        {
            oVar2 = oVar1.parent;
            obj = oVar1;
            oVar1 = oVar2;
        }

        return oVar1;
    }

    public static VigObject FUN_2CDB0(VigObject obj)
    {
        VigObject oVar1;

        oVar1 = obj.parent;

        while (oVar1 != null)
        {
            obj = FUN_2CD78(obj);
            oVar1 = obj.parent;
        }

        return obj;
    }

    public static Vector3Int FUN_23F58(Vector3Int v3)
    {
        Coprocessor.vector0.vx0 = (short)v3.x;
        Coprocessor.vector0.vy0 = (short)v3.y;
        Coprocessor.vector0.vz0 = (short)v3.z;
        Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.V0, _MVMVA_TRANSLATION_VECTOR.TR, 12, false);
        return new Vector3Int(
            Coprocessor.mathsAccumulator.mac1, 
            Coprocessor.mathsAccumulator.mac2, 
            Coprocessor.mathsAccumulator.mac3);
    }

    public static Vector3Int FUN_23F7C(Vector3Int v3)
    {
        int iVar1;
        int iVar2;
        int iVar3;
        int iVar4;
        int iVar5;
        int iVar6;

        Coprocessor.accumulator.ir1 = (short)(v3.x >> 15);
        Coprocessor.accumulator.ir2 = (short)(v3.y >> 15);
        Coprocessor.accumulator.ir3 = (short)(v3.z >> 15);
        Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.IR, _MVMVA_TRANSLATION_VECTOR.None, 0, false);
        Coprocessor.vector0.vx0 = (short)(v3.x & 0x7fff);
        Coprocessor.vector0.vy0 = (short)(v3.y & 0x7fff);
        Coprocessor.vector0.vz0 = (short)(v3.z & 0x7fff);
        iVar4 = Coprocessor.mathsAccumulator.mac1;
        iVar5 = Coprocessor.mathsAccumulator.mac2;
        iVar6 = Coprocessor.mathsAccumulator.mac3;
        Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.V0, _MVMVA_TRANSLATION_VECTOR.None, 12, false);
        iVar1 = Coprocessor.mathsAccumulator.mac1;
        iVar2 = Coprocessor.mathsAccumulator.mac2;
        iVar3 = Coprocessor.mathsAccumulator.mac3;

        return new Vector3Int
            (iVar1 + iVar4 * 8,
             iVar2 + iVar5 * 8,
             iVar3 + iVar6 * 8);
    }

    public static void FUN_246BC(VigTransform transform)
    {
        Coprocessor.rotationMatrix.rt11 = transform.rotation.V00;
        Coprocessor.rotationMatrix.rt12 = transform.rotation.V01;
        Coprocessor.rotationMatrix.rt13 = transform.rotation.V02;
        Coprocessor.rotationMatrix.rt21 = transform.rotation.V10;
        Coprocessor.rotationMatrix.rt22 = transform.rotation.V11;
        Coprocessor.rotationMatrix.rt23 = transform.rotation.V12;
        Coprocessor.rotationMatrix.rt31 = transform.rotation.V20;
        Coprocessor.rotationMatrix.rt32 = transform.rotation.V21;
        Coprocessor.rotationMatrix.rt33 = transform.rotation.V22;
        Coprocessor.translationVector._trx = transform.position.x;
        Coprocessor.translationVector._try = transform.position.y;
        Coprocessor.translationVector._trz = transform.position.z;
    }

    public static Matrix3x3 FUN_247C4(Matrix3x3 m1, Matrix3x3 m2)
    {
        Coprocessor.rotationMatrix.rt11 = m1.V00;
        Coprocessor.rotationMatrix.rt12 = m1.V01;
        Coprocessor.rotationMatrix.rt13 = m1.V02;
        Coprocessor.rotationMatrix.rt21 = m1.V10;
        Coprocessor.rotationMatrix.rt22 = m1.V11;
        Coprocessor.rotationMatrix.rt23 = m1.V12;
        Coprocessor.rotationMatrix.rt31 = m1.V20;
        Coprocessor.rotationMatrix.rt32 = m1.V21;
        Coprocessor.rotationMatrix.rt33 = m1.V22;

        return FUN_247F4(m2);
    }

    public static Matrix3x3 FUN_247F4(Matrix3x3 m33)
    {
        Coprocessor.vector0.vx0 = m33.V00;
        Coprocessor.vector0.vy0 = m33.V10;
        Coprocessor.vector0.vz0 = m33.V20;
        Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.V0, _MVMVA_TRANSLATION_VECTOR.None, 12, false);

        Coprocessor.vector1.vx1 = m33.V01;
        Coprocessor.vector1.vy1 = m33.V12;
        Coprocessor.vector1.vz1 = m33.V21;
        short IR1_1 = Coprocessor.accumulator.ir1;
        short IR2_1 = Coprocessor.accumulator.ir2;
        short IR3_1 = Coprocessor.accumulator.ir3;
        Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.V1, _MVMVA_TRANSLATION_VECTOR.None, 12, false);

        Coprocessor.vector0.vx0 = m33.V02;
        Coprocessor.vector0.vy0 = m33.V12;
        Coprocessor.vector0.vz0 = m33.V21;
        short IR1_2 = Coprocessor.accumulator.ir1;
        short IR2_2 = Coprocessor.accumulator.ir2;
        short IR3_2 = Coprocessor.accumulator.ir3;
        Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.V0, _MVMVA_TRANSLATION_VECTOR.None, 12, false);

        short IR1_3 = Coprocessor.accumulator.ir1;
        short IR2_3 = Coprocessor.accumulator.ir2;
        short IR3_3 = Coprocessor.accumulator.ir3;

        return new Matrix3x3
        {
            V00 = IR1_1,
            V01 = IR1_2,
            V02 = IR1_3,
            V10 = IR2_1,
            V11 = IR2_2,
            V12 = IR2_3,
            V20 = IR3_1,
            V21 = IR3_2,
            V22 = IR3_3
        };
    }

    public static Vector3Int FUN_24008(Vector3Int v3)
    {
        int iVar1;
        int iVar2;
        int iVar3;
        int iVar4;
        int iVar5;
        int iVar6;

        Coprocessor.accumulator.ir1 = (short)(v3.x >> 15);
        Coprocessor.accumulator.ir2 = (short)(v3.y >> 15);
        Coprocessor.accumulator.ir3 = (short)(v3.z >> 15);
        Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.IR, _MVMVA_TRANSLATION_VECTOR.None, 0, false);
        Coprocessor.vector0.vx0 = (short)(v3.x & 0x7fff);
        Coprocessor.vector0.vy0 = (short)(v3.y & 0x7fff);
        Coprocessor.vector0.vz0 = (short)(v3.z & 0x7fff);
        iVar4 = Coprocessor.mathsAccumulator.mac1;
        iVar5 = Coprocessor.mathsAccumulator.mac2;
        iVar6 = Coprocessor.mathsAccumulator.mac3;
        Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.V0, _MVMVA_TRANSLATION_VECTOR.TR, 12, false);
        iVar1 = Coprocessor.mathsAccumulator.mac1;
        iVar2 = Coprocessor.mathsAccumulator.mac2;
        iVar3 = Coprocessor.mathsAccumulator.mac3;
        return new Vector3Int(iVar1 + iVar4 * 8, iVar2 + iVar5 * 8, iVar3 + iVar6 * 8);
    }

    public static Vector3Int FUN_2426C(Matrix3x3 m33, Matrix2x4 m24)
    {
        FUN_243B4(m33);

        int iVar1 = m24.X; //r8
        int iVar2 = m24.Y; //r9
        int iVar3 = m24.Z; //r10
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

    public static Matrix3x3 FUN_245AC(Matrix3x3 m33, Vector3Int sv)
    {
        int iVar3;
        int iVar4;
        int iVar5;

        iVar3 = sv.x;
        iVar4 = sv.y;
        iVar5 = sv.z;

        return new Matrix3x3()
        {
            V00 = (short)(m33.V00 * iVar3 >> 12),
            V01 = (short)(m33.V01 * iVar4 >> 12),
            V02 = (short)(m33.V02 * iVar5 >> 12),
            V10 = (short)(m33.V10 * iVar3 >> 12),
            V11 = (short)(m33.V11 * iVar4 >> 12),
            V12 = (short)(m33.V12 * iVar5 >> 12),
            V20 = (short)(m33.V20 * iVar3 >> 12),
            V21 = (short)(m33.V21 * iVar4 >> 12),
            V22 = (short)(m33.V22 * iVar5 >> 12)
        };
    }

    public static Matrix3x3 FUN_2449C(Matrix3x3 m33, Vector3Int v3)
    {
        return new Matrix3x3()
        {
            V00 = (short)((int)((uint)m33.V00 * (uint)v3.x) >> 12),
            V01 = (short)((int)((uint)m33.V01 * (uint)v3.y) >> 12),
            V02 = (short)((int)((uint)m33.V02 * (uint)v3.z) >> 12),
            V10 = (short)((int)((uint)m33.V10 * (uint)v3.x) >> 12),
            V11 = (short)((int)((uint)m33.V11 * (uint)v3.y) >> 12),
            V12 = (short)((int)((uint)m33.V12 * (uint)v3.z) >> 12),
            V20 = (short)((int)((uint)m33.V20 * (uint)v3.x) >> 12),
            V21 = (short)((int)((uint)m33.V21 * (uint)v3.y) >> 12),
            V22 = (short)((int)((uint)m33.V22 * (uint)v3.z) >> 12)
        };
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

    public static Vector3Int FUN_24304(VigTransform transform, Vector3Int v)
    {
        FUN_243B4(transform.rotation);

        int iVar1 = v.x - transform.position.x;
        int iVar2 = v.y - transform.position.y;
        int iVar3 = v.z - transform.position.z;
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

    public static Vector3Int FUN_24148(VigTransform transform, Vector3Int v)
    {
        Coprocessor.rotationMatrix.rt11 = transform.rotation.V00;
        Coprocessor.rotationMatrix.rt12 = transform.rotation.V01;
        Coprocessor.rotationMatrix.rt13 = transform.rotation.V02;
        Coprocessor.rotationMatrix.rt21 = transform.rotation.V10;
        Coprocessor.rotationMatrix.rt22 = transform.rotation.V11;
        Coprocessor.rotationMatrix.rt23 = transform.rotation.V12;
        Coprocessor.rotationMatrix.rt31 = transform.rotation.V20;
        Coprocessor.rotationMatrix.rt32 = transform.rotation.V21;
        Coprocessor.rotationMatrix.rt33 = transform.rotation.V22;
        Coprocessor.accumulator.ir1 = (short)(v.x >> 15);
        Coprocessor.accumulator.ir2 = (short)(v.y >> 15);
        Coprocessor.accumulator.ir3 = (short)(v.z >> 15);
        Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.IR, _MVMVA_TRANSLATION_VECTOR.None, 0, false);

        int mac1_1 = Coprocessor.mathsAccumulator.mac1 << 3;
        int mac2_1 = Coprocessor.mathsAccumulator.mac2 << 3;
        int mac3_1 = Coprocessor.mathsAccumulator.mac3 << 3;

        Coprocessor.translationVector._trx = transform.position.x;
        Coprocessor.translationVector._try = transform.position.y;
        Coprocessor.translationVector._trz = transform.position.z;
        Coprocessor.accumulator.ir1 = (short)(v.x & 0x7fff);
        Coprocessor.accumulator.ir2 = (short)(v.y & 0x7fff);
        Coprocessor.accumulator.ir3 = (short)(v.z & 0x7fff);
        Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.IR, _MVMVA_TRANSLATION_VECTOR.TR, 12, false);

        int mac1_2 = Coprocessor.mathsAccumulator.mac1 + mac1_1;
        int mac2_2 = Coprocessor.mathsAccumulator.mac2 + mac2_1;
        int mac3_2 = Coprocessor.mathsAccumulator.mac3 + mac3_1;

        return new Vector3Int(mac1_2, mac2_2, mac3_2);
    }

    public static Vector3Int FUN_24238(Matrix3x3 m33, Vector3Int v3)
    {
        Coprocessor.vector0.vx0 = (short)v3.x;
        Coprocessor.vector0.vy0 = (short)v3.y;
        Coprocessor.vector0.vz0 = (short)v3.z;
        FUN_243B4(m33);
        Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.V0, _MVMVA_TRANSLATION_VECTOR.None, 12, false);

        return new Vector3Int(
            Coprocessor.accumulator.ir1,
            Coprocessor.accumulator.ir2,
            Coprocessor.accumulator.ir3);
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

    public static Vector3Int FUN_23EA0(Vector3Int v3)
    {
        Coprocessor.vector0.vx0 = (short)v3.x;
        Coprocessor.vector0.vy0 = (short)v3.y;
        Coprocessor.vector0.vz0 = (short)v3.z;
        Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.V0, _MVMVA_TRANSLATION_VECTOR.None, 12, false);
        return new Vector3Int(
            Coprocessor.accumulator.ir1,
            Coprocessor.accumulator.ir2,
            Coprocessor.accumulator.ir3);
    }

    public static void FUN_18C54(BinaryReader param1, int param2, BinaryWriter param3, long param4)
    {
        ushort uVar1;
        uint uVar2;
        uint uVar3;
        uint uVar4;
        int iVar5;
        int iVar6;
        int iVar7;
        int iVar8;
        int iVar9;
        int iVar10;
        int iVar11;
        int iVar12;
        int iVar13;
        uint local_20;
        Color32 local_1c;

        if (0 < param2)
        {
            uVar3 = 0x84210843; //r22
            uVar4 = 0x80808081; //r20
            local_20 = 0;

            for (int i = 0; i < param2; i++)
            {
                uVar1 = param1.ReadUInt16(); //r16
                uVar2 = (uint)uVar1 << 16; //r5

                if (uVar2 == 0)
                    param3.Write(uVar1);
                else
                {
                    iVar5 = ((uVar1 & 0x1f) << 8) - (uVar1 & 0x1f); //r3
                    iVar6 = (int)((long)iVar5 * (int)uVar3 >> 32); //r11
                    iVar7 = (int)uVar2 >> 21 & 0x1f; //r2
                    iVar8 = (iVar7 << 8) - iVar7; //r6
                    iVar9 = (int)((long)iVar8 * (int)uVar3 >> 32); //r9
                    iVar7 = (int)uVar2 >> 26 & 0x1f;
                    iVar10 = (iVar7 << 8) - iVar7; //r5
                    iVar11 = iVar6 + iVar5 >> 4; //r4
                    iVar11 -= iVar5 >> 0x1f;
                    iVar11 &= 0xff;
                    local_20 = local_20 & 0xffffff00 | (uint)iVar11;
                    iVar6 = (int)((long)iVar10 * (int)uVar3 >> 32);
                    local_20 &= 0xffff00ff;
                    iVar7 = iVar9 + iVar8 >> 4;
                    iVar8 >>= 31;
                    iVar7 = (iVar7 - iVar8 & 0xff) << 8;
                    local_20 = (local_20 | (uint)iVar7) & 0xff00ffff;
                    iVar7 = iVar6 + iVar10 >> 4;
                    iVar10 >>= 31;
                    iVar7 = (iVar7 - iVar10 & 0xff) << 16;
                    local_20 |= (uint)iVar7;
                    local_1c = DpqColor(new Color32
                        ((byte)local_20, (byte)(local_20 >> 8), (byte)(local_20 >> 16), (byte)(local_20 >> 24)), param4);
                    iVar10 = (local_1c.r << 5) - local_1c.r + 128;
                    iVar5 = (int)((long)iVar10 * (int)uVar4 >> 32);
                    iVar11 = (local_1c.g << 5) - local_1c.g + 128;
                    iVar12 = (int)((long)iVar11 * (int)uVar4 >> 32); //r8
                    iVar8 = (local_1c.b << 5) - local_1c.b + 128;
                    iVar13 = (int)((long)iVar8 * (int)uVar4 >> 32); //r7
                    iVar5 = iVar5 + iVar10 >> 7;
                    iVar10 >>= 31;
                    iVar5 -= iVar10;
                    iVar7 = iVar12 + iVar11 >> 7;
                    iVar11 >>= 31;
                    iVar7 = iVar7 - iVar11 << 5;
                    iVar5 |= iVar7;
                    iVar7 = iVar13 + iVar8 >> 7;
                    iVar8 >>= 31;
                    iVar7 = iVar7 - iVar8 << 10;
                    iVar5 |= iVar7;
                    iVar7 = uVar1 & -0x8000;
                    iVar5 |= iVar7;
                    param3.Write((short)iVar5);
                }
            }
        }
    }

    public static VigObject FUN_52188(VigObject src, Type type)
    {
        VigObject newObj = src.gameObject.AddComponent(type) as VigObject;
        newObj.flags = src.flags;
        newObj.type = src.type;
        newObj.tags = src.tags;
        newObj.id = src.id;
        newObj.child = src.child;
        newObj.child2 = src.child2;
        newObj.parent = src.parent;
        newObj.DAT_18 = src.DAT_18;
        newObj.DAT_19 = src.DAT_19;
        newObj.DAT_1A = src.DAT_1A;
        newObj.maxHalfHealth = src.maxHalfHealth;
        newObj.maxFullHealth = src.maxFullHealth;
        newObj.vTransform = src.vTransform;
        newObj.vMesh = src.vMesh;
        newObj.vr = src.vr;
        newObj.DAT_4A = src.DAT_4A;
        newObj.screen = src.screen;
        newObj.DAT_58 = src.DAT_58;
        newObj.vData = src.vData;
        newObj.vCollider = src.vCollider;
        newObj.vAnim = src.vAnim;
        newObj.vLOD = src.vLOD;
        newObj.DAT_6C = src.DAT_6C;
        newObj.vShadow = src.vShadow;
        newObj.PDAT_74 = src.PDAT_74;
        newObj.CCDAT_74 = src.CCDAT_74;
        newObj.IDAT_74 = src.IDAT_74;
        newObj.PDAT_78 = src.PDAT_78;
        newObj.IDAT_78 = src.IDAT_78;
        newObj.PDAT_7C = src.PDAT_7C;
        newObj.IDAT_7C = src.IDAT_7C;
        newObj.physics1 = src.physics1;
        newObj.physics2 = src.physics2;
        newObj.DAT_A0 = src.DAT_A0;
        newObj.DAT_A6 = src.DAT_A6;
        src.transform.parent = null;
        UnityEngine.Object.Destroy(src);
        return newObj;
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

    //FUN_5B9AC
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

    //FUN_5961C
    public static void SetFogNearFar(int a, int b, int h)
    {
        int iVar1;
        int iVar2;
        int iVar3;

        iVar2 = b - a;

        if (99 < iVar2)
        {
            if (iVar2 == 0)
                return; //exception 0x1c00

            if (iVar2 == -1 && -a * b == -0x80000000)
                return; //exception 0x1800

            if (iVar2 == 0)
                return; //exception 0x1c00

            if (iVar2 == -1 && b << 12 == -0x80000000)
                return; //exception 0x1800

            iVar1 = (-a * b) / iVar2 << 8;
            iVar3 = iVar1 / h;

            if (h == 0)
                return; //exception 0x1c00

            if (h == -1 && iVar1 == -0x80000000)
                return; //exception 0x1800

            if (iVar3 < -0x8000)
                iVar3 = -0x8000;

            if (0x7fff < iVar3)
                iVar3 = 0x7fff;

            SetDQA(iVar3);
            SetDQB((b << 12) / iVar2 << 12);
        }
    }

    //FUN_59D4C
    public static Matrix3x3 MulMatrix(Matrix3x3 m0, Matrix3x3 m1)
    {
        short sVar1;
        short sVar2;
        short sVar3;
        short sVar4;
        short sVar5;
        short sVar6;
        short sVar7;
        short sVar8;
        short sVar9;

        Coprocessor.rotationMatrix.rt11 = m0.V00;
        Coprocessor.rotationMatrix.rt12 = m0.V01;
        Coprocessor.rotationMatrix.rt13 = m0.V02;
        Coprocessor.rotationMatrix.rt21 = m0.V10;
        Coprocessor.rotationMatrix.rt22 = m0.V11;
        Coprocessor.rotationMatrix.rt23 = m0.V12;
        Coprocessor.rotationMatrix.rt31 = m0.V20;
        Coprocessor.rotationMatrix.rt32 = m0.V21;
        Coprocessor.rotationMatrix.rt33 = m0.V22;
        Coprocessor.vector0.vx0 = m1.V00;
        Coprocessor.vector0.vy0 = m1.V10;
        Coprocessor.vector0.vz0 = m1.V20;
        Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.V0, _MVMVA_TRANSLATION_VECTOR.None, 12, false);
        sVar2 = Coprocessor.accumulator.ir1;
        sVar3 = Coprocessor.accumulator.ir2;
        sVar4 = Coprocessor.accumulator.ir3;
        Coprocessor.vector0.vx0 = m1.V01;
        Coprocessor.vector0.vy0 = m1.V11;
        Coprocessor.vector0.vz0 = m1.V21;
        Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.V0, _MVMVA_TRANSLATION_VECTOR.None, 12, false);
        sVar5 = Coprocessor.accumulator.ir1;
        sVar6 = Coprocessor.accumulator.ir2;
        sVar7 = Coprocessor.accumulator.ir3;
        Coprocessor.vector0.vx0 = m1.V02;
        Coprocessor.vector0.vy0 = m1.V12;
        Coprocessor.vector0.vz0 = m1.V22;
        Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.V0, _MVMVA_TRANSLATION_VECTOR.None, 12, false);
        sVar1 = Coprocessor.accumulator.ir1;
        sVar8 = Coprocessor.accumulator.ir2;
        sVar9 = Coprocessor.accumulator.ir3;

        return new Matrix3x3()
        {
            V00 = sVar2,
            V01 = sVar5,
            V20 = sVar4,
            V21 = sVar7,
            V02 = sVar1,
            V10 = sVar3,
            V11 = sVar6,
            V12 = sVar8,
            V22 = sVar9
        };
    }

    public static VigTransform CompMatrixLV(VigTransform m0, VigTransform m1)
    {
        short sVar1;
        int iVar1;
        int iVar2;
        int iVar3;
        int iVar4;
        uint uVar5;
        int iVar6;
        int iVar7;
        uint uVar8;
        int iVar9;
        int iVar10;
        uint uVar11;
        int iVar12;
        VigTransform m2;

        Coprocessor.rotationMatrix.rt11 = m0.rotation.V00;
        Coprocessor.rotationMatrix.rt12 = m0.rotation.V01;
        Coprocessor.rotationMatrix.rt13 = m0.rotation.V02;
        Coprocessor.rotationMatrix.rt21 = m0.rotation.V10;
        Coprocessor.rotationMatrix.rt22 = m0.rotation.V11;
        Coprocessor.rotationMatrix.rt23 = m0.rotation.V12;
        Coprocessor.rotationMatrix.rt31 = m0.rotation.V20;
        Coprocessor.rotationMatrix.rt32 = m0.rotation.V21;
        Coprocessor.rotationMatrix.rt33 = m0.rotation.V22;
        Coprocessor.vector0.vx0 = m1.rotation.V00;
        Coprocessor.vector0.vy0 = m1.rotation.V10;
        Coprocessor.vector0.vz0 = m1.rotation.V20;
        Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.V0, _MVMVA_TRANSLATION_VECTOR.None, 12, false);
        uVar5 = (ushort)Coprocessor.accumulator.ir1;
        iVar6 = Coprocessor.accumulator.ir2;
        uVar8 = (ushort)Coprocessor.accumulator.ir3;
        Coprocessor.vector0.vx0 = m1.rotation.V01;
        Coprocessor.vector0.vy0 = m1.rotation.V11;
        Coprocessor.vector0.vz0 = m1.rotation.V21;
        Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.V0, _MVMVA_TRANSLATION_VECTOR.None, 12, false);
        iVar10 = Coprocessor.accumulator.ir1;
        uVar11 = (ushort)Coprocessor.accumulator.ir2;
        iVar12 = Coprocessor.accumulator.ir3;
        Coprocessor.vector0.vx0 = m1.rotation.V02;
        Coprocessor.vector0.vy0 = m1.rotation.V12;
        Coprocessor.vector0.vz0 = m1.rotation.V22;
        Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.V0, _MVMVA_TRANSLATION_VECTOR.None, 12, false);
        m2.rotation.V01 = (short)iVar10;
        m2.rotation.V00 = (short)uVar5;
        m2.rotation.V21 = (short)iVar12;
        m2.rotation.V20 = (short)uVar8;
        uVar5 = (ushort)Coprocessor.accumulator.ir1;
        iVar10 = Coprocessor.accumulator.ir2;
        sVar1 = Coprocessor.accumulator.ir3;
        m2.rotation.V22 = sVar1;
        m2.rotation.V02 = (short)uVar5;
        m2.rotation.V10 = (short)iVar6;
        m2.rotation.V11 = (short)uVar11;
        m2.rotation.V12 = (short)iVar10;
        uVar5 = (uint)m1.position.x;
        uVar8 = (uint)m1.position.y;
        uVar11 = (uint)m1.position.z;

        if ((int)uVar5 < 0)
        {
            iVar10 = -(-(int)uVar5 >> 15);
            uVar5 = (uint)-(-(int)uVar5 & 0x7fff);
        }
        else
        {
            iVar10 = (int)uVar5 >> 15;
            uVar5 = uVar5 & 0x7fff;
        }

        if ((int)uVar8 < 0)
        {
            iVar6 = -(-(int)uVar8 >> 15);
            uVar8 = (uint)-(-(int)uVar8 & 0x7fff);
        }
        else
        {
            iVar6 = (int)uVar8 >> 15;
            uVar8 = uVar8 & 0x7fff;
        }

        if ((int)uVar11 < 0)
        {
            iVar12 = -(-(int)uVar11 >> 15);
            uVar11 = (uint)-(-(int)uVar11 & 0x7fff);
        }
        else
        {
            iVar12 = (int)uVar11 >> 15;
            uVar11 = uVar11 & 0x7fff;
        }

        Coprocessor.accumulator.ir1 = (short)iVar10;
        Coprocessor.accumulator.ir2 = (short)iVar6;
        Coprocessor.accumulator.ir3 = (short)iVar12;
        Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.IR, _MVMVA_TRANSLATION_VECTOR.None, 0, false);
        iVar10 = Coprocessor.mathsAccumulator.mac1;
        iVar6 = Coprocessor.mathsAccumulator.mac2;
        iVar12 = Coprocessor.mathsAccumulator.mac3;
        Coprocessor.accumulator.ir1 = (short)uVar5;
        Coprocessor.accumulator.ir2 = (short)uVar8;
        Coprocessor.accumulator.ir3 = (short)uVar11;
        Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.IR, _MVMVA_TRANSLATION_VECTOR.None, 12, false);

        if (iVar10 < 0)
            iVar10 *= 8;
        else
            iVar10 <<= 3;

        if (iVar6 < 0)
            iVar6 *= 8;
        else
            iVar6 <<= 3;

        if (iVar12 < 0)
            iVar12 *= 8;
        else
            iVar12 <<= 3;

        iVar2 = Coprocessor.mathsAccumulator.mac1;
        iVar3 = Coprocessor.mathsAccumulator.mac2;
        iVar4 = Coprocessor.mathsAccumulator.mac3;
        iVar7 = m0.position.y;
        iVar9 = m0.position.z;
        m2.position = new Vector3Int();
        m2.position.x = iVar2 + iVar10 + m0.position.x;
        m2.position.y = iVar3 + iVar6 + iVar7;
        m2.position.z = iVar4 + iVar12 + iVar9;
        m2.padding = 0;

        return m2;
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

    //FUN_5A01C
    public static void SetRotMatrix(Matrix3x3 m)
    {
        Coprocessor.rotationMatrix.rt11 = m.V00;
        Coprocessor.rotationMatrix.rt12 = m.V01;
        Coprocessor.rotationMatrix.rt13 = m.V02;
        Coprocessor.rotationMatrix.rt21 = m.V10;
        Coprocessor.rotationMatrix.rt22 = m.V11;
        Coprocessor.rotationMatrix.rt23 = m.V12;
        Coprocessor.rotationMatrix.rt31 = m.V20;
        Coprocessor.rotationMatrix.rt32 = m.V21;
        Coprocessor.rotationMatrix.rt33 = m.V22;
    }

    //FUN_5A04C
    public static void SetLightMatrix(Matrix3x3 m)
    {
        Coprocessor.lightMatrix.l11 = m.V00;
        Coprocessor.lightMatrix.l12 = m.V01;
        Coprocessor.lightMatrix.l13 = m.V02;
        Coprocessor.lightMatrix.l21 = m.V10;
        Coprocessor.lightMatrix.l22 = m.V11;
        Coprocessor.lightMatrix.l23 = m.V12;
        Coprocessor.lightMatrix.l31 = m.V20;
        Coprocessor.lightMatrix.l32 = m.V21;
        Coprocessor.lightMatrix.l33 = m.V22;
    }

    //FUN_5A07C
    public static void SetColorMatrix(Matrix3x3 m)
    {
        Coprocessor.lightColorMatrix.lr1 = m.V00;
        Coprocessor.lightColorMatrix.lr2 = m.V01;
        Coprocessor.lightColorMatrix.lr3 = m.V02;
        Coprocessor.lightColorMatrix.lg1 = m.V10;
        Coprocessor.lightColorMatrix.lg2 = m.V11;
        Coprocessor.lightColorMatrix.lg3 = m.V12;
        Coprocessor.lightColorMatrix.lb1 = m.V20;
        Coprocessor.lightColorMatrix.lb2 = m.V21;
        Coprocessor.lightColorMatrix.lb3 = m.V22;
    }

    //FUN_5A1A4
    public static void SetDQA(int param1)
    {
        Coprocessor.depthQueingA = (short)param1;
    }

    //FUN_5A1B0
    public static void SetDQB(int param1)
    {
        Coprocessor.depthQueingB = (uint)param1;
    }

    //FUN_5A1BC
    public static void SetBackColor(int rbk, int gbk, int bbk)
    {
        Coprocessor.backgroundColor._rbk = rbk << 4;
        Coprocessor.backgroundColor._gbk = gbk << 4;
        Coprocessor.backgroundColor._bbk = bbk << 4;
    }

    //FUN_5A1DC
    public static void SetFarColor(int rfc, int gfc, int bfc)
    {
        Coprocessor.farColor._rfc = rfc << 4;
        Coprocessor.farColor._gfc = gfc << 4;
        Coprocessor.farColor._bfc = bfc << 4;
    }

    //FUN_5A1FC
    public static void SetScreenOffset(int param1, int param2)
    {
        Coprocessor.screenOffset.ofx = param1 << 16;
        Coprocessor.screenOffset.ofy = param2 << 16;
    }

    //FUN_5A21C
    public static void SetProjectionPlane(int param1)
    {
        Coprocessor.projectionPlaneDistance = (ushort)param1;
    }

    public static Color32 DpqColor(Color32 vin, long p)
    {
        Coprocessor.colorCode.r = vin.r;
        Coprocessor.colorCode.g = vin.g;
        Coprocessor.colorCode.b = vin.b;
        Coprocessor.colorCode.code = vin.a;
        Coprocessor.accumulator.ir0 = (short)p;
        Coprocessor.ExecuteDPCS(12, false);

        return new Color32(
            Coprocessor.colorFIFO.r2, 
            Coprocessor.colorFIFO.g2, 
            Coprocessor.colorFIFO.b2, 
            Coprocessor.colorFIFO.cd2);
    }

    public static Color32 NormalColorCol(Vector3Int v0, Color32 v1)
    {
        Coprocessor.vector0.vx0 = (short)v0.x;
        Coprocessor.vector0.vy0 = (short)v0.y;
        Coprocessor.vector0.vz0 = (short)v0.z;
        Coprocessor.colorCode.r = v1.r;
        Coprocessor.colorCode.g = v1.g;
        Coprocessor.colorCode.b = v1.b;
        Coprocessor.colorCode.code = v1.a;
        Coprocessor.ExecuteNCCS(12, true);
        return new Color32(
            Coprocessor.colorFIFO.r2,
            Coprocessor.colorFIFO.g2,
            Coprocessor.colorFIFO.b2,
            Coprocessor.colorFIFO.cd2);
    }

    //FUN_5A40C
    public static Matrix3x3 TransposeMatrix(Matrix3x3 m)
    {
        Matrix3x3 output;

        output = new Matrix3x3();
        output.V00 = m.V02;
        output.V01 = m.V10;
        output.V02 = m.V00;
        output.V10 = m.V01;
        output.V00 = m.V00;
        output.V20 = m.V11;
        output.V21 = m.V12;
        output.V11 = m.V20;
        output.V12 = m.V21;
        output.V20 = m.V02;
        output.V11 = m.V11;
        output.V02 = m.V20;
        output.V22 = m.V22;

        return output;
    }

    //FUN_5A44C
    public static Matrix3x3 RotMatrixX(long r, Matrix3x3 min)
    {
        int iVar2;
        int iVar3;
        int iVar4;
        int iVar5;
        int iVar6;
        int iVar7;
        int iVar8;
        int iVar9;

        if (-1 < r)
        {
            iVar3 = GameManager.DAT_65C90[(r & 0xfff) * 2];
            iVar2 = GameManager.DAT_65C90[(r & 0xfff) * 2 + 1];
        }
        else
        {
            r = -r;
            iVar3 = -GameManager.DAT_65C90[(r & 0xfff) * 2];
            iVar2 = GameManager.DAT_65C90[(r & 0xfff) * 2 + 1];
        }

        iVar4 = min.V10;
        iVar7 = min.V20;
        iVar5 = min.V11;
        iVar8 = min.V21;
        iVar6 = min.V12;
        iVar9 = min.V22;
        Matrix3x3 mout = new Matrix3x3();
        mout.V00 = min.V00;
        mout.V01 = min.V01;
        mout.V02 = min.V02;
        mout.V10 = (short)(iVar2 * iVar4 - iVar3 * iVar7 >> 12);
        mout.V11 = (short)(iVar2 * iVar5 - iVar3 * iVar8 >> 12);
        mout.V12 = (short)(iVar2 * iVar6 - iVar3 * iVar9 >> 12);
        mout.V20 = (short)(iVar3 * iVar4 + iVar2 * iVar7 >> 12);
        mout.V21 = (short)(iVar3 * iVar5 + iVar2 * iVar8 >> 12);
        mout.V22 = (short)(iVar3 * iVar6 + iVar2 * iVar9 >> 12);
        return mout;
    }

    //FUN_5A5EC
    public static Matrix3x3 RotMatrixY(long r, Matrix3x3 min)
    {
        int iVar1;
        int iVar3;
        int iVar4;
        int iVar5;
        int iVar6;
        int iVar7;
        int iVar8;
        int iVar9;

        if (-1 < r)
        {
            iVar1 = -GameManager.DAT_65C90[(r & 0xfff) * 2];
            iVar3 = GameManager.DAT_65C90[(r & 0xfff) * 2 + 1];
        }
        else
        {
            r = -r;
            iVar1 = GameManager.DAT_65C90[(r & 0xfff) * 2];
            iVar3 = GameManager.DAT_65C90[(r & 0xfff) * 2 + 1];
        }

        iVar4 = min.V00;
        iVar7 = min.V20;
        iVar5 = min.V01;
        iVar8 = min.V21;
        iVar6 = min.V02;
        iVar9 = min.V22;
        Matrix3x3 mout = new Matrix3x3();
        mout.V00 = (short)(iVar3 * iVar4 - iVar1 * iVar7 >> 12);
        mout.V01 = (short)(iVar3 * iVar5 - iVar1 * iVar8 >> 12);
        mout.V02 = (short)(iVar3 * iVar6 - iVar1 * iVar9 >> 12);
        mout.V10 = min.V10;
        mout.V11 = min.V11;
        mout.V12 = min.V12;
        mout.V20 = (short)(iVar1 * iVar4 + iVar3 * iVar7 >> 12);
        mout.V21 = (short)(iVar1 * iVar5 + iVar3 * iVar8 >> 12);
        mout.V22 = (short)(iVar1 * iVar6 + iVar3 * iVar9 >> 12);
        return mout;
    }

    //FUN_5A78C
    public static Matrix3x3 RotMatrixYXZ_gte(Vector3Int r)
    {
        uint uVar1;
        uint uVar2;
        uint uVar3;
        int iVar4;
        int iVar5;
        int iVar6;
        int iVar7;
        int iVar8;
        int uVar9;
        uint uVar10;
        int iVar11;
        uint uVar12;
        int iVar13;
        uint uVar14;
        int uVar15;
        int iVar16;
        int iVar17;
        int iVar18;
        int iVar19;
        int index;
        Matrix3x3 m33;

        m33 = new Matrix3x3();
        iVar16 = r.y << 16 | (ushort)r.x;
        uVar14 = (uint)(r.z >> 31);
        uVar12 = (uint)(iVar16 >> 31);
        uVar10 = (uint)((short)iVar16 >> 31);
        index = ((r.z + (int)uVar14 ^ (int)uVar14) & 0xfff) * 2;
        uVar1 = (uint)((GameManager.DAT_65C90[index + 1] << 16 | (ushort)GameManager.DAT_65C90[index])
                        * 0x10000 + uVar14 ^ uVar14);
        index = (((iVar16 >> 16) + (int)uVar12 ^ (int)uVar12) & 0xfff) * 2;
        uVar2 = (uint)((GameManager.DAT_65C90[index + 1] << 16 | (ushort)GameManager.DAT_65C90[index])
                        * 0x10000 + uVar12 ^ uVar12);
        index = (((short)iVar16 + (int)uVar10 ^ (int)uVar10) & 0xfff) * 2;
        uVar3 = (uint)((GameManager.DAT_65C90[index + 1] << 16 | (ushort)GameManager.DAT_65C90[index])
                        * 0x10000 + uVar10 ^ uVar10);
        index = (((iVar16 >> 16) + (int)uVar12 ^ (int)uVar12) & 0xfff) * 2;
        iVar8 = (((GameManager.DAT_65C90[index + 1] << 16 | (ushort)GameManager.DAT_65C90[index])
                        >> 16) << 16 | (int)(uVar2 >> 16)) >> 16;
        Coprocessor.accumulator.ir0 = (short)iVar8;
        iVar7 = (int)uVar3 >> 16;
        Coprocessor.accumulator.ir1 = (short)iVar7;
        iVar5 = (int)uVar1 >> 16;
        Coprocessor.accumulator.ir2 = (short)iVar5;
        index = ((r.z + (int)uVar14 ^ (int)uVar14) & 0xfff) * 2;
        iVar4 = (((GameManager.DAT_65C90[index + 1] << 16 | (ushort)GameManager.DAT_65C90[index])
                        >> 16) << 16 | (int)(uVar1 >> 16)) >> 16;
        Coprocessor.accumulator.ir3 = (short)iVar4;
        index = (((short)iVar16 + (int)uVar10 ^ (int)uVar10) & 0xfff) * 2;
        iVar16 = (((GameManager.DAT_65C90[index + 1] << 16 | (ushort)GameManager.DAT_65C90[index])
                        >> 16) << 16 | (int)(uVar3 >> 16)) >> 16;
        Coprocessor.ExecuteGPF(12, false);
        uVar9 = Coprocessor.accumulator.ir1;
        iVar11 = Coprocessor.accumulator.ir2;
        iVar13 = Coprocessor.accumulator.ir3;
        iVar19 = (int)uVar2 >> 16;
        Coprocessor.accumulator.ir0 = (short)iVar19;
        Coprocessor.accumulator.ir1 = (short)iVar7;
        Coprocessor.accumulator.ir2 = (short)iVar5;
        Coprocessor.accumulator.ir3 = (short)iVar4;
        Coprocessor.ExecuteGPF(12, false);
        m33.SetValue32(4, (short)(iVar16 * iVar8 >> 12));
        uVar15 = Coprocessor.accumulator.ir1;
        iVar17 = Coprocessor.accumulator.ir2;
        iVar18 = Coprocessor.accumulator.ir3;
        Coprocessor.accumulator.ir0 = (short)iVar4;
        Coprocessor.accumulator.ir1 = (short)iVar16;
        Coprocessor.accumulator.ir2 = (short)uVar15;
        Coprocessor.accumulator.ir3 = (short)uVar9;
        Coprocessor.ExecuteGPF(12, false);
        uVar1 = (uint)Coprocessor.accumulator.ir1;
        iVar8 = Coprocessor.accumulator.ir2;
        iVar6 = Coprocessor.accumulator.ir3;
        Coprocessor.accumulator.ir0 = (short)iVar5;
        Coprocessor.accumulator.ir1 = (short)iVar16;
        Coprocessor.accumulator.ir2 = (short)uVar15;
        Coprocessor.accumulator.ir3 = (short)uVar9;
        Coprocessor.ExecuteGPF(12, false);
        m33.SetValue32(2, (int)(uVar1 & 0xffff) | iVar7 * -0x10000);
        iVar4 = Coprocessor.accumulator.ir1;
        iVar5 = Coprocessor.accumulator.ir2;
        iVar7 = Coprocessor.accumulator.ir3;
        m33.SetValue32(0, (iVar8 - iVar11) * 0x10000 | (int)(iVar13 + iVar5 & 0xffffU));
        m33.SetValue32(1, iVar4 << 16 | (int)(iVar16 * iVar19 >> 12 & 0xffffU));
        m33.SetValue32(3, (iVar6 + iVar17) * 0x10000 | (int)(iVar7 - iVar18 & 0xffffU));
        return m33;
    }

    //FUN_5991C
    public static Vector3Int VectorNormal(Vector3Int n1)
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

    public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
    {
        if (val.CompareTo(min) < 0) return min;
        else if (val.CompareTo(max) > 0) return max;
        else return val;
    }

    public static void SetGlobalScale(this Transform transform, Vector3 globalScale)
    {
        transform.localScale = Vector3.one;
        transform.localScale = new Vector3
            (globalScale.x / transform.lossyScale.x, 
            globalScale.y / transform.lossyScale.y, 
            globalScale.z / transform.lossyScale.z);
    }


    public static float MoveDecimal(int value, int space)
    {
        return (float)(value / Math.Pow(10, space));
    }

    public static float MoveDecimal(long value, int space)
    {
        return (float)(value / Math.Pow(10, space));
    }

    public static sbyte ReadSByte(this BinaryReader reader, int offset)
    {
        long position = reader.BaseStream.Position;
        reader.BaseStream.Seek(offset, SeekOrigin.Current);
        sbyte value = reader.ReadSByte();
        reader.BaseStream.Seek(position, SeekOrigin.Begin);
        return value;
    }

    public static byte ReadByte(this BinaryReader reader, int offset)
    {
        long position = reader.BaseStream.Position;
        reader.BaseStream.Seek(offset, SeekOrigin.Current);
        byte value = reader.ReadByte();
        reader.BaseStream.Seek(position, SeekOrigin.Begin);
        return value;
    }

    public static byte[] ReadBytes(this BinaryReader reader, int offset, int length)
    {
        long position = reader.BaseStream.Position;
        reader.BaseStream.Seek(offset, SeekOrigin.Current);
        byte[] value = reader.ReadBytes(length);
        reader.BaseStream.Seek(position, SeekOrigin.Begin);
        return value;
    }

    public static short ReadInt16(this BinaryReader reader, int offset)
    {
        long position = reader.BaseStream.Position;
        reader.BaseStream.Seek(offset, SeekOrigin.Current);
        short value = reader.ReadInt16();
        reader.BaseStream.Seek(position, SeekOrigin.Begin);
        return value;
    }
    
    public static ushort ReadUInt16(this BinaryReader reader, int offset)
    {
        long position = reader.BaseStream.Position;
        reader.BaseStream.Seek(offset, SeekOrigin.Current);
        ushort value = reader.ReadUInt16();
        reader.BaseStream.Seek(position, SeekOrigin.Begin);
        return value;
    }

    public static int ReadInt32(this BinaryReader reader, int offset)
    {
        long position = reader.BaseStream.Position;
        reader.BaseStream.Seek(offset, SeekOrigin.Current);
        int value = reader.ReadInt32();
        reader.BaseStream.Seek(position, SeekOrigin.Begin);
        return value;
    }

    public static uint ReadUInt32(this BinaryReader reader, int offset)
    {
        long position = reader.BaseStream.Position;
        reader.BaseStream.Seek(offset, SeekOrigin.Current);
        uint value = reader.ReadUInt32();
        reader.BaseStream.Seek(position, SeekOrigin.Begin);
        return value;
    }

    public static string ReadNullTerminatedString(this BinaryReader stream)
    {
        string str = "";
        char ch;
        while ((int)(ch = stream.ReadChar()) != 0)
            str = str + ch;
        return str;
    }

    public static void Write(this BinaryWriter writer, int offset, sbyte value)
    {
        long position = writer.BaseStream.Position;
        writer.BaseStream.Seek(offset, SeekOrigin.Current);
        writer.Write(value);
        writer.BaseStream.Seek(position, SeekOrigin.Begin);
    }

    public static void Write(this BinaryWriter writer, int offset, byte value)
    {
        long position = writer.BaseStream.Position;
        writer.BaseStream.Seek(offset, SeekOrigin.Current);
        writer.Write(value);
        writer.BaseStream.Seek(position, SeekOrigin.Begin);
    }

    public static void Write(this BinaryWriter writer, int offset, byte[] value)
    {
        long position = writer.BaseStream.Position;
        writer.BaseStream.Seek(offset, SeekOrigin.Current);
        writer.Write(value);
        writer.BaseStream.Seek(position, SeekOrigin.Begin);
    }

    public static void Write(this BinaryWriter writer, int offset, short value)
    {
        long position = writer.BaseStream.Position;
        writer.BaseStream.Seek(offset, SeekOrigin.Current);
        writer.Write(value);
        writer.BaseStream.Seek(position, SeekOrigin.Begin);
    }

    public static void Write(this BinaryWriter writer, int offset, ushort value)
    {
        long position = writer.BaseStream.Position;
        writer.BaseStream.Seek(offset, SeekOrigin.Current);
        writer.Write(value);
        writer.BaseStream.Seek(position, SeekOrigin.Begin);
    }

    public static void Write(this BinaryWriter writer, int offset, int value)
    {
        long position = writer.BaseStream.Position;
        writer.BaseStream.Seek(offset, SeekOrigin.Current);
        writer.Write(value);
        writer.BaseStream.Seek(position, SeekOrigin.Begin);
    }

    public static void Write(this BinaryWriter writer, int offset, uint value)
    {
        long position = writer.BaseStream.Position;
        writer.BaseStream.Seek(offset, SeekOrigin.Current);
        writer.Write(value);
        writer.BaseStream.Seek(position, SeekOrigin.Begin);
    }

    public static void SaveObjectToFile(UnityEngine.Object obj, string filename)
    {
#if UNITY_EDITOR
        AssetDatabase.CreateAsset(obj, filename);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
#endif
    }
}

public class FixedSizedQueue<T>
{
    ConcurrentQueue<T> q = new ConcurrentQueue<T>();
    private object lockObject = new object();

    public int Limit { get; set; }
    public void Enqueue(T obj)
    {
        q.Enqueue(obj);
        lock (lockObject)
        {
            T overflow;
            while (q.Count > Limit && q.TryDequeue(out overflow)) ;
        }
    }
    public T Dequeue()
    {
        T result;
        q.TryDequeue(out result);
        return result;
    }
    public T Peek()
    {
        T result;
        q.TryPeek(out result);
        return result;
    }
    public T PeekAt(int index)
    {
        T result;
        result = q.ElementAt(index);
        return result;
    }
}

public class BufferedBinaryReader : IDisposable
{
    public long Position { get { return bufferOffset; } }
    public long Length { get { return bufferSize; } }

    private readonly Stream stream;
    private byte[] buffer;
    private int bufferSize;
    private int bufferOffset;
    private int numBufferedBytes;

    public BufferedBinaryReader(Stream stream, int bufferSize)
    {
        this.stream = stream;
        this.bufferSize = bufferSize;
        buffer = new byte[bufferSize];
        bufferOffset = bufferSize;
    }

    public BufferedBinaryReader(byte[] buffer)
    {
        bufferSize = buffer.Length;
        this.buffer = buffer;
    }

    public int NumBytesAvailable { get { return Math.Max(0, numBufferedBytes - bufferOffset); } }

    public bool FillBuffer()
    {
        var numBytesUnread = bufferSize - bufferOffset;
        var numBytesToRead = bufferSize - numBytesUnread;
        bufferOffset = 0;
        numBufferedBytes = numBytesUnread;
        if (numBytesUnread > 0)
        {
            Buffer.BlockCopy(buffer, numBytesToRead, buffer, 0, numBytesUnread);
        }
        while (numBytesToRead > 0)
        {
            var numBytesRead = stream.Read(buffer, numBytesUnread, numBytesToRead);
            if (numBytesRead == 0)
            {
                return false;
            }
            numBufferedBytes += numBytesRead;
            numBytesToRead -= numBytesRead;
            numBytesUnread += numBytesRead;
        }
        return true;
    }

    public void ChangeBuffer(byte[] buffer)
    {
        bufferSize = buffer.Length;
        this.buffer = buffer;
        bufferOffset = 0;
    }

    public void ChangeBuffer(BufferedBinaryReader reader)
    {
        bufferSize = reader.bufferSize;
        buffer = reader.buffer;
        bufferOffset = reader.bufferOffset;
    }

    public byte[] GetBuffer()
    {
        return buffer;
    }

    public byte ReadByte(int offset)
    {
        byte val = (byte)(int)buffer[bufferOffset + offset];
        //bufferOffset += 2;
        return val;
    }

    public short ReadInt16(int offset)
    {
        var val = (short)((int)buffer[bufferOffset + offset] | (int)buffer[bufferOffset + offset + 1] << 8);
        //bufferOffset += 2;
        return val;
    }

    public ushort ReadUInt16(int offset)
    {
        var val = (ushort)((int)buffer[bufferOffset + offset] | (int)buffer[bufferOffset + offset + 1] << 8);
        //bufferOffset += 2;
        return val;
    }

    public int ReadInt32(int offset)
    {
        var val = (int)((int)buffer[bufferOffset + offset] | (int)buffer[bufferOffset + offset + 1] << 8 |
                        (int)buffer[bufferOffset + offset + 2] << 16 | (int)buffer[bufferOffset + offset + 3] << 24);
        return val;
    }

    public void Dispose()
    {
        if (stream != null)
            stream.Close();
    }

    public void Seek(long offset, SeekOrigin origin)
    {
        switch (origin)
        {
            case SeekOrigin.Begin:
                bufferOffset = (int)offset;
                break;
            case SeekOrigin.Current:
                bufferOffset += (int)offset;
                break;
            case SeekOrigin.End:
                bufferOffset = bufferSize - (int)offset;
                break;
        }
    }
}

public class VigTuple
{
    public VigObject vObject;
    public uint flag;

    public VigTuple(VigObject o, uint f)
    {
        vObject = o;
        flag = f;
    }
}

public class Navigation
{
    public Navigation next; //0x00
    public Navigation DAT_04; //0x04
    public int aimpIndex; //0x08
    public ushort DAT_0C; //0x0C
    public ushort DAT_0E; //0x0E
    public byte DAT_10; //0x10
    public byte DAT_11; //0x11
    public int DAT_14; //0x14
    public int DAT_18; //0x18
}