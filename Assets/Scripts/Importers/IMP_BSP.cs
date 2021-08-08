using System.IO;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class IMP_BSP
{
    public static void LoadAsset(string assetPath)
    {
        using (BinaryReader reader = new BinaryReader(File.Open(assetPath, FileMode.Open)))
        {
            LevelManager levelManager = GameObject.FindObjectOfType<LevelManager>();

            levelManager.bspData = reader.ReadBytes((int)reader.BaseStream.Length);
#if UNITY_EDITOR
            EditorUtility.SetDirty(levelManager);
#endif
        }
    }

    //FUN_2BB4 (LOAD.DLL)
    public static void LoadData(byte[] bytes)
    {
        MemoryStream stream = new MemoryStream(bytes);

        using (BinaryReader reader = new BinaryReader(stream, Encoding.Default, true))
        {
            GameManager.instance.bspTree = FUN_26F0(reader);
        }
    }

    //FUN_26F0 (LOAD.DLL)
    private static BSP FUN_26F0(BinaryReader reader)
    {
        int iVar1;
        BSP piVar2;
        BSP cVar3;

        iVar1 = reader.ReadUInt16BE();

        if (iVar1 == 0)
        {
            piVar2 = new BSP();
            piVar2.LDAT_04 = new List<VigTuple>();
            piVar2.DAT_00 = 0; //not sure
            piVar2.DAT_08 = null; //not sure
        }
        else
        {
            piVar2 = new BSP();
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