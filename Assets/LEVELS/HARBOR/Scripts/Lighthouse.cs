using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighthouse : VigObject
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
        ConfigContainer ccVar3;
        VigCamera cVar3;
        Vehicle puVar4;
        VigTransform auStack40;

        if (hit.collider1.ReadUInt16(0) == 1 && hit.collider1.ReadUInt16(2) != 0 && 
            hit.self.type == 2)
        {
            ccVar3 = FUN_2C5F4(0x8000);

            if (ccVar3 != null)
            {
                puVar4 = (Vehicle)hit.self;
                iVar2 = GameManager.instance.FUN_1DD9C();
                GameManager.instance.FUN_1E580(iVar2, GameManager.instance.DAT_C2C, 36, puVar4.vTransform.position);
                GameManager.instance.FUN_1E2C8(puVar4.DAT_18, 0);
                puVar4.state = _VEHICLE_TYPE.Lighthouse;
                puVar4.tags = 0;
                puVar4.flags = puVar4.flags & 0xfffffffd | 0x6000020;
                auStack40 = GameManager.instance.FUN_2CEAC(this, ccVar3);
                puVar4.screen = auStack40.position;
                puVar4.vr = ccVar3.v3_2;
                puVar4.vr.y += vr.y;
                GameManager.instance.FUN_2F798(this, hit);
                puVar4.physics1.X = hit.normal1.x * 476;
                puVar4.physics1.Y = hit.normal1.y * 476;
                puVar4.physics1.Z = hit.normal1.z * 476;
                puVar4.physics2.X = 0;
                puVar4.physics2.Y = 0;
                puVar4.physics2.Z = 0;
                cVar3 = puVar4.vCamera;

                if (cVar3 != null)
                {
                    cVar3.DAT_84 = new Vector3Int(0, 0, 0);
                    cVar3.flags |= 0xc000000;
                }

                GameManager.instance.FUN_30CB0(puVar4, 64);
                return 0;
            }
        }

        if (hit.self.type != 8)
            return 0;

        hit.object1.FUN_32B90(hit.self.maxHalfHealth);
        return 0;
    }

    //FUN_1170 (HARBOR.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        VigObject oVar3;

        if (arg1 < 4)
        {
            if (arg1 != 1)
                return 0;

            oVar3 = child2;

            while (oVar3 != null)
            {
                oVar3.maxHalfHealth = 100;
                oVar3 = oVar3.child2;
            }

            return 0;
        }

        if (arg1 != 8)
            return 0;

        FUN_32B90((uint)arg2);
        return 0;
    }
}
