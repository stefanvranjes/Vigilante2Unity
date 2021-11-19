using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disco : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_6CC (MARATHON.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        switch (arg1)
        {
            case 0:
                FUN_42330(arg2);
                break;
            case 4:
                GameManager.instance.DAT_1084--;
                break;
        }

        return 0;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        Disco2 puVar1;
        Ballistic puVar2;
        int iVar3;
        ConfigContainer ccVar3;
        uint uVar4;
        Disco3 puVar5;
        int iVar6;
        VigObject oVar6;

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
            case 5:
                child2.vAnim = null;
                FUN_30C20();
                uVar4 = 0xffffffff;
                break;
            case 12:
                ccVar3 = FUN_2C5F4(0x8000);
                Dictionary<int, Type> dict = new Dictionary<int, Type>();
                dict.Add(72, typeof(Disco3));
                dict.Add(85, typeof(Disco3));
                puVar1 = vData.ini.FUN_2C17C(4, typeof(Disco2), 8, dict) as Disco2;
                puVar2 = LevelManager.instance.xobfList[19].ini.FUN_2C17C(1, typeof(Ballistic), 8) as Ballistic;
                puVar1.id = arg2.id;
                puVar1.screen = ccVar3.v3_1;
                puVar5 = puVar1.child2 as Disco3;
                puVar1.maxHalfHealth = 28;
                puVar1.physics1.W -= 0x800;
                oVar6 = ((Vehicle)arg2).target;
                puVar1.DAT_80 = arg2;
                puVar1.DAT_84 = oVar6;

                while (puVar5 != null)
                {
                    if ((puVar5.flags & 0x10) != 0)
                        puVar5.flags |= 0x400;

                    puVar5 = puVar5.child as Disco3;
                }

                puVar1.FUN_2D1DC();
                puVar1.FUN_30B78();
                Utilities.FUN_2CC48(this, puVar1);
                puVar2.flags |= 0x10;
                Utilities.FUN_2CA94(this, ccVar3, puVar2);
                Utilities.ParentChildren(this, this);
                child2.FUN_2C05C();
                iVar6 = GameManager.instance.FUN_1DD9C();
                GameManager.instance.FUN_1E188(iVar6, vData.sndList, 2);
                uVar4 = 540;
                maxHalfHealth--;
                flags |= 0x8000000;
                GameManager.instance.DAT_1084++;

                if (arg2.id < 0)
                    uVar4 = 360;

                break;
            case 13:
                uVar4 = 0;

                if (GameManager.instance.DAT_1084 == 0)
                {
                    iVar3 = Utilities.FUN_29F6C(arg2.screen, ((Vehicle)arg2).target.screen);

                    if (iVar3 < 0x12c000)
                        uVar4 = 0x32000 < iVar3 ? 1U : 0;
                }

                break;
        }

        return uVar4;
    }
}
