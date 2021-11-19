using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turbine : Destructible
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

    //FUN_1FD4 (NUCLEAR.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        sbyte sVar1;
        VigObject ppcVar2;
        VigObject oVar3;
        VigObject oVar4;

        switch (arg1)
        {
            case 1:
                oVar4 = child2;
                oVar4.maxFullHealth = 300; //not in the original code
                oVar4.maxHalfHealth = 300; //-||-

                while (oVar4 != null)
                {
                    if ((ushort)oVar4.id - 1U < 2)
                        oVar4.flags = oVar4.flags & 0xfffffffb | 2;

                    oVar4 = oVar4.child;
                }

                break;
            case 2:
                if (tags == 1)
                {
                    oVar4 = child2;

                    if (oVar4 != null)
                    {
                        do
                        {
                            if (oVar4.id == 1) break;

                            oVar4 = oVar4.child;
                        } while (oVar4 != null);

                        if (oVar4 != null)
                        {
                            oVar4.flags |= 2;
                            oVar4.FUN_30C20();
                        }
                    }
                }
                else
                {
                    if (tags != 2)
                    {
                        tags = 0;
                        return 0;
                    }

                    oVar4 = child2;

                    if (oVar4 != null)
                    {
                        do
                        {
                            if (oVar4.id == 2) break;

                            oVar4 = oVar4.child;
                        } while (oVar4 != null);

                        if (oVar4 != null)
                        {
                            oVar4.flags |= 2;
                            oVar4.FUN_30C20();
                        }
                    }
                }

                tags = 0;
                break;
            case 8:
                FUN_32B90((uint)arg2);
                break;
            case 9:
                if (arg2 != 0)
                    flags |= 0x1000000;

                break;
            case 20:
                if (id == 113)
                {
                    ppcVar2 = GameManager.instance.FUN_31950(114);

                    if (ppcVar2 != null)
                    {
                        if (ppcVar2.GetType().IsSubclassOf(typeof(VigObject)))
                            ppcVar2.UpdateW(20, arg2 | 0x100);
                    }
                }

                if ((arg2 & 0xff) == 1)
                {
                    if (tags == 0)
                    {
                        oVar4 = child2;

                        while (oVar4 != null)
                        {
                            if (oVar4.id == 1)
                            {
                                oVar4.flags &= 0xfffffffd;
                                oVar4.FUN_30BF0();
                                break;
                            }

                            oVar4 = oVar4.child;
                        }

                        if ((arg2 & 0x100) == 0)
                        {
                            oVar3 = FUN_1F78(1);
                            PDAT_78 = oVar3;
                        }
                    }
                    else
                    {
                        if (tags == 2)
                        {
                            oVar4 = child2;

                            while (oVar4 != null)
                            {
                                if (oVar4.id == 1)
                                {
                                    oVar4.flags &= 0xfffffffd;
                                    oVar4.FUN_30BF0();
                                }
                                else
                                {
                                    if (oVar4.id == 2)
                                    {
                                        oVar4.flags |= 2;
                                        oVar4.FUN_30C20();
                                    }
                                }

                                oVar4 = oVar4.child;
                            }

                            if ((arg2 & 0x100) == 0)
                            {
                                GameManager.instance.FUN_309A0(PDAT_78);
                                PDAT_78 = null;
                                oVar3 = FUN_1F78(1);
                                PDAT_78 = oVar3;
                            }
                        }
                    }

                    oVar4 = PDAT_78;
                    sVar1 = 1;
                }
                else
                {
                    if ((arg2 & 0xff) != 2)
                        return 0;

                    if (tags == 0)
                    {
                        oVar4 = child2;

                        while (oVar4 != null)
                        {
                            if (oVar4.id == 2)
                            {
                                oVar4.flags &= 0xfffffffd;
                                oVar4.FUN_30BF0();
                                break;
                            }

                            oVar4 = oVar4.child;
                        }

                        if ((arg2 & 0x100) == 0)
                        {
                            oVar3 = FUN_1F78(2);
                            PDAT_78 = oVar3;
                        }
                    }
                    else
                    {
                        if (tags == 1)
                        {
                            oVar4 = child2;

                            while (oVar4 != null)
                            {
                                if (oVar4.id == 2)
                                {
                                    oVar4.flags &= 0xfffffffd;
                                    oVar4.FUN_30BF0();
                                }
                                else
                                {
                                    if (oVar4.id == 1)
                                    {
                                        oVar4.flags |= 2;
                                        oVar4.FUN_30C20();
                                    }
                                }

                                oVar4 = oVar4.child;
                            }

                            if ((arg2 & 0x100) == 0)
                            {
                                GameManager.instance.FUN_309A0(PDAT_78);
                                PDAT_78 = null;
                                oVar3 = FUN_1F78(2);
                                PDAT_78 = oVar3;
                            }
                        }
                    }

                    oVar4 = PDAT_78;
                    sVar1 = 2;
                }

                tags = sVar1;

                if (oVar4 != null)
                    GameManager.instance.FUN_30CB0(oVar4, 1800);

                GameManager.instance.FUN_30CB0(this, 1800);
                break;
        }

        return 0;
    }

    private static VigObject FUN_1F78(sbyte param1)
    {
        VigObject oVar1;
        VigObject oVar2;

        oVar1 = GameManager.instance.FUN_30250(GameManager.instance.DAT_1078, 400);
        oVar2 = oVar1.FUN_31DDC();

        if (oVar2 != null)
        {
            Utilities.ParentChildren(oVar2, oVar2);
            oVar2.tags = param1;
            oVar2.FUN_3066C();
        }

        return oVar2;
    }
}
