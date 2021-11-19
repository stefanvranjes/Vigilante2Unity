using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mansion : Mausoleum
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

    //FUN_2A60 (BAYOU.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        return base.UpdateW(arg1, arg2);
    }
}
