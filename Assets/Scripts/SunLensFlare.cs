using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunLensFlare : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        transform.rotation = transform.localRotation;
    }

    //FUN_33674
    public override uint UpdateW(int arg1, int arg2)
    {
        uint uVar2;

        if (arg1 == 0)
        {
            uVar2 = 0;
        }
        else
        {
            uVar2 = 0;

            if (arg1 == 1)
            {
                vr = LevelManager.instance.DAT_10F8;
                vTransform = GameManager.FUN_2A39C();
                uVar2 = 0xffffffff;
                GameManager.instance.DAT_1124 = this;
                transform.parent = LevelManager.instance.defaultCamera.transform;
            }
        }

        return uVar2;
    }
}
