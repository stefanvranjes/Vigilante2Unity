using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum _PARTICLE1_TYPE
{
    Default, //FUN_4DE1C
    LaunchRocket //FUN_183C (LAUNCH.DLL)
}

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

    public _PARTICLE1_TYPE state;

    public override uint UpdateW(int arg1, int arg2)
    {
        switch (state)
        {
            case _PARTICLE1_TYPE.LaunchRocket:
                if (arg1 == 0)
                {
                    vTransform.position.x += screen.x;
                    vTransform.position.y += screen.y;
                    vTransform.position.z += screen.z;
                    screen.y += 28;
                }

                return 0;
        }

        return 0;
    }

    //FUN_4DE1C
    public override uint UpdateW(int arg1, VigObject arg2)
    {
        uint uVar1;

        uVar1 = 0;

        switch (state)
        {
            case _PARTICLE1_TYPE.Default:
                if (arg1 == 5)
                {
                    Tuple<List<VigTuple>, VigTuple> tuple;
                    tuple = new Tuple<List<VigTuple>, VigTuple>(GameManager.instance.interObjs, TDAT_74);
                    GameManager.instance.FUN_3094C(tuple);
                    uVar1 = 0xffffffff;
                }
                else
                    uVar1 = 0;

                break;

            case _PARTICLE1_TYPE.LaunchRocket:
                uVar1 = 0;

                if (arg1 == 5)
                {
                    FUN_4EE8C(GameManager.instance.interObjs);
                    uVar1 = 0xfffffffe;
                }

                break;
        }

        return uVar1;
    }
}
