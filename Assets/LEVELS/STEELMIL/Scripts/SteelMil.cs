using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteelMil : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        GameManager.instance.FUN_17F34(0x16800, 0x5830000);
    }

    public static SteelMil instance;
    public int DAT_4600; //0x4600 (STEELMIL.DLL)
    public int DAT_4614; //0x4614 (STEELMIL.DLL)
    public byte[] DAT_4618 = new byte[20]; //0x4618 (STEELMIL.DLL)
    
    //FUN_628 (STEELMIL.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        VigObject oVar2;
        uint uVar6;
        VigObject oVar6;

        switch (arg1)
        {
            case 1:
                GameManager.instance.offsetFactor = 2.5f;
                GameManager.instance.offsetStart = 0;
                GameManager.instance.angleOffset = 0.4f;
                Color32 color = LevelManager.instance.DAT_DE0;
                color.a = 0x80;
                UIManager.instance.underwater.color = color;
                FUN_34FC();
                oVar6 = GameManager.instance.FUN_30250(GameManager.instance.DAT_1078, 0x100);
                oVar2 = GameManager.instance.FUN_4AC1C(0xfe000000, oVar6);
                GameManager.instance.DAT_1038 = oVar2 != null ? 1 : 0;
                DAT_4600 = 0;
                goto case 2;
            case 2:
                GameManager.instance.FUN_34B34();
                GameManager.instance.FUN_30CB0(this, 240);
                uVar6 = 0;
                break;
            case 17:
                GameManager.instance.FUN_17EB8();
                uVar6 = 0;
                break;
            default:
                uVar6 = 0;
                break;
        }

        return uVar6;
    }

    public override uint UpdateW(VigObject arg1, int arg2, int arg3)
    {
        sbyte sVar1;
        int iVar2;
        TileData tVar2;
        VigObject oVar2;
        VigCamera cVar2;
        VigObject oVar3;
        Spark puVar4;
        VigObject oVar5;
        int iVar6;
        ConfigContainer ccVar6;
        VigCollider cVar6;
        Vehicle vVar7;
        VigCamera cVar8;
        Vector3Int local_18;

        switch (arg2)
        {
            case 18:
                if (arg3 != 0 && arg1.type == 8)
                {
                    tVar2 = GameManager.instance.terrain.GetTileByPosition((uint)arg1.vTransform.position.x, (uint)arg1.vTransform.position.z);

                    if (tVar2.DAT_10[3] == 1)
                    {
                        GameObject obj = new GameObject();
                        puVar4 = obj.AddComponent<Spark>();
                        oVar2 = LevelManager.instance.xobfList[19].ini.FUN_2C17C(153, typeof(VigObject), 8);
                        Utilities.ParentChildren(oVar2, oVar2);
                        oVar3 = LevelManager.instance.xobfList[19].ini.FUN_2C17C(51, typeof(VigObject), 8);
                        Utilities.ParentChildren(oVar3, oVar3);
                        oVar5 = GameManager.instance.FUN_320DC(arg1.vTransform.position, 0);
                        oVar3.vTransform = GameManager.FUN_2A39C();
                        oVar2.vTransform = GameManager.FUN_2A39C();
                        oVar2.flags = 0x10;
                        Utilities.FUN_2CC9C(puVar4, oVar2);
                        oVar2.transform.parent = puVar4.transform;
                        Utilities.FUN_2CC9C(puVar4, oVar3);
                        oVar3.transform.parent = puVar4.transform;
                        puVar4.flags = 0x84;
                        puVar4.type = 3;
                        puVar4.screen = arg1.vTransform.position;
                        cVar6 = new VigCollider(oVar2.vCollider.reader.GetBuffer());
                        puVar4.physics1.M6 = 0x1000;
                        puVar4.vCollider = cVar6;
                        Utilities.FUN_2A168(out local_18, arg1.vTransform.position, oVar5.vTransform.position); //originally - oVar5.screen
                        iVar2 = local_18.x * 3051;

                        if (iVar2 < 0)
                            iVar2 += 4095;

                        puVar4.physics1.X = iVar2 >> 12;
                        puVar4.physics1.Y = 0;
                        iVar2 = local_18.z * 3051;

                        if (iVar2 < 0)
                            iVar2 += 4095;

                        puVar4.physics1.Z = iVar2 >> 12;
                        puVar4.FUN_3066C();
                        sVar1 = (sbyte)GameManager.instance.FUN_1DD9C();
                        puVar4.DAT_18 = sVar1;
                        GameManager.instance.FUN_1E580(sVar1, LevelManager.instance.xobfList[42].sndList, 0, arg1.vTransform.position, true);
                        return 0;
                    }
                }

                GameManager.instance.FUN_327CC(arg1);
                return 0;
            case 19:
                iVar2 = (int)GameManager.FUN_2AC5C();
                oVar3 = GameManager.instance.FUN_318D0((iVar2 * 3 >> 15) + 40);
                ccVar6 = oVar3.FUN_2C5F4(0x8000);
                arg1.vTransform = GameManager.instance.FUN_2CEAC(oVar3, ccVar6);
                arg1.screen = arg1.vTransform.position;
                iVar2 = arg1.vTransform.rotation.V02 * 7629;
                arg1.flags |= 0x20;

                if (iVar2 < 0)
                    iVar2 += 31;

                arg1.physics1.X = iVar2 >> 5;
                iVar2 = arg1.vTransform.rotation.V12 * 7629;

                if (iVar2 < 0)
                    iVar2 += 31;

                arg1.physics1.Y = iVar2 >> 5;
                iVar2 = arg1.vTransform.rotation.V22 * 7629;

                if (iVar2 < 0)
                    iVar2 += 31;

                arg1.physics1.Z = iVar2 >> 5;
                arg1.physics2.X = 0;
                arg1.physics2.Y = 0;
                arg1.physics2.Z = 0;
                iVar6 = GameManager.instance.FUN_1DD9C();
                GameManager.instance.FUN_1E580(iVar6, GameManager.instance.DAT_C2C, 37, arg1.vTransform.position);
                vVar7 = (Vehicle)arg1;
                cVar2 = vVar7.vCamera;

                if (cVar2 != null)
                {
                    oVar3 = GameManager.instance.FUN_30250(GameManager.instance.DAT_1078, oVar3.id + 472);
                    oVar3.tags = 90;
                    oVar3.maxHalfHealth = 0x100;
                    GameManager.instance.FUN_30CB0(cVar2, oVar3.tags);
                    cVar2.screen = oVar3.screen;
                    cVar8 = LevelManager.instance.FUN_4B984(vVar7, oVar3);
                    vVar7.vCamera = cVar8;
                    LevelManager.instance.defaultCamera.transform.SetParent(cVar8.transform, false);
                    cVar8.FUN_30B78();
                }

                vVar7.state = _VEHICLE_TYPE.MilTunnel;
                GameManager.instance.FUN_30CB0(vVar7, 30);
                return 0xffffffff;
        }

        return 0;
    }

    private static void FUN_34FC()
    {
        long lVar1;
        long lVar2;
        int iVar3;
        int iVar4;
        int iVar5;
        uint uVar6;
        int iVar7;
        int iVar8;
        RSEG_DB rVar9;
        int iVar10;
        int iVar11;
        JUNC_DB jVar12;
        int iVar13;
        uint uVar14;
        int local_30;

        local_30 = 0;

        if (0 < LevelManager.instance.DAT_1184)
        {
            do
            {
                jVar12 = LevelManager.instance.juncList[local_30];

                if (jVar12.DAT_18 != null && (jVar12.DAT_10 & 0x40) != 0)
                {
                    iVar4 = (jVar12.DAT_16 & 0xfff) * 2;
                    iVar3 = -GameManager.DAT_65C90[iVar4];
                    iVar4 = GameManager.DAT_65C90[iVar4 + 1];
                    iVar10 = jVar12.DAT_11 - 1;

                    if (-1 < iVar10)
                    {
                        do
                        {
                            rVar9 = jVar12.DAT_1C[iVar10];
                            iVar8 = jVar12 == rVar9.DAT_00[1] ? 1 : 0;
                            lVar1 = (long)iVar4 * rVar9.DAT_10[iVar8];
                            lVar2 = (long)iVar3 * rVar9.DAT_14[iVar8];
                            uVar14 = (uint)lVar2;
                            uVar6 = (uint)lVar1 + uVar14;
                            iVar7 = (int)((ulong)lVar1 >> 32) + (int)((ulong)lVar2 >> 32) + (uVar6 < uVar14 ? 1 : 0);
                            iVar5 = FUN_45C0(uVar6, iVar7, 0, 0);

                            if (iVar5 < 1)
                            {
                                uVar6 += 4095;
                                iVar7 += (uVar6 < 4095 ? 1 : 0);
                            }

                            uVar6 = uVar6 >> 12 | (uint)iVar7 << 20;
                            lVar1 = (long)iVar4 * (long)(int)uVar6;
                            uVar14 = (uint)lVar1;
                            iVar7 = (int)((ulong)lVar1 >> 32);
                            iVar5 = FUN_45C0(uVar14, iVar7, 0, 0);

                            if (iVar5 < 1)
                            {
                                uVar14 += 4095;
                                iVar7 += (uVar14 < 4095 ? 1 : 0);
                            }

                            lVar1 = (long)iVar3 * (long)(int)uVar6;
                            uVar6 = (uint)lVar1;
                            iVar13 = (int)((ulong)lVar1 >> 32);
                            rVar9.DAT_10[iVar8] = (int)(uVar14 >> 12 | (uint)iVar7 << 20);
                            iVar5 = FUN_45C0(uVar6, iVar13, 0, 0);

                            if (iVar5 < 1)
                            {
                                uVar6 += 4095;
                                iVar13 += (uVar6 < 4095 ? 1 : 0);
                            }

                            rVar9.DAT_14[iVar8] = (int)(uVar6 >> 12 | (uint)iVar13 << 20);
                            rVar9.FUN_50EFC();
                            iVar10--;
                        } while (-1 < iVar10);
                    }
                }

                local_30++;
            } while (local_30 < LevelManager.instance.DAT_1184);
        }
    }

    private static int FUN_45C0(uint param1, int param2, uint param3, int param4)
    {
        int iVar1;

        iVar1 = 0;

        if (param4 <= param2)
        {
            iVar1 = 2;

            if (param2 <= param4)
            {
                if (param1 < param3)
                    iVar1 = 0;
                else
                {
                    iVar1 = 2;

                    if (param1 <= param3)
                        iVar1 = 1;
                }
            }
        }

        return iVar1;
    }
}
