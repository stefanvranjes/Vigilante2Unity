using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Tile
{
    public int uv1_x, uv1_y;
    public int uv2_x, uv2_y;
    public int uv3_x, uv3_y;
    public int uv4_x, uv4_y;

    public short[] unk2;

    public byte unk3; // 0x1C
    public byte unk4; // 0x1D
    public byte unk5; // 0x1E
    public byte unk1; // 0x1F
}

public class VigTerrain : MonoBehaviour
{
    public static VigTerrain instance;
    
    public int bitmapID;
    public List<Tile> tileData;
    public ushort[] vertices;
    public byte[] tiles;
    public ushort defaultVertex;
    public byte defaultTile;
    public ushort[] chunks;
    public int unk1, unk2, unk3, unk4;
    public int tileXZ;
    public int tileY;

    public int zoneCount;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetNumberOfZones()
    {
        vertices = new ushort[zoneCount * 4096];
        tiles = new byte[zoneCount * 4096];

        for (int i = 0; i < 4096; i++)
        {
            vertices[i] = defaultVertex;
            tiles[i] = defaultTile;
        }
    }

    public Vector3Int FUN_1B998(uint x, uint z)
    {
        uint uVar1 = x >> 16; //r10
        uint uVar2 = z >> 16; //r11

        if ((x & 0xffff) + (z & 0xffff) < 0x10000)
        {
            uint uVar4 = z >> 22 << 2;
            x = x >> 22 << 7;
            uint uVar6 = (uVar2 & 0x3F) << 1; //r5
            uint uVar5 = (uVar1 & 0x3F) << 7; //r8
            int chunk1ID = chunks[(uVar4 + x) / 4]; //r6
            int vertex1ID = (int)(uVar6 + uVar5) / 2; //r6
            uint uVar = uVar1 + 1; //r3
            uVar4 += uVar >> 6 << 7;
            int chunk2ID = chunks[uVar4 / 4]; //r2
            uVar = (uVar & 0x3F) << 7;
            uVar6 += uVar;
            int vertex2ID = (int)uVar6 / 2;
            int iVar1 = vertices[chunk1ID * 4096 + vertex1ID] & 0x7FF; //r4
            int iVar2 = vertices[chunk2ID * 4096 + vertex2ID] & 0x7FF; //r2
            uVar = uVar2 + 1;
            int chunk3ID = chunks[((uVar >> 6 << 2) + x) / 4];
            uVar = (uVar & 0x3F) << 1;
            int vertex3ID = (int)(uVar + uVar5) / 2;
            int iVar3 = vertices[chunk3ID * 4096 + vertex3ID] & 0x7FF; //r2
            return new Vector3Int(iVar2 - iVar1, -32, iVar3 - iVar1);
        }
        else
        {
            uint uVar3 = uVar1 + 1; //r6
            uint uVar4 = uVar2 + 1; //r4
            uint uVar = uVar4 >> 6 << 2; //r3
            uint uVar5 = uVar3 >> 6 << 7; //r8
            uVar4 = (uVar4 & 0x3F) << 1;
            uVar3 = (uVar3 & 0x3F) << 7;
            int chunk1ID = chunks[(uVar + uVar5) / 4]; //r5
            int vertex1ID = (int)(uVar4 + uVar3) / 2; //r5
            uVar += x >> 22 << 7;
            int chunk2ID = chunks[uVar / 4];
            uVar4 += (uVar1 & 0x3F) << 7;
            int vertex2ID = (int)uVar4 / 2;
            int iVar1 = vertices[chunk1ID * 4096 + vertex1ID] & 0x7FF; //r4
            int iVar2 = vertices[chunk2ID * 4096 + vertex2ID] & 0x7FF; //r2
            uVar = (uVar2 & 0x3F) << 1;
            int chunk3ID = chunks[((z >> 22 << 2) + uVar5) / 4]; //r2
            int vertex3ID = (int)(uVar + uVar3) / 2;
            int iVar3 = vertices[chunk3ID * 4096 + vertex3ID] & 0x7FF;
            return new Vector3Int(iVar1 - iVar2, -32, iVar1 - iVar3);
        }
    }

