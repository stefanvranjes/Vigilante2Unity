using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum _THROWAWAY_TYPE
{
    Spawnable, //FUN_4A6E0
    Unspawnable,  //FUN_4CD60
    Type3 //FUN_4CB4C
}

public class Throwaway : VigObject
{
    public byte DAT_87;
    public _THROWAWAY_TYPE state;

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
        int iVar1;
        int iVar3;
        int iVar4;
        VigObject oVar4;
        Vector3Int local_18;

        switch (state)
        {
            case _THROWAWAY_TYPE.Unspawnable:
                oVar4 = hit.self;

                if (oVar4.type != 2)
                    return 0;

                GameManager.instance.FUN_2F798(this, hit);
                iVar3 = physics1.Z * hit.normal1.x +
                        physics1.W * hit.normal1.y +
                        physics2.X * hit.normal1.z;

                if (iVar3 < 0)
                    iVar3 += 2047;

                iVar3 >>= 11;

                if (-1 < iVar3)
                    return 0;

                local_18 = new Vector3Int();
                local_18.x = physics1.Z << 7;
                local_18.y = physics1.W << 7;
                local_18.z = physics2.X << 7;
                oVar4.FUN_2B370(local_18, vTransform.position);

                if (oVar4.id < 0)
                    GameManager.instance.FUN_15B00(~oVar4.id, 255, 2, 128);

                iVar4 = iVar3 * hit.normal1.x;

                if (iVar4 < 0)
                    iVar4 += 4095;

                physics1.Z -= iVar4 >> 12;
                iVar4 = iVar3 * hit.normal1.y;

                if (iVar4 < 0)
                    iVar4 += 4095;

                physics1.W -= iVar4 >> 12;
                iVar3 = iVar3 * hit.normal1.z;

                if (iVar3 < 0)
                    iVar3 += 4095;

                physics2.X -= iVar3 >> 12;
                break;

            case _THROWAWAY_TYPE.Type3:
                GameManager.instance.FUN_2F798(this, hit);
                iVar1 = physics1.Z * hit.normal1.x +
                        physics1.W * hit.normal1.y +
                        physics2.X * hit.normal1.z;

                if (iVar1 < 0)
                    iVar1 += 2047;

                iVar1 >>= 11;

                if (-1 < iVar1)
                    return 0;

                iVar4 = iVar1 * hit.normal1.x;

                if (iVar4 < 0)
                    iVar4 += 4095;

                physics1.Z -= iVar4 >> 12;
                iVar4 = iVar1 * hit.normal1.y;

                if (iVar4 < 0)
                    iVar4 += 4095;

                physics1.W -= iVar4 >> 12;
                iVar1 = iVar1 * hit.normal1.z;

                if (iVar1 < 0)
                    iVar1 += 4095;

                physics2.X -= iVar1 >> 12;
                break;
        }

        return 0;
    }

    public override uint UpdateW(int arg1, int arg2)
    {
        int iVar1;
        byte bVar2;
        byte bVar3;
        int iVar3;
        Pickup pVar4;

        switch (state)
        {
            case _THROWAWAY_TYPE.Unspawnable:
                return FUN_4CD60(arg1, arg2);
            case _THROWAWAY_TYPE.Spawnable:
                iVar3 = (int)FUN_4CD60(arg1, arg2);

                if (iVar3 < 0)
                {
                    pVar4 = LevelManager.instance.FUN_4AD24(GameManager.DAT_63FA4[tags + 6]);
                    pVar4.maxHalfHealth = maxHalfHealth;
                    pVar4.vTransform = vTransform;
                    pVar4.screen = vTransform.position;
                    bVar2 = (byte)GameManager.FUN_2AC5C();
                    pVar4.DAT_19 = bVar2;
                    pVar4.FUN_3066C();
                    GameManager.instance.FUN_30CB0(pVar4, 600);
                }

                return (uint)iVar3;
            case _THROWAWAY_TYPE.Type3:
                if (arg1 == 1)
                {
                    bVar3 = 3;

                    if (10239 < DAT_58)
                    {
                        bVar3 = 1;

                        if (DAT_58 < 30720)
                            bVar3 = 2;
                    }

                    DAT_87 = bVar3;
                }
                else
                {
                    if (arg1 == 0)
                    {
                        vTransform.position.x += physics1.Z;
                        vTransform.position.y += physics1.W;
                        vTransform.position.z += physics2.X;
                        physics1.W += 90;
                        iVar1 = GameManager.instance.terrain.FUN_1B750((uint)vTransform.position.x, (uint)vTransform.position.z);

                        if (vTransform.position.y <= iVar1)
                            return 0;

                        vTransform.position.y = iVar1;
                        physics1.W = -physics1.W / 2;
                        bVar2 = (byte)(DAT_87 - 1);
                        DAT_87 = bVar2;

                        if (bVar2 != 0)
                            return 0;

                        GameManager.instance.FUN_309A0(this);
                        return 0xffffffff;
                    }
                }

                break;
        }

        return 0; //not in original code
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        byte bVar3;

        switch (state)
        {
            case _THROWAWAY_TYPE.Type3:
                if (arg1 == 1)
                {
                    bVar3 = 3;

                    if (10239 < DAT_58)
                    {
                        bVar3 = 1;

                        if (DAT_58 < 30720)
                            bVar3 = 2;
                    }

                    DAT_87 = bVar3;
                }

                break;
        }

        return 0;
    }

    private uint FUN_4CD60(int arg1, int arg2)
    {
        byte bVar1;
        sbyte sVar2;
        int iVar4;

        if (arg1 == 0)
        {
            vTransform.position.x += physics1.Z;
            vTransform.position.y += physics1.W;
            vTransform.position.z += physics2.X;
            FUN_24700(physics1.M0, physics1.M1, physics1.M2);
            bVar1 = (byte)(DAT_19 + 1);
            DAT_19 = bVar1;

            if ((bVar1 & 15) == 0)
                vTransform.rotation = Utilities.MatrixNormal(vTransform.rotation);

            iVar4 = physics1.W + 90;
            physics1.W = iVar4;

            if (0 < iVar4)
            {
                iVar4 = FUN_2CFBC(vTransform.position);

                if (vTransform.position.y <= iVar4)
                    return 0;

                vTransform.position.y = iVar4;
                physics1.W = -physics1.W / 2;

                if (vCollider != null)
                    LevelManager.instance.FUN_4DE54(vTransform.position, 13);

                sVar2 = (sbyte)(DAT_87 - 1);
                DAT_87 = (byte)sVar2;

                if (sVar2 != 0)
                    return 0;

                GameManager.instance.FUN_309A0(this);
                return 0xffffffff;
            }
        }

        return 0;
    }
}
