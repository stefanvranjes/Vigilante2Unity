using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum _THROWAWAY_TYPE
{
    Spawnable, //FUN_4A6E0
    Unspawnable //FUN_4CD60
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

    public override uint UpdateW(int arg1, int arg2)
    {
        byte bVar2;
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
        }

        return 0; //not in original code
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
