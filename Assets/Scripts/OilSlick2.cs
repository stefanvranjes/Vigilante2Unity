using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum _OILSLICK_TYPE
{
    Oilslick, 
    Ballistic
}

public class OilSlick2 : VigObject
{
    public _OILSLICK_TYPE state;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_48974
    public override uint UpdateW(int arg1, VigObject arg2)
    {
        switch (state)
        {
            case _OILSLICK_TYPE.Oilslick:
                sbyte sVar1;

                if (arg1 == 5)
                {
                    sVar1 = tags;
                    tags = (sbyte)(sVar1 - 1);

                    if (sVar1 == 1)
                    {
                        FUN_2C124(220);
                        state = _OILSLICK_TYPE.Ballistic;
                        return 0xffffffff;
                    }

                    id = 0;
                }

                return 0;
            case _OILSLICK_TYPE.Ballistic:
                VigObject oVar1;
                uint uVar1;

                if (arg1 == 5)
                {
                    if (parent == null)
                    {
                        GameManager.instance.FUN_309A0(this);
                        uVar1 = 0xfffffffe;
                    }
                    else
                    {
                        oVar1 = FUN_2CCBC();
                        GameManager.instance.FUN_307CC(oVar1);
                        uVar1 = 0xfffffffe;
                    }
                }
                else
                    uVar1 = 0;

                return uVar1;
        }

        return 0;
    }
}
