using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //0x20 (EXCELSR.DLL)
    public static Vector3Int DAT_20 = new Vector3Int(0, 0, 0);

    //FUN_8D0 (EXCELSR.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        if (arg1 == 0)
            FUN_42330(arg2);
        else
        {
            if (arg1 == 4)
                GameManager.instance.DAT_1084--;
        }

        return 0;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        short sVar1;
        short sVar2;
        short sVar3;
        sbyte sVar4;
        int iVar5;
        uint uVar5;
        int iVar6;
        ConfigContainer ccVar6;
        Lightning2 puVar7;
        int iVar8;
        Lightning3 puVar9;
        int iVar10;
        ConfigContainer ccVar10;
        VigObject oVar10;
        List<VigTuple> ppiVar11;
        int iVar12;
        VigObject oVar12;
        uint uVar13;
        uint uVar14;
        VigObject piVar16;
        VigTuple ppiVar18;
        VigObject piVar19;
        VigObject piVar20;
        Vector2Int v2Var21;
        Lightning2 puVar22;
        Vehicle vVar23;
        int local_28;
        Lightning2 local_24;
        Vector3Int local_20;
        Vector3Int local_40;
        Vector3Int local_a4;
        Vector3Int local_84;
        Vector3Int local_78;
        Vector3Int local_68;
        Vector3Int auStack72;
        Vector3Int auStack88;
        VigTransform auStack184;

        if (arg1 == 1)
        {
            type = 3;
            maxHalfHealth = 2;
            flags |= 0x4000;
            uVar5 = 0;
        }
        else
        {
            if (arg1 == 0)
            {
                FUN_42330(arg2);
                uVar5 = 0;
            }
            else
            {
                if (arg1 == 12)
                {
                    if (arg2.id < 0)
                        local_28 = GameManager.instance.DAT_1128[~arg2.id];
                    else
                        local_28 = -1;

                    piVar19 = null;
                    GameManager.instance.DAT_1084++;
                    iVar6 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E188(iVar6, vData.sndList, 2);
                    ccVar6 = FUN_2C5F4(0x8000);
                    GameManager.instance.FUN_2CF00(out local_a4, this, ccVar6);
                    ccVar6 = FUN_2C5F4(0x8001);
                    GameManager.instance.FUN_2CF00(out local_84, this, ccVar6);
                    local_78 = new Vector3Int();
                    local_78.x = local_a4.x / 2 + local_84.x / 2;
                    local_78.y = local_a4.y / 2 + local_84.y / 2;
                    local_78.z = local_a4.z / 2 + local_84.z / 2;
                    DAT_19 = 1;
                    maxHalfHealth--;
                    uVar5 = 0x7fff0000;

                    if (GameManager.instance.worldObjs.Count > 0)
                    {
                        ppiVar11 = GameManager.instance.worldObjs;
                        
                        for (int i = 0; i < ppiVar11.Count; i++)
                        {
                            ppiVar18 = ppiVar11[i];
                            piVar16 = ppiVar18.vObject;
                            piVar20 = piVar19;
                            uVar13 = uVar5;

                            if (piVar16 == arg2 || piVar16.type != 2 || 
                                (piVar16.flags & 0x4004000U) != 0x4000 || ((Vehicle)piVar16).shield != 0)
                            {
                                piVar19 = piVar20;
                                uVar5 = uVar13;
                            }
                            else
                            {
                                if (0 < piVar16.id || GameManager.instance.DAT_1128[~piVar16.id] != local_28)
                                {
                                    local_68 = new Vector3Int();
                                    local_68.x = piVar16.vTransform.position.x - local_78.x;
                                    local_68.y = piVar16.vTransform.position.y - local_78.y;
                                    local_68.z = piVar16.vTransform.position.z - local_78.z;
                                    v2Var21 = Utilities.FUN_2A1C0(local_68);
                                    uVar13 = (uint)v2Var21.x >> 16 | (uint)v2Var21.y << 16;
                                    piVar20 = piVar16;

                                    if ((int)uVar13 < (int)uVar5)
                                    {
                                        piVar19 = piVar20;
                                        uVar5 = uVar13;
                                    }
                                }
                            }
                        }
                    }

                    puVar7 = vData.ini.FUN_2C17C(1, typeof(Lightning2), 8) as Lightning2;

                    if (puVar7 != null)
                    {
                        arg2.tags = 3;

                        if (piVar19 == null)
                            local_68 = new Vector3Int(0, 0, 0);
                        else
                        {
                            local_68 = new Vector3Int();
                            local_68.x = piVar19.vTransform.position.x - local_78.x;
                            local_68.y = piVar19.vTransform.position.y - local_78.y;
                            local_68.z = piVar19.vTransform.position.z - local_78.z;
                        }

                        iVar10 = local_68.x;

                        if (local_68.x < 0)
                            iVar10 = local_68.x + 4095;

                        sVar1 = arg2.vTransform.rotation.V00;
                        iVar12 = local_68.y;

                        if (local_68.y < 0)
                            iVar12 = local_68.y + 4095;

                        sVar2 = arg2.vTransform.rotation.V10;
                        iVar8 = local_68.z;

                        if (local_68.z < 0)
                            iVar8 = local_68.z + 4095;

                        sVar3 = arg2.vTransform.rotation.V20;
                        iVar6 = 0x8000;
                        puVar7.DAT_84 = this;

                        if (0 < (iVar10 >> 12) * sVar1 + (iVar12 >> 12) * sVar2 + (iVar8 >> 12) * sVar3)
                            iVar6 = 0x8001;

                        local_24 = puVar7;
                        ccVar10 = FUN_2C5F4((ushort)iVar6);
                        puVar7.DAT_90 = ccVar10;
                        auStack184 = GameManager.instance.FUN_2CEAC(this, ccVar10);
                        auStack88 = Utilities.FUN_2426C(auStack184.rotation, 
                            new Matrix2x4(local_68.x, local_68.y, local_68.z, 0));
                        iVar12 = Utilities.FUN_29FC8(auStack88, out auStack72);
                        iVar5 = iVar12 / 2;

                        if (piVar19 == null || 0x70000 < iVar5)
                        {
                            iVar5 = 0x70000;
                            puVar7.DAT_80 = null;
                            puVar7.DAT_19 = 15;
                        }
                        else
                        {
                            puVar9 = vData.ini.FUN_2C17C(3, typeof(Lightning3), 8) as Lightning3;
                            puVar9.type = 3;
                            puVar9.flags |= 0x10;
                            puVar7.DAT_19 = 40;
                            puVar9.vTransform = GameManager.defaultTransform;
                            puVar9.PDAT_74 = piVar19;
                            puVar9.PDAT_78 = puVar7;
                            Utilities.FUN_2CC48(piVar19, puVar9);
                            Utilities.ParentChildren(piVar19, piVar19);
                            puVar7.DAT_8C = puVar9;
                            puVar9.FUN_30BF0();
                            puVar7.DAT_80 = piVar19;
                            piVar19.flags |= 0x4000000;
                            piVar19.FUN_30BA8();
                            iVar6 = ((Vehicle)piVar19).FUN_3B078(arg2, (ushort)DAT_1A, -100, 1);
                            ((Vehicle)piVar19).FUN_3A020(iVar6, DAT_20, true);
                        }

                        oVar12 = vData.ini.FUN_2C17C(2, typeof(VigObject), 8);

                        if (oVar12 != null)
                        {
                            oVar12.type = 3;
                            oVar12.flags |= 0x10;
                            oVar12.vTransform = GameManager.defaultTransform;
                            Utilities.FUN_2CC48(puVar7, oVar12);
                            Utilities.ParentChildren(puVar7, puVar7);
                            oVar12.FUN_30BF0();
                        }

                        puVar7.vTransform.rotation = Utilities.FUN_2A724(auStack72);
                        iVar12 = iVar5 * puVar7.vTransform.rotation.V02;

                        if (iVar12 < 0)
                            iVar12 += 0xffff;

                        iVar8 = iVar5 * puVar7.vTransform.rotation.V12;
                        puVar7.vTransform.rotation.V02 = (short)(iVar12 >> 16);

                        if (iVar8 < 0)
                            iVar8 += 0xffff;

                        iVar12 = iVar5 * puVar7.vTransform.rotation.V22;
                        puVar7.vTransform.rotation.V12 = (short)(iVar8 >> 16);

                        if (iVar12 < 0)
                            iVar12 += 0xffff;

                        puVar7.vTransform.rotation.V22 = (short)(iVar12 >> 16);
                        puVar7.vTransform.position = ccVar10.v3_1;
                        puVar7.flags = 0x20;
                        puVar7.state = _LIGHTNING2_TYPE.Type1;
                        Utilities.FUN_2CC48(this, puVar7);
                        Utilities.ParentChildren(this, this);
                        puVar7.FUN_30B78();
                        puVar7.FUN_30BF0();
                        sVar4 = (sbyte)GameManager.instance.FUN_1DD9C();
                        puVar7.DAT_18 = sVar4;
                        GameManager.instance.FUN_1E580(sVar4, puVar7.vData.sndList, 3, local_a4, true);
                        GameManager.instance.FUN_30CB0(puVar7, 180);
                        oVar10 = puVar7.DAT_80;

                        while(oVar10 != null)
                        {
                            uVar13 = 0xc40000;
                            piVar19 = null;

                            if (GameManager.instance.worldObjs != null)
                            {
                                ppiVar11 = GameManager.instance.worldObjs;

                                for (int i = 0; i < ppiVar11.Count; i++)
                                {
                                    ppiVar18 = ppiVar11[i];
                                    piVar16 = ppiVar18.vObject;
                                    piVar20 = piVar19;
                                    uVar14 = uVar13;

                                    if (piVar16 == arg2 || piVar16.type != 2 || (piVar16.flags & 0x4004000U) != 0x4000)
                                    {
                                        piVar19 = piVar20;
                                        uVar13 = uVar14;
                                    }
                                    else
                                    {
                                        if (((Vehicle)piVar16).shield == 0 && 
                                            (0 < piVar16.id || GameManager.instance.DAT_1128[~piVar16.id] != local_28))
                                        {
                                            oVar10 = puVar7.DAT_80;
                                            local_40 = new Vector3Int();
                                            local_40.x = piVar16.vTransform.position.x - oVar10.vTransform.position.x;
                                            local_40.y = piVar16.vTransform.position.y - oVar10.vTransform.position.y;
                                            local_40.z = piVar16.vTransform.position.z - oVar10.vTransform.position.z;
                                            v2Var21 = Utilities.FUN_2A1C0(local_40);
                                            uVar14 = (uint)v2Var21.x >> 16 | (uint)v2Var21.y << 16;
                                            piVar20 = piVar16;
                                            uVar5 = uVar14;

                                            if ((int)uVar14 < (int)uVar13)
                                            {
                                                piVar19 = piVar20;
                                                uVar13 = uVar14;
                                            }
                                        }
                                    }
                                }
                            }

                            if (piVar19 == null) break;

                            puVar22 = vData.ini.FUN_2C17C(1, typeof(Lightning2), 8) as Lightning2;

                            if (puVar22 == null) break;

                            oVar10 = puVar7.DAT_80;
                            local_40 = new Vector3Int();
                            local_40.x = piVar19.vTransform.position.x - oVar10.vTransform.position.x;
                            local_40.y = piVar19.vTransform.position.y - oVar10.vTransform.position.y;
                            local_40.z = piVar19.vTransform.position.z - oVar10.vTransform.position.z;
                            oVar10 = puVar7.DAT_80;
                            puVar7.DAT_88 = puVar22;
                            puVar22.vTransform.position = oVar10.vTransform.position;
                            puVar22.DAT_19 = 40;
                            Utilities.FUN_29FC8(local_40, out local_20);
                            puVar9 = vData.ini.FUN_2C17C(3, typeof(Lightning3), 8) as Lightning3;
                            puVar9.type = 3;
                            puVar9.flags |= 0x10;
                            puVar9.vTransform = GameManager.FUN_2A39C();
                            puVar9.vTransform.position = new Vector3Int(0, 0, 0);
                            puVar9.PDAT_74 = piVar19;
                            puVar9.PDAT_78 = local_24;
                            Utilities.FUN_2CC48(piVar19, puVar9);
                            Utilities.ParentChildren(piVar19, piVar19);
                            puVar22.DAT_8C = puVar9;
                            puVar9.FUN_30BF0();
                            puVar22.DAT_80 = piVar19;
                            piVar19.flags |= 0x4000000;
                            piVar19.FUN_30BA8();
                            iVar6 = ((Vehicle)piVar19).FUN_3B078(arg2, (ushort)DAT_1A, -100, 1);
                            ((Vehicle)piVar19).FUN_3A020(iVar6, DAT_20, true);
                            puVar22.vTransform.rotation = Utilities.FUN_2A724(local_20);
                            iVar10 = iVar5 * puVar22.vTransform.rotation.V02;

                            if (iVar10 < 0)
                                iVar10 += 0xffff;

                            iVar12 = iVar5 * puVar22.vTransform.rotation.V12;
                            puVar22.vTransform.rotation.V02 = (short)(iVar10 >> 16);

                            if (iVar12 < 0)
                                iVar12 += 0xffff;

                            iVar10 = iVar5 * puVar22.vTransform.rotation.V22;
                            puVar22.vTransform.rotation.V12 = (short)(iVar12 >> 16);

                            if (iVar10 < 0)
                                iVar10 += 0xffff;

                            puVar22.vTransform.rotation.V22 = (short)(iVar10 >> 16);
                            puVar22.flags = 0x24;
                            puVar22.FUN_3066C();
                            oVar10 = puVar22.DAT_80;
                            puVar7 = puVar22;
                        }
                    }

                    uVar5 = 540;

                    if (arg2.id < 0)
                        uVar5 = 360;
                }
                else
                {
                    uVar5 = 0;

                    if (arg1 == 13 && GameManager.instance.DAT_1084 == 0)
                    {
                        vVar23 = (Vehicle)arg2;
                        oVar12 = vVar23.target;
                        iVar10 = vVar23.vTransform.position.x - oVar12.vTransform.position.x;

                        if (iVar10 < 0)
                            iVar10 = -iVar10;

                        uVar5 = 0;

                        if (iVar10 < 0x40000)
                        {
                            iVar10 = vVar23.vTransform.position.y - oVar12.vTransform.position.y;

                            if (iVar10 < 0)
                                iVar10 = -iVar10;

                            uVar5 = 0;

                            if (iVar10 < 0x40000)
                            {
                                iVar10 = vVar23.vTransform.position.z - oVar12.vTransform.position.z;

                                if (iVar10 < 0)
                                    iVar10 = -iVar10;

                                uVar5 = (0x3ffff < iVar10 ? 1U : 0) ^ 1;
                            }
                        }
                    }
                }
            }
        }

        return uVar5;
    }
}
