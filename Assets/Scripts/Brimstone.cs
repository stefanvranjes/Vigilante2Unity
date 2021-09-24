using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brimstone : VigObject
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
        uint uVar2;
        bool bVar4;
        VigObject oVar4;
        Vehicle vVar4;

        if (hit.object2.type == 3)
            return 0;

        oVar4 = hit.self;

        if (oVar4.type == 2)
        {
            vVar4 = (Vehicle)oVar4;
            vVar4.FUN_3A064(-200, vTransform.position, true);
            uVar2 = (uint)GameManager.instance.FUN_1DD9C();
            GameManager.instance.FUN_1E628((int)uVar2, GameManager.instance.DAT_C2C, 63, vTransform.position);
            bVar4 = LevelManager.instance.FUN_39AF8(vVar4);
            if (!bVar4) goto LAB_48598;
            LevelManager.instance.FUN_4DE54(vTransform.position, 35);
            uVar2 = 0x8008080;
        }
        else
        {
            LevelManager.instance.FUN_4DE54(vTransform.position, 35);
            uVar2 = (uint)GameManager.instance.FUN_1DD9C();
            GameManager.instance.FUN_1E580((int)uVar2, GameManager.instance.DAT_C2C, 63, vTransform.position);
            uVar2 = 0x8000080;
        }

        //FUN_4E414
        LAB_48598:
        GameManager.instance.FUN_309A0(this);
        return 0xffffffff;
    }

    //FUN_48214
    public override uint UpdateW(int arg1, int arg2)
    {
        short sVar1;
        int iVar2;
        Vector3Int v0;
        int iVar3;
        int iVar4;
        TileData tVar4;
        Particle2 pVar4;

        if (arg1 == 0)
        {
            vTransform.position.x += physics1.Z;
            vTransform.position.y += physics1.W;
            iVar3 = GameManager.instance.DAT_DA0;
            vTransform.position.z += physics2.X;
            physics1.W += 56;

            if (iVar3 <= vTransform.position.z || vTransform.position.y <= GameManager.instance.DAT_DB0)
            {
                iVar3 = GameManager.instance.terrain.FUN_1B750((uint)vTransform.position.x, (uint)vTransform.position.z);

                if (vTransform.position.y <= iVar3)
                {
                    if ((GameManager.instance.DAT_28 - DAT_19 & 3) == 0)
                    {
                        LevelManager.instance.FUN_4DE54(vTransform.position, 22);
                        return 0;
                    }

                    return 0;
                }

                pVar4 = LevelManager.instance.FUN_4E128(vTransform.position, 83, 40);
                Utilities.ParentChildren(pVar4, pVar4);
                pVar4.FUN_2D114(pVar4.screen, ref pVar4.vTransform);
                iVar2 = GameManager.instance.FUN_1DD9C();
                GameManager.instance.FUN_1E628(iVar2, GameManager.instance.DAT_C2C, 63, pVar4.vTransform.position);
                sVar1 = physics2.M3;
                physics2.M3 = (short)(sVar1 - 1);

                if (sVar1 != 1)
                {
                    tVar4 = GameManager.instance.terrain.GetTileByPosition((uint)vTransform.position.x, (uint)vTransform.position.z);

                    if (tVar4.DAT_10[3] == 0 || tVar4.DAT_10[3] == 7)
                    {
                        v0 = GameManager.instance.terrain.FUN_1B998((uint)vTransform.position.x, (uint)vTransform.position.z);
                        v0 = Utilities.VectorNormal(v0);
                        vTransform.position.y = iVar3;
                        iVar3 = v0.x * physics1.Z +
                                v0.y * physics1.W +
                                v0.z * physics2.X;

                        if (iVar3 < 0)
                            iVar3 += 2047;

                        iVar3 >>= 11;
                        iVar4 = iVar3 * v0.x;

                        if (iVar4 < 0)
                            iVar4 += 4095;

                        physics1.Z = (physics1.Z - (iVar4 >> 12)) / 2;
                        iVar4 = iVar3 * v0.y;

                        if (iVar4 < 0)
                            iVar4 += 4095;

                        physics1.W = (physics1.W - (iVar4 >> 12)) / 2;
                        iVar3 = iVar3 * v0.z;

                        if (iVar3 < 0)
                            iVar3 += 4095;

                        physics2.X = (physics2.X - (iVar3 >> 12)) / 2;
                        return 0;
                    }
                }

                LevelManager.instance.FUN_309C8(this, 1);
                return 0xffffffff;
            }

            LevelManager.instance.FUN_4DE54(vTransform.position, 138);
            iVar2 = GameManager.instance.FUN_1DD9C();
            GameManager.instance.FUN_1E14C(iVar2, GameManager.instance.DAT_C2C, 70);
        }

        return 0;
    }
}
