using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTunnel2 : VigObject
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
        int iVar1;
        uint uVar1;
        int iVar2;
        VigObject oVar2;
        int iVar3;
        VigCamera pcVar3;
        int iVar4;
        int iVar5;
        int iVar6;
        VigObject ppcVar7;
        int iVar8;
        Vehicle vVar9;
        Vector3Int local_50;
        Vector3Int local_40;
        Vector3Int local_30;

        uVar1 = 0;
        ppcVar7 = hit.self;

        if (hit.collider1.ReadUInt16(0) == 1 && ppcVar7.type == 2 && 
            (ppcVar7.flags & 0x4000000) == 0)
        {
            local_50 = new Vector3Int();
            local_50.x = vTransform.position.x - ppcVar7.vTransform.position.x;
            local_50.y = vTransform.position.y - ppcVar7.vTransform.position.y;
            local_50.z = vTransform.position.z - ppcVar7.vTransform.position.z;
            iVar2 = Utilities.FUN_29FC8(local_50, out local_30);

            if (iVar2 < 0xc0001)
            {
                local_40 = new Vector3Int();
                local_40.z = ((0xc0000 - iVar2) / 0x180) * 0x80;
                GameManager.instance.FUN_30CB0(this, 300);
                local_40.x = local_30.x * local_40.z;

                if (local_40.x < 0)
                    local_40.x += 4095;

                local_40.y = local_30.y * local_40.z;
                local_40.x >>= 12;

                if (local_40.y < 0)
                    local_40.y += 4095;

                local_40.z = local_30.z * local_40.z;
                local_40.y >>= 12;

                if (local_40.z < 0)
                    local_40.z += 4095;

                local_40.z >>= 12;
                iVar4 = ppcVar7.physics1.X;
                iVar3 = iVar4;

                if (iVar4 < 0)
                    iVar3 = iVar4 + 63;

                iVar8 = ppcVar7.physics1.Y;
                ppcVar7.physics1.X = iVar4 + (local_40.x - (iVar3 >> 6));
                iVar3 = iVar8;

                if (iVar8 < 0)
                    iVar3 = iVar8 + 63;

                iVar4 = ppcVar7.physics1.Z;
                ppcVar7.physics1.Y = iVar8 + (local_40.y - (iVar3 >> 6));
                iVar3 = iVar4;

                if (iVar4 < 0)
                    iVar3 = iVar4 + 63;

                ppcVar7.physics1.Z = iVar4 + (local_40.z - (iVar3 >> 6));
                iVar2 = local_50.x;

                if (local_50.x < 0)
                    iVar2 = -local_50.x;

                uVar1 = 0;
                vVar9 = (Vehicle)ppcVar7;

                if (iVar2 < 0x10001)
                {
                    if (vVar9.vCamera != null)
                    {
                        oVar2 = GameManager.instance.FUN_30250(GameManager.instance.DAT_1078, id + 418);
                        vVar9.vCamera.flags |= 0xc000000;

                        if (oVar2 != null)
                        {
                            pcVar3 = vVar9.vCamera;
                            pcVar3.screen = oVar2.vTransform.position;
                        }

                        GameManager.instance.FUN_30CB0(vVar9.vCamera, 60);
                    }

                    iVar1 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E580(iVar1, GameManager.instance.DAT_C2C, 7, vTransform.position);
                    iVar1 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E580(iVar1, GameManager.instance.DAT_C2C, 37, vTransform.position);
                    vVar9.state = _VEHICLE_TYPE.WindTunnel;
                    vVar9.physics2.X = 0;
                    vVar9.flags = vVar9.flags & 0xfffffffd | 0x6000020;
                    vVar9.physics2.Y = 0;
                    vVar9.physics2.Z = 0xaa80;
                    iVar2 = vTransform.rotation.V02 * 24414;

                    if (iVar2 < 0)
                        iVar2 += 4095;

                    vVar9.physics1.X = (iVar2 >> 12) << 7;
                    iVar5 = vTransform.rotation.V12 * 24414;
                    iVar2 = iVar5 - 762;

                    if (iVar2 < 0)
                        iVar2 = iVar5 + 3333;

                    vVar9.physics1.Y = (iVar2 >> 12) << 7;
                    iVar2 = vTransform.rotation.V22 * 24414;

                    if (iVar2 < 0)
                        iVar2 += 4095;

                    vVar9.physics1.Z = (iVar2 >> 12) << 7;
                    GameManager.instance.FUN_30CB0(vVar9, 18);
                    vVar9.vTransform.position = vTransform.position;
                    iVar1 = -15;

                    if (vVar9.id < 0)
                    {
                        UIManager.instance.FUN_4E338(new Color32(0x80, 0x80, 0x80, 8));

                        if (vVar9.id < 0)
                            iVar1 = -50;
                    }

                    vVar9.FUN_39DCC(iVar1, LaunchRocket3.DAT_D4, false);
                    iVar6 = vVar9.id;

                    if (0 < iVar6)
                    {
                        if ((Launch.instance.DAT_5874 >> (iVar6 & 31) & 1) != 0)
                            return 0;

                        Launch.instance.DAT_5874 |= (byte)(1 << (iVar6 & 31));
                        Launch.instance.DAT_5875++;

                        if (Launch.instance.DAT_5875 != 2)
                            return 0;

                        //FUN_326B0
                    }

                    uVar1 = 0;
                }
            }
        }

        return uVar1;
    }

    //FUN_293C (LAUNCH.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        uint uVar1;

        if (arg1 == 2)
        {
            FUN_30BA8();
            GameManager.instance.FUN_1DE78(DAT_18);
            GameManager.instance.FUN_309A0(this);
            uVar1 = 0xffffffff;
        }
        else
        {
            if (arg1 < 3)
            {
                uVar1 = 0;

                if (arg1 == 0 && DAT_18 != 0 && arg2 != 0)
                {
                    uVar1 = GameManager.instance.FUN_1E7A8(vTransform.position);
                    GameManager.instance.FUN_1E2C8(DAT_18, uVar1);
                    uVar1 = 0;
                }
            }
            else
                uVar1 = 0;
        }

        return uVar1;
    }
}
