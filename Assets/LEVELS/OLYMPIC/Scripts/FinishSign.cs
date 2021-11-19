using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum _FINISHSIGN_TYPE
{
    Default, //FUN_41B0
    Type1 //FUN_3E58
}

public class FinishSign : Destructible
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public _FINISHSIGN_TYPE state;

    public override uint OnCollision(HitDetection hit)
    {
        List<VigTuple> ppiVar1;
        VigTuple ppiVar2;
        sbyte sVar3;
        VigObject oVar4;
        int iVar5;
        Pickup pVar5;
        bool bVar6;
        OLYMPIC oVar6;
        int iVar7;
        VigObject oVar7;
        VigObject pcVar8;
        VigObject oVar9;
        Vehicle vVar9;
        VigObject oVar10;

        switch (state)
        {
            case _FINISHSIGN_TYPE.Default:
                oVar6 = (OLYMPIC)LevelManager.instance.level;

                if (hit.collider1.ReadUInt16(0) == 1 && hit.collider1.ReadUInt16(2) != 0)
                {
                    oVar9 = hit.self;

                    if (oVar9.type == 2)
                    {
                        vVar9 = (Vehicle)oVar9;
                        ppiVar1 = oVar6.DAT_98;

                        for (int i = 0; i < ppiVar1.Count; i++)
                        {
                            ppiVar2 = ppiVar1[i];

                            if (ppiVar2.vObject.id == vVar9.id)
                            {
                                oVar4 = GameManager.instance.FUN_318D0(81);
                                oVar10 = ppiVar2.vObject;

                                if (oVar10.DAT_19 == id)
                                {
                                    iVar7 = GameManager.instance.DAT_28 - oVar10.maxHalfHealth;

                                    if (oVar6.DAT_D8 <= iVar7)
                                    {
                                        sVar3 = 3;
                                        oVar4.tags = sVar3;
                                        iVar5 = GameManager.instance.FUN_1DD9C();
                                        GameManager.instance.FUN_1E580(iVar5, vData.sndList, 1, vTransform.position);
                                    }
                                    else
                                    {
                                        oVar6.DAT_D8 = iVar7;
                                        oVar4.tags = 1;
                                        iVar5 = GameManager.instance.FUN_1DD9C();
                                        GameManager.instance.FUN_1E580(iVar5, vData.sndList, 0, vTransform.position);
                                        oVar7 = GameManager.instance.FUN_30250(GameManager.instance.DAT_1078, 400);
                                        pVar5 = LevelManager.instance.FUN_4AE08(0x80000000, oVar7.screen);
                                        GameManager.instance.FUN_30CB0(pVar5, 1200);
                                        oVar7 = GameManager.instance.FUN_30250(GameManager.instance.DAT_1078, 401);
                                        pVar5 = LevelManager.instance.FUN_4AE08(0xfe000000, oVar7.screen);
                                        GameManager.instance.FUN_30CB0(pVar5, 1200);
                                        oVar7 = GameManager.instance.FUN_30250(GameManager.instance.DAT_1078, 402);
                                        pVar5 = LevelManager.instance.FUN_4AE08(0xfe000000, oVar7.screen);
                                        GameManager.instance.FUN_30CB0(pVar5, 1200);

                                        if (vVar9.id < 0)
                                        {
                                            //FUN_326B0
                                            oVar4.DAT_19 = 0;
                                            goto LAB_43B4;
                                        }
                                    }
                                }
                                else
                                {
                                    sVar3 = 2;
                                    oVar4.tags = sVar3;
                                    iVar5 = GameManager.instance.FUN_1DD9C();
                                    GameManager.instance.FUN_1E580(iVar5, vData.sndList, 1, vTransform.position);
                                }

                                oVar4.DAT_19 = 0;
                                LAB_43B4:
                                GameManager.instance.FUN_30CB0(oVar4, 6);
                                GameManager.instance.FUN_300B8(oVar6.DAT_98, oVar10);
                                Destroy(oVar10.gameObject);
                                return 0;
                            }
                        }

                        return 0;
                    }
                }

                bVar6 = FUN_32CF0(hit);

                if (bVar6)
                {
                    pcVar8 = child2;

                    while (pcVar8 != null)
                    {
                        if (pcVar8.id == 1)
                            pcVar8.type = 3;

                        pcVar8 = pcVar8.child;
                    }
                }

                break;

            case _FINISHSIGN_TYPE.Type1:
                oVar6 = (OLYMPIC)LevelManager.instance.level;

                if (hit.collider1.ReadUInt16(0) == 1 && hit.collider1.ReadUInt16(2) != 0)
                {
                    oVar9 = hit.self;

                    if (oVar9.type == 2)
                    {
                        vVar9 = (Vehicle)oVar9;
                        ppiVar1 = oVar6.DAT_A4;

                        for (int i = 0; i < ppiVar1.Count; i++)
                        {
                            ppiVar2 = ppiVar1[i];

                            if (ppiVar2.vObject.id == vVar9.id)
                            {
                                oVar4 = GameManager.instance.FUN_318D0(81);
                                oVar10 = ppiVar2.vObject;

                                if (oVar10.DAT_19 == 73 || oVar10.DAT_19 == 105)
                                {
                                    if (oVar10.DAT_19 != 73)
                                        sVar3 = 2;

                                    iVar7 = GameManager.instance.DAT_28 - oVar10.maxHalfHealth;

                                    if (oVar6.DAT_E0 <= iVar7)
                                    {
                                        sVar3 = 3;
                                        oVar4.tags = sVar3;
                                        iVar5 = GameManager.instance.FUN_1DD9C();
                                        GameManager.instance.FUN_1E580(iVar5, vData.sndList, 1, vTransform.position);
                                    }
                                    else
                                    {
                                        oVar6.DAT_E0 = iVar7;
                                        oVar4.tags = 1;
                                        iVar5 = GameManager.instance.FUN_1DD9C();
                                        GameManager.instance.FUN_1E580(iVar5, vData.sndList, 0, vTransform.position);
                                        oVar7 = GameManager.instance.FUN_30250(GameManager.instance.DAT_1078, 400);
                                        pVar5 = LevelManager.instance.FUN_4AE08(0x80000000, oVar7.screen);
                                        GameManager.instance.FUN_30CB0(pVar5, 1200);
                                        oVar7 = GameManager.instance.FUN_30250(GameManager.instance.DAT_1078, 401);
                                        pVar5 = LevelManager.instance.FUN_4AE08(0xfe000000, oVar7.screen);
                                        GameManager.instance.FUN_30CB0(pVar5, 1200);
                                        oVar7 = GameManager.instance.FUN_30250(GameManager.instance.DAT_1078, 402);
                                        pVar5 = LevelManager.instance.FUN_4AE08(0xfe000000, oVar7.screen);
                                        GameManager.instance.FUN_30CB0(pVar5, 1200);

                                        if (vVar9.id < 0)
                                        {
                                            //FUN_326B0
                                            oVar4.DAT_19 = 0;
                                            goto LAB_405C;
                                        }
                                    }
                                }
                                else
                                {
                                    sVar3 = 2;
                                    oVar4.tags = sVar3;
                                    iVar5 = GameManager.instance.FUN_1DD9C();
                                    GameManager.instance.FUN_1E580(iVar5, vData.sndList, 1, vTransform.position);
                                }

                                oVar4.DAT_19 = 0;
                                LAB_405C:
                                GameManager.instance.FUN_30CB0(oVar4, 6);
                                GameManager.instance.FUN_300B8(oVar6.DAT_A4, oVar10);
                                Destroy(oVar10.gameObject);
                                return 0;
                            }
                        }

                        return 0;
                    }
                }
                else
                {
                    bVar6 = FUN_32CF0(hit);

                    if (bVar6)
                    {
                        oVar7 = child2;

                        while(oVar7 != null)
                        {
                            if (oVar7.id == 1)
                                oVar7.type = 3;

                            oVar7 = oVar7.child;
                        }
                    }
                }

                break;
        }

        return 0;
    }

    //FUN_41B0
    public override uint UpdateW(int arg1, int arg2)
    {
        bool bVar6;
        bool bVar7;
        VigObject oVar7;
        VigObject pcVar8;

        switch (state)
        {
            case _FINISHSIGN_TYPE.Default:
                if (arg1 < 4)
                {
                    if (arg1 == 1)
                    {
                        pcVar8 = child2;

                        while (pcVar8 != null)
                        {
                            if (pcVar8.id == 1)
                                pcVar8.type = 3;

                            pcVar8 = pcVar8.child;
                        }

                        if (id == 41)
                            state = _FINISHSIGN_TYPE.Type1;
                    }
                }
                else
                {
                    if (arg1 == 8)
                    {
                        bVar6 = FUN_32B90((uint)arg2);

                        if (bVar6)
                        {
                            pcVar8 = child2;

                            while (pcVar8 != null)
                            {
                                if (pcVar8.id == 1)
                                    pcVar8.type = 3;

                                pcVar8 = pcVar8.child;
                            }
                        }
                    }
                }

                break;

            case _FINISHSIGN_TYPE.Type1:
                if (arg1 < 4)
                {
                    if (arg1 == 1)
                    {
                        oVar7 = child2;

                        while (oVar7 != null)
                        {
                            if (oVar7.id == 1)
                                oVar7.type = 3;

                            oVar7 = oVar7.child;
                        }
                    }
                }
                else
                {
                    if (arg1 == 8)
                    {
                        bVar7 = FUN_32B90((uint)arg2);

                        if (bVar7)
                        {
                            oVar7 = child2;

                            while (oVar7 != null)
                            {
                                if (oVar7.id == 1)
                                    oVar7.type = 3;

                                oVar7 = oVar7.child;
                            }
                        }
                    }
                }

                break;
        }

        return 0;
    }
}
