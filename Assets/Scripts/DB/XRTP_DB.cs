using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRTP_DB : MonoBehaviour
{
    public int DAT_0C; //0x0C
    public int DAT_10; //0x10
    public int DAT_14; //0x14
    public int DAT_1C; //0x1C
    public int DAT_24; //0x24
    public int DAT_28; //0x28
    public ushort DAT_2C; //0x2C
    public short DAT_2E; //0x2E
    public short DAT_30; //0x30

    //76B8 (LOAD.DLL)
    public void LoadDB(string assetPath)
    {
        ushort uVar2;
        short sVar3;
        int iVar4;
        int uVar5;
        int iVar6;

        LevelManager levelManager = GameObject.Find("GameControl").GetComponent<LevelManager>();

        using (BinaryReader reader = new BinaryReader(File.Open(assetPath, FileMode.Open)))
        {
            levelManager.xrtpList.Add(this);
            uVar5 = reader.ReadInt32BE();
            DAT_24 = uVar5;
            uVar5 = reader.ReadInt32BE();
            DAT_28 = uVar5;
            uVar5 = reader.ReadUInt16BE();
            DAT_1C = uVar5;
            uVar2 = reader.ReadUInt16BE();
            DAT_2C = uVar2;
            DAT_14 = 0;
            DAT_0C = 0;
            DAT_10 = 0;

            if (DAT_1C < 16)
                DAT_1C = 48;

            iVar4 = DAT_28 >> 8;
            iVar6 = DAT_24 >> 8;
            sVar3 = (short)(int)Utilities.SquareRoot(iVar4 * iVar4 + iVar6 * iVar6);
            iVar4 = DAT_24;
            DAT_30 = (short)(sVar3 - 128);

            if (iVar4 < 0)
                iVar4 += 255;

            iVar6 = DAT_28;

            if (iVar6 < 0)
                iVar6 += 255;

            iVar4 = (iVar4 >> 8) * (iVar6 >> 8);

            if (iVar4 < 0)
                iVar4 += 127;

            DAT_2E = (short)(iVar4 >> 7);

            if (12 < reader.BaseStream.Length)
            {

            }
        }
    }
}
