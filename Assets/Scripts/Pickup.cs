using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override uint OnCollision(HitDetection param1)
    {
        bool bVar1;
        VigObject oVar2;
        ConfigContainer ccVar2;
        int iVar3;
        VigObject oVar3;
        uint uVar4;
        int iVar5;
        int iVar6;
        VigObject oVar8;
        Vehicle vVar8;

        oVar8 = param1.self;

        if (oVar8.type != 2 || oVar8.maxHalfHealth == 0)
        {
            if (oVar8.type == 8)
                return 1;

            return 0;
        }

        uVar4 = 6;

        if (tags == 0 || oVar8.id < 1)
        {
            uVar4 = (ushort)DAT_1A;
            bVar1 = uVar4 < 24;
        }
        else
            bVar1 = true;

        if (bVar1)
        {
            switch (uVar4)
            {
                case 0:
                    

                    iVar3 = 500;

                    if (GameManager.instance.gameMode == _GAME_MODE.Survival)
                    {
                        if (oVar8.id < 0)
                        {
                            iVar5 = (int)((long)GameManager.instance.DAT_CC4 * 0x51eb851f) >> 32;
                            iVar5 = (iVar5 >> 4) - (GameManager.instance.DAT_CC4 >> 31);
                            iVar6 = 2;

                            if (iVar5 < iVar6)
                                iVar6 = iVar5;

                            iVar3 >>= iVar6;
                        }
                    }

                    vVar8 = (Vehicle)oVar8;
                    vVar8.FUN_3A0C0(iVar3);
                    LAB_49ED0:
                    //sound
                    //flash
                    if ((flags & 0x1000000) != 0)
                        GameManager.instance.DAT_10F0--;

                    GameManager.instance.FUN_309A0(this);
                    oVar2 = GameManager.instance.FUN_30250(GameManager.instance.DAT_1078, id);

                    if (oVar2 != null)
                        GameManager.instance.FUN_30CB0(oVar2, 600);

                    return 0xfffffffe;
                case 1:
                case 5:
                    return 0;
                case 2:
                    vVar8 = (Vehicle)oVar8;
                    vVar8.jammer = 900;
                    goto LAB_49ED0;
                case 3:
                    vVar8 = (Vehicle)oVar8;
                    vVar8.doubleDamage = 900;
                    goto LAB_49ED0;
                case 4:
                    vVar8 = (Vehicle)oVar8;
                    vVar8.shield = 900;
                    goto LAB_49ED0;
                case 10:
                    iVar3 = 2;
                    goto LAB_4A180;
                case 11:
                    iVar3 = 3;
                    goto LAB_4A180;
                case 12:
                    iVar3 = 1;
                    goto LAB_4A180;
                case 13:
                    iVar3 = 5;
                    goto LAB_4A180;
                case 14:
                    iVar3 = 4;
                    goto LAB_4A180;
                case 15:
                    iVar3 = 6;
                    goto LAB_4A180;
                case 16:
                    iVar3 = 0;
                    goto LAB_4A180;
                case 6:
                    iVar3 = 7;
                    LAB_4A180:
                    ccVar2 = oVar8.FUN_4AE5C(iVar3);

                    if (ccVar2 != null)
                    {
                        oVar3 = oVar8.FUN_4AE94(iVar3);

                        if (oVar3 != null)
                        {
                            oVar3.CCDAT_74 = ccVar2;
                            oVar3.vr = ccVar2.v3_2;
                            oVar3.flags |= 0x1000000;
                            oVar3.maxFullHealth = oVar3.maxHalfHealth;

                            if (maxHalfHealth != 0)
                                oVar3.maxHalfHealth = maxHalfHealth;

                            oVar3.screen.x = screen.x - oVar8.screen.x;
                            oVar3.screen.y = screen.y - oVar8.screen.y;
                            oVar3.screen.z = screen.z - oVar8.screen.z;
                            oVar3.ApplyTransformation();

                            if ((oVar3.flags & 0x80) == 0)
                                oVar3.FUN_30B78();

                            vVar8 = (Vehicle)oVar8;
                            vVar8.FUN_3A3D4(oVar3);
                            //sound

                            if (vVar8.id == -1)
                                ; //FUN_4E414
                        }
                    }

                    if ((flags & 0x1000000) != 0)
                        GameManager.instance.DAT_1038--;

                    GameManager.instance.FUN_309A0(this);
                    oVar2 = GameManager.instance.FUN_30250(GameManager.instance.DAT_1078, id);

                    if (oVar2 != null)
                        GameManager.instance.FUN_30CB0(oVar2, 600);

                    return 0xfffffffe;
            }
        }

        return 0;
    }

    //FUN_49D54
    public override uint UpdateW(int arg1, int arg2)
    {
        ushort uVar2;
        short sVar3;
        int iVar5;
        int iVar6;
        ushort uVar7;
        int iVar8;

        switch (arg1)
        {
            case 0:
                vr.y += 68;

                if (arg2 != 0)
                {
                    ApplyTransformation();
                    return 0;
                }

                break;
            case 1:
                LevelManager.instance.DAT_1178 = vData;

                if (arg2 == 0)
                {
                    type = 3;
                    flags = flags & 0xfffffff7 | 0x380;

                    if (DAT_6C == 0)
                        DAT_6C = 0x1f4000;

                    sVar3 = DAT_1A;

                    if (sVar3 == 5)
                    {
                        iVar8 = (int)GameManager.instance.FUN_4A970(flags, (ushort)sVar3);
                        DAT_1A = GameManager.DAT_63FA4[iVar8];
                        sVar3 = DAT_1A;
                    }

                    if (sVar3 != 8)
                        return 0;

                    iVar5 = screen.x;
                    iVar6 = screen.z;
                    uVar7 = maxHalfHealth;
                }
                else
                {
                    if ((flags & 0x1000000) == 0)
                        return 0;

                    uVar2 = (ushort)LevelManager.instance.FUN_35778(screen.x, screen.z);
                    iVar5 = screen.x;
                    iVar6 = screen.z;
                    uVar7 = 0;
                    maxHalfHealth = uVar7;
                }

                LevelManager.instance.FUN_359FC(iVar5, iVar6, uVar7);
                break;
            case 2:
                if (type != 3)
                {
                    flags &= 0xffff7fff;
                    return 0xffffffff;
                }

                LevelManager.instance.FUN_4DF20(screen, 13, 2048);
                GameManager.instance.FUN_309A0(this);
                return 0xffffffff;
        }

        return 0;
    }
}
