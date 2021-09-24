using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle7 : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_4D454
    public override uint UpdateW(int arg1, int arg2)
    {
        if (arg1 == 2)
        {
            LevelManager.instance.FUN_4D16C(vData, (ushort)DAT_1A, vTransform);
            Destroy(this);
        }

        return 0;
    }
}
