using System;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

public static class IMP_TIM
{
    private static int LZCS = 0; //cop2r30
    private static int LZCR = 0; //cop2r31

    private static uint FLAG = 0;
    private static long POS_CLUT_RECT = 0;
    private static long POS_CLUT_COLORS = 0;
    private static long POS_IMG_RECT = 0;
    private static long POS_IMG_INDICES = 0;
    private static long POS_IMG_INDICES2 = 0;

    private static readonly int NUM_BLOCKS = 6;
    
    private static readonly byte[] BYTES =
    {
        0x07, 0x01, 0x06, 0x01, 0x01, 0x02, 0x05, 0x01, 0x02, 0x02, 0x09,
        0x01, 0x00, 0x04, 0x08, 0x01, 0x10, 0x01, 0x05, 0x02, 0x00, 0x07,
        0x02, 0x03, 0x01, 0x04, 0x0F, 0x01, 0x0E, 0x01, 0x04, 0x02, 0x00,
        0x0B, 0x08, 0x02, 0x04, 0x03, 0x00, 0x0A, 0x02, 0x04, 0x07, 0x02,
        0x15, 0x01, 0x14, 0x01, 0x00, 0x09, 0x13, 0x01, 0x12, 0x01, 0x01,
        0x05, 0x03, 0x03, 0x00, 0x08, 0x06, 0x02, 0x11, 0x01, 0x0A, 0x02,
        0x09, 0x02, 0x05, 0x03, 0x03, 0x04, 0x02, 0x05, 0x01, 0x07, 0x01,
        0x06, 0x00, 0x0F, 0x00, 0x0E, 0x00, 0x0D, 0x00, 0x0C, 0x1A, 0x01,
        0x19, 0x01, 0x18, 0x01, 0x17, 0x01, 0x16, 0x01, 0x00, 0x1F, 0x00,
        0x1E, 0x00, 0x1D, 0x00, 0x1C, 0x00, 0x1B, 0x00, 0x1A, 0x00, 0x19,
        0x00, 0x18, 0x00, 0x17, 0x00, 0x16, 0x00, 0x15, 0x00, 0x14, 0x00,
        0x13, 0x00, 0x12, 0x00, 0x11, 0x00, 0x10, 0x00, 0x28, 0x00, 0x27,
        0x00, 0x26, 0x00, 0x25, 0x00, 0x24, 0x00, 0x23, 0x00, 0x22, 0x00,
        0x21, 0x00, 0x20, 0x01, 0x0E, 0x01, 0x0D, 0x01, 0x0C, 0x01, 0x0B,
        0x01, 0x0A, 0x01, 0x09, 0x01, 0x08, 0x01, 0x12, 0x01, 0x11, 0x01,
        0x10, 0x01, 0x0F, 0x06, 0x03, 0x10, 0x02, 0x0F, 0x02, 0x0E, 0x02,
        0x0D, 0x02, 0x0C, 0x02, 0x0B, 0x02, 0x1F, 0x01, 0x1E, 0x01, 0x1D,
        0x01, 0x1C, 0x01, 0x1B, 0x01, 0x0D, 0x01, 0x00, 0x06, 0x0C, 0x01,
        0x0B, 0x01, 0x03, 0x02, 0x01, 0x03, 0x00, 0x05, 0x0A, 0x01, 0x00,
        0x03, 0x04, 0x01, 0x03, 0x01
    };
    private static readonly byte[] QUANT_TABLE =
    {
        0x02, 0x10, 0x10, 0x13, 0x10, 0x13, 0x16, 0x16, 0x16, 0x16, 0x16,
        0x16, 0x1A, 0x18, 0x1A, 0x1B, 0x1B, 0x1B, 0x1A, 0x1A, 0x1A, 0x1A,
        0x1B, 0x1B, 0x1B, 0x1D, 0x1D, 0x1D, 0x22, 0x22, 0x22, 0x1D, 0x1D,
        0x1D, 0x1B, 0x1B, 0x1D, 0x1D, 0x20, 0x20, 0x22, 0x22, 0x25, 0x26,
        0x25, 0x23, 0x23, 0x22, 0x23, 0x26, 0x26, 0x28, 0x28, 0x28, 0x30,
        0x30, 0x2E, 0x2E, 0x38, 0x38, 0x3A, 0x45, 0x45, 0x53, 0x02, 0x10,
        0x10, 0x13, 0x10, 0x13, 0x16, 0x16, 0x16, 0x16, 0x16, 0x16, 0x1A,
        0x18, 0x1A, 0x1B, 0x1B, 0x1B, 0x1A, 0x1A, 0x1A, 0x1A, 0x1B, 0x1B,
        0x1B, 0x1D, 0x1D, 0x1D, 0x22, 0x22, 0x22, 0x1D, 0x1D, 0x1D, 0x1B,
        0x1B, 0x1D, 0x1D, 0x20, 0x20, 0x22, 0x22, 0x25, 0x26, 0x25, 0x23,
        0x23, 0x22, 0x23, 0x26, 0x26, 0x28, 0x28, 0x28, 0x30, 0x30, 0x2E,
        0x2E, 0x38, 0x38, 0x3A, 0x45, 0x45, 0x53
    };
    private static readonly ushort[] SCALE_TABLE =
    {
        0x5A82, 0x5A82, 0x5A82, 0x5A82, 0x5A82, 0x5A82, 0x5A82, 0x5A82,
        0x7D8A, 0x6A6D, 0x471C, 0x18F8, 0xE707, 0xB8E3, 0x9592, 0x8275,
        0x7641, 0x30FB, 0xCF04, 0x89BE, 0x89BE, 0xCF04, 0x30FB, 0x7641,
        0x6A6D, 0xE707, 0x8275, 0xB8E3, 0x471C, 0x7D8A, 0x18F8, 0x9592,
        0x5A82, 0xA57D, 0xA57D, 0x5A82, 0x5A82, 0xA57D, 0xA57D, 0x5A82,
        0x471C, 0x8275, 0x18F8, 0x6A6D, 0x9592, 0xE707, 0x7D8A, 0xB8E3,
        0x30FB, 0x89BE, 0x7641, 0xCF04, 0xCF04, 0x7641, 0x89BE, 0x30FB,
        0x18F8, 0xB8E3, 0x6A6D, 0x8275, 0x7D8A, 0x9592, 0x471C, 0xE707
    };
    private static readonly byte[] ZAGZIG =
    {
        0,  1,  8,  16, 9,  2,  3,  10, 17, 24, 32, 25, 18, 11, 4,  5,
        12, 19, 26, 33, 40, 48, 41, 34, 27, 20, 13, 6,  7,  14, 21, 28,
        35, 42, 49, 56, 57, 50, 43, 36, 29, 22, 15, 23, 30, 37, 44, 51,
        58, 59, 52, 45, 38, 31, 39, 46, 53, 60, 61, 54, 47, 55, 62, 63
    };

