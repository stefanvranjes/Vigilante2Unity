using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum _CHILD_TYPE
{
    Child, //FUN_4CA10
    Default
}

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

    public _CHILD_TYPE state;

    public override uint OnCollision(HitDetection param1)
    {
        VigObject ppcVar1;
        uint uVar2;

        if (state == _CHILD_TYPE.Child)
        {
            ppcVar1 = Utilities.FUN_2CD78(this);

            if (ppcVar1.GetType().IsSubclassOf(typeof(VigObject)))
                uVar2 = 0;
            else
                uVar2 = ppcVar1.OnCollision(param1);

            return uVar2;
        }

        return base.OnCollision(param1);
    }

    //FUN_4CA10
    public override uint UpdateW(int arg1, int arg2)
    {
        VigObject ppcVar1;
        uint uVar2;

        if (state == _CHILD_TYPE.Child)
        {
            ppcVar1 = Utilities.FUN_2CD78(this);

            if (!ppcVar1.GetType().IsSubclassOf(typeof(VigObject)))
                uVar2 = 0;
            else
                uVar2 = ppcVar1.UpdateW(arg1, arg2);

            return uVar2;
        }

        return base.UpdateW(arg1, arg2);
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        VigObject ppcVar1;
        uint uVar2;

        if (state == _CHILD_TYPE.Child)
        {
            ppcVar1 = Utilities.FUN_2CD78(this);

            if (!ppcVar1.GetType().IsSubclassOf(typeof(VigObject)))
                uVar2 = 0;
            else
                uVar2 = ppcVar1.UpdateW(arg1, arg2);

            return uVar2;
        }

        return base.UpdateW(arg1, arg2);
    }
}
