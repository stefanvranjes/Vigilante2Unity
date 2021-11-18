using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crater : VigObject
{
    protected override void Start()
    {
        
    }

    protected override void Update()
    {
        
    }

    public ushort[] indices;

    //FUN_459B8
    public override uint UpdateW(int arg1, int arg2)
    {
        ushort uVar1;
        bool bVar2;
        int iVar4;
        int iVar5;
        uint uVar6;
        int psVar7;
        uint uVar8;
        int psVar9;
        uint uVar10;
        uint uVar11;
        VigTerrain terrain;

        if (arg1 == 2)
        {
            iVar4 = screen.x;
            iVar5 = DAT_58;
            uVar8 = (uint)(iVar4 - iVar5) + 0xffffU >> 16;
            uVar11 = (uint)(screen.z - iVar5) + 0xffffU >> 16;
            uVar10 = (uint)(screen.z + iVar5) >> 16;
            bVar2 = false;
            psVar7 = 0;
            terrain = GameManager.instance.terrain;

            while(uVar8 <= (uint)(iVar4 + iVar5) >> 16)
            {
                psVar9 = psVar7;

                if (uVar11 <= uVar10)
                {
                    uVar6 = uVar11;

                    do
                    {
                        uVar1 = indices[psVar9];
                        psVar9++;

                        if (uVar1 != 0)
                        {
                            indices[psVar7]--;
                            terrain.vertices[(terrain.chunks[(uVar8 >> 6) * 0x20 + (uVar6 >> 6)] * 4096) +
                                            ((int)(uVar8 & 63) * 128 + (int)(uVar6 & 63) * 2) / 2]--;
                            bVar2 = true;
                        }

                        uVar6++;
                        psVar7++;
                    } while (uVar6 <= uVar10);
                }

                uVar8++;
                psVar7 = psVar9;
            }

            terrain.FUN_50E40(screen.x, screen.z, DAT_58);

            if (!bVar2)
            {
                Destroy(gameObject);
                return 0xffffffff;
            }

            GameManager.instance.FUN_30CB0(this, 60);
        }

        return 0;
    }
}
