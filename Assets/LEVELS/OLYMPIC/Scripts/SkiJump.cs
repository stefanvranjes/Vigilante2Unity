using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkiJump : VigObject
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
        int iVar3;
        ConfigContainer ccVar4;
        VigTransform tVar4;
        VigObject oVar5;
        VigCamera pcVar5;
        Vehicle ppcVar6;
        VigTransform auStack40;
        HitDetection auStack96;

        if (hit.collider1.ReadUInt16(0) == 1)
        {
            sVar2 = hit.collider1.ReadInt16(2);

            if (sVar2 == 1)
            {
                if (hit.self.type != 2)
                    return 0;

                ccVar4 = FUN_2C5F4(0x8000);

                if (ccVar4 == null)
                    return 0;

                ppcVar6 = (Vehicle)hit.self;
                Utilities.FUN_2CD78(this);
                iVar3 = GameManager.instance.FUN_1DD9C();
                GameManager.instance.FUN_1E580(iVar3, GameManager.instance.DAT_C2C, 36, ppcVar6.vTransform.position);
                GameManager.instance.FUN_1E2C8(ppcVar6.DAT_18, 0);
                ppcVar6.state = _VEHICLE_TYPE.SkiJump;
                ppcVar6.tags = 0;
                ppcVar6.flags = ppcVar6.flags & 0xfffffffd | 0x6000020;
                auStack40 = GameManager.instance.FUN_2CEAC(this, ccVar4);
                ppcVar6.screen = auStack40.position;
                ppcVar6.vr = ccVar4.v3_2;
                ppcVar6.vr.y += vr.y;
                auStack96 = new HitDetection(null);
                GameManager.instance.FUN_2FB70(this, hit, auStack96);
                ppcVar6.physics1.X = auStack96.normal1.x * -143;
                ppcVar6.physics1.Y = auStack96.normal1.y * -143;
                ppcVar6.physics1.Z = auStack96.normal1.z * -143;
                ppcVar6.physics2.X = 0;
                ppcVar6.physics2.Y = 0;
                ppcVar6.physics2.Z = 0;
                pcVar5 = ppcVar6.vCamera;

                if (pcVar5 != null)
                {
                    pcVar5.DAT_84 = new Vector3Int(0, 0, 0);
                    pcVar5.flags |= 0xc000000;
                    oVar5 = GameManager.instance.FUN_30250(GameManager.instance.DAT_1078, 514);
                    ppcVar6.PDAT_74 = oVar5;
                }

                GameManager.instance.FUN_30CB0(ppcVar6, 64);
                return 0;
            }

            if (sVar2 == 2)
            {
                oVar5 = hit.self;

                if (oVar5.type != 2)
                    return 0;

                tVar4 = GameManager.instance.FUN_2CDF4(hit.object1);
                oVar5.physics1.X += tVar4.rotation.V02 * 4;
                oVar5.physics1.Y += tVar4.rotation.V12 * 4;
                oVar5.physics1.Z += tVar4.rotation.V22 * 4;
                return 0;
            }

            if (sVar2 == 3)
            {
                if (hit.self.type != 2)
                    return 0;

                ppcVar6 = (Vehicle)hit.self;
                tVar4 = GameManager.instance.FUN_2CDF4(hit.object1);
                ppcVar6.physics1.X += tVar4.rotation.V02 * 32;
                ppcVar6.physics1.Y += tVar4.rotation.V12 * 32;
                ppcVar6.physics1.Z += tVar4.rotation.V22 * 32;

                if (ppcVar6.state != _VEHICLE_TYPE.Vehicle)
                    return 0;

                ppcVar6.state = _VEHICLE_TYPE.SkiJump2;
                iVar3 = GameManager.instance.FUN_1DD9C();
                GameManager.instance.FUN_1E580(iVar3, vData.sndList, 0, ppcVar6.vTransform.position);
                return 0;
            }
        }

        if ((hit.self.type != 2 || hit.object1 == this) && hit.self.type != 8)
            return 0;

        oVar5 = hit.self;
        iVar3 = 10;

        if (oVar5.type != 2)
            iVar3 = oVar5.maxHalfHealth;

        hit.object1.FUN_32B90((uint)iVar3);
        return 0;
    }

    //FUN_2C9C (OLYMPIC.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        VigObject oVar4;

        if (arg1 < 4)
        {
            if (arg1 != 1)
                return 0;

            oVar4 = child2;

            while (oVar4 != null)
            {
                if (oVar4.id == 1)
                    oVar4.maxHalfHealth = 30;
                else
                {
                    if ((ushort)((ushort)oVar4.id - 2) < 2U)
                        oVar4.type = 3;
                }

                oVar4 = oVar4.child;
            }

            return 0;
        }

        if (arg1 != 8)
            return 0;

        FUN_32B90((uint)arg2);
        return 0;
    }
}
