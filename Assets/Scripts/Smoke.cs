using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public XOBF_DB DAT_98; //0x98

    //FUN_4F1C4
    public override uint UpdateW(int arg1, int arg2)
    {
        short sVar1;
        short sVar2;
        uint uVar3;
        VigObject oVar3;
        Smoke2 ppcVar4;
        int iVar5;
        VigObject oVar5;
        uint uVar6;
        int iVar7;
        VigTransform t0;

        if (arg1 == 0)
        {
            if ((flags & 0x2000000) != 0)
            {
                oVar3 = Utilities.FUN_2CD78(this);
                t0 = GameManager.instance.FUN_2CDF4(oVar3);
                vTransform.rotation = Utilities.TransposeMatrix(t0.rotation);
            }

            sVar1 = (short)(physics1.M0 - 1);
            physics1.M0 = sVar1;

            if (sVar1 == -1)
            {
                ppcVar4 = DAT_98.ini.FUN_2C17C((ushort)physics2.M3, typeof(Smoke2), 8) as Smoke2;
                ppcVar4.flags |= 0x410;
                sVar2 = physics2.M2;
                ppcVar4.physics1.X = physics2.X;
                ppcVar4.physics1.M2 = sVar2;
                ppcVar4.physics2.W = physics1.Y;
                iVar5 = (int)GameManager.FUN_2AC5C();
                ppcVar4.physics1.W = physics1.Z + (iVar5 * physics1.Z >> 15);
                ppcVar4.screen = new Vector3Int(0, 0, 0);

                if ((flags & 0x1000000) != 0)
                {
                    sVar2 = (short)GameManager.FUN_2AC5C();
                    ppcVar4.physics1.M3 = sVar2;
                }

                Utilities.FUN_2CC48(this, ppcVar4);
                Utilities.ParentChildren(this, this);
                physics1.M0 = physics1.M1;
            }

            oVar5 = child2;

            if (oVar5 == null)
            {
                if (parent == null)
                {
                    GameManager.instance.FUN_309A0(this);
                    uVar3 = 0xffffffff;
                }
                else
                {
                    oVar3 = FUN_2CCBC();
                    GameManager.instance.FUN_307CC(oVar3);
                    uVar3 = 0xffffffff;
                }
            }
            else
            {
                do
                {
                    oVar5.screen.y += oVar5.physics1.W;
                    uVar6 = (uint)(ushort)oVar5.physics1.M3 + (ushort)oVar5.physics1.M1;
                    oVar5.physics1.M3 = (short)uVar6;
                    oVar5.vr.z += oVar5.physics1.M2;
                    iVar7 = oVar5.physics2.Z * GameManager.DAT_65C90[(uVar6 & 0xfff) * 2];

                    if (iVar7 < 0)
                        iVar7 += 4095;

                    oVar5.screen.x = iVar7 >> 12;
                    iVar7 = oVar5.physics2.Z * GameManager.DAT_65C90[(oVar5.physics1.M3 & 0xfff) * 2];

                    if (iVar7 < 0)
                        iVar7 += 4095;

                    oVar5.screen.z = iVar7 >> 12;
                    oVar5.physics2.Z = oVar5.physics2.Z + oVar5.physics2.W;

                    if (arg2 != 0)
                        oVar5.ApplyTransformation();

                    oVar5 = oVar5.child;
                    uVar3 = 0;
                } while (oVar5 != null);
            }
        }
        else
        {
            uVar3 = 0;

            if (arg1 == 2)
            {
                physics1.M0 = -1;
                uVar3 = 0;
            }
        }

        return uVar3;
    }
}
