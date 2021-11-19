using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPower3 : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_80 (VERTIGO.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        int iVar2;
        int iVar3;

        if (arg1 == 0)
        {
            iVar2 = vTransform.rotation.V02;

            if (iVar2 < 0)
                iVar2 += 3;

            iVar3 = vTransform.rotation.V12;
            vTransform.position.x += iVar2 >> 2;

            if (iVar3 < 0)
                iVar3 += 3;

            iVar2 = vTransform.rotation.V22;
            vTransform.position.y += iVar3 >> 2;

            if (iVar2 < 0)
                iVar2 += 3;

            vTransform.position.z += iVar2 >> 2;
        }

        return 0;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        if (arg1 == 5)
        {
            GameManager.instance.FUN_309A0(this);
            return 0xffffffff;
        }

        return 0;
    }
}
