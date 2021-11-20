using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneLarge : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override uint OnCollision(HitDetection hit)
    {
        bool bVar7;

        if (hit.self.type != 8)
            return 0;

        bVar7 = FUN_32B90(hit.self.maxHalfHealth);

        if (!bVar7)
            return 0;

        if ((byte)tags - 3U < 3)
            child2.child2.child2.FUN_4ECA0();

        FUN_30BA8();
        FUN_30C68();

        if (DAT_80 != null)
            GameManager.instance.FUN_30CB0(DAT_80, 60);

        if (DAT_84 != null)
            GameManager.instance.FUN_30CB0(DAT_84, 60);

        tags = -1;
        return 0;
    }

    public Vector3Int DAT_88; //0x88
    public int DAT_94; //0x94

    //FUN_2C1C (HARBOR.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        bool bVar1;
        sbyte sVar2;
        Vector3Int v3Var3;
        int iVar4;
        VigObject oVar4;
        ConfigContainer ccVar4;
        int iVar5;
        uint uVar5;
        VigObject oVar5;
        ConfigContainer ccVar5;
        Vector3Int v3Var5;
        int iVar6;
        VigObject oVar6;
        int iVar7;
        bool bVar7;
        VigObject oVar7;
        VigObject oVar8;
        int iVar9;
        Vector3Int auStack24;

        switch (arg1)
        {
            case 0:
                oVar7 = child2;
                oVar8 = oVar7.child2;

                switch(((byte)tags - 1) * 0x1000000 >> 24)
                {
                    case 0:
                    case 3:
                        iVar4 = oVar7.vTransform.position.z + DAT_94;
                        oVar7.vTransform.position.z = iVar4;

                        if (DAT_88.z - iVar4 < 0)
                        {
                            iVar4 = DAT_94 - 25;
                            iVar7 = -1525;

                            if (-1526 < iVar4)
                            {
                                iVar7 = 0;

                                if (iVar4 < 1)
                                    iVar7 = iVar4;
                            }

                            DAT_94 = iVar7;
                        }
                        else
                        {
                            iVar7 = DAT_94 + 25;

                            if (iVar7 < 0)
                                iVar4 = 0;
                            else
                            {
                                iVar4 = 1525;

                                if (iVar7 < 1526)
                                    iVar4 = iVar7;
                            }

                            DAT_94 = iVar4;
                        }

                        bVar1 = tags == 1;

                        if (id == 121)
                        {
                            iVar7 = 0x400;

                            if (bVar1)
                                iVar7 = 0;
                        }
                        else
                        {
                            iVar7 = 0x400;

                            if (!bVar1)
                                iVar7 = 0;
                        }

                        iVar6 = iVar7 - oVar8.vr.y;
                        iVar4 = -8;

                        if (-9 < iVar6)
                        {
                            iVar4 = 8;

                            if (iVar6 < 9)
                                iVar4 = iVar6;
                        }

                        iVar4 = oVar8.vr.y + iVar4;
                        oVar8.vr.y = iVar4;

                        if (iVar4 * 0x10000 >> 0x10 != iVar7 || DAT_94 != 0)
                        {
                            if (arg2 == 0)
                                return 0;

                            oVar8.ApplyRotationMatrix();
                            goto LAB_2CA0;
                        }

                        oVar8.ApplyRotationMatrix();
                        break;
                    case 1:
                    case 4:
                        iVar7 = oVar8.vTransform.position.y + 1525;
                        oVar8.vTransform.position.y = iVar7;

                        if (iVar7 <= DAT_88.y) goto LAB_2CA0;

                        iVar5 = GameManager.instance.FUN_1DD9C();
                        v3Var3 = GameManager.instance.FUN_2CE50(oVar8);
                        GameManager.instance.FUN_1E580(iVar5, vData.sndList, 2, v3Var3);
                        iVar9 = (uint)(tags ^ 2) < 1 ? 1 : 0;

                        if (id != 120)
                        {
                            if (iVar9 != 0)
                                oVar7 = DAT_80;
                            else
                                oVar7 = DAT_84;
                        }
                        else
                        {
                            if ((iVar9 ^ 1) != 0)
                                oVar7 = DAT_80;
                            else
                                oVar7 = DAT_84;
                        }

                        if (oVar7 == null) goto LAB_2FD4;

                        if (tags == 2)
                        {
                            if (id == 120)
                            {
                                oVar7 = DAT_84;
                                GameManager.instance.FUN_30CB0(oVar7, 180);
                                oVar4 = oVar7.child2;
                            }
                            else
                            {
                                oVar7 = DAT_80;
                                GameManager.instance.FUN_30CB0(oVar7, 180);
                                oVar4 = oVar7.child2;
                                oVar7.DAT_80 = null;
                            }

                            oVar4.vTransform = GameManager.FUN_2A39C();
                            oVar5 = oVar4.FUN_2CCBC();
                            Utilities.FUN_2CC9C(oVar8, oVar5);
                            oVar5.transform.parent = oVar8.transform;
                        }
                        else
                        {
                            if (id == 120)
                            {
                                oVar7 = oVar8.child2;
                                oVar4 = DAT_80;
                                oVar4.DAT_80 = oVar7;
                            }
                            else
                            {
                                oVar4 = DAT_84;
                                oVar7 = oVar8.child2;
                            }

                            ccVar5 = oVar4.FUN_2C5F4(0x8000);
                            oVar7.vTransform = Utilities.FUN_2C77C(ccVar5);
                            oVar5 = oVar7.FUN_2CCBC();
                            Utilities.FUN_2CC9C(oVar4, oVar5);
                            oVar5.transform.parent = oVar4.transform;
                            GameManager.instance.FUN_30CB0(oVar4, 180);
                        }

                        break;
                    case 2:
                    case 5:
                        iVar4 = oVar8.vTransform.position.y - 1525;
                        oVar8.vTransform.position.y = iVar4;

                        if (oVar8.screen.y <= iVar4) goto LAB_2CA0;

                        if (id == 120)
                            oVar4 = DAT_80;
                        else
                            oVar4 = DAT_84;

                        if (oVar4 != null)
                        {
                            if (id == 120)
                            {
                                ccVar4 = DAT_80.FUN_2C5F4(0x8000);
                                oVar6 = DAT_80;
                            }
                            else
                            {
                                ccVar4 = DAT_84.FUN_2C5F4(0x8000);
                                oVar6 = DAT_84;
                            }

                            auStack24 = Utilities.FUN_24148(oVar6.vTransform, ccVar4.v3_1);
                            DAT_88 = Utilities.FUN_24304(vTransform, auStack24);
                            iVar7 = oVar7.screen.y;
                            sVar2 = (sbyte)(tags + 1);
                            tags = sVar2;
                            DAT_88.y -= iVar7;

                            if (sVar2 == 7)
                            {
                                GameManager.instance.FUN_1DE78(DAT_18);
                                tags = 0;
                                DAT_18 = 0;
                                FUN_30BA8();
                                return 0;
                            }

                            goto LAB_2CA0;
                        }

                        LAB_2FD4:
                        tags = 0;
                        FUN_30C68();
                        FUN_30BA8();
                        break;
                    default:
                        goto LAB_2CA0;
                }

                tags++;

                LAB_2CA0:
                if (arg2 == 0)
                    return 0;

                v3Var5 = GameManager.instance.FUN_2CE50(oVar8);
                uVar5 = GameManager.instance.FUN_1E7A8(v3Var5);
                GameManager.instance.FUN_1E2C8(DAT_18, uVar5);
                return 0;
            case 8:
                bVar7 = FUN_32B90((uint)arg2);

                if (!bVar7)
                    return 0;

                if ((byte)tags - 3U < 3)
                    child2.child2.child2.FUN_4ECA0();

                FUN_30BA8();
                FUN_30C68();

                if (DAT_80 != null)
                    GameManager.instance.FUN_30CB0(DAT_80, 60);

                if (DAT_84 != null)
                    GameManager.instance.FUN_30CB0(DAT_84, 60);

                tags = -1;
                break;
        }

        return 0;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        sbyte sVar2;
        int iVar7;
        VigObject oVar7;
        VigObject oVar8;
        Vector3Int auStack24;

        switch (arg1)
        {
            case 20:
            case 21:
                if (arg1 == 20)
                    DAT_84 = arg2;
                else
                    DAT_80 = arg2;

                if (tags != 0)
                    return 0;

                if (DAT_80 == null)
                    return 0;

                if (DAT_84 != null)
                {
                    if (id != 120 && DAT_80.DAT_80 == null)
                        return 0;

                    DAT_80.FUN_30C68();
                    DAT_84.FUN_30C68();
                    FUN_30B78();
                    sVar2 = (sbyte)GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E098(sVar2, vData.sndList, 1, 0);

                    if (id == 120)
                    {
                        oVar7 = DAT_84;
                        oVar8 = oVar7.child2;
                    }
                    else
                    {
                        oVar7 = DAT_80;
                        oVar8 = oVar7.DAT_80;
                    }

                    auStack24 = Utilities.FUN_24148(oVar7.vTransform, oVar8.vTransform.position);
                    DAT_88 = Utilities.FUN_24304(vTransform, auStack24);
                    iVar7 = child2.screen.y;
                    tags = 1;
                    DAT_88.y -= iVar7;
                }

                break;
        }

        return 0;
    }
}
