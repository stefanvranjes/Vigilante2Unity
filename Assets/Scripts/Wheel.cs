using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum _WHEEL_TYPE
{
    Unflatten, 
    Flatten //FUN_395E0
}

public class Wheel : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public _WHEEL_TYPE state;

    public override uint UpdateW(int arg1, int arg2)
    {
        Vehicle vVar1;
        int iVar3;

        switch (state)
        {
            case _WHEEL_TYPE.Flatten:
                if (arg1 == 2)
                {
                    vVar1 = Utilities.FUN_2CDB0(this) as Vehicle;
                    iVar3 = 0;
                    flags &= 0xbfffffff;
                    physics2.X += 0xc00;

                    do
                    {
                        if (vVar1.wheels[iVar3] != null &&
                            (vVar1.wheels[iVar3].flags & 0x40000000) != 0)
                            return 0;

                        iVar3++;
                    } while (iVar3 < 6);

                    vVar1.flags &= 0xfffdffff;
                    state = _WHEEL_TYPE.Unflatten;
                }

                return 0;
        }

        return 0;
    }
}
