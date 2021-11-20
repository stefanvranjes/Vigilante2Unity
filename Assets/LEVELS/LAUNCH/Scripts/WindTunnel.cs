using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTunnel : Destructible
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

    //FUN_2E30 (LAUNCH.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        sbyte sVar1;
        WindTunnel2 ppcVar2;
        int iVar3;
        ConfigContainer ccVar3;

        if (arg1 == 2)
        {
            iVar3 = (int)GameManager.FUN_2AC5C();
            GameManager.instance.FUN_30CB0(this, (iVar3 * 300 >> 15) + 600);
            ccVar3 = FUN_2C5F4(0x8000);

            if (ccVar3 != null)
            {
                ppcVar2 = vData.ini.FUN_2C17C(10, typeof(WindTunnel2), 8) as WindTunnel2;

                if (ppcVar2 != null)
                {
                    Utilities.ParentChildren(ppcVar2, ppcVar2);
                    ppcVar2.vTransform = GameManager.instance.FUN_2CEAC(this, ccVar3);
                    ppcVar2.type = 3;
                    ppcVar2.flags = 0x384;
                    ppcVar2.id = id;
                    iVar3 = (int)GameManager.FUN_2AC5C();
                    GameManager.instance.FUN_30CB0(ppcVar2, (iVar3 * 180 >> 15) + 300);
                    ppcVar2.FUN_305FC();
                    ppcVar2.tags = 0;
                    ppcVar2.DAT_19 = 0;
                    ppcVar2.DAT_58 = 0xa0000;
                    sVar1 = (sbyte)GameManager.instance.FUN_1DD9C();
                    ppcVar2.DAT_18 = sVar1;
                    GameManager.instance.FUN_1E580(sVar1, ppcVar2.vData.sndList, 5, ppcVar2.vTransform.position, true);
                }
            }
        }
        else
        {
            if (arg1 < 3)
            {
                if (arg1 == 1)
                {
                    iVar3 = (int)GameManager.FUN_2AC5C();
                    GameManager.instance.FUN_30CB0(this, iVar3 * 600 >> 15);
                }
            }
            else
            {
                if (arg1 == 8)
                    FUN_32B90((uint)arg2);
            }
        }

        return 0;
    }
}
