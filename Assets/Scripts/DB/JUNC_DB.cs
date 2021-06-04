using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void DELEGATE_79A0();

public class JUNC_DB
{
    //FUN_79A0 (LOAD.DLL)
    public static void LoadDB(string assetPath)
    {
        byte bVar1;
        short sVar2;
        int iVar3;
        int iVar4;
        int iVar5;
        int iVar7;
        VigMesh pbVar8;
        MemoryStream psVar9;
        short sVar12;
        MemoryStream psVar13;
        uint uVar14;
        short sVar15;
        short sVar16;
        short sVar17;
        DELEGATE_79A0 dVar18;
        VigTransform auStack72;

        LevelManager levelManager = GameObject.Find("GameControl").GetComponent<LevelManager>();

        using (BinaryReader reader = new BinaryReader(File.Open(assetPath, FileMode.Open)))
        {
            long length = reader.BaseStream.Length;
            VigTerrain terrain = GameObject.Find("Terrain").GetComponent<VigTerrain>();
            iVar3 = reader.ReadInt32();
            iVar4 = reader.ReadInt32();
            bVar1 = reader.ReadByte();
            iVar5 = reader.ReadByte();
            Junction newJunc = new Junction();
            newJunc.DAT_00 = iVar3;
            newJunc.DAT_08 = iVar4;
            newJunc.DAT_10 = bVar1;
            newJunc.DAT_11 = (byte)iVar5;

            if ((bVar1 & 0x40) == 0)
                newJunc.DAT_12 = 0;
            else
            {
                sVar2 = reader.ReadInt16();
                newJunc.DAT_12 = sVar2;
                length -= 2;
            }

            if ((bVar1 & 2) == 0)
            {
                iVar7 = terrain.FUN_1B750((uint)iVar3, (uint)iVar4);
                newJunc.DAT_04 = iVar7;
            }
            else
            {
                iVar7 = reader.ReadInt32();
                newJunc.DAT_04 = iVar7 - 0x100000;
                length -= 4;
            }

            iVar7 = 0;

            if (0 < iVar5)
            {
                newJunc.DAT_1C = new byte[iVar5];

                do
                {
                    newJunc.DAT_1C[iVar7] = 0;
                    iVar7++;
                } while (iVar7 < iVar5);
            }

            if (length < 11)
                newJunc.DAT_18 = null;
            else
            {
                iVar5 = reader.ReadInt16();
                newJunc.DAT_0C = levelManager.xobfList[iVar5];
                sVar2 = reader.ReadInt16();
                newJunc.DAT_14 = sVar2;
                sVar2 = reader.ReadInt16();
                newJunc.DAT_16 = sVar2;
                pbVar8 = newJunc.DAT_0C.FUN_2CB74((ushort)newJunc.DAT_14);
                newJunc.DAT_18 = pbVar8;
                pbVar8.DAT_00 = (byte)(pbVar8.DAT_00 & 0xfe | 4);
                psVar13 = new MemoryStream(newJunc.DAT_18.vertexStream);
                psVar9 = new MemoryStream(new byte[newJunc.DAT_18.vertices << 3]);
                bVar1 = newJunc.DAT_18.DAT_01;
                newJunc.DAT_18.DAT_02 = 16;
                newJunc.DAT_18.vertexStream = psVar9.GetBuffer();
                uVar14 = 16 - (uint)bVar1;

                if (0 < newJunc.DAT_18.vertices)
                {
                    using (BinaryReader reader2 = new BinaryReader(psVar13, Encoding.Default, true))
                    {
                        using (BinaryWriter writer = new BinaryWriter(psVar9, Encoding.Default, true))
                        {
                            for (int i = 0; i < newJunc.DAT_18.vertices; i++)
                            {
                                iVar7 = ((ushort)newJunc.DAT_16 & 0xfff) * 2;
                                sVar15 = reader2.ReadInt16();
                                reader2.BaseStream.Seek(2, SeekOrigin.Current);
                                sVar16 = reader2.ReadInt16();
                                iVar7 = GameManager.DAT_65C90[iVar7 + 1] * sVar15 +
                                        GameManager.DAT_65C90[iVar7] * sVar16;

                                if (iVar7 < 0)
                                    iVar7 += 4095;

                                sVar17 = (short)(iVar7 >> 12);
                                writer.Write(sVar17);
                                iVar7 = ((ushort)newJunc.DAT_16 & 0xfff) * 2;
                                iVar7 = -GameManager.DAT_65C90[iVar7] * sVar15 +
                                        GameManager.DAT_65C90[iVar7 + 1] * sVar16;

                                if (iVar7 < 0)
                                    iVar7 += 4095;

                                sVar12 = (short)(iVar7 >> 12);
                                iVar7 = terrain.FUN_1B750((uint)(newJunc.DAT_00 + (sVar17 << (int)(uVar14 & 31))),
                                                          (uint)(newJunc.DAT_08 + (sVar12 << (int)(uVar14 & 31))));
                                reader2.BaseStream.Seek(2, SeekOrigin.Current);
                                writer.Write((short)(iVar7 - newJunc.DAT_04 >> (int)(uVar14 & 31)));
                                writer.Write(sVar12);
                            }
                        }
                    }
                }

                auStack72 = GameManager.FUN_2A39C();
                Utilities.SetRotMatrix(auStack72.rotation);
                Coprocessor.translationVector._trx = iVar3;
                Coprocessor.translationVector._try = newJunc.DAT_04;
                Coprocessor.translationVector._trz = iVar4;
                dVar18 = new DELEGATE_79A0(FUN_78CC);
                newJunc.DAT_18.FUN_39A8(dVar18);
            }
        }
    }

    //FUN_78CC (LOAD.DLL)
    private static void FUN_78CC()
    {

    }
}
