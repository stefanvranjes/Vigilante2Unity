using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freezer : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //0x4C-58 (STINGER.DLL)
    public static int DAT_4C, DAT_50, DAT_54 = 0x199;
    public static int DAT_58_2 = 0;

    //FUN_BE0 (STINGER.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        byte bVar1;
        sbyte sVar3;
        VigObject oVar7;

        switch (arg1)
        {
            case 0:
                bVar1 = DAT_19;

                if (bVar1 == 1)
                {
                    oVar7 = child2;

                    if (oVar7 == null)
                        return 0;

                    do
                    {
                        if (oVar7.id == 0) break;
                        oVar7 = oVar7.child;
                    } while (oVar7 != null);

                    if (oVar7 == null)
                        return 0;

                    oVar7.IDAT_78 += 2;
                }
                else
                {
                    if (bVar1 < 2)
                    {
                        if (bVar1 != 0)
                            return 0;

                        FUN_42330(arg2);
                        return 0;
                    }

                    if (bVar1 != 2)
                        return 0;

                    oVar7 = child2;

                    if (oVar7 == null)
                        return 0;

                    do
                    {
                        if (oVar7.id == 0) break;

                        oVar7 = oVar7.child;
                    } while (oVar7 != null);

                    if (oVar7 == null)
                        return 0;
                }

                oVar7.vr.z += oVar7.IDAT_78;
                oVar7.ApplyTransformation();
                return 0;
            case 2:
                sVar3 = (sbyte)(DAT_19 + 1);
                DAT_19 = (byte)sVar3;

                if (sVar3 == 2)
                {
                    GameManager.instance.FUN_30CB0(this, 90);
                    return 0;
                }

                if (sVar3 != 3)
                    return 0;

                DAT_19 = 0;
                break;
        }

        return 0;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        byte bVar1;
        ushort uVar2;
        int iVar4;
        ConfigContainer ccVar4;
        Freezer2 puVar5;
        VigTransform puVar6;
        VigObject oVar7;
        Vector3Int local_10;
        int local_4;

        switch (arg1)
        {
            case 0:
                bVar1 = DAT_19;

                if (bVar1 == 1)
                {
                    oVar7 = child2;

                    if (oVar7 == null)
                        return 0;

                    do
                    {
                        if (oVar7.id == 0) break;
                        oVar7 = oVar7.child;
                    } while (oVar7 != null);

                    if (oVar7 == null)
                        return 0;

                    oVar7.IDAT_78 += 2;
                }
                else
                {
                    if (bVar1 < 2)
                    {
                        if (bVar1 != 0)
                            return 0;

                        FUN_42330(arg2);
                        return 0;
                    }

                    if (bVar1 != 2)
                        return 0;

                    oVar7 = child2;

                    if (oVar7 == null)
                        return 0;

                    do
                    {
                        if (oVar7.id == 0) break;

                        oVar7 = oVar7.child;
                    } while (oVar7 != null);

                    if (oVar7 == null)
                        return 0;
                }

                oVar7.vr.z += oVar7.IDAT_78;
                oVar7.ApplyTransformation();
                return 0;
            case 1:
                type = 3;
                maxHalfHealth = 1;
                break;
            case 12:
                if (DAT_19 != 0 && maxHalfHealth != 0)
                {
                    iVar4 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E14C(iVar4, GameManager.instance.DAT_C2C, 1);
                    return 720;
                }

                puVar5 = arg2.vData.ini.FUN_2C17C(1, typeof(Freezer2), 8) as Freezer2;

                if (puVar5 == null)
                {
                    //FUN_15D9C
                    return 720;
                }

                local_10 = new Vector3Int(DAT_4C, DAT_50, DAT_54);
                local_4 = DAT_58_2;
                DAT_19 = 1;
                puVar5.flags = 0x60000410;
                uVar2 = (ushort)arg2.id;
                puVar5.type = 8;
                puVar5.DAT_80 = arg2;
                puVar5.DAT_B8 = this;
                puVar5.id = (short)uVar2;
                puVar6 = GameManager.instance.FUN_2CDF4(arg2);
                puVar5.DAT_84_2 = puVar6;
                puVar5.vTransform = arg2.vTransform;
                puVar5.DAT_A4 = 0x199;
                puVar5.FUN_30B78();
                puVar5.FUN_30BF0();
                GameManager.instance.FUN_30CB0(puVar5, 29);
                GameManager.instance.FUN_30CB0(this, 29);
                oVar7 = child2;

                if (oVar7 != null)
                {
                    do
                    {
                        if (oVar7.id == 0) break;

                        oVar7 = oVar7.child;
                    } while (oVar7 != null);

                    if (oVar7 != null)
                        oVar7.IDAT_78 = 0;
                }

                ccVar4 = FUN_2C5F4(0x8000);
                Utilities.FUN_2CA94(this, ccVar4, puVar5);
                Utilities.ParentChildren(this, this);
                puVar5.DAT_84_2 = puVar5.vTransform;
                puVar5.vTransform.rotation = Utilities.FUN_2449C(puVar5.vTransform.rotation, local_10);
                maxHalfHealth--;
                iVar4 = GameManager.instance.FUN_1DD9C();
                GameManager.instance.FUN_1E14C(iVar4, arg2.vData.sndList, 4);
                iVar4 = GameManager.instance.FUN_1DD9C();
                GameManager.instance.FUN_1E188(iVar4, vData.sndList, 2);
                return 720;
            case 13:
                if (GameManager.instance.DAT_1084 != 0)
                    return 0;

                if (arg2.physics1.W < 1525 && (arg2.flags & 0x20000000) != 0)
                {
                    local_10 = Utilities.FUN_24304(arg2.vTransform, ((Vehicle)arg2).target.vTransform.position);

                    if (0x63ffe < (uint)local_10.z - 0x19001)
                        return 0;

                    if (local_10.x < 0)
                        local_10.x = -local_10.x;

                    return (uint)(local_10.x * 6 - 0x10000 < local_10.z ? 1 : 0);
                }

                break;
        }

        return 0;
    }
}
