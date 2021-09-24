using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Empty : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_49880
    public override uint UpdateW(int arg1, int arg2)
    {
        uint uVar2;

        switch (arg1)
        {
            case 0:
                FUN_42330(arg2);
                uVar2 = 0;
                break;
            default:
                uVar2 = 0;
                break;
        }

        return uVar2;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        short sVar1;
        uint uVar2;

        switch (arg1)
        {
            case 0:
                FUN_42330(arg2);
                uVar2 = 0;
                break;
            case 1:
                maxHalfHealth = 6;
                goto default;
            default:
                uVar2 = 0;
                break;
            case 12:
                sVar1 = (short)(maxHalfHealth - 1);
                maxHalfHealth = (ushort)sVar1;
                uVar2 = 60;

                if (sVar1 == 0)
                {
                    FUN_3A368();
                    uVar2 = 60;
                }

                break;
        }

        return uVar2;
    }
}