    private struct RECT
    {
        public short x, y, w, h;
    }

    private struct RGB
    {
        public byte r, g, b, a;
    }

    public static void LoadAsset(string assetPath, string bmp)
    {
        using (BinaryReader reader = new BinaryReader(File.Open(assetPath, FileMode.Open)))
        {
            int header = reader.ReadInt32();

            if (header >> 24 == 8)
            {
                LoadTIM(reader, bmp);
            }
            else if (header != 0x10)
            {
                reader.BaseStream.Seek(0, SeekOrigin.Begin);
                short width = reader.ReadInt16();
                short height = reader.ReadInt16();
                long pos = reader.BaseStream.Position;
                int tag = reader.ReadInt32();
                int unk1 = 0; //r30

                if (tag == 0x10)
                {
                    string file2 = Path.GetFileNameWithoutExtension(bmp);
                    string newPath2 = bmp.Replace(file2, file2 + "_1");
                    LoadTIM(reader, newPath2);
                    pos = POS_IMG_INDICES2;
                    unk1 |= 10;
                }

                reader.BaseStream.Seek(pos, SeekOrigin.Begin);
                RECT rect = new RECT
                {
                    x = 0,
                    y = 0,
                    w = width,
                    h = height
                };

                int unk2 = (unk1 >> 2 ^ 1) & 1; //r20
                int unk3 = unk1 & 1; //r19
                int unk4 = unk3 + 2; //r21
                                     //r17
                int unk5 = rect.h + 0x0F & -16; //sp+24h
                                                //sp+20h = rect.y + rect.h
                int unk6 = unk4 * 16; //r16
                unk6 = unk5 * unk6;
                int unk7 = 0; //r23
                int unk8 = 0; //sp+10h
                int unk9 = 0; //r18

                byte[] m_iq_y = new byte[64];
                byte[] m_iq_uv = new byte[64];

                short[] m_scale_table = new short[SCALE_TABLE.Length];
                for (int i = 0; i < m_scale_table.Length; i++)
                    m_scale_table[i] = (short)SCALE_TABLE[i];

                int data_output_depth = 0;
                int data_output_signed = 0;
                int data_output_bit15 = 0;
                Queue<ushort> m_data_in_fifo = new Queue<ushort>();
                Queue<uint> m_data_out_fifo = new Queue<uint>();
                short[,] m_blocks = new short[NUM_BLOCKS, 64];
                uint[] m_block_rgb = new uint[256];

                for (int i = 0; i < 64; i++)
                {
                    m_iq_y[i] = QUANT_TABLE[i];
                    m_iq_uv[i] = QUANT_TABLE[i + 64];
                }

                ushort[] fastRam = new ushort[214];
                for (int i = 0; i < fastRam.Length; i += 2)
                {
                    int iVar1 = BYTES[i] << 10;
                    int iVar2 = iVar1 | BYTES[i + 1];
                    int iVar3 = iVar1 | iVar2 * -1 & 0x3FF;
                    fastRam[i] = (ushort)iVar2;
                    fastRam[i + 1] = (ushort)iVar3;
                }

                using (MemoryStream stream = new MemoryStream())
                {
                    using (BinaryWriter writer = new BinaryWriter(stream, Encoding.Default, true))
                    {
                        uint uVar1 = reader.ReadUInt32(); //r8
                        uint uVar2 = reader.ReadUInt32(); //r11
                        uint uVar3 = reader.ReadUInt32(); //r2
                        uint uVar4 = reader.ReadUInt32(); //r3
                        uint uVar5 = reader.ReadUInt32(); //r6
                        int iVar6 = 0; //r9
                        int iVar7 = 0; //r10
                        uint uVar8 = 0; //r15
                        uint uVar9 = 0; //r24
                        uint uVar10 = 0; //r25

                        uVar3 = uVar3 >> 16 | uVar3 << 16;
                        uVar4 = uVar4 >> 16 | uVar4 << 16;
                        writer.Write(uVar1);
                        uVar1 = (uVar1 & 0xFFFF) << 2;
                        int streamLength = (int)uVar1 + 4; //r13
                        if ((uVar2 >> 16 ^ 3) == 0)
                        {
                            //...
                        }
                        else
                        {
                            uVar2 = (uVar2 & 0x3F) << 10;
                            uVar1 = uVar3 >> 22 | uVar2;
                            iVar6 = 10;
                            Huffman();

                            LABEL1:
                            uVar1 = uVar1 >> 22;

                            if ((uVar1 ^ 511) != 0)
                            {
                                uVar1 = uVar1 | uVar2;
                                iVar6 = 12;
                                Huffman();
                                goto LABEL1;
                            }
                        }

                        uVar1 = 0xFE00;
                        while (writer.BaseStream.Length != streamLength)
                            writer.Write((ushort)uVar1);

                        using (BinaryReader reader2 = new BinaryReader(stream, Encoding.Default, true))
                        {
                            writer.BaseStream.Seek(0, SeekOrigin.Begin);
                            int iVar11 = unk1 & 3;
                            uint uVar12;

                            if ((iVar11 & 1) == 0)
                                uVar12 = reader2.ReadUInt32() | 0x8000000;
                            else
                                uVar12 = reader2.ReadUInt32() & 0xf7ffffff;

                            writer.BaseStream.Seek(0, SeekOrigin.Begin);
                            writer.Write(uVar12);
                            reader2.BaseStream.Seek(0, SeekOrigin.Begin);

                            if ((iVar11 & 2) == 0)
                                uVar12 = reader2.ReadUInt32() & 0xfdffffff;
                            else
                                uVar12 = reader2.ReadUInt32() | 0x2000000;

                            writer.BaseStream.Seek(0, SeekOrigin.Begin);
                            writer.Write(uVar12);

                            string ext = Path.GetExtension(bmp);
                            string newPath2 = bmp.Replace(ext, ".txt");
                            using (BinaryWriter writer2 = new BinaryWriter(File.Open(newPath2, FileMode.Create)))
                            {
                                while (reader2.BaseStream.Position != reader2.BaseStream.Length)
                                {
                                    byte firstNibble = 0;  // a Nibble is 4 bits, half a byte, one hexadecimal character
                                    char firstHexChar;
                                    byte initialByte = reader2.ReadByte();  //initialize this to the byte you want to print
                                    byte secondNibble = 0;
                                    char secondHexChar;


                                    firstNibble = (byte)((uint)initialByte >> 4);  // isolate first 4 bits

                                    if (firstNibble < 10U)
                                    {
                                        firstHexChar = (char)('0' + firstNibble);
                                    }
                                    else
                                    {
                                        firstNibble -= 10;
                                        firstHexChar = (char)('A' + firstNibble);
                                    }

                                    secondNibble = (byte)((uint)initialByte & 0x0F);  // isolate last 4 bits

                                    if (secondNibble < 10U)
                                    {
                                        secondHexChar = (char)('0' + secondNibble);
                                    }
                                    else
                                    {
                                        secondNibble -= 10;
                                        secondHexChar = (char)('A' + secondNibble);
                                    }

                                    writer2.Write(firstHexChar);
                                    writer2.Write(secondHexChar);
                                    writer2.Write((byte)0x20);
                                }
                            }

                            reader2.BaseStream.Seek(0, SeekOrigin.Begin);
                        }

                        void Huffman()
                        {
                            while (true)
                            {
                                while (true)
                                {
                                    writer.Write((ushort)uVar1);
                                    uVar3 = uVar3 << iVar6;
                                    uVar3 = uVar3 | uVar4 >> iVar6 * -1;
                                    iVar7 += iVar6;
                                    uVar4 = uVar4 << iVar6;

                                    if (-1 < iVar7 - 32)
                                    {
                                        iVar7 -= 32;
                                        uVar1 = uVar5 << 16;
                                        uVar5 = uVar5 >> 16 | uVar1;
                                        uVar3 |= -iVar7 != 0 ? uVar5 >> -iVar7 : 0;
                                        uVar4 = uVar5 << iVar7;

                                        if (reader.BaseStream.Position != reader.BaseStream.Length)
                                            uVar5 = reader.ReadUInt32();
                                        else
                                            uVar5 = 0;
                                    }

                                    if ((int)uVar3 < 0) break;

                                    if ((int)(uVar3 << 1) < 0)
                                    {
                                        if ((int)(uVar3 << 2) < 0)
                                        {
                                            iVar6 = 4;
                                            uVar1 = 1025;

                                            if ((int)(uVar3 << 3) < 0)
                                                uVar1 = 2047;
                                        }
                                        else
                                        {
                                            iVar6 = 5;

                                            if ((int)(uVar3 << 3) < 0)
                                            {
                                                uVar1 = 2049;

                                                if ((int)(uVar3 << 4) < 0)
                                                    uVar1 = 3071;
                                            }
                                            else
                                            {
                                                uVar1 = 2;

                                                if ((int)(uVar3 << 4) < 0)
                                                    uVar1 = 1022;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if ((int)(uVar3 << 2) < 0)
                                        {
                                            if ((uVar3 << 3) >> 30 == 0)
                                            {
                                                uVar1 = (uint)(short)fastRam[192 + (uVar3 >> 22 & 30) / 2];
                                                iVar6 = 9;
                                            }
                                            else
                                            {
                                                uVar1 = (uint)(short)fastRam[206 + (uVar3 >> 25 & 14) / 2];
                                                iVar6 = 6;
                                            }
                                        }
                                        else
                                        {
                                            LZCS = (int)uVar3;
                                            LZCR = Utilities.LeadingZeros(LZCS);
                                            iVar6 = LZCR;

                                            if (uVar3 >> 26 == 1)
                                            {
                                                uVar1 = uVar3 >> 10 & 0xFFFF;
                                                iVar6 = 22;
                                            }
                                            else
                                            {
                                                uint uVar = uVar3 << iVar6;

                                                if ((int)((uVar3 >> 26) - 1) < 0)
                                                {
                                                    if (iVar6 - 6 < 1)
                                                    {
                                                        uVar = uVar >> 26 & 30;
                                                        uVar1 = (uint)(short)fastRam[16 + uVar / 2];
                                                        iVar6 = 11;
                                                    }
                                                    else
                                                    {
                                                        uVar1 = (uint)iVar6 - 6 << 6;
                                                        uVar = (uVar >> 25 & 62) + uVar1;
                                                        uVar1 = (uint)(short)fastRam[uVar / 2];
                                                        iVar6 += 6;
                                                    }
                                                }
                                                else
                                                {
                                                    uVar1 = (uint)(iVar6 - 3 << 4);
                                                    uVar = (uVar >> 27 & 14) + uVar1;
                                                    uVar1 = (uint)(short)fastRam[uVar / 2];
                                                    iVar6 += 4;
                                                }
                                            }
                                        }
                                    }
                                }

                                if (-1 < (int)(uVar3 << 1)) break;

                                iVar6 = 3;
                                uVar1 = 1;

                                if ((int)(uVar3 << 2) < 0)
                                    uVar1 = 1023;
                            }

                            writer.Write((ushort)0xFE00);
                            uVar1 = uVar3 << 2;
                            return;
                        }
                    }

                    if (unk3 == 0)
                    {
                        unk8 = rect.x;
                    }
                    else
                    {
                        int iVar = (((rect.x << 16) >> 16) + (int)((uint)(rect.x << 16) >> 31)) >> 1;
                        unk8 = (iVar << 1) + iVar;
                    }

                    short sVar1 = rect.y; //sp+12h
                    short sVar2 = (short)(unk4 << 3); //sp+14h
                    short sVar3 = (short)(rect.h - sVar2);

                    if (unk3 == 0)
                    {
                        unk3 = unk8 + rect.w;
                    }
                    else
                    {
                        int iVar = ((rect.w << 16) >> 16) + (int)((uint)(rect.w << 16) >> 31) >> 1;
                        unk3 = (iVar << 1) + iVar;
                    }

                    unk4 = ((unk4 << 3) * unk5) + (int)((uint)((unk4 << 3) * unk5) >> 31);
                    unk9 = unk7 << 2;

                    using (BinaryReader reader2 = new BinaryReader(stream, Encoding.Default, true))
                    {
                        int command = reader2.ReadInt32();
                        data_output_depth = BitExtracted(command, 2, 27);
                        data_output_signed = BitExtracted(command, 1, 26);
                        data_output_bit15 = BitExtracted(command, 1, 25);

                        int m_remaining_halfwords = BitExtracted(command, 16, 0) * 2;
                        int m_current_block = 0;
                        int m_current_coefficient = 64;
                        ushort m_current_q_scale = 0;

                        while (m_remaining_halfwords > 0)
                        {
                            bool mainBreak = false;

                            for (; m_current_block < NUM_BLOCKS; m_current_block++)
                            {
                                if (!rl_decode_block(m_blocks, (m_current_block >= 2) ? m_iq_y : m_iq_uv))
                                { mainBreak = true; break; }

                                IDCT(m_blocks);
                            }

                            if (mainBreak) break;

                            m_current_block = 0;
                            m_current_coefficient = 64;
                            m_current_q_scale = 0;

                            yuv_to_rgb(0, 0, m_blocks, 0, 1, 2);
                            yuv_to_rgb(8, 0, m_blocks, 0, 1, 3);
                            yuv_to_rgb(0, 8, m_blocks, 0, 1, 4);
                            yuv_to_rgb(8, 8, m_blocks, 0, 1, 5);

                            #region CopyOutBlock
                            switch (data_output_depth)
                            {
                                case 0:
                                    for (int i = 0; i < 64; i += 8)
                                    {
                                        uint value = m_block_rgb[i] >> 4;
                                        value |= (m_block_rgb[i + 1] >> 4) << 4;
                                        value |= (m_block_rgb[i + 2] >> 4) << 8;
                                        value |= (m_block_rgb[i + 3] >> 4) << 12;
                                        value |= (m_block_rgb[i + 4] >> 4) << 16;
                                        value |= (m_block_rgb[i + 5] >> 4) << 20;
                                        value |= (m_block_rgb[i + 6] >> 4) << 24;
                                        value |= (m_block_rgb[i + 7] >> 4) << 28;
                                        m_data_out_fifo.Enqueue(value);
                                    }
                                    break;

                                case 1:
                                    for (int i = 0; i < 64; i += 4)
                                    {
                                        uint value = m_block_rgb[i];
                                        value |= m_block_rgb[i + 1] << 8;
                                        value |= m_block_rgb[i + 2] << 16;
                                        value |= m_block_rgb[i + 3] << 24;
                                        m_data_out_fifo.Enqueue(value);
                                    }
                                    break;

                                case 2:
                                    int index = 0;
                                    int state = 0;
                                    uint rgb = 0;

                                    while (index < m_block_rgb.Length)
                                    {
                                        switch (state)
                                        {
                                            case 0:
                                                rgb = m_block_rgb[index++];
                                                state = 1;
                                                break;
                                            case 1:
                                                rgb |= (m_block_rgb[index] & 0xFF) << 24;
                                                m_data_out_fifo.Enqueue(rgb);
                                                rgb = m_block_rgb[index] >> 8;
                                                index++;
                                                state = 2;
                                                break;
                                            case 2:
                                                rgb |= m_block_rgb[index] << 16;
                                                m_data_out_fifo.Enqueue(rgb);
                                                rgb = m_block_rgb[index] >> 16;
                                                index++;
                                                state = 3;
                                                break;
                                            case 3:
                                                rgb |= m_block_rgb[index] << 8;
                                                m_data_out_fifo.Enqueue(rgb);
                                                index++;
                                                state = 0;
                                                break;
                                        }
                                    }
                                    break;

                                case 3:
                                    ushort a = (ushort)data_output_bit15;
                                    for (int i = 0; i < m_block_rgb.Length;)
                                    {
                                        uint color = m_block_rgb[i++];
                                        ushort r = (ushort)((color >> 3) & 0x1Fu);
                                        ushort g = (ushort)((color >> 11) & 0x1Fu);
                                        ushort b = (ushort)((color >> 19) & 0x1Fu);
                                        ushort color15a = (ushort)(r | (g << 5) | (b << 10) | (a << 15));

                                        color = m_block_rgb[i++];
                                        r = (ushort)((color >> 3) & 0x1Fu);
                                        g = (ushort)((color >> 11) & 0x1Fu);
                                        b = (ushort)((color >> 19) & 0x1Fu);
                                        ushort color15b = (ushort)(r | (g << 5) | (b << 10) | (a << 15));

                                        m_data_out_fifo.Enqueue((uint)color15a | ((uint)color15b << 16));
                                    }
                                    break;

                                default:
                                    break;
                            }
                            #endregion
                        }

                        bool rl_decode_block(short[,] blk, byte[] qt)
                        {
                            if (m_current_coefficient == 64)
                            {
                                for (int i = 0; i < 64; i++)
                                    blk[m_current_block, i] = 0;

                                ushort n;
                                for (; ; )
                                {
                                    if (m_remaining_halfwords == 0)
                                        return false;

                                    n = reader2.ReadUInt16();
                                    m_remaining_halfwords--;

                                    if (n == 0xFE00)
                                        continue;
                                    else
                                        break;
                                }

                                m_current_coefficient = 0;
                                m_current_q_scale = (ushort)((n >> 10) & 0x3F);
                                int val = SignedNBits((int)(n & 0x3FF), 10) * (int)(uint)qt[m_current_coefficient];

                                if (m_current_q_scale == 0)
                                    val = SignedNBits((int)(n & 0x3FF), 10) * 2;

                                val = Utilities.Clamp(val, -0x400, 0x3FF);

                                if (m_current_q_scale > 0)
                                    blk[m_current_block, ZAGZIG[m_current_coefficient]] = (short)val;
                                else if (m_current_q_scale == 0)
                                    blk[m_current_block, m_current_coefficient] = (short)val;
                            }

                            while (m_remaining_halfwords > 0)
                            {
                                ushort n = reader2.ReadUInt16();
                                m_remaining_halfwords--;

                                m_current_coefficient += ((n >> 10) & 0x3F) + 1;
                                if (m_current_coefficient >= 64)
                                {
                                    m_current_coefficient = 64;
                                    return true;
                                }

                                int val = (SignedNBits((int)(n & 0x3FF), 10) * (int)(uint)qt[m_current_coefficient] * (int)m_current_q_scale + 4) / 8;

                                if (m_current_q_scale == 0)
                                    val = SignedNBits((int)(n & 0x3FF), 10) * 2;

                                val = Utilities.Clamp(val, -0x400, 0x3FF);

                                if (m_current_q_scale > 0)
                                    blk[m_current_block, ZAGZIG[m_current_coefficient]] = (short)val;
                                else if (m_current_q_scale == 0)
                                    blk[m_current_block, m_current_coefficient] = (short)val;
                            }

                            return false;
                        }

                        void IDCT(short[,] blk)
                        {
                            long[] temp_buffer = new long[64];

                            for (int x = 0; x < 8; x++)
                            {
                                for (int y = 0; y < 8; y++)
                                {
                                    long sum = 0;

                                    for (int u = 0; u < 8; u++)
                                        sum += (blk[m_current_block, u * 8 + x]) * (m_scale_table[u * 8 + y]);

                                    temp_buffer[x + y * 8] = sum;
                                }
                            }

                            for (int x = 0; x < 8; x++)
                            {
                                for (int y = 0; y < 8; y++)
                                {
                                    long sum = 0;

                                    for (int u = 0; u < 8; u++)
                                        sum += (temp_buffer[u + y * 8]) * (m_scale_table[u * 8 + x]);

                                    blk[m_current_block, x + y * 8] = (short)Utilities.Clamp(SignedNBits((int)((sum >> 32) + ((sum >> 31) & 1)), 9), -128, 127);
                                }
                            }
                        }

                        void yuv_to_rgb(int xx, int yy, short[,] blk, int Crblk, int Cbblk, int Yblk)
                        {
                            for (int y = 0; y < 8; y++)
                            {
                                for (int x = 0; x < 8; x++)
                                {
                                    short R = m_blocks[Crblk, ((x + xx) / 2) + ((y + yy) / 2) * 8];
                                    short B = m_blocks[Cbblk, ((x + xx) / 2) + ((y + yy) / 2) * 8];
                                    short G = (short)((-0.3437f * (float)B) + (-0.7143f * (float)R));

                                    R = (short)(1.402f * (float)R);
                                    B = (short)(1.772f * (float)B);

                                    short Y = m_blocks[Yblk, x + y * 8];
                                    R = (short)Utilities.Clamp((int)Y + R, -128, 127);
                                    G = (short)Utilities.Clamp((int)Y + G, -128, 127);
                                    B = (short)Utilities.Clamp((int)Y + B, -128, 127);

                                    R += 128;
                                    G += 128;
                                    B += 128;

                                    m_block_rgb[(x + xx) + ((y + yy) * 16)] = (uint)(ushort)R | ((uint)(ushort)G << 8) | ((uint)(ushort)B << 16);
                                }
                            }
                        }
                    }
                }

                RGB[,] chunks = new RGB[width / 16, 16 * height];

                for (int i = 0; i < width / 16; i++)
                {
                    ushort redMask = 0x7C00;
                    ushort greenMask = 0x3E0;
                    ushort blueMask = 0x1F;

                    switch (data_output_depth)
                    {
                        case 3:
                            for (int j = 0; j < 16 * height;)
                            {
                                uint colors = m_data_out_fifo.Dequeue();
                                ushort[] color = new ushort[2] { (ushort)(colors & 0xFFFF), (ushort)(colors >> 16) };

                                for (int k = 0; k < color.Length; k++, j++)
                                {
                                    byte R5 = (byte)((color[k] & redMask) >> 10);
                                    byte G5 = (byte)((color[k] & greenMask) >> 5);
                                    byte B5 = (byte)(color[k] & blueMask);
                                    byte A = (byte)(color[k] >> 15 == 1 ? 128 : 255);

                                    byte R8 = (byte)(R5 << 3);
                                    byte G8 = (byte)(G5 << 3);
                                    byte B8 = (byte)(B5 << 3);

                                    chunks[i, j].r = R8;
                                    chunks[i, j].g = G8;
                                    chunks[i, j].b = B8;
                                    chunks[i, j].a = A;
                                }
                            }
                            break;
                    }
                }

                string file = Path.GetFileNameWithoutExtension(bmp);
                string newPath = bmp.Replace(file, file + "_2");
                #region Bitmap
                using (BinaryWriter writer = new BinaryWriter(File.Open(newPath, FileMode.Create)))
                {
                    writer.Write((short)0x4D42); //Signature
                    writer.Write(width * height * 4 + 56); //File Size
                    writer.Write((short)0); //Reserved1
                    writer.Write((short)0); //Reserved2
                    writer.Write(54); //File Offset to Pixel Array
                    writer.Write(40); //DIB Header Size
                    writer.Write((int)width); //Image Width
                    writer.Write((int)height); //Image Height
                    writer.Write(0x00200001); //Compression
                    writer.Write((long)0);
                    writer.Write(2834); //X Pixels Per Meter
                    writer.Write(2834); //Y Pixels Per Meter
                    writer.Write((long)0);

                    List<RGB> pixels = new List<RGB>();

                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width / 16; x++)
                        {
                            for (int z = 0; z < 16; z++)
                            {
                                RGB rgb;
                                rgb.r = chunks[x, z + (y * 16)].r;
                                rgb.g = chunks[x, z + (y * 16)].g;
                                rgb.b = chunks[x, z + (y * 16)].b;
                                rgb.a = chunks[x, z + (y * 16)].a;

                                pixels.Add(rgb);
                            }
                        }
                    }

                    for (int i = 0, j = pixels.Count - width; i < pixels.Count; i += width, j -= width)
                    {
                        for (int k = 0; k < width; ++k)
                        {
                            writer.Write(pixels[j + k].r);
                            writer.Write(pixels[j + k].g);
                            writer.Write(pixels[j + k].b);
                            writer.Write(pixels[j + k].a);
                        }
                    }

                    writer.Write((short)0);
                }
                #endregion
            }
            else
            {
                LoadTIM(reader, bmp);
            }
        }
    }

