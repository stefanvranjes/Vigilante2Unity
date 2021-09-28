using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmogPipe : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_208 (BUS.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        switch (arg1)
        {
            case 0:
                FUN_42330(arg2);
                return 0;
            case 11:
                vr.x = 0;
                break;
        }

        return 0;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        short sVar1;
        short sVar2;
        Smog puVar3;
        VigTransform puVar4;
        int iVar5;
        int iVar6;
        int iVar7;
        ConfigContainer ccVar7;
        Vector3Int local_18;

        switch (arg1)
        {
            case 0:
                FUN_42330(arg2);
                return 0;
            case 1:
                maxHalfHealth = 40;
                break;
            case 11:
                vr.x = 0;
                break;
            case 12:
                ccVar7 = FUN_2C5F4(0x8000);
                puVar3 = LevelManager.instance.xobfList[19].ini.FUN_2C17C(11, typeof(Smog), 8) as Smog;
                puVar3.flags = 0x60000794;
                sVar1 = arg2.id;
                puVar3.type = 8;
                puVar3.maxHalfHealth = 40;
                puVar3.DAT_80 = arg2;
                puVar3.id = sVar1;

                if (ccVar7 == null)
                {
                    puVar4 = GameManager.instance.FUN_2CDF4(this);
                    puVar3.vTransform = puVar4;
                }
                else
                    puVar3.vTransform = GameManager.instance.FUN_2CEAC(this, ccVar7);

                puVar3.screen = puVar3.vTransform.position;
                iVar7 = arg2.physics1.X;

                if (iVar7 < 0)
                    iVar7 += 127;

                puVar3.physics1.Z = (iVar7 >> 7) -
                                    (((int)((uint)(ushort)puVar3.vTransform.rotation.V02 << 16) >> 16) -
                                    ((int)((uint)(ushort)puVar3.vTransform.rotation.V02 << 16) >> 31) >> 1);
                iVar7 = arg2.physics1.Y;

                if (iVar7 < 0)
                    iVar7 += 127;

                iVar5 = puVar3.vTransform.rotation.V12 << 16;
                puVar3.physics1.W = (iVar7 >> 7) - ((iVar5 >> 16) - (iVar5 >> 31) >> 1);
                iVar7 = arg2.physics1.Z;

                if (iVar7 < 0)
                    iVar7 += 127;

                puVar3.physics2.X = (iVar7 >> 7) -
                                    (((int)((uint)(ushort)puVar3.vTransform.rotation.V22 << 16) >> 16) -
                                    ((int)((uint)(ushort)puVar3.vTransform.rotation.V22 << 16) >> 31) >> 1);
                iVar7 = (int)GameManager.FUN_2AC5C();
                puVar3.physics2.M3 = (short)((iVar7 * 68 >> 15) - 34);
                puVar3.FUN_305FC();
                sVar2 = (short)(vr.x + 1);
                vr.x = sVar2;

                if (sVar2 == 1)
                {
                    iVar6 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E188(iVar6, vData.sndList, 2);
                }

                sVar2 = (short)(maxHalfHealth - 1);
                maxHalfHealth = (ushort)sVar2;

                if (sVar2 == 0)
                    FUN_3A368();

                if (-1 < arg2.id && 19 < vr.x)
                {
                    flags &= 0xfbffffff;
                    return 180;
                }

                flags |= 0x4000000;
                return 10;
            case 13:
                if (vr.x != 0)
                    return vr.x < 20 ? 1U : 0;

                if (GameManager.instance.DAT_1084 != 0)
                    return 0;

                local_18 = Utilities.FUN_24304(arg2.vTransform, ((Vehicle)arg2).target.vTransform.position);

                if (-1 < local_18.z)
                    return 0;

                if (local_18.z < -0x4afff)
                    return 0;

                iVar7 = Utilities.Ratan2(local_18.x, local_18.z);
                iVar7 = (iVar7 << 20) >> 20;

                if (iVar7 < 0)
                    iVar7 = -iVar7;

                return 0x555 < iVar7 ? 1U : 0;
        }

        return 0;
    }
}
