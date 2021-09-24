using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle8 : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_4EB8C
    public override uint UpdateW(int arg1, int arg2)
    {
        short sVar1;
        VigObject oVar2;
        Ballistic ppcVar3;

        if (arg1 == 2)
        {
            oVar2 = Utilities.FUN_2CD78(this);
            ppcVar3 = vData.ini.FUN_2C17C((ushort)DAT_1A, typeof(Ballistic), 8) as Ballistic;
            ppcVar3.screen = Utilities.FUN_24148(oVar2.vTransform, vTransform.position);
            ppcVar3.flags |= 0x434;
            sVar1 = (short)GameManager.FUN_2AC5C();
            ppcVar3.vr.z = sVar1;
            ppcVar3.FUN_3066C();
            GameManager.instance.FUN_30CB0(this, tags);
        }

        return 0;
    }
}
