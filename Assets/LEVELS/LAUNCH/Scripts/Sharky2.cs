using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sharky2 : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_3B5C (LAUNCH.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        RSEG_DB rVar1;
        VigTuple ppiVar1;
        bool bVar2;
        List<VigTuple> ppiVar4;
        int iVar5;
        JUNC_DB jVar5;
        int iVar6;
        JUNC_DB jVar6;
        JUNC_DB puVar7;
        JUNC_DB jVar8;
        JUNC_DB jVar11;
        int iVar12;
        JUNC_DB jVar12;
        int iVar13;
        Sharky oVar13;
        VigObject oVar14;

        if (arg1 == 2)
        {
            ppiVar4 = GameManager.instance.worldObjs;

            for (int i = 0; i < ppiVar4.Count; i++)
            {
                ppiVar1 = ppiVar4[i];
                oVar14 = ppiVar1.vObject;

                if (oVar14.type == 2 && oVar14.id < 0 && oVar14.maxHalfHealth != 0)
                {
                    iVar5 = GameManager.instance.terrain.FUN_1B750((uint)oVar14.vTransform.position.x, (uint)oVar14.vTransform.position.z);

                    if (0x2e6800 < iVar5)
                    {
                        iVar13 = 0x7fffffff;
                        iVar12 = 0;
                        jVar5 = null;

                        if (0 < LevelManager.instance.DAT_1184)
                        {
                            do
                            {
                                jVar11 = LevelManager.instance.juncList[iVar12];

                                if (jVar11.DAT_12 - 16 < 4)
                                {
                                    iVar6 = Utilities.FUN_29F6C(oVar14.vTransform.position, jVar11.DAT_00);

                                    if (iVar6 < iVar13)
                                    {
                                        jVar5 = jVar11;
                                        iVar13 = iVar6;
                                    }
                                }

                                iVar12++;
                            } while (iVar12 < LevelManager.instance.DAT_1184);
                        }

                        if (iVar5 != 0)
                        {
                            oVar13 = GameManager.instance.FUN_30250(GameManager.instance.worldObjs, jVar5.DAT_12) as Sharky;

                            if (oVar13 != null && oVar13.tags < 3)
                            {
                                if (oVar13.DAT_19 == 0)
                                {
                                    jVar11 = oVar13.DAT_84_2;
                                    jVar12 = jVar11.DAT_1C[0].DAT_00[1];
                                }
                                else
                                {
                                    jVar12 = oVar13.DAT_84_2;
                                    jVar11 = jVar12.DAT_1C[jVar12.DAT_11 - 1].DAT_00[0];
                                }

                                bVar2 = jVar12 == jVar5;

                                if (bVar2)
                                    oVar13.tags = 2;
                                else
                                {
                                    jVar6 = jVar11;
                                    jVar8 = jVar12;

                                    if (jVar11 == jVar5)
                                        oVar13.tags = 2;
                                    else
                                    {
                                        while (!(bVar2 = jVar6.DAT_11 == 1) && jVar8.DAT_11 != 1)
                                        {
                                            rVar1 = jVar6.DAT_1C[1];
                                            jVar8 = jVar8.DAT_1C[0].DAT_00[1];
                                            bVar2 = jVar8 == jVar5;

                                            if (bVar2) break;
                                            else
                                            {
                                                jVar6 = rVar1.DAT_00[0];

                                                if (rVar1.DAT_00[0] == jVar5) break;
                                            }
                                        }

                                        oVar13.tags = 1;
                                    }
                                }

                                oVar13.DAT_80 = oVar14;
                                oVar13.DAT_19 = (byte)(bVar2 ? 1 : 0);

                                if (bVar2)
                                    oVar13.DAT_84_2 = jVar12;
                                else
                                    oVar13.DAT_84_2 = jVar11;

                                puVar7 = oVar13.DAT_84_2;
                                oVar13.physics1.Z = puVar7.DAT_00.x;
                                oVar13.physics1.W = puVar7.DAT_00.y;
                                oVar13.physics2.X = puVar7.DAT_00.z;
                            }
                        }
                    }
                }
            }

            GameManager.instance.FUN_30CB0(this, 60);
        }

        return 0;
    }
}
