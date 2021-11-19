using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiftBlade : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_9C4 (TSUNAMI.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        switch (arg1)
        {
            case 0:
                FUN_42330(arg2);
                break;
        }

        return 0;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        short sVar1;
        uint uVar2;
        int iVar3;
        RiftBlade2 puVar4;
        VigTransform puVar5;
        short sVar6;
        Vector3Int local_18;

        switch (arg1)
        {
            case 0:
                FUN_42330(arg2);
                uVar2 = 0;
                break;
            case 1:
                type = 3;
                maxHalfHealth = 3;
                flags |= 0x4000;
                goto default;
            default:
                uVar2 = 0;
                break;
            case 12:
                if (DAT_19 == 0)
                {
                    puVar4 = arg2.vData.ini.FUN_2C17C(2, typeof(RiftBlade2), 8) as RiftBlade2;

                    if (puVar4 == null)
                        return 0; //FUN_15D9C
                    else
                    {
                        Utilities.ParentChildren(puVar4, puVar4);
                        DAT_19 = 0;
                        puVar4.flags = 0x20000000;
                        sVar1 = arg2.id;
                        puVar4.type = 8;
                        puVar4.DAT_80 = arg2;
                        puVar4.id = sVar1;
                        puVar5 = GameManager.instance.FUN_2CDF4(arg2);
                        puVar4.DAT_84_2 = puVar5;
                        puVar4.vTransform = arg2.vTransform;
                        puVar4.DAT_AC = 0x199;
                        puVar4.DAT_B0 = 0x1000;
                        puVar4.FUN_305FC();
                        puVar4.FUN_30B78();
                        puVar4.FUN_30BF0();
                        arg2.FUN_30BA8();
                        arg2.physics1.W = 0;
                        arg2.physics1.X = 0;
                        arg2.flags |= 0x6000020;
                        arg2.physics1.Y = 0;
                        arg2.physics1.Z = 0;
                        arg2.physics2.X = 0;
                        arg2.physics2.Y = 0;
                        arg2.physics2.Z = 0;
                        GameManager.instance.FUN_30CB0(puVar4, 29);
                        puVar4.screen = vTransform.position;
                        puVar4.screen.x = -vTransform.position.x >> 12;
                        puVar4.screen.y = -vTransform.position.y >> 12;
                        puVar4.screen.z = -vTransform.position.z >> 12;
                        puVar4.DAT_B4 = 0;
                        puVar4.vTransform.position = Utilities.FUN_24148(puVar4.vTransform, vTransform.position);
                        iVar3 = GameManager.instance.FUN_1DD9C();
                        GameManager.instance.FUN_1E580(iVar3, arg2.vData.sndList, 4, puVar4.vTransform.position);
                        iVar3 = GameManager.instance.FUN_1DD9C();
                        GameManager.instance.FUN_1E188(iVar3, vData.sndList, 2);
                        GameManager.instance.DAT_1084++;
                        sVar6 = (short)(maxHalfHealth - 1);
                        maxHalfHealth = (ushort)sVar6;

                        if (sVar6 == 0)
                        {
                            FUN_3A368();
                            return 240;
                        }
                    }

                    uVar2 = 240;
                }
                else
                {
                    //iVar3 = GameManager.instance.FUN_1DD9C();
                    //GameManager.instance.FUN_1E14C(iVar3, GameManager.instance.DAT_C2C, 1);
                    //uVar2 = 240;
                    return 0;
                }

                break;
            case 13:
                uVar2 = 0;

                if (GameManager.instance.DAT_1084 == 0)
                {
                    local_18 = Utilities.FUN_24304(arg2.vTransform, ((Vehicle)arg2).target.vTransform.position);
                    uVar2 = 0;

                    if ((uint)local_18.z - 0x19001 < 0x63fff)
                    {
                        if (local_18.x < 0)
                            local_18.x = -local_18.x;

                        uVar2 = local_18.x * 6 - 0x10000 < local_18.z ? 1U : 0;
                    }
                }

                break;
        }

        return uVar2;
    }
}
