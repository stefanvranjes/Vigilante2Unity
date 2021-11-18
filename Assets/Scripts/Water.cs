using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Primitive
{
    public Vector3 verts;
    public Vector2Int screen;
    public Vector2Int uvs;
}

public class Water : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = mesh;
            int primCount = 2400;
            newVertices = new Vector3[primCount];
            newColors = new Color32[primCount];
            newUVs = new Vector2[primCount];
            newIndices = new int[primCount];
            primitives = new Primitive[primCount];
            DAT_B5D70 = new byte[1024];
            DAT_B5570 = new short[1024];

            for (int i = 0; i < primCount; i += 6)
            {
                newColors[i] = new Color32(0x80, 0x80, 0x80, 0xff);
                newColors[i + 1] = new Color32(0x80, 0x80, 0x80, 0xff);
                newColors[i + 2] = new Color32(0x80, 0x80, 0x80, 0xff);
                newColors[i + 3] = new Color32(0x80, 0x80, 0x80, 0xff);
                newColors[i + 4] = new Color32(0x80, 0x80, 0x80, 0xff);
                newColors[i + 5] = new Color32(0x80, 0x80, 0x80, 0xff);
                newIndices[i] = i;
                newIndices[i + 1] = i + 1;
                newIndices[i + 2] = i + 2;
                newIndices[i + 3] = i + 3;
                newIndices[i + 4] = i + 4;
                newIndices[i + 5] = i + 5;
            }

            LOD = new Mesh();
            lod.GetComponent<MeshFilter>().mesh = LOD;
            int primCount2 = width * height * 4;
            newLODVertices = new Vector3[primCount2];
            newLODColors = new Color32[primCount2];
            newLODUVs = new Vector2[primCount2];
            newLODIndices = new int[primCount2 * 2];
            Color32 colorLOD = LevelManager.instance.DAT_DE0;
            colorLOD.a = 128;

            for (int i = 0; i < primCount2; i += 4)
            {
                newLODColors[i] = colorLOD;
                newLODColors[i + 1] = colorLOD;
                newLODColors[i + 2] = colorLOD;
                newLODColors[i + 3] = colorLOD;
                newLODUVs[i] = new Vector2(0, 0);
                newLODUVs[i + 1] = new Vector2(0, 0);
                newLODUVs[i + 2] = new Vector2(0, 0);
                newLODUVs[i + 3] = new Vector2(0, 0);
            }

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int index = x + y * height;
                    int _x = x < width / 2 ? -x - 1 : x - width / 2;
                    newLODVertices[index * 4] = new Vector3(cellSize * _x, 0, cellSize * y);
                    newLODVertices[index * 4 + 1] = new Vector3(cellSize * _x + cellSize, 0, cellSize * y);
                    newLODVertices[index * 4 + 2] = new Vector3(cellSize * _x, 0, cellSize * y + cellSize);
                    newLODVertices[index * 4 + 3] = new Vector3(cellSize * _x + cellSize, 0, cellSize * y + cellSize);
                }
            }

            int count = 0;

            for (int i = 0, j = 0; j < primCount2; i += 6, j += 4)
            {
                newLODIndices[i] = j + 2;
                newLODIndices[i + 1] = j + 1;
                newLODIndices[i + 2] = j;
                newLODIndices[i + 3] = j + 1;
                newLODIndices[i + 4] = j + 2;
                newLODIndices[i + 5] = j + 3;
                count = i + 6;
            }

            LOD.SetVertices(newLODVertices, 0, primCount2);
            LOD.SetColors(newLODColors, 0, primCount2);
            LOD.SetUVs(0, newLODUVs, 0, primCount2);
            LOD.SetIndices(newLODIndices, 0, count, MeshTopology.Triangles, 0);
        }

        //lod.parent = null;

        terrain = FindObjectOfType<VigTerrain>();
        mainT = (Texture2D)FindObjectOfType<LevelManager>().DAT_DC0.mainTexture;
        lod2 = Instantiate(lod.gameObject, lod).transform;
        lod2.localEulerAngles = new Vector3(0, 180f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        /*transform.position = new Vector3(transform.position.x,
            -GameManager.instance.DAT_DB0 / GameManager.instance.translateFactor,
            transform.position.z);
        transform.rotation = Quaternion.identity;*/
        //transform.localRotation = Quaternion.identity;
        //transform.rotation = transform.localRotation;
    }

    public static Water instance;
    public Transform lod;
    public Vector3 lodOffset;
    public bool topView;
    public int width;
    public int height;
    public int cellSize;
    public short[] DAT_B5570; //0x800B5570
    public byte[] DAT_B5D70; //0x800B5D70

    private Mesh mesh;
    private Mesh LOD;
    private Vector3[] newVertices;
    private Vector2[] newUVs;
    private Color32[] newColors;
    private int[] newIndices;
    private Vector3[] newLODVertices;
    private Vector2[] newLODUVs;
    private Color32[] newLODColors;
    private int[] newLODIndices;
    private VigTerrain terrain;
    private Primitive[] primitives;
    private Texture2D mainT;
    private float lod_y;
    private Vector3 masterPosition;
    private Quaternion masterRotation;
    private Vector3 masterScale;
    private Transform lod2;

    public void UpdatePosition(Vector3 pos)
    {
        Vector3 wantedPosition = new Vector3(pos.x, -pos.y, pos.z);
        Vector3 relativePosition = masterRotation * Vector3.Scale(wantedPosition, masterScale) + masterPosition;
        transform.position = relativePosition;
        //transform.LookAt(masterObject);
        //transform.localPosition = new Vector3(pos.x, -pos.y, pos.z);
        //transform.localPosition = new Vector3(pos.x, -pos.y, pos.z);
        //transform.localRotation = Quaternion.identity;
        //transform.rotation = transform.localRotation;
    }

    public void FUN_15F28(VigTransform param1, int param2)
    {
        short sVar1;
        ushort uVar2;
        Color32 cVar3;
        short sVar4;
        short sVar5;
        short sVar6;
        short sVar7;
        short sVar8;
        int iVar9;
        uint uVar10;
        uint uVar11;
        int iVar12;
        int iVar13;
        short sVar14;
        uint uVar15;
        uint uVar16;
        Color32 cVar16;
        int iVar17;
        short sVar18;
        uint uVar19;
        int psVar20;
        int iVar21;
        int iVar22;
        short sVar23;
        int iVar24;
        uint uVar25;
        int iVar26;
        int iVar27;
        int iVar28;
        int iVar29;
        int iVar30;
        int iVar31;
        int psVar32;

        masterPosition = new Vector3(
            (float)param1.position.x / GameManager.instance.translateFactor,
            (float)-param1.position.y / GameManager.instance.translateFactor,
            (float)param1.position.z / GameManager.instance.translateFactor);
        masterRotation = param1.rotation.Matrix2Quaternion;
        masterRotation.eulerAngles = new Vector3(-masterRotation.eulerAngles.x, masterRotation.eulerAngles.y, -masterRotation.eulerAngles.z);
        masterScale = param1.rotation.Scale;
        lod_y = (float)GameManager.instance.DAT_DB0 / GameManager.instance.translateFactor;
        Vector3 wantedPosition = new Vector3(lodOffset.x, -lodOffset.y, lodOffset.z);
        Vector3 relativePosition = masterRotation * Vector3.Scale(wantedPosition, masterScale) + masterPosition;
        relativePosition.y = -lod_y;
        lod.position = relativePosition;
        lod.localEulerAngles = new Vector3(0, masterRotation.eulerAngles.y, 0);

        if (!topView)
        {
            if (lod2.gameObject.activeSelf)
                lod2.gameObject.SetActive(false);
            UIManager.instance.Underwater(masterPosition.y, lod.position.y);
        }
        else
        {
            if (!lod2.gameObject.activeSelf)
                lod2.gameObject.SetActive(true);
        }

        /*iVar17 = GameManager.instance.DAT_F20;
        iVar24 = GameManager.instance.DAT_EDC;
        sVar1 = param1.rotation.V10;
        uVar2 = (ushort)param1.rotation.V11;
        iVar30 = GameManager.instance.DAT_ED8 * param1.rotation.V12;
        uVar25 = (uint)(param2 - param1.position.y);
        iVar9 = Utilities.LeadingZeros((int)uVar25);
        uVar16 = 12;

        if (iVar9 - 1 < 12)
            uVar16 = (uint)(iVar9 - 1);

        LevelManager.instance.DAT_DE0.a = 42;
        uVar10 = uVar25;

        if ((int)uVar25 < 0)
            uVar10 = (uint)-(int)uVar25;

        iVar9 = (int)(uVar10 << (int)(uVar16 & 31));
        Coprocessor.colorCode.r = LevelManager.instance.DAT_DE0.r;
        Coprocessor.colorCode.g = LevelManager.instance.DAT_DE0.g;
        Coprocessor.colorCode.b = LevelManager.instance.DAT_DE0.b;
        Coprocessor.colorCode.code = LevelManager.instance.DAT_DE0.a;
        Coprocessor.farColor._rfc = 0xff0;
        Coprocessor.farColor._gfc = 0xff0;
        Coprocessor.farColor._bfc = 0xff0;
        uVar15 = (uint)(int)sVar1;
        uVar19 = (uint)(int)(short)uVar2;
        uVar10 = uVar19;

        if ((int)uVar19 < 0)
            uVar10 = (uint)-(int)uVar19;

        uVar11 = uVar15;

        if ((int)uVar15 < 0)
            uVar11 = (uint)-(int)uVar15;

        sVar23 = (short)GameManager.instance.DAT_EDC;
        sVar5 = (short)GameManager.instance.DAT_F20;

        

        if ((int)uVar11 < (int)uVar10)
        {
            iVar27 = (int)((GameManager.instance.DAT_EDC / 2) * (int)uVar15) / (int)uVar19;
            iVar24 = iVar27;

            if (iVar27 < 0)
                iVar24 = -iVar27;

            iVar24 = GameManager.instance.DAT_F20 / 2 + iVar24;
            iVar28 = iVar24 * (int)uVar19;
            uVar10 = uVar19 ^ uVar25;
            iVar12 = (int)uVar19 >> (int)(12 - uVar16 & 31);
            iVar31 = iVar30;

            if (iVar12 < 0)
                iVar12 = -iVar12;

            if (iVar30 < 0)
                iVar31 = -iVar30;

            if (iVar28 < 0)
                iVar28 = -iVar28;

            if (iVar28 < iVar31)
            {
                iVar28 = iVar24;

                if (-1 < (int)uVar10)
                    iVar28 = -iVar24;

                iVar22 = iVar28 * (int)uVar19 + iVar30 >> (int)(12 - uVar16 & 31);
            }
            else
            {
                iVar31 = -iVar30;
                iVar28 = iVar31 / (int)uVar19;
                iVar22 = 0;
            }

            iVar26 = GameManager.instance.DAT_F20 / 2;
            psVar20 = 0;
            psVar32 = 0;

            do
            {
                if ((int)uVar10 < 0)
                    iVar21 = iVar28 - 1;
                else
                    iVar21 = iVar28 + 1;

                iVar22 += iVar12;
                iVar29 = iVar9 / iVar22;
                iVar13 = iVar29 - 0xc00;

                if (iVar29 < 0xc00) break;

                if (iVar13 < 0)
                    iVar13 = iVar29 - 3069;

                Coprocessor.accumulator.ir0 = (short)(iVar13 >> 2);
                Coprocessor.ExecuteDPCS(12, false);
                cVar3 = new Color32(Coprocessor.colorFIFO.r2, Coprocessor.colorFIFO.g2, Coprocessor.colorFIFO.b2, 255);
                sVar4 = (short)iVar26;
                sVar1 = (short)(sVar4 + iVar28);
                sVar6 = (short)iVar27;
                sVar4 += (short)iVar21;
                newLODVertices[psVar20] = new Vector3(0, sVar1 + sVar6, psVar20 / 4 * 0x2);
                newLODVertices[psVar20 + 1] = new Vector3(sVar23, sVar1 - sVar6, psVar20 / 4 * 0x2);
                newLODVertices[psVar20 + 2] = new Vector3(0, sVar4 + sVar6, (psVar20 / 4 + 1) * 0x2);
                newLODVertices[psVar20 + 3] = new Vector3(sVar23, sVar4 - sVar6, (psVar20 / 4 + 1) * 0x2);
                newLODColors[psVar20] = cVar3;
                newLODColors[psVar20 + 1] = cVar3;
                newLODColors[psVar20 + 2] = cVar3;
                newLODColors[psVar20 + 3] = cVar3;
                newLODUVs[psVar20] = new Vector2(0, 0);
                newLODUVs[psVar20 + 1] = new Vector2(0, 0);
                newLODUVs[psVar20 + 2] = new Vector2(0, 0);
                newLODUVs[psVar20 + 3] = new Vector2(0, 0);
                newLODIndices[psVar32] = psVar20 + 2;
                newLODIndices[psVar32 + 1] = psVar20 + 1;
                newLODIndices[psVar32 + 2] = psVar20;
                newLODIndices[psVar32 + 3] = psVar20 + 1;
                newLODIndices[psVar32 + 4] = psVar20 + 2;
                newLODIndices[psVar32 + 5] = psVar20 + 3;
                psVar20 += 4;
                psVar32 += 6;
                iVar13 = iVar21;

                if ((int)uVar10 < 0)
                    iVar13 = -iVar21;

                iVar28 = iVar21;
            } while (iVar13 < iVar24);

            if (uVar2 == 0)
                goto LAB_16620;

            iVar24 = iVar17 / 2 + (int)(uVar25 * 16) / (short)uVar2;
            iVar9 = iVar24 + iVar27;
            iVar24 -= iVar27;
            cVar16 = new Color32(Coprocessor.colorCode.r, Coprocessor.colorCode.g, Coprocessor.colorCode.b, 255);
            newLODVertices[psVar20 + 2].x = 0;
            newLODVertices[psVar20].x = 0;
            newLODVertices[psVar20 + 3].x = sVar23;
            newLODVertices[psVar20 + 1].x = sVar23;
            sVar18 = (short)iVar9;
            sVar14 = (short)iVar24;

            if (uVar2 << 16 < 0)
            {
                if (iVar9 < iVar17 || iVar24 < iVar17)
                {
                    newLODVertices[psVar20].y = sVar18;
                    newLODVertices[psVar20 + 1].y = sVar14;
                }
                else
                {
                    newLODVertices[psVar20 + 1].y = sVar5 - 1;
                    newLODVertices[psVar20].y = sVar5 - 1;
                }

                sVar8 = 0;

                if (iVar9 < 0)
                    sVar8 = sVar18;

                newLODVertices[psVar20 + 2].y = sVar8;
                sVar18 = 0;

                if (iVar24 < 0)
                    sVar18 = sVar14;

                newLODVertices[psVar20 + 3].y = sVar18;
            }
            else
            {
                if (iVar9 < 0 && iVar24 < 0)
                {
                    newLODVertices[psVar20 + 1].y = 0;
                    newLODVertices[psVar20].y = 0;
                }
                else
                {
                    newLODVertices[psVar20].y = sVar18;
                    newLODVertices[psVar20 + 1].y = sVar14;
                }

                iVar17--;
                iVar27 = iVar17;

                if (iVar17 < iVar9)
                    iVar27 = iVar9;

                newLODVertices[psVar20 + 2].y = iVar27;

                if (iVar17 < iVar24)
                    iVar17 = iVar24;

                newLODVertices[psVar20 + 3].y = iVar17;
            }

            newLODColors[psVar20] = cVar16;
            newLODColors[psVar20 + 1] = cVar16;
            newLODColors[psVar20 + 2] = cVar16;
            newLODColors[psVar20 + 3] = cVar16;
            newLODUVs[psVar20] = new Vector2(0, 0);
            newLODUVs[psVar20 + 1] = new Vector2(0, 0);
            newLODUVs[psVar20 + 2] = new Vector2(0, 0);
            newLODUVs[psVar20 + 3] = new Vector2(0, 0);
            newLODIndices[psVar32] = psVar20;
            newLODIndices[psVar32 + 1] = psVar20 + 1;
            newLODIndices[psVar32 + 2] = psVar20 + 2;
            newLODIndices[psVar32 + 3] = psVar20 + 3;
            newLODIndices[psVar32 + 4] = psVar20 + 2;
            newLODIndices[psVar32 + 5] = psVar20 + 1;
            psVar20 += 4;
            psVar32 += 6;
        }
        else
        {
            iVar27 = ((GameManager.instance.DAT_F20 / 2) * (int)uVar19) / (int)uVar15;
            iVar17 = iVar27;

            if (iVar27 < 0)
                iVar17 = -iVar27;

            iVar17 = GameManager.instance.DAT_EDC / 2 + iVar17;
            iVar28 = iVar17 * (int)uVar15;
            uVar10 = uVar15 ^ uVar25;
            iVar12 = (int)uVar15 >> (int)(12 - uVar16 & 31);
            iVar31 = iVar30;

            if (iVar12 < 0)
                iVar12 = -iVar12;

            if (iVar30 < 0)
                iVar31 = -iVar30;

            if (iVar28 < 0)
                iVar28 = -iVar28;

            if (iVar28 < iVar31)
            {
                iVar28 = iVar17;

                if (-1 < (int)uVar10)
                    iVar28 = -iVar17;

                iVar22 = iVar28 * (int)uVar15 + iVar30 >> (int)(12 - uVar16 & 31);
            }
            else
            {
                iVar31 = -iVar30;
                iVar28 = iVar31 / (int)uVar15;
                iVar22 = 0;
            }

            iVar26 = GameManager.instance.DAT_EDC / 2;
            psVar20 = 0;
            psVar32 = 0;

            do
            {
                if ((int)uVar10 < 0)
                    iVar21 = iVar28 - 1;
                else
                    iVar21 = iVar28 + 1;

                iVar22 += iVar12;
                iVar29 = iVar9 / iVar22;
                iVar13 = iVar29 - 0xc00;

                if (iVar29 < 0xc00) break;

                if (iVar13 < 0)
                    iVar13 = iVar29 - 3069;

                Coprocessor.accumulator.ir0 = (short)(iVar13 >> 2);
                Coprocessor.ExecuteDPCS(12, false);
                cVar3 = new Color32(Coprocessor.colorFIFO.r2, Coprocessor.colorFIFO.g2, Coprocessor.colorFIFO.b2, 255);
                sVar7 = (short)iVar26;
                sVar6 = (short)(sVar7 + iVar28);
                sVar4 = (short)iVar27;
                sVar7 += (short)iVar21;
                newLODVertices[psVar20] = new Vector3(sVar6 + sVar4, 0, 0);
                newLODVertices[psVar20 + 1] = new Vector3(sVar6 - sVar4, sVar5, 0);
                newLODVertices[psVar20 + 2] = new Vector3(sVar7 + sVar4, 0, 0);
                newLODVertices[psVar20 + 3] = new Vector3(sVar7 - sVar4, sVar5, 0);
                newLODColors[psVar20] = cVar3;
                newLODColors[psVar20 + 1] = cVar3;
                newLODColors[psVar20 + 2] = cVar3;
                newLODColors[psVar20 + 3] = cVar3;
                newLODUVs[psVar20] = new Vector2(0, 0);
                newLODUVs[psVar20 + 1] = new Vector2(0, 0);
                newLODUVs[psVar20 + 2] = new Vector2(0, 0);
                newLODUVs[psVar20 + 3] = new Vector2(0, 0);
                newLODIndices[psVar32] = psVar20;
                newLODIndices[psVar32 + 1] = psVar20 + 1;
                newLODIndices[psVar32 + 2] = psVar20 + 2;
                newLODIndices[psVar32 + 3] = psVar20 + 3;
                newLODIndices[psVar32 + 4] = psVar20 + 2;
                newLODIndices[psVar32 + 5] = psVar20 + 1;
                psVar20 += 4;
                psVar32 += 6;
                iVar13 = iVar21;

                if ((int)uVar10 < 0)
                    iVar13 = -iVar21;

                iVar28 = iVar21;
            } while (iVar13 < iVar17);

            if (sVar1 == 0)
                goto LAB_16620;

            iVar17 = iVar24 / 2 + (int)(uVar25 * 16) / sVar1;
            iVar9 = iVar17 + iVar27;
            iVar17 -= iVar27;
            cVar16 = new Color32(Coprocessor.colorCode.r, Coprocessor.colorCode.g, Coprocessor.colorCode.b, 255);
            newLODVertices[psVar20 + 2].y = 0;
            newLODVertices[psVar20].y = 0;
            newLODVertices[psVar20 + 3].y = sVar5;
            newLODVertices[psVar20 + 1].y = sVar5;
            sVar18 = (short)iVar9;
            sVar14 = (short)iVar17;

            if (sVar1 < 0)
            {
                if (iVar9 < iVar24 || iVar17 < iVar24)
                {
                    newLODVertices[psVar20].x = sVar18;
                    newLODVertices[psVar20 + 1].x = sVar14;
                }
                else
                {
                    newLODVertices[psVar20 + 1].x = sVar23 - 1;
                    newLODVertices[psVar20].x = sVar23 - 1;
                }

                sVar8 = 0;

                if (iVar9 < 0)
                    sVar8 = sVar18;

                newLODVertices[psVar20 + 2].x = sVar8;

                sVar18 = 0;

                if (iVar17 < 0)
                    sVar18 = sVar14;

                newLODVertices[psVar20 + 3].x = sVar18;
            }
            else
            {
                if (iVar9 < 0 && iVar17 < 0)
                {
                    newLODVertices[psVar20 + 1].x = 0;
                    newLODVertices[psVar20].x = 0;
                }
                else
                {
                    newLODVertices[psVar20].x = sVar18;
                    newLODVertices[psVar20 + 1].x = sVar14;
                }

                iVar24--;
                iVar27 = iVar24;

                if (iVar24 < iVar9)
                    iVar27 = iVar9;

                newLODVertices[psVar20 + 2].x = iVar27;

                if (iVar24 < iVar17)
                    iVar24 = iVar17;

                newLODVertices[psVar20 + 3].x = iVar24;
            }

            newLODColors[psVar20] = cVar16;
            newLODColors[psVar20 + 1] = cVar16;
            newLODColors[psVar20 + 2] = cVar16;
            newLODColors[psVar20 + 3] = cVar16;
            newLODUVs[psVar20] = new Vector2(0, 0);
            newLODUVs[psVar20 + 1] = new Vector2(0, 0);
            newLODUVs[psVar20 + 2] = new Vector2(0, 0);
            newLODUVs[psVar20 + 3] = new Vector2(0, 0);
            newLODIndices[psVar32] = psVar20;
            newLODIndices[psVar32 + 1] = psVar20 + 1;
            newLODIndices[psVar32 + 2] = psVar20 + 2;
            newLODIndices[psVar32 + 3] = psVar20 + 3;
            newLODIndices[psVar32 + 4] = psVar20 + 2;
            newLODIndices[psVar32 + 5] = psVar20 + 1;
            psVar20 += 4;
            psVar32 += 6;
        }

        LAB_16620:
        mesh.Clear();
        mesh.SetVertices(newLODVertices, 0, psVar20);
        mesh.SetColors(newLODColors, 0, psVar20);
        mesh.SetUVs(0, newLODUVs, 0, psVar20);
        mesh.SetIndices(newLODIndices, 0, psVar32, MeshTopology.Triangles, 0);*/
    }

    public void FUN_16664(Vector3Int param1, int param2)
    {
        ushort uVar1;
        int iVar2;
        int iVar3;
        int iVar4;
        int iVar5;
        byte bVar6;
        int iVar7;
        uint uVar8;
        int psVar9;
        int pbVar10;
        int puVar11;
        int pbVar12;
        uint uVar13;
        int iVar14;
        int puVar15;
        int iVar16;
        int iVar17;
        int iVar18;
        int psVar19;
        int puVar20;
        int iVar21;
        int iVar22;
        int puVar23;
        int iVar24;
        int iVar25; //t4
        int iVar26;
        int iVar27;
        Vector3Int local_b8;
        Vector3Int local_b0;
        Vector3Int local_a0;
        Vector3Int local_90;
        Vector3Int local_80;
        Vector3Int local_70;
        int local_60;
        int local_5c;
        int local_58;
        int local_54;
        int local_50;
        int local_48;
        int local_40;
        int local_30;
        int local_2c;
        int local_28;
        int local_24;
        int local_20;

        iVar22 = GameManager.instance.DAT_F20;
        iVar7 = GameManager.instance.DAT_EDC;
        iVar24 = GameManager.instance.DAT_ED8; //s5
        puVar23 = 0;
        puVar20 = 0; //not in the original code
        Utilities.FUN_246BC(GameManager.instance.DAT_F28);
        local_b8 = param1;

        if (local_b8.z < 0xff1)
        {
            iVar4 = local_b8.y;
            iVar21 = local_b8.x;
            iVar3 = iVar4;

            if (iVar4 < 0)
                iVar3 = -iVar4;

            iVar2 = iVar21;

            if (iVar21 < 0)
                iVar2 = -iVar21;

            local_b0 = new Vector3Int();

            if (iVar2 < iVar3)
            {
                local_b0.x = (iVar7 * -0x100) / 2;
                local_b0.z = iVar24 << 8;
                param2 *= 0x1000;
                local_b0.y = (param2 - iVar21 * local_b0.x - local_b8.z * local_b0.z) / iVar4;
                local_a0 = Utilities.FUN_24008(local_b0);
                local_b0.x = iVar7 * 128;
                local_b0.y = ((param2 - local_b8.x * local_b0.x) - local_b8.z * local_b0.z) / local_b8.y;
                local_90 = Utilities.FUN_24008(local_b0);
                local_b0.x = (iVar7 * -0xc00) / 2;
                local_b0.z = (iVar24 << 1) + iVar24 << 10;
                local_b0.y = (param2 - local_b8.x * local_b0.x - local_b8.z * local_b0.z) / local_b8.y;
                local_80 = Utilities.FUN_24008(local_b0);
                local_b0.x = iVar7 * 0x600;
                local_b0.y = ((param2 - local_b8.x * local_b0.x) - local_b8.z * local_b0.z) / local_b8.y;
            }
            else
            {
                local_b0.y = (iVar22 * -0x100) / 2;
                local_b0.z = iVar24 << 8;
                param2 *= 0x1000;
                local_b0.x = (param2 - iVar4 * local_b0.y - local_b8.z * local_b0.z) / iVar21;
                local_a0 = Utilities.FUN_24008(local_b0);
                local_b0.y = iVar22 * 128;
                local_b0.x = ((param2 - local_b8.y * local_b0.y) - local_b8.z * local_b0.z) / local_b8.x;
                local_90 = Utilities.FUN_24008(local_b0);
                local_b0.y = (iVar22 * -0xc00) / 2;
                local_b0.z = (iVar24 << 1) + iVar24 << 10;
                local_b0.x = (param2 - local_b8.y * local_b0.y - local_b8.z * local_b0.z) / local_b8.x;
                local_80 = Utilities.FUN_24008(local_b0);
                local_b0.y = iVar22 * 0x600;
                local_b0.x = ((param2 - local_b8.y * local_b0.y) - local_b8.z * local_b0.z) / local_b8.x;
            }
        }
        else
        {
            local_b0 = new Vector3Int();
            iVar3 = -iVar7;
            param2 = iVar24 * param2 << 12;
            local_b0.z = param2 / ((iVar3 * local_b8.x) / 2 - (iVar22 * local_b8.y) / 2 + iVar24 * local_b8.z);
            local_b0.x = (iVar3 * local_b0.z) / iVar24;
            local_b0.y = (-iVar22 * local_b0.z) / iVar24;
            local_a0 = Utilities.FUN_24008(local_b0);
            local_b0.z = param2 / ((iVar7 * local_b8.x) / 2 - (iVar22 * local_b8.y) / 2 + iVar24 * local_b8.z);
            local_b0.x = (iVar7 * local_b0.z) / iVar24;
            local_b0.y = (-iVar22 * local_b0.z) / iVar24;
            local_90 = Utilities.FUN_24008(local_b0);
            local_b0.z = param2 / ((iVar3 * local_b8.x) / 2 + (iVar22 * local_b8.y) / 2 + iVar24 * local_b8.z);
            local_b0.x = (iVar3 * local_b0.z) / iVar24;
            local_b0.y = (iVar22 * local_b0.z) / iVar24;
            local_80 = Utilities.FUN_24008(local_b0);
            local_b0.z = param2 / ((iVar7 * local_b8.x) / 2 + (iVar22 * local_b8.y) / 2 + iVar24 * local_b8.z);
            local_b0.x = (iVar7 * local_b0.z) / iVar24;
            local_b0.y = (iVar22 * local_b0.z) / iVar24;
        }

        local_70 = Utilities.FUN_24008(local_b0);
        iVar7 = local_90.x;

        if (local_a0.x < local_90.x)
            iVar7 = local_a0.x;

        iVar22 = local_80.x;

        if (iVar7 < local_80.x)
            iVar22 = iVar7;

        iVar7 = local_70.x;

        if (iVar22 < local_70.x)
            iVar7 = iVar22;

        local_40 = iVar7 >> 16;
        iVar7 = local_90.z;

        if (local_a0.z < local_90.z)
            iVar7 = local_a0.z;

        iVar22 = local_80.z;

        if (iVar7 < local_80.z)
            iVar22 = iVar7;

        local_28 = local_70.z;

        if (iVar22 < local_70.z)
            local_28 = iVar22;

        local_28 >>= 16;

        if (local_90.x < local_a0.x)
            local_90.x = local_a0.x;

        if (local_80.x < local_90.x)
            local_80.x = local_90.x;

        if (local_70.x < local_80.x)
            local_70.x = local_80.x;

        local_60 = local_70.x + 0xffff >> 16;

        if (local_90.z < local_a0.z)
            local_90.z = local_a0.z;

        if (local_80.z < local_90.z)
            local_80.z = local_90.z;

        if (local_70.z < local_80.z)
            local_70.z = local_80.z;

        iVar7 = GameManager.instance.DAT_DA0 >> 16;
        local_5c = local_70.z + 0xffff >> 16;

        if (local_5c <= iVar7 || local_28 < iVar7)
        {
            if (!(local_5c <= iVar7))
                local_5c = iVar7;

            if (31 < local_60 - local_40)
                local_60 = local_40 + 31;

            if (31 < local_5c - local_28)
                local_5c = local_28 + 31;

            Utilities.SetRotMatrix(GameManager.instance.DAT_F00.rotation);
            
            local_b0.x = local_40 << 8;
            local_b0.y = GameManager.instance.DAT_DB0;

            if (GameManager.instance.DAT_DB0 < 0)
                local_b0.y = GameManager.instance.DAT_DB0 + 255;

            local_b0.y >>= 8;
            local_b0.z = local_28 << 8;
            local_b0 = Utilities.FUN_23F7C(local_b0);
            iVar25 = GameManager.instance.DAT_F00.position.x;

            if (iVar25 < 0)
                iVar25 += 255;
            
            local_b0.x += iVar25 >> 8;
            iVar25 = GameManager.instance.DAT_F00.position.y;

            if (iVar25 < 0)
                iVar25 += 255;

            local_b0.y += iVar25 >> 8;
            iVar25 = GameManager.instance.DAT_F00.position.z;

            if (iVar25 < 0)
                iVar25 += 255;

            local_b0.z += iVar25 >> 8;
            UpdatePosition((Vector3)(new Vector3Int(local_b0.x << 8, local_b0.y << 8, local_b0.z << 8)) / GameManager.instance.translateFactor);
            Coprocessor.translationVector._trx = local_b0.x;
            Coprocessor.translationVector._try = local_b0.y;
            Coprocessor.translationVector._trz = local_b0.z;
            iVar7 = GameManager.instance.DAT_DB0;

            if (GameManager.instance.DAT_DB0 < 0)
                iVar7 = GameManager.instance.DAT_DB0 + 2047;

            local_24 = iVar7 >> 11;
            iVar22 = 0;

            if (-1 < local_5c - local_28)
            {
                iVar25 = ((iVar24 << 1) + iVar24 << 2); //t4
                iVar3 = 0;

                do
                {
                    Coprocessor.vector0.vz0 = (short)(iVar22 << 8);
                    iVar21 = 0;

                    if (-1 < local_60 - local_40)
                    {
                        pbVar12 = iVar3;

                        do
                        {
                            Coprocessor.vector0.vx0 = (short)(iVar21 << 8);
                            Coprocessor.vector0.vy0 = (short)(iVar21 << 8 >> 16);
                            Coprocessor.ExecuteRTPS(12, false);
                            iVar17 = Coprocessor.screenXYFIFO.sx2; //sxp
                            iVar3 = Coprocessor.screenXYFIFO.sy2; //syp
                            iVar4 = Coprocessor.screenZFIFO.sz3;
                            bVar6 = (terrain.vertices[terrain.chunks[((uint)(iVar21 + local_40) >> 6) * 32 + ((uint)(iVar22 + local_28) >> 6)] * 4096 +
                                    ((iVar22 + local_28 & 63) * 2 + (iVar21 + local_40 & 63) * 128) / 2] & 0x7ff) < local_24 ? (byte)1 : (byte)0;

                            if (iVar4 < iVar24)
                                bVar6 |= 2;

                            if (iVar25 < iVar4)
                                bVar6 |= 4;

                            /*if (iVar17 < 0)
                                bVar6 |= 8;

                            if (320 < iVar17)
                                bVar6 |= 0x10;

                            if (iVar3 < 0)
                                bVar6 |= 0x20;

                            if (240 < iVar3)
                                bVar6 |= 0x40;*/
                                
                            iVar21++;
                            DAT_B5D70[pbVar12] = bVar6;
                            pbVar12++;
                        } while (iVar21 <= local_5c - local_28);
                    }

                    iVar22++;
                    iVar3 = iVar22 * 32;
                } while (iVar22 <= local_5c - local_28);
            }

            iVar27 = GameManager.instance.DAT_28 << 12;
            iVar26 = (int)((long)iVar27 * -0x77777777 >> 32) + iVar27;
            iVar27 >>= 31;
            iVar26 = (iVar26 >> 7) - iVar27;
            iVar16 = local_28 * 873 + iVar26;
            iVar7 = 0;

            if (-1 < local_5c - local_28)
            {
                iVar24 = local_40 * 873;
                local_24 = -0x5FFF5FFF;

                do
                {
                    iVar27 = GameManager.instance.DAT_28 << 12;
                    iVar26 = (int)((long)iVar27 * -0x77777777 >> 32) + iVar27;
                    iVar27 >>= 31;
                    iVar26 = (iVar26 >> 7) - iVar27;
                    iVar14 = iVar24 + iVar26;
                    iVar22 = 0;
                    puVar15 = iVar7 * 0x20;
                    pbVar12 = iVar7 * 0x20;

                    if (-1 < local_60 - local_40)
                    {
                        pbVar10 = 33 + iVar7 * 0x20;

                        do
                        {
                            if ((DAT_B5D70[pbVar10] & DAT_B5D70[pbVar10 - 1] & DAT_B5D70[pbVar12] & DAT_B5D70[pbVar10 - 32]) != 0)
                                DAT_B5D70[pbVar12] |= 0x80;

                            iVar5 = iVar14 & 0xfff;
                            local_20 = (int)((ulong)((long)(GameManager.DAT_65C90[(iVar5 * 4) / 2] *
                                       GameManager.DAT_65C90[((iVar16 & 0xfff) * 4) / 2]) * local_24) >> 32);
                            iVar22++;
                            pbVar10++;
                            pbVar12++;
                            iVar14 += 873;
                            iVar26 = (GameManager.DAT_65C90[(iVar5 * 4) / 2] *
                                      GameManager.DAT_65C90[((iVar16 & 0xfff) * 4) / 2]);
                            iVar27 = local_20 + iVar26 >> 14;
                            iVar27 -= iVar26 >> 31;
                            DAT_B5570[puVar15] = (short)iVar27;
                            puVar15++;
                        } while (iVar22 <= local_60 - local_40);
                    }

                    iVar7++;
                    iVar16 += 873;
                } while (iVar7 <= local_5c - local_28);
            }

            iVar27 = GameManager.instance.DAT_28 << 12;
            iVar26 = (int)((long)iVar27 * -0x63F63F63 >> 32) + iVar27;
            iVar27 >>= 31;
            iVar26 = (iVar26 >> 8) - iVar27;
            iVar16 = local_28 * 1456 + iVar26;
            iVar7 = 0;

            if (-1 < local_5c - local_28)
            {
                iVar24 = local_40 * 1456;

                do
                {
                    iVar22 = 0;
                    psVar9 = iVar7 * 0x20;
                    pbVar12 = iVar7 * 0x20;
                    iVar27 = GameManager.instance.DAT_28 << 12;
                    iVar26 = (int)((long)iVar27 * -0x63F63F63 >> 32) + iVar27;
                    iVar27 >>= 31;
                    iVar26 = (iVar26 >> 8) - iVar27;
                    iVar14 = iVar24 + iVar26;

                    if (-1 < local_60 - local_40)
                    {
                        do
                        {
                            if ((DAT_B5D70[pbVar12] & 4) == 0)
                            {
                                iVar3 = GameManager.DAT_65C90[((iVar14 & 0xfff) * 4) / 2] *
                                        GameManager.DAT_65C90[((iVar16 & 0xfff) * 4) / 2];

                                if (iVar3 < 0)
                                    iVar3 += 0x1ffff;

                                DAT_B5570[psVar9] += (short)(iVar3 >> 17);
                            }
                            else
                                DAT_B5570[psVar9] = 0;

                            iVar22++;
                            pbVar12++;
                            psVar9++;
                            iVar14 += 1456;
                        } while (iVar22 <= local_60 - local_40);
                    }

                    iVar7++;
                    iVar16 += 1456;
                } while (iVar7 <= local_5c - local_28);
            }

            local_24 = GameManager.instance.DAT_DB0;

            if (GameManager.instance.DAT_DB0 < 0)
                local_24 = GameManager.instance.DAT_DB0 + 15;

            local_24 >>= 4;
            iVar7 = 0;

            if (0 < local_5c - local_28)
            {
                local_50 = 0x100;

                do
                {
                    psVar9 = iVar7 * 0x20;
                    local_2c = iVar7 * 0x20;
                    iVar16 = 0;

                    if (0 < local_60 - local_40)
                    {
                        local_58 = iVar7 << 8;
                        psVar19 = 33 + iVar7 * 0x20;
                        pbVar12 = 33 + iVar7 * 0x20;
                        iVar5 = 0x100;
                        puVar20 = puVar23 + 2;
                        local_30 = iVar7 + local_28;
                        local_54 = (int)((uint)local_30 >> 6) << 2;
                        local_48 = local_50;
                        iVar14 = local_40;

                        do
                        {
                            if ((DAT_B5D70[local_2c] & 0x80) == 0)
                            {
                                //goto SKIP2;

                                iVar22 = DAT_B5570[psVar9];
                                iVar3 = (iVar16 & 0xff) * 0x100;

                                if (iVar22 < 0)
                                    iVar22 += 15;

                                Coprocessor.vector0.vx0 = (short)iVar3;
                                Coprocessor.vector0.vy0 = (short)(iVar22 >> 4);
                                Coprocessor.vector0.vz0 = (short)local_58;
                                iVar22 = DAT_B5570[psVar19 - 32];

                                if (iVar22 < 0)
                                    iVar22 += 15;

                                Coprocessor.vector1.vx1 = (short)iVar5;
                                Coprocessor.vector1.vy1 = (short)(iVar22 >> 4);
                                Coprocessor.vector1.vz1 = (short)local_58;
                                iVar22 = DAT_B5570[psVar19 - 1];

                                if (iVar22 < 0)
                                    iVar22 += 15;

                                Coprocessor.vector2.vx2 = (short)iVar3;
                                Coprocessor.vector2.vy2 = (short)(iVar22 >> 4);
                                Coprocessor.vector2.vz2 = (short)local_48;
                                Coprocessor.ExecuteRTPT(12, false);
                                //...
                                iVar27 = 0;

                                if (psVar19 - 34 >= 0)
                                    iVar27 = DAT_B5570[psVar19 - 34];

                                iVar22 = DAT_B5570[psVar19 - 32] - iVar27;

                                if (iVar22 < 0)
                                    iVar22 += 15;

                                iVar22 = (iVar22 >> 4) + 32;

                                if (iVar22 < 0)
                                    iVar3 = 0;
                                else
                                {
                                    iVar3 = 63;

                                    if (iVar22 < 64)
                                        iVar3 = iVar22;
                                }

                                iVar27 = 0;

                                if (psVar19 - 65 >= 0)
                                    iVar27 = DAT_B5570[psVar19 - 65];

                                iVar22 = DAT_B5570[psVar19 - 1] - iVar27;

                                if (iVar22 < 0)
                                    iVar22 += 15;

                                iVar22 = (iVar22 >> 4) + 32;

                                if (iVar22 < 0)
                                {
                                    iVar22 = 0;
                                    uVar1 = (ushort)(iVar22 << 8);
                                }
                                else
                                {
                                    uVar1 = 0x3f00;

                                    if (iVar22 < 64)
                                        uVar1 = (ushort)(iVar22 << 8);
                                }

                                primitives[puVar20 - 2].uvs = new Vector2Int(iVar3, uVar1 >> 8);
                                iVar22 = DAT_B5570[psVar19 - 31] - DAT_B5570[psVar9];

                                if (iVar22 < 0)
                                    iVar22 += 15;

                                iVar22 = (iVar22 >> 4) + 32;

                                if (iVar22 < 0)
                                    iVar3 = 0;
                                else
                                {
                                    iVar3 = 63;

                                    if (iVar22 < 64)
                                        iVar3 = iVar22;
                                }

                                iVar27 = 0;

                                if (psVar19 - 64 >= 0)
                                    iVar27 = DAT_B5570[psVar19 - 64];

                                iVar22 = DAT_B5570[psVar19] - iVar27;

                                if (iVar22 < 0)
                                    iVar22 += 15;

                                iVar22 = (iVar22 >> 4) + 32;

                                if (iVar22 < 0)
                                {
                                    iVar22 = 0;
                                    uVar1 = (ushort)(iVar22 << 8);
                                }
                                else
                                {
                                    uVar1 = 0x3f00;

                                    if (iVar22 < 64)
                                        uVar1 = (ushort)(iVar22 << 8);
                                }

                                primitives[puVar20 - 1].uvs = new Vector2Int(iVar3, uVar1 >> 8);
                                iVar22 = DAT_B5570[psVar19] - DAT_B5570[psVar19 - 2];

                                if (iVar22 < 0)
                                    iVar22 += 15;

                                iVar22 = (iVar22 >> 4) + 32;

                                if (iVar22 < 0)
                                    iVar3 = 0;
                                else
                                {
                                    iVar3 = 63;

                                    if (iVar22 < 64)
                                        iVar3 = iVar22;
                                }

                                iVar22 = DAT_B5570[psVar19 + 31] - DAT_B5570[psVar9];

                                if (iVar22 < 0)
                                    iVar22 += 15;

                                iVar22 = (iVar22 >> 4) + 32;

                                if (iVar22 < 0)
                                {
                                    iVar22 = 0;
                                    uVar1 = (ushort)(iVar22 << 8);
                                }
                                else
                                {
                                    uVar1 = 0x3f00;

                                    if (iVar22 < 64)
                                        uVar1 = (ushort)(iVar22 << 8);
                                }

                                primitives[puVar20].uvs = new Vector2Int(iVar3, uVar1 >> 8);
                                Coprocessor.ExecuteAVSZ3();

                                if (((DAT_B5D70[pbVar12 - 1] | DAT_B5D70[local_2c] | DAT_B5D70[pbVar12 - 32]) & 1) == 0)
                                {
                                    primitives[puVar20 - 2].screen = new Vector2Int(Coprocessor.screenXYFIFO.sx0, Coprocessor.screenXYFIFO.sy0);
                                    primitives[puVar20 - 2].verts = new Vector3Int(Coprocessor.screenXYZFIFO.sx0, Coprocessor.screenXYZFIFO.sy0, Coprocessor.screenXYZFIFO.sz0);
                                    primitives[puVar20 - 1].screen = new Vector2Int(Coprocessor.screenXYFIFO.sx1, Coprocessor.screenXYFIFO.sy1);
                                    primitives[puVar20 - 1].verts = new Vector3Int(Coprocessor.screenXYZFIFO.sx1, Coprocessor.screenXYZFIFO.sy1, Coprocessor.screenXYZFIFO.sz1);
                                    primitives[puVar20].screen = new Vector2Int(Coprocessor.screenXYFIFO.sx2, Coprocessor.screenXYFIFO.sy2);
                                    primitives[puVar20].verts = new Vector3Int(Coprocessor.screenXYZFIFO.sx2, Coprocessor.screenXYZFIFO.sy2, Coprocessor.screenXYZFIFO.sz2);
                                    newIndices[puVar23] = puVar23;
                                    newIndices[puVar23 + 1] = puVar23 + 1;
                                    newIndices[puVar23 + 2] = puVar23 + 2;
                                    iVar22 = DAT_B5570[psVar19];

                                    if (iVar22 < 0)
                                        iVar22 += 15;
                                    
                                    Coprocessor.vector0.vx0 = (short)iVar5;
                                    Coprocessor.vector0.vy0 = (short)(iVar22 >> 4);
                                    Coprocessor.vector0.vz0 = (short)local_48;
                                    Coprocessor.ExecuteRTPS(12, false);
                                    iVar22 = Coprocessor.averageZ;
                                    //...
                                    puVar20 += 3;
                                    puVar23 += 3;
                                }
                                else
                                {
                                    iVar3 = (iVar14 & 63) * 128;
                                    iVar22 = (local_30 & 63) * 2;
                                    iVar22 = Utilities.FUN_26B80(puVar23,
                                                                ((terrain.vertices[terrain.chunks[(local_54 + (int)((uint)iVar14 >> 6) * 128) / 4]
                                                                * 4096 + (iVar22 + iVar3) / 2] & 0x7ff) * 128 - local_24) - DAT_B5570[psVar9],
                                                                ((terrain.vertices[terrain.chunks[(local_54 + (int)((uint)iVar14 + 1 >> 6) * 128) / 4]
                                                                * 4096 + (iVar22 + (iVar14 + 1 & 63) * 128) / 2] & 0x7ff) * 128 - local_24) - DAT_B5570[psVar19 - 32],
                                                                ((terrain.vertices[terrain.chunks[(((uint)iVar14 >> 6) * 32 + ((uint)local_30 + 1 >> 6))]
                                                                * 4096 + ((local_30 + 1 & 63) * 2 + iVar3) / 2] & 0x7ff) * 128 - local_24) - DAT_B5570[psVar19 - 1],
                                                                primitives);
                                    iVar3 = Coprocessor.averageZ;
                                    iVar21 = DAT_B5570[psVar19 - 32];
                                    //...

                                    if (iVar21 < 0)
                                        iVar21 += 15;

                                    Coprocessor.vector0.vx0 = (short)iVar5;
                                    Coprocessor.vector0.vy0 = (short)(iVar21 >> 4);
                                    Coprocessor.vector0.vz0 = (short)local_58;
                                    iVar3 = DAT_B5570[psVar19 - 1];

                                    if (iVar3 < 0)
                                        iVar3 += 15;

                                    Coprocessor.vector1.vx1 = (short)((iVar16 & 0xff) * 0x100);
                                    Coprocessor.vector1.vy1 = (short)(iVar3 >> 4);
                                    Coprocessor.vector1.vz1 = (short)local_48;
                                    iVar3 = DAT_B5570[psVar19];

                                    if (iVar3 < 0)
                                        iVar3 += 15;

                                    Coprocessor.vector2.vx2 = (short)iVar5;
                                    Coprocessor.vector2.vy2 = (short)(iVar3 >> 4);
                                    Coprocessor.vector2.vz2 = (short)local_48;
                                    Coprocessor.ExecuteRTPT(12, false);

                                    if (iVar22 != 0)
                                    {
                                        if (iVar22 != 4)
                                        {
                                            newIndices[puVar23] = puVar23;
                                            newIndices[puVar23 + 1] = puVar23 + 1;
                                            newIndices[puVar23 + 2] = puVar23 + 2;
                                            //...
                                            puVar20 += 3;
                                            puVar23 += 3;
                                        }
                                        else
                                        {
                                            primitives[puVar23 + 5].verts = primitives[puVar23 + 3].verts;
                                            primitives[puVar23 + 4].verts = primitives[puVar23 + 2].verts;
                                            primitives[puVar23 + 3].verts = primitives[puVar23 + 1].verts;
                                            primitives[puVar23 + 5].uvs = primitives[puVar23 + 3].uvs;
                                            primitives[puVar23 + 4].uvs = primitives[puVar23 + 2].uvs;
                                            primitives[puVar23 + 3].uvs = primitives[puVar23 + 1].uvs;
                                            newIndices[puVar23] = puVar23;
                                            newIndices[puVar23 + 1] = puVar23 + 1;
                                            newIndices[puVar23 + 2] = puVar23 + 2;
                                            newIndices[puVar23 + 3] = puVar23 + 5;
                                            newIndices[puVar23 + 4] = puVar23 + 4;
                                            newIndices[puVar23 + 5] = puVar23 + 3;
                                            //...
                                            puVar20 += 6;
                                            //...
                                            puVar23 += 6;
                                        }
                                    }
                                }

                                //goto SKIP;

                                SKIP2:
                                //...
                                iVar22 = DAT_B5570[psVar19 - 31] - DAT_B5570[psVar9];

                                if (iVar22 < 0)
                                    iVar22 += 15;

                                iVar22 = (iVar22 >> 4) + 32;

                                if (iVar22 < 0)
                                    iVar3 = 0;
                                else
                                {
                                    iVar3 = 63;

                                    if (iVar22 < 64)
                                        iVar3 = iVar22;
                                }

                                iVar27 = 0;

                                if (psVar19 - 64 >= 0)
                                    iVar27 = DAT_B5570[psVar19 - 64];

                                iVar22 = DAT_B5570[psVar19] - iVar27;

                                if (iVar22 < 0)
                                    iVar22 += 15;

                                iVar22 = (iVar22 >> 4) + 32;

                                if (iVar22 < 0)
                                {
                                    iVar22 = 0;
                                    uVar1 = (ushort)(iVar22 << 8);
                                }
                                else
                                {
                                    uVar1 = 0x3f00;

                                    if (iVar22 < 64)
                                        uVar1 = (ushort)(iVar22 << 8);
                                }

                                primitives[puVar20 - 2].uvs = new Vector2Int(iVar3, uVar1 >> 8);
                                iVar22 = DAT_B5570[psVar19] - DAT_B5570[psVar19 - 2];

                                if (iVar22 < 0)
                                    iVar22 += 15;

                                iVar22 = (iVar22 >> 4) + 32;

                                if (iVar22 < 0)
                                    iVar3 = 0;
                                else
                                {
                                    iVar3 = 63;

                                    if (iVar22 < 64)
                                        iVar3 = iVar22;
                                }

                                iVar22 = DAT_B5570[psVar19 + 31] - DAT_B5570[psVar9];

                                if (iVar22 < 0)
                                    iVar22 += 15;

                                iVar22 = (iVar22 >> 4) + 32;

                                if (iVar22 < 0)
                                {
                                    iVar22 = 0;
                                    uVar1 = (ushort)(iVar22 << 8);
                                }
                                else
                                {
                                    uVar1 = 0x3f00;

                                    if (iVar22 < 64)
                                        uVar1 = (ushort)(iVar22 << 8);
                                }

                                primitives[puVar20 - 1].uvs = new Vector2Int(iVar3, uVar1 >> 8);
                                iVar22 = DAT_B5570[psVar19 + 1] - DAT_B5570[psVar19 - 1];

                                if (iVar22 < 0)
                                    iVar22 += 15;

                                iVar22 = (iVar22 >> 4) + 32;

                                if (iVar22 < 0)
                                    iVar3 = 0;
                                else
                                {
                                    iVar3 = 63;

                                    if (iVar22 < 64)
                                        iVar3 = iVar22;
                                }

                                iVar22 = DAT_B5570[psVar19 + 32] - DAT_B5570[psVar19 - 32];

                                if (iVar22 < 0)
                                    iVar22 += 15;

                                iVar22 = (iVar22 >> 4) + 32;

                                if (iVar22 < 0)
                                {
                                    iVar22 = 0;
                                    uVar1 = (ushort)(iVar22 << 8);
                                }
                                else
                                {
                                    uVar1 = 0x3f00;

                                    if (iVar22 < 64)
                                        uVar1 = (ushort)(iVar22 << 8);
                                }

                                primitives[puVar20].uvs = new Vector2Int(iVar3, uVar1 >> 8);
                                Coprocessor.ExecuteAVSZ3();

                                if (((DAT_B5D70[pbVar12] | DAT_B5D70[pbVar12 - 32] | DAT_B5D70[pbVar12 - 1]) & 1) == 0)
                                {
                                    primitives[puVar20 - 2].screen = new Vector2Int(Coprocessor.screenXYFIFO.sx0, Coprocessor.screenXYFIFO.sy0);
                                    primitives[puVar20 - 2].verts = new Vector3Int(Coprocessor.screenXYZFIFO.sx0, Coprocessor.screenXYZFIFO.sy0, Coprocessor.screenXYZFIFO.sz0);
                                    primitives[puVar20 - 1].screen = new Vector2Int(Coprocessor.screenXYFIFO.sx1, Coprocessor.screenXYFIFO.sy1);
                                    primitives[puVar20 - 1].verts = new Vector3Int(Coprocessor.screenXYZFIFO.sx1, Coprocessor.screenXYZFIFO.sy1, Coprocessor.screenXYZFIFO.sz1);
                                    primitives[puVar20].screen = new Vector2Int(Coprocessor.screenXYFIFO.sx2, Coprocessor.screenXYFIFO.sy2);
                                    primitives[puVar20].verts = new Vector3Int(Coprocessor.screenXYZFIFO.sx2, Coprocessor.screenXYZFIFO.sy2, Coprocessor.screenXYZFIFO.sz2);
                                    newIndices[puVar23] = puVar23 + 2;
                                    newIndices[puVar23 + 1] = puVar23 + 1;
                                    newIndices[puVar23 + 2] = puVar23;
                                    iVar22 = Coprocessor.averageZ;
                                    puVar20 += 3;
                                    //...
                                    puVar23 += 3;
                                }
                                else
                                {
                                    iVar3 = DAT_B5570[psVar19 - 32];
                                    iVar22 = iVar3;

                                    if (iVar3 < 0)
                                        iVar22 = iVar3 + 15;

                                    Coprocessor.vector0.vx0 = (short)iVar5;
                                    Coprocessor.vector0.vy0 = (short)(iVar22 >> 4);
                                    Coprocessor.vector0.vz0 = (short)local_58;
                                    iVar21 = DAT_B5570[psVar19 - 1];
                                    iVar22 = iVar21;

                                    if (iVar21 < 0)
                                        iVar22 = iVar21 + 15;

                                    Coprocessor.vector1.vx1 = (short)((iVar16 & 0xff) * 0x100);
                                    Coprocessor.vector1.vy1 = (short)(iVar22 >> 4);
                                    Coprocessor.vector1.vz1 = (short)local_48;
                                    iVar4 = DAT_B5570[psVar19];
                                    iVar22 = iVar4;

                                    if (iVar4 < 0)
                                        iVar22 = iVar4 + 15;

                                    Coprocessor.vector2.vx2 = (short)iVar5;
                                    Coprocessor.vector2.vy2 = (short)(iVar22 >> 4);
                                    Coprocessor.vector2.vz2 = (short)local_48;
                                    uVar8 = (uint)(local_40 + iVar16 + 1);
                                    uVar13 = (uint)local_30 + 1 >> 6;
                                    iVar22 = (local_30 + 1 & 63) * 2;
                                    iVar22 = Utilities.FUN_26B80(puVar23,
                                                                ((terrain.vertices[terrain.chunks[(local_54 + (int)(uVar8 >> 6) * 128) / 4]
                                                                * 4096 + ((local_30 & 63) * 2 + (uVar8 & 63) * 128) / 2] & 0x7ff) * 128 - local_24) - iVar3,
                                                                ((terrain.vertices[terrain.chunks[(((uint)iVar14 >> 6) * 32 + uVar13)]
                                                                * 4096 + (iVar22 + (iVar14 & 63) * 128) / 2] & 0x7ff) * 128 - local_24) - iVar21,
                                                                ((terrain.vertices[terrain.chunks[(((uint)iVar14 + 1 >> 6) * 32 + uVar13)]
                                                                * 4096 + (iVar22 + (iVar14 + 1 & 63) * 128) / 2] & 0x7ff) * 128 - local_24) - iVar4,
                                                                primitives);

                                    if (iVar22 != 0)
                                    {
                                        if (iVar22 != 4)
                                        {
                                            iVar22 = Coprocessor.averageZ;
                                            newIndices[puVar23] = puVar23 + 2;
                                            newIndices[puVar23 + 1] = puVar23 + 1;
                                            newIndices[puVar23 + 2] = puVar23;
                                            puVar20 += 3;
                                                //...
                                            puVar23 += 3;
                                        }
                                        else
                                        {
                                            primitives[puVar23 + 5].verts = primitives[puVar23 + 3].verts;
                                            primitives[puVar23 + 4].verts = primitives[puVar23 + 2].verts;
                                            primitives[puVar23 + 3].verts = primitives[puVar23 + 1].verts;
                                            primitives[puVar23 + 5].uvs = primitives[puVar23 + 3].uvs;
                                            primitives[puVar23 + 4].uvs = primitives[puVar23 + 2].uvs;
                                            primitives[puVar23 + 3].uvs = primitives[puVar23 + 1].uvs;
                                            newIndices[puVar23] = puVar23 + 2;
                                            newIndices[puVar23 + 1] = puVar23 + 1;
                                            newIndices[puVar23 + 2] = puVar23;
                                            newIndices[puVar23 + 3] = puVar23 + 3;
                                            newIndices[puVar23 + 4] = puVar23 + 4;
                                            newIndices[puVar23 + 5] = puVar23 + 5;
                                            //....
                                            iVar22 = Coprocessor.averageZ;
                                            puVar20 += 6;
                                            //...
                                            puVar23 += 6;
                                        }
                                    }
                                }
                            }

                            SKIP:
                            iVar14++;
                            iVar5 += 0x100;
                            iVar16++;
                            pbVar12++;
                            psVar19++;
                            psVar9++;
                            local_2c++;
                        } while (iVar16 < local_60 - local_40);
                    }

                    iVar7++;
                    local_50 += 0x100;
                } while (iVar7 < local_5c - local_28);
            }
        }

        //int primCount = Mathf.Max(puVar23, puVar20);
        int primCount = puVar23;
        int tFactor = GameManager.instance.translateFactor2;

        for (int i = 0; i < primCount; i++)
        {
            newVertices[i] = new Vector3(primitives[i].verts.x, -primitives[i].verts.y, primitives[i].verts.z) / tFactor;
            newUVs[i] = new Vector2((float)primitives[i].uvs.x / (mainT.width - 1), 
                                    1.0f - (float)primitives[i].uvs.y / (mainT.height - 1));
        }

        mesh.Clear();
        mesh.SetVertices(newVertices, 0, primCount);
        mesh.SetColors(newColors, 0, primCount);
        mesh.SetUVs(0, newUVs, 0, primCount);
        mesh.SetTriangles(newIndices, 0, primCount, 0);
    }
}
