using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : Destructible
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
        FUN_32CF0(hit);
        return 0;
    }

    //FUN_32BC (HARBOR.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        short sVar1;
        VigObject oVar2;
        Vector2Int local_20;
        Vector2Int local_18;

        if (arg1 < 4)
        {
            if (arg1 == 2)
                FUN_4DC94();
        }
        else
        {
            if (arg1 == 8)
                FUN_32B90((uint)arg2);
            else
            {
                if (arg1 == 9 && arg2 == 0)
                {
                    local_20 = new Vector2Int(screen.x, screen.x);
                    local_18 = new Vector2Int(screen.z, screen.z);
                    sVar1 = id;
                    id = 0;
                    oVar2 = GameManager.instance.FUN_31C98(sVar1, sVar1, local_20, local_18);

                    if (oVar2 != null && oVar2.screen.y < screen.y)
                        GameManager.instance.FUN_30CB0(oVar2, 30);

                    id = sVar1;
                }
            }
        }

        return 0;
    }
}
