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

    private string prefabPath = "";
    private string prefabName = "";

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

    private VigMesh FUN_1FD18(GameObject param1, uint param2)
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
