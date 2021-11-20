using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : Destructible
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
        VigTransform tVar1;
        VigObject oVar2;
        int iVar3;
        VigObject oVar4;
        BufferedBinaryReader pcVar4;
        VigCamera cVar4;
        Vehicle ppcVar6;
        Vector3Int local_18;
        Vector3Int local_20;

        if (id < 99 && hit.collider1.ReadUInt16(0) == 1 &&
            hit.collider1.ReadUInt16(2) == 1)
        {
            if (hit.self.type == 2 && (hit.self.flags & 0x4000000) == 0)
            {
                ppcVar6 = (Vehicle)hit.self;

                if (id == 98)
                {
                    iVar3 = (int)GameManager.FUN_2AC5C();
                    iVar3 = ((iVar3 << 1) >> 15) + 99;
                }
                else
                {
                    iVar3 = (int)GameManager.FUN_2AC5C();
                    iVar3 = (iVar3 * 3 >> 15) + 98;
                }

                iVar1 = GameManager.instance.FUN_1DD9C();
                GameManager.instance.FUN_1E580(iVar1, GameManager.instance.DAT_C2C, 36, ppcVar6.vTransform.position);
                GameManager.instance.FUN_1E2C8(ppcVar6.DAT_18, 0);
                ppcVar6.state = _VEHICLE_TYPE.Pipe;
                ppcVar6.tags = 0;
                ppcVar6.DAT_19 = (byte)iVar3;
                ppcVar6.flags = ppcVar6.flags & 0xfffffffd | 0x6000020;
                iVar1 = 401;

                if (iVar3 == 100 && id != 98)
                    iVar1 = 400;

                oVar2 = GameManager.instance.FUN_30250(GameManager.instance.DAT_1078, iVar1);

                if (id == 98 && iVar3 == 100)
                    ppcVar6.DAT_19 = 102;

                ppcVar6.screen = oVar2.screen;
                pcVar4 = hit.collider1;
                tVar1 = GameManager.instance.FUN_2CDF4(this);
                local_18 = new Vector3Int();
                local_18.x = (pcVar4.ReadInt32(4) + pcVar4.ReadInt32(16)) / 2;
                local_18.y = (pcVar4.ReadInt32(8) + pcVar4.ReadInt32(20)) / 2;
                local_18.z = (pcVar4.ReadInt32(12) + pcVar4.ReadInt32(24)) / 2;
                local_18 = Utilities.FUN_24148(tVar1, local_18);
                Utilities.FUN_2A168(out local_20, ppcVar6.vTransform.position, local_18);
                ppcVar6.physics1.X = local_20.x * 143;
                ppcVar6.physics1.Y = local_20.y * 143;
                ppcVar6.physics1.Z = local_20.z * 143;
                ppcVar6.physics2.X = 0;
                ppcVar6.physics2.Y = 0;
                ppcVar6.physics2.Z = 0;
                cVar4 = ppcVar6.vCamera;

                if (cVar4 != null)
                {
                    cVar4.DAT_84 = new Vector3Int(0, 0, 0);
                    cVar4.flags |= 0xc000000;
                    oVar4 = GameManager.instance.FUN_30250(GameManager.instance.DAT_1078, 514);
                    ppcVar6.PDAT_74 = oVar4;
                }

                GameManager.instance.FUN_30CB0(ppcVar6, 64);
            }
        }
        else
            FUN_32CF0(hit);

        return 0;
    }

    //FUN_1860 (OILFIELD.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        VigObject oVar3;

        if (arg1 < 4)
        {
            if (arg1 == 1)
            {
                oVar3 = child2;

                while (oVar3 != null)
                {
                    if (oVar3.id == 1)
                        oVar3.type = 3;

                    oVar3 = oVar3.child;
                }
            }
        }
        else
        {
            if (arg1 == 8)
                FUN_32B90((uint)arg2);
        }

        return 0;
    }
}
