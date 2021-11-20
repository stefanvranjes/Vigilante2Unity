using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBridge : Destructible
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

    //FUN_1DF4 (HARBOR.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        sbyte sVar1;
        sbyte sVar2;
        short sVar3;
        int iVar4;
        uint uVar4;
        VigObject oVar5;
        VigObject oVar6;

        if (arg1 == 2)
        {
            if (arg1 == 10)
                sVar2 = 1;
            else
                sVar2 = 2;

            tags = sVar2;
            DAT_19 = 240;
            FUN_30B78();
            uVar4 = 0;
        }
        else
        {
            if (arg1 < 3)
            {
                if (arg1 == 0)
                {
                    oVar5 = child2;
                    oVar6 = oVar5.child;

                    if (tags == 1)
                    {
                        oVar5.vr.z -= 2;
                        sVar3 = (short)(oVar6.vr.z + 2);
                    }
                    else
                    {
                        oVar5.vr.z += 2;
                        sVar3 = (short)(oVar6.vr.z - 2);
                    }

                    oVar6.vr.z = sVar3;

                    if (arg2 != 0)
                    {
                        oVar5.ApplyTransformation();
                        oVar6.ApplyTransformation();
                    }

                    sVar1 = (sbyte)(DAT_19 - 1);
                    DAT_19 = (byte)sVar1;

                    if (sVar1 != 0)
                        return 0;

                    FUN_30BA8();

                    if (tags == 1)
                    {
                        GameManager.instance.FUN_30CB0(this, 300);
                        return 0;
                    }

                    tags = 0;
                    return 0;
                }
            }
            else
            {
                if (arg1 == 10)
                {
                    iVar4 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E580(iVar4, vData.sndList, 5, vTransform.position);

                    if (arg1 == 10)
                        sVar2 = 1;
                    else
                        sVar2 = 2;

                    tags = sVar2;
                    DAT_19 = 240;
                    FUN_30B78();
                    return 0;
                }
            }

            uVar4 = base.UpdateW(arg1, arg2);
        }

        return uVar4;
    }
}
