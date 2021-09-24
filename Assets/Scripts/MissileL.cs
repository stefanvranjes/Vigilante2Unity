using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileL : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_44790
    public override uint UpdateW(int arg1, int arg2)
    {
        short sVar1;
        int iVar2;
        Vehicle vVar2;
        ConfigContainer ccVar2;
        Afterburner ppcVar3;
        Vehicle pcVar4;
        Missile mVar5;
        Vehicle vVar6;
        Missile mVar6;
        uint uVar7;
        ushort uVar8;
        ushort uVar9;
        Matrix3x3 MStack32;

        switch (arg1)
        {
            case 0:
                FUN_42330(arg2);
                uVar7 = 0;
                break;
            default:
                uVar7 = 0;
                break;
            case 2:
                vVar6 = Utilities.FUN_2CD78(this) as Vehicle;
                mVar5 = FUN_445B0(vVar6, 188);
                iVar2 = GameManager.instance.FUN_1DD9C();
                GameManager.instance.FUN_1E580(iVar2, GameManager.instance.DAT_C2C, 51, mVar5.screen);
                mVar5.flags |= 0x40000000;
                uVar8 = 60;

                if (vVar6.doubleDamage != 0)
                    uVar8 = 120;

                mVar5.maxHalfHealth = uVar8;
                sVar1 = (short)(maxFullHealth - 1);
                maxFullHealth = (ushort)sVar1;

                if (sVar1 != 0)
                    GameManager.instance.FUN_30CB0(this, 8);

                MStack32 = GameManager.FUN_2A39C().rotation;
                MStack32 = Utilities.RotMatrixY(maxFullHealth * 0x100 - 0x180, MStack32);
                mVar5.vTransform.rotation = Utilities.MulMatrix(mVar5.vTransform.rotation, MStack32);
                uVar7 = 120;
                break;
            case 10:
                arg2 &= 0xfff;

                if (arg2 == 0x444)
                {
                    if (1 < maxHalfHealth)
                    {
                        ppcVar3 = vData.ini.FUN_2C17C(186, typeof(Afterburner), 8) as Afterburner;
                        pcVar4 = Utilities.FUN_2CD78(this) as Vehicle;
                        ppcVar3.PDAT_78 = pcVar4;
                        ppcVar3.id = 120;
                        ccVar2 = FUN_2C5F4(0x8001);
                        Utilities.FUN_2CA94(this, ccVar2, ppcVar3);
                        ppcVar3.transform.parent = transform;
                        ppcVar3.FUN_30B78();
                        ppcVar3.FUN_30BF0();
                        maxHalfHealth -= 2;
                        pcVar4.FUN_39B50();
                        return 240;
                    }
                }
                else
                {
                    if (arg2 == 0x442)
                    {
                        if (1 < maxHalfHealth)
                        {
                            maxHalfHealth--;
                            vVar2 = Utilities.FUN_2CD78(this) as Vehicle;
                            mVar6 = FUN_445B0(vVar2, 199);
                            mVar6.DAT_84 = vVar2;
                            mVar6.flags |= 0x41000000;
                            iVar2 = GameManager.instance.FUN_1DD9C();
                            GameManager.instance.FUN_1E580(iVar2, GameManager.instance.DAT_C2C, 67, mVar6.screen);
                            return 120;
                        }
                    }
                    else
                    {
                        if (arg2 != 0x443)
                            return 0;

                        if (1 < maxHalfHealth)
                        {
                            uVar9 = 4;

                            if (maxHalfHealth < 4)
                                uVar9 = maxHalfHealth;

                            maxFullHealth = uVar9;
                            goto case 2;
                        }
                    }
                }

                uVar7 = 0xffffffff;
                break;
        }

        return uVar7;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        int iVar2;
        int iVar6;
        VigObject oVar6;
        uint uVar7;
        ushort uVar8;

        switch (arg1)
        {
            case 0:
                FUN_42330(arg2);
                uVar7 = 0;
                break;
            case 1:
                maxHalfHealth = 12;
                flags |= 0x4000;
                goto default;
            default:
                uVar7 = 0;
                break;
            case 12:
                oVar6 = FUN_445B0((Vehicle)arg2, 188);
                uVar8 = 60;

                if (((Vehicle)arg2).doubleDamage != 0)
                    uVar8 = 120;

                oVar6.maxHalfHealth = uVar8;
                iVar2 = GameManager.instance.FUN_1DD9C();
                GameManager.instance.FUN_1E580(iVar2, GameManager.instance.DAT_C2C, 51, oVar6.screen);
                uVar7 = 90;
                break;
            case 13:
                iVar6 = Utilities.FUN_29F6C(arg2.screen, ((Vehicle)arg2).target.screen);
                uVar7 = (uint)(0x3b5ffe < (uint)(iVar6 - 0x32001 ^ 1) ? 1 : 0);
                break;
        }

        return uVar7;
    }

    private Missile FUN_445B0(Vehicle param1, short param2)
    {
        Missile ppcVar2;
        int iVar3;
        VigObject pcVar4;
        int iVar5;
        Vector3Int local_18;

        ppcVar2 = LevelManager.instance.FUN_42408(param1, this, (ushort)param2, typeof(Missile), null) as Missile;
        local_18 = new Vector3Int(
            ppcVar2.vTransform.rotation.V01 << 5,
            ppcVar2.vTransform.rotation.V11 << 5,
            ppcVar2.vTransform.rotation.V21 << 5);
        ppcVar2.flags = 0x20000084;
        ppcVar2.FUN_305FC();
        iVar5 = param1.physics1.X;

        if (iVar5 < 0)
            iVar5 += 127;

        iVar3 = ppcVar2.vTransform.rotation.V01 * 1750;

        if (iVar3 < 0)
            iVar3 += 4095;

        ppcVar2.physics1.Z = (iVar5 >> 7) - (iVar3 >> 12);
        iVar5 = param1.physics1.Y;

        if (iVar5 < 0)
            iVar5 += 127;

        iVar3 = ppcVar2.vTransform.rotation.V11 * 1750;

        if (iVar3 < 0)
            iVar3 += 4095;

        ppcVar2.physics1.W = (iVar5 >> 7) - (iVar3 >> 12);
        iVar5 = param1.physics1.Z;

        if (iVar5 < 0)
            iVar5 += 127;

        iVar3 = ppcVar2.vTransform.rotation.V21 * 1750;

        if (iVar3 < 0)
            iVar3 += 4095;

        ppcVar2.physics2.X = (iVar5 >> 7) - (iVar3 >> 12);
        pcVar4 = param1.target;

        if (param1.target == null)
            pcVar4 = param1;

        ppcVar2.DAT_84 = pcVar4;
        param1.FUN_2B370(local_18, ppcVar2.screen);
        maxHalfHealth--;

        if (maxHalfHealth == 0)
            FUN_3A368();

        return ppcVar2;
    }
}
