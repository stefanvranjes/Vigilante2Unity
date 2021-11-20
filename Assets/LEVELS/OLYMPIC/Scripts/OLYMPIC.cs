using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OLYMPIC : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public List<VigTuple> DAT_80_2;
    public List<VigTuple> DAT_8C;
    public List<VigTuple> DAT_98;
    public List<VigTuple> DAT_A4;
    public VigObject[] DAT_B0;
    public VigObject[] DAT_B8;
    public ushort DAT_D0;
    public ushort DAT_D2;
    public int DAT_D8;
    public int DAT_DC;
    public int DAT_E0;

    public List<Matrix4x4> pole1M;
    public List<Matrix4x4> pole2M;
    public Mesh pole1;
    public Mesh pole2;
    private Vector3[] newVertices;
    private Vector2[] newUVs;
    private Color32[] newColors;
    private int[] newIndicies;

    private void Awake()
    {
        DAT_80_2 = new List<VigTuple>();
        DAT_8C = new List<VigTuple>();
        newVertices = new Vector3[40];
        newUVs = new Vector2[40];
        newColors = new Color32[40];
        newIndicies = new int[40];
        pole1M = new List<Matrix4x4>();
        pole2M = new List<Matrix4x4>();
        GameObject obj = new GameObject();
        pole1 = new Mesh();
        obj.AddComponent<MeshFilter>().mesh = pole1;
        obj.AddComponent<MeshRenderer>().sharedMaterial = LevelManager.instance.defaultMaterial;
        obj = new GameObject();
        pole2 = new Mesh();
        obj.AddComponent<MeshFilter>().mesh = pole2;
        obj.AddComponent<MeshRenderer>().sharedMaterial = LevelManager.instance.defaultMaterial;
    }

    //FUN_1568 (OLYMPIC.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        VigObject puVar1;
        sbyte sVar2;
        ushort uVar3;
        Gondola ppcVar4;
        DragStick ppcVar5;
        int iVar5;
        VigObject oVar5;
        VigObject oVar6;
        VigMesh mVar6;
        int iVar7;
        VigObject oVar7;
        uint uVar7;
        short sVar8;
        uint uVar9;
        int iVar9;

        switch (arg1)
        {
            case 0:
                sVar8 = (short)DAT_D2;

                if (sVar8 == 0)
                {
                    uVar3 = (ushort)(DAT_D0 + 32);
                    DAT_D0 = uVar3;

                    if ((uVar3 & 0x7fff) == 0)
                    {
                        iVar9 = 0;

                        do
                        {
                            oVar5 = DAT_B0[iVar9];
                            uVar3 = (ushort)(oVar5.DAT_1A | 4);
                            oVar5.flags |= 0x20;
                            oVar5.DAT_1A = (short)uVar3;

                            if (uVar3 != 0)
                            {
                                GameManager.instance.FUN_1FEB8(oVar5.vMesh);
                                oVar5.DAT_1A = 4;
                                mVar6 = oVar5.vData.FUN_2CB74(oVar5.gameObject, 4, true);
                                oVar5.vMesh = mVar6;
                            }

                            iVar9++;
                        } while (iVar9 < 2);

                        DAT_D2 = 0x4b0;
                        arg2 = 1;
                        GameManager.instance.FUN_1DE78(DAT_18);
                        DAT_18 = 0;
                    }

                    if (arg2 == 0)
                        return 0;

                    FUN_974(DAT_B0[0], DAT_80_2, DAT_D0, true);
                    FUN_974(DAT_B0[1], DAT_80_2, (ushort)(((DAT_D0 + 0x8000) * 0x10000) >> 16), true);
                    uVar7 = GameManager.instance.FUN_1E478(DAT_B0[0].screen);
                    uVar9 = GameManager.instance.FUN_1E478(DAT_B0[1].screen);
                    GameManager.instance.FUN_1E2C8(DAT_18, uVar7 + uVar9);
                }
                else
                {
                    DAT_D2 = (ushort)(sVar8 - 1);

                    if (sVar8 == 1)
                    {
                        sVar2 = (sbyte)GameManager.instance.FUN_1DD9C();
                        DAT_18 = sVar2;
                        GameManager.instance.FUN_1E098(sVar2, DAT_B0[0].vData.sndList, 4, 0, true);
                        DAT_B0[0].flags &= 0xffffffdf;
                        DAT_B0[1].flags &= 0xffffffdf;
                    }
                }

                iVar7 = 0;

                if (arg2 != 0)
                {
                    iVar5 = GameManager.instance.DAT_28 << 5;

                    do
                    {
                        puVar1 = DAT_B8[iVar7];
                        sVar8 = (short)iVar5;
                        iVar7++;
                        iVar5 += 0x2aaa;
                        FUN_974(puVar1, DAT_8C, (ushort)sVar8, false);
                    } while (iVar7 < 6);

                    return 0;
                }

                break;

            case 1:
                GameManager.instance.offsetFactor = 2.5f;
                GameManager.instance.offsetStart = 0;
                GameManager.instance.angleOffset = 0.2f;
                GameManager.instance.DAT_1000 |= 1;
                FUN_D64(DAT_80_2);
                iVar9 = 0;
                DAT_B0 = new VigObject[2];

                do
                {
                    ppcVar4 = Utilities.FUN_31D30(typeof(Gondola), LevelManager.instance.xobfList[42], 4, 0) as Gondola;
                    DAT_B0[iVar9] = ppcVar4;

                    if (iVar9 == 0)
                        oVar5 = DAT_80_2[0].vObject;
                    else
                        oVar5 = DAT_80_2[DAT_80_2.Count - 1].vObject;

                    ppcVar4.vr.y = oVar5.vr.y;
                    ppcVar4.FUN_3066C();
                    FUN_974(ppcVar4, DAT_80_2, (ushort)((iVar9 << 31) >> 16), true);
                    iVar9++;
                } while (iVar9 < 2);

                DAT_D2 = 0x4b0;
                FUN_D64(DAT_8C);
                iVar9 = 0;
                DAT_B8 = new VigObject[6];

                do
                {
                    ppcVar5 = Utilities.FUN_31D30(typeof(DragStick), LevelManager.instance.xobfList[42], 8, 0) as DragStick;
                    Utilities.ParentChildren(ppcVar5, ppcVar5);
                    DAT_B8[iVar9] = ppcVar5;
                    DAT_B8[iVar9].FUN_3066C();
                    iVar5 = iVar9 << 16;
                    puVar1 = DAT_B8[iVar9];
                    iVar9++;
                    FUN_974(puVar1, DAT_8C, (ushort)((iVar5 / 6) * 0x10000 >> 16), false);
                } while (iVar9 < 6);

                flags |= 0x80;
                DAT_98 = new List<VigTuple>();
                DAT_A4 = new List<VigTuple>();
                DAT_D8 = 0x708;
                DAT_E0 = 0xe10;
                DAT_DC = 0x20000;
                oVar6 = GameManager.instance.FUN_30250(GameManager.instance.DAT_1078, 0x100);
                oVar7 = GameManager.instance.FUN_4AC1C(0xfe000000, oVar6);
                GameManager.instance.DAT_1038 = oVar7 != null ? 1 : 0;
                goto case 2;
            case 2:
                GameManager.instance.FUN_34B34();
                GameManager.instance.FUN_30CB0(this, 240);
                return 0;
            case 4:
                FUN_1D0(DAT_98);
                FUN_1D0(DAT_A4);
                GameManager.instance.FUN_3001C(DAT_80_2);
                GameManager.instance.FUN_3001C(DAT_8C);
                break;
            case 17:
                FUN_558(DAT_80_2, pole1M, pole1);
                FUN_558(DAT_8C, pole2M, pole2);
                return 0;
        }

        return 0;
    }

    public override uint UpdateW(VigObject arg1, int arg2, Vector3Int arg3)
    {
        VigObject oVar7;
        Vector3Int local_48;
        Vector3Int local_38;
        Vector3Int auStack64;

        switch (arg2)
        {
            case 10:
                oVar7 = Utilities.FUN_2CD78(arg1);

                if (oVar7 == null)
                    oVar7 = arg1;

                local_38 = new Vector3Int(0, 0, 0);
                local_38.y = -0x1000;
                local_48 = GameManager.instance.terrain.FUN_1B998((uint)arg3.x, (uint)arg3.z);

                if (local_48.x == 0 && local_48.z == 0)
                    return 0;

                local_48 = Utilities.VectorNormal(local_48);
                auStack64 = Utilities.FUN_2A1E0(local_48, local_38);
                local_38 = Utilities.FUN_2A1E0(local_48, auStack64);
                local_38 = Utilities.VectorNormal(local_38);
                oVar7.physics1.X += local_38.x * 2;
                oVar7.physics1.Y += local_38.y * 2;
                oVar7.physics1.Z += local_38.z * 2;
                break;
        }

        return 0;
    }

    public override uint UpdateW(VigObject arg1, int arg2, int arg3)
    {
        switch (arg2)
        {
            case 18:
                GameManager.instance.FUN_327CC(arg1);
                return 0;
        }

        return 0;
    }

    //FUN_1D0 (OLYMPIC.DLL)
    private static void FUN_1D0(List<VigTuple> param1)
    {
        List<VigTuple> ppiVar1;
        VigTuple ppiVar2;

        ppiVar1 = param1;

        if (ppiVar1 != null)
        {
            for (int i = 0; i < ppiVar1.Count; i++)
            {
                ppiVar2 = ppiVar1[i];
                Destroy(ppiVar2.vObject.gameObject);
                ppiVar2.vObject = null;
            }
        }

        GameManager.instance.FUN_3001C(param1);
    }

    //private void FUN_244()

    private void FUN_558(List<VigTuple> param1, List<Matrix4x4> param2, Mesh param3)
    {
        VigTuple ppiVar2;
        int iVar3;
        ConfigContainer ccVar3;
        ConfigContainer ccVar4;
        uint uVar8;
        VigObject oVar9;
        int puVar11;
        VigTransform auStack56;
        Vector3Int local_78;
        Vector3Int local_68;
        Vector3Int local_58;
        Vector3Int local_48;
        Vector3 vert1, vert2, vert3, vert4;

        int tFactor = GameManager.instance.translateFactor2;
        puVar11 = 0;
        local_68 = new Vector3Int(); //not in the original code
        local_78 = new Vector3Int(); // - || -
        vert3 = new Vector3();
        vert4 = new Vector3();

        for (int i = 0; i < param1.Count; i++)
        {
            ppiVar2 = param1[i];
            oVar9 = ppiVar2.vObject;
            iVar3 = oVar9.vr.y;

            if (iVar3 < 0)
                iVar3 = -iVar3;

            uVar8 = 0x8000;

            if (0x400 < iVar3)
                uVar8 = 0x8001;

            ccVar4 = oVar9.FUN_2C5F4((ushort)uVar8);
            uVar8 = 0x8000;
            iVar3 = oVar9.vr.y;

            if (iVar3 < 0)
                iVar3 = -iVar3;

            if (iVar3 < 0x401)
                uVar8 = 0x8001;

            ccVar3 = oVar9.FUN_2C5F4((ushort)uVar8);
            auStack56 = Utilities.CompMatrixLV(GameManager.instance.DAT_F00, oVar9.vTransform);
            Utilities.SetRotMatrix(auStack56.rotation);
            Coprocessor.translationVector._trx = auStack56.position.x >> 8;
            Coprocessor.translationVector._try = auStack56.position.y >> 8;
            Coprocessor.translationVector._trz = auStack56.position.z >> 8;
            Coprocessor.vector0.vx0 = (short)(ccVar4.v3_1.x >> 8);
            Coprocessor.vector0.vy0 = (short)(ccVar4.v3_1.y >> 8);
            Coprocessor.vector0.vz0 = (short)(ccVar4.v3_1.z >> 8);
            vert1 = new Vector3(Coprocessor.vector0.vx0, -Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor;
            vert1 = param2[i].MultiplyPoint3x4(vert1);
            Coprocessor.ExecuteRTPS(12, false);
            Coprocessor.vector0.vx0 = (short)(ccVar3.v3_1.x >> 8);
            Coprocessor.vector0.vy0 = (short)(ccVar3.v3_1.y >> 8);
            Coprocessor.vector0.vz0 = (short)(ccVar3.v3_1.z >> 8);
            vert2 = new Vector3(Coprocessor.vector0.vx0, -Coprocessor.vector0.vy0, Coprocessor.vector0.vz0) / tFactor;
            vert2 = param2[i].MultiplyPoint3x4(vert2);
            local_58 = new Vector3Int(Coprocessor.accumulator.ir1, Coprocessor.accumulator.ir2, Coprocessor.accumulator.ir3);
            Coprocessor.ExecuteRTPS(12, false);
            local_48 = new Vector3Int(Coprocessor.accumulator.ir1, Coprocessor.accumulator.ir2, Coprocessor.accumulator.ir3);

            if (ppiVar2 != param1[0])
            {
                newVertices[puVar11] = vert1;
                newVertices[puVar11 + 1] = vert3;
                newVertices[puVar11 + 2] = vert2;
                newVertices[puVar11 + 3] = vert4;
                newColors[puVar11] = new Color32(0, 0, 0, 255);
                newColors[puVar11 + 1] = new Color32(0, 0, 0, 255);
                newColors[puVar11 + 2] = new Color32(0, 0, 0, 255);
                newColors[puVar11 + 3] = new Color32(0, 0, 0, 255);
                newUVs[puVar11] = new Vector2(0, 0);
                newUVs[puVar11 + 1] = new Vector2(0, 0);
                newUVs[puVar11 + 2] = new Vector2(0, 0);
                newUVs[puVar11 + 3] = new Vector2(0, 0);
                newIndicies[puVar11] = puVar11;
                newIndicies[puVar11 + 1] = puVar11 + 1;
                newIndicies[puVar11 + 2] = puVar11 + 2;
                newIndicies[puVar11 + 3] = puVar11 + 3;
                puVar11 += 4;
            }

            local_68 = local_48;
            local_78 = local_58;
            vert3 = vert1;
            vert4 = vert2;
        }

        param3.SetVertices(newVertices, 0, puVar11);
        param3.SetColors(newColors, 0, puVar11);
        param3.SetUVs(0, newUVs, 0, puVar11);
        param3.SetIndices(newIndicies, 0, puVar11, MeshTopology.Lines, 0);
    }

    //FUN_974 (OLYMPIC.DLL)
    private static void FUN_974(VigObject param1, List<VigTuple> param2, ushort param3, bool param4)
    {
        VigTuple ppiVar1;
        VigTuple ppiVar2;
        short sVar3;
        uint uVar3;
        VigTuple piVar4;
        int iVar5;
        ConfigContainer ccVar5;
        uint uVar6;
        int iVar7;
        VigObject oVar8;
        ConfigContainer ccVar8;
        VigObject oVar9;
        int iVar10;
        VigObject oVar10;
        int iVar11;
        Vector3Int local_40;
        Vector3Int local_30;
        Vector3Int local_20;

        param1.DAT_4A = param3;

        if ((param3 & 0x7fff) < 0x7000)
        {
            sVar3 = (short)param3;

            if (sVar3 < 0)
                sVar3 = (short)(-sVar3 - 0x1000);

            ppiVar1 = param2[0];
            ppiVar2 = null;

            for (int i = 1; i < param2.Count; i++)
            {
                ppiVar1 = param2[i - 1];
                ppiVar2 = param2[i];

                if (ppiVar2.flag >= (uint)sVar3) break;
            }

            oVar8 = ppiVar1.vObject;
            oVar10 = ppiVar2.vObject;
            iVar5 = oVar8.vr.y;
            uVar3 = (((uint)sVar3 - ppiVar1.flag) * 0x100) / (ppiVar2.flag - ppiVar1.flag);

            if (iVar5 < 0)
                iVar5 = -iVar5;

            uVar6 = 0x8000;

            if (0x400 < iVar5)
                uVar6 = 0x8001;

            if (-1 < param3 << 16)
                uVar6 ^= 1;

            ccVar5 = oVar8.FUN_2C5F4((ushort)uVar6);
            local_40 = Utilities.FUN_24148(oVar8.vTransform, ccVar5.v3_1);
            iVar5 = oVar10.vr.y;

            if (iVar5 < 0)
                iVar5 = -iVar5;

            uVar6 = 0x8000;

            if (0x400 < iVar5)
                uVar6 = 0x8001;

            if (-1 < param3 << 16)
                uVar6 ^= 1;

            ccVar5 = oVar10.FUN_2C5F4((ushort)uVar6);
            local_30 = Utilities.FUN_24148(oVar10.vTransform, ccVar5.v3_1);
            iVar5 = (local_30.x - local_40.x) * (int)uVar3;

            if (iVar5 < 0)
                iVar5 += 255;

            param1.vTransform.position.x = local_40.x + (iVar5 >> 8);
            iVar5 = (local_30.y - local_40.y) * (int)uVar3;

            if (iVar5 < 0)
                iVar5 += 255;

            param1.vTransform.position.y = local_40.y + (iVar5 >> 8);
            iVar5 = (local_30.z - local_40.z) * (int)uVar3;

            if (iVar5 < 0)
                iVar5 += 255;

            param1.vTransform.position.z = local_40.z + (iVar5 >> 8);
        }
        else
        {
            if (param3 < 0xf000U)
                piVar4 = param2[param2.Count - 1];
            else
                piVar4 = param2[0];

            oVar9 = piVar4.vObject;
            ccVar5 = oVar9.FUN_2C5F4(0x8000);
            ccVar8 = oVar9.FUN_2C5F4(0x8001);
            iVar10 = (-(short)param3 / 2 & 0xfff) * 4;
            iVar7 = ccVar8.v3_1.x - ccVar5.v3_1.x;
            iVar11 = GameManager.DAT_65C90[iVar10 / 2 + 1] * iVar7;

            if (iVar11 < 0)
                iVar11 += 0x1fff;

            local_20 = new Vector3Int();
            local_20.x = (ccVar5.v3_1.x + ccVar8.v3_1.x) / 2 + (iVar11 >> 13);
            local_20.y = (ccVar5.v3_1.y + ccVar8.v3_1.y) / 2;
            iVar7 = GameManager.DAT_65C90[iVar10 / 2] * iVar7;

            if (iVar7 < 0)
                iVar7 += 0x1fff;

            local_20.z = (ccVar5.v3_1.z + ccVar8.v3_1.z) / 2 - (iVar7 >> 13);
            param1.vTransform.position = Utilities.FUN_24148(oVar9.vTransform, local_20);

            if (param4)
            {
                param1.vr.y = oVar9.vr.y + (-(short)param3 / 2);
                param1.ApplyRotationMatrix();
            }
        }
    }

    //FUN_CCC (OLYMPIC.DLL)
    public static void FUN_CCC(List<VigTuple> param1, List<Matrix4x4> param2, VigObject param3)
    {
        VigTuple ppiVar1;
        VigTuple ppiVar2;

        //ppiVar1 = GameManager.instance.FUN_335FC(param1, param2);
        ppiVar2 = null;
        int i = 0;

        for (; i < param1.Count; i++)
        {
            ppiVar2 = param1[i];

            if (param3.screen.z <= ppiVar2.vObject.screen.z) break;
        }

        Matrix4x4 localToWorld = param3.GetTransform().localToWorldMatrix;
        param2.Insert(i, localToWorld);
        param1.Insert(i, new VigTuple(param3, 0));
    }

    //FUN_D64 (OLYMPIC.DLL)
    private static void FUN_D64(List<VigTuple> param1)
    {
        VigTuple ppiVar1;
        VigTuple ppiVar3;
        int iVar4;
        int iVar2;

        ppiVar3 = param1[0];
        iVar4 = ppiVar3.vObject.screen.z;
        iVar2 = param1[param1.Count - 1].vObject.screen.z - iVar4;

        if (iVar2 < 0)
            iVar2 += 255;

        ppiVar3.flag = (uint)(((ppiVar3.vObject.screen.z - iVar4) * 0x70) / (iVar2 >> 8));

        for (int i = 1; i < param1.Count; i++)
        {
            ppiVar1 = param1[i];
            ppiVar3 = ppiVar1;
            ppiVar3.flag = (uint)(((ppiVar3.vObject.screen.z - iVar4) * 0x70) / (iVar2 >> 8));
        }
    }
}
