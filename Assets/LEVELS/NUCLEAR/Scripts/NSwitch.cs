using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NSwitch : VigObject
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
        VigObject ppcVar4;
        int iVar5;
        int iVar6;
        VigObject oVar6;
        XOBF_DB dbVar6;
        int iVar7;

        if (hit.self.type == 2 && (flags & 0x1000000) == 0 && tags != 0)
        {
            ppcVar4 = GameManager.instance.FUN_31950(113);

            if (ppcVar4 == null || (ppcVar4.flags & 0x1000000) != 0)
            {
                ppcVar4 = GameManager.instance.FUN_31950(114);

                if (ppcVar4 == null || (ppcVar4.flags & 0x1000000) != 0)
                    ppcVar4 = null;
            }

            if (tags == 1)
            {
                iVar5 = GameManager.instance.FUN_1DD9C();
                dbVar6 = vData;
                iVar7 = 6;
            }
            else
            {
                iVar5 = GameManager.instance.FUN_1DD9C();
                dbVar6 = vData;
                iVar7 = 5;
            }

            GameManager.instance.FUN_1E580(iVar5, dbVar6.sndList, iVar7, vTransform.position);

            if (ppcVar4 != null)
            {
                if (ppcVar4.GetType().IsSubclassOf(typeof(VigObject)))
                    ppcVar4.UpdateW(20, tags);
            }

            oVar6 = child2;

            if (oVar6 != null)
            {
                do
                {
                    if (oVar6.id == tags) break;

                    oVar6 = oVar6.child;
                } while (oVar6 != null);

                if (oVar6 != null)
                {
                    oVar6.flags |= 2;
                    oVar6.FUN_30C20();
                }
            }

            iVar6 = (int)GameManager.FUN_2AC5C();
            GameManager.instance.FUN_30CB0(this, (iVar6 * 300 >> 15) + 600);
            tags = 0;
            return 0;
        }

        if (hit.self.type != 8)
            return 0;

        FUN_32B90(hit.self.maxHalfHealth);
        return 0;
    }

    //FUN_1564 (NUCLEAR.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        sbyte sVar1;
        sbyte sVar2;
        byte bVar3;
        int iVar6;
        VigObject oVar6;

        switch (arg1)
        {
            default:
                return 0;
            case 1:
                tags = 2;
                bVar3 = (byte)GameManager.FUN_2AC5C();
                oVar6 = child2;
                DAT_19 = (byte)(bVar3 & 1);

                while (oVar6 != null)
                {
                    oVar6.type = 3;
                    oVar6.flags = oVar6.flags & 0xfffffffb | 2;
                    oVar6 = oVar6.child;
                }

                flags &= 0xfffffffb;
                iVar6 = (int)GameManager.FUN_2AC5C();
                iVar6 = iVar6 * 180 >> 15;
                break;
            case 2:
                flags &= 0xfeffffff;
                oVar6 = GameManager.instance.FUN_31950(113);

                if (oVar6 == null || (oVar6.flags & 0x1000000) != 0)
                {
                    oVar6 = GameManager.instance.FUN_31950(114);

                    if (oVar6 == null || (oVar6.flags & 0x1000000) != 0)
                    {
                        oVar6 = child2;

                        if (oVar6 != null)
                        {
                            do
                            {
                                if (oVar6.id == tags)
                                {
                                    oVar6.flags |= 2;
                                    oVar6.FUN_30C20();
                                }

                                oVar6 = oVar6.child;
                            } while (oVar6 != null);

                            tags = 0;
                            return 0;
                        }

                        tags = 0;
                        return 0;
                    }
                }

                sVar1 = tags;

                if (sVar1 == 0)
                {
                    bVar3 = (byte)(DAT_19 ^ 1);
                    DAT_19 = bVar3;

                    if (bVar3 == 0)
                        sVar2 = 2;
                    else
                        sVar2 = 1;

                    tags = sVar2;
                    oVar6 = child2;

                    while (oVar6 != null)
                    {
                        if (oVar6.id == tags)
                        {
                            oVar6.flags &= 0xfffffffd;
                            oVar6.FUN_30BF0();
                        }

                        oVar6 = oVar6.child;
                    }
                }
                else
                {
                    if (-1 < sVar1 && sVar1 < 3)
                    {
                        oVar6 = child2;

                        while (oVar6 != null)
                        {
                            if (oVar6.id == tags)
                            {
                                oVar6.flags |= 2;
                                oVar6.FUN_30C20();
                            }

                            oVar6 = oVar6.child;
                        }

                        tags = 0;
                    }
                }

                iVar6 = (int)GameManager.FUN_2AC5C();
                iVar6 = (iVar6 * 300 >> 15) + 600;
                break;
            case 8:
                FUN_32B90((uint)arg2);
                return 0;
        }

        GameManager.instance.FUN_30CB0(this, iVar6);
        return 0;
    }
}
