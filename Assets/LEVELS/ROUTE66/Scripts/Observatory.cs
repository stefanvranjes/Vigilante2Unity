using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observatory : VigObject
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
        short sVar1;
        short sVar2;
        int iVar3;
        VigTransform tVar3;
        uint uVar4;
        VigObject oVar5;
        ConfigContainer ccVar6;
        uint uVar7;
        bool bVar8;
        int iVar8;
        VigObject oVar8;
        ConfigContainer ccVar8;
        Vehicle ppcVar9;
        VigObject oVar10;
        VigCamera cVar10;
        BufferedBinaryReader pcVar10;
        Vector3Int local_40;
        Vector3Int local_38;
        Vector3Int local_28;
        Vector3Int local_20;
        VigTransform auStack96;

        if (hit.collider1.ReadUInt16(0) == 1)
        {
            sVar2 = hit.collider1.ReadInt16(2);

            if (sVar2 == 3 || sVar2 == 4)
            {
                if (hit.self.type != 2)
                    return 0;

                ppcVar9 = (Vehicle)hit.self;
                Utilities.FUN_2CD78(this);
                iVar3 = GameManager.instance.FUN_1DD9C();
                GameManager.instance.FUN_1E580(iVar3, GameManager.instance.DAT_C2C, 36, ppcVar9.vTransform.position);
                GameManager.instance.FUN_1E2C8(ppcVar9.DAT_18, 0);
                uVar7 = 0x6000020;
                ppcVar9.tags = 0;
                ppcVar9.flags = (ppcVar9.flags & 0xfffffffd | 0x6000020);

                if (sVar2 == 3)
                {
                    ccVar8 = FUN_2C5F4(0x8005);
                    
                    if (ccVar8 != null)
                    {
                        auStack96 = GameManager.instance.FUN_2CEAC(this, ccVar8);
                        ppcVar9.state = _VEHICLE_TYPE.Observatory;
                        ppcVar9.screen = auStack96.position;
                        ppcVar9.vr = ccVar8.v3_2;
                        ppcVar9.vr.y += vr.y;
                        pcVar10 = hit.collider1;
                        tVar3 = GameManager.instance.FUN_2CDF4(this);
                        local_38 = new Vector3Int();
                        local_38.x = (pcVar10.ReadInt32(4) + pcVar10.ReadInt32(16)) / 2;
                        local_38.y = (pcVar10.ReadInt32(8) + pcVar10.ReadInt32(20)) / 2;
                        local_38.z = (pcVar10.ReadInt32(12) + pcVar10.ReadInt32(24)) / 2;
                        local_38 = Utilities.FUN_24148(tVar3, local_38);
                        Utilities.FUN_2A168(out local_40, ppcVar9.vTransform.position, local_38);
                        ppcVar9.physics1.X = local_40.x * 143;
                        ppcVar9.physics1.Y = local_40.y * 143;
                        ppcVar9.physics1.Z = local_40.z * 143;
                        ppcVar9.physics2.X = 0;
                        ppcVar9.physics2.Y = 0;
                        ppcVar9.physics2.Z = 0;
                        cVar10 = ppcVar9.vCamera;

                        if (cVar10 != null)
                        {
                            cVar10.DAT_84 = new Vector3Int(0, 0, 0);
                            cVar10.flags |= 0xc000000;
                            ppcVar9.PDAT_74 = null;
                            ppcVar9.IDAT_74 = 0;
                            ppcVar9.CCDAT_74 = null;
                        }

                        GameManager.instance.FUN_30CB0(ppcVar9, 64);
                        return 0;
                    }
                }

                iVar8 = 0;
                uVar4 = GameManager.FUN_2AC5C();

                if ((uVar4 & 3) != 0)
                    iVar8 = (int)(uVar4 & 3) - 1;

                oVar5 = GameManager.instance.FUN_31950(iVar8 + 81);

                if (oVar5 != null)
                {
                    ccVar6 = oVar5.FUN_2C5F4(0x8000);

                    if (ccVar6 != null)
                    {
                        auStack96 = GameManager.instance.FUN_2CEAC(oVar5, ccVar6);
                        ppcVar9.state = _VEHICLE_TYPE.Observatory2;
                        ppcVar9.screen = auStack96.position;
                        ppcVar9.vr = ccVar6.v3_2;
                        ppcVar9.vr.y += oVar5.vr.y;
                        pcVar10 = hit.collider1;
                        tVar3 = GameManager.instance.FUN_2CDF4(this);
                        local_20 = new Vector3Int();
                        local_20.x = (pcVar10.ReadInt32(4) + pcVar10.ReadInt32(16)) / 2;
                        local_20.y = (pcVar10.ReadInt32(8) + pcVar10.ReadInt32(20)) / 2;
                        local_20.z = (pcVar10.ReadInt32(12) + pcVar10.ReadInt32(24)) / 2;
                        local_20 = Utilities.FUN_24148(tVar3, local_20);
                        Utilities.FUN_2A168(out local_28, ppcVar9.vTransform.position, local_20);
                        ppcVar9.physics1.X = local_28.x * 143;
                        ppcVar9.physics1.Y = local_28.y * 143;
                        ppcVar9.physics1.Z = local_28.z * 143;
                        ppcVar9.physics2.X = 0;
                        ppcVar9.physics2.Y = 0;
                        ppcVar9.physics2.Z = 0;
                        cVar10 = ppcVar9.vCamera;

                        if (cVar10 != null)
                        {
                            cVar10.DAT_84 = new Vector3Int(0, 0, 0);
                            cVar10.flags |= 0xc000000;
                            oVar10 = GameManager.instance.FUN_30250(GameManager.instance.DAT_1078, iVar8 + 513);
                            ppcVar9.PDAT_74 = oVar10;
                        }
                    }
                }

                GameManager.instance.FUN_30CB0(ppcVar9, 64);
                return 0;
            }

            if (sVar2 == 5)
            {
                if (hit.self.type != 2)
                    return 0;

                PDAT_78 = hit.self;
                GameManager.instance.FUN_30CB0(this, 30);
                return 0;
            }
        }

        if ((hit.self.type != 2 || hit.object1 == this) && hit.self.type != 8)
            return 0;

        oVar10 = hit.self;
        iVar3 = 10;

        if (oVar10.type != 2)
            iVar3 = oVar10.maxHalfHealth;

        bVar8 = hit.object1.FUN_32B90((uint)iVar3);

        if (bVar8)
        {
            oVar8 = Utilities.FUN_2CD78(hit.object1);

            if (oVar8 == null)
                Ant.FUN_134C();
        }

        return 0;
    }

    //FUN_31D0 (ROUTE66.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        bool bVar8;
        VigObject oVar8;
        VigTransform tVar8;

        if (arg1 == 2)
        {
            PDAT_78 = null;
            return 0;
        }

        if (arg1 < 3)
        {
            if (arg1 != 1)
                return 0;

            oVar8 = child2;

            while(oVar8 != null)
            {
                if ((ushort)oVar8.id - 1 < 2)
                    oVar8.maxHalfHealth = 30;
                else
                {
                    if (oVar8.id != 0)
                        oVar8.type = 3;
                }

                oVar8 = oVar8.child;
            }

            return 0;
        }

        if (arg1 != 8)
            return 0;

        bVar8 = FUN_32B90((uint)arg2);

        if (bVar8)
        {
            oVar8 = Utilities.FUN_2CD78(this);

            if (oVar8 == null)
                Ant.FUN_134C();
        }

        return 0;
    }
}
