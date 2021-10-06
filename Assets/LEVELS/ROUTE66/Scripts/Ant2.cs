using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum _ANT2_TYPE
{
    Default, 
    Ant2 //FUN_2CAC (ROUTE66.DLL)
};

public class Ant2 : Destructible
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //0x138 (ROUTE66.DLL)
    private static Vector3Int DAT_138 = new Vector3Int(0x7000, 0x7000, 0x7000);
    //0x148 (ROUTE66.DLL)
    private static Vector3Int DAT_148 = new Vector3Int(0, -0x80000, 0);
    //0x154 (ROUTE66.DLL)
    private static Vector3Int DAT_154 = new Vector3Int(0x7800, 0x7800, 0x7800);

    public _ANT2_TYPE state;

    public override uint OnCollision(HitDetection hit)
    {
        bool bVar15;

        switch (state)
        {
            case _ANT2_TYPE.Ant2:
                bVar15 = FUN_32CF0(hit);

                if (bVar15)
                    FUN_2B5C();

                break;
        }

        return 0;
    }

    //FUN_2CAC (ROUTE66.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        sbyte sVar1;
        short sVar2;
        int iVar3;
        int iVar4;
        Ant3 ppcVar5;
        Ballistic puVar6;
        int iVar7;
        uint uVar8;
        VigObject piVar9;
        VigTuple ppiVar10;
        List<VigTuple> ppiVar13;
        Ant4 ppcVar14;
        int iVar15;
        bool bVar15;
        Vector3Int local_20;

        switch (state)
        {
            case _ANT2_TYPE.Ant2:
                if (arg1 == 2)
                {
                    if (tags == 0)
                    {
                        FUN_30B78();
                        iVar3 = GameManager.instance.FUN_1DD9C();
                        GameManager.instance.FUN_1E188(iVar3, GameManager.instance.DAT_C2C, 58);
                        GameManager.instance.FUN_1E30C(iVar3, 0x800);
                    }
                }
                else
                {
                    if (arg1 < 3)
                    {
                        if (arg1 == 0 && tags == 0)
                        {
                            FUN_2AF20();

                            if (vTransform.position.y < screen.y)
                            {
                                sVar1 = (sbyte)(DAT_19 - 1);
                                DAT_19 = (byte)sVar1;

                                if (sVar1 == 0)
                                {
                                    puVar6 = LevelManager.instance.xobfList[19].ini.FUN_2C17C(109, typeof(Ballistic), 8) as Ballistic;
                                    uVar8 = GameManager.FUN_2AC5C();
                                    DAT_19 = (byte)((uVar8 & 3) + 5);
                                    puVar6.flags = 0x434;
                                    puVar6.id = 0;
                                    puVar6.vTransform.position.x = vTransform.position.x + ((int)(uVar8 & 3) - 2) * 0x8000;
                                    puVar6.vTransform.position.y = vTransform.position.y;
                                    puVar6.vTransform.position.z = vTransform.position.z + (((int)uVar8 >> 2 & 3) - 2) * 0x8000;
                                    FUN_50F0(ref puVar6.vTransform.rotation, DAT_154);
                                    puVar6.FUN_305FC();
                                }
                            }
                            else
                            {
                                iVar3 = GameManager.instance.terrain.FUN_1B750((uint)screen.x, (uint)screen.z);
                                iVar4 = GameManager.instance.FUN_1DD9C();
                                GameManager.instance.FUN_1E188(iVar4, GameManager.instance.DAT_C2C, 66);
                                GameManager.instance.FUN_1E30C(iVar4, 0xe3d);
                                iVar4 = GameManager.instance.FUN_1DD9C();
                                GameManager.instance.FUN_1E188(iVar4, GameManager.instance.DAT_C2C, 63);
                                GameManager.instance.FUN_1E30C(iVar4, 0xe3d);
                                //FUN_4E338
                                GameObject obj = new GameObject();
                                ppcVar5 = obj.AddComponent<Ant3>();
                                ppcVar5.physics1.Y = 0x150000;
                                ppcVar5.physics1.Z = 0x10ccc;
                                GameManager.instance.FUN_30CB0(ppcVar5, 0);
                                screen.y = iVar3;
                                iVar15 = 0;

                                do
                                {
                                    iVar15++;
                                    GameManager.FUN_2AC5C();
                                    puVar6 = LevelManager.instance.xobfList[19].ini.FUN_2C17C(150, typeof(Ballistic), 8) as Ballistic;
                                    puVar6.type = 7;
                                    puVar6.flags = 0x434;
                                    iVar7 = (int)GameManager.FUN_2AC5C();
                                    puVar6.screen.x = screen.x + (iVar7 * 0xc8000 >> 15) - 0x64000;
                                    iVar7 = (int)GameManager.FUN_2AC5C();
                                    puVar6.screen.y = (screen.y - (iVar7 * 0x5000 >> 15)) - 0xf000;
                                    iVar7 = (int)GameManager.FUN_2AC5C();
                                    puVar6.screen.z = screen.z + (iVar7 * 0xc8000 >> 15) - 0x64000;
                                    sVar2 = (short)GameManager.FUN_2AC5C();
                                    puVar6.vr.z = sVar2;
                                    puVar6.FUN_3066C();
                                    FUN_50F0(ref puVar6.vTransform.rotation, DAT_138);
                                } while (iVar15 < 12);

                                iVar15 = (int)GameManager.FUN_2AC5C();
                                iVar7 = (int)GameManager.FUN_2AC5C();

                                if (GameManager.instance.worldObjs != null)
                                {
                                    ppiVar13 = GameManager.instance.worldObjs;

                                    for (int i = 0; i < ppiVar13.Count; i++)
                                    {
                                        ppiVar10 = ppiVar13[i];
                                        piVar9 = ppiVar10.vObject;

                                        if (piVar9.type == 2 && piVar9.maxHalfHealth != 0)
                                        {
                                            local_20 = new Vector3Int();
                                            local_20.x = piVar9.vTransform.position.x + (iVar15 * 0x2800 >> 15) - 0x1400;
                                            local_20.y = piVar9.vTransform.position.y;
                                            local_20.z = piVar9.vTransform.position.z + (iVar7 * 0x2800 >> 15) - 0x1400;
                                            piVar9.FUN_2B370(DAT_148, local_20);
                                        }
                                    }
                                }

                                ppcVar14 = vData.ini.FUN_2C17C(21, typeof(Ant4), 8, typeof(VigChild)) as Ant4;
                                ppcVar14.type = 8;
                                sVar2 = id;
                                ppcVar14.flags = 0x104;
                                ppcVar14.id = sVar2;
                                ppcVar14.screen = screen;
                                ppcVar14.maxHalfHealth = 4;
                                ppcVar14.FUN_3066C();
                                screen = vTransform.position;
                                vr = new Vector3Int(0, 0, 0);
                                ApplyTransformation();
                                tags = 1;
                            }
                        }
                    }
                    else
                    {
                        if (arg1 != 8)
                            return 0;

                        bVar15 = FUN_32B90((uint)arg2);

                        if (bVar15)
                            FUN_2B5C();
                    }
                }

                break;
        }

        return 0;
    }

    private void FUN_2B5C()
    {
        sbyte sVar1;
        int iVar2;
        VigObject pcVar2;
        Ant ppcVar3;
        VigObject pcVar4;

        iVar2 = GameManager.instance.terrain.FUN_1B750((uint)screen.x, (uint)screen.z);
        ppcVar3 = vData.ini.FUN_2C17C_2(16, typeof(Ant), 8) as Ant;
        Utilities.ParentChildren(ppcVar3, ppcVar3);
        ppcVar3.flags = 0x184;
        pcVar4 = ppcVar3.child2;
        ppcVar3.physics2.Y = screen.x;
        ppcVar3.physics2.Z = iVar2;
        ppcVar3.physics2.W = screen.z;
        ppcVar3.screen.x = screen.x;
        ppcVar3.screen.y = iVar2;
        ppcVar3.screen.z = screen.z;
        ppcVar3.FUN_3066C();
        pcVar4.vTransform.rotation.V22 = 0x800;
        pcVar4.vTransform.rotation.V11 = 0x800;
        pcVar4.vTransform.rotation.V00 = 0x800;
        ppcVar3.physics1.X = 0x8000;
        ppcVar3.physics1.Y = 0x12;
        pcVar2 = ppcVar3.child2.child2.child2;

        while (pcVar2 != null && pcVar2.id != 1)
            pcVar2 = pcVar2.child;

        ppcVar3.DAT_8C = pcVar2;
        ((Body)pcVar2.child2).state = _BODY_TYPE.Ant;
        sVar1 = (sbyte)GameManager.instance.FUN_1DD9C();
        ppcVar3.DAT_18 = sVar1;
        GameManager.instance.FUN_1E580(sVar1, ppcVar3.vData.sndList, 0, ppcVar3.vTransform.position, true);
    }

    //FUN_50F0 (ROUTE66.DLL)
    public static void FUN_50F0(ref Matrix3x3 param1, Vector3Int param2)
    {
        int iVar1;
        int iVar2;
        int iVar3;

        iVar1 = param2.x;
        iVar2 = param2.y;
        iVar3 = param2.z;

        param1.V00 = (short)(param1.V00 * iVar1 >> 12);
        param1.V01 = (short)(param1.V01 * iVar2 >> 12);
        param1.V02 = (short)(param1.V02 * iVar3 >> 12);
        param1.V10 = (short)(param1.V10 * iVar1 >> 12);
        param1.V11 = (short)(param1.V11 * iVar2 >> 12);
        param1.V12 = (short)(param1.V12 * iVar3 >> 12);
        param1.V20 = (short)(param1.V20 * iVar1 >> 12);
        param1.V21 = (short)(param1.V21 * iVar2 >> 12);
        param1.V22 = (short)(param1.V22 * iVar3 >> 12);
    }
}