    public static void LoadTIM(BinaryReader reader, string path)
    {
        short clutWidth = 0;
        short clutHeight = 0;
        short imageWidth = 0;
        short imageHeight = 0;
        ushort[] colors = new ushort[16];
        byte[] indices;

        InitGlobals(reader);

        if (FLAG == 0x12)
            FLAG = 0x12;

        #region CLUT
        if (POS_CLUT_RECT != 0)
        {
            if ((FLAG & 0x20) == 0)
            {
                reader.BaseStream.Seek(POS_CLUT_RECT + 0x04, SeekOrigin.Begin);
                clutWidth = reader.ReadInt16();
                clutHeight = reader.ReadInt16();
            }
            else
            {
                clutWidth = 16;
                if (16 < reader.ReadInt16())
                    clutWidth = 256;
            }

            colors = new ushort[clutHeight * clutWidth];
            reader.BaseStream.Seek(POS_CLUT_COLORS, SeekOrigin.Begin);
            for (int i = 0; i < colors.Length; i++)
                colors[i] = reader.ReadUInt16();
        }

        //_clutWidth = (byte)clutWidth;
        goto SET_IMAGE;
        #endregion

        //_unknown1 = 0;
        #region Image
        SET_IMAGE:
        //unknownReturn = 0;
        reader.BaseStream.Seek(POS_IMG_RECT + 0x04, SeekOrigin.Begin);
        imageWidth = reader.ReadInt16();
        imageHeight = reader.ReadInt16();

        if ((FLAG & 0x10) == 0)
        {
            indices = new byte[imageWidth * imageHeight * 2];
            for (int i = 0; i < indices.Length; i++)
                indices[i] = reader.ReadByte();
        }
        else
        {
            reader.BaseStream.Seek(POS_IMG_INDICES, SeekOrigin.Begin);
            RECT newRect = new RECT
            {
                x = 0,
                y = 0,
                w = imageWidth,
                h = imageHeight
            };

            indices = Decompressor(reader, newRect).ToArray();
        }
        #endregion

        #region Bitmap
        using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
        {
            if (FLAG == 0x12)
            {
                writer.Write((short)0x4D42); //Signature
                writer.Write(imageWidth * imageHeight * 4 + 56); //File Size
                writer.Write((short)0); //Reserved1
                writer.Write((short)0); //Reserved2
                writer.Write(54); //File Offset to Pixel Array
                writer.Write(40); //DIB Header Size
                writer.Write((int)imageWidth); //Image Width
                writer.Write((int)imageHeight); //Image Height
                writer.Write(0x00200001); //Compression
                writer.Write((long)0);
                writer.Write(2834); //X Pixels Per Meter
                writer.Write(2834); //Y Pixels Per Meter
                writer.Write((long)0);

                colors = new ushort[imageWidth * imageHeight];

                for (int i = 0; i < indices.Length; i += 2)
                    colors[i / 2] = (ushort)(indices[i + 1] << 8 | indices[i]);

                ushort redMask = 0x7C00;
                ushort greenMask = 0x3E0;
                ushort blueMask = 0x1F;
                RGB[] pixels = new RGB[imageWidth * imageHeight];

                for (int i = 0; i < pixels.Length; i++)
                {
                    byte R5 = (byte)((colors[i] & redMask) >> 10);
                    byte G5 = (byte)((colors[i] & greenMask) >> 5);
                    byte B5 = (byte)(colors[i] & blueMask);

                    byte R8 = (byte)(R5 << 3);
                    byte G8 = (byte)(G5 << 3);
                    byte B8 = (byte)(B5 << 3);

                    byte A = 255;

                    if (colors[i] >> 15 == 0)
                    {
                        if (R8 == 0 && G8 == 0 && B8 == 0)
                            A = 0;
                        else
                            A = 255;
                    }
                    else
                    {
                        if (R8 == 0 && G8 == 0 && B8 == 0)
                            A = 127;
                        else
                            A = 127;
                    }

                    /*pixels[i].r = A == 0 ? R8 : (byte)0;
                    pixels[i].g = A == 0 ? G8 : (byte)0;
                    pixels[i].b = A == 0 ? B8 : (byte)0;
                    pixels[i].a = A;*/

                    pixels[i].r = R8;
                    pixels[i].g = G8;
                    pixels[i].b = B8;
                    pixels[i].a = A;

                    writer.Write(pixels[i].r);
                    writer.Write(pixels[i].g);
                    writer.Write(pixels[i].b);
                    writer.Write(pixels[i].a);
                }

                writer.Write((short)0);
            }
            else if ((FLAG & 1) == 0)
            {
                writer.Write((short)0x4D42); //Signature
                writer.Write(imageWidth * imageHeight * 16 + 56); //File Size
                writer.Write((short)0); //Reserved1
                writer.Write((short)0); //Reserved2
                writer.Write(54); //File Offset to Pixel Array
                writer.Write(40); //DIB Header Size
                writer.Write(imageWidth * 4); //Image Width
                writer.Write((int)imageHeight); //Image Height
                writer.Write(0x00200001); //Compression
                writer.Write((long)0);
                writer.Write(2834); //X Pixels Per Meter
                writer.Write(2834); //Y Pixels Per Meter
                writer.Write((long)0);

                ushort redMask = 0x7C00;
                ushort greenMask = 0x3E0;
                ushort blueMask = 0x1F;
                RGB[] pixels = new RGB[imageWidth * imageHeight * 4];

                for (int i = 0; i < pixels.Length; i++)
                {
                    int colorIndex = i % 2 == 0 ? indices[i / 2] & 0x0F : indices[i / 2] >> 4;

                    byte R5 = (byte)((colors[colorIndex] & redMask) >> 10);
                    byte G5 = (byte)((colors[colorIndex] & greenMask) >> 5);
                    byte B5 = (byte)(colors[colorIndex] & blueMask);

                    byte R8 = (byte)(R5 << 3);
                    byte G8 = (byte)(G5 << 3);
                    byte B8 = (byte)(B5 << 3);

                    byte A = 255;

                    if (colors[colorIndex] >> 15 == 0)
                    {
                        if (R8 == 0 && G8 == 0 && B8 == 0)
                            A = 0;
                        else
                            A = 255;
                    }
                    else
                    {
                        if (R8 == 0 && G8 == 0 && B8 == 0)
                            A = 127;
                        else
                            A = 127;
                    }

                    /*pixels[i].r = A == 0 ? R8 : (byte)0;
                    pixels[i].g = A == 0 ? G8 : (byte)0;
                    pixels[i].b = A == 0 ? B8 : (byte)0;
                    pixels[i].a = A;*/

                    pixels[i].r = R8;
                    pixels[i].g = G8;
                    pixels[i].b = B8;
                    pixels[i].a = A;
                }

                int width = imageWidth * 4;

                for (int i = 0, j = pixels.Length - width; i < pixels.Length; i += width, j -= width)
                {
                    for (int k = 0; k < width; ++k)
                    {
                        writer.Write(pixels[j + k].r);
                        writer.Write(pixels[j + k].g);
                        writer.Write(pixels[j + k].b);
                        writer.Write(pixels[j + k].a);
                    }
                }

                writer.Write((short)0);
            }
            else
            {
                writer.Write((short)0x4D42); //Signature
                writer.Write(imageWidth * imageHeight * 16 + 56); //File Size
                writer.Write((short)0); //Reserved1
                writer.Write((short)0); //Reserved2
                writer.Write(54); //File Offset to Pixel Array
                writer.Write(40); //DIB Header Size
                writer.Write(imageWidth * 2); //Image Width
                writer.Write((int)imageHeight); //Image Height
                writer.Write(0x00200001); //Compression
                writer.Write((long)0);
                writer.Write(2834); //X Pixels Per Meter
                writer.Write(2834); //Y Pixels Per Meter
                writer.Write((long)0);

                ushort redMask = 0x7C00;
                ushort greenMask = 0x3E0;
                ushort blueMask = 0x1F;
                RGB[] pixels = new RGB[imageWidth * imageHeight * 2];

                for (int i = 0; i < pixels.Length; i++)
                {
                    int colorIndex = indices[i];

                    byte R5 = (byte)((colors[colorIndex] & redMask) >> 10);
                    byte G5 = (byte)((colors[colorIndex] & greenMask) >> 5);
                    byte B5 = (byte)(colors[colorIndex] & blueMask);

                    byte R8 = (byte)(R5 << 3);
                    byte G8 = (byte)(G5 << 3);
                    byte B8 = (byte)(B5 << 3);

                    byte A = 255;

                    if (colors[colorIndex] >> 15 == 0)
                    {
                        if (R8 == 0 && G8 == 0 && B8 == 0)
                            A = 0;
                        else
                            A = 255;
                    }
                    else
                    {
                        if (R8 == 0 && G8 == 0 && B8 == 0)
                            A = 127;
                        else
                            A = 127;
                    }

                    /*pixels[i].r = A == 0 ? R8 : (byte)0;
                    pixels[i].g = A == 0 ? G8 : (byte)0;
                    pixels[i].b = A == 0 ? B8 : (byte)0;
                    pixels[i].a = A;*/

                    pixels[i].r = R8;
                    pixels[i].g = G8;
                    pixels[i].b = B8;
                    pixels[i].a = A;
                }

                int width = imageWidth * 2;

                for (int i = 0, j = pixels.Length - width; i < pixels.Length; i += width, j -= width)
                {
                    for (int k = 0; k < width; ++k)
                    {
                        writer.Write(pixels[j + k].r);
                        writer.Write(pixels[j + k].g);
                        writer.Write(pixels[j + k].b);
                        writer.Write(pixels[j + k].a);
                    }
                }

                writer.Write((short)0);
            }
        }
        #endregion
    }

