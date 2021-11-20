using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace4 : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_2098 (STEELMIL.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        short sVar1;
        VigObject oVar2;

        if (arg1 == 0)
        {
            vTransform.position.x += physics1.Z;
            vTransform.position.z += physics2.X;
            sVar1 = (short)IDAT_78;
            vTransform.position.y += physics1.W;
            physics1.W += 90;
            IDAT_78 = sVar1 - 68;
            vTransform.rotation.V22 = sVar1;
            vTransform.rotation.V11 = sVar1;
            vTransform.rotation.V00 = sVar1;

            if (sVar1 - 68 < 205)
            {
                oVar2 = FUN_2CCBC();
                GameManager.instance.FUN_307CC(oVar2);
                return 0xffffffff;
            }
        }

        return 0;
    }
}
