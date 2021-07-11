using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class IMP_XBGM
{
    public static void LoadAsset(string assetPath, string savePath)
    {
        using (BinaryReader reader = new BinaryReader(File.Open(assetPath, FileMode.Open)))
        {
            int iVar1;
            LevelManager levelManager = GameObject.FindObjectOfType<LevelManager>();
            VigTerrain terrain = GameObject.FindObjectOfType<VigTerrain>();
            string relativePath = savePath;

            if (savePath.StartsWith(Application.dataPath))
                relativePath = "Assets" + savePath.Substring(Application.dataPath.Length);

            iVar1 = reader.ReadInt32BE();
            reader.ReadInt32();
            string bmpApsolute = savePath + "/XBGM" + 0.ToString().PadLeft(2, '0') + ".bmp";
            string bmpRelative = relativePath + "/XBGM" + 0.ToString().PadLeft(2, '0') + ".bmp";
            string matPath = relativePath + "/XBGM" + 0.ToString().PadLeft(2, '0') + ".mat";
            IMP_TIM.LoadTIM(reader, bmpApsolute);
            AssetDatabase.Refresh();
            Material newMaterial = new Material(AssetDatabase.LoadAssetAtPath(relativePath + "/default.mat", typeof(Material)) as Material);
            newMaterial.mainTexture = AssetDatabase.LoadAssetAtPath(bmpRelative, typeof(Texture2D)) as Texture2D;
            Utilities.SaveObjectToFile(newMaterial, matPath);
            levelManager.DAT_DD0 = newMaterial;
            levelManager.DAT_6398A = (short)(iVar1 << 4);
            levelManager.DAT_63972 = (short)((iVar1 - levelManager.DAT_DD0.mainTexture.height) * 16);
            levelManager.DAT_6397A = levelManager.DAT_63972;
            levelManager.DAT_63982 = levelManager.DAT_63972;
            levelManager.DAT_63992 = levelManager.DAT_6398A;
            levelManager.DAT_6399A = levelManager.DAT_6398A;

            if (levelManager.DAT_E48 == null)
                levelManager.DAT_E48 = levelManager.DAT_DD0;

            levelManager.DAT_E58 = AssetDatabase.LoadAssetAtPath("Assets/SHELL/glow.mat", typeof(Material)) as Material;
            EditorUtility.SetDirty(levelManager.gameObject);
        }
    }
}
