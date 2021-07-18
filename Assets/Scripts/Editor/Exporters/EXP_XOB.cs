using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EXP_XOB
{
    public static void ExtractXOB(string assetPath, string outputDir)
    {
        const string identifier = "XOBFBIN ";

        using (BinaryReader reader = new BinaryReader(File.Open(assetPath, FileMode.Open)))
        {
            string headerString = new string(reader.ReadChars(8));

            if (headerString == identifier)
            {
                reader.ReadInt32();                                 // skip

                int tmdElements = reader.ReadInt32();               // 3D Models
                int tmdOffset = reader.ReadInt32();
                int cbbElements = reader.ReadInt32();               // Collisions
                int cbbOffset = reader.ReadInt32();
                int timElements = reader.ReadInt32();               // Textures
                int timOffset = reader.ReadInt32();
                int iniElements = reader.ReadInt32();               // Animations
                int iniOffset = (int)reader.BaseStream.Position;

                #region TMD
                long tmdPosition = reader.BaseStream.Seek(tmdOffset + 12, SeekOrigin.Begin);

                for (int i = 0; i < tmdElements; i++)
                {
                    reader.BaseStream.Seek(tmdPosition + i * 4, SeekOrigin.Begin);
                    int elementOffset = reader.ReadInt32();
                    int elementSize = reader.ReadInt32() - elementOffset;

                    reader.BaseStream.Seek(tmdPosition + elementOffset, SeekOrigin.Begin);
                    byte[] elementBuffer = reader.ReadBytes(elementSize);

                    string outputFile = outputDir + Path.DirectorySeparatorChar;
                    outputFile += "3DMODEL_" + i.ToString().PadLeft(3, '0') + ".tmd";

                    File.WriteAllBytes(outputFile, elementBuffer);
                }
                #endregion

                #region CBB
                long cbbPosition = reader.BaseStream.Seek(cbbOffset + 12, SeekOrigin.Begin);

                for (int i = 0; i < cbbElements; i++)
                {
                    reader.BaseStream.Seek(cbbPosition + i * 4, SeekOrigin.Begin);
                    int elementOffset = reader.ReadInt32();
                    int elementSize = reader.ReadInt32() - elementOffset;

                    reader.BaseStream.Seek(cbbPosition + elementOffset, SeekOrigin.Begin);
                    byte[] elementBuffer = reader.ReadBytes(elementSize);

                    string outputFile = outputDir + Path.DirectorySeparatorChar;
                    outputFile += "COLLISION_" + i.ToString().PadLeft(3, '0') + ".cbb";

                    File.WriteAllBytes(outputFile, elementBuffer);
                }
                #endregion

                #region TIM
                long timPosition = reader.BaseStream.Seek(timOffset + 12, SeekOrigin.Begin);

                for (int i = 0; i < timElements; i++)
                {
                    reader.BaseStream.Seek(timPosition + i * 4, SeekOrigin.Begin);
                    int elementOffset = reader.ReadInt32();
                    int elementSize = reader.ReadInt32() - elementOffset;

                    reader.BaseStream.Seek(timPosition + elementOffset, SeekOrigin.Begin);
                    byte[] elementBuffer = reader.ReadBytes(elementSize);

                    string outputFile = outputDir + Path.DirectorySeparatorChar;
                    outputFile += "TEXTURE_" + i.ToString().PadLeft(3, '0') + ".tim";

                    File.WriteAllBytes(outputFile, elementBuffer);
                }
                #endregion

                #region INI
                long iniPosition = reader.BaseStream.Seek(iniOffset - 4, SeekOrigin.Begin);
                int size = iniElements * 0x1C + 4;

                byte[] buffer = reader.ReadBytes(size);

                string output = outputDir + Path.DirectorySeparatorChar;
                output += "CONFIG" + ".ini";

                File.WriteAllBytes(output, buffer);
                #endregion
            }
        }
    }
}
