using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bearhug : Mine
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_47384
    public override uint UpdateW(int arg1, int arg2)
    {
        int iVar2;

        if (arg1 == 2)
            id = 0;
        else
        {
            if (arg1 < 3)
            {
                if (arg1 == 0)
                {
                    if (tags == 0)
                    {
                        iVar2 = FUN_46D70();

                        if (iVar2 != 0)
                        {
                            if (vTransform.position.z < GameManager.instance.DAT_DA0 &&
                                GameManager.instance.DAT_DB0 <= vTransform.position.y)
                            {
                                tags = 1;
                                physics2.M2 = 0;
                            }
                            else
                                FUN_30BA8();

                            flags |= 0x100;
                            GameManager.instance.FUN_30CB0(this, 120);
                        }
                    }
                    else
                    {
                        iVar2 = GameManager.DAT_65C90[(physics2.M2 & 0xfff) * 2] * 0x1400;

                        if (iVar2 < 0)
                            iVar2 += 0xfff;

                        vTransform.position.y = GameManager.instance.DAT_DB0 + (iVar2 >> 12);
                        physics2.M2 += 34;
                    }
                }
            }
            else
            {
                if (arg1 == 4)
                    ; //sound
            }
        }

        return 0;
    }
}
