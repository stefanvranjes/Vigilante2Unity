using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public static VigObject OnInitialize(XOBF_DB arg1, int arg2)
    {
        return arg1.ini.FUN_2C17C((ushort)arg2, typeof(Revolver), 8, typeof(Revolver2));
    }

    //FUN_4E0 (TBOLT.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        uint uVar2;

        switch (arg1)
        {
            case 0:
                FUN_42330(arg2);
                uVar2 = 0;
                break;
            default:
                uVar2 = 0;
                break;
        }

        return uVar2;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        bool bVar1;
        uint uVar2;
        ushort uVar3;
        short sVar4;
        uint uVar5;
        Ballistic puVar6;
        Revolver3 puVar7;
        int iVar8;
        int iVar9;
        Vector3Int local_28;
        Vector3Int local_18;

        switch (arg1)
        {
            case 0:
                FUN_42330(arg2);
                uVar2 = 0;
                break;
            case 1:
                maxHalfHealth = 6;
                goto default;
            default:
                uVar2 = 0;
                break;
            case 12:
                uVar5 = GameManager.FUN_2AC5C();
                bVar1 = (uVar5 & 3) == 0;
                puVar6 = LevelManager.instance.xobfList[19].ini.FUN_2C17C(3, typeof(Ballistic), 8) as Ballistic;
                iVar9 = 3;

                if (bVar1)
                    iVar9 = 2;

                puVar7 = LevelManager.instance.FUN_42408(arg2, this, (ushort)iVar9, typeof(Revolver3), puVar6) as Revolver3;
                local_28 = new Vector3Int();
                local_28.x = 0;
                local_28.y = 0;

                if (bVar1)
                    local_28.z = -0x180000;
                else
                    local_28.z = -0x10000;

                puVar7.flags = 0x60000084;
                puVar7.tags = (sbyte)(bVar1 ? 1 : 0);

                if (bVar1)
                    uVar3 = 450;
                else
                    uVar3 = 250;

                puVar7.maxHalfHealth = uVar3;
                puVar7.physics2.M2 = 60;
                iVar8 = arg2.physics1.X;

                if (iVar8 < 0)
                    iVar8 += 127;

                puVar7.physics1.Z = (iVar8 >> 7) + puVar7.vTransform.rotation.V02 * 6;
                iVar8 = arg2.physics1.Y;

                if (iVar8 < 0)
                    iVar8 += 127;

                puVar7.physics1.W = (iVar8 >> 7) + puVar7.vTransform.rotation.V12 * 6;
                iVar8 = arg2.physics1.Z;

                if (iVar8 < 0)
                    iVar8 += 127;

                puVar7.physics2.X = (iVar8 >> 7) + puVar7.vTransform.rotation.V22 * 6;
                puVar7.FUN_305FC();
                GameManager.instance.FUN_30CB0(puVar7, 120);
                puVar6.FUN_30BF0();

                if ((child2.flags & 0x80) == 0)
                    child2.FUN_30B78();

                child2.maxHalfHealth = 90;
                arg2.FUN_2B1FC(local_28, screen);
                iVar9 = GameManager.instance.FUN_1DD9C();
                GameManager.instance.FUN_1E580(iVar9, vData.sndList, 3, puVar7.screen);
                iVar9 = GameManager.instance.FUN_1DD9C();
                GameManager.instance.FUN_1E188(iVar9, vData.sndList, 2);

                if (arg2.id < 0)
                    GameManager.instance.FUN_15B00(~arg2.id, 255, (byte)((uint)(bVar1 ? 1 : 0) << 5), 64);

                sVar4 = (short)(maxHalfHealth - 1);
                maxHalfHealth = (ushort)sVar4;
                uVar2 = 60;

                if (sVar4 == 0)
                {
                    FUN_3A368();
                    uVar2 = 60;
                }

                break;

            case 13:
                local_18 = Utilities.FUN_24304(arg2.vTransform, ((Vehicle)arg2).target.vTransform.position);
                uVar2 = 0;

                if ((uint)local_18.z - 1 < 0x7cfff)
                {
                    if (local_18.x < 0)
                        local_18.x = -local_18.x;

                    uVar2 = local_18.x << 3 < local_18.z ? 1U : 0;
                }

                break;
        }

        return uVar2;
    }
}
