using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum _MATERIAL
{
    Cutout, 
    Additive, 
    Billboard, 
    Lines,
    Mirror,
    CutAdd,
    Subtractive,
    Lines2, 
    Billboard2, 
    Subtractive2
}

public class XOBF_DB : MonoBehaviour
{
    [Serializable]
    public class TMD
    {
        public int vertices; //0x00
        public ushort flag; //0x02
        public int normals; //0x08
        public int faces; //0x10
        public byte DAT_18; //0x18
        public byte DAT_19; //0x19
        public ushort DAT_1A; //0x1A
        public byte[] vertexStream; //0x04
        public byte[] normalStream; //0x0C
        public byte[] faceStream; //0x14
    }
    
    public List<TMD> tmdList = new List<TMD>();
    public List<VigCollider> cbbList = new List<VigCollider>();
    public List<Texture2D> timList = new List<Texture2D>();
    public VigConfig ini;
    public byte[] animations; //0x08
    public List<AudioClip> sndList = new List<AudioClip>();
    public List<_MATERIAL> materialList = new List<_MATERIAL>();

    private string prefabPath;
    private string prefabName;
    private BufferedBinaryReader r;
    private Texture2D atlas;
    private Material matAtlas;
    private Material additive;
    private Material billboard;
    private Material subtractive;
    private Material billboard2;
    private Material subtractive2;
    private Rect[] rects;

    private void Reset()
    {
#if UNITY_EDITOR
        prefabName = name;
        prefabPath = Application.dataPath.Remove(Application.dataPath.Length - 6, 6)
            + Path.GetDirectoryName(AssetDatabase.GetAssetPath(gameObject));
        prefabPath = prefabPath.Replace("\\", "/");
#endif
    }

    public void SetAtlas()
    {
        Texture2D[] atlasTextures = new Texture2D[timList.Count];

        for (int i = 0; i < atlasTextures.Length; i++)
            atlasTextures[i] = timList[i];

        atlas = new Texture2D(1024, 1024);
        rects = atlas.PackTextures(atlasTextures, 0, 1024);
        atlas.wrapMode = TextureWrapMode.Clamp;
        atlas.filterMode = FilterMode.Point;
        matAtlas = new Material(Shader.Find("PSXEffects/PS1Shader"));
        matAtlas.mainTexture = atlas;
        matAtlas.SetFloat("_ColorOnly", 0);
        matAtlas.SetFloat("_Unlit", 1);
        matAtlas.SetFloat("_OffsetFactor", 0.5f);
        matAtlas.SetFloat("_OffsetUnits", 0.5f);
        matAtlas.SetFloat("_RenderMode", 2);
        matAtlas.SetFloat("_Cul", 1);
        matAtlas.SetFloat("_DrawDist", 0);
        additive = new Material(Shader.Find("PSXEffects/PS1Additive"));
        additive.mainTexture = atlas;
        additive.SetFloat("_ColorOnly", 0);
        additive.SetFloat("_Unlit", 1);
        additive.SetFloat("_OffsetFactor", -2);
        additive.SetFloat("_OffsetUnits", -2);
        additive.SetFloat("_RenderMode", 2);
        additive.SetFloat("_Cul", 2);
        additive.SetFloat("_DrawDist", 0);
        billboard = new Material(Shader.Find("PSXEffects/PS1Billboard"));
        billboard.mainTexture = atlas;
        billboard.SetFloat("_ColorOnly", 0);
        billboard.SetFloat("_Unlit", 1);
        billboard.SetFloat("_OffsetFactor", -2);
        billboard.SetFloat("_OffsetUnits", -2);
        billboard.SetFloat("_RenderMode", 2);
        billboard.SetFloat("_Cul", 1);
        billboard.SetFloat("_DrawDist", 0);
        subtractive = new Material(Shader.Find("PSXEffects/PS1Subtractive"));
        subtractive.mainTexture = atlas;
        subtractive.SetFloat("_ColorOnly", 0);
        subtractive.SetFloat("_Unlit", 1);
        subtractive.SetFloat("_OffsetFactor", -2);
        subtractive.SetFloat("_OffsetUnits", -2);
        subtractive.SetFloat("_RenderMode", 2);
        subtractive.SetFloat("_Cul", 1);
        subtractive.SetFloat("_DrawDist", 0);
        billboard2 = new Material(Shader.Find("PSXEffects/PS1Billboard2"));
        billboard2.mainTexture = atlas;
        billboard2.SetFloat("_ColorOnly", 0);
        billboard2.SetFloat("_Unlit", 1);
        billboard2.SetFloat("_OffsetFactor", -2);
        billboard2.SetFloat("_OffsetUnits", -2);
        billboard2.SetFloat("_RenderMode", 2);
        billboard2.SetFloat("_Cul", 1);
        billboard2.SetFloat("_DrawDist", 0);
        subtractive2 = new Material(Shader.Find("PSXEffects/PS1SubtractiveBB"));
        subtractive2.mainTexture = atlas;
        subtractive2.SetFloat("_ColorOnly", 0);
        subtractive2.SetFloat("_Unlit", 1);
        subtractive2.SetFloat("_OffsetFactor", -2);
        subtractive2.SetFloat("_OffsetUnits", -2);
        subtractive2.SetFloat("_RenderMode", 2);
        subtractive2.SetFloat("_Cul", 1);
        subtractive2.SetFloat("_DrawDist", 0);
    }

    public Material[] GetMaterialList(VigMesh mesh, int tmdID)
    {
        List<Material> materialList = new List<Material>();
        materialList.Add(LevelManager.instance.defaultMaterial);
        mesh.subMeshCount = 2;

        switch (this.materialList[tmdID])
        {
            case _MATERIAL.Cutout:
                materialList.Add(matAtlas);
                break;
            case _MATERIAL.Additive:
                materialList.Add(additive);
                break;
            case _MATERIAL.Billboard:
                materialList.Add(billboard);
                break;
            case _MATERIAL.Lines:
                mesh.topology = MeshTopology.Lines;
                mesh.subMeshCount = 1;
                break;
            case _MATERIAL.Mirror:
                materialList.Add(additive);
                mesh.mirror = true;
                break;
            case _MATERIAL.CutAdd:
                materialList.Add(matAtlas);
                materialList.Add(additive);
                mesh.subMeshCount = 3;
                break;
            case _MATERIAL.Subtractive:
                materialList.Add(subtractive);
                break;
            case _MATERIAL.Lines2:
                mesh.topology = MeshTopology.Lines;
                materialList.Add(matAtlas);
                materialList.Add(LevelManager.instance.defaultMaterial);
                mesh.subMeshCount = 3;
                break;
            case _MATERIAL.Billboard2:
                materialList.Add(billboard2);
                break;
        }
        
        mesh.mainT = rects;
        mesh.atlas = atlas;

        return materialList.ToArray();
    }

