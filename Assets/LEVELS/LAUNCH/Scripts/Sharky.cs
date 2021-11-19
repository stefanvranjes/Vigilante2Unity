using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sharky : VigObject
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
        int iVar7;
        uint uVar8;
        JUNC_DB puVar9;
        int iVar12;
        RSEG_DB rVar12;
        int iVar17;
        VigObject oVar17;
        Vehicle vVar17;
        Vector3Int auStack24;

        oVar17 = hit.self;

        if (oVar17.type == 2 && oVar17.id != 0)
        {
            vVar17 = (Vehicle)oVar17;

            if (tags == 3)
            {
                GameManager.instance.FUN_2F798(this, hit);
                auStack24 = Utilities.FUN_24148(vTransform, hit.position);
                LevelManager.instance.FUN_4DE54(auStack24, 142);
                UIManager.instance.FUN_4E414(auStack24, new Color32(0x80, 0x80, 0x80, 8));
                LevelManager.instance.FUN_4EAE8(auStack24, hit.normal1, 148);
                iVar7 = GameManager.instance.FUN_1DD9C();
                GameManager.instance.FUN_1E628(iVar7, GameManager.instance.DAT_C2C, 24, auStack24);
                vVar17.FUN_3A064(-100, auStack24, true);
                vVar17.physics1.Y += 0x1dcd00;
                iVar12 = vTransform.rotation.V02 * 3814;

                if (iVar12 < 0)
                    iVar12 += 31;

                vVar17.physics1.X += iVar12 >> 5;
                iVar12 = vTransform.rotation.V22 * 3814;

                if (iVar12 < 0)
                    iVar12 += 31;

                vVar17.physics1.Z += iVar12 >> 5;
                uVar8 = flags;
            }
            else
            {
                if (tags == 0)
                    return 0;

                iVar7 = GameManager.instance.FUN_1DD9C();
                GameManager.instance.FUN_1E580(iVar7, GameManager.instance.DAT_C2C, 38, vTransform.position);
                GameManager.instance.FUN_1E30C(iVar7, 6500);
                uVar8 = flags;
                tags = 3;
                maxHalfHealth = 32;
            }

            flags = uVar8 | 0x20;
            return 0;
        }

        if (oVar17.type != 8)
            return 0;

        if (tags != 0)
        {
            tags = 0;
            return 0;
        }

        iVar17 = 0;

        if (DAT_84_2.DAT_11 == 2)
            iVar17 = DAT_19;

        rVar12 = DAT_84_2.DAT_1C[iVar17];
        iVar17 = 1 - DAT_19;
        DAT_19 = (byte)iVar17;
        puVar9 = rVar12.DAT_00[iVar17];
        DAT_84_2 = puVar9;
        physics1.Z = puVar9.DAT_00.x;
        physics1.W = puVar9.DAT_00.y;
        physics2.X = puVar9.DAT_00.z;
        return 0;
    }

    public JUNC_DB DAT_84_2; //0x84

    //FUN_3E28 (LAUNCH.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        sbyte sVar1;
        short sVar2;
        short sVar3;
        bool bVar4;
        uint uVar5;
        int iVar6;
        JUNC_DB jVar6;
        uint uVar8;
        JUNC_DB puVar9;
        Sharky2 ppcVar10;
        RSEG_DB piVar11;
        int iVar12;
        int iVar13;
        JUNC_DB jVar13;
        RSEG_DB piVar14;
        int iVar15;
        int iVar17;
        List<JUNC_DB> piVar18;

        if (arg1 == 1)
        {
            piVar11 = LevelManager.instance.FUN_518DC(screen, id);
            puVar9 = piVar11.DAT_00[0];
            DAT_84_2 = puVar9;
            physics1.Z = puVar9.DAT_00.x;
            physics1.W = puVar9.DAT_00.y;
            physics2.X = puVar9.DAT_00.z;
            flags |= 0x10184;

            if (id == 16)
            {
                GameObject obj = new GameObject();
                ppcVar10 = obj.AddComponent<Sharky2>();
                ppcVar10.type = 255;
                GameManager.instance.FUN_30CB0(ppcVar10, 60);
                iVar17 = LevelManager.instance.DAT_1184;
                iVar12 = 0;
                piVar18 = LevelManager.instance.juncList;

                if (0 < LevelManager.instance.DAT_1184)
                {
                    do
                    {
                        jVar6 = piVar18[iVar12];
                        iVar15 = 0;

                        if (jVar6.DAT_11 != 0)
                        {
                            do
                            {
                                piVar14 = jVar6.DAT_1C[iVar15];

                                if (piVar14.DAT_08 - 16 < 4)
                                {
                                    jVar6.DAT_12 = (short)piVar14.DAT_08;

                                    if (jVar6 == piVar14.DAT_00[0] && jVar6.DAT_1C[0] != piVar14)
                                    {
                                        jVar6.DAT_1C[1] = jVar6.DAT_1C[0];
                                        jVar6.DAT_1C[0] = piVar14;
                                        break;
                                    }
                                }

                                iVar15++;
                            } while (iVar15 < jVar6.DAT_11);
                        }

                        iVar12++;
                    } while (iVar12 < iVar17);
                }
            }

            GameManager.instance.FUN_30CB0(this, 60);
            return 0;
        }

        if (arg1 == 0)
        {
            sVar1 = tags;

            if (sVar1 != 3)
            {
                iVar17 = physics1.Z - vTransform.position.x;
                iVar12 = physics2.X - vTransform.position.z;

                if (sVar1 == 1)
                {
                    iVar13 = iVar12;

                    if (iVar12 < 0)
                        iVar13 = -iVar12;

                    iVar6 = iVar17;

                    if (iVar17 < 0)
                        iVar6 = -iVar17;

                    if (iVar13 < iVar6)
                        iVar13 = iVar6;

                    if (0xffff < iVar13) goto LAB_419C;

                    if (DAT_84_2.DAT_11 == 2)
                        iVar13 = (1 - DAT_19);
                    else
                        iVar13 = 0;

                    puVar9 = DAT_84_2.DAT_1C[iVar13].DAT_00[DAT_19];
                    DAT_84_2 = puVar9;
                }
                else
                {
                    if (1 < sVar1)
                    {
                        if (sVar1 == 2)
                        {
                            iVar13 = iVar12;

                            if (iVar12 < 0)
                                iVar13 = -iVar12;

                            iVar6 = iVar17;

                            if (iVar17 < 0)
                                iVar6 = -iVar17;

                            if (iVar13 < iVar6)
                                iVar13 = iVar6;

                            if (iVar13 < 0xfa001 && 0x2e67ff < GameManager.instance.terrain.FUN_1B750((uint)vTransform.position.x, (uint)vTransform.position.z))
                            {
                                iVar17 = DAT_80.vTransform.position.x - vTransform.position.x;
                                iVar12 = DAT_80.vTransform.position.z - vTransform.position.z;
                            }
                            else
                                tags = 0;
                        }

                        goto LAB_419C;
                    }

                    if (sVar1 != 0) goto LAB_419C;

                    iVar13 = iVar12;

                    if (iVar12 < 0)
                        iVar13 = -iVar12;

                    iVar6 = iVar17;

                    if (iVar17 < 0)
                        iVar6 = -iVar17;

                    if (iVar13 < iVar6)
                        iVar13 = iVar6;

                    if (0xffff < iVar13) goto LAB_419C;

                    jVar13 = DAT_84_2;

                    if (jVar13.DAT_11 == 2)
                    {
                        uVar8 = GameManager.FUN_2AC5C();
                        iVar6 = (int)(uVar8 & 1);
                    }
                    else
                        iVar6 = 0;

                    piVar11 = jVar13.DAT_1C[iVar6];
                    bVar4 = jVar13 == piVar11.DAT_00[0];
                    DAT_19 = (byte)(bVar4 ? 1 : 0);
                    puVar9 = piVar11.DAT_00[DAT_19];
                    DAT_84_2 = puVar9;
                }

                physics1.Z = puVar9.DAT_00.x;
                physics1.W = puVar9.DAT_00.y;
                physics2.X = puVar9.DAT_00.z;
                LAB_419C:
                iVar17 = Utilities.Ratan2(iVar17, iVar12);
                iVar12 = ((iVar17 - (ushort)vr.y) * 0x100000) >> 20;
                iVar17 = -45;

                if (-46 < iVar12)
                {
                    iVar17 = 45;

                    if (iVar12 < 46)
                        iVar17 = iVar12;
                }

                vr.y += iVar17;
                iVar17 = (vr.y & 0xfff) * 2;
                sVar2 = GameManager.DAT_65C90[iVar17];
                sVar3 = GameManager.DAT_65C90[iVar17 + 1];
                iVar17 = sVar2 * 7629;

                if (iVar17 < 0)
                    iVar17 += 4095;

                iVar12 = sVar3 * 7629;
                vTransform.position.x += iVar17 >> 12;

                if (iVar12 < 0)
                    iVar12 += 4095;

                vTransform.rotation.V20 = (short)-sVar2;
                iVar17 = GameManager.instance.DAT_DB0;
                vTransform.rotation.V22 = sVar3;
                vTransform.rotation.V00 = sVar3;
                vTransform.rotation.V02 = sVar2;
                vTransform.position.z += iVar12 >> 12;
                vTransform.position.y = iVar17;
                return 0;
            }

            uVar8 = maxHalfHealth;
            maxHalfHealth--;

            if (uVar8 == 0)
            {
                uVar5 = flags;
                tags = 0;
            }
            else
            {
                if (uVar8 != 16) goto LAB_3ECC;

                uVar5 = flags;
            }

            flags = uVar5 & 0xffffffdf;
            LAB_3ECC:
            iVar17 = (int)uVar8 - 16;

            if (iVar17 < 0)
                iVar17 = -iVar17;

            iVar12 = (16 - iVar17) * 227;

            if (iVar12 < 0)
                iVar12 += 15;

            vr.x = iVar12 >> 4;
            ApplyRotationMatrix();
            iVar12 = vTransform.rotation.V02 * 3814;

            if (iVar12 < 0)
                iVar12 += 4095;

            iVar13 = vTransform.rotation.V22 * 3814;
            vTransform.position.x += iVar12 >> 12;

            if (iVar13 < 0)
                iVar13 += 4095;

            vTransform.position.z += iVar13 >> 12;
            vTransform.position.y = GameManager.instance.DAT_DB0 + (16 - iVar17) * -1920;
        }

        return 0;
    }
}
