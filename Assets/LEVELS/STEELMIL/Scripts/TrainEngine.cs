using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainEngine : Destructible
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public TrainEngine DAT_A8; //0xA8
    public TrainEngine DAT_AC; //0xAC
    public int DAT_B0; //0xB0
    public int DAT_B4; //0xB4
    public RSEG_DB DAT_B8; //0xB8
    public int DAT_BC; //0xBC
    public int DAT_C0; //0xC0
    public int DAT_C4; //0xC4
    public int DAT_C8; //0xC8

    public override uint OnCollision(HitDetection hit)
    {
        byte bVar1;
        int iVar2;
        int iVar3;
        uint uVar3;
        VigObject oVar4;

        oVar4 = hit.self;
        bVar1 = oVar4.type;

        if (bVar1 == 8)
        {
            FUN_32B90(oVar4.maxHalfHealth);
            uVar3 = 0;
        }
        else
        {
            if (bVar1 == 2)
            {
                iVar2 = (int)FUN_33798(hit, DAT_BC);
                uVar3 = 0;

                if (iVar2 != 0)
                {
                    iVar3 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E580(iVar3, GameManager.instance.DAT_C2C, 7, oVar4.vTransform.position);
                    uVar3 = 0;
                }
            }
            else
            {
                uVar3 = 0;

                if (bVar1 != 3 && oVar4 != DAT_A8 && oVar4 != DAT_AC)
                {
                    FUN_4DC94();
                    oVar4.FUN_4DC94();
                }
            }
        }

        return uVar3;
    }

    //FUN_3FD4 (STEELMIL.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        sbyte sVar1;
        int iVar3;
        uint uVar3;
        int iVar4;

        switch (arg1)
        {
            case 0:
                iVar4 = FUN_3AD0();

                if (iVar4 == 0 || DAT_B8 == null || (DAT_B8.DAT_0C & 0x100) == 0)
                {
                    uVar3 = 0;

                    if (arg2 != 0 && DAT_B8 != null)
                    {
                        uVar3 = GameManager.instance.FUN_1E7A8(vTransform.position);
                        GameManager.instance.FUN_1E2C8(DAT_18, uVar3);
                        uVar3 = 0;
                    }
                }
                else
                {
                    iVar3 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E580(iVar3, vData.sndList, 3, vTransform.position);
                    uVar3 = 0;
                }

                break;
            case 1:
                sVar1 = (sbyte)GameManager.instance.FUN_1DD9C();
                DAT_18 = sVar1;
                GameManager.instance.FUN_1E098(sVar1, vData.sndList, 2, 0, true);
                tags = 2;
                FUN_3754();
                uVar3 = 0;
                break;
            case 2:
                uVar3 = 0;

                if (vMesh != null)
                {
                    FUN_4DC94();
                    uVar3 = 0;
                }

                break;
            case 4:
                FUN_38FC();
                GameManager.instance.FUN_1DE78(DAT_18);
                goto default;
            default:
                uVar3 = 0;
                break;
            case 8:
                FUN_32B90((uint)arg2);
                uVar3 = 0;
                break;
            case 9:
                uVar3 = 0;

                if (arg2 != 0)
                {
                    GameManager.instance.FUN_309A0(this);
                    uVar3 = 0;
                }

                break;
        }

        return uVar3;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        switch (arg1)
        {
            case 20:
                FUN_3838((TrainEngine)arg2);
                return 0;
        }

        return 0;
    }

    public void FUN_3754()
    {
        RSEG_DB rVar1;
        int iVar2;

        DAT_A8 = null;
        DAT_B0 = vCollider.reader.ReadInt32(24) + 0x5000;
        DAT_B4 = vCollider.reader.ReadInt32(12) - 0x5000;
        rVar1 = LevelManager.instance.FUN_518DC(screen, -1);
        DAT_B8 = rVar1;
        DAT_C0 = 1;
        iVar2 = rVar1.FUN_51334(screen);
        DAT_C4 = iVar2 << 16;

        if (tags != 0)
        {
            DAT_BC = 3814;
            flags |= 0x80;
        }

        physics2.X = 0;
        flags |= 0x100;
        physics2.Y = 0;
        physics2.Z = 0;
        DAT_A0 = new Vector3Int(0x10, 0x20, 0x40);
        GameManager.instance.FUN_30334(GameManager.instance.worldObjs, 20, this);
    }

    public void FUN_3838(TrainEngine param1)
    {
        Vector3Int local_10;

        local_10 = Utilities.FUN_24304(vTransform, param1.vTransform.position);

        if (local_10.x < 0)
            local_10.x = -local_10.x;

        if (local_10.x < 0x10001)
        {
            if (local_10.z < 0)
            {
                if (-local_10.z < (param1.DAT_B0 - DAT_B4) + 0x10000)
                {
                    param1.DAT_A8 = this;
                    DAT_AC = param1;
                }
            }
            else
            {
                if (local_10.z < (DAT_B0 - param1.DAT_B4) + 0x10000)
                {
                    DAT_A8 = param1;
                    param1.DAT_AC = this;
                }
            }
        }
    }

    public void FUN_38FC()
    {
        TrainEngine tVar1;

        tVar1 = DAT_A8;

        if (tVar1 != null)
        {
            DAT_A8 = null;
            tVar1.DAT_AC = null;
        }

        tVar1 = DAT_AC;

        if (tVar1 != null)
        {
            DAT_AC = null;
            tVar1.DAT_A8 = null;
        }
    }

    public void FUN_3934(JUNC_DB param1)
    {
        RSEG_DB piVar1;
        int iVar2;

        piVar1 = param1.DAT_1C[0];
        DAT_B8 = piVar1;
        iVar2 = piVar1.DAT_00[0] == param1 ? 1 : 0;
        DAT_C0 = iVar2;
        DAT_C4 = (iVar2 == 0 ? 1 : 0) << 28;
    }

    public long FUN_3964()
    {
        int iVar1;
        long lVar2;
        Vector3Int local_10;

        iVar1 = DAT_C4;

        if (iVar1 < 0)
            iVar1 += 0xffff;

        DAT_B8.FUN_285E4(iVar1 >> 16, ref vTransform.position, out local_10);

        if (DAT_C0 == 0)
        {
            local_10.x = -local_10.x;
            local_10.z = -local_10.z;
        }

        lVar2 = Utilities.VectorNormal2(local_10, out local_10);
        vTransform.rotation.V22 = (short)local_10.z;
        vTransform.rotation.V00 = (short)local_10.z;
        vTransform.rotation.V02 = (short)local_10.x;
        vTransform.rotation.V20 = (short)-local_10.x;
        return lVar2;
    }

    public JUNC_DB FUN_3A00()
    {
        JUNC_DB jVar1;
        int iVar2;
        int iVar3;
        JUNC_DB jVar4;

        if (DAT_A8 == null)
        {
            flags &= 0xffffffdd;
            FUN_30B78();
            jVar4 = DAT_B8.DAT_00[DAT_C0];

            do
            {
                iVar2 = (int)GameManager.FUN_2AC5C();
                iVar3 = (iVar2 * 3 >> 15) + 40;
                SteelMil.instance.DAT_4614 = iVar3;
            } while ((iVar3 & 0xff) == jVar4.DAT_12);

            jVar1 = LevelManager.instance.FUN_51B74(iVar3 & 0xff);
        }
        else
            jVar1 = DAT_A8.FUN_3A00();

        FUN_3934(jVar1);
        FUN_3964();
        return jVar1;
    }

    public int FUN_3AD0()
    {
        int iVar1;
        long lVar1;
        RSEG_DB piVar2;
        int iVar3;
        TrainEngine oVar3;
        JUNC_DB jVar3;
        TrainEngine piVar4;
        RSEG_DB rVar4;
        RSEG_DB piVar5;
        int iVar6;
        JUNC_DB jVar6;
        int iVar7;

        if (DAT_B8 == null)
        {
            if (vCollider != null)
            {
                vCollider.reader.Seek(4, SeekOrigin.Current);
                FUN_2B4F8(vCollider.reader);
                vCollider.reader.Seek(-4, SeekOrigin.Current);
                return 0;
            }

            GameManager.instance.FUN_309A0(this);
            return 0;
        }

        lVar1 = FUN_3964();
        oVar3 = DAT_A8;

        if (oVar3 == null)
        {
            if (tags < 2)
            {
                iVar3 = DAT_BC - 14;
                DAT_BC = iVar3;

                if (iVar3 < 0)
                {
                    piVar4 = this;

                    do
                    {
                        piVar4.tags = 0;
                        piVar4.FUN_30BA8();
                        GameManager.instance.FUN_1DE78(DAT_18);
                        DAT_18 = 0;
                        piVar4 = piVar4.DAT_AC;
                    } while (piVar4 != null);
                }
            }

            iVar3 = (int)Utilities.SquareRoot(lVar1);
            iVar6 = DAT_C4;

            if (DAT_C0 != 0)
            {
                iVar7 = DAT_BC << 16;
                goto LAB_3CBC;
            }
        }
        else
        {
            if ((oVar3.flags & 2) == 0)
            {
                iVar6 = oVar3.vTransform.rotation.V02 * oVar3.DAT_B4;

                if (iVar6 < 0)
                    iVar6 += 4095;

                iVar7 = oVar3.vTransform.rotation.V22 * oVar3.DAT_B4;

                if (iVar7 < 0)
                    iVar7 += 4095;

                iVar3 = vTransform.rotation.V02 * ((oVar3.vTransform.position.x + (iVar6 >> 12)) - vTransform.position.x) +
                        vTransform.rotation.V22 * ((oVar3.vTransform.position.z + (iVar7 >> 12)) - vTransform.position.z) +
                        DAT_B0 * -0x1000;

                iVar7 = 0;

                if (0 < iVar3)
                    iVar7 = iVar3;

                iVar3 = iVar7;

                if (iVar7 < 0)
                    iVar3 = iVar7 + 4095;

                DAT_BC = iVar3 >> 12;
                iVar3 = (int)Utilities.SquareRoot(lVar1);
                iVar6 = DAT_C4;

                if (DAT_C0 == 0)
                    iVar7 *= -16;
                else
                    iVar7 <<= 4;

                goto LAB_3CBC;
            }

            iVar3 = (int)Utilities.SquareRoot(lVar1);
            iVar6 = DAT_C4;

            if (DAT_C0 != 0)
            {
                iVar7 = DAT_BC << 16;
                goto LAB_3CBC;
            }
        }

        iVar7 = DAT_BC * -0x10000;
        LAB_3CBC:
        DAT_C4 = iVar6 + iVar7 / iVar3;
        oVar3 = DAT_AC;

        if (oVar3 != null && (oVar3.flags & 2) != 0)
        {
            iVar6 = oVar3.DAT_C8 + DAT_BC;
            oVar3.DAT_C8 = iVar6;

            if (oVar3.DAT_B0 - DAT_B4 < iVar6)
            {
                oVar3.flags &= 0xffffffdd;
                oVar3.FUN_30B78();
            }
        }

        iVar1 = 0;

        if (0x10000000 < (uint)DAT_C4)
        {
            piVar2 = DAT_B8;
            jVar3 = piVar2.DAT_00[DAT_C0];
            rVar4 = null;

            if (jVar3.DAT_11 == 1)
            {
                FUN_30BA8();
                DAT_C8 = 0;
                flags |= 0x22;

                if (DAT_AC == null)
                    FUN_3A00();

                iVar1 = -1;
            }
            else
            {
                if (jVar3.DAT_11 == 2)
                {
                    rVar4 = jVar3.DAT_1C[0];

                    if (rVar4 == piVar2)
                        rVar4 = jVar3.DAT_1C[1];

                    if (rVar4.DAT_08 == 0xffff)
                    {
                        FUN_38FC();
                        iVar3 = vTransform.rotation.V02 * DAT_BC;
                        rVar4 = null;
                        flags &= 0xfffffeff;

                        if (iVar3 < 0)
                            iVar3 += 31;

                        physics1.X = iVar3 >> 5;
                        physics1.Y = 0;
                        iVar3 = vTransform.rotation.V22 * DAT_BC;

                        if (iVar3 < 0)
                            iVar3 += 31;

                        physics1.Z = iVar3 >> 5;
                        GameManager.instance.FUN_30CB0(this, 300);
                    }
                    else
                        DAT_C0 = rVar4.DAT_00[0] == jVar3 ? 1 : 0;
                }
                else
                {
                    iVar7 = 0;
                    jVar6 = jVar3;
                    piVar5 = rVar4;

                    if (jVar3.DAT_11 != 0)
                    {
                        do
                        {
                            rVar4 = jVar3.DAT_1C[iVar7];

                            if (rVar4 != piVar2 && DAT_C0 == (rVar4.DAT_00[0] == jVar3 ? 1 : 0))
                            {
                                piVar5 = rVar4;

                                if ((rVar4.DAT_08 != piVar2.DAT_08 ? 1 : 0) ==
                                    SteelMil.instance.DAT_4618[jVar3.DAT_12])
                                    break;
                            }

                            rVar4 = piVar5;
                            iVar7++;
                            piVar5 = rVar4;
                        } while (iVar7 < jVar3.DAT_11);
                    }

                    if (DAT_A8 != null && DAT_A8.DAT_B8 != rVar4)
                        FUN_38FC();
                }

                DAT_B8 = rVar4;
                DAT_C4 = (DAT_C0 == 0 ? 1 : 0) << 28;

                if (rVar4 != null)
                {
                    if (tags != 2)
                        return 1;

                    if (rVar4.DAT_00[DAT_C0].DAT_11 != 1)
                        return 1;

                    if (rVar4.DAT_00[DAT_C0].DAT_12 != 0)
                        return 1;

                    //FUN_326B0
                    tags = 1;
                }

                iVar1 = 1;
            }
        }

        return iVar1;
    }
}
