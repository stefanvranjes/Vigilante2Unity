using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailSwitch : VigObject
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
        int iVar2;
        byte bVar3;

        if ((flags & 0x80) != 0)
            return 0;

        bVar3 = (byte)((byte)tags ^ 1);
        tags = (sbyte)bVar3;
        SteelMil.instance.DAT_4618[id - 1000] = bVar3;
        FUN_30B78();
        iVar2 = GameManager.instance.FUN_1DD9C();
        GameManager.instance.FUN_1E628(iVar2, vData.sndList, 5, screen);
        return 0;
    }

    //FUN_43FC (STEELMIL.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        short sVar1;
        VigObject oVar4;

        if (arg1 == 1)
            return 0;

        if (arg1 != 0)
            return 0;

        oVar4 = child2;

        if (tags == 0)
        {
            sVar1 = (short)(oVar4.vr.z - 16);
            oVar4.vr.z = sVar1;

            if (-0x201 < sVar1) goto LAB_44A0;
        }
        else
        {
            sVar1 = (short)(oVar4.vr.z + 16);
            oVar4.vr.z = sVar1;

            if (sVar1 < 0x201) goto LAB_44A0;
        }

        FUN_30BA8();
        LAB_44A0:
        if (arg2 != 0)
            oVar4.ApplyTransformation();

        return 0;
    }
}
