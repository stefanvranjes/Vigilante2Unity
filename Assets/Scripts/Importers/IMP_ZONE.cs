using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMP_ZONE
{
    public static void LoadAsset(string assetPath)
    {
        VigTerrain terr = GameObject.FindObjectOfType<VigTerrain>();
        string name = Path.GetFileNameWithoutExtension(assetPath);
        int zone = (name[4] - '0') + (name[5] - '0') + (name[6] - '0') + (name[7] - '0');
        zone++;

        /*using (BinaryReader reader = new BinaryReader(File.Open(assetPath, FileMode.Open)))
        {
            int iVar1 = 0; //r10

            do
            {
                int iVar2 = 0; //r8
                int iVar3 = iVar1 << 6; //r12
                int verticesIndex = iVar1 << 7; //r9
                long chunkEnd = reader.BaseStream.Position + 3; //r7

                do
                {
                    uint uVar2 = reader.ReadUInt16(); //r3
                    long chunkNext = reader.BaseStream.Position + 2;
                    int tilesIndex = iVar2 + iVar3; //r6
                    iVar2++;
                    reader.BaseStream.Seek(chunkEnd - 1, SeekOrigin.Begin);
                    uint uVar1 = (uint)reader.ReadByte() >> 3; //r4
                    uint uVar3 = uVar2 >> 8; //r2
                    uVar2 = (uVar2 & 0xFF) << 8;
                    uVar3 |= uVar2;
                    uVar3 -= 512;
                    uVar1 = uVar1 << 11;
                    uVar3 |= uVar1;
                    terr.vertices[zone * 4096 + verticesIndex / 2] = (ushort)uVar3;
                    reader.BaseStream.Seek(chunkEnd, SeekOrigin.Begin);
                    uVar2 = reader.ReadByte();
                    chunkEnd += 4;
                    terr.tiles[zone * 4096 + tilesIndex] = (byte)uVar2;
                    verticesIndex += 2;
                } while (iVar2 < 64);

                iVar1++;
            } while (iVar1 < 64);
        }*/

        using (BinaryReader reader = new BinaryReader(File.Open(assetPath, FileMode.Open)))
        {
            byte bVar1;
            ushort uVar2;
            long puVar5;
            int iVar4;
            int iVar6;
            int iVar7;
            int iVar8;

            iVar8 = 0;

            do
            {
                iVar6 = 0;
                iVar7 = iVar8 << 6;
                puVar5 = reader.BaseStream.Position + 3;

                do
                {
                    uVar2 = reader.ReadUInt16(0);
                    iVar4 = iVar6 + iVar8 * 64;
                    iVar6++;
                    terr.vertices[zone * 4096 + iVar7] =
                        (ushort)((uVar2 >> 8 | (ushort)((uVar2 & 0xff) << 8)) - 0x200 |
                        (ushort)((uint)reader.ReadByte(2) >> 3) << 11);
                    bVar1 = reader.ReadByte(3);
                    puVar5 += 4;
                    reader.BaseStream.Seek(4, SeekOrigin.Current);
                    terr.tiles[zone * 4096 + iVar4] = bVar1;
                    iVar7++;
                } while (iVar6 < 64);

                iVar8++;
            } while (iVar8 < 64);
        }
    }
}
