using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant3 : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public VigCamera[] DAT_8C = new VigCamera[2];
    public short[] cameraDefault = new short[2];

    //FUN_2914 (ROUTE66.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        int iVar1;
        int iVar2;
        int iVar3;
        VigCamera cVar4;
        Vehicle vVar5;
        Vehicle[] puVar6;
        int iVar7;

        if (arg1 == 2)
        {
            iVar1 = physics1.Y;
            iVar2 = -iVar1;
            physics1.Y = iVar2;

            if (iVar1 < 0)
            {
                iVar2 -= physics1.Z;
                physics1.Y = iVar2;

                if (iVar2 < 1)
                {
                    GameManager.instance.FUN_307CC(this);

                    for (int i = 0; i < DAT_8C.Length; i++)
                        if (DAT_8C[i] != null)
                            DAT_8C[i].DAT_94 = cameraDefault[i];

                    return 0xffffffff;
                }

                iVar1 = physics1.Z;
                iVar2 = iVar1;

                if (iVar1 < 0)
                    iVar2 += 31;

                physics1.Z = iVar1 - (iVar2 >> 5);
            }

            iVar7 = 0;
            iVar1 = physics1.Y;
            iVar3 = physics1.X;
            puVar6 = GameManager.instance.playerObjects;

            do
            {
                vVar5 = puVar6[iVar7];

                if (vVar5 != null && (vVar5.flags & 0x2000000) == 0 && vVar5.vCamera != null)
                {
                    cVar4 = DAT_8C[iVar7];

                    if (cVar4 == vVar5.vCamera)
                        cVar4.DAT_94 += (short)(iVar1 - iVar3 >> 16);
                    else
                    {
                        if (cVar4 != null)
                            cVar4.DAT_94 -= physics1.M1;

                        DAT_8C[iVar7] = vVar5.vCamera;
                        cameraDefault[iVar7] = DAT_8C[iVar7].DAT_94;
                        vVar5.vCamera.DAT_94 += physics1.M3;
                    }
                }

                iVar7++;
            } while (iVar7 < 2);

            physics1.X = physics1.Y;
            GameManager.instance.FUN_30CB0(this, 3);
        }

        return 0;
    }
}
