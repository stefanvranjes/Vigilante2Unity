using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferBooth : VigObject
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
        VigObject oVar2;
        uint uVar3;
        ConfigContainer ccVar4;
        int iVar5;
        VigObject oVar5;
        VigCamera cVar5;
        int unaff_s5;
        Vehicle puVar6;
        VigTransform auStack48;
        HitDetection auStack104;

        if (hit.collider1.ReadUInt16(0) == 1 && hit.collider1.ReadUInt16(2) == 1)
        {
            if (hit.self.type != 2)
                return 0;

            puVar6 = (Vehicle)hit.self;

            if ((puVar6.flags & 0x6000000) != 0)
                return 0;

            Utilities.FUN_2CD78(this);
            iVar2 = GameManager.instance.FUN_1DD9C();
            GameManager.instance.FUN_1E580(iVar2, GameManager.instance.DAT_C2C, 36, puVar6.vTransform.position);
            GameManager.instance.FUN_1E2C8(puVar6.DAT_18, 0);
            puVar6.state = _VEHICLE_TYPE.TransferBooth;
            puVar6.tags = 0;
            puVar6.flags = puVar6.flags & 0xfffffffd | 0x6000020;
            iVar5 = id - 49;
            uVar3 = GameManager.FUN_2AC5C();
            uVar3 &= 3;
            unaff_s5 = 0; //not in the original code

            if ((int)uVar3 < 2)
            {
                iVar5 = (int)(uVar3 ^ 1) + 49;
                unaff_s5 = (int)(uVar3 ^ 1) + 0x200;
            }
            else
            {
                if ((int)uVar3 < 4)
                {
                    iVar5 = (int)uVar3 + 0x200;
                    unaff_s5 = iVar5;
                }
            }

            oVar5 = GameManager.instance.FUN_31950(iVar5);

            if (oVar5 != null)
            {
                ccVar4 = oVar5.FUN_2C5F4(0x8000);

                if (ccVar4 != null)
                {
                    auStack48 = GameManager.instance.FUN_2CEAC(oVar5, ccVar4);
                    puVar6.screen = auStack48.position;
                    puVar6.vr = ccVar4.v3_2;
                    puVar6.vr.y += oVar5.vr.y;
                    auStack104 = new HitDetection(null);
                    GameManager.instance.FUN_2FB70(this, hit, auStack104);
                    puVar6.physics1.X = auStack104.normal1.x * -143;
                    puVar6.physics1.Y = auStack104.normal1.y * -143;
                    puVar6.physics1.Z = auStack104.normal1.z * -143;
                    puVar6.physics2.X = 0;
                    puVar6.physics2.Y = 0;
                    puVar6.physics2.Z = 0;
                    cVar5 = puVar6.vCamera;

                    if (cVar5 != null)
                    {
                        cVar5.DAT_84 = new Vector3Int(0, 0, 0);
                        cVar5.flags |= 0xc000000;
                        oVar2 = GameManager.instance.FUN_30250(GameManager.instance.DAT_1078, unaff_s5);
                        puVar6.PDAT_74 = oVar2;
                    }
                }
            }

            GameManager.instance.FUN_30CB0(puVar6, 64);
            return 0;
        }

        if (hit.self.type != 8)
            return 0;

        hit.object1.FUN_32B90(hit.self.maxHalfHealth);
        return 0;
    }

    public override uint UpdateW(int arg1, int arg2)
    {
        VigObject oVar5;

        if (arg1 < 4)
        {
            if (arg1 != 1)
                return 0;

            oVar5 = child2;

            while (oVar5 != null)
            {
                if (oVar5.id != 0)
                    oVar5.type = 3;

                oVar5 = oVar5.child;
            }

            return 0;
        }

        if (arg1 != 8)
            return 0;

        FUN_32B90((uint)arg2);
        return 0;
    }
}
