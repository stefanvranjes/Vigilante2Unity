using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class IMP_TEXT
{
    public static void LoadAsset(string assetPath)
    {
        using (BinaryReader reader = new BinaryReader(File.Open(assetPath, FileMode.Open)))
        {
            LevelManager levelManager = GameObject.FindObjectOfType<LevelManager>();

            levelManager.desc = new string(reader.ReadChars((int)reader.BaseStream.Length - 1));
#if UNITY_EDITOR
            EditorUtility.SetDirty(levelManager.gameObject);
#endif
        }
    }
}