    private static void InitGlobals(BinaryReader reader)
    {
        uint flag = reader.ReadUInt32();
        FLAG = flag;

        if ((flag & 8) == 0)
        {
            POS_CLUT_COLORS = 0;
            POS_CLUT_RECT = 0;
        }
        else
        {
            POS_CLUT_RECT = reader.BaseStream.Position + 0x04;
            POS_CLUT_COLORS = reader.BaseStream.Position + 0x0C;
            int IMG_OFFSET = reader.ReadInt32() - 0x04;
            reader.BaseStream.Seek(IMG_OFFSET, SeekOrigin.Current);
        }

        POS_IMG_RECT = reader.BaseStream.Position + 0x04;
        POS_IMG_INDICES = reader.BaseStream.Position + 0x0C;

        int length = reader.ReadInt32();

        POS_IMG_INDICES2 = (reader.BaseStream.Position - 0x0C + length + 0x0B) & -4;
    }

    private static List<byte> Decompressor(BinaryReader reader, RECT rect)
    {
        long begin = reader.BaseStream.Position;
        byte[] pixelBuffer = new byte[2048 + 800];
        int index1 = 0;
        int index2 = 0;
        uint flag = 256;
        short chunkPosX = rect.x;
        short chunkPosY = rect.y;
        short chunkWidth = rect.w;
        short chunkHeight = rect.h;
        List<byte> decompressedData = new List<byte>();
        int width = rect.w * 2;
        do
        {
            int size = index2 + (rect.y + rect.h - chunkPosY) * width;
            int chunk = 2048;
            if (size < 2048)
                chunk = size;
            int bufferPos = 400 + index1;
            do
            {
                if ((flag & 0x100) != 0)
                {
                    byte bVar = reader.ReadByte();
                    flag = (uint)bVar << 0x18 | 1;
                }

                if ((int)flag < 0)
                {
                    byte bVar = reader.ReadByte();
                    index1++;
                    pixelBuffer[bufferPos] = bVar;
                    bufferPos++;
                }
                else
                {
                    uint uVar1 = reader.ReadByte();
                    uint uVar2 = reader.ReadByte();
                    uVar2 = uVar2 << 8;
                    uVar1 = uVar1 | uVar2;
                    uint uVar3 = uVar1 >> 5;
                    uint uVar4 = uVar1 & 0x1F;

                    if ((index1 < (int)uVar3) && (2048 < (int)(uVar3 + uVar4 + 2)))
                    {
                        int iVar5 = (int)uVar4 + 1;
                        while (iVar5 != -1)
                        {
                            uVar4 = uVar3 & 0x7FF;
                            uVar3++;
                            index1++;
                            iVar5--;
                            byte bVar = pixelBuffer[400 + uVar4];
                            pixelBuffer[bufferPos] = bVar;
                            bufferPos++;
                        }
                    }
                    else
                    {
                        int iVar5 = (int)uVar4 + 1;
                        while (iVar5 != -1)
                        {
                            byte bVar = pixelBuffer[400 + uVar3];
                            uVar3++;
                            index1++;
                            iVar5--;
                            pixelBuffer[bufferPos] = bVar;
                            bufferPos++;
                        }
                    }
                }

                flag = flag << 1;
            } while (index1 < chunk);

            int hS = (chunk - index2) / width;
            chunkHeight = (short)hS;
            int hU = (hS << 0x10) >> 0x10;
            if (((hU & 1) != 0) && ((width & 2) != 0) &&
                (chunkPosY + hS < rect.y + rect.h))
                chunkHeight = (short)(hS - 1);

            for (int i = 0; i < width * chunkHeight; i++)
                decompressedData.Add(pixelBuffer[400 + index2 + i]);
            
            chunkPosY = (short)(chunkHeight + chunkPosY);
            index2 += ((chunkHeight << 0x10) >> 0x10) * width;
            InitBuffer(pixelBuffer, 400 + index2 - 2048, 400 + index2, index1 - index2);

            index2 -= 2048;
            index1 -= 2048;
        } while (chunkPosY < rect.y + rect.h);

        return decompressedData;
    }

