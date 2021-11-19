using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class IMP_XBMP
{
    public static void LoadAsset(string assetPath, string savePath)
    {
        using (BinaryReader reader = new BinaryReader(File.Open(assetPath, FileMode.Open)))
        {
            int iVar3;
            int iVar4;
            int local_210;
            MemoryStream auStack520;
            LevelManager levelManager = GameObject.FindObjectOfType<LevelManager>();
            string relativePath = savePath;

            iVar4 = 0;
            iVar3 = 0;
            levelManager.DAT_DF8 = new Material[16];
            long begin = reader.BaseStream.Position;
            Utilities.SetFarColor(levelManager.DAT_DDC.r, levelManager.DAT_DDC.g, levelManager.DAT_DDC.b);

            if (savePath.StartsWith(Application.dataPath))
                relativePath = "Assets" + savePath.Substring(Application.dataPath.Length);

            do
            {
                reader.BaseStream.Seek(begin, SeekOrigin.Begin);
                auStack520 = new MemoryStream();

                using (BinaryWriter writer = new BinaryWriter(auStack520, Encoding.Default, true))
                {
                    writer.Write(reader.ReadBytes(16));
                    local_210 = reader.ReadInt16();
                    writer.Write((short)local_210);
                    writer.Write(reader.ReadInt16());
                    Utilities.FUN_18C54(reader, local_210, writer, iVar3);
                    writer.Write(reader.ReadBytes((int)(reader.BaseStream.Length - reader.BaseStream.Position)));
                    writer.BaseStream.Seek(4, SeekOrigin.Begin);
                }

                using (BinaryReader reader2 = new BinaryReader(auStack520, Encoding.Default, true))
                {
                    string bmpApsolute = savePath + "/XBMP_FAR" + iVar4.ToString().PadLeft(2, '0') + ".bmp";
                    string bmpRelative = relativePath + "/XBMP_FAR" + iVar4.ToString().PadLeft(2, '0') + ".bmp";
                    string matPath = relativePath + "/XBMP_FAR" + iVar4.ToString().PadLeft(2, '0') + ".mat";
                    IMP_TIM.LoadTIM(reader2, bmpApsolute);
                    Material newMaterial = null;
#if UNITY_EDITOR
                    AssetDatabase.Refresh();
                    newMaterial = new Material(AssetDatabase.LoadAssetAtPath(relativePath + "/default.mat", typeof(Material)) as Material);
                    newMaterial.mainTexture = AssetDatabase.LoadAssetAtPath(bmpRelative, typeof(Texture2D)) as Texture2D;
                    Utilities.SaveObjectToFile(newMaterial, matPath);
#endif
                    levelManager.DAT_DF8[iVar4] = newMaterial;
                }

                iVar4++;
                iVar3 += 0x100;
            } while (iVar4 < 16);

#if UNITY_EDITOR
            EditorUtility.SetDirty(levelManager.gameObject);
#endif
        }
    }
}
