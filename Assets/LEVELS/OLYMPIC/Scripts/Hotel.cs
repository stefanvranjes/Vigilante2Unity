using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotel : VigObject
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
        short sVar2;
        VigObject pcVar3;
        int iVar4;
        VigObject oVar5;
        ConfigContainer ccVar5;
        VigObject oVar6;
        VigCamera pcVar6;
        Vehicle ppcVar7;
        Hotel2 hVar7;
        uint uVar8;
        VigTransform auStack48;
        HitDetection auStack104;

        if (hit.collider1.ReadUInt16(0) == 1)
        {
            uVar8 = hit.collider1.ReadUInt16(2);

            if (uVar8 == 1 || uVar8 == 2)
            {
                if (hit.self.type == 2)
                {
                    ccVar5 = FUN_2C5F4((ushort)(uVar8 - 0x8000 & 0xffff));

                    if (ccVar5 != null)
                    {
                        ppcVar7 = (Vehicle)hit.self;
                        pcVar3 = Utilities.FUN_2CD78(this);

                        if (pcVar3 == null)
                            pcVar3 = this;

                        iVar4 = GameManager.instance.FUN_1DD9C();
                        GameManager.instance.FUN_1E580(iVar4, GameManager.instance.DAT_C2C, 36, ppcVar7.vTransform.position);
                        GameManager.instance.FUN_1E2C8(ppcVar7.DAT_18, 0);
                        ppcVar7.state = _VEHICLE_TYPE.Hotel;
                        ppcVar7.tags = 0;
                        ppcVar7.flags = ppcVar7.flags & 0xfffffffd | 0x6000020;
                        auStack48 = GameManager.instance.FUN_2CEAC(this, ccVar5);
                        ppcVar7.screen = auStack48.position;
                        ppcVar7.vr = ccVar5.v3_2;
                        ppcVar7.vr.y += vr.y;
                        auStack104 = new HitDetection(null);
                        GameManager.instance.FUN_2FB70(this, hit, auStack104);
                        ppcVar7.physics1.X = auStack104.normal1.x * -143;
                        ppcVar7.physics1.Y = auStack104.normal1.y * -143;
                        ppcVar7.physics1.Z = auStack104.normal1.z * -143;
                        ppcVar7.physics2.X = 0;
                        ppcVar7.physics2.Y = 0;
                        ppcVar7.physics2.Z = 0;
                        pcVar6 = ppcVar7.vCamera;

                        if (pcVar6 != null)
                        {
                            pcVar6.DAT_84 = new Vector3Int(0, 0, 0);
                            pcVar6.flags |= 0xc000000;
                            oVar6 = GameManager.instance.FUN_30250(GameManager.instance.DAT_1078, (int)(uVar8 + 0x1ff));
                            ppcVar7.PDAT_74 = oVar6;
                        }

                        GameManager.instance.FUN_30CB0(ppcVar7, 64);
                        sVar2 = 1;

                        if (uVar8 == 1)
                            sVar2 = 2;

                        oVar5 = pcVar3.child2;

                        while (true)
                        {
                            if (oVar5 == null)
                                return 0;

                            if (oVar5.id == sVar2) break;

                            oVar5 = oVar5.child;
                        }

                        GameObject obj = new GameObject();
                        hVar7 = obj.AddComponent<Hotel2>();
                        hVar7.type = 0xff;
                        hVar7.id = sVar2;
                        hVar7.child = pcVar3;
                        GameManager.instance.FUN_30CB0(hVar7, 90);
                        return 0;
                    }
                }
            }
        }

        if (hit.self.type != 8)
            return 0;

        hit.object1.FUN_32B90(hit.self.maxHalfHealth);
        return 0;
    }

    //FUN_22B8 (OLYMPIC.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        VigObject oVar5;

        if (arg1 < 4)
        {
            if (arg1 != 1)
                return 0;

            oVar5 = child2;

            while(oVar5 != null)
            {
                if (oVar5.id - 1 < 2)
                    oVar5.maxHalfHealth = 30;

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
