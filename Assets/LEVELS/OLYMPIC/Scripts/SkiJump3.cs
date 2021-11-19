using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkiJump3 : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_2628 (OLYMPIC.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        if (arg1 == 2)
        {
            ((VigCamera)PDAT_74).DAT_90 = -0x100;
            GameManager.instance.FUN_308C4(this);
        }

        return 0;
    }
}
