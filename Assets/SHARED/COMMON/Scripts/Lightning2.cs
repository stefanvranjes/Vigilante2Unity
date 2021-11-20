using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum _LIGHTNING2_TYPE
{
    Default,
    Type1 //FUN_F8 (EXCELSR.DLL)
}

public class Lightning2 : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public _LIGHTNING2_TYPE state;
    public Lightning2 DAT_88; //0x88
    public Lightning3 DAT_8C; //0x8C
    public ConfigContainer DAT_90; //0x90
    public int DAT_94;

    //FUN_F8 (EXCELSR.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        short sVar1;
        bool bVar2;
        sbyte sVar3;
        int iVar4;
        VigObject oVar4;
        VigTuple tVar4;
        int iVar5;
        VigObject oVar5;
        uint uVar6;
        uint uVar7;
        int iVar8;
        VigObject oVar8;
        Throwaway puVar9;
        int iVar10;
        VigObject oVar10;
        int iVar12;
        Lightning2 oVar12;
        int iVar13;
        Lightning2 oVar14;
        Lightning2 oVar15;
        Lightning3 oVar16;
        VigObject local_20;
        Vector3Int local_38;
        Vector3Int local_68;
        Vector3Int auStack88;
        VigTransform auStack80;

        switch (state)
        {
            case _LIGHTNING2_TYPE.Type1:
                if (arg1 == 2)
                {
                    if (DAT_94 == 0)
                    {
                        oVar5 = FUN_2CCBC();
                        GameManager.instance.FUN_307CC(oVar5);
                        return 0;
                    }
                    else
                    {
                        DAT_94--;
                        GameManager.instance.FUN_30CB0(this, 180);
                        DAT_84.id += 180;
                    }
                }

                if (arg1 < 3)
                {
                    if (arg1 != 0)
                        return 0;

                    oVar4 = Utilities.FUN_2CD78(this);
                    DAT_84 = oVar4;

                    if (oVar4 == null || (oVar4 = Utilities.FUN_2CD78(oVar4)) == null)
                    {
                        oVar5 = FUN_2CCBC();
                        GameManager.instance.FUN_308C4(oVar5);
                        return 0xffffffff;
                    }

                    oVar12 = this;
                    oVar4 = oVar12.DAT_80;

                    if (oVar4 != null)
                    {
                        while ((oVar4.flags & 0x4000) == 0)
                        {
                            if (oVar12.DAT_88 == null)
                            {
                                oVar5 = FUN_2CCBC();
                                GameManager.instance.FUN_308C4(oVar5);
                                return 0xffffffff;
                            }
                            else
                            {
                                oVar12 = oVar12.DAT_88;
                                oVar4 = oVar12.DAT_80;
                            }
                        }
                    }

                    oVar4 = DAT_80;
                    local_38 = new Vector3Int();
                    bVar2 = true;

                    if (oVar4 == null)
                    {
                        sVar3 = (sbyte)(DAT_19 - 1);
                        DAT_19 = (byte)sVar3;

                        if (sVar3 != 0)
                            return 0;

                        iVar4 = (int)GameManager.FUN_2AC5C();
                        local_68 = new Vector3Int();
                        local_68.x = (iVar4 - 0x4000) * 0x10000;
                        uVar6 = GameManager.FUN_2AC5C();
                        local_68.y = ((int)(uVar6 & 0x1fff) - 0x1000) * 0x10000;
                        iVar4 = (int)GameManager.FUN_2AC5C();
                        local_68.z = (iVar4 - 0x4000) * 0x10000;
                        Utilities.FUN_29FC8(local_68, out auStack88);
                        vTransform.rotation = Utilities.FUN_2A724(auStack88);
                        iVar4 = (int)GameManager.FUN_2AC5C();
                        iVar12 = (iVar4 * 0x50000 >> 15) + 0x20000;
                        iVar4 = iVar12 * vTransform.rotation.V02;

                        if (iVar4 < 0)
                            iVar4 += 0xffff;

                        iVar8 = iVar12 * vTransform.rotation.V12;
                        vTransform.rotation.V02 = (short)(iVar4 >> 16);

                        if (iVar8 < 0)
                            iVar8 += 0xffff;

                        iVar12 = iVar12 * vTransform.rotation.V22;
                        vTransform.rotation.V12 = (short)(iVar8 >> 16);

                        if (iVar12 < 0)
                            iVar12 += 0xffff;

                        vTransform.rotation.V22 = (short)(iVar12 >> 16);
                        iVar4 = (int)GameManager.FUN_2AC5C();
                        DAT_19 = (byte)((iVar4 * 10 >> 15) + 5);
                    }
                    else
                    {
                        local_20 = Utilities.FUN_2CD78(DAT_84);
                        uVar6 = GameManager.FUN_2AC5C();
                        oVar12 = this;

                        while (true)
                        {
                            uVar7 = GameManager.FUN_2AC5C();

                            if (DAT_94 == 0)
                                oVar12.DAT_80.vTransform.position.y -= 682;
                            else
                                oVar12.DAT_80.vTransform.position.y -= 222;

                            iVar10 = oVar12.DAT_80.vTransform.position.x;
                            iVar8 = iVar10 - 2730;

                            if ((uVar7 & 1) == 0)
                                iVar8 = iVar10 + 2730;

                            oVar12.DAT_80.vTransform.position.x = iVar8;
                            iVar10 = oVar12.DAT_80.vTransform.position.z;
                            iVar8 = iVar10 - 2730;

                            if ((uVar7 & 2) == 0)
                                iVar8 = iVar10 + 2730;

                            oVar12.DAT_80.vTransform.position.z = iVar8;
                            oVar8 = oVar12.DAT_80;
                            oVar8.screen = oVar8.vTransform.position; //should be ignored as a counter to his special
                            iVar8 = (int)GameManager.FUN_2AC5C();
                            oVar12.DAT_80.FUN_24700((short)((iVar8 * 511 >> 15) - 255), 0, 0);
                            sVar3 = (sbyte)(oVar12.DAT_19 - 1);
                            oVar12.DAT_19 = (byte)sVar3;

                            if (sVar3 == 0)
                            {
                                oVar12.DAT_19 = 40;

                                if (bVar2)
                                {
                                    iVar8 = 0;
                                    iVar5 = GameManager.instance.FUN_1DD9C();
                                    GameManager.instance.FUN_1E580(iVar5, GameManager.instance.DAT_C2C, 75, oVar12.DAT_80.vTransform.position);
                                    UIManager.instance.FUN_4E414(oVar12.DAT_80.vTransform.position, new Color32(0x00, 0x00, 0xff, 8));

                                    do
                                    {
                                        puVar9 = LevelManager.instance.xobfList[19].ini.FUN_2C17C(49, typeof(Throwaway), 8) as Throwaway;
                                        puVar9.physics1.M0 = 0;
                                        puVar9.physics1.M1 = 0;
                                        puVar9.physics1.M2 = 0;
                                        uVar7 = GameManager.FUN_2AC5C();
                                        puVar9.physics1.Z = (int)(uVar7 & 0xfff) - 0x800;
                                        iVar10 = (int)GameManager.FUN_2AC5C();

                                        if (iVar10 < 0)
                                            iVar10 += 15;

                                        puVar9.physics1.W = -(iVar10 >> 4);
                                        uVar7 = GameManager.FUN_2AC5C();
                                        puVar9.physics2.X = (int)(uVar7 & 0xfff) - 0x800;
                                        puVar9.type = 7;
                                        puVar9.flags |= 0xb4;
                                        sVar1 = id;
                                        iVar8++;
                                        puVar9.state = _THROWAWAY_TYPE.Type3;
                                        puVar9.id = sVar1;
                                        puVar9.vTransform = GameManager.FUN_2A39C();
                                        oVar10 = oVar12.DAT_80;
                                        puVar9.vTransform.position = oVar10.vTransform.position;
                                        puVar9.FUN_2D1DC();
                                        puVar9.DAT_87 = 1;
                                        puVar9.FUN_305FC();
                                    } while (iVar8 < 5);
                                }
                            }

                            if (((uVar6 ^ 1) & 1) != 0)
                            {
                                iVar5 = ((Vehicle)oVar12.DAT_80).FUN_3B078(local_20, (ushort)DAT_1A, -2, 1);
                                ((Vehicle)oVar12.DAT_80).FUN_39DCC(iVar5, Lightning.DAT_20, true);

                                if (oVar12.DAT_80.id < 0)
                                    GameManager.instance.FUN_15ADC(~oVar12.DAT_80.id, 20);
                            }

                            if (bVar2)
                            {
                                auStack80 = GameManager.instance.FUN_2CEAC(oVar12.DAT_84, oVar12.DAT_90);
                                local_68 = Utilities.FUN_24304(auStack80, oVar12.DAT_80.vTransform.position);
                            }
                            else
                            {
                                oVar8 = oVar12.DAT_80;
                                local_68 = new Vector3Int();
                                local_68.x = oVar8.vTransform.position.x - local_38.x;
                                local_68.y = oVar8.vTransform.position.y - local_38.y;
                                local_68.z = oVar8.vTransform.position.z - local_38.z;
                                oVar12.vTransform.position = local_38;
                            }

                            oVar8 = oVar12.DAT_80;
                            local_38 = oVar8.vTransform.position;
                            Utilities.FUN_29FC8(local_68, out auStack88);
                            iVar8 = Utilities.FUN_29E84(local_68);
                            iVar8 /= 2;

                            if (0x70000 < iVar8 && bVar2) break;

                            iVar10 = iVar8;

                            if (!bVar2)
                            {
                                iVar10 = 0x70000;

                                if (iVar8 < 0x70000)
                                    iVar10 = iVar8;
                            }

                            oVar12.vTransform.rotation = Utilities.FUN_2A724(auStack88);
                            iVar8 = iVar10 * oVar12.vTransform.rotation.V02;

                            if (iVar8 < 0)
                                iVar8 += 0xffff;

                            iVar13 = iVar10 * oVar12.vTransform.rotation.V12;
                            oVar12.vTransform.rotation.V02 = (short)(iVar8 >> 16);

                            if (iVar13 < 0)
                                iVar13 += 0xffff;

                            iVar10 = iVar10 * oVar12.vTransform.rotation.V22;
                            oVar12.vTransform.rotation.V12 = (short)(iVar13 >> 16);

                            if (iVar10 < 0)
                                iVar10 += 0xffff;

                            oVar12.vTransform.rotation.V22 = (short)(iVar10 >> 16);
                            oVar12 = oVar12.DAT_88;
                            bVar2 = false;

                            if (oVar12 == null)
                                return 0;
                        }

                        oVar12.DAT_19 = 5;
                        oVar14 = oVar12;

                        while ((oVar15 = oVar14) != null)
                        {
                            oVar14 = oVar15.DAT_88;
                            oVar15.DAT_88 = null;

                            if (oVar15.DAT_80 != null)
                            {
                                oVar16 = oVar15.DAT_8C;

                                if (oVar16 != null && (oVar16.flags & 0x3000000) == 0)
                                {
                                    oVar16.flags |= 0x2000000;
                                    oVar5 = oVar15.DAT_8C.FUN_2CCBC();
                                    GameManager.instance.FUN_307CC(oVar5);
                                    oVar15.DAT_8C = null;
                                }
                                else
                                {
                                    oVar15.DAT_80.flags &= 0xfbffffff;
                                    oVar15.DAT_80.FUN_30B78();
                                }

                                oVar15.DAT_80 = null;
                            }

                            if (oVar15 != oVar12)
                                GameManager.instance.FUN_309A0(oVar15);
                        }

                        oVar12.DAT_80 = null;
                    }
                }
                else
                {
                    if (arg1 != 4)
                        return 0;

                    oVar14 = DAT_88;

                    while (oVar14 != null)
                    {
                        oVar15 = oVar14.DAT_88;
                        oVar16 = oVar14.DAT_8C;
                        oVar14.DAT_88 = null;

                        if (oVar16 != null && (oVar16.flags & 0x3000000) == 0)
                        {
                            oVar16.flags |= 0x2000000;
                            oVar5 = oVar14.DAT_8C.FUN_2CCBC();
                            GameManager.instance.FUN_307CC(oVar5);
                            oVar14.DAT_8C = null;
                        }
                        else
                        {
                            oVar14.DAT_80.flags &= 0xfbffffff;
                            oVar14.DAT_80.FUN_30B78();
                        }

                        oVar14.DAT_80 = null;
                        GameManager.instance.FUN_309A0(oVar14);
                        oVar14 = oVar15;
                    }

                    if (DAT_80 != null)
                    {
                        oVar16 = DAT_8C;

                        if (oVar16 != null && (oVar16.flags & 0x3000000) == 0)
                        {
                            oVar16.flags |= 0x2000000;
                            oVar5 = DAT_8C.FUN_2CCBC();
                            GameManager.instance.FUN_307CC(oVar5);
                            DAT_8C = null;
                        }
                        else
                        {
                            DAT_80.flags &= 0xfbffffff;
                            DAT_80.FUN_30B78();
                        }

                        DAT_80 = null;
                    }

                    if (DAT_84 != null)
                    {
                        oVar4 = Utilities.FUN_2CD78(DAT_84);

                        if (oVar4 != null)
                        {
                            tVar4 = GameManager.instance.FUN_30134(GameManager.instance.worldObjs, oVar4);

                            if (tVar4 != null)
                            {
                                if (DAT_84.maxHalfHealth == 0)
                                    DAT_84.FUN_3A368();
                            }
                        }
                    }

                    GameManager.instance.DAT_1084--;

                    if (DAT_18 == 0)
                        return 0;

                    GameManager.instance.FUN_1DE78(DAT_18);
                }

                break;
        }

        return 0;
    }
}
