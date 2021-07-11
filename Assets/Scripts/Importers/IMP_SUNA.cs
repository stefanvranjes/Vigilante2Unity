using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class IMP_SUNA
{
    //FUN_2A6C (LOAD.DLL)
    public static void LoadAsset(string assetPath)
    {
        using (BinaryReader reader = new BinaryReader(File.Open(assetPath, FileMode.Open)))
        {
            int iVar1;
            int iVar2;
            uint uVar3;
            int iVar4;
            int iVar5;
            LevelManager levelManager = GameObject.FindObjectOfType<LevelManager>();

            if (reader.BaseStream.Length < 13)
                uVar3 = 0x1000;
            else
                uVar3 = (uint)reader.ReadUInt16(12) >> 8 | (uint)reader.ReadByte(12) << 8;

            iVar1 = (int)((uint)reader.ReadUInt16(0) >> 8 | (uint)(reader.ReadUInt16(0) & 0xf) << 8) * 2;
            iVar2 = (int)((uint)reader.ReadUInt16(2) >> 8 | (uint)(reader.ReadUInt16(2) & 0xf) << 8) * 2;
            iVar4 = GameManager.DAT_65C90[iVar1 + 1];
            iVar5 = GameManager.DAT_65C90[iVar2] * iVar4;

            if (iVar5 < 0)
                iVar5 += 4095;

            iVar5 = (iVar5 >> 12) * (int)uVar3;

            if (iVar5 < 0)
                iVar5 += 4095;

            iVar1 = -GameManager.DAT_65C90[iVar1] * (int)uVar3;

            if (iVar1 < 0)
                iVar1 += 4095;

            iVar4 = GameManager.DAT_65C90[iVar2 + 1] * iVar4;

            if (iVar4 < 0)
                iVar4 += 4095;

            iVar2 = (iVar4 >> 12) * (int)uVar3;

            if (iVar2 < 0)
                iVar2 += 4095;

            levelManager.DAT_10F8 = new Vector3Int
                (iVar5 >> 12, iVar1 >> 12, iVar2 >> 12);
            EditorUtility.SetDirty(levelManager.gameObject);
        }
    }
}
