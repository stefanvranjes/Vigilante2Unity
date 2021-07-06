using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class IMP_BSP
{
    //FUN_2BB4 (LOAD.DLL)
    public static void LoadAsset(string assetPath)
    {
        using (BinaryReader reader = new BinaryReader(File.Open(assetPath, FileMode.Open)))
        {
            LevelManager levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();

            levelManager.staticObjs = FUN_26F0(reader);
        }
    }

    //FUN_26F0 (LOAD.DLL)
    private static _CLASS_102C FUN_26F0(BinaryReader reader)
    {
        int iVar1;
        _CLASS_102C piVar2;
        _CLASS_102C cVar3;

        iVar1 = reader.ReadUInt16BE();

        if (iVar1 == 0)
        {
            piVar2 = new _CLASS_102C();
            piVar2.DAT_00 = 0; //not sure
            piVar2.DAT_08 = null; //not sure
        }
        else
        {
            piVar2 = new _CLASS_102C();
            piVar2.DAT_00 = iVar1;
            iVar1 = reader.ReadInt32BE();
            piVar2.DAT_04 = iVar1;
            cVar3 = FUN_26F0(reader);
            piVar2.DAT_08 = cVar3;
            cVar3 = FUN_26F0(reader);
            piVar2.DAT_0C = cVar3;
        }

        return piVar2;
    }
}