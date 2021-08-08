using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RSEG_DB : MonoBehaviour
{
    public JUNC_DB[] DAT_00; //0x00
    public ushort DAT_08; //0x08
    public ushort DAT_0A; //0x0A
    public ushort DAT_0C; //0x0C
    public int[] DAT_10; //0x10
    public int[] DAT_14; //0x14
    public int DAT_20; //0x20
    public int DAT_24; //0x24
    public int DAT_28; //0x28
    public int DAT_2C; //0x2C
    public int DAT_30; //0x30
    public int DAT_34; //0x34

    public bool LoadDB(string assetPath)
    {
        ushort uVar1;
        int iVar3;
        RSEG_DB dbVar4;
        int iVar5;
        JUNC_DB dbVar6;
        JUNC_DB dbVar7;

        LevelManager levelManager = GameObject.FindObjectOfType<LevelManager>();

        using (BinaryReader reader = new BinaryReader(File.Open(assetPath, FileMode.Open)))
        {
            if (reader == null)
                return false;

            if (reader.BaseStream.Length == 22)
            {
                uVar1 = reader.ReadByte();
                DAT_0A = uVar1;
                uVar1 = reader.ReadByte();
                DAT_08 = uVar1;
                DAT_0C = 0;
            }
            else
            {
                uVar1 = reader.ReadUInt16BE();
                DAT_0A = uVar1;
                uVar1 = reader.ReadUInt16BE();
                DAT_08 = uVar1;
                uVar1 = reader.ReadUInt16BE();
                DAT_0C = uVar1;
            }

            iVar3 = reader.ReadUInt16BE();
            DAT_00 = new JUNC_DB[2];
            dbVar7 = levelManager.juncList[iVar3];
            DAT_00[0] = dbVar7;
            iVar3 = reader.ReadUInt16BE();
            dbVar6 = levelManager.juncList[iVar3];
            DAT_00[1] = dbVar6;
            DAT_10 = new int[2];
            iVar3 = reader.ReadInt32BE();
            DAT_10[0] = iVar3;
            DAT_14 = new int[2];
            iVar3 = reader.ReadInt32BE();
            DAT_14[0] = iVar3;
            iVar3 = reader.ReadInt32BE();
            DAT_10[1] = iVar3;
            iVar3 = reader.ReadInt32BE();
            DAT_14[1] = iVar3;
            dbVar4 = dbVar7.DAT_1C[0];
            iVar5 = 0;

            while(dbVar4 != null)
            {
                dbVar4 = dbVar7.DAT_1C[iVar5 + 1];
                iVar5++;
            }

            dbVar7.DAT_1C[iVar5] = this;
            dbVar4 = dbVar6.DAT_1C[0];
            iVar5 = 0;

            while (dbVar4 != null)
            {
                dbVar4 = dbVar6.DAT_1C[iVar5 + 1];
                iVar5++;
            }

            dbVar6.DAT_1C[iVar5] = this;
            FUN_50EFC();

#if UNITY_EDITOR
            EditorUtility.SetDirty(gameObject);
            EditorUtility.SetDirty(levelManager.gameObject);
            PrefabUtility.RecordPrefabInstancePropertyModifications(gameObject);
#endif
            return true;
        }
    }

    private void FUN_50EFC()
    {
        int iVar1;
        int iVar2;
        int iVar3;

        iVar1 = DAT_00[0].DAT_00.x - DAT_00[1].DAT_00.x << 1; //r4
        iVar2 = DAT_10[0] - DAT_10[1]; //r3
        iVar3 = (iVar2 << 1) + iVar2; //r2
        iVar3 = iVar1 + iVar3;

        if (iVar3 < 0)
            iVar3 += 15;

        DAT_20 = iVar3 >> 4;
        iVar1 = DAT_00[0].DAT_00.z - DAT_00[1].DAT_00.z << 1; //r4
        iVar2 = DAT_14[0] - DAT_14[1]; //r3
        iVar3 = (iVar2 << 1) + iVar2; //r2
        iVar3 = iVar1 + iVar3;

        if (iVar3 < 0)
            iVar3 += 15;

        DAT_24 = iVar3 >> 4;
        iVar3 = DAT_00[1].DAT_00.x - DAT_00[0].DAT_00.x; //r2
        iVar2 = (iVar3 << 1) + iVar3; //r3
        iVar3 = (DAT_10[0] << 1) + DAT_10[0] << 1;
        iVar2 -= iVar3;
        iVar3 = iVar2 + ((DAT_10[1] << 1) + DAT_10[1]);

        if (iVar3 < 0)
            iVar3 += 15;

        DAT_28 = iVar3 >> 4;
        iVar3 = DAT_00[1].DAT_00.z - DAT_00[0].DAT_00.z; //r2
        iVar2 = (iVar3 << 1) + iVar3; //r3
        iVar3 = (DAT_14[0] << 1) + DAT_14[0] << 1;
        iVar2 -= iVar3;
        iVar2 = iVar2 + ((DAT_14[1] << 1) + DAT_14[1]);

        if (iVar2 < 0)
            iVar2 += 15;

        DAT_2C = iVar2 >> 4;
        iVar2 = (DAT_10[0] << 1) + DAT_10[0]; //r3

        if (iVar2 < 0)
            iVar2 += 15;

        DAT_30 = iVar2 >> 4;
        iVar3 = (DAT_14[0] << 1) + DAT_14[0]; //r3

        if (iVar3 < 0)
            iVar3 += 15;

        DAT_34 = iVar3 >> 4;
    }
}
