using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardTower : Destructible
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override uint OnCollision(HitDetection hit)
    {
        return base.OnCollision(hit);
    }

    public static VigObject OnInitialize(XOBF_DB arg1, int arg2, uint arg3)
    {
        Dictionary<int, Type> dict = new Dictionary<int, Type>();
        dict.Add(849, typeof(GuardTower2));
        return arg1.ini.FUN_2C17C((ushort)arg2, typeof(GuardTower), arg3, dict);
    }

    //FUN_361C (LAUNCH.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        VigObject ppcVar1;

        if (arg1 < 4)
        {
            if (arg1 == 1)
            {
                ppcVar1 = child2;

                if (ppcVar1 != null)
                {
                    do
                    {
                        if (ppcVar1.id == 1) break;

                        ppcVar1 = ppcVar1.child;
                    } while (ppcVar1 != null);

                    if (ppcVar1 != null)
                    {
                        ((GuardTower2)ppcVar1).state = _GUARDTOWER2_TYPE.Type1;
                        GameManager.instance.FUN_30CB0(ppcVar1, 8);
                    }
                }
            }
        }
        else
        {
            if (arg1 == 8)
                FUN_32B90((uint)arg2);
        }

        return 0;
    }
}
