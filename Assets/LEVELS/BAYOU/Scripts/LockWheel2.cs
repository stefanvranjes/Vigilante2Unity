using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockWheel2 : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_1110 (BAYOU.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        VigTuple2 tVar1;
        VigObject oVar1;
        VigObject oVar2;
        VigObject oVar3;

        if (arg1 == 2)
        {
            FUN_30B78();
            tags = 1;
        }
        else
        {
            if (arg1 < 3)
            {
                if (arg1 == 0)
                {
                    if (tags == 0)
                    {
                        GameManager.instance.DAT_DB0 -= 68;

                        if (GameManager.instance.DAT_DB0 < 0x2d7800)
                        {
                            GameManager.instance.FUN_30CB0(this, 300);
                            FUN_30BA8();
                        }
                    }
                    else
                    {
                        GameManager.instance.DAT_DB0 += 68;

                        if (0x2f3000 < GameManager.instance.DAT_DB0)
                        {
                            oVar3 = child;
                            tVar1 = GameManager.instance.FUN_2FFD0(oVar3.id);

                            if (tVar1 != null)
                                LevelManager.instance.FUN_359CC(tVar1.array, 0x8f80);

                            oVar3.ApplyTransformation();
                            oVar3.tags = 0;
                            oVar1 = oVar3.PDAT_74;
                            oVar1.ApplyTransformation();
                            oVar1.tags = 0;
                            oVar2 = FUN_306FC();
                            Destroy(oVar2);
                        }
                    }
                }
            }
            else
            {
                if (arg1 == 4)
                    LevelManager.instance.level.flags &= 0xfeffffff;
            }
        }

        return 0;
    }
}
