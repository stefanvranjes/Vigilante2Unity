using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class IMP_AIMP
{
    public static void LoadAsset(string assetPath)
    {
        using (BinaryReader reader = new BinaryReader(File.Open(assetPath, FileMode.Open)))
        {
            LevelManager levelManager = GameObject.FindObjectOfType<LevelManager>();
            levelManager.aimpSize = (int)reader.BaseStream.Length;
            levelManager.aimpData = new ushort[levelManager.aimpSize / 2];
            int i = 0;

            while (reader.BaseStream.Position < reader.BaseStream.Length)
                levelManager.aimpData[i++] = reader.ReadUInt16();

#if UNITY_EDITOR
            EditorUtility.SetDirty(levelManager);
#endif
        }
    }
}
