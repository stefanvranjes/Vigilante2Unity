using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TileData
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

public class Tile
{
    public Tile[] neighbours = new Tile[4];
    //0 - Up
    //1 - Down
    //2 - Left
    //3 - Right

    public int[] indices = new int[4];
    public float x, y;
}

public struct TerrainScreen
{
    public int ir0; //0x00
    public Vector3Int vert; //0x04
    public Color32 color; //0x10
}

public struct ScreenPoly
{
    public int index;
    public Vector2Int v1, v2, v3;
    public Color32 clr1, clr2, clr3;
}

public class VigTerrain : MonoBehaviour
{
    public static VigTerrain instance;
    
    public int bitmapID;
    public List<TileData> tileData;
    public ushort[] vertices;
    public byte[] tiles;
    public ushort defaultVertex;
    public byte defaultTile;
    public ushort[] chunks;
    public int unk1, unk2, unk3, unk4;
    public int tileXZ;
    public int tileY;
    public int zoneCount;
    public float drawDistance;
    public Color32[] DAT_B9370 = new Color32[32];
    public Color32[] DAT_BA4F0 = new Color32[32];
    public VigTransform DAT_BDFF0;

    private Dictionary<int, List<int>> verticesDict;
    private Dictionary<int, Tile> tilesDict;
    private MeshRenderer[] meshes;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        meshes = GetComponentsInChildren<MeshRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        verticesDict = new Dictionary<int, List<int>>();
        tilesDict = new Dictionary<int, Tile>();

