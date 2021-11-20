using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngPlant : Destructible
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
        bool bVar2;

        bVar2 = FUN_32CF0(hit);

        if (bVar2)
            FUN_30C68();

        return 0;
    }

    //FUN_1F7C (STEELMIL.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        IngPlant2 ppcVar1;
        bool bVar2;
        int iVar2;
        ConfigContainer ccVar2;

        if (arg1 == 2)
        {
            ppcVar1 = vData.ini.FUN_2C17C(29, typeof(IngPlant2), 8) as IngPlant2;
            iVar2 = (int)GameManager.FUN_2AC5C();
            ccVar2 = FUN_2C5F4((ushort)((iVar2 * 3 >> 15) - 0x8000 & 0xffff));
            ppcVar1.screen = Utilities.FUN_24148(vTransform, ccVar2.v3_1);
            ppcVar1.FUN_3066C();
            GameManager.instance.FUN_30CB0(this, 240);
        }
        else
        {
            if (arg1 < 3)
            {
                if (arg1 == 1)
                    GameManager.instance.FUN_30CB0(this, 120);
            }
            else
            {
                if (arg1 != 8)
                    return 0;

                bVar2 = FUN_32B90((uint)arg2);

                if (bVar2)
                    FUN_30C68();
            }
        }

        return 0;
    }
}
