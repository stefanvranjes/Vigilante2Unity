using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire2 : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_4F0CC
    public override uint UpdateW(int arg1, int arg2)
    {
        if (arg1 == 0)
        {
            vTransform.position.x += physics1.Z;
            vTransform.position.y += physics1.W;
            vTransform.position.z += physics2.X;
        }

        return 0;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        uint uVar1;

        uVar1 = 0;

        if (arg1 == 5)
        {
            FUN_4EE8C(GameManager.instance.interObjs);
            uVar1 = 0xffffffff;
        }

        return uVar1;
    }

    public void FUN_4EE40()
    {
        VigTuple tVar1;

        ApplyTransformation();
        FUN_2D1DC();
        tVar1 = GameManager.instance.FUN_30080(GameManager.instance.interObjs, this);
        TDAT_74 = tVar1;
        tVar1 = GameManager.instance.FUN_30080(GameManager.instance.DAT_10A8, this);
        TDAT_78 = tVar1;
    }
}
