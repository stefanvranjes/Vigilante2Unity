using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreboard : Destructible
{
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
        return base.OnCollision(hit);
    }

    //FUN_4518 (OLYMPIC.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        sbyte sVar1;
        ushort uVar2;
        byte bVar3;
        int iVar4;
        uint uVar5;
        BufferedBinaryReader puVar6;

        if (arg1 != 2)
        {
            if (2 < arg1)
            {
                if (arg1 != 8)
                    return 0;

                FUN_32B90((uint)arg2);
                return 0;
            }

            if (arg1 != 1)
                return 0;

            flags &= 0xfffffffb;
            FUN_2C01C();
            FUN_2FBC8(0);
            return 0;
        }

        sVar1 = (sbyte)DAT_19;
        bVar3 = (byte)(sVar1 + 1);
        DAT_19 = bVar3;

        if (19 < (byte)(sVar1 + 1))
        {
            FUN_2C01C();
            FUN_2FBC8(0);
            return 0;
        }

        if ((bVar3 & 1) == 0)
        {
            FUN_2C01C();
            iVar4 = 0;
        }
        else
        {
            puVar6 = vAnim;

            if (puVar6 == null) goto LAB_4658;
            
            sVar1 = tags;

            if (sVar1 == 2)
                iVar4 = DAT_4A + puVar6.ReadUInt16(0) * 3;
            else
            {
                if (sVar1 < 3)
                {
                    if (sVar1 != 1) goto LAB_4658;

                    uVar2 = DAT_4A;
                    uVar5 = puVar6.ReadUInt16(0);
                }
                else
                {
                    if (sVar1 != 3) goto LAB_4658;

                    uVar2 = DAT_4A;
                    uVar5 = (uint)puVar6.ReadUInt16(0) << 1;
                }

                iVar4 = (int)(uVar2 + uVar5);
            }
        }

        FUN_2FBC8((ushort)iVar4);
        LAB_4658:
        GameManager.instance.FUN_30CB0(this, 30);
        return 0;
    }
}
