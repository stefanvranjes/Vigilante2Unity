using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMP_ZMAP
{
    public static void LoadAsset(string assetPath)
    {
        VigTerrain terr = GameObject.FindObjectOfType<VigTerrain>();

        using (BinaryReader reader = new BinaryReader(File.Open(assetPath, FileMode.Open)))
        {
            terr.chunks = new ushort[32 * 32];

            int iVar1 = 0; //r10
            int iVar2 = 0x400000; //r9

            terr.DAT_DEC = 0x8000000;
            terr.DAT_DE4 = 0x8000000;
            terr.DAT_DE8 = 0;
            terr.DAT_DF0 = 0;

            do
            {
                int iVar3 = 0; //r7
                uint uVar3 = 0x400000; //r6
                int arrayIndex = iVar1 << 2; //r8

                do
                {
                    uint uVar1 = reader.ReadUInt16(); //r2
                    uint uVar2 = uVar1 >> 8; //r4
                    uVar1 = (uVar1 & 0xFF) << 8;
                    uVar2 |= uVar1;
                    uVar1 = uVar2 << 2;
                    int zoneIndex = (int)uVar2;
                    terr.chunks[arrayIndex / 4] = (ushort)zoneIndex;

                    if (uVar2 != 0)
                    {
                        uVar2 = (uint)iVar3 << 22;

                        if (terr.DAT_DE4 < uVar2)
                            uVar2 = (uint)terr.DAT_DE4;

                        terr.DAT_DE4 = (int)uVar2;
                        int iVar = (int)uVar3;

                        if (uVar3 < terr.DAT_DE8)
                            iVar = terr.DAT_DE8;

                        terr.DAT_DE8 = iVar;
                        uVar2 = (uint)iVar1 << 22;

                        if (terr.DAT_DEC < uVar2)
                            uVar2 = (uint)terr.DAT_DEC;

                        terr.DAT_DEC = (int)uVar2;
                        iVar = iVar2;

                        if (iVar2 < terr.DAT_DF0)
                            iVar = terr.DAT_DF0;

                        terr.DAT_DF0 = iVar;
                    }

                    uint uVar = 0x400000; //r3
                    uVar3 += uVar;
                    iVar3++;
                    arrayIndex += 128;
                } while (iVar3 < 32);

                iVar1++;
                iVar2 += 0x400000;
            } while (iVar1 < 32);
        }
    }
}