    public Smoke3 FUN_4F730(short param1, Vector3Int param2)
    {
        Smoke3 ppcVar1;

        GameObject obj = new GameObject();
        ppcVar1 = obj.AddComponent<Smoke3>();
        ppcVar1.flags = 0x20;
        ppcVar1.screen = param2;
        ppcVar1.DAT_58 = 0x10000;
        ppcVar1.ApplyTransformation();
        ppcVar1.physics1.M1 = 4;
        ppcVar1.physics1.Y = 0x200;
        ppcVar1.DAT_98 = this;
        ppcVar1.physics2.M3 = param1;
        ppcVar1.physics1.Z = -0x600;
        ppcVar1.physics1.W = 0;
        return ppcVar1;
    }

    public Smoke FUN_4F438(short param1, Vector3Int param2)
    {
        Smoke ppcVar1;

        GameObject obj = new GameObject();
        ppcVar1 = obj.AddComponent<Smoke>();
        ppcVar1.flags = 0x20;
        ppcVar1.screen = param2;
        ppcVar1.DAT_58 = 0x10000;
        ppcVar1.ApplyTransformation();
        ppcVar1.physics1.M1 = 7;
        ppcVar1.DAT_98 = this;
        ppcVar1.physics2.M3 = param1;
        ppcVar1.physics1.Y = 0x100;
        ppcVar1.physics1.Z = -0x200;
        ppcVar1.physics1.W = 0;
        ppcVar1.physics2.X = 0x400000;
        ppcVar1.physics2.M2 = 0x20;
        return ppcVar1;
    }

    public Particle8 FUN_4EC2C(short param1, sbyte param2)
    {
        Particle8 ppcVar1;

        GameObject obj = new GameObject();
        ppcVar1 = obj.AddComponent<Particle8>();
        ppcVar1.vData = this;
        ppcVar1.DAT_1A = param1;
        ppcVar1.tags = param2;
        GameManager.instance.FUN_30CB0(ppcVar1, 0);
        return ppcVar1;
    }

    public VigObject FUN_4D498(ushort param1, VigTransform param2, int param3)
    {
        ushort uVar1;
        Particle6 ppcVar2;
        ConfigContainer ccVar3;
        Particle8 pVar4;
        VigObject ppcVar5;
        byte bVar6;
        uint uVar7;
        uint uVar8;
        ConfigContainer puVar9;
        VigObject ppcVar10;
        VigTransform local_38;
        
        ppcVar10 = null;
        ppcVar5 = ppcVar10;

        if (param1 != 0xffff)
        {
            do
            {
                puVar9 = ini.configContainers[param1];
                local_38 = Utilities.FUN_2C77C(puVar9);
                local_38 = Utilities.CompMatrixLV(param2, local_38);
                ppcVar10 = ppcVar5;

                if ((puVar9.flag & 0x7ff) == 0x7ff && param3 != 0)
                {
                    ppcVar2 = ini.FUN_2C17C(param1, typeof(Particle6), 8) as Particle6;

                    if (ppcVar2 != null)
                    {
                        ppcVar2.ApplyTransformation();

                        if (ppcVar2.vAnim != null)
                            ppcVar2.state = _PARTICLE6_TYPE.Type1;

                        ppcVar10 = ppcVar2;

                        if (ppcVar5 != null)
                        {
                            ppcVar2.child = ppcVar5;
                            ppcVar5.parent = ppcVar2;
                        }
                    }

                    goto LAB_CASE_1;
                }

                switch ((uint)puVar9.flag >> 12)
                {
                    case 0:
                        if (param3 != 0 && puVar9.objID != 0xaaaa)
                        {
                            ppcVar2 = ini.FUN_2C17C(param1, typeof(Particle6), 8) as Particle6;
                            ppcVar2.ApplyTransformation();

                            if (puVar9.objID == 0)
                                if (ppcVar2.vAnim != null)
                                    ppcVar2.state = _PARTICLE6_TYPE.Type1;

                            ppcVar10 = ppcVar2;

                            if (ppcVar5 != null)
                            {
                                ppcVar2.child = ppcVar5;
                                ppcVar5.parent = ppcVar2;
                            }

                            goto LAB_CASE_1;
                        }

                        ppcVar5 = ini.FUN_2BF44(puVar9, typeof(Throwaway));
                        LAB_4D658:
                        uVar1 = (ushort)GameManager.FUN_2AC5C();
                        ppcVar5.physics1.M0 = (short)(uVar1 & 0xff);
                        uVar1 = (ushort)GameManager.FUN_2AC5C();
                        ppcVar5.physics1.M1 = (short)(uVar1 & 0xff);
                        uVar1 = (ushort)GameManager.FUN_2AC5C();
                        ppcVar5.physics1.M2 = (short)(uVar1 & 0xff);
                        goto LAB_4D680;
                    case 8:
                    case 9:
                    case 14:
                        if (puVar9.objID == 0xaaaa || puVar9.objID == 0)
                            LevelManager.instance.FUN_4D16C(this, param1, local_38);
                        else
                        {
                            GameObject obj = new GameObject();
                            ppcVar5 = obj.AddComponent<Particle7>();
                            ppcVar5.vTransform = local_38;
                            ppcVar5.vData = this;
                            ppcVar5.DAT_1A = (short)param1;
                            GameManager.instance.FUN_30CB0(ppcVar5, puVar9.objID);
                        }

                        break;
                    case 13:
                        ppcVar5 = ini.FUN_2BF44(puVar9, typeof(Throwaway));

                        if (puVar9.objID != 0) goto LAB_4D658;

                        ppcVar5.physics1.M0 = 0;
                        ppcVar5.physics1.M1 = 0;
                        ppcVar5.physics1.M2 = 0;
                        LAB_4D680:
                        uVar8 = 0x180;
                        ppcVar5.type = 7;

                        if (ppcVar5.vAnim != null)
                            uVar8 = 0x184;

                        uVar7 = ppcVar5.flags;

                        if (ppcVar5.vCollider == null)
                            uVar7 |= 0x20;

                        ppcVar5.flags = uVar7 | uVar8;
                        ppcVar5.id = (short)param3;

                        if ((uVar7 & 0x10) == 0)
                            ((Throwaway)ppcVar5).state = _THROWAWAY_TYPE.Unspawnable;
                        else
                            ((Throwaway)ppcVar5).state = _THROWAWAY_TYPE.Type3;

                        ppcVar5.physics1.Z = puVar9.v3_1.x << 8 >> 12;
                        ppcVar5.physics1.W = puVar9.v3_1.y << 8 >> 12;
                        ppcVar5.physics2.X = puVar9.v3_1.z << 8 >> 12;
                        Vector3Int v0 = Utilities.FUN_24094(param2.rotation,
                            new Vector3Int(ppcVar5.physics1.Z, ppcVar5.physics1.W, ppcVar5.physics2.X));
                        ppcVar5.physics1.Z = v0.x;
                        ppcVar5.physics1.W = v0.y;
                        ppcVar5.physics2.X = v0.z;
                        ppcVar5.vTransform = local_38;
                        ppcVar5.FUN_305FC();
                        bVar6 = 3;

                        if (0x27ff < ppcVar5.DAT_58)
                        {
                            bVar6 = 1;

                            if (ppcVar5.DAT_58 < 0x7800)
                                bVar6 = 2;
                        }

                        ((Throwaway)ppcVar5).DAT_87 = bVar6;
                        ccVar3 = ini.FUN_2C5CC(puVar9, 0x8622);

                        if (ccVar3 != null)
                        {
                            pVar4 = LevelManager.instance.xobfList[19].FUN_4EC2C(109, 8);
                            Utilities.FUN_2CA94(ppcVar5, ccVar3, pVar4);
                            pVar4.transform.parent = ppcVar5.transform;
                        }

                        break;
                }

                LAB_CASE_1:
                param1 = puVar9.previous;
                ppcVar5 = ppcVar10;
            } while (param1 != 0xffff);
        }

        return ppcVar10;
    }

