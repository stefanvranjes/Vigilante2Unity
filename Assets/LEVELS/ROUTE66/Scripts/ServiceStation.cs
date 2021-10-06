using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceStation : Destructible
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
        bool bVar4;
        VigObject oVar4;
        Vehicle vVar4;
        int iVar5;

        oVar4 = hit.self;

        if (oVar4.type == 2 && (oVar4.flags & 0x4000) != 0 && 
            tags != 0 && hit.object1.type == 3)
        {
            vVar4 = (Vehicle)oVar4;
            PDAT_74 = vVar4;

            if (tags != 1)
                return 0;

            FUN_30B78();
            tags = 2;
            DAT_19 = 0;
            return 0;
        }

        if (oVar4.type != 8)
            return 0;

        bVar4 = hit.object1.FUN_32B90(oVar4.maxHalfHealth);

        if (!bVar4)
            return 0;

        if (hit.object1.id != 1)
            return 0;

        hit.object1.id = 0;
        oVar4 = child2;
        iVar5 = 0;

        while (oVar4 != null)
        {
            if (oVar4.id == 1)
                iVar5++;

            oVar4 = oVar4.child;
        }

        if (iVar5 != 0)
            return 0;

        FUN_30C68();
        tags = 0;
        return 0;
    }

    //FUN_4574 (ROUTE66.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        short sVar1;
        sbyte sVar2;
        int iVar3;
        bool bVar4;
        VigObject oVar4;
        Vehicle vVar4;

        switch (arg1)
        {
            case 0:
                vVar4 = PDAT_74 as Vehicle;

                if (vVar4 != null)
                {
                    sVar2 = (sbyte)(DAT_19 + 1);
                    DAT_19 = (byte)sVar2;

                    if (sVar2 != 30)
                        return 0;

                    DAT_19 = 0;

                    if (vVar4.body[0] == null)
                    {
                        if (vVar4.maxFullHealth <= vVar4.maxHalfHealth)
                            goto LAB_468C;
                    }
                    else
                    {
                        if (vVar4.maxFullHealth <= 
                            vVar4.body[0].maxHalfHealth +
                            vVar4.body[1].maxHalfHealth)
                        {
                            tags = 0;
                            goto LAB_4690;
                        }
                    }

                    vVar4.FUN_3A0C0(60);
                    iVar3 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E628(iVar3, GameManager.instance.DAT_C2C, 44, vTransform.position);
                    //FUN_4E414
                    PDAT_74 = null;
                    sVar2 = (sbyte)(tags + 1);
                    tags = sVar2;

                    if (sVar2 < 8)
                        return 0;
                }

                LAB_468C:
                tags = 0;
                LAB_4690:
                FUN_30BA8();
                GameManager.instance.FUN_30CB0(this, 1800);
                return 0;
            case 1:
                oVar4 = child2;

                while (oVar4 != null)
                {
                    sVar1 = oVar4.id;

                    if (sVar1 == 1)
                        oVar4.maxHalfHealth = 100;
                    else
                    {
                        if (sVar1 == 2)
                            oVar4.maxHalfHealth = 50;
                        else
                        {
                            if (sVar1 == 4)
                                oVar4.type = 3;
                        }
                    }

                    oVar4 = oVar4.child;
                }
                goto case 2;
            case 2:
                tags = 1;
                break;
            default:
                break;
            case 8:
                bVar4 = FUN_32B90((uint)arg2);

                if (!bVar4)
                    return 0;

                FUN_30C68();
                tags = 0;
                break;
        }

        return 0;
    }
}
