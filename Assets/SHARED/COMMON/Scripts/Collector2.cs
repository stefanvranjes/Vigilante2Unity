using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector2 : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //0x20 (GRBLDER.DLL)
    private static Vector3Int DAT_20 = new Vector3Int(0, 0, 0);

    public override uint OnCollision(HitDetection hit)
    {
        short sVar1;
        VigObject oVar1;
        Vehicle vVar1;
        short sVar2;
        short sVar6;
        VigObject oVar8;
        int iVar9;
        ConfigContainer ccVar9;
        byte bVar14;
        VigObject oVar16;
        VigCamera cVar16;

        oVar1 = hit.self;

        if (oVar1.type != 2)
            return 0;

        vVar1 = (Vehicle)oVar1;

        if ((vVar1.flags & 0x4004000) != 0x4000)
            return 0;

        ccVar9 = FUN_2C5F4(0x8000);
        GameObject obj = new GameObject();
        oVar16 = obj.AddComponent<VigObject>();
        oVar8 = Utilities.FUN_2CD78(this);
        PDAT_74 = vVar1;
        PDAT_78 = oVar16;
        Utilities.FUN_2CA94(this, ccVar9, oVar16);
        Utilities.ParentChildren(this, this);

        if (vVar1.DAT_58 < 0x10000)
        {
            oVar16.type = 4;

            if ((flags & 0x10000000) != 0)
            {
                DAT_19 = 0x23;
                iVar9 = 15;
                goto LAB_1374;
            }

            DAT_19 = 3;
        }
        else
        {
            sVar6 = oVar16.vTransform.rotation.V00;
            oVar16.vTransform.rotation.V00 = oVar16.vTransform.rotation.V02;
            sVar1 = oVar16.vTransform.rotation.V10;
            sVar2 = oVar16.vTransform.rotation.V20;
            oVar16.vTransform.rotation.V02 = (short)-sVar6;
            oVar16.vTransform.rotation.V10 = oVar16.vTransform.rotation.V12;
            oVar16.vTransform.rotation.V12 = (short)-sVar1;
            oVar16.vTransform.rotation.V20 = oVar16.vTransform.rotation.V22;
            oVar16.vTransform.rotation.V22 = (short)-sVar2;
            bVar14 = 0x10;

            if ((flags & 0x10000000) != 0)
                bVar14 = 0x30;

            DAT_19 = bVar14;
        }

        iVar9 = 63;
        LAB_1374:
        GameManager.instance.FUN_30CB0(this, iVar9);
        FUN_30B78();
        flags |= 0x20000020;
        vVar1.flags = vVar1.flags & 0xfffffffd | 0x6000020;
        oVar8.flags &= 0xfffff7ff;
        vVar1.FUN_30BA8();
        vVar1.state = _VEHICLE_TYPE.Collector;
        vVar1.PDAT_74 = this;
        vVar1.physics1.X = 0;
        vVar1.physics1.Y = 0;
        vVar1.physics1.Z = 0;
        vVar1.physics2.X = 0;
        vVar1.physics2.Y = 0;
        vVar1.physics2.Z = 0;
        cVar16 = vVar1.vCamera;

        if (cVar16 != null)
        {
            cVar16.target = oVar8;
            cVar16.DAT_92 = 0x400;
            cVar16.DAT_9C = 0x3c000;
        }

        iVar9 = vVar1.FUN_3B078(oVar8, (ushort)DAT_1A, -150, 1);
        vVar1.FUN_3A020(iVar9, DAT_20, true);

        if (vVar1.id < 0)
            GameManager.instance.FUN_15B00(~vVar1.id, 255, 0, 64);

        return 1;
    }

    //FUN_3B0 (GRBLDER.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        sbyte sVar4;
        sbyte sVar5;
        short sVar6;
        VigTransform puVar7;
        Body bVar7;
        VigObject oVar7;
        int iVar8;
        VigObject oVar8;
        Vehicle vVar8;
        VigTransform tVar8;
        int iVar9;
        VigTransform tVar9;
        VigObject oVar9;
        ConfigContainer ccVar9;
        Vector3Int v3Var10;
        int iVar11;
        int iVar12;
        uint uVar12;
        int iVar13;
        int iVar16;
        VigObject oVar16;
        Vehicle vVar16;
        int iVar17;
        VigObject oVar17;
        int iVar18;
        Matrix3x3 local_68;
        Vector3Int local_58;
        Matrix3x3 auStack72;
        Matrix3x3 auStack40;

        switch (arg1)
        {
            case 0:
                iVar16 = FUN_42330(arg2);

                if (iVar16 < 1)
                    return 0;

                if (arg2 < 0)
                    return 0;

                oVar16 = child2;

                switch (DAT_19)
                {
                    case 0:
                        vr.x -= 64;
                        sVar6 = (short)(oVar16.vr.x - 64);
                        goto LAB_480;
                    default:
                        return 0;
                    case 2:
                        vr.x += 64;
                        sVar6 = (short)(oVar16.vr.x + 64);
                        LAB_480:
                        oVar16.vr.x = sVar6;
                        
                        if (arg2 == 0)
                            return 0;
                        
                        ApplyRotationMatrix();
                        oVar16.ApplyRotationMatrix();
                        return 0;
                    case 3:
                        oVar17 = PDAT_74;
                        tVar8 = GameManager.instance.FUN_2CDF4(PDAT_78);
                        Utilities.FUN_248C4(oVar17.vTransform.rotation, tVar8.rotation, out local_68);
                        iVar16 = -local_68.V12;

                        if (0 < local_68.V12)
                            iVar16 += 7;

                        iVar11 = local_68.V02 - local_68.V20;
                        iVar16 >>= 3;

                        if (iVar11 < 0)
                            iVar11 += 7;

                        iVar13 = local_68.V10;
                        iVar11 >>= 3;
                        goto LAB_9D8;
                    case 4:
                        vr.x += 32;
                        oVar8 = PDAT_78;
                        oVar16.vr.x += 32;
                        oVar8.vr.x -= 32;

                        if (arg2 == 0)
                            return 0;

                        ApplyRotationMatrix();
                        oVar16.ApplyRotationMatrix();
                        oVar8.ApplyRotationMatrix();
                        goto LAB_BA4;
                    case 5:
                    case 0x26:
                        oVar8 = PDAT_78;
                        iVar16 = oVar8.vTransform.rotation.V02 * 1525;

                        if (iVar16 < 0)
                            iVar16 += 4095;

                        iVar17 = oVar8.vTransform.rotation.V12 * 1525;
                        oVar8.vTransform.position.x += iVar16 >> 12;

                        if (iVar17 < 0)
                            iVar17 += 4095;

                        iVar16 = oVar8.vTransform.rotation.V22 * 1525;
                        oVar8.vTransform.position.y += iVar17 >> 12;

                        if (iVar16 < 0)
                            iVar16 += 4095;

                        oVar8.vTransform.position.z += iVar16 >> 12;

                        if (arg2 == 0)
                            return 0;

                        goto LAB_BA4;
                    case 6:
                    case 0x28:
                        oVar16 = Utilities.FUN_2CD78(this);

                        if ((oVar16.flags & 0x4000000) == 0)
                        {
                            uVar12 = GameManager.FUN_2AC5C();

                            if ((uVar12 & 7) == 0)
                            {
                                iVar8 = (int)GameManager.FUN_2AC5C();
                                oVar16.physics1.X += ((iVar8 * 1524 >> 15) - 762) * 128;
                                iVar8 = (int)GameManager.FUN_2AC5C();
                                oVar16.physics1.Y += ((iVar8 * 1524 >> 15) - 762) * 128;
                                iVar8 = (int)GameManager.FUN_2AC5C();
                                oVar16.physics1.Z += ((iVar8 * 1524 >> 15) - 762) * 128;
                            }
                        }

                        ((Vehicle)PDAT_74).FUN_39DCC(-2, DAT_20, false);
                        return 0;
                    case 7:
                    case 0x29:
                        oVar8 = PDAT_74;
                        iVar16 = oVar8.physics1.X;

                        if (iVar16 < 0)
                            iVar16 += 127;

                        iVar17 = oVar8.physics1.Y;
                        oVar8.vTransform.position.x += iVar16 >> 7;

                        if (iVar17 < 0)
                            iVar17 += 127;

                        iVar16 = oVar8.physics1.Z;
                        oVar8.vTransform.position.y += iVar17 >> 7;

                        if (iVar16 < 0)
                            iVar16 += 127;

                        oVar8.vTransform.position.z += iVar16 >> 7;
                        return 0;
                    case 0x10:
                    case 0x30:
                        oVar17 = PDAT_74;
                        tVar8 = GameManager.instance.FUN_2CDF4(PDAT_78);
                        Utilities.FUN_248C4(oVar17.vTransform.rotation, tVar8.rotation, out auStack72);
                        iVar16 = -auStack72.V12;

                        if (0 < auStack72.V12)
                            iVar16 += 7;

                        iVar11 = auStack72.V02 - auStack72.V20;
                        iVar16 >>= 3;

                        if (iVar11 < 0)
                            iVar11 += 7;

                        iVar13 = auStack72.V10;
                        iVar11 >>= 3;
                        goto LAB_9D8;
                    case 0x11:
                    case 0x13:
                    case 0x15:
                    case 0x17:
                        vr.x += 32;
                        oVar16.vr.x += 32;

                        if (arg2 == 0)
                            return 0;

                        ApplyTransformation();
                        oVar16.ApplyTransformation();
                        sVar5 = (sbyte)DAT_19;
                        sVar4 = 23;
                        break;
                    case 0x12:
                    case 0x14:
                    case 0x16:
                        vr.x -= 64;
                        oVar16.vr.x -= 64;

                        if (arg2 == 0)
                            return 0;

                        ApplyTransformation();
                        oVar16.ApplyTransformation();
                        sVar5 = (sbyte)DAT_19;
                        sVar4 = 22;
                        break;
                    case 0x20:
                        do
                        {
                            sVar6 = -21;

                            if (oVar16.id != 0)
                                sVar6 = 21;

                            oVar16.vr.y += sVar6;
                            oVar16.child2.vr.y += sVar6;

                            if (arg2 != 0)
                            {
                                oVar16.ApplyTransformation();
                                oVar16.child2.ApplyTransformation();
                            }

                            oVar16 = oVar16.child;
                        } while (oVar16 != null);

                        return 0;
                    case 0x22:
                    case 0x37:
                        do
                        {
                            sVar6 = 21;

                            if (oVar16.id != 0)
                                sVar6 = -21;

                            oVar16.vr.y += sVar6;
                            oVar16.child2.vr.y += sVar6;

                            if (arg2 != 0)
                            {
                                oVar16.ApplyTransformation();
                                oVar16.child2.ApplyTransformation();
                            }

                            oVar16 = oVar16.child;
                        } while (oVar16 != null);

                        return 0;
                    case 0x23:
                        do
                        {
                            sVar6 = 21;

                            if (oVar16.id != 0)
                                sVar6 = -21;

                            oVar16.vr.y += sVar6;
                            oVar16.child2.vr.y += sVar6;

                            if (arg2 != 0)
                            {
                                oVar16.ApplyTransformation();
                                oVar16.child2.ApplyTransformation();
                            }

                            oVar16 = oVar16.child;
                        } while (oVar16.type == 0);

                        goto case 0x24;
                    case 0x24:
                        oVar17 = PDAT_74;
                        tVar8 = GameManager.instance.FUN_2CDF4(PDAT_78);
                        Utilities.FUN_248C4(oVar17.vTransform.rotation, tVar8.rotation, out auStack40);
                        iVar16 = -auStack40.V12;

                        if (0 < auStack40.V12)
                            iVar16 += 7;

                        iVar11 = auStack40.V02 - auStack40.V20;
                        iVar16 >>= 3;

                        if (iVar11 < 0)
                            iVar11 += 7;

                        iVar13 = auStack40.V10;
                        iVar11 >>= 3;
                        LAB_9D8:

                        if (iVar13 < 0)
                            iVar13 += 7;

                        oVar17.FUN_24700((short)iVar16, (short)iVar11, (short)(iVar13 >> 3));
                        iVar16 = tVar8.position.x - oVar17.vTransform.position.x;

                        if (iVar16 < 0)
                            iVar16 += 7;

                        iVar16 >>= 3;
                        oVar17.vTransform.position.x += iVar16;
                        iVar18 = tVar8.position.y - oVar17.vTransform.position.y;

                        if (iVar18 < 0)
                            iVar18 += 7;

                        iVar18 >>= 3;
                        oVar17.vTransform.position.y += iVar18;
                        iVar8 = tVar8.position.z - oVar17.vTransform.position.z;

                        if (iVar8 < 0)
                            iVar8 += 7;

                        iVar8 >>= 3;
                        oVar17.vTransform.position.z += iVar8;

                        if ((flags & 0x20000000) != 0)
                        {
                            if (iVar16 < 0)
                                iVar16 = -iVar16;

                            if (iVar16 < 0x800)
                            {
                                if (iVar18 < 0)
                                    iVar18 = -iVar18;

                                if (iVar18 < 0x800)
                                {
                                    if (iVar8 < 0)
                                        iVar8 = -iVar8;

                                    if (iVar8 < 0x800)
                                    {
                                        flags &= 0xdfffffff;
                                        iVar9 = GameManager.instance.FUN_1DD9C();
                                        v3Var10 = GameManager.instance.FUN_2CE50(oVar17);
                                        GameManager.instance.FUN_1E628(iVar9, vData.sndList, 6, v3Var10);
                                    }
                                }
                            }
                        }

                        oVar17.vTransform.rotation = Utilities.MatrixNormal(oVar17.vTransform.rotation);
                        return 0;
                    case 0x25:
                        oVar8 = PDAT_78;
                        vr.x += 32;

                        if (arg2 == 0)
                            return 0;

                        ApplyRotationMatrix();
                        goto LAB_BA4;
                    case 0x27:
                        vr.x -= 64;

                        if (arg2 == 0)
                            return 0;

                        ApplyTransformation();
                        return 0;
                    case 0x31:
                    case 0x33:
                    case 0x35:
                        vr.x += 32;

                        if (arg2 == 0)
                            return 0;

                        ApplyTransformation();
                        goto LAB_BA0;
                    case 0x32:
                    case 0x34:
                    case 0x36:
                        vr.x -= 64;

                        if (arg2 == 0)
                            return 0;

                        ApplyTransformation();
                        sVar5 = (sbyte)DAT_19;
                        sVar4 = 54;
                        break;
                }

                if (sVar5 == sVar4)
                    return 0;

                LAB_BA0:
                oVar8 = PDAT_78;
                LAB_BA4:
                puVar7 = GameManager.instance.FUN_2CDF4(oVar8);
                oVar16 = PDAT_74;
                oVar16.vTransform = puVar7;
                return 0;
            case 2:
                break;
            case 4:
                if (DAT_19 == 0)
                    return 0;

                if (PDAT_78 != null)
                {
                    oVar9 = PDAT_78.FUN_2CCBC();
                    Destroy(oVar9.gameObject);
                    PDAT_78 = null;
                }

                oVar16 = PDAT_74;

                if (oVar16 != null)
                {
                    vVar16 = (Vehicle)oVar16;
                    vVar16.FUN_41FEC();
                    uVar12 = vVar16.flags;
                    vVar16.flags = uVar12 & 0xffffffdd;

                    if ((uVar12 & 0x80) == 0)
                        vVar16.FUN_30B78();

                    if (vVar16.vCamera != null)
                    {
                        vVar16.vCamera.target = vVar16;
                        vVar16.vCamera.FUN_4B898();
                    }

                    PDAT_74 = null;
                }

                GameManager.instance.FUN_1DE78(DAT_18);
                return 0;
            default:
                return 0;
        }

        ApplyTransformation();
        child2.ApplyTransformation();
        sVar5 = (sbyte)(DAT_19 + 1);
        DAT_19 = (byte)sVar5;

        switch (sVar5)
        {
            case 1:
            case 0x21:
                FUN_30BA8();
                GameManager.instance.FUN_30CB0(this, 128);
                flags &= 0xffffffdf;
                oVar16 = Utilities.FUN_2CD78(this);
                oVar16.flags |= 0x800;
                break;
            case 2:
            case 0x22:
                FUN_30B78();
                GameManager.instance.FUN_30CB0(this, 16);
                flags |= 0x20;
                oVar16 = Utilities.FUN_2CD78(this);
                oVar16.flags &= 0xfffff7ff;
                iVar9 = GameManager.instance.FUN_1DD9C();
                GameManager.instance.FUN_1E628(iVar9, vData.sndList, 4, oVar16.vTransform.position);
                break;
            case 3:
            case 0x23:
                FUN_30BA8();
                DAT_19 = 0;

                if (maxHalfHealth == 0)
                {
                    FUN_3A368();
                    return 0;
                }

                break;
            case 4:
            case 0x25:
                iVar9 = 32;
                goto LAB_11A0;
            case 5:
            case 0x26:
                LevelManager.instance.FUN_4DE54(PDAT_74.vTransform.position, 138);
                GameManager.instance.FUN_30CB0(this, 24);
                iVar9 = GameManager.instance.FUN_1DD9C();
                v3Var10 = GameManager.instance.FUN_2CE50(this);
                GameManager.instance.FUN_1E580(iVar9, GameManager.instance.DAT_C2C, 36, v3Var10);
                break;
            case 6:
            case 0x28:
                oVar8 = PDAT_74;
                vVar16 = (Vehicle)Utilities.FUN_2CD78(this);
                oVar8.flags |= 2;
                sVar5 = (sbyte)GameManager.instance.FUN_1DD9C();
                DAT_18 = sVar5;
                GameManager.instance.FUN_1E580(sVar5, vData.sndList, 3, oVar8.vTransform.position, true);

                if (oVar8.id < 0)
                    GameManager.instance.FUN_15AA8(~oVar8.id, 255, 255, 255, 64);

                if (vVar16.id < 0)
                    GameManager.instance.FUN_15B00(~vVar16.id, 255, 255, 64);

                oVar9 = PDAT_78.FUN_2CCBC();
                Destroy(oVar9.gameObject);
                PDAT_78 = null;
                GameManager.instance.FUN_30CB0(this, 256);
                bVar7 = vVar16.body[1].child2 as Body;

                if (bVar7 != null)
                {
                    do
                    {
                        if (bVar7.id - 16 < 2)
                        {
                            bVar7.state = _BODY_TYPE.Collector;
                            bVar7.tags = (sbyte)((byte)bVar7.id & 1);
                            GameManager.instance.FUN_30CB0(bVar7, 232);
                        }

                        bVar7 = bVar7.child as Body;
                    } while (bVar7 != null);

                    return 0;
                }

                break;
            case 7:
            case 0x29:
                oVar8 = PDAT_74;
                oVar8.flags &= 0xfffffffd;
                ccVar9 = FUN_2C5F4(0x8001);
                oVar8.vTransform = GameManager.instance.FUN_2CEAC(this, ccVar9);
                iVar16 = oVar8.vTransform.rotation.V02 * 4577;

                if (iVar16 < 0)
                    iVar16 += 31;

                oVar8.physics1.X = iVar16 >> 5;
                iVar16 = oVar8.vTransform.rotation.V12 * 4577;

                if (iVar16 < 0)
                    iVar16 += 31;

                oVar8.physics1.Y = iVar16 >> 5;
                iVar16 = oVar8.vTransform.rotation.V22 * 4577;

                if (iVar16 < 0)
                    iVar16 += 31;

                oVar8.physics1.Z = iVar16 >> 5;
                GameManager.instance.FUN_30CB0(this, 24);
                GameManager.instance.FUN_1E580(DAT_18, GameManager.instance.DAT_C2C, 37, oVar8.vTransform.position);
                DAT_18 = 0;
                break;
            case 8:
            case 0x2A:
                vVar16 = (Vehicle)PDAT_74;
                vVar16.FUN_41FEC();
                vVar16.flags &= 0xfdffffdf;
                vVar16.FUN_30B78();
                PDAT_74 = null;
                goto LAB_1210;
            case 0x13:
            case 0x15:
            case 0x33:
            case 0x35:
                iVar9 = GameManager.instance.FUN_1DD9C();
                v3Var10 = GameManager.instance.FUN_2CE50(this);
                GameManager.instance.FUN_1E628(iVar9, vData.sndList, 5, v3Var10);
                goto case 0x11;
            case 0x11:
            case 0x31:
                oVar9 = Utilities.FUN_2CD78(this);
                iVar9 = ((Vehicle)PDAT_74).FUN_3B078(oVar9, (ushort)DAT_1A, -150, 1);
                ((Vehicle)PDAT_74).FUN_3A020(iVar9, DAT_20, true);
                goto case 0x17;
            case 0x17:
                iVar9 = 32;
                goto LAB_11A0;
            case 0x16:
            case 0x36:
                oVar8 = PDAT_78;
                tVar9 = GameManager.instance.FUN_2CDF4(this);
                oVar16 = Utilities.FUN_2CD78(this);
                local_58 = new Vector3Int
                    (0, oVar8.vTransform.position.z, -oVar8.vTransform.position.y);
                local_58 = Utilities.FUN_24094(tVar9.rotation, local_58);
                vVar8 = (Vehicle)PDAT_74;
                vVar8.FUN_41FEC();
                vVar8.physics1.X = oVar16.physics1.X + local_58.x * 16;
                vVar8.physics1.Y = oVar16.physics1.Y + local_58.y * 16;
                vVar8.physics1.Z = oVar16.physics1.Z + local_58.z * 16;
                vVar8.physics2.Z = -0x40000;
                vVar8.flags &= 0xffffffdf;
                vVar8.FUN_30B78();
                iVar9 = GameManager.instance.FUN_1DD9C();
                v3Var10 = GameManager.instance.FUN_2CE50(this);
                GameManager.instance.FUN_1E628(iVar9, vData.sndList, 4, v3Var10);
                oVar9 = PDAT_78.FUN_2CCBC();
                Destroy(oVar9.gameObject);
                PDAT_78 = null;
                iVar9 = vVar8.FUN_3B078(oVar16, (ushort)DAT_1A, -150, 1);
                vVar8.FUN_3A020(iVar9, DAT_20, true);
                goto case 0x12;
            case 0x12:
            case 0x14:
            case 0x32:
            case 0x34:
                iVar12 = PDAT_74.id;

                if (iVar12 < 0)
                    GameManager.instance.FUN_15B00(~iVar12, 255, 0, 64);

                iVar9 = 16;
                goto LAB_11A0;
            case 0x18:
            case 0x38:
                vVar16 = (Vehicle)PDAT_74;
                PDAT_74 = null;
                vVar16.flags &= 0xfdffffdf;
                LAB_1210:
                DAT_19 = 0;
                FUN_30BA8();

                if (maxHalfHealth == 0)
                    FUN_3A368();

                if (vVar16.vCamera != null)
                {
                    vVar16.vCamera.target = vVar16;
                    vVar16.vCamera.FUN_4B898();
                }

                break;
            case 0x24:
                GameManager.instance.FUN_30CB0(this, 48);
                break;
            case 0x27:
                iVar9 = 16;
                PDAT_74.flags |= 2;
                LAB_11A0:
                GameManager.instance.FUN_30CB0(this, iVar9);
                iVar9 = GameManager.instance.FUN_1DD9C();
                v3Var10 = GameManager.instance.FUN_2CE50(this);
                GameManager.instance.FUN_1E628(iVar9, vData.sndList, 4, v3Var10);
                break;
            case 0x37:
                GameManager.instance.FUN_30CB0(this, 16);
                break;
        }

        return 0;
    }

    
    public override uint UpdateW(int arg1, VigObject arg2)
    {
        bool bVar3;
        int iVar9;
        Vector3Int v3Var10;
        Vector3Int local_58;

        switch (arg1)
        {
            case 1:
                type = 3;
                maxHalfHealth = 6;

                if (-1 < arg2.id)
                    return 0;

                //salvage points
                return 0;
            default:
                return 0;
            case 12:
                if (DAT_19 == 0)
                {
                    arg2.tags = 3;
                    PDAT_74 = null;
                    DAT_19 = (byte)(((flags & 0x10000000) != 0 ? 1 : 0) << 5);
                    FUN_30B78();
                    GameManager.instance.FUN_30CB0(this, 15);
                    iVar9 = GameManager.instance.FUN_1DD9C();
                    v3Var10 = GameManager.instance.FUN_2CE50(this);
                    GameManager.instance.FUN_1E628(iVar9, vData.sndList, 4, v3Var10);
                    iVar9 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E188(iVar9, vData.sndList, 2);
                    maxHalfHealth--;
                }
                else
                {
                    iVar9 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E14C(iVar9, GameManager.instance.DAT_C2C, 1);
                }

                if (-1 < arg2.id)
                    return 900;

                return 480;
            case 13:
                if (DAT_19 != 0)
                    return 0;

                local_58 = Utilities.FUN_24304(arg2.vTransform, ((Vehicle)arg2).target.vTransform.position);

                if (0x22ffe < (uint)local_58.z - 1)
                    return 0;

                if (local_58.x < 0)
                    local_58.x = -local_58.x;

                bVar3 = local_58.x < 0x10000;

                if (0x10000 < local_58.z)
                    bVar3 = local_58.x < local_58.z;

                if (!bVar3)
                    return 0;

                return 1;
        }

        return 0;
    }
}
