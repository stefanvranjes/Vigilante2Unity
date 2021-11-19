using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : VigObject
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
        uint uVar3;
        int iVar4;
        Vehicle vVar4;
        VigTransform tVar6;
        Throwaway puVar6;
        uint uVar7;
        int iVar11;
        HitDetection auStack88;
        Vector3Int local_88;
        Vector3Int local_20;

        flags |= 0x20;
        auStack88 = new HitDetection(null);
        GameManager.instance.FUN_2FB70(this, hit, auStack88);
        local_88 = new Vector3Int
            (auStack88.position.x / 2, auStack88.position.y / 2, auStack88.position.z / 2);
        //iVar3 = GameManager.instance.FUN_1DD9C();
        //GameManager.instance.FUN_1E580(iVar3, vData.sndList, 2, vTransform.position);
        LevelManager.instance.FUN_4DE54(local_88, 144);
        flags |= 0x20;
        uVar3 = 0;

        if (hit.self.type == 2)
        {
            tVar6 = GameManager.instance.FUN_2CDF4(this);
            iVar11 = 0;
            vVar4 = (Vehicle)hit.self;
            local_20 = new Vector3Int();
            local_20.x = tVar6.rotation.V02 << 6;
            local_20.y = tVar6.rotation.V12 << 6;
            local_20.z = tVar6.rotation.V22 << 6;
            local_88 = Utilities.FUN_24148(vVar4.vTransform, local_88);
            vVar4.FUN_2B370(local_20, local_88);
            UIManager.instance.FUN_4E414(hit.self.vTransform.position, new Color32(0x00, 0x80, 0x00, 8));
            vVar4.vTransform.position.y -= 0x1400;

            do
            {
                puVar6 = LevelManager.instance.xobfList[19].ini.FUN_2C17C(47, typeof(Throwaway), 8) as Throwaway;
                puVar6.physics1.M0 = 0;
                puVar6.physics1.M1 = 0;
                puVar6.physics1.M2 = 0;
                uVar7 = GameManager.FUN_2AC5C();
                puVar6.physics1.Z = (int)(uVar7 & 0xfff) - 0x800;
                iVar4 = (int)GameManager.FUN_2AC5C();

                if (iVar4 < 0)
                    iVar4 += 15;

                puVar6.physics1.W = -(iVar4 >> 4);
                uVar7 = GameManager.FUN_2AC5C();
                puVar6.physics2.X = (int)(uVar7 & 0xfff) - 0x800;
                puVar6.type = 7;
                puVar6.flags |= 0x1b4;
                sVar2 = id;
                iVar11++;
                puVar6.state = _THROWAWAY_TYPE.Type3;
                puVar6.vTransform = GameManager.FUN_2A39C();
                puVar6.vTransform.position = local_88;
                puVar6.FUN_2D1DC();
                puVar6.DAT_87 = 1;
                puVar6.FUN_305FC();
            } while (iVar11 < 12);

            uVar3 = 0;
        }

        return uVar3;
    }

    //FUN_15C8 (ROUTE66.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        short sVar1;
        short sVar2;
        ushort uVar2;
        uint uVar3;
        int iVar4;
        ConfigContainer ccVar4;
        Laser ppcVar5;
        Vector3Int local_88;

        if (arg1 < 4)
        {
            uVar3 = 0;

            if (arg1 == 0)
            {
                vTransform.position.x += physics1.Z;
                vTransform.position.y += physics1.W;
                vTransform.position.z += physics2.X;
                screen = vTransform.position;
                sVar1 = physics2.M3;
                physics2.M3 = (short)(sVar1 - 1);
                uVar3 = 0;

                if (sVar1 == 1)
                {
                    if (physics2.M2 == 0)
                        uVar3 = 0;
                    else
                    {
                        ccVar4 = FUN_2C5F4(0x8000);
                        local_88 = Utilities.FUN_24148(vTransform, ccVar4.v3_1);
                        iVar4 = GameManager.instance.terrain.FUN_1B750((uint)local_88.x, (uint)local_88.z);

                        if (local_88.y < iVar4)
                        {
                            ppcVar5 = vData.ini.FUN_2C17C((ushort)DAT_1A, typeof(Laser), 8) as Laser;
                            sVar2 = id;
                            ppcVar5.type = 8;
                            ppcVar5.flags = 0x84;
                            ppcVar5.id = sVar2;
                            uVar2 = maxHalfHealth;
                            ppcVar5.maxHalfHealth = uVar2;
                            ppcVar5.FUN_3066C();
                            sVar1 = physics2.M2;
                            ppcVar5.physics2.M3 = 3;
                            ppcVar5.physics2.M2 = (short)(sVar1 - 1);
                            ppcVar5.physics1.Z = physics1.Z;
                            ppcVar5.physics1.W = physics1.W;
                            ppcVar5.physics2.X = physics2.X;
                            ppcVar5.DAT_80 = DAT_80;
                            ppcVar5.vTransform = vTransform;
                            ppcVar5.vTransform.position = local_88;
                            uVar3 = 0;
                        }
                        else
                        {
                            local_88.y = iVar4;
                            LevelManager.instance.FUN_4DE54(local_88, 144);
                            uVar3 = 0;
                        }
                    }
                }
            }
        }
        else
            uVar3 = 0;

        return uVar3;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        uint uVar3;

        uVar3 = 0;

        if (arg1 == 5)
        {
            GameManager.instance.FUN_309A0(this);
            uVar3 = 0xffffffff;
        }

        return uVar3;
    }
}