    private static int InitBuffer(byte[] buffer, int begin, int end, int previousLength)
    {
        int rIndex = begin;
        if (previousLength != 0)
        {
            int unk1 = 0;
            while ((end & 3) != 0)
            {
                buffer[begin++] = buffer[end++];
                previousLength--;

                if (previousLength == 0)
                    return rIndex;
            }

            unk1 = begin & 3;
            previousLength -= 16;
            if (unk1 == 0)
            {
                while (-1 < previousLength)
                    for (int i = 0; i < 16; i++, previousLength--)
                        buffer[begin++] = buffer[end++];

                previousLength += 12;
                while (-1 < previousLength)
                    for (int i = 0; i < 4; i++, previousLength--)
                        buffer[begin++] = buffer[end++];

                previousLength += 3;
                if (-1 < previousLength)
                {
                    end += previousLength;
                    begin += previousLength;

                    if (end % 4 == 0)
                        buffer[begin] = buffer[end];
                    else if (end % 4 == 2)
                    {
                        buffer[begin] = buffer[end];
                        buffer[begin - 1] = buffer[end - 1];
                        buffer[begin - 2] = buffer[end - 2];
                    }
                    else if (end % 4 == 1)
                    {
                        buffer[begin] = buffer[end];
                        buffer[begin - 1] = buffer[end - 1];
                    }
                    else if (end % 4 == 3)
                    {
                        buffer[begin] = buffer[end];
                        buffer[begin - 1] = buffer[end - 1];
                        buffer[begin - 2] = buffer[end - 2];
                        buffer[begin - 3] = buffer[end - 3];
                    }

                    return rIndex;
                }
            }
            else
            {
                while (-1 < previousLength)
                    for (int i = 0; i < 16; i++, previousLength--)
                        buffer[begin++] = buffer[end++];

                previousLength += 12;
                while (-1 < previousLength)
                    for (int i = 0; i < 4; i++, previousLength--)
                        buffer[begin++] = buffer[end++];

                previousLength += 4;
                while (previousLength != 0)
                {
                    buffer[begin++] = buffer[end++];
                    previousLength--;
                }
            }
        }

        return rIndex;
    }

    private static int BitExtracted(int number, int k, int p)
    {
        return (((1 << k) - 1) & (number >> p));
    }
    
    private static int SignedNBits(int value, int NBITS)
    {
        int shift = 8 * sizeof(int) - NBITS;
        return (int)(((int)value << shift) >> shift);
    }
}
