using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum _STARTSIGN_TYPE
{
    Default, //FUN_3BA4
    Type1 //FUN_3900
}

public class StartSign : Destructible
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public _STARTSIGN_TYPE state;

    public override uint OnCollision(HitDetection hit)
    {
        short sVar1;
        List<VigTuple> ppiVar2;
        VigTuple ppiVar3;
        int iVar4;
        VigObject oVar5;
        bool bVar5;
        Vehicle vVar5;
        VigObject pcVar6;
        VigObject oVar7;

        switch (state)
        {
            case _STARTSIGN_TYPE.Default:
                if (hit.collider1.ReadUInt16(0) == 1 && hit.collider1.ReadUInt16(2) != 0)
                {
                    oVar5 = hit.self;

                    if (oVar5.type == 2)
                    {
                        vVar5 = (Vehicle)oVar5;
                        ppiVar2 = ((OLYMPIC)LevelManager.instance.level).DAT_98;
                        oVar7 = null;

                        for (int i = 0; i < ppiVar2.Count; i++)
                        {
                            ppiVar3 = ppiVar2[i];
                            oVar7 = ppiVar3.vObject;

                            if (oVar7.id == vVar5.id)
                            {
                                if (oVar7.DAT_19 != 34 ||
                                    180 < GameManager.instance.DAT_28 - oVar7.maxHalfHealth)
                                {
                                    GameManager.instance.FUN_300B8(ppiVar2, oVar7);
                                    Destroy(oVar7.gameObject);
                                    oVar7 = null;
                                }

                                break;
                            }

                            oVar7 = null;
                        }

                        if (oVar7 == null)
                        {
                            GameObject obj = new GameObject();
                            oVar7 = obj.AddComponent<VigObject>();

                            if (oVar7 != null)
                            {
                                GameManager.instance.FUN_30080(ppiVar2, oVar7);
                                sVar1 = vVar5.id;
                                oVar7.DAT_19 = 34;
                                oVar7.id = sVar1;
                                oVar7.maxHalfHealth = (ushort)GameManager.instance.DAT_28;
                                iVar4 = GameManager.instance.FUN_1DD9C();
                                GameManager.instance.FUN_1E580(iVar4, vData.sndList, 2, vTransform.position);
                            }
                        }

                        return 0;
                    }
                }

                bVar5 = FUN_32CF0(hit);

                if (bVar5)
                {
                    pcVar6 = child2;

                    while (pcVar6 != null)
                    {
                        if (pcVar6.id == 1)
                            pcVar6.type = 3;

                        pcVar6 = pcVar6.child;
                    }
                }

                break;

            case _STARTSIGN_TYPE.Type1:
                if (hit.collider1.ReadUInt16(0) == 1 && hit.collider1.ReadUInt16(2) != 0)
                {
                    oVar5 = hit.self;

                    if (oVar5.type == 2)
                    {
                        vVar5 = (Vehicle)oVar5;
                        ppiVar2 = ((OLYMPIC)LevelManager.instance.level).DAT_A4;
                        oVar7 = null;

                        for (int i = 0; i < ppiVar2.Count; i++)
                        {
                            ppiVar3 = ppiVar2[i];
                            oVar7 = ppiVar3.vObject;

                            if (oVar7.id == vVar5.id)
                            {
                                if (oVar7.DAT_19 != 97 || 
                                    180 < GameManager.instance.DAT_28 - oVar7.maxHalfHealth)
                                {
                                    GameManager.instance.FUN_300B8(ppiVar2, oVar7);
                                    Destroy(oVar7.gameObject);
                                    oVar7 = null;
                                }

                                break;
                            }

                            oVar7 = null;
                        }

                        if (oVar7 == null)
                        {
                            GameObject obj = new GameObject();
                            oVar7 = obj.AddComponent<VigObject>();

                            if (oVar7 != null)
                            {
                                GameManager.instance.FUN_30080(ppiVar2, oVar7);
                                sVar1 = vVar5.id;
                                oVar7.DAT_19 = 97;
                                oVar7.id = sVar1;
                                oVar7.maxHalfHealth = (ushort)GameManager.instance.DAT_28;
                                iVar4 = GameManager.instance.FUN_1DD9C();
                                GameManager.instance.FUN_1E580(iVar4, vData.sndList, 2, vTransform.position);
                            }
                        }

                        return 0;
                    }
                }

                bVar5 = FUN_32CF0(hit);

                if (bVar5)
                {
                    pcVar6 = child2;

                    while (pcVar6 != null)
                    {
                        if (pcVar6.id == 1)
                            pcVar6.type = 3;

                        pcVar6 = pcVar6.child;
                    }
                }

                break;
        }

        return 0;
    }

    //FUN_3BA4 (OLYMPIC.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        bool bVar5;
        VigObject pcVar6;

        switch (state)
        {
            case _STARTSIGN_TYPE.Default:
                if (arg1 < 4)
                {
                    if (arg1 == 1)
                    {
                        pcVar6 = child2;

                        while (pcVar6 != null)
                        {
                            if (pcVar6.id == 1)
                                pcVar6.type = 3;

                            pcVar6 = pcVar6.child;
                        }

                        if (id == 40)
                            state = _STARTSIGN_TYPE.Type1;
                    }
                }
                else
                {
                    if (arg1 == 8)
                    {
                        bVar5 = FUN_32B90((uint)arg2);

                        if (bVar5)
                        {
                            pcVar6 = child2;

                            while (pcVar6 != null)
                            {
                                if (pcVar6.id == 1)
                                    pcVar6.type = 3;

                                pcVar6 = pcVar6.child;
                            }
                        }
                    }
                }

                break;

            case _STARTSIGN_TYPE.Type1:
                if (arg1 < 4)
                {
                    if (arg1 == 1)
                    {
                        pcVar6 = child2;

                        while(pcVar6 != null)
                        {
                            if (pcVar6.id == 1)
                                pcVar6.type = 3;

                            pcVar6 = pcVar6.child;
                        }
                    }
                }
                else
                {
                    if (arg1 == 8)
                    {
                        bVar5 = FUN_32B90((uint)arg2);

                        if (bVar5)
                        {
                            pcVar6 = child2;

                            while (pcVar6 != null)
                            {
                                if (pcVar6.id == 1)
                                    pcVar6.type = 3;

                                pcVar6 = pcVar6.child;
                            }
                        }
                    }
                }

                break;
        }
        
        return 0;
    }
}
