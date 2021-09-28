using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaCollider : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_6D0 (CORSAIR.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        switch (arg1)
        {
            case 0:
                FUN_42330(arg2);
                return 0;
        }

        return 0;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        ushort uVar1;
        int iVar2;
        ConfigContainer ccVar2;
        Beam puVar3;
        VigTransform puVar4;
        VigTransform piVar5;
        Beam2 puVar6;
        VigObject oVar7;
        ConfigContainer ccVar7;
        uint uVar10;
        VigTransform local_48;
        Vector3Int local_28;
        Vector3Int local_18;

        switch (arg1)
        {
            case 0:
                FUN_42330(arg2);
                return 0;
            case 1:
                type = 3;
                maxHalfHealth = 3;
                break;
            case 12:
                if (DAT_19 != 0 && maxHalfHealth != 0)
                {
                    iVar2 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E14C(iVar2, GameManager.instance.DAT_C2C, 1);
                    return 360;
                }

                puVar3 = arg2.vData.ini.FUN_2C17C(3, typeof(Beam), 8) as Beam;

                if (puVar3 != null)
                {
                    uVar10 = 0;
                    puVar3.flags = 0x60000200;
                    uVar1 = (ushort)arg2.id;
                    puVar3.type = 8;
                    puVar3.DAT_80 = arg2;
                    puVar3.id = (short)uVar1;
                    puVar4 = GameManager.instance.FUN_2CDF4(arg2);
                    puVar3.vTransform = puVar4;
                    ccVar2 = FUN_2C5F4(0x8000);
                    Utilities.FUN_2CA94(this, ccVar2, puVar3);
                    Utilities.ParentChildren(this, this);
                    puVar3.FUN_30BF0();
                    puVar3.FUN_30B78();
                    piVar5 = GameManager.instance.FUN_2CDF4(puVar3);
                    local_48 = piVar5;
                    local_28 = new Vector3Int(); //not in the original code
                    puVar3.DAT_84_2 = new Beam2[15];

                    do
                    {
                        puVar6 = arg2.vData.ini.FUN_2C17C(1, typeof(Beam2), 8) as Beam2;

                        if (puVar6 != null)
                        {
                            puVar6.flags = 0x60000006;
                            uVar1 = (ushort)arg2.id;
                            puVar6.type = 8;
                            puVar6.DAT_80 = arg2;
                            puVar6.maxHalfHealth = 4;
                            puVar6.id = (short)uVar1;
                            puVar6.vTransform = local_48;

                            if (uVar10 == 0)
                            {
                                ccVar7 = puVar6.FUN_2C5F4(0x8000);
                                puVar3.DAT_D0 = ccVar7.v3_1;
                                local_28 = puVar3.DAT_D0;
                            }
                            else
                            {
                                puVar6.vTransform.position = Utilities.FUN_24148(local_48, local_28);
                                local_28.x += puVar3.DAT_D0.x;
                                local_28.y += puVar3.DAT_D0.y;
                                local_28.z += puVar3.DAT_D0.z;
                            }

                            if ((uVar10 & 1) != 0)
                                puVar6.flags |= 0x20;

                            puVar6.FUN_305FC();
                            puVar3.DAT_84_2[uVar10] = puVar6;
                        }

                        uVar10++;
                    } while ((int)uVar10 < 15);

                    oVar7 = arg2.vData.ini.FUN_2C17C(2, typeof(VigObject), 8);

                    if (oVar7 != null)
                    {
                        oVar7.flags = 0x20000000;
                        uVar1 = (ushort)arg2.id;
                        oVar7.type = 3;
                        oVar7.id = (short)uVar1;
                        oVar7.vTransform = GameManager.FUN_2A39C();
                        ccVar2 = puVar3.DAT_84_2[0].FUN_2C5F4(0x8000);
                        Utilities.FUN_2CA94(puVar3.DAT_84_2[0], ccVar2, oVar7);
                        Utilities.ParentChildren(puVar3.DAT_84_2[0], puVar3.DAT_84_2[0]);
                        puVar3.DAT_C0 = oVar7;
                    }

                    maxHalfHealth--;
                    iVar2 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E580(iVar2, arg2.vData.sndList, 3, arg2.vTransform.position);
                    iVar2 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E188(iVar2, arg2.vData.sndList, 2);
                    GameManager.instance.FUN_30CB0(puVar3, 2);
                    puVar3.DAT_C4 = this;
                    GameManager.instance.DAT_1084++;
                }

                return 360;

            case 13:
                if (GameManager.instance.DAT_1084 != 0)
                    return 0;

                if (arg2.physics1.W < 3051 && (arg2.flags & 0x20000000) != 0)
                {
                    local_18 = Utilities.FUN_24304(arg2.vTransform, ((Vehicle)arg2).target.vTransform.position);

                    if (0x63ffe < (uint)local_18.z - 0x19001)
                        return 0;

                    if (local_18.x < 0)
                        local_18.x = -local_18.x;

                    return (uint)(local_18.x * 6 < local_18.z ? 1 : 0);
                }

                break;
        }

        return 0;
    }
}
