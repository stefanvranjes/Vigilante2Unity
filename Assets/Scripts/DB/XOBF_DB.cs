using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XOBF_DB : MonoBehaviour
{
    public class TMD
    {
        public int vertices;
        public ushort flag;
        public int normals;
        public int faces;
        public byte[] vertexStream;
        public byte[] normalStream;
        public byte[] faceStream;
    }
    
    public List<TMD> tmdList = new List<TMD>();
    public List<byte[]> cbbList = new List<byte[]>();
    public List<Texture2D> timList = new List<Texture2D>();
    public List<byte[]> iniList = new List<byte[]>();

    private string prefabPath = "";
    private string prefabName = "";

    public void FUN_2CB74(uint param2)
    {
        MemoryStream stream = new MemoryStream(iniList[(int)(param2 & 0xffff)]);

        using (BinaryReader reader = new BinaryReader(stream, Encoding.Default, true))
        {
            FUN_1FD18(reader.ReadUInt16() & 0x7ffU);
        }
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
            for (int i = 0; i < iniElements; i++)
            {
                reader.BaseStream.Seek(iniPosition + i * 4, SeekOrigin.Begin);
                int elementOffset = reader.ReadInt32();
                int elementSize = reader.ReadInt32();
                long elementPosition = reader.BaseStream.Seek(iniPosition + elementOffset, SeekOrigin.Begin);
                iniList.Add(reader.ReadBytes(elementSize));
            }
        }
    }

    private void FUN_1FD18(uint param2)
    {
        int iVar5;
        TMD puVar7;

        puVar7 = tmdList[(int)(param2 & 0xffff)];
        iVar5 = 0;

    }
}
