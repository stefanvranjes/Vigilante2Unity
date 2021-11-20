using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellGate : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public static VigObject OnInitialize(XOBF_DB arg1, int arg2)
    {
        VigObject oVar6;

        GameObject obj = new GameObject();
        oVar6 = obj.AddComponent<HellGate>();
        oVar6.vData = arg1;
        return oVar6;
    }

    //FUN_12E8 (HALFTRAK.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        VigTransform puVar1;
        Demon puVar2;
        VigObject oVar3;
        int iVar7;
        VigObject oVar7;
        ConfigContainer ccVar7;
        VigObject puVar12;

        switch (arg1)
        {
            case 0:
                if ((flags & 0x1000000) != 0)
                {
                    if (-1 < tags)
                    {
                        ccVar7 = CCDAT_74;
                        vTransform.position = ccVar7.v3_1;
                    }

                    CCDAT_74 = null;
                    flags &= 0xfeffffff;
                }

                if (CCDAT_74 == null)
                {
                    oVar3 = Utilities.FUN_2CD78(this);
                    puVar1 = GameManager.instance.FUN_2CDF4(this);
                    iVar7 = 0;
                    puVar12 = this;

                    do
                    {
                        iVar7++;
                        puVar2 = vData.ini.FUN_2C17C(2, typeof(Demon), 8) as Demon;
                        puVar2.flags = 0xa4;
                        puVar2.vTransform = puVar1;
                        puVar2.DAT_80 = oVar3;
                        puVar2.DAT_94 = this;
                        puVar12.PDAT_74 = puVar2;
                        puVar2.FUN_305FC();
                        puVar12 = puVar2;
                    } while (iVar7 < 3);

                    puVar2.PDAT_74 = null;

                    if (-1 < tags)
                        FUN_30BA8();
                }

                return (uint)FUN_4205C();
            case 4:
                oVar7 = PDAT_74;

                if (oVar7 == null)
                    return 0;

                do
                {
                    GameManager.instance.FUN_309A0(oVar7);
                    oVar7 = oVar7.PDAT_74;
                } while (oVar7 != null);

                break;
        }

        return 0;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        int iVar3;
        HellGate2 ppcVar4;
        VigObject pcVar5;
        XOBF_DB dbVar5;
        int iVar7;
        VigObject oVar7;
        VigTransform tVar7;
        Vector3Int auStack24;

        switch (arg1)
        {
            case 1:
                FUN_30B78();
                maxHalfHealth = 3;
                flags |= 0x4000;
                break;
            case 12:
                pcVar5 = ((Vehicle)arg2).target;

                if ((pcVar5 == null && (pcVar5 = arg2) == null) || 
                    (flags & 0x1000000) != 0 || (arg2.flags & 0x30000000) != 0x30000000 || 
                    pcVar5.vTransform.position.x < VigTerrain.instance.DAT_DE4 || 
                    VigTerrain.instance.DAT_DF0 < pcVar5.vTransform.position.x ||
                    pcVar5.vTransform.position.z < VigTerrain.instance.DAT_DEC || 
                    VigTerrain.instance.DAT_DF0 < pcVar5.vTransform.position.z || 
                    VigTerrain.instance.GetTileByPosition
                    ((uint)pcVar5.vTransform.position.x, (uint)pcVar5.vTransform.position.z).DAT_10[3] == 7)
                {
                    //iVar3 = GameManager.instance.FUN_1DD9C();
                    //GameManager.instance.FUN_1E14C(iVar3, GameManager.instance.DAT_C2C, 1);
                    return 0;
                }
                else
                {
                    if (arg2.id < 0)
                        GameManager.instance.FUN_15B00(~arg2.id, 128, 64, 64);

                    if (maxHalfHealth < 4)
                    {
                        PDAT_74 = PDAT_74.PDAT_74;
                        GameManager.instance.FUN_309A0(PDAT_74);
                    }

                    iVar3 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E580(iVar3, vData.sndList, 4, arg2.vTransform.position);
                    iVar3 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E188(iVar3, vData.sndList, 2);
                    tVar7 = GameManager.instance.FUN_2CDF4(this);
                    auStack24 = GameManager.instance.terrain.FUN_1B998((uint)tVar7.position.x, (uint)tVar7.position.z);
                    auStack24 = Utilities.VectorNormal(auStack24);
                    GameObject obj = new GameObject();
                    ppcVar4 = obj.AddComponent<HellGate2>();
                    ppcVar4.flags = 0xa4;
                    dbVar5 = vData;
                    ppcVar4.DAT_80 = arg2;
                    ppcVar4.DAT_94 = this;
                    ppcVar4.vData = dbVar5;

                    if (pcVar5 == null)
                        pcVar5 = arg2;

                    ppcVar4.DAT_84 = pcVar5;
                    ppcVar4.vTransform.rotation = Utilities.FUN_2A5EC(auStack24);
                    ppcVar4.vTransform.position = tVar7.position;
                    ppcVar4.FUN_305FC();

                    if (ppcVar4.GetType().IsSubclassOf(typeof(VigObject)))
                        ppcVar4.UpdateW(2, 0);

                    arg2.FUN_30BA8();
                    arg2.flags |= 0x6000020;
                    GameManager.instance.DAT_1084++;
                    maxHalfHealth--;
                }

                if (-1 < arg2.id)
                    return 900;

                return 480;
            case 13:
                if (GameManager.instance.DAT_1084 != 0)
                    return 0;

                if ((flags & 0x1000000) != 0)
                    return 0;

                oVar7 = ((Vehicle)arg2).target;

                if ((arg2.flags & oVar7.flags & 0x30000000) != 0x30000000)
                    return 0;

                if (1524 < oVar7.physics1.W)
                    return 0;

                iVar7 = Utilities.FUN_29F6C(arg2.screen, oVar7.screen);
                return 0x64000 < iVar7 ? 1U : 0;
            case 16:
                oVar7 = arg2.PDAT_74;
                arg2.PDAT_74 = oVar7.PDAT_74;

                if (2 < maxHalfHealth)
                {
                    GameManager.instance.FUN_309A0(oVar7);
                    return 0;
                }

                oVar7.PDAT_74 = PDAT_74;
                PDAT_74 = oVar7;
                ((Demon)oVar7).DAT_94 = this;
                break;
        }

        return 0;
    }
}
