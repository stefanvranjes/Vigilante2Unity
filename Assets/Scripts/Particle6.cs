using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum _PARTICLE6_TYPE
{
    Default, //0
    Type1, //FUN_4CB04
}

public class Particle6 : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public _PARTICLE6_TYPE state;

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        VigObject oVar1;

        switch (state)
        {
            case _PARTICLE6_TYPE.Type1:
                if (arg1 == 5)
                {
                    oVar1 = Utilities.FUN_2CD78(this);
                    oVar1.FUN_30C20();
                    vAnim = null;
                    return 0xffffffff;
                }

                break;
        }

        return 0;
    }
}
