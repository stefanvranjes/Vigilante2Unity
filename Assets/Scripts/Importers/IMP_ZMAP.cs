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

            terr.unk3 = 0x8000000;
            terr.unk1 = 0x8000000;
            terr.unk2 = 0;
            terr.unk4 = 0;

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

                        if (terr.unk1 < uVar2)
                            uVar2 = (uint)terr.unk1;

                        terr.unk1 = (int)uVar2;
                        int iVar = (int)uVar3;

                        if (uVar3 < terr.unk2)
                            iVar = terr.unk2;

                        terr.unk2 = iVar;
                        uVar2 = (uint)iVar1 << 22;

                        if (terr.unk3 < uVar2)
                            uVar2 = (uint)terr.unk3;

                        terr.unk3 = (int)uVar2;
                        iVar = iVar2;

                        if (terr.unk4 < iVar2)
                            iVar = terr.unk4;

                        terr.unk4 = iVar;
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