    public Vehicle FUN_3C464(ushort param1, VehicleData param2, Type param3, bool bodyParts = false)
    {
        _VEHICLE eVar1;
        byte bVar2;
        byte bVar3;
        VigObject pcVar4;
        sbyte sVar5;
        short sVar6;
        Vehicle ppcVar7;
        ConfigContainer ccVar8;
        ConfigContainer ccVar9;
        uint uVar10;
        uint uVar11;
        int iVar14;
        int iVar15;
        VigObject pcVar16;
        ushort local_20;
        byte[] local_28;

        local_20 = param1;

        if (!bodyParts)
            ppcVar7 = ini.FUN_2C17C(param1, param3, (uint)(animations.Length > 0 ? 1 : 0) << 3) as Vehicle;
        else
            ppcVar7 = ini.FUN_2C17C_2(param1, param3, (uint)(animations.Length > 0 ? 1 : 0) << 3) as Vehicle;

        uVar11 = param2.DAT_0C;

        if ((param2.DAT_0C & 240) == 0)
            uVar11 |= 48;

        ppcVar7.id = 0;
        ppcVar7.type = 2;
        ppcVar7.maxHalfHealth = param2.maxHalfHealth;
        eVar1 = param2.vehicleID;
        ppcVar7.DAT_E0 = 1024;
        ppcVar7.vehicle = eVar1;
        ppcVar7.lightness = param2.lightness;

        if (animations.Length > 0)
            ppcVar7.flags |= 4;

        ppcVar7.DAT_E4 = -ppcVar7.screen.y;
        pcVar16 = ppcVar7.child2;
        ppcVar7.body = new VigObject[4];
        Utilities.ParentChildren(ppcVar7, ppcVar7);

        for (pcVar4 = pcVar16; pcVar4 != null; pcVar4 = pcVar16)
        {
            pcVar16 = pcVar4.child;

            if ((ushort)pcVar4.id < 4)
            {
                ppcVar7.body[pcVar4.id] = pcVar4;
                sVar5 = (sbyte)pcVar4.FUN_4DCD8();
                pcVar4.tags = (sbyte)(sVar5 + 1);
                pcVar4.maxHalfHealth = param2.maxHalfHealth;
            }
        }

        ppcVar7.wheels = new Wheel[6];

        for (int i = 0; i < ppcVar7.wheels.Length; i++)
        {
            ccVar8 = ini.FUN_2C590(local_20, i - 0x8000 & 0xffff);

            if (ccVar8 != null)
            {
                ccVar9 = ini.FUN_2C6D0(ccVar8, 0);

                if (ccVar9 == null)
                {
                    uVar10 = 12;

                    if ((GameManager.instance.DAT_40 & 1) == 0)
                        uVar10 = (ushort)param2.DAT_00[(i < 2 ? 1 : 0) ^ 1];

                    pcVar16 = LevelManager.instance.xobfList[18].ini.FUN_2C17C((ushort)uVar10, typeof(Wheel), 8);
                    Utilities.ParentChildren(pcVar16, pcVar16);
                    pcVar16.physics2.X = -LevelManager.instance.xobfList[18].ini.configContainers[(int)uVar10].v3_1.y;
                    sVar6 = (short)GameManager.FUN_2AC5C();
                    pcVar16.vr = new Vector3Int(sVar6, 0, (i & 1) << 11);
                }
                else
                {
                    sVar6 = (short)ini.FUN_2C73C(ccVar9);
                    pcVar16 = ini.FUN_2C17C((ushort)sVar6, typeof(Wheel), 8);
                    Utilities.ParentChildren(pcVar16, pcVar16);
                    pcVar16.physics2.X = -(ppcVar7.screen.y + ccVar8.v3_1.y + ccVar9.v3_1.y);
                }

                pcVar16.transform.parent = ppcVar7.transform;
                pcVar16.id = pcVar16.DAT_1A;
                pcVar16.screen = ccVar8.v3_1;
                Utilities.FUN_2CC48(ppcVar7, pcVar16);
                ppcVar7.wheels[i] = (Wheel)pcVar16;
                ccVar8 = ini.FUN_2C5CC(ccVar8, 0x8000);
                pcVar16.type = 9;

                if (ccVar8 == null)
                    pcVar16.physics1.X = 0;
                else
                    pcVar16.physics1.X = ccVar8.v3_1.y;

                pcVar16.physics1.Y = pcVar16.screen.y;
                pcVar16.physics1.M6 = param2.DAT_00[(i >> 1) + 2];
                pcVar16.physics1.M7 = param2.DAT_00[(i >> 1) + 4]; //in original game goes beyond size of the array, can cause bugs probably?
                
                if (pcVar16.vMesh != null)
                {
                    if ((pcVar16.flags & 0x10) == 0)
                    {
                        iVar14 = pcVar16.physics2.X * 25734;

                        if (iVar14 < 0)
                            iVar14 += 4095;

                        pcVar16.physics2.Y = 0x1000000 / (iVar14 >> 12);
                    }
                    else
                    {
                        pcVar16.flags &= 0xffffffef;
                        pcVar16.physics2.Y = 0;
                    }
                }

                if ((GameManager.instance.DAT_40 & 0x40000) != 0)
                    pcVar16.physics1.Y += 10240;

                pcVar16.physics1.Z = pcVar16.physics2.X;

                if (pcVar16.vAnim != null)
                    pcVar16.FUN_2FBC8(GameManager.instance.timer);

                if ((uVar11 & 16 << (i & 31)) == 0)
                    uVar10 = (uint)i << 28 | 32;
                else
                    uVar10 = (uint)i << 28 | 0x2000020;

                pcVar16.flags |= (uint)((int)uVar11 >> (i & 31) & 1) << 24 | uVar10;
                pcVar16.ApplyTransformation();
            }
        }

        ppcVar7.DAT_A0 = param2.DAT_24;
        ppcVar7.DAT_A6 = param2.DAT_2A;
        //set 0xA8-0xCA all to zero
        ppcVar7.wheelsType = _WHEELS.Ground;
        ppcVar7.direction = 1;
        ppcVar7.DAT_B3 = param2.DAT_13;
        ppcVar7.DAT_B1 = param2.DAT_0E;
        ppcVar7.DAT_B2 = param2.DAT_0F;
        ppcVar7.DAT_AF = param2.DAT_15;
        ppcVar7.DAT_C3 = param2.DAT_10;
        ppcVar7.DAT_C4 = param2.DAT_11;
        ppcVar7.DAT_C5 = param2.DAT_12;
        local_28 = new byte[4];
        Array.Copy(GameManager.DAT_A14, local_28, 4);

        do
        {
            iVar15 = 0;
            iVar14 = iVar15;

            do
            {
                iVar14++;
                bVar2 = local_28[iVar14 - 1];
                bVar3 = local_28[iVar14];

                if (param2.DAT_2C[bVar2] < param2.DAT_2C[bVar3])
                {
                    iVar15 = 1;
                    local_28[iVar14 - 1] = bVar3;
                    local_28[iVar14] = bVar2;
                }
            } while (iVar14 < 3);
        } while (iVar15 != 0);

        ppcVar7.DAT_C0 = (byte)(local_28[0] | local_28[1] << 2 | local_28[2] << 4 | local_28[3] << 6);
        pcVar16 = ppcVar7.FUN_2CA1C();
        ppcVar7.PDAT_7C = pcVar16;
        return ppcVar7;
    }

