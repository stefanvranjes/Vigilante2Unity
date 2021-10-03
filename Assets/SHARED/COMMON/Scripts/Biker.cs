using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biker : VigObject
{
    //FUN_294 (STNTBIKE.DLL)
    public static Vehicle LoadDakota(XOBF_DB param1, int param2)
    {
        VigObject puVar1;
        Vehicle vVar2;
        ConfigContainer ccVar2;

        vVar2 = param1.FUN_3C464(0, GameManager.vehicleConfigs[2], typeof(Vehicle));
        puVar1 = vVar2.vData.ini.FUN_2C17C(1, typeof(Biker), 8);
        Utilities.ParentChildren(puVar1, puVar1);
        puVar1.flags |= 4;
        ccVar2 = vVar2.FUN_2C5F4(0x8102);
        Utilities.FUN_2CA94(vVar2, ccVar2, puVar1);
        Utilities.ParentChildren(vVar2, puVar1);
        puVar1.FUN_30B78();
        return vVar2;
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_A0 (STNTBIKE.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        byte bVar1;
        uint uVar2;
        Vehicle vVar3;
        uint uVar4;
        int iVar5;

        if (arg1 == 0)
        {
            uVar2 = 0;

            if (arg2 != 0)
            {
                vVar3 = (Vehicle)Utilities.FUN_2CD78(this);
                uVar4 = flags;
                iVar5 = (vVar3.turning << 3) / 682 + 8;
                flags = uVar4 | 4;

                if (iVar5 == tags)
                {
                    uVar2 = 0;

                    if (iVar5 == 8)
                    {
                        bVar1 = DAT_19;

                        if (bVar1 == 0)
                        {
                            if (DAT_1A == 3)
                                return 0;

                            if (0 < vVar3.physics1.Y)
                            {
                                if ((vVar3.flags & 0x10000000) != 0)
                                    return 0;

                                if ((uVar4 & 0x1000000) != 0)
                                    return 0;

                                flags = uVar4 | 0x1000004;
                                FUN_2C124(3);
                                Utilities.ParentChildren(this, this);
                                return 0;
                            }
                        }
                        else
                        {
                            DAT_19 = (byte)(bVar1 - 1);

                            if (bVar1 != 1)
                                return 0;

                            FUN_2C124(2);
                            Utilities.ParentChildren(this, this);
                            GameManager.instance.FUN_30080(GameManager.instance.DAT_10A8, this);
                            flags &= 0xfeffffff;
                        }

                        uVar2 = 0;
                    }
                }
                else
                {
                    if (DAT_1A == 1)
                    {
                        if (iVar5 < tags)
                            GameManager.instance.FUN_2C0A0(child2);
                    }
                    else
                    {
                        FUN_2C124(1);
                        Utilities.ParentChildren(this, this);
                        GameManager.instance.FUN_300B8(GameManager.instance.DAT_10A8, this);
                    }

                    DAT_19 = 15;
                    tags = (sbyte)iVar5;
                    GameManager.instance.FUN_2FEE8(this, (ushort)(child2.DAT_4A + iVar5 * 2));
                    uVar2 = 0;
                }
            }
        }
        else
            uVar2 = 0;

        return uVar2;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        uint uVar2;

        uVar2 = 0;

        if (arg1 == 5)
        {
            uVar2 = 0;

            if (DAT_1A == 3)
            {
                FUN_2C124(2);
                Utilities.ParentChildren(this, this);
                uVar2 = 0xfffffffe;
            }
        }

        return uVar2;
    }
}
