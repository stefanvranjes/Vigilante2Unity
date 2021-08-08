using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Junction : MonoBehaviour
{
    public VigTransform vTransform;
    public XRTP_DB xrtp; //0x08
    public Vector3Int pos; //0x0C
    public int DAT_18; //0x18
    public int DAT_1C; //0x1C
    public List<Vector3Int> DAT_20; //0x20
    public List<short> DAT_26; //0x26
    public List<Vector3Int> DAT_28; //0x28
    public List<short> DAT_2E; //0x2E

    private Mesh mesh;
    private Texture mainT;
    private static Vector3[] newVertices = new Vector3[5121];
    private static Vector2[] newUVs = new Vector2[5121];
    private static Color32[] newColors = new Color32[5121];
    private static int[][] newTriangles = new int[16][]
    {
        new int[1023], new int[1023], new int[1023], new int[1023],
        new int[1023], new int[1023], new int[1023], new int[1023],
        new int[1023], new int[1023], new int[1023], new int[1023],
        new int[1023], new int[1023], new int[1023], new int[1023]
    };
    private int index;
    private int[] index2 = new int[16];
    private static Color32[] local_f0 = new Color32[32];
    private static Vector3[] verts = new Vector3[64];
    private static Color32[] colors = new Color32[64];

    // Start is called before the first frame update
    void Awake()
    {
        if (xrtp.timFarList.Count > 0)
            mainT = xrtp.timFarList[0].mainTexture;
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.subMeshCount = 16;
        /*newVertices = new Vector3[1023];
        newUVs = new Vector2[1023];
        newColors = new Color32[1023];
        newTriangles = new int[16][];
        index2 = new int[16];

        for (int i = 0; i < 16; i++)
            newTriangles[i] = new int[1023];*/
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(
            (float)vTransform.position.x / GameManager.instance.translateFactor,
            (float)-vTransform.position.y / GameManager.instance.translateFactor,
            (float)vTransform.position.z / GameManager.instance.translateFactor);

        //transform.localRotation = vTransform.rotation.Matrix2Quaternion;
        //transform.localEulerAngles = new Vector3(-transform.localEulerAngles.x, transform.localEulerAngles.y, -transform.localEulerAngles.z);
    }

    public void ClearRoadData()
    {
        for (int i = 0; i < index; i++)
        {
            newVertices[i] = new Vector3(0, 0, 0);
            newUVs[i] = new Vector2(0, 0);
        }

        for (int i = 0; i < 16; i++)
            for (int j = 0; j < index2[i]; j++)
                newTriangles[i][j] = 0;

        index = 0;
        for (int i = 0; i < index2.Length; i++)
            index2[i] = 0;

        mesh.Clear();
    }

    public void CreateRoadData()
    {
        mesh.subMeshCount = 16;

        for (int j = 0; j < index; j++)
            newVertices[j] = new Vector3(newVertices[j].x, -newVertices[j].y, newVertices[j].z);

        mesh.SetVertices(newVertices, 0, index);
        mesh.SetColors(newColors, 0, index);
        mesh.SetUVs(0, newUVs, 0, index);

        for (int j = 0; j < 16; j++)
            mesh.SetTriangles(newTriangles[j], 0, index2[j], j, false);
    }

    public void FUN_4F804()
    {
        ClearRoadData();
        FUN_4F828();
        CreateRoadData();
    }

    private void FUN_4F828()
    {
        uint uVar1;
        Color32 clrVar1;
        int iVar3;
        int iVar4;
        uint uVar5;
        uint uVar6;
        uint uVar7;
        Color32 clrVar7;
        int puVar8;
        int puVar9;
        int iVar10;
        uint uVar11;
        int iVar12;
        int iVar13;
        int puVar14;
        int puVar15;
        XRTP_DB dbVar16;
        int puVar18;
        int puVar19;
        uint uVar20;
        int local_38;
        short local_30;
        ushort local_28;
        Vector3 v3;
        int tFactor = GameManager.instance.translateFactor2;
        VigTerrain terrain = LevelManager.instance.terrain;
        int index3;

        puVar18 = 0;
        dbVar16 = xrtp;
        uVar7 = 0x2C;

        if ((dbVar16.DAT_2C & 2) != 0)
            uVar7 = 0x3C;

        if ((dbVar16.DAT_2C & 0x100) != 0)
            uVar7 |= 2;

        local_28 = (ushort)dbVar16.DAT_2E;

        for (int i = 0; i < 32; i++)
        {
            local_f0[i] = new Color32
                (
                    terrain.DAT_B9370[i].r,
                    terrain.DAT_B9370[i].g,
                    terrain.DAT_B9370[i].b,
                    (byte)uVar7
                );
        }

        Coprocessor.vector0.vx0 = (short)DAT_20[0].x;
        Coprocessor.vector0.vy0 = (short)DAT_20[0].y;
        Coprocessor.vector0.vz0 = (short)DAT_20[0].z;
        //roadVertices.Enqueue(new Vector3(Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor);
        Coprocessor.ExecuteRTPS(12, false);
        Coprocessor.vector0.vx0 = (short)DAT_28[0].x;
        Coprocessor.vector0.vy0 = (short)DAT_28[0].y;
        Coprocessor.vector0.vz0 = (short)DAT_28[0].z;
        //roadVertices.Enqueue(new Vector3(Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor);
        Coprocessor.ExecuteRTPS(12, false);

        if (dbVar16.DAT_14 < dbVar16.DAT_18 + DAT_1C)
            dbVar16.DAT_18 = 0;

        if ((dbVar16.DAT_2C & 2) == 0)
        {
            /*if (0 < DAT_1C)
            {
                puVar15 = dbVar16.DAT_18 * 10;

                for (int i = 0; i < DAT_1C; i++)
                {
                    puVar14 = i + 1;
                    Coprocessor.vector0.vx0 = (short)DAT_20[puVar14].x;
                    Coprocessor.vector0.vy0 = (short)DAT_20[puVar14].y;
                    Coprocessor.vector0.vz0 = (short)DAT_20[puVar14].z;
                    roadVertices.Enqueue(new Vector3(Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor);
                    Coprocessor.ExecuteRTPS(12, false);
                    Coprocessor.ExecuteNCLIP();
                    iVar3 = Coprocessor.mathsAccumulator.mac0;

                    if (iVar3 < 0)
                    {
                        iVar3 = Coprocessor.screenZFIFO.sz2;

                        if (iVar3 < (int)(uint)local_28)
                        {
                            if (0 < Coprocessor.screenZFIFO.sz1 || 0 < Coprocessor.screenZFIFO.sz2 ||
                                0 < Coprocessor.screenZFIFO.sz3)
                            {
                                puVar8 = dbVar16.DAT_20 * 40;
                                uVar1 = (uint)Coprocessor.screenXYFIFO.sy0 << 16 | (ushort)Coprocessor.screenXYFIFO.sx0;
                                //dbVar16.V3_DAT_10[puVar8 + 2] = roadVertices.Dequeue();
                                verts[2] = roadVertices.Dequeue();
                                uVar1 = (uint)Coprocessor.screenXYFIFO.sy1 << 16 | (ushort)Coprocessor.screenXYFIFO.sx1;
                                //dbVar16.V3_DAT_10[puVar8 + 14] = roadVertices.Dequeue();
                                verts[14] = roadVertices.Dequeue();
                                uVar1 = (uint)Coprocessor.screenXYFIFO.sy2 << 16 | (ushort)Coprocessor.screenXYFIFO.sx2;
                                //dbVar16.V3_DAT_10[puVar8 + 26] = roadVertices.Dequeue();
                                verts[26] = roadVertices.Dequeue();
                                uVar20 = (uint)Coprocessor.screenXYFIFO.sy0 << 16 | (ushort)Coprocessor.screenXYFIFO.sx0;
                                uVar5 = (uint)((int)uVar20 << 16 >> 16);
                                uVar20 = (uint)Coprocessor.screenXYFIFO.sy1 << 16 | (ushort)Coprocessor.screenXYFIFO.sx1;
                                uVar1 = (uint)((int)uVar20 << 16 >> 16);
                                uVar11 = uVar1;

                                if ((int)uVar5 < (int)uVar1)
                                    uVar11 = uVar5;

                                if ((int)uVar1 < (int)uVar5)
                                    uVar1 = uVar5;

                                uVar20 = (uint)Coprocessor.screenXYFIFO.sy2 << 16 | (ushort)Coprocessor.screenXYFIFO.sx2;
                                uVar5 = (uint)((int)uVar20 << 16 >> 16);
                                uVar6 = uVar5;

                                if ((int)uVar11 < (int)uVar5)
                                    uVar6 = uVar11;

                                if ((int)uVar5 < (int)uVar1)
                                    uVar5 = uVar1;

                                iVar12 = Coprocessor.screenZFIFO.sz1;
                                iVar3 = Coprocessor.screenZFIFO.sz2;
                                iVar4 = Coprocessor.screenZFIFO.sz3;
                                iVar13 = iVar12 * 4 + (iVar3 - iVar12) + (iVar4 - iVar12);
                                Coprocessor.vector0.vx0 = (short)DAT_28[puVar14].x;
                                Coprocessor.vector0.vy0 = (short)DAT_28[puVar14].y;
                                Coprocessor.vector0.vz0 = (short)DAT_28[puVar14].z;
                                roadVertices.Enqueue(new Vector3(Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor);
                                Coprocessor.vector1.vx1 = (short)((DAT_20[puVar18].x + DAT_28[puVar14 - 1].x) / 2);
                                Coprocessor.vector1.vy1 = (short)((DAT_20[puVar14 - 1].y + DAT_28[puVar14 - 1].y) / 2);
                                Coprocessor.vector1.vz1 = (short)((DAT_20[puVar14 - 1].z + DAT_28[puVar14 - 1].z) / 2);
                                roadVertices.Enqueue(new Vector3(Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor);
                                iVar10 = (DAT_20[puVar14 - 1].y + DAT_20[puVar14].y) / 2;
                                Coprocessor.vector2.vx2 = (short)((DAT_20[puVar18].x + DAT_20[puVar14].x) / 2);
                                Coprocessor.vector2.vy2 = (short)iVar10;
                                Coprocessor.vector2.vz2 = (short)((DAT_20[puVar14 - 1].z + DAT_20[puVar14].z) / 2);
                                roadVertices.Enqueue(new Vector3(Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor);
                                Coprocessor.ExecuteRTPT(12, false);
                                uVar20 = (uint)Coprocessor.screenXYFIFO.sy0 << 16 | (ushort)Coprocessor.screenXYFIFO.sx0;
                                uVar1 = (uint)((int)uVar20 << 16 >> 16);
                                uVar11 = uVar1;

                                if ((int)uVar6 < (int)uVar1)
                                    uVar11 = uVar6;

                                uVar6 = (uint)((int)uVar1 < (int)uVar5 ? 1 : 0);

                                if ((int)uVar11 < 320)
                                {
                                    if (uVar6 != 0)
                                        uVar1 = uVar5;

                                    if (-1 < (int)uVar1)
                                    {
                                        uVar1 = (uint)Coprocessor.screenXYFIFO.sy0 << 16 | (ushort)Coprocessor.screenXYFIFO.sx0;
                                        //dbVar16.V3_DAT_10[puVar8 + 38] = roadVertices.Dequeue();
                                        verts[38] = roadVertices.Dequeue();
                                        uVar1 = (uint)Coprocessor.screenXYFIFO.sy1 << 16 | (ushort)Coprocessor.screenXYFIFO.sx1;
                                        //dbVar16.V3_DAT_10[puVar8 + 12] = roadVertices.Peek();
                                        //dbVar16.V3_DAT_10[puVar8 + 4] = roadVertices.Dequeue();
                                        verts[12] = roadVertices.Peek();
                                        verts[4] = roadVertices.Dequeue();
                                        uVar1 = (uint)Coprocessor.screenXYFIFO.sy2 << 16 | (ushort)Coprocessor.screenXYFIFO.sx2;
                                        //dbVar16.V3_DAT_10[puVar8 + 22] = roadVertices.Peek();
                                        //dbVar16.V3_DAT_10[puVar8 + 6] = roadVertices.Dequeue();
                                        verts[22] = roadVertices.Peek();
                                        verts[6] = roadVertices.Dequeue();
                                        iVar10 = 0;

                                        if (0 < iVar13)
                                            iVar10 = iVar13;

                                        puVar9 = iVar10 >> 5;
                                        iVar10 = iVar13 + (iVar3 - iVar12) * 2;
                                        iVar3 = 0;

                                        if (0 < iVar10)
                                            iVar3 = iVar10;

                                        puVar9 = iVar3 >> 5;
                                        iVar4 = (iVar4 - iVar12) * 2;
                                        iVar13 += iVar4;
                                        iVar3 = 0;

                                        if (0 < iVar13)
                                            iVar3 = iVar13;

                                        puVar9 = iVar3 >> 5;
                                        iVar10 += iVar4;
                                        iVar3 = 0;

                                        if (0 < iVar10)
                                            iVar3 = iVar10;

                                        puVar9 = iVar3 >> 5;
                                        Coprocessor.vector0.vx0 = (short)((DAT_28[puVar14 - 1].x + DAT_28[puVar14].x) / 2);
                                        Coprocessor.vector0.vy0 = (short)((DAT_28[puVar14 - 1].y + DAT_28[puVar14].y) / 2);
                                        Coprocessor.vector0.vz0 = (short)((DAT_28[puVar14 - 1].z + DAT_28[puVar14].z) / 2);
                                        roadVertices.Enqueue(new Vector3(Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor);
                                        iVar10 = (DAT_20[puVar14].y + DAT_28[puVar14].y) / 2;
                                        Coprocessor.vector1.vx1 = (short)((DAT_20[puVar14].x + DAT_28[puVar14].x) / 2);
                                        Coprocessor.vector1.vy1 = (short)iVar10;
                                        Coprocessor.vector1.vz1 = (short)((DAT_20[puVar14].z + DAT_28[puVar14].z) / 2);
                                        roadVertices.Enqueue(new Vector3(Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor);
                                        Coprocessor.vector2.vx2 = (short)((DAT_28[puVar14 - 1].x + DAT_20[puVar14].x) / 2);
                                        Coprocessor.vector2.vy2 = (short)((DAT_28[puVar14 - 1].y + DAT_20[puVar14].y) / 2);
                                        Coprocessor.vector2.vz2 = (short)((DAT_28[puVar14 - 1].z + DAT_20[puVar14].z) / 2);
                                        roadVertices.Enqueue(new Vector3(Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor);
                                        Coprocessor.ExecuteRTPT(12, false);
                                        clrVar1 = local_f0[DAT_26[puVar14 - 1] >> 2];
                                        //dbVar16.C32_DAT_10[puVar8 + 1] = clrVar1;
                                        //dbVar16.C32_DAT_10[puVar8 + 31] = clrVar1;
                                        //dbVar16.C32_DAT_10[puVar8 + 21] = clrVar1;
                                        //dbVar16.C32_DAT_10[puVar8 + 11] = clrVar1;
                                        colors[1] = clrVar1;
                                        colors[31] = clrVar1;
                                        colors[21] = clrVar1;
                                        colors[11] = clrVar1;
                                        uVar1 = (uint)Coprocessor.screenXYFIFO.sy0 << 16 | (ushort)Coprocessor.screenXYFIFO.sx0;
                                        //dbVar16.V3_DAT_10[puVar8 + 34] = roadVertices.Peek();
                                        //dbVar16.V3_DAT_10[puVar8 + 18] = roadVertices.Dequeue();
                                        verts[34] = roadVertices.Peek();
                                        verts[18] = roadVertices.Dequeue();
                                        uVar1 = (uint)Coprocessor.screenXYFIFO.sy1 << 16 | (ushort)Coprocessor.screenXYFIFO.sx1;
                                        //dbVar16.V3_DAT_10[puVar8 + 36] = roadVertices.Peek();
                                        //dbVar16.V3_DAT_10[puVar8 + 28] = roadVertices.Dequeue();
                                        verts[36] = roadVertices.Peek();
                                        verts[28] = roadVertices.Dequeue();
                                        uVar1 = (uint)Coprocessor.screenXYFIFO.sy2 << 16 | (ushort)Coprocessor.screenXYFIFO.sx2;
                                        //dbVar16.V3_DAT_10[puVar8 + 32] = roadVertices.Peek();
                                        //dbVar16.V3_DAT_10[puVar8 + 24] = roadVertices.Peek();
                                        //dbVar16.V3_DAT_10[puVar8 + 16] = roadVertices.Peek();
                                        //dbVar16.V3_DAT_10[puVar8 + 8] = roadVertices.Dequeue();

                                        verts[32] = roadVertices.Peek();
                                        verts[24] = roadVertices.Peek();
                                        verts[16] = roadVertices.Peek();
                                        verts[8] = roadVertices.Dequeue();

                                        newVertices.Add(verts[2]);
                                        newVertices.Add(verts[4]);
                                        newVertices.Add(verts[6]);
                                        newColors.Add(colors[1]);
                                        newColors.Add(colors[1]);
                                        newColors.Add(colors[1]);
                                        newUVs.Add(new Vector2(0, 0));
                                        newUVs.Add(new Vector2(0, 0));
                                        newUVs.Add(new Vector2(0, 0));
                                        index = newVertices.Count - 1;
                                        newTriangles[0].Add(index);
                                        newTriangles[0].Add(index - 1);
                                        newTriangles[0].Add(index - 2);

                                        newVertices.Add(verts[12]);
                                        newVertices.Add(verts[14]);
                                        newVertices.Add(verts[16]);
                                        newColors.Add(colors[11]);
                                        newColors.Add(colors[11]);
                                        newColors.Add(colors[11]);
                                        newUVs.Add(new Vector2(0, 0));
                                        newUVs.Add(new Vector2(0, 0));
                                        newUVs.Add(new Vector2(0, 0));
                                        index += 3;
                                        newTriangles[0].Add(index);
                                        newTriangles[0].Add(index - 1);
                                        newTriangles[0].Add(index - 2);

                                        newVertices.Add(verts[22]);
                                        newVertices.Add(verts[24]);
                                        newVertices.Add(verts[26]);
                                        newColors.Add(colors[21]);
                                        newColors.Add(colors[21]);
                                        newColors.Add(colors[21]);
                                        newUVs.Add(new Vector2(0, 0));
                                        newUVs.Add(new Vector2(0, 0));
                                        newUVs.Add(new Vector2(0, 0));
                                        index += 3;
                                        newTriangles[0].Add(index);
                                        newTriangles[0].Add(index - 1);
                                        newTriangles[0].Add(index - 2);

                                        newVertices.Add(verts[32]);
                                        newVertices.Add(verts[34]);
                                        newVertices.Add(verts[36]);
                                        newColors.Add(colors[31]);
                                        newColors.Add(colors[31]);
                                        newColors.Add(colors[31]);
                                        newUVs.Add(new Vector2(0, 0));
                                        newUVs.Add(new Vector2(0, 0));
                                        newUVs.Add(new Vector2(0, 0));
                                        index += 3;
                                        newTriangles[0].Add(index);
                                        newTriangles[0].Add(index - 1);
                                        newTriangles[0].Add(index - 2);

                                        uVar1 = (uint)dbVar16.DAT_1C;
                                        uVar6 = (uint)dbVar16.DAT_20 + 1;
                                        uVar11 = 0;

                                        if (uVar6 != uVar1)
                                            uVar11 = uVar6;

                                        dbVar16.DAT_20 = (int)uVar11;
                                    }
                                }

                                Coprocessor.vector0.vx0 = (short)DAT_28[puVar14 - 1].x;
                                Coprocessor.vector0.vy0 = (short)DAT_28[puVar14 - 1].y;
                                Coprocessor.vector0.vz0 = (short)DAT_28[puVar14 - 1].z;
                                roadVertices.Enqueue(new Vector3(Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor);
                                Coprocessor.vector1.vx1 = (short)DAT_20[puVar14].x;
                                Coprocessor.vector1.vy1 = (short)DAT_20[puVar14].y;
                                Coprocessor.vector1.vz1 = (short)DAT_20[puVar14].z;
                                roadVertices.Enqueue(new Vector3(Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor);
                                Coprocessor.vector2.vx2 = (short)DAT_28[puVar14].x;
                                Coprocessor.vector2.vy2 = (short)DAT_28[puVar14].y;
                                Coprocessor.vector2.vz2 = (short)DAT_28[puVar14].z;
                                roadVertices.Enqueue(new Vector3(Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor);
                                Coprocessor.ExecuteRTPT(12, false);
                            }
                        }
                        else
                        {
                            uVar1 = (uint)Coprocessor.screenXYFIFO.sy0 << 16 | (ushort)Coprocessor.screenXYFIFO.sx0;
                            //dbVar16.V3_DAT_0C[puVar15 + 12] = roadVertices.Dequeue();
                            verts[12] = roadVertices.Dequeue();
                            uVar1 = (uint)Coprocessor.screenXYFIFO.sy1 << 16 | (ushort)Coprocessor.screenXYFIFO.sx1;
                            //dbVar16.V3_DAT_0C[puVar15 + 14] = roadVertices.Dequeue();
                            verts[14] = roadVertices.Dequeue();
                            uVar1 = (uint)Coprocessor.screenXYFIFO.sy2 << 16 | (ushort)Coprocessor.screenXYFIFO.sx2;
                            //dbVar16.V3_DAT_0C[puVar15 + 16] = roadVertices.Dequeue();
                            verts[16] = roadVertices.Dequeue();
                            Coprocessor.vector0.vx0 = (short)DAT_28[puVar14].x;
                            Coprocessor.vector0.vy0 = (short)DAT_28[puVar14].y;
                            Coprocessor.vector0.vz0 = (short)DAT_28[puVar14].z;
                            roadVertices.Enqueue(new Vector3(Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor);
                            Coprocessor.ExecuteRTPS(12, false);
                            //dbVar16.C32_DAT_0C[puVar15 + 11] = local_f0[DAT_26[puVar14 - 1] >> 2];
                            colors[11] = local_f0[DAT_26[puVar14 - 1] >> 2];
                            uVar1 = (uint)Coprocessor.screenXYFIFO.sy2 << 16 | (ushort)Coprocessor.screenXYFIFO.sx2;
                            //dbVar16.V3_DAT_0C[puVar15 + 18] = roadVertices.Dequeue();
                            verts[18] = roadVertices.Dequeue();
                            Coprocessor.ExecuteAVSZ4();
                            iVar3 = Coprocessor.accumulator.ir0;

                            if (iVar3 < 0)
                                iVar3 += 255;

                            //...
                            iVar3 = Coprocessor.averageZ;
                            puVar15 += 10;
                            dbVar16.DAT_18++;
                        }
                    }
                    else
                    {
                        Coprocessor.vector0.vx0 = (short)DAT_28[puVar14].x;
                        Coprocessor.vector0.vy0 = (short)DAT_28[puVar14].y;
                        Coprocessor.vector0.vz0 = (short)DAT_28[puVar14].z;
                        roadVertices.Enqueue(new Vector3(Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor);
                        Coprocessor.ExecuteRTPS(12, false);
                    }

                    puVar18++;
                }
            }*/
        }
        else
        {
            puVar19 = dbVar16.DAT_18 * 13;
            //...

            if (0 < DAT_1C)
            {
                puVar15 = puVar19 - 13;

                for (int i = 0; i < DAT_1C; i++)
                {
                    puVar14 = i + 1;
                    Coprocessor.vector0.vx0 = (short)DAT_20[puVar14].x;
                    Coprocessor.vector0.vy0 = (short)DAT_20[puVar14].y;
                    Coprocessor.vector0.vz0 = (short)DAT_20[puVar14].z;
                    //roadVertices.Enqueue(new Vector3(Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor);
                    Coprocessor.ExecuteRTPS(12, false);
                    Coprocessor.ExecuteNCLIP();
                    iVar3 = Coprocessor.mathsAccumulator.mac0;

                    //if (iVar3 < 0)
                    //{
                        iVar3 = Coprocessor.screenZFIFO.sz2;

                        if (iVar3 < local_28)
                        {
                            iVar3 = Coprocessor.screenZFIFO.sz1;

                            if (0 < iVar3 || 0 < Coprocessor.screenZFIFO.sz2 ||
                                0 < Coprocessor.screenZFIFO.sz3)
                            {
                                puVar8 = dbVar16.DAT_20 * 52;
                                uVar7 = (uint)Coprocessor.screenXYFIFO.sy0 << 16 | (ushort)Coprocessor.screenXYFIFO.sx0;
                                v3 = new Vector3(Coprocessor.screenXYZFIFO.sx0, Coprocessor.screenXYZFIFO.sy0, Coprocessor.screenXYZFIFO.sz0) / tFactor;
                                //V3_DAT_10[puVar8 + 2] = roadVertices.Dequeue();
                                verts[2] = v3;
                                uVar7 = (uint)Coprocessor.screenXYFIFO.sy1 << 16 | (ushort)Coprocessor.screenXYFIFO.sx1;
                                v3 = new Vector3(Coprocessor.screenXYZFIFO.sx1, Coprocessor.screenXYZFIFO.sy1, Coprocessor.screenXYZFIFO.sz1) / tFactor;
                                //dbVar16.V3_DAT_10[puVar8 + 18] = roadVertices.Dequeue();
                                verts[18] = v3;
                                uVar7 = (uint)Coprocessor.screenXYFIFO.sy2 << 16 | (ushort)Coprocessor.screenXYFIFO.sx2;
                                v3 = new Vector3(Coprocessor.screenXYZFIFO.sx2, Coprocessor.screenXYZFIFO.sy2, Coprocessor.screenXYZFIFO.sz2) / tFactor;
                                //dbVar16.V3_DAT_10[puVar8 + 34] = roadVertices.Dequeue();
                                verts[34] = v3;
                                uVar20 = (uint)Coprocessor.screenXYFIFO.sy0 << 16 | (ushort)Coprocessor.screenXYFIFO.sx0;
                                uVar11 = (uint)((int)uVar20 << 16 >> 16);
                                uVar20 = (uint)Coprocessor.screenXYFIFO.sy1 << 16 | (ushort)Coprocessor.screenXYFIFO.sx1;
                                uVar7 = (uint)((int)uVar20 << 16 >> 16);
                                uVar1 = uVar7;

                                if ((int)uVar11 < (int)uVar7)
                                    uVar1 = uVar11;

                                if ((int)uVar7 < (int)uVar11)
                                    uVar7 = uVar11;

                                uVar20 = (uint)Coprocessor.screenXYFIFO.sy2 << 16 | (ushort)Coprocessor.screenXYFIFO.sx2;
                                uVar11 = (uint)((int)uVar20 << 16 >> 16);
                                uVar5 = uVar11;

                                if ((int)uVar1 < (int)uVar11)
                                    uVar5 = uVar1;

                                if ((int)uVar11 < (int)uVar7)
                                    uVar11 = uVar7;

                                iVar12 = Coprocessor.screenZFIFO.sz1;
                                iVar3 = Coprocessor.screenZFIFO.sz2;
                                iVar4 = Coprocessor.screenZFIFO.sz3;
                                iVar13 = iVar12 * 4 + (iVar3 - iVar12) + (iVar4 - iVar12);
                                Coprocessor.vector0.vx0 = (short)DAT_28[puVar14].x;
                                Coprocessor.vector0.vy0 = (short)DAT_28[puVar14].y;
                                Coprocessor.vector0.vz0 = (short)DAT_28[puVar14].z;
                                //roadVertices.Enqueue(new Vector3(Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor);
                                Coprocessor.vector1.vx1 = (short)((DAT_20[puVar18].x + DAT_28[puVar14 - 1].x) / 2);
                                Coprocessor.vector1.vy1 = (short)((DAT_20[puVar14 - 1].y + DAT_28[puVar14 - 1].y) / 2);
                                Coprocessor.vector1.vz1 = (short)((DAT_20[puVar14 - 1].z + DAT_28[puVar14 - 1].z) / 2);
                                //roadVertices.Enqueue(new Vector3(Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor);
                                iVar10 = (DAT_20[puVar14 - 1].y + DAT_20[puVar14].y) / 2;
                                Coprocessor.vector2.vx2 = (short)((DAT_20[puVar18].x + DAT_20[puVar14].x) / 2);
                                Coprocessor.vector2.vy2 = (short)iVar10;
                                Coprocessor.vector2.vz2 = (short)((DAT_20[puVar14 - 1].z + DAT_20[puVar14].z) / 2);
                                //roadVertices.Enqueue(new Vector3(Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor);
                                Coprocessor.ExecuteRTPT(12, false);
                                //dbVar16.C32_DAT_10[puVar8 + 1] = local_f0[DAT_26[puVar14 - 1] >> 2];
                                //dbVar16.C32_DAT_10[puVar8 + 17] = local_f0[DAT_2E[puVar14 - 1] >> 2];
                                //dbVar16.C32_DAT_10[puVar8 + 33] = local_f0[DAT_26[puVar14] >> 2];
                                //dbVar16.C32_DAT_10[puVar8 + 49] = local_f0[DAT_2E[puVar14] >> 2];
                                colors[1] = local_f0[DAT_26[puVar14 - 1] >> 2];
                                colors[17] = local_f0[DAT_2E[puVar14 - 1] >> 2];
                                colors[33] = local_f0[DAT_26[puVar14] >> 2];
                                colors[49] = local_f0[DAT_2E[puVar14] >> 2];
                                uVar20 = (uint)Coprocessor.screenXYFIFO.sy0 << 16 | (ushort)Coprocessor.screenXYFIFO.sx0;
                                uVar7 = (uint)((int)uVar20 << 16 >> 16);
                                uVar1 = uVar7;

                                if ((int)uVar5 < (int)uVar7)
                                    uVar1 = uVar5;

                                uVar5 = (uint)((int)uVar7 < (int)uVar11 ? 1 : 0);

                                if (true) //((int)uVar1 < 320)
                                {
                                    if (uVar5 != 0)
                                        uVar7 = uVar11;

                                    if (true) //(-1 < (int)uVar7)
                                    {
                                        uVar7 = (uint)Coprocessor.screenXYFIFO.sy0 << 16 | (ushort)Coprocessor.screenXYFIFO.sx0;
                                        v3 = new Vector3(Coprocessor.screenXYZFIFO.sx0, Coprocessor.screenXYZFIFO.sy0, Coprocessor.screenXYZFIFO.sz0) / tFactor;
                                        //dbVar16.V3_DAT_10[puVar8 + 50] = roadVertices.Dequeue();
                                        verts[50] = v3;
                                        uVar7 = (uint)Coprocessor.screenXYFIFO.sy1 << 16 | (ushort)Coprocessor.screenXYFIFO.sx1;
                                        v3 = new Vector3(Coprocessor.screenXYZFIFO.sx1, Coprocessor.screenXYZFIFO.sy1, Coprocessor.screenXYZFIFO.sz1) / tFactor;
                                        //dbVar16.V3_DAT_10[puVar8 + 15] = roadVertices.Peek();
                                        //dbVar16.V3_DAT_10[puVar8 + 5] = roadVertices.Dequeue();
                                        verts[15] = v3;
                                        verts[5] = v3;
                                        uVar7 = (uint)Coprocessor.screenXYFIFO.sy2 << 16 | (ushort)Coprocessor.screenXYFIFO.sy2;
                                        v3 = new Vector3(Coprocessor.screenXYZFIFO.sx2, Coprocessor.screenXYZFIFO.sy2, Coprocessor.screenXYZFIFO.sz2) / tFactor;
                                        //dbVar16.V3_DAT_10[puVar8 + 28] = roadVertices.Peek();
                                        //dbVar16.V3_DAT_10[puVar8 + 8] = roadVertices.Dequeue();
                                        verts[28] = v3;
                                        verts[8] = v3;
                                        iVar10 = 0;

                                        if (0 < iVar13)
                                            iVar10 = iVar13;

                                        iVar10 = iVar13 + (iVar3 - iVar12) * 2;
                                        iVar3 = 0;

                                        if (0 < iVar10)
                                            iVar3 = iVar10;

                                        iVar4 = (iVar4 - iVar12) * 2;
                                        iVar13 += iVar4;
                                        iVar3 = 0;

                                        if (0 < iVar13)
                                            iVar3 = iVar13;

                                        iVar10 += iVar4;
                                        iVar3 = 0;

                                        if (0 < iVar10)
                                            iVar3 = iVar10;

                                        Coprocessor.vector0.vx0 = (short)((DAT_28[puVar14 - 1].x + DAT_28[puVar14].x) / 2);
                                        Coprocessor.vector0.vy0 = (short)((DAT_28[puVar14 - 1].y + DAT_28[puVar14].y) / 2);
                                        Coprocessor.vector0.vz0 = (short)((DAT_28[puVar14 - 1].z + DAT_28[puVar14].z) / 2);
                                        //roadVertices.Enqueue(new Vector3(Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor);
                                        iVar10 = (DAT_20[puVar14].y + DAT_28[puVar14].y) / 2;
                                        Coprocessor.vector1.vx1 = (short)((DAT_20[puVar14].x + DAT_28[puVar14].x) / 2);
                                        Coprocessor.vector1.vy1 = (short)iVar10;
                                        Coprocessor.vector1.vz1 = (short)((DAT_20[puVar14].z + DAT_28[puVar14].z) / 2);
                                        //roadVertices.Enqueue(new Vector3(Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor);
                                        Coprocessor.vector2.vx2 = (short)((DAT_28[puVar14 - 1].x + DAT_20[puVar14].x) / 2);
                                        Coprocessor.vector2.vy2 = (short)((DAT_28[puVar14 - 1].y + DAT_20[puVar14].y) / 2);
                                        Coprocessor.vector2.vz2 = (short)((DAT_28[puVar14 - 1].z + DAT_20[puVar14].z) / 2);
                                        //roadVertices.Enqueue(new Vector3(Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor);
                                        Coprocessor.ExecuteRTPT(12, false);
                                        clrVar7 = local_f0[DAT_26[puVar14 - 1] +
                                                           DAT_2E[puVar14 - 1] >> 3];
                                        //dbVar16.C32_DAT_10[puVar8 + 14] = clrVar7;
                                        //dbVar16.C32_DAT_10[puVar8 + 4] = clrVar7;
                                        colors[14] = clrVar7;
                                        colors[4] = clrVar7;
                                        clrVar7 = local_f0[DAT_26[puVar14 - 1] +
                                                           DAT_26[puVar14] >> 3];
                                        //dbVar16.C32_DAT_10[puVar8 + 27] = clrVar7;
                                        //dbVar16.C32_DAT_10[puVar8 + 7] = clrVar7;
                                        colors[27] = clrVar7;
                                        colors[7] = clrVar7;
                                        clrVar7 = local_f0[DAT_26[puVar14] +
                                                           DAT_2E[puVar14] >> 3];
                                        //dbVar16.C32_DAT_10[puVar8 + 46] = clrVar7;
                                        //dbVar16.C32_DAT_10[puVar8 + 36] = clrVar7;
                                        colors[46] = clrVar7;
                                        colors[36] = clrVar7;
                                        clrVar7 = local_f0[DAT_2E[puVar14 - 1] +
                                                           DAT_2E[puVar14] >> 3];
                                        //dbVar16.C32_DAT_10[puVar14 + 43] = clrVar7;
                                        //dbVar16.C32_DAT_10[puVar14 + 23] = clrVar7;
                                        colors[43] = clrVar7;
                                        colors[23] = clrVar7;
                                        clrVar7 = local_f0[DAT_2E[puVar14 - 1] +
                                                           DAT_26[puVar14] >> 3];
                                        //dbVar16.C32_DAT_10[puVar14 + 40] = clrVar7;
                                        //dbVar16.C32_DAT_10[puVar14 + 30] = clrVar7;
                                        //dbVar16.C32_DAT_10[puVar14 + 20] = clrVar7;
                                        //dbVar16.C32_DAT_10[puVar14 + 10] = clrVar7;
                                        colors[40] = clrVar7;
                                        colors[30] = clrVar7;
                                        colors[20] = clrVar7;
                                        colors[10] = clrVar7;
                                        uVar7 = (uint)Coprocessor.screenXYFIFO.sy0 << 16 | (ushort)Coprocessor.screenXYFIFO.sx0;
                                        v3 = new Vector3(Coprocessor.screenXYZFIFO.sx0, Coprocessor.screenXYZFIFO.sy0, Coprocessor.screenXYZFIFO.sz0) / tFactor;
                                        //dbVar16.V3_DAT_10[puVar14 + 44] = roadVertices.Peek();
                                        //dbVar16.V3_DAT_10[puVar14 + 24] = roadVertices.Dequeue();
                                        verts[44] = v3;
                                        verts[24] = v3;
                                        uVar7 = (uint)Coprocessor.screenXYFIFO.sy1 << 16 | (ushort)Coprocessor.screenXYFIFO.sx1;
                                        v3 = new Vector3(Coprocessor.screenXYZFIFO.sx1, Coprocessor.screenXYZFIFO.sy1, Coprocessor.screenXYZFIFO.sz1) / tFactor;
                                        //dbVar16.V3_DAT_10[puVar14 + 47] = roadVertices.Peek();
                                        //dbVar16.V3_DAT_10[puVar14 + 37] = roadVertices.Dequeue();
                                        verts[47] = v3;
                                        verts[37] = v3;
                                        uVar7 = (uint)Coprocessor.screenXYFIFO.sy2 << 16 | (ushort)Coprocessor.screenXYFIFO.sx2;
                                        v3 = new Vector3(Coprocessor.screenXYZFIFO.sx2, Coprocessor.screenXYZFIFO.sy2, Coprocessor.screenXYZFIFO.sz2) / tFactor;
                                        //dbVar16.V3_DAT_10[puVar14 + 41] = roadVertices.Peek();
                                        //dbVar16.V3_DAT_10[puVar14 + 31] = roadVertices.Peek();
                                        //dbVar16.V3_DAT_10[puVar14 + 21] = roadVertices.Peek();
                                        //dbVar16.V3_DAT_10[puVar14 + 11] = roadVertices.Dequeue();
                                        verts[41] = v3;
                                        verts[31] = v3;
                                        verts[21] = v3;
                                        verts[11] = v3;
                                    float distance = Vector3.Distance(transform.position + verts[11], GameManager.instance.cameraObjects[0].transform.position);
                                    index3 = (int)Mathf.Clamp(distance / 18, 0, 15);

                                        newVertices[index] = verts[2];
                                        newVertices[index + 1] = verts[5];
                                        newVertices[index + 2] = verts[8];
                                        newColors[index] = colors[1];
                                        newColors[index + 1] = colors[4];
                                        newColors[index + 2] = colors[7];
                                        newUVs[index] = new Vector2(0, 0);
                                        newUVs[index + 1] = new Vector2(0.5f, 0);
                                        newUVs[index + 2] = new Vector2(0, 0.5f);
                                        newTriangles[index3][index2[index3]] = index + 2;
                                        newTriangles[index3][index2[index3] + 1] = index + 1;
                                        newTriangles[index3][index2[index3] + 2] = index;
                                        index += 3;
                                        index2[index3] += 3;

                                        newVertices[index] = verts[5];
                                        newVertices[index + 1] = verts[8];
                                        newVertices[index + 2] = verts[11];
                                        newColors[index] = colors[4];
                                        newColors[index + 1] = colors[7];
                                        newColors[index + 2] = colors[10];
                                        newUVs[index] = new Vector2(0.5f, 0);
                                        newUVs[index + 1] = new Vector2(0, 0.5f);
                                        newUVs[index + 2] = new Vector2(0.5f, 0.5f);
                                        newTriangles[index3][index2[index3]] = index;
                                        newTriangles[index3][index2[index3] + 1] = index + 1;
                                        newTriangles[index3][index2[index3] + 2] = index + 2;
                                        index += 3;
                                        index2[index3] += 3;
                                        
                                        newVertices[index] = verts[15];
                                        newVertices[index + 1] = verts[18];
                                        newVertices[index + 2] = verts[21];
                                        newColors[index] = colors[14];
                                        newColors[index + 1] = colors[17];
                                        newColors[index + 2] = colors[20];
                                        newUVs[index] = new Vector2(0.5f, 0);
                                        newUVs[index + 1] = new Vector2(1, 0);
                                        newUVs[index + 2] = new Vector2(0.5f, 0.5f);
                                        newTriangles[index3][index2[index3]] = index + 2;
                                        newTriangles[index3][index2[index3] + 1] = index + 1;
                                        newTriangles[index3][index2[index3] + 2] = index;
                                        index += 3;
                                        index2[index3] += 3;

                                        newVertices[index] = verts[18];
                                        newVertices[index + 1] = verts[21];
                                        newVertices[index + 2] = verts[24];
                                        newColors[index] = colors[17];
                                        newColors[index + 1] = colors[20];
                                        newColors[index + 2] = colors[23];
                                        newUVs[index] = new Vector2(1, 0);
                                        newUVs[index + 1] = new Vector2(0.5f, 0.5f);
                                        newUVs[index + 2] = new Vector2(1, 0.5f);
                                        newTriangles[index3][index2[index3]] = index;
                                        newTriangles[index3][index2[index3] + 1] = index + 1;
                                        newTriangles[index3][index2[index3] + 2] = index + 2;
                                        index += 3;
                                        index2[index3] += 3;

                                        newVertices[index] = verts[28];
                                        newVertices[index + 1] = verts[31];
                                        newVertices[index + 2] = verts[34];
                                        newColors[index] = colors[27];
                                        newColors[index + 1] = colors[30];
                                        newColors[index + 2] = colors[33];
                                        newUVs[index] = new Vector2(0, 0.5f);
                                        newUVs[index + 1] = new Vector2(0.5f, 0.5f);
                                        newUVs[index + 2] = new Vector2(0, 1);
                                        newTriangles[index3][index2[index3]] = index + 2;
                                        newTriangles[index3][index2[index3] + 1] = index + 1;
                                        newTriangles[index3][index2[index3] + 2] = index;
                                        index += 3;
                                        index2[index3] += 3;

                                        newVertices[index] = verts[31];
                                        newVertices[index + 1] = verts[34];
                                        newVertices[index + 2] = verts[37];
                                        newColors[index] = colors[30];
                                        newColors[index + 1] = colors[33];
                                        newColors[index + 2] = colors[36];
                                        newUVs[index] = new Vector2(0.5f, 0.5f);
                                        newUVs[index + 1] = new Vector2(0, 1);
                                        newUVs[index + 2] = new Vector2(0.5f, 1);
                                        newTriangles[index3][index2[index3]] = index;
                                        newTriangles[index3][index2[index3] + 1] = index + 1;
                                        newTriangles[index3][index2[index3] + 2] = index + 2;
                                        index += 3;
                                        index2[index3] += 3;

                                        newVertices[index] = verts[41];
                                        newVertices[index + 1] = verts[44];
                                        newVertices[index + 2] = verts[47];
                                        newColors[index] = colors[40];
                                        newColors[index + 1] = colors[43];
                                        newColors[index + 2] = colors[46];
                                        newUVs[index] = new Vector2(0.5f, 0.5f);
                                        newUVs[index + 1] = new Vector2(1, 0.5f);
                                        newUVs[index + 2] = new Vector2(0.5f, 1);
                                        newTriangles[index3][index2[index3]] = index + 2;
                                        newTriangles[index3][index2[index3] + 1] = index + 1;
                                        newTriangles[index3][index2[index3] + 2] = index;
                                        index += 3;
                                        index2[index3] += 3;

                                        newVertices[index] = verts[44];
                                        newVertices[index + 1] = verts[47];
                                        newVertices[index + 2] = verts[50];
                                        newColors[index] = colors[43];
                                        newColors[index + 1] = colors[46];
                                        newColors[index + 2] = colors[49];
                                        newUVs[index] = new Vector2(1, 0.5f);
                                        newUVs[index + 1] = new Vector2(0.5f, 1);
                                        newUVs[index + 2] = new Vector2(1, 1);
                                        newTriangles[index3][index2[index3]] = index;
                                        newTriangles[index3][index2[index3] + 1] = index + 1;
                                        newTriangles[index3][index2[index3] + 2] = index + 2;
                                        index += 3;
                                        index2[index3] += 3;

                                        uVar7 = (uint)dbVar16.DAT_1C;
                                        uVar5 = (uint)dbVar16.DAT_20 + 1;
                                        uVar1 = 0;

                                        if (uVar5 != uVar7)
                                            uVar1 = uVar5;

                                        dbVar16.DAT_20 = (int)uVar1;
                                    }
                                }

                                Coprocessor.vector0.vx0 = (short)DAT_28[puVar14 - 1].x;
                                Coprocessor.vector0.vy0 = (short)DAT_28[puVar14 - 1].y;
                                Coprocessor.vector0.vz0 = (short)DAT_28[puVar14 - 1].z;
                                //roadVertices.Enqueue(new Vector3(Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor);
                                Coprocessor.vector1.vx1 = (short)DAT_20[puVar14].x;
                                Coprocessor.vector1.vy1 = (short)DAT_20[puVar14].y;
                                Coprocessor.vector1.vz1 = (short)DAT_20[puVar14].z;
                                //roadVertices.Enqueue(new Vector3(Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor);
                                Coprocessor.vector2.vx2 = (short)DAT_28[puVar14].x;
                                Coprocessor.vector2.vy2 = (short)DAT_28[puVar14].y;
                                Coprocessor.vector2.vz2 = (short)DAT_28[puVar14].z;
                                //roadVertices.Enqueue(new Vector3(Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor);
                                Coprocessor.ExecuteRTPT(12, false);
                            }
                        }
                        else
                        {
                        
                            uVar7 = (uint)Coprocessor.screenXYFIFO.sy0 << 16 | (ushort)Coprocessor.screenXYFIFO.sx0;
                            v3 = new Vector3(Coprocessor.screenXYZFIFO.sx0, Coprocessor.screenXYZFIFO.sy0, Coprocessor.screenXYZFIFO.sz0) / tFactor;
                            //dbVar16.V3_DAT_0C[puVar15 + 15] = roadVertices.Dequeue();
                            verts[15] = v3;
                            uVar7 = (uint)Coprocessor.screenXYFIFO.sy1 << 16 | (ushort)Coprocessor.screenXYFIFO.sx1;
                            v3 = new Vector3(Coprocessor.screenXYZFIFO.sx1, Coprocessor.screenXYZFIFO.sy1, Coprocessor.screenXYZFIFO.sz1) / tFactor;
                            //dbVar16.V3_DAT_0C[puVar15 + 18] = roadVertices.Dequeue();
                            verts[18] = v3;
                            uVar7 = (uint)Coprocessor.screenXYFIFO.sy2 << 16 | (ushort)Coprocessor.screenXYFIFO.sx2;
                            v3 = new Vector3(Coprocessor.screenXYZFIFO.sx2, Coprocessor.screenXYZFIFO.sy2, Coprocessor.screenXYZFIFO.sz2) / tFactor;
                            //dbVar16.V3_DAT_0C[puVar15 + 21] = roadVertices.Dequeue();
                            verts[21] = v3;
                            Coprocessor.vector0.vx0 = (short)DAT_28[puVar14].x;
                            Coprocessor.vector0.vy0 = (short)DAT_28[puVar14].y;
                            Coprocessor.vector0.vz0 = (short)DAT_28[puVar14].z;
                            //roadVertices.Enqueue(new Vector3(Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor);
                            Coprocessor.ExecuteRTPS(12, false);
                            //dbVar16.C32_DAT_0C[puVar15 + 14] = local_f0[DAT_26[puVar14 - 1] >> 2];
                            //dbVar16.C32_DAT_0C[puVar15 + 17] = local_f0[DAT_2E[puVar14 - 1] >> 2];
                            //dbVar16.C32_DAT_0C[puVar15 + 20] = local_f0[DAT_26[puVar14] >> 2];
                            //dbVar16.C32_DAT_0C[puVar15 + 23] = local_f0[DAT_2E[puVar14] >> 2];
                            colors[14] = local_f0[DAT_26[puVar14 - 1] >> 2];
                            colors[17] = local_f0[DAT_2E[puVar14 - 1] >> 2];
                            colors[20] = local_f0[DAT_26[puVar14] >> 2];
                            colors[23] = local_f0[DAT_2E[puVar14] >> 2];
                            uVar7 = (uint)Coprocessor.screenXYFIFO.sy2 << 16 | (ushort)Coprocessor.screenXYFIFO.sx2;
                            v3 = new Vector3(Coprocessor.screenXYZFIFO.sx2, Coprocessor.screenXYZFIFO.sy2, Coprocessor.screenXYZFIFO.sz2) / tFactor;
                            //dbVar16.V3_DAT_0C[puVar15 + 24] = roadVertices.Dequeue();
                            verts[24] = v3;
                        float distance = Vector3.Distance(transform.position + verts[15], GameManager.instance.cameraObjects[0].transform.position);
                        index3 = (int)Mathf.Clamp(distance / 18, 0, 15);

                            newVertices[index] = verts[15];
                            newVertices[index + 1] = verts[18];
                            newVertices[index + 2] = verts[21];
                            newColors[index] = colors[14];
                            newColors[index + 1] = colors[17];
                            newColors[index + 2] = colors[20];
                            newUVs[index] = new Vector2(0, 0);
                            newUVs[index + 1] = new Vector2(1, 0);
                            newUVs[index + 2] = new Vector2(0, 1);
                            newTriangles[index3][index2[index3]] = index + 2;
                            newTriangles[index3][index2[index3] + 1] = index + 1;
                            newTriangles[index3][index2[index3] + 2] = index;
                            index += 3;
                            index2[index3] += 3;

                            newVertices[index] = verts[18];
                            newVertices[index + 1] = verts[21];
                            newVertices[index + 2] = verts[24];
                            newColors[index] = colors[17];
                            newColors[index + 1] = colors[20];
                            newColors[index + 2] = colors[23];
                            newUVs[index] = new Vector2(1, 0);
                            newUVs[index + 1] = new Vector2(0, 1);
                            newUVs[index + 2] = new Vector2(1, 1);
                            newTriangles[index3][index2[index3]] = index;
                            newTriangles[index3][index2[index3] + 1] = index + 1;
                            newTriangles[index3][index2[index3] + 2] = index + 2;
                            index += 3;
                            index2[index3] += 3;

                            Coprocessor.ExecuteAVSZ4();
                            iVar3 = Coprocessor.accumulator.ir0;

                            if (iVar3 < 0)
                                iVar3 += 255;

                            //...
                            iVar3 = Coprocessor.averageZ;
                            puVar15 += 13;
                            puVar19 += 13;
                            dbVar16.DAT_18++;
                            
                        }
                    //}
                    /*else
                    {
                        Coprocessor.vector0.vx0 = (short)DAT_28[puVar14].x;
                        Coprocessor.vector0.vy0 = (short)DAT_28[puVar14].y;
                        Coprocessor.vector0.vz0 = (short)DAT_28[puVar14].z;
                        roadVertices.Enqueue(new Vector3(Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor);
                        Coprocessor.ExecuteRTPS(12, false);
                    }*/

                    puVar18 += 1;
                }
            }
        }
    }
}
