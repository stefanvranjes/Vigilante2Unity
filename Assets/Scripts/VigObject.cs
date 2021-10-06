using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using MathExtended.Matrices;

[Serializable]
public struct Matrix3x3
{
    public short V00, V01, V02;
    public short V10, V11, V12;
    public short V20, V21, V22;

    public Vector3 Scale
    {
        get
        {
            float fV00, fV01, fV02;

            const int SHRT_MAX = 4096;
            fV00 = (float)V00 / SHRT_MAX;
            fV01 = (float)V01 / SHRT_MAX;
            fV02 = (float)V02 / SHRT_MAX;

            //float sx = new Vector3(fV00, fV01, fV02).magnitude;

            float fV10, fV11, fV12;

            fV10 = (float)V10 / SHRT_MAX;
            fV11 = (float)V11 / SHRT_MAX;
            fV12 = (float)V12 / SHRT_MAX;

            //float sy = new Vector3(fV10, fV11, fV12).magnitude;

            float fV20, fV21, fV22;

            fV20 = (float)V20 / SHRT_MAX;
            fV21 = (float)V21 / SHRT_MAX;
            fV22 = (float)V22 / SHRT_MAX;

            float sx = new Vector3(fV00, fV10, fV20).magnitude;
            float sy = new Vector3(fV01, fV11, fV21).magnitude;
            float sz = new Vector3(fV02, fV12, fV22).magnitude;

            return new Vector3(sx, sy, sz);
        }
    }

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

