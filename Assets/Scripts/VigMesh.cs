using System.IO;
using System.Text;
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
    public Texture2D[] DAT_1C; //0x1C

    private VigMesh instance;
    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;
    private List<int> materialIDs;
    private List<Vector3> newVertices;
    private List<Vector2> newUVs;
    private List<Color32> newColors;
    private List<List<int>> newTriangles;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FUN_21F70(VigTransform param1)
    {
        byte bVar1;
        int ppuVar2;
        int iVar3;
        MemoryStream msVar3;
        BinaryReader brVar3;
        int iVar4;
        MemoryStream msVar4;
        BinaryReader brVar4;
        int puVar5;
        int iVar6;
        MemoryStream msVar6;
        BinaryReader brVar6;
        int puVar7;
        int iVar8;
        uint uVar9;
        int puVar10;
        int iVar11;
        int iVar12;
        MemoryStream msVar13;
        BinaryReader brVar13;
        int iVar14;
        int iVar15;
        int iVar16;
        uint uVar17;

        int tFactor = 1;
        
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
            Matrix3x3 gp00000f48 = LevelManager.instance.DAT_F48;
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
        msVar13 = new MemoryStream(faceStream);
        msVar3 = new MemoryStream(vertexStream);
        msVar4 = new MemoryStream(normalStream);
        msVar6 = new MemoryStream(DAT_14);

        using (brVar13 = new BinaryReader(msVar13, Encoding.Default, true))
        {
            using (brVar3 = new BinaryReader(msVar3, Encoding.Default, true))
            {
                using (brVar4 = new BinaryReader(msVar4, Encoding.Default, true))
                {
                    using (brVar6 = new BinaryReader(msVar6, Encoding.Default, true))
                    {
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

                                    if (Coprocessor.mathsAccumulator.mac0 > 0)
                                    {
                                        Color32 color = new Color32
                                            (brVar13.ReadByte(0), brVar13.ReadByte(1), brVar13.ReadByte(2), (byte)(brVar13.ReadByte(3) + 32));
                                        newColors.Add(color);
                                        newColors.Add(color);
                                        newColors.Add(color);
                                        newUVs.Add(new Vector2(0, 0));
                                        newUVs.Add(new Vector2(0, 0));
                                        newUVs.Add(new Vector2(0, 0));
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor);
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor);
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor);
                                        int index = newVertices.Count - 1;
                                        newTriangles[0].Add(index);
                                        newTriangles[0].Add(index - 1);
                                        newTriangles[0].Add(index - 2);
                                        brVar13.BaseStream.Seek(12, SeekOrigin.Current);
                                        iVar15 = 0x4000000;
                                        goto LAB_22F08;
                                    }
                                    else
                                    {
                                        brVar13.BaseStream.Seek(12, SeekOrigin.Current);
                                        break;
                                    }

                                case 0x80022280:
                                    Coprocessor.ExecuteNCLIP();

                                    if (Coprocessor.mathsAccumulator.mac0 > 0)
                                    {
                                        newColors.Add(new Color32
                                            (brVar13.ReadByte(0), brVar13.ReadByte(1), brVar13.ReadByte(2), (byte)(brVar13.ReadByte(3) + 48)));
                                        int materialID = brVar13.ReadByte(22) + 1;
                                        newUVs.Add(new Vector2
                                            ((float)brVar13.ReadByte(12) / (meshRenderer.materials[materialID].mainTexture.width - 1),
                                             1f - (float)brVar13.ReadByte(13) / (meshRenderer.materials[materialID].mainTexture.height - 1)));
                                        newUVs.Add(new Vector2
                                            ((float)brVar13.ReadByte(16) / (meshRenderer.materials[materialID].mainTexture.width - 1),
                                             1f - (float)brVar13.ReadByte(17) / (meshRenderer.materials[materialID].mainTexture.height - 1)));
                                        newUVs.Add(new Vector2
                                            ((float)brVar13.ReadByte(20) / (meshRenderer.materials[materialID].mainTexture.width - 1),
                                             1f - (float)brVar13.ReadByte(21) / (meshRenderer.materials[materialID].mainTexture.height - 1)));
                                        newColors.Add(new Color32
                                            (brVar13.ReadByte(24), brVar13.ReadByte(25), brVar13.ReadByte(26), brVar13.ReadByte(27)));
                                        newColors.Add(new Color32
                                            (brVar13.ReadByte(28), brVar13.ReadByte(29), brVar13.ReadByte(30), brVar13.ReadByte(31)));
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor);
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor);
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor);
                                        int index = newVertices.Count - 1;
                                        newTriangles[materialIDs.IndexOf(materialID)].Add(index);
                                        newTriangles[materialIDs.IndexOf(materialID)].Add(index - 1);
                                        newTriangles[materialIDs.IndexOf(materialID)].Add(index - 2);
                                        brVar13.BaseStream.Seek(40, SeekOrigin.Current);
                                        iVar15 = 0x9000000;
                                        goto LAB_22F08;
                                    }
                                    else
                                    {
                                        brVar13.BaseStream.Seek(40, SeekOrigin.Current);
                                        break;
                                    }

                                case 0x800222E4:
                                    Coprocessor.ExecuteNCLIP();

                                    if (Coprocessor.mathsAccumulator.mac0 > 0)
                                    {
                                        newColors.Add(new Color32
                                            (brVar13.ReadByte(0), brVar13.ReadByte(1), brVar13.ReadByte(2), (byte)(brVar13.ReadByte(3) + 40)));
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor);
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor);
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor);
                                        newColors.Add(new Color32
                                            (brVar13.ReadByte(12), brVar13.ReadByte(13), brVar13.ReadByte(14), brVar13.ReadByte(15)));
                                        newColors.Add(new Color32
                                            (brVar13.ReadByte(20), brVar13.ReadByte(21), brVar13.ReadByte(22), brVar13.ReadByte(23)));
                                        newUVs.Add(new Vector2(0, 0));
                                        newUVs.Add(new Vector2(0, 0));
                                        newUVs.Add(new Vector2(0, 0));
                                        int index = newVertices.Count - 1;
                                        newTriangles[0].Add(index);
                                        newTriangles[0].Add(index - 1);
                                        newTriangles[0].Add(index - 2);
                                        brVar13.BaseStream.Seek(20, SeekOrigin.Current);
                                        iVar15 = 0x6000000;
                                        goto LAB_22F08;
                                    }
                                    else
                                    {
                                        brVar13.BaseStream.Seek(20, SeekOrigin.Current);
                                        break;
                                    }

                                case 0x80022330:
                                    Coprocessor.ExecuteNCLIP();

                                    if (Coprocessor.mathsAccumulator.mac0 > 0)
                                    {
                                        //...
                                        brVar13.BaseStream.Seek(32, SeekOrigin.Current);
                                        iVar15 = 0x9000000;
                                        goto LAB_22F08;
                                    }
                                    else
                                    {
                                        brVar13.BaseStream.Seek(32, SeekOrigin.Current);
                                        break;
                                    }

                                case 0x800224A0:
                                    Coprocessor.ExecuteNCLIP();

                                    if (Coprocessor.mathsAccumulator.mac0 > 0)
                                    {
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor);
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor);
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor);
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
                                            (Coprocessor.colorFIFO.r2, Coprocessor.colorFIFO.g2, Coprocessor.colorFIFO.b2, Coprocessor.colorFIFO.cd2);
                                        newColors.Add(color);
                                        newColors.Add(color);
                                        newColors.Add(color);
                                        newUVs.Add(new Vector2(0, 0));
                                        newUVs.Add(new Vector2(0, 0));
                                        newUVs.Add(new Vector2(0, 0));
                                        int index = newVertices.Count - 1;
                                        newTriangles[0].Add(index);
                                        newTriangles[0].Add(index - 1);
                                        newTriangles[0].Add(index - 2);
                                        brVar13.BaseStream.Seek(12, SeekOrigin.Current);
                                        iVar15 = 0x4000000;
                                        goto LAB_22F08;
                                    }
                                    else
                                    {
                                        brVar13.BaseStream.Seek(12, SeekOrigin.Current);
                                        break;
                                    }

                                case 0x800225AC:
                                    Coprocessor.ExecuteNCLIP();

                                    if (Coprocessor.mathsAccumulator.mac0 > 0)
                                    {
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor);
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor);
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor);
                                        Coprocessor.colorCode.r = brVar13.ReadByte(0);
                                        Coprocessor.colorCode.g = brVar13.ReadByte(1);
                                        Coprocessor.colorCode.b = brVar13.ReadByte(2);
                                        Coprocessor.colorCode.code = (byte)(brVar13.ReadByte(3) + 16);
                                        puVar5 = brVar13.ReadInt16(10);
                                        Coprocessor.vector0.vx0 = brVar4.ReadInt16(puVar5);
                                        Coprocessor.vector0.vy0 = brVar4.ReadInt16(puVar5 + 2);
                                        Coprocessor.vector0.vz0 = brVar4.ReadInt16(puVar5 + 4);
                                        Coprocessor.ExecuteNCCS(12, true);
                                        int materialID = brVar13.ReadByte(22) + 1;
                                        newUVs.Add(new Vector2
                                            ((float)brVar13.ReadByte(12) / (meshRenderer.materials[materialID].mainTexture.width - 1),
                                             1f - (float)brVar13.ReadByte(13) / (meshRenderer.materials[materialID].mainTexture.height - 1)));
                                        newUVs.Add(new Vector2
                                            ((float)brVar13.ReadByte(16) / (meshRenderer.materials[materialID].mainTexture.width - 1),
                                             1f - (float)brVar13.ReadByte(17) / (meshRenderer.materials[materialID].mainTexture.height - 1)));
                                        newUVs.Add(new Vector2
                                            ((float)brVar13.ReadByte(20) / (meshRenderer.materials[materialID].mainTexture.width - 1),
                                             1f - (float)brVar13.ReadByte(21) / (meshRenderer.materials[materialID].mainTexture.height - 1)));
                                        Color32 color = new Color32
                                            (Coprocessor.colorFIFO.r2, Coprocessor.colorFIFO.g2, Coprocessor.colorFIFO.b2, Coprocessor.colorFIFO.cd2);
                                        newColors.Add(color);
                                        newColors.Add(color);
                                        newColors.Add(color);
                                        int index = newVertices.Count - 1;
                                        newTriangles[materialIDs.IndexOf(materialID)].Add(index);
                                        newTriangles[materialIDs.IndexOf(materialID)].Add(index - 1);
                                        newTriangles[materialIDs.IndexOf(materialID)].Add(index - 2);
                                        brVar13.BaseStream.Seek(24, SeekOrigin.Current);
                                        iVar15 = 0x7000000;
                                        goto LAB_22F08;
                                    }
                                    else
                                    {
                                        brVar13.BaseStream.Seek(24, SeekOrigin.Current);
                                        break;
                                    }

                                case 0x8002260C:
                                    //...
                                    brVar13.BaseStream.Seek(12, SeekOrigin.Current);
                                    break;

                                case 0x80022674:
                                    Coprocessor.ExecuteNCLIP();

                                    if (Coprocessor.mathsAccumulator.mac0 > 0)
                                    {
                                        //...
                                        brVar13.BaseStream.Seek(24, SeekOrigin.Current);
                                        iVar15 = 0x7000000;
                                        goto LAB_22F08;
                                    }
                                    else
                                    {
                                        brVar13.BaseStream.Seek(24, SeekOrigin.Current);
                                        break;
                                    }

                                case 0x80022870:
                                    Coprocessor.ExecuteNCLIP();

                                    if (Coprocessor.mathsAccumulator.mac0 > 0)
                                    {
                                        Coprocessor.ExecuteAVSZ3();
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor);
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor);
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor);
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
                                        newColors.Add(new Color32
                                            (Coprocessor.colorFIFO.r0, Coprocessor.colorFIFO.g0, Coprocessor.colorFIFO.b0, Coprocessor.colorFIFO.cd0));
                                        newColors.Add(new Color32
                                            (Coprocessor.colorFIFO.r1, Coprocessor.colorFIFO.g1, Coprocessor.colorFIFO.b1, Coprocessor.colorFIFO.cd1));
                                        newColors.Add(new Color32
                                            (Coprocessor.colorFIFO.r2, Coprocessor.colorFIFO.g2, Coprocessor.colorFIFO.b2, Coprocessor.colorFIFO.cd2));
                                        newUVs.Add(new Vector2(0, 0));
                                        newUVs.Add(new Vector2(0, 0));
                                        newUVs.Add(new Vector2(0, 0));
                                        int index = newVertices.Count - 1;
                                        newTriangles[0].Add(index);
                                        newTriangles[0].Add(index - 1);
                                        newTriangles[0].Add(index - 2);
                                        brVar13.BaseStream.Seek(16, SeekOrigin.Current);
                                        iVar15 = 0x6000000;
                                        break;
                                    }
                                    else
                                    {
                                        brVar13.BaseStream.Seek(16, SeekOrigin.Current);
                                        break;
                                    }

                                case 0x80022A4C:
                                    Coprocessor.ExecuteNCLIP();

                                    if (Coprocessor.mathsAccumulator.mac0 > 0)
                                    {
                                        Coprocessor.ExecuteAVSZ3();
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor);
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor);
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor);
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
                                        int materialID = brVar13.ReadByte(26) + 1;
                                        newUVs.Add(new Vector2
                                            ((float)brVar13.ReadByte(16) / (meshRenderer.materials[materialID].mainTexture.width - 1),
                                             1f - (float)brVar13.ReadByte(17) / (meshRenderer.materials[materialID].mainTexture.height - 1)));
                                        newUVs.Add(new Vector2
                                            ((float)brVar13.ReadByte(20) / (meshRenderer.materials[materialID].mainTexture.width - 1),
                                             1f - (float)brVar13.ReadByte(21) / (meshRenderer.materials[materialID].mainTexture.height - 1)));
                                        newUVs.Add(new Vector2
                                            ((float)brVar13.ReadByte(24) / (meshRenderer.materials[materialID].mainTexture.width - 1),
                                             1f - (float)brVar13.ReadByte(25) / (meshRenderer.materials[materialID].mainTexture.height - 1)));
                                        newColors.Add(new Color32
                                            (Coprocessor.colorFIFO.r0, Coprocessor.colorFIFO.g0, Coprocessor.colorFIFO.b0, Coprocessor.colorFIFO.cd0));
                                        newColors.Add(new Color32
                                            (Coprocessor.colorFIFO.r1, Coprocessor.colorFIFO.g1, Coprocessor.colorFIFO.b1, Coprocessor.colorFIFO.cd1));
                                        newColors.Add(new Color32
                                            (Coprocessor.colorFIFO.r2, Coprocessor.colorFIFO.g2, Coprocessor.colorFIFO.b2, Coprocessor.colorFIFO.cd2));
                                        int index = newVertices.Count - 1;
                                        newTriangles[materialIDs.IndexOf(materialID)].Add(index);
                                        newTriangles[materialIDs.IndexOf(materialID)].Add(index - 1);
                                        newTriangles[materialIDs.IndexOf(materialID)].Add(index - 2);
                                        brVar13.BaseStream.Seek(28, SeekOrigin.Current);
                                        iVar15 = 0x9000000;
                                        break;
                                    }
                                    else
                                    {
                                        brVar13.BaseStream.Seek(28, SeekOrigin.Current);
                                        break;
                                    }

                                case 0x80022B04:
                                    //...
                                    brVar13.BaseStream.Seek(12, SeekOrigin.Current);
                                    break;

                                case 0x80022B7C:
                                    Coprocessor.ExecuteNCLIP();

                                    if (Coprocessor.mathsAccumulator.mac0 > 0)
                                    {
                                        //...
                                        brVar13.BaseStream.Seek(28, SeekOrigin.Current);
                                        iVar15 = 0x9000000;
                                        goto LAB_22F08;
                                    }
                                    else
                                    {
                                        brVar13.BaseStream.Seek(28, SeekOrigin.Current);
                                        break;
                                    }

                                case 0x80022C54:
                                    Coprocessor.ExecuteNCLIP();

                                    if (Coprocessor.mathsAccumulator.mac0 > 0)
                                    {
                                        //...
                                        brVar13.BaseStream.Seek(24, SeekOrigin.Current);
                                        iVar15 = 0x7000000;
                                        goto LAB_22F08;
                                    }
                                    else
                                    {
                                        brVar13.BaseStream.Seek(24, SeekOrigin.Current);
                                        break;
                                    }

                                case 0x80022E2C:
                                    Coprocessor.ExecuteNCLIP();

                                    if (Coprocessor.mathsAccumulator.mac0 > 0)
                                    {
                                        //...
                                        brVar13.BaseStream.Seek(24, SeekOrigin.Current);
                                        iVar15 = 0x7000000;
                                        goto LAB_22F08;
                                    }
                                    else
                                    {
                                        brVar13.BaseStream.Seek(24, SeekOrigin.Current);
                                        break;
                                    }

                                case 0x80022FBC:
                                    return;

                                case 0x80022E78:
                                    Coprocessor.ExecuteNCLIP();

                                    if (Coprocessor.mathsAccumulator.mac0 > 0)
                                    {
                                        //...
                                        brVar13.BaseStream.Seek(24, SeekOrigin.Current);
                                        iVar15 = 0x7000000;
                                        goto LAB_22F08;
                                    }
                                    else
                                    {
                                        brVar13.BaseStream.Seek(24, SeekOrigin.Current);
                                        break;
                                    }

                                case 0x800221CC:
                                    Coprocessor.ExecuteNCLIP();

                                    if (Coprocessor.mathsAccumulator.mac0 > 0)
                                    {
                                        //...
                                        Color32 color = new Color32
                                            (brVar13.ReadByte(0), brVar13.ReadByte(1), brVar13.ReadByte(2), (byte)(brVar13.ReadByte(3) + 32));
                                        newColors.Add(color);
                                        newColors.Add(color);
                                        newColors.Add(color);
                                        newUVs.Add(new Vector2(0, 0));
                                        newUVs.Add(new Vector2(0, 0));
                                        newUVs.Add(new Vector2(0, 0));
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor);
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor);
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor);
                                        int index = newVertices.Count - 1;
                                        newTriangles[0].Add(index);
                                        newTriangles[0].Add(index - 1);
                                        newTriangles[0].Add(index - 2);
                                        brVar13.BaseStream.Seek(12, SeekOrigin.Current);
                                        iVar15 = 0x4000000;
                                        goto LAB_22F08;
                                    }
                                    else
                                    {
                                        brVar13.BaseStream.Seek(12, SeekOrigin.Current);
                                        break;
                                    }

                                case 0x800223F0:
                                    //...
                                    brVar13.BaseStream.Seek(12, SeekOrigin.Current);
                                    break;

                                case 0x80022504:
                                    //...
                                    brVar13.BaseStream.Seek(24, SeekOrigin.Current);
                                    break;

                                case 0x800227AC:
                                    //...
                                    goto case 0x80022870;

                                case 0x800229A0:
                                    //...
                                    goto case 0x80022A4C;

                                case 0x80022DA8:
                                    Coprocessor.ExecuteNCLIP();

                                    if (Coprocessor.mathsAccumulator.mac0 > 0)
                                    {
                                        //...
                                        Color32 color = new Color32
                                            (brVar13.ReadByte(0), brVar13.ReadByte(1), brVar13.ReadByte(2), (byte)(brVar13.ReadByte(3) + 240));
                                        newColors.Add(color);
                                        newColors.Add(color);
                                        newColors.Add(color);
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor);
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor);
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor);
                                        int materialID = brVar13.ReadByte(22) + 1;
                                        newUVs.Add(new Vector2
                                            ((float)brVar13.ReadByte(12) / (meshRenderer.materials[materialID].mainTexture.width - 1),
                                             1f - (float)brVar13.ReadByte(13) / (meshRenderer.materials[materialID].mainTexture.height - 1)));
                                        newUVs.Add(new Vector2
                                            ((float)brVar13.ReadByte(16) / (meshRenderer.materials[materialID].mainTexture.width - 1),
                                             1f - (float)brVar13.ReadByte(17) / (meshRenderer.materials[materialID].mainTexture.height - 1)));
                                        newUVs.Add(new Vector2
                                            ((float)brVar13.ReadByte(20) / (meshRenderer.materials[materialID].mainTexture.width - 1),
                                             1f - (float)brVar13.ReadByte(21) / (meshRenderer.materials[materialID].mainTexture.height - 1)));
                                        int index = newVertices.Count - 1;
                                        newTriangles[materialIDs.IndexOf(materialID)].Add(index);
                                        newTriangles[materialIDs.IndexOf(materialID)].Add(index - 1);
                                        newTriangles[materialIDs.IndexOf(materialID)].Add(index - 2);
                                        brVar13.BaseStream.Seek(24, SeekOrigin.Current);
                                        iVar15 = 0x7000000;
                                        goto LAB_22F08;
                                    }
                                    else
                                    {
                                        brVar13.BaseStream.Seek(24, SeekOrigin.Current);
                                        break;
                                    }

                                case 0x8002246C:
                                    Coprocessor.ExecuteNCLIP();

                                    if (Coprocessor.mathsAccumulator.mac0 > 0)
                                    {
                                        Color32 color = new Color32
                                            (brVar6.ReadByte(), brVar6.ReadByte(), brVar6.ReadByte(), brVar6.ReadByte());
                                        newColors.Add(color);
                                        newColors.Add(color);
                                        newColors.Add(color);
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor);
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor);
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor);
                                        newUVs.Add(new Vector2(0, 0));
                                        newUVs.Add(new Vector2(0, 0));
                                        newUVs.Add(new Vector2(0, 0));
                                        int index = newVertices.Count - 1;
                                        newTriangles[0].Add(index);
                                        newTriangles[0].Add(index - 1);
                                        newTriangles[0].Add(index - 2);
                                        brVar13.BaseStream.Seek(12, SeekOrigin.Current);
                                        iVar15 = 0x4000000;
                                        goto LAB_22F08;
                                    }
                                    else
                                    {
                                        brVar13.BaseStream.Seek(12, SeekOrigin.Current);
                                        brVar6.BaseStream.Seek(4, SeekOrigin.Current);
                                        break;
                                    }

                                case 0x80022580:
                                    Coprocessor.ExecuteNCLIP();

                                    if (Coprocessor.mathsAccumulator.mac0 > 0)
                                    {
                                        Color32 color = new Color32
                                            (brVar6.ReadByte(), brVar6.ReadByte(), brVar6.ReadByte(), brVar6.ReadByte());
                                        newColors.Add(color);
                                        newColors.Add(color);
                                        newColors.Add(color);
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor);
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor);
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor);
                                        int materialID = brVar13.ReadByte(22) + 1;
                                        newUVs.Add(new Vector2
                                            ((float)brVar13.ReadByte(12) / (meshRenderer.materials[materialID].mainTexture.width - 1),
                                             1f - (float)brVar13.ReadByte(13) / (meshRenderer.materials[materialID].mainTexture.height - 1)));
                                        newUVs.Add(new Vector2
                                            ((float)brVar13.ReadByte(16) / (meshRenderer.materials[materialID].mainTexture.width - 1),
                                             1f - (float)brVar13.ReadByte(17) / (meshRenderer.materials[materialID].mainTexture.height - 1)));
                                        newUVs.Add(new Vector2
                                            ((float)brVar13.ReadByte(20) / (meshRenderer.materials[materialID].mainTexture.width - 1),
                                             1f - (float)brVar13.ReadByte(21) / (meshRenderer.materials[materialID].mainTexture.height - 1)));
                                        int index = newVertices.Count - 1;
                                        newTriangles[materialIDs.IndexOf(materialID)].Add(index);
                                        newTriangles[materialIDs.IndexOf(materialID)].Add(index - 1);
                                        newTriangles[materialIDs.IndexOf(materialID)].Add(index - 2);
                                        brVar13.BaseStream.Seek(24, SeekOrigin.Current);
                                        iVar15 = 0x7000000;
                                        goto LAB_22F08;
                                    }
                                    else
                                    {
                                        brVar13.BaseStream.Seek(24, SeekOrigin.Current);
                                        brVar6.BaseStream.Seek(4, SeekOrigin.Current);
                                        break;
                                    }

                                case 0x80022640:
                                    Coprocessor.ExecuteNCLIP();

                                    if (Coprocessor.mathsAccumulator.mac0 > 0)
                                    {
                                        Color32 color = new Color32
                                            (brVar6.ReadByte(), brVar6.ReadByte(), brVar6.ReadByte(), brVar6.ReadByte());
                                        //...
                                        newUVs.Add(new Vector2(0, 0)); //tmp
                                        newUVs.Add(new Vector2(0, 0)); //tmp
                                        newUVs.Add(new Vector2(0, 0)); //tmp
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor);
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor);
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor);
                                        newColors.Add(color);
                                        newColors.Add(color);
                                        newColors.Add(color);
                                        int index = newVertices.Count - 1;
                                        newTriangles[0].Add(index); //tmp
                                        newTriangles[0].Add(index - 1); //tmp
                                        newTriangles[0].Add(index - 2); //tmp
                                        brVar13.BaseStream.Seek(24, SeekOrigin.Current);
                                        iVar15 = 0x7000000;
                                        goto LAB_22F08;
                                    }
                                    else
                                    {
                                        brVar13.BaseStream.Seek(24, SeekOrigin.Current);
                                        brVar6.BaseStream.Seek(4, SeekOrigin.Current);
                                        break;
                                    }

                                case 0x80022828:
                                    Coprocessor.ExecuteNCLIP();

                                    if (Coprocessor.mathsAccumulator.mac0 > 0)
                                    {
                                        newColors.Add(new Color32
                                            (brVar6.ReadByte(), brVar6.ReadByte(), brVar6.ReadByte(), brVar6.ReadByte()));
                                        newColors.Add(new Color32
                                            (brVar6.ReadByte(), brVar6.ReadByte(), brVar6.ReadByte(), brVar6.ReadByte()));
                                        newColors.Add(new Color32
                                            (brVar6.ReadByte(), brVar6.ReadByte(), brVar6.ReadByte(), brVar6.ReadByte()));
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor);
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor);
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor);
                                        newUVs.Add(new Vector2(0, 0));
                                        newUVs.Add(new Vector2(0, 0));
                                        newUVs.Add(new Vector2(0, 0));
                                        int index = newVertices.Count - 1;
                                        newTriangles[0].Add(index);
                                        newTriangles[0].Add(index - 1);
                                        newTriangles[0].Add(index - 2);
                                        brVar13.BaseStream.Seek(16, SeekOrigin.Current);
                                        iVar15 = 0x6000000;
                                        goto LAB_22F08;
                                    }
                                    else
                                    {
                                        brVar13.BaseStream.Seek(16, SeekOrigin.Current);
                                        brVar6.BaseStream.Seek(12, SeekOrigin.Current);
                                        break;
                                    }

                                case 0x80022A1C:
                                    Coprocessor.ExecuteNCLIP();

                                    if (Coprocessor.mathsAccumulator.mac0 > 0)
                                    {
                                        newColors.Add(new Color32
                                            (brVar6.ReadByte(), brVar6.ReadByte(), brVar6.ReadByte(), brVar6.ReadByte()));
                                        newColors.Add(new Color32
                                            (brVar6.ReadByte(), brVar6.ReadByte(), brVar6.ReadByte(), brVar6.ReadByte()));
                                        newColors.Add(new Color32
                                            (brVar6.ReadByte(), brVar6.ReadByte(), brVar6.ReadByte(), brVar6.ReadByte()));
                                        Coprocessor.ExecuteAVSZ3();
                                        uVar17 = (uint)Coprocessor.averageZ >> iVar16;
                                        int materialID = brVar13.ReadByte(26) + 1;
                                        newUVs.Add(new Vector2
                                            ((float)brVar13.ReadByte(16) / (meshRenderer.materials[materialID].mainTexture.width - 1),
                                             1f - (float)brVar13.ReadByte(17) / (meshRenderer.materials[materialID].mainTexture.height - 1)));
                                        newUVs.Add(new Vector2
                                            ((float)brVar13.ReadByte(20) / (meshRenderer.materials[materialID].mainTexture.width - 1),
                                             1f - (float)brVar13.ReadByte(21) / (meshRenderer.materials[materialID].mainTexture.height - 1)));
                                        newUVs.Add(new Vector2
                                            ((float)brVar13.ReadByte(24) / (meshRenderer.materials[materialID].mainTexture.width - 1),
                                             1f - (float)brVar13.ReadByte(25) / (meshRenderer.materials[materialID].mainTexture.height - 1)));
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor);
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor);
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor);
                                        int index = newVertices.Count - 1;
                                        newTriangles[materialIDs.IndexOf(materialID)].Add(index);
                                        newTriangles[materialIDs.IndexOf(materialID)].Add(index - 1);
                                        newTriangles[materialIDs.IndexOf(materialID)].Add(index - 2);
                                        brVar13.BaseStream.Seek(28, SeekOrigin.Current);
                                        iVar15 = 0x9000000;
                                        break;
                                    }
                                    else
                                    {
                                        brVar13.BaseStream.Seek(28, SeekOrigin.Current);
                                        brVar6.BaseStream.Seek(12, SeekOrigin.Current);
                                        break;
                                    }

                                case 0x80022B44:
                                    Coprocessor.ExecuteNCLIP();

                                    if (Coprocessor.mathsAccumulator.mac0 > 0)
                                    {
                                        //...
                                        brVar13.BaseStream.Seek(28, SeekOrigin.Current);
                                        brVar6.BaseStream.Seek(12, SeekOrigin.Current);
                                        iVar15 = 0x9000000;
                                        goto LAB_22F08;
                                    }
                                    else
                                    {
                                        brVar13.BaseStream.Seek(28, SeekOrigin.Current);
                                        brVar6.BaseStream.Seek(12, SeekOrigin.Current);
                                        break;
                                    }

                                case 0x800223D4:
                                    Coprocessor.ExecuteNCLIP();

                                    if (Coprocessor.mathsAccumulator.mac0 > 0)
                                    {
                                        //...
                                        Color32 color = new Color32
                                            (brVar6.ReadByte(), brVar6.ReadByte(), brVar6.ReadByte(), brVar6.ReadByte());
                                        newColors.Add(color);
                                        newColors.Add(color);
                                        newColors.Add(color);
                                        newUVs.Add(new Vector2(0, 0));
                                        newUVs.Add(new Vector2(0, 0));
                                        newUVs.Add(new Vector2(0, 0));
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor);
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor);
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor);
                                        int index = newVertices.Count - 1;
                                        newTriangles[0].Add(index);
                                        newTriangles[0].Add(index - 1);
                                        newTriangles[0].Add(index - 2);
                                        brVar13.BaseStream.Seek(12, SeekOrigin.Current);
                                        iVar15 = 0x4000000;
                                        goto LAB_22F08;
                                    }
                                    else
                                    {
                                        brVar13.BaseStream.Seek(12, SeekOrigin.Current);
                                        brVar6.BaseStream.Seek(4, SeekOrigin.Current);
                                        break;
                                    }

                                case 0x800224E8:
                                    Coprocessor.ExecuteNCLIP();

                                    if (Coprocessor.mathsAccumulator.mac0 > 0)
                                    {
                                        //...
                                        Color32 color = new Color32
                                            (brVar6.ReadByte(), brVar6.ReadByte(), brVar6.ReadByte(), brVar6.ReadByte());
                                        newColors.Add(color);
                                        newColors.Add(color);
                                        newColors.Add(color);
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor);
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor);
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor);
                                        int materialID = brVar13.ReadByte(22) + 1;
                                        newUVs.Add(new Vector2
                                            ((float)brVar13.ReadByte(12) / (meshRenderer.materials[materialID].mainTexture.width - 1),
                                             1f - (float)brVar13.ReadByte(13) / (meshRenderer.materials[materialID].mainTexture.height - 1)));
                                        newUVs.Add(new Vector2
                                            ((float)brVar13.ReadByte(16) / (meshRenderer.materials[materialID].mainTexture.width - 1),
                                             1f - (float)brVar13.ReadByte(17) / (meshRenderer.materials[materialID].mainTexture.height - 1)));
                                        newUVs.Add(new Vector2
                                            ((float)brVar13.ReadByte(20) / (meshRenderer.materials[materialID].mainTexture.width - 1),
                                             1f - (float)brVar13.ReadByte(21) / (meshRenderer.materials[materialID].mainTexture.height - 1)));
                                        int index = newVertices.Count - 1;
                                        newTriangles[materialIDs.IndexOf(materialID)].Add(index);
                                        newTriangles[materialIDs.IndexOf(materialID)].Add(index - 1);
                                        newTriangles[materialIDs.IndexOf(materialID)].Add(index - 2);
                                        brVar13.BaseStream.Seek(24, SeekOrigin.Current);
                                        iVar15 = 0x7000000;
                                        goto LAB_22F08;
                                    }
                                    else
                                    {
                                        brVar13.BaseStream.Seek(24, SeekOrigin.Current);
                                        brVar6.BaseStream.Seek(4, SeekOrigin.Current);
                                        break;
                                    }

                                case 0x8002271C:
                                    Coprocessor.ExecuteNCLIP();

                                    if (Coprocessor.mathsAccumulator.mac0 > 0)
                                    {
                                        //...
                                        newColors.Add(new Color32
                                            (brVar6.ReadByte(), brVar6.ReadByte(), brVar6.ReadByte(), brVar6.ReadByte()));
                                        newColors.Add(new Color32
                                            (brVar6.ReadByte(), brVar6.ReadByte(), brVar6.ReadByte(), brVar6.ReadByte()));
                                        newColors.Add(new Color32
                                            (brVar6.ReadByte(), brVar6.ReadByte(), brVar6.ReadByte(), brVar6.ReadByte()));
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor);
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor);
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor);
                                        newUVs.Add(new Vector2(0, 0));
                                        newUVs.Add(new Vector2(0, 0));
                                        newUVs.Add(new Vector2(0, 0));
                                        int index = newVertices.Count - 1;
                                        newTriangles[0].Add(index);
                                        newTriangles[0].Add(index - 1);
                                        newTriangles[0].Add(index - 2);
                                        brVar13.BaseStream.Seek(16, SeekOrigin.Current);
                                        iVar15 = 0x6000000;
                                        goto LAB_22F08;
                                    }
                                    else
                                    {
                                        brVar13.BaseStream.Seek(16, SeekOrigin.Current);
                                        brVar6.BaseStream.Seek(12, SeekOrigin.Current);
                                        break;
                                    }

                                case 0x80022910:
                                    Coprocessor.ExecuteNCLIP();

                                    if (Coprocessor.mathsAccumulator.mac0 > 0)
                                    {
                                        //...
                                        newColors.Add(new Color32
                                            (brVar6.ReadByte(), brVar6.ReadByte(), brVar6.ReadByte(), brVar6.ReadByte()));
                                        newColors.Add(new Color32
                                            (brVar6.ReadByte(), brVar6.ReadByte(), brVar6.ReadByte(), brVar6.ReadByte()));
                                        newColors.Add(new Color32
                                            (brVar6.ReadByte(), brVar6.ReadByte(), brVar6.ReadByte(), brVar6.ReadByte()));
                                        int materialID = brVar13.ReadByte(26) + 1;
                                        newUVs.Add(new Vector2
                                            ((float)brVar13.ReadByte(16) / (meshRenderer.materials[materialID].mainTexture.width - 1),
                                             1f - (float)brVar13.ReadByte(17) / (meshRenderer.materials[materialID].mainTexture.height - 1)));
                                        newUVs.Add(new Vector2
                                            ((float)brVar13.ReadByte(20) / (meshRenderer.materials[materialID].mainTexture.width - 1),
                                             1f - (float)brVar13.ReadByte(21) / (meshRenderer.materials[materialID].mainTexture.height - 1)));
                                        newUVs.Add(new Vector2
                                            ((float)brVar13.ReadByte(24) / (meshRenderer.materials[materialID].mainTexture.width - 1),
                                             1f - (float)brVar13.ReadByte(25) / (meshRenderer.materials[materialID].mainTexture.height - 1)));
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector0.vx0, Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor);
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector1.vx1, Coprocessor.vector1.vy1, Coprocessor.vector1.vz1) / tFactor);
                                        newVertices.Add(new Vector3
                                            (Coprocessor.vector2.vx2, Coprocessor.vector2.vy2, Coprocessor.vector2.vz2) / tFactor);
                                        int index = newVertices.Count - 1;
                                        newTriangles[materialIDs.IndexOf(materialID)].Add(index);
                                        newTriangles[materialIDs.IndexOf(materialID)].Add(index - 1);
                                        newTriangles[materialIDs.IndexOf(materialID)].Add(index - 2);
                                        brVar13.BaseStream.Seek(28, SeekOrigin.Current);
                                        iVar15 = 0x9000000;
                                        break;
                                    }
                                    else
                                    {
                                        brVar13.BaseStream.Seek(28, SeekOrigin.Current);
                                        brVar6.BaseStream.Seek(12, SeekOrigin.Current);
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
                            else
                                return;
                        }
                    }
                }
            }
        }
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

        msVar4 = new MemoryStream(new byte[iVar4 << 2]);
        DAT_14 = msVar4.GetBuffer();

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
                                bwVar4.Write((byte)(brVar6.ReadByte(3) & 3 | 0x20), 3);
                                uVar3 = brVar6.ReadUInt16(4);
                                msVar5 = new MemoryStream(vertexStream);
                                uVar2 = brVar6.ReadUInt16(10);
                                lVar8 = bwVar4.BaseStream.Position + 4;
                                break;
                            case 5:
                                bwVar4.Write((byte)(brVar6.ReadByte(3) & 3 | 0x24), 3);
                                uVar3 = brVar6.ReadUInt16(4);
                                msVar5 = new MemoryStream(vertexStream);
                                uVar2 = brVar6.ReadUInt16(10);
                                lVar8 = bwVar4.BaseStream.Position + 4;
                                break;
                            default:
                                goto switchD_00003ae4_caseD_6;
                            case 7:
                                bwVar4.Write((byte)(brVar6.ReadByte(3) & 3 | 0x24), 3);
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
                                bwVar4.Write(bVar1, 11);
                                bwVar4.Write(bVar1, 7);
                                bwVar4.Write(bVar1, 3);
                                msVar7 = new MemoryStream(vertexStream);
                                msVar7.Seek(brVar6.ReadUInt16(4), SeekOrigin.Begin);
                                msVar8 = new MemoryStream(normalStream);
                                msVar8.Seek(brVar6.ReadUInt16(10), SeekOrigin.Begin);
                                param1(msVar4, msVar6, msVar7, msVar8);
                                bwVar4.BaseStream.Seek(4, SeekOrigin.Current);
                                msVar7.Seek(brVar6.ReadUInt16(6), SeekOrigin.Begin);
                                msVar8.Seek(brVar6.ReadUInt16(12), SeekOrigin.Begin);
                                param1(msVar4, msVar6, msVar7, msVar8);
                                bwVar4.BaseStream.Seek(-4, SeekOrigin.Current);
                                lVar8 = bwVar4.BaseStream.Position + 12;
                                uVar3 = brVar6.ReadUInt16(8);
                                msVar5 = new MemoryStream(vertexStream);
                                uVar2 = brVar6.ReadUInt16(14);
                                bwVar4.BaseStream.Seek(8, SeekOrigin.Current);
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
