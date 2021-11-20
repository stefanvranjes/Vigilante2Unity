using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gondola : Destructible
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
        uint uVar2;
        VigObject ppcVar3;

        ppcVar3 = hit.self;

        if (ppcVar3.type == 3 && DAT_80 != null)
        {
            hit.self = DAT_80;

            if (ppcVar3.GetType().IsSubclassOf(typeof(VigObject)))
                ppcVar3.OnCollision(hit);

            uVar2 = 1;
        }
        else
        {
            FUN_32CF0(hit);
            uVar2 = 0;
        }

        return uVar2;
    }

    //FUN_106C (OLYMPIC.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        uint uVar1;
        uint uVar2;

        switch (arg1)
        {
            case 1:
                maxHalfHealth = 24;
                uVar1 = flags | 0x108;
                goto LAB_1140;
            case 2:
                uVar1 = flags & 0xffffffdf;
                LAB_1140:
                flags = uVar1;
                CASE_4:
                uVar2 = 0;
                break;
            default:
                goto CASE_4;
            case 8:
                FUN_32B90((uint)arg2);
                uVar2 = 0;
                break;
        }

        return uVar2;
    }
}
