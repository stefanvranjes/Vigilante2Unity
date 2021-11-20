using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warehouse : VigObject
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
        ushort uVar2;
        ConfigContainer ccVar3;
        VigCamera cVar3;
        int iVar4;
        VigObject oVar4;
        Vehicle puVar5;
        uint uVar6;
        VigTransform auStack40;
        HitDetection auStack96;

        if (hit.collider1.ReadUInt16(0) == 1)
        {
            uVar2 = hit.collider1.ReadUInt16(2);
            uVar6 = uVar2;

            if (uVar6 != 0 && hit.self.type == 2)
            {
                ccVar3 = FUN_2C5F4((ushort)(uVar6 - 0x8000 & 0xffff));

                if (ccVar3 != null)
                {
                    puVar5 = (Vehicle)hit.self;
                    iVar4 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E580(iVar4, GameManager.instance.DAT_C2C, 36, puVar5.vTransform.position);
                    GameManager.instance.FUN_1E2C8(puVar5.DAT_18, 0);
                    puVar5.state = _VEHICLE_TYPE.Warehouse;
                    puVar5.tags = 0;
                    puVar5.flags = puVar5.flags & 0xfffffffd | 0x6000020;
                    auStack40 = GameManager.instance.FUN_2CEAC(this, ccVar3);
                    puVar5.screen = auStack40.position;
                    puVar5.vr = ccVar3.v3_2;
                    puVar5.DAT_DE = (byte)uVar2;

                    if (uVar6 - 2 < 3)
                        puVar5.FUN_3E32C(_WHEELS.Sea, 500);

                    auStack96 = new HitDetection(null);
                    GameManager.instance.FUN_2FB70(this, hit, auStack96);
                    puVar5.physics1.X = auStack96.normal1.x * -143;
                    puVar5.physics1.Y = auStack96.normal1.y * -143;
                    puVar5.physics1.Z = auStack96.normal1.z * -143;
                    puVar5.physics2.X = 0;
                    puVar5.physics2.Y = 0;
                    puVar5.physics2.Z = 0;
                    cVar3 = puVar5.vCamera;

                    if (cVar3 != null)
                    {
                        cVar3.DAT_84 = new Vector3Int(0, 0, 0);
                        cVar3.flags |= 0xc000000;
                        oVar4 = GameManager.instance.FUN_30250(GameManager.instance.DAT_1078, (int)uVar6 + 0x1ff);
                        puVar5.PDAT_74 = oVar4;
                    }

                    GameManager.instance.FUN_30CB0(puVar5, 64);
                    return 0;
                }
            }
        }

        if (hit.self.type != 8)
            return 0;

        FUN_32B90(hit.self.maxHalfHealth);
        return 0;
    }

    //FUN_CB4 (HARBOR.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        if (arg1 != 8)
            return 0;

        FUN_32B90((uint)arg2);
        return 0;
    }
}
