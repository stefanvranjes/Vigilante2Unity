using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook2 : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //0x20 (TOWTRUCK.DLL)
    private static Vector3Int DAT_20 = new Vector3Int(0, 0, 0);

    public VigObject DAT_88; //0x88
    public int DAT_A0_2; //0xA0
    public ConfigContainer DAT_A4; //0xA4

    //FUN_40 (TOWTRUCK.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        VigTuple ppiVar2;
        List<VigTuple> ppiVar3;
        sbyte sVar4;
        int iVar5;
        VigObject oVar5;
        Vehicle vVar5;
        int iVar6;
        VigObject oVar6;
        VigTransform tVar6;
        VigObject oVar8;
        VigTuple tVar8;
        ConfigContainer ccVar9;
        VigObject oVar10;
        int iVar10;
        int iVar11;
        VigObject oVar11;
        int iVar12;
        Vector3Int iStack40;
        Vector3Int iStack56;
        Vector3Int iStack72;
        Vector3Int sStack80;
        Vector3Int iStack96;
        Vector3Int iStack112;
        Vector3Int iStack128;

        if (arg1 == 2)
        {
            sVar4 = tags;

            if (sVar4 == 1)
            {
                oVar6 = FUN_2CCBC();
                GameManager.instance.FUN_308C4(oVar6);
                return 0;
            }

            if (sVar4 < 2)
            {
                if (sVar4 != 0)
                    return 0;

                iVar11 = physics2.Z;
                physics2.W = iVar11;
                iVar5 = iVar11;

                if (iVar11 < 0)
                    iVar5 = iVar11 + 15;

                child2.vTransform.rotation.V22 = (short)(iVar5 >> 4);
                DAT_88.vTransform.position.z = iVar11 << 1;
                iStack112 = Utilities.FUN_24148(DAT_80.vTransform, 
                    new Vector3Int(physics1.X, physics2.X, physics2.Y));
                iStack96 = GameManager.instance.FUN_2CE50(DAT_88);
                iStack128 = new Vector3Int();
                iStack128.x = iStack112.x - iStack96.x;
                iStack128.y = iStack112.y - iStack96.y;
                iStack128.z = iStack112.z - iStack96.z;
                iVar5 = Utilities.FUN_29E84(iStack128);

                if (0x14000 < iVar5 || (DAT_80.flags & 0x4000000) != 0)
                {
                    tags = 1;
                    DAT_A0_2 = (physics2.W - 0x4000) / 15;
                    GameManager.instance.FUN_30CB0(this, 15);
                    return 0;
                }

                ((Vehicle)DAT_80).FUN_3A020(-10, DAT_20, true);
                tags = 2;
                physics1.Z = 0x18000;
                GameManager.instance.FUN_30CB0(this, 300);

                if (((Vehicle)DAT_80).wheelsType == _WHEELS.Air)
                    ((Vehicle)DAT_80).FUN_3E32C(_WHEELS.Ground, 0);

                sVar4 = (sbyte)GameManager.instance.FUN_1DD9C();
                DAT_18 = sVar4;
                GameManager.instance.FUN_1E5D4(sVar4, vData.sndList, 5, iStack96, true);
                oVar5 = Utilities.FUN_2CD78(DAT_84);
                oVar5.tags = 3;
            }
            else
            {
                if (sVar4 != 2)
                {
                    if (sVar4 != 3)
                        return 0;

                    oVar6 = FUN_2CCBC();
                    GameManager.instance.FUN_308C4(oVar6);
                    return 0;
                }

                vVar5 = (Vehicle)DAT_80;
                iStack56 = new Vector3Int();
                iStack56.x = vVar5.vTransform.position.x - vTransform.position.x;
                iStack56.y = vVar5.vTransform.position.y - vTransform.position.y;
                iStack56.z = vVar5.vTransform.position.z - vTransform.position.z;
                iVar5 = 0x1000;
                oVar11 = null;

                if (vTransform.rotation.V02 * iStack56.x + vTransform.rotation.V12 * iStack56.y + vTransform.rotation.V22 * iStack56.z < 0)
                {
                    oVar8 = Utilities.FUN_2CD78(DAT_84);
                    tVar6 = GameManager.instance.FUN_2CDF4(oVar8);
                    ppiVar3 = GameManager.instance.worldObjs;

                    for (int i = 0; i < ppiVar3.Count; i++)
                    {
                        ppiVar2 = ppiVar3[i];
                        oVar10 = ppiVar2.vObject;

                        if (oVar10.type == 2 && oVar10 != oVar8 && oVar10 != DAT_80)
                        {
                            iStack56 = Utilities.FUN_24304(tVar6, oVar10.screen);

                            if (0 < iStack56.z)
                            {
                                iVar12 = iStack56.x / (iStack56.z >> 12);

                                if (iVar12 < 0)
                                    iVar12 = -iVar12;

                                if (iVar12 < iVar5)
                                {
                                    oVar11 = oVar10;
                                    iVar5 = iVar12;
                                }
                            }
                        }
                    }

                    if (oVar11 == null)
                    {
                        iStack56.x = oVar8.vTransform.rotation.V02;
                        iStack56.y = oVar8.vTransform.rotation.V12 - 0x666;
                        iStack56.z = oVar8.vTransform.rotation.V22;
                    }
                    else
                    {
                        vVar5 = (Vehicle)DAT_80;
                        iStack56.x = oVar11.screen.x - vVar5.vTransform.position.x;
                        iStack56.z = oVar11.screen.z - vVar5.vTransform.position.z;
                        iStack56.y = oVar11.screen.y - vVar5.vTransform.position.y - 0x14000;
                    }

                    Utilities.FUN_29FC8(iStack56, out sStack80);
                    iVar5 = physics2.W;

                    if (iVar5 < 0xc000)
                        iVar11 = 0xc000;
                    else
                    {
                        iVar11 = 0x28000;

                        if (iVar5 < 0x28001)
                            iVar11 = iVar5;
                    }

                    iVar5 = sStack80.x * iVar11;

                    if (iVar5 < 0)
                        iVar5 += 0x3fff;

                    iVar10 = sStack80.y * iVar11;
                    iStack72 = new Vector3Int();
                    iStack72.x = (iVar5 >> 14) << 7;

                    if (iVar10 < 0)
                        iVar10 += 0x3fff;

                    iStack72.y = (iVar10 >> 14) << 7;
                    iVar11 = sStack80.z * iVar11;

                    if (iVar11 < 0)
                        iVar11 += 0x3fff;

                    iStack72.z = (iVar11 >> 14) << 7;
                    iStack72 = Utilities.FUN_2426C(DAT_80.vTransform.rotation, 
                        new Matrix2x4(iStack72.x, iStack72.y, iStack72.z, 0));
                    DAT_80.FUN_2B1FC(iStack72, new Vector3Int(physics1.W, physics2.X, physics2.Y));
                    iVar6 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E580(iVar6, vData.sndList, 4, oVar8.vTransform.position);
                }

                GameManager.instance.FUN_30CB0(this, 6);
                tags = 3;
            }
        }
        else
        {
            if (arg1 < 3)
            {
                if (arg1 != 0)
                    return 0;

                oVar5 = Utilities.FUN_2CD78(this);
                DAT_84 = oVar5;

                if (oVar5 == null || (oVar5=Utilities.FUN_2CD78(oVar5)) == null)
                {
                    oVar6 = FUN_2CCBC();
                    GameManager.instance.FUN_308C4(oVar6);
                    return 0xffffffff;
                }

                sVar4 = tags;

                if (sVar4 == 1)
                    physics2.W -= DAT_A0_2;
                else
                {
                    if (sVar4 < 2)
                    {
                        if (sVar4 == 0)
                            physics2.W += DAT_A0_2;
                    }
                    else
                    {
                        if (sVar4 < 4)
                        {
                            vVar5 = (Vehicle)DAT_80;

                            if (vVar5.shield != 0)
                            {
                                vVar5.FUN_393F8();
                                GameManager.instance.FUN_30CB0(this, 0);
                                tags = 3;
                                return 0;
                            }

                            iStack112 = Utilities.FUN_24148(DAT_80.vTransform, 
                                new Vector3Int(physics1.W, physics2.X, physics2.Y));
                            tVar6 = GameManager.instance.FUN_2CEAC(DAT_84, DAT_A4);
                            iStack128 = new Vector3Int();
                            iStack128.x = iStack112.x - tVar6.position.x;
                            iStack128.y = iStack112.y - tVar6.position.y;
                            iStack128.z = iStack112.z - tVar6.position.z;
                            iStack96 = Utilities.FUN_2426C(tVar6.rotation, 
                                new Matrix2x4(iStack128.x, iStack128.y, iStack128.z, 0));
                            iVar5 = Utilities.FUN_29FC8(iStack96, out sStack80);
                            iVar5 /= 2;

                            if (0x4ffff < iVar5)
                            {
                                oVar6 = FUN_2CCBC();
                                GameManager.instance.FUN_308C4(oVar6);
                                return 0xffffffff;
                            }

                            vTransform.rotation = Utilities.FUN_2A724(sStack80);
                            physics2.W = iVar5;

                            if (tags != 3 && physics2.Z < iVar5)
                            {
                                Utilities.FUN_29FC8(iStack128, out sStack80);
                                iVar5 = physics2.Z - iVar5;
                                iStack40 = new Vector3Int();
                                iStack40.x = iVar5 * sStack80.x;

                                if (iStack40.x < 0)
                                    iStack40.x += 0x1fff;

                                iStack40.y = iVar5 * sStack80.y;
                                iStack40.x >>= 13;

                                if (iStack40.y < 0)
                                    iStack40.y += 0x1fff;

                                iStack40.y >>= 13;
                                iStack40.z = iVar5 * sStack80.z;

                                if (iStack40.z < 0)
                                    iStack40.z += 0x1fff;

                                iStack40.z >>= 13;
                                DAT_80.FUN_2B370(iStack40, iStack112);
                                oVar6 = Utilities.FUN_2CD78(DAT_84);
                                iVar5 *= 3;

                                if (iVar5 < 0)
                                    iVar5 += 0xffff;

                                iVar5 >>= 16;

                                if (-1 < iVar5)
                                    iVar5 = -1;

                                iVar6 = ((Vehicle)DAT_80).FUN_3B078(oVar6, (ushort)DAT_1A, iVar5, 1);
                                ((Vehicle)DAT_80).FUN_39DCC(iVar6, DAT_20, true);
                            }
                        }
                    }
                }

                iVar5 = physics2.W;

                if (iVar5 < 0)
                    iVar5 += 15;

                child2.vTransform.rotation.V22 = (short)(iVar5 >> 4);
                DAT_88.vTransform.position.z = physics2.W << 1;
            }
            else
            {
                if (arg1 != 4)
                    return 0;

                oVar11 = DAT_84;
                oVar5 = Utilities.FUN_2CD78(oVar11);

                if (DAT_18 != 0)
                    GameManager.instance.FUN_1DE78(DAT_18);

                if (oVar5 == null)
                    return 0;

                tVar8 = GameManager.instance.FUN_30134(GameManager.instance.worldObjs, oVar5);

                if (tVar8 != null)
                {
                    oVar5.DAT_58 -= 0xa0000;

                    if (oVar11.maxHalfHealth == 0)
                    {
                        oVar11.FUN_3A368();
                        return 0;
                    }

                    oVar6 = DAT_84.vData.ini.FUN_2C17C(2, typeof(VigObject), 8);
                    ccVar9 = oVar11.FUN_2C5F4(0x8000);
                    Utilities.FUN_2CA94(oVar11, ccVar9, oVar6);
                    Utilities.ParentChildren(oVar11, oVar11);
                    oVar6.FUN_30BF0();
                    oVar11.PDAT_78 = oVar6;
                    oVar11.flags &= 0xf7ffffff;
                }
            }
        }

        return 0;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        if (arg1 == 5)
        {
            FUN_30C20();
            vAnim = null;
            flags |= 4;
            return 0xfffffffe;
        }

        return 0;
    }
}
