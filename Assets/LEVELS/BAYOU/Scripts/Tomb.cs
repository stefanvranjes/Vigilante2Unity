using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tomb : VigObject
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
        bool bVar3;

        if (hit.self.type != 8)
            return 0;

        bVar3 = FUN_32B90(hit.self.maxHalfHealth);

        if (!bVar3)
            return 0;

        iVar2 = 60;
        GameManager.instance.FUN_30CB0(this, iVar2);
        return 0;
    }

    //FUN_24E0 (BAYOU.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        Ghost ppcVar1;
        int iVar2;
        bool bVar3;

        if (arg1 < 4)
        {
            if (arg1 != 2)
                return 0;

            ppcVar1 = vData.ini.FUN_2C17C(28, typeof(Ghost), 8) as Ghost;
            ppcVar1.screen = screen;
            ppcVar1.vr.x = 113;
            ppcVar1.flags = 0x184;
            ppcVar1.type = 8;
            ppcVar1.maxHalfHealth = 150;
            ppcVar1.FUN_3066C();
            ppcVar1.DAT_80 = ppcVar1;
            ppcVar1.screen.y -= 0x14000;
            iVar2 = GameManager.instance.FUN_1DD9C();
            GameManager.instance.FUN_1E628(iVar2, vData.sndList, 6, screen);
            iVar2 = 120;
            GameManager.instance.FUN_30CB0(ppcVar1, iVar2);
        }

        if (arg1 != 8)
            return 0;

        bVar3 = FUN_32B90((uint)arg2);

        if (!bVar3)
            return 0;

        iVar2 = 60;
        GameManager.instance.FUN_30CB0(this, iVar2);
        return 0;
    }
}
