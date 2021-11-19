using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Trailer
{
    //FUN_13DC (TRUCK.DLL)
    public static uint LoadTrailer(Vehicle param1, int param2, int param3)
    {
        short sVar1;
        Body ppcVar2;

        if (param2 == 1)
        {
            ppcVar2 = param1.body[1].child2 as Body;

            while (ppcVar2 != null)
            {
                if (ppcVar2.id == 1)
                {
                    sVar1 = param1.id;
                    ppcVar2.state = _BODY_TYPE.Trailer;
                    ppcVar2.id = sVar1;
                    ppcVar2.UpdateW(1, 0);
                    param1.FUN_2D1DC();
                    break;
                }

                ppcVar2 = ppcVar2.child as Body;
            }

            return param1.FUN_367A4(param2, param3);
        }

        return 0;
    }

    //FUN_A0 (TRUCK.DLL)
    public static void FUN_A0(this Body param1)
    {
        int iVar1;
        VigObject oVar1;
        ConfigContainer ccVar1;
        Wheel wVar2;
        ConfigContainer ccVar2;
        VigTransform puVar3;
        Trailer2 tVar4;
        Vector3Int[] piVar5;
        int iVar6;
        VigObject oVar6;
        int iVar9;
        Vector3Int[] puVar10;
        uint uVar11;

        oVar1 = Utilities.FUN_2CDB0(param1);
        ccVar2 = param1.FUN_2C5F4(0x8100);
        puVar3 = GameManager.instance.FUN_2CDF4(param1);
        param1.vTransform = puVar3;
        uVar11 = 0;
        oVar6 = param1.FUN_2CCBC();
        tVar4 = Utilities.FUN_52188(oVar6, typeof(Trailer2)) as Trailer2;
        tVar4.type = 10;
        tVar4.flags = 0x88;
        tVar4.maxHalfHealth = oVar1.maxHalfHealth;
        tVar4.FUN_4C98C();
        tVar4.FUN_305FC();
        tVar4.physics1.X = 0;
        tVar4.physics1.Y = 0;
        tVar4.physics1.Z = 0;
        tVar4.physics2.X = 0;
        tVar4.physics2.Y = 0;
        tVar4.physics2.Z = 0;
        tVar4.DAT_A0 = new Vector3Int(0x64, 0x1c, 0x30);
        tVar4.DAT_A6 = 0x3000;
        oVar1.DAT_A6 += 0x3000;
        tVar4.DAT_A8 = ccVar2.v3_1;
        tVar4.DAT_C0 = (Vehicle)oVar1;
        tVar4.DAT_B4 = Utilities.FUN_24148(tVar4.vTransform, tVar4.DAT_A8);
        tVar4.DAT_B4 = Utilities.FUN_24304(oVar1.vTransform, tVar4.DAT_B4);

        do
        {
            ccVar1 = tVar4.FUN_2C5F4((ushort)(uVar11 - 0x8000 & 0xffff));

            if (ccVar1 != null)
            {
                wVar2 = LevelManager.instance.xobfList[18].ini.FUN_2C17C(28, typeof(Wheel), 8) as Wheel;
                wVar2.physics2.X = -LevelManager.instance.xobfList[18].ini.configContainers[28].v3_1.y;
                iVar9 = (int)GameManager.FUN_2AC5C();
                wVar2.vr.x = iVar9 << 4;
                wVar2.vr.y = 0;
                wVar2.vr.z = (int)(uVar11 & 1) << 11;
                wVar2.FUN_2C7D0();
                wVar2.id = wVar2.DAT_1A;
                wVar2.screen = ccVar1.v3_1;
                Utilities.FUN_2CC48(tVar4, wVar2);
                Utilities.ParentChildren(tVar4, tVar4);
                tVar4.DAT_C4[uVar11] = wVar2;
                ccVar1 = tVar4.vData.ini.FUN_2C5CC(ccVar1, 0x8000);
                wVar2.type = 9;

                if (ccVar1 == null)
                    wVar2.physics1.X = 0;
                else
                    wVar2.physics1.X = ccVar1.v3_1.y;

                wVar2.physics1.M6 = 40;
                wVar2.physics1.M7 = 128;
                iVar1 = wVar2.physics2.X * 0x6486;
                wVar2.physics1.Y = wVar2.screen.y;

                if (iVar1 < 0)
                    iVar1 += 4095;

                wVar2.physics2.Y = 0x1000000 / (iVar1 >> 12);
                wVar2.ApplyTransformation();
            }

            uVar11++;
        } while ((int)uVar11 < 2);

        piVar5 = new Vector3Int[3];
        puVar10 = new Vector3Int[3];
        piVar5[0] = tVar4.DAT_A8;
        puVar10[0] = Utilities.FUN_24148(tVar4.vTransform, piVar5[0]);
        iVar1 = 0;

        do
        {
            wVar2 = tVar4.DAT_C4[iVar1];
            piVar5[iVar1 + 1] = new Vector3Int();
            piVar5[iVar1 + 1].x = wVar2.screen.x;
            piVar5[iVar1 + 1].y = wVar2.screen.y + wVar2.physics2.X - 0x1400;
            piVar5[iVar1 + 1].z = wVar2.screen.z;
            puVar10[iVar1 + 1] = Utilities.FUN_24148(tVar4.vTransform, piVar5[iVar1 + 1]);
            iVar6 = tVar4.FUN_2CFBC(puVar10[iVar1 + 1]);
            puVar10[iVar1 + 1].y = iVar6;
            iVar1++;
        } while (iVar1 < 2);

        tVar4.vTransform = Utilities.FUN_2A9F4(piVar5, puVar10);
    }
}