    public VigMesh FUN_2CB74(GameObject param1, uint param2, bool init)
    {
        return FUN_1FD18(param1, (ushort)ini.configContainers[(int)(param2 & 0xffff)].flag & 0x7ffU, init);
    }

    public VigMesh FUN_2CB74_2(GameObject param1, uint param2)
    {
        return FUN_1FD18_2(param1, (ushort)ini.configContainers[(int)(param2 & 0xffff)].flag & 0x7ffU);
    }

    public BufferedBinaryReader FUN_2CBB0(int param1)
    {
        int iVar1;
        BufferedBinaryReader brVar2;
        int iVar3;

        if (r == null)
            r = new BufferedBinaryReader(animations);

        brVar2 = r;
        brVar2.Seek(0, SeekOrigin.Begin);
        iVar3 = 0;

        if (brVar2.Length != 0)
        {
            iVar1 = brVar2.ReadInt32((param1 & 0xffff) * 4 + 4);

            if (iVar1 != 0)
                iVar3 += iVar1;
        }

        brVar2.Seek(iVar3, SeekOrigin.Begin);
        return brVar2;
    }

    //FUN_32F40
    public void LoadDB(string assetPath, string identifier)
    {
        using (BinaryReader reader = new BinaryReader(File.Open(assetPath, FileMode.Open)))
        {
            if (reader != null)
            {
                string headerString = new string(reader.ReadChars(4));

                if (headerString == "XOBF")
                {
                    do
                    {
                        headerString = new string(reader.ReadChars(4));
                        int size = reader.ReadInt32BE();
                        size += size % 2;

                        if (headerString == identifier)
                        {
                            if (identifier == "BIN ")
                                LoadBIN(reader);
                            else if (identifier == "ANM ")
                            {
                                animations = reader.ReadBytes(size);
#if UNITY_EDITOR
                                EditorUtility.SetDirty(gameObject);
                                PrefabUtility.RecordPrefabInstancePropertyModifications(gameObject);
#endif
                            }
                            else if (identifier == "SND ")
                                LoadSND(reader);
                            break;
                        }
                        else
                            reader.BaseStream.Seek(size, SeekOrigin.Current);
                    } while (reader.BaseStream.Position != reader.BaseStream.Length);
                }
            }
        }
    }

