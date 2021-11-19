using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPower : VigObject
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
        StarPower oVar6;
        GameObject obj = new GameObject();
        oVar6 = obj.AddComponent<StarPower>();
        oVar6.vData = arg1;
        return oVar6;
    }

    //FUN_B58 (VERTIGO.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        VigObject pcVar2;
        StarPower2 ppcVar3;
        VigTransform ppcVar4;
        ConfigContainer ccVar11;

        switch (arg1)
        {
            case 0:
                if (0x8000000 < (uint)arg2)
                    return 0;

                if (tags < 0)
                    return (uint)FUN_4205C();

                pcVar2 = Utilities.FUN_2CD78(this);
                ppcVar3 = vData.ini.FUN_2C17C(1, typeof(StarPower2), 8) as StarPower2;

                if ((flags & 0x1000000) != 0)
                {
                    ccVar11 = CCDAT_74;
                    vTransform.position = ccVar11.v3_1;
                    flags &= 0xfeffffff;
                }

                ppcVar3.type = 8;
                ppcVar3.id = pcVar2.id;
                ppcVar4 = GameManager.instance.FUN_2CDF4(this);
                ppcVar3.vTransform = ppcVar4;
                ppcVar3.flags = 0x600000a4;
                ppcVar3.maxHalfHealth = 4;
                ppcVar3.DAT_80 = pcVar2;
                ppcVar3.DAT_94 = this;
                PDAT_74 = ppcVar3;

                if (ppcVar3.GetType().IsSubclassOf(typeof(VigObject)))
                    ppcVar3.UpdateW(1, 0);

                ppcVar3.FUN_305FC();
                FUN_30BA8();
                return 0;
            case 4:
                GameManager.instance.DAT_1084--;

                if ((flags & 0x1000000) != 0)
                    return 0;

                if (PDAT_74 == null)
                    return 0;

                GameManager.instance.FUN_309A0(PDAT_74);
                PDAT_74 = null;
                break;
        }

        return 0;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        sbyte sVar1;
        int iVar5;
        VigObject oVar6;
        VigObject oVar11;

        switch (arg1)
        {
            case 1:
                FUN_30B78();
                maxHalfHealth = 6;
                flags |= 0x4000;
                break;
            case 12:
                oVar11 = PDAT_74;

                if ((flags & 0x1000000) == 0 && oVar11.tags == 0)
                {
                    sVar1 = (sbyte)GameManager.instance.FUN_1DD9C();
                    oVar11.DAT_18 = sVar1;
                    GameManager.instance.FUN_1E14C(sVar1, vData.sndList, 3, true);
                    iVar5 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E188(iVar5, vData.sndList, 2);
                    oVar11.tags = 1;
                    oVar11.flags |= 0x20000000;
                    oVar11.vr.x = oVar11.vr.x << 20 >> 20;
                    oVar6 = ((Vehicle)arg2).target;

                    if (oVar6 == null)
                        oVar6 = arg2;

                    oVar11.DAT_84 = oVar6;
                    GameManager.instance.DAT_1084++;
                    maxHalfHealth--;
                }
                else
                {
                    //iVar5 = GameManager.instance.FUN_1DD9C();
                    //GameManager.instance.FUN_1E14C(iVar5, GameManager.instance.DAT_C2C, 1);
                    return 0;
                }

                if (-1 < arg2.id)
                    return 900;

                return 600;
            case 13:
                if (GameManager.instance.DAT_1084 != 0)
                    return 0;

                if ((flags & 0x1000000) == 0)
                    return PDAT_74.tags == 0 ? 1U : 0;

                break;
        }

        return 0;
    }
}
