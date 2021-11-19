using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quake : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public int DAT_80_2; //0x80
    public ushort[] DAT_84_2 = new ushort[576]; //0x84
    public short[] DAT_504 = new short[576]; //0x504
    public byte[] DAT_984 = new byte[576]; //0x984

    //FUN_90 (HALFTRAK.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        short sVar1;
        byte bVar2;
        int iVar3;
        int iVar4;
        ushort uVar6;
        uint uVar7;
        uint uVar8;
        uint uVar11;
        int iVar12;
        int iVar13;
        int iVar14;
        int iVar15;
        int iVar16;
        int iVar17;
        int iVar18;
        int iVar22;

        if (arg1 == 0)
        {
            if (arg2 != 0)
            {
                iVar17 = DAT_80_2;

                if (-1 < iVar17)
                {
                    iVar18 = screen.x;

                    if (iVar18 < 0)
                        iVar18 += 0xffff;

                    iVar16 = screen.z;

                    if (iVar16 < 0)
                        iVar16 += 0xffff;

                    iVar22 = 0;
                    iVar15 = 0;
                    iVar14 = 0;

                    do
                    {
                        iVar12 = 0;
                        uVar7 = (uint)((iVar18 >> 16) - 12 + iVar22);
                        iVar13 = iVar15;

                        do
                        {
                            uVar11 = DAT_984[iVar12 + iVar14];
                            iVar3 = 128 - (int)uVar11;
                            iVar3 = iVar3 * iVar3 * 3;

                            if (iVar3 < 0)
                                iVar3 += 3;

                            iVar4 = (int)uVar11 - (iVar17 / 2 + 48);
                            uVar8 = (uint)(iVar4 * 128);
                            uVar11 = uVar8;

                            if ((int)uVar8 < 0)
                                uVar11 = (uint)(iVar4 * -128);

                            if ((int)uVar11 < 0x1000)
                            {
                                iVar4 = (int)(uVar8 & 0xfff) * 4;
                                iVar3 = GameManager.DAT_65C90[iVar4 / 2] * (iVar3 >> 2);

                                if (iVar3 < 0)
                                    iVar3 += 0xfffff;

                                iVar4 = GameManager.DAT_65C90[iVar4 / 2 + 1];

                                if (iVar4 < 0)
                                    iVar4 += 0x1ff;

                                uVar6 = (ushort)((iVar3 >> 20) | (iVar4 >> 9) * -0x800);
                            }
                            else
                                uVar6 = 0;

                            DAT_84_2[iVar13 / 2] = uVar6;
                            iVar3 = (short)uVar6 - DAT_504[iVar13 / 2];
                            uVar11 = (uint)((iVar16 >> 16) - 12 + iVar12);

                            if (iVar3 != 0)
                            {
                                VigTerrain.instance.vertices[VigTerrain.instance.chunks[((uVar11 >> 6) * 4 + (uVar7 >> 6) * 128) / 4] * 4096 +
                                                            ((uVar7 & 63) * 128 + (uVar11 & 63) * 2) / 2] += (ushort)iVar3;
                            }

                            iVar12++;
                            iVar13 += 2;
                        } while (iVar12 < 24);

                        iVar15 += 48;
                        iVar22++;
                        iVar14 += 24;
                    } while (iVar22 < 24);

                    for (int i = 0; i < 576; i++)
                        DAT_504[i] = (short)DAT_84_2[i];
                }
            }

            iVar17 = DAT_80_2 + 1;
            DAT_80_2 = iVar17;

            if (iVar17 == 0x101)
            {
                iVar17 = screen.x;

                if (iVar17 < 0)
                    iVar17 += 0xffff;

                iVar18 = screen.z;

                if (iVar18 < 0)
                    iVar18 += 0xffff;

                iVar16 = 0;
                iVar22 = 0;

                do
                {
                    iVar15 = 0;
                    uVar7 = (uint)((iVar17 >> 16) - 12 + iVar16);
                    iVar14 = iVar22;

                    do
                    {
                        sVar1 = DAT_504[iVar14 / 2];
                        uVar11 = (uint)((iVar18 >> 16) - 12 + iVar15);

                        if (sVar1 != 0)
                        {
                            VigTerrain.instance.vertices[VigTerrain.instance.chunks[((uVar11 >> 6) * 4 + (uVar7 >> 6) * 128) / 4] * 4096 +
                                                        ((uVar7 & 63) * 128 + (uVar11 & 63) * 2) / 2] -= (ushort)sVar1;
                        }

                        iVar15++;
                        iVar14 += 2;
                    } while (iVar15 < 24);

                    iVar16++;
                    iVar22 += 48;
                } while (iVar16 < 24);

                GameManager.instance.FUN_309A0(this);
                return 0xffffffff;
            }
        }
        else
        {
            if (arg1 != 1)
                return 0;

            iVar17 = -12;
            iVar18 = 12;

            do
            {
                iVar16 = -12;
                iVar22 = 144;

                do
                {
                    bVar2 = (byte)Utilities.SquareRoot((iVar17 * iVar17 + iVar22) * 100);
                    DAT_984[iVar16 + iVar18] = bVar2;
                    iVar16++;
                    iVar22 = iVar16 * iVar16;
                } while (iVar16 < 12);

                iVar17++;
                iVar18 += 24;
            } while (iVar17 < 12);
        }

        return 0;
    }
}
