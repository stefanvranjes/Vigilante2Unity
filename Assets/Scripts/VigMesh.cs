using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VigMesh : MonoBehaviour
{
    public byte DAT_00; //0x00
    public byte DAT_01; //0x01
    public short DAT_02; //0x02
    public ushort vertices; //0x04
    public ushort faces; //0x06
    public byte[] vertexStream; //0x08
    public byte[] normalStream; //0x0C
    public byte[] faceStream; //0x10
    public byte[] DAT_14; //0x14
    public uint DAT_18; //0x18
    public int[] DAT_1C; //0x1C
    public Rect[] mainT;
    public Texture2D atlas;
    public Dictionary<int, int> materialIDs;
    public bool initAtStart;
    public MeshTopology topology;
    public int subMeshCount;
    public uint tmdID;
    public bool mirror;
    public XOBF_DB xobf;

    private VigMesh instance;
    private Mesh mesh;
    private static Vector3[] newVertices = new Vector3[1023];
    private static Vector2[] newUVs = new Vector2[1023];
    private static Color32[] newColors = new Color32[1023];
    private static int[][] newTriangles = new int[3][]
    {
        new int[1023], new int[1023], new int[1023]
    };
    private int index;
    private int[] index2 = new int[3];
    private float scale;
    private BufferedBinaryReader brVar3;
    private BufferedBinaryReader brVar4;
    private BufferedBinaryReader brVar6;
    private BufferedBinaryReader brVar13;
    private MeshRenderer renderer;

    private static uint[] DAT_22FEC = new uint[]
    {
        0x8002224c, 0x80022280, 0x800222e4, 0x80022330, 0x800224a0,
        0x800225ac, 0x8002260c, 0x80022674, 0x80022870, 0x80022a4c,
        0x80022b04, 0x80022b7c, 0x80022c54, 0x80022e2c, 0x80022fbc,
        0x80022e78, 0x800221cc, 0x80022280, 0x800222e4, 0x80022330,
        0x800223f0, 0x80022504, 0x8002260c, 0x80022674, 0x800227ac,
        0x800229a0, 0x80022b04, 0x80022b7c, 0x80022c54, 0x80022da8,
        0x80022fbc, 0x80022e78, 0x8002224c, 0x80022280, 0x800222e4,
        0x80022330, 0x8002246c, 0x80022580, 0x8002260c, 0x80022640,
        0x80022828, 0x80022a1c, 0x80022b04, 0x80022b44, 0x80022c54,
        0x80022e2c, 0x80022fbc, 0x80022e78, 0x800221cc, 0x80022280,
        0x800222e4, 0x80022330, 0x800223d4, 0x800224e8, 0x8002260c,
        0x80022640, 0x8002271c, 0x80022910, 0x80022b04, 0x80022b44,
        0x80022c54, 0x80022da8, 0x80022fbc, 0x80022e78
    };

    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        if (initAtStart)
        {
            Initialize();
            renderer.sharedMaterials = xobf.GetMaterialList(this, (int)tmdID);
        }
    }

    public Mesh GetMesh() { return mesh; }

    public void SetVertices(byte[] newBuffer, int start)
    {
        brVar3.ChangeBuffer(newBuffer);
        brVar3.Seek(start, SeekOrigin.Begin);
    }

    public void Initialize()
    {
        byte[] array;

        if (DAT_14 != null)
            array = DAT_14;
        else
            array = new byte[0];

        brVar13 = new BufferedBinaryReader(faceStream);

        brVar3 = new BufferedBinaryReader(vertexStream);

        brVar4 = new BufferedBinaryReader(normalStream);

        brVar6 = new BufferedBinaryReader(array);

        renderer = GetComponent<MeshRenderer>();
        mesh = GetComponent<MeshFilter>().mesh;
    }

    public void Initialize2()
    {
        byte[] array;

        if (DAT_14 != null)
            array = DAT_14;
        else
            array = new byte[0];

        brVar13 = new BufferedBinaryReader(faceStream);

        brVar3 = new BufferedBinaryReader(vertexStream);

        brVar4 = new BufferedBinaryReader(normalStream);

        brVar6 = new BufferedBinaryReader(array);

        mesh = new Mesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClearMeshData()
    {
        if (index == 0) return;

        for (int i = 0; i < index; i++)
        {
            newVertices[i] = new Vector3(0, 0, 0);
            newUVs[i] = new Vector2(0, 0);
        }

        for (int i = 0; i < mesh.subMeshCount; i++)
            for (int j = 0; j < index2[i]; j++)
                newTriangles[i][j] = 0;

        index = 0;
        for (int i = 0; i < index2.Length; i++)
            index2[i] = 0;

        mesh.Clear();
    }

    public void CreateMeshData()
    {
        if (mirror)
        {
            index *= 2;
            index2[1] *= 2;

            for (int i = index / 2, j = 0; i < index; i++, j++)
            {
                newVertices[i] = new Vector3(-newVertices[j].x, newVertices[j].y, -newVertices[j].z);
                newColors[i] = newColors[j];
                newUVs[i] = newUVs[j];
            }

            for (int i = index / 2; i < index; i += 3)
            {
                newTriangles[1][i] = i + 2;
                newTriangles[1][i + 1] = i + 1;
                newTriangles[1][i + 2] = i;
            }
        }

        for (int j = 0; j < index; j++)
            newVertices[j] = new Vector3(newVertices[j].x, -newVertices[j].y, newVertices[j].z) * scale;
        
        mesh.subMeshCount = subMeshCount;

        mesh.SetVertices(newVertices, 0, index);
        mesh.SetColors(newColors, 0, index);
        mesh.SetUVs(0, newUVs, 0, index);

        for (int j = 0; j < mesh.subMeshCount; j++)
        {
            MeshTopology _topology = j == subMeshCount - 1 ? topology : MeshTopology.Triangles;
            mesh.SetIndices(newTriangles[j], 0, index2[j], _topology, j, false);
        }
    }

    public void FUN_2D2A8(VigTransform param1)
    {
        int iVar1;
        Vector3Int auStack16;
        int local_c;

        auStack16 = Utilities.FUN_24148(GameManager.instance.DAT_F28, param1.position);
        local_c = GameManager.instance.DAT_DB0 - auStack16.y;
        iVar1 = local_c;

        if (local_c < 0)
            iVar1 = -local_c;

        if (iVar1 < DAT_18)
        {
            //...
            FUN_21F70(param1); //FUN_21F40
        }
        else
            FUN_21F70(param1);
    }

    public void FUN_2D4D4(VigTransform param1, Vector3Int param2, Vector3Int param3)
    {
        int iVar1;
        int iVar2;
        VigTransform MStack48;
        Vector3Int auStack16;

        MStack48 = Utilities.CompMatrixLV(GameManager.instance.DAT_F28, param1);
        iVar1 = (param3.x - MStack48.position.x) * param2.x +
                (param3.y - MStack48.position.y) * param2.y +
                (param3.z - MStack48.position.z) * param2.z;
        iVar2 = -iVar1;

        if (0 < iVar1)
            iVar2 += 4095;

        iVar2 >>= 12;
        iVar1 = iVar2;

        if (iVar2 < 0)
            iVar1 = -iVar2;

        if (iVar1 < DAT_18)
        {
            auStack16 = Utilities.FUN_24238(MStack48.rotation, param2);
            FUN_21F70(param1); //FUN_21ECC
        }
        else
        {
            //if (0 < iVar2) //tmp
                FUN_21F70(param1);
        }
    }

    public void FUN_21F70(VigTransform param1)
    {
        byte bVar1;
        int ppuVar2;
        int iVar3;
        int iVar4;
        int puVar5;
        int iVar6;
        int puVar7;
        int iVar8;
        uint uVar9;
        int puVar10;
        int iVar11;
        int iVar12;
        int iVar14;
        int iVar15;
        int iVar16;
        uint uVar17;
        float fVar18;
        float fVar19;
        
        int tFactor = GameManager.instance.translateFactor2;
        float min = GameManager.instance.pixelSnapMin;
        float max = GameManager.instance.pixelSnapMax;
        ClearMeshData();
        
        bVar1 = DAT_00;
        ppuVar2 = 0;

        if ((bVar1 & 4) != 0)
            ppuVar2 = 32;

        if (param1.position.z < DAT_18 * 5)
        {
            ppuVar2 += 16;
            bVar1 = DAT_00;
        }

        if ((bVar1 & 1) != 0)
        {
            Matrix3x3 gp00000f48 = GameManager.instance.DAT_F48;
            Coprocessor.rotationMatrix.rt11 = gp00000f48.V00;
            Coprocessor.rotationMatrix.rt12 = gp00000f48.V01;
            Coprocessor.rotationMatrix.rt13 = gp00000f48.V02;
            Coprocessor.rotationMatrix.rt21 = gp00000f48.V10;
            Coprocessor.rotationMatrix.rt22 = gp00000f48.V11;
            Coprocessor.rotationMatrix.rt23 = gp00000f48.V12;
            Coprocessor.rotationMatrix.rt31 = gp00000f48.V20;
            Coprocessor.rotationMatrix.rt32 = gp00000f48.V21;
            Coprocessor.rotationMatrix.rt33 = gp00000f48.V22;
            Coprocessor.accumulator.ir1 = param1.rotation.V00;
            Coprocessor.accumulator.ir2 = param1.rotation.V10;
            Coprocessor.accumulator.ir3 = param1.rotation.V20;
            Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.IR, _MVMVA_TRANSLATION_VECTOR.None, 12, false);
            iVar3 = Coprocessor.accumulator.ir1;
            iVar4 = Coprocessor.accumulator.ir2;
            iVar6 = Coprocessor.accumulator.ir3;
            Coprocessor.accumulator.ir1 = param1.rotation.V01;
            Coprocessor.accumulator.ir2 = param1.rotation.V11;
            Coprocessor.accumulator.ir3 = param1.rotation.V21;
            Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.IR, _MVMVA_TRANSLATION_VECTOR.None, 12, false);
            iVar8 = Coprocessor.accumulator.ir1;
            iVar11 = Coprocessor.accumulator.ir2;
            iVar12 = Coprocessor.accumulator.ir3;
            Coprocessor.accumulator.ir1 = param1.rotation.V02;
            Coprocessor.accumulator.ir2 = param1.rotation.V12;
            Coprocessor.accumulator.ir3 = param1.rotation.V22;
            Coprocessor.ExecuteMVMVA(_MVMVA_MULTIPLY_MATRIX.Rotation, _MVMVA_MULTIPLY_VECTOR.IR, _MVMVA_TRANSLATION_VECTOR.None, 12, false);
            Coprocessor.lightMatrix.l11 = (short)iVar3;
            Coprocessor.lightMatrix.l12 = (short)iVar8;
            Coprocessor.lightMatrix.l31 = (short)iVar6;
            Coprocessor.lightMatrix.l32 = (short)iVar12;
            iVar3 = Coprocessor.accumulator.ir1;
            iVar6 = Coprocessor.accumulator.ir2;
            iVar14 = Coprocessor.accumulator.ir3;
            Coprocessor.lightMatrix.l13 = (short)iVar3;
            Coprocessor.lightMatrix.l21 = (short)iVar4;
            Coprocessor.lightMatrix.l22 = (short)iVar11;
            Coprocessor.lightMatrix.l23 = (short)iVar6;
            Coprocessor.lightMatrix.l33 = (short)iVar14;
        }

        Coprocessor.rotationMatrix.rt11 = param1.rotation.V00;
        Coprocessor.rotationMatrix.rt12 = param1.rotation.V01;
        Coprocessor.rotationMatrix.rt13 = param1.rotation.V02;
        Coprocessor.rotationMatrix.rt21 = param1.rotation.V10;
        Coprocessor.rotationMatrix.rt22 = param1.rotation.V11;
        Coprocessor.rotationMatrix.rt23 = param1.rotation.V12;
        Coprocessor.rotationMatrix.rt31 = param1.rotation.V20;
        Coprocessor.rotationMatrix.rt32 = param1.rotation.V21;
        Coprocessor.rotationMatrix.rt33 = param1.rotation.V22;
        uVar9 = 16 - (uint)DAT_01;
        iVar16 = (bVar1 & 2) == 0 ? DAT_01 - 7 : 16;
        Coprocessor.translationVector._trx = param1.position.x >> (int)(uVar9 & 31);
        Coprocessor.translationVector._try = param1.position.y >> (int)(uVar9 & 31);
        Coprocessor.translationVector._trz = param1.position.z >> (int)(uVar9 & 31);
        fVar18 = DAT_01 - 8;
        fVar19 = Mathf.Pow(2f, fVar18);
        scale = 1f / fVar19;
        //Utilities.SetGlobalScale(transform, new Vector3(fVar19, fVar19, fVar19));
        brVar13.Seek(0, SeekOrigin.Begin);
        brVar3.Seek(0, SeekOrigin.Begin);
        brVar4.Seek(0, SeekOrigin.Begin);
        brVar6.Seek(0, SeekOrigin.Begin);
        /*Debug.Log(GetComponent<VigObject>().id);
        if (GetComponent<VigObject>().id == 80)
        {
            uVar9 = 0;
        }*/
        /*if (GetComponent<VigObject>().DAT_1A == 23)
        {
            uVar9 = 0;
        }*/

        for (int i = faces; i > 0;)
        {
            puVar5 = brVar13.ReadUInt16(4);
            puVar7 = brVar13.ReadUInt16(6);
            puVar10 = brVar13.ReadUInt16(8);
            Coprocessor.vector0.vx0 = brVar3.ReadInt16(puVar5);
            Coprocessor.vector0.vy0 = brVar3.ReadInt16(puVar5 + 2);
            Coprocessor.vector0.vz0 = brVar3.ReadInt16(puVar5 + 4);
            Coprocessor.vector1.vx1 = brVar3.ReadInt16(puVar7);
            Coprocessor.vector1.vy1 = brVar3.ReadInt16(puVar7 + 2);
            Coprocessor.vector1.vz1 = brVar3.ReadInt16(puVar7 + 4);
            Coprocessor.vector2.vx2 = brVar3.ReadInt16(puVar10);
            Coprocessor.vector2.vy2 = brVar3.ReadInt16(puVar10 + 2);
            Coprocessor.vector2.vz2 = brVar3.ReadInt16(puVar10 + 4);
            Coprocessor.ExecuteRTPT(12, false);
            i--;

            LAB_221B0:
            switch (DAT_22FEC[(brVar13.ReadByte(3) & 0x3C) / 4 + ppuVar2])
            {
                case 0x8002224C:
                    Coprocessor.ExecuteNCLIP();

                    if (true) //(Coprocessor.mathsAccumulator.mac0 > 0)
                    {
                        Color32 color = new Color32
                            (brVar13.ReadByte(0), brVar13.ReadByte(1), brVar13.ReadByte(2), 255 /*(byte)(brVar13.ReadByte(3) + 32)*/);
                        newColors[index] = color;
                        newColors[index + 1] = color;
                        newColors[index + 2] = color;
                        newUVs[index] = new Vector2(0, 0);
                        newUVs[index + 1] = new Vector2(0, 0);
                        newUVs[index + 2] = new Vector2(0, 0);
                        newVertices[index] = new Vector3
                            (Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor;
                        newVertices[index + 1] = new Vector3
                            (Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor;
                        newVertices[index + 2] = new Vector3
                            (Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor;
                        newTriangles[0][index2[0]] = index;
                        newTriangles[0][index2[0] + 1] = index + 1;
                        newTriangles[0][index2[0] + 2] = index + 2;
                        index += 3;
                        index2[0] += 3;
                        brVar13.Seek(12, SeekOrigin.Current);
                        iVar15 = 0x4000000;
                        goto LAB_22F08;
                    }
                    else
                    {
                        brVar13.Seek(12, SeekOrigin.Current);
                        break;
                    }

                case 0x80022280:
                    Coprocessor.ExecuteNCLIP();

                    if (true) //(Coprocessor.mathsAccumulator.mac0 > 0)
                    {
                        newColors[index] = new Color32
                            (brVar13.ReadByte(0), brVar13.ReadByte(1), brVar13.ReadByte(2), 255 /*(byte)(brVar13.ReadByte(3) + 48)*/);
                        int materialID = brVar13.ReadByte(22);
                        Rect rect = mainT[materialID];
                        float width = Mathf.Clamp(atlas.width * rect.width - 1, 1, atlas.width);
                        float height = Mathf.Clamp(atlas.height * rect.height - 1, 1, atlas.height);
                        newUVs[index] = new Vector2
                            (Mathf.Clamp((float)brVar13.ReadByte(12) / width, min, max) * rect.width + rect.x,
                                (-1f + (rect.height - Mathf.Clamp((float)brVar13.ReadByte(13) / height, min, max) * rect.height)) + rect.y + 1f);
                        newUVs[index + 1] = new Vector2
                            (Mathf.Clamp((float)brVar13.ReadByte(16) / width, min, max) * rect.width + rect.x,
                                (-1f + (rect.height - Mathf.Clamp((float)brVar13.ReadByte(17) / height, min, max) * rect.height)) + rect.y + 1f);
                        newUVs[index + 2] = new Vector2
                            (Mathf.Clamp((float)brVar13.ReadByte(20) / width, min, max) * rect.width + rect.x,
                                (-1f + (rect.height - Mathf.Clamp((float)brVar13.ReadByte(21) / height, min, max) * rect.height)) + rect.y + 1f);
                        newColors[index + 1] = new Color32
                            (brVar13.ReadByte(24), brVar13.ReadByte(25), brVar13.ReadByte(26), brVar13.ReadByte(27));
                        newColors[index + 2] = new Color32
                            (brVar13.ReadByte(28), brVar13.ReadByte(29), brVar13.ReadByte(30), brVar13.ReadByte(31));
                        newVertices[index] = new Vector3
                            (Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor;
                        newVertices[index + 1] = new Vector3
                            (Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor;
                        newVertices[index + 2] = new Vector3
                            (Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor;
                        newTriangles[1][index2[1]] = index + 2;
                        newTriangles[1][index2[1] + 1] = index + 1;
                        newTriangles[1][index2[1] + 2] = index;
                        index += 3;
                        index2[1] += 3;
                        brVar13.Seek(32, SeekOrigin.Current);
                        iVar15 = 0x9000000;
                        goto LAB_22F08;
                    }
                    else
                    {
                        brVar13.Seek(32, SeekOrigin.Current);
                        break;
                    }

                case 0x800222E4:
                    Coprocessor.ExecuteNCLIP();

                    if (true) //(Coprocessor.mathsAccumulator.mac0 > 0)
                    {
                        newColors[index] = new Color32
                            (brVar13.ReadByte(0), brVar13.ReadByte(1), brVar13.ReadByte(2), 255 /*(byte)(brVar13.ReadByte(3) + 40)*/);
                        newVertices[index] = new Vector3
                            (Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor;
                        newVertices[index + 1] = new Vector3
                            (Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor;
                        newVertices[index + 2] = new Vector3
                            (Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor;
                        newColors[index + 1] = new Color32
                            (brVar13.ReadByte(12), brVar13.ReadByte(13), brVar13.ReadByte(14), brVar13.ReadByte(15));
                        newColors[index + 2] = new Color32
                            (brVar13.ReadByte(20), brVar13.ReadByte(21), brVar13.ReadByte(22), brVar13.ReadByte(23));
                        newUVs[index] = new Vector2(0, 0);
                        newUVs[index + 1] = new Vector2(0, 0);
                        newUVs[index + 2] = new Vector2(0, 0);
                        newTriangles[0][index2[0]] = index;
                        newTriangles[0][index2[0] + 1] = index + 1;
                        newTriangles[0][index2[0] + 2] = index + 2;
                        index += 3;
                        index2[0] += 3;
                        brVar13.Seek(20, SeekOrigin.Current);
                        iVar15 = 0x6000000;
                        goto LAB_22F08;
                    }
                    else
                    {
                        brVar13.Seek(20, SeekOrigin.Current);
                        break;
                    }

                case 0x80022330:
                    Coprocessor.ExecuteNCLIP();

                    if (true) //(Coprocessor.mathsAccumulator.mac0 > 0)
                    {
                        newColors[index] = new Color32
                            (brVar13.ReadByte(0), brVar13.ReadByte(1), brVar13.ReadByte(2), 255 /*(byte)(brVar13.ReadByte(3) + 40)*/);
                        newColors[index + 1] = new Color32
                            (brVar13.ReadByte(24), brVar13.ReadByte(25), brVar13.ReadByte(26), brVar13.ReadByte(27));
                        newColors[index + 2] = new Color32
                            (brVar13.ReadByte(28), brVar13.ReadByte(29), brVar13.ReadByte(30), brVar13.ReadByte(31));
                        newVertices[index] = new Vector3
                            (Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor;
                        newVertices[index + 1] = new Vector3
                            (Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor;
                        newVertices[index + 2] = new Vector3
                            (Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor;
                        int materialID = DAT_1C[brVar13.ReadUInt16(22) & 0x3fff];
                        Rect rect = mainT[materialID];
                        float width = Mathf.Clamp(atlas.width * rect.width - 1, 1, atlas.width);
                        float height = Mathf.Clamp(atlas.height * rect.height - 1, 1, atlas.height);
                        newUVs[index] = new Vector2
                            (Mathf.Clamp((float)brVar13.ReadByte(12) / width, min, max) * rect.width + rect.x,
                                (-1f + (rect.height - Mathf.Clamp((float)brVar13.ReadByte(13) / height, min, max) * rect.height)) + rect.y + 1f);
                        newUVs[index + 1] = new Vector2
                            (Mathf.Clamp((float)brVar13.ReadByte(16) / width, min, max) * rect.width + rect.x,
                                (-1f + (rect.height - Mathf.Clamp((float)brVar13.ReadByte(17) / height, min, max) * rect.height)) + rect.y + 1f);
                        newUVs[index + 2] = new Vector2
                            (Mathf.Clamp((float)brVar13.ReadByte(20) / width, min, max) * rect.width + rect.x,
                                (-1f + (rect.height - Mathf.Clamp((float)brVar13.ReadByte(21) / height, min, max) * rect.height)) + rect.y + 1f);
                        newTriangles[subMeshCount - 1][index2[subMeshCount - 1]] = index + 2;
                        newTriangles[subMeshCount - 1][index2[subMeshCount - 1] + 1] = index + 1;
                        newTriangles[subMeshCount - 1][index2[subMeshCount - 1] + 2] = index;
                        index += 3;
                        index2[subMeshCount - 1] += 3;
                        brVar13.Seek(32, SeekOrigin.Current);
                        iVar15 = 0x9000000;
                        goto LAB_22F08;
                    }
                    else
                    {
                        brVar13.Seek(32, SeekOrigin.Current);
                        break;
                    }

                case 0x800224A0:
                    Coprocessor.ExecuteNCLIP();

                    if (true) //(Coprocessor.mathsAccumulator.mac0 > 0)
                    {
                        newVertices[index] = new Vector3
                            (Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor;
                        newVertices[index + 1] = new Vector3
                            (Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor;
                        newVertices[index + 2] = new Vector3
                            (Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor;
                        Coprocessor.colorCode.r = brVar13.ReadByte(0);
                        Coprocessor.colorCode.g = brVar13.ReadByte(1);
                        Coprocessor.colorCode.b = brVar13.ReadByte(2);
                        Coprocessor.colorCode.code = (byte)(brVar13.ReadByte(3) + 16);
                        puVar5 = brVar13.ReadInt16(10);
                        Coprocessor.vector0.vx0 = brVar4.ReadInt16(puVar5);
                        Coprocessor.vector0.vy0 = brVar4.ReadInt16(puVar5 + 2);
                        Coprocessor.vector0.vz0 = brVar4.ReadInt16(puVar5 + 4);
                        Coprocessor.ExecuteNCCS(12, true);
                        Color32 color = new Color32
                            (Coprocessor.colorFIFO.r2, Coprocessor.colorFIFO.g2, Coprocessor.colorFIFO.b2, 255 /*Coprocessor.colorFIFO.cd2*/);
                        newColors[index] = color;
                        newColors[index + 1] = color;
                        newColors[index + 2] = color;
                        newUVs[index] = new Vector2(0, 0);
                        newUVs[index + 1] = new Vector2(0, 0);
                        newUVs[index + 2] = new Vector2(0, 0);
                        newTriangles[0][index2[0]] = index;
                        newTriangles[0][index2[0] + 1] = index + 1;
                        newTriangles[0][index2[0] + 2] = index + 2;
                        index += 3;
                        index2[0] += 3;
                        brVar13.Seek(12, SeekOrigin.Current);
                        iVar15 = 0x4000000;
                        goto LAB_22F08;
                    }
                    else
                    {
                        brVar13.Seek(12, SeekOrigin.Current);
                        break;
                    }

                case 0x800225AC:
                    Coprocessor.ExecuteNCLIP();

                    if (true) //(Coprocessor.mathsAccumulator.mac0 > 0)
                    {
                        newVertices[index] = new Vector3
                            (Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor;
                        newVertices[index + 1] = new Vector3
                            (Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor;
                        newVertices[index + 2] = new Vector3
                            (Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor;
                        Coprocessor.colorCode.r = brVar13.ReadByte(0);
                        Coprocessor.colorCode.g = brVar13.ReadByte(1);
                        Coprocessor.colorCode.b = brVar13.ReadByte(2);
                        Coprocessor.colorCode.code = (byte)(brVar13.ReadByte(3) + 16);
                        puVar5 = brVar13.ReadInt16(10);
                        Coprocessor.vector0.vx0 = brVar4.ReadInt16(puVar5);
                        Coprocessor.vector0.vy0 = brVar4.ReadInt16(puVar5 + 2);
                        Coprocessor.vector0.vz0 = brVar4.ReadInt16(puVar5 + 4);
                        Coprocessor.ExecuteNCCS(12, true);
                        int materialID = brVar13.ReadByte(22);
                        Rect rect = mainT[materialID];
                        float width = Mathf.Clamp(atlas.width * rect.width - 1, 1, atlas.width);
                        float height = Mathf.Clamp(atlas.height * rect.height - 1, 1, atlas.height);
                        newUVs[index] = new Vector2
                            (Mathf.Clamp((float)brVar13.ReadByte(12) / width, min, max) * rect.width + rect.x,
                                (-1f + (rect.height - Mathf.Clamp((float)brVar13.ReadByte(13) / height, min, max) * rect.height)) + rect.y + 1f);
                        newUVs[index + 1] = new Vector2
                            (Mathf.Clamp((float)brVar13.ReadByte(16) / width, min, max) * rect.width + rect.x,
                                (-1f + (rect.height - Mathf.Clamp((float)brVar13.ReadByte(17) / height, min, max) * rect.height)) + rect.y + 1f);
                        newUVs[index + 2] = new Vector2
                            (Mathf.Clamp((float)brVar13.ReadByte(20) / width, min, max) * rect.width + rect.x,
                                (-1f + (rect.height - Mathf.Clamp((float)brVar13.ReadByte(21) / height, min, max) * rect.height)) + rect.y + 1f);
                        Color32 color = new Color32
                            (Coprocessor.colorFIFO.r2, Coprocessor.colorFIFO.g2, Coprocessor.colorFIFO.b2, 255 /*Coprocessor.colorFIFO.cd2*/);
                        newColors[index] = color;
                        newColors[index + 1] = color;
                        newColors[index + 2] = color;
                        newTriangles[1][index2[1]] = index + 2;
                        newTriangles[1][index2[1] + 1] = index + 1;
                        newTriangles[1][index2[1] + 2] = index;
                        index += 3;
                        index2[1] += 3;
                        brVar13.Seek(24, SeekOrigin.Current);
                        iVar15 = 0x7000000;
                        goto LAB_22F08;
                    }
                    else
                    {
                        brVar13.Seek(24, SeekOrigin.Current);
                        break;
                    }

                case 0x8002260C:
                    Color32 color32 = new Color32
                            (brVar13.ReadByte(0), brVar13.ReadByte(1), brVar13.ReadByte(2), 255 /*(byte)(brVar13.ReadByte(3) + 32)*/);
                    newColors[index] = color32;
                    newColors[index + 1] = color32;
                    newUVs[index] = new Vector2(0, 0);
                    newUVs[index + 1] = new Vector2(0, 0);
                    newVertices[index] = new Vector3
                        (Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor;
                    newVertices[index + 1] = new Vector3
                        (Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor;
                    newTriangles[subMeshCount - 1][index2[subMeshCount - 1]] = index;
                    newTriangles[subMeshCount - 1][index2[subMeshCount - 1] + 1] = index + 1;
                    index += 2;
                    index2[subMeshCount - 1] += 2;
                    brVar13.Seek(12, SeekOrigin.Current);
                    break;

                case 0x80022674:
                    Coprocessor.ExecuteNCLIP();

                    if (true) //(Coprocessor.mathsAccumulator.mac0 > 0)
                    {
                        //...
                        brVar13.Seek(24, SeekOrigin.Current);
                        iVar15 = 0x7000000;
                        goto LAB_22F08;
                    }
                    else
                    {
                        brVar13.Seek(24, SeekOrigin.Current);
                        break;
                    }

                case 0x80022870:
                    Coprocessor.ExecuteNCLIP();

                    if (true) //(Coprocessor.mathsAccumulator.mac0 > 0)
                    {
                        Coprocessor.ExecuteAVSZ3();
                        newVertices[index] = new Vector3
                            (Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor;
                        newVertices[index + 1] = new Vector3
                            (Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor;
                        newVertices[index + 2] = new Vector3
                            (Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor;
                        Coprocessor.colorCode.r = brVar13.ReadByte(0);
                        Coprocessor.colorCode.g = brVar13.ReadByte(1);
                        Coprocessor.colorCode.b = brVar13.ReadByte(2);
                        Coprocessor.colorCode.code = (byte)(brVar13.ReadByte(3) + 16);
                        puVar5 = brVar13.ReadInt16(10);
                        puVar7 = brVar13.ReadInt16(12);
                        puVar10 = brVar13.ReadInt16(14);
                        Coprocessor.vector0.vx0 = brVar4.ReadInt16(puVar5);
                        Coprocessor.vector0.vy0 = brVar4.ReadInt16(puVar5 + 2);
                        Coprocessor.vector0.vz0 = brVar4.ReadInt16(puVar5 + 4);
                        Coprocessor.vector1.vx1 = brVar4.ReadInt16(puVar7);
                        Coprocessor.vector1.vy1 = brVar4.ReadInt16(puVar7 + 2);
                        Coprocessor.vector1.vz1 = brVar4.ReadInt16(puVar7 + 4);
                        Coprocessor.vector2.vx2 = brVar4.ReadInt16(puVar10);
                        Coprocessor.vector2.vy2 = brVar4.ReadInt16(puVar10 + 2);
                        Coprocessor.vector2.vz2 = brVar4.ReadInt16(puVar10 + 4);
                        Coprocessor.ExecuteNCCT(12, true);
                        uVar17 = (uint)Coprocessor.averageZ >> iVar16;
                        newColors[index] = new Color32
                            (Coprocessor.colorFIFO.r0, Coprocessor.colorFIFO.g0, Coprocessor.colorFIFO.b0, 255 /*Coprocessor.colorFIFO.cd0*/);
                        newColors[index + 1] = new Color32
                            (Coprocessor.colorFIFO.r1, Coprocessor.colorFIFO.g1, Coprocessor.colorFIFO.b1, 255 /*Coprocessor.colorFIFO.cd1*/);
                        newColors[index + 2] = new Color32
                            (Coprocessor.colorFIFO.r2, Coprocessor.colorFIFO.g2, Coprocessor.colorFIFO.b2, 255 /*Coprocessor.colorFIFO.cd2*/);
                        newUVs[index] = new Vector2(0, 0);
                        newUVs[index + 1] = new Vector2(0, 0);
                        newUVs[index + 2] = new Vector2(0, 0);
                        newTriangles[0][index2[0]] = index;
                        newTriangles[0][index2[0] + 1] = index + 1;
                        newTriangles[0][index2[0] + 2] = index + 2;
                        index += 3;
                        index2[0] += 3;
                        brVar13.Seek(16, SeekOrigin.Current);
                        iVar15 = 0x6000000;
                        break;
                    }
                    else
                    {
                        brVar13.Seek(16, SeekOrigin.Current);
                        break;
                    }

                case 0x80022A4C:
                    Coprocessor.ExecuteNCLIP();

                    if (true) //(Coprocessor.mathsAccumulator.mac0 > 0)
                    {
                        Coprocessor.ExecuteAVSZ3();
                        newVertices[index] = new Vector3
                            (Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor;
                        newVertices[index + 1] = new Vector3
                            (Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor;
                        newVertices[index + 2] = new Vector3
                            (Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor;
                        Coprocessor.colorCode.r = brVar13.ReadByte(0);
                        Coprocessor.colorCode.g = brVar13.ReadByte(1);
                        Coprocessor.colorCode.b = brVar13.ReadByte(2);
                        Coprocessor.colorCode.code = (byte)(brVar13.ReadByte(3) + 16);
                        puVar5 = brVar13.ReadInt16(10);
                        puVar7 = brVar13.ReadInt16(12);
                        puVar10 = brVar13.ReadInt16(14);
                        Coprocessor.vector0.vx0 = brVar4.ReadInt16(puVar5);
                        Coprocessor.vector0.vy0 = brVar4.ReadInt16(puVar5 + 2);
                        Coprocessor.vector0.vz0 = brVar4.ReadInt16(puVar5 + 4);
                        Coprocessor.vector1.vx1 = brVar4.ReadInt16(puVar7);
                        Coprocessor.vector1.vy1 = brVar4.ReadInt16(puVar7 + 2);
                        Coprocessor.vector1.vz1 = brVar4.ReadInt16(puVar7 + 4);
                        Coprocessor.vector2.vx2 = brVar4.ReadInt16(puVar10);
                        Coprocessor.vector2.vy2 = brVar4.ReadInt16(puVar10 + 2);
                        Coprocessor.vector2.vz2 = brVar4.ReadInt16(puVar10 + 4);
                        Coprocessor.ExecuteNCCT(12, true);
                        uVar17 = (uint)Coprocessor.averageZ >> iVar16;
                        int materialID = brVar13.ReadByte(26);
                        Rect rect = mainT[materialID];
                        float width = Mathf.Clamp(atlas.width * rect.width - 1, 1, atlas.width);
                        float height = Mathf.Clamp(atlas.height * rect.height - 1, 1, atlas.height);
                        newUVs[index] = new Vector2
                            (Mathf.Clamp((float)brVar13.ReadByte(16) / width, min, max) * rect.width + rect.x,
                                (-1f + (rect.height - Mathf.Clamp((float)brVar13.ReadByte(17) / height, min, max) * rect.height)) + rect.y + 1f);
                        newUVs[index + 1] = new Vector2
                            (Mathf.Clamp((float)brVar13.ReadByte(20) / width, min, max) * rect.width + rect.x,
                                (-1f + (rect.height - Mathf.Clamp((float)brVar13.ReadByte(21) / height, min, max) * rect.height)) + rect.y + 1f);
                        newUVs[index + 2] = new Vector2
                            (Mathf.Clamp((float)brVar13.ReadByte(24) / width, min, max) * rect.width + rect.x,
                                (-1f + (rect.height - Mathf.Clamp((float)brVar13.ReadByte(25) / height, min, max) * rect.height)) + rect.y + 1f);
                        newColors[index] = new Color32
                            (Coprocessor.colorFIFO.r0, Coprocessor.colorFIFO.g0, Coprocessor.colorFIFO.b0, 255 /*Coprocessor.colorFIFO.cd0*/);
                        newColors[index + 1] = new Color32
                            (Coprocessor.colorFIFO.r1, Coprocessor.colorFIFO.g1, Coprocessor.colorFIFO.b1, 255 /*Coprocessor.colorFIFO.cd1*/);
                        newColors[index + 2] = new Color32
                            (Coprocessor.colorFIFO.r2, Coprocessor.colorFIFO.g2, Coprocessor.colorFIFO.b2, 255 /*Coprocessor.colorFIFO.cd2*/);
                        newTriangles[1][index2[1]] = index + 2;
                        newTriangles[1][index2[1] + 1] = index + 1;
                        newTriangles[1][index2[1] + 2] = index;
                        index += 3;
                        index2[1] += 3;
                        brVar13.Seek(28, SeekOrigin.Current);
                        iVar15 = 0x9000000;
                        break;
                    }
                    else
                    {
                        brVar13.Seek(28, SeekOrigin.Current);
                        break;
                    }

                case 0x80022B04:
                    int count = brVar13.ReadUInt16(10) * 8;
                    //...
                    brVar13.Seek(count + 12, SeekOrigin.Current);
                    break;

                case 0x80022B7C:
                    Coprocessor.ExecuteNCLIP();

                    if (true) //(Coprocessor.mathsAccumulator.mac0 > 0)
                    {
                        newVertices[index] = new Vector3
                            (Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor;
                        newVertices[index + 1] = new Vector3
                            (Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor;
                        newVertices[index + 2] = new Vector3
                            (Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor;
                        Coprocessor.colorCode.r = brVar13.ReadByte(0);
                        Coprocessor.colorCode.g = brVar13.ReadByte(1);
                        Coprocessor.colorCode.b = brVar13.ReadByte(2);
                        Coprocessor.colorCode.code = (byte)(brVar13.ReadByte(3) + 8);
                        puVar5 = brVar13.ReadInt16(10);
                        puVar7 = brVar13.ReadInt16(12);
                        puVar10 = brVar13.ReadInt16(14);
                        Coprocessor.vector0.vx0 = brVar4.ReadInt16(puVar5);
                        Coprocessor.vector0.vy0 = brVar4.ReadInt16(puVar5 + 2);
                        Coprocessor.vector0.vz0 = brVar4.ReadInt16(puVar5 + 4);
                        Coprocessor.vector1.vx1 = brVar4.ReadInt16(puVar7);
                        Coprocessor.vector1.vy1 = brVar4.ReadInt16(puVar7 + 2);
                        Coprocessor.vector1.vz1 = brVar4.ReadInt16(puVar7 + 4);
                        Coprocessor.vector2.vx2 = brVar4.ReadInt16(puVar10);
                        Coprocessor.vector2.vy2 = brVar4.ReadInt16(puVar10 + 2);
                        Coprocessor.vector2.vz2 = brVar4.ReadInt16(puVar10 + 4);
                        Coprocessor.ExecuteNCCT(12, true);
                        newColors[index] = new Color32
                            (Coprocessor.colorFIFO.r0, Coprocessor.colorFIFO.g0, Coprocessor.colorFIFO.b0, 255 /*Coprocessor.colorFIFO.cd0*/);
                        newColors[index + 1] = new Color32
                            (Coprocessor.colorFIFO.r1, Coprocessor.colorFIFO.g1, Coprocessor.colorFIFO.b1, 255 /*Coprocessor.colorFIFO.cd1*/);
                        newColors[index + 2] = new Color32
                            (Coprocessor.colorFIFO.r2, Coprocessor.colorFIFO.g2, Coprocessor.colorFIFO.b2, 255 /*Coprocessor.colorFIFO.cd2*/);
                        int materialID = DAT_1C[brVar13.ReadUInt16(26) & 0x3fff];
                        Rect rect = mainT[materialID];
                        float width = Mathf.Clamp(atlas.width * rect.width - 1, 1, atlas.width);
                        float height = Mathf.Clamp(atlas.height * rect.height - 1, 1, atlas.height);
                        newUVs[index] = new Vector2
                            (Mathf.Clamp((float)brVar13.ReadByte(16) / width, min, max) * rect.width + rect.x,
                                (-1f + (rect.height - Mathf.Clamp((float)brVar13.ReadByte(17) / height, min, max) * rect.height)) + rect.y + 1f);
                        newUVs[index + 1] = new Vector2
                            (Mathf.Clamp((float)brVar13.ReadByte(20) / width, min, max) * rect.width + rect.x,
                                (-1f + (rect.height - Mathf.Clamp((float)brVar13.ReadByte(21) / height, min, max) * rect.height)) + rect.y + 1f);
                        newUVs[index + 2] = new Vector2
                            (Mathf.Clamp((float)brVar13.ReadByte(24) / width, min, max) * rect.width + rect.x,
                                (-1f + (rect.height - Mathf.Clamp((float)brVar13.ReadByte(25) / height, min, max) * rect.height)) + rect.y + 1f);
                        newTriangles[1][index2[1]] = index + 2;
                        newTriangles[1][index2[1] + 1] = index + 1;
                        newTriangles[1][index2[1] + 2] = index;
                        index += 3;
                        index2[1] += 3;
                        brVar13.Seek(28, SeekOrigin.Current);
                        iVar15 = 0x9000000;
                        goto LAB_22F08;
                    }
                    else
                    {
                        brVar13.Seek(28, SeekOrigin.Current);
                        break;
                    }

                case 0x80022C54:
                    Coprocessor.ExecuteNCLIP();

                    if (true) //(Coprocessor.mathsAccumulator.mac0 > 0)
                    {
                        //tmp
                        if (brVar13.ReadUInt16(16) == 0x3fff)
                        {
                            Color32 color = new Color32
                                (brVar13.ReadByte(0), brVar13.ReadByte(1), brVar13.ReadByte(2), 255 /*(byte)(brVar13.ReadByte(3) + 32)*/);
                            newColors[index] = color;
                            newColors[index + 1] = color;
                            newColors[index + 2] = color;
                            newVertices[index] = new Vector3
                                (Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor;
                            newVertices[index + 1] = new Vector3
                                (Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor;
                            newVertices[index + 2] = new Vector3
                                (Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor;
                            newUVs[index] = new Vector2(0, 0);
                            newUVs[index + 1] = new Vector2(0, 0);
                            newUVs[index + 2] = new Vector2(0, 0);
                            newTriangles[0][index2[0]] = index;
                            newTriangles[0][index2[0] + 1] = index + 1;
                            newTriangles[0][index2[0] + 2] = index + 2;
                            index += 3;
                            index2[0] += 3;
                        }
                        brVar13.Seek(24, SeekOrigin.Current);
                        iVar15 = 0x7000000;
                        goto LAB_22F08;
                    }
                    else
                    {
                        brVar13.Seek(24, SeekOrigin.Current);
                        break;
                    }

                case 0x80022E2C:
                    Coprocessor.ExecuteNCLIP();

                    if (true) //(Coprocessor.mathsAccumulator.mac0 > 0)
                    {
                        Color32 color = new Color32
                                (brVar13.ReadByte(0), brVar13.ReadByte(1), brVar13.ReadByte(2), 255 /*(byte)(brVar13.ReadByte(3) + 240)*/);
                        newColors[index] = color;
                        newColors[index + 1] = color;
                        newColors[index + 2] = color;
                        newVertices[index] = new Vector3
                            (Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor;
                        newVertices[index + 1] = new Vector3
                            (Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor;
                        newVertices[index + 2] = new Vector3
                            (Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor;
                        int materialID = brVar13.ReadByte(22);
                        Rect rect = mainT[materialID];
                        float width = Mathf.Clamp(atlas.width * rect.width - 1, 1, atlas.width);
                        float height = Mathf.Clamp(atlas.height * rect.height - 1, 1, atlas.height);
                        newUVs[index] = new Vector2
                            (Mathf.Clamp((float)brVar13.ReadByte(12) / width, min, max) * rect.width + rect.x,
                                (-1f + (rect.height - Mathf.Clamp((float)brVar13.ReadByte(13) / height, min, max) * rect.height)) + rect.y + 1f);
                        newUVs[index + 1] = new Vector2
                            (Mathf.Clamp((float)brVar13.ReadByte(16) / width, min, max) * rect.width + rect.x,
                                (-1f + (rect.height - Mathf.Clamp((float)brVar13.ReadByte(17) / height, min, max) * rect.height)) + rect.y + 1f);
                        newUVs[index + 2] = new Vector2
                            (Mathf.Clamp((float)brVar13.ReadByte(20) / width, min, max) * rect.width + rect.x,
                                (-1f + (rect.height - Mathf.Clamp((float)brVar13.ReadByte(21) / height, min, max) * rect.height)) + rect.y + 1f);
                        newTriangles[1][index2[1]] = index + 2;
                        newTriangles[1][index2[1] + 1] = index + 1;
                        newTriangles[1][index2[1] + 2] = index;
                        index += 3;
                        index2[1] += 3;
                        brVar13.Seek(24, SeekOrigin.Current);
                        iVar15 = 0x7000000;
                        goto LAB_22F08;
                    }
                    else
                    {
                        brVar13.Seek(24, SeekOrigin.Current);
                        break;
                    }

                case 0x80022FBC:
                    return;

                case 0x80022E78:
                    Coprocessor.ExecuteNCLIP();

                    if (true) //(Coprocessor.mathsAccumulator.mac0 > 0)
                    {
                        Color32 color = new Color32
                            (brVar13.ReadByte(0), brVar13.ReadByte(1), brVar13.ReadByte(2), 255 /*(byte)(brVar13.ReadByte(3) + 232)*/);
                        newColors[index] = color;
                        newColors[index + 1] = color;
                        newColors[index + 2] = color;
                        newVertices[index] = new Vector3
                            (Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor;
                        newVertices[index + 1] = new Vector3
                            (Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor;
                        newVertices[index + 2] = new Vector3
                            (Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor;
                        int materialID = DAT_1C[brVar13.ReadUInt16(22) & 0x3fff];
                        Rect rect = mainT[materialID];
                        float width = Mathf.Clamp(atlas.width * rect.width - 1, 1, atlas.width);
                        float height = Mathf.Clamp(atlas.height * rect.height - 1, 1, atlas.height);
                        newUVs[index] = new Vector2
                            (Mathf.Clamp((float)brVar13.ReadByte(12) / width, min, max) * rect.width + rect.x,
                                (-1f + (rect.height - Mathf.Clamp((float)brVar13.ReadByte(13) / height, min, max) * rect.height)) + rect.y + 1f);
                        newUVs[index + 1] = new Vector2
                            (Mathf.Clamp((float)brVar13.ReadByte(16) / width, min, max) * rect.width + rect.x,
                                (-1f + (rect.height - Mathf.Clamp((float)brVar13.ReadByte(17) / height, min, max) * rect.height)) + rect.y + 1f);
                        newUVs[index + 2] = new Vector2
                            (Mathf.Clamp((float)brVar13.ReadByte(20) / width, min, max) * rect.width + rect.x,
                                (-1f + (rect.height - Mathf.Clamp((float)brVar13.ReadByte(21) / height, min, max) * rect.height)) + rect.y + 1f);
                        newTriangles[subMeshCount - 1][index2[subMeshCount - 1]] = index + 2;
                        newTriangles[subMeshCount - 1][index2[subMeshCount - 1] + 1] = index + 1;
                        newTriangles[subMeshCount - 1][index2[subMeshCount - 1] + 2] = index;
                        index += 3;
                        index2[subMeshCount - 1] += 3;
                        brVar13.Seek(24, SeekOrigin.Current);
                        iVar15 = 0x7000000;
                        goto LAB_22F08;
                    }
                    else
                    {
                        brVar13.Seek(24, SeekOrigin.Current);
                        break;
                    }

                case 0x800221CC:
                    Coprocessor.ExecuteNCLIP();

                    if (true) //(Coprocessor.mathsAccumulator.mac0 > 0)
                    {
                        //...
                        Color32 color = new Color32
                            (brVar13.ReadByte(0), brVar13.ReadByte(1), brVar13.ReadByte(2), 255 /*(byte)(brVar13.ReadByte(3) + 32)*/);
                        newColors[index] = color;
                        newColors[index + 1] = color;
                        newColors[index + 2] = color;
                        newVertices[index] = new Vector3
                            (Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor;
                        newVertices[index + 1] = new Vector3
                            (Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor;
                        newVertices[index + 2] = new Vector3
                            (Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor;
                        newUVs[index] = new Vector2(0, 0);
                        newUVs[index + 1] = new Vector2(0, 0);
                        newUVs[index + 2] = new Vector2(0, 0);
                        newTriangles[0][index2[0]] = index;
                        newTriangles[0][index2[0] + 1] = index + 1;
                        newTriangles[0][index2[0] + 2] = index + 2;
                        index += 3;
                        index2[0] += 3;
                        brVar13.Seek(12, SeekOrigin.Current);
                        iVar15 = 0x4000000;
                        goto LAB_22F08;
                    }
                    else
                    {
                        brVar13.Seek(12, SeekOrigin.Current);
                        break;
                    }

                case 0x800223F0:
                    Coprocessor.ExecuteNCLIP();

                    if (true) //(Coprocessor.mathAccumulator.mac0 > 0)
                    {
                        newVertices[index] = new Vector3
                            (Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor;
                        newVertices[index + 1] = new Vector3
                            (Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor;
                        newVertices[index + 2] = new Vector3
                            (Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor;
                        Coprocessor.colorCode.r = brVar13.ReadByte(0);
                        Coprocessor.colorCode.g = brVar13.ReadByte(1);
                        Coprocessor.colorCode.b = brVar13.ReadByte(2);
                        Coprocessor.colorCode.code = (byte)(brVar13.ReadByte(3) + 16);
                        puVar5 = brVar13.ReadInt16(10);
                        Coprocessor.vector0.vx0 = brVar4.ReadInt16(puVar5);
                        Coprocessor.vector0.vy0 = brVar4.ReadInt16(puVar5 + 2);
                        Coprocessor.vector0.vz0 = brVar4.ReadInt16(puVar5 + 4);
                        Coprocessor.ExecuteNCCS(12, true);
                        Color32 color = new Color32(Coprocessor.colorFIFO.r2, Coprocessor.colorFIFO.g2, Coprocessor.colorFIFO.b2, 255 /*Coprocessor.colorFIFO.cd2*/);
                        newColors[index] = color;
                        newColors[index + 1] = color;
                        newColors[index + 2] = color;
                        newUVs[index] = new Vector2(0, 0);
                        newUVs[index + 1] = new Vector2(0, 0);
                        newUVs[index + 2] = new Vector2(0, 0);
                        newTriangles[0][index2[0]] = index;
                        newTriangles[0][index2[0] + 1] = index + 1;
                        newTriangles[0][index2[0] + 2] = index + 2;
                        index += 3;
                        index2[0] += 3;
                        brVar13.Seek(12, SeekOrigin.Current);
                        iVar15 = 0x4000000;
                        goto LAB_22F08;
                    }
                    else
                    {
                        brVar13.Seek(12, SeekOrigin.Current);
                        goto LAB_22F08;
                    }

                case 0x80022504:
                    Coprocessor.ExecuteNCLIP();

                    if (true) //(Coprocessor.mathsAccumulator.mac0 > 0)
                    {
                        newVertices[index] = new Vector3
                            (Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor;
                        newVertices[index + 1] = new Vector3
                            (Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor;
                        newVertices[index + 2] = new Vector3
                            (Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor;
                        Coprocessor.colorCode.r = brVar13.ReadByte(0);
                        Coprocessor.colorCode.g = brVar13.ReadByte(1);
                        Coprocessor.colorCode.b = brVar13.ReadByte(2);
                        Coprocessor.colorCode.code = brVar13.ReadByte(3);
                        puVar5 = brVar13.ReadInt16(10);
                        Coprocessor.vector0.vx0 = brVar4.ReadInt16(puVar5);
                        Coprocessor.vector0.vy0 = brVar4.ReadInt16(puVar5 + 2);
                        Coprocessor.vector0.vz0 = brVar4.ReadInt16(puVar5 + 4);
                        Coprocessor.ExecuteNCCS(12, true);
                        int materialID = brVar13.ReadByte(22);
                        Rect rect = mainT[materialID];
                        float width = Mathf.Clamp(atlas.width * rect.width - 1, 1, atlas.width);
                        float height = Mathf.Clamp(atlas.height * rect.height - 1, 1, atlas.height);
                        newUVs[index] = new Vector2
                            (Mathf.Clamp((float)brVar13.ReadByte(12) / width, min, max) * rect.width + rect.x,
                                (-1f + (rect.height - Mathf.Clamp((float)brVar13.ReadByte(13) / height, min, max) * rect.height)) + rect.y + 1f);
                        newUVs[index + 1] = new Vector2
                            (Mathf.Clamp((float)brVar13.ReadByte(16) / width, min, max) * rect.width + rect.x,
                                (-1f + (rect.height - Mathf.Clamp((float)brVar13.ReadByte(17) / height, min, max) * rect.height)) + rect.y + 1f);
                        newUVs[index + 2] = new Vector2
                            (Mathf.Clamp((float)brVar13.ReadByte(20) / width, min, max) * rect.width + rect.x,
                                (-1f + (rect.height - Mathf.Clamp((float)brVar13.ReadByte(21) / height, min, max) * rect.height)) + rect.y + 1f);
                        Color32 color = new Color32
                            (Coprocessor.colorFIFO.r2, Coprocessor.colorFIFO.g2, Coprocessor.colorFIFO.b2, 255 /*Coprocessor.colorFIFO.cd2*/);
                        newColors[index] = color;
                        newColors[index + 1] = color;
                        newColors[index + 2] = color;
                        newTriangles[1][index2[1]] = index + 2;
                        newTriangles[1][index2[1] + 1] = index + 1;
                        newTriangles[1][index2[1] + 2] = index;
                        index += 3;
                        index2[1] += 3;
                        brVar13.Seek(24, SeekOrigin.Current);
                        goto LAB_22F08;
                    }
                    else
                    {
                        brVar13.Seek(24, SeekOrigin.Current);
                        break;
                    }

                case 0x800227AC:
                    //...
                    goto case 0x80022870;

                case 0x800229A0:
                    //...
                    goto case 0x80022A4C;

                case 0x80022DA8:
                    Coprocessor.ExecuteNCLIP();

                    if (true) //(Coprocessor.mathsAccumulator.mac0 > 0)
                    {
                        //...
                        Color32 color = new Color32
                            (brVar13.ReadByte(0), brVar13.ReadByte(1), brVar13.ReadByte(2), 255 /*(byte)(brVar13.ReadByte(3) + 240)*/);
                        newColors[index] = color;
                        newColors[index + 1] = color;
                        newColors[index + 2] = color;
                        newVertices[index] = new Vector3
                            (Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor;
                        newVertices[index + 1] = new Vector3
                            (Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor;
                        newVertices[index + 2] = new Vector3
                            (Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor;
                        int materialID = brVar13.ReadByte(22);
                        Rect rect = mainT[materialID];
                        float width = Mathf.Clamp(atlas.width * rect.width - 1, 1, atlas.width);
                        float height = Mathf.Clamp(atlas.height * rect.height - 1, 1, atlas.height);
                        newUVs[index] = new Vector2
                            (Mathf.Clamp((float)brVar13.ReadByte(12) / width, min, max) * rect.width + rect.x,
                                (-1f + (rect.height - Mathf.Clamp((float)brVar13.ReadByte(13) / height, min, max) * rect.height)) + rect.y + 1f);
                        newUVs[index + 1] = new Vector2
                            (Mathf.Clamp((float)brVar13.ReadByte(16) / width, min, max) * rect.width + rect.x,
                                (-1f + (rect.height - Mathf.Clamp((float)brVar13.ReadByte(17) / height, min, max) * rect.height)) + rect.y + 1f);
                        newUVs[index + 2] = new Vector2
                            (Mathf.Clamp((float)brVar13.ReadByte(20) / width, min, max) * rect.width + rect.x,
                                (-1f + (rect.height - Mathf.Clamp((float)brVar13.ReadByte(21) / height, min, max) * rect.height)) + rect.y + 1f);
                        newTriangles[1][index2[1]] = index + 2;
                        newTriangles[1][index2[1] + 1] = index + 1;
                        newTriangles[1][index2[1] + 2] = index;
                        index += 3;
                        index2[1] += 3;
                        brVar13.Seek(24, SeekOrigin.Current);
                        iVar15 = 0x7000000;
                        goto LAB_22F08;
                    }
                    else
                    {
                        brVar13.Seek(24, SeekOrigin.Current);
                        break;
                    }

                case 0x8002246C:
                    Coprocessor.ExecuteNCLIP();

                    if (true) //(Coprocessor.mathsAccumulator.mac0 > 0)
                    {
                        Color32 color = new Color32
                            (brVar6.ReadByte(0), brVar6.ReadByte(1), brVar6.ReadByte(2), 255 /*brVar6.ReadByte(3)*/);
                        newColors[index] = color;
                        newColors[index + 1] = color;
                        newColors[index + 2] = color;
                        newVertices[index] = new Vector3
                            (Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor;
                        newVertices[index + 1] = new Vector3
                            (Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor;
                        newVertices[index + 2] = new Vector3
                            (Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor;
                        newUVs[index] = new Vector2(0, 0);
                        newUVs[index + 1] = new Vector2(0, 0);
                        newUVs[index + 2] = new Vector2(0, 0);
                        newTriangles[0][index2[0]] = index;
                        newTriangles[0][index2[0] + 1] = index + 1;
                        newTriangles[0][index2[0] + 2] = index + 2;
                        index += 3;
                        index2[0] += 3;
                        brVar13.Seek(12, SeekOrigin.Current);
                        brVar6.Seek(4, SeekOrigin.Current);
                        iVar15 = 0x4000000;
                        goto LAB_22F08;
                    }
                    else
                    {
                        brVar13.Seek(12, SeekOrigin.Current);
                        brVar6.Seek(4, SeekOrigin.Current);
                        break;
                    }

                case 0x80022580:
                    Coprocessor.ExecuteNCLIP();

                    if (true) //(Coprocessor.mathsAccumulator.mac0 > 0)
                    {
                        Color32 color = new Color32
                            (brVar6.ReadByte(0), brVar6.ReadByte(1), brVar6.ReadByte(2), 255 /*brVar6.ReadByte(3)*/);
                        newColors[index] = color;
                        newColors[index + 1] = color;
                        newColors[index + 2] = color;
                        newVertices[index] = new Vector3
                            (Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor;
                        newVertices[index + 1] = new Vector3
                            (Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor;
                        newVertices[index + 2] = new Vector3
                            (Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor;
                        int materialID = brVar13.ReadByte(22);
                        Rect rect = mainT[materialID];
                        float width = Mathf.Clamp(atlas.width * rect.width - 1, 1, atlas.width);
                        float height = Mathf.Clamp(atlas.height * rect.height - 1, 1, atlas.height);
                        newUVs[index] = new Vector2
                            (Mathf.Clamp((float)brVar13.ReadByte(12) / width, min, max) * rect.width + rect.x,
                                (-1f + (rect.height - Mathf.Clamp((float)brVar13.ReadByte(13) / height, min, max) * rect.height)) + rect.y + 1f);
                        newUVs[index + 1] = new Vector2
                            (Mathf.Clamp((float)brVar13.ReadByte(16) / width, min, max) * rect.width + rect.x,
                                (-1f + (rect.height - Mathf.Clamp((float)brVar13.ReadByte(17) / height, min, max) * rect.height)) + rect.y + 1f);
                        newUVs[index + 2] = new Vector2
                            (Mathf.Clamp((float)brVar13.ReadByte(20) / width, min, max) * rect.width + rect.x,
                                (-1f + (rect.height - Mathf.Clamp((float)brVar13.ReadByte(21) / height, min, max) * rect.height)) + rect.y + 1f);
                        newTriangles[1][index2[1]] = index + 2;
                        newTriangles[1][index2[1] + 1] = index + 1;
                        newTriangles[1][index2[1] + 2] = index;
                        index += 3;
                        index2[1] += 3;
                        brVar13.Seek(24, SeekOrigin.Current);
                        brVar6.Seek(4, SeekOrigin.Current);
                        iVar15 = 0x7000000;
                        goto LAB_22F08;
                    }
                    else
                    {
                        brVar13.Seek(24, SeekOrigin.Current);
                        brVar6.Seek(4, SeekOrigin.Current);
                        break;
                    }

                case 0x80022640:
                    Coprocessor.ExecuteNCLIP();

                    if (true) //(Coprocessor.mathsAccumulator.mac0 > 0)
                    {
                        Color32 color = new Color32
                            (brVar6.ReadByte(0), brVar6.ReadByte(1), brVar6.ReadByte(2), 255 /*brVar6.ReadByte(3)*/);
                        //...
                        newVertices[index] = new Vector3
                            (Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor;
                        newVertices[index + 1] = new Vector3
                            (Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor;
                        newVertices[index + 2] = new Vector3
                            (Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor;
                        newColors[index] = color;
                        newColors[index + 1] = color;
                        newColors[index + 2] = color;
                        newUVs[index] = new Vector2(0, 0); //tmp
                        newUVs[index + 1] = new Vector2(0, 0); //tmp
                        newUVs[index + 2] = new Vector2(0, 0); //tmp
                        newTriangles[0][index2[0]] = index; //tmp
                        newTriangles[0][index2[0] + 1] = index + 1; //tmp
                        newTriangles[0][index2[0] + 2] = index + 2; //tmp
                        index += 3;
                        index2[0] += 3;
                        brVar13.Seek(24, SeekOrigin.Current);
                        brVar6.Seek(4, SeekOrigin.Current);
                        iVar15 = 0x7000000;
                        goto LAB_22F08;
                    }
                    else
                    {
                        brVar13.Seek(24, SeekOrigin.Current);
                        brVar6.Seek(4, SeekOrigin.Current);
                        break;
                    }

                case 0x80022828:
                    Coprocessor.ExecuteNCLIP();

                    if (true) //(Coprocessor.mathsAccumulator.mac0 > 0)
                    {
                        newColors[index] = new Color32
                            (brVar6.ReadByte(0), brVar6.ReadByte(1), brVar6.ReadByte(2), 255 /*brVar6.ReadByte(3)*/);
                        newColors[index + 1] = new Color32
                            (brVar6.ReadByte(4), brVar6.ReadByte(5), brVar6.ReadByte(6), 255 /*brVar6.ReadByte(7)*/);
                        newColors[index + 2] = new Color32
                            (brVar6.ReadByte(8), brVar6.ReadByte(9), brVar6.ReadByte(10), 255 /*brVar6.ReadByte(11)*/);
                        newVertices[index] = new Vector3
                            (Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor;
                        newVertices[index + 1] = new Vector3
                            (Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor;
                        newVertices[index + 2] = new Vector3
                            (Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor;
                        newUVs[index] = new Vector2(0, 0);
                        newUVs[index + 1] = new Vector2(0, 0);
                        newUVs[index + 2] = new Vector2(0, 0);
                        newTriangles[0][index2[0]] = index;
                        newTriangles[0][index2[0] + 1] = index + 1;
                        newTriangles[0][index2[0] + 2] = index + 2;
                        index += 3;
                        index2[0] += 3;
                        brVar13.Seek(16, SeekOrigin.Current);
                        brVar6.Seek(12, SeekOrigin.Current);
                        iVar15 = 0x6000000;
                        goto LAB_22F08;
                    }
                    else
                    {
                        brVar13.Seek(16, SeekOrigin.Current);
                        brVar6.Seek(12, SeekOrigin.Current);
                        break;
                    }

                case 0x80022A1C:
                    Coprocessor.ExecuteNCLIP();

                    if (true) //(Coprocessor.mathsAccumulator.mac0 > 0)
                    {
                        newColors[index] = new Color32
                            (brVar6.ReadByte(0), brVar6.ReadByte(1), brVar6.ReadByte(2), 255 /*brVar6.ReadByte(3)*/);
                        newColors[index + 1] = new Color32
                            (brVar6.ReadByte(4), brVar6.ReadByte(5), brVar6.ReadByte(6), 255 /*brVar6.ReadByte(7)*/);
                        newColors[index + 2] = new Color32
                            (brVar6.ReadByte(8), brVar6.ReadByte(9), brVar6.ReadByte(10), 255 /*brVar6.ReadByte(11)*/);
                        Coprocessor.ExecuteAVSZ3();
                        uVar17 = (uint)Coprocessor.averageZ >> iVar16;
                        int materialID = brVar13.ReadByte(26);
                        Rect rect = mainT[materialID];
                        float width = Mathf.Clamp(atlas.width * rect.width - 1, 1, atlas.width);
                        float height = Mathf.Clamp(atlas.height * rect.height - 1, 1, atlas.height);
                        newUVs[index] = new Vector2
                            (Mathf.Clamp((float)brVar13.ReadByte(16) / width, min, max) * rect.width + rect.x,
                                (-1f + (rect.height - Mathf.Clamp((float)brVar13.ReadByte(17) / height, min, max) * rect.height)) + rect.y + 1f);
                        newUVs[index + 1] = new Vector2
                            (Mathf.Clamp((float)brVar13.ReadByte(20) / width, min, max) * rect.width + rect.x,
                                (-1f + (rect.height - Mathf.Clamp((float)brVar13.ReadByte(21) / height, min, max) * rect.height)) + rect.y + 1f);
                        newUVs[index + 2] = new Vector2
                            (Mathf.Clamp((float)brVar13.ReadByte(24) / width, min, max) * rect.width + rect.x,
                                (-1f + (rect.height - Mathf.Clamp((float)brVar13.ReadByte(25) / height, min, max) * rect.height)) + rect.y + 1f);
                        newVertices[index] = new Vector3
                            (Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor;
                        newVertices[index + 1] = new Vector3
                            (Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor;
                        newVertices[index + 2] = new Vector3
                            (Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor;
                        newTriangles[1][index2[1]] = index + 2;
                        newTriangles[1][index2[1] + 1] = index + 1;
                        newTriangles[1][index2[1] + 2] = index;
                        index += 3;
                        index2[1] += 3;
                        brVar13.Seek(28, SeekOrigin.Current);
                        brVar6.Seek(12, SeekOrigin.Current);
                        iVar15 = 0x9000000;
                        break;
                    }
                    else
                    {
                        brVar13.Seek(28, SeekOrigin.Current);
                        brVar6.Seek(12, SeekOrigin.Current);
                        break;
                    }

                case 0x80022B44:
                    Coprocessor.ExecuteNCLIP();

                    if (true) //(Coprocessor.mathsAccumulator.mac0 > 0)
                    {
                        //...
                        brVar13.Seek(28, SeekOrigin.Current);
                        brVar6.Seek(12, SeekOrigin.Current);
                        iVar15 = 0x9000000;
                        goto LAB_22F08;
                    }
                    else
                    {
                        brVar13.Seek(28, SeekOrigin.Current);
                        brVar6.Seek(12, SeekOrigin.Current);
                        break;
                    }

                case 0x800223D4:
                    Coprocessor.ExecuteNCLIP();

                    if (true) //(Coprocessor.mathsAccumulator.mac0 > 0)
                    {
                        //...
                        Color32 color = new Color32
                            (brVar6.ReadByte(0), brVar6.ReadByte(1), brVar6.ReadByte(2), 255 /*brVar6.ReadByte(3)*/);
                        newColors[index] = color;
                        newColors[index + 1] = color;
                        newColors[index + 2] = color;
                        newVertices[index] = new Vector3
                            (Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor;
                        newVertices[index + 1] = new Vector3
                            (Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor;
                        newVertices[index + 2] = new Vector3
                            (Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor;
                        newUVs[index] = new Vector2(0, 0);
                        newUVs[index + 1] = new Vector2(0, 0);
                        newUVs[index + 2] = new Vector2(0, 0);
                        newTriangles[0][index2[0]] = index;
                        newTriangles[0][index2[0] + 1] = index + 1;
                        newTriangles[0][index2[0] + 2] = index + 2;
                        index += 3;
                        index2[0] += 3;
                        brVar13.Seek(12, SeekOrigin.Current);
                        brVar6.Seek(4, SeekOrigin.Current);
                        iVar15 = 0x4000000;
                        goto LAB_22F08;
                    }
                    else
                    {
                        brVar13.Seek(12, SeekOrigin.Current);
                        brVar6.Seek(4, SeekOrigin.Current);
                        break;
                    }

                case 0x800224E8:
                    Coprocessor.ExecuteNCLIP();

                    if (true) //(Coprocessor.mathsAccumulator.mac0 > 0)
                    {
                        //...
                        Color32 color = new Color32
                            (brVar6.ReadByte(0), brVar6.ReadByte(1), brVar6.ReadByte(2), 255 /*brVar6.ReadByte(3)*/);
                        newColors[index] = color;
                        newColors[index + 1] = color;
                        newColors[index + 2] = color;
                        newVertices[index] = new Vector3
                            (Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor;
                        newVertices[index + 1] = new Vector3
                            (Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor;
                        newVertices[index + 2] = new Vector3
                            (Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor;
                        int materialID = brVar13.ReadByte(22);
                        Rect rect = mainT[materialID];
                        float width = Mathf.Clamp(atlas.width * rect.width - 1, 1, atlas.width);
                        float height = Mathf.Clamp(atlas.height * rect.height - 1, 1, atlas.height);
                        newUVs[index] = new Vector2
                            (Mathf.Clamp((float)brVar13.ReadByte(12) / width, min, max) * rect.width + rect.x,
                                (-1f + (rect.height - Mathf.Clamp((float)brVar13.ReadByte(13) / height, min, max) * rect.height)) + rect.y + 1f);
                        newUVs[index + 1] = new Vector2
                            (Mathf.Clamp((float)brVar13.ReadByte(16) / width, min, max) * rect.width + rect.x,
                                (-1f + (rect.height - Mathf.Clamp((float)brVar13.ReadByte(17) / height, min, max) * rect.height)) + rect.y + 1f);
                        newUVs[index + 2] = new Vector2
                            (Mathf.Clamp((float)brVar13.ReadByte(20) / width, min, max) * rect.width + rect.x,
                                (-1f + (rect.height - Mathf.Clamp((float)brVar13.ReadByte(21) / height, min, max) * rect.height)) + rect.y + 1f);
                        newTriangles[1][index2[1]] = index + 2;
                        newTriangles[1][index2[1] + 1] = index + 1;
                        newTriangles[1][index2[1] + 2] = index;
                        index += 3;
                        index2[1] += 3;
                        brVar13.Seek(24, SeekOrigin.Current);
                        brVar6.Seek(4, SeekOrigin.Current);
                        iVar15 = 0x7000000;
                        goto LAB_22F08;
                    }
                    else
                    {
                        brVar13.Seek(24, SeekOrigin.Current);
                        brVar6.Seek(4, SeekOrigin.Current);
                        break;
                    }

                case 0x8002271C:
                    Coprocessor.ExecuteNCLIP();

                    if (true) //(Coprocessor.mathsAccumulator.mac0 > 0)
                    {
                        //...
                        newColors[index] = new Color32
                            (brVar6.ReadByte(0), brVar6.ReadByte(1), brVar6.ReadByte(2), 255 /*brVar6.ReadByte(3)*/);
                        newColors[index + 1] = new Color32
                            (brVar6.ReadByte(4), brVar6.ReadByte(5), brVar6.ReadByte(6), 255 /*brVar6.ReadByte(7)*/);
                        newColors[index + 2] = new Color32
                            (brVar6.ReadByte(8), brVar6.ReadByte(9), brVar6.ReadByte(10), 255 /*brVar6.ReadByte(11)*/);
                        newVertices[index] = new Vector3
                            (Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor;
                        newVertices[index + 1] = new Vector3
                            (Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor;
                        newVertices[index + 2] = new Vector3
                            (Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor;
                        newUVs[index] = new Vector2(0, 0);
                        newUVs[index + 1] = new Vector2(0, 0);
                        newUVs[index + 2] = new Vector2(0, 0);
                        newTriangles[0][index2[0]] = index;
                        newTriangles[0][index2[0] + 1] = index + 1;
                        newTriangles[0][index2[0] + 2] = index + 2;
                        index += 3;
                        index2[0] += 3;
                        brVar13.Seek(16, SeekOrigin.Current);
                        brVar6.Seek(12, SeekOrigin.Current);
                        iVar15 = 0x6000000;
                        goto LAB_22F08;
                    }
                    else
                    {
                        brVar13.Seek(16, SeekOrigin.Current);
                        brVar6.Seek(12, SeekOrigin.Current);
                        break;
                    }

                case 0x80022910:
                    Coprocessor.ExecuteNCLIP();

                    if (true) //(Coprocessor.mathsAccumulator.mac0 > 0)
                    {
                        //...
                        newColors[index] = new Color32
                            (brVar6.ReadByte(0), brVar6.ReadByte(1), brVar6.ReadByte(2), 255 /*brVar6.ReadByte(3)*/);
                        newColors[index + 1] = new Color32
                            (brVar6.ReadByte(4), brVar6.ReadByte(5), brVar6.ReadByte(6), 255 /*brVar6.ReadByte(7)*/);
                        newColors[index + 2] = new Color32
                            (brVar6.ReadByte(8), brVar6.ReadByte(9), brVar6.ReadByte(10), 255 /*brVar6.ReadByte(11)*/);
                        int materialID = brVar13.ReadByte(26);
                        Rect rect = mainT[materialID];
                        float width = Mathf.Clamp(atlas.width * rect.width - 1, 1, atlas.width);
                        float height = Mathf.Clamp(atlas.height * rect.height - 1, 1, atlas.height);
                        newUVs[index] = new Vector2
                            (Mathf.Clamp((float)brVar13.ReadByte(16) / width, min, max) * rect.width + rect.x,
                                (-1f + (rect.height - Mathf.Clamp((float)brVar13.ReadByte(17) / height, min, max) * rect.height)) + rect.y + 1f);
                        newUVs[index + 1] = new Vector2
                            (Mathf.Clamp((float)brVar13.ReadByte(20) / width, min, max) * rect.width + rect.x,
                                (-1f + (rect.height - Mathf.Clamp((float)brVar13.ReadByte(21) / height, min, max) * rect.height)) + rect.y + 1f);
                        newUVs[index + 2] = new Vector2
                            (Mathf.Clamp((float)brVar13.ReadByte(24) / width, min, max) * rect.width + rect.x,
                                (-1f + (rect.height - Mathf.Clamp((float)brVar13.ReadByte(25) / height, min, max) * rect.height)) + rect.y + 1f);
                        newVertices[index] = new Vector3
                            (Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor;
                        newVertices[index + 1] = new Vector3
                            (Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor;
                        newVertices[index + 2] = new Vector3
                            (Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor;
                        newTriangles[1][index2[1]] = index + 2;
                        newTriangles[1][index2[1] + 1] = index + 1;
                        newTriangles[1][index2[1] + 2] = index;
                        index += 3;
                        index2[1] += 3;
                        brVar13.Seek(28, SeekOrigin.Current);
                        brVar6.Seek(12, SeekOrigin.Current);
                        iVar15 = 0x9000000;
                        break;
                    }
                    else
                    {
                        brVar13.Seek(28, SeekOrigin.Current);
                        brVar6.Seek(12, SeekOrigin.Current);
                        break;
                    }
            }

            continue;

            LAB_22F08:
            Coprocessor.ExecuteAVSZ3();
            uVar17 = (uint)Coprocessor.averageZ >> iVar16;

            if (i > 0)
            {
                puVar5 = brVar13.ReadUInt16(4);
                puVar7 = brVar13.ReadUInt16(6);
                puVar10 = brVar13.ReadUInt16(8);
                Coprocessor.vector0.vx0 = brVar3.ReadInt16(puVar5);
                Coprocessor.vector0.vy0 = brVar3.ReadInt16(puVar5 + 2);
                Coprocessor.vector0.vz0 = brVar3.ReadInt16(puVar5 + 4);
                Coprocessor.vector1.vx1 = brVar3.ReadInt16(puVar7);
                Coprocessor.vector1.vy1 = brVar3.ReadInt16(puVar7 + 2);
                Coprocessor.vector1.vz1 = brVar3.ReadInt16(puVar7 + 4);
                Coprocessor.vector2.vx2 = brVar3.ReadInt16(puVar10);
                Coprocessor.vector2.vy2 = brVar3.ReadInt16(puVar10 + 2);
                Coprocessor.vector2.vz2 = brVar3.ReadInt16(puVar10 + 4);
                Coprocessor.ExecuteRTPT(12, false);
                i--;
                goto LAB_221B0;
            }
        }

        CreateMeshData();
    }

    //FUN_39A8 (LOAD.DLL)
    public void FUN_39A8(DELEGATE_79A0 param1)
    {
        byte bVar1;
        uint uVar2;
        uint uVar3;
        int iVar4;
        MemoryStream msVar4;
        MemoryStream msVar5;
        MemoryStream msVar6;
        long lVar7;
        long lVar8;
        long lVar9;
        MemoryStream msVar7;
        MemoryStream msVar8;
        byte[] aVar9;

        iVar4 = 0;
        msVar6 = new MemoryStream(faceStream);

        if (0 < faces)
        {
            using (BinaryReader reader = new BinaryReader(msVar6, Encoding.Default, true))
            {
                for (int i = 0; i < faces; i++)
                {
                    lVar7 = reader.BaseStream.Position;
                    reader.BaseStream.Seek(3, SeekOrigin.Current);
                    bVar1 = reader.ReadByte();

                    switch((uint)bVar1 >> 2 & 15)
                    {
                        case 4:
                        case 5:
                        case 7:
                            iVar4++;
                            break;
                        case 8:
                        case 9:
                        case 11:
                            iVar4 += 3;
                            break;
                        case 10:
                            reader.BaseStream.Seek(6, SeekOrigin.Current);
                            lVar7 += reader.ReadUInt16() * 8;
                            break;
                    }

                    reader.BaseStream.Seek(lVar7 + GameManager.DAT_854[(uint)bVar1 >> 2 & 15], SeekOrigin.Begin);
                }

                reader.BaseStream.Seek(0, SeekOrigin.Begin);
            }
        }

        aVar9 = new byte[iVar4 << 2];
        msVar4 = new MemoryStream(aVar9);
        DAT_14 = aVar9;

        if (0 < faces)
        {
            using (BinaryReader brVar6 = new BinaryReader(msVar6, Encoding.Default, true))
            {
                using (BinaryWriter bwVar4 = new BinaryWriter(msVar4, Encoding.Default, true))
                {
                    for (int i = 0; i < faces; i++)
                    {
                        lVar7 = brVar6.BaseStream.Position;

                        switch((uint)brVar6.ReadByte(3) >> 2 & 15)
                        {
                            case 4:
                                bwVar4.Write(3, (byte)(brVar6.ReadByte(3) & 3 | 0x20));
                                uVar3 = brVar6.ReadUInt16(4);
                                msVar5 = new MemoryStream(vertexStream);
                                uVar2 = brVar6.ReadUInt16(10);
                                lVar8 = bwVar4.BaseStream.Position + 4;
                                break;
                            case 5:
                                bwVar4.Write(3, (byte)(brVar6.ReadByte(3) & 3 | 0x24));
                                uVar3 = brVar6.ReadUInt16(4);
                                msVar5 = new MemoryStream(vertexStream);
                                uVar2 = brVar6.ReadUInt16(10);
                                lVar8 = bwVar4.BaseStream.Position + 4;
                                break;
                            default:
                                goto switchD_00003ae4_caseD_6;
                            case 7:
                                bwVar4.Write(3, (byte)(brVar6.ReadByte(3) & 3 | 0x24));
                                uVar3 = brVar6.ReadUInt16(4);
                                msVar5 = new MemoryStream(vertexStream);
                                uVar2 = brVar6.ReadUInt16(10);
                                lVar8 = bwVar4.BaseStream.Position + 4;
                                break;
                            case 8:
                                bVar1 = (byte)(brVar6.ReadByte(3) & 3 | 0x30);
                                goto LAB_00003bb8;
                            case 9:
                            case 11:
                                bVar1 = (byte)(brVar6.ReadByte(3) & 3 | 0x34);
                                LAB_00003bb8:
                                bwVar4.Write(11, bVar1);
                                bwVar4.Write(7, bVar1);
                                bwVar4.Write(3, bVar1);
                                msVar7 = new MemoryStream(vertexStream);
                                msVar7.Seek(brVar6.ReadUInt16(4), SeekOrigin.Begin);
                                msVar8 = new MemoryStream(normalStream);
                                msVar8.Seek(brVar6.ReadUInt16(10), SeekOrigin.Begin);
                                param1(msVar4, msVar6, msVar7, msVar8);
                                bwVar4.Seek(1, SeekOrigin.Current);
                                msVar7.Seek(brVar6.ReadUInt16(6), SeekOrigin.Begin);
                                msVar8.Seek(brVar6.ReadUInt16(12), SeekOrigin.Begin);
                                param1(msVar4, msVar6, msVar7, msVar8);
                                bwVar4.Seek(1, SeekOrigin.Current);
                                lVar8 = bwVar4.BaseStream.Position + 4;
                                uVar3 = brVar6.ReadUInt16(8);
                                msVar5 = new MemoryStream(vertexStream);
                                uVar2 = brVar6.ReadUInt16(14);
                                break;
                            case 10:
                                lVar7 += brVar6.ReadUInt16(10) * 8;
                                goto switchD_00003ae4_caseD_6;
                        }

                        msVar5.Seek(uVar3, SeekOrigin.Begin);
                        msVar7 = new MemoryStream(normalStream);
                        msVar7.Seek(uVar2, SeekOrigin.Begin);
                        param1(msVar4, msVar6, msVar5, msVar7);
                        bwVar4.BaseStream.Seek(lVar8, SeekOrigin.Begin);
                        switchD_00003ae4_caseD_6:
                        brVar6.BaseStream.Seek(lVar7 + GameManager.DAT_854[brVar6.ReadByte(3) >> 2 & 15], SeekOrigin.Begin);
                    }
                }
            }
        }
    }
}
