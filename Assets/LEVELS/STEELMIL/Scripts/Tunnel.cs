using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunnel : Destructible
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
        VigObject oVar1;
        int iVar2;
        VigCamera cVar2;
        uint uVar3;
        Vehicle puVar4;

        if (hit.collider1.ReadUInt16(0) == 1 && hit.collider1.ReadUInt16(2) != 0 && 
            hit.self.type == 2 && (hit.self.flags & 0x4000000) == 0)
        {
            puVar4 = (Vehicle)hit.self;

            do
            {
                iVar2 = (int)GameManager.FUN_2AC5C();
                uVar3 = (uint)(iVar2 * 3 >> 15) + 40;
                puVar4.DAT_DE = (byte)uVar3;
            } while ((uVar3 & 0xff) == id);

            GameManager.instance.FUN_1E2C8(puVar4.DAT_18, 0);
            puVar4.state = _VEHICLE_TYPE.Tunnel;
            puVar4.tags = 0;
            puVar4.flags = puVar4.flags & 0xfffffffd | 0x6000020;
            iVar2 = vTransform.rotation.V02 * 4577;

            if (iVar2 < 0)
                iVar2 += 31;

            puVar4.physics1.X = iVar2 >> 5;
            iVar2 = vTransform.rotation.V12 * 4577;

            if (iVar2 < 0)
                iVar2 += 31;

            puVar4.physics1.Y = iVar2 >> 5;
            iVar2 = vTransform.rotation.V22 * 4577;

            if (iVar2 < 0)
                iVar2 += 31;

            puVar4.physics1.Z = iVar2 >> 5;
            GameManager.instance.FUN_30CB0(puVar4, 120);
            cVar2 = puVar4.vCamera;

            if (cVar2 != null)
            {
                cVar2.DAT_84 = new Vector3Int(0, 0, 0);
                cVar2.flags |= 0xc000000;
                oVar1 = GameManager.instance.FUN_30250(GameManager.instance.DAT_1078, puVar4.DAT_DE + 472);
                puVar4.PDAT_74 = oVar1;
            }

            uVar1 = 1;
        }
        else
        {
            FUN_32CF0(hit);
            uVar1 = 0;
        }

        return uVar1;
    }

    //FUN_DF8 (STEELMIL.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        if (arg1 == 8)
            FUN_32B90((uint)arg2);

        return 0;
    }
}
