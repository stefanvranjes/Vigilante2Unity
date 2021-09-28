using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloryRockets : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_101C (STNTBIKE.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        uint uVar4;

        switch (arg1)
        {
            case 0:
                FUN_42330(arg2);
                uVar4 = 0;
                break;
            default:
                uVar4 = 0;
                break;
            case 14:
                uVar4 = 6;
                break;
        }

        return uVar4;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        GloryRocket puVar1;
        VigObject oVar2;
        int iVar3;
        VigObject oVar3;
        uint uVar4;
        int iVar5;
        VigObject oVar5;
        int iVar6;
        VigObject oVar6;
        short sVar9;
        VigObject oVar10;

        switch (arg1)
        {
            case 0:
                FUN_42330(arg2);
                uVar4 = 0;
                break;
            case 1:
                maxHalfHealth = 3;
                flags |= 0x4000;
                goto default;
            default:
                uVar4 = 0;
                break;
            case 12:
                if ((flags & 0x80) == 0)
                {
                    iVar6 = 0;
                    sVar9 = 1;
                    oVar5 = child2;

                    do
                    {
                        oVar10 = oVar5.child;
                        puVar1 = oVar5.vData.ini.FUN_2C17C((ushort)oVar5.DAT_1A, typeof(GloryRocket), 0) as GloryRocket;
                        puVar1.id = 3;
                        puVar1.vTransform = oVar5.vTransform;
                        puVar1.screen = oVar5.vTransform.position;
                        puVar1.type = 8;
                        puVar1.maxHalfHealth = 100;
                        puVar1.DAT_19 = (byte)iVar6;
                        puVar1.DAT_80 = arg2;
                        puVar1.flags |= 0x60000020;
                        oVar2 = ((Vehicle)arg2).target;

                        if (((Vehicle)arg2).target == null)
                            oVar2 = arg2;

                        puVar1.DAT_84 = oVar2;
                        puVar1.physics2.M3 = sVar9;
                        puVar1.FUN_30B78();

                        if (maxHalfHealth < 2)
                        {
                            oVar3 = oVar5.FUN_2CCBC();
                            GameManager.instance.FUN_2C4B4(oVar3);
                        }

                        Utilities.FUN_2CC48(this, puVar1);
                        Utilities.ParentChildren(this, this);
                        GameManager.instance.DAT_1084++;
                        iVar5 = (int)GameManager.FUN_2AC5C();
                        sVar9 += 15;
                        iVar6++;
                        puVar1.vr.x = 0x1000 / ((iVar5 * 15 >> 15) + 15);
                        GameManager.instance.FUN_30CB0(puVar1, 480);
                        oVar5 = oVar10;
                    } while (iVar6 < 3);

                    DAT_19 = 0;
                    maxHalfHealth--;
                    iVar3 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E188(iVar3, vData.sndList, 2);
                }
                else
                {
                    iVar3 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E14C(iVar3, GameManager.instance.DAT_C2C, 1);
                }

                uVar4 = 780;

                if (arg2.id < 0)
                    uVar4 = 390;

                break;
            case 13:
                uVar4 = 0;

                if (GameManager.instance.DAT_1084 == 0)
                {
                    iVar5 = Utilities.FUN_29F6C(arg2.screen, ((Vehicle)arg2).target.screen);

                    if (iVar5 < 0x3e8000)
                        uVar4 = (uint)(0x96000 < iVar5 ? 1 : 0);
                }

                break;
            case 14:
                uVar4 = 6;
                break;
            case 16:
                iVar5 = child2.id;

                if (iVar5 != 0)
                {
                    oVar6 = arg2.child2;
                    sVar9 = oVar6.id;

                    while(sVar9 != iVar5 - 1)
                    {
                        oVar6 = oVar6.child;
                        sVar9 = oVar6.id;
                    }

                    oVar3 = oVar6.FUN_2CCBC();
                    Utilities.FUN_2CC9C(this, oVar3);
                    Utilities.ParentChildren(this, this);
                    return 0;
                }

                goto default;
        }

        return uVar4;
    }
}
