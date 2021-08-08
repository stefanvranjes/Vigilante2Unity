using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileL : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_44790
    public override uint UpdateW(int arg1, int arg2)
    {
        uint uVar7;

        switch (arg1)
        {
            case 0:
                FUN_42330(arg2);
                uVar7 = 0;
                break;
            default:
                uVar7 = 0;
                break;
        }

        return uVar7;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        int iVar6;
        VigObject oVar6;
        uint uVar7;
        ushort uVar8;

        switch (arg1)
        {
            case 1:
                maxHalfHealth = 12;
                flags |= 0x4000;
                goto default;
            default:
                uVar7 = 0;
                break;
            case 12:
                oVar6 = FUN_445B0((Vehicle)arg2, 188);
                uVar8 = 60;

                if (((Vehicle)arg2).doubleDamage != 0)
                    uVar8 = 120;

                oVar6.maxHalfHealth = uVar8;
                //sound
                uVar7 = 90;
                break;
            case 13:
                iVar6 = Utilities.FUN_29F6C(arg2.screen, ((Vehicle)arg2).target.screen);
                uVar7 = (uint)(0x3b5ffe < (uint)(iVar6 - 0x32001 ^ 1) ? 1 : 0);
                break;
        }

        return uVar7;
    }

    private VigObject FUN_445B0(Vehicle param1, short param2)
    {
        Missile ppcVar2;
        int iVar3;
        VigObject pcVar4;
        int iVar5;
        Vector3Int local_18;

        ppcVar2 = LevelManager.instance.FUN_42408(param1, this, (ushort)param2, typeof(Missile), null) as Missile;
        local_18 = new Vector3Int(
            ppcVar2.vTransform.rotation.V01 << 5,
            ppcVar2.vTransform.rotation.V11 << 5,
            ppcVar2.vTransform.rotation.V21 << 5);
        ppcVar2.flags = 0x20000084;
        ppcVar2.FUN_305FC();
        iVar5 = param1.physics1.X;

        if (iVar5 < 0)
            iVar5 += 127;

        iVar3 = ppcVar2.vTransform.rotation.V01 * 1750;

        if (iVar3 < 0)
            iVar3 += 4095;

        ppcVar2.physics1.Z = (iVar5 >> 7) - (iVar3 >> 12);
        iVar5 = param1.physics1.Y;

        if (iVar5 < 0)
            iVar5 += 127;

        iVar3 = ppcVar2.vTransform.rotation.V11 * 1750;

        if (iVar3 < 0)
            iVar3 += 4095;

        ppcVar2.physics1.W = (iVar5 >> 7) - (iVar3 >> 12);
        iVar5 = param1.physics1.Z;

        if (iVar5 < 0)
            iVar5 += 127;

        iVar3 = ppcVar2.vTransform.rotation.V21 * 1750;

        if (iVar3 < 0)
            iVar3 += 4095;

        ppcVar2.physics2.X = (iVar5 >> 7) - (iVar3 >> 12);
        pcVar4 = param1.target;

        if (param1.target == null)
            pcVar4 = param1;

        ppcVar2.DAT_84 = pcVar4;
        param1.FUN_2B370(local_18, ppcVar2.screen);
        maxHalfHealth--;

        if (maxHalfHealth == 0)
            FUN_3A368();

        return ppcVar2;
    }
}
