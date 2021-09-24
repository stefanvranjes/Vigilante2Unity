using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketL : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_4351C
    public override uint UpdateW(int arg1, int arg2)
    {
        VigTuple ppiVar2;
        List<VigTuple> ppiVar3;
        short sVar4;
        VigObject oVar5;
        int iVar6;
        VigObject oVar8;
        uint uVar9;
        VigObject oVar10;
        int iVar11;
        Rocket rVar11;
        int iVar12;
        VigObject oVar12;
        VigObject oVar13;
        int iVar14;
        VigObject[] local_38;

        switch (arg1)
        {
            case 0:
                FUN_42330(arg2);
                uVar9 = 0;
                break;
            default:
                uVar9 = 0;
                break;
            case 2:
                oVar5 = Utilities.FUN_2CD78(this);
                oVar8 = FUN_4336C((Vehicle)oVar5, 174);
                oVar8.flags |= 0x40000000;
                sVar4 = (short)(maxHalfHealth - 1);
                maxHalfHealth = (ushort)sVar4;

                if (sVar4 != 0)
                {
                    sVar4 = (short)(maxFullHealth - 1);
                    maxFullHealth = (ushort)sVar4;

                    if (sVar4 != 0)
                        GameManager.instance.FUN_30CB0(this, 8);

                    return 120;
                }

                goto LAB_43608;
            case 10:
                arg2 &= 0xfff;

                if (arg2 != 0x422)
                {
                    if (arg2 == 0x423)
                    {
                        oVar8 = Utilities.FUN_2CD78(this);
                        local_38 = new VigObject[3];

                        if (maxHalfHealth < 2)
                            return 0xffffffff;

                        iVar14 = 0;

                        do
                        {
                            iVar11 = 0xc350000;
                            oVar12 = null;
                            ppiVar3 = GameManager.instance.worldObjs;

                            for (int i = 0; i < ppiVar3.Count; i++)
                            {
                                ppiVar2 = ppiVar3[i];
                                oVar10 = ppiVar2.vObject;
                                iVar6 = iVar11;
                                oVar13 = oVar12;

                                if (oVar10.type != 8)
                                {
                                    iVar11 = iVar6;
                                    oVar12 = oVar13;
                                }
                                else if (oVar10.DAT_84 == oVar8 && oVar10 != local_38[0] && oVar10 != local_38[1])
                                {
                                    iVar6 = Utilities.FUN_29F6C(oVar10.vTransform.position, oVar8.vTransform.position);
                                    oVar13 = oVar10;

                                    if (iVar6 < iVar11)
                                    {
                                        iVar11 = iVar6;
                                        oVar12 = oVar13;
                                    }
                                }
                            }

                            local_38[iVar14] = oVar12;
                            rVar11 = FUN_4336C((Vehicle)oVar8, GameManager.DAT_63FC8[iVar14]);
                            rVar11.DAT_1A = 211;
                            rVar11.DAT_84 = oVar12;
                            rVar11.maxHalfHealth = (ushort)((uint)rVar11.maxHalfHealth >> 1);
                            iVar12 = (int)GameManager.FUN_2AC5C();
                            rVar11.physics1.Z += -0x400 + ((iVar12 << 11) >> 15);
                            iVar12 = (int)GameManager.FUN_2AC5C();
                            iVar14++;
                            rVar11.physics1.W += -0x400 + ((iVar12 << 11) >> 15);
                            iVar12 = (int)GameManager.FUN_2AC5C();
                            rVar11.physics2.X += -0x400 + ((iVar12 << 11) >> 15);
                            GameManager.instance.FUN_30CB0(rVar11, 7);
                        } while (iVar14 < 3);

                        sVar4 = (short)(maxHalfHealth - 2);
                        maxHalfHealth = (ushort)sVar4;
                        uVar9 = 30;

                        if (sVar4 == 0)
                        {
                            FUN_3A368();
                            uVar9 = 30;
                        }

                        break;
                    }

                    if (arg2 != 0x424)
                        return 0;

                    if (maxHalfHealth < 2)
                        return 0xffffffff;

                    maxFullHealth = 5;
                    goto case 2;
                }

                if (maxHalfHealth < 2)
                    return 0xffffffff;

                oVar5 = Utilities.FUN_2CD78(this);
                oVar8 = FUN_4336C((Vehicle)oVar5, 187);
                oVar8.flags |= 0x41000000;
                sVar4 = (short)(maxHalfHealth - 2);
                maxHalfHealth = (ushort)sVar4;

                if (sVar4 != 0)
                    return 120;

                LAB_43608:
                FUN_3A368();
                uVar9 = 120;
                break;
        }

        return uVar9;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        short sVar4;
        long lVar7;
        int iVar8;
        uint uVar9;
        Vector3Int local_28;

        switch (arg1)
        {
            case 0:
                FUN_42330(arg2);
                uVar9 = 0;
                break;
            case 1:
                maxHalfHealth = 10;
                goto default;
            default:
                uVar9 = 0;
                break;
            case 12:
                FUN_4336C((Vehicle)arg2, 174);
                sVar4 = (short)(maxHalfHealth - 1);
                LAB_437B0:
                maxHalfHealth = (ushort)sVar4;
                uVar9 = 30;

                if (sVar4 == 0)
                {
                    FUN_3A368();
                    uVar9 = 30;
                }

                break;
            case 13:
                local_28 = Utilities.FUN_24304(arg2.vTransform, ((Vehicle)arg2).target.vTransform.position);
                uVar9 = 0;

                if (local_28.z < 0x1f4000)
                {
                    lVar7 = Utilities.Ratan2(local_28.x, local_28.z);
                    iVar8 = (int)(lVar7 << 20) >> 20;

                    if (iVar8 < 0)
                        iVar8 = -iVar8;

                    uVar9 = (uint)(iVar8 < 113 ? 1 : 0);
                }

                break;
        }

        return uVar9;
    }

    private Rocket FUN_4336C(Vehicle param1, int param2)
    {
        Rocket ppcVar1;
        int iVar2;
        Particle1 oVar2;
        int iVar3;
        ushort uVar4;

        ppcVar1 = LevelManager.instance.FUN_42408(param1, this, (ushort)param2, typeof(Rocket), null) as Rocket;

        if (1 < (uint)param2 - 212)
        {
            oVar2 = LevelManager.instance.FUN_4DE54(ppcVar1.screen, 1);
            oVar2.flags &= 0xffffffef;
            oVar2.vTransform = ppcVar1.vTransform;
        }

        ppcVar1.flags = 0x20000084;
        uVar4 = 80;

        if (param1.doubleDamage != 0)
            uVar4 = 160;

        ppcVar1.maxHalfHealth = uVar4;
        ppcVar1.FUN_305FC();
        iVar2 = param1.physics1.X;

        if (iVar2 < 0)
            iVar2 += 127;

        ppcVar1.physics1.Z = (iVar2 >> 7) + ppcVar1.vTransform.rotation.V02 * 4;
        iVar2 = param1.physics1.Y;

        if (iVar2 < 0)
            iVar2 += 127;

        ppcVar1.physics1.W = (iVar2 >> 7) + ppcVar1.vTransform.rotation.V12 * 4;
        iVar2 = param1.physics1.Z;

        if (iVar2 < 0)
            iVar2 += 127;

        ppcVar1.physics2.X = (iVar2 >> 7) + ppcVar1.vTransform.rotation.V22 * 4;
        ppcVar1.physics2.M2 = 240;
        iVar3 = GameManager.instance.FUN_1DD9C();
        GameManager.instance.FUN_1E580(iVar3, GameManager.instance.DAT_C2C, 52, ppcVar1.screen);
        param1.FUN_2B1FC(GameManager.DAT_A30, screen);
        return ppcVar1;
    }
}
