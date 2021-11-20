using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disco3 : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_400 (MARATHON.DLL)
    public override uint UpdateW(int arg1, VigObject arg2)
    {
        if (arg1 == 5)
        {
            vAnim = null;
            return 0xffffffff;
        }

        return 0;
    }
}
