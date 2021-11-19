using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reactor : VigObject
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
        bool bVar6;

        if (hit.self.type != 8)
            return 0;

        bVar6 = FUN_32B90(hit.self.maxHalfHealth);

        if (!bVar6)
        {
            if (DAT_18 != 0)
                GameManager.instance.FUN_1E30C(DAT_18, ((maxFullHealth - maxHalfHealth) * 4999) / maxFullHealth + 2298);

            DAT_19 = (byte)(((maxFullHealth - maxHalfHealth) * 5) / maxFullHealth);
        }

        return 0;
    }

    //FUN_2D04 (NUCLEAR.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        VigTuple ppiVar2;
        List<VigTuple> ppiVar3;
        sbyte sVar4;
        ForkLift3 ppcVar5;
        int iVar6;
        bool bVar6;
        int iVar7;
        VigObject oVar7;
        Ballistic puVar8;

        switch (arg1)
        {
            case 0:
                if (arg2 != 0)
                {
                    iVar6 = (int)GameManager.instance.FUN_1E7A8(screen);

                    if (iVar6 == 0)
                    {
                        if (DAT_18 == 0)
                        {
                            GameManager.instance.FUN_1DE78(0);
                            DAT_18 = 0;
                        }
                    }
                    else
                    {
                        if (DAT_18 == 0)
                        {
                            sVar4 = (sbyte)GameManager.instance.FUN_1DD9C();
                            DAT_18 = sVar4;
                            GameManager.instance.FUN_1E098(sVar4, vData.sndList, 0, (uint)iVar6, true);
                            GameManager.instance.FUN_1E30C(DAT_18, ((maxFullHealth - maxHalfHealth) * 4999) / maxFullHealth + 2298);
                        }
                        else
                            GameManager.instance.FUN_1E2C8(DAT_18, (uint)iVar6);
                    }
                }

                child2.DAT_4A -= DAT_19;
                break;
            case 1:
                DAT_19 = 0;
                flags |= 0x80;
                break;
            case 2:
                sVar4 = tags;
                tags = (sbyte)(sVar4 + 1);

                if (sVar4 == 0)
                {
                    ppiVar3 = GameManager.instance.worldObjs;

                    for (int i = 0; i < ppiVar3.Count; i++)
                    {
                        ppiVar2 = ppiVar3[i];

                        if (ppiVar2.vObject.type == 2 && ppiVar2.vObject.maxHalfHealth != 0)
                            ((Vehicle)ppiVar2.vObject).FUN_39BC4();
                    }

                    ppcVar5 = LevelManager.instance.xobfList[42].ini.FUN_2C17C(27, typeof(ForkLift3), 8) as ForkLift3;
                    Utilities.ParentChildren(ppcVar5, ppcVar5);
                    ppcVar5.type = 8;
                    ppcVar5.flags = 0x314;
                    ppcVar5.screen = screen;
                    ppcVar5.maxHalfHealth = 500;
                    ppcVar5.FUN_3066C();
                    ppcVar5.DAT_58 = ppcVar5.vCollider.reader.ReadInt32(16);
                    UIManager.instance.FUN_4E338(new Color32(0xff, 0x80, 0x40, 24));
                    GameManager.instance.FUN_30CB0(this, 60);
                }
                else
                {
                    if (sVar4 == 1)
                    {
                        iVar7 = 53;

                        if (id == 98)
                            iVar7 = 51;

                        oVar7 = GameManager.instance.FUN_318D0(iVar7);
                        oVar7.FUN_4DC94();
                        iVar7 = 54;

                        if (id == 98)
                            iVar7 = 52;

                        oVar7 = GameManager.instance.FUN_318D0(iVar7);
                        oVar7.FUN_4DC94();
                    }
                }

                break;
            case 8:
                bVar6 = FUN_32B90((uint)arg2);

                if (!bVar6)
                {
                    if (DAT_18 != 0)
                        GameManager.instance.FUN_1E30C(DAT_18, ((maxFullHealth - maxHalfHealth) * 4999) / maxFullHealth + 2298);

                    DAT_19 = (byte)(((maxFullHealth - maxHalfHealth) * 5) / maxFullHealth);
                }

                break;
            case 9:
                if (arg2 != 0)
                {
                    FUN_30BA8();
                    GameManager.instance.FUN_1DE78(DAT_18);
                    iVar7 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E14C(iVar7, LevelManager.instance.xobfList[42].sndList, 1);
                    iVar7 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E14C(iVar7, LevelManager.instance.xobfList[42].sndList, 2);
                    UIManager.instance.FUN_4E338(new Color32(0xff, 0xff, 0x80, 8));
                    puVar8 = LevelManager.instance.xobfList[42].ini.FUN_2C17C(33, typeof(Ballistic), 8) as Ballistic;
                    puVar8.screen = screen;
                    puVar8.flags = 0x24;
                    puVar8.FUN_3066C();
                    GameManager.instance.FUN_30CB0(this, 30);
                }

                break;
        }

        return 0;
    }
}
