using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning3 : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_40 (EXCELSR.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        VigObject oVar1;
        VigTuple tVar2;

        if (arg1 == 4)
        {
            if ((flags & 0x2000000) == 0 && PDAT_78 != null)
            {
                if (((Lightning2)PDAT_78).DAT_88 == null)
                {
                    flags |= 0x1000000;
                    oVar1 = PDAT_78.FUN_2CCBC();
                    GameManager.instance.FUN_308C4(oVar1);
                    PDAT_78 = null;
                }
                else
                {
                    ((Lightning2)PDAT_78).DAT_94++;
                    return 0;
                }
            }

            if (PDAT_74 != null)
            {
                tVar2 = GameManager.instance.FUN_30134(GameManager.instance.worldObjs, PDAT_74);

                if (tVar2 != null)
                {
                    PDAT_74.flags &= 0xfbffffff;
                    PDAT_74.FUN_30B78();
                    PDAT_74 = null;
                }
            }
        }

        return 0;
    }
}
