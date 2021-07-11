using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public delegate void DELEGATE_79A0(MemoryStream param1, MemoryStream param2, MemoryStream param3, MemoryStream param4);

public class JUNC_DB : MonoBehaviour
{
    public Vector3Int DAT_00; //0x00
    public XOBF_DB DAT_0C; //0x0C
    public byte DAT_10; //0x10
    public byte DAT_11; //0x11
    public short DAT_12; //0x12
    public short DAT_14; //0x14
    public short DAT_16; //0x16
    public VigMesh DAT_18; //0x18
    public RSEG_DB[] DAT_1C; //0x1C

    //FUN_79A0 (LOAD.DLL)
    public void LoadDB(string assetPath)
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

        LevelManager levelManager = GameObject.FindObjectOfType<LevelManager>();

        using (BinaryReader reader = new BinaryReader(File.Open(assetPath, FileMode.Open)))
        {
            long length = reader.BaseStream.Length;
            VigTerrain terrain = GameObject.FindObjectOfType<VigTerrain>();
            iVar3 = reader.ReadInt32BE();
            iVar4 = reader.ReadInt32BE();
            bVar1 = reader.ReadByte();
            iVar5 = reader.ReadByte();
            DAT_00.x = iVar3;
            DAT_00.z = iVar4;
            DAT_10 = bVar1;
            DAT_11 = (byte)iVar5;

            if ((bVar1 & 0x40) == 0)
                DAT_12 = 0;
            else
            {
                sVar2 = reader.ReadInt16BE();
                DAT_12 = sVar2;
                length -= 2;
            }

            if ((bVar1 & 2) == 0)
            {
                iVar7 = terrain.FUN_1B750((uint)iVar3, (uint)iVar4);
                DAT_00.y = iVar7;
            }
            else
            {
                iVar7 = reader.ReadInt32BE();
                DAT_00.y = iVar7 - 0x100000;
                length -= 4;
            }

            iVar7 = 0;

            if (0 < iVar5)
            {
                DAT_1C = new RSEG_DB[iVar5];

                do
                {
                    DAT_1C[iVar7] = null;
                    iVar7++;
                } while (iVar7 < iVar5);
            }

            if (length < 11)
                DAT_18 = null;
            else
            {
                iVar5 = reader.ReadInt16BE();
                DAT_0C = levelManager.xobfList[iVar5];
                sVar2 = reader.ReadInt16BE();
                DAT_14 = sVar2;
                sVar2 = reader.ReadInt16BE();
                DAT_16 = sVar2;
                pbVar8 = DAT_0C.FUN_2CB74(gameObject, (ushort)DAT_14);
                DAT_18 = pbVar8;
                pbVar8.DAT_00 = (byte)(pbVar8.DAT_00 & 0xfe | 4);
                psVar13 = new MemoryStream(DAT_18.vertexStream);
                psVar9 = new MemoryStream(new byte[DAT_18.vertices << 3]);
                bVar1 = DAT_18.DAT_01;
                DAT_18.DAT_02 = 16;
                DAT_18.vertexStream = psVar9.GetBuffer();
                uVar14 = 16 - (uint)bVar1;

                if (0 < DAT_18.vertices)
                {
                    using (BinaryReader reader2 = new BinaryReader(psVar13, Encoding.Default, true))
                    {
                        using (BinaryWriter writer = new BinaryWriter(psVar9, Encoding.Default, true))
                        {
                            for (int i = 0; i < DAT_18.vertices; i++)
                            {
                                iVar7 = ((ushort)DAT_16 & 0xfff) * 2;
                                sVar15 = reader2.ReadInt16();
                                reader2.BaseStream.Seek(2, SeekOrigin.Current);
                                sVar16 = reader2.ReadInt16();
                                iVar7 = GameManager.DAT_65C90[iVar7 + 1] * sVar15 +
                                        GameManager.DAT_65C90[iVar7] * sVar16;

                                if (iVar7 < 0)
                                    iVar7 += 4095;

                                sVar17 = (short)(iVar7 >> 12);
                                writer.Write(sVar17);
                                iVar7 = ((ushort)DAT_16 & 0xfff) * 2;
                                iVar7 = -GameManager.DAT_65C90[iVar7] * sVar15 +
                                        GameManager.DAT_65C90[iVar7 + 1] * sVar16;

                                if (iVar7 < 0)
                                    iVar7 += 4095;

                                sVar12 = (short)(iVar7 >> 12);
                                iVar7 = terrain.FUN_1B750((uint)(DAT_00.x + (sVar17 << (int)(uVar14 & 31))),
                                                          (uint)(DAT_00.z + (sVar12 << (int)(uVar14 & 31))));
                                reader2.BaseStream.Seek(2, SeekOrigin.Current);
                                writer.Write((short)(iVar7 - DAT_00.y >> (int)(uVar14 & 31)));
                                writer.Write(sVar12);
                            }
                        }
                    }
                }

                auStack72 = GameManager.FUN_2A39C();
                Utilities.SetRotMatrix(auStack72.rotation);
                Coprocessor.translationVector._trx = iVar3;
                Coprocessor.translationVector._try = DAT_00.y;
                Coprocessor.translationVector._trz = iVar4;
                dVar18 = new DELEGATE_79A0(FUN_78CC);
                DAT_18.FUN_39A8(dVar18);
            }
        }

        levelManager.juncList.Add(this);
        EditorUtility.SetDirty(gameObject);
        EditorUtility.SetDirty(levelManager.gameObject);
        PrefabUtility.RecordPrefabInstancePropertyModifications(gameObject);
    }

    //FUN_78CC (LOAD.DLL)
    private static void FUN_78CC(MemoryStream param1, MemoryStream param2, MemoryStream param3, MemoryStream param4)
    {
        int iVar1;
        Vector3Int local_8;
        long param1_pos = param1.Position;
        long param2_pos = param2.Position;
        long param3_pos = param3.Position;
        long param4_pos = param4.Position;
        VigTerrain terrain = GameObject.FindObjectOfType<VigTerrain>();

        using (BinaryReader reader = new BinaryReader(param3, Encoding.Default, true))
            local_8 = Utilities.FUN_23F58(new Vector3Int(reader.ReadInt16(), reader.ReadInt16(), reader.ReadInt16()));

        if (local_8.x < 0)
            local_8.x += 0xffff;

        if (local_8.z < 0)
            local_8.z += 0xffff;

        iVar1 = terrain.vertices[((local_8.z >> 16 & 0x3fU) * 2 + (local_8.x >> 16 & 0x3fU) * 128) / 2 + 4096 *
                        terrain.chunks[(((uint)(local_8.z >> 16) >> 6) * 4 +
                                        ((uint)(local_8.x >> 16) >> 6) * 128) / 4]] >> 11;

        using (BinaryWriter writer = new BinaryWriter(param1, Encoding.Default, true))
        {
            writer.Write(terrain.DAT_B9370[iVar1].r);
            writer.Write(terrain.DAT_B9370[iVar1].g);
            writer.Write(terrain.DAT_B9370[iVar1].b);
        }

        param1.Seek(param1_pos, SeekOrigin.Begin);
        param2.Seek(param2_pos, SeekOrigin.Begin);
        param3.Seek(param3_pos, SeekOrigin.Begin);
        param4.Seek(param4_pos, SeekOrigin.Begin);
    }
}
