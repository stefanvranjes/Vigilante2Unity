using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public Beam2[] DAT_84_2; //0x84
    public VigObject DAT_C0; //0xC0
    public MegaCollider DAT_C4; //0xC4
    public int DAT_C8; //0xC8
    public int DAT_CC; //0xCC
    public Vector3Int DAT_D0; //0xD0

    //FUN_234 (CORSAIR.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        Beam2 piVar1;
        sbyte sVar2;
        VigTransform puVar3;
        ConfigContainer ccVar4;
        VigObject oVar4;
        int iVar5;
        int iVar6;
        Beam2 bVar6;
        VigTuple tVar6;
        int iVar7;
        int iVar8;
        VigObject oVar8;
        Vehicle vVar8;
        Vector3Int local_48;
        Vector3Int local_18;

        if (arg1 == 2)
        {
            sVar2 = tags;

            if (sVar2 == 1)
            {
                tags = 2;
                GameManager.instance.FUN_30CB0(this, 5);
                DAT_CC = 0;
            }
            else
            {
                if (sVar2 < 2)
                {
                    if (sVar2 == 0)
                    {
                        iVar6 = DAT_CC;
                        DAT_84_2[iVar6].flags &= 0xfffffffd;

                        if (iVar6 != 0)
                        {
                            DAT_C0.FUN_2CCBC();
                            ccVar4 = DAT_84_2[iVar6].FUN_2C5F4(0x8000);
                            Utilities.FUN_2CA94(DAT_84_2[iVar6], ccVar4, DAT_C0);
                            Utilities.ParentChildren(DAT_84_2[iVar6], DAT_84_2[iVar6]);
                        }

                        iVar6 = DAT_CC + 1;
                        DAT_CC = iVar6;

                        if (iVar6 < 15)
                            GameManager.instance.FUN_30CB0(this, 2);
                        else
                        {
                            GameManager.instance.FUN_30CB0(this, 60);
                            tags = 1;
                        }
                    }
                }
                else
                {
                    if (sVar2 == 2)
                    {
                        tags = 3;
                        GameManager.instance.FUN_30CB0(this, 30);
                        DAT_CC = 0;
                        DAT_C8 = 0x1000;
                    }
                    else
                    {
                        if (sVar2 == 3)
                        {
                            FUN_30BA8();
                            oVar4 = FUN_2CCBC();
                            GameManager.instance.FUN_308C4(oVar4);
                        }
                    }
                }
            }
        }
        else
        {
            if (arg1 < 3)
            {
                if (arg1 == 0)
                {
                    local_48 = DAT_D0;
                    puVar3 = GameManager.instance.FUN_2CDF4(this);
                    iVar8 = 1;
                    bVar6 = DAT_84_2[0];
                    bVar6.vTransform = puVar3;

                    do
                    {
                        bVar6 = DAT_84_2[iVar8];
                        bVar6.vTransform = puVar3;
                        bVar6.vTransform.position = Utilities.FUN_24148(puVar3, local_48);
                        local_48.x += DAT_D0.x;
                        local_48.y += DAT_D0.y;
                        local_48.z += DAT_D0.z;
                        iVar8++;
                        bVar6.flags ^= 0x20;
                    } while (iVar8 < 15);

                    if (tags == 3)
                    {
                        iVar7 = 0;
                        local_18 = new Vector3Int();
                        local_18.x = DAT_C8 - 129;
                        DAT_C8 = local_18.x;
                        local_18.y = DAT_C8;
                        local_18.z = 0x1000;
                        FUN_B90(ref vTransform.rotation, local_18);

                        do
                        {
                            piVar1 = DAT_84_2[iVar7];
                            iVar7++;
                            FUN_B90(ref piVar1.vTransform.rotation, local_18);
                        } while (iVar7 < 15);
                    }

                    vVar8 = (Vehicle)DAT_80;
                    iVar7 = vVar8.physics2.X;
                    iVar6 = iVar7;

                    if (iVar7 < 0)
                        iVar6 = iVar7 + 3;

                    iVar5 = vVar8.physics2.Y;
                    vVar8.physics2.X = iVar7 - (iVar6 >> 2);
                    iVar6 = iVar5;

                    if (iVar5 < 0)
                        iVar6 = iVar5 + 3;

                    iVar7 = vVar8.physics2.Z;
                    vVar8.physics2.Y = iVar5 - (iVar6 >> 2);
                    iVar6 = iVar7;

                    if (iVar7 < 0)
                        iVar6 = iVar7 + 3;

                    iVar5 = vVar8.physics1.X;
                    vVar8.physics2.Z = iVar7 - (iVar6 >> 2);
                    iVar6 = iVar5;

                    if (iVar5 < 0)
                        iVar6 = iVar5 + 3;

                    iVar7 = vVar8.physics1.Y;
                    vVar8.physics1.X = iVar5 - (iVar6 >> 2);
                    iVar6 = iVar7;

                    if (iVar7 < 0)
                        iVar6 = iVar7 + 3;

                    iVar5 = vVar8.physics1.Z;
                    vVar8.physics1.Y = iVar7 - (iVar6 >> 2);
                    iVar6 = iVar5;

                    if (iVar5 < 0)
                        iVar6 = iVar5 + 3;

                    vVar8.physics1.Z = iVar5 - (iVar6 >> 2);
                }
            }
            else
            {
                iVar6 = 0;

                if (arg1 == 4)
                {
                    oVar8 = Utilities.FUN_2CD78(DAT_C4);

                    do
                    {
                        GameManager.instance.FUN_309A0(DAT_84_2[iVar6]);
                        DAT_84_2[iVar6] = null;
                        iVar6++;
                    } while (iVar6 < 15);

                    if (oVar8 != null)
                    {
                        tVar6 = GameManager.instance.FUN_30134(GameManager.instance.worldObjs, oVar8);

                        if (tVar6 != null && DAT_C4.maxHalfHealth == 0)
                            DAT_C4.FUN_3A368();
                    }

                    GameManager.instance.DAT_1084--;
                }
            }
        }

        return 0;
    }

    //FUN_B90 (CORSAIR.DLL)
    private static void FUN_B90(ref Matrix3x3 param1, Vector3Int param2)
    {
        int iVar1;
        int iVar2;
        int iVar3;

        iVar1 = param2.x;
        iVar2 = param2.y;
        iVar3 = param2.z;
        param1.V00 = (short)(param1.V00 * iVar1 >> 12);
        param1.V01 = (short)(param1.V01 * iVar2 >> 12);
        param1.V02 = (short)(param1.V02 * iVar3 >> 12);
        param1.V10 = (short)(param1.V10 * iVar1 >> 12);
        param1.V11 = (short)(param1.V11 * iVar2 >> 12);
        param1.V12 = (short)(param1.V12 * iVar3 >> 12);
        param1.V20 = (short)(param1.V20 * iVar1 >> 12);
        param1.V21 = (short)(param1.V21 * iVar2 >> 12);
        param1.V22 = (short)(param1.V22 * iVar3 >> 12);
    }
}
