using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum _BODY_TYPE
{
    Default, 
    Collector //FUN_25C (GRBLDER.DLL)
}

public class Body : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public _BODY_TYPE state;

    public override uint UpdateW(int arg1, int arg2)
    {
        short sVar1;
        Vector3Int v3Var2;
        int iVar3;

        switch (state)
        {
            case _BODY_TYPE.Collector:
                if (arg1 == 0)
                {
                    sVar1 = 32;

                    if (tags == 0)
                        sVar1 = -32;

                    vr.x += sVar1;

                    if (arg2 != 0)
                        ApplyTransformation();
                }
                else
                {
                    if (arg1 == 2)
                    {
                        if ((flags & 0x80) == 0)
                        {
                            GameManager.instance.FUN_30CB0(this, 31);
                            FUN_30B78();
                        }
                        else
                        {
                            ApplyTransformation();
                            iVar3 = 1 - tags;
                            tags = (sbyte)iVar3;

                            if (((iVar3 * 0x1000000 >> 24 ^ id) & 1) == 0)
                                FUN_30BA8();
                            else
                                GameManager.instance.FUN_30CB0(this, 31);

                            v3Var2 = GameManager.instance.FUN_2CE50(this);
                            LevelManager.instance.FUN_4DE54(v3Var2, 13);
                        }
                    }
                }

                break;
        }

        return 0;
    }
}
