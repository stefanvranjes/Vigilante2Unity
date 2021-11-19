using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freezer2 : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public VigTransform DAT_84_2; //0x80
    public int DAT_A4; //0xA4
    public Vector3Int DAT_A8; //0xA8
    public int DAT_B4; //0xB4
    public VigObject DAT_B8; //0xB8
    public int DAT_BC; //0xBC
    public List<VigTuple> DAT_C0; //0xC0

    public override uint OnCollision(HitDetection hit)
    {
        uint uVar4;

        uVar4 = 0;

        if (hit.self.type != 3)
        {
            FUN_5F4();
            uVar4 = 0xffffffff;
        }

        return uVar4;
    }

    //FUN_760 (STINGER.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        byte bVar1;
        sbyte sVar2;
        uint uVar4;
        uint uVar5;
        Color32 cVar5;
        uint uVar6;
        uint uVar7;
        int iVar8;
        Particle1 pVar8;
        VigObject oVar9;
        VigObject oVar10;
        Vector3Int local_48;
        VigTransform local_28;

        switch (arg1)
        {
            case 0:
                bVar1 = DAT_19;

                if (bVar1 == 1)
                {
                    vTransform.position.x += DAT_A8.x;
                    vTransform.position.y += DAT_A8.y;
                    vTransform.position.z += DAT_A8.z;
                    iVar8 = DAT_B4 - 1;
                    DAT_B4 = iVar8;

                    if (0 < iVar8)
                    {
                        iVar8 = GameManager.instance.terrain.FUN_1B750((uint)vTransform.position.x, (uint)vTransform.position.z);

                        if (vTransform.position.y <= iVar8)
                            return 0;
                    }
                    
                    FUN_5F4();
                    return 0;
                }

                if (bVar1 < 2)
                {
                    if (bVar1 != 0)
                        return 0;

                    local_48 = new Vector3Int(DAT_A4, DAT_A4, DAT_A4);
                    Utilities.FUN_2449C(DAT_84_2.rotation, local_48, ref vTransform.rotation);
                    DAT_A4 += 122;
                }
                else
                {
                    if (3 < bVar1)
                        return 0;

                    FUN_570();

                    if (DAT_C0.Count == 0 && 2 < DAT_19)
                    {
                        UIManager.instance.FUN_4E338(new Color32(0x80, 0x80, 0x80, 4));
                        GameManager.instance.FUN_309A0(this);
                        return 0;
                    }

                    if (arg2 != 0)
                    {
                        uVar5 = ((Color32)UIManager.instance.flash.color).b + 0x40U;
                        uVar7 = 0x80;

                        if (uVar5 < 0x80)
                            uVar7 = uVar5;

                        uVar6 = ((Color32)UIManager.instance.flash.color).g + 0x40U;
                        uVar5 = 0x80;

                        if (uVar6 < 0x80)
                            uVar5 = uVar6;

                        cVar5 = new Color32();
                        cVar5.b = (byte)uVar7;
                        cVar5.g = (byte)uVar5;
                        uVar7 = ((Color32)UIManager.instance.flash.color).r + 0x40U;
                        cVar5.r = 0x80;

                        if (uVar7 < 0x80)
                            cVar5.r = (byte)uVar7;

                        cVar5.a = 0xff;
                        UIManager.instance.flash.color = cVar5;
                    }
                }
                goto default;
            default:
                uVar4 = 0;
                break;
            case 2:
                sVar2 = (sbyte)(DAT_19 + 1);
                DAT_19 = (byte)sVar2;
                uVar4 = 0;

                if (sVar2 == 1)
                {
                    oVar9 = DAT_80;
                    local_48 = new Vector3Int(0, 0, 0);
                    local_48.z = 0x1000;
                    oVar10 = DAT_B8;
                    local_28 = GameManager.instance.FUN_2CDF4(this);
                    local_48 = Utilities.ApplyMatrixSV(local_28.rotation, local_48);
                    DAT_B4 = 120;
                    iVar8 = oVar9.physics1.X;

                    if (iVar8 < 0)
                        iVar8 += 127;

                    DAT_A8.x = (iVar8 >> 7) + local_48.x * 4;
                    iVar8 = oVar9.physics1.Y;

                    if (iVar8 < 0)
                        iVar8 += 127;

                    DAT_A8.y = (iVar8 >> 7) + local_48.y * 4;
                    iVar8 = oVar9.physics1.Z;

                    if (iVar8 < 0)
                        iVar8 += 127;

                    DAT_A8.z = (iVar8 >> 7) + local_48.z * 4;
                    FUN_2CCBC();
                    transform.parent = null;
                    vTransform = local_28;
                    GameManager.instance.FUN_30080(GameManager.instance.worldObjs, this);
                    UIManager.instance.FUN_4E414(vTransform.position, new Color32(0x00, 0x40, 0x00, 8));
                    pVar8 = LevelManager.instance.FUN_4DE54(vTransform.position, 138);

                    if (pVar8 != null)
                        pVar8.id = id;

                    uVar4 = 0;

                    if (oVar10.maxHalfHealth == 0)
                    {
                        oVar10.FUN_3A368();
                        uVar4 = 0;
                    }
                }

                break;
            case 4:
                if (DAT_18 != 0)
                {
                    GameManager.instance.FUN_1DE78(DAT_18);
                    DAT_18 = 0;
                }

                GameManager.instance.FUN_300B8(GameManager.instance.DAT_1068, this);
                uVar4 = 0;
                break;
        }

        return uVar4;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        uint uVar4;

        switch (arg1)
        {
            default:
                uVar4 = 0;
                break;
            case 1:
                type = 3;
                goto default;
            case 10:
                GameManager.instance.FUN_300B8(DAT_C0, arg2);
                uVar4 = 0;
                break;
        }

        return uVar4;
    }

    //FUN_5F4 (STINGER.DLL)
    private void FUN_5F4()
    {
        short sVar1;
        sbyte sVar2;
        int iVar3;
        Particle1 pVar4;
        Ballistic puVar5;
        VigChild puVar6;
        Dictionary<int, Type> dict = new Dictionary<int, Type>();

        iVar3 = GameManager.instance.FUN_1DD9C();
        GameManager.instance.FUN_1E628(iVar3, DAT_80.vData.sndList, 5, vTransform.position);
        sVar2 = (sbyte)GameManager.instance.FUN_1DD9C();
        DAT_18 = sVar2;
        GameManager.instance.FUN_1E14C(sVar2, DAT_80.vData.sndList, 3, true);
        pVar4 = LevelManager.instance.FUN_4DE54(vTransform.position, 140);

        if (pVar4 != null)
            pVar4.id = id;

        dict.Add(79, typeof(VigChild));
        dict.Add(81, typeof(FreezerChild));
        dict.Add(95, typeof(VigChild));
        puVar5 = DAT_80.vData.ini.FUN_2C17C(2, typeof(Ballistic), 8, dict) as Ballistic;

        if (puVar5 != null)
        {
            sVar1 = id;
            puVar5.type = 7;
            puVar5.flags = 0x24;
            puVar5.id = sVar1;
            puVar5.screen = vTransform.position;
            puVar5.FUN_3066C();
        }

        Utilities.ParentChildren(puVar5, puVar5);
        DAT_19 = 2;
        flags |= 0x22;
        GameManager.instance.FUN_30CB0(this, 90);
        FUN_47C();
        GameManager.instance.FUN_30080(GameManager.instance.DAT_1068, this);
        UIManager.instance.FUN_4E414(vTransform.position, new Color32(0x80, 0x80, 0x80, 8));
    }

    //FUN_570 (STINGER.DLL)
    private void FUN_570()
    {
        int iVar1;
        VigObject oVar1;
        List<VigTuple> piVar2;
        VigTuple piVar3;

        iVar1 = DAT_BC - 1;
        DAT_BC = iVar1;

        if (iVar1 < 1)
        {
            piVar2 = DAT_C0;

            if (DAT_C0 != null)
            {
                for (int i = 0; i < piVar2.Count; i++)
                {
                    piVar3 = piVar2[i];
                    oVar1 = piVar3.vObject;
                    FUN_20C(oVar1, false);
                    GameManager.instance.FUN_300B8(DAT_C0, oVar1);
                }
            }
        }
    }

    //FUN_47C (STINGER.DLL)
    private void FUN_47C()
    {
        BSP puVar1;
        int ppuVar2;
        int iVar3;
        int iVar4;
        BSP[] local_404;

        local_404 = new BSP[255];
        iVar4 = vTransform.position.x;
        iVar3 = vTransform.position.z;
        DAT_C0 = new List<VigTuple>();
        DAT_BC = 240;
        FUN_2E8(GameManager.instance.worldObjs, iVar4, iVar3);
        puVar1 = GameManager.instance.bspTree;
        ppuVar2 = 0;

        do
        {
            if (puVar1.DAT_00 == 0)
                FUN_2E8(puVar1.LDAT_04, iVar4, iVar3);
            else
            {
                if (puVar1.DAT_00 < 4)
                {
                    local_404[ppuVar2] = puVar1.DAT_08;
                    local_404[ppuVar2 + 1] = puVar1.DAT_0C;
                    ppuVar2 += 2;
                }
            }

            ppuVar2--;

            if (ppuVar2 > 0)
                puVar1 = local_404[ppuVar2];
        } while (ppuVar2 >= 0);
    }

    //FUN_2E8 (STINGER.DLL)
    private void FUN_2E8(List<VigTuple> param1, int param2, int param3)
    {
        ushort uVar1;
        VigTuple piVar2;
        int iVar3;
        int iVar4;
        int iVar5;
        int iVar6;
        VigObject oVar7;
        Vehicle vVar7;

        iVar5 = 240;
        iVar6 = iVar5;

        for (int i = 0; i < param1.Count; i++)
        {
            iVar6 = iVar5;
            piVar2 = param1[i];
            oVar7 = piVar2.vObject;
            iVar5 = iVar6;

            if (oVar7.id != id && (oVar7.type != 2 || ((Vehicle)oVar7).shield == 0))
            {
                iVar3 = param2 - oVar7.vTransform.position.x >> 16;
                iVar4 = param3 - oVar7.vTransform.position.z >> 16;
                iVar3 = iVar3 * iVar3 + iVar4 * iVar4;

                if (iVar3 < 100)
                {
                    GameManager.instance.FUN_30080(DAT_C0, oVar7);
                    FUN_A0(oVar7, iVar6, false);
                }

                if (oVar7.type == 2)
                {
                    iVar5 = (int)Utilities.SquareRoot(iVar3);
                    iVar5 = (10 - iVar5) * 24 + 240;

                    if (iVar5 < iVar6)
                        iVar5 = iVar6;
                }
                else
                {
                    if (oVar7.type == 8)
                    {
                        uVar1 = oVar7.maxHalfHealth;
                        oVar7.maxHalfHealth = 0;
                        oVar7.maxFullHealth = uVar1;
                    }
                }
            }
        }

        if (iVar6 < DAT_BC)
            iVar6 = DAT_BC;

        DAT_BC = iVar6;
    }

    //FUN_20C (STINGER.DLL)
    private static void FUN_20C(VigObject param1, bool param2)
    {
        ushort uVar1;
        bool bVar2;
        bool bVar3;

        if (param1.type == 8)
        {
            uVar1 = param1.maxFullHealth;
            param1.maxFullHealth = 0;
            param1.maxHalfHealth = uVar1;
        }

        if (param1 != null)
        {
            do
            {
                bVar3 = false;

                if ((param1.flags & 0x80) != 0)
                    bVar3 = FUN_1B0(GameManager.instance.DAT_1088, param1);

                if ((param1.flags & 4) != 0)
                {
                    bVar2 = FUN_1B0(GameManager.instance.DAT_10A8, param1);
                    bVar3 = bVar3 | bVar2;
                }

                if (!bVar3 && param1.child2 != null)
                    FUN_20C(param1.child2, true);

                param1 = param1.child;
            } while (param2 && param1 != null);
        }
    }

    //FUN_1B0 (STINGER.DLL)
    private static bool FUN_1B0(List<VigTuple> param1, VigObject param2)
    {
        for (int i = 0; i < param1.Count; i++)
            if (param1[i].vObject == param2) return true;

        GameManager.instance.FUN_30080(param1, param2);
        return false;
    }

    //FUN_A0 (STINGER.DLL)
    private static void FUN_A0(VigObject param1, int param2, bool param3)
    {
        VigTuple tVar1;
        bool bVar1;

        if (param1 != null)
        {
            do
            {
                if ((param1.flags & 0x80) != 0)
                    GameManager.instance.FUN_300B8(GameManager.instance.DAT_1088, param1);

                param1.DAT_4A += (ushort)param2;

                if ((param1.flags & 4) != 0)
                {
                    bVar1 = GameManager.instance.FUN_300B8(GameManager.instance.DAT_10A8, param1);

                    if (!bVar1)
                        param1.flags &= 0xfffffffb;
                }

                if ((param1.flags & 1) != 0)
                {
                    tVar1 = GameManager.instance.FUN_30134(GameManager.instance.DAT_1110, param1);

                    if (tVar1 != null)
                        GameManager.instance.FUN_30CB0(param1, (int)tVar1.flag + param2 - GameManager.instance.DAT_28);
                }

                if (param1.child2 != null)
                    FUN_A0(param1.child2, param2, true);

                param1 = param1.child;
            } while (param3 && param1 != null);
        }
    }
}
