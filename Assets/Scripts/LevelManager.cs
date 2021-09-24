using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class OBJ
{
    public byte[] buffer;

    public OBJ(byte[] b)
    {
        buffer = b;
    }
}

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    private static Vector3Int DAT_15F0 = new Vector3Int(0, 4096, 0); //LOAD.DLL+15F0h

    public string title;
    public string desc;
    public short DAT_63972;
    public short DAT_6397A;
    public short DAT_63982;
    public short DAT_6398A;
    public short DAT_63992;
    public short DAT_6399A;
    public Material defaultMaterial;
    public Camera defaultCamera;
    public VigTerrain terrain;
    public Matrix3x3 DAT_738; //gp+738h
    public ushort[] DAT_C18; //gp+C18h
    public Color32 DAT_D98; //gp+D98h
    public Color32 DAT_DA4; //gp+DA4h
    public Color32 DAT_DAC; //gp+DACh
    public short DAT_DBA; //gp+DBAh
    public Color32 DAT_DBC; //gp+DBCh
    public Material DAT_DD0; //gp+DD0h
    public Color32 DAT_DDC; //gp+DDCh
    public Color32 DAT_DE0; //gp+DE0h
    public Material[] DAT_DF8; //gp+DF8h
    public Color32 DAT_E04; //gp+E04h
    public Color32 DAT_E08; //gp+E08h
    public Material DAT_E48; //gp+E48h
    public Material DAT_E58; //gp+E58h
    public Vector3Int DAT_10F8; //gp+10F8h
    public XOBF_DB DAT_1178; //gp+1178h
    public int DAT_117C; //gp+117Ch
    public int DAT_1180; //gp+1180h
    public int DAT_1184; //gp+1184h
    public int DAT_118C; //gp+118Ch
    public byte[] bspData;
    public List<OBJ> objData;
    public int aimpSize; //gp+1018h
    public ushort[] aimpData; //gp+1020h
    public VigObject level; //gp+1024h
    public AudioSource music;
    public List<Junction> roadList = new List<Junction>(); //gp+1190h
    public List<XRTP_DB> xrtpList = new List<XRTP_DB>(); //gp+1194h
    public List<JUNC_DB> juncList = new List<JUNC_DB>(); //gp+1198h
    public List<XOBF_DB> xobfList = new List<XOBF_DB>(); //0xC6178
    public Navigation ainav; //0xC6278
    public List<VigTuple> levelObjs; //ffffa718 (LOAD.DLL)

    private int counter;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        ainav = new Navigation();
        music.mute = !GameManager.instance.playMusic;

        for (int i = 0; i < xobfList.Count; i++)
            if (xobfList[i] != null)
                xobfList[i].SetAtlas();
    }

    // Start is called before the first frame update
    // FUN_3F10 (LOAD.DLL)
    void Start()
    {
        VigMesh pbVar2;
        VigTuple ppiVar4;
        int iVar5;
        int iVar6;
        int iVar8;
        uint uVar14;
        int iVar21;
        BSP cVar22;
        Vector3Int local_310;
        List<VigTuple> ppiVar15;
        VigObject ppiVar18;

        PSXEffects psx = defaultCamera.GetComponent<PSXEffects>();

        switch (GameManager.instance.ditheringMethod)
        {
            case _DITHERING.None:
                psx.colorDepth = 16;
                psx.resolutionFactor = 1;
                psx.dithering = false;
                break;
            case _DITHERING.Standard:
                psx.colorDepth = 5;
                psx.resolutionFactor = 2;
                psx.dithering = true;
                psx.ditherType = 1;
                break;
            case _DITHERING.PSX:
                psx.colorDepth = 5;
                psx.resolutionFactor = 2;
                psx.dithering = true;
                psx.ditherType = 0;
                break;
        }

        GameManager.instance.levelManager = this;
        GameManager.instance.terrain = terrain;

        if ((GameManager.instance.DAT_40 & 0x40) != 0)
            DAT_C18[4] = 0;

        LoadData();

        GameManager.DAT_63970[0].y = DAT_63972;
        GameManager.DAT_63970[1].y = DAT_6397A;
        GameManager.DAT_63970[2].y = DAT_63982;
        GameManager.DAT_63970[3].y = DAT_6398A;
        GameManager.DAT_63970[4].y = DAT_63992;
        GameManager.DAT_63970[5].y = DAT_6399A;

        iVar5 = 0;

        do
        {
            pbVar2 = xobfList[18].FUN_2CB74_2(gameObject, GameManager.DAT_63A7C[iVar5]);
            GameManager.instance.DAT_1150[iVar5] = pbVar2;
            pbVar2.topology = MeshTopology.Lines;
            iVar5++;
        } while (iVar5 < 4);

        GameManager.instance.targetHUD = new Material(Shader.Find("PSXEffects/PS1Screen"));

        iVar6 = 1;

        if (_GAME_MODE.Demo < GameManager.instance.gameMode)
        {
            iVar6 = 4;

            if (GameManager.instance.gameMode < _GAME_MODE.Unk2)
                iVar6 = 2;
        }

        iVar8 = 0;

        if (iVar6 != 0)
        {
            do
            {
                iVar21 = GameManager.instance.vehicles[iVar8];

                if (-1 < iVar21)
                {
                    //Checking salvage points... (FUN_365E0)

                }

                iVar8++;
            } while (iVar8 < iVar6);
        }

        GameManager.instance.timer = 0;
        local_310 = new Vector3Int(
            DAT_10F8.x * 0x1800 >> 12,
            DAT_10F8.y * 0x1800 >> 12,
            DAT_10F8.z * 0x1800 >> 12);
        GameManager.instance.FUN_2DE84(0, local_310, DAT_DAC);
        GameManager.instance.FUN_2DE84(1, DAT_15F0, DAT_D98);
        local_310 = new Vector3Int(-DAT_10F8.x, DAT_10F8.y, -DAT_10F8.z);
        GameManager.instance.FUN_2DE84(2, local_310, DAT_DBC);
        iVar5 = GameManager.instance.interObjs.Count;
        Utilities.SetColorMatrix(GameManager.instance.DAT_FA8);
        Utilities.SetLightMatrix(GameManager.instance.DAT_F68);
        Utilities.SetBackColor(64, 64, 64);

        if (GameManager.instance.interObjs.Count > 0)
        {
            ppiVar15 = GameManager.instance.interObjs;

            do
            {
                ppiVar4 = ppiVar15[0];
                ppiVar18 = ppiVar4.vObject;
                ppiVar15.RemoveAt(0);
                FUN_3C8C(ppiVar18, GameManager.defaultTransform);
                //Move image? (probably the loading bar) ... 
                if (ppiVar4.flag == 0)
                    FUN_278C(GameManager.instance.bspTree, ppiVar4);
                else
                {
                    cVar22 = FUN_284C((int)ppiVar4.flag & 0x7fffffff);
                    cVar22.LDAT_04.Add(ppiVar4);
                }
            } while (GameManager.instance.interObjs.Count > 0);
        }

        //FUN_30A00
        //FUN_7E6C (loading junction in editor)

        if (GameManager.instance.gameMode == _GAME_MODE.Quest ||
            GameManager.instance.gameMode == _GAME_MODE.Quest2)
        {
            //...
        }
        else
            ; //...

        if (GameManager.instance.playerObjects[0] == null)
        {
            GameManager.instance.playerObjects[0] = GameManager.instance.FUN_3208C(-1);

            if (GameManager.instance.playerObjects[0] != null)
            {
                iVar5 = 1;
                GameManager.instance.playerObjects[0].flags =
                    GameManager.instance.playerObjects[0].flags & 0x1ffffff | 0x80000000;

                do
                {
                    do
                    {
                        iVar6 = (int)GameManager.FUN_2AC5C();
                        uVar14 = 0x1000000U << ((iVar6 * 7 >> 15) + 1 & 31);
                    } while ((GameManager.instance.playerObjects[0].flags & uVar14) != 0);

                    GameManager.instance.playerObjects[0].flags |= uVar14;
                    iVar5++;
                } while (iVar5 < 3);

                GameManager.instance.playerObjects[0].FUN_3066C();
            }
        }

        //second player...

        FUN_3D94(GameManager.instance.playerObjects[0]);
        //second player...
        GameManager.instance.DAT_CC4 = 0;
        GameManager.instance.inDebug = false;

        level.UpdateW(1, 0);

        if ((level.flags & 0x80) != 0)
            GameManager.instance.FUN_30080(GameManager.instance.DAT_1088, level);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadData()
    {
        IMP_BSP.LoadData(bspData);

        for (int i = 0; i < objData.Count; i++)
            IMP_OBJ.LoadOBJ(objData[i].buffer);
    }

    //FUN_9C10 (LOAD.DLL)
    public int FUN_9C10(uint param1, int param2, uint param3, int param4)
    {
        int iVar1;

        iVar1 = 0;

        if (param4 < param2)
        {
            iVar1 = 2;

            if (param2 <= param4)
            {
                if (param1 < param3)
                    iVar1 = 0;
                else
                {
                    iVar1 = 2;

                    if (param1 <= param3)
                        iVar1 = 1;
                }
            }
        }

        return iVar1;
    }

    //FUN_7E6C (LOAD.DLL)
    public void FUN_7E6C()
    {
        RSEG_DB dbVar1;
        int iVar2;
        JUNC_DB dbVar3;
        int iVar4;
        JUNC_DB dbVar5;

        iVar4 = 0;
        counter = 0;

        if (0 < DAT_1184)
        {
            for (int i = 0; i < DAT_1184; i++)
            {
                dbVar5 = juncList[i];

                if (dbVar5.DAT_11 != 0)
                {
                    for (int j = 0; j < dbVar5.DAT_11; j++)
                    {
                        dbVar1 = dbVar5.DAT_1C[j];

                        if (dbVar5 == dbVar1.DAT_00[0] && (dbVar1.DAT_0C & 0x10) == 0 &&
                            xrtpList[dbVar1.DAT_0A].timFarList.Count > 0)
                            FUN_719C(dbVar1);
                    }
                }
            }
        }
        
        if (0 < DAT_1180)
        {
            for (int i = 0; i < DAT_1180; i++)
                if (xrtpList[i].timFarList != null)
                    FUN_50F0(xrtpList[i]);
        }
    }

    public uint FUN_35778(int param1, int param2)
    {
        ushort uVar1;
        uint uVar2;
        uint uVar3;
        int iVar4;

        uVar3 = (uint)(param1 << 5);
        param2 <<= 5;
        iVar4 = 0;

        while (true)
        {
            uVar2 = uVar3 >> 31;

            if (param2 < 0)
                uVar2 |= 2;

            uVar1 = aimpData[iVar4 + uVar2 + 1];
            uVar2 = uVar1;

            if (uVar2 == 0 || (uVar1 & 0x8000) != 0) break;

            iVar4 += (int)(uVar2 * 5);
            uVar3 <<= 1;
            param2 <<= 1;
        }

        return uVar2;
    }

    public void FUN_357D4(short[] param1, uint param2, int param3, int param4, int param5, int param6, ushort[] param7)
    {
        ushort uVar1;
        ushort uVar2;

        param6 >>= 1;
        uVar2 = (ushort)param2;

        if (param1[1] < param5 + param6)
        {
            if (param1[0] < param4 + param6)
            {
                uVar1 = param7[param3 + 1];

                if (uVar1 == 0 || (uVar1 & 0x8000) != 0)
                    param7[param3 + 1] = uVar2;
                else
                    FUN_357D4(param1, param2, param3 + uVar1 * 5, param4, param5, param6, param7);
            }

            if (param4 + param6 < param1[0] + param1[2])
            {
                uVar1 = param7[param3 + 2];

                if (uVar1 == 0 || (uVar1 & 0x8000) != 0)
                    param7[param3 + 2] = uVar2;
                else
                    FUN_357D4(param1, param2, param3 + uVar1 * 5, param4 + param6, param5, param6, param7);
            }
        }

        if (param5 + param6 < param1[1] + param1[3])
        {
            if (param1[0] < param4 + param6)
            {
                uVar1 = param7[param3 + 3];

                if (uVar1 == 0 || (uVar1 & 0x8000) != 0)
                    param7[param3 + 3] = uVar2;
                else
                    FUN_357D4(param1, param2, param3 + uVar1 * 5, param4, param5 + param6, param6, param7);
            }

            if (param4 + param6 < param1[0] + param1[2])
            {
                uVar1 = param7[param3 + 4];

                if (uVar1 == 0 || (uVar1 & 0x8000) != 0)
                    param7[param3 + 4] = uVar2;
                else
                    FUN_357D4(param1, param2, param3 + uVar1 * 5, param4 + param6, param5 + param6, param6, param7);
            }
        }
    }

    public void FUN_309C8(VigObject param1, int param2)
    {
        level.UpdateW(param1, 18, param2);
        GameManager.instance.FUN_309A0(param1);
    }

    public void FUN_359FC(int param1, int param2, uint param3)
    {
        if (param1 < 0)
            param1 += 0xffff;

        if (param2 < 0)
            param2 += 0xffff;

        FUN_357D4(new short[] { (short)(param1 >> 16), (short)(param2 >> 16), 1, 1 },
                  param3, 0, 0, 0, 0x800, aimpData);
    }

    public void FUN_38EF4(int param1, int param2)
    {
        Particle1 pVar1;
        Vector3Int local_8 = new Vector3Int(param1, GameManager.instance.DAT_DB0, param2);

        pVar1 = FUN_4DE54(local_8, 147);
        pVar1.flags &= 0xffffffef;
    }

    public void FUN_38F38(int param1, int param2)
    {
        Particle1 pVar1;
        Vector3Int local_8 = new Vector3Int(param1, GameManager.instance.DAT_DA0, param2);

        pVar1 = FUN_4DE54(local_8, 146);
        pVar1.flags &= 0xffffffef;
    }

    public Fire1 FUN_399FC(Vehicle param1, XOBF_DB param2, short param3)
    {
        Fire1 ppcVar1;

        if ((param1.DAT_F6 & 8) == 0)
        {
            ppcVar1 = null;

            if (param1.shield == 0)
            {
                GameObject obj = new GameObject();
                ppcVar1 = obj.AddComponent<Fire1>();
                ppcVar1.DAT_58 = 0x10000;
                ppcVar1.physics1.M1 = 4;
                ppcVar1.DAT_98 = param2;
                ppcVar1.physics2.M3 = param3;
                ppcVar1.maxHalfHealth = 2;
                ppcVar1.flags |= 0x20;
                ppcVar1.vTransform = GameManager.FUN_2A39C();
                ppcVar1.physics1.Y = 0x200;
                ppcVar1.physics1.Z = -0x600;
                ppcVar1.physics1.W = 0;
                Utilities.FUN_2CC9C(param1, ppcVar1);
                ppcVar1.transform.parent = param1.transform;
                ppcVar1.FUN_30B78();
                GameManager.instance.FUN_30CB0(ppcVar1, 600);
                param1.DAT_F6 |= 8;
            }
        }
        else
            ppcVar1 = null;

        return ppcVar1;
    }

    public bool FUN_39AF8(Vehicle param1)
    {
        Fire1 fVar1;
        int iVar1;

        fVar1 = FUN_399FC(param1, xobfList[19], 22);

        if (fVar1 != null)
        {
            iVar1 = GameManager.instance.FUN_1DD9C();
            GameManager.instance.FUN_1E628(iVar1, GameManager.instance.DAT_C2C, 68, param1.vTransform.position);
        }

        return fVar1 != null;
    }

    public VigObject FUN_42408(VigObject param1, VigObject param2, ushort param3, Type param4, VigObject param5)
    {
        ushort uVar1;
        ConfigContainer ccVar2;
        VigObject oVar3;
        VigTransform puVar4;

        if (param2.vData == null)
            ccVar2 = null;
        else
            ccVar2 = param2.FUN_2C5F4(0x8000);

        if (param3 << 16 < 0)
        {
            GameObject obj = new GameObject();
            oVar3 = obj.AddComponent(param4) as VigObject;
        }
        else
            oVar3 = param2.vData.ini.FUN_2C17C(param3, param4, 8);
        
        oVar3.DAT_80 = param1;
        oVar3.flags = 0x20000000;
        uVar1 = (ushort)param1.id;
        oVar3.type = 8;
        oVar3.id = (short)uVar1;

        if (ccVar2 == null)
        {
            puVar4 = GameManager.instance.FUN_2CDF4(param2);
            oVar3.vTransform = puVar4;
        }
        else
            oVar3.vTransform = GameManager.instance.FUN_2CEAC(param2, ccVar2);

        oVar3.screen = oVar3.vTransform.position;

        if (param5 != null)
        {
            Utilities.FUN_2CA94(param2, ccVar2, param5);
            param5.transform.parent = param2.transform;
        }

        return oVar3;
    }

    public VigObject FUN_42560(VigObject param1, VigObject param2, VigObject param3, VigObject param4)
    {
        short sVar1;

        param3.DAT_80 = param1;
        param3.flags = 0x20000000;
        sVar1 = param1.id;
        param3.type = 8;
        param3.id = sVar1;
        param3.vTransform = GameManager.instance.FUN_2CDF4(param2);
        param3.screen = param3.vTransform.position;

        if (param4 != null)
        {
            param4.vTransform = GameManager.FUN_2A39C();
            Utilities.FUN_2CC48(param2, param4);
            param4.transform.parent = param2.transform;
        }

        return param3;
    }

    public Pickup FUN_4AA24(ushort param1, Vector3Int param2, Vector3Int param3)
    {
        Pickup ppcVar1;

        ppcVar1 = DAT_1178.ini.FUN_2C17C(param1, typeof(Pickup), 0) as Pickup;
        ppcVar1.state = _PICKUP_TYPE.Type2;
        ppcVar1.screen = param2;
        ppcVar1.physics1.Z = param3.x;
        ppcVar1.physics1.W = param3.y;
        ppcVar1.physics2.X = param3.z;
        ppcVar1.DAT_87 = 2;
        ppcVar1.FUN_3066C();
        return ppcVar1;
    }

    public Pickup FUN_4AD24(short param1)
    {
        Pickup ppcVar1;

        ppcVar1 = Utilities.FUN_31D30(typeof(Pickup), DAT_1178, param1, 0) as Pickup;
        ppcVar1.FUN_2C7D0();
        return ppcVar1;
    }

    public void FUN_4DF20(Vector3Int param1, ushort param2, short param3)
    {
        Particle1 oVar1;
        Vector3Int local_8;

        oVar1 = FUN_4DE54(param1, param2);
        local_8 = new Vector3Int(param3, param3, param3);
        oVar1.vTransform.rotation = Utilities.FUN_245AC(oVar1.vTransform.rotation, local_8);
        oVar1.vTransform.padding = param3;
    }

    public Particle1 FUN_4DE54(Vector3Int param1, ushort param2)
    {
        Particle1 ppcVar1;
        VigObject pcVar2;
        VigTuple pcVar3;

        ppcVar1 = xobfList[19].ini.FUN_2C17C(param2, typeof(Particle1), 8) as Particle1;
        Utilities.ParentChildren(ppcVar1, ppcVar1);
        ppcVar1.type = 7;
        ppcVar1.flags = 0x34;
        ppcVar1.screen = param1;
        pcVar2 = ppcVar1.child2;

        while(pcVar2 != null)
        {
            pcVar2.flags = 0x10;
            pcVar2 = pcVar2.child;
        }

        ppcVar1.ApplyTransformation();
        ppcVar1.FUN_2D1DC();
        pcVar3 = GameManager.instance.FUN_30080(GameManager.instance.interObjs, ppcVar1);
        ppcVar1.TDAT_74 = pcVar3;
        pcVar3 = GameManager.instance.FUN_30080(GameManager.instance.DAT_10A8, ppcVar1);
        ppcVar1.TDAT_78 = pcVar3;
        return ppcVar1;
    }

    public void FUN_4D16C(XOBF_DB param1, ushort param2, VigTransform param3)
    {
        ushort uVar1;
        Particle5 ppcVar2;
        uint uVar3;
        int iVar4;
        VigCollider uVar4;
        uint uVar8;
        ConfigContainer puVar9;

        puVar9 = param1.ini.configContainers[param2];
        uVar1 = puVar9.flag;
        uVar8 = uVar1;
        uVar3 = (uint)((int)uVar8 >> 12);

        if (uVar3 == 9)
        {
            iVar4 = GameManager.instance.FUN_1DD9C();
            GameManager.instance.FUN_1E628(iVar4, GameManager.instance.DAT_C2C,
                GameManager.DAT_BC0[GameManager.DAT_640C8[(uVar1 >> 6 & 0x3c) / 4] + (uVar8 & 0xff)], param3.position);
        }
        else
        {
            if (uVar3 < 10)
            {
                if (uVar3 == 8)
                {
                    if (uVar8 - 0x8400 < 61)
                    {
                        if (GameManager.DAT_63FE4[uVar8-0x8400] != -1)
                        {
                            if (puVar9.colliderID < 0)
                                uVar4 = null;
                            else
                            {
                                uVar4 = param1.cbbList[puVar9.colliderID];
                                uVar4.GetReader();
                            }

                            FUN_4DF74(param3.position, GameManager.DAT_63FE4[uVar8 - 0x8400], uVar4);
                        }
                    }
                    else
                    {
                        uVar3 = uVar8 - 0x8500;

                        if (uVar3 < 0x15)
                        {
                            if (uVar8 < 0x850a)
                                FUN_4E56C(param3, GameManager.DAT_6405C[uVar3]);
                            else
                                FUN_4E8C8(param3.position, GameManager.DAT_6405C[uVar3]);
                        }
                        else
                        {
                            if (uVar8 - 0x8700 < 21)
                            {
                                FUN_4E128(param3.position, (ushort)GameManager.DAT_64084[uVar8 - 0x8700], 100);
                            }
                            else
                            {
                                if (uVar8 - 0x8800 < 8)
                                    ; //FUN_4E414
                            }
                        }
                    }
                }
            }
            else
            {
                if (uVar3 == 14)
                {
                    puVar9.flag = (ushort)(uVar1 & 0xfff);
                    ppcVar2 = param1.ini.FUN_2C17C((ushort)(param2 & 0xffff), typeof(Particle5), 0) as Particle5;
                    puVar9.flag |= 0xe000;
                    ppcVar2.type = 7;
                    ppcVar2.flags |= 0x1000080;
                    ppcVar2.vTransform = param3;
                    ppcVar2.vr.y = 12;
                    ppcVar2.FUN_2D1DC();
                    ppcVar2.FUN_305FC();
                }
            }
        }

        if (puVar9.next != 0xffff)
            param1.FUN_4D498(puVar9.next, param3, 0);
    }

    public void FUN_4DF74(Vector3Int param1, int param2, VigCollider param3)
    {
        Particle1 pVar1;
        VigCollider psVar2;
        int iVar3;
        int iVar4;
        Vector3Int local_8;

        pVar1 = FUN_4DE54(param1, (ushort)param2);

        if (param3 != null && param3.reader.ReadUInt16(0) == 1)
        {
            psVar2 = pVar1.vCollider;

            if (psVar2 != null && psVar2.reader.ReadUInt16(0) == 1)
            {
                iVar4 = ((param3.reader.ReadInt32(16) - param3.reader.ReadInt32(4)) * 0x1000) /
                        (psVar2.reader.ReadInt32(16) - psVar2.reader.ReadInt32(4));
                iVar3 = 0x7fff;

                if (iVar4 < 0x7fff)
                    iVar3 = iVar4;

                local_8 = new Vector3Int(iVar3, iVar3, iVar3);
                pVar1.vTransform.rotation = Utilities.FUN_245AC(pVar1.vTransform.rotation, local_8);
                pVar1.vTransform.padding = (short)iVar3;
            }
        }
    }

    public Particle2 FUN_4E128(Vector3Int param1, ushort param2, int param3)
    {
        Particle2 ppcVar1;
        uint pcVar2;
        BufferedBinaryReader brVar3;

        ppcVar1 = xobfList[19].ini.FUN_2C17C(param2, typeof(Particle2), 8) as Particle2;
        Utilities.ParentChildren(ppcVar1, ppcVar1);
        ppcVar1.type = 8;

        if (param3 == 0)
            pcVar2 = ppcVar1.flags | 0x324;
        else
            pcVar2 = ppcVar1.flags | 0x304;

        ppcVar1.flags = pcVar2;
        ppcVar1.screen = param1;
        ppcVar1.maxHalfHealth = (ushort)(param3 / 12);
        ppcVar1.FUN_3066C();
        brVar3 = ppcVar1.vCollider.reader;
        ppcVar1.DAT_58 = brVar3.ReadInt32(16);
        return ppcVar1;
    }

    public Particle3 FUN_4E56C(VigTransform param1, int param2)
    {
        uint uVar1;
        ushort uVar2;
        Particle3 ppcVar3;
        int iVar4;
        VigObject oVar5;
        VigConfig cVar5;
        int iVar6;
        Vector3Int v0;

        GameObject obj = new GameObject();
        ppcVar3 = obj.AddComponent<Particle3>();
        cVar5 = xobfList[19].ini;
        ppcVar3.flags |= 0xA0;
        ppcVar3.screen = param1.position;
        ppcVar3.DAT_58 = 0x10000;
        ppcVar3.ApplyTransformation();
        Utilities.SetRotMatrix(param1.rotation);
        uVar2 = cVar5.configContainers[param2].next;
        uVar1 = uVar2;

        while(uVar1 != 0xffff)
        {
            oVar5 = xobfList[19].ini.FUN_2C17C((ushort)uVar1, typeof(VigObject), 0);
            v0 = Utilities.FUN_23F7C(oVar5.screen);
            oVar5.physics1.Z = v0.x;
            oVar5.physics1.W = v0.y;
            oVar5.physics2.X = v0.z;
            iVar4 = oVar5.physics1.Z;

            if (iVar4 < 0)
                iVar4 += 3;

            iVar6 = oVar5.physics1.W;
            oVar5.physics1.Z = iVar4 >> 2;

            if (iVar6 < 0)
                iVar6 += 3;

            iVar4 = oVar5.physics2.X;
            oVar5.physics1.W = iVar6 >> 2;

            if (iVar4 < 0)
                iVar4 += 3;

            oVar5.physics2.X = iVar4 >> 2;
            uVar2 = (ushort)GameManager.FUN_2AC5C();
            oVar5.physics1.M0 = (short)(uVar2 & 31);
            uVar2 = (ushort)GameManager.FUN_2AC5C();
            oVar5.physics1.M1 = (short)(uVar2 & 31);
            uVar2 = (ushort)GameManager.FUN_2AC5C();
            oVar5.physics1.M2 = (short)(uVar2 & 31);
            oVar5.screen = new Vector3Int(0, 0, 0);
            oVar5.ApplyTransformation();
            iVar4 = (int)GameManager.FUN_2AC5C();
            oVar5.physics2.Y = (iVar4 * 30 >> 15) + 30;
            Utilities.FUN_2CC48(ppcVar3, oVar5);
            oVar5.transform.parent = ppcVar3.transform;
            uVar2 = xobfList[19].ini.configContainers[(int)uVar1].previous;
            uVar1 = uVar2;
        }

        ppcVar3.FUN_3066C();
        return ppcVar3;
    }

    public Particle4 FUN_4E8C8(Vector3Int param1, short param2)
    {
        Particle4 ppcVar1;

        GameObject obj = new GameObject();
        ppcVar1 = obj.AddComponent<Particle4>();
        ppcVar1.flags = 0xA4;
        ppcVar1.DAT_1A = param2;
        ppcVar1.screen = param1;
        ppcVar1.FUN_3066C();
        return ppcVar1;
    }

    public Particle9 FUN_4EAE8(Vector3Int param1, Vector3Int param2, short param3)
    {
        Particle9 ppcVar1;

        GameObject obj = new GameObject();
        ppcVar1 = obj.AddComponent<Particle9>();
        ppcVar1.type = 8;
        ppcVar1.flags = 0xA4;
        ppcVar1.DAT_1A = param3;
        ppcVar1.screen = param1;
        ppcVar1.maxHalfHealth = 0x1000;
        ppcVar1.FUN_3066C();
        ppcVar1.vTransform.rotation = Utilities.FUN_2A5EC(param2);
        return ppcVar1;
    }

    private void FUN_278C(BSP param1, VigTuple param2)
    {
        int iVar1;
        BSP ppiVar2;
        BSP cVar3;

        iVar1 = param1.DAT_00;

        if (iVar1 == 1)
        {
            if (param2.vObject.screen.x <= param1.DAT_04)
            {
                cVar3 = param1.DAT_08;
                goto LAB_2834;
            }
        }
        else
        {
            if (iVar1 == 0)
            {
                param1.LDAT_04.Add(param2);
                return;
            }

            if (iVar1 != 2)
                return;

            if (param2.vObject.screen.z <= param1.DAT_04)
            {
                cVar3 = param1.DAT_08;
                goto LAB_2834;
            }
        }

        cVar3 = param1.DAT_0C;
        LAB_2834:
        FUN_278C(cVar3, param2);
    }

    private BSP FUN_284C(int param1)
    {
        BSP piVar1;

        piVar1 = GameManager.instance.bspTree;

        if (0 < param1)
        {
            param1 <<= 1;

            while (0 < param1)
                param1 <<= 1;
        }

        for (param1 <<= 1; piVar1.DAT_00 != 0; param1 <<= 1)
        {
            if (param1 < 0)
                piVar1 = piVar1.DAT_0C;
            else
                piVar1 = piVar1.DAT_08;
        }

        return piVar1;
    }

    private uint FUN_3630(VigObject param1, Vector3Int param2, Vector3Int param3)
    {
        ushort uVar1;
        int iVar2;
        uint uVar3;
        int iVar4;
        int iVar5;
        uint uVar6;
        uint uVar7;
        Vector3Int local_30;
        Vector3Int local_28;
        Vector3Int auStack24;

        local_30 = new Vector3Int(
            param1.vTransform.rotation.V01,
            param1.vTransform.rotation.V11,
            param1.vTransform.rotation.V21);
        iVar2 = Utilities.FUN_29C5C(param3, local_30);
        uVar3 = 0;

        if (iVar2 < 0)
        {
            uVar7 = (ushort)GameManager.DAT_65C90[(param1.physics1.M7 & 0xfff) * 2 + 1];
            uVar1 = (ushort)GameManager.DAT_65C90[(param1.physics1.M6 & 0xfff) * 2 + 1];
            local_28 = new Vector3Int(
                param2.x - param1.screen.x,
                param2.y - param1.screen.y,
                param2.z - param1.screen.z);
            uVar3 = (uint)Utilities.FUN_29E84(local_28);
            Utilities.FUN_29FC8(local_28, out auStack24);
            iVar4 = Utilities.FUN_29C5C(auStack24, local_30);

            if (iVar4 < 0)
                iVar4 += 4095;

            uVar6 = (uint)param1.physics1.Z;

            if (uVar3 < uVar6)
            {
                iVar5 = -iVar2;

                if ((int)uVar7 < iVar4 >> 12)
                {
                    if (0 < iVar2)
                        iVar5 += 4095;

                    uVar3 = (uVar6 - uVar3) / (uVar6 - (uint)param1.physics1.Y >> 12);

                    if (4096 < (int)uVar3)
                        uVar3 = 4096;

                    iVar2 = (iVar5 >> 12) * (int)uVar3;

                    if (iVar2 < 0)
                        iVar2 += 4095;

                    iVar4 = (int)((uVar7 - (iVar4 >> 12)) * 4096) / (int)(uVar7 - uVar1);

                    if (4096 < iVar4)
                        iVar4 = 4096;

                    iVar4 = (iVar2 >> 12) * iVar4;

                    if (iVar4 < 0)
                        iVar4 += 4095;

                    iVar2 = (iVar4 >> 12) * (ushort)param1.physics2.M0;

                    if (iVar2 < 0)
                        iVar2 += 4095;

                    uVar3 = (uint)(iVar2 >> 12 & 0xffff);
                }
                else
                    uVar3 = 0;
            }
            else
                uVar3 = 0;
        }

        return uVar3;
    }

    private void FUN_3828(MemoryStream param1, MemoryStream param2, MemoryStream param3, MemoryStream param4)
    {
        int iVar1;
        int iVar2;
        int iVar3;
        uint uVar4;
        VigObject oVar5;
        uint uVar6;
        uint uVar7;
        uint uVar8;
        Vector3Int auStack48;
        Vector3Int auStack32;
        Color32 local_18;

        using (BinaryReader reader = new BinaryReader(param3, Encoding.Default, true))
            auStack48 = Utilities.FUN_23F58(new Vector3Int
                (reader.ReadInt16(0), reader.ReadInt16(2), reader.ReadInt16(4)));

        using (BinaryReader reader = new BinaryReader(param4, Encoding.Default, true))
            auStack32 = Utilities.FUN_23EA0(new Vector3Int
                (reader.ReadInt16(0), reader.ReadInt16(2), reader.ReadInt16(4)));

        using (BinaryReader reader = new BinaryReader(param2, Encoding.Default, true))
            local_18 = Utilities.NormalColorCol(auStack32, new Color32
                (reader.ReadByte(0), reader.ReadByte(1), reader.ReadByte(2), reader.ReadByte(3)));

        uVar6 = local_18.r;
        uVar7 = local_18.g;
        uVar8 = local_18.b;

        if (levelObjs != null)
        {
            for (int i = 0; i < levelObjs.Count; i++)
            {
                oVar5 = levelObjs[i].vObject;
                uVar4 = FUN_3630(oVar5, auStack48, auStack32);
                uVar4 &= 0xffff;

                if (uVar4 != 0)
                {
                    iVar1 = (int)uVar4 * (byte)oVar5.physics1.M0;

                    if (iVar1 < 0)
                        iVar1 += 4095;

                    uVar6 += (uint)(iVar1 >> 12);
                    iVar2 = (int)uVar4 * (byte)(oVar5.physics1.M0 >> 8);

                    if (iVar2 < 0)
                        iVar2 += 4095;

                    uVar7 += (uint)(iVar2 >> 12);
                    iVar3 = (int)uVar4 * (byte)oVar5.physics1.M1;

                    if (iVar3 < 0)
                        iVar3 += 4095;

                    uVar8 += (uint)(iVar3 >> 12);
                }
            }
        }

        using (BinaryWriter writer = new BinaryWriter(param1, Encoding.Default, true))
        {
            uVar4 = 255;

            if ((int)uVar6 < 255)
                uVar4 = uVar6;

            writer.Write((byte)uVar4);
            uVar6 = 255;

            if ((int)uVar7 < 255)
                uVar6 = uVar7;

            writer.Write((byte)uVar6);
            uVar6 = 255;

            if ((int)uVar8 < 255)
                uVar6 = uVar8;

            writer.Write((byte)uVar6);
        }
    }

    private void FUN_3C8C(VigObject param1, VigTransform param2)
    {
        VigMesh mVar1;
        DELEGATE_79A0 dVar2;
        VigTransform auStack32;

        do
        {
            auStack32 = Utilities.CompMatrixLV(param2, param1.vTransform);
            Utilities.FUN_246BC(auStack32);
            mVar1 = param1.vMesh;
            dVar2 = new DELEGATE_79A0(FUN_3828);

            if (mVar1 != null)
            {
                if ((mVar1.DAT_00 & 1) != 0)
                {
                    mVar1.FUN_39A8(dVar2);
                    mVar1.Initialize();
                    mVar1.DAT_00 &= 254;
                    mVar1.DAT_00 |= 4;
                }
            }

            mVar1 = param1.vLOD;

            if (mVar1 != null)
            {
                if ((mVar1.DAT_00 & 1) != 0)
                {
                    mVar1.FUN_39A8(FUN_3828);
                    mVar1.Initialize();
                    mVar1.DAT_00 &= 254;
                    mVar1.DAT_00 |= 4;
                }
            }

            if (param1.child2 != null)
                FUN_3C8C(param1.child2, auStack32);

            param1 = param1.child;
        } while (param1 != null);
    }

    //FUN_3D94 (LOAD.DLL)
    private void FUN_3D94(Vehicle param1)
    {
        VigCamera cVar1;
        int iVar2;
        VigObject oVar3;
        ConfigContainer ccVar4;

        param1.FUN_3CCD4(true);
        cVar1 = GameManager.instance.FUN_4B914(param1, 256, defaultCamera);
        terrain.vCamera = cVar1;
        param1.vCamera = cVar1;
        GameManager.instance.cameraObjects[~param1.id] = cVar1;

        if (param1.vehicle == _VEHICLE.Livingston)
            param1.vCamera.DAT_9C += 0x19000;

        param1.view = _CAR_VIEW.Far;
        param1.vCamera.FUN_30B78();
        param1.vCamera.FUN_4BC0C();
        param1.FUN_38408();
        param1.FUN_3C9C4(~param1.id);
        //...
        GameObject obj = new GameObject();
        oVar3 = obj.AddComponent<VigObject>();
        obj.transform.parent = param1.transform;
        param1.closeViewer = oVar3;
        ccVar4 = param1.FUN_2C5F4(0x8100);

        if (ccVar4 == null)
        {
            param1.closeViewer.screen.y = -21845;
            param1.closeViewer.ApplyTransformation();
            Utilities.FUN_2CC48(param1, param1.closeViewer);
        }
        else
            Utilities.FUN_2CA94(param1, ccVar4, param1.closeViewer);

        if ((GameManager.instance.DAT_40 & 0x8000) != 0)
            param1.DAT_A6 = 0x5000;

        if ((GameManager.instance.DAT_40 & 0x10000) != 0)
            param1.lightness = 0;
    }

    private void FUN_719C(RSEG_DB param1)
    {
        byte pbVar1;
        ushort uVar2;
        int uVar3;
        int iVar5;
        long lVar5;
        JUNC_DB piVar7;
        ConfigContainer cVar10;
        int iVar10;
        int iVar12;
        int iVar13;
        VigConfig iVar14;
        Vector2Int uVar15;
        int[,] local_b0;
        int[,] local_80;
        int local_3c;
        int local_30;

        VigTerrain terrain = GameObject.FindObjectOfType<VigTerrain>();

        local_80 = new int[,]
        {
            {
                param1.DAT_00[0].DAT_00.x,
                param1.DAT_00[0].DAT_00.y,
                param1.DAT_00[0].DAT_00.z,
                param1.DAT_00[0].DAT_00.x + param1.DAT_10[0]
            },
            {
                0,
                param1.DAT_00[0].DAT_00.z + param1.DAT_14[0],
                param1.DAT_00[1].DAT_00.x + param1.DAT_10[1],
                0
            },
            {
                param1.DAT_00[1].DAT_00.z + param1.DAT_14[1],
                param1.DAT_00[1].DAT_00.x,
                param1.DAT_00[1].DAT_00.y,
                param1.DAT_00[1].DAT_00.z
            }
        };
        local_b0 = new int[3, 4];
        Array.Copy(local_80, local_b0, local_80.Length);
        local_30 = 0;

        for (int i = 0; i < 2; i++)
        {
            piVar7 = param1.DAT_00[i];

            if (piVar7.DAT_18 == null)
            {
                if ((piVar7.DAT_10 & 1) == 0)
                {
                    iVar5 = 0;

                    if ((param1.DAT_0C & 2 << (i & 31)) == 0)
                    {
                        pbVar1 = piVar7.DAT_11;
                        iVar12 = 0;

                        if (pbVar1 != 0)
                        {
                            iVar13 = iVar12;

                            do
                            {
                                iVar12 = iVar13;

                                if ((short)piVar7.DAT_1C[iVar5].DAT_08 < (short)param1.DAT_08)
                                {
                                    iVar12 = xrtpList[piVar7.DAT_1C[iVar5].DAT_0A].DAT_24 / 2;

                                    if (iVar12 < iVar13)
                                        iVar12 = iVar13;
                                }

                                iVar5++;
                                iVar13 = iVar12;
                            } while (iVar5 < pbVar1);
                        }

                        if (iVar12 != 0)
                            FUN_6604(local_b0, i, iVar12);
                    }
                }
            }
            else
            {
                iVar14 = piVar7.DAT_0C.ini;
                local_80[1, 1] = 0;
                local_80[1, 0] = param1.DAT_10[i];
                local_80[1, 3] = 0;
                local_80[1, 2] = param1.DAT_14[i];
                local_80[0, 0] = param1.DAT_10[i];
                local_80[0, 1] = 0;
                local_80[0, 2] = param1.DAT_14[i];
                local_80[0, 3] = 0;
                local_3c = 0;
                uVar2 = (ushort)iVar14.configContainers[piVar7.DAT_14].next;
                iVar5 = (piVar7.DAT_16 & 0xfff) * 2;
                iVar13 = GameManager.DAT_65C90[iVar5 + 1];
                iVar12 = GameManager.DAT_65C90[iVar5];
                iVar5 = local_30;

                while(uVar2 != 0xffff)
                {
                    cVar10 = iVar14.configContainers[uVar2];
                    local_80 = new int[,]
                    {
                        { local_80[0, 0], local_80[0, 1], local_80[0, 2], local_80[0, 3] },
                        { local_80[1, 0], local_80[1, 1], local_80[1, 2], local_80[1, 3] },
                        { local_80[2, 0], local_80[2, 1], local_80[2, 2], local_80[2, 3] },
                        { 0, 0, 0, 0 }
                    };
                    iVar5 = iVar13 * cVar10.v3_1.x + iVar12 * cVar10.v3_1.z;

                    if (iVar5 < 0)
                        iVar5 += 4095;

                    local_80[2, 0] = iVar5 >> 12;
                    iVar5 = -iVar12 * cVar10.v3_1.x + iVar13 * cVar10.v3_1.z;

                    if (iVar5 < 0)
                        iVar5 += 4095;

                    local_80[2, 2] = iVar5 >> 12;
                    local_80[2, 1] = local_80[3, 1];
                    local_80[2, 3] = local_80[3, 3];
                    local_80[3, 0] = local_80[2, 0];
                    local_80[3, 2] = local_80[2, 2];
                    uVar15 = Utilities.FUN_2ACD0(new Vector3Int(local_80[2, 0], local_80[2, 1], local_80[2, 2]),
                                                 new Vector3Int(local_80[0, 0], local_80[0, 1], local_80[0, 2]));
                    iVar5 = Utilities.FUN_29E84(new Vector3Int(local_80[2, 0], local_80[2, 1], local_80[2, 2]));
                    iVar10 = Utilities.FUN_29E84(new Vector3Int(local_80[0, 0], local_80[0, 1], local_80[0, 2]));

                    if (iVar5 < 0)
                        iVar5 += 4095;

                    iVar10 = (iVar5 >> 12) * iVar10;
                    lVar5 = Utilities.Divdi3(uVar15.x, uVar15.y, iVar10, iVar10 >> 31);

                    if (local_3c < (int)lVar5)
                    {
                        local_3c = (int)lVar5;
                        local_80[1, 0] = local_80[2, 0];
                        local_80[1, 1] = local_80[2, 1];
                        local_80[1, 2] = local_80[2, 2];
                        local_80[1, 3] = local_80[2, 3];
                    }

                    uVar2 = (ushort)iVar14.configContainers[uVar2].previous;
                    iVar5 = local_30;
                    local_30 = iVar5;
                }

                local_b0[local_30, 0 + i] = piVar7.DAT_00.x + local_80[1, 0];
                local_b0[local_30, 2 + i] = piVar7.DAT_00.z + local_80[1, 2];
                uVar3 = terrain.FUN_1B750((uint)local_b0[local_30, 0 + i], (uint)local_b0[local_30, 2 + i]);
                local_b0[local_30, 1 + i] = uVar3;
            }

            local_30 += 2;
        }

        FUN_630C(local_b0, xrtpList[param1.DAT_0A], param1.DAT_0C);
    }

    private Junction FUN_50C0(int param1)
    {
        GameObject obj = new GameObject("ROAD" + counter.ToString().PadLeft(2, '0'));
        Junction newJunc = obj.AddComponent<Junction>();
        obj.AddComponent<MeshFilter>();
        obj.AddComponent<MeshRenderer>();
        newJunc.DAT_1C = param1;
        newJunc.DAT_20 = new List<Vector3Int>();
        newJunc.DAT_26 = new List<short>();
        newJunc.DAT_28 = new List<Vector3Int>();
        newJunc.DAT_2E = new List<short>();
        return newJunc;
    }

    private void FUN_50F0(XRTP_DB param1)
    {
        short sVar1;
        int iVar2;
        int iVar3;

        iVar2 = param1.DAT_28 >> 8;
        iVar3 = param1.DAT_24 >> 8;
        sVar1 = (short)Utilities.SquareRoot(iVar2 * iVar2 + iVar3 * iVar3);
        param1.DAT_30 = (short)(sVar1 - 128);
        param1.DAT_20 = 0;
        param1.DAT_18 = 0;

        /*if ((param1.DAT_2C & 2) == 0)
        {
            param1.V3_DAT_0C = new Vector3[param1.DAT_14 * 4];
            param1.C32_DAT_0C = new Color32[param1.DAT_14 * 10];
            param1.V3_DAT_10 = new Vector3[param1.DAT_1C * 14];
            param1.C32_DAT_10 = new Color32[param1.DAT_1C * 40];
            //...
        }
        else
        {
            param1.V3_DAT_0C = new Vector3[param1.DAT_14 * 5];
            param1.C32_DAT_0C = new Color32[param1.DAT_14 * 13];
            param1.V3_DAT_10 = new Vector3[param1.DAT_1C * 18];
            param1.C32_DAT_10 = new Color32[param1.DAT_1C * 52];
        }*/
    }

    private int FUN_57AC(int[] param1)
    {
        int iVar1;
        int iVar2;
        int iVar3;
        int iVar4;
        int iVar5;
        int piVar6;
        int iVar7;
        int iVar8;
        int iVar9;
        int iVar10;

        iVar10 = 1;
        piVar6 = 3;
        iVar3 = param1[2];
        iVar7 = param1[2];
        iVar8 = param1[0];
        iVar9 = param1[0];

        do
        {
            iVar1 = param1[piVar6];
            iVar4 = iVar1;

            if (iVar8 < iVar1)
                iVar4 = iVar8;

            if (iVar1 < iVar9)
                iVar1 = iVar9;

            iVar5 = param1[piVar6 + 2];
            iVar2 = iVar5;

            if (iVar3 < iVar5)
                iVar2 = iVar3;

            if (iVar5 < iVar7)
                iVar5 = iVar7;

            iVar10++;
            piVar6 += 3;
            iVar3 = iVar2;
            iVar7 = iVar5;
            iVar8 = iVar4;
            iVar9 = iVar1;
        } while (iVar10 < 3);

        iVar3 = iVar5 - iVar2;

        if (iVar5 - iVar2 < iVar1 - iVar4)
            iVar3 = iVar1 - iVar4;

        return iVar3;
    }

    private Junction FUN_5850(int[,] param1, XRTP_DB param2, ushort param3)
    {
        long lVar1;
        uint uVar2;
        uint uVar3;
        int iVar4;
        int iVar5;
        int iVar6;
        int iVar7;
        int iVar8;
        int iVar9;
        int iVar10;
        int iVar11;
        int iVar12;
        int iVar13;
        uint uVar14;
        int iVar15;
        int iVar16;
        int iVar17;
        int iVar18;
        uint uVar19;
        int iVar20;
        Junction jVar20;
        int iVar21;
        int iVar22;
        int iVar23;
        int iVar24;
        Vector3Int local_70;
        Vector2Int local_68;
        Vector2Int local_64;
        int local_50;
        int local_4c;
        int local_48;
        int local_40;
        int local_3c;
        int local_38;
        uint local_30;
        int local_2c;

        VigTerrain terrain = GameObject.FindObjectOfType<VigTerrain>();

        iVar4 = (param1[0, 3] * 3 - param1[0, 0]) + param1[1, 2] * -3 + param1[2, 1];

        if (iVar4 < 0)
            iVar4 += 15;

        iVar5 = (param1[1, 1] * 3 - param1[0, 2]) + param1[2, 0] * -3 + param1[2, 3];

        if (iVar5 < 0)
            iVar5 += 15;

        iVar6 = param1[0, 0] * 3 + param1[0, 3] * -6 + param1[1, 2] * 3;

        if (iVar6 < 0)
            iVar6 += 15;

        iVar7 = param1[0, 2] * 3 + param1[1, 1] * -6 + param1[2, 0] * 3;

        if (iVar7 < 0)
            iVar7 += 15;

        iVar8 = param1[0, 3] * 3 + param1[0, 0] * -3;

        if (iVar8 < 0)
            iVar8 += 15;

        iVar8 >>= 4;
        iVar9 = param1[1, 1] * 3 + param1[0, 2] * -3;

        if (iVar9 < 0)
            iVar9 += 15;

        iVar9 >>= 4;
        iVar10 = param1[0, 0];
        iVar11 = param1[0, 2];
        uVar2 = (uint)(iVar4 >> 4) * 3;
        uVar3 = (uint)(iVar5 >> 4) * 3;
        iVar12 = (iVar6 >> 4) * 2;
        iVar16 = (iVar7 >> 4) * 2;
        local_70 = new Vector3Int((short)iVar10, iVar10 >> 16, iVar11);
        local_48 = 0; //not in original code

        if ((param3 & 1) != 0)
        {
            local_70 = terrain.FUN_1BB50(param1[0, 0], param1[0, 1]);
            local_70 = Utilities.VectorNormal(local_70);
        }

        iVar20 = 0;
        local_50 = 0;

        do
        {
            iVar21 = iVar20 * iVar20;

            if (iVar21 < 0)
                iVar21 += 4095;

            iVar13 = (int)uVar2 * (iVar21 >> 12) + iVar12 * iVar20;

            if (iVar13 < 0)
                iVar13 += 4095;

            iVar13 = (iVar13 >> 12) + iVar8;

            if (iVar13 < 0)
                iVar13 += 255;

            iVar21 = (int)uVar3 * (iVar21 >> 12) + iVar16 * iVar20;

            if (iVar21 < 0)
                iVar21 += 4095;

            iVar21 = (iVar21 >> 12) + iVar9;

            if (iVar21 < 0)
                iVar21 += 255;

            iVar21 = (int)Utilities.SquareRoot((iVar13 >> 8) * (iVar13 >> 8) + (iVar21 >> 8) * (iVar21 >> 8));
            local_50++;
            iVar20 = iVar20 + param2.DAT_28 / iVar21;
        } while (iVar20 < 0x1000);

        local_4c = 0;
        param2.DAT_14 += local_50 * 2;
        jVar20 = FUN_50C0(local_50);
        iVar21 = (param1[0, 0] + param1[2, 1]) / 2;
        iVar22 = (param1[0, 2] + param1[2, 3]) / 2;
        uVar14 = (uint)terrain.FUN_1B750((uint)iVar21, (uint)iVar22);
        jVar20.pos = new Vector3Int(iVar21, (int)uVar14, iVar22);
        jVar20.xrtp = param2;
        jVar20.GetComponent<MeshRenderer>().materials = jVar20.xrtp.timFarList.ToArray();
        iVar21 = 0;

        if (-1 < local_50)
        {
            local_40 = 0;
            local_3c = 0;
            local_38 = 0;

            do
            {
                iVar13 = iVar21 * iVar21;

                if (iVar13 < 0)
                    iVar13 += 4095;

                iVar13 >>= 12;
                iVar22 = iVar13 * iVar21;

                if (iVar22 < 0)
                    iVar22 += 4095;

                iVar17 = (iVar4 >> 4) * (iVar22 >> 12) + (iVar6 >> 4) * iVar13 + iVar8 * iVar21;

                if (iVar17 < 0)
                    iVar17 += 255;

                iVar18 = (iVar5 >> 4) * (iVar22 >> 12) + (iVar7 >> 4) * iVar13 + iVar9 * iVar21;
                iVar22 = (iVar17 >> 8) + iVar10;

                if (iVar18 < 0)
                    iVar18 += 255;

                iVar17 = (int)uVar2 * iVar13 + iVar12 * iVar21;
                iVar18 = (iVar18 >> 8) + iVar11;

                if (iVar17 < 0)
                    iVar17 += 4095;

                iVar17 = (iVar17 >> 12) + iVar8;

                if (iVar17 < 0)
                    iVar17 += 255;

                iVar13 = (int)uVar3 * iVar13 + iVar16 * iVar21;
                iVar17 >>= 8;

                if (iVar13 < 0)
                    iVar13 += 4095;

                iVar13 = (iVar13 >> 12) + iVar9;

                if (iVar13 < 0)
                    iVar13 += 255;

                iVar13 >>= 8;
                iVar15 = (int)Utilities.SquareRoot(iVar17 * iVar17 + iVar13 * iVar13);
                iVar23 = ((iVar13 * param2.DAT_24) / 2) / iVar15;
                iVar24 = ((iVar17 * param2.DAT_24) / 2) / iVar15;
                iVar13 = iVar22 - iVar23;
                iVar22 += iVar23;
                iVar17 = iVar18 + iVar24;
                iVar18 -= iVar24;

                if ((param3 & 1) == 0)
                {
                    local_68 = new Vector2Int();
                    local_64 = new Vector2Int();
                    local_68.x = iVar13 - jVar20.pos.x >> 8;
                    iVar23 = terrain.FUN_1B750((uint)iVar13, (uint)iVar17);
                    local_68.y = iVar23 - jVar20.pos.y >> 8;
                    iVar23 = iVar17 - jVar20.pos.z;
                    local_64.x = iVar23 >> 8;

                    if (iVar13 < 0)
                        iVar13 += 0xffff;

                    if (iVar17 < 0)
                        iVar17 += 0xffff;

                    local_64.y = (int)((uint)terrain.vertices[terrain.chunks[(((uint)(iVar17 >> 16) >> 6) * 4 +
                                                                ((uint)(iVar13 >> 16) >> 6) * 128) / 4] * 4096 +
                                               ((iVar17 >> 16 & 63) * 2 + (iVar13 >> 16 & 63) * 128) / 2] >> 11) << 2;
                    jVar20.DAT_20.Add(new Vector3Int(local_68.x, local_68.y, local_64.x));
                    jVar20.DAT_26.Add((short)local_64.y);
                    local_68.x = iVar22 - jVar20.pos.x >> 8;
                    iVar13 = terrain.FUN_1B750((uint)iVar22, (uint)iVar18);
                    local_68.y = iVar13 - jVar20.pos.y >> 8;
                    iVar13 = iVar18 - jVar20.pos.z;

                    if (iVar22 < 0)
                        iVar22 += 0xffff;

                    if (iVar18 < 0)
                        iVar18 += 0xffff;

                    local_64.x = iVar13 >> 8;
                    local_64.y = (int)((uint)terrain.vertices[terrain.chunks[(((uint)(iVar18 >> 16) >> 6) * 4 +
                                                                          ((uint)(iVar22 >> 16) >> 6) * 128) / 4] * 4096 +
                                                           ((iVar18 >> 16 & 63) * 2 + (iVar22 >> 16 & 63) * 128) / 2] >> 11) << 2;
                    jVar20.DAT_28.Add(new Vector3Int(local_68.x, local_68.y, local_64.x));
                    jVar20.DAT_2E.Add((short)local_64.y);
                }
                else
                {
                    lVar1 = (long)(0x1000 - iVar21) * param1[0, 1];
                    local_30 = (uint)((long)iVar21 * param1[2, 2]);
                    local_2c = (int)((ulong)((long)iVar21 * param1[2, 2]) >> 32);
                    uVar19 = (uint)((int)lVar1 + local_30);
                    iVar24 = (int)((ulong)lVar1 >> 32) + local_2c + (int)(uVar19 < local_30 ? 1 : 0);
                    iVar23 = FUN_9C10(uVar19, iVar24, 0, 0);

                    if (iVar23 < 1)
                    {
                        uVar19 += 4095;
                        iVar24 += uVar19 < 0xfffU ? 1 : 0;
                    }

                    uVar19 = uVar19 >> 12 | (uint)(iVar24 << 20);
                    local_68 = new Vector2Int();
                    local_64 = new Vector2Int();
                    local_68.x = iVar13 - jVar20.pos.x >> 8;
                    local_68.y = (int)uVar19 - jVar20.pos.y >> 8;
                    local_64.x = iVar17 - jVar20.pos.z >> 8;
                    iVar17 = Utilities.FUN_29C5C(local_70, DAT_10F8);
                    iVar13 = 0;

                    if (0 < iVar17)
                        iVar13 = iVar17;

                    if (iVar13 < 0)
                        iVar13 += 0x1ffff;

                    iVar17 = (iVar13 >> 17) + 32;
                    iVar13 = 128;

                    if (iVar17 < 128)
                        iVar13 = iVar17;

                    local_64.y = iVar13;
                    jVar20.DAT_20.Add(new Vector3Int(local_68.x, local_68.y, local_64.x));
                    jVar20.DAT_26.Add((short)local_64.y);
                    local_68.x = iVar22 - jVar20.pos.x >> 8;
                    local_68.y = (int)uVar19 - jVar20.pos.y >> 8;
                    local_64.x = iVar18 - jVar20.pos.z >> 8;
                    iVar22 = Utilities.FUN_29C5C(local_70, DAT_10F8);
                    iVar13 = 0;

                    if (0 < iVar22)
                        iVar13 = iVar22;

                    if (iVar13 < 0)
                        iVar13 += 0x1ffff;

                    iVar22 = (iVar13 >> 17) + 32;
                    iVar13 = 128;

                    if (iVar22 < 128)
                        iVar13 = iVar22;

                    local_64.y = iVar13;
                    jVar20.DAT_28.Add(new Vector3Int(local_68.x, local_68.y, local_64.x));
                    jVar20.DAT_2E.Add((short)local_64.y);
                }

                iVar13 = Utilities.FUN_29DDC(jVar20.DAT_20[local_40]);

                if (iVar13 < local_48)
                    iVar13 = local_48;

                local_48 = Utilities.FUN_29DDC(jVar20.DAT_28[local_40]);

                if (local_48 < iVar13)
                    local_48 = iVar13;

                if (local_4c == local_50 - 1)
                    iVar21 = 4096;
                else
                    iVar21 += param2.DAT_28 / iVar15;

                local_40++;
                local_3c++;
                local_38++;
                local_4c++;
            } while (local_4c <= local_50);
        }

        iVar4 = (int)Utilities.SquareRoot(local_48);
        jVar20.DAT_18 = iVar4 << 8;
        return jVar20;
    }

    private void FUN_630C(int[,] param1, XRTP_DB param2, ushort param3)
    {
        int iVar3;
        int iVar5;
        Junction jVar4;
        Vector3Int local_70;
        Vector3Int local_1c;
        Vector3Int local_64;
        Vector3Int local_58;
        Vector3Int local_28;
        Vector3Int local_34;
        Vector3Int local_4c;
        Vector3Int local_40;
        Vector3Int local_10;

        iVar3 = FUN_57AC(new int[] { param1[0, 0], param1[0, 1], param1[0, 2], param1[0, 3],
                                     param1[1, 0], param1[1, 1], param1[1, 2], param1[1, 3],
                                     param1[2, 0], param1[2, 1], param1[2, 2], param1[2, 3] });

        if (iVar3 < 0x100000)
        {
            //if (param2.DAT_28 != 0) //tmp
            //{
                jVar4 = FUN_5850(param1, param2, param3);
                counter++;
                roadList.Add(jVar4);
            //}
            /*else
            {
                Debug.Log("!");
                counter++;
            }*/
        }
        else
        {
            iVar3 = (param1[0, 3] + param1[1, 2]) / 2;
            iVar5 = (param1[1, 1] + param1[2, 0]) / 2;
            local_70 = new Vector3Int(param1[0, 0], param1[0, 1], param1[0, 2]);
            local_1c = new Vector3Int(param1[2, 1], param1[2, 2], param1[2, 3]);
            local_64 = new Vector3Int((param1[0, 0] + param1[0, 3]) / 2, 0,
                                    (param1[0, 2] + param1[1, 1]) / 2);
            local_58 = new Vector3Int((local_64.x + iVar3) / 2, 0, (local_64.z + iVar5) / 2);
            local_28 = new Vector3Int((param1[1, 2] + param1[2, 1]) / 2, 0,
                                    (param1[2, 0] + param1[2, 3]) / 2);
            local_34 = new Vector3Int((local_28.x + iVar3) / 2, 0, (local_28.z + iVar5) / 2);
            local_4c = new Vector3Int((local_58.x + local_34.x) / 2,
                                    (param1[0, 1] + param1[2, 2]) / 2,
                                    (local_58.z + local_34.z) / 2);
            local_40 = local_4c;
            local_10 = local_4c;
            FUN_630C(new int[,] { { local_70.x, local_70.y, local_70.z, local_64.x },
                                  { local_64.y, local_64.z, local_58.x, local_58.y },
                                  { local_58.z, local_4c.x, local_4c.y, local_4c.z } },
                                    param2, param3);
            FUN_630C(new int[,] { { local_40.x, local_40.y, local_40.z, local_34.x },
                                  { local_34.y, local_34.z, local_28.x, local_28.y },
                                  { local_28.z, local_1c.x, local_1c.y, local_1c.z } }, 
                                    param2, param3);
        }
    }

    private void FUN_6604(int[,] param1, int param2, int param3)
    {
        long lVar1;
        long lVar2;
        uint uVar3;
        uint uVar4;
        int puVar5;
        uint uVar6;
        uint uVar7;
        uint uVar8;
        int iVar9;
        uint uVar10;
        int iVar11;
        uint uVar12;
        uint uVar13;
        uint[] local_188 = new uint[27];
        uint local_30;
        int local_2c;

        uVar12 = 0;
        uVar10 = 0x8000;
        iVar11 = 0;
        uVar7 = 0x10000;

        do
        {
            uVar3 = (uint)param1[0, 3];
            uVar8 = 0x10000 - uVar10;
            iVar9 = -(int)(0x10000 < uVar10 ? 1 : 0) - iVar11;
            uVar6 = (uint)param1[1, 2];
            uVar13 = (uint)((ulong)uVar10 * uVar6);
            uVar4 = (uint)((int)((ulong)uVar8 * uVar3) + (int)uVar13);
            local_188[26] = (uint)Utilities.Divdi3((int)uVar4, (int)((ulong)uVar8 * uVar3 >> 32) +
                                                       (int)uVar8 * ((int)uVar3 >> 31) + (int)uVar3 * iVar9 +
                                                       (int)((ulong)uVar10 * uVar6 >> 32) +
                                                       (int)uVar10 * ((int)uVar6 >> 31) + (int)uVar6 * iVar11 +
                                                       (int)(uVar4 < uVar13 ? 1 : 0), 0x10000, 0);
            uVar3 = (uint)param1[1, 1];
            uVar6 = (uint)param1[2, 0];
            uVar13 = (uint)((ulong)uVar10 * uVar6);
            uVar4 = (uint)((int)((ulong)uVar8 * uVar3) + (int)uVar13);
            local_188[25] = (uint)Utilities.Divdi3((int)uVar4, (int)((ulong)uVar8 * uVar3 >> 32) +
                                                             (int)uVar8 * ((int)uVar3 >> 31) + (int)uVar3 * iVar9 +
                                                             (int)((ulong)uVar10 * uVar6 >> 32) +
                                                             (int)uVar10 * ((int)uVar6 >> 31) + (int)uVar6 * iVar11 +
                                                             (int)(uVar4 < uVar13 ? 1 : 0), 0x10000, 0);
            local_188[24] = local_188[26];
            local_188[0] = (uint)param1[0, 0];
            local_188[1] = (uint)param1[0, 1];
            local_188[2] = (uint)param1[0, 2];
            local_188[21] = (uint)param1[2, 1];
            local_188[22] = (uint)param1[2, 2];
            local_188[23] = (uint)param1[2, 3];
            uVar3 = (uint)param1[0, 0];
            uVar6 = (uint)param1[0, 3];
            uVar13 = (uint)((ulong)uVar10 * uVar6);
            uVar4 = (uint)((int)((ulong)uVar8 * uVar3) + (int)uVar13);
            local_188[27] = local_188[25];
            local_188[28] = (uint)Utilities.Divdi3((int)uVar4, (int)((ulong)uVar8 * uVar3 >> 32) +
                                                              (int)uVar8 * ((int)uVar3 >> 31) + (int)uVar3 * iVar9 +
                                                              (int)((ulong)uVar10 * uVar6 >> 32) +
                                                              (int)uVar10 * ((int)uVar6 >> 31) + (int)uVar6 * iVar11 +
                                                              (int)(uVar4 < uVar13 ? 1 : 0), 0x10000, 0);
            local_188[29] = 0;
            uVar3 = (uint)param1[0, 2];
            uVar6 = (uint)param1[1, 1];
            uVar13 = (uint)((ulong)uVar10 * uVar6);
            uVar4 = (uint)((int)((ulong)uVar8 * uVar3) + (int)uVar13);
            local_188[5] = (uint)Utilities.Divdi3((int)uVar4, (int)((ulong)uVar8 * uVar3 >> 32) +
                                                            (int)uVar8 * ((int)uVar3 >> 31) + (int)uVar3 * iVar9 +
                                                            (int)((ulong)uVar10 * uVar6 >> 32) +
                                                            (int)uVar10 * ((int)uVar6 >> 31) + (int)uVar6 * iVar11 +
                                                            (int)(uVar4 < uVar13 ? 1 : 0), 0x10000, 0);
            local_188[3] = local_188[28];
            local_188[4] = local_188[29];
            uVar4 = (uint)((ulong)uVar10 * local_188[24]);
            uVar3 = (uint)((int)((ulong)uVar8 * local_188[28]) + (int)uVar4);
            local_188[30] = local_188[5];
            local_188[26] = (uint)Utilities.Divdi3((int)uVar3, (int)((ulong)uVar8 * local_188[28] >> 32) +
                                                             (int)uVar8 * ((int)local_188[28] >> 31) + (int)local_188[28] * iVar9 +
                                                             (int)((ulong)uVar10 * local_188[24] >> 32) +
                                                             (int)uVar10 * ((int)local_188[24] >> 31) + (int)local_188[24] * iVar11 +
                                                             (int)(uVar3 < uVar4 ? 1 : 0), 0x10000, 0);
            uVar4 = (uint)((ulong)uVar10 * local_188[25]);
            local_188[27] = 0;
            uVar3 = (uint)((int)((ulong)uVar8 * local_188[25]) + (int)uVar4);
            local_188[8] = (uint)Utilities.Divdi3((int)uVar3, (int)((ulong)uVar8 * local_188[5] >> 32) +
                                                            (int)uVar8 * ((int)local_188[5] >> 31) + (int)local_188[5] * iVar9 +
                                                            (int)((ulong)uVar10 * local_188[25] >>
                                                            32) + (int)uVar10 * ((int)local_188[25] >> 31) +
                                                            (int)local_188[25] * iVar11 + (int)(uVar3 < uVar4 ? 1 : 0), 0x10000, 0);
            local_188[6] = local_188[26];
            local_188[7] = local_188[27];
            uVar3 = (uint)param1[2, 0];
            uVar6 = (uint)param1[1, 2];
            uVar13 = (uint)((ulong)uVar8 * uVar6);
            uVar4 = (uint)((int)((ulong)uVar10 * uVar3) + (int)uVar13);
            local_188[28] = local_188[8];
            local_188[26] = (uint)Utilities.Divdi3((int)uVar4, (int)((ulong)uVar10 * uVar3 >> 32) +
                                                             (int)uVar10 * ((int)uVar3 >> 31) + (int)uVar3 * iVar11 +
                                                             (int)((ulong)uVar8 * uVar6 >> 32) +
                                                             (int)uVar8 * ((int)uVar6 >> 31) + (int)uVar6 * iVar9 +
                                                             (int)(uVar4 < uVar13 ? 1 : 0), 0x10000, 0);
            local_188[27] = 0;
            uVar3 = (uint)param1[2, 3];
            uVar6 = (uint)param1[2, 0];
            uVar13 = (uint)((ulong)uVar8 * uVar6);
            uVar4 = (uint)((int)((ulong)uVar10 * uVar3) + (int)uVar13);
            local_188[20] = (uint)Utilities.Divdi3((int)uVar4, (int)((ulong)uVar10 * uVar3 >> 32) +
                                                             (int)uVar10 * ((int)uVar3 >> 31) + (int)uVar3 * iVar11 +
                                                             (int)((ulong)uVar8 * uVar6 >> 32) +
                                                             (int)uVar8 * ((int)uVar6 >> 31) + (int)uVar6 * iVar9 +
                                                             (int)(uVar4 < uVar13 ? 1 : 0), 0x10000, 0);
            local_188[18] = local_188[26];
            local_188[19] = local_188[27];
            uVar4 = (uint)((ulong)uVar8 * local_188[24]);
            uVar3 = (uint)((int)((ulong)uVar10 * local_188[26]) + (int)uVar4);
            local_188[28] = local_188[20];
            local_188[26] = (uint)Utilities.Divdi3((int)uVar3, (int)((ulong)uVar10 * local_188[26] >> 32) +
                                                             (int)uVar10 * ((int)local_188[26] >> 31) + (int)local_188[26] * iVar11 +
                                                             (int)((ulong)uVar8 * local_188[24] >> 32) +
                                                             (int)uVar8 * ((int)local_188[24] >> 31) + (int)local_188[24] * iVar9 +
                                                             (int)(uVar3 < uVar4 ? 1 : 0), 0x10000, 0);
            uVar4 = (uint)((ulong)uVar8 * local_188[25]);
            local_188[27] = 0;
            uVar3 = (uint)((int)((ulong)uVar10 * local_188[20]) + (int)uVar4);
            local_188[17] = (uint)Utilities.Divdi3((int)uVar3, (int)((ulong)uVar10 * local_188[20] >> 32) +
                                                             (int)uVar10 * ((int)local_188[20] >> 31) + (int)local_188[20] * iVar11 +
                                                             (int)((ulong)uVar8 * local_188[25] >> 32) +
                                                             (int)uVar8 * ((int)local_188[25] >> 31) + (int)local_188[25] * iVar9 +
                                                             (int)(uVar3 < uVar4 ? 1 : 0), 0x10000, 0);
            local_188[15] = local_188[26];
            local_188[16] = local_188[27];
            uVar4 = (uint)((ulong)uVar10 * local_188[26]);
            uVar3 = (uint)((int)((ulong)uVar8 * local_188[6]) + (int)uVar4);
            local_188[28] = local_188[17];
            local_188[26] = (uint)Utilities.Divdi3((int)uVar3, (int)((ulong)uVar8 * local_188[6] >> 32) +
                                                             (int)uVar8 * ((int)local_188[6] >> 31) + (int)local_188[6] * iVar9 +
                                                             (int)((ulong)uVar10 * local_188[26] >> 32) +
                                                             (int)uVar10 * ((int)local_188[26] >> 31) + (int)local_188[26] * iVar11 +
                                                             (int)(uVar3 < uVar4 ? 1 : 0), 0x10000, 0);
            uVar3 = (uint)param1[0, 1];
            uVar6 = (uint)param1[2, 2];
            uVar13 = (uint)((ulong)uVar10 * uVar6);
            uVar4 = (uint)((int)((ulong)uVar8 * uVar3) + (int)uVar13);
            local_188[27] = (uint)Utilities.Divdi3((int)uVar3, (int)((ulong)uVar8 * uVar3 >> 32) +
                                                             (int)uVar8 * ((int)uVar3 >> 31) + (int)uVar3 * iVar9 +
                                                             (int)((ulong)uVar10 * uVar6 >> 32) +
                                                             (int)uVar10 * ((int)uVar6 >> 31) + (int)uVar6 * iVar11 +
                                                             (int)(uVar4 < uVar13 ? 1 : 0), 0x10000, 0);
            uVar4 = (uint)((ulong)uVar10 * local_188[17]);
            uVar3 = (uint)((int)((ulong)uVar8 * local_188[8]) + (int)uVar4);
            local_188[28] = (uint)Utilities.Divdi3((int)uVar3, (int)((ulong)uVar8 * local_188[8] >> 32) +
                                                             (int)uVar8 * ((int)local_188[8] >> 31) + (int)local_188[8] * iVar9 +
                                                             (int)((ulong)uVar10 * local_188[17] >> 32) +
                                                             (int)uVar10 * ((int)local_188[17] >> 31) + (int)local_188[17] * iVar11 +
                                                             (int)(uVar3 < uVar4 ? 1 : 0), 0x10000, 0);
            local_188[12] = local_188[26];
            local_188[13] = local_188[27];
            local_188[17] = local_188[28];
            local_188[9] = local_188[26];
            local_188[10] = local_188[27];
            local_188[11] = local_188[28];
            uVar3 = uVar10;

            if (param2 == 0)
            {
                lVar1 = (long)((int)local_188[26] - param1[0, 0]) * ((int)local_188[26] - param1[0, 0]);
                lVar2 = (long)((int)local_188[28] - param1[0, 2]) * ((int)local_188[28] - param1[0, 2]);
                local_30 = (uint)lVar2;
                local_2c = (int)((ulong)lVar2 >> 32);
                iVar9 = (int)((ulong)((long)param3 * param3) >> 32);
                uVar4 = (uint)((int)lVar1 + local_30);
                iVar11 = (int)((ulong)lVar1 >> 32) + local_2c + (int)(uVar4 < local_30 ? 1 : 0);

                if (iVar11 <= iVar9 &&
                    (iVar11 != iVar9 || uVar4 <= (int)((long)param3 * param3)))
                {
                    uVar3 = uVar7;
                    uVar12 = uVar10;
                }
            }
            else
            {
                lVar1 = (long)((int)local_188[26] - param1[2, 1]) * ((int)local_188[26] - param1[2, 1]);
                lVar2 = (long)((int)local_188[28] - param1[2, 3]) * ((int)local_188[28] - param1[2, 3]);
                local_30 = (uint)lVar2;
                local_2c = (int)((ulong)lVar2 >> 32);
                iVar9 = (int)((ulong)((long)param3 * param3) >> 32);
                uVar4 = (uint)((int)lVar1 + local_30);
                iVar11 = (int)((ulong)lVar1 >> 32) + local_2c + (int)(uVar4 < local_30 ? 1 : 0);

                if (iVar9 < iVar11 ||
                    (iVar11 == iVar9 && (uint)((long)param3 * param3) < uVar4))
                {
                    uVar3 = uVar7;
                    uVar12 = uVar10;
                }
            }

            iVar11 = (int)(uVar12 + uVar3);
            uVar10 = (uint)(iVar11 / 2);
            iVar11 = iVar11 - (iVar11 >> 31) >> 31;
            uVar7 = uVar3;

            if ((int)(uVar3 - uVar12) < 2)
            {
                puVar5 = 0;

                if (param2 == 0)
                {
                    puVar5 = 12;

                    for (int i = 0; puVar5 < 24; puVar5 += 4, i++)
                    {
                        uVar7 = local_188[puVar5 + 1];
                        uVar12 = local_188[puVar5 + 2];
                        uVar10 = local_188[puVar5 + 3];
                        param1[i, 0] = (int)local_188[puVar5];
                        param1[i, 1] = (int)uVar7;
                        param1[i, 2] = (int)uVar12;
                        param1[i, 3] = (int)uVar10;
                    }
                }
                else
                {
                    for (int i = 0; puVar5 < 12; puVar5 += 4, i++)
                    {
                        uVar7 = local_188[puVar5 + 1];
                        uVar12 = local_188[puVar5 + 2];
                        uVar10 = local_188[puVar5 + 3];
                        param1[i, 0] = (int)local_188[puVar5];
                        param1[i, 1] = (int)uVar7;
                        param1[i, 2] = (int)uVar12;
                        param1[i, 3] = (int)uVar10;
                    }
                }

                return;
            }
        } while (true);
    }
}
