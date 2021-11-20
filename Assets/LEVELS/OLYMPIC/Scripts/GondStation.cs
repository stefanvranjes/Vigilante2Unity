using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GondStation : Destructible
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
        uint uVar1;
        VigObject oVar3;
        VigObject puVar4;
        OLYMPIC oVar5;
        VigCamera cVar5;
        int iVar6;
        VigObject oVar7;

        puVar4 = hit.self;
        oVar5 = (OLYMPIC)LevelManager.instance.level;

        if (hit.object1.id == 1 && puVar4.type == 2 && 
            (puVar4.flags & 0x4000) != 0 && oVar5.DAT_D2 != 0)
        {
            uVar1 = (uint)vr.y >> 31;

            if (oVar5.DAT_D0 == 0)
                uVar1 ^= 1;

            oVar3 = oVar5.DAT_B0[uVar1];
            oVar3.DAT_80 = puVar4;
            ((Vehicle)puVar4).state = _VEHICLE_TYPE.Gondola;
            puVar4.PDAT_78 = oVar3;
            puVar4.flags = puVar4.flags & 0xfffffff7 | 0x6000020;
            puVar4.physics1.X = (oVar3.vTransform.position.x - puVar4.vTransform.position.x) * 4;
            puVar4.physics1.Y = (oVar3.vTransform.position.y - (puVar4.vTransform.position.y - 0x10000)) * 4;
            puVar4.physics1.Z = (oVar3.vTransform.position.z - puVar4.vTransform.position.z) * 4;
            GameManager.instance.FUN_1E2C8(puVar4.DAT_18, 0);
            GameManager.instance.FUN_30CB0(puVar4, 32);
            oVar5.DAT_D2 = 0x4b0;
            puVar4.FUN_30BA8();
            puVar4.FUN_30B78();
            cVar5 = ((Vehicle)puVar4).vCamera;

            if (cVar5 == null)
                return 0;

            cVar5.FUN_30BA8();
            cVar5.FUN_30B78();
            return 0;
        }

        if ((hit.self.type != 2 || hit.object1 == this) &&
            hit.self.type != 8)
            return 0;

        oVar7 = hit.self;
        iVar6 = 10;

        if (oVar7.type != 2)
            iVar6 = oVar7.maxHalfHealth;

        hit.object1.FUN_32B90((uint)iVar6);
        return 0;
    }

    //FUN_1B28 (OLYMPIC.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        OLYMPIC oVar1;

        if (arg1 < 4)
        {
            if (arg1 == 1)
            {
                oVar1 = (OLYMPIC)LevelManager.instance.level;
                OLYMPIC.FUN_CCC(oVar1.DAT_80_2, oVar1.pole1M, this);
            }
        }
        else
        {
            if (arg1 == 8)
            {
                FUN_32B90((uint)arg2);
                return 0;
            }
        }

        return base.UpdateW(arg1, arg2);
    }
}
