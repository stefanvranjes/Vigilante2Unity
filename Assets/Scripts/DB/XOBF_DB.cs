using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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
    public List<Material> timList = new List<Material>();
    public VigConfig ini;
    public byte[] animations; //0x08

    private string prefabPath;
    private string prefabName;

    private void Reset()
    {
        prefabName = name;
        prefabPath = Application.dataPath.Remove(Application.dataPath.Length - 6, 6)
            + Path.GetDirectoryName(AssetDatabase.GetAssetPath(gameObject));
        prefabPath = prefabPath.Replace("\\", "/");
    }

    /*public Vehicle FUN_3C464(short param1, VehicleData param2)
    {
        VigObject currentObj = config.FUN_2C17C(0, 308, 0); //r20
        int configID = config.dataID;
        int iVar1 = GameManager.vehicleConfigs[configID].unk0xC;

        if ((iVar1 & 240) == 0)
            iVar1 |= 48;

        currentObj.id = 0;
        currentObj.type = 2;
        currentObj.maxHalfHealth = GameManager.vehicleConfigs[configID].maxHalfHealth;
        vehicle = GameManager.vehicleConfigs[configID].vehicleID;
        DAT_E0 = 0x400;
        lightness = GameManager.vehicleConfigs[configID].lightness;
        int iVar2 = iVar1 & 255; //r30

        if (config.pointerUnk1 != 0)
            currentObj.flags |= 4;

        VigObject child2 = currentObj.child2; //r16
        DAT_E4 = -currentObj.screen.y;

        while (child2 != null)
        {
            VigObject child = child2.child; //r17

            if ((ushort)child2.id < 4)
            {
                body[(child2.id << 16 >> 14) / 4] = child2;
                child2.ai = (byte)(child2.FUN_4DCD8() + 1);
                child2.maxHalfHealth = GameManager.vehicleConfigs[configID].maxHalfHealth;
            }

            child2 = child;
        }

        int iVar3 = 0; //r18
        VigObject wheelObject; //r16
        ConfigContainer nextContainer; //r19
        ConfigContainer previousContainer; //r17
        GameManager.instance.commonWheelConfiguration.currentID = 0;
        wheels.CopyTo(GameManager.instance.commonWheelConfiguration.obj, 0);

        for (int i = 0; i < wheels.Length; i++)
        {
            nextContainer = config.FUN_2C590(0, iVar3 - 0x8000 & 0xFFFF);

            if (nextContainer != null)
            {
                previousContainer = config.FUN_2C6D0(nextContainer, 0);

                if (previousContainer == null)
                {
                    int iVar = 12; //r16

                    if ((GameManager.instance.DAT_40 & 1) == 0)
                        iVar = GameManager.vehicleConfigs[configID].unk0x0[(((iVar3 < 2 ? 1 : 0) ^ 1) << 1) / 2];

                    wheelObject = GameManager.instance.commonWheelConfiguration.FUN_2C17C(iVar, 156, 8); //r16
                    int configIndex = (iVar << 3) - iVar << 2;
                    wheelObject.physics2.X = -GameManager.instance.commonWheelConfiguration.configContainers[configIndex / 0x1C].v3_1.y;
                    wheelObject.vr = new Vector3Int((int)GameManager.FUN_2AC5C(), 0, (iVar3 & 1) << 11);
                }
                else
                {
                    wheelObject = config.FUN_2C17C(config.FUN_2C73C(previousContainer) & 0xFFFF, 156, 8);
                    wheelObject.physics2.X = -(currentObj.screen.y + previousContainer.v3_1.y + nextContainer.v3_1.y);
                }

                wheelObject.id = wheelObject.DAT_1A;
                wheelObject.screen = nextContainer.v3_1;
                Utilities.FUN_2CC48(currentObj, wheelObject);
                wheels[i] = wheelObject;
                nextContainer = config.FUN_2C5CC(nextContainer, 0x8000);
                wheelObject.type = 9;

                if (nextContainer == null)
                    wheelObject.physics1.X = 0;
                else
                    wheelObject.physics1.X = nextContainer.v3_1.y;

                wheelObject.physics1.Y = wheelObject.screen.y;
                int index = (iVar3 >> 1 << 1) / 2;
                wheelObject.physics1.M6 = GameManager.vehicleConfigs[configID].unk0x0[index + 2];
                wheelObject.physics1.M7 = GameManager.vehicleConfigs[configID].unk0x0[index + 4];

                if (wheelObject.vMesh != null)
                {
                    if ((wheelObject.flags & 16) == 0)
                    {
                        int iVar5 = wheelObject.physics2.X * 0x6486;

                        if (iVar5 < 0)
                            iVar5 += 0xFFF;

                        wheelObject.physics2.Y = 0x1000000 / (iVar5 >> 12);
                    }
                    else
                    {
                        wheelObject.flags &= 0xffffffef;
                        wheelObject.physics2.Y = 0;
                    }
                }

                if ((GameManager.instance.DAT_40 & 0x40000) != 0)
                    wheelObject.physics1.Y += 0x2800;

                wheelObject.physics1.Z = wheelObject.physics2.X;

                if (wheelObject.unk3 != 0)
                {
                    //...
                }

                iVar1 = (iVar2 >> iVar3 & 1) << 24;
                int iVar4;

                if ((iVar2 & 16 << iVar3) == 0)
                    iVar4 = iVar3 << 28 | 48;
                else
                    iVar4 = iVar3 << 28 | 0x2000020;

                iVar4 = iVar1 | iVar4;
                wheelObject.flags |= (uint)iVar4;
                wheelObject.ApplyTransformation();
            }

            iVar3++;
        }

        currentObj.vectorUnk1 = GameManager.vehicleConfigs[configID].vectorUnk;
        currentObj.DAT_A6 = GameManager.vehicleConfigs[configID].unk0x2A;
        wheelsType = _WHEELS.Ground;
        direction = 1;
        DAT_B3 = GameManager.vehicleConfigs[configID].unk0x13;
        DAT_B1 = GameManager.vehicleConfigs[configID].unk0xE;
        DAT_B2 = GameManager.vehicleConfigs[configID].unk0xF;
        DAT_AF = GameManager.vehicleConfigs[configID].unk0x15;
        DAT_C3 = GameManager.vehicleConfigs[configID].unk0x10;
        DAT_C4 = GameManager.vehicleConfigs[configID].unk0x11;
        DAT_C5 = GameManager.vehicleConfigs[configID].unk0x12;
        byte[] local_28 = new byte[4]; //sp+10h
        Array.Copy(GameManager.DAT_6B204, local_28, 4);
        int iVar6;

        do
        {
            iVar3 = 0;
            iVar6 = iVar3;

            do
            {
                int iVar5 = iVar3 + 1;
                byte bVar1 = GameManager.vehicleConfigs[configID].unk0x2C[local_28[iVar3]];
                byte bVar2 = GameManager.vehicleConfigs[configID].unk0x2C[local_28[iVar5]];
                byte bVar3 = local_28[iVar3];
                byte bVar4 = local_28[iVar5];

                if (bVar1 < bVar2)
                {
                    iVar6 = 1;
                    local_28[iVar3] = bVar4;
                    local_28[iVar5] = bVar3;
                }

                iVar3 = iVar5;
            } while (iVar3 < 3);
        } while (iVar6 != 0);

        DAT_C0 = (byte)(local_28[0] | local_28[1] << 2 | local_28[2] << 4 | local_28[3] << 6);
        currentObj.PDAT_7C = currentObj.FUN_2CA1C();
    }*/

    public Vehicle FUN_3C464(ushort param1, VehicleData param2)
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
        ppcVar7 = ini.FUN_2C17C(param1, typeof(Vehicle), (uint)(animations.Length > 0 ? 1 : 0) << 3) as Vehicle;
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
        ppcVar7.body = new VigObject[2];

        for (pcVar4 = pcVar16; pcVar4 != null; pcVar4 = pcVar16)
        {
            pcVar16 = pcVar4.child;

            if (pcVar4.id < 4)
            {
                ppcVar7.body[pcVar4.id] = pcVar4;
                pcVar4.transform.parent = ppcVar7.transform;
                sVar5 = (sbyte)pcVar4.FUN_4DCD8();
                pcVar4.ai = (byte)(sVar5 + 1);
                pcVar4.maxHalfHealth = param2.maxHalfHealth;
            }
        }

        ppcVar7.wheels = new VigObject[6];

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

                    pcVar16 = LevelManager.instance.wheels.ini.FUN_2C17C((ushort)uVar10, typeof(VigObject), 8);
                    pcVar16.physics2.X = -LevelManager.instance.wheels.ini.configContainers[(int)uVar10].v3_1.y;
                    sVar6 = (short)GameManager.FUN_2AC5C();
                    pcVar16.vr = new Vector3Int(sVar6, 0, (i & 1) << 11);
                }
                else
                {
                    sVar6 = (short)ini.FUN_2C73C(ccVar9);
                    pcVar16 = ini.FUN_2C17C((ushort)sVar6, typeof(VigObject), 8);
                    pcVar16.physics2.X = -(ppcVar7.screen.y + ccVar8.v3_1.y + ccVar9.v3_1.y);
                }

                pcVar16.transform.parent = ppcVar7.transform;
                pcVar16.id = pcVar16.DAT_1A;
                pcVar16.screen = ccVar8.v3_1;
                Utilities.FUN_2CC48(ppcVar7, pcVar16);
                ppcVar7.wheels[i] = pcVar16;
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

                if (pcVar16.DAT_64 != 0)
                    ; //animations

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

                        if (headerString == identifier)
                        {
                            if (identifier == "BIN ")
                                LoadBIN(reader);

                            break;
                        }
                        else
                            reader.BaseStream.Seek(size + ((-(size % 4) + 4) % 4), SeekOrigin.Current);
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
                string matPath = relativePath + "/Materials/" + prefabName + "_" + i.ToString().PadLeft(4, '0') + ".mat";
                reader.ReadInt32();
                IMP_TIM.LoadTIM(reader, bmpApsolute);
                AssetDatabase.Refresh();
                Material newMaterial = new Material(AssetDatabase.LoadAssetAtPath(relativePath + "/default.mat", typeof(Material)) as Material);
                newMaterial.mainTexture = AssetDatabase.LoadAssetAtPath(bmpRelative, typeof(Texture2D)) as Texture2D;
                Utilities.SaveObjectToFile(newMaterial, matPath);
                timList.Add(newMaterial);
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

        EditorUtility.SetDirty(gameObject);
        PrefabUtility.RecordPrefabInstancePropertyModifications(gameObject);
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
                                puVar7 += reader.ReadUInt16() * 8;
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
        MeshFilter meshFilter = param1.AddComponent<MeshFilter>();
        meshFilter.mesh = new Mesh();
        MeshRenderer meshRenderer = param1.AddComponent<MeshRenderer>();
        pbVar6 = pbVar3;
        iVar5 = 0;

        if (puVar7.DAT_19 != 0)
        {
            pbVar3.DAT_1C = new int[puVar7.DAT_19];

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
                                iVar4 = 10;

                                do
                                {
                                    iVar3++;
                                    iVar5 = reader.ReadUInt16(iVar4 - 2) & 0x3fff;

                                    if (!materialIDs.Contains(iVar5))
                                        materialIDs.Add(iVar5);

                                    iVar5 += 8;
                                } while (iVar3 < reader.ReadUInt16(10));
                            }

                            lVar8 += reader.ReadUInt16() * 8;
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

        List<Material> materialList = new List<Material>();
        materialList.Add(levelManager.defaultMaterial);

        for (int i = 0; i < materialIDs.Count; i++)
        {
            if (materialIDs[i] == 0xffff ||
                materialIDs[i] == 0x3fff)
                materialList.Add(levelManager.DAT_E48);
            else if (materialIDs[i] == 0x3ffe)
                materialList.Add(levelManager.DAT_E58);
            else
                materialList.Add(timList[materialIDs[i]]);
        }
        
        meshRenderer.sharedMaterials = materialList.ToArray();
        pbVar3.materialIDs = materialIDs.ToArray();

        if (init)
            pbVar3.Initialize();

        return pbVar3;
    }
}
