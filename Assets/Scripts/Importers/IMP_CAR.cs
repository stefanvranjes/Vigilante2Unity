using System;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

public class IMP_CAR
{
    public static void LoadAsset(string assetPath, string bmp)
    {
        string fileName = Path.GetFileName(assetPath);

        using (BinaryReader reader = new BinaryReader(File.Open(assetPath, FileMode.Open)))
        {
            reader.BaseStream.Seek(0x10, SeekOrigin.Begin);
            int textureOffset = reader.ReadInt32();
            reader.BaseStream.Seek(textureOffset, SeekOrigin.Begin);

            #region TEXTURE
            int unknown = reader.ReadInt32();
            int width = reader.ReadInt16();
            int height = reader.ReadInt16();
            
            using (BinaryWriter writer = new BinaryWriter(File.Open(bmp, FileMode.Create)))
            {
                writer.Write((short)0x4D42); //Signature
                writer.Write(width * height * 4 + 56); //File Size
                writer.Write((short)0); //Reserved1
                writer.Write((short)0); //Reserved2
                writer.Write(54); //File Offset to Pixel Array
                writer.Write(40); //DIB Header Size
                writer.Write(width); //Image Width
                writer.Write(height); //Image Height
                writer.Write(0x00200001); //Compression
                writer.Write((long)0);
                writer.Write(2834); //X Pixels Per Meter
                writer.Write(2834); //Y Pixels Per Meter
                writer.Write((long)0);

                ushort redMask = 0x7C00;
                ushort greenMask = 0x3E0;
                ushort blueMask = 0x1F;

                for (int i = 0; i < width * height; i++)
                {
                    ushort color = reader.ReadUInt16();

                    byte R5 = (byte)((color & redMask) >> 10);
                    byte G5 = (byte)((color & greenMask) >> 5);
                    byte B5 = (byte)(color & blueMask);

                    byte R8 = (byte)(R5 << 3);
                    byte G8 = (byte)(G5 << 3);
                    byte B8 = (byte)(B5 << 3);

                    byte A = 255;

                    if (color >> 15 == 0)
                    {
                        if (R8 == 0 && G8 == 0 && B8 == 0)
                            A = 0;
                        else
                            A = 255;
                    }
                    else
                    {
                        if (R8 == 0 && G8 == 0 && B8 == 0)
                            A = 255;
                        else
                            A = 255;
                    }

                    /*pixels[i].r = A == 0 ? R8 : (byte)0;
                    pixels[i].g = A == 0 ? G8 : (byte)0;
                    pixels[i].b = A == 0 ? B8 : (byte)0;
                    pixels[i].a = A;*/

                    writer.Write(R8);
                    writer.Write(G8);
                    writer.Write(B8);
                    writer.Write(A);
                }

                writer.Write((short)0);
            }

            #endregion
        }
    }
}
