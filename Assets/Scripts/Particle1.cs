using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle1 : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_4DE1C
    public override uint UpdateW(int arg1, VigObject arg2)
    {
        uint uVar1;

        if (arg1 == 5)
        {
            Tuple<List<VigTuple>, VigTuple> tuple;
            tuple = new Tuple<List<VigTuple>, VigTuple>(GameManager.instance.interObjs, TDAT_74);
            GameManager.instance.FUN_3094C(tuple);
            uVar1 = 0xffffffff;
        }
        else
            uVar1 = 0;

        return uVar1;
    }
}
