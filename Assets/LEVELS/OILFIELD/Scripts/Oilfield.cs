using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oilfield : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        GameManager.instance.FUN_17F34(0x20800, 0x4420000);
        DAT_84_2 = new List<VigTuple>[2] { new List<VigTuple>(), new List<VigTuple>() };
        DAT_9C = 0;
        DAT_A0_2 = 0;
        DAT_A4 = 0;
        DAT_A8 = 0x10000;
        DAT_AC = 0x8000;
    }

    private static Vector3Int glacierSpawn = new Vector3Int(0x50EED71, 0x2DE5D6, 0x3A80D81);

    public static Oilfield instance;
    public VigTuple2 DAT_80_2; //0x80
    public List<VigTuple>[] DAT_84_2; //0x84
    public int DAT_9C; //0a9C
    public int DAT_A0_2; //0xA0
    public int DAT_A4;
    public int DAT_A8;
    public int DAT_AC;
    private int DAT_B0;
    
    //FUN_A88 (OILFIELD.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        int iVar1;
        short sVar2;
        uint uVar3;
        ConfigContainer ccVar3;
        VigObject oVar4;
        GlacierSmall ppcVar4;
        int iVar7;
        VigObject oVar7;
        VigTuple2 tVar7;
        int iVar8;
        int iVar9;
        Vector3Int local_10;

        switch (arg1)
        {
            case 1:
                GameManager.instance.offsetFactor = 2.5f;
                GameManager.instance.offsetStart = 0;
                GameManager.instance.angleOffset = 0.4f;
                Color32 color = LevelManager.instance.DAT_DE0;
                color.a = 0x80;
                UIManager.instance.underwater.color = color;
                tVar7 = GameManager.instance.FUN_2FFD0(1);
                DAT_80_2 = tVar7;
                oVar7 = GameManager.instance.FUN_30250(GameManager.instance.DAT_1078, 0x100);
                oVar4 = GameManager.instance.FUN_4AC1C(0xfe000000, oVar7);
                GameManager.instance.DAT_1038 = oVar4 != null ? 1 : 0;
                goto case 2;
            case 2:
                GameManager.instance.FUN_34B34();
                GameManager.instance.FUN_30CB0(this, 240);
                iVar1 = 0;

                while (DAT_A4 <= 16 && iVar1 < 3)
                {
                    iVar8 = Random.Range(0, 3);
                    ppcVar4 = LevelManager.instance.xobfList[42].ini.FUN_2C17C((ushort)Glacier.DAT_29D0[iVar8], typeof(GlacierSmall), 8) as GlacierSmall;
                    iVar9 = Random.Range(-40, 41);
                    local_10 = new Vector3Int();
                    local_10.x = iVar9 * DAT_A8 + glacierSpawn.x;
                    local_10.y = glacierSpawn.y;
                    local_10.z = glacierSpawn.z;
                    ppcVar4.screen = local_10;
                    ppcVar4.tags = 1;
                    ppcVar4.id = (short)DAT_B0;
                    ppcVar4.flags |= 0x80;
                    iVar8 = Random.Range(-4, 3);
                    iVar9 = Random.Range(0, 21);
                    ppcVar4.physics1.X = iVar8 * DAT_AC;
                    ppcVar4.physics1.Z = iVar9 * DAT_AC;
                    sVar2 = (short)GameManager.FUN_2AC5C();
                    ppcVar4.vr.y = sVar2;
                    iVar9 = (int)GameManager.FUN_2AC5C();
                    GameManager.instance.FUN_30CB0(ppcVar4, (iVar9 * 840 >> 15) + 1800);
                    ppcVar4.FUN_3066C();
                    DAT_A4++;
                    DAT_B0++;
                    iVar1++;
                }

                if (DAT_B0 >= 16)
                    DAT_B0 = 0;

                if (DAT_9C != 0)
                    return 0;

                uVar3 = GameManager.FUN_2AC5C();
                iVar7 = 166;

                if ((iVar7 & 1) != 0)
                    iVar7 = 161;

                oVar4 = GameManager.instance.FUN_31EDC(iVar7);
                oVar4.flags |= 0x80;
                oVar4.FUN_3066C();
                DAT_9C++;

                break;
            case 4:
                FUN_4F4(DAT_84_2[0]);
                FUN_4F4(DAT_84_2[1]);
                break;
            case 17:
                GameManager.instance.FUN_17EB8();
                return 0;
        }

        return 0;
    }

    public override uint UpdateW(VigObject arg1, int arg2, int arg3)
    {
        Spark2 puVar2;
        int iVar3;
        uint uVar3;
        int iVar4;
        VigObject oVar4;
        TileData tVar4;
        VigCamera cVar4;
        int iVar5;
        VigObject oVar5;
        Mud3 mVar6;
        int iVar7;
        VigObject oVar7;
        ConfigContainer ccVar7;
        Vehicle vVar8;
        Vector3Int local_18;

        switch (arg2)
        {
            case 18:
                if (arg3 != 0 && arg1.type == 8)
                {
                    tVar4 = GameManager.instance.terrain.GetTileByPosition((uint)arg1.vTransform.position.x, (uint)arg1.vTransform.position.z);

                    if (tVar4.DAT_10[3] == 1)
                    {
                        GameObject obj = new GameObject();
                        puVar2 = obj.AddComponent<Spark2>();
                        oVar4 = LevelManager.instance.xobfList[19].ini.FUN_2C17C(153, typeof(VigObject), 8);
                        oVar5 = LevelManager.instance.xobfList[19].ini.FUN_2C17C(51, typeof(VigObject), 8);
                        iVar3 = 1;

                        if (DAT_80_2.array[0] <= arg1.vTransform.position.x >> 16 &&
                            DAT_80_2.array[1] <= arg1.vTransform.position.z >> 16 &&
                            arg1.vTransform.position.x >> 16 <= DAT_80_2.array[0] + DAT_80_2.array[2])
                            iVar3 = DAT_80_2.array[1] + DAT_80_2.array[3] < arg1.vTransform.position.z >> 16 ? 1 : 0;

                        mVar6 = FUN_848(DAT_84_2[iVar3], arg1.vTransform.position);
                        oVar5.vTransform = GameManager.FUN_2A39C();
                        oVar4.vTransform = GameManager.FUN_2A39C();
                        oVar4.flags = 0x10;
                        Utilities.FUN_2CC9C(puVar2, oVar4);
                        Utilities.FUN_2CC9C(puVar2, oVar5);
                        Utilities.ParentChildren(puVar2, puVar2);
                        puVar2.flags = 0x84;
                        puVar2.type = 3;
                        puVar2.screen = arg1.vTransform.position;
                        puVar2.physics1.M6 = 0x1000;
                        puVar2.vCollider = new VigCollider(oVar4.vCollider.reader.GetBuffer());

                        if (mVar6 == null)
                        {
                            uVar3 = GameManager.FUN_2AC5C();
                            local_18 = new Vector3Int();
                            local_18.x = (short)(uVar3 >> 3);
                            local_18.y = 0;
                            iVar4 = (int)GameManager.FUN_2AC5C();
                            local_18.z = (short)((iVar4 << 12) >> 15);
                            local_18 = Utilities.VectorNormal(local_18);
                        }
                        else
                        {
                            if (mVar6.DAT_8C == 0)
                                Utilities.FUN_2A168(out local_18, puVar2.screen, mVar6.DAT_88.screen);
                            else
                            {
                                puVar2.DAT_90 = 0;
                                puVar2.tags = 1;
                                puVar2.DAT_94 = mVar6;
                                Utilities.FUN_2A168(out local_18, puVar2.screen, mVar6.DAT_90[puVar2.DAT_90]);
                            }
                        }

                        iVar4 = local_18.x * 9155;

                        if (iVar4 < 0)
                            iVar4 += 4095;

                        puVar2.physics1.X = iVar4 >> 12;
                        puVar2.physics1.Y = 0;
                        iVar4 = local_18.z * 9155;

                        if (iVar4 < 0)
                            iVar4 += 4095;

                        puVar2.physics1.Z = iVar4 >> 12;
                        puVar2.FUN_3066C();
                        return 0;
                    }
                }

                GameManager.instance.FUN_327CC(arg1);
                return 0;
            case 19:
                uVar3 = GameManager.FUN_2AC5C();
                iVar7 = 100;

                if ((uVar3 & 1) != 0)
                    iVar7 = 99;

                oVar4 = GameManager.instance.FUN_318D0(iVar7);
                ccVar7 = oVar4.FUN_2C5F4(0x8000);
                arg1.vTransform = GameManager.instance.FUN_2CEAC(oVar4, ccVar7);
                iVar5 = arg1.vTransform.rotation.V02 * 11444;

                if (iVar5 < 0)
                    iVar5 += 31;

                arg1.physics1.X = iVar5 >> 5;
                iVar5 = arg1.vTransform.rotation.V12 * 11444;

                if (iVar5 < 0)
                    iVar5 += 31;

                arg1.physics1.Y = iVar5 >> 5;
                iVar5 = arg1.vTransform.rotation.V22 * 11444;

                if (iVar5 < 0)
                    iVar5 += 31;

                arg1.physics1.Z = iVar5 >> 5;
                arg1.physics2.X = 0;
                arg1.physics2.Y = 0;
                arg1.physics2.Z = 0;
                iVar7 = GameManager.instance.FUN_1DD9C();
                GameManager.instance.FUN_1E580(iVar7, GameManager.instance.DAT_C2C, 37, arg1.vTransform.position);
                vVar8 = (Vehicle)arg1;

                if (vVar8.vCamera != null)
                {
                    iVar7 = 513;

                    if (oVar4.id != 99)
                        iVar7 = 514;

                    oVar7 = GameManager.instance.FUN_30250(GameManager.instance.DAT_1078, iVar7);
                    cVar4 = LevelManager.instance.FUN_4B984(vVar8, oVar7);
                    cVar4.maxHalfHealth = 0x100;
                    vVar8.vCamera.flags &= 0xfbffffff;
                    GameManager.instance.FUN_30CB0(vVar8.vCamera, 90);
                    vVar8.vCamera = cVar4;
                    LevelManager.instance.defaultCamera.transform.SetParent(cVar4.transform, false);
                    cVar4.FUN_30B78();
                }

                vVar8.FUN_41FEC();
                return 0xffffffff;
        }

        return 0;
    }

    public override uint UpdateW(VigObject arg1, int arg2, Vector3Int arg3)
    {
        VigTuple ppiVar1;
        Mud2 puVar2;
        int iVar4;
        Vehicle vVar5;
        int iVar6;
        Wheel wVar6;
        VigTuple2 tVar6;
        int iVar7;
        List<VigTuple> ppiVar8;
        Mud3 puVar9;
        int iVar10;

        switch (arg2)
        {
            case 10:
                vVar5 = (Vehicle)Utilities.FUN_2CDB0(arg1);

                if (vVar5.wheelsType == _WHEELS.Air)
                    return 0;

                if (arg1.vMesh != null)
                {
                    tVar6 = DAT_80_2;
                    iVar10 = 0;
                    puVar2 = null;

                    if (arg3.x >> 16 < tVar6.array[0] || arg3.z >> 16 < tVar6.array[1] ||
                        tVar6.array[0] + tVar6.array[2] < arg3.x >> 16 ||
                        tVar6.array[1] + tVar6.array[3] < arg3.z >> 16)
                        iVar10 = 1;
                    
                    if (vVar5.physics1.W < 4577)
                    {
                        if ((vVar5.flags & 0x20000) == 0)
                        {
                            GameObject obj = new GameObject();
                            puVar2 = obj.AddComponent<Mud2>();
                            puVar2.type = 255;
                            puVar2.child = vVar5;
                            puVar2.FUN_30B78();
                            vVar5.flags |= 0x20000;
                        }

                        if (arg1.physics1.Z - arg1.physics2.X < 0x3c00)
                        {
                            iVar6 = -vVar5.physics1.W + 4577;

                            if (iVar6 < 0)
                                iVar6 = -vVar5.physics1.W + 4584;

                            arg1.physics2.X -= iVar6 >> 3;
                        }

                        if (arg1 == vVar5.wheels[2])
                            wVar6 = vVar5.wheels[4];
                        else
                        {
                            if (arg1 != vVar5.wheels[3]) goto LAB_11C4;

                            wVar6 = vVar5.wheels[5];
                        }

                        if (wVar6 != null)
                            wVar6.physics2.X = (wVar6.physics1.Z + arg1.physics2.X) - arg1.physics1.Z;
                    }

                    LAB_11C4:
                    ppiVar8 = DAT_84_2[iVar10];
                    
                    for (int i = 0; i < ppiVar8.Count; i++)
                    {
                        ppiVar1 = ppiVar8[i];
                        puVar9 = (Mud3)ppiVar1.vObject;

                        if (puVar9.DAT_88 == vVar5)
                        {
                            if (GameManager.instance.DAT_28 == puVar9.DAT_80_2)
                                return 0;

                            iVar4 = GameManager.instance.DAT_28;
                            puVar9.DAT_8C = 0;
                            puVar9.DAT_80_2 = iVar4;
                            GameManager.instance.FUN_30CB0(puVar9, 30);
                            return 0;
                        }
                    }

                    GameObject obj2 = new GameObject();
                    puVar9 = obj2.AddComponent<Mud3>();
                    GameManager.instance.FUN_30080(DAT_84_2[iVar10], puVar9);
                    puVar9.tags = 0;
                    puVar9.DAT_88 = vVar5;
                    puVar9.DAT_84_2 = iVar10;
                    iVar7 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E580(iVar7, LevelManager.instance.xobfList[42].sndList, 0, vVar5.vTransform.position);
                    iVar4 = GameManager.instance.DAT_28;
                    puVar9.DAT_8C = 0;
                    puVar9.DAT_80_2 = iVar4;
                    GameManager.instance.FUN_30CB0(puVar9, 30);
                    return 0;
                }

                break;
        }

        return 0;
    }

    //FUN_4F4 (OILFIELD.DLL)
    private static void FUN_4F4(List<VigTuple> param1)
    {
        List<VigTuple> ppiVar1;
        VigTuple ppiVar2;

        ppiVar1 = param1;

        for (int i = 0; i < ppiVar1.Count; i++)
        {
            ppiVar2 = ppiVar1[i];
            ppiVar2.vObject.FUN_30C68();
            Destroy(ppiVar2.vObject.gameObject);
            ppiVar2.vObject = null;
        }

        GameManager.instance.FUN_3001C(param1);
    }

    //FUN_848 (OILFIELD.DLL)
    private static Mud3 FUN_848(List<VigTuple> param1, Vector3Int param2)
    {
        List<VigTuple> ppiVar1;
        VigTuple ppiVar2;
        int iVar3;
        Vehicle vVar3;
        int iVar4;
        VigObject oVar5;

        oVar5 = null;
        iVar4 = 0x7fff0000;
        ppiVar1 = param1;

        for (int i = 0; i < ppiVar1.Count; i++)
        {
            ppiVar2 = ppiVar1[i];
            vVar3 = ((Mud3)ppiVar2.vObject).DAT_88;

            if (vVar3 != null && vVar3.maxHalfHealth != 0)
            {
                iVar3 = Utilities.FUN_29F6C(param2, vVar3.screen);

                if (iVar3 < iVar4)
                {
                    oVar5 = ppiVar2.vObject;
                    iVar4 = iVar3;
                }
            }
        }

        return oVar5 as Mud3;
    }
}
