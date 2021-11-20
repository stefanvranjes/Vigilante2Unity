using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : Destructible
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
        Throwaway puVar4;
        int iVar5;
        int iVar6;
        int iVar9;
        VigObject oVar9;
        VigObject ppcVar10;
        Vehicle vVar10;
        Ballistic puVar11;
        HitDetection auStack96;
        Vector3Int local_70;
        Vector3Int local_28;

        ppcVar10 = hit.self;

        if (ppcVar10.type == 2 && tags == 0)
        {
            auStack96 = new HitDetection(null);
            GameManager.instance.FUN_2FB70(this, hit, auStack96);
            iVar9 = 0;
            local_70 = Utilities.FUN_24148(hit.self.vTransform, auStack96.position);
            LevelManager.instance.FUN_4DE54(local_70, 42);
            iVar3 = GameManager.instance.FUN_1DD9C();
            GameManager.instance.FUN_1E580(iVar3, GameManager.instance.DAT_C2C, 75, local_70);
            GameManager.instance.FUN_30CB0(this, 60);
            tags = 1;

            do
            {
                puVar4 = LevelManager.instance.xobfList[19].ini.FUN_2C17C(49, typeof(Throwaway), 8) as Throwaway;
                puVar4.physics1.M0 = 0;
                puVar4.physics1.M1 = 0;
                puVar4.physics1.M2 = 0;
                iVar5 = (int)GameManager.FUN_2AC5C();
                puVar4.physics1.Z = (iVar5 & 0xfff) - 0x800;
                iVar6 = (int)GameManager.FUN_2AC5C();

                if (iVar6 < 0)
                    iVar6 += 15;

                puVar4.physics1.W = -(iVar6 >> 4);
                iVar5 = (int)GameManager.FUN_2AC5C();
                puVar4.physics2.X = (iVar5 & 0xfff) - 0x800;
                puVar4.type = 7;
                puVar4.flags |= 0x1b4;
                sVar1 = id;
                iVar9++;
                puVar4.state = _THROWAWAY_TYPE.Type3;
                puVar4.id = sVar1;
                puVar4.vTransform = GameManager.FUN_2A39C();
                puVar4.vTransform.position = local_70;
                puVar4.FUN_2D1DC();
                puVar4.DAT_87 = 1;
                puVar4.FUN_305FC();
            } while (iVar9 < 12);

            puVar11 = vData.ini.FUN_2C17C(17, typeof(Ballistic), 8) as Ballistic;

            if (puVar11 != null)
            {
                oVar9 = Utilities.FUN_2CD78(this);

                if (oVar9 == null)
                    oVar9 = this;

                puVar11.vTransform = oVar9.vTransform;
                puVar11.flags = 4;
                puVar11.type = 3;
                puVar11.FUN_305FC();
            }

            ppcVar10.physics1.X = ppcVar10.physics1.X / 2;
            ppcVar10.physics1.Z = ppcVar10.physics1.Z / 2;
            local_28 = new Vector3Int();
            local_28.x = auStack96.normal2.x << 3;
            local_28.y = auStack96.normal2.y * 8 - 0x8f080;
            local_28.z = auStack96.normal2.z << 3;
            ppcVar10.FUN_2B1FC(local_28, auStack96.position);
            sVar2 = ppcVar10.id;

            if (sVar2 < 0)
            {
                UIManager.instance.FUN_4E414(local_70, new Color32(0x00, 0x00, 0xff, 8));
                sVar2 = ppcVar10.id;
            }

            iVar3 = -25;

            if (sVar2 < 0)
                iVar3 = -100;

            vVar10 = (Vehicle)ppcVar10;
            vVar10.FUN_3A064(iVar3, auStack96.position, false);
            vVar10.state = _VEHICLE_TYPE.Fence;
            GameManager.instance.FUN_30CB0(vVar10, 15);
        }
        else
            FUN_32CF0(hit);

        return 0;
    }

    //FUN_3798 (LAUNCH.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        switch (arg1)
        {
            case 1:
                if (tags == 0)
                    DAT_19 = 1;
                else
                {
                    tags = 0;
                    DAT_19 = 0;
                }

                break;
            case 2:
                tags = 0;
                break;
            case 8:
                FUN_32B90((uint)arg2);
                break;
            case 9:
                if (arg2 == 0 && DAT_19 != 0)
                    FUN_33A28(0x8f80);

                break;
        }

        return 0;
    }
}