    //FUN_1E914
    private void LoadBIN(BinaryReader reader)
    {
        byte bVar1;
        bool bVar2;
        byte bVar6;
        long lVar9;
        ushort uVar12;
        long begin = reader.BaseStream.Position;
        int tmdElements = reader.ReadInt32();
        int tmdOffset = reader.ReadInt32();
        long tmdPosition = begin + tmdOffset;
        int cbbElements = reader.ReadInt32();
        int cbbOffset = reader.ReadInt32();
        long cbbPosition = begin + cbbOffset;
        int timElements = reader.ReadInt32();
        int timOffset = reader.ReadInt32();
        long timPosition = begin + timOffset;
        int iniElements = reader.ReadInt32();
        long iniPosition = reader.BaseStream.Position;

        reader.BaseStream.Seek(tmdPosition, SeekOrigin.Begin);

        if (0 < tmdElements)
        {
            for (int i = 0; i < tmdElements; i++)
            {
                reader.BaseStream.Seek(tmdPosition + i * 4, SeekOrigin.Begin);
                int elementOffset = reader.ReadInt32();
                long elementPosition = reader.BaseStream.Seek(tmdPosition + elementOffset, SeekOrigin.Begin);
                TMD newTMD = new TMD();
                newTMD.vertices = reader.ReadInt32();
                int verticesOffset = reader.ReadInt32();
                newTMD.normals = reader.ReadInt32();
                int normalsOffset = reader.ReadInt32();
                newTMD.faces = reader.ReadInt32();
                int facesOffset = reader.ReadInt32();
                newTMD.DAT_18 = reader.ReadByte();
                newTMD.DAT_19 = reader.ReadByte();
                newTMD.DAT_1A = reader.ReadUInt16();
                bVar2 = false;

                if (0 < newTMD.faces)
                {
                    reader.BaseStream.Seek(elementPosition + facesOffset, SeekOrigin.Begin);
                    MemoryStream stream = new MemoryStream();

                    using (BinaryWriter writer = new BinaryWriter(stream, Encoding.Default, true))
                    {
                        for (int j = 0; j < newTMD.faces; j++)
                        {
                            lVar9 = reader.BaseStream.Position;
                            writer.Write(reader.ReadByte());
                            writer.Write(reader.ReadByte());
                            writer.Write(reader.ReadByte());
                            bVar1 = reader.ReadByte();
                            bVar6 = (byte)((bVar1 & 15) << 2);

                            if ((bVar1 & 0x80) != 0)
                                bVar6 |= 0x40;

                            if ((bVar1 & 0x10) != 0)
                                bVar6 |= 2;

                            if ((bVar1 & 0x40) != 0)
                                bVar6 |= 0x80;

                            writer.Write(bVar6);
                            writer.Write((short)(reader.ReadInt16() << 3));
                            writer.Write((short)(reader.ReadInt16() << 3));
                            writer.Write((short)(reader.ReadInt16() << 3));

                            switch (bVar6 >> 2 & 15)
                            {
                                case 1:
                                case 3:
                                    writer.Write(reader.ReadBytes(17));
                                    writer.Write((byte)0x34);
                                    reader.ReadByte();
                                    writer.Write(reader.ReadBytes(3));
                                    writer.Write((byte)0x34);
                                    reader.ReadByte();
                                    break;
                                case 2:
                                    writer.Write(reader.ReadBytes(5));
                                    writer.Write((byte)0x30);
                                    reader.ReadByte();
                                    writer.Write(reader.ReadBytes(3));
                                    writer.Write((byte)0x30);
                                    reader.ReadByte();
                                    break;
                                case 4:
                                case 5:
                                case 7:
                                    bVar2 = true;
                                    writer.Write((short)(reader.ReadInt16() << 3));
                                    break;
                                case 8:
                                case 9:
                                case 11:
                                    bVar2 = true;
                                    writer.Write((short)(reader.ReadInt16() << 3));
                                    writer.Write((short)(reader.ReadInt16() << 3));
                                    writer.Write((short)(reader.ReadInt16() << 3));
                                    break;
                                case 12:
                                    writer.Write((short)(reader.ReadInt16() << 3));
                                    writer.Write((short)(reader.ReadInt16() << 3));
                                    writer.Write((short)(reader.ReadInt16() << 3));
                                    break;
                                case 10:
                                    uVar12 = reader.ReadUInt16();
                                    writer.Write(uVar12);
                                    writer.Write(reader.ReadBytes(uVar12 * 8));
                                    lVar9 += uVar12 * 8;
                                    break;
                            }

                            int remain = GameManager.DAT_854[bVar6 >> 2 & 15] -
                                (int)(reader.BaseStream.Position - lVar9);
                            writer.Write(reader.ReadBytes(remain));
                        }
                    }

                    if (bVar2)
                        newTMD.flag |= 0x8000;

                    newTMD.faceStream = stream.ToArray();
                }

                if (0 < newTMD.vertices)
                {
                    reader.BaseStream.Seek(elementPosition + verticesOffset, SeekOrigin.Begin);
                    MemoryStream stream = new MemoryStream();

                    using (BinaryWriter writer = new BinaryWriter(stream, Encoding.Default, true))
                    {
                        for (int j = 0; j < newTMD.vertices; j++)
                            writer.Write(reader.ReadBytes(8));
                    }

                    newTMD.vertexStream = stream.ToArray();
                }

                if (0 < newTMD.normals)
                {
                    reader.BaseStream.Seek(elementPosition + normalsOffset, SeekOrigin.Begin);
                    MemoryStream stream = new MemoryStream();

                    using (BinaryWriter writer = new BinaryWriter(stream, Encoding.Default, true))
                    {
                        for (int j = 0; j < newTMD.normals; j++)
                            writer.Write(reader.ReadBytes(8));
                    }

                    newTMD.normalStream = stream.ToArray();
                }

                tmdList.Add(newTMD);
            }
        }

        reader.BaseStream.Seek(cbbPosition, SeekOrigin.Begin);

        if (0 < cbbElements)
        {
            for (int i = 0; i < cbbElements; i++)
            {
                reader.BaseStream.Seek(cbbPosition + i * 4, SeekOrigin.Begin);
                int elementOffset = reader.ReadInt32();
                int elementSize = reader.ReadInt32() - elementOffset;
                long elementPosition = reader.BaseStream.Seek(cbbPosition + elementOffset, SeekOrigin.Begin);
                cbbList.Add(new VigCollider(reader.ReadBytes(elementSize)));
            }
        }

        reader.BaseStream.Seek(timPosition, SeekOrigin.Begin);
        string relativePath = prefabPath;

        if (prefabPath.StartsWith(Application.dataPath))
            relativePath = "Assets" + prefabPath.Substring(Application.dataPath.Length);

        if (0 < timElements)
        {
            for (int i = 0; i < timElements; i++)
            {
                reader.BaseStream.Seek(timPosition + i * 4, SeekOrigin.Begin);
                int elementOffset = reader.ReadInt32();
                int elementSize = reader.ReadInt32() - elementOffset;
                long elementPosition = reader.BaseStream.Seek(timPosition + elementOffset, SeekOrigin.Begin);
                string bmpApsolute = prefabPath + "/Textures/" + prefabName + "_" + i.ToString().PadLeft(4, '0') + ".bmp";
                string bmpRelative = relativePath + "/Textures/" + prefabName + "_" + i.ToString().PadLeft(4, '0') + ".bmp";
                reader.ReadInt32();
                IMP_TIM.LoadTIM(reader, bmpApsolute);
                //Material newMaterial = null;
#if UNITY_EDITOR
                AssetDatabase.Refresh();
                //newMaterial = new Material(AssetDatabase.LoadAssetAtPath(relativePath + "/default.mat", typeof(Material)) as Material);
                //newMaterial.mainTexture = AssetDatabase.LoadAssetAtPath(bmpRelative, typeof(Texture2D)) as Texture2D;
                //Utilities.SaveObjectToFile(newMaterial, matPath);
                timList.Add(AssetDatabase.LoadAssetAtPath(bmpRelative, typeof(Texture2D)) as Texture2D);
#endif
            }
        }

        reader.BaseStream.Seek(iniPosition, SeekOrigin.Begin);

        if (0 < iniElements)
        {
            VigConfig newConfig = gameObject.AddComponent<VigConfig>();
            ini = newConfig;
            newConfig.configContainers = new List<ConfigContainer>();
            newConfig.xobf = this;

            for (int i = 0; i < iniElements; i++)
            {
                ConfigContainer newContainer = new ConfigContainer();
                newContainer.flag = reader.ReadUInt16();
                newContainer.colliderID = reader.ReadInt16();
                newContainer.v3_1 = new Vector3Int(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
                newContainer.v3_2 = new Vector3Int(reader.ReadInt16(), reader.ReadInt16(), reader.ReadInt16());
                newContainer.objID = reader.ReadUInt16();
                newContainer.previous = reader.ReadUInt16();
                newContainer.next = reader.ReadUInt16();
                newConfig.configContainers.Add(newContainer);
            }
        }

#if UNITY_EDITOR
        EditorUtility.SetDirty(gameObject);
        PrefabUtility.RecordPrefabInstancePropertyModifications(gameObject);
#endif
    }

    private void LoadSND(BinaryReader reader)
    {
#if UNITY_EDITOR
        long startPosition = reader.BaseStream.Position;
        int elementsCount = reader.ReadUInt16();
        int eof = reader.ReadUInt16() * 8;
        long elementsPosition = reader.BaseStream.Position + elementsCount * 4;
        double[,] f = { {   0.0, 0.0 },
                        {  60.0 / 64.0, 0.0 },
                        { 115.0 / 64.0, -52.0 / 64.0 },
                        {  98.0 / 64.0, -55.0 / 64.0 },
                        { 122.0 / 64.0, -60.0 / 64.0 } };
        double[] samples = new double[28];
        prefabName = name;
        prefabPath = Application.dataPath.Remove(Application.dataPath.Length - 6, 6)
            + Path.GetDirectoryName(AssetDatabase.GetAssetPath(gameObject));
        prefabPath = prefabPath.Replace("\\", "/");
        string relativePath = prefabPath;

        if (prefabPath.StartsWith(Application.dataPath))
            relativePath = "Assets" + prefabPath.Substring(Application.dataPath.Length);

        for (int i = 0; i < elementsCount; i++)
        {
            int samplesCount = 0;
            int loopBegin = 0;
            int loopEnd = 0;
            double s_1 = 0.0;
            double s_2 = 0.0;
            string wavApsolute = prefabPath + "/Sounds/" + prefabName + "_" + i.ToString().PadLeft(4, '0') + ".wav";
            string wavRelative = relativePath + "/Sounds/" + prefabName + "_" + i.ToString().PadLeft(4, '0') + ".wav";

            using (BinaryWriter writer = new BinaryWriter(File.Open(wavApsolute, FileMode.Create)))
            {
                reader.BaseStream.Seek(startPosition, SeekOrigin.Begin);
                reader.BaseStream.Seek(i * 4 + 4, SeekOrigin.Current);
                int elementOffset = reader.ReadUInt16() * 8;
                uint samp_freq = reader.ReadUInt16() * 11U;
                int nextOffset = reader.ReadUInt16() * 8;
                if (i == elementsCount - 1) nextOffset = eof;
                reader.BaseStream.Seek(elementsPosition + elementOffset, SeekOrigin.Begin);
                //reader.BaseStream.Seek(16, SeekOrigin.Current); //vag name?
                writer.Write(0x46464952); //RIFF
                writer.Write(0); //skip file size for now
                writer.Write(0x45564157); //WAVE
                writer.Write(0x20746D66); //fmt
                writer.Write(0x10);
                writer.Write((short)1);
                writer.Write((short)1);
                writer.Write((byte)samp_freq);
                writer.Write((byte)(samp_freq >> 8));
                writer.Write((short)0);
                writer.Write(samp_freq * 2);
                writer.Write((short)2);
                writer.Write((short)16);
                writer.Write(0x6C706D73); //smpl
                writer.Write(60);
                writer.Write(0);
                writer.Write(0);
                writer.Write(0);
                writer.Write(60);
                writer.Write(0);
                writer.Write(0);
                writer.Write(0);
                writer.Write(1);
                writer.Write(0);
                writer.Write(1);
                writer.Write(1);
                writer.Write(0);
                writer.Write(0);
                writer.Write(0);
                writer.Write(0);
                writer.Write(0x61746164); //data
                writer.Write(0); //skip SubChunk2Size for now

                while (reader.BaseStream.Position < elementsPosition + nextOffset)
                {
                    int predict_nr = reader.ReadByte();
                    int shift_factor = predict_nr & 0xf;
                    predict_nr >>= 4;
                    int flags = reader.ReadByte();

                    if (flags == 7)
                        break;
                    else if (flags == 6)
                        loopBegin = samplesCount;
                    else if (flags == 3)
                        loopEnd = samplesCount + 28;

                    for (int j = 0; j < 28; j += 2)
                    {
                        int d = reader.ReadByte();
                        int s = (d & 0xf) << 12;

                        if ((s & 0x8000) != 0)
                            s |= -0x10000;

                        samples[j] = s >> shift_factor;
                        s = (d & 0xf0) << 8;

                        if ((s & 0x8000) != 0)
                            s |= -0x10000;

                        samples[j + 1] = s >> shift_factor;
                    }

                    for (int j = 0; j < 28; j++)
                    {
                        samplesCount++;
                        samples[j] = samples[j] + s_1 * f[predict_nr, 0] + s_2 * f[predict_nr, 1];
                        s_2 = s_1;
                        s_1 = samples[j];
                        int d = (int)(samples[j] + 0.5);
                        writer.Write((short)d);
                    }
                }

                if (loopEnd == 0)
                    loopEnd = samplesCount;

                long sz = writer.BaseStream.Length;
                writer.BaseStream.Seek(4, SeekOrigin.Begin);
                writer.Write((int)(sz - 8));
                writer.BaseStream.Seek(108, SeekOrigin.Begin);
                writer.Write((int)(sz - 44));
                writer.BaseStream.Seek(88, SeekOrigin.Begin);
                writer.Write(loopBegin);
                writer.Write(loopEnd);
            }

            AssetDatabase.Refresh();
            sndList.Add(AssetDatabase.LoadAssetAtPath(wavRelative, typeof(AudioClip)) as AudioClip);
        }
        EditorUtility.SetDirty(gameObject);
        PrefabUtility.RecordPrefabInstancePropertyModifications(gameObject);
#endif
    }

    private void FUN_1F2FC(uint param2)
    {
        //load VRAM data
    }

    private void FUN_1F6AC(TMD param1)
    {
        byte bVar1;
        int iVar3;
        MemoryStream puVar6;
        long puVar7;
        puVar6 = new MemoryStream(param1.faceStream);
        param1.flag |= 0x4000;

        using (BinaryReader reader = new BinaryReader(puVar6, Encoding.Default, true))
        {
            using (BinaryWriter writer = new BinaryWriter(puVar6, Encoding.Default, true))
            {
                if (0 < param1.faces)
                {
                    for (int i = 0; i < param1.faces; i++)
                    {
                        puVar7 = reader.BaseStream.Position;
                        reader.BaseStream.Seek(3, SeekOrigin.Current);
                        bVar1 = reader.ReadByte();

                        if ((bVar1 & 0x80) != 0)
                        {
                            writer.Write(-4, (byte)0);
                            writer.Write(-3, (byte)0);
                            writer.Write(-2, (byte)0);
                        }

                        switch((uint)bVar1 >> 2 & 15)
                        {
                            case 1:
                            case 5:
                                break;
                            case 9:
                                break;
                            case 10:
                                puVar7 += reader.ReadUInt16(6) * 8;
                                break;
                            case 12:
                                break;
                            case 13:
                                break;
                        }

                        reader.BaseStream.Seek(puVar7, SeekOrigin.Begin);
                        reader.BaseStream.Seek(GameManager.DAT_854[bVar1 >> 2 & 15], SeekOrigin.Current);
                    }
                }
            }
        }
    }

    /*public Material FUN_1F288(uint param1)
    {
        Material pcVar2;

        pcVar2 = timList[(int)param1 & 0xffff];
        //...

        return pcVar2;
    }*/

    public VigMesh FUN_1FD18(GameObject param1, uint param2, bool init)
    {
        byte bVar1;
        ushort uVar2;
        int iVar3;
        int iVar4;
        int iVar5;
        VigMesh pbVar3;
        VigMesh pbVar6;
        TMD puVar7;
        long lVar8;
        LevelManager levelManager = LevelManager.instance;
        List<int> materialIDs = new List<int>();

        if (levelManager == null)
            levelManager = GameObject.FindObjectOfType<LevelManager>();

        puVar7 = tmdList[(int)(param2 & 0xffff)];
        pbVar3 = param1.AddComponent<VigMesh>();
        pbVar3.tmdID = param2;
        pbVar3.topology = MeshTopology.Triangles;
        MeshFilter meshFilter = param1.GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = param1.GetComponent<MeshRenderer>();
        List<Material> materialList;
        pbVar3.subMeshCount = 2;

        if (meshFilter == null)
        {
            meshFilter = param1.AddComponent<MeshFilter>();
            meshFilter.mesh = new Mesh();
            meshRenderer = param1.AddComponent<MeshRenderer>();
            meshRenderer.receiveShadows = false;
            meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        }

        materialList = new List<Material>();
        materialList.Add(levelManager.defaultMaterial);

        switch (this.materialList[(int)param2])
        {
            case _MATERIAL.Cutout:
                materialList.Add(matAtlas);
                break;
            case _MATERIAL.Additive:
                materialList.Add(additive);
                break;
            case _MATERIAL.Billboard:
                materialList.Add(billboard);
                break;
            case _MATERIAL.Lines:
                pbVar3.topology = MeshTopology.Lines;
                pbVar3.subMeshCount = 1;
                break;
            case _MATERIAL.Mirror:
                materialList.Add(additive);
                pbVar3.mirror = true;
                break;
            case _MATERIAL.CutAdd:
                materialList.Add(matAtlas);
                materialList.Add(additive);
                pbVar3.subMeshCount = 3;
                break;
            case _MATERIAL.Subtractive:
                materialList.Add(subtractive);
                break;
            case _MATERIAL.Lines2:
                pbVar3.topology = MeshTopology.Lines;
                materialList.Add(matAtlas);
                materialList.Add(levelManager.defaultMaterial);
                pbVar3.subMeshCount = 3;
                break;
            case _MATERIAL.Billboard2:
                materialList.Add(billboard2);
                break;
            case _MATERIAL.Subtractive2:
                materialList.Add(subtractive2);
                break;
        }

        pbVar6 = pbVar3;
        iVar5 = 0;
        pbVar3.DAT_1C = new int[16]; //not in the original code

        if (puVar7.DAT_19 != 0)
        {
            //pbVar3.DAT_1C = new int[puVar7.DAT_19];

            for (int i = 0; i < puVar7.DAT_19; i++)
            {
                pbVar3.DAT_1C[i] = 0;

                if (!materialIDs.Contains(pbVar3.DAT_1C[i]))
                    materialIDs.Add(pbVar3.DAT_1C[i]);
            }
        }

        pbVar3.DAT_00 = (byte)(((uint)(short)puVar7.flag >> 15) & 1);
        pbVar3.vertices = (ushort)puVar7.vertices;
        pbVar3.vertexStream = puVar7.vertexStream;
        pbVar3.normalStream = puVar7.normalStream;
        pbVar3.faces = (ushort)puVar7.faces;
        pbVar3.faceStream = puVar7.faceStream;
        bVar1 = puVar7.DAT_18;
        pbVar3.DAT_02 = 0;
        pbVar3.DAT_01 = bVar1;
        uVar2 = puVar7.DAT_1A;
        bVar1 = puVar7.DAT_18;
        pbVar3.DAT_14 = null;
        pbVar3.DAT_18 = (uint)uVar2 << (16 - bVar1 & 31);

        if ((puVar7.flag & 0x4000) == 0)
            FUN_1F6AC(puVar7);

        MemoryStream stream = new MemoryStream(pbVar3.faceStream);

        using (BinaryReader reader = new BinaryReader(stream, Encoding.Default, true))
        {
            if (0 < pbVar3.faces)
            {
                for (int i = 0; i < pbVar3.faces; i++)
                {
                    lVar8 = reader.BaseStream.Position;
                    bVar1 = reader.ReadByte(3);

                    switch ((uint)bVar1 >> 2 & 15)
                    {
                        case 1:
                        case 5:
                            iVar5 = reader.ReadUInt16(22) & 0x3fff;
                            break;
                        case 9:
                            iVar5 = reader.ReadUInt16(26) & 0x3fff;
                            break;
                        case 10:
                            iVar3 = 0;

                            if (reader.ReadUInt16(10) != 0)
                            {
                                iVar4 = 16;

                                do
                                {
                                    iVar3++;
                                    iVar5 = reader.ReadUInt16(iVar4 - 2) & 0x3fff;

                                    if (!materialIDs.Contains(iVar5))
                                        materialIDs.Add(iVar5);

                                    iVar4 += 8;
                                } while (iVar3 < reader.ReadUInt16(10));
                            }

                            lVar8 += reader.ReadUInt16(10) * 8;
                            break;
                        case 12:
                            if ((reader.ReadUInt16(16) & 0x3fff) == 0x3ffe)
                                iVar5 = 0x3ffe;
                            else
                            {
                                if ((reader.ReadUInt16(16) & 0x3fff) == 0x3fff)
                                    iVar5 = 0x3fff;
                                else
                                    iVar5 = reader.ReadUInt16(16) & 0x3fff;
                            }

                            break;
                        case 13:
                            if (reader.ReadUInt16(22) == 0xffff)
                                iVar5 = 0xffff;
                            else
                                iVar5 = reader.ReadUInt16(22) & 0x3fff;
                            break;
                    }

                    if (!materialIDs.Contains(iVar5))
                        materialIDs.Add(iVar5);
                    
                    reader.BaseStream.Seek(lVar8, SeekOrigin.Begin);
                    reader.BaseStream.Seek(GameManager.DAT_854[bVar1 >> 2 & 15], SeekOrigin.Current);
                }
            }
        }

        if (init)
        {
            pbVar3.materialIDs = new Dictionary<int, int>();

            if (pbVar3.mainT != rects)
            {
                pbVar3.mainT = rects;
                pbVar3.atlas = atlas;
            }

            /*for (int i = 0; i < materialIDs.Count; i++)
            {
                if (materialIDs[i] == 0xffff ||
                    materialIDs[i] == 0x3fff)
                {
                    materialList.Add(levelManager.DAT_E48);
                    pbVar3.materialIDs.Add(materialIDs[i], materialList.Count - 1);
                }
                else if (materialIDs[i] == 0x3ffe)
                {
                    materialList.Add(levelManager.DAT_E58);
                    pbVar3.materialIDs.Add(materialIDs[i], materialList.Count - 1);
                }
            }*/

            meshRenderer.sharedMaterials = materialList.ToArray();
            pbVar3.Initialize();
        }
        return pbVar3;
    }

    public VigMesh FUN_1FD18_2(GameObject param1, uint param2)
    {
        byte bVar1;
        ushort uVar2;
        VigMesh pbVar3;
        VigMesh pbVar6;
        TMD puVar7;
        LevelManager levelManager = LevelManager.instance;
        List<int> materialIDs = new List<int>();

        if (levelManager == null)
            levelManager = GameObject.FindObjectOfType<LevelManager>();

        puVar7 = tmdList[(int)(param2 & 0xffff)];
        pbVar3 = param1.AddComponent<VigMesh>();
        pbVar3.tmdID = param2;
        pbVar3.topology = MeshTopology.Triangles;
        pbVar3.subMeshCount = 1;

        pbVar6 = pbVar3;
        pbVar3.DAT_1C = new int[16]; //not in the original code

        if (puVar7.DAT_19 != 0)
        {
            //pbVar3.DAT_1C = new int[puVar7.DAT_19];

            for (int i = 0; i < puVar7.DAT_19; i++)
            {
                pbVar3.DAT_1C[i] = 0;

                if (!materialIDs.Contains(pbVar3.DAT_1C[i]))
                    materialIDs.Add(pbVar3.DAT_1C[i]);
            }
        }

        pbVar3.DAT_00 = (byte)(((uint)(short)puVar7.flag >> 15) & 1);
        pbVar3.vertices = (ushort)puVar7.vertices;
        pbVar3.vertexStream = puVar7.vertexStream;
        pbVar3.normalStream = puVar7.normalStream;
        pbVar3.faces = (ushort)puVar7.faces;
        pbVar3.faceStream = puVar7.faceStream;
        bVar1 = puVar7.DAT_18;
        pbVar3.DAT_02 = 0;
        pbVar3.DAT_01 = bVar1;
        uVar2 = puVar7.DAT_1A;
        bVar1 = puVar7.DAT_18;
        pbVar3.DAT_14 = null;
        pbVar3.DAT_18 = (uint)uVar2 << (16 - bVar1 & 31);

        if ((puVar7.flag & 0x4000) == 0)
            FUN_1F6AC(puVar7);
    
        pbVar3.Initialize2();

        return pbVar3;
    }
}
