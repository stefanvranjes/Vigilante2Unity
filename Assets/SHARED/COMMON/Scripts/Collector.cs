using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //0x20 (LUNAR.DLL)
    private static Vector3Int DAT_20 = new Vector3Int(0, 0, 0);
    //0x710 (LUNAR.DLL)
    private static Vector3Int DAT_710 = new Vector3Int(0, -0x80000, 0x40000);
    //0x720 (LUNAR.DLL)
    private static int[] DAT_720 = new int[4] { 14, 24, 19, 24 };

    public static VigObject OnInitialize(XOBF_DB arg1, int arg2)
    {
        return arg1.ini.FUN_2C17C(2, typeof(Collector), 8);
    }

    public override uint OnCollision(HitDetection hit)
    {
        int iVar3;
        ConfigContainer ccVar3;
        int iVar4;
        uint uVar5;
        int iVar6;
        VigObject oVar7;
        Vehicle vVar7;
        int iVar8;
        uint uVar8;
        VigObject oVar8;
        int iVar9;
        VigTransform asStack64;
        Vector3Int iStack72;
        Vector3Int iStack32;

        oVar7 = hit.self;
        uVar8 = 0;
        
        if ((oVar7.type == 2) && (PDAT_74 == null || PDAT_74 == oVar7))
        {
            vVar7 = (Vehicle)oVar7;
            ccVar3 = FUN_2C5F4(0x8000);
            asStack64 = GameManager.instance.FUN_2CEAC(this, ccVar3);
            vVar7.vTransform.position.x += (asStack64.position.x - vVar7.vTransform.position.x) / 2;
            vVar7.vTransform.position.y += (asStack64.position.y - vVar7.vTransform.position.y) / 2;
            vVar7.vTransform.position.z += (asStack64.position.z - vVar7.vTransform.position.z) / 2;
            iVar4 = IDAT_78 - 1;
            PDAT_74 = hit.self;
            IDAT_78 = iVar4;

            if (iVar4 < 1)
            {
                uVar5 = GameManager.FUN_2AC5C();
                IDAT_78 = (int)(uVar5 & 31) + 15;
                //FUN_4E414

                if ((uVar5 & 0x1c0) == 0)
                {
                    if (vVar7.weapons[0] != null && vVar7.weapons[0].tags < 8)
                    {
                        vVar7.weapons[0].FUN_3A368();
                        return 0;
                    }

                    LevelManager.instance.FUN_4DE54(asStack64.position, 142);
                }
                else
                {
                    iVar8 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E628(iVar8, GameManager.instance.DAT_C2C, DAT_720[uVar5 & 3], asStack64.position);

                    if ((uVar5 & 1) == 0)
                    {
                        iStack72 = new Vector3Int(asStack64.rotation.V02, asStack64.rotation.V12, asStack64.rotation.V22);
                        LevelManager.instance.FUN_4EAE8(asStack64.position, iStack72, 148);
                    }
                    else
                        LevelManager.instance.FUN_4E56C(asStack64, 123);
                }

                oVar8 = Utilities.FUN_2CD78(this);
                uVar5 = GameManager.FUN_2AC5C();
                iStack32 = new Vector3Int();
                iStack32.x = (int)((uVar5 & 127) - 64) * 128;
                iStack32.y = 0x12000;
                iStack32.z = 0;
                oVar8.FUN_2B1FC(iStack32, ccVar3.v3_1);
            }

            Utilities.FUN_248C4(vVar7.vTransform.rotation, asStack64.rotation, out asStack64.rotation);
            iVar3 = -asStack64.rotation.V10;

            if (0 < asStack64.rotation.V10)
                iVar3 += 7;

            iVar9 = asStack64.rotation.V00 + asStack64.rotation.V22;

            if (iVar9 < 0)
                iVar9 += 7;

            iVar6 = -asStack64.rotation.V12;

            if (0 < asStack64.rotation.V12)
                iVar6 += 7;

            vVar7.FUN_24700((short)(iVar3 >> 3), (short)(iVar9 >> 3), (short)(iVar6 >> 3));
            vVar7.vTransform.rotation = Utilities.MatrixNormal(vVar7.vTransform.rotation);

            if (vVar7.id < 0)
            {
                uVar5 = GameManager.FUN_2AC5C();

                if ((uVar5 & 31) == 0)
                    GameManager.instance.FUN_15AA8(~vVar7.id, 8, 255, 0, 16);
            }

            oVar8 = Utilities.FUN_2CD78(this);
            iVar8 = vVar7.FUN_3B078(oVar8, (ushort)DAT_1A, -1, 1);
            vVar7.FUN_39DCC(iVar8, DAT_20, true);
            uVar8 = 1;
        }

        return uVar8;
    }

    //FUN_70 (LUNAR.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        sbyte sVar2;
        int iVar3;
        VigObject oVar7;
        int iVar8;
        uint uVar8;
        VigObject oVar8;
        ConfigContainer ccVar8;
        VigTransform tVar8;
        VigTransform auStack112;
        Vector3Int aiStack80;

        switch (arg1)
        {
            case 0:
                FUN_42330(arg2);
                uVar8 = 0;
                break;
            default:
                uVar8 = 0;
                break;
            case 2:
                ApplyTransformation();
                child2.ApplyTransformation();
                sVar2 = (sbyte)(DAT_19 + 1);
                DAT_19 = (byte)sVar2;

                if (sVar2 != 2)
                    return 0;

                FUN_30BA8();

                if (DAT_18 != 0)
                {
                    GameManager.instance.FUN_1DE78(DAT_18);
                    DAT_18 = 0;
                }

                oVar7 = PDAT_74;

                if (oVar7 != null)
                {
                    ccVar8 = FUN_2C5F4(0x8000);
                    auStack112 = GameManager.instance.FUN_2CEAC(this, ccVar8);
                    iVar3 = Utilities.FUN_29F6C(auStack112.position, oVar7.vTransform.position);

                    if (iVar3 < 0x10000)
                    {
                        oVar7.flags &= 0xffffffdf;
                        oVar8 = Utilities.FUN_2CD78(this);
                        tVar8 = GameManager.instance.FUN_2CDF4(oVar8);
                        aiStack80 = Utilities.FUN_24094(tVar8.rotation, DAT_710);
                        oVar7.FUN_2B370(aiStack80, oVar7.vTransform.position);
                        oVar7.physics2.Z = -0x20000;
                        iVar8 = GameManager.instance.FUN_1DD9C();
                        GameManager.instance.FUN_1E628(iVar8, GameManager.instance.DAT_C2C, 46, oVar7.vTransform.position);
                    }
                }

                FUN_2C124(2);
                Utilities.ParentChildren(this, this);
                DAT_19 = 0;
                flags |= 0x20;

                if (maxHalfHealth == 0)
                {
                    FUN_3A368();
                    return 0;
                }

                goto default;
            case 4:
                if (DAT_18 == 0)
                    return 0;

                GameManager.instance.FUN_1DE78(DAT_18);
                DAT_18 = 0;
                goto default;
        }

        return uVar8;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        bool bVar1;
        sbyte sVar2;
        VigObject oVar7;
        int iVar8;
        uint uVar8;
        Vector3Int aiStack80;

        switch (arg1)
        {
            case 0:
                FUN_42330(arg2);
                uVar8 = 0;
                break;
            case 1:
                type = 3;
                maxHalfHealth = 6;
                goto default;
            default:
                uVar8 = 0;
                break;
            case 12:
                if (DAT_19 == 0)
                {
                    arg2.tags = 3;
                    DAT_19 = 1;
                    FUN_2C124(1);
                    FUN_30B78();
                    Utilities.ParentChildren(this, this);
                    GameManager.instance.FUN_30CB0(this, 239);
                    sVar2 = (sbyte)GameManager.instance.FUN_1DD9C();
                    DAT_18 = sVar2;
                    GameManager.instance.FUN_1E14C(sVar2, vData.sndList, 3, true);
                    iVar8 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E188(iVar8, vData.sndList, 2);
                    oVar7 = Utilities.FUN_2CD78(this);
                    oVar7.flags |= 0x800;
                    IDAT_78 = 0;
                    PDAT_74 = null;
                    flags &= 0xffffffdf;
                    maxHalfHealth--;
                }
                else
                {
                    iVar8 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E14C(iVar8, GameManager.instance.DAT_C2C, 1);
                }

                uVar8 = 900;

                if (arg2.id < 0)
                    uVar8 = 600;

                break;
            case 13:
                uVar8 = 0;

                if (DAT_19 == 0)
                {
                    aiStack80 = Utilities.FUN_24304(arg2.vTransform, ((Vehicle)arg2).target.vTransform.position);
                    uVar8 = 0;

                    if ((uint)aiStack80.z - 1 < 0x22fff)
                    {
                        if (aiStack80.x < 0)
                            aiStack80.x = -aiStack80.x;

                        bVar1 = aiStack80.x < 0x10000;

                        if (0x10000 < aiStack80.z)
                            bVar1 = aiStack80.x < aiStack80.z;

                        uVar8 = 0;

                        if (bVar1)
                            uVar8 = 1;
                    }
                }

                break;
        }

        return uVar8;
    }
}
