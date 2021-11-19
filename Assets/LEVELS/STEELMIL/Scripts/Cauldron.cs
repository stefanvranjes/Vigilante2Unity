using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : Destructible
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
        bool bVar7;

        bVar7 = FUN_32CF0(hit);

        if (!bVar7)
            return 0;

        FUN_30C68();
        return 0;
    }

    public VigTuple2 DAT_80_2; //0x80

    //FUN_25B4 (STEELMIL.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        VigTuple ppiVar2;
        List<VigTuple> ppiVar3;
        sbyte sVar4;
        int iVar4;
        short sVar5;
        short sVar6;
        bool bVar7;
        int iVar7;
        VigObject oVar7;
        Furnace3 fVar7;
        VigTuple2 tVar7;
        int iVar8;
        int iVar9;
        VigObject oVar9;
        VigTuple2 tVar10;
        VigObject oVar11;
        uint uVar12;
        uint uVar13;
        Vector3Int local_18;
        Vector3Int local_28;

        switch (arg1)
        {
            case 0:
                switch(((byte)tags - 1) * 0x1000000 >> 0x18)
                {
                    case 0:
                        tVar10 = DAT_80_2;
                        oVar9 = DAT_84;
                        local_18 = new Vector3Int(0, 0, 0);
                        local_28 = new Vector3Int();
                        local_28.x = oVar9.vTransform.position.x - vTransform.position.x;
                        local_28.z = oVar9.vTransform.position.z - vTransform.position.z;
                        local_28.y = local_18.y;
                        iVar7 = local_28.x;

                        if (local_28.x < 0)
                            iVar7 = -local_28.x;

                        if (0x5000 < iVar7)
                        {
                            iVar7 = tVar10.array[0] << 16;

                            if (iVar7 < oVar9.vTransform.position.x)
                                iVar7 = oVar9.vTransform.position.x;

                            iVar8 = (tVar10.array[0] + tVar10.array[2]) * 0x10000;

                            if (iVar7 < iVar8)
                                iVar8 = iVar7;

                            iVar7 = tVar10.array[1] << 16;
                            iVar8 -= vTransform.position.x;

                            if (iVar7 < oVar9.vTransform.position.z)
                                iVar7 = oVar9.vTransform.position.z;

                            iVar9 = (tVar10.array[1] + tVar10.array[3]) * 0x10000;

                            if (iVar7 < iVar9)
                                iVar9 = iVar7;

                            iVar9 -= vTransform.position.z;
                            iVar7 = -3051;

                            if (-3052 < iVar8)
                            {
                                iVar7 = 3051;

                                if (iVar8 < 3052)
                                    iVar7 = iVar8;
                            }

                            vTransform.position.x += iVar7;
                            iVar7 = -3051;

                            if (-3052 < iVar9)
                            {
                                iVar7 = 3051;

                                if (iVar9 < 3052)
                                    iVar7 = iVar9;
                            }

                            vTransform.position.z += iVar7;
                            local_18.x = local_28.x;
                            local_18.z = local_28.z;
                            Utilities.FUN_2A098(local_28, out local_28);
                            iVar7 = local_28.z * vTransform.rotation.V02 - local_28.x * vTransform.rotation.V22;

                            if (iVar7 < 0)
                                iVar7 += 4095;

                            iVar7 >>= 12;
                            iVar9 = iVar7;

                            if (iVar7 < 0)
                                iVar9 = -iVar7;

                            if (20 < iVar9)
                            {
                                iVar9 = -iVar7;

                                if (0 < iVar7)
                                    iVar9 += 15;

                                FUN_24700(0, (short)(iVar9 >> 4), 0);
                            }

                            if ((GameManager.instance.DAT_28 - DAT_19 & 15) == 0)
                            {
                                vTransform.rotation = Utilities.MatrixNormal(vTransform.rotation);
                                return 0;
                            }

                            return 0;
                        }

                        iVar7 = local_28.z;

                        if (local_28.z < 0)
                            iVar7 = -local_28.z;

                        if (0x5000 < iVar7)
                        {
                            iVar7 = tVar10.array[0] << 16;

                            if (iVar7 < oVar9.vTransform.position.x)
                                iVar7 = oVar9.vTransform.position.x;

                            iVar8 = (tVar10.array[0] + tVar10.array[2]) * 0x10000;

                            if (iVar7 < iVar8)
                                iVar8 = iVar7;

                            iVar7 = tVar10.array[1] << 16;
                            iVar8 -= vTransform.position.x;

                            if (iVar7 < oVar9.vTransform.position.z)
                                iVar7 = oVar9.vTransform.position.z;

                            iVar9 = (tVar10.array[1] + tVar10.array[3]) * 0x10000;

                            if (iVar7 < iVar9)
                                iVar9 = iVar7;

                            iVar9 -= vTransform.position.z;
                            iVar7 = -3051;

                            if (-3052 < iVar8)
                            {
                                iVar7 = 3051;

                                if (iVar8 < 3052)
                                    iVar7 = iVar8;
                            }

                            vTransform.position.x += iVar7;
                            iVar7 = -3051;

                            if (-3052 < iVar9)
                            {
                                iVar7 = 3051;

                                if (iVar9 < 3052)
                                    iVar7 = iVar9;
                            }

                            vTransform.position.z += iVar7;
                            local_18.x = local_28.x;
                            local_18.z = local_28.z;
                            Utilities.FUN_2A098(local_28, out local_28);
                            iVar7 = local_28.z * vTransform.rotation.V02 - local_28.x * vTransform.rotation.V22;

                            if (iVar7 < 0)
                                iVar7 += 4095;

                            iVar7 >>= 12;
                            iVar9 = iVar7;

                            if (iVar7 < 0)
                                iVar9 = -iVar7;

                            if (20 < iVar9)
                            {
                                iVar9 = -iVar7;

                                if (0 < iVar7)
                                    iVar9 += 15;

                                FUN_24700(0, (short)(iVar9 >> 4), 0);
                            }

                            if ((GameManager.instance.DAT_28 - DAT_19 & 15) == 0)
                            {
                                vTransform.rotation = Utilities.MatrixNormal(vTransform.rotation);
                                return 0;
                            }

                            return 0;
                        }

                        break;
                    case 1:
                        oVar7 = child2;
                        sVar5 = (short)(oVar7.vr.x - 17);
                        oVar7.vr.x = sVar5;

                        if (sVar5 < -511)
                            tags++;

                        goto LAB_2944;
                    case 2:
                        fVar7 = FUN_2458();
                        iVar4 = GameManager.instance.FUN_1DD9C();
                        GameManager.instance.FUN_1E628(iVar4, GameManager.instance.DAT_C2C, 67, fVar7.vTransform.position);

                        if (fVar7 != null)
                            GameManager.instance.FUN_30CB0(fVar7, 60);

                        FUN_30BA8();
                        GameManager.instance.FUN_30CB0(this, 120);
                        break;
                    default:
                        return 0;
                    case 4:
                        oVar7 = child2;
                        sVar6 = (short)(oVar7.vr.x + 17);
                        oVar7.vr.x = sVar6;

                        if (0 < sVar6 << 16)
                        {
                            tags = 0;
                            iVar9 = (int)GameManager.FUN_2AC5C();
                            GameManager.instance.FUN_30CB0(this, (iVar9 * 120 >> 15) + 180);
                            FUN_30BA8();
                        }

                        LAB_2944:
                        oVar7.ApplyTransformation();
                        return 0;
                }

                LAB_2B28:
                tags++;
                break;
            case 1:
                flags |= 0x100;
                tVar7 = GameManager.instance.FUN_2FFD0(1);
                DAT_80_2 = tVar7;

                if (tVar7 != null)
                {
                    iVar7 = (int)GameManager.FUN_2AC5C();
                    GameManager.instance.FUN_30CB0(this, iVar7 * 60 >> 15);
                }

                vTransform.position.y = 0x2af800;
                break;
            case 2:
                sVar4 = tags;

                if (sVar4 == 1)
                {
                    tVar7 = DAT_80_2;
                    oVar9 = DAT_84;

                    if ((oVar9.maxHalfHealth == 0 || oVar9.vTransform.position.x <= tVar7.array[0] << 16) || 
                        (tVar7.array[0] + tVar7.array[2]) * 0x10000 <= oVar9.vTransform.position.x || 
                        (oVar9.vTransform.position.z <= tVar7.array[1] << 16 || 
                        (tVar7.array[1] + tVar7.array[3]) * 0x10000 <= oVar9.vTransform.position.z))
                    {
                        FUN_30BA8();
                        sVar4 = 5;
                        tags = sVar4;
                    }

                    iVar7 = (int)GameManager.FUN_2AC5C();
                    GameManager.instance.FUN_30CB0(this, iVar7 * 60 >> 15);
                    return 0;
                }

                if (sVar4 < 2)
                {
                    if (sVar4 != 0)
                        return 0;

                    oVar11 = null;
                    uVar12 = 0xffffffff;
                    tVar7 = DAT_80_2;
                    ppiVar3 = GameManager.instance.worldObjs;
                    
                    for (int i = 0; i < ppiVar3.Count; i++)
                    {
                        ppiVar2 = ppiVar3[i];
                        oVar9 = ppiVar2.vObject;

                        if (oVar9.type == 2 && oVar9.maxHalfHealth != 0 && 
                            tVar7.array[0] << 16 < oVar9.vTransform.position.x && 
                            oVar9.vTransform.position.x < (tVar7.array[0] + tVar7.array[2]) * 0x10000 && 
                            tVar7.array[1] << 16 < oVar9.vTransform.position.z && 
                            oVar9.vTransform.position.z < (tVar7.array[1] + tVar7.array[3]) * 0x10000)
                        {
                            
                            uVar13 = (uint)Utilities.FUN_29F6C(vTransform.position, oVar9.screen);

                            if (uVar13 < uVar12)
                            {
                                oVar11 = oVar9;
                                uVar12 = uVar13;
                            }
                        }
                    }

                    if (oVar11 != null)
                    {
                        DAT_84 = oVar11;
                        FUN_30B78();
                        sVar4 = (sbyte)(tags + 1);
                        tags = sVar4;
                        iVar7 = (int)GameManager.FUN_2AC5C();
                        GameManager.instance.FUN_30CB0(this, iVar7 * 60 >> 15);
                        return 0;
                    }
                    else
                    {
                        iVar7 = (int)GameManager.FUN_2AC5C();
                        GameManager.instance.FUN_30CB0(this, iVar7 * 60 >> 15);
                        return 0;
                    }
                }

                if (sVar4 != 4)
                {
                    if (sVar4 == 5)
                    {
                        iVar7 = (int)GameManager.FUN_2AC5C();
                        GameManager.instance.FUN_30CB0(this, iVar7 * 60 >> 15);
                        tags = 0;
                        return 0;
                    }

                    return 0;
                }

                FUN_30B78();
                goto LAB_2B28;
            case 8:
                bVar7 = FUN_32B90((uint)arg2);

                if (!bVar7)
                    return 0;

                FUN_30C68();
                break;
        }

        return 0;
    }

    //FUN_2458 (STEELMIL.DLL)
    private Furnace3 FUN_2458()
    {
        Furnace3 ppcVar1;
        ConfigContainer ccVar2;
        int iVar3;
        XOBF_DB pcVar4;
        VigTransform auStack48;
        Vector3Int local_10;

        GameObject obj = new GameObject();
        ppcVar1 = obj.AddComponent<Furnace3>();
        ccVar2 = child2.FUN_2C5F4(0x8000);
        auStack48 = GameManager.instance.FUN_2CEAC(child2, ccVar2);
        ppcVar1.vTransform = vTransform;
        ppcVar1.vTransform.position = auStack48.position;
        ppcVar1.type = 8;
        ppcVar1.maxHalfHealth = 5;
        ppcVar1.vCollider = new VigCollider(Furnace.DAT_144);
        ppcVar1.flags |= 0x184;
        pcVar4 = LevelManager.instance.xobfList[19];
        ppcVar1.physics2.M3 = 21;
        ppcVar1.physics1.M1 = 4;
        ppcVar1.DAT_98 = pcVar4;
        iVar3 = GameManager.instance.terrain.FUN_1B750((uint)ppcVar1.vTransform.position.x, (uint)ppcVar1.vTransform.position.z);
        ppcVar1.screen.y = iVar3 - ppcVar1.vTransform.position.y;
        local_10 = new Vector3Int(0, 0, 0x100);
        local_10 = Utilities.FUN_24094(ppcVar1.vTransform.rotation, local_10);
        ppcVar1.physics1.Y = local_10.x;
        ppcVar1.physics1.Z = local_10.y;
        ppcVar1.physics1.W = local_10.z;
        ppcVar1.FUN_305FC();
        ppcVar1.DAT_58 = 0x40000;
        return ppcVar1;
    }
}
