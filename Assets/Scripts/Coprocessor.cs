using System;
using System.Collections;
using System.Collections.Generic;
using Types;
using static Types.TypesClass;

public struct Cop2Vector0
{
    public short vx0, vy0, vz0;
}

public struct Cop2Vector1
{
    public short vx1, vy1, vz1;
}

public struct Cop2Vector2
{
    public short vx2, vy2, vz2;
}

public struct Cop2Accumulator
{
    public short ir0;               // Interpolate
    public short ir1, ir2, ir3;     // Vector
}

public struct Cop2Maths
{
    public int mac0;                // Value
    public int mac1, mac2, mac3;    // Vector
}

public struct Cop2RT
{
    public short rt11, rt12, rt13, rt21, rt22, rt23, rt31, rt32, rt33;
}

public struct Cop2LLM
{
    public short l11, l12, l13, l21, l22, l23, l31, l32, l33;
}

public struct Cop2LCM
{
    public short lr1, lr2, lr3, lg1, lg2, lg3, lb1, lb2, lb3;
}

public struct Cop2ClrCode
{
    public byte r, g, b, code;
}

public struct Cop2TV
{
    public int _trx, _try, _trz;
}

public struct Cop2BC
{
    public int _rbk, _gbk, _bbk;
}

public struct Cop2FC
{
    public int _rfc, _gfc, _bfc;
}

public struct Cop2ClrFIFO
{
    public byte r0, g0, b0, cd0;
    public byte r1, g1, b1, cd1;
    public byte r2, g2, b2, cd2;
}

public struct Cop2SxyFIFO
{
    public short sx0, sx1, sx2, sxp;
    public short sy0, sy1, sy2, syp;
}

public struct Cop2SzFIFO
{
    public short sz0, sz1, sz2, sz3;
}

public struct Cop2OF
{
    public int ofx, ofy;
}

public enum _MVMVA_MULTIPLY_MATRIX
{
    Rotation,
    Light,
    Color,
    Reserved
}

public enum _MVMVA_MULTIPLY_VECTOR
{
    V0,
    V1,
    V2,
    IR
}

public enum _MVMVA_TRANSLATION_VECTOR
{
    TR,
    BK,
    FC,
    None
}

public class Coprocessor
{
    public static Cop2Vector0 vector0;
    public static Cop2Vector1 vector1;
    public static Cop2Vector2 vector2;
    public static ushort averageZ;
    public static Cop2Accumulator accumulator;
    public static Cop2Maths mathsAccumulator;
    public static Cop2RT rotationMatrix;
    public static Cop2LLM lightMatrix;
    public static Cop2LCM lightColorMatrix;
    public static Cop2ClrCode colorCode;
    public static Cop2TV translationVector;
    public static Cop2BC backgroundColor;
    public static Cop2FC farColor;
    public static Cop2ClrFIFO colorFIFO;
    public static Cop2SxyFIFO screenXYFIFO;
    public static Cop2SzFIFO screenZFIFO;
    public static Cop2OF screenOffset;
    public static short depthQueingA;
    public static uint depthQueingB;
    public static ushort projectionPlaneDistance;
    public static int zScaleFactor3;
    public static int zScaleFactor4;

    static readonly long MAC0_MIN_VALUE = -(INT64_C(1) << 31);
    static readonly long MAC0_MAX_VALUE = (INT64_C(1) << 31) - 1;
    static readonly long MAC123_MIN_VALUE = -(INT64_C(1) << 43);
    static readonly long MAC123_MAX_VALUE = (INT64_C(1) << 43) - 1;
    static readonly int IR0_MIN_VALUE = 0x0000;
    static readonly int IR0_MAX_VALUE = 0x1000;
    static readonly int IR123_MIN_VALUE = -(INT64_C(1) << 15);
    static readonly int IR123_MAX_VALUE = (INT64_C(1) << 15) - 1;

    static DisplayAspectRatio s_aspect_ratio = DisplayAspectRatio.R4_3;
    static uint s_custom_aspect_ratio_numerator;
    static uint s_custom_aspect_ratio_denominator;
    static float s_custom_aspect_ratio_f;

    public static void ExecuteRTPS(byte shift, bool lm)
    {
        short[] V0 = new short[]
        {
            vector0.vx0, vector0.vy0, vector0.vz0
        };

        RTPS(V0, shift, lm, true);
    }

