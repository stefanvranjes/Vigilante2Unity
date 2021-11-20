using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disco2 : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    private static ushort[] DAT_95C = new ushort[] { 3, 1, 2, 10, 8, 40, 0 };

    //FUN_420 (MARATHON.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        ushort uVar1;
        int iVar2;
        VigObject oVar2;
        DiscoBall puVar3;
        uint uVar4;
        uint uVar5;
        uint uVar6;
        VigObject oVar6;
        int iVar7;
        int iVar8;
        uint uVar9;
        int iVar10;

        if (arg1 == 0)
        {
            screen.y += physics1.W;
            vr.x += 91;
            vr.y += 68;

            if (arg2 != 0)
                ApplyTransformation();

            if (physics1.W < 0)
                physics1.W += 56;
            else
                physics1.W = 0;

            uVar1 = (ushort)(physics2.M2 + 1);
            physics2.M2 = (short)uVar1;

            if ((uVar1 & 15) == 0)
            {
                iVar2 = (int)GameManager.FUN_2AC5C();
                uVar9 = (uint)(iVar2 * 3) >> 15;
                puVar3 = vData.ini.FUN_2C17C(DAT_95C[uVar9], typeof(DiscoBall), 8) as DiscoBall;
                uVar4 = GameManager.FUN_2AC5C();
                uVar5 = GameManager.FUN_2AC5C();
                iVar7 = DAT_58;
                puVar3.type = 8;
                iVar2 = (int)(uVar5 & 0xfff) * 2;
                puVar3.DAT_1A = 3;
                puVar3.tags = (sbyte)uVar9;
                iVar8 = (int)(uVar4 & 0xfff) * 2;
                puVar3.id = id;
                iVar10 = GameManager.DAT_65C90[iVar2 + 1] * GameManager.DAT_65C90[iVar8];

                if (iVar10 < 0)
                    iVar10 += 4095;

                iVar10 = (iVar10 >> 12) * iVar7;

                if (iVar10 < 0)
                    iVar10 += 4095;

                puVar3.screen.x = iVar10 >> 12;
                iVar10 = GameManager.DAT_65C90[iVar2] * iVar7;

                if (iVar10 < 0)
                    iVar10 += 4095;

                puVar3.screen.y = iVar10 >> 12;
                iVar2 = GameManager.DAT_65C90[iVar2 + 1] * GameManager.DAT_65C90[iVar8 + 1];

                if (iVar2 < 0)
                    iVar2 += 4095;

                iVar7 = (iVar2 >> 12) * iVar7;

                if (iVar7 < 0)
                    iVar7 += 4095;

                puVar3.screen.x = iVar7 >> 12;
                puVar3.flags = 0x60000410;
                puVar3.maxHalfHealth = maxHalfHealth;
                puVar3.DAT_84 = DAT_84;
                puVar3.DAT_80 = DAT_80;
                puVar3.ApplyTransformation();
                Utilities.FUN_2CC48(this, puVar3);
                Utilities.ParentChildren(this, this);
                GameManager.instance.FUN_30CB0(puVar3, 120);
            }

            uVar6 = 0;

            if (physics2.M2 == 240)
            {
                oVar2 = Utilities.FUN_2CD78(this);

                if (oVar2.maxHalfHealth == 0)
                    oVar2.FUN_3A368();
                else
                    oVar2.flags &= 0xf7ffffff;

                FUN_4DC94();
                oVar6 = FUN_2CCBC();
                GameManager.instance.FUN_307CC(oVar6);
                uVar6 = 0xffffffff;
                GameManager.instance.DAT_1084--;
            }
        }
        else
            uVar6 = 0;

        return uVar6;
    }
}
