using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : Vehicle
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //0x5230 (ROUTE66.DLL)
    private static VehicleData DAT_5230 = new VehicleData()
    {
        DAT_00 = new short[6] { 4, 4, 50, 50, 200, 200 },
        DAT_0C = 12,
        vehicleID = _VEHICLE.NONE,
        DAT_0E = 14,
        DAT_0F = 0,
        DAT_10 = 32,
        DAT_11 = 32,
        DAT_12 = 32,
        DAT_13 = 120,
        DAT_15 = 46,
        maxHalfHealth = 900,
        lightness = 3458,
        DAT_24 = new Vector3Int(100, 64, 100), //perhaps an error, since 0x40 is 64?
        DAT_2A = 0x5000,
        DAT_2C = new byte[4] { 17, 10, 10, 12 }
    };

    public int counter; //not in the original code

    public static VigObject OnInitialize(XOBF_DB arg1, int arg2, uint arg3)
    {
        VigObject oVar8;

        oVar8 = arg1.FUN_3C464((ushort)arg2, DAT_5230, typeof(Police));
        return oVar8;
    }

    public override uint OnCollision(HitDetection hit)
    {
        int iVar5;
        int iVar6;
        uint uVar7;
        uint uVar8;
        VigObject oVar10;
        Vehicle vVar10;
        VigObject oVar12;

        oVar10 = hit.self;

        if (oVar10.type != 8)
        {
            if (oVar10.type == 2)
            {
                vVar10 = (Vehicle)oVar10;

                if (vVar10.id < 0)
                    ; //FUN_32758

                uVar7 = GameManager.FUN_2AC5C();

                if ((uVar7 & 1) != 0)
                    vVar10.FUN_39714(vTransform.position);
            }

            uVar8 = (uint)FUN_3B424(this, hit);
            return uVar8;
        }

        if ((oVar10.flags & 0x20000000) != 0)
        {
            oVar12 = oVar10.DAT_80;
            iVar5 = Utilities.FUN_29F6C(oVar12.vTransform.position, vTransform.position);
            iVar6 = (int)GameManager.FUN_2AC5C();

            if (iVar6 * 10 >> 15 == 0)
            {
                if (counter == 0)
                {
                    uVar8 = (uint)GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E580((int)uVar8, LevelManager.instance.xobfList[42].sndList, 4, vTransform.position);
                    counter = 400;
                }
            }

            if (tags == 2)
            {
                if (iVar5 < 0xfa000)
                {
                    if ((uint)maxFullHealth >> 2 < maxHalfHealth &&
                        GetType().IsSubclassOf(typeof(VigObject)))
                        UpdateW(20, oVar12);
                }
            }
            else
            {
                if (iVar5 < 0x1f4000)
                    if ((uint)maxFullHealth >> 2 < maxHalfHealth &&
                        GetType().IsSubclassOf(typeof(VigObject)))
                        UpdateW(20, oVar12);
            }
        }

        FUN_32B90(oVar10.maxHalfHealth);
        //FUN_32B90?
        return 0;
    }

    //FUN_3BDC (ROUTE66.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        bool bVar1;
        short sVar3;
        int iVar4;
        VigObject oVar4;
        int iVar5;
        int iVar6;
        uint uVar7;
        uint uVar8;
        VigObject oVar8;
        short sVar9;
        int iVar10;
        uint uVar11;
        VigObject pcVar11;
        VigMesh mVar11;
        int iVar12;

        switch (arg1)
        {
            case 0:
                if (counter != 0)
                    counter--;

                if ((GameManager.instance.DAT_28 - DAT_19 & 127) == 0 && 
                    (ai.DAT_00 < 1 || (flags & 0x20000000) != 0))
                {
                    pcVar11 = target;

                    if (tags == 1)
                    {
                        iVar4 = vTransform.position.x - pcVar11.vTransform.position.x;

                        if (iVar4 < 0)
                            iVar4 = -iVar4;

                        if (iVar4 < 0xa000)
                        {
                            FUN_30BA8();
                            tags = 0;

                            if (DAT_1A != 20)
                            {
                                GameManager.instance.FUN_1FEB8(vMesh);
                                DAT_1A = 20;
                                mVar11 = vData.FUN_2CB74(gameObject, 20, true);
                                vMesh = mVar11;
                                maxHalfHealth = maxFullHealth;
                            }

                            FUN_30C68();
                            return 0;
                        }

                        iVar4 = vTransform.position.z - pcVar11.vTransform.position.z;

                        if (iVar4 < 0)
                            iVar4 = -iVar4;

                        if (iVar4 < 0xa000)
                        {
                            FUN_30BA8();
                            tags = 0;

                            if (DAT_1A != 20)
                            {
                                GameManager.instance.FUN_1FEB8(vMesh);
                                DAT_1A = 20;
                                mVar11 = vData.FUN_2CB74(gameObject, 20, true);
                                vMesh = mVar11;
                                maxHalfHealth = maxFullHealth;
                            }

                            FUN_30C68();
                            return 0;
                        }
                    }
                    else
                    {
                        iVar4 = vTransform.position.x - pcVar11.vTransform.position.x;

                        if (iVar4 < 0)
                            iVar4 = -iVar4;

                        if (iVar4 < 0x2ee001)
                        {
                            iVar4 = vTransform.position.z - pcVar11.vTransform.position.z;

                            if (iVar4 < 0)
                                iVar4 = -iVar4;

                            if (iVar4 < 0x2ee001) goto LAB_3E3C;
                        }

                        oVar4 = child2;
                        iVar5 = 0;
                        oVar4.FUN_30C20();
                        FUN_30C68();
                        oVar4.flags |= 2;
                        GameManager.instance.FUN_1DE78(DAT_18);
                        DAT_18 = 0;
                        tags = 1;
                        iVar10 = (int)GameManager.FUN_2AC5C();
                        DAT_F4 = (short)((iVar10 * 3 >> 15) + 97);

                        do
                        {
                            oVar4 = GameManager.instance.FUN_30250(GameManager.instance.DAT_1078, DAT_F4);
                            target = oVar4;

                            if (oVar4 != null) break;

                            sVar3 = DAT_F4;
                            DAT_F4 = (short)(sVar3 + 1);

                            if (99 < sVar3 + 1)
                                DAT_F4 = 97;

                            iVar5++;
                        } while (iVar5 < 3);

                        if (target == null)
                        {
                            oVar4 = GameManager.instance.FUN_30250(GameManager.instance.DAT_1078, 1);
                            target = oVar4;
                        }
                    }

                    LAB_3E3C:
                    ai.FUN_51C54(vTransform.position, pcVar11.vTransform.position, 0x22740, 0);
                }

                direction = 1;
                sVar3 = (short)ai.FUN_51CFC(this, physics1.W * 32 + 0x10000);
                iVar10 = sVar3;
                iVar5 = -682;

                if (-683 < iVar10)
                {
                    iVar5 = 682;

                    if (iVar10 < 683)
                        iVar5 = iVar10;
                }

                turning = (short)iVar5;
                iVar6 = physics1.W * DAT_B2;

                if (iVar6 < 0)
                    iVar6 += 4095;

                iVar12 = DAT_B1 + (iVar6 >> 12);
                iVar6 = 0;

                if (0 < iVar12)
                    iVar6 = iVar12;

                iVar5 *= iVar6;

                if (iVar5 < 0)
                    iVar5 += 15;

                physics2.Y += iVar5 >> 4;

                if (iVar10 < 0)
                    iVar10 = -iVar10;

                if (iVar10 < 342 || physics1.W < 3052)
                {
                    if (physics1.W < 7630)
                    {
                        iVar10 = 0;

                        if (0 < acceleration)
                            iVar10 = acceleration;

                        uVar7 = DAT_B3;
                        iVar10++;
                        bVar1 = iVar10 < (int)uVar7;
                        sVar9 = (short)uVar7;

                        if (bVar1)
                            sVar9 = (short)iVar10;

                        acceleration = sVar9;
                    }
                    else
                    {
                        iVar5 = acceleration - 1;
                        iVar10 = -DAT_B3;

                        if (-DAT_B3 < iVar5)
                            iVar10 = iVar5;

                        acceleration = (short)iVar10;
                    }
                }
                else
                {
                    iVar10 = 0;

                    if (acceleration < 0)
                        iVar10 = acceleration;

                    iVar10--;
                    uVar7 = (uint)-DAT_B3;
                    bVar1 = (int)uVar7 < iVar10;
                    sVar9 = (short)uVar7;

                    if (bVar1)
                        sVar9 = (short)iVar10;

                    acceleration = sVar9;
                }

                FUN_41AE8();

                if (arg2 != 0)
                {
                    uVar11 = flags & 0xfeff7fff;

                    if ((flags & 0x1000000) != 0)
                        uVar11 |= 0x8000;

                    flags = uVar11;
                    uVar8 = GameManager.instance.FUN_1E478(vTransform.position);
                    GameManager.instance.FUN_1E2C8(DAT_18, uVar8);
                    return 0;
                }

                break;
            case 1:
                if (arg2 != 0)
                {
                    if (id != 97)
                        return 0;

                    oVar8 = FUN_31DDC(OnInitialize);
                    oVar8.FUN_3066C();
                    return 0;
                }

                type = 10;
                iVar10 = GameManager.instance.terrain.FUN_1B750((uint)screen.x, (uint)screen.z);
                screen.y = iVar10 - DAT_E4;
                vTransform.position.y = iVar10 - DAT_E4;
                flags |= 0x108;
                child2.flags |= 2;
                DAT_F4 = id;
                break;
            case 2:
                pcVar11 = child2;
                iVar5 = 0;
                pcVar11.FUN_30C20();
                pcVar11.flags |= 2;
                GameManager.instance.FUN_1DE78(DAT_18);
                DAT_18 = 0;
                tags = 1;
                iVar10 = (int)GameManager.FUN_2AC5C();
                DAT_F4 = (short)((iVar10 * 3 >> 15) + 97);

                do
                {
                    pcVar11 = GameManager.instance.FUN_30250(GameManager.instance.DAT_1078, DAT_F4);
                    target = pcVar11;

                    if (pcVar11 != null) break;

                    sVar3 = DAT_F4;
                    DAT_F4 = (short)(sVar3 + 1);

                    if (99 < sVar3 + 1)
                        DAT_F4 = 97;

                    iVar5++;
                } while (iVar5 < 3);

                if (target != null)
                    return 0;

                pcVar11 = GameManager.instance.FUN_30250(GameManager.instance.DAT_1078, 1);
                target = pcVar11;
                break;
            case 4:
                if (type == 5)
                    return 0;

                GameManager.instance.FUN_1DE78(DAT_18);
                FUN_38484();
                break;
            case 8:
                FUN_32B90((uint)arg2);
                return 0;
        }

        return 0;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        sbyte sVar2;

        switch (arg1)
        {
            case 20:
                target = arg2;

                if (tags == 0)
                {
                    flags &= 0xfffffcff;
                    FUN_30B78();
                }

                if (tags < 2)
                {
                    child2.flags &= 0xfffffffd;
                    child2.FUN_30BF0();
                    sVar2 = (sbyte)GameManager.instance.FUN_1DD9C();
                    DAT_18 = sVar2;
                    GameManager.instance.FUN_1E098(sVar2, LevelManager.instance.xobfList[42].sndList, 5, 0, true);
                    GameManager.instance.FUN_30CB0(this, 2700);
                }

                tags = 2;
                break;
        }

        return 0;
    }
}
