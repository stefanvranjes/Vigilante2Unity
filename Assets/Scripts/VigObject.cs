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

            if (fV02 < 1)
            {
                if (fV02 > -1)
                {
                    y = Math.Asin(fV02);
                    x = Math.Atan2(-fV12, fV22);
                    z = Math.Atan2(-fV01, fV00);
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

            return new Vector3((float)x, (float)y, (float)z) * Mathf.Rad2Deg;
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

            if (fV10 < 1)
            {
                if (fV10 > -1)
                {
                    z = Math.Asin(fV10);
                    y = Math.Atan2(-fV20, fV00);
                    x = Math.Atan2(-fV12, fV11);
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
            x = 0;
            y = 0;
            z = 0;
            x = Math.Atan2(fV12, fV11);
            y = Math.Atan2(fV02, fV00);
            z = Math.Asin(fV10);

            return new Vector3((float)x, (float)y, (float)z) * Mathf.Rad2Deg;
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

public class VigObject : MonoBehaviour
{
    public uint flags;
    public byte type;
    public byte ai;
    public short id;

    public VigObject child; //0x0C
    public VigObject child2; //0x10
    public VigObject parent; //0x14

    public short unk2; //0x1A

    public ushort maxHalfHealth;
    public ushort maxFullHealth;

    public Vector3Int position;
    public Matrix3x3 rotation;

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
    public uint pointerUnk2; //0x78
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
        
        position = new Vector3Int(
            (int)(transform.localPosition.x * GameManager.translateFactor), 
            (int)(-transform.localPosition.y * GameManager.translateFactor), 
            (int)(transform.localPosition.z * GameManager.translateFactor));

        rotation.Euler2Matrix(transform.localRotation.eulerAngles * Mathf.Deg2Rad);
        //screen = position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(
            (float)position.x / GameManager.translateFactor, 
            (float)-position.y / GameManager.translateFactor, 
            (float)position.z / GameManager.translateFactor);

        transform.localRotation = Quaternion.Euler(rotation.Euler);
    }

    // FUN_2CF74
    public void ApplyTransformation()
    {
        position = screen;
        rotation = Utilities.RotMatrixYXZ(vr);
        padding1 = 0;
    }

    public void FUN_24700(short x, short y, short z)
    {
        Coprocessor.rotationMatrix.rt11 = rotation.V00;
        Coprocessor.rotationMatrix.rt12 = rotation.V01;
        Coprocessor.rotationMatrix.rt13 = rotation.V02;
        Coprocessor.rotationMatrix.rt21 = rotation.V10;
        Coprocessor.rotationMatrix.rt22 = rotation.V11;
        Coprocessor.rotationMatrix.rt23 = rotation.V12;
        Coprocessor.rotationMatrix.rt31 = rotation.V20;
        Coprocessor.rotationMatrix.rt32 = rotation.V21;
        Coprocessor.rotationMatrix.rt33 = rotation.V22;
        Coprocessor.accumulator.ir1 = 0x1000;
        Coprocessor.accumulator.ir2 = z;
        Coprocessor.accumulator.ir3 = (short)-y;
        Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.IR, _MVMVA_TRANSLATION_VECTOR.None, 12, false);

        rotation.V00 = Coprocessor.accumulator.ir1;
        rotation.V10 = Coprocessor.accumulator.ir2;
        rotation.V20 = Coprocessor.accumulator.ir3;

        Coprocessor.vector0.vx0 = (short)-z;
        Coprocessor.vector0.vy0 = 0x1000;
        Coprocessor.vector0.vz0 = x;
        Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.V0, _MVMVA_TRANSLATION_VECTOR.None, 12, false);

        rotation.V01 = Coprocessor.accumulator.ir1;
        rotation.V11 = Coprocessor.accumulator.ir2;
        rotation.V21 = Coprocessor.accumulator.ir3;

        Coprocessor.vector1.vx1 = y;
        Coprocessor.vector1.vy1 = (short)-x;
        Coprocessor.vector1.vz1 = 0x1000;
        Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.V1, _MVMVA_TRANSLATION_VECTOR.None, 12, false);

        rotation.V02 = Coprocessor.accumulator.ir1;
        rotation.V12 = Coprocessor.accumulator.ir2;
        rotation.V22 = Coprocessor.accumulator.ir3;
    }

    public void FUN_2AEAC(out Matrix3x3 m33, out Vector3Int p)
    {
        m33.V22 = 0;
        m33.V11 = 0;
        m33.V00 = 0;
        m33.V21 = physics2.M0;
        m33.V12 = (short)-physics2.M0;
        m33.V02 = physics2.M2;
        m33.V20 = (short)-physics2.M2;
        m33.V10 = physics2.M4;
        m33.V01 = (short)-physics2.M4;

        p = Utilities.FUN_2426C(rotation, physics1);
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

        position.x += iVar1 >> 7;
        iVar2 = physics1.Y;

        if (iVar2 < 0)
            iVar2 += 127;

        position.y += iVar2 >> 7;
        iVar1 = physics1.Z;

        if (iVar1 < 0)
            iVar1 += 127;

        position.z += iVar1 >> 7;

        rotation = Utilities.MatrixNormal(rotation);
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
        normalVector3 = Utilities.VectorNormalSS(normalVector3);
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
