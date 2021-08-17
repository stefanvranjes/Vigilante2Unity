using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum _OILSLICK_TYPE
{
    Oilslick, 
    Ballistic
}

public class OilSlick2 : VigObject
{
    public _OILSLICK_TYPE state;

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
        OilSlick3 ppcVar2;
        Wheel wVar3;
        int iVar5;
        bool bVar6;
        Vehicle pcVar7;

        if (hit.self.type != 2)
            return 0;

        pcVar7 = (Vehicle)hit.self;

        if ((pcVar7.flags & 0x4004000) != 0x4000)
            return 0;

        if (pcVar7.wheelsType != _WHEELS.Ground)
            return 0;

        bVar6 = false;
        iVar5 = 0;

        do
        {
            wVar3 = pcVar7.wheels[iVar5];

            if (wVar3 != null && (wVar3.flags & 0x4000000) == 0)
            {
                wVar3.flags |= 0x4000000;
                bVar6 = true;
            }

            iVar5++;
        } while (iVar5 < 4);

        if (!bVar6)
            return 0;

        GameObject obj = new GameObject();
        ppcVar2 = obj.AddComponent<OilSlick3>();
        ppcVar2.type = 255;
        ppcVar2.child = pcVar7;
        GameManager.instance.FUN_30CB0(ppcVar2, 180);

        if (pcVar7.physics2.Y < 0)
            iVar5 = pcVar7.physics1.W * -16;
        else
            iVar5 = pcVar7.physics1.W * 16;

        pcVar7.physics2.Y += iVar5;
        return 0;
    }

    //FUN_48974
    public override uint UpdateW(int arg1, VigObject arg2)
    {
        switch (state)
        {
            case _OILSLICK_TYPE.Oilslick:
                sbyte sVar1;

                if (arg1 == 5)
                {
                    sVar1 = tags;
                    tags = (sbyte)(sVar1 - 1);

                    if (sVar1 == 1)
                    {
                        FUN_2C124(220);
                        state = _OILSLICK_TYPE.Ballistic;
                        return 0xffffffff;
                    }

                    id = 0;
                }

                return 0;
            case _OILSLICK_TYPE.Ballistic:
                VigObject oVar1;
                uint uVar1;

                if (arg1 == 5)
                {
                    if (parent == null)
                    {
                        GameManager.instance.FUN_309A0(this);
                        uVar1 = 0xfffffffe;
                    }
                    else
                    {
                        oVar1 = FUN_2CCBC();
                        GameManager.instance.FUN_307CC(oVar1);
                        uVar1 = 0xfffffffe;
                    }
                }
                else
                    uVar1 = 0;

                return uVar1;
        }

        return 0;
    }
}
