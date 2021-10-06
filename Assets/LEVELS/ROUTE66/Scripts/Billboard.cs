using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : Destructible
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

    //FUN_4500 (ROUTE66.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        int iVar1;

        if (arg1 == 1)
        {
            iVar1 = (int)GameManager.FUN_2AC5C();
            iVar1 = iVar1 * 3 >> 15;
            FUN_2C01C();
            GameManager.instance.FUN_2FEE8(this, (ushort)(iVar1 * 60));
            tags = (sbyte)iVar1;
            return 0;
        }
        else
            return base.UpdateW(arg1, arg2);
    }
}
