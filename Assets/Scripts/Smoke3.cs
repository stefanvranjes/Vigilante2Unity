using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke3 : VigObject
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

    //FUN_4F514
    public override uint UpdateW(int arg1, int arg2)
    {
        short sVar1;
        uint uVar2;
        VigObject oVar2;
        uint uVar3;
        Smoke2 ppcVar4;
        int iVar5;
        int iVar6;
        VigObject oVar6;
        VigTransform t0;

        if (arg1 == 0)
        {
            if ((flags & 0x2000000) != 0)
            {
                oVar2 = Utilities.FUN_2CD78(this);
                t0 = GameManager.instance.FUN_2CDF4(oVar2);
                vTransform.rotation = Utilities.TransposeMatrix(t0.rotation);
            }

            sVar1 = (short)(physics1.M0 - 1);
            physics1.M0 = sVar1;

            if (sVar1 == -1)
            {
                uVar3 = GameManager.FUN_2AC5C();
                ppcVar4 = DAT_98.ini.FUN_2C17C((ushort)physics2.M3, typeof(Smoke2), 8) as Smoke2;
                iVar5 = (int)(uVar3 & 0xfff) * 2;
                ppcVar4.flags |= 0x410;
                iVar6 = physics1.Y * GameManager.DAT_65C90[iVar5];

                if (iVar6 < 0)
                    iVar6 += 4095;

                ppcVar4.physics1.Z = iVar6 >> 12;
                iVar6 = physics1.Y * GameManager.DAT_65C90[iVar5 + 1];

                if (iVar6 < 0)
                    iVar6 += 4095;

                ppcVar4.physics2.X = iVar6 >> 12;
                iVar6 = (int)GameManager.FUN_2AC5C();
                iVar5 = physics1.Z;
                ppcVar4.vTransform.position = new Vector3Int(0, 0, 0);
                ppcVar4.physics1.W = iVar5 + (iVar6 * iVar5 >> 15);
                ppcVar4.vTransform = GameManager.FUN_2A39C();
                Utilities.FUN_2CC48(this, ppcVar4);
                Utilities.ParentChildren(this, this);
                physics1.M0 = physics1.M1;
            }

            oVar6 = child2;

            if (oVar6 == null)
            {
                if (parent == null)
                {
                    GameManager.instance.FUN_309A0(this);
                    uVar2 = 0xffffffff;
                }
                else
                {
                    oVar2 = FUN_2CCBC();
                    GameManager.instance.FUN_307CC(oVar2);
                    uVar2 = 0xffffffff;
                }
            }
            else
            {
                do
                {
                    oVar6.vTransform.position.x += oVar6.physics1.Z;
                    oVar6.vTransform.position.y += oVar6.physics1.W;
                    oVar6.vTransform.position.z += oVar6.physics2.X;
                    oVar6 = oVar6.child;
                    uVar2 = 0;
                } while (oVar6 != null);
            }
        }
        else
        {
            uVar2 = 0;

            if (arg1 == 2)
            {
                physics1.M0 = -1;
                uVar2 = 0;
            }
        }

        return uVar2;
    }
}
