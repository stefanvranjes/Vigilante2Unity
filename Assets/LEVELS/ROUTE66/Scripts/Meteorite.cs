using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_9F0 (ROUTE66.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        int iVar1;
        int iVar2;
        VigObject oVar2;
        Vehicle vVar2;
        Meteorite2 puVar3;
        VigObject oVar4;
        int iVar5;
        uint uVar6;
        Vector3Int[] piVar8;
        int iVar9;
        uint uVar10;
        Vector2Int v2Var11;
        Vector3Int local_30;
        Vector3Int local_20;

        if (arg1 == 2)
        {
            iVar1 = (int)GameManager.FUN_2AC5C();
            iVar1 = iVar1 * 12 >> 15;
            iVar2 = (int)GameManager.FUN_2AC5C();
            local_30 = new Vector3Int();
            local_30.x = ((iVar2 << 8) >> 15) + 0xb80;
            local_30.y = 0x1000;
            iVar2 = (int)GameManager.FUN_2AC5C();
            local_30.z = ((iVar2 << 8) >> 15) + 0xb80;
            oVar2 = GameManager.instance.FUN_31950(33);

            if (oVar2 == null)
                iVar5 = 900;
            else
            {
                vVar2 = oVar2.PDAT_78 as Vehicle;
                iVar5 = 900;

                if (vVar2 != null)
                {
                    uVar10 = 0x7fff0000;

                    if (vVar2.target != null)
                    {
                        iVar9 = 0;
                        piVar8 = Route66.DAT_5268;

                        do
                        {
                            oVar4 = vVar2.target;
                            local_20 = new Vector3Int();
                            local_20.x = piVar8[iVar9].x - oVar4.screen.x;
                            local_20.y = piVar8[iVar9].y - oVar4.screen.y;
                            local_20.z = piVar8[iVar9].z - oVar4.screen.z;
                            v2Var11 = Utilities.FUN_2A1C0(local_20);
                            uVar6 = (uint)v2Var11.x >> 16 | (uint)v2Var11.y << 16;

                            if ((int)uVar6 < (int)uVar10)
                            {
                                uVar10 = uVar6;
                                iVar1 = iVar9;
                            }

                            iVar9++;
                        } while (iVar9 < 13);

                        iVar5 = 360;
                    }
                }
            }

            GameManager.instance.FUN_30CB0(this, iVar5);
            puVar3 = LevelManager.instance.xobfList[42].ini.FUN_2C17C(11, typeof(Meteorite2), 0) as Meteorite2;
            puVar3.screen = Route66.DAT_5268[iVar1];
            iVar1 = (int)GameManager.FUN_2AC5C();
            puVar3.screen.x = puVar3.screen.x - 0x64000 + (iVar1 * 0xc8000 >> 15);
            iVar1 = (int)GameManager.FUN_2AC5C();
            puVar3.id = 1000;
            puVar3.maxHalfHealth = 100;
            puVar3.DAT_19 = 1;
            puVar3.screen.z = puVar3.screen.z - 0x64000 + (iVar1 * 0xc8000 >> 15);
            puVar3.flags |= 0x180;
            local_30 = Utilities.VectorNormal(local_30);
            puVar3.vTransform.position.x = puVar3.screen.x + local_30.x * -750;
            puVar3.vTransform.position.y = puVar3.screen.y + local_30.y * -750;
            puVar3.vTransform.position.z = puVar3.screen.z + local_30.z * -750;
            iVar1 = local_30.x * 30517;

            if (iVar1 < 0)
                iVar1 += 4095;

            puVar3.physics1.X = (iVar1 >> 12) << 7;
            iVar1 = local_30.y * 30517;

            if (iVar1 < 0)
                iVar1 += 4095;

            puVar3.physics1.Y = (iVar1 >> 12) << 7;
            iVar1 = local_30.z * 30517;

            if (iVar1 < 0)
                iVar1 += 4095;

            puVar3.physics1.Z = (iVar1 >> 12) << 7;
            uVar10 = GameManager.FUN_2AC5C(); ;
            puVar3.physics2.X = (int)(uVar10 & 0x1ff) << 7;
            uVar10 = GameManager.FUN_2AC5C();
            puVar3.physics2.Z = (int)(uVar10 & 0x1ff) << 7;
            puVar3.FUN_2D1DC();
            puVar3.ApplyRotationMatrix();
            puVar3.FUN_305FC();
            iVar5 = GameManager.instance.FUN_1DD9C();
            GameManager.instance.FUN_1E14C(iVar5, GameManager.instance.DAT_C2C, 58);
            GameManager.instance.FUN_1E30C(iVar5, 0xc7a);
        }

        return 0;
    }
}
