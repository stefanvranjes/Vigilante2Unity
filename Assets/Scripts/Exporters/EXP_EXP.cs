using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

public static class EXP_EXP
{
    public static void ExtractEXP(string inFile, string outDir)
    {
        const int ATTR_QUANTITY = 21;
        string[] AttrName = new string[ATTR_QUANTITY] { "FORM", "TERRTITL", "TEXT", "XLSC", "HEAD", "XBGM", "SUNA", "COLS", "XBMP", "XTIN", "ZONE", "ZMAP", "AIMP", "RECT", "XWAT", "XRTP", "PLTX", "JUNC", ".", "RSEG", "BSP ", };
        int[] AttrLenght = new int[ATTR_QUANTITY] { 4, 8, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 1, 4, 4, };
        int[] AttrQuanity = new int[ATTR_QUANTITY] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, };
        long spos;

        List<string> pack_list = new List<string>();

        BinaryReader EXPStream = new BinaryReader(File.Open(inFile, FileMode.Open));
        EXPStream.BaseStream.Seek(8, SeekOrigin.Begin);

        if (!Directory.Exists(outDir))
        {
            Directory.CreateDirectory(outDir);
        }

        while (EXPStream.BaseStream.Position < EXPStream.BaseStream.Length)
        {
            spos = EXPStream.BaseStream.Position;

            for (int attr = 0; attr < ATTR_QUANTITY; attr++)
            {
                string Compare = Encoding.UTF8.GetString(EXPStream.ReadBytes(AttrLenght[attr]));

                if (Equals(Compare, AttrName[attr]))
                {
                    int data_size = BigEndian.ReadInt32BE(EXPStream);
                    long data_size_aligned = (data_size + 1) & 0xfffffffe;
                    byte[] buff = EXPStream.ReadBytes(Convert.ToInt32(data_size_aligned));

                    string outFile = outDir + Path.DirectorySeparatorChar;
                    outFile += AttrName[attr] + AttrQuanity[attr].ToString("D4");

                    pack_list.Add(AttrName[attr] + AttrQuanity[attr].ToString("D4"));

                    File.WriteAllBytes(outFile, buff);
                    AttrQuanity[attr]++;
                    break;
                }
                else
                {
                    EXPStream.BaseStream.Seek(spos, SeekOrigin.Begin);
                }
            }
        }
        File.WriteAllLines(outDir + Path.DirectorySeparatorChar + "!archive.cfg", pack_list);
        EXPStream.Close();

    }
}
