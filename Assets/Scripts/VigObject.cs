using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathExtended.Matrices;

[Serializable]
public struct Matrix3x3
{
    public short V00, V01, V02;
    public short V10, V11, V12;
    public short V20, V21, V22;

    public Vector3 EulerXYZ
    {
        get
        {
            double fV00, fV01, fV02;
            double fV10, fV11, fV12;
            double fV20, fV21, fV22;

            const int SHRT_MAX = 4096;
            fV00 = Math.Round((double)V00 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV01 = Math.Round((double)V01 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV02 = Math.Round((double)V02 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV10 = Math.Round((double)V10 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV11 = Math.Round((double)V11 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV12 = Math.Round((double)V12 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV20 = Math.Round((double)V20 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV21 = Math.Round((double)V21 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV22 = Math.Round((double)V22 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);

            double x, y, z;

            if (fV02 < 1)
            {
                if (fV02 > -1)
                {
                    y = Math.Atan2(fV02, fV22);
                    x = -Math.Atan2(fV12, fV22);
                    z = -Math.Atan2(-fV10, fV00);
                }
                else
                {
                    y = -Math.PI / 2f;
                    x = -Math.Atan2(fV10, fV11);
                    z = 0f;
                }
            }
            else
            {
                y = Math.PI / 2f;
                x = Math.Atan2(fV10, fV11);
                z = 0;
            }

            return new Vector3((float)0, (float)y, (float)0) * Mathf.Rad2Deg;
        }
    }

    public Vector3 EulerXZY
    {
        get
        {
            double fV00, fV01, fV02;
            double fV10, fV11, fV12;
            double fV20, fV21, fV22;

            const int SHRT_MAX = 0x7FFF;
            fV00 = Math.Round((double)V00 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV01 = Math.Round((double)V01 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV02 = Math.Round((double)V02 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV10 = Math.Round((double)V10 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV11 = Math.Round((double)V11 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV12 = Math.Round((double)V12 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV20 = Math.Round((double)V20 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV21 = Math.Round((double)V21 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV22 = Math.Round((double)V22 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);

            double x, y, z;

            if (fV01 < 1)
            {
                if (fV01 > -1)
                {
                    z = Math.Asin(-fV01);
                    x = Math.Atan2(fV21, fV11);
                    y = Math.Atan2(fV02, fV00);
                }
                else
                {
                    z = Math.PI / 2f;
                    x = -Math.Atan2(-fV20, fV22);
                    y = 0f;
                }
            }
            else
            {
                z = -Math.PI / 2f;
                x = Math.Atan2(-fV20, fV22);
                y = 0;
            }

            return new Vector3((float)x, (float)y, (float)z) * Mathf.Rad2Deg;
        }
    }

    public Vector3 EulerYXZ
    {
        get
        {
            double fV00, fV01, fV02;
            double fV10, fV11, fV12;
            double fV20, fV21, fV22;

            const int SHRT_MAX = 0x7FFF;
            fV00 = Math.Round((double)V00 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV01 = Math.Round((double)V01 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV02 = Math.Round((double)V02 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV10 = Math.Round((double)V10 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV11 = Math.Round((double)V11 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV12 = Math.Round((double)V12 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV20 = Math.Round((double)V20 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV21 = Math.Round((double)V21 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV22 = Math.Round((double)V22 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);

            double x, y, z;

            if (fV12 < 1)
            {
                if (fV12 > -1)
                {
                    x = Math.Asin(-fV12);
                    y = Math.Atan2(fV02, fV22);
                    z = Math.Atan2(fV10, fV11);
                }
                else
                {
                    x = Math.PI / 2f;
                    y = -Math.Atan2(-fV01, fV00);
                    z = 0f;
                }
            }
            else
            {
                x = -Math.PI / 2f;
                y = Math.Atan2(-fV01, fV00);
                z = 0;
            }

            return new Vector3((float)x, (float)y, (float)z) * Mathf.Rad2Deg;
        }
    }

    public Vector3 EulerYZX
    {
        get
        {
            double fV00, fV01, fV02;
            double fV10, fV11, fV12;
            double fV20, fV21, fV22;

            const int SHRT_MAX = 4096;
            fV00 = Math.Round((double)V00 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV01 = Math.Round((double)V01 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV02 = Math.Round((double)V02 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV10 = Math.Round((double)V10 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV11 = Math.Round((double)V11 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV12 = Math.Round((double)V12 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV20 = Math.Round((double)V20 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV21 = Math.Round((double)V21 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV22 = Math.Round((double)V22 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);

            double x, y, z;

            if (fV10 < 1)
            {
                if (fV10 > -1)
                {
                    z = Math.Asin(-fV10);
                    y = Math.Atan2(fV02, fV00);
                    x = Math.Atan2(fV12, fV11);
                }
                else
                {
                    z = -Math.PI / 2f;
                    y = -Math.Atan2(fV21, fV22);
                    x = 0f;
                }
            }
            else
            {
                z = Math.PI / 2f;
                y = Math.Atan2(fV21, fV22);
                x = 0;
            }
            
            return new Vector3((float)x, (float)y, (float)z) * Mathf.Rad2Deg;
        }
    }

    public Vector3 EulerZXY
    {
        get
        {
            double fV00, fV01, fV02;
            double fV10, fV11, fV12;
            double fV20, fV21, fV22;

            const int SHRT_MAX = 0x7FFF;
            fV00 = Math.Round((double)V00 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV01 = Math.Round((double)V01 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV02 = Math.Round((double)V02 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV10 = Math.Round((double)V10 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV11 = Math.Round((double)V11 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV12 = Math.Round((double)V12 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV20 = Math.Round((double)V20 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV21 = Math.Round((double)V21 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV22 = Math.Round((double)V22 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);

            double x, y, z;

            if (fV21 < 1)
            {
                if (fV21 > -1)
                {
                    x = Math.Asin(fV21);
                    z = Math.Atan2(-fV01, fV11);
                    y = Math.Atan2(-fV20, fV22);
                }
                else
                {
                    x = -Math.PI / 2f;
                    y = Math.Atan2(fV02, fV00);
                    z = 0f;
                }
            }
            else
            {
                x = Math.PI / 2f;
                y = Math.Atan2(fV02, fV00);
                z = 0f;
            }

            return new Vector3((float)x, (float)y, (float)z) * Mathf.Rad2Deg;
        }
    }

    public Vector3 EulerZYX
    {
        get
        {
            double fV00, fV01, fV02;
            double fV10, fV11, fV12;
            double fV20, fV21, fV22;

            const int SHRT_MAX = 0x7FFF;
            fV00 = Math.Round((double)V00 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV01 = Math.Round((double)V01 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV02 = Math.Round((double)V02 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV10 = Math.Round((double)V10 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV11 = Math.Round((double)V11 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV12 = Math.Round((double)V12 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV20 = Math.Round((double)V20 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV21 = Math.Round((double)V21 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV22 = Math.Round((double)V22 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);

            double x, y, z;

            if (fV20 < 1)
            {
                if (fV20 > -1)
                {
                    y = Math.Asin(-fV20);
                    z = Math.Atan2(fV10, fV00);
                    x = Math.Atan2(fV21, fV22);
                }
                else
                {
                    y = Math.PI / 2f;
                    z = -Math.Atan2(-fV12, fV11);
                    x = 0f;
                }
            }
            else
            {
                y = -Math.PI / 2f;
                z = Math.Atan2(-fV12, fV11);
                x = 0;
            }

            return new Vector3((float)x, (float)y, (float)z) * Mathf.Rad2Deg;
        }
    }

    public Vector3 Euler
    {
        get
        {
            double fV00, fV01, fV02;
            double fV10, fV11, fV12;
            double fV20, fV21, fV22;

            const int SHRT_MAX = 4095;
            fV00 = Math.Round((double)V00 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV01 = Math.Round((double)V01 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV02 = Math.Round((double)V02 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV10 = Math.Round((double)V10 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV11 = Math.Round((double)V11 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV12 = Math.Round((double)V12 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV20 = Math.Round((double)V20 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV21 = Math.Round((double)V21 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);
            fV22 = Math.Round((double)V22 / SHRT_MAX, 4, MidpointRounding.AwayFromZero);

            double x, y, z;
            x = 0;
            y = 0;
            z = 0;
            x = Math.Atan2(fV12, fV11);
            y = Math.Atan2(fV02, fV00);
            z = Math.Asin(fV10);

            return new Vector3((float)x, (float)y, (float)z) * Mathf.Rad2Deg;
        }
    }

    public Quaternion Matrix2Quaternion
    {
        get
        {
            double fV00, fV01, fV02;
            double fV10, fV11, fV12;
            double fV20, fV21, fV22;

            const int SHRT_MAX = 4096;
            fV00 = (double)V00 / SHRT_MAX;
            fV01 = (double)V01 / SHRT_MAX;
            fV02 = (double)V02 / SHRT_MAX;
            fV10 = (double)V10 / SHRT_MAX;
            fV11 = (double)V11 / SHRT_MAX;
            fV12 = (double)V12 / SHRT_MAX;
            fV20 = (double)V20 / SHRT_MAX;
            fV21 = (double)V21 / SHRT_MAX;
            fV22 = (double)V22 / SHRT_MAX;

            float tr = (float)(fV00 + fV11 + fV22);
            double qw, qx, qy, qz;

            if (tr > 0)
            {
                float S = (float)Math.Sqrt(tr + 1f) * 2;
                qw = 0.25 * S;
                qx = (fV21 - fV12) / S;
                qy = (fV02 - fV20) / S;
                qz = (fV10 - fV01) / S;
            }
            else if ((fV00 > fV11) & (fV00 > fV22))
            {
                float S = (float)Math.Sqrt(1.0 + fV00 - fV11 - fV22) * 2; // S=4*qx 
                qw = (fV21 - fV12) / S;
                qx = 0.25 * S;
                qy = (fV01 + fV10) / S;
                qz = (fV02 + fV20) / S;
            }
            else if (fV11 > fV22)
            {
                float S = (float)Math.Sqrt(1.0 + fV11 - fV00 - fV22) * 2; // S=4*qy
                qw = (fV02 - fV20) / S;
                qx = (fV01 + fV10) / S;
                qy = 0.25 * S;
                qz = (fV12 + fV21) / S;
            }
            else
            {
                float S = (float)Math.Sqrt(1.0 + fV22 - fV00 - fV11) * 2; // S=4*qz
                qw = (fV10 - fV01) / S;
                qx = (fV02 + fV20) / S;
                qy = (fV12 + fV21) / S;
                qz = 0.25 * S;
            }

            return new Quaternion((float)qx, (float)qy, (float)qz, (float)qw);
        }
    }

    public void Euler2Matrix(Vector3 euler)
    {
        const int SHRT_MAX = 0x7FFF;

        Matrix R_x = new Matrix(3, 3);
        R_x[1, 1] = 1f; R_x[1, 2] = 0f; R_x[1, 3] = 0f;
        R_x[2, 1] = 0f; R_x[2, 2] = Math.Cos(euler.x); R_x[2, 3] = -Math.Sin(euler.x);
        R_x[3, 1] = 0f; R_x[3, 2] = Math.Sin(euler.x); R_x[3, 3] = Math.Cos(euler.x);

        Matrix R_y = new Matrix(3, 3);
        R_y[1, 1] = Math.Cos(euler.y); R_y[1, 2] = 0f; R_y[1, 3] = Math.Sin(euler.y);
        R_y[2, 1] = 0f; R_y[2, 2] = 1f; R_y[2, 3] = 0f;
        R_y[3, 1] = -Math.Sin(euler.y); R_y[3, 2] = 0f; R_y[3, 3] = Math.Cos(euler.y);

        Matrix R_z = new Matrix(3, 3);
        R_z[1, 1] = Math.Cos(euler.z); R_z[1, 2] = -Math.Sin(euler.z); R_z[1, 3] = 0f;
        R_z[2, 1] = Math.Sin(euler.z); R_z[2, 2] = Math.Cos(euler.z); R_z[2, 3] = 0f;
        R_z[3, 1] = 0f; R_z[3, 2] = 0f; R_z[3, 3] = 1f;

        Matrix R = new Matrix(3, 3);
        R = R_z * R_x * R_y;

        V00 = (short)(R[1, 1] * SHRT_MAX);
        V01 = (short)(R[1, 2] * SHRT_MAX);
        V02 = (short)(R[1, 3] * SHRT_MAX);
        V10 = (short)(R[2, 1] * SHRT_MAX);
        V11 = (short)(R[2, 2] * SHRT_MAX);
        V12 = (short)(R[2, 3] * SHRT_MAX);
        V20 = (short)(R[3, 1] * SHRT_MAX);
        V21 = (short)(R[3, 2] * SHRT_MAX);
        V22 = (short)(R[3, 3] * SHRT_MAX);
    }

    public int GetValue32(int index)
    {
        int value = 0;

        if (index >= 5)
            index = 4;
        else if (index < 0)
            index = 0;

        switch (index)
        {
            case 0:
                value = V01 << 16 | (ushort)V00;
                break;
            case 1:
                value = V10 << 16 | (ushort)V02;
                break;
            case 2:
                value = V12 << 16 | (ushort)V11;
                break;
            case 3:
                value = V21 << 16 | (ushort)V20;
                break;
            default:
                value = V22;
                break;
        }

        return value;
    }

    public void SetValue32(int index, int value)
    {
        switch (index)
        {
            case 0:
                V00 = (short)value;
                V01 = (short)(value >> 16);
                break;
            case 1:
                V02 = (short)value;
                V10 = (short)(value >> 16);
                break;
            case 2:
                V11 = (short)value;
                V12 = (short)(value >> 16);
                break;
            case 3:
                V20 = (short)value;
                V21 = (short)(value >> 16);
                break;
            default:
                V22 = (short)value;
                break;
        }
    }
}

[Serializable]
public struct Matrix2x3
{
    public short M0, M1;
    public short M2, M3;
    public short M4, M5;

    public Matrix2x3(int x, int y, int z)
    {
        M0 = (short)(x & 0xFFFF);
        M1 = (short)(x >> 16);
        M2 = (short)(y & 0xFFFF);
        M3 = (short)(y >> 16);
        M4 = (short)(z & 0xFFFF);
        M5 = (short)(z >> 16);
    }

    public Matrix2x3(Vector3Int v3)
    {
        M0 = (short)(v3.x & 0xFFFF);
        M1 = (short)(v3.x >> 16);
        M2 = (short)(v3.y & 0xFFFF);
        M3 = (short)(v3.y >> 16);
        M4 = (short)(v3.z & 0xFFFF);
        M5 = (short)(v3.z >> 16);
    }

    public int X
    {
        get { return M1 << 16 | (ushort)M0; }
        set
        {
            M0 = (short)(value & 0xFFFF);
            M1 = (short)(value >> 16);
        }
    }

    public int Y
    {
        get { return M3 << 16 | (ushort)M2; }
        set
        {
            M2 = (short)(value & 0xFFFF);
            M3 = (short)(value >> 16);
        }
    }

    public int Z
    {
        get { return M5 << 16 | (ushort)M4; }
        set
        {
            M4 = (short)(value & 0xFFFF);
            M5 = (short)(value >> 16);
        }
    }
}

[Serializable]
public struct Matrix2x4
{
    public short M0, M1;
    public short M2, M3;
    public short M4, M5;
    public short M6, M7;

    public Matrix2x4(int x, int y, int z, int w)
    {
        M0 = (short)(x & 0xFFFF);
        M1 = (short)(x >> 16);
        M2 = (short)(y & 0xFFFF);
        M3 = (short)(y >> 16);
        M4 = (short)(z & 0xFFFF);
        M5 = (short)(z >> 16);
        M6 = (short)(w & 0xFFFF);
        M7 = (short)(w >> 16);
    }

    public int X
    {
        get { return M1 << 16 | (ushort)M0; }
        set
        {
            M0 = (short)(value & 0xFFFF);
            M1 = (short)(value >> 16);
        }
    }

    public int Y
    {
        get { return M3 << 16 | (ushort)M2; }
        set
        {
            M2 = (short)(value & 0xFFFF);
            M3 = (short)(value >> 16);
        }
    }

    public int Z
    {
        get { return M5 << 16 | (ushort)M4; }
        set
        {
            M4 = (short)(value & 0xFFFF);
            M5 = (short)(value >> 16);
        }
    }

    public int W
    {
        get { return M7 << 16 | (ushort)M6; }
        set
        {
            M6 = (short)(value & 0xFFFF);
            M7 = (short)(value >> 16);
        }
    }
}

[Serializable]
public struct VigTransform
{
    public Matrix3x3 rotation;
    public short padding;
    public Vector3Int position;
}

public class VigObject : MonoBehaviour
{
    public uint flags; //0x04
    public byte type; //0x08
    public byte ai; //0x09
    public short id; //0x0A

    public VigObject child; //0x0C
    public VigObject child2; //0x10
    public VigObject parent; //0x14

    public sbyte DAT_18; //0x18
    public byte DAT_19; //0x19
    public short DAT_1A; //0x1A

    public ushort maxHalfHealth; //0x1C
    public ushort maxFullHealth; //0x1E

    public VigTransform vTransform; //0x20

    public VigMesh vMesh; //0x40

    public Vector3Int screen; //0x4C
    public Vector3Int vr; //0x44
    public ushort unk4; //0x4A

    public int DAT_58; //0x58
    public XOBF_DB vData; //0x5C
    public byte[] vCollider; //0x60
    public int unk3; //0x64
    public VigMesh vLOD; //0x68
    public int DAT_6C; //0x6C
    public VigShadow vShadow; //0x70
    public VigObject PDAT_74; //0x74
    public int IDAT_74; //0x74
    public VigObject PDAT_78; //0x78
    public int IDAT_78; //0x78
    public VigObject PDAT_7C; //0x7C
    public int IDAT_7C; //0x7C

    public Matrix2x4 physics1;
    public Matrix2x4 physics2;

    public Vector3Int vectorUnk1; //0xA0
    public short unk1; //0xA6

    // Start is called before the first frame update
    protected virtual void Start()
    {
        vTransform.position = new Vector3Int(
            (int)(transform.localPosition.x * GameManager.translateFactor), 
            (int)(-transform.localPosition.y * GameManager.translateFactor), 
            (int)(transform.localPosition.z * GameManager.translateFactor));

        ApplyRotationMatrix();
        //screen = position;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        transform.localPosition = new Vector3(
            (float)vTransform.position.x / GameManager.translateFactor, 
            (float)-vTransform.position.y / GameManager.translateFactor, 
            (float)vTransform.position.z / GameManager.translateFactor);

        transform.localRotation = vTransform.rotation.Matrix2Quaternion;
        transform.localEulerAngles = new Vector3(-transform.localEulerAngles.x, transform.localEulerAngles.y, -transform.localEulerAngles.z);
    }

    public virtual uint Execute(int arg1, int arg2)
    {
        return 0;
    }

    // FUN_2CF74
    public void ApplyTransformation()
    {
        vTransform.position = screen;
        vTransform.rotation = Utilities.RotMatrixYXZ_gte(vr);
        vTransform.padding = 0;
    }

    //FUN_2CF44
    public void ApplyRotationMatrix()
    {
        vTransform.rotation = Utilities.RotMatrixYXZ_gte(vr);
        vTransform.padding = 0;
    }

    public void FUN_24700(short x, short y, short z)
    {
        Coprocessor.rotationMatrix.rt11 = vTransform.rotation.V00;
        Coprocessor.rotationMatrix.rt12 = vTransform.rotation.V01;
        Coprocessor.rotationMatrix.rt13 = vTransform.rotation.V02;
        Coprocessor.rotationMatrix.rt21 = vTransform.rotation.V10;
        Coprocessor.rotationMatrix.rt22 = vTransform.rotation.V11;
        Coprocessor.rotationMatrix.rt23 = vTransform.rotation.V12;
        Coprocessor.rotationMatrix.rt31 = vTransform.rotation.V20;
        Coprocessor.rotationMatrix.rt32 = vTransform.rotation.V21;
        Coprocessor.rotationMatrix.rt33 = vTransform.rotation.V22;
        Coprocessor.accumulator.ir1 = 0x1000;
        Coprocessor.accumulator.ir2 = z;
        Coprocessor.accumulator.ir3 = (short)-y;
        Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.IR, _MVMVA_TRANSLATION_VECTOR.None, 12, false);

        vTransform.rotation.V00 = Coprocessor.accumulator.ir1;
        vTransform.rotation.V10 = Coprocessor.accumulator.ir2;
        vTransform.rotation.V20 = Coprocessor.accumulator.ir3;

        Coprocessor.vector0.vx0 = (short)-z;
        Coprocessor.vector0.vy0 = 0x1000;
        Coprocessor.vector0.vz0 = x;
        Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.V0, _MVMVA_TRANSLATION_VECTOR.None, 12, false);

        vTransform.rotation.V01 = Coprocessor.accumulator.ir1;
        vTransform.rotation.V11 = Coprocessor.accumulator.ir2;
        vTransform.rotation.V21 = Coprocessor.accumulator.ir3;

        Coprocessor.vector1.vx1 = y;
        Coprocessor.vector1.vy1 = (short)-x;
        Coprocessor.vector1.vz1 = 0x1000;
        Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.V1, _MVMVA_TRANSLATION_VECTOR.None, 12, false);

        vTransform.rotation.V02 = Coprocessor.accumulator.ir1;
        vTransform.rotation.V12 = Coprocessor.accumulator.ir2;
        vTransform.rotation.V22 = Coprocessor.accumulator.ir3;
    }

    public VigTransform FUN_2AE18()
    {
        VigTransform container = new VigTransform
        {
            rotation = new Matrix3x3
            {
                V00 = 0,
                V01 = (short)-physics2.M4,
                V02 = physics2.M2,
                V10 = physics2.M4,
                V11 = 0,
                V12 = (short)-physics2.M0,
                V20 = (short)-physics2.M2,
                V21 = physics2.M0,
                V22 = 0
            },
            position = new Vector3Int
            (physics1.X, physics1.Y, physics1.Z)
        };

        container.rotation = Utilities.FUN_247C4(vTransform.rotation, container.rotation);
        return container;
    }

    public VigTransform FUN_2AEAC()
    {
        VigTransform output = new VigTransform();

        output.rotation.V22 = 0;
        output.rotation.V11 = 0;
        output.rotation.V00 = 0;
        output.rotation.V21 = physics2.M0;
        output.rotation.V12 = (short)-physics2.M0;
        output.rotation.V02 = physics2.M2;
        output.rotation.V20 = (short)-physics2.M2;
        output.rotation.V10 = physics2.M4;
        output.rotation.V01 = (short)-physics2.M4;

        output.position = Utilities.FUN_2426C(vTransform.rotation, physics1);
        return output;
    }

    public void FUN_2AFF8(Vector3Int v1, Vector3Int v2)
    {
        physics1.X = physics1.X + v1.x;
        physics1.Y = physics1.Y + v1.y;
        physics1.Z = physics1.Z + v1.z;

        int iVar1 = v2.x * vectorUnk1.x; //r3

        if (iVar1 < 0)
            iVar1 += 63;

        int iVar2 = physics2.X + (iVar1 >> 6); //r4
        iVar1 = -0x8000;
        
        if (-0x8001 < iVar2)
        {
            iVar1 = 0x7FFF;

            if (iVar2 < iVar1)
                iVar1 = iVar2;
        }

        physics2.X = iVar1;
        iVar1 = v2.y * vectorUnk1.y;

        if (iVar1 < 0)
            iVar1 += 63;

        iVar2 = physics2.Y + (iVar1 >> 6);
        iVar1 = -0x8000;

        if (-0x8001 < iVar2)
        {
            iVar1 = 0x7FFF;

            if (iVar2 < iVar1)
                iVar1 = iVar2;
        }

        physics2.Y = iVar1;
        iVar1 = v2.z * vectorUnk1.z;

        if (iVar1 < 0)
            iVar1 += 63;

        iVar2 = physics2.Z + (iVar1 >> 6);
        iVar1 = -0x8000;

        if (-0x8001 < iVar2)
        {
            iVar1 = 0x7FFF;

            if (iVar2 < iVar1)
                iVar1 = iVar2;
        }

        physics2.Z = iVar1;
        iVar2 = physics2.X;

        if (iVar2 < 0)
            iVar2 += 127;

        int iVar3 = physics2.Y; //r6

        if (iVar3 < 0)
            iVar3 += 127;

        if (iVar1 < 0)
            iVar1 += 127;

        FUN_24700((short)(iVar2 >> 7), (short)(iVar3 >> 7), (short)(iVar1 >> 7));

        iVar1 = physics1.X;

        if (iVar1 < 0)
            iVar1 += 127;

        vTransform.position.x += iVar1 >> 7;
        iVar2 = physics1.Y;

        if (iVar2 < 0)
            iVar2 += 127;

        vTransform.position.y += iVar2 >> 7;
        iVar1 = physics1.Z;

        if (iVar1 < 0)
            iVar1 += 127;

        vTransform.position.z += iVar1 >> 7;

        vTransform.rotation = Utilities.MatrixNormal(vTransform.rotation);
    }

    public void FUN_2B1FC(Vector3Int v1, Vector3Int v2)
    {
        int iVar1;
        Vector3Int local_10;

        local_10 = Utilities.FUN_24094(vTransform.rotation, v1);
        Coprocessor.rotationMatrix.rt11 = (short)(v2.x >> 4 & 0xFFFF);
        Coprocessor.rotationMatrix.rt12 = (short)(v2.x >> 4 >> 16);
        Coprocessor.rotationMatrix.rt22 = (short)(v2.y >> 4 & 0xFFFF);
        Coprocessor.rotationMatrix.rt23 = (short)(v2.y >> 4 >> 16);
        Coprocessor.rotationMatrix.rt33 = (short)(v2.z >> 4);
        Coprocessor.accumulator.ir1 = (short)(v1.x >> 3);
        Coprocessor.accumulator.ir2 = (short)(v1.y >> 3);
        Coprocessor.accumulator.ir3 = (short)(v1.z >> 3);
        Coprocessor.ExecuteOP(12, false);

        physics1.X += local_10.x;
        physics1.Y += local_10.y;
        physics1.Z += local_10.z;

        iVar1 = Coprocessor.mathsAccumulator.mac1;
        iVar1 = iVar1 * vectorUnk1.x;

        if (iVar1 < 0)
            iVar1 += 63;

        physics2.X += iVar1 >> 6;
        iVar1 = Coprocessor.mathsAccumulator.mac2;
        iVar1 = iVar1 * vectorUnk1.y;

        if (iVar1 < 0)
            iVar1 += 63;

        physics2.Y += iVar1 >> 6;
        iVar1 = Coprocessor.mathsAccumulator.mac3;
        iVar1 = iVar1 * vectorUnk1.z;

        if (iVar1 < 0)
            iVar1 += 63;

        physics2.Z += iVar1 >> 6;
    }

    public int FUN_2CFBC(Vector3Int pos, out Vector3Int normalVector3, out TileData normalTile)
    {
        GameManager gameManager = GameManager.instance;
        TileData tile = gameManager.terrain.GetTileByPosition((uint)pos.x, (uint)pos.z); //r21
        int vertexHeight = 0x2f0000; //r16

        if ((tile.unk1 & 4) == 0)
            vertexHeight = gameManager.terrain.FUN_1B750((uint)pos.x, (uint)pos.z);
        else
            vertexHeight = 0x2ff800;

        if (PDAT_74 != null)
        {
            // ...
        }

        normalVector3 = gameManager.terrain.FUN_1B998((uint)pos.x, (uint)pos.z);
        normalVector3 = Utilities.VectorNormal(normalVector3);
        normalTile = tile;

        return vertexHeight;
    }

    public int FUN_2CFBC(Vector3Int pos, out TileData normalTile)
    {
        GameManager gameManager = GameManager.instance;
        TileData tile = gameManager.terrain.GetTileByPosition((uint)pos.x, (uint)pos.z); //r21
        int vertexHeight = 0x2f0000; //r16

        if ((tile.unk1 & 4) == 0)
            vertexHeight = gameManager.terrain.FUN_1B750((uint)pos.x, (uint)pos.z);
        else
            vertexHeight = 0x2ff800;

        if (PDAT_74 != null)
        {
            // ...
        }

        normalTile = tile;

        return vertexHeight;
    }

    public int FUN_2CFBC(Vector3Int pos)
    {
        GameManager gameManager = GameManager.instance;
        TileData tile = gameManager.terrain.GetTileByPosition((uint)pos.x, (uint)pos.z); //r21
        int vertexHeight = 0x2f0000; //r16

        if ((tile.unk1 & 4) == 0)
            vertexHeight = gameManager.terrain.FUN_1B750((uint)pos.x, (uint)pos.z);
        else
            vertexHeight = 0x2ff800;

        if (PDAT_74 != null)
        {
            // ...
        }

        return vertexHeight;
    }

    public VigObject FUN_2CA1C()
    {
        VigObject oVar1;
        ConfigContainer container = FUN_2C9A4();

        if (container == null)
            oVar1 = null;
        else
        {
            oVar1 = null;
            //...
        }

        return oVar1;
    }

    public void FUN_2C958()
    {
        VigObject oVar1;

        FUN_2C7D0();
        oVar1 = child2;

        while (oVar1 != null)
        {
            oVar1.FUN_2C958();
            oVar1 = child;
        }
    }

    public int FUN_2D1DC()
    {
        int iVar1;
        int iVar2;
        int iVar3;
        VigObject oVar4;
        int iVar5;

        iVar5 = 0;

        if (vMesh != null)
            iVar5 = (int)vMesh.DAT_18;

        oVar4 = child2;

        while(oVar4 != null)
        {
            iVar1 = oVar4.FUN_2D1DC();
            iVar2 = Utilities.FUN_29E84(vTransform.position);
            iVar3 = iVar1 + iVar2;

            if (iVar1 + iVar2 < iVar5)
                iVar3 = iVar5;

            oVar4 = oVar4.child;
            iVar5 = iVar3;
        }

        DAT_58 = iVar5;
        return iVar5;
    }

    public void FUN_2D368(VigTransform param1)
    {
        VigTransform t2;
        bool local_2e;

        if ((flags & 2) == 0)
        {
            t2 = Utilities.CompMatrixLV(param1, vTransform);
            t2.padding = 0;

            if (param1.padding != 0 || vTransform.padding != 0)
                t2.padding = 1;

            if ((flags & 0x10) != 0)
            {
                if ((flags & 0x400) == 0)
                {
                    if (t2.padding == 0)
                        t2.rotation = GameManager.instance.DAT_EE0.rotation;
                    else
                        t2.rotation = Utilities.FUN_2A4A4(t2.rotation);
                }
                else
                    t2.rotation = vTransform.rotation;
            }

            if (vMesh != null)
                vMesh.FUN_2D2A8(t2);

            if (child2 != null)
                child2.FUN_2D368(t2);
        }

        if (child != null)
            child.FUN_2D368(param1);
    }

    public void FUN_2D5EC(VigTransform param1, Vector3Int param2, Vector3Int param3)
    {
        VigTransform t2;

        if ((flags & 2) == 0)
        {
            t2 = Utilities.CompMatrixLV(param1, vTransform);
            t2.padding = 0;

            if (param1.padding != 0 || vTransform.padding != 0)
                t2.padding = 1;

            if ((flags & 0x10) != 0)
            {
                if ((flags & 0x400) == 0)
                {
                    if (t2.padding == 0)
                    {
                        t2.rotation.SetValue32(0, 0);
                        t2.rotation.SetValue32(1, 0);
                        t2.rotation.SetValue32(2, 0);
                        t2.rotation.SetValue32(3, 0);
                        t2.rotation.SetValue32(4, 0);
                    }
                    else
                        t2.rotation = Utilities.FUN_2A4A4(t2.rotation);
                }
                else
                    t2.rotation = vTransform.rotation;
            }

            if (vMesh != null)
                vMesh.FUN_2D4D4(t2, param2, param3);

            if (child2 != null)
                child2.FUN_2D5EC(t2, param2, param3);
        }

        if (child != null)
            child.FUN_2D5EC(param1, param2, param3);
    }

    public void FUN_2D778(VigTransform param1)
    {
        VigTransform t2;
        
        if ((flags & 2) == 0)
        {
            t2 = Utilities.CompMatrixLV(param1, vTransform);

            if ((flags & 0x10) != 0)
            {
                if ((flags & 0x400) == 0)
                    t2.rotation = Utilities.FUN_2A4A4(t2.rotation);
                else
                    t2.rotation = vTransform.rotation;
            }

            if (vMesh != null)
                vMesh.FUN_21F70(t2);

            if (child2 != null)
                child2.FUN_2D778(t2);
        }

        if (child != null)
            child.FUN_2D778(param1);
    }

    public Matrix3x3 FUN_2D884(VigTransform param1)
    {
        VigObject oVar6;
        VigTransform auStack32;

        if ((flags & 0x1010) == 0x1000)
        {
            if ((flags & 0x400) == 0)
            {
                if (vTransform.padding == 0)
                    param1.rotation = GameManager.instance.DAT_EE0.rotation;
                else
                    param1.rotation = Utilities.FUN_2A4A4(param1.rotation);
            }
            else
            {
                param1.rotation = GameManager.DAT_878.rotation;
            }
        }

        vLOD.FUN_21F70(param1);
        oVar6 = child2;

        while (oVar6 != null)
        {
            if ((oVar6.flags & 2) == 0 && oVar6.vLOD != null)
            {
                auStack32 = Utilities.CompMatrixLV(param1, oVar6.vTransform);
                auStack32.padding = 0;

                if (param1.padding != 0 || oVar6.vTransform.padding != 0)
                    auStack32.padding = 1;

                auStack32.rotation = oVar6.FUN_2D884(auStack32);
            }

            oVar6 = oVar6.child;
        }

        return param1.rotation;
    }

    public virtual int FUN_2DD78(HitDetection param1)
    {
        return 0;
    }

    public uint FUN_2EC7C()
    {
        uint uVar1;
        VigObject oVar2;
        uint uVar3;

        oVar2 = child2;
        uVar3 = 0;

        while(oVar2 != null)
        {
            uVar1 = oVar2.FUN_2EC7C();
            oVar2 = oVar2.child;
            uVar3 |= uVar1;
        }

        if (uVar3 != 0)
            flags |= 0x800;

        return uVar3 | (uint)(vCollider != null ? 1 : 0);
    }

    public bool FUN_3066C()
    {
        int iVar1;
        bool bVar2;

        ApplyTransformation();

        if (!GetType().IsSubclassOf(typeof(VigObject)))
            iVar1 = 0;
        else
            iVar1 = (int)Execute(1, 0);

        bVar2 = false;

        if (-1 < iVar1)
        {
            if ((flags & 8) != 0)
            {
                if (vShadow == null)
                    FUN_4C98C();

                FUN_4C4F4();
            }

            FUN_305FC();
            bVar2 = true;
        }

        return bVar2;
    }

    public void FUN_3BFC0()
    {
        int iVar1;
        int iVar2;
        uint uVar3;
        uint uVar4;
        Vector3Int local_18;

        iVar1 = physics1.X;
        iVar2 = -iVar1;
        physics1.X = iVar2;
        physics1.Z = -physics1.Z;
        physics1.Y = -physics1.Y;

        if (0 < iVar1)
            iVar2 += 63;

        iVar1 = physics1.Y;
        vTransform.position.x += iVar2 >> 6;

        if (iVar1 < 0)
            iVar1 += 63;

        iVar2 = physics1.Z;
        vTransform.position.y += iVar1 >> 6;

        if (iVar2 < 0)
            iVar2 += 63;

        vTransform.position.z += iVar2 >> 6;
        TileData tile = VigTerrain.instance.GetTileByPosition
            ((uint)vTransform.position.x, (uint)vTransform.position.z);

        if (tile.unk2[3] == 7)
        {
            uVar3 = (uint)vTransform.position.x;
            uVar4 = (uint)vTransform.position.z;
            local_18 = new Vector3Int(-(int)uVar3, 0, -(int)uVar4);
            local_18 = Utilities.VectorNormal(local_18);

            do
            {
                uVar3 += (uint)local_18.x;
                uVar4 += (uint)local_18.z;
                tile = VigTerrain.instance.GetTileByPosition(uVar3, uVar4);
            } while (tile.unk2[3] == 7);

            vTransform.position.x = (int)((uVar3 & 0xffff0000) + 0x8000);
            vTransform.position.z = (int)((uVar4 & 0xffff0000) + 0x8000);
        }
    }

    public void FUN_4BAFC(Vector3Int position)
    {
        position.x = position.x - screen.x;
        position.z = position.z - screen.z;
        position.y = position.y - screen.y;

        vr.y = Utilities.Ratan2(position.x, position.z);
        int iVar1 = Utilities.LeadingZeros(position.x);
        int iVar2 = Utilities.LeadingZeros(position.z);

        if (iVar1 < iVar2)
            iVar2 = iVar1;

        if (iVar2 < 18)
        {
            uint uVar3 = 18 - (uint)iVar2;
            position.x = position.x >> (int)(uVar3 & 31);
            position.y = position.y >> (int)(uVar3 & 31);
            position.z = position.z >> (int)(uVar3 & 31);
        }

        int iVar4 = (int)Utilities.SquareRoot(position.x * position.x + position.z * position.z);
        vr.x = Utilities.Ratan2(-position.y, iVar4);
    }

    public void FUN_4C4F4()
    {
        TileData tVar1;
        int iVar2;
        int iVar3;
        Vector3Int local_60;
        Vector3Int local_58;
        Matrix3x3 local_48;
        Matrix3x3 auStack40;
        VigTerrain terrain = GameManager.instance.terrain;

        tVar1 = terrain.GetTileByPosition((uint)vTransform.position.x, (uint)vTransform.position.z);

        if ((tVar1.unk1 & 4) == 0)
            iVar2 = terrain.FUN_1B750((uint)vTransform.position.x, (uint)vTransform.position.z);
        else
            iVar2 = 0x2ff800;

        vShadow.vTransform.position.x = vTransform.position.x;
        vShadow.vTransform.position.z = vTransform.position.z;

        if (PDAT_74 != null)
        {
            iVar3 = PDAT_74.FUN_2F710(iVar2, vTransform.position, out local_60);

            if (iVar3 != 0)
                goto LAB_4C5B8;

            if (PDAT_78 == null)
                goto LAB_4C5C0;

            iVar3 = PDAT_78.FUN_2F710(iVar2, vTransform.position, out local_60);

            if (iVar3 == 0)
                goto LAB_4C5C0;

            LAB_4C5B8:
            vShadow.vTransform.position.y = iVar3;
            goto LAB_4C5D4;
        }

        LAB_4C5C0:
        vShadow.vTransform.position.y = iVar2;
        local_60 = terrain.FUN_1BB50(vTransform.position.x, vTransform.position.z);
        LAB_4C5D4:
        if ((vShadow.vMesh.DAT_00 & 8) == 0)
        {
            local_48 = new Matrix3x3();
            local_48.V00 = 0x1000;
            local_48.V01 = 0;
            local_48.V02 = 0;
            local_48.V11 = 0;
            local_48.V20 = 0;
            local_48.V21 = 0;
            local_48.V22 = 0x1000;

            if (local_60.y == 0)
                local_48.V10 = (short)(local_60.x * -16);
            else
                local_48.V10 = (short)((local_60.x * -4096) / local_60.y);

            if (local_60.y == 0)
                local_48.V12 = (short)(local_60.z * -16);
            else
                local_48.V12 = (short)((local_60.z * -4096) / local_60.y);

            local_58 = new Vector3Int();
            local_58.x = vShadow.DAT_24;

            if (vTransform.rotation.V11 < 1)
                local_58.x = -local_58.x;

            local_58.y = 0;
            local_58.z = vShadow.DAT_28;
            auStack40 = Utilities.FUN_2449C(vTransform.rotation, local_58);
            vShadow.vTransform.rotation = Utilities.FUN_247C4(local_48, auStack40);
        }
        else
        {
            iVar2 = vShadow.DAT_24;
            vShadow.vTransform.rotation.V21 = 0;
            vShadow.vTransform.rotation.V20 = 0;
            vShadow.vTransform.rotation.V11 = 0;
            vShadow.vTransform.rotation.V02 = 0;
            vShadow.vTransform.rotation.V01 = 0;
            vShadow.vTransform.rotation.V22 = (short)iVar2;
            vShadow.vTransform.rotation.V00 = (short)iVar2;
            vShadow.vTransform.rotation.V10 = (short)((-local_60.x * iVar2) / local_60.y);
            vShadow.vTransform.rotation.V12 = (short)((-local_60.z * iVar2) / local_60.y);
        }
    }

    public void FUN_4C98C()
    {
        VigMesh mVar1;

        mVar1 = GameManager.instance.levelManager.DAT_C61C0.FUN_2CB74(gameObject, 92);
        FUN_4C7E0(mVar1);
    }

    public int FUN_4DCD8()
    {
        int configIndex = (DAT_1A << 3) - DAT_1A << 2;
        int nextContainer = vData.configContainers[configIndex / 0x1C].next;
        int iVar1 = 0;

        while (nextContainer != -1)
        {
            configIndex = (nextContainer << 3) - nextContainer << 2;
            int flag = vData.configContainers[configIndex / 0x1C].flag >> 12;

            if (flag == 15)
            {
                nextContainer = vData.configContainers[configIndex / 0x1C].next;
                iVar1++;
            }
            else
            {
                nextContainer = vData.configContainers[configIndex / 0x1C].previous;
            }
        }

        return iVar1;
    }

    private bool FUN_2C7D0()
    {
        ushort uVar1;
        VigMesh mVar2;
        int iVar3;
        int iVar4;
        List<ConfigContainer> ccVar5;

        if (vData != null)
        {
            ccVar5 = vData.ini.configContainers;
            uVar1 = (ushort)ccVar5[id].next;

            while(uVar1 != 0xffff)
            {
                iVar4 = uVar1;

                if (ccVar5[iVar4].flag >> 12 == 12)
                {
                    if ((ccVar5[iVar4].flag & 0x800) != 0)
                        flags |= 0x1000;

                    if ((ccVar5[iVar4].flag & 0x7ff) == 0x7ff)
                        vLOD = null;
                    else
                    {
                        if (((ccVar5[iVar4].flag ^ ccVar5[id].flag) & 0x7ff) == 0)
                            vLOD = vMesh;
                        else
                        {
                            mVar2 = vData.FUN_1FD18(gameObject, (ushort)ccVar5[iVar4].flag & 0x7ffU);
                            vLOD = mVar2;
                        }
                    }

                    iVar3 = ccVar5[iVar4].objID * 0x10000;

                    if (ccVar5[iVar4].objID == 0)
                        iVar3 = 0;

                    iVar3 *= 255;

                    if (iVar3 < 0)
                        iVar3 += 255;

                    DAT_6C = iVar3 >> 8;
                    return true;
                }

                uVar1 = (ushort)ccVar5[iVar4].previous;
            }
        }

        vLOD = null;
        DAT_6C = 0;
        return false;
    }

    private ConfigContainer FUN_2C9A4()
    {
        int configIndex = (DAT_1A << 3) - DAT_1A << 2;
        short sVar1 = vData.configContainers[configIndex / 0x1C].next;
        ConfigContainer container;

        while (true)
        {
            if (sVar1 == -1)
                return null;

            configIndex = (sVar1 << 3) - sVar1 << 2;
            configIndex = configIndex / 0x1C;
            container = vData.configContainers[configIndex];

            if ((uint)container.flag >> 12 == 11)
                break;

            sVar1 = container.previous;
        }

        return container;
    }

    private int FUN_2F16C(VigTransform param1, int param2, Vector3Int param3, out Vector3Int param4)
    {
        short sVar1;
        long lVar2;
        int puVar3;
        int iVar4;
        int iVar5;
        uint uVar6;
        int iVar7;
        uint uVar8;
        int iVar9;
        int psVar9;
        int psVar10;
        int iVar11;
        int iVar12;
        uint uVar13;
        uint uVar14;
        Vector3Int local_60;
        Vector3Int local_50;
        Vector3Int local_40;
        uint local_30;
        int local_2c;
        param4 = new Vector3Int(); //not in the original code

        if (vCollider != null)
        {
            MemoryStream msCollider = new MemoryStream(vCollider);

            using (BinaryReader brCollider = new BinaryReader(msCollider, Encoding.Default, true))
            {
                local_60 = new Vector3Int(
                    param3.x - param1.position.x,
                    param3.y - param1.position.y,
                    param3.z - param1.position.z);
                sVar1 = brCollider.ReadInt16(0);
                psVar10 = 0;

                while(sVar1 != 0)
                {
                    if (brCollider.ReadUInt16(psVar10) == 1)
                    {
                        local_50 = Utilities.FUN_2426C(param1.rotation, 
                            new Matrix2x4(local_60.x, local_60.y, local_60.z, 0));
                        psVar9 = psVar10 + 4;

                        if (local_50.x < brCollider.ReadInt32(psVar9 + 12) && brCollider.ReadInt32(psVar10 + 4) < local_50.x && 
                            local_50.z < brCollider.ReadInt32(psVar9 + 20) && brCollider.ReadInt32(psVar9 + 8) < local_50.z && 
                            local_50.y < brCollider.ReadInt32(psVar9 + 16))
                        {
                            iVar12 = brCollider.ReadInt32(psVar9 + 4);

                            if (local_50.y < iVar12 + 0x2800 && iVar12 - 0x5000 < local_50.y)
                            {
                                if (param1.position.y + iVar12 < param2)
                                    goto LAB_2F2F0;

                                psVar10 += 28;

                                if (!(param2 + 0x10000 < param3.y))
                                    goto LAB_2F568;
                                
                                LAB_2F2F0:
                                param4 = new Vector3Int
                                    (-param1.rotation.V01, -param1.rotation.V11, -param1.rotation.V21);
                                local_50.y = brCollider.ReadInt32(psVar9 + 4);
                                local_50 = Utilities.FUN_24094(param1.rotation, local_50);
                                return param1.position.y + local_50.y;
                            }
                        }

                        psVar10 += 28;
                    }
                    else
                    {
                        iVar12 = 0x7fff0000;

                        if (brCollider.ReadUInt16(psVar10) == 2)
                        {
                            uVar8 = 0x80010000;
                            Utilities.SetRotMatrix(param1.rotation);
                            local_40 = new Vector3Int(); //not in original code
                            iVar9 = 0;

                            if (brCollider.ReadUInt16(psVar10 + 2) != 0)
                            {
                                iVar11 = 4;
                                puVar3 = psVar10 + iVar11;

                                do
                                {
                                    Coprocessor.vector0.vx0 = brCollider.ReadInt16(puVar3);
                                    Coprocessor.vector0.vy0 = brCollider.ReadInt16(puVar3 + 2);
                                    Coprocessor.vector0.vz0 = brCollider.ReadInt16(puVar3 + 4);
                                    Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.V0, _MVMVA_TRANSLATION_VECTOR.None, 12, false);
                                    uVar13 = (uint)((long)brCollider.ReadInt32(puVar3 + 8) * 4096);
                                    iVar4 = Coprocessor.accumulator.ir1;
                                    uVar14 = (uint)((long)iVar4 * local_60.x);
                                    uVar6 = uVar13 - uVar14;
                                    iVar5 = Coprocessor.accumulator.ir3;
                                    local_30 = (uint)((long)iVar5 * local_60.z);
                                    local_2c = (int)((ulong)((long)iVar5 * local_60.z) >> 32);
                                    iVar5 = (int)(uVar6 - local_30);
                                    iVar7 = ((((int)((ulong)((long)brCollider.ReadInt32(puVar3 + 8) * 4096) >> 32) -
                                               (int)((ulong)((long)iVar4 * local_60.x) >> 32)) -
                                               (int)(uVar13 < uVar14 ? 1 : 0)) - local_2c) - (int)(uVar6 < local_30 ? 1 : 0);
                                    iVar4 = Coprocessor.accumulator.ir2;

                                    if (iVar4 < 0)
                                    {
                                        iVar4 = Coprocessor.accumulator.ir2;
                                        lVar2 = Utilities.Divdi3(iVar5, iVar7, iVar4, iVar4 >> 31);

                                        if ((int)uVar8 < (int)lVar2)
                                        {
                                            local_40 = new Vector3Int(
                                                Coprocessor.accumulator.ir1,
                                                Coprocessor.accumulator.ir2,
                                                Coprocessor.accumulator.ir3);
                                            uVar8 = (uint)lVar2;
                                        }
                                    }
                                    else
                                    {
                                        iVar4 = Coprocessor.accumulator.ir2;

                                        if (iVar4 < 1)
                                        {
                                            if (iVar7 < 0)
                                                goto LAB_2F54C;
                                        }
                                        else
                                        {
                                            iVar4 = Coprocessor.accumulator.ir2;
                                            lVar2 = Utilities.Divdi3(iVar5, iVar7, iVar4, iVar4 >> 31);

                                            if ((int)lVar2 < local_60.y)
                                                goto LAB_2F54C;

                                            if ((int)lVar2 < iVar12)
                                                iVar12 = (int)lVar2;
                                        }
                                    }

                                    iVar11 += 12;
                                    iVar9++;
                                    puVar3 = psVar10 + iVar11;
                                } while (iVar9 < brCollider.ReadUInt16(psVar10 + 2));
                            }

                            if ((int)uVar8 < iVar12)
                            {
                                iVar12 = (int)uVar8 + param1.position.y;

                                if (iVar12 < param2 && param3.y - 0x2800 < iVar12 && 
                                    iVar12 < param3.y + 0x5000 && local_40.y < -2048)
                                {
                                    param4 = local_40;
                                    return iVar12;
                                }
                            }

                            LAB_2F54C:
                            psVar10 += brCollider.ReadUInt16(psVar10 + 2) * 12 + 4;
                        }
                    }

                    LAB_2F568:
                    sVar1 = brCollider.ReadInt16(psVar10);
                }
            }
        }

        return 0;
    }

    private int FUN_2F16C(VigTransform param1, int param2, Vector3Int param3)
    {
        short sVar1;
        long lVar2;
        int puVar3;
        int iVar4;
        int iVar5;
        uint uVar6;
        int iVar7;
        uint uVar8;
        int iVar9;
        int psVar9;
        int psVar10;
        int iVar11;
        int iVar12;
        uint uVar13;
        uint uVar14;
        Vector3Int local_60;
        Vector3Int local_50;
        Vector3Int local_40;
        uint local_30;
        int local_2c;

        if (vCollider != null)
        {
            MemoryStream msCollider = new MemoryStream(vCollider);

            using (BinaryReader brCollider = new BinaryReader(msCollider, Encoding.Default, true))
            {
                local_60 = new Vector3Int(
                    param3.x - param1.position.x,
                    param3.y - param1.position.y,
                    param3.z - param1.position.z);
                sVar1 = brCollider.ReadInt16(0);
                psVar10 = 0;

                while (sVar1 != 0)
                {
                    if (brCollider.ReadUInt16(psVar10) == 1)
                    {
                        local_50 = Utilities.FUN_2426C(param1.rotation,
                            new Matrix2x4(local_60.x, local_60.y, local_60.z, 0));
                        psVar9 = psVar10 + 4;

                        if (local_50.x < brCollider.ReadInt32(psVar9 + 12) && brCollider.ReadInt32(psVar10 + 4) < local_50.x &&
                            local_50.z < brCollider.ReadInt32(psVar9 + 20) && brCollider.ReadInt32(psVar9 + 8) < local_50.z &&
                            local_50.y < brCollider.ReadInt32(psVar9 + 16))
                        {
                            iVar12 = brCollider.ReadInt32(psVar9 + 4);

                            if (local_50.y < iVar12 + 0x2800 && iVar12 - 0x5000 < local_50.y)
                            {
                                if (param1.position.y + iVar12 < param2)
                                    goto LAB_2F2F0;

                                psVar10 += 28;

                                if (!(param2 + 0x10000 < param3.y))
                                    goto LAB_2F568;

                                LAB_2F2F0:
                                local_50.y = brCollider.ReadInt32(psVar9 + 4);
                                local_50 = Utilities.FUN_24094(param1.rotation, local_50);
                                return param1.position.y + local_50.y;
                            }
                        }

                        psVar10 += 28;
                    }
                    else
                    {
                        iVar12 = 0x7fff0000;

                        if (brCollider.ReadUInt16(psVar10) == 2)
                        {
                            uVar8 = 0x80010000;
                            Utilities.SetRotMatrix(param1.rotation);
                            local_40 = new Vector3Int(); //not in original code
                            iVar9 = 0;

                            if (brCollider.ReadUInt16(psVar10 + 2) != 0)
                            {
                                iVar11 = 4;
                                puVar3 = psVar10 + iVar11;

                                do
                                {
                                    Coprocessor.vector0.vx0 = brCollider.ReadInt16(puVar3);
                                    Coprocessor.vector0.vy0 = brCollider.ReadInt16(puVar3 + 2);
                                    Coprocessor.vector0.vz0 = brCollider.ReadInt16(puVar3 + 4);
                                    Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.V0, _MVMVA_TRANSLATION_VECTOR.None, 12, false);
                                    uVar13 = (uint)((long)brCollider.ReadInt32(puVar3 + 8) * 4096);
                                    iVar4 = Coprocessor.accumulator.ir1;
                                    uVar14 = (uint)((long)iVar4 * local_60.x);
                                    uVar6 = uVar13 - uVar14;
                                    iVar5 = Coprocessor.accumulator.ir3;
                                    local_30 = (uint)((long)iVar5 * local_60.z);
                                    local_2c = (int)((ulong)((long)iVar5 * local_60.z) >> 32);
                                    iVar5 = (int)(uVar6 - local_30);
                                    iVar7 = ((((int)((ulong)((long)brCollider.ReadInt32(puVar3 + 8) * 4096) >> 32) -
                                               (int)((ulong)((long)iVar4 * local_60.x) >> 32)) -
                                               (int)(uVar13 < uVar14 ? 1 : 0)) - local_2c) - (int)(uVar6 < local_30 ? 1 : 0);
                                    iVar4 = Coprocessor.accumulator.ir2;

                                    if (iVar4 < 0)
                                    {
                                        iVar4 = Coprocessor.accumulator.ir2;
                                        lVar2 = Utilities.Divdi3(iVar5, iVar7, iVar4, iVar4 >> 31);

                                        if ((int)uVar8 < (int)lVar2)
                                        {
                                            local_40 = new Vector3Int(
                                                Coprocessor.accumulator.ir1,
                                                Coprocessor.accumulator.ir2,
                                                Coprocessor.accumulator.ir3);
                                            uVar8 = (uint)lVar2;
                                        }
                                    }
                                    else
                                    {
                                        iVar4 = Coprocessor.accumulator.ir2;

                                        if (iVar4 < 1)
                                        {
                                            if (iVar7 < 0)
                                                goto LAB_2F54C;
                                        }
                                        else
                                        {
                                            iVar4 = Coprocessor.accumulator.ir2;
                                            lVar2 = Utilities.Divdi3(iVar5, iVar7, iVar4, iVar4 >> 31);

                                            if ((int)lVar2 < local_60.y)
                                                goto LAB_2F54C;

                                            if ((int)lVar2 < iVar12)
                                                iVar12 = (int)lVar2;
                                        }
                                    }

                                    iVar11 += 12;
                                    iVar9++;
                                    puVar3 = psVar10 + iVar11;
                                } while (iVar9 < brCollider.ReadUInt16(psVar10 + 2));
                            }

                            if ((int)uVar8 < iVar12)
                            {
                                iVar12 = (int)uVar8 + param1.position.y;

                                if (iVar12 < param2 && param3.y - 0x2800 < iVar12 &&
                                    iVar12 < param3.y + 0x5000 && local_40.y < -2048)
                                {
                                    return iVar12;
                                }
                            }

                            LAB_2F54C:
                            psVar10 += brCollider.ReadUInt16(psVar10 + 2) * 12 + 4;
                        }
                    }

                    LAB_2F568:
                    sVar1 = brCollider.ReadInt16(psVar10);
                }
            }
        }

        return 0;
    }

    private int FUN_2F5AC(VigTransform param1, int param2, Vector3Int param3, out Vector3Int param4)
    {
        int iVar1;
        VigObject oVar2;
        VigTransform MStack64;

        oVar2 = child2;
        param4 = new Vector3Int(); //not in the original code

        do
        {
            if (oVar2 == null)
                return 0;

            if (oVar2.vCollider == null)
            {
                if ((oVar2.flags & 0x800) != 0)
                {
                    MStack64 = Utilities.CompMatrixLV(param1, oVar2.vTransform);
                    iVar1 = oVar2.FUN_2F5AC(MStack64, param2, param3, out param4);

                    if (iVar1 != 0)
                        return iVar1;
                }
            }
            else
            {
                MStack64 = Utilities.CompMatrixLV(param1, oVar2.vTransform);

                if (0 < MStack64.rotation.V11 ||
                    2048 < MStack64.rotation.V01 * vTransform.rotation.V01 +
                           MStack64.rotation.V11 * vTransform.rotation.V11 +
                           MStack64.rotation.V21 * vTransform.rotation.V21)
                {
                    iVar1 = oVar2.FUN_2F16C(MStack64, param2, param3, out param4);

                    if (iVar1 != 0)
                        return iVar1;
                }

                if ((oVar2.flags & 0x800) != 0)
                {
                    iVar1 = oVar2.FUN_2F5AC(MStack64, param2, param3, out param4);

                    if (iVar1 != 0)
                        return iVar1;
                }
            }

            oVar2 = oVar2.child;
        } while (true);
    }

    private int FUN_2F710(int param1, Vector3Int param2, out Vector3Int param3)
    {
        int iVar1;

        if ((flags & 0x800) != 0)
        {
            iVar1 = FUN_2F5AC(vTransform, param1, param2, out param3);

            if (iVar1 != 0)
                return iVar1;
        }

        return FUN_2F16C(vTransform, param1, param2, out param3);
    }

    private void FUN_305FC()
    {
        FUN_2D1DC();

        if ((flags & 4) != 0)
            GameManager.instance.FUN_30080(GameManager.instance.DAT_10A8, this);

        if ((flags & 0x80) != 0)
            GameManager.instance.FUN_30080(GameManager.instance.DAT_1088, this);

        GameManager.instance.FUN_30080(GameManager.instance.worldObjs, this);
    }

    private VigShadow FUN_4C44C(VigMesh param1, int param2, int param3)
    {
        VigShadow puVar1;

        puVar1 = gameObject.AddComponent<VigShadow>();
        puVar1.vMesh = param1;

        if (param2 < 0)
            param2 += 15;

        puVar1.DAT_24 = param2 >> 4;

        if (param3 < 0)
            param3 += 15;

        puVar1.DAT_28 = param3 >> 4;
        return puVar1;
    }

    private void FUN_4C7E0(VigMesh param1)
    {
        short sVar1;
        int iVar2;
        VigShadow sVar3;
        MemoryStream msVar4;
        int iVar5;
        bool bVar6;
        int local_res0;
        int local_res4;
        int local_10;
        int local_c;
        int local_8;
        int local_4;

        bVar6 = true;

        if (vCollider == null)
        {
            local_4 = DAT_58;
            local_res4 = local_4;
        }
        else
        {
            msVar4 = new MemoryStream(vCollider);

            using (BinaryReader reader = new BinaryReader(msVar4, Encoding.Default, true))
            {
                sVar1 = reader.ReadInt16(0);
                local_10 = 0;  // not in original code
                local_c = 0;   // -||-
                local_8 = 0;   // -||-
                local_4 = 0;   // -||-
                local_res0 = 0; // -||-
                local_res4 = 0; // -||-

                joined_r0x8004c820:
                if (sVar1 != 0)
                {
                    if (sVar1 != 1) goto code_r0x8004c83c;

                    if (bVar6)
                    {
                        local_10 = reader.ReadInt16(4);
                        local_c = reader.ReadInt16(8);
                        local_8 = reader.ReadInt16(12);
                        local_4 = reader.ReadInt16(16);
                        local_res0 = reader.ReadInt16(20);
                        local_res4 = reader.ReadInt16(24);
                        bVar6 = false;
                        iVar5 = 14;
                        reader.BaseStream.Seek(iVar5, SeekOrigin.Current);
                        sVar1 = reader.ReadInt16(0);
                        goto joined_r0x8004c820;
                    }
                    else
                    {
                        if (reader.ReadInt16(4) < local_10)
                            local_10 = reader.ReadInt16(2); //not used?

                        if (reader.ReadInt16(8) < local_c)
                            local_c = reader.ReadInt16(4); //not used?

                        if (reader.ReadInt16(12) < local_8)
                            local_8 = reader.ReadInt16(6); //not used?

                        if (local_4 < reader.ReadInt16(16))
                            local_4 = reader.ReadInt16(16);

                        if (local_res0 < reader.ReadInt16(20))
                            local_res0 = reader.ReadInt16(20);

                        if (reader.ReadInt16(24) < local_res4)
                            local_res4 = reader.ReadInt16(24);

                        iVar5 = 14;
                        reader.BaseStream.Seek(iVar5, SeekOrigin.Current);
                        sVar1 = reader.ReadInt16(0);
                        goto joined_r0x8004c820;
                    }
                }

                sVar3 = FUN_4C44C(param1, local_4, local_res4);
                vShadow = sVar3;
                return;

                code_r0x8004c83c:
                if (sVar1 == 2)
                {
                    iVar5 = reader.ReadUInt16(2) * 6 + 2;
                    reader.BaseStream.Seek(iVar5, SeekOrigin.Current);
                    sVar1 = reader.ReadInt16(0);
                }

                goto joined_r0x8004c820;
            }
        }
    }
}
