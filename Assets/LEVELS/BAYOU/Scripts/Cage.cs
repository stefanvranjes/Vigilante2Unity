using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : Destructible
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
        sbyte sVar1;
        short sVar2;
        int iVar4;
        bool bVar5;
        int iVar5;
        Particle1 pVar5;
        VigObject ppcVar10;
        Vector3Int local_18;

        sVar1 = tags;

        if (sVar1 != 1)
        {
            if (sVar1 < 2)
            {
                if (sVar1 != 0)
                    return 0;

                if (hit.self.type != 2 && hit.self.GetType() != typeof(Aligator))
                    return 0;

                tags = 1;
                FUN_30B78();
                iVar4 = GameManager.instance.FUN_1DD9C();
                GameManager.instance.FUN_1E580(iVar4, GameManager.instance.DAT_C2C, 43, vTransform.position);
                GameManager.instance.FUN_309A0(DAT_90);
                DAT_90 = null;
            }
            else
            {
                if (sVar1 < 4)
                {
                    if (tags == 2 && DAT_94 == null)
                    {
                        ppcVar10 = hit.self;

                        if (ppcVar10.type == 2 || ppcVar10.GetType() == typeof(Aligator))
                        {
                            DAT_94 = ppcVar10;
                            hit.self.flags |= 0x20;
                            FUN_30B78();
                            iVar4 = GameManager.instance.FUN_1DD9C();
                            GameManager.instance.FUN_1E580(iVar4, GameManager.instance.DAT_C2C, 39, vTransform.position);
                            local_18 = new Vector3Int();
                            local_18.x = DAT_94.vTransform.position.x;
                            local_18.z = DAT_94.vTransform.position.z;
                            local_18.y = GameManager.instance.DAT_DB0;
                            pVar5 = LevelManager.instance.FUN_4DE54(local_18, 146);

                            if (pVar5 != null)
                            {
                                pVar5.flags &= 0xffffffef;
                                sVar2 = (short)GameManager.FUN_2AC5C();
                                pVar5.vr.y = sVar2;
                                pVar5.ApplyTransformation();
                            }

                            if (hit.self.type == 2)
                                return 0;

                            iVar5 = (int)GameManager.FUN_2AC5C();
                            local_18.x = (iVar5 * 3051 >> 15) - 1525;
                            local_18.y = -4577;
                            iVar5 = (int)GameManager.FUN_2AC5C();
                            local_18.z = (iVar5 * 3051 >> 15) - 1525;
                            LevelManager.instance.FUN_4AAC0(0x400000, hit.self.vTransform.position, local_18);
                            iVar5 = (int)GameManager.FUN_2AC5C();
                            local_18.x = (iVar5 * 3051 >> 15) - 1525;
                            local_18.y = -4577;
                            iVar5 = (int)GameManager.FUN_2AC5C();
                            local_18.z = (iVar5 * 3051 >> 15) - 1525;
                            LevelManager.instance.FUN_4AAC0(0x80000000, hit.self.vTransform.position, local_18);
                            iVar5 = (int)GameManager.FUN_2AC5C();
                            local_18.x = (iVar5 * 3051 >> 15) - 1525;
                            local_18.y = -4577;
                            iVar5 = (int)GameManager.FUN_2AC5C();
                            local_18.z = (iVar5 * 3051 >> 15) - 1525;
                            LevelManager.instance.FUN_4AAC0(0x80400000, hit.self.vTransform.position, local_18);
                            //FUN_326B0
                            return 0;
                        }
                    }

                    bVar5 = FUN_32CF0(hit);

                    if (!bVar5)
                        return 0;

                    return 0xffffffff;
                }
            }
        }

        return 0;
    }

    //0xD8 (BAYOU.DLL)
    private static Vector3Int DAT_D8 = new Vector3Int(0, 0, 0);

    public Cage DAT_8C; //0x8C
    public VigObject DAT_90; //0x90
    public VigObject DAT_94; //0x94

    //FUN_988 (BAYOU.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        sbyte sVar1;
        short sVar2;
        uint uVar3;
        int iVar4;
        bool bVar5;
        Particle1 pVar5;
        int iVar6;
        VigObject pcVar7;
        int iVar8;
        int iVar9;
        VigObject pcVar9;
        VigObject ppcVar10;
        Vector3Int local_28;

        switch (arg1)
        {
            case 0:
                sVar1 = tags;

                if (sVar1 == 1)
                {
                    iVar9 = physics1.Y + 720;
                    physics1.Y = iVar9;
                    iVar6 = vTransform.position.y;
                    vTransform.position.y = iVar6 + iVar9;

                    if (iVar6 + iVar9 < physics2.Z + 0x34800)
                        return 0;

                    FUN_30BA8();
                    tags = 2;
                    vTransform.position.y = physics2.Z + 0x34800;
                    GameManager.instance.FUN_30CB0(this, 30);
                    physics1.Y = -0x555;
                    maxHalfHealth = 100;
                    iVar4 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E580(iVar4, vData.sndList, 1, vTransform.position);
                    iVar4 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E580(iVar4, GameManager.instance.DAT_C2C, 38, vTransform.position);
                    GameManager.instance.FUN_1E30C(iVar4, 0x1800);
                    return 0;
                }

                if (0 < sVar1)
                {
                    if (3 < sVar1)
                        return 0;

                    pcVar9 = DAT_94;

                    if (pcVar9 == null)
                        return 0;

                    iVar8 = vTransform.position.x - pcVar9.vTransform.position.x;
                    iVar6 = vTransform.position.y - pcVar9.vTransform.position.y;
                    iVar9 = vTransform.position.z - pcVar9.vTransform.position.z;

                    if (iVar8 < 0)
                        iVar8 += 3;

                    if (iVar6 < 0)
                        iVar6 += 3;

                    if (iVar9 < 0)
                        iVar9 += 3;

                    pcVar7 = DAT_94;
                    pcVar7.vTransform.position.x += iVar8 >> 2;
                    pcVar7.vTransform.position.y += iVar6 >> 2;
                    pcVar7.vTransform.position.z += iVar9 >> 2;

                    if (DAT_94.type != 2)
                        return 0;

                    ((Vehicle)DAT_94).FUN_39DCC(-1, DAT_D8, true);

                    if (arg2 == 0)
                        return 0;

                    uVar3 = GameManager.FUN_2AC5C();

                    if ((uVar3 & 7) != 0)
                        return 0;

                    local_28 = new Vector3Int();
                    local_28.x = DAT_94.vTransform.position.x;
                    local_28.z = DAT_94.vTransform.position.z;
                    local_28.y = GameManager.instance.DAT_DB0;
                    pVar5 = LevelManager.instance.FUN_4DE54(local_28, 146);

                    if (pVar5 != null)
                    {
                        pVar5.flags &= 0xffffffef;
                        sVar2 = (short)GameManager.FUN_2AC5C();
                        pVar5.vr.y = sVar2;
                        pVar5.ApplyTransformation();
                        return 0;
                    }
                }

                break;
            case 1:
                tags = -1;
                GameManager.instance.FUN_30CB0(this, 2);
                break;
            case 2:
                if (tags == 2)
                {
                    GameManager.instance.FUN_30CB0(this, 300);
                    tags = 3;
                }
                else
                {
                    if (tags != 3)
                        return 0;

                    if (GetType().IsSubclassOf(typeof(VigObject)))
                    {
                        UpdateW(8, 1000);
                        return 0;
                    }
                }

                break;
            case 8:
                if (1 < (byte)tags - 2U && arg2 != 1000)
                    return 0;

                bVar5 = FUN_32B90((uint)arg2);

                if (!bVar5)
                    return 0;

                return 0xffffffff;
            case 9:
                if (arg2 == 0)
                    return 0;

                if (DAT_90 != null)
                {
                    GameManager.instance.FUN_309A0(DAT_90);
                    DAT_90 = null;
                }

                if (type == 4)
                {
                    Bayou.FUN_7F0(DAT_19);
                    pcVar9 = DAT_94;

                    if (pcVar9 != null)
                    {
                        pcVar9.flags &= 0xffffffdf;
                        DAT_94 = null;
                    }
                }
                else
                {
                    ppcVar10 = DAT_8C;

                    if (ppcVar10 == null) goto LAB_10C0;

                    if (ppcVar10 != null)
                        ppcVar10.UpdateW(8, 1000);
                }

                if (DAT_8C != null)
                    DAT_8C.DAT_8C = null;

                LAB_10C0:
                GameManager.instance.FUN_309A0(this);
                return 0;
        }

        return 0;
    }
}
