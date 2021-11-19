using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class IMP_COLS
{
    //FUN_7FE0 (LOAD.DLL)
    public static void LoadAsset(string assetPath)
    {
        using (BinaryReader reader = new BinaryReader(File.Open(assetPath, FileMode.Open)))
        {
            int iVar1;
            Color32[] puVar2;
            int iVar3;
            int iVar4;
            long lVar5;
            LevelManager levelManager = GameObject.FindObjectOfType<LevelManager>();
            VigTerrain terrain = GameObject.FindObjectOfType<VigTerrain>();

            iVar3 = 0;
            unchecked { lVar5 = (int)0x84210843; }
            levelManager.DAT_E08 = new Color32
                (reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
            levelManager.DAT_DA4 = new Color32
                (reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
            levelManager.DAT_DDC = new Color32
                (reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), 0x30);
            reader.BaseStream.Seek(1, SeekOrigin.Current);
            levelManager.DAT_E04 = new Color32
                (reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
            puVar2 = terrain.DAT_B9370;
            levelManager.DAT_DAC = new Color32
                (reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
            levelManager.DAT_DBC = new Color32
                (reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
            levelManager.DAT_D98 = new Color32
                (reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
            levelManager.DAT_DE0 = new Color32
                (reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte());

            do
            {
                iVar1 = (levelManager.DAT_DAC.r - levelManager.DAT_E04.r) * iVar3; //v1
                iVar4 = (int)(iVar1 * lVar5 >> 32) + iVar1 >> 4; //v0
                iVar4 -= iVar1 >> 31;
                iVar4 = iVar4 + levelManager.DAT_E04.r + (int)((uint)(iVar1 >> 31) << 31);
                puVar2[iVar3].r = (byte)(iVar4 >> 1);
                iVar1 = (levelManager.DAT_DAC.g - levelManager.DAT_E04.g) * iVar3; //v1
                iVar4 = (int)(iVar1 * lVar5 >> 32) + iVar1 >> 4; //v0
                iVar4 -= iVar1 >> 31;
                iVar4 = iVar4 + levelManager.DAT_E04.g + (int)((uint)(iVar1 >> 31) << 31);
                puVar2[iVar3].g = (byte)(iVar4 >> 1);
                iVar1 = (levelManager.DAT_DAC.b - levelManager.DAT_E04.b) * iVar3; //v1
                iVar4 = (int)(iVar1 * lVar5 >> 32) + iVar1 >> 4; //v0
                iVar4 -= iVar1 >> 31;
                iVar4 = iVar4 + levelManager.DAT_E04.b + (int)((uint)(iVar1 >> 31) << 31);
                puVar2[iVar3].b = (byte)(iVar4 >> 1);
                puVar2[iVar3].a = 0;
                iVar3++;
            } while (iVar3 < 32);

            levelManager.DAT_738 = new Matrix3x3()
            {
                V00 = (short)((levelManager.DAT_DAC.r - levelManager.DAT_E04.r) * 16),
                V01 = 0,
                V02 = 0,
                V10 = (short)((levelManager.DAT_DAC.g - levelManager.DAT_E04.g) * 16),
                V11 = 0,
                V12 = 0,
                V20 = (short)((levelManager.DAT_DAC.b - levelManager.DAT_E04.b) * 16),
                V21 = 0,
                V22 = 0
            };

            terrain.DAT_B9314 = new Color32[]
            {
                levelManager.DAT_E08,
                levelManager.DAT_E08
            };

            terrain.DAT_B932C = new Color32[]
            {
                levelManager.DAT_DA4,
                levelManager.DAT_DA4
            };

#if UNITY_EDITOR
            EditorUtility.SetDirty(levelManager.gameObject);
            EditorUtility.SetDirty(terrain.gameObject);
#endif
        }
    }
}
