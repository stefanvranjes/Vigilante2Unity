using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingL : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_D14 (ELGUERRO.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        sbyte sVar1;
        short sVar2;
        int iVar5;
        VigObject oVar5;

        switch (arg1)
        {
            case 0:
                iVar5 = FUN_42330(arg2);

                if (iVar5 == 0)
                    return 0;

                if (arg2 < 0)
                    return 0;

                oVar5 = child2;

                if (DAT_19 < 30)
                    sVar2 = (short)(oVar5.vr.x + 22);
                else
                {
                    if (DAT_19 < 61) goto LAB_DB8;

                    sVar2 = (short)(oVar5.vr.x - 22);
                }

                oVar5.vr.x = sVar2;
                LAB_DB8:

                if (arg2 != 0)
                    oVar5.ApplyTransformation();

                sVar1 = (sbyte)(DAT_19 + 1);
                DAT_19 = (byte)sVar1;

                if (sVar1 != 91)
                    return 0;

                FUN_30BA8();
                return 0;
        }

        return 0;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        short sVar2;
        Lemming puVar3;
        int iVar4;
        int iVar5;
        VigObject oVar4;
        VigObject oVar5;
        VigObject oVar6;

        switch (arg1)
        {
            case 0:
                iVar5 = FUN_42330(arg2);

                if (iVar5 == 0)
                    return 0;

                break;
            case 1:
                maxHalfHealth = 3;
                flags |= 0x4000;
                break;
            case 12:
                if ((flags & 0x80) == 0)
                {
                    oVar6 = child2.child2;
                    puVar3 = oVar6.vData.ini.FUN_2C17C((ushort)oVar6.DAT_1A, typeof(Lemming), 0) as Lemming;
                    puVar3.id = 3;
                    puVar3.vTransform = oVar6.vTransform;
                    puVar3.type = 8;
                    puVar3.maxHalfHealth = 100;
                    puVar3.DAT_80 = arg2;
                    puVar3.flags |= 0x60000020;
                    oVar5 = ((Vehicle)arg2).target;

                    if (((Vehicle)arg2).target == null)
                        oVar5 = arg2;

                    puVar3.DAT_84 = oVar5;
                    puVar3.physics2.M3 = 30;
                    puVar3.FUN_30B78();

                    if (maxHalfHealth < 4)
                    {
                        oVar4 = oVar6.FUN_2CCBC();
                        oVar6.transform.parent = null;
                        GameManager.instance.FUN_2C4B4(oVar4);
                    }

                    Utilities.FUN_2CC48(child2, puVar3);
                    Utilities.ParentChildren(child2, child2);
                    GameManager.instance.DAT_1084++;
                    maxHalfHealth--;
                    DAT_19 = 0;
                    FUN_30B78();
                    iVar4 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E188(iVar4, vData.sndList, 2);
                }
                else
                {
                    //iVar4 = GameManager.instance.FUN_1DD9C();
                    //GameManager.instance.FUN_1E14C(iVar4, GameManager.instance.DAT_C2C, 1);
                    return 0;
                }

                if (-1 < arg2.id)
                    return 780;

                return 540;
            case 13:
                if (GameManager.instance.DAT_1084 != 0)
                    return 0;

                iVar5 = Utilities.FUN_29F6C(arg2.screen, ((Vehicle)arg2).target.screen);

                if (0x3e7fff < iVar5)
                    return 0;

                return (uint)(0x96000 < iVar5 ? 1 : 0);
            case 16:
                iVar5 = child2.child2.id;

                if (iVar5 != 0)
                {
                    oVar6 = arg2.child2.child2;
                    sVar2 = oVar6.id;

                    while(sVar2 != iVar5 - 1)
                    {
                        oVar6 = oVar6.child;
                        sVar2 = oVar6.id;
                    }

                    oVar4 = oVar6.FUN_2CCBC();
                    oVar6.transform.parent = null;
                    Utilities.FUN_2CC9C(child2, oVar4);
                    Utilities.ParentChildren(child2, child2);
                    return 0;
                }

                break;
        }

        return 0;
    }
}
