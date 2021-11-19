using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMP_TIN
{
    /*public static List<TileData> LoadAsset(string assetPath1)
    {
        List<TileData> output = new List<TileData>();
        LevelManager levelManager = GameObject.FindObjectOfType<LevelManager>();
        VigTerrain terrain = GameObject.FindObjectOfType<VigTerrain>();

        using (BinaryReader reader = new BinaryReader(File.Open(assetPath1, FileMode.Open)))
        {
            int iVar1 = 0; //r6
            byte[] aVar1 = BYTES;

            do
            {
                int iVar = 0; //r5
                int arrayIndex = iVar1 << 3; //r4

                do
                {
                    int iVar3; //r2

                    if (aVar1[arrayIndex] == 0)
                        iVar3 = 0;
                    else
                        iVar3 = levelManager.DAT_DBA - 1;

                    aVar1[arrayIndex] = (byte)iVar3;
                    iVar3 = aVar1[arrayIndex + 1];

                    if (iVar3 == 0)
                        iVar3 = 0;
                    else
                        iVar3 = levelManager.DAT_DBA - 1;

                    aVar1[arrayIndex + 1] = (byte)iVar3;
                    arrayIndex += 2;
                }
                while (++iVar < 4);
            } while (++iVar1 < 8);

            long chunkEnd = reader.BaseStream.Position + 34;
            long chunkStart = reader.BaseStream.Position;
            int iVar2 = 0; //r21

            do
            {
                reader.BaseStream.Seek(chunkEnd - 32, SeekOrigin.Begin);
                uint uVar1 = reader.ReadByte(); //r6
                int iVar4 = (int)(uVar1 & 15) * levelManager.DAT_DBA; //r9
                uVar1 = (uint)((int)(uVar1 >> 4) * levelManager.DAT_DBA);
                reader.BaseStream.Seek(chunkEnd - 31, SeekOrigin.Begin);
                int iVar5 = reader.ReadByte(); //r20
                int arrayIndex = (iVar5 & 7) << 3; //r5
                int uv_y = aVar1[arrayIndex + 1]; //r2
                int uv = aVar1[arrayIndex]; //r3
                int iVar6 = iVar4 & 127; //r8
                uv += iVar6;
                uv_y = uv_y + (int)uVar1 << 8;
                uv |= uv_y;
                reader.BaseStream.Seek(chunkStart, SeekOrigin.Begin);
                uint uVar2 = reader.ReadUInt16(); //r7

                TileData newTile = new TileData();
                newTile.uv1_x = uv & 0xFF;
                newTile.uv1_y = uv >> 8;
                uv = (aVar1[arrayIndex + 2] + iVar6) | (aVar1[arrayIndex + 3] + (int)uVar1 << 8);
                newTile.uv2_x = uv & 0xFF;
                newTile.uv2_y = uv >> 8;
                uv = (aVar1[arrayIndex + 4] + iVar6) | (aVar1[arrayIndex + 5] + (int)uVar1 << 8);
                newTile.uv3_x = uv & 0xFF;
                newTile.uv3_y = uv >> 8;
                uv = (aVar1[arrayIndex + 6] + iVar6) | (aVar1[arrayIndex + 7] + (int)uVar1 << 8);
                newTile.uv4_x = uv & 0xFF;
                newTile.uv4_y = uv >> 8;

                uint uVar3 = uVar2 >> 8;
                uVar2 = (uVar2 & 0xFF) << 8;
                uint uVar4 = uVar3 | uVar2; //r17
                uint uVar7 = 0;

                if ((uVar4 & 16) == 0)
                {
                    uVar7 = (uint)iVar4 >> 7;
                    int iVar = (terrain.bitmapID & 3) + 5;
                    uVar7 = uVar7 << iVar;
                    uVar7 = (uVar7 / 64) * 128; // need to test if the scale is right
                    newTile.uv4_x += (int)uVar7;
                }

                uint uVar5 = uVar4 >> 4 & 1;
                uint uVar6 = ((uint)iVar5 & 18) >> 2;
                uVar5 |= uVar6;
                uVar2 = 0;
                long bufferPos = chunkStart;
                arrayIndex = iVar2;
                newTile.unk1 = (byte)uVar2;
                newTile.uv1_x += (int)uVar7;
                newTile.uv2_x += (int)uVar7;
                newTile.uv3_x += (int)uVar7;

                newTile.unk2 = new short[6];

                do
                {
                    reader.BaseStream.Seek(bufferPos + 4, SeekOrigin.Begin);
                    uVar5 = reader.ReadUInt16();
                    bufferPos += 2;
                    arrayIndex += 2;
                    uVar2++;
                    uVar6 = uVar5 >> 8;
                    uVar5 &= 0xFF;
                    uVar6 |= uVar5;
                    newTile.unk2[arrayIndex / 2 - 1] = (short)uVar6;
                } while (uVar2 < 6);

                reader.BaseStream.Seek(chunkEnd - 2, SeekOrigin.Begin);
                newTile.unk3 = reader.ReadByte();
                reader.BaseStream.Seek(chunkEnd - 1, SeekOrigin.Begin);
                newTile.unk4 = reader.ReadByte();
                reader.BaseStream.Seek(chunkEnd, SeekOrigin.Begin);
                newTile.unk5 = reader.ReadByte();
                iVar1 += 32;
                chunkStart += 36;
                chunkEnd += 36;
                output.Add(newTile);
            } while (output.Count < 256);
        }

        return output;
    }*/

    public static void LoadAsset(string assetPath)
    {
        using (BinaryReader reader = new BinaryReader(File.Open(assetPath, FileMode.Open)))
        {
            int puVar1;
            byte bVar2;
            ushort uVar3;
            ushort uVar4;
            sbyte sVar5;
            short sVar6;
            uint uVar6;
            int iVar7;
            int iVar8;
            int iVar9;
            int puVar10;
            int puVar11;
            TileData psVar12;
            uint uVar13;
            LevelManager levelManager = GameObject.FindObjectOfType<LevelManager>();
            VigTerrain terrain = GameObject.FindObjectOfType<VigTerrain>();

            byte[] BYTES =
            {
                0x00, 0x1F, 0x1F, 0x1F, 0x00, 0x00, 0x1F, 0x00, 0x1F, 0x1F,
                0x00, 0x1F, 0x1F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x1F,
                0x1F, 0x00, 0x1F, 0x1F, 0x00, 0x1F, 0x00, 0x00, 0x1F, 0x1F,
                0x1F, 0x00, 0x1F, 0x00, 0x00, 0x00, 0x1F, 0x1F, 0x00, 0x1F,
                0x00, 0x00, 0x1F, 0x00, 0x00, 0x1F, 0x1F, 0x1F, 0x1F, 0x1F,
                0x1F, 0x00, 0x00, 0x1F, 0x00, 0x00, 0x1F, 0x00, 0x1F, 0x1F,
                0x00, 0x00, 0x00, 0x1F
            };

            iVar9 = 0;

            if (terrain.tileData == null)
                terrain.tileData = new List<TileData>();

            terrain.tileData.Clear();

            do
            {
                iVar8 = 0;
                iVar7 = iVar9 << 3;

                do
                {
                    if (BYTES[iVar7] == 0)
                        sVar5 = 0;
                    else
                        sVar5 = (sbyte)((byte)levelManager.DAT_DBA - 1);

                    BYTES[iVar7] = (byte)sVar5;

                    if (BYTES[iVar7 + 1] == 0)
                        sVar5 = 0;
                    else
                        sVar5 = (sbyte)((byte)levelManager.DAT_DBA - 1);

                    BYTES[iVar7 + 1] = (byte)sVar5;
                    iVar8++;
                    iVar7 += 2;
                } while (iVar8 < 4);

                iVar9++;
            } while (iVar9 < 8);

            puVar11 = (int)(reader.BaseStream.Position + 0x22);
            iVar9 = 0;

            do
            {
                uVar13 = (uint)(reader.ReadByte(puVar11 - 0x20) & 15) * (ushort)levelManager.DAT_DBA;
                sVar6 = (short)(((uint)reader.ReadByte(puVar11 - 0x20) >> 4) * (ushort)levelManager.DAT_DBA);
                bVar2 = reader.ReadByte(puVar11 - 0x1f);
                iVar7 = (bVar2 & 7) * 8;
                uVar4 = (ushort)(uVar13 & 0x7f);
                uVar3 = reader.ReadUInt16(puVar11 - 0x22);
                psVar12 = new TileData();
                terrain.tileData.Add(psVar12);
                psVar12.uv1_x = BYTES[iVar7] + uVar4;
                psVar12.uv1_y = BYTES[iVar7 + 1] + sVar6;
                psVar12.uv2_x = BYTES[iVar7 + 2] + uVar4;
                psVar12.uv2_y = BYTES[iVar7 + 3] + sVar6;
                psVar12.uv3_x = BYTES[iVar7 + 4] + uVar4;
                psVar12.uv3_y = BYTES[iVar7 + 5] + sVar6;
                psVar12.uv4_x = BYTES[iVar7 + 6] + uVar4;
                psVar12.uv4_y = BYTES[iVar7 + 7] + sVar6;

                if ((uVar3 >> 8 & 16) == 0)
                {
                    uVar6 = (uVar13 >> 7) << (terrain.bitmapID & 3) + 5;
                    uVar6 = (uVar6 / 64) * 128;
                    psVar12.uv4_x += (int)uVar6;
                }
                else
                    uVar6 = 0;

                iVar8 = 0;
                psVar12.flags = (byte)((byte)((uint)uVar3 >> 12) & 1 | (byte)((uint)(bVar2 & 0x18) >> 2));
                psVar12.uv3_x += (int)uVar6;
                psVar12.uv2_x += (int)uVar6;
                psVar12.uv1_x += (int)uVar6;
                puVar10 = puVar11 - 0x22;
                psVar12.DAT_10 = new short[6];

                do
                {
                    puVar1 = puVar10 + 4;
                    puVar10 += 2;
                    psVar12.DAT_10[iVar8] = (short)((uint)reader.ReadUInt16(puVar1) >> 8 | (uint)((reader.ReadUInt16(puVar1) & 0xff) << 8));
                    iVar8++;
                } while (iVar8 < 6);

                iVar9 += 0x20;
                psVar12.DAT_1C = reader.ReadByte(puVar11 - 2);
                psVar12.DAT_1D = reader.ReadByte(puVar11 - 1);
                psVar12.DAT_1E = reader.ReadByte(puVar11);
                puVar11 += 0x24;
            } while (iVar9 < 0x2000);
        }
    }
}
