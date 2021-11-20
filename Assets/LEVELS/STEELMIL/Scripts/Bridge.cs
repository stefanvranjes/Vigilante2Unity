using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeS : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_452C (STEELMIL.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        RSEG_DB rVar1;

        if (arg1 == 8)
            FUN_32B90((uint)arg2);
        else
        {
            if (arg1 == 9 && arg2 != 0)
            {
                rVar1 = LevelManager.instance.FUN_518DC(screen, -1);
                rVar1.DAT_08 = 0xffff;
            }
        }

        return 0;
    }
}
