using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

public static class EXP_SND
{
    public static void ExtractSND(string inFile, string outDir)
    {
        List<string> pack_list = new List<string>();

        BinaryReader SNDStream = new BinaryReader(File.Open(inFile, FileMode.Open));

        if (!Directory.Exists(outDir))
        {
            Directory.CreateDirectory(outDir);
        }

        long startPosition = SNDStream.BaseStream.Position;
        int elementsCount = SNDStream.ReadUInt16();
        int eof = SNDStream.ReadUInt16() * 8;
        long elementsPosition = SNDStream.BaseStream.Position + elementsCount * 4;
        double[,] f = { {   0.0, 0.0 },
                        {  60.0 / 64.0, 0.0 },
                        { 115.0 / 64.0, -52.0 / 64.0 },
                        {  98.0 / 64.0, -55.0 / 64.0 },
                        { 122.0 / 64.0, -60.0 / 64.0 } };
        double[] samples = new double[28];

        for (int i = 0; i < elementsCount; i++)
        {
            int samplesCount = 0;
            int loopBegin = 0;
            int loopEnd = 0;
            double s_1 = 0.0;
            double s_2 = 0.0;
            string wavApsolute = outDir + "/SOUND_" + i.ToString().PadLeft(4, '0') + ".wav";

            using (BinaryWriter writer = new BinaryWriter(File.Open(wavApsolute, FileMode.Create)))
            {
                SNDStream.BaseStream.Seek(startPosition, SeekOrigin.Begin);
                SNDStream.BaseStream.Seek(i * 4 + 4, SeekOrigin.Current);
                int elementOffset = SNDStream.ReadUInt16() * 8;
                uint samp_freq = SNDStream.ReadUInt16() * 11U;
                int nextOffset = SNDStream.ReadUInt16() * 8;
                if (i == elementsCount - 1) nextOffset = eof;
                SNDStream.BaseStream.Seek(elementsPosition + elementOffset, SeekOrigin.Begin);
                //SNDStream.BaseStream.Seek(16, SeekOrigin.Current); //vag name?
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

                while (SNDStream.BaseStream.Position < elementsPosition + nextOffset)
                {
                    int predict_nr = SNDStream.ReadByte();
                    int shift_factor = predict_nr & 0xf;
                    predict_nr >>= 4;
                    int flags = SNDStream.ReadByte();

                    if (flags == 7)
                        break;
                    else if (flags == 6)
                        loopBegin = samplesCount;
                    else if (flags == 3)
                        loopEnd = samplesCount + 28;

                    for (int j = 0; j < 28; j += 2)
                    {
                        int d = SNDStream.ReadByte();
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
        }

        SNDStream.Close();
    }
}
