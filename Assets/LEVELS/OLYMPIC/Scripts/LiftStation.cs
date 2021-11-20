using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftStation : Destructible
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
        OLYMPIC oVar1;
        uint uVar2;
        VigObject oVar3;
        VigObject puVar4;

        oVar1 = (OLYMPIC)LevelManager.instance.level;
        puVar4 = hit.self;

        if (hit.object1.id == 1 && puVar4.type == 2 && 
            puVar4.id < 0 && oVar1.DAT_D2 != 0)
        {
            uVar2 = (uint)vr.y >> 31;

            if (oVar1.DAT_D0 != 0)
                uVar2 ^= 1;

            oVar3 = oVar1.DAT_B0[uVar2];
            oVar3.DAT_80 = puVar4;
            ((Vehicle)puVar4).state = _VEHICLE_TYPE.Gondola;
            puVar4.flags = puVar4.flags & 0xfffffff7 | 0x2000020;
            puVar4.physics1.X = (oVar3.vTransform.position.x - puVar4.vTransform.position.x) * 4;
            puVar4.physics1.Y = (oVar3.vTransform.position.y - (puVar4.vTransform.position.y - 0x10000)) * 4;
            puVar4.physics1.Z = (oVar3.vTransform.position.z - puVar4.vTransform.position.z) * 4;
            GameManager.instance.FUN_1E2C8(puVar4.DAT_18, 0);
            GameManager.instance.FUN_30CB0(puVar4, 32);
            oVar1.DAT_D2 = 0x4b0;
            puVar4.FUN_30BA8();
            puVar4.FUN_30B78();
        }

        return base.OnCollision(hit);
    }

    //FUN_1DBC (OLYMPIC.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        OLYMPIC oVar1;

        if (arg1 == 1)
        {
            oVar1 = (OLYMPIC)LevelManager.instance.level;
            OLYMPIC.FUN_CCC(oVar1.DAT_8C, oVar1.pole2M, this);
        }

        return base.UpdateW(arg1, arg2);
    }
}
