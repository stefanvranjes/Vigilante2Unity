using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class IMP_OBJ
{
    public static bool LoadAsset(string assetPath)
    {
        using (BinaryReader reader = new BinaryReader(File.Open(assetPath, FileMode.Open)))
        {
            if (reader == null) return false;

            LevelManager levelManager = GameObject.FindObjectOfType<LevelManager>();

            levelManager.objData.Add(new OBJ(reader.ReadBytes((int)reader.BaseStream.Length)));
#if UNITY_EDITOR
            EditorUtility.SetDirty(levelManager);
#endif
        }

        return true;
    }

    //FUN_34F8 (LOAD.DLL)
    public static void LoadOBJ(byte[] obj)
    {
        MemoryStream stream = new MemoryStream(obj);

        using (BinaryReader reader = new BinaryReader(stream, Encoding.Default, true))
        {
            string identifier = new string(reader.ReadChars(4));
            VigObject oVar3 = null;

            if (identifier == "OBJ ")
            {
                if (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    do
                    {
                        if (reader.BaseStream.Length - reader.BaseStream.Position < 8)
                            break;

                        reader.BaseStream.Seek(reader.BaseStream.Position % 2, SeekOrigin.Current);

                        string header = new string(reader.ReadChars(4));
                        int chunkSize = reader.ReadInt32BE();

                        if (header == "HEAD")
                            oVar3 = LoadHEAD(reader, chunkSize);
                        else
                        {
                            if (header == "BSPI" && oVar3 != null)
                                LoadBSPI(oVar3, reader);
                            else if (header == "LGHT" && oVar3 != null)
                                LoadLGHT(oVar3, reader);
                            else if (header == "STRN" && oVar3 != null)
                                LoadSTRN(oVar3, reader);
                        }
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

        long begin = reader.BaseStream.Position;
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
        auStack112 = new string(reader.ReadChars((int)(size - (reader.BaseStream.Position - begin))));
        pcVar10 = Utilities.FUN_14E1C(0, auStack112);

        if (pcVar10 is null)
        {
            if (Utilities.levelTypes == null)
                pcVar10 = null;
            else
                pcVar10 = Utilities.FUN_14DAC(Utilities.levelTypes[0], auStack112); //improvised

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
        if ((pcVar17 & 4) != 0 && levelManager.xobfList[iVar9].animations.Length > 0)
        {
            iVar11 = (int)GameManager.FUN_2AC5C();
            MemoryStream stream = new MemoryStream(levelManager.xobfList[iVar9].animations);

            BinaryReader reader2 = new BinaryReader(stream, Encoding.Default, true);
            GameManager.instance.timer = (ushort)-(iVar11 * reader2.ReadInt32() >> 15);
        }

        if (6 < uVar7)
            return null;

        bVar1 = (byte)uVar7;
        ppcVar12 = null; //not in the original code

        switch (uVar7)
        {
            case 0:
                ppcVar12 = Utilities.FUN_31D30(pcVar10, levelManager.xobfList[iVar9], sVar3,
                                                (pcVar17 & 4) << 1);
                if (ppcVar12 == null)
                    return null;

                ppcVar12.gameObject.name = auStack112;
                Utilities.ParentChildren(ppcVar12, ppcVar12);

                ppcVar12.flags = pcVar17;
                ppcVar12.type = bVar1;
                ppcVar12.id = sVar2;
                ppcVar12.tags = (sbyte)local_30;
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
                    iVar9 = (int)ppcVar12.UpdateW(1, 0);

                if (-1 < iVar9)
                {
                    if ((ppcVar12.flags & 8) != 0 && ppcVar12.vShadow == null)
                        ppcVar12.FUN_4C98C();

                    if ((ppcVar12.flags & 4) != 0)
                        GameManager.instance.FUN_30080(GameManager.instance.DAT_10A8, ppcVar12);

                    if ((ppcVar12.flags & 0x80) != 0)
                        GameManager.instance.FUN_30080(GameManager.instance.DAT_1088, ppcVar12);

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
                ppcVar12 = Utilities.FUN_31D30(pcVar10, levelManager.xobfList[iVar9], sVar3, (pcVar17 & 4) << 1);

                if (ppcVar12 == null)
                    return null;

                ppcVar12.gameObject.name = auStack112;
                Utilities.ParentChildren(ppcVar12, ppcVar12);

                ppcVar12.flags = pcVar17;
                ppcVar12.type = bVar1;
                ppcVar12.id = sVar2;
                ppcVar12.tags = (sbyte)local_30;
                ppcVar12.screen = local_88;
                ppcVar12.vr = local_78;
                oVar18 = ppcVar12.child2;
                ppcVar12.maxFullHealth = uVar6;
                ppcVar12.maxHalfHealth = uVar6;

                while (oVar18 != null)
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
                obj = new GameObject(auStack112);
                ppcVar12 = obj.AddComponent(pcVar10) as VigObject;
                ppcVar12.DAT_1A = sVar3;
                ppcVar12.flags = pcVar17;
                ppcVar12.type = bVar1;
                ppcVar12.id = sVar2;
                ppcVar12.tags = (sbyte)local_30;
                ppcVar12.vData = levelManager.xobfList[iVar9];
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
                    iVar9 = (int)ppcVar12.UpdateW(1, 1);

                if (-1 < iVar9)
                {
                    ppiVar16 = 0;

                    if (GameManager.instance.DAT_1078.Count > 0)
                    {
                        do
                        {
                            if (sVar2 <= GameManager.instance.DAT_1078[ppiVar16].vObject.id)
                                break;

                            ppiVar16++;
                        } while (ppiVar16 < GameManager.instance.DAT_1078.Count);

                        if (ppiVar16 < GameManager.instance.DAT_1078.Count)
                        {
                            piVar15 = GameManager.instance.DAT_1078[ppiVar16].vObject;

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
                                ppcVar12.transform.parent = piVar15.transform;
                                return ppcVar12;
                            }
                        }
                    }

                    GameManager.instance.DAT_1078.Add(new VigTuple(ppcVar12, 0));
                    return ppcVar12;
                }

                break;
            case 6:
                obj = new GameObject(auStack112);
                ppcVar12 = obj.AddComponent<VigObject>();
                ppcVar12.flags = pcVar17;
                ppcVar12.type = bVar1;
                ppcVar12.id = sVar2;
                ppcVar12.tags = (sbyte)local_30;
                ppcVar12.screen = local_88;
                ppcVar12.vr = local_78;
                bVar1 = (byte)GameManager.FUN_2AC5C();
                ppcVar12.DAT_19 = bVar1;
                ppcVar12.ApplyTransformation();
                GameManager.instance.FUN_30080(LevelManager.instance.levelObjs, ppcVar12);
                goto case 1;
            case 1:
                return ppcVar12;
        }

        ppcVar12 = null;
        return ppcVar12;
    }

    private static void LoadBSPI(VigObject param1, BinaryReader reader)
    {
        VigTuple tVar1;
        uint uVar2;

        tVar1 = GameManager.instance.FUN_30134(GameManager.instance.interObjs, param1);
        uVar2 = reader.ReadUInt32BE();
        tVar1.flag = uVar2 | 0x80000000;
    }

    private static void LoadLGHT(VigObject param1, BinaryReader reader)
    {
        short sVar1;
        int iVar2;

        iVar2 = reader.ReadInt32BE();
        param1.physics1.X = iVar2;
        iVar2 = reader.ReadInt32BE();
        param1.physics1.Y = iVar2;
        iVar2 = reader.ReadInt32BE();
        param1.physics1.Z = iVar2;
        sVar1 = reader.ReadInt16BE();
        param1.physics1.M6 = sVar1;
        sVar1 = reader.ReadInt16BE();
        param1.physics1.M7 = sVar1;
        sVar1 = reader.ReadInt16BE();
        param1.physics2.M0 = sVar1;
    }

    private static void LoadSTRN(VigObject param1, BinaryReader reader)
    {
        ushort uVar1;
        ushort uVar2;
        VigObject oVar3;

        uVar1 = (ushort)reader.ReadInt32BE();

        if (reader.BaseStream.Length < 5)
        {
            param1.maxHalfHealth = uVar1;
            uVar2 = uVar1;
        }
        else
        {
            uVar2 = (ushort)reader.ReadInt32BE();
            param1.maxHalfHealth = uVar1;
        }

        param1.maxFullHealth = uVar2;
        oVar3 = param1.child2;

        while(oVar3 != null)
        {
            if (oVar3.maxHalfHealth == 0)
                oVar3.maxHalfHealth = uVar1;

            oVar3 = oVar3.child;
        }
    }
}
