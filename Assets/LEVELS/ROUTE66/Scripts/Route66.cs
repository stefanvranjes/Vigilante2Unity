using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route66 : VigObject
{
    public static Meteorite DAT_5304; //0x5304 (ROUTE66.DLL)
    public static Vector3Int[] DAT_5268 = new Vector3Int[32];

    //FUN_250 (ROUTE66.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        VigTuple ppiVar2;
        Meteorite puVar3;
        List<VigTuple> ppiVar4;
        VigObject oVar5;
        VigObject oVar6;
        int iVar7;

        switch (arg1)
        {
            case 1:
                GameManager.instance.offsetFactor = 2.5f;
                GameManager.instance.offsetStart = 0;
                GameManager.instance.angleOffset = 0.4f;
                GameManager.instance.DAT_1000 |= 1;
                GameObject obj = new GameObject();
                DAT_5304 = obj.AddComponent<Meteorite>();
                DAT_5304.id = 0;
                GameManager.instance.FUN_30CB0(DAT_5304, 900);
                oVar5 = GameManager.instance.FUN_30250(GameManager.instance.DAT_1078, 0x100);
                oVar6 = GameManager.instance.FUN_4AC1C(0xfe000000, oVar5);
                GameManager.instance.DAT_1038 = oVar6 != null ? 1 : 0;
                ppiVar4 = GameManager.instance.DAT_1078;

                for (int i = 0; i < ppiVar4.Count; i++)
                {
                    ppiVar2 = ppiVar4[i];
                    oVar6 = ppiVar2.vObject;

                    if ((uint)(ushort)oVar6.id - 400 < 13)
                    {
                        iVar7 = (ushort)oVar6.id - 400;
                        DAT_5268[iVar7] = oVar6.screen;
                    }
                }

                goto case 2;

            case 2:
                GameManager.instance.FUN_34B34();
                GameManager.instance.FUN_30CB0(this, 240);
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
                break;
        }

        return 0;
    }
}