        for (int i = 0, k = 0; i < chunks.Length; i++)
        {
            if (chunks[i] == 0 || zoneCount <= chunks[i])
                continue;

            int zone = chunks[i];

            for (int x = 0; x < 64; x++)
            {
                for (int y = 0; y < 64; y++)
                {
                    int index1 = zone * 4096 + x * 64 + y;

                    int nextZone = x + 1 < 64 ? zone : chunks[i + 32];
                    int nextX = x + 1 < 64 ? x + 1 : 0;
                    int index2 = nextZone * 4096 + nextX * 64 + y;

                    nextZone = y + 1 < 64 ? zone : chunks[i + 1];
                    int nextY = y + 1 < 64 ? y + 1 : 0;
                    int index3 = nextZone * 4096 + x * 64 + nextY;

                    if (x + 1 >= 64 && y + 1 >= 64)
                    {
                        nextZone = chunks[i + 33];
                        nextX = 0;
                        nextY = 0;
                    }
                    else if (x + 1 >= 64)
                    {
                        nextZone = chunks[i + 32];
                        nextX = 0;
                        nextY = y + 1;
                    }
                    else if (y + 1 >= 64)
                    {
                        nextZone = chunks[i + 1];
                        nextX = x + 1;
                        nextY = 0;
                    }
                    else
                    {
                        nextZone = zone;
                        nextX = x + 1;
                        nextY = y + 1;
                    }
                    int index4 = nextZone * 4096 + nextX * 64 + nextY;

                    int tileIndex1 = k * 4096 + x * 64 + y;
                    int vertexIndex = (k * 4096 + x * 64 + y) * 4;

                    if (verticesDict.ContainsKey(index1))
                    {
                        verticesDict.Add(index1, new List<int>());
                        tilesDict.Add(index1, new Tile());
                    }
                    verticesDict[index1].Add(vertexIndex);

                    if (verticesDict.ContainsKey(index2))
                    {
                        verticesDict.Add(index2, new List<int>());
                        tilesDict.Add(index2, new Tile());
                    }
                    verticesDict[index2].Add(vertexIndex + 1);

                    if (verticesDict.ContainsKey(index3))
                    {
                        verticesDict.Add(index3, new List<int>());
                        tilesDict.Add(index3, new Tile());
                    }
                    verticesDict[index3].Add(vertexIndex + 2);

                    if (verticesDict.ContainsKey(index4))
                    {
                        verticesDict.Add(index4, new List<int>());
                        tilesDict.Add(index4, new Tile());
                    }
                    verticesDict[index4].Add(vertexIndex + 3);

                    tilesDict[index1].neighbours[0] = tilesDict[index3];
                    tilesDict[index1].neighbours[3] = tilesDict[index2];
                    tilesDict[index3].neighbours[1] = tilesDict[index1];
                    tilesDict[index2].neighbours[2] = tilesDict[index1];

                    tilesDict[index1].indices[0] = vertexIndex;
                    tilesDict[index1].indices[1] = vertexIndex + 1;
                    tilesDict[index1].indices[2] = vertexIndex + 2;
                    tilesDict[index1].indices[3] = vertexIndex + 3;

                    tilesDict[index1].x = x + i / 32 * 64;
                    tilesDict[index1].y = y + i % 32 * 64;
                }
            }

            k++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < GameManager.instance.players.Length; i++)
        {
            Vector3Int cameraPosition = GameManager.instance.players[i].vCamera.vObject.vTransform.position;
            Tile onTile = GetTileByPosition2((uint)cameraPosition.x, (uint)cameraPosition.y);
            List<Tile> closeTiles = BreadthFirstSearch(onTile, drawDistance);

        }
    }

    private bool InsideCircle(Tile center, Tile tile, float radius)
    {
        float dx = center.x - tile.x,
              dy = center.y - tile.y;
        float distance = dx * dx + dy * dy;
        return distance <= radius * radius;
    }

    private List<Tile> BreadthFirstSearch(Tile start, float radius)
    {
        var frontier = new Queue<Tile>();
        frontier.Enqueue(start);

        var reached = new List<Tile>();
        reached.Add(start);
        
        while (frontier.Count > 0)
        {
            var current = frontier.Dequeue();

            foreach(var next in current.neighbours)
            {
                if (!reached.Contains(next) && InsideCircle(start, next, radius))
                {
                    frontier.Enqueue(next);
                    reached.Add(next);
                }
            }
        }

        return reached;
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

    public void FUN_1BE68(int param1, int param2, int param3)
    {
        if (param1 < param2)
        {
            do
            {
                FUN_288E0((uint)param1, (uint)param3, GameManager.instance.DAT_610);
                param1 += 4;

            } while (param1 < param2);
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
    public TileData GetTileByPosition(uint x, uint z)
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

    public Tile GetTileByPosition2(uint x, uint z)
    {
        uint uVar1 = z >> 16;
        z = z >> 22 << 2;
        z += x >> 22 << 7;
        uVar1 = uVar1 & 0x3F;
        x = x >> 10 & 0xFC0;
        int chunkID = chunks[z / 4];
        uVar1 += x;
        return tilesDict[chunkID * 4096 + (int)uVar1];
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

    private void FUN_288E0(uint param1, uint param2, Queue<ScreenPoly> param3)
    {
        int iVar1;
        int iVar2;
        Vector2Int v2Var2;
        uint uVar3;
        uint uVar4;
        int iVar5;
        uint uVar6;
        uint uVar7;
        Vector2Int v2Var8;
        int iVar9;
        Vector2Int v2Var9;
        uint uVar10;
        Color32 uVar11;
        Color32 uVar12;
        Color32 uVar13;
        int puVar14;
        int puVar15;
        int puVar16;
        int puVar17;
        int puVar18;
        uint uVar19;
        uint uVar20;
        uint uVar21;

        iVar1 = (int)(param1 >> 6) * 32 + (int)(param2 >> 6);
        iVar2 = iVar1 * 4;
        iVar5 = (int)((param2 & 63) + (param1 & 63) * 64) * 2;
        puVar14 = chunks[32 + iVar1] * 4096 + iVar5 / 2;
        uVar7 = param1 + 4 & 63;
        puVar15 = puVar14 + 128;

        if (uVar7 == 0)
            puVar15 = chunks[64 + iVar1] * 4096 + (iVar5 - 7680) / 2;

        puVar16 = puVar14 + 2;
        puVar17 = puVar15 + 2;

        if ((param2 + 4 & 63) == 0)
        {
            puVar16 = chunks[33 + iVar1] * 4096 + (iVar5 - 120) / 2;
            puVar17 = puVar16 + 128;

            if (uVar7 == 0)
                puVar17 = chunks[65 + iVar1] * 4096;
        }

        iVar1 = (int)param2 * 128 + GameManager.DAT_1f800084.z;
        uVar4 = (uint)((int)param1 * 128 + GameManager.DAT_1f800084.x & 0xffff);
        iVar5 = GameManager.DAT_1f800084.y * 0x10000;
        Coprocessor.vector0.vx0 = (short)uVar4;
        Coprocessor.vector0.vy0 = (short)((vertices[puVar14] & 0x7ff) * 0x80000 + iVar5 >> 16);
        Coprocessor.vector0.vz0 = (short)iVar1;
        Coprocessor.ExecuteRTPS(12, false);
        uVar7 = (uint)GameManager.DAT_1f800098;
        uVar6 = uVar4 + 0x400 & 0xffff;
        iVar2 = Coprocessor.screenZFIFO.sz2;
        GameManager.terrainScreen[0].ir0 = Coprocessor.accumulator.ir0;
        Coprocessor.vector0.vx0 = (short)uVar6;
        Coprocessor.vector0.vy0 = (short)((vertices[puVar15] & 0x7ff) * 0x80000 + iVar5 >> 16);
        Coprocessor.vector0.vz0 = (short)iVar1;
        Coprocessor.ExecuteRTPS(12, false);
        uVar19 = (uint)(iVar2 < (int)uVar7 ? 1 : 0);
        iVar9 = Coprocessor.screenZFIFO.sz2;
        GameManager.terrainScreen[2].ir0 = Coprocessor.accumulator.ir0;
        Coprocessor.vector0.vx0 = (short)uVar4;
        Coprocessor.vector0.vy0 = (short)((vertices[puVar16] & 0x7ff) * 0x80000 + iVar5 >> 16);
        Coprocessor.vector0.vz0 = (short)(iVar1 + 1024);
        v2Var2 = new Vector2Int(Coprocessor.screenXYFIFO.sx1, Coprocessor.screenXYFIFO.sy1);
        Coprocessor.ExecuteRTPS(12, false);
        uVar10 = (uint)(iVar9 < (int)uVar7 ? 1 : 0) << 1;
        uVar20 = uVar19 | uVar10;
        iVar9 = Coprocessor.screenZFIFO.sz3;
        GameManager.terrainScreen[10].ir0 = Coprocessor.accumulator.ir0;
        Coprocessor.ExecuteNCLIP();
        Coprocessor.vector0.vx0 = (short)uVar6;
        Coprocessor.vector0.vy0 = (short)((vertices[puVar17] & 0x7ff) * 0x80000 + iVar5 >> 16);
        Coprocessor.vector0.vz0 = (short)(iVar1 + 1024);
        iVar5 = Coprocessor.mathsAccumulator.mac0;
        Coprocessor.ExecuteRTPS(12, false);
        uVar6 = (uint)(iVar9 < (int)uVar7 ? 1 : 0) << 2;
        iVar9 = Coprocessor.screenZFIFO.sz3;
        GameManager.terrainScreen[12].ir0 = Coprocessor.accumulator.ir0;
        Coprocessor.ExecuteAVSZ4();
        uVar7 = (uint)(iVar9 < (int)uVar7 ? 1 : 0) << 3;
        uVar21 = uVar20 | uVar6 | uVar7;
        Coprocessor.colorCode.r = GameManager.instance.DAT_DDC.r;
        Coprocessor.colorCode.g = GameManager.instance.DAT_DDC.g;
        Coprocessor.colorCode.b = GameManager.instance.DAT_DDC.b;
        Coprocessor.colorCode.code = GameManager.instance.DAT_DDC.a;

        if (uVar21 == 15)
        {
            GameManager.terrainScreen[0].vert.z = Coprocessor.screenZFIFO.sz0;
            GameManager.terrainScreen[2].vert.z = Coprocessor.screenZFIFO.sz1;
            GameManager.terrainScreen[10].vert.z = Coprocessor.screenZFIFO.sz2;
            GameManager.terrainScreen[12].vert.z = Coprocessor.screenZFIFO.sz3;
            Coprocessor.accumulator.ir1 = (short)((uint)vertices[puVar14] >> 13 << 7);
            Coprocessor.ExecuteCC(12, true);
            GameManager.terrainScreen[2].vert.x = Coprocessor.screenXYFIFO.sx0;
            GameManager.terrainScreen[2].vert.y = Coprocessor.screenXYFIFO.sy0;
            Coprocessor.accumulator.ir1 = (short)((uint)vertices[puVar15] >> 13 << 7);
            GameManager.terrainScreen[0].color = new Color32(Coprocessor.colorFIFO.r2, Coprocessor.colorFIFO.g2, Coprocessor.colorFIFO.b2, Coprocessor.colorFIFO.cd2);
            Coprocessor.ExecuteCC(12, true);
            GameManager.terrainScreen[10].vert.x = Coprocessor.screenXYFIFO.sx1;
            GameManager.terrainScreen[10].vert.y = Coprocessor.screenXYFIFO.sy1;
            Coprocessor.accumulator.ir1 = (short)((uint)vertices[puVar16] >> 13 << 7);
            GameManager.terrainScreen[2].color = new Color32(Coprocessor.colorFIFO.r2, Coprocessor.colorFIFO.g2, Coprocessor.colorFIFO.b2, Coprocessor.colorFIFO.cd2);
            Coprocessor.ExecuteCC(12, true);
            GameManager.terrainScreen[12].vert.x = Coprocessor.screenXYFIFO.sx2;
            GameManager.terrainScreen[12].vert.y = Coprocessor.screenXYFIFO.sy2;
            Coprocessor.accumulator.ir1 = (short)((uint)vertices[puVar17] >> 13 << 7);
            GameManager.terrainScreen[10].color = new Color32(Coprocessor.colorFIFO.r2, Coprocessor.colorFIFO.g2, Coprocessor.colorFIFO.b2, Coprocessor.colorFIFO.cd2);
            Coprocessor.ExecuteCC(12, true);
            uVar7 = (uint)(GameManager.DAT_1f800084.y * 0x10000);
            Coprocessor.vector0.vz0 = (short)iVar1;
            Coprocessor.vector1.vz1 = (short)(iVar1 + 512);
            Coprocessor.vector2.vz2 = (short)(iVar1 + 1024);
            uVar6 = uVar4 + 512 & 0xffff | uVar7;
            uint cop2r0 = (uint)(vertices[puVar14 + 128] & 0x7ff) * 0x80000 + uVar6;
            uint cop2r2 = (uint)(vertices[puVar14 + 130] & 0x7ff) * 0x80000 + uVar6;
            uint cop2r4 = (uint)(vertices[puVar16 + 128] & 0x7ff) * 0x80000 + uVar6;
            Coprocessor.vector0.vx0 = (short)cop2r0;
            Coprocessor.vector0.vy0 = (short)(cop2r0 >> 16);
            Coprocessor.vector1.vx1 = (short)cop2r2;
            Coprocessor.vector1.vy1 = (short)(cop2r2 >> 16);
            Coprocessor.vector2.vx2 = (short)cop2r4;
            Coprocessor.vector2.vy2 = (short)(cop2r4 >> 16);
            GameManager.terrainScreen[12].color = new Color32(Coprocessor.colorFIFO.r2, Coprocessor.colorFIFO.g2, Coprocessor.colorFIFO.b2, Coprocessor.colorFIFO.cd2);
            Coprocessor.ExecuteRTPT(12, false);
            GameManager.terrainScreen[1].vert.x = Coprocessor.screenXYFIFO.sx0;
            GameManager.terrainScreen[1].vert.y = Coprocessor.screenXYFIFO.sy0;
            GameManager.terrainScreen[11].vert.x = Coprocessor.screenXYFIFO.sx2;
            GameManager.terrainScreen[11].vert.y = Coprocessor.screenXYFIFO.sy2;
            Coprocessor.vector0.vx0 = (short)uVar4;
            Coprocessor.vector0.vy0 = (short)((vertices[puVar14 + 2] & 0x7ff) * 0x80000 + uVar7);
            cop2r4 = (uint)(vertices[puVar15 + 2] & 0x7ff) * 0x80000 +
                        (uVar4 + 1024 & 0xffff | uVar7);
            Coprocessor.vector2.vx2 = (short)cop2r4;
            Coprocessor.vector2.vy2 = (short)(cop2r4 >> 16);
            Coprocessor.vector0.vz0 = (short)(iVar1 + 512);
            Coprocessor.vector2.vz2 = (short)(iVar1 + 512);
            GameManager.terrainScreen[1].vert.z = Coprocessor.screenZFIFO.sz1;
            GameManager.terrainScreen[11].vert.z = Coprocessor.screenZFIFO.sz3;
            Coprocessor.ExecuteRTPT(12, false);
            GameManager.terrainScreen[5].vert.x = Coprocessor.screenXYFIFO.sx0;
            GameManager.terrainScreen[5].vert.y = Coprocessor.screenXYFIFO.sy0;
            GameManager.terrainScreen[6].vert.x = Coprocessor.screenXYFIFO.sx1;
            GameManager.terrainScreen[6].vert.y = Coprocessor.screenXYFIFO.sy1;
            cop2r0 = (uint)vertices[puVar14 + 128] >> 13 << 7;
            cop2r2 = (uint)vertices[puVar14 + 130] >> 13 << 7;
            cop2r4 = (uint)vertices[puVar16 + 128] >> 13 << 7;
            Coprocessor.vector0.vx0 = (short)cop2r0;
            Coprocessor.vector0.vy0 = (short)(cop2r0 >> 16);
            Coprocessor.vector1.vx1 = (short)cop2r2;
            Coprocessor.vector1.vy1 = (short)(cop2r2 >> 16);
            Coprocessor.vector2.vx2 = (short)cop2r4;
            Coprocessor.vector2.vy2 = (short)(cop2r4 >> 16);
            GameManager.terrainScreen[5].vert.z = Coprocessor.screenZFIFO.sz1;
            GameManager.terrainScreen[6].vert.z = Coprocessor.screenZFIFO.sz2;
            Coprocessor.ExecuteNCCT(12, true);
            GameManager.terrainScreen[1].color = new Color32(Coprocessor.colorFIFO.r0, Coprocessor.colorFIFO.g0, Coprocessor.colorFIFO.b0, Coprocessor.colorFIFO.cd0);
            Coprocessor.accumulator.ir1 = (short)((uint)vertices[puVar14 + 2] >> 13 << 7);
            GameManager.terrainScreen[6].color = new Color32(Coprocessor.colorFIFO.r1, Coprocessor.colorFIFO.g1, Coprocessor.colorFIFO.b1, Coprocessor.colorFIFO.cd1);
            GameManager.terrainScreen[11].color = new Color32(Coprocessor.colorFIFO.r2, Coprocessor.colorFIFO.g2, Coprocessor.colorFIFO.b2, Coprocessor.colorFIFO.cd2);
            Coprocessor.ExecuteCC(12, true);
            GameManager.terrainScreen[5].color = new Color32(Coprocessor.colorFIFO.r2, Coprocessor.colorFIFO.g2, Coprocessor.colorFIFO.b2, Coprocessor.colorFIFO.cd2);
            Coprocessor.accumulator.ir1 = (short)((uint)vertices[puVar15 + 2] >> 13 << 7);
            GameManager.terrainScreen[7].vert.x = Coprocessor.screenXYFIFO.sx2;
            GameManager.terrainScreen[7].vert.y = Coprocessor.screenXYFIFO.sy2;
            GameManager.terrainScreen[7].vert.z = Coprocessor.screenZFIFO.sz3;
            Coprocessor.ExecuteCC(12, true);
            uVar7 = uVar4 + 512 & 0xffff;
            iVar1 += 512;
            GameManager.terrainScreen[7].color = new Color32(Coprocessor.colorFIFO.r2, Coprocessor.colorFIFO.g2, Coprocessor.colorFIFO.b2, Coprocessor.colorFIFO.cd2);
            GameManager.terrainScreen[0].vert.x = v2Var2.x;
            GameManager.terrainScreen[0].vert.y = v2Var2.y;
            //FUN_290A8(uVar7, iVar1, param3, 6);
            //...
        }
        else
        {
            uVar3 = Coprocessor.averageZ;
            Coprocessor.accumulator.ir0 = (short)GameManager.terrainScreen[0].ir0;
            Coprocessor.accumulator.ir1 = (short)((uint)vertices[puVar14] >> 11 << 7);
            Coprocessor.ExecuteCDP(12, true);
            v2Var9 = new Vector2Int(Coprocessor.screenXYFIFO.sx0, Coprocessor.screenXYFIFO.sy0);
            Coprocessor.accumulator.ir0 = (short)GameManager.terrainScreen[2].ir0;
            Coprocessor.accumulator.ir1 = (short)((uint)vertices[puVar15] >> 11 << 7);
            puVar18 = GameManager.DAT_1f800080 + (int)(uVar3 >> 1);
            Coprocessor.ExecuteCDP(12, true);
            v2Var8 = new Vector2Int(Coprocessor.screenXYFIFO.sx1, Coprocessor.screenXYFIFO.sy1);
            Coprocessor.accumulator.ir0 = (short)GameManager.terrainScreen[10].ir0;
            Coprocessor.accumulator.ir1 = (short)((uint)vertices[puVar16] >> 11 << 7);
            Coprocessor.ExecuteCDP(12, true);

            if (uVar21 == 0)
            {
                if (iVar5 < 1)
                {
                    ScreenPoly screenPoly = new ScreenPoly
                    {
                        index = 0, //not in the original code
                        v1 = v2Var2,
                        v2 = v2Var9,
                        v3 = v2Var8,
                        clr1 = new Color32(Coprocessor.colorFIFO.r0, Coprocessor.colorFIFO.g0, Coprocessor.colorFIFO.b0, Coprocessor.colorFIFO.cd0),
                        clr2 = new Color32(Coprocessor.colorFIFO.r1, Coprocessor.colorFIFO.g1, Coprocessor.colorFIFO.b1, Coprocessor.colorFIFO.cd1),
                        clr3 = new Color32(Coprocessor.colorFIFO.r2, Coprocessor.colorFIFO.g2, Coprocessor.colorFIFO.b2, Coprocessor.colorFIFO.cd2)
                    };
                    param3.Enqueue(screenPoly);
                }

                Coprocessor.ExecuteNCLIP();
                iVar1 = Coprocessor.mathsAccumulator.mac0;
                Coprocessor.accumulator.ir0 = (short)GameManager.terrainScreen[12].ir0;
                Coprocessor.accumulator.ir1 = (short)((uint)vertices[puVar17] >> 11 << 7);

                if (-1 < iVar1)
                {
                    Coprocessor.ExecuteCDP(12, true);
                    ScreenPoly screenPoly = new ScreenPoly
                    {
                        index = 0, //not in the original code
                        v1 = v2Var9,
                        v2 = v2Var8,
                        v3 = new Vector2Int(Coprocessor.screenXYFIFO.sx2, Coprocessor.screenXYFIFO.sy2),
                        clr1 = new Color32(Coprocessor.colorFIFO.r0, Coprocessor.colorFIFO.g0, Coprocessor.colorFIFO.b0, Coprocessor.colorFIFO.cd0),
                        clr2 = new Color32(Coprocessor.colorFIFO.r1, Coprocessor.colorFIFO.g1, Coprocessor.colorFIFO.b1, Coprocessor.colorFIFO.cd1),
                        clr3 = new Color32(Coprocessor.colorFIFO.r2, Coprocessor.colorFIFO.g2, Coprocessor.colorFIFO.b2, Coprocessor.colorFIFO.cd2)
                    };
                    param3.Enqueue(screenPoly);
                }
            }
            else
            {
                GameManager.terrainScreen[12].vert.x = Coprocessor.screenXYFIFO.sx2;
                GameManager.terrainScreen[12].vert.y = Coprocessor.screenXYFIFO.sy2;
                Coprocessor.accumulator.ir0 = (short)GameManager.terrainScreen[12].ir0;
                Coprocessor.accumulator.ir1 = (short)((uint)vertices[puVar17] >> 11 << 7);
                GameManager.terrainScreen[0].vert.z = Coprocessor.screenZFIFO.sz0;
                GameManager.terrainScreen[2].vert.z = Coprocessor.screenZFIFO.sz1;
                GameManager.terrainScreen[10].vert.z = Coprocessor.screenZFIFO.sz2;
                GameManager.terrainScreen[12].vert.z = Coprocessor.screenZFIFO.sz3;
                GameManager.terrainScreen[0].color = new Color32(Coprocessor.colorFIFO.r0, Coprocessor.colorFIFO.g0, Coprocessor.colorFIFO.b0, Coprocessor.colorFIFO.cd0);
                GameManager.terrainScreen[2].color = new Color32(Coprocessor.colorFIFO.r1, Coprocessor.colorFIFO.g1, Coprocessor.colorFIFO.b1, Coprocessor.colorFIFO.cd1);
                GameManager.terrainScreen[10].color = new Color32(Coprocessor.colorFIFO.r2, Coprocessor.colorFIFO.g2, Coprocessor.colorFIFO.b2, Coprocessor.colorFIFO.cd2);
                Coprocessor.ExecuteCDP(12, true);
                GameManager.terrainScreen[12].color = new Color32(Coprocessor.colorFIFO.r2, Coprocessor.colorFIFO.g2, Coprocessor.colorFIFO.b2, Coprocessor.colorFIFO.cd2);
                iVar5 = GameManager.DAT_1f800084.y * 0x10000;

                if (uVar20 != 0)
                {
                    Coprocessor.vector0.vx0 = (short)(uVar4 + 512 & 0xffff);
                    Coprocessor.vector0.vy0 = (short)((vertices[puVar14 + 128] & 0x7ff) * 0x80000 + iVar5);
                    Coprocessor.vector0.vz0 = (short)iVar1;
                    Coprocessor.ExecuteRTPS(12, false);
                    GameManager.terrainScreen[1].vert.x = Coprocessor.screenXYFIFO.sx2;
                    GameManager.terrainScreen[1].vert.y = Coprocessor.screenXYFIFO.sy2;
                    Coprocessor.accumulator.ir1 = (short)((uint)vertices[puVar14 + 128] >> 11 << 7);
                    GameManager.terrainScreen[1].vert.z = Coprocessor.screenZFIFO.sz3;
                    Coprocessor.ExecuteCDP(12, true);
                }

                if ((uVar19 | uVar6) != 0)
                {
                    Coprocessor.vector0.vx0 = (short)uVar4;
                    Coprocessor.vector0.vy0 = (short)((vertices[puVar14 + 2] & 0x7ff) * 0x80000 + iVar5);
                    Coprocessor.vector0.vz0 = (short)(iVar1 + 512);
                    uVar11 = new Color32(Coprocessor.colorFIFO.r2, Coprocessor.colorFIFO.g2, Coprocessor.colorFIFO.b2, Coprocessor.colorFIFO.cd2);
                    Coprocessor.ExecuteRTPS(12, false);

                    if (uVar20 != 0)
                        GameManager.terrainScreen[1].color = uVar11;

                    GameManager.terrainScreen[5].vert.x = Coprocessor.screenXYFIFO.sx2;
                    GameManager.terrainScreen[5].vert.y = Coprocessor.screenXYFIFO.sy2;
                    Coprocessor.accumulator.ir1 = (short)((uint)vertices[puVar14 + 2] >> 11 << 7);
                    GameManager.terrainScreen[5].vert.z = Coprocessor.screenZFIFO.sz3;
                    Coprocessor.ExecuteCDP(12, true);
                }

                if ((uVar10 | uVar7) != 0)
                {
                    Coprocessor.vector0.vx0 = (short)(uVar4 + 1024 & 0xffff);
                    Coprocessor.vector0.vy0 = (short)((vertices[puVar15 + 2] & 0x7ff) * 0x80000 + iVar5);
                    Coprocessor.vector0.vz0 = (short)(iVar1 + 512);
                    uVar12 = new Color32(Coprocessor.colorFIFO.r2, Coprocessor.colorFIFO.g2, Coprocessor.colorFIFO.b2, Coprocessor.colorFIFO.cd2);
                    Coprocessor.ExecuteRTPS(12, false);
                    uVar11 = uVar12;

                    if ((uVar19 | uVar6) == 0)
                    {
                        uVar11 = GameManager.terrainScreen[5].color;

                        if (uVar20 != 0)
                            GameManager.terrainScreen[1].color = uVar12;
                    }

                    GameManager.terrainScreen[5].color = uVar11;
                    GameManager.terrainScreen[7].vert.x = Coprocessor.screenXYFIFO.sx2;
                    GameManager.terrainScreen[7].vert.y = Coprocessor.screenXYFIFO.sy2;
                    Coprocessor.accumulator.ir1 = (short)((uint)vertices[puVar15 + 2] >> 11 << 7);
                    GameManager.terrainScreen[7].vert.z = Coprocessor.screenZFIFO.sz3;
                    Coprocessor.ExecuteCDP(12, true);
                }

                if ((uVar6 | uVar7) == 0)
                {
                    if ((uVar10 | uVar7) == 0)
                    {
                        if ((uVar19 | uVar6) == 0)
                        {
                            if (uVar20 != 0)
                                GameManager.terrainScreen[1].color = new Color32(Coprocessor.colorFIFO.r2, Coprocessor.colorFIFO.g2, Coprocessor.colorFIFO.b2, Coprocessor.colorFIFO.cd2);
                        }
                        else
                            GameManager.terrainScreen[5].color = new Color32(Coprocessor.colorFIFO.r2, Coprocessor.colorFIFO.g2, Coprocessor.colorFIFO.b2, Coprocessor.colorFIFO.cd2);
                    }
                    else
                        GameManager.terrainScreen[7].color = new Color32(Coprocessor.colorFIFO.r2, Coprocessor.colorFIFO.g2, Coprocessor.colorFIFO.b2, Coprocessor.colorFIFO.cd2);
                }
                else
                {
                    Coprocessor.vector0.vx0 = (short)(uVar4 + 512 & 0xffff);
                    Coprocessor.vector0.vy0 = (short)((vertices[puVar16 + 128] & 0x7ff) * 0x80000 + iVar5);
                    Coprocessor.vector0.vz0 = (short)(iVar1 + 1024);
                    uVar13 = new Color32(Coprocessor.colorFIFO.r2, Coprocessor.colorFIFO.g2, Coprocessor.colorFIFO.b2, Coprocessor.colorFIFO.cd2);
                    Coprocessor.ExecuteRTPS(12, false);
                    GameManager.terrainScreen[11].vert.x = Coprocessor.screenXYFIFO.sx2;
                    GameManager.terrainScreen[11].vert.y = Coprocessor.screenXYFIFO.sy2;
                    Coprocessor.accumulator.ir1 = (short)((uint)vertices[puVar16 + 128] >> 11 << 7);
                    GameManager.terrainScreen[11].vert.z = Coprocessor.screenZFIFO.sz3;
                    Coprocessor.ExecuteCDP(12, true);
                    uVar11 = GameManager.terrainScreen[5].color;
                    uVar12 = uVar13;

                    if ((uVar10 | uVar7) == 0)
                    {
                        uVar11 = uVar13;
                        uVar12 = GameManager.terrainScreen[7].color;

                        if ((uVar19 | uVar6) == 0)
                        {
                            uVar11 = GameManager.terrainScreen[5].color;

                            if (uVar20 != 0)
                                GameManager.terrainScreen[1].color = uVar13;
                        }
                    }

                    GameManager.terrainScreen[7].color = uVar12;
                    GameManager.terrainScreen[5].color = uVar11;
                    GameManager.terrainScreen[11].color = new Color32(Coprocessor.colorFIFO.r2, Coprocessor.colorFIFO.g2, Coprocessor.colorFIFO.b2, Coprocessor.colorFIFO.cd2);
                }

                GameManager.terrainScreen[0].vert.x = v2Var2.x;
                GameManager.terrainScreen[0].vert.y = v2Var2.y;
                GameManager.terrainScreen[2].vert.x = v2Var9.x;
                GameManager.terrainScreen[2].vert.y = v2Var9.y;
                GameManager.terrainScreen[10].vert.x = v2Var8.x;
                GameManager.terrainScreen[10].vert.y = v2Var8.y;

            }
        }
    }

    private void FUN_290A8(uint param1, int param2, Queue<ScreenPoly> param3, int param4)
    {
        uint uVar2;
        int iVar4;
        Color32 clrVar4;
        int iVar6;
        int iVar9;
        Color32 clrVar9;
        int iVar11;
        Color32 clrVar11;
        uint uVar13;
        int puVar17;

        uVar2 = (ushort)GameManager.DAT_1f80009a;
        iVar4 = GameManager.terrainScreen[param4].vert.z;
        iVar6 = GameManager.terrainScreen[param4 + 1].vert.z;
        iVar9 = GameManager.terrainScreen[param4 + 5].vert.z;
        uVar13 = (uint)(iVar9 < (int)uVar2 ? 1 : 0) << 2;
        iVar11 = GameManager.terrainScreen[param4 + 6].vert.z;
        uVar2 = (uint)(iVar4 < (int)uVar2 ? 1 : 0) | (uint)(iVar6 < (int)uVar2 ? 1 : 0) << 1 | uVar13 |
                (uint)(iVar11 < (int)uVar2 ? 1 : 0) << 3;

        if (uVar2 == 0)
        {
            Coprocessor.screenXYFIFO.sx0 = (short)GameManager.terrainScreen[param4].vert.x;
            Coprocessor.screenXYFIFO.sy0 = (short)GameManager.terrainScreen[param4].vert.y;
            Coprocessor.screenXYFIFO.sx1 = (short)GameManager.terrainScreen[param4 + 1].vert.x;
            Coprocessor.screenXYFIFO.sy1 = (short)GameManager.terrainScreen[param4 + 1].vert.y;
            Coprocessor.screenXYFIFO.sx2 = (short)GameManager.terrainScreen[param4 + 5].vert.x;
            Coprocessor.screenXYFIFO.sy2 = (short)GameManager.terrainScreen[param4 + 5].vert.y;
            Coprocessor.ExecuteNCLIP();
            puVar17 = GameManager.DAT_1f800080 + (int)((uint)(iVar4 + iVar6 + iVar9 + iVar11) >> 5);
            clrVar9 = GameManager.terrainScreen[param4 + 2].color;
            clrVar11 = GameManager.terrainScreen[param4 + 5].color;
            iVar6 = Coprocessor.mathsAccumulator.mac0;
            param3[1].v2 = new Vector2Int(Coprocessor.screenXYFIFO.sx0, Coprocessor.screenXYFIFO.sy0);
            Coprocessor.screenXYFIFO.sx0 = (short)GameManager.terrainScreen[param4 + 6].vert.x;
            Coprocessor.screenXYFIFO.sy0 = (short)GameManager.terrainScreen[param4 + 6].vert.y;

            if (iVar6 < 1)
            {
                param3[2].v2 = new Vector2Int(Coprocessor.screenXYFIFO.sx1, Coprocessor.screenXYFIFO.sy1);
                param3[3].v2 = new Vector2Int(Coprocessor.screenXYFIFO.sx2, Coprocessor.screenXYFIFO.sy2);
                clrVar4 = GameManager.terrainScreen[param4].color;
                param3[2].clr = clrVar9;
                param3[3].clr = clrVar11;
                param3[1].clr = clrVar4;
            }
        }
    }
}
