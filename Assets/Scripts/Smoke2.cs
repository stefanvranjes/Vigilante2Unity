using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke2 : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_4F034
    public override uint UpdateW(int arg1, VigObject arg2)
    {
        if (arg1 == 5)
        {
            FUN_2CCBC();
            GameManager.instance.FUN_2C4B4(this);
            return 0xffffffff;
        }

        return 0;
    }
}
