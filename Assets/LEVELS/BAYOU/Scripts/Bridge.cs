using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : VigObject
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
        if (hit.self.type != 8)
            return 0;

        FUN_32B90(hit.self.maxHalfHealth);
        return 0;
    }

    //FUN_788 (BAYOU.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        if (arg1 < 4)
            return 0;

        if (arg1 != 8)
            return 0;

        FUN_32B90((uint)arg2);
        return 0;
    }
}
