using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VigChild : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override uint OnCollision(HitDetection param1)
    {
        VigObject ppcVar1;
        uint uVar2;

        ppcVar1 = Utilities.FUN_2CD78(this);

        if (ppcVar1.GetType().IsSubclassOf(typeof(VigObject)))
            uVar2 = 0;
        else
            uVar2 = ppcVar1.OnCollision(param1);

        return uVar2;
    }

    //FUN_4CA10
    public override uint UpdateW(int arg1, int arg2)
    {
        VigObject ppcVar1;
        uint uVar2;

        ppcVar1 = Utilities.FUN_2CD78(this);

        if (!ppcVar1.GetType().IsSubclassOf(typeof(VigObject)))
            uVar2 = 0;
        else
            uVar2 = ppcVar1.UpdateW(arg1, arg2);

        return uVar2;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        VigObject ppcVar1;
        uint uVar2;

        ppcVar1 = Utilities.FUN_2CD78(this);

        if (!ppcVar1.GetType().IsSubclassOf(typeof(VigObject)))
            uVar2 = 0;
        else
            uVar2 = ppcVar1.UpdateW(arg1, arg2);

        return uVar2;
    }
}