    // 8001B944
    public Tile GetTileByPosition(uint x, uint z)
    {
        uint uVar1 = z >> 16;
        z = z >> 22 << 2;
        z += x >> 22 << 7;
        uVar1 = uVar1 & 0x3F;
        x = x >> 10 & 0xFC0;
        int chunkID = chunks[z / 4];
        uVar1 += x;
        return tileData[tiles[chunkID * 4096 + uVar1]];
    }

    public int FUN_1B750(uint x, uint z)
    {
        int iVar1 = (int)x & 0xffff; //r11
        int iVar2 = iVar1; //r15
        uint uVar3 = z & 0xffff; //r12
        int iVar4 = (int)uVar3; //r24
        uint uVar5 = x >> 16; //r10
        uint uVar6 = z >> 16; //r13

        if (iVar1 + uVar3 < 0x10000)
        {
            uint uVar7 = z >> 22 << 2; //r4
            x = x >> 22 << 7;
            uint uVar8 = (uVar6 & 0x3f) << 1; //r5
            uint uVar9 = (uVar5 & 0x3f) << 7; //r7
            int chunkID = chunks[(uVar7 + x) / 4];
            int vertexID = (int)(uVar8 + uVar9);
            uVar7 += (uVar5 + 1) >> 6 << 7;
            uVar8 += (uVar5 + 1 & 0x3f) << 7;
            uVar7 = (uint)vertices[chunks[uVar7 / 4] * 4096 + uVar8 / 2] & 0x7ff;
            uVar8 = (uint)vertices[chunkID * 4096 + vertexID / 2] & 0x7ff;
            int iVar3 = (int)uVar7 - (int)uVar8; //r4
            iVar3 = iVar1 * iVar3;
            chunkID = chunks[(int)((uVar6 + 1 >> 6 << 2) + x) / 4];
            vertexID = (int)(((uVar6 + 1 & 0x3F) << 1) + uVar9);
            int vertexH = (vertices[chunkID * 4096 + vertexID / 2] & 0x7FF) - (int)uVar8;
            vertexH = iVar3 + (int)uVar3 * vertexH;
            vertexH += (int)uVar8 << 16;

            if (vertexH < 0)
                vertexH += 31;

            return vertexH >> 5;
        }
        else
        {
            uint uVar9 = uVar5 + 1;
            uint uVar7 = uVar6 + 1;
            uint uVar = uVar7 >> 6 << 2; //r3
            uint uVar10 = uVar9 >> 6 << 7; //r6
            uVar7 = (uVar7 & 0x3F) << 1;
            uVar9 = (uVar9 & 0x3F) << 7;
            int chunk1ID = chunks[(uVar + uVar10) / 4];
            int vertex1ID = (int)uVar7 + (int)uVar9;
            int chunk2ID = chunks[(uVar + (x >> 22 << 7)) / 4];
            uVar7 += (uVar5 & 0x3F) << 7;
            int vertex2ID = (int)uVar7 / 2;
            int iVar7 = vertices[chunk2ID * 4096 + vertex2ID] & 0x7FF; //r2
            uVar = (z >> 22 << 2) + uVar10;
            int iVar9 = 0x10000;
            int iVar6 = vertices[chunk1ID * 4096 + vertex1ID / 2] & 0x7FF; //r4
            int iVar8 = iVar9 - iVar2; //r5
            iVar7 -= iVar6;
            iVar8 = iVar8 * iVar7;
            int chunk3ID = chunks[uVar / 4];
            iVar9 -= iVar4; //r6
            int vertex3ID = (int)(((uVar6 & 0x3F) << 1) + uVar9);
            int vertexH = vertices[chunk3ID * 4096 + vertex3ID / 2] & 0x7FF; //r2
            vertexH -= iVar6;
            iVar6 = iVar6 << 16;
            vertexH = iVar8 + iVar9 * vertexH;
            vertexH = iVar6 + vertexH;

            if (vertexH < 0)
                vertexH += 31;

            return vertexH >> 5;
        }
    }
}
