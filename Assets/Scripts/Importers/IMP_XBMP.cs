using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMP_XBMP
{
    public static void LoadAsset(string assetPath, string bmp)
    {
        using (BinaryReader reader = new BinaryReader(File.Open(assetPath, FileMode.Open)))
        {
            int iVar3;
            int iVar4;
            int local_210;
            MemoryStream auStack520;
            LevelManager levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();

            iVar4 = 0;
            iVar3 = 0;
            levelManager.DAT_DF8 = new Texture2D[16];
            long begin = reader.BaseStream.Position;
            Utilities.SetFarColor(levelManager.DAT_DDC.r, levelManager.DAT_DDC.g, levelManager.DAT_DDC.b);

            do
            {
                reader.BaseStream.Seek(begin, SeekOrigin.Begin);
                auStack520 = new MemoryStream();

                using (BinaryWriter writer = new BinaryWriter(auStack520, Encoding.Default, true))
                {
                    writer.Write(reader.ReadBytes(16));
                    local_210 = reader.ReadInt16();
                    writer.Write(local_210);
                    writer.Write(reader.ReadInt16());
                    Utilities.FUN_18C54(reader, local_210, writer, iVar3);
                    writer.Write(reader.ReadBytes((int)(reader.BaseStream.Length - reader.BaseStream.Position)));
                    writer.BaseStream.Seek(0, SeekOrigin.Begin);
                }

                using (BinaryReader reader2 = new BinaryReader(auStack520, Encoding.Default, true))
                {
                    string bmpPath = Path.GetFileNameWithoutExtension(bmp) + "_FAR" + iVar4.ToString().PadLeft(2, '0') + ".bmp";
                    IMP_TIM.LoadTIM(reader2, bmpPath);
                    levelManager.DAT_DF8[iVar3] = Resources.Load<Texture2D>(bmpPath.Substring(Application.dataPath.Length + 16));
                }

                iVar4++;
                iVar3 += 0x100;
            } while (iVar4 < 16);
        }
    }
}
