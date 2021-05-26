using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruiserCannon : Weapon
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    // FUN_453E0
    public override void UpdateW()
    {
        long lVar4;
        long lVar5;
        VigObject oVar7;
        int iVar7;
        VigTransform tVar10;
        int iVar10;
        uint uVar11;
        VigObject oVar13;
        Vector3Int local_30;
        uint local_20;
        int local_1c;

        //FUN_42330

        if (owner.target != null)
        {
            oVar13 = vObject.child2;
            tVar10 = GameManager.instance.FUN_2CDF4(vObject);
            oVar7 = owner.target;
            local_30 = new Vector3Int();
            local_30.x = oVar7.screen.x - tVar10.position.x;
            local_30.y = oVar7.screen.y - tVar10.position.y;
            local_30.z = oVar7.screen.z - tVar10.position.z;
            local_30 = Utilities.FUN_2426C(tVar10.rotation, 
                new Matrix2x4(local_30.x, local_30.y, local_30.z, 0));
            lVar4 = Utilities.Ratan2(local_30.x, local_30.z);
            local_20 = (uint)((long)local_30.z * local_30.z);
            local_1c = (int)((ulong)((long)local_30.z * local_30.z) >> 32);
            uVar11 = (uint)((long)local_30.x * local_30.x) + local_20;
            iVar10 = Utilities.FUN_2ABC4(uVar11, (int)((ulong)((long)local_30.x * local_30.x) >>
                                                    32) + local_1c + (uVar11 < local_20 ? 1 : 0));
            local_20 = (uint)((long)local_30.y * 0x6000);
            local_1c = (int)((ulong)((long)local_30.y * 0x6000) >> 32);
            iVar7 = (int)Utilities.Divdi3((int)local_20, local_1c, iVar10, iVar10 >> 31);
            lVar5 = Utilities.Ratan2(iVar7 - (iVar10 * 56) / 0xC000, 0x6000);
            iVar7 = (int)(lVar5 * -0x100000 >> 20);
            iVar10 = 256;

            if (iVar7 < 256)
                iVar10 = iVar7;

            iVar7 = -128;

            if (-128 < iVar10)
                iVar7 = iVar10;

            iVar10 = (int)((lVar4 << 20) >> 20) - oVar13.vr.y;

            if (iVar10 < 0)
                iVar10 += 3;

            oVar13.vr.y += iVar10 >> 2;
            iVar10 = (short)iVar7 - oVar13.vr.x;

            if (iVar10 < 0)
                iVar10 += 3;

            oVar13.vr.x += iVar10 >> 2;
            oVar13.ApplyTransformation();
        }
    }

    //FUN_45670
    public override void ComboW(uint combo)
    {
        uint uVar11;
        VigObject oVar12;

        combo &= 0xfff;

        if (combo == 0x242)
        {
            uVar11 = 0xffffffff;

            if (1 < ammo)
            {
                ammo--;
                oVar12 = Utilities.FUN_2CD78(vObject);
                //...
            }
        }
    }
}
