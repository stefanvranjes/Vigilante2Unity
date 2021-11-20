using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exhaust : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_1F90 (LAUNCH.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        if (arg1 == 2)
        {
            FUN_1E78();
            GameManager.instance.FUN_307CC(this);
        }

        return 0;
    }

    private static void FUN_1E78()
    {
        VigObject oVar1;
        ConfigContainer ccVar2;
        LaunchRocket3 ppcVar3;
        VigCollider pcVar4;
        int iVar5;

        oVar1 = GameManager.instance.FUN_318D0(49);
        iVar5 = 0;

        if (oVar1 != null)
        {
            do
            {
                ccVar2 = oVar1.FUN_2C5F4((ushort)(iVar5 - 0x8000 & 0xffff));

                if (ccVar2 != null)
                {
                    GameObject obj = new GameObject();
                    ppcVar3 = obj.AddComponent<LaunchRocket3>();

                    if (ppcVar3 != null)
                    {
                        pcVar4 = null;

                        if (-1 < ccVar2.colliderID)
                            pcVar4 = new VigCollider(oVar1.vData.cbbList[ccVar2.colliderID].buffer);

                        ppcVar3.vCollider = pcVar4;
                        ppcVar3.vTransform = GameManager.instance.FUN_2CEAC(oVar1, ccVar2);
                        ppcVar3.flags = 0x182;
                        ppcVar3.type = 3;
                        ppcVar3.FUN_305FC();
                        ppcVar3.DAT_58 = 0xd0000;
                        GameManager.instance.FUN_30CB0(ppcVar3, 180);
                    }
                }

                iVar5++;
            } while (iVar5 < 2);
        }
    }
}
