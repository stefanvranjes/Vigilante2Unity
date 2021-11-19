using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchVehicle : Destructible
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
        int iVar3;
        VigObject oVar4;
        Vehicle vVar4;
        Vector3Int local_18;
        Vector3Int local_28;
        Vector3Int local_78;
        HitDetection auStack96;

        oVar4 = hit.self;

        if (oVar4.type == 2)
        {
            vVar4 = (Vehicle)oVar4;

            if ((flags & 0x80) != 0 && (tags == 0 || tags == 8) && 
                vVar4.vTransform.position.z < vTransform.position.z)
            {
                local_18 = new Vector3Int(0, 0, physics1.W * 128);
                local_28 = new Vector3Int(0, 0, local_18.z);

                if (tags == 0)
                    local_28.z = physics1.W * -128;

                auStack96 = new HitDetection(null);
                GameManager.instance.FUN_2FB70(this, hit, auStack96);
                local_78 = new Vector3Int();
                local_78.x = auStack96.position.x / 2;
                local_78.y = auStack96.position.y / 2;
                local_78.z = auStack96.position.z / 2;
                local_78 = Utilities.FUN_24148(vVar4.vTransform, local_78);
                vVar4.FUN_2B370(local_28, local_78);
                iVar3 = -15;

                if (vVar4.id < 0)
                    iVar3 = -50;

                vVar4.FUN_3A064(iVar3, vTransform.position, true);
                return 0;
            }

            FUN_32CF0(hit);
            return 0;
        }

        if (oVar4.type != 8)
            return 0;

        if (2 < tags - 1)
        {
            FUN_32B90(oVar4.maxHalfHealth);
            return 0;
        }

        return 0;
    }

    //FUN_22EC (LAUNCH.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        sbyte sVar1;
        int iVar2;
        int iVar3;
        uint uVar3;
        int iVar4;
        Vector3Int local_68;

        switch (arg1)
        {
            case 0:
                if (DAT_18 != 0 && arg2 != 0)
                {
                    uVar3 = GameManager.instance.FUN_1E7A8(vTransform.position);
                    GameManager.instance.FUN_1E2C8(DAT_18, uVar3);
                }

                if (tags == 0 || tags == 8)
                {
                    iVar2 = physics1.W + 25;
                    iVar4 = 3051;

                    if (iVar2 < 3051)
                        iVar4 = iVar2;

                    physics1.W = iVar4;
                }

                if (vTransform.position.z < 0x4b82001)
                {
                    vTransform.position.y = 0x28b000;
                    local_68 = new Vector3Int(0, -0x1000, 0);
                }
                else
                {
                    local_68 = new Vector3Int();
                    iVar3 = FUN_2CFBC(vTransform.position, ref local_68);
                    vTransform.position.y = iVar3;
                    local_68 = Utilities.VectorNormal(local_68);
                }

                iVar4 = vTransform.rotation.V21 * local_68.y -
                        vTransform.rotation.V11 * local_68.z;

                if (iVar4 < 0)
                    iVar4 += 4095;

                iVar4 >>= 12;
                iVar2 = iVar4;

                if (iVar4 < 0)
                    iVar2 = -iVar4;

                if (2 < iVar2)
                {
                    if (iVar4 < 0)
                        iVar4 += 15;

                    FUN_24700((short)(iVar4 >> 4), 0, 0);
                    vTransform.rotation = Utilities.MatrixNormal(vTransform.rotation);
                }

                if (physics2.X == 0)
                    iVar4 = physics1.W;
                else
                    iVar4 = -physics1.W;

                iVar4 = vTransform.position.z + iVar4;
                vTransform.position.z = iVar4;

                if (tags == 0)
                {
                    if (0x4b73000 < iVar4)
                        return 0;

                    tags = 1;
                    physics1.W = 0;
                    physics2.X ^= 1;
                    GameManager.instance.FUN_1DE78(DAT_18);
                    DAT_18 = 0;
                    FUN_30BA8();
                    GameManager.instance.FUN_30CB0(this, 60);
                    flags &= 0xfeffffff;
                }
                else
                {
                    if (tags != 8)
                        return 0;

                    if (0x511cfff < iVar4)
                    {
                        tags = 9;
                        physics1.W = 0;
                        FUN_30BA8();
                        return 0;
                    }
                }

                break;
            case 1:
                tags = 0;
                physics2.X = 1;
                physics1.W = 0;
                flags |= 8;
                break;
            case 2:
                sVar1 = tags;

                if (sVar1 != 2)
                {
                    if (sVar1 < 3)
                    {
                        if (sVar1 != 1)
                            return 0;

                        if ((flags & 0x1000000) == 0)
                        {
                            iVar3 = GameManager.instance.FUN_1DD9C();
                            GameManager.instance.FUN_1E14C(iVar3, vData.sndList, 6);
                            FUN_1FCC();
                            iVar4 = (int)GameManager.FUN_2AC5C();
                            GameManager.instance.FUN_30CB0(this, (iVar4 * 300 >> 15) + 600);
                            return 0;
                        }

                        GameManager.instance.FUN_30CB0(this, 210);
                        tags = 2;
                        iVar3 = GameManager.instance.FUN_1DD9C();
                        GameManager.instance.FUN_1E14C(iVar3, vData.sndList, 4);
                        return 0;
                    }

                    if (sVar1 == 3)
                    {
                        FUN_30B78();
                        tags = 8;
                        return 0;
                    }

                    if (sVar1 != 5)
                        return 0;
                }

                FUN_1BB4();
                GameManager.instance.FUN_30CB0(this, 1200);
                tags++;
                break;
            case 8:
                if (2 < tags - 1)
                {
                    FUN_32B90((uint)arg2);
                    return 0;
                }

                return 0;
            case 9:
                if (arg2 != 0)
                {
                    if (tags == 0)
                    {
                        tags = 5;
                        FUN_30C68();
                        FUN_1BB4();
                        FUN_30BA8();
                        return 0;
                    }

                    return 0;
                }

                GameManager.instance.FUN_1DE78(DAT_18);

                if ((flags & 8) != 0)
                {
                    flags &= 0xfffffff7;
                    GameManager.instance.FUN_4C4BC(vShadow);
                    vShadow = null;
                }

                break;
        }

        return 0;
    }

    private void FUN_1BB4()
    {
        sbyte sVar1;
        VigTransform puVar2;
        LaunchRocket ppcVar3;
        LaunchRocket2 ppcVar4;
        ConfigContainer ccVar5;
        VigCollider pcVar6;
        LaunchRocket3 ppcVar6;
        sbyte sVar7;
        VigObject oVar11;
        ConfigContainer ccVar11;
        int iVar12;

        oVar11 = child2;

        if (oVar11 != null)
        {
            do
            {
                if (oVar11.id == 1) break;

                oVar11 = oVar11.child;
            } while (oVar11 != null);

            if (oVar11 != null)
            {
                puVar2 = GameManager.instance.FUN_2CDF4(oVar11);
                oVar11.vTransform = puVar2;
                oVar11.FUN_2CCBC();
                ppcVar3 = Utilities.FUN_52188(oVar11, typeof(LaunchRocket)) as LaunchRocket;
                ppcVar3.flags |= 0x80;
                sVar7 = 0;

                if (tags == 5)
                    sVar7 = 10;

                ppcVar3.tags = sVar7;
                ppcVar3.FUN_305FC();
                GameManager.instance.FUN_30CB0(ppcVar3, 60);
                sVar1 = (sbyte)GameManager.instance.FUN_1DD9C();
                ppcVar3.DAT_18 = sVar1;
                GameManager.instance.FUN_1E628(sVar1, ppcVar3.vData.sndList, 2, vTransform.position, true);
                ccVar11 = ppcVar3.FUN_2C5F4(0x8000);

                if (ccVar11 != null)
                {
                    ppcVar4 = ppcVar3.vData.ini.FUN_2C17C(25, typeof(LaunchRocket2), 8) as LaunchRocket2;

                    if (ppcVar4 != null)
                    {
                        ppcVar4.type = 3;
                        Utilities.FUN_2CA94(ppcVar3, ccVar11, ppcVar4);
                        Utilities.ParentChildren(ppcVar3, ppcVar3);
                        ppcVar4.FUN_30BF0();
                        sVar1 = (sbyte)GameManager.instance.FUN_1DD9C();
                        ppcVar4.DAT_18 = sVar1;
                        GameManager.instance.FUN_1E580(sVar1, ppcVar4.vData.sndList, 2, ppcVar4.vTransform.position, true);
                    }
                }

                if (tags != 5)
                {
                    oVar11 = GameManager.instance.FUN_318D0(49);
                    iVar12 = 0;

                    if (oVar11 != null)
                    {
                        do
                        {
                            ccVar5 = oVar11.FUN_2C5F4((ushort)(iVar12 - 0x8000 & 0xffff));

                            if (ccVar5 != null)
                            {
                                GameObject obj = new GameObject();
                                ppcVar6 = obj.AddComponent<LaunchRocket3>();

                                if (ppcVar6 != null)
                                {
                                    pcVar6 = null;

                                    if (-1 < ccVar5.colliderID)
                                        pcVar6 = new VigCollider(oVar11.vData.cbbList[ccVar5.colliderID].buffer);

                                    ppcVar6.vCollider = pcVar6;
                                    ppcVar6.vTransform = GameManager.instance.FUN_2CEAC(oVar11, ccVar5);
                                    ppcVar6.flags = 0x182;
                                    ppcVar6.type = 3;
                                    ppcVar6.FUN_305FC();
                                    ppcVar6.DAT_58 = 0xd0000;
                                    GameManager.instance.FUN_30CB0(ppcVar6, 180);
                                }
                            }

                            iVar12++;
                        } while (iVar12 < 2);
                    }
                }

                //FUN_326B0
                //FUN_32758
            }
        }
    }

    private static void FUN_1FCC()
    {
        Exhaust ppcVar1;

        GameObject obj = new GameObject();
        ppcVar1 = obj.AddComponent<Exhaust>();
        GameManager.instance.FUN_30CB0(ppcVar1, 60);
        return;
    }
}
