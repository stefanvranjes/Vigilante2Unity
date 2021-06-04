using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMP_JUNC
{
    public static void LoadAsset(string assetPath)
    {
        byte bVar1;
        short sVar2;
        int iVar3;
        int iVar4;
        int iVar5;
        int iVar7;

        LevelManager levelManager = GameObject.Find("GameControl").GetComponent<LevelManager>();

        using (BinaryReader reader = new BinaryReader(File.Open(assetPath, FileMode.Open)))
        {
            long length = reader.BaseStream.Length;
            VigTerrain terrain = GameObject.Find("Terrain").GetComponent<VigTerrain>();
            iVar3 = reader.ReadInt32();
            iVar4 = reader.ReadInt32();
            bVar1 = reader.ReadByte();
            iVar5 = reader.ReadByte();
            Junction newJunc = new Junction();
            newJunc.DAT_00 = iVar3;
            newJunc.DAT_08 = iVar4;
            newJunc.DAT_10 = bVar1;
            newJunc.DAT_11 = (byte)iVar5;

            if ((bVar1 & 0x40) == 0)
                newJunc.DAT_12 = 0;
            else
            {
                sVar2 = reader.ReadInt16();
                newJunc.DAT_12 = sVar2;
                length -= 2;
            }

            if ((bVar1 & 2) == 0)
            {
                iVar7 = terrain.FUN_1B750((uint)iVar3, (uint)iVar4);
                newJunc.DAT_04 = iVar7;
            }
            else
            {
                iVar7 = reader.ReadInt32();
                newJunc.DAT_04 = iVar7 - 0x100000;
                length -= 4;
            }

            iVar7 = 0;

            if (0 < iVar5)
            {
                newJunc.DAT_1C = new byte[iVar5];

                do
                {
                    newJunc.DAT_1C[iVar7] = 0;
                    iVar7++;
                } while (iVar7 < iVar5);
            }

            if (length < 11)
                ; //...
            else
            {
                iVar5 = reader.ReadInt16();
                newJunc.DAT_0C = levelManager.xobfList[iVar5];
                sVar2 = reader.ReadInt16();
                newJunc.DAT_14 = sVar2;
                sVar2 = reader.ReadInt16();
                newJunc.DAT_16 = sVar2;
                
            }
        }
    }
}
