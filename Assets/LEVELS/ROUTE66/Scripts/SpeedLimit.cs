using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedLimit : VigObject
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
        Police ppcVar1;
        VigObject oVar2;

        oVar2 = hit.self;

        if (oVar2.type == 2 && 8392 < oVar2.physics1.W)
        {
            ppcVar1 = GameManager.instance.FUN_31994(typeof(Police)) as Police;

            if (ppcVar1 == null)
                return 0;

            if (ppcVar1.tags != 0)
                return 0;

            if (ppcVar1.DAT_F4 != id)
                return 0;

            if (!ppcVar1.GetType().IsSubclassOf(typeof(VigObject)))
                return 0;

            ppcVar1.UpdateW(20, oVar2);
            return 0;
        }

        if (oVar2.type != 8)
            return 0;

        if (hit.object1 != this)
            return 0;

        FUN_32B90(oVar2.maxHalfHealth);
        return 0;
    }

    //FUN_43DC (ROUTE66.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        if (arg1 < 4)
        {
            if (arg1 != 1)
                return 0;

            child2.type = 3;
            return 0;
        }

        if (arg1 != 8)
            return 0;

        FUN_32B90((uint)arg2);
        return 0;
    }
}
