using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class IMP_HEAD
{
    //FUN_28A4 (LOAD.DLL)
    public static void LoadAsset(string assetPath)
    {
        ushort uVar1;
        ushort uVar2;
        ushort uVar3;
        ushort uVar4;
        int iVar5;

        LevelManager levelManager = GameObject.FindObjectOfType<LevelManager>();

        using (BinaryReader reader = new BinaryReader(File.Open(assetPath, FileMode.Open)))
        {
            uVar1 = reader.ReadUInt16BE();
            uVar3 = reader.ReadUInt16BE();
            uVar4 = reader.ReadUInt16BE();
            reader.ReadUInt16BE();
            reader.ReadUInt16BE();
            iVar5 = (int)(reader.BaseStream.Length - 10) / 2;

            if (0 < iVar5)
            {
                levelManager.DAT_C18 = new ushort[iVar5];

                for (int i = 0; i < iVar5; i++)
                {
                    uVar2 = reader.ReadUInt16BE();
                    levelManager.DAT_C18[i] = uVar2;
                }
            }

            /*if ((gameManager.DAT_40 & 0x40) != 0) //this needs to change at runtime
                gameManager.DAT_C18[4] = 0;*/ 

            levelManager.DAT_DBA = (short)uVar1;
            FUN_508AC(uVar3, uVar4);
#if UNITY_EDITOR
            EditorUtility.SetDirty(levelManager.gameObject);
#endif
        }
    }

    private static void FUN_508AC(int param1, int param2)
    {
        LevelManager levelManager = GameObject.FindObjectOfType<LevelManager>();
        levelManager.DAT_118C = 0;
        levelManager.DAT_1180 = param1;
        levelManager.DAT_1184 = param2;
    }
}
