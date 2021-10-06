using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineEntry : VigObject
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
        VigTransform tVar2;
        uint uVar3;
        VigObject oVar4;
        int iVar5;
        VigObject oVar5;
        Vehicle ppcVar6;
        int unaff_s7;
        VigObject oVar7;
        BufferedBinaryReader pcVar7;
        VigCamera cVar7;
        int iVar8;
        ConfigContainer ccVar8;
        VigTransform auStack80;
        Vector3Int local_30;
        Vector3Int local_28;

        if (hit.collider1.ReadUInt16(0) == 1 && hit.collider1.ReadUInt16(2) == 1)
        {
            if (hit.self.type != 2)
                return 0;

            ppcVar6 = (Vehicle)hit.self;

            if ((ppcVar6.flags & 0x4000000) != 0)
                return 0;

            Utilities.FUN_2CD78(this);
            iVar8 = 0;
            iVar2 = GameManager.instance.FUN_1DD9C();
            GameManager.instance.FUN_1E580(iVar2, GameManager.instance.DAT_C2C, 36, ppcVar6.vTransform.position);
            GameManager.instance.FUN_1E2C8(ppcVar6.DAT_18, 0);
            ppcVar6.state = _VEHICLE_TYPE.Observatory2;
            ppcVar6.tags = 0;
            ppcVar6.flags = ppcVar6.flags & 0xfffffffd | 0x6000020;
            iVar5 = id;
            unaff_s7 = 0; //not in the original code

            if (iVar5 == 81)
            {
                uVar3 = GameManager.FUN_2AC5C();

                if ((uVar3 & 2) != 0)
                    iVar5 = (int)(uVar3 & 1) + 82;
            }

            if (iVar5 == 82)
            {
                unaff_s7 = 515;
                iVar5 = 83;
            }
            else
            {
                if (iVar5 < 83)
                {
                    if (iVar5 == 81)
                    {
                        unaff_s7 = 516;
                        iVar8 = 4;
                        iVar5 = 33;
                        goto LAB_3944;
                    }
                }
                else
                {
                    if (iVar5 == 83)
                    {
                        unaff_s7 = 514;
                        iVar5 = 82;
                        goto LAB_3944;
                    }
                }

                //FUN_15D9C
            }

            LAB_3944:
            oVar4 = GameManager.instance.FUN_31950(iVar5);

            if (oVar4 != null)
            {
                ccVar8 = oVar4.FUN_2C5F4((ushort)(iVar8 - 0x8000 & 0xfffc));

                if (ccVar8 != null)
                {
                    if (iVar5 == 33)
                    {
                        oVar5 = oVar4.child2;

                        while(oVar5 != null)
                        {
                            if (oVar5.id == 1)
                                oVar5.FUN_4DC94();

                            oVar5 = oVar5.child;
                        }
                    }

                    auStack80 = GameManager.instance.FUN_2CEAC(oVar4, ccVar8);
                    ppcVar6.screen = auStack80.position;
                    ppcVar6.vr = ccVar8.v3_2;
                    ppcVar6.vr.y += oVar4.vr.y;
                    pcVar7 = hit.collider1;
                    tVar2 = GameManager.instance.FUN_2CDF4(this);
                    local_28 = new Vector3Int();
                    local_28.x = (pcVar7.ReadInt32(4) + pcVar7.ReadInt32(16)) / 2;
                    local_28.y = (pcVar7.ReadInt32(8) + pcVar7.ReadInt32(20)) / 2;
                    local_28.z = (pcVar7.ReadInt32(12) + pcVar7.ReadInt32(24)) / 2;
                    local_28 = Utilities.FUN_24148(tVar2, local_28);
                    Utilities.FUN_2A168(out local_30, ppcVar6.vTransform.position, local_28);
                    ppcVar6.physics1.X = local_30.x * 143;
                    ppcVar6.physics1.Y = local_30.y * 143;
                    ppcVar6.physics1.Z = local_30.z * 143;
                    ppcVar6.physics2.X = 0;
                    ppcVar6.physics2.Y = 0;
                    ppcVar6.physics2.Z = 0;
                    cVar7 = ppcVar6.vCamera;

                    if (cVar7 != null)
                    {
                        cVar7.DAT_84 = new Vector3Int(0, 0, 0);
                        cVar7.flags |= 0xc000000;
                        oVar7 = GameManager.instance.FUN_30250(GameManager.instance.DAT_1078, unaff_s7);
                        ppcVar6.PDAT_74 = oVar7;
                    }
                }
            }

            GameManager.instance.FUN_30CB0(ppcVar6, 64);
            return 0;
        }

        if (hit.self.type != 0)
            return 0;

        hit.object1.FUN_32B90(hit.self.maxHalfHealth);
        return 0;
    }

    //FUN_378C (ROUTE66.DLL)
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
