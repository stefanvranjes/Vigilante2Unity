using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchEntry : VigObject
{
    protected override void Update()
    {
        base.Update();
    }

    protected override void Start()
    {
        base.Start();
    }

    public override uint OnCollision(HitDetection hit)
    {
        ushort uVar2;
        int iVar3;
        VigObject oVar3;
        VigObject oVar4;
        ConfigContainer ccVar4;
        ConfigContainer ccVar5;
        VigObject ppcVar6;
        Vehicle vVar6;
        VigCamera pcVar7;
        VigObject oVar8;
        HitDetection auStack96;
        VigTransform auStack40;

        ppcVar6 = hit.self;

        if (ppcVar6.type != 2 || (ppcVar6.flags & 0x4000000) != 0 || 
            hit.collider1.ReadUInt16(0) != 1 || hit.collider1.ReadUInt16(2) == 0)
        {
            if (hit.self.type != 8)
                return 0;

            FUN_32B90(hit.self.maxHalfHealth);
            return 0;
        }

        vVar6 = (Vehicle)ppcVar6;
        iVar3 = GameManager.instance.FUN_1DD9C();
        GameManager.instance.FUN_1E580(iVar3, GameManager.instance.DAT_C2C, 36, ppcVar6.vTransform.position);
        auStack96 = new HitDetection(null);
        GameManager.instance.FUN_2FB70(this, hit, auStack96);
        GameManager.instance.FUN_1E2C8(ppcVar6.DAT_18, 0);
        ppcVar6.tags = 0;
        ppcVar6.flags = ppcVar6.flags & 0xfffffffd | 0x6000020;
        ppcVar6.physics1.X = auStack96.normal1.x * -143;
        ppcVar6.physics1.Y = auStack96.normal1.y * -143;
        ppcVar6.physics1.Z = auStack96.normal1.z * -143;
        ppcVar6.physics2.X = 0;
        ppcVar6.physics2.Y = 0;
        ppcVar6.physics2.Z = 0;
        GameManager.instance.FUN_30CB0(ppcVar6, 64);

        if (tags == 0)
        {
            oVar4 = GameManager.instance.FUN_318D0(113);

            if (oVar4 != null)
            {
                ccVar5 = oVar4.FUN_2C5F4(0x8000);

                if (ccVar5 != null)
                {
                    auStack40 = GameManager.instance.FUN_2CEAC(oVar4, ccVar5);
                    uVar2 = 2;
                    ppcVar6.screen = auStack40.position;
                    ppcVar6.screen.y += 0x50000;
                    ppcVar6.vr = ccVar5.v3_2;
                    vVar6.state = _VEHICLE_TYPE.LaunchEntry;
                    GameManager.instance.FUN_30CB0(this, 0);
                    goto LAB_566C;
                }
            }
        }

        oVar3 = GameManager.instance.FUN_318D0(49);
        uVar2 = (ushort)GameManager.FUN_2AC5C();
        uVar2 &= 1;
        ccVar4 = oVar3.FUN_2C5F4((ushort)(uVar2 + 0x8000));
        auStack40 = GameManager.instance.FUN_2CEAC(oVar3, ccVar4);
        ppcVar6.screen = auStack40.position;
        ppcVar6.vr = ccVar4.v3_2;
        vVar6.state = _VEHICLE_TYPE.LaunchEntry2;
        LAB_566C:
        pcVar7 = vVar6.vCamera;

        if (pcVar7 == null)
            return 0;

        pcVar7.DAT_84 = new Vector3Int(0, 0, 0);
        pcVar7.flags |= 0xc000000;
        oVar8 = GameManager.instance.FUN_30250(GameManager.instance.DAT_1078, uVar2 + 513);
        ppcVar6.PDAT_74 = oVar8;
        return 0;
    }

    //FUN_5324 (LAUNCH.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        int iVar4;
        VigObject oVar4;

        if (arg1 == 2)
        {
            iVar4 = (int)GameManager.FUN_2AC5C();
            GameManager.instance.FUN_30CB0(this, (iVar4 * 600 >> 15) + 600);

            if (tags == 1)
            {
                oVar4 = GameManager.instance.FUN_302A8(GameManager.instance.worldObjs, typeof(LaunchVehicle));

                if (oVar4 != null && oVar4.vTransform.position.z < 0x4b73001)
                    return 0;
            }

            GameManager.instance.FUN_2C0A0(this);
            GameManager.instance.FUN_2FEE8(this, (ushort)(DAT_4A + tags * 2));
            tags = (sbyte)(1 - tags);
            return 0;
        }

        if (arg1 < 3)
        {
            if (arg1 != 1)
                return 0;

            flags &= 0xfffffffb;
            GameManager.instance.FUN_30CB0(this, 600);
            GameManager.instance.FUN_2C0A0(this);
            GameManager.instance.FUN_2FEE8(this, DAT_4A);
            return 0;
        }

        if (arg1 != 8)
            return 0;

        FUN_32B90((uint)arg2);
        return 0;
    }
}
