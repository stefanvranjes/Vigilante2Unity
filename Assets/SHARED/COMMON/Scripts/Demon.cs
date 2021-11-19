using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public VigObject DAT_94; //0x94

    //FUN_444 (HALFTRAK.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        short sVar1;
        uint uVar2;
        VigTransform tVar3;
        int iVar4;
        int iVar5;
        int iVar6;
        Vector3Int local_20;
        Vector3Int local_18;

        if (arg1 == 0)
        {
            local_20 = new Vector3Int();
            local_20.x = GameManager.DAT_65C90[((ushort)vr.y & 0xfff) * 2] * 7;
            local_20.y = 0;
            local_20.z = GameManager.DAT_65C90[((ushort)vr.x & 0xfff) * 2 + 1] * 7;
            uVar2 = GameManager.FUN_2AC5C();

            if ((uVar2 & 31) == 0)
            {
                sVar1 = (short)GameManager.FUN_2AC5C();
                vr.y = sVar1;
            }

            tVar3 = GameManager.instance.FUN_2CDF4(DAT_94);
            local_18 = Utilities.FUN_23ED0(tVar3, local_20);
            local_18.x -= vTransform.position.x;
            local_18.y -= vTransform.position.y;
            local_18.z -= vTransform.position.z;
            Utilities.FUN_29FC8(local_18, out local_20);
            iVar4 = local_20.x;

            if (iVar4 < 0)
                iVar4 += 15;

            iVar5 = physics1.Z;
            iVar6 = iVar5;

            if (iVar5 < 0)
                iVar6 = iVar5 + 255;

            physics1.Z = iVar5 + ((iVar4 >> 4) - (iVar6 >> 8));
            iVar4 = local_20.y;

            if (iVar4 < 0)
                iVar4 += 15;

            iVar5 = physics1.W;
            iVar6 = iVar5;

            if (iVar5 < 0)
                iVar6 = iVar5 + 63;

            physics1.W = iVar5 + ((iVar4 >> 4) - (iVar6 >> 6));
            iVar4 = local_20.z;

            if (iVar4 < 0)
                iVar4 += 15;

            iVar5 = physics2.X;
            iVar6 = iVar5;

            if (iVar5 < 0)
                iVar6 = iVar5 + 255;

            physics2.X = iVar5 + ((iVar4 >> 4) - (iVar6 >> 8));
            vTransform.position.x += physics1.Z;
            vTransform.position.y += physics1.W;
            vTransform.position.z += physics2.X;
            Utilities.FUN_29FC8(new Vector3Int(physics1.Z, physics1.W, physics2.X), out local_20);
            vTransform.rotation = Utilities.FUN_2A724(local_20);
        }

        return 0;
    }
}
