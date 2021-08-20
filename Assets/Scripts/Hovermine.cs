using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hovermine : Mine
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
        VigObject oVar9;
        Vehicle vVar9;
        Vector3Int local_30;

        local_30 = new Vector3Int(0, -0x20000, 0);
        oVar9 = hit.self;

        if (oVar9.type == 0)
            return 1;

        if (oVar9.type == 2)
        {
            vVar9 = (Vehicle)oVar9;
            vVar9.FUN_2B370(local_30, screen);

            if (vVar9.id < 0)
                GameManager.instance.FUN_15B00(~vVar9.id, 255, 4, 64);
        }

        GameManager.instance.FUN_300B8(GameManager.instance.DAT_10C8, this);
        //sound
        LevelManager.instance.FUN_4DE54(screen, 39);
        LevelManager.instance.FUN_309C8(this, 0);
        return 0xffffffff;
    }

    //FUN_4692C
    public override uint UpdateW(int arg1, int arg2)
    {
        VigTuple ppiVar2;
        List<VigTuple> ppiVar3;
        byte bVar4;
        short sVar5;
        int iVar6;
        VigObject oVar8;
        int iVar9;
        VigObject oVar9;
        int iVar10;
        Vector3Int local_30;
        Vector3Int local_20;

        local_30 = new Vector3Int(0, -0x20000, 0);

        if (arg1 != 2)
        {
            if (arg1 < 3)
            {
                if (arg1 != 0)
                    return 0;

                sVar5 = (short)(physics2.M3 + 1);
                physics2.M3 = sVar5;

                if (30 < sVar5)
                {
                    oVar9 = null;
                    physics2.M3 = 0;
                    bVar4 = (byte)(DAT_19 + 1);
                    DAT_19 = bVar4;
                    iVar10 = 0x1f4000;

                    if (10 < bVar4)
                        id = 0;

                    ppiVar3 = GameManager.instance.worldObjs;

                    for (int i = 0; i < ppiVar3.Count; i++)
                    {
                        ppiVar2 = ppiVar3[i];
                        oVar8 = ppiVar2.vObject;

                        if (oVar8.type == 2 && (oVar8.flags & 0x4000) != 0 && oVar8.id != id && 
                           (iVar6 = Utilities.FUN_29F6C(oVar8.vTransform.position, vTransform.position)) < iVar10)
                        {
                            oVar9 = oVar8;
                            iVar10 = iVar6;
                        }
                    }

                    DAT_84 = oVar9;

                    if (oVar9 == null)
                    {
                        physics1.Z = 0;
                        physics1.W = 56;
                        physics2.X = 0;
                    }
                    else
                    {
                        local_20 = new Vector3Int(
                            oVar9.vTransform.position.x - vTransform.position.x,
                            0,
                            oVar9.vTransform.position.z - vTransform.position.z);
                        Utilities.FUN_2A098(local_20, out local_20);
                        iVar9 = local_20.x * 0x8f0;

                        if (iVar9 < 0)
                            iVar9 += 4095;

                        physics1.Z = iVar9 >> 12;
                        physics1.W = 56;
                        iVar9 = local_20.z * 0x8f0;

                        if (iVar9 < 0)
                            iVar9 += 4095;

                        physics2.X = iVar9 >> 12;
                    }
                }

                screen.x += physics1.Z;
                screen.y += physics1.W;
                screen.z += physics2.X;
                physics1.W += 56;
                iVar9 = screen.y + 0x5000;
                screen.y = iVar9;
                FUN_2D114(screen, ref vTransform);

                if (vTransform.position.z < GameManager.instance.DAT_DA0 &&
                    GameManager.instance.DAT_DB0 + 0x2800 < vTransform.position.y)
                    vTransform.position.y = GameManager.instance.DAT_DB0;

                if (vTransform.position.y < iVar9)
                {
                    physics1.W = 0;
                    iVar10 = vTransform.position.y - 0x5000;
                    screen.y = iVar10;
                    iVar9 = GameManager.DAT_65C90[(physics2.M2 & 0xfff) * 2] * 0x1400;

                    if (iVar9 < 0)
                        iVar9 += 4095;

                    vTransform.position.y = iVar10 + (iVar9 >> 12);
                }
                else
                {
                    iVar9 = GameManager.DAT_65C90[(physics2.M2 & 0xfff) * 2] * 0x1400;

                    if (iVar9 < 0)
                        iVar9 += 0xfff;

                    vTransform.position.y = screen.y + (iVar9 >> 12) - 0x5000;
                }

                physics2.M2 += 34;
                return 0;
            }

            return 0;
        }

        GameManager.instance.FUN_300B8(GameManager.instance.DAT_10C8, this);
        //sound
        LevelManager.instance.FUN_4DE54(screen, 39);
        LevelManager.instance.FUN_309C8(this, 0);
        return 0xffffffff;
    }
}
