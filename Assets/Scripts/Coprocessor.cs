using System.Collections;
using System.Collections.Generic;
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
    public static Cop2Accumulator accumulator;
    public static Cop2Maths mathsAccumulator;
    public static Cop2RT rotationMatrix;
    public static Cop2LCM lightMatrix;
    public static Cop2LLM lightColorMatrix;
    public static Cop2ClrCode colorCode;
    public static Cop2TV translationVector;
    public static Cop2BC backgroundColor;
    public static Cop2FC farColor;
    public static Cop2ClrFIFO colorFIFO;

    static readonly long MAC0_MIN_VALUE = -(INT64_C(1) << 31);
    static readonly long MAC0_MAX_VALUE = (INT64_C(1) << 31) - 1;
    static readonly long MAC123_MIN_VALUE = -(INT64_C(1) << 43);
    static readonly long MAC123_MAX_VALUE = (INT64_C(1) << 43) - 1;
    static readonly int IR0_MIN_VALUE = 0x0000;
    static readonly int IR0_MAX_VALUE = 0x1000;
    static readonly int IR123_MIN_VALUE = -(INT64_C(1) << 15);
    static readonly int IR123_MAX_VALUE = (INT64_C(1) << 15) - 1;

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

    private static void MultMatVec(short[,] M, int[] T, short Vx, short Vy, short Vz, byte shift, bool lm)
    {
        for (int i = 0; i < 3; i++)
        {
            TruncateAndSetMACAndIR(SignExtendMACResult(SignExtendMACResult(((long)T[i] << 12) + (M[i, 0] * Vx), i + 1) +
                (M[i, 1] * Vy), i + 1) +
                (M[i, 2] * Vz), i + 1, shift, lm);
        }
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

        if (index == 0)
            accumulator.ir0 = (short)value;
        else if (index == 1)
            accumulator.ir1 = (short)value;
        else if (index == 2)
            accumulator.ir2 = (short)value;
        else
            accumulator.ir3 = (short)value;
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

    private static uint TruncateRGB(int value, int index)
    {
        if (value < 0 || value > 0xFF)
        {
            return (value < 0) ? 0U : 0xFF;
        }

        return (uint)value;
    }
}
