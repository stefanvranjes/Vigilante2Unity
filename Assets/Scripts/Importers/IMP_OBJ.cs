using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMP_OBJ
{
    //FUN_34F8 (LOAD.DLL)
    public static void LoadAsset(string assetPath)
    {
        using (BinaryReader reader = new BinaryReader(File.Open(assetPath, FileMode.Open)))
        {
            string obj = new string(reader.ReadChars(4));

            if (obj == "OBJ ")
            {
                if (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    do
                    {
                        string header = new string(reader.ReadChars(4));
                        int chunkSize = reader.ReadInt32BE();

                        if (header == "HEAD")
                            LoadHEAD(reader, chunkSize);
                    } while (reader.BaseStream.Position != reader.BaseStream.Length);
                }
            }
        }
    }

    //FUN_2CF0 (LOAD.DLL)
    private static VigObject LoadHEAD(BinaryReader reader, int size)
    {
        byte bVar1;
        short sVar2;
        short sVar3;
        short sVar4;
        short sVar5;
        ushort uVar6;
        uint uVar7;
        uint uVar8;
        int iVar9;
        Type pcVar10;
        int iVar11;
        VigObject ppcVar12;
        VigObject piVar15;
        int ppiVar16;
        uint pcVar17;
        VigObject oVar18;
        bool bVar19;
        int local_30;
        Vector3Int local_88;
        Vector3Int local_78;
        string auStack112;
        LevelManager levelManager = GameManager.instance.levelManager;
        GameObject obj;

        local_30 = reader.ReadByte();
        uVar7 = reader.ReadByte();
        sVar2 = reader.ReadInt16BE();
        uVar8 = reader.ReadUInt32BE();
        pcVar17 = uVar8 & 0xfffc67fe;
        local_88 = new Vector3Int();
        local_88.x = reader.ReadInt32BE();
        iVar9 = reader.ReadInt32BE();
        local_88.y = iVar9 - 0x100000;
        local_88.z = reader.ReadInt32BE();
        local_78 = new Vector3Int();
        sVar3 = reader.ReadInt16BE();
        local_78.x = sVar3;
        sVar4 = reader.ReadInt16BE();
        local_78.y = sVar4;
        local_78.z = reader.ReadInt16BE();
        sVar5 = reader.ReadInt16BE();
        iVar9 = sVar5 + 42;
        sVar3 = reader.ReadInt16BE();
        uVar6 = (ushort)reader.ReadInt32BE();
        auStack112 = reader.ReadNullTerminatedString();
        pcVar10 = Utilities.FUN_14E1C(0, auStack112);

        if (pcVar10 is null)
        {
            if (GameManager.instance.DAT_1050 == null)
                pcVar10 = null;
            else
                pcVar10 = Utilities.FUN_14DAC(GameManager.instance.DAT_1050[0], auStack112); //improvised

            if (pcVar10 == null)
                pcVar10 = typeof(Destructible);
        }

        if ((uVar8 & 2) != 0 && uVar7 != 5)
        {
            if (GameManager.instance.gameMode < _GAME_MODE.Versus)
            {
                pcVar17 = uVar8 & 0xfffc67fc;
                goto LAB_2E74;
            }

            return null;
        }

        LAB_2E74:
        if ((pcVar17 & 4) != 0 && levelManager.xobfList[iVar9].animations != null)
        {
            iVar11 = (int)GameManager.FUN_2AC5C();
            MemoryStream stream = new MemoryStream(levelManager.xobfList[iVar9].animations);

            using (BinaryReader reader2 = new BinaryReader(stream, Encoding.Default, true))
                GameManager.instance.DAT_EA0 = (ushort)-(iVar11 * reader2.ReadInt32() >> 15);
        }

        if (6 < uVar7)
            return null;

        bVar1 = (byte)uVar7;
        ppcVar12 = null; //not in the original code

        switch (uVar7)
        {
            case 0:
                ppcVar12 = Utilities.FUN_31D30(pcVar10, levelManager.charsList[iVar9], sVar3,
                                                (pcVar17 & 4) << 1);
                ppcVar12.flags = pcVar17;
                ppcVar12.type = bVar1;
                ppcVar12.id = sVar2;
                ppcVar12.ai = (byte)local_30;
                ppcVar12.screen = local_88;
                ppcVar12.vr = local_78;
                ppcVar12.maxFullHealth = uVar6;
                ppcVar12.maxHalfHealth = uVar6;
                bVar1 = (byte)GameManager.FUN_2AC5C();
                ppcVar12.DAT_19 = bVar1;
                ppcVar12.ApplyTransformation();
                ppcVar12.FUN_2D1DC();
                ppcVar12.FUN_2C958();

                if (!pcVar10.IsSubclassOf(typeof(VigObject)))
                    iVar9 = 0;
                else
                    iVar9 = (int)ppcVar12.Execute(1, 0);

                if (-1 < iVar9)
                {
                    if ((ppcVar12.flags & 8) != 0 && ppcVar12.vShadow == null)
                        ppcVar12.FUN_4C98C();

                    if ((ppcVar12.flags & 4) != 0)
                        GameManager.instance.FUN_30080(GameManager.instance.DAT_10A8, ppcVar12);

                    if ((ppcVar12.flags & 0x80) != 0)
                        GameManager.instance.FUN_30080(GameManager.instance.DAT_1078, ppcVar12);

                    ppcVar12.FUN_2EC7C();
                    GameManager.instance.FUN_30080(GameManager.instance.interObjs, ppcVar12);
                    return ppcVar12;
                }

                break;
            case 4:
                pcVar17 &= 0xffff;
                goto case 2;
            case 2:
            case 3:
                ppcVar12 = Utilities.FUN_31D30(pcVar10, levelManager.charsList[iVar9], sVar3, (pcVar17 & 4) << 1);
                ppcVar12.flags = pcVar17;
                ppcVar12.type = bVar1;
                ppcVar12.id = sVar2;
                ppcVar12.ai = (byte)local_30;
                ppcVar12.screen = local_88;
                ppcVar12.vr = local_78;
                oVar18 = ppcVar12.child2;
                ppcVar12.maxFullHealth = uVar6;
                ppcVar12.maxHalfHealth = uVar6;

                while(oVar18 != null)
                {
                    oVar18.maxFullHealth = uVar6;
                    oVar18.maxHalfHealth = uVar6;
                    oVar18 = oVar18.child;
                }

                bVar1 = (byte)GameManager.FUN_2AC5C();
                ppcVar12.DAT_19 = bVar1;
                ppcVar12.FUN_2C958();
                bVar19 = ppcVar12.FUN_3066C();

                if (bVar19)
                    return ppcVar12;

                return null;
            case 5:
                obj = new GameObject();
                ppcVar12 = obj.AddComponent(pcVar10) as VigObject;
                ppcVar12.DAT_1A = sVar3;
                ppcVar12.flags = pcVar17;
                ppcVar12.type = bVar1;
                ppcVar12.id = sVar2;
                ppcVar12.ai = (byte)local_30;
                ppcVar12.vData = levelManager.charsList[iVar9];
                ppcVar12.screen = local_88;
                ppcVar12.vr = local_78;
                ppcVar12.maxFullHealth = uVar6;
                ppcVar12.maxHalfHealth = uVar6;
                bVar1 = (byte)GameManager.FUN_2AC5C();
                ppcVar12.DAT_19 = bVar1;
                ppcVar12.ApplyTransformation();

                if (!pcVar10.IsSubclassOf(typeof(VigObject)))
                    iVar9 = 0;
                else
                    iVar9 = (int)ppcVar12.Execute(1, 1);

                if (-1 < iVar9)
                {
                    ppiVar16 = 0;

                    if (GameManager.instance.DAT_1078 != null)
                    {
                        do
                        {
                            if (sVar2 <= GameManager.instance.DAT_1078[ppiVar16].id)
                                break;

                            ppiVar16++;
                        } while (ppiVar16 < GameManager.instance.DAT_1078.Count);

                        if (ppiVar16 < GameManager.instance.DAT_1078.Count)
                        {
                            piVar15 = GameManager.instance.DAT_1078[ppiVar16];

                            if (sVar2 == piVar15.id)
                            {
                                oVar18 = piVar15.child;

                                while(oVar18 != null)
                                {
                                    piVar15 = piVar15.child;
                                    oVar18 = piVar15.child;
                                }

                                piVar15.child = ppcVar12;
                                ppcVar12.parent = piVar15;
                                return ppcVar12;
                            }
                        }
                    }

                    GameManager.instance.DAT_1078.Add(ppcVar12);
                    return ppcVar12;
                }

                break;
            case 6:
                obj = new GameObject();
                ppcVar12 = obj.AddComponent<VigObject>();
                ppcVar12.flags = pcVar17;
                ppcVar12.type = bVar1;
                ppcVar12.id = sVar2;
                ppcVar12.ai = (byte)local_30;
                ppcVar12.screen = local_88;
                ppcVar12.vr = local_78;
                bVar1 = (byte)GameManager.FUN_2AC5C();
                ppcVar12.DAT_19 = bVar1;
                ppcVar12.ApplyTransformation();
                GameManager.instance.FUN_30080(GameManager.instance.levelObjs, ppcVar12);
                goto case 1;
            case 1:
                return ppcVar12;
        }

        ppcVar12 = null;
        return ppcVar12;
    }
}
