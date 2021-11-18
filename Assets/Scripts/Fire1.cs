using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire1 : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public XOBF_DB DAT_98;

    //FUN_3982C
    public override uint UpdateW(int arg1, int arg2)
    {
        short sVar1;
        uint uVar2;
        VigObject oVar2;
        Vehicle vVar2;
        Fire2 ppcVar3;
        uint uVar4;
        int iVar6;
        Vehicle vVar6;
        int iVar9;

        if (arg1 == 0)
        {
            sVar1 = (short)(physics1.M0 - 1);
            physics1.M0 = sVar1;
            uVar2 = 0;

            if (sVar1 == -1)
            {
                ppcVar3 = DAT_98.ini.FUN_2C17C((ushort)physics2.M3, typeof(Fire2), 8) as Fire2;
                uVar4 = GameManager.FUN_2AC5C();
                iVar6 = (int)(uVar4 & 0xfff) * 2;
                ppcVar3.flags |= 0x434;
                iVar9 = physics1.Y * GameManager.DAT_65C90[iVar6];

                if (iVar9 < 0)
                    iVar9 += 4095;

                ppcVar3.physics1.Z = iVar9 >> 12;
                iVar6 = physics1.Y * GameManager.DAT_65C90[iVar6 + 1];

                if (iVar6 < 0)
                    iVar6 += 4095;

                ppcVar3.physics2.X = iVar6 >> 12;
                iVar6 = (int)GameManager.FUN_2AC5C();
                ppcVar3.physics1.W = physics1.Z + (iVar6 * physics1.Z >> 15);
                ppcVar3.screen = GameManager.instance.FUN_2CE50(this);
                ppcVar3.FUN_4EE40();
                ppcVar3.FUN_30B78();
                physics1.M0 = physics1.M1;
                vVar2 = Utilities.FUN_2CDB0(this) as Vehicle;
                vVar2.FUN_39DCC(-maxHalfHealth, vTransform.position, true);

                if (DAT_18 != 0)
                {
                    uVar2 = GameManager.instance.FUN_1E478(ppcVar3.vTransform.position);
                    GameManager.instance.FUN_1E2C8(DAT_18, uVar2);
                    uVar2 = 0;
                }
            }
        }
        else
        {
            if (arg1 == 2)
            {
                vVar6 = Utilities.FUN_2CDB0(this) as Vehicle;
                vVar6.DAT_F6 &= 0xfff7;
                GameManager.instance.FUN_1DE78(DAT_18);
                oVar2 = FUN_2CCBC();
                GameManager.instance.FUN_307CC(oVar2);
                uVar2 = 0xffffffff;
            }
            else
                uVar2 = 0;
        }

        return uVar2;
    }
}
