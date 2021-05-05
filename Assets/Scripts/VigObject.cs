using System;
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
                float S = 0.5f / (float)Math.Sqrt(tr + 1f);
                qw = 0.25 / S;
                qx = (fV21 - fV12) * S;
                qy = (fV02 - fV20) * S;
                qz = (fV10 - fV01) * S;
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

public struct Matrix2x4
{
    public short M0, M1;
    public short M2, M3;
    public short M4, M5;
    public short M6, M7;

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
    public Vector3Int position;
}

public class VigObject : MonoBehaviour
{
    public uint flags;
    public byte type;
    public byte ai;
    public short id;

    public VigObject child; //0x0C
    public VigObject child2; //0x10
    public VigObject parent; //0x14

    public sbyte DAT_18; //0x18
    public short unk2; //0x1A

    public ushort maxHalfHealth;
    public ushort maxFullHealth;

    public VigTransform vTransform;

    public short padding1; //0x32

    public VigMesh vMesh; //0x40

    public Vector3Int screen; //0x4C
    public Vector3Int vr; //0x44
    public ushort unk4; //0x4A

    public int DAT_58; //0x58
    public VehicleConfig vConfig; //0x5C
    public VigCollider vCollider; //0x60
    public int unk3; //0x64
    public uint pointerUnk1; //0x74
    public int DAT_78; //0x78
    public uint pointerUnk3; //0x7C

    public Matrix2x3 physics1;
    public short phy1Unk1; //0x8C
    public short phy1Unk2; //0x8E
    public Matrix2x3 physics2;
    public short phy2Unk1; //0x9C
    public short phy2Unk2; //0x9E

    public Vector3Int vectorUnk1; //0xA0
    public short unk1; //0xA6

    // Start is called before the first frame update
    void Start()
    {

        vTransform.position = new Vector3Int(
            (int)(transform.localPosition.x * GameManager.translateFactor), 
            (int)(-transform.localPosition.y * GameManager.translateFactor), 
            (int)(transform.localPosition.z * GameManager.translateFactor));

        vTransform.rotation.Euler2Matrix(transform.localRotation.eulerAngles * Mathf.Deg2Rad);
        //screen = position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(
            (float)vTransform.position.x / GameManager.translateFactor, 
            (float)-vTransform.position.y / GameManager.translateFactor, 
            (float)vTransform.position.z / GameManager.translateFactor);

        //transform.localRotation = vTransform.rotation.Matrix2Quaternion;
        transform.localRotation = Quaternion.Euler(vTransform.rotation.EulerXYZ);
        /*float x = (float)Utilities.FUN_2A248(vTransform.rotation) / 4096;
        float y = (float)Utilities.FUN_2A27C(vTransform.rotation) / 4096;
        float z = (float)Utilities.FUN_2A2AC(vTransform.rotation) / 4096;

        int iVar = x;

        if (iVar < 0)
            iVar = -iVar;

        if (0x400 < iVar)
            x = 0x800 - x;

        iVar = z;

        if (iVar < 0)
            iVar = -iVar;

        if (0x400 < iVar)
            z = 0x800 - z;

        transform.localRotation = Quaternion.Euler(new Vector3(0, y, 0) * 360f);*/
    }

    // FUN_2CF74
    public void ApplyTransformation()
    {
        vTransform.position = screen;
        vTransform.rotation = Utilities.RotMatrixYXZ(vr);
        /*Vector3 eulerAngles = new Vector3
            ((float)-vr.x / 4096, (float)vr.y / 4096, (float)vr.z / 4096);
        transform.localRotation = Quaternion.Euler(eulerAngles * 360f);*/
        padding1 = 0;
    }

    //FUN_2CF44
    public void ApplyRotationMatrix()
    {
        vTransform.rotation = Utilities.RotMatrixYXZ(vr);
        /*Vector3 eulerAngles = new Vector3
            ((float)-vr.x / 4096, (float)vr.y / 4096, (float)vr.z / 4096);
        transform.localRotation = Quaternion.Euler(eulerAngles * 360f);*/
        padding1 = 0;
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

        if (pointerUnk1 != 0)
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

        if (pointerUnk1 != 0)
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

        if (pointerUnk1 != 0)
        {
            // ...
        }

        return vertexHeight;
    }

    public uint FUN_2CA1C()
    {
        uint uVar1;
        VehicleConfigContainer container = FUN_2C9A4();

        if (container == null)
            uVar1 = 0;
        else
        {
            uVar1 = 0;
            //...
        }

        return uVar1;
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

    public int FUN_4DCD8()
    {
        int configIndex = (unk2 << 3) - unk2 << 2;
        int nextContainer = vConfig.configContainers[configIndex / 0x1C].next;
        int iVar1 = 0;

        while (nextContainer != -1)
        {
            configIndex = (nextContainer << 3) - nextContainer << 2;
            int flag = vConfig.configContainers[configIndex / 0x1C].flag >> 12;

            if (flag == 15)
            {
                nextContainer = vConfig.configContainers[configIndex / 0x1C].next;
                iVar1++;
            }
            else
            {
                nextContainer = vConfig.configContainers[configIndex / 0x1C].previous;
            }
        }

        return iVar1;
    }

    private VehicleConfigContainer FUN_2C9A4()
    {
        int configIndex = (unk2 << 3) - unk2 << 2;
        short sVar1 = vConfig.configContainers[configIndex / 0x1C].next;
        VehicleConfigContainer container;

        while (true)
        {
            if (sVar1 == -1)
                return null;

            configIndex = (sVar1 << 3) - sVar1 << 2;
            configIndex = configIndex / 0x1C;
            container = vConfig.configContainers[configIndex];

            if ((uint)container.flag >> 12 == 11)
                break;

            sVar1 = container.previous;
        }

        return container;
    }
}
