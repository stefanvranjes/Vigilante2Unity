using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectionBooth : Destructible
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override uint OnCollision(HitDetection hit)
    {
        return base.OnCollision(hit);
    }

    //FUN_4F54 (ROUTE66.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        sbyte sVar1;
        int iVar2;
        uint uVar3;

        if (arg1 == 1)
        {
            GameManager.instance.FUN_30CB0(this, DAT_19 + 120);
            uVar3 = 0;
        }
        else
        {
            if (arg1 == 0)
            {
                uVar3 = 0;

                if (arg2 != 0)
                {
                    iVar2 = (int)GameManager.instance.FUN_1E7A8(screen);

                    if (iVar2 == 0)
                    {
                        FUN_30BA8();
                        GameManager.instance.FUN_30CB0(this, 120);
                        GameManager.instance.FUN_1DE78(DAT_18);
                        DAT_18 = 0;
                        uVar3 = 0;
                    }
                    else
                    {
                        GameManager.instance.FUN_1E2C8(DAT_18, (uint)iVar2);
                        uVar3 = 0;
                    }
                }
            }
            else
            {
                if (arg1 == 2)
                {
                    iVar2 = (int)GameManager.instance.FUN_1E7A8(screen);

                    if (iVar2 == 0)
                    {
                        GameManager.instance.FUN_30CB0(this, 120);
                        uVar3 = 0;
                    }
                    else
                    {
                        sVar1 = (sbyte)GameManager.instance.FUN_1DD9C();
                        DAT_18 = sVar1;
                        GameManager.instance.FUN_1E098(sVar1, vData.sndList, 6, (uint)iVar2, true);
                        FUN_30B78();
                        uVar3 = 0;
                    }
                }
                else
                {
                    if (arg1 == 9)
                    {
                        FUN_30BA8();
                        FUN_30C68();
                        GameManager.instance.FUN_1DE78(DAT_18);
                        uVar3 = 0;
                    }
                    else
                        uVar3 = base.UpdateW(arg1, arg2);
                }
            }
        }

        return uVar3;
    }
}