    public void SetValue16(int index, int value)
    {
        switch (index)
        {
            case 0:
                V00 = (short)value;
                break;
            case 1:
                V01 = (short)value;
                break;
            case 2:
                V02 = (short)value;
                break;
            case 3:
                V10 = (short)value;
                break;
            case 4:
                V11 = (short)value;
                break;
            case 5:
                V12 = (short)value;
                break;
            case 6:
                V20 = (short)value;
                break;
            case 7:
                V21 = (short)value;
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
    public sbyte tags; //0x09
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
    public ushort DAT_4A; //0x4A

    public int DAT_58; //0x58
    public XOBF_DB vData; //0x5C
    public VigCollider vCollider; //0x60
    public BufferedBinaryReader vAnim; //0x64
    public VigMesh vLOD; //0x68
    public int DAT_6C; //0x6C
    public VigShadow vShadow; //0x70
    public VigObject PDAT_74; //0x74
    public ConfigContainer CCDAT_74; //0x74
    public VigTuple TDAT_74; //0x74
    public int IDAT_74; //0x74
    public VigObject PDAT_78; //0x78
    public VigTuple TDAT_78; //0x78
    public int IDAT_78; //0x78
    public VigObject PDAT_7C; //0x7C
    public int IDAT_7C; //0x7C
    public VigObject DAT_80; //0x80
    public VigObject DAT_84; //0x84

    public Matrix2x4 physics1;
    public Matrix2x4 physics2;

    public Vector3Int DAT_A0; //0xA0
    public short DAT_A6; //0xA6

    // Start is called before the first frame update
    protected virtual void Start()
    {
        /*vTransform.position = new Vector3Int(
            (int)(transform.localPosition.x * GameManager.translateFactor), 
            (int)(-transform.localPosition.y * GameManager.translateFactor), 
            (int)(transform.localPosition.z * GameManager.translateFactor));

        ApplyRotationMatrix();*/
        //screen = position;


    }

    // Update is called once per frame
    protected virtual void Update()
    {
        transform.localPosition = new Vector3(
            (float)vTransform.position.x / GameManager.instance.translateFactor, 
            (float)-vTransform.position.y / GameManager.instance.translateFactor, 
            (float)vTransform.position.z / GameManager.instance.translateFactor);

        transform.localRotation = vTransform.rotation.Matrix2Quaternion;
        transform.localEulerAngles = new Vector3(-transform.localEulerAngles.x, transform.localEulerAngles.y, -transform.localEulerAngles.z);
        transform.localScale = vTransform.rotation.Scale;
    }

    public virtual uint UpdateW(int arg1, int arg2)
    {
        return 0;
    }

    public virtual uint UpdateW(int arg1, VigObject arg2)
    {
        return 0;
    }

    public virtual uint UpdateW(VigObject arg1, int arg2, int arg3)
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

    public void FUN_2AF20()
    {
        int iVar1;
        int iVar2;
        int iVar3;
        Matrix3x3 m;

        iVar1 = physics2.X;

        if (iVar1 < 0)
            iVar1 += 127;

        iVar2 = physics2.Y;

        if (iVar2 < 0)
            iVar2 += 127;

        iVar3 = physics2.Z;

        if (iVar3 < 0)
            iVar3 += 127;

        FUN_24700((short)(iVar1 >> 7), (short)(iVar2 >> 7), (short)(iVar3 >> 7));
        iVar1 = physics1.X;

        if (iVar1 < 0)
            iVar1 += 127;

        iVar2 = physics1.Y;
        vTransform.position.x += iVar1 >> 7;

        if (iVar2 < 0)
            iVar2 += 127;

        iVar1 = physics1.Z;
        vTransform.position.y += iVar2 >> 7;

        if (iVar1 < 0)
            iVar1 += 127;

        vTransform.position.z += iVar1 >> 7;
        vTransform.rotation = Utilities.MatrixNormal(vTransform.rotation);
    }

    public void FUN_2AFF8(Vector3Int v1, Vector3Int v2)
    {
        physics1.X = physics1.X + v1.x;
        physics1.Y = physics1.Y + v1.y;
        physics1.Z = physics1.Z + v1.z;

        int iVar1 = v2.x * DAT_A0.x; //r3

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
        iVar1 = v2.y * DAT_A0.y;

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
        iVar1 = v2.z * DAT_A0.z;

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
        iVar1 = iVar1 * DAT_A0.x;

        if (iVar1 < 0)
            iVar1 += 63;

        physics2.X += iVar1 >> 6;
        iVar1 = Coprocessor.mathsAccumulator.mac2;
        iVar1 = iVar1 * DAT_A0.y;

        if (iVar1 < 0)
            iVar1 += 63;

        physics2.Y += iVar1 >> 6;
        iVar1 = Coprocessor.mathsAccumulator.mac3;
        iVar1 = iVar1 * DAT_A0.z;

        if (iVar1 < 0)
            iVar1 += 63;

        physics2.Z += iVar1 >> 6;
    }

    public void FUN_2B370(Vector3Int v1, Vector3Int v2)
    {
        int iVar2;
        Vector3Int local_res0;

        int cop2r32 = v2.x - vTransform.position.x >> 3;
        int cop2r34 = v2.y - vTransform.position.y >> 3;
        int cop2r36 = v2.z - vTransform.position.z >> 3;
        Coprocessor.rotationMatrix.rt11 = (short)cop2r32;
        Coprocessor.rotationMatrix.rt12 = (short)(cop2r32 >> 16);
        Coprocessor.rotationMatrix.rt22 = (short)cop2r34;
        Coprocessor.rotationMatrix.rt23 = (short)(cop2r34 >> 16);
        Coprocessor.rotationMatrix.rt33 = (short)cop2r36;
        Coprocessor.accumulator.ir1 = (short)(v1.x >> 3);
        Coprocessor.accumulator.ir2 = (short)(v1.y >> 3);
        Coprocessor.accumulator.ir3 = (short)(v1.z >> 3);
        Coprocessor.ExecuteOP(12, false);
        physics1.X += v1.x;
        physics1.Y += v1.y;
        physics1.Z += v1.z;
        local_res0 = new Vector3Int(
            Coprocessor.mathsAccumulator.mac1,
            Coprocessor.mathsAccumulator.mac2,
            Coprocessor.mathsAccumulator.mac3);
        local_res0 = Utilities.FUN_24238(vTransform.rotation, local_res0);
        iVar2 = local_res0.x * DAT_A0.x;

        if (iVar2 < 0)
            iVar2 += 127;

        physics2.X += iVar2 >> 7;
        iVar2 = local_res0.y * DAT_A0.y;

        if (iVar2 < 0)
            iVar2 += 127;

        physics2.Y += iVar2 >> 7;
        iVar2 = local_res0.z * DAT_A0.z;

        if (iVar2 < 0)
            iVar2 += 127;

        physics2.Z += iVar2 >> 7;
    }

    public void FUN_2C01C()
    {
        int iVar1;
        int iVar2;
        BufferedBinaryReader brVar3;

        brVar3 = new BufferedBinaryReader(vData.animations);

        if (brVar3.GetBuffer() != null)
        {
            iVar2 = brVar3.ReadInt32((ushort)DAT_1A * 4 + 4);

            if (iVar2 != 0)
                brVar3.Seek(iVar2, SeekOrigin.Begin);

            vAnim = brVar3;
        }
        else
            vAnim = null;

        DAT_4A = 0;
    }

    public void FUN_2C05C()
    {
        ushort uVar1;
        int iVar2;
        BufferedBinaryReader brVar3;

        uVar1 = GameManager.instance.timer;
        brVar3 = new BufferedBinaryReader(vData.animations);

        if (brVar3.GetBuffer() != null)
        {
            iVar2 = brVar3.ReadInt32((ushort)DAT_1A * 4 + 4);

            if (iVar2 != 0)
                brVar3.Seek(iVar2, SeekOrigin.Begin);

            vAnim = brVar3;
        }
        else
            vAnim = null;

        DAT_4A = uVar1;
    }

    public void FUN_2C124(ushort param1)
    {
        GameManager.instance.FUN_1FEB8(vMesh);
        GameManager.instance.FUN_307CC(child2);
        child2 = null;
        FUN_2C344(vData, param1, 8);
    }

    public void FUN_2C124_2(ushort param1)
    {
        GameManager.instance.FUN_1FEB8(vMesh);
        GameManager.instance.FUN_307CC(child2);
        child2 = null;
        FUN_2C344_2(vData, param1, 8);
    }

    public VigObject FUN_2C344(XOBF_DB param1, ushort param2, uint param3)
    {
        VigMesh mVar1;
        int iVar2;
        VigObject oVar3;
        BufferedBinaryReader brVar4;
        ConfigContainer puVar5;

        puVar5 = param1.ini.configContainers[param2];

        if ((puVar5.flag & 0x7ff) == 0x7ff)
            vMesh = null;
        else
        {
            mVar1 = param1.FUN_1FD18(gameObject, puVar5.flag, true);
            vMesh = mVar1;
        }

        if (puVar5.colliderID < 0)
            vCollider = null;
        else
        {
            VigCollider collider = param1.cbbList[puVar5.colliderID];
            vCollider = new VigCollider(collider.buffer);
        }

        vData = param1;
        DAT_1A = (short)param2;

        if ((param3 & 8) == 0)
            vAnim = null;
        else
        {
            brVar4 = vAnim;

            if (brVar4 == null)
            {
                brVar4 = new BufferedBinaryReader(param1.animations);

                if (brVar4.GetBuffer() != null)
                {
                    iVar2 = brVar4.ReadInt32(param2 * 4 + 4);

                    if (iVar2 != 0)
                        brVar4.Seek(iVar2, SeekOrigin.Begin);
                    else
                        brVar4 = null;
                }
                else
                    brVar4 = null;
            }
            else
            {
                brVar4.Seek(0, SeekOrigin.Begin);
                iVar2 = brVar4.ReadInt32(param2 * 4 + 4);

                if (iVar2 != 0)
                    brVar4.Seek(iVar2, SeekOrigin.Begin);
                else
                    brVar4 = null;
            }

            vAnim = brVar4;
        }

        DAT_4A = GameManager.instance.timer;

        if ((param3 & 2) == 0 && puVar5.next != 0xffff)
        {
            oVar3 = param1.ini.FUN_2C17C(puVar5.next, typeof(VigObject), param3 | 0x21);
            child2 = oVar3;

            if (oVar3 != null)
            {
                oVar3.ApplyTransformation();
                child2.parent = this;
            }
        }
        else
            child2 = null;

        return this;
    }

    public VigObject FUN_2C344_2(XOBF_DB param1, ushort param2, uint param3)
    {
        VigMesh mVar1;
        int iVar2;
        VigObject oVar3;
        BufferedBinaryReader brVar4;
        ConfigContainer puVar5;

        puVar5 = param1.ini.configContainers[param2];

        if ((puVar5.flag & 0x7ff) == 0x7ff)
            vMesh = null;
        else
        {
            mVar1 = param1.FUN_1FD18(gameObject, puVar5.flag, true);
            vMesh = mVar1;
        }

        if (puVar5.colliderID < 0)
            vCollider = null;
        else
        {
            VigCollider collider = param1.cbbList[puVar5.colliderID];
            vCollider = new VigCollider(collider.buffer);
        }

        vData = param1;
        DAT_1A = (short)param2;

        if ((param3 & 8) == 0)
            vAnim = null;
        else
        {
            brVar4 = vAnim;

            if (brVar4 == null)
            {
                brVar4 = new BufferedBinaryReader(param1.animations);

                if (brVar4.GetBuffer() != null)
                {
                    iVar2 = brVar4.ReadInt32(param2 * 4 + 4);

                    if (iVar2 != 0)
                        brVar4.Seek(iVar2, SeekOrigin.Begin);
                    else
                        brVar4 = null;
                }
                else
                    brVar4 = null;
            }
            else
            {
                brVar4.Seek(0, SeekOrigin.Begin);
                iVar2 = brVar4.ReadInt32(param2 * 4 + 4);

                if (iVar2 != 0)
                    brVar4.Seek(iVar2, SeekOrigin.Begin);
                else
                    brVar4 = null;
            }

            vAnim = brVar4;
        }

        DAT_4A = GameManager.instance.timer;

        if ((param3 & 2) == 0 && puVar5.next != 0xffff)
        {
            oVar3 = param1.ini.FUN_2C17C_2(puVar5.next, typeof(Body), param3 | 0x21);
            child2 = oVar3;

            if (oVar3 != null)
            {
                oVar3.ApplyTransformation();
                child2.parent = this;
            }
        }
        else
            child2 = null;

        return this;
    }

    public int FUN_2CFBC(Vector3Int pos, ref Vector3Int normalVector3, out TileData normalTile)
    {
        int iVar1;
        int iVar3;

        GameManager gameManager = GameManager.instance;
        TileData tile = gameManager.terrain.GetTileByPosition((uint)pos.x, (uint)pos.z); //r21
        int vertexHeight = 0x2f0000; //r16

        if ((tile.flags & 4) == 0)
            vertexHeight = gameManager.terrain.FUN_1B750((uint)pos.x, (uint)pos.z);
        else
            vertexHeight = 0x2ff800;

        if (PDAT_74 != null)
        {
            iVar3 = PDAT_74.FUN_2F710(vertexHeight, pos, ref normalVector3);

            if (iVar3 != 0)
            {
                if (PDAT_78 != null)
                {
                    iVar1 = PDAT_78.FUN_2F710(iVar3, pos, ref normalVector3);

                    if (iVar1 != 0)
                        iVar3 = iVar1;
                }

                normalTile = null;
                return iVar3;
            }

            if (PDAT_78 != null)
            {
                iVar3 = PDAT_78.FUN_2F710(vertexHeight, pos, ref normalVector3);

                if (iVar3 != 0)
                {
                    normalTile = null;
                    return iVar3;
                }
            }
        }

        normalVector3 = gameManager.terrain.FUN_1B998((uint)pos.x, (uint)pos.z);
        normalVector3 = Utilities.VectorNormal(normalVector3);
        normalTile = tile;

        return vertexHeight;
    }

    public int FUN_2CFBC(Vector3Int pos, ref Vector3Int normalVector3)
    {
        int iVar1;
        int iVar3;

        GameManager gameManager = GameManager.instance;
        TileData tile = gameManager.terrain.GetTileByPosition((uint)pos.x, (uint)pos.z); //r21
        int vertexHeight = 0x2f0000; //r16

        if ((tile.flags & 4) == 0)
            vertexHeight = gameManager.terrain.FUN_1B750((uint)pos.x, (uint)pos.z);
        else
            vertexHeight = 0x2ff800;

        if (PDAT_74 != null)
        {
            iVar3 = PDAT_74.FUN_2F710(vertexHeight, pos, ref normalVector3);

            if (iVar3 != 0)
            {
                if (PDAT_78 != null)
                {
                    iVar1 = PDAT_78.FUN_2F710(iVar3, pos, ref normalVector3);

                    if (iVar1 != 0)
                        iVar3 = iVar1;
                }

                return iVar3;
            }

            if (PDAT_78 != null)
            {
                iVar3 = PDAT_78.FUN_2F710(vertexHeight, pos, ref normalVector3);

                if (iVar3 != 0)
                {
                    return iVar3;
                }
            }
        }

        normalVector3 = gameManager.terrain.FUN_1B998((uint)pos.x, (uint)pos.z);
        normalVector3 = Utilities.VectorNormal(normalVector3);

        return vertexHeight;
    }

    public int FUN_2CFBC(Vector3Int pos, out TileData normalTile)
    {
        GameManager gameManager = GameManager.instance;
        TileData tile = gameManager.terrain.GetTileByPosition((uint)pos.x, (uint)pos.z); //r21
        int vertexHeight = 0x2f0000; //r16

        if ((tile.flags & 4) == 0)
            vertexHeight = gameManager.terrain.FUN_1B750((uint)pos.x, (uint)pos.z);
        else
            vertexHeight = 0x2ff800;

        if (PDAT_74 != null)
        {
            //...
        }

        normalTile = tile;

        return vertexHeight;
    }

    public int FUN_2CFBC(Vector3Int pos)
    {
        GameManager gameManager = GameManager.instance;
        TileData tile = gameManager.terrain.GetTileByPosition((uint)pos.x, (uint)pos.z); //r21
        int vertexHeight = 0x2f0000; //r16

        if ((tile.flags & 4) == 0)
            vertexHeight = gameManager.terrain.FUN_1B750((uint)pos.x, (uint)pos.z);
        else
            vertexHeight = 0x2ff800;

        return vertexHeight;
    }

    public VigObject FUN_2CCBC()
    {
        VigObject oVar1;
        VigObject oVar2;

        oVar2 = parent;

        if (oVar2 != null)
        {
            oVar1 = child;

            if (oVar2.child2 == this)
                oVar2.child2 = oVar1;
            else
                oVar2.child = oVar1;

            if (oVar1 != null)
                oVar1.parent = oVar2;

            child = null;
            parent = null;
        }

        return this;
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

    public ConfigContainer FUN_2C5F4(ushort param2)
    {
        VigConfig cVar1;

        cVar1 = vData.ini;
        return cVar1.FUN_2C534(cVar1.configContainers[DAT_1A].next, param2);
    }

    public void FUN_2C958()
    {
        VigObject oVar1;

        FUN_2C7D0();
        oVar1 = child2;

        while (oVar1 != null)
        {
            oVar1.FUN_2C958();
            oVar1 = oVar1.child;
        }
    }

    public void FUN_2D114(Vector3Int param1, ref VigTransform param2)
    {
        int iVar1;
        Vector3Int auStack8;

        auStack8 = new Vector3Int();
        iVar1 = FUN_2CFBC(param1, ref auStack8);
        param2.position.y = iVar1;
        param2.position.x = param1.x;
        param2.position.z = param1.z;
        param2.rotation = Utilities.FUN_2A5EC(auStack8);
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
            iVar2 = Utilities.FUN_29E84(oVar4.vTransform.position);
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
                param1.rotation = GameManager.defaultTransform.rotation;
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

    public virtual uint OnCollision(HitDetection hit)
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

    public int FUN_2FBC8(ushort param1)
    {
        ushort uVar1;
        short sVar2;
        ushort uVar3;
        bool bVar4;
        int iVar5;
        VigObject ppcVar5;
        int iVar6;
        Material mVar7;
        int iVar11;
        uint uVar12;
        int iVar13;
        VigMesh pcVar14;

        if (vAnim != null)
        {
            if ((uint)param1 - DAT_4A < vAnim.ReadUInt16(0))
                return 0;

            iVar13 = -1;
            iVar11 = 0; //not in the original code
            pcVar14 = vMesh;
            bVar4 = false;

            do
            {
                uVar12 = (uint)(int)vAnim.ReadInt16(2);
                iVar5 = 4;

                if ((int)uVar12 < 0)
                {
                    DAT_4A += vAnim.ReadUInt16(0);
                    vAnim.Seek((int)uVar12, SeekOrigin.Current);
                    ppcVar5 = this;

                    if (!GetType().IsSubclassOf(typeof(VigObject)))
                    {
                        do
                        {
                            if (ppcVar5.parent == null) break;

                            ppcVar5 = Utilities.FUN_2CD78(ppcVar5);
                        } while (!ppcVar5.GetType().IsSubclassOf(typeof(VigObject)));

                        iVar6 = 0;

                        if (ppcVar5.GetType().IsSubclassOf(typeof(VigObject)))
                            iVar6 = (int)ppcVar5.UpdateW(5, this);
                    }
                    else
                        iVar6 = (int)ppcVar5.UpdateW(5, this);

                    if (iVar6 < 0)
                        return iVar6;
                }
                else
                {
                    if ((uVar12 & 1) != 0)
                    {
                        sVar2 = vAnim.ReadInt16(8);
                        vr.x = vAnim.ReadInt16(4);
                        vr.y = vAnim.ReadInt16(6);
                        vr.z = sVar2;
                        iVar5 = 12;
                        bVar4 = true;
                    }

                    if ((uVar12 & 2) != 0)
                    {
                        screen.x = vAnim.ReadInt32(iVar5);
                        screen.y = vAnim.ReadInt32(iVar5 + 4);
                        screen.z = vAnim.ReadInt32(iVar5 + 8);
                        iVar5 += 12;
                        bVar4 = true;
                    }

                    if ((uVar12 & 8) != 0)
                    {
                        screen.x += vAnim.ReadInt16(iVar5);
                        bVar4 = true;
                        screen.y += vAnim.ReadInt16(iVar5 + 2);
                        iVar11 = iVar5 + 4;
                        iVar5 += 8;
                        screen.z += vAnim.ReadInt16(iVar11);
                    }

                    if ((uVar12 & 0x10) != 0)
                    {
                        do
                        {
                            uVar3 = vAnim.ReadUInt16(iVar5);
                            uVar1 = vAnim.ReadUInt16(iVar5 + 2);
                            iVar5 += 4;
                            //mVar7 = vData.FUN_1F288(uVar1);
                            if (vMesh == null)
                                Debug.Log(id);
                            vMesh.DAT_1C[uVar3 & 0x7fff] = uVar1;
                        } while (-1 < uVar3 << 16);
                    }

                    iVar11 = iVar5;

                    if ((uVar12 & 0x20) != 0)
                    {
                        iVar11 = iVar5 + 8;
                        bVar4 = true;
                        iVar13 = iVar5;
                    }

                    if ((uVar12 & 0x40) != 0)
                    {
                        vMesh.SetVertices(vAnim.GetBuffer(), (int)vAnim.Position + iVar11 + 4);
                        iVar11 += vAnim.ReadInt32(iVar11) * 8 + 4;
                    }

                    vAnim.Seek(iVar11, SeekOrigin.Current);
                }
            } while (vAnim.ReadUInt16(0) <= param1 - DAT_4A);

            if (!bVar4)
                return 0;

            ApplyTransformation();

            if (iVar13 != -1)
            {
                iVar13 -= iVar11; //not in the original code
                vTransform.rotation = Utilities.FUN_245AC(vTransform.rotation, 
                    new Vector3Int(vAnim.ReadInt16(iVar13), vAnim.ReadInt16(iVar13 + 2), vAnim.ReadInt16(iVar13 + 4)));
                vTransform.padding = vAnim.ReadInt16(iVar13);
                //transform.localScale = vTransform.rotation.Scale;
            }
        }

        return 0;
    }

    public bool FUN_3066C()
    {
        int iVar1;
        bool bVar2;

        ApplyTransformation();

        if (!GetType().IsSubclassOf(typeof(VigObject)))
            iVar1 = 0;
        else
            iVar1 = (int)UpdateW(1, 0);

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

    public VigObject FUN_306FC()
    {
        if (GetType().IsSubclassOf(typeof(VigObject)))
            UpdateW(4, 0);

        if ((flags & 0x80) != 0)
            GameManager.instance.FUN_300B8(GameManager.instance.DAT_1088, this);

        if ((flags & 4) != 0)
            GameManager.instance.FUN_300B8(GameManager.instance.DAT_10A8, this);

        if ((flags & 1) != 0)
            GameManager.instance.FUN_300B8(GameManager.instance.DAT_1110, this);

        return this;
    }

    public void FUN_30B78()
    {
        flags |= 0x80;
        GameManager.instance.FUN_30080(GameManager.instance.DAT_1088, this);
    }

    public bool FUN_30BA8()
    {
        bool bVar1;

        if ((flags & 0x80) == 0)
            bVar1 = false;
        else
        {
            flags &= 0xffffff7f;
            bVar1 = GameManager.instance.FUN_300B8(GameManager.instance.DAT_1088, this);
        }

        return bVar1;
    }

    public void FUN_30BF0()
    {
        flags |= 4;
        GameManager.instance.FUN_30080(GameManager.instance.DAT_10A8, this);
    }

    public bool FUN_30C20()
    {
        bool bVar1;

        if ((flags & 4) == 0)
            bVar1 = false;
        else
        {
            flags &= 0xfffffffb;
            bVar1 = GameManager.instance.FUN_300B8(GameManager.instance.DAT_10A8, this);
        }

        return bVar1;
    }

    public bool FUN_30C68()
    {
        bool bVar1;

        if ((flags & 1) == 0)
            bVar1 = false;
        else
        {
            flags &= 0xfffffffe;
            bVar1 = GameManager.instance.FUN_300B8(GameManager.instance.DAT_1110, this);
        }

        return bVar1;
    }

    public VigObject FUN_31DDC(_VEHICLE_INIT param1)
    {
        ushort uVar2;
        ushort uVar3;
        VigObject puVar4;
        VigObject oVar7;

        puVar4 = Utilities.FUN_31D30(param1, vData, (ushort)DAT_1A, (flags & 4) << 1);
        uVar2 = maxHalfHealth;
        uVar3 = maxFullHealth;
        puVar4.flags |= flags;
        puVar4.id = id;
        puVar4.tags = tags;
        puVar4.screen = screen;
        puVar4.vr = vr;
        puVar4.DAT_19 = DAT_19;

        if (uVar2 != 0 || uVar3 != 0)
        {
            oVar7 = puVar4.child2;
            puVar4.maxHalfHealth = uVar2;
            puVar4.maxFullHealth = uVar3;

            while (oVar7 != null)
            {
                oVar7.maxHalfHealth = uVar2;
                oVar7 = oVar7.child;
            }
        }

        puVar4.FUN_2D1DC();
        puVar4.FUN_2C958();
        return puVar4;
    }

    public VigObject FUN_31DDC()
    {
        ushort uVar2;
        ushort uVar3;
        VigObject puVar4;
        VigObject oVar7;

        puVar4 = Utilities.FUN_31D30(GetType(), vData, DAT_1A, (flags & 4) << 1);
        uVar2 = maxHalfHealth;
        uVar3 = maxFullHealth;
        puVar4.flags |= flags;
        puVar4.id = id;
        puVar4.tags = tags;
        puVar4.screen = screen;
        puVar4.vr = vr;
        puVar4.DAT_19 = DAT_19;

        if (uVar2 != 0 || uVar3 != 0)
        {
            oVar7 = puVar4.child2;
            puVar4.maxHalfHealth = uVar2;
            puVar4.maxFullHealth = uVar3;

            while (oVar7 != null)
            {
                oVar7.maxHalfHealth = uVar2;
                oVar7 = oVar7.child;
            }
        }

        puVar4.FUN_2D1DC();
        puVar4.FUN_2C958();
        return puVar4;
    }

    public bool FUN_32B90(uint param1)
    {
        bool bVar1;
        int iVar1;
        Pickup pVar1;
        Vector3Int local_10;

        if ((flags & 0x8000) == 0)
        {
            if (maxHalfHealth < param1)
            {
                maxHalfHealth = maxFullHealth;

                if ((flags & 0x40000) == 0)
                {
                    bVar1 = FUN_4DC94();

                    if (bVar1)
                    {
                        LevelManager.instance.level.UpdateW(this, 18, 0);
                        return true;
                    }
                }
                else
                {
                    iVar1 = (int)GameManager.FUN_2AC5C();
                    local_10 = new Vector3Int();
                    local_10.x = (iVar1 * 3051 >> 15) - 1525;
                    local_10.y = -4577;
                    iVar1 = (int)GameManager.FUN_2AC5C();
                    local_10.z = (iVar1 * 3051 >> 15) - 1525;
                    pVar1 = LevelManager.instance.FUN_4AA24((ushort)GameManager.DAT_63FA4[GameManager.instance.DAT_1004],
                                                          vTransform.position, local_10);
                    pVar1.flags |= 0x2040000;
                    flags &= 0xfffbffff;
                }
            }
            else
                maxHalfHealth -= (ushort)param1;
        }

        return false;
    }

    public void FUN_3A368()
    {
        VigObject oVar1;
        Vehicle vVar2;
        int iVar3;

        oVar1 = Utilities.FUN_2CD78(this);

        if (oVar1 != null)
        {
            iVar3 = 0;
            vVar2 = (Vehicle)oVar1;

            do
            {
                if (vVar2.weapons[iVar3] == this)
                {
                    vVar2.FUN_3A280((uint)iVar3);
                    return;
                }

                iVar3++;
            } while (iVar3 < 3);
        }
    }

    public void FUN_3AC4C()
    {
        int iVar1;

        iVar1 = GameManager.instance.FUN_1DD9C();
        GameManager.instance.FUN_1E188(iVar1, vData.sndList, 1);
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

        if (tile.DAT_10[3] == 7)
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
            } while (tile.DAT_10[3] == 7);

            vTransform.position.x = (int)((uVar3 & 0xffff0000) + 0x8000);
            vTransform.position.z = (int)((uVar4 & 0xffff0000) + 0x8000);
        }
    }

    private int FUN_4205C()
    {
        ushort uVar1;
        short sVar2;
        VigObject oVar3;
        int iVar3;
        Vehicle vVar4;
        int iVar5;
        Vector3Int v3Var6;
        VigObject ppcVar7;
        int iVar8;

        iVar3 = 0;

        if ((GameManager.instance.DAT_28 - DAT_19 & 3) == 0)
        {
            vVar4 = (Vehicle)Utilities.FUN_2CD78(this);
            iVar8 = 0;

            do
            {
                ppcVar7 = vVar4.weapons[iVar8];

                if (ppcVar7 != null && ppcVar7.tags == -tags)
                {
                    if (!ppcVar7.GetType().IsSubclassOf(typeof(VigObject)))
                        iVar5 = 0;
                    else
                        iVar5 = (int)ppcVar7.UpdateW(16, this);

                    if (iVar5 == 0)
                    {
                        uVar1 = vVar4.weapons[iVar8].maxHalfHealth;

                        if (uVar1 < 99)
                            vVar4.weapons[iVar8].maxHalfHealth++;
                    }
                }

                iVar8++;
            } while (iVar8 < 3);

            sVar2 = (short)(maxHalfHealth - 1);
            maxHalfHealth = (ushort)sVar2;

            if (sVar2 == 0)
            {
                iVar3 = GameManager.instance.FUN_1DD9C();
                v3Var6 = GameManager.instance.FUN_2CE50(this);
                GameManager.instance.FUN_1E628(iVar3, GameManager.instance.DAT_C2C, 45, v3Var6);
                oVar3 = FUN_2CCBC();
                GameManager.instance.FUN_307CC(oVar3);
                iVar3 = -1;
            }
            else
                iVar3 = 0;
        }

        return iVar3;
    }

    private int FUN_4219C(ConfigContainer param1)
    {
        int iVar1;
        int iVar2;
        int iVar3;
        int iVar4;
        int iVar5;
        int iVar6;

        iVar6 = param1.v3_1.x - screen.x;
        iVar1 = param1.v3_1.y - screen.y;
        iVar2 = param1.v3_1.z - screen.z;

        if (-1 < tags)
        {
            iVar3 = iVar6;

            if (iVar6 < 0)
                iVar3 = -iVar6;

            iVar5 = iVar1;

            if (iVar1 < 0)
                iVar5 = -iVar1;

            if (iVar5 < iVar3)
                iVar5 = iVar3;

            iVar3 = iVar2;

            if (iVar2 < 0)
                iVar3 = -iVar2;

            if (iVar3 < iVar5)
                iVar3 = iVar5;

            if (iVar3 < 0x801)
                return 1;
        }

        iVar3 = iVar6;

        if (iVar6 < 0)
            iVar3 = iVar6 + 31;

        iVar5 = iVar2;

        if (iVar2 < 0)
            iVar5 = iVar2 + 7;

        screen.x += (iVar3 >> 5) + (iVar5 >> 3);

        if (iVar1 < 0)
            iVar1 += 15;

        screen.y += iVar1 >> 4;

        if (iVar2 < 0)
            iVar2 += 31;

        if (iVar6 < 0)
            iVar6 += 7;

        screen.z += (iVar2 >> 5) - (iVar6 >> 3);
        vTransform.position = screen;
        iVar4 = 0;

        if (tags < 0)
            iVar4 = FUN_4205C();

        return iVar4;
    }

    public int FUN_42330(int param1)
    {
        int iVar1;
        ConfigContainer ccVar1;
        int iVar2;
        Vector3Int v3Var3;

        iVar1 = 1;

        if ((flags & 0x1000000) != 0)
        {
            iVar1 = FUN_4219C(CCDAT_74);

            if (param1 < 0 || 0 < iVar1)
            {
                iVar2 = GameManager.instance.FUN_1DD9C();
                v3Var3 = GameManager.instance.FUN_2CE50(this);
                GameManager.instance.FUN_1E628(iVar2, GameManager.instance.DAT_C2C, 49, v3Var3);
                ccVar1 = CCDAT_74;
                flags &= 0xfeffffff;
                screen = ccVar1.v3_1;
                vTransform.position = screen;
                FUN_30BA8();
                iVar1 = 0;
            }
        }

        return iVar1;
    }

    public int FUN_42330(VigObject param1)
    {
        int iVar1;
        ConfigContainer ccVar1;
        int iVar2;
        Vector3Int v3Var3;

        iVar1 = 1;

        if ((flags & 0x1000000) != 0)
        {
            iVar1 = FUN_4219C(CCDAT_74);

            if (param1 != null || 0 < iVar1)
            {
                iVar2 = GameManager.instance.FUN_1DD9C();
                v3Var3 = GameManager.instance.FUN_2CE50(this);
                GameManager.instance.FUN_1E628(iVar2, GameManager.instance.DAT_C2C, 49, v3Var3);
                ccVar1 = CCDAT_74;
                flags &= 0xfeffffff;
                screen = ccVar1.v3_1;
                vTransform.position = screen;
                FUN_30BA8();
                iVar1 = 0;
            }
        }

        return iVar1;
    }

    public uint FUN_42638(HitDetection param1, short param2, int param3)
    {
        int iVar1;
        int iVar2;
        VigObject oVar3;
        Vehicle vVar3;
        int iVar4;
        Vector3Int local_18;

        if (param1 != null)
        {
            if (param1.object2.type == 3)
                return 0;

            oVar3 = param1.self;
            iVar1 = param3 << 16;

            if (oVar3.type != 2) goto LAB_427B8;

            vVar3 = (Vehicle)oVar3;
            iVar4 = (maxHalfHealth << 11) / vVar3.DAT_A6;
            iVar1 = physics1.Z * iVar4;
            local_18 = new Vector3Int();

            if (iVar1 < -0x80000)
                local_18.x = -0x80000;
            else
            {
                local_18.x = 0x80000;

                if (iVar1 < 0x80001)
                    local_18.x = iVar1;
            }

            iVar1 = physics1.W * iVar4;
            local_18.y = -0x80000;

            if (-0x80001 < iVar1)
            {
                local_18.y = 0x80000;

                if (iVar1 < 0x80001)
                    local_18.y = iVar1;
            }

            iVar1 = physics2.X * iVar4;
            local_18.z = -0x80000;

            if (-0x80001 < iVar1)
            {
                local_18.z = 0x80000;

                if (iVar1 < 0x80001)
                    local_18.z = iVar1;
            }

            vVar3.FUN_2B370(local_18, vTransform.position);

            if (vVar3.id < 0)
                GameManager.instance.FUN_15B00(~vVar3.id, 255, 2, 128);

            iVar1 = param3 << 16;

            if (vVar3.shield == 0) goto LAB_427B8;

            param3 = -1;
        }

        iVar1 = param3 << 16;
        LAB_427B8:

        if (-1 < iVar1 >> 16)
        {
            iVar2 = GameManager.instance.FUN_1DD9C();
            GameManager.instance.FUN_1E628(iVar2, GameManager.instance.DAT_C2C, iVar1 >> 16, vTransform.position);
        }

        LevelManager.instance.FUN_4DE54(vTransform.position, (ushort)param2);
        LevelManager.instance.level.UpdateW(this, 18, 0);
        GameManager.instance.FUN_309A0(this);
        return 0xffffffff;
    }

    public ConfigContainer FUN_4AE5C(int param1)
    {
        uint uVar1;

        if (param1 == 7)
            uVar1 = 0x801f;
        else
            uVar1 = (uint)param1 - 0x7ff0U & 0xffff;

        return FUN_2C5F4((ushort)uVar1);
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
        local_60 = new Vector3Int(); //not in original code

        tVar1 = terrain.GetTileByPosition((uint)vTransform.position.x, (uint)vTransform.position.z);

        if ((tVar1.flags & 4) == 0)
            iVar2 = terrain.FUN_1B750((uint)vTransform.position.x, (uint)vTransform.position.z);
        else
            iVar2 = 0x2ff800;

        vShadow.vTransform.position.x = vTransform.position.x;
        vShadow.vTransform.position.z = vTransform.position.z;

        if (PDAT_74 != null)
        {
            iVar3 = PDAT_74.FUN_2F710(iVar2, vTransform.position, ref local_60);

            if (iVar3 != 0)
                goto LAB_4C5B8;

            if (PDAT_78 == null)
                goto LAB_4C5C0;

            iVar3 = PDAT_78.FUN_2F710(iVar2, vTransform.position, ref local_60);

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
            vShadow.eulerAngles = vTransform.rotation.Matrix2Quaternion.eulerAngles; //not in the original code
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

        GameObject obj = new GameObject();
        mVar1 = GameManager.instance.levelManager.xobfList[18].FUN_2CB74(obj, 92, true);
        FUN_4C7E0(mVar1, obj);
    }

    public void FUN_4C9C8()
    {
        VigMesh mVar1;

        GameObject obj = new GameObject();
        mVar1 = GameManager.instance.levelManager.xobfList[18].FUN_2CB74(obj, 93, true);
        mVar1.DAT_00 |= 8;
        FUN_4C7E0(mVar1, obj);
    }

    public void FUN_4D8A8(XOBF_DB param1, ushort param2, VigObject param3)
    {
        bool bVar1;
        ushort uVar2;
        VigMesh pcVar3;
        int iVar4;
        ConfigContainer puVar5;
        VigTransform auStack40;

        if ((flags & 4) != 0)
            FUN_30C20();

        GameManager.instance.FUN_1FEB8(vMesh);
        GameManager.instance.FUN_307CC(child2);

        if (vLOD != null)
        {
            if (vLOD != vMesh)
                GameManager.instance.FUN_1FEB8(vLOD);

            vLOD.ClearMeshData();
            MeshFilter meshFilter = GetComponent<MeshFilter>();
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

            if (meshFilter != null)
                DestroyImmediate(meshFilter, false);

            if (meshRenderer != null)
                DestroyImmediate(meshRenderer, false);

            vLOD = null;
            DAT_6C = 0;
        }

        puVar5 = param1.ini.configContainers[param2];
        auStack40 = Utilities.FUN_2C77C(puVar5);
        vTransform = Utilities.CompMatrixLV(vTransform, auStack40);
        screen = vTransform.position;

        if ((puVar5.flag & 0x7ff) == 0x7ff)
        {
            if (vMesh != null)
            {
                vMesh.ClearMeshData();
                MeshFilter meshFilter = GetComponent<MeshFilter>();
                MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

                if (meshFilter != null)
                    DestroyImmediate(meshFilter, false);

                if (meshRenderer != null)
                    DestroyImmediate(meshRenderer, false);
            }

            vMesh = null;
        }
        else
        {
            if (vMesh != null)
            {
                vMesh.ClearMeshData();

                MeshFilter meshFilter = GetComponent<MeshFilter>();
                MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

                if (meshFilter != null)
                    DestroyImmediate(meshFilter, false);

                if (meshRenderer != null)
                    DestroyImmediate(meshRenderer, false);
            }

            pcVar3 = param1.FUN_1FD18(gameObject, puVar5.flag & 0x7ffU, true);
            vMesh = pcVar3;
        }

        vAnim = null;
        DAT_1A = (short)param2;
        child2 = param3;

        if (param3 != null) {
            param3.parent = this;
            Utilities.ParentChildren(this, this);
        }

        if (puVar5.colliderID < 0)
            vCollider = null;
        else
            vCollider = new VigCollider(param1.cbbList[puVar5.colliderID].buffer);

        iVar4 = (int)FUN_2EC7C();

        if (iVar4 == 0)
            flags |= 0x20;

        uVar2 = GameManager.instance.timer;
        bVar1 = false;

        if (param3 != null)
        {
            do
            {
                if (param3.vAnim != null)
                {
                    param3.DAT_4A = uVar2;
                    bVar1 = true;
                }

                param3 = param3.child;
            } while (param3 != null);
        }

        if (bVar1)
            FUN_30BF0();

        flags &= 0xffff7fff;
        FUN_2D1DC();

        if (GetType().IsSubclassOf(typeof(VigObject)))
            UpdateW(9, 1);
    }

    public void FUN_4DB00(XOBF_DB param1, ushort param2)
    {
        VigObject pcVar2;
        VigObject ppcVar3;
        ConfigContainer ccVar4;
        VigTransform auStack48;

        if (GetType().IsSubclassOf(typeof(VigObject)))
            UpdateW(9, 0);

        ccVar4 = param1.ini.configContainers[param2];
        auStack48 = GameManager.instance.FUN_2CEAC(this, ccVar4);
        pcVar2 = param1.FUN_4D498(ccVar4.next, auStack48, id);

        if (ccVar4.objID == 0xaaaa || ccVar4.objID == 0)
            FUN_4D8A8(param1, param2, pcVar2);
        else
        {
            GameObject obj = new GameObject();
            ppcVar3 = obj.AddComponent<Delay>();
            ppcVar3.vData = param1;
            ppcVar3.DAT_1A = (short)param2;
            ppcVar3.PDAT_74 = this;
            ppcVar3.child2 = pcVar2;
            DAT_1A = (short)param2;
            flags |= 0x8000;
            GameManager.instance.FUN_30CB0(ppcVar3, ccVar4.objID);
        }
    }

    public uint FUN_4DC20()
    {
        ushort uVar1;
        uint uVar2;
        ConfigContainer ccVar3;
        VigConfig cVar4;

        cVar4 = vData.ini;
        uVar1 = cVar4.configContainers[DAT_1A].next;

        while (true)
        {
            uVar2 = uVar1;

            if (uVar2 == 0xffff)
                return 0;

            ccVar3 = cVar4.configContainers[(int)uVar2];

            if ((uint)ccVar3.flag >> 12 == 15) break;

            uVar1 = ccVar3.previous;
        }

        return uVar2;
    }

    public bool FUN_4DC94()
    {
        uint uVar1;

        uVar1 = FUN_4DC20();

        if (uVar1 != 0)
            FUN_4DB00(vData, (ushort)uVar1);

        return uVar1 != 0;
    }

    public int FUN_4DCD8()
    {
        int configIndex = (DAT_1A << 3) - DAT_1A << 2;
        int nextContainer = vData.ini.configContainers[configIndex / 0x1C].next;
        int iVar1 = 0;

        while (nextContainer != 0xffff)
        {
            configIndex = (nextContainer << 3) - nextContainer << 2;
            int flag = vData.ini.configContainers[configIndex / 0x1C].flag >> 12;

            if (flag == 15)
            {
                nextContainer = vData.ini.configContainers[configIndex / 0x1C].next;
                iVar1++;
            }
            else
            {
                nextContainer = vData.ini.configContainers[configIndex / 0x1C].previous;
            }
        }

        return iVar1;
    }

    public Throwaway FUN_4ECA0()
    {
        ushort uVar1;
        VigObject oVar2;
        VigTransform t0;
        Throwaway tVar3;
        int iVar4;
        int iVar5;

        oVar2 = Utilities.FUN_2CD78(this);
        t0 = GameManager.instance.FUN_2CDF4(oVar2);
        FUN_306FC();
        FUN_2CCBC();
        type = 8;
        PDAT_78 = null;
        IDAT_78 = 0;
        PDAT_74 = null;
        CCDAT_74 = null;
        IDAT_74 = 0;
        flags = flags & 0xffffbfff | 0x80;
        tVar3 = Utilities.FUN_52188(this, typeof(Throwaway)) as Throwaway;
        tVar3.state = _THROWAWAY_TYPE.Unspawnable;

        if (tVar3.child2 != null)
            tVar3.child2.parent = tVar3;

        iVar4 = tVar3.vTransform.position.x;

        if (iVar4 < 0)
            iVar4 += 7;

        iVar5 = tVar3.vTransform.position.y;
        tVar3.physics1.Z = iVar4 >> 3;

        if (iVar5 < 0)
            iVar5 += 7;

        iVar4 = tVar3.vTransform.position.z;
        tVar3.physics1.W = iVar5 >> 3;

        if (iVar4 < 0)
            iVar4 += 7;

        tVar3.physics2.X = iVar4 >> 3;
        tVar3.vTransform = Utilities.CompMatrixLV(t0, tVar3.vTransform);
        uVar1 = (ushort)GameManager.FUN_2AC5C();
        tVar3.physics1.M0 = (short)(uVar1 & 0xff);
        uVar1 = (ushort)GameManager.FUN_2AC5C();
        tVar3.physics1.M1 = (short)(uVar1 & 0xff);
        uVar1 = (ushort)GameManager.FUN_2AC5C();
        tVar3.physics1.M2 = (short)(uVar1 & 0xff);
        tVar3.screen = tVar3.vTransform.position;
        tVar3.DAT_87 = 2;
        Vector3Int v3 = Utilities.FUN_24094(t0.rotation, new Vector3Int(tVar3.physics1.Z, tVar3.physics1.W, tVar3.physics2.X));
        tVar3.physics1.Z = v3.x;
        tVar3.physics1.W = v3.y;
        tVar3.physics2.X = v3.z;
        tVar3.FUN_3066C();
        return tVar3;
    }

    public void FUN_4EDFC()
    {
        VigTuple tVar1;

        FUN_2D1DC();
        tVar1 = GameManager.instance.FUN_30080(GameManager.instance.interObjs, this);
        TDAT_74 = tVar1;
        tVar1 = GameManager.instance.FUN_30080(GameManager.instance.DAT_10A8, this);
        TDAT_78 = tVar1;
    }

    public void FUN_4EE8C(List<VigTuple> param1)
    {
        Tuple<List<VigTuple>, VigTuple> tuple;
        tuple = new Tuple<List<VigTuple>, VigTuple>(param1, TDAT_74);
        GameManager.instance.FUN_3094C(tuple);
    }

    public bool FUN_2C7D0()
    {
        ushort uVar1;
        VigMesh mVar2;
        int iVar3;
        int iVar4;
        List<ConfigContainer> ccVar5;

        if (vData != null)
        {
            ccVar5 = vData.ini.configContainers;
            uVar1 = ccVar5[(ushort)DAT_1A].next;

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
                        if (((ccVar5[iVar4].flag ^ ccVar5[(ushort)DAT_1A].flag) & 0x7ff) == 0)
                            vLOD = vMesh;
                        else
                        {
                            mVar2 = vData.FUN_1FD18(gameObject, ccVar5[iVar4].flag & 0x7ffU, true);
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
        short sVar1 = (short)vData.ini.configContainers[configIndex / 0x1C].next;
        ConfigContainer container;

        while (true)
        {
            if (sVar1 == -1)
                return null;

            configIndex = (sVar1 << 3) - sVar1 << 2;
            configIndex = configIndex / 0x1C;
            container = vData.ini.configContainers[configIndex];

            if ((uint)container.flag >> 12 == 11)
                break;

            sVar1 = (short)container.previous;
        }

        return container;
    }

    public VigObject FUN_2CD04()
    {
        FUN_306FC();
        vTransform = GameManager.instance.FUN_2CDF4(this);
        return FUN_2CCBC();
    }

    private int FUN_2F16C(VigTransform param1, int param2, Vector3Int param3, ref Vector3Int param4)
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
            MemoryStream msCollider = new MemoryStream(vCollider.buffer);

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
            MemoryStream msCollider = new MemoryStream(vCollider.buffer);

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

    private int FUN_2F5AC(VigTransform param1, int param2, Vector3Int param3, ref Vector3Int param4)
    {
        int iVar1;
        VigObject oVar2;
        VigTransform MStack64;

        oVar2 = child2;

        do
        {
            if (oVar2 == null)
                return 0;

            if (oVar2.vCollider == null)
            {
                if ((oVar2.flags & 0x800) != 0)
                {
                    MStack64 = Utilities.CompMatrixLV(param1, oVar2.vTransform);
                    iVar1 = oVar2.FUN_2F5AC(MStack64, param2, param3, ref param4);

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
                    iVar1 = oVar2.FUN_2F16C(MStack64, param2, param3, ref param4);

                    if (iVar1 != 0)
                        return iVar1;
                }

                if ((oVar2.flags & 0x800) != 0)
                {
                    iVar1 = oVar2.FUN_2F5AC(MStack64, param2, param3, ref param4);

                    if (iVar1 != 0)
                        return iVar1;
                }
            }

            oVar2 = oVar2.child;
        } while (true);
    }

    private int FUN_2F710(int param1, Vector3Int param2, ref Vector3Int param3)
    {
        int iVar1;

        if ((flags & 0x800) != 0)
        {
            iVar1 = FUN_2F5AC(vTransform, param1, param2, ref param3);

            if (iVar1 != 0)
                return iVar1;
        }

        return FUN_2F16C(vTransform, param1, param2, ref param3);
    }

    public void FUN_305FC()
    {
        FUN_2D1DC();

        if ((flags & 4) != 0)
            GameManager.instance.FUN_30080(GameManager.instance.DAT_10A8, this);

        if ((flags & 0x80) != 0)
            GameManager.instance.FUN_30080(GameManager.instance.DAT_1088, this);

        GameManager.instance.FUN_30080(GameManager.instance.worldObjs, this);
    }

    private void FUN_4C7E0(VigMesh param1, GameObject param2)
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
            sVar3 = Utilities.FUN_4C44C(param1, local_4, local_res4, param2);
            vShadow = sVar3;
        }
        else
        {
            msVar4 = new MemoryStream(vCollider.buffer);

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
                        local_10 = reader.ReadInt32(4);
                        local_c = reader.ReadInt32(8);
                        local_8 = reader.ReadInt32(12);
                        local_4 = reader.ReadInt32(16);
                        local_res0 = reader.ReadInt32(20);
                        local_res4 = reader.ReadInt32(24);
                        bVar6 = false;
                        iVar5 = 28;
                        reader.BaseStream.Seek(iVar5, SeekOrigin.Current);
                        sVar1 = reader.ReadInt16(0);
                        goto joined_r0x8004c820;
                    }
                    else
                    {
                        if (reader.ReadInt32(4) < local_10)
                            local_10 = reader.ReadInt32(4); //not used?

                        if (reader.ReadInt32(8) < local_c)
                            local_c = reader.ReadInt32(8); //not used?

                        if (reader.ReadInt32(12) < local_8)
                            local_8 = reader.ReadInt32(12); //not used?

                        if (local_4 < reader.ReadInt32(16))
                            local_4 = reader.ReadInt32(16);

                        if (local_res0 < reader.ReadInt32(20))
                            local_res0 = reader.ReadInt32(20);

                        if (local_res4 < reader.ReadInt32(24))
                            local_res4 = reader.ReadInt32(24);

                        iVar5 = 14;
                        reader.BaseStream.Seek(iVar5, SeekOrigin.Current);
                        sVar1 = reader.ReadInt16(0);
                        goto joined_r0x8004c820;
                    }
                }

                sVar3 = Utilities.FUN_4C44C(param1, local_4, local_res4, param2);
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
