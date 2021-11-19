using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDam : Destructible
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

    //FUN_1474 (BAYOU.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        sbyte sVar1;
        uint uVar2;
        uint uVar3;
        int iVar4;
        VigObject oVar4;
        int iVar5;

        if (arg1 == 1)
        {
            iVar5 = 227 - id;
            oVar4 = GameManager.instance.FUN_30250(GameManager.instance.interObjs, iVar5);
            uVar3 = 0;

            if (oVar4 != null)
            {
                if (oVar4.DAT_1A != DAT_1A)
                    oVar4 = GameManager.instance.FUN_3027C(GameManager.instance.interObjs, iVar5, oVar4);

                uVar3 = 0;

                if (oVar4 != null)
                {
                    oVar4.PDAT_74 = this;
                    PDAT_74 = oVar4;
                }
            }
        }
        else
        {
            if (arg1 == 0)
            {
                iVar4 = screen.y - vTransform.position.y;
                uVar2 = GameManager.instance.FUN_1E7A8(screen);

                if (iVar4 == 0 || uVar2 == 0)
                {
                    FUN_30BA8();
                    GameManager.instance.FUN_30CB0(this, 120);
                    GameManager.instance.FUN_1DE78(DAT_18);
                    DAT_18 = 0;
                    uVar3 = 0;
                }
                else
                {
                    iVar4 /= 15;
                    iVar5 = (int)(uVar2 & 0xffff) * iVar4;

                    if (iVar5 < 0)
                        iVar5 += 4095;

                    GameManager.instance.FUN_1E2C8(DAT_18, ((uint)((int)(uVar2 >> 16) * iVar4) >> 12) << 16 | (uint)(iVar5 >> 12));
                    uVar3 = 0;
                }
            }
            else
            {
                if (arg1 == 2)
                {
                    iVar4 = screen.y - vTransform.position.y;
                    uVar2 = GameManager.instance.FUN_1E7A8(screen);

                    if (iVar4 == 0 || uVar2 == 0)
                    {
                        GameManager.instance.FUN_30CB0(this, 120);
                        uVar3 = 0;
                    }
                    else
                    {
                        iVar4 /= 15;
                        sVar1 = (sbyte)GameManager.instance.FUN_1DD9C();
                        DAT_18 = sVar1;
                        GameManager.instance.FUN_1E098(sVar1, vData.sndList, 7,
                            ((uint)((int)(uVar2 >> 16) * iVar4) >> 12) << 16 | (uint)((int)uVar2 * iVar4) >> 12 & 0xffff, true);
                        FUN_30B78();
                        uVar3 = 0;
                    }
                }
                else
                    uVar3 = base.UpdateW(arg1, arg2);
            }
        }

        return uVar3;
    }
}
