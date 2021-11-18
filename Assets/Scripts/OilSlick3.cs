using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilSlick3 : VigObject
{
    protected override void Start()
    {
        
    }

    protected override void Update()
    {
        
    }

    //FUN_488FC
    public override uint UpdateW(int arg1, int arg2)
    {
        uint uVar1;
        Wheel wVar2;
        Vehicle vVar3;
        int iVar4;

        if (arg1 == 2)
        {
            iVar4 = 0;
            vVar3 = (Vehicle)child;

            do
            {
                wVar2 = vVar3.wheels[iVar4];

                if (wVar2 != null)
                    wVar2.flags &= 0xfbffffff;

                iVar4++;
            } while (iVar4 < 4);

            GameManager.instance.FUN_30904(this);
            uVar1 = 0xffffffff;
        }
        else
            uVar1 = 0;

        return uVar1;
    }
}
