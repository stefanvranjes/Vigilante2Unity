using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : VigObject
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
        oVar6 = obj.AddComponent<Bird>();
        oVar6.vData = arg1;
        return oVar6;
    }

    //FUN_A44 (FRONTIER.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        VigObject pcVar1;
        Bird2 ppcVar2;
        VigTransform ppcVar3;
        ConfigContainer ccVar5;
        uint uVar6;

        switch (arg1)
        {
            case 0:
                uVar6 = 0;
                if (tags < 0)
                    uVar6 = (uint)FUN_4205C();
                else
                {
                    pcVar1 = Utilities.FUN_2CD78(this);
                    ppcVar2 = vData.ini.FUN_2C17C(1, typeof(Bird2), 8) as Bird2;

                    if ((flags & 0x1000000) != 0)
                    {
                        ccVar5 = CCDAT_74;
                        vTransform.position = ccVar5.v3_1;
                        flags &= 0xfeffffff;
                    }

                    ppcVar2.id = pcVar1.id;
                    ppcVar3 = GameManager.instance.FUN_2CDF4(this);
                    ppcVar2.vTransform = ppcVar3;
                    ppcVar2.flags = 0xa4;
                    ppcVar2.DAT_80 = pcVar1;
                    ppcVar2.DAT_94 = this;
                    PDAT_74 = ppcVar2;

                    if (ppcVar2.GetType().IsSubclassOf(typeof(VigObject)))
                        ppcVar2.UpdateW(1, 0);

                    ppcVar2.FUN_305FC();
                    FUN_30BA8();
                    uVar6 = 0;
                }

                break;
            default:
                uVar6 = 0;
                break;
            case 4:
                GameManager.instance.DAT_1084--;

                if ((flags & 0x1000000) != 0)
                    return 0;
                
                GameManager.instance.FUN_309A0(PDAT_74);
                goto default;
        }

        return uVar6;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        int iVar4;
        int iVar5;
        VigObject oVar5;
        VigObject oVar6;
        uint uVar6;

        switch (arg1)
        {
            case 1:
                FUN_30B78();
                maxHalfHealth = 3;
                flags |= 0x4000;
                goto default;
            default:
                uVar6 = 0;
                break;
            case 12:
                oVar5 = PDAT_74;

                if ((flags & 0x1000000) == 0 && oVar5.DAT_19 == 0)
                {
                    oVar5.DAT_19 = 1;
                    oVar6 = ((Vehicle)arg2).target;

                    if (oVar6 == null)
                        oVar6 = arg2;

                    oVar5.DAT_84 = oVar6;
                    maxHalfHealth--;
                    GameManager.instance.FUN_30CB0(oVar5, 450);
                    iVar4 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E188(iVar4, vData.sndList, 2);
                    GameManager.instance.DAT_1084++;
                }
                else
                {
                    //iVar4 = GameManager.instance.FUN_1DD9C();
                    //GameManager.instance.FUN_1E14C(iVar4, GameManager.instance.DAT_C2C, 1);
                    return 0;
                }

                uVar6 = 900;

                if (arg2.id < 0)
                    uVar6 = 600;

                break;
            case 13:
                uVar6 = 0;

                if (GameManager.instance.DAT_1084 == 0 && PDAT_74 != null && PDAT_74.DAT_19 == 0)
                {
                    if (((Vehicle)arg2).target.physics1.W < 1525)
                    {
                        iVar5 = Utilities.FUN_29F6C(arg2.screen, ((Vehicle)arg2).target.screen);
                        uVar6 = 0x64000 < iVar5 ? 1U : 0;
                    }
                }

                break;
        }

        return uVar6;
    }
}
