using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballistic : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_4CA68
    public override uint UpdateW(int arg1, VigObject arg2)
    {
        VigObject oVar1;
        uint uVar1;

        if (arg1 == 5)
        {
            if (parent == null)
            {
                GameManager.instance.FUN_309A0(this);
                uVar1 = 0xfffffffe;
            }
            else
            {
                oVar1 = FUN_2CCBC();
                GameManager.instance.FUN_307CC(oVar1);
                uVar1 = 0xfffffffe;
            }
        }
        else
            uVar1 = 0;

        return uVar1;
    }
}
