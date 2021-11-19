using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mausoleum : VigObject
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
        ConfigContainer ccVar4;
        int iVar5;
        VigTransform tVar5;
        VigObject oVar6;
        BufferedBinaryReader pcVar6;
        VigCamera cVar6;
        Vehicle ppcVar7;
        VigTransform auStack128;
        Vector3Int local_20;
        Vector3Int local_28;

        if (physics1.X != 0)
        {
            if (hit.self.type == 2)
                hit.object1.FUN_32B90((uint)physics1.X);

            physics1.X = 0;
        }

        if (hit.collider1.ReadUInt16(0) != 1 || hit.collider1.ReadUInt16(2) == 0)
        {
            if ((hit.self.type != 2 || hit.object1 == this) && hit.self.type != 8)
                return 0;

            hit.object1.FUN_32B90(hit.self.maxHalfHealth);
            return 0;
        }

        if (tags != 1)
            return 0;

        if (hit.self.type != 2)
            return 0;

        ccVar4 = DAT_84.FUN_2C5F4(0x8002);

        if (ccVar4 == null)
            return 0;

        ppcVar7 = (Vehicle)hit.self;
        iVar5 = GameManager.instance.FUN_1DD9C();
        GameManager.instance.FUN_1E580(iVar5, GameManager.instance.DAT_C2C, 36, ppcVar7.vTransform.position);
        GameManager.instance.FUN_1E2C8(ppcVar7.DAT_18, 0);
        ppcVar7.state = _VEHICLE_TYPE.Mansion;
        ppcVar7.tags = 0;
        ppcVar7.flags = ppcVar7.flags & 0xfffffffd | 0x6000020;
        auStack128 = GameManager.instance.FUN_2CEAC(DAT_84, ccVar4);
        ppcVar7.screen = auStack128.position;
        ppcVar7.vr = ccVar4.v3_2;
        ppcVar7.vr.y += DAT_84.vr.y;
        pcVar6 = hit.collider1;
        tVar5 = GameManager.instance.FUN_2CDF4(this);
        local_20 = new Vector3Int();
        local_20.x = (pcVar6.ReadInt32(4) + pcVar6.ReadInt32(16)) / 2;
        local_20.y = (pcVar6.ReadInt32(8) + pcVar6.ReadInt32(20)) / 2;
        local_20.z = (pcVar6.ReadInt32(12) + pcVar6.ReadInt32(24)) / 2;
        local_20 = Utilities.FUN_24148(tVar5, local_20);
        Utilities.FUN_2A168(out local_28, ppcVar7.vTransform.position, local_20);
        ppcVar7.physics1.X = local_28.x * 143;
        ppcVar7.physics1.Y = local_28.y * 143;
        ppcVar7.physics1.Z = local_28.z * 143;
        ppcVar7.physics2.X = 0;
        ppcVar7.physics2.Y = 0;
        ppcVar7.physics2.Z = 0;
        cVar6 = ppcVar7.vCamera;

        if (cVar6 != null)
        {
            cVar6.DAT_84 = new Vector3Int(0, 0, 0);
            cVar6.flags |= 0xc000000;
            iVar5 = 2000;

            if (GetType() == typeof(Mansion))
                iVar5 = 2001;

            oVar6 = GameManager.instance.FUN_30250(GameManager.instance.DAT_1078, iVar5);
            ppcVar7.PDAT_74 = oVar6;
        }

        GameManager.instance.FUN_30CB0(ppcVar7, 58);

        if (-1 < ppcVar7.id)
            return 0;

        UIManager.instance.FUN_4E3A8(new Color32(0xff, 0xff, 0xff, 2));
        return 0;
    }

    public VigObject[] DAT_88 = new VigObject[2]; //0x88

    //FUN_2A80 (BAYOU.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        sbyte sVar1;
        ConfigContainer ccVar3;
        int iVar4;
        int iVar5;
        VigObject pcVar6;
        Type tVar6;

        switch (arg1)
        {
            case 1:
                pcVar6 = child2;

                while(pcVar6 != null)
                {
                    if (pcVar6.id == 1)
                    {
                        pcVar6.maxHalfHealth = 1;
                        break;
                    }

                    pcVar6 = pcVar6.child2;
                }

                tags = -1;
                GameManager.instance.FUN_30CB0(this, 2);
                break;
            case 2:
                sVar1 = tags;

                if (sVar1 == 0)
                {
                    tags = 1;
                    GameManager.instance.FUN_30CB0(this, 600);
                    iVar4 = 0;

                    do
                    {
                        ccVar3 = FUN_2C5F4((ushort)(iVar4 - 0x8000 & 0xffff));

                        if (ccVar3 != null)
                        {
                            pcVar6 = vData.ini.FUN_2C17C(19, typeof(VigObject), 8);

                            if (pcVar6 != null)
                            {
                                pcVar6.vTransform = GameManager.instance.FUN_2CEAC(this, ccVar3);
                                pcVar6.flags = 0x34;
                                pcVar6.FUN_305FC();
                                DAT_88[iVar4] = pcVar6;
                                iVar5 = GameManager.instance.FUN_1DD9C();
                                GameManager.instance.FUN_1E580(iVar5, vData.sndList, 2, pcVar6.vTransform.position);
                            }
                        }

                        iVar4++;
                    } while (iVar4 < 2);

                    FUN_2E868(1);
                    return 0;
                }

                if (0 < sVar1)
                {
                    if (sVar1 != 1)
                        return 0;

                    iVar4 = 0;

                    do
                    {
                        if (DAT_88[iVar4] != null)
                        {
                            GameManager.instance.FUN_309A0(DAT_88[iVar4]);
                            DAT_88[iVar4] = null;
                        }

                        iVar4++;
                    } while (iVar4 < 2);

                    iVar4 = (int)GameManager.FUN_2AC5C();
                    GameManager.instance.FUN_30CB0(DAT_84, (iVar4 * 300 >> 15) + 300);
                    tags = 0;
                    FUN_2E900(1);
                    return 0;
                }

                if (sVar1 != -1)
                    return 0;

                if (DAT_84 == null)
                {
                    tVar6 = typeof(Mansion);

                    if (GetType() == typeof(Mansion))
                        tVar6 = typeof(Mausoleum);

                    pcVar6 = GameManager.instance.FUN_31994(tVar6);
                    DAT_84 = pcVar6;

                    if (pcVar6 != null)
                    {
                        pcVar6.DAT_84 = this;
                        iVar4 = (int)GameManager.FUN_2AC5C();
                        GameManager.instance.FUN_30CB0(this, (iVar4 * 300 >> 15) + 300);
                    }
                }

                tags = 0;
                goto LAB_2FDC;
            case 8:
                physics1.X = arg2;
                break;
            case 9:
                if (arg2 == 0)
                    return 0;

                if (tags != 0)
                    return 0;

                LAB_2FDC:
                FUN_2E900(1);
                return 0;
        }

        return 0;
    }
}
