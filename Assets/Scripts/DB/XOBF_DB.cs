using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XOBF_DB : MonoBehaviour
{
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
    public List<byte[]> cbbList = new List<byte[]>();
    public List<Texture2D> timList = new List<Texture2D>();
    public VigConfig ini;
    public byte[] animations; //0x08

    private string prefabPath;
    private string prefabName;

    public Vehicle FUN_3C464(short param1, VehicleData param2)
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
    }

    public VigMesh FUN_2CB74(GameObject param1, uint param2)
    {
        return FUN_1FD18(param1, (ushort)ini.configContainers[(int)(param2 & 0xffff)].flag & 0x7ffU);
    }

    //FUN_32F40
    public void LoadDB(string assetPath, int param2)
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

                        if (headerString == "BIN ")
                        {
                            LoadBIN(reader);
                        }
                    } while (reader.BaseStream.Position != reader.BaseStream.Length);
                }
            }
        }

        LevelManager levelManager = GameObject.Find("GameControl").GetComponent<LevelManager>();
        levelManager.xobfList.Add(this);
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
                            lVar9 = elementPosition + facesOffset;

                            switch (bVar6 >> 2 & 15)
                            {
                                case 1:
                                case 3:
                                    writer.Write(reader.ReadBytes(17));
                                    writer.Write(0x34);
                                    writer.Write(0x34);
                                    reader.ReadBytes(2);
                                    break;
                                case 2:
                                    writer.Write(reader.ReadBytes(5));
                                    writer.Write(0x30);
                                    reader.ReadByte();
                                    writer.Write(reader.ReadBytes(4));
                                    writer.Write(0x30);
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
                                    writer.Write(reader.ReadBytes(uVar12 * 8 - 12));
                                    lVar9 += uVar12 * 8 + 12;
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
                cbbList.Add(reader.ReadBytes(elementSize));
            }
        }

        reader.BaseStream.Seek(timPosition, SeekOrigin.Begin);

        if (0 < timElements)
        {
            for (int i = 0; i < timElements; i++)
            {
                reader.BaseStream.Seek(timPosition + i * 4, SeekOrigin.Begin);
                int elementOffset = reader.ReadInt32();
                int elementSize = reader.ReadInt32() - elementOffset;
                long elementPosition = reader.BaseStream.Seek(timPosition + elementOffset, SeekOrigin.Begin);
                MemoryStream stream = new MemoryStream();

                using (BinaryReader reader2 = new BinaryReader(stream, Encoding.Default, true))
                {
                    string bmpPath = Application.dataPath + "/Resources/" + prefabPath + prefabName + ".bmp";
                    IMP_TIM.LoadTIM(reader2, bmpPath);
                    timList.Add(Resources.Load<Texture2D>(bmpPath.Substring(Application.dataPath.Length + 16)));
                }
            }
        }

        reader.BaseStream.Seek(iniPosition, SeekOrigin.Begin);

        if (0 < iniElements)
        {
            VigConfig newConfig = gameObject.AddComponent<VigConfig>();

            for (int i = 0; i < iniElements; i++)
            {
                reader.BaseStream.Seek(iniPosition + i * 4, SeekOrigin.Begin);
                ConfigContainer newContainer = new ConfigContainer();
                newContainer.flag = reader.ReadInt16();
                newContainer.colliderID = reader.ReadInt16();
                newContainer.v3_1 = new Vector3Int(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
                newContainer.v3_2 = new Vector3Int(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
                newContainer.objID = reader.ReadInt16();
                newContainer.previous = reader.ReadInt16();
                newContainer.next = reader.ReadInt16();
                newConfig.configContainers.Add(newContainer);
            }
        }
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
                            writer.Write((byte)0);
                            writer.Write((byte)0);
                            writer.Write((byte)0);
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

    public VigMesh FUN_1FD18(GameObject param1, uint param2)
    {
        byte bVar1;
        ushort uVar2;
        int iVar5;
        VigMesh pbVar3;
        VigMesh pbVar6;
        TMD puVar7;

        puVar7 = tmdList[(int)(param2 & 0xffff)];
        pbVar3 = param1.AddComponent<VigMesh>();
        pbVar6 = pbVar3;
        iVar5 = 0;

        if (puVar7.DAT_19 != 0)
        {
            pbVar3.DAT_1C = new Texture2D[puVar7.DAT_19];

            for (int i = 0; i < puVar7.DAT_19; i++)
                pbVar3.DAT_1C[i] = timList[0];
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

        return pbVar3;
    }
}
