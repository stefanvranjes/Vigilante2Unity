using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum _TURBINESHOCK2_TYPE
{
    Default,
    Type1 //FUN_19B8 (NUCLEAR.DLL)
}

public class TurbineShock2 : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public _TURBINESHOCK2_TYPE state;

    //FUN_19B8 (NUCLEAR.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        VigObject oVar1;

        switch (state)
        {
            case _TURBINESHOCK2_TYPE.Type1:
                if (arg1 == 2)
                {
                    state = _TURBINESHOCK2_TYPE.Default;
                    oVar1 = Utilities.FUN_2CDB0(this);
                    oVar1.flags &= 0xffffffdf;
                }

                break;
        }

        return 0;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        switch (state)
        {
            case _TURBINESHOCK2_TYPE.Type1:
                if (arg1 == 5)
                {
                    FUN_30C20();
                    flags |= 2;
                    GameManager.instance.FUN_30CB0(this, 120);
                }

                break;
        }

        return 0;
    }
}
