using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetMetalDrum : Destructible
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
        ushort uVar1;
        bool bVar2;
        int iVar2;
        VigObject oVar2;
        uint uVar3;
        VigTransform tVar4;
        SheetMetalDrum2 puVar4;
        Vector3Int local_10;

        bVar2 = FUN_32CF0(hit);
        uVar3 = 0;

        if (bVar2)
        {
            oVar2 = child2;
            tVar4 = GameManager.instance.FUN_2CDF4(oVar2);
            oVar2.vTransform = tVar4;
            oVar2.FUN_2CCBC();
            puVar4 = Utilities.FUN_52188(oVar2, typeof(SheetMetalDrum2)) as SheetMetalDrum2;
            puVar4.id = 1000;
            iVar2 = puVar4.screen.y * -0x3243;
            puVar4.flags |= 0x80; //originally 0x88
            uVar1 = maxFullHealth;
            puVar4.DAT_A8 = -puVar4.screen.y;
            puVar4.maxHalfHealth = uVar1;

            if (iVar2 < 0)
                iVar2 += 4095;

            local_10 = new Vector3Int();
            local_10.x = puVar4.vTransform.position.x;
            puVar4.DAT_A4 = (short)(0x1000000 / (iVar2 >> 12));
            local_10.y = puVar4.vTransform.position.y + puVar4.DAT_A8;
            local_10.z = puVar4.vTransform.position.z;
            iVar2 = puVar4.FUN_2CFBC(local_10);
            puVar4.physics2.W = iVar2;
            puVar4.physics2.Z = iVar2 - 655;
            puVar4.DAT_A0_2 = 240;
            puVar4.physics2.Y = 0x1999;
            puVar4.physics1.M6 = 0;
            puVar4.physics1.M7 = 0;
            puVar4.physics2.M0 = 0;
            puVar4.FUN_2D1DC();
            puVar4.FUN_4C98C();
            puVar4.FUN_305FC();
            GameManager.instance.FUN_4C4BC(puVar4.vShadow); //not in the original code
        }

        return uVar3;
    }

    //FUN_130C (STEELMIL.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        ushort uVar1;
        bool bVar2;
        int iVar2;
        VigObject oVar2;
        uint uVar3;
        VigTransform tVar4;
        SheetMetalDrum2 puVar4;
        Vector3Int local_10;

        if (arg1 != 8)
            return 0;

        bVar2 = FUN_32B90((uint)arg2);
        uVar3 = 0;

        if (bVar2)
        {
            oVar2 = child2;
            tVar4 = GameManager.instance.FUN_2CDF4(oVar2);
            oVar2.vTransform = tVar4;
            oVar2.FUN_2CCBC();
            puVar4 = Utilities.FUN_52188(oVar2, typeof(SheetMetalDrum2)) as SheetMetalDrum2;
            puVar4.id = 1000;
            iVar2 = puVar4.screen.y * -0x3243;
            puVar4.flags |= 0x80; //originally 0x88
            uVar1 = maxFullHealth;
            puVar4.DAT_A8 = -puVar4.screen.y;
            puVar4.maxHalfHealth = uVar1;

            if (iVar2 < 0)
                iVar2 += 4095;

            local_10 = new Vector3Int();
            local_10.x = puVar4.vTransform.position.x;
            puVar4.DAT_A4 = (short)(0x1000000 / (iVar2 >> 12));
            local_10.y = puVar4.vTransform.position.y + puVar4.DAT_A8;
            local_10.z = puVar4.vTransform.position.z;
            iVar2 = puVar4.FUN_2CFBC(local_10);
            puVar4.physics2.W = iVar2;
            puVar4.physics2.Z = iVar2 - 655;
            puVar4.DAT_A0_2 = 240;
            puVar4.physics2.Y = 0x1999;
            puVar4.physics1.M6 = 0;
            puVar4.physics1.M7 = 0;
            puVar4.physics2.M0 = 0;
            puVar4.FUN_2D1DC();
            puVar4.FUN_4C98C();
            puVar4.FUN_305FC();
            GameManager.instance.FUN_4C4BC(puVar4.vShadow); //not in the original code
        }

        return uVar3;
    }
}