    public static void ExecuteNCLIP()
    {
        TruncateAndSetMAC((long)screenXYFIFO.sx0 * screenXYFIFO.sy1 + (long)screenXYFIFO.sx1 * screenXYFIFO.sy2 +
                            (long)screenXYFIFO.sx2 * screenXYFIFO.sy0 - (long)screenXYFIFO.sx0 * screenXYFIFO.sy2 -
                            (long)screenXYFIFO.sy1 * screenXYFIFO.sy0 - (long)screenXYFIFO.sx2 * screenXYFIFO.sy1,
                            0, 0);
    }

    public static void ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX matrix, _MVMVA_MULTIPLY_VECTOR multiply, _MVMVA_TRANSLATION_VECTOR translation, byte shift, bool lm)
    {
        short[,] M = new short[3, 3];
        int[] T = new int[3];
        short Vx = 0;
        short Vy = 0;
        short Vz = 0;

        switch (matrix)
        {
            case _MVMVA_MULTIPLY_MATRIX.Color:
                M[0, 0] = lightColorMatrix.l11;
                M[0, 1] = lightColorMatrix.l12;
                M[0, 2] = lightColorMatrix.l13;
                M[1, 0] = lightColorMatrix.l21;
                M[1, 1] = lightColorMatrix.l22;
                M[1, 2] = lightColorMatrix.l23;
                M[2, 0] = lightColorMatrix.l31;
                M[2, 1] = lightColorMatrix.l32;
                M[2, 2] = lightColorMatrix.l33;
                break;
            case _MVMVA_MULTIPLY_MATRIX.Light:
                M[0, 0] = lightMatrix.lb1;
                M[0, 1] = lightMatrix.lb2;
                M[0, 2] = lightMatrix.lb3;
                M[1, 0] = lightMatrix.lg1;
                M[1, 1] = lightMatrix.lg2;
                M[1, 2] = lightMatrix.lg3;
                M[2, 0] = lightMatrix.lr1;
                M[2, 1] = lightMatrix.lr2;
                M[2, 2] = lightMatrix.lr3;
                break;
            case _MVMVA_MULTIPLY_MATRIX.Rotation:
                M[0, 0] = rotationMatrix.rt11;
                M[0, 1] = rotationMatrix.rt12;
                M[0, 2] = rotationMatrix.rt13;
                M[1, 0] = rotationMatrix.rt21;
                M[1, 1] = rotationMatrix.rt22;
                M[1, 2] = rotationMatrix.rt23;
                M[2, 0] = rotationMatrix.rt31;
                M[2, 1] = rotationMatrix.rt32;
                M[2, 2] = rotationMatrix.rt33;
                break;
            default:
                M[0, 0] = (short)-(colorCode.r << 4);
                M[0, 1] = (short)(colorCode.r << 4);
                M[0, 2] = accumulator.ir0;
                M[1, 0] = rotationMatrix.rt13;
                M[1, 1] = rotationMatrix.rt13;
                M[1, 2] = rotationMatrix.rt13;
                M[2, 0] = rotationMatrix.rt22;
                M[2, 1] = rotationMatrix.rt22;
                M[2, 2] = rotationMatrix.rt22;
                break;
        }

        switch (multiply)
        {
            case _MVMVA_MULTIPLY_VECTOR.V0:
                Vx = vector0.vx0;
                Vy = vector0.vy0;
                Vz = vector0.vz0;
                break;
            case _MVMVA_MULTIPLY_VECTOR.V1:
                Vx = vector1.vx1;
                Vy = vector1.vy1;
                Vz = vector1.vz1;
                break;
            case _MVMVA_MULTIPLY_VECTOR.V2:
                Vx = vector2.vx2;
                Vy = vector2.vy2;
                Vz = vector2.vz2;
                break;
            default:
                Vx = accumulator.ir1;
                Vy = accumulator.ir2;
                Vz = accumulator.ir3;
                break;
        }

        switch (translation)
        {
            case _MVMVA_TRANSLATION_VECTOR.TR:
                T[0] = translationVector._trx;
                T[1] = translationVector._try;
                T[2] = translationVector._trz;
                break;
            case _MVMVA_TRANSLATION_VECTOR.BK:
                T[0] = backgroundColor._rbk;
                T[1] = backgroundColor._gbk;
                T[2] = backgroundColor._bbk;
                break;
            case _MVMVA_TRANSLATION_VECTOR.FC:
                T[0] = farColor._rfc;
                T[1] = farColor._gfc;
                T[3] = farColor._bfc;
                break;
            default:
                T[0] = 0;
                T[1] = 0;
                T[2] = 0;
                break;
        }

        MultMatVec(M, T, Vx, Vy, Vz, shift, lm);
    }

    public static void ExecuteCDP(byte shift, bool lm)
    {
        short[,] LCM = new short[,]
        {
            { lightColorMatrix.lr1, lightColorMatrix.lr2, lightColorMatrix.lr3 },
            { lightColorMatrix.lg1, lightColorMatrix.lg2, lightColorMatrix.lg3 },
            { lightColorMatrix.lb1, lightColorMatrix.lb2, lightColorMatrix.lb3 }
        };
        int[] BK = new int[]
        {
            backgroundColor._rbk, backgroundColor._gbk, backgroundColor._bbk
        };

        MultMatVec(LCM, BK, accumulator.ir1, accumulator.ir2, accumulator.ir3, shift, lm);

        int in_MAC1 = ((int)(uint)colorCode.r * accumulator.ir1) << 4;
        int in_MAC2 = ((int)(uint)colorCode.g * accumulator.ir2) << 4;
        int in_MAC3 = ((int)(uint)colorCode.b * accumulator.ir3) << 4;

        InterpolateColor(in_MAC1, in_MAC2, in_MAC3, shift, lm);

        PushRGBFromMAC();
    }

    public static void ExecuteSQR(byte shift, bool lm)
    {
        mathsAccumulator.mac1 = (accumulator.ir1 * accumulator.ir1) >> shift;
        mathsAccumulator.mac2 = (accumulator.ir2 * accumulator.ir2) >> shift;
        mathsAccumulator.mac3 = (accumulator.ir3 * accumulator.ir3) >> shift;

        TruncateAndSetIR(mathsAccumulator.mac1, 1, lm);
        TruncateAndSetIR(mathsAccumulator.mac2, 2, lm);
        TruncateAndSetIR(mathsAccumulator.mac3, 3, lm);
    }

    public static void ExecuteGPF(byte shift, bool lm)
    {
        TruncateAndSetMACAndIR(accumulator.ir1 * accumulator.ir0, 1, shift, lm);
        TruncateAndSetMACAndIR(accumulator.ir2 * accumulator.ir0, 2, shift, lm);
        TruncateAndSetMACAndIR(accumulator.ir3 * accumulator.ir0, 3, shift, lm);

        PushRGBFromMAC();
    }

    public static void ExecuteOP(byte shift, bool lm)
    {
        int D1 = rotationMatrix.rt11;
        int D2 = rotationMatrix.rt22;
        int D3 = rotationMatrix.rt33;
        int IR1 = accumulator.ir1;
        int IR2 = accumulator.ir2;
        int IR3 = accumulator.ir3;

        TruncateAndSetMACAndIR(IR3 * D2 - IR2 * D3, 1, shift, lm);
        TruncateAndSetMACAndIR(IR1 * D3 - IR3 * D1, 2, shift, lm);
        TruncateAndSetMACAndIR(IR2 * D1 - IR1 * D2, 3, shift, lm);
    }

    public static void ExecuteCC(byte shift, bool lm)
    {
        short[,] LCM = new short[,]
        {
            { lightColorMatrix.lr1, lightColorMatrix.lr2, lightColorMatrix.lr3 },
            { lightColorMatrix.lg1, lightColorMatrix.lg2, lightColorMatrix.lg3 },
            { lightColorMatrix.lb1, lightColorMatrix.lb2, lightColorMatrix.lb3 }
        };

        int[] BK = new int[]
        {
            backgroundColor._rbk, backgroundColor._gbk, backgroundColor._bbk
        };

        MultMatVec(LCM, BK, accumulator.ir1, accumulator.ir2, accumulator.ir3, shift, lm);

        TruncateAndSetMACAndIR((long)((int)(uint)colorCode.r * accumulator.ir1) << 4, 1, shift, lm);
        TruncateAndSetMACAndIR((long)((int)(uint)colorCode.g * accumulator.ir2) << 4, 2, shift, lm);
        TruncateAndSetMACAndIR((long)((int)(uint)colorCode.b * accumulator.ir3) << 4, 3, shift, lm);

        PushRGBFromMAC();
    }

    public static void ExecuteRTPT(byte shift, bool lm)
    {
        short[] V0 = new short[]
        {
            vector0.vx0, vector0.vy0, vector0.vz0
        };
        short[] V1 = new short[]
        {
            vector1.vx1, vector1.vy1, vector1.vz1
        };
        short[] V2 = new short[]
        {
            vector2.vx2, vector2.vy2, vector2.vz2
        };

        RTPS(V0, shift, lm, false);
        RTPS(V1, shift, lm, false);
        RTPS(V2, shift, lm, true);
    }

    public static void ExecuteAVSZ4()
    {
        long result = (long)zScaleFactor4 * (int)((uint)screenZFIFO.sz0 + (uint)screenZFIFO.sz1 + (uint)screenZFIFO.sz2 + (uint)screenZFIFO.sz3);
        TruncateAndSetMAC(result, 0, 0);
        SetOTZ((int)(result >> 12));
    }

    public static void ExecuteNCCT(byte shift, bool lm)
    {
        short[] V0 = new short[]
        {
            vector0.vx0, vector0.vy0, vector0.vz0
        };
        short[] V1 = new short[]
        {
            vector1.vx1, vector1.vy1, vector1.vz1
        };
        short[] V2 = new short[]
        {
            vector2.vx2, vector2.vy2, vector2.vz2
        };

        NCCS(V0, shift, lm);
        NCCS(V1, shift, lm);
        NCCS(V2, shift, lm);
    }

    private static void RTPS(short[] V, byte shift, bool lm, bool last)
    {
        int[] TR = new int[]
        {
            translationVector._trx, translationVector._try, translationVector._trz
        };

        int[,] RT = new int[,]
        {
            { rotationMatrix.rt11, rotationMatrix.rt12, rotationMatrix.rt13 },
            { rotationMatrix.rt21, rotationMatrix.rt22, rotationMatrix.rt23 },
            { rotationMatrix.rt31, rotationMatrix.rt32, rotationMatrix.rt33 }
        };

        long[] xyz = new long[3];

        for (int i = 0; i < 3; i++)
            xyz[i] = SignExtendMACResult(SignExtendMACResult(((long)TR[i] << 12) + ((long)RT[i, 0] * V[0]), i + 1) +
                                ((long)RT[i, 1] * V[1]), i + 1) + ((long)RT[i, 2] * V[2]);

        long x = xyz[0];
        long y = xyz[1];
        long z = xyz[2];
        TruncateAndSetMAC(x, 1, shift);
        TruncateAndSetMAC(y, 2, shift);
        TruncateAndSetMAC(z, 3, shift);
        TruncateAndSetIR(mathsAccumulator.mac1, 1, lm);
        TruncateAndSetIR(mathsAccumulator.mac2, 2, lm);
        TruncateAndSetIR((int)(z >> 12), 3, false);
        accumulator.ir3 = (short)Utilities.Clamp<int>(mathsAccumulator.mac3, lm ? 0 : IR123_MIN_VALUE, IR123_MAX_VALUE);

        PushSZ((int)(z >> 12));

        long result = (long)((ulong)UNRDivide(projectionPlaneDistance, (ushort)screenZFIFO.sz2));

        long Sx;
        switch (s_aspect_ratio)
        {
            case DisplayAspectRatio.R16_9:
                Sx = ((((result * accumulator.ir1) * 3) / 4) + screenOffset.ofx);
                break;

            case DisplayAspectRatio.R19_9:
                Sx = ((((result * accumulator.ir1) * 12) / 19) + screenOffset.ofx);
                break;

            case DisplayAspectRatio.R20_9:
                Sx = ((((result * accumulator.ir1) * 3) / 5) + screenOffset.ofx);
                break;

            case DisplayAspectRatio.Custom:
            case DisplayAspectRatio.MatchWindow:
                Sx = ((((result * accumulator.ir1) * s_custom_aspect_ratio_numerator) /
                        s_custom_aspect_ratio_denominator) +
                        screenOffset.ofx);
                break;

            case DisplayAspectRatio.Auto:
            case DisplayAspectRatio.R4_3:
            case DisplayAspectRatio.PAR1_1:
            default:
                Sx = (result * accumulator.ir1 + screenOffset.ofx);
                break;
        }

        long Sy = result * accumulator.ir2 + screenOffset.ofy;
        PushSXY((int)(Sx >> 16), (int)(Sy >> 16));

        if (last)
        {
            long Sz = result * depthQueingA + depthQueingB;
            TruncateAndSetMAC(Sz, 0, 0);
            TruncateAndSetIR((int)(Sz >> 12), 0, true);
        }
    }

    private static void NCCS(short[] V, byte shift, bool lm)
    {
        short[,] LLM = new short[,]
        {
            { lightMatrix.l11, lightMatrix.l12, lightMatrix.l13 },
            { lightMatrix.l21, lightMatrix.l22, lightMatrix.l23 },
            { lightMatrix.l31, lightMatrix.l32, lightMatrix.l33 }
        };
        short[,] LCM = new short[,]
        {
            { lightColorMatrix.lr1, lightColorMatrix.lr2, lightColorMatrix.lr3 },
            { lightColorMatrix.lg1, lightColorMatrix.lg2, lightColorMatrix.lg3 },
            { lightColorMatrix.lb1, lightColorMatrix.lb2, lightColorMatrix.lb3 }
        };
        int[] BK = new int[]
        {
            backgroundColor._rbk, backgroundColor._gbk, backgroundColor._bbk
        };

        MultMatVec(LLM, V[0], V[1], V[2], shift, lm);

        MultMatVec(LCM, BK, accumulator.ir1, accumulator.ir2, accumulator.ir3, shift, lm);

        TruncateAndSetMACAndIR((long)((int)(uint)colorCode.r * accumulator.ir1) << 4, 1, shift, lm);
        TruncateAndSetMACAndIR((long)((int)(uint)colorCode.g * accumulator.ir2) << 4, 2, shift, lm);
        TruncateAndSetMACAndIR((long)((int)(uint)colorCode.b * accumulator.ir3) << 4, 3, shift, lm);

        PushRGBFromMAC();
    }

    private static void MultMatVec(short[,] M, short Vx, short Vy, short Vz, byte shift, bool lm)
    {
        for (int i = 0; i < 3; i++)
        {
            TruncateAndSetMACAndIR(SignExtendMACResult(((long)M[i, 0] * Vx) + ((long)M[i, 1] * Vy), i + 1) +
                                    ((long)M[i, 2] * Vz), i + 1, shift, lm);
        }
    }

    private static void MultMatVec(short[,] M, int[] T, short Vx, short Vy, short Vz, byte shift, bool lm)
    {
        for (int i = 0; i < 3; i++)
        {
            TruncateAndSetMACAndIR(SignExtendMACResult(SignExtendMACResult(((long)T[i] << 12) + (M[i, 0] * Vx), i + 1) +
                (M[i, 1] * Vy), i + 1) +
                (M[i, 2] * Vz), i + 1, shift, lm);
        }
    }

    private static uint UNRDivide(uint lhs, uint rhs)
    {
        if (rhs * 2 <= lhs) return 0x1FFFF;

        int shift = (rhs == 0) ? 16 : Utilities.LeadingZeros((ushort)rhs);
        lhs <<= shift;
        rhs <<= shift;

        byte[] unr_table = new byte[257]
        {
            0xFF, 0xFD, 0xFB, 0xF9, 0xF7, 0xF5, 0xF3, 0xF1, 0xEF, 0xEE, 0xEC, 0xEA, 0xE8, 0xE6, 0xE4, 0xE3, //
            0xE1, 0xDF, 0xDD, 0xDC, 0xDA, 0xD8, 0xD6, 0xD5, 0xD3, 0xD1, 0xD0, 0xCE, 0xCD, 0xCB, 0xC9, 0xC8, //  00h..3Fh
            0xC6, 0xC5, 0xC3, 0xC1, 0xC0, 0xBE, 0xBD, 0xBB, 0xBA, 0xB8, 0xB7, 0xB5, 0xB4, 0xB2, 0xB1, 0xB0, //
            0xAE, 0xAD, 0xAB, 0xAA, 0xA9, 0xA7, 0xA6, 0xA4, 0xA3, 0xA2, 0xA0, 0x9F, 0x9E, 0x9C, 0x9B, 0x9A, //
            0x99, 0x97, 0x96, 0x95, 0x94, 0x92, 0x91, 0x90, 0x8F, 0x8D, 0x8C, 0x8B, 0x8A, 0x89, 0x87, 0x86, //
            0x85, 0x84, 0x83, 0x82, 0x81, 0x7F, 0x7E, 0x7D, 0x7C, 0x7B, 0x7A, 0x79, 0x78, 0x77, 0x75, 0x74, //  40h..7Fh
            0x73, 0x72, 0x71, 0x70, 0x6F, 0x6E, 0x6D, 0x6C, 0x6B, 0x6A, 0x69, 0x68, 0x67, 0x66, 0x65, 0x64, //
            0x63, 0x62, 0x61, 0x60, 0x5F, 0x5E, 0x5D, 0x5D, 0x5C, 0x5B, 0x5A, 0x59, 0x58, 0x57, 0x56, 0x55, //
            0x54, 0x53, 0x53, 0x52, 0x51, 0x50, 0x4F, 0x4E, 0x4D, 0x4D, 0x4C, 0x4B, 0x4A, 0x49, 0x48, 0x48, //
            0x47, 0x46, 0x45, 0x44, 0x43, 0x43, 0x42, 0x41, 0x40, 0x3F, 0x3F, 0x3E, 0x3D, 0x3C, 0x3C, 0x3B, //  80h..BFh
            0x3A, 0x39, 0x39, 0x38, 0x37, 0x36, 0x36, 0x35, 0x34, 0x33, 0x33, 0x32, 0x31, 0x31, 0x30, 0x2F, //
            0x2E, 0x2E, 0x2D, 0x2C, 0x2C, 0x2B, 0x2A, 0x2A, 0x29, 0x28, 0x28, 0x27, 0x26, 0x26, 0x25, 0x24, //
            0x24, 0x23, 0x22, 0x22, 0x21, 0x20, 0x20, 0x1F, 0x1E, 0x1E, 0x1D, 0x1D, 0x1C, 0x1B, 0x1B, 0x1A, //
            0x19, 0x19, 0x18, 0x18, 0x17, 0x16, 0x16, 0x15, 0x15, 0x14, 0x14, 0x13, 0x12, 0x12, 0x11, 0x11, //  C0h..FFh
            0x10, 0x0F, 0x0F, 0x0E, 0x0E, 0x0D, 0x0D, 0x0C, 0x0C, 0x0B, 0x0A, 0x0A, 0x09, 0x09, 0x08, 0x08, //
            0x07, 0x07, 0x06, 0x06, 0x05, 0x05, 0x04, 0x04, 0x03, 0x03, 0x02, 0x02, 0x01, 0x01, 0x00, 0x00, //
            0x00
        };

        uint divisor = rhs | 0x8000;
        int x = (int)(0x101 + (uint)(unr_table[((divisor & 0x7FFF) + 0x40) >> 7]));
        int d = (((int)divisor * -x) + 0x80) >> 8;
        uint recip = (uint)(((x * (0x20000 + d)) + 0x80) >> 8);

        uint result = (uint)(((ulong)lhs * recip + 0x8000) >> 16);

        return Math.Min(0x1FFFF, result);
    }

    private static void InterpolateColor(long in_MAC1, long in_MAC2, long in_MAC3, byte shift, bool lm)
    {
        TruncateAndSetMACAndIR(((long)farColor._rfc << 12) - in_MAC1, 1, shift, false);
        TruncateAndSetMACAndIR(((long)farColor._gfc << 12) - in_MAC2, 2, shift, false);
        TruncateAndSetMACAndIR(((long)farColor._bfc << 12) - in_MAC3, 3, shift, false);

        TruncateAndSetMACAndIR(accumulator.ir1 * accumulator.ir0 + in_MAC1, 1, shift, lm);
        TruncateAndSetMACAndIR(accumulator.ir2 * accumulator.ir0 + in_MAC2, 2, shift, lm);
        TruncateAndSetMACAndIR(accumulator.ir3 * accumulator.ir0 + in_MAC3, 3, shift, lm);
    }

    private static void SetOTZ(int value)
    {
        if (value < 0)
            value = 0;
        else if (value > 0xFFFF)
            value = 0xFFFF;

        averageZ = (ushort)value;
    }

    private static void TruncateAndSetMACAndIR(long value, int index, byte shift, bool lm)
    {
        value >>= shift;

        int value32 = (int)value;
        if (index == 0)
            mathsAccumulator.mac0 = value32;
        else if (index == 1)
            mathsAccumulator.mac1 = value32;
        else if (index == 2)
            mathsAccumulator.mac2 = value32;
        else
            mathsAccumulator.mac3 = value32;

        TruncateAndSetIR(value32, index, lm);
    }

    private static void TruncateAndSetIR(int value, int index, bool lm)
    {
        int MIN_VALUE = (index == 0) ? IR0_MIN_VALUE : IR123_MIN_VALUE;
        int MAX_VALUE = (index == 0) ? IR0_MAX_VALUE : IR123_MAX_VALUE;
        int actualMinValue = lm ? 0 : MIN_VALUE;

        if (value < actualMinValue)
            value = actualMinValue;
        else if (value > MAX_VALUE)
            value = MAX_VALUE;

        switch (index)
        {
            case 0:
                accumulator.ir0 = (short)value;
                break;
            case 1:
                accumulator.ir1 = (short)value;
                break;
            case 2:
                accumulator.ir2 = (short)value;
                break;
            default:
                accumulator.ir3 = (short)value;
                break;
        }
    }

    private static void TruncateAndSetMAC(long value, int index, byte shift)
    {
        value >>= shift;

        switch (index)
        {
            case 0:
                mathsAccumulator.mac0 = (int)(uint)(ulong)value;
                break;
            case 1:
                mathsAccumulator.mac1 = (int)(uint)(ulong)value;
                break;
            case 2:
                mathsAccumulator.mac2 = (int)(uint)(ulong)value;
                break;
            default:
                mathsAccumulator.mac3 = (int)(uint)(ulong)value;
                break;
        }
    }

    private static long SignExtendMACResult(long value, int index)
    {
        return SignExtend64(value, index == 0 ? 31 : 44);
    }

    private static void PushRGBFromMAC()
    {
        uint r = TruncateRGB((int)(uint)(mathsAccumulator.mac1 >> 4), 0);
        uint g = TruncateRGB((int)(uint)(mathsAccumulator.mac2 >> 4), 1);
        uint b = TruncateRGB((int)(uint)(mathsAccumulator.mac3 >> 4), 2);
        uint c = colorCode.code;

        colorFIFO.r0 = colorFIFO.r1;
        colorFIFO.g0 = colorFIFO.g1;
        colorFIFO.b0 = colorFIFO.b1;
        colorFIFO.cd0 = colorFIFO.cd1;

        colorFIFO.r1 = colorFIFO.r2;
        colorFIFO.g1 = colorFIFO.g2;
        colorFIFO.b1 = colorFIFO.b2;
        colorFIFO.cd1 = colorFIFO.cd2;

        colorFIFO.r2 = (byte)r;
        colorFIFO.g2 = (byte)g;
        colorFIFO.b2 = (byte)b;
        colorFIFO.cd2 = (byte)c;
    }

    private static void PushSXY(int x, int y)
    {
        if (x < -1024)
            x = -1024;
        else if (x > 1023)
            x = 1023;

        if (y < -1024)
            y = -1024;
        else if (y > 1023)
            y = 1023;

        screenXYFIFO.sx0 = screenXYFIFO.sx1;
        screenXYFIFO.sy0 = screenXYFIFO.sy1;
        screenXYFIFO.sx1 = screenXYFIFO.sx2;
        screenXYFIFO.sy1 = screenXYFIFO.sy2;
        screenXYFIFO.sx2 = (short)x;
        screenXYFIFO.sy2 = (short)y;
    }

    private static void PushSZ(int value)
    {
        if (value < 0)
            value = 0;
        else if (value > 0xFFFF)
            value = 0xFFFF;

        screenZFIFO.sz0 = screenZFIFO.sz1;
        screenZFIFO.sz1 = screenZFIFO.sz2;
        screenZFIFO.sz2 = screenZFIFO.sz3;
        screenZFIFO.sz3 = (short)value;
    }

    private static uint TruncateRGB(int value, int index)
    {
        if (value < 0 || value > 0xFF)
        {
            return (value < 0) ? 0U : 0xFF;
        }

        return (uint)value;
    }
}
