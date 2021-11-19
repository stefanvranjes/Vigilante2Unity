using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformer : VigObject
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
        short sVar2;
        int iVar3;
        int iVar4;
        Throwaway puVar5;
        uint uVar6;
        int iVar7;
        int iVar8;
        Vehicle puVar9;
        bool bVar12;
        int iVar12;
        VigObject oVar12;
        Vehicle vVar12;
        Ballistic puVar13;
        Vector3Int local_70;
        Vector3Int local_60;
        Vector3Int local_20;
        HitDetection auStack88;

        if (hit.collider1.ReadUInt16(2) == 1 && (byte)tags - 1U < 2)
        {
            if (hit.self.type != 2)
                return 0;

            vVar12 = (Vehicle)hit.self;
            local_70 = new Vector3Int();
            local_70.x = vTransform.position.x - vVar12.vTransform.position.x;
            local_70.y = vTransform.position.y - vVar12.vTransform.position.y;
            local_70.z = vTransform.position.z - vVar12.vTransform.position.z;
            iVar3 = Utilities.FUN_29FC8(local_70, out local_60);
            iVar7 = -iVar3 + 0x50000;

            if (0x50000 < iVar3)
                return 0;

            if (iVar7 < 0)
                iVar7 = -iVar3 + 0x50003;

            iVar7 >>= 2;
            local_70.x = local_60.x * iVar7 >> 12;
            local_70.y = local_60.y * iVar7 >> 12;
            local_70.z = local_60.z * iVar7 >> 12;
            iVar7 = vVar12.physics1.X;
            iVar3 = iVar7;

            if (iVar7 < 0)
                iVar3 = iVar7 + 31;

            iVar8 = vVar12.physics1.Y;
            vVar12.physics1.X = iVar7 + (local_70.x - (iVar3 >> 5));
            iVar3 = iVar8;

            if (iVar8 < 0)
                iVar3 = iVar8 + 31;

            iVar7 = vVar12.physics1.Z;
            vVar12.physics1.Y = iVar8 + (local_70.y - (iVar3 >> 5));
            iVar3 = iVar7;

            if (iVar7 < 0)
                iVar3 = iVar7 + 31;

            vVar12.physics1.Z = iVar7 + (local_70.z - (iVar3 >> 5));

            if (tags == 1)
            {
                FUN_30B78();
                tags = 2;
            }

            PDAT_78 = hit.self;
            return 0;
        }

        if ((hit.object1 == this || hit.self.type != 2) || 1 < (byte)tags - 1U)
        {
            if (hit.self.type == 8)
            {
                bVar12 = FUN_32B90(hit.self.maxHalfHealth);
                return bVar12 ? 1U : 0;
            }

            return 0;
        }

        puVar9 = (Vehicle)hit.self;
        FUN_30BA8();
        auStack88 = new HitDetection(null);
        GameManager.instance.FUN_2FB70(this, hit, auStack88);
        iVar12 = 0;
        local_70 = Utilities.FUN_24148(hit.self.vTransform, auStack88.position);
        LevelManager.instance.FUN_4DE54(local_70, 42);
        iVar4 = GameManager.instance.FUN_1DD9C();
        GameManager.instance.FUN_1E580(iVar4, GameManager.instance.DAT_C2C, 75, local_70);
        GameManager.instance.FUN_30CB0(this, 60);

        do
        {
            puVar5 = LevelManager.instance.xobfList[19].ini.FUN_2C17C(49, typeof(Throwaway), 8) as Throwaway;
            puVar5.physics1.M0 = 0;
            puVar5.physics1.M1 = 0;
            puVar5.physics1.M2 = 0;
            uVar6 = GameManager.FUN_2AC5C();
            puVar5.physics1.Z = (int)(uVar6 & 0xfff) - 0x800;
            iVar3 = (int)GameManager.FUN_2AC5C();

            if (iVar3 < 0)
                iVar3 += 15;

            puVar5.physics1.W = -(iVar3 >> 4);
            uVar6 = GameManager.FUN_2AC5C();
            puVar5.physics2.X = (int)(uVar6 & 0xfff) - 0x800;
            puVar5.type = 7;
            puVar5.flags |= 0x1b4;
            sVar2 = id;
            iVar12++;
            puVar5.state = _THROWAWAY_TYPE.Type3;
            puVar5.id = sVar2;
            puVar5.vTransform = GameManager.FUN_2A39C();
            puVar5.vTransform.position = local_70;
            puVar5.FUN_2D1DC();
            puVar5.DAT_87 = 1;
            puVar5.FUN_305FC();
        } while (iVar12 < 12);

        puVar13 = vData.ini.FUN_2C17C(15, typeof(Ballistic), 8) as Ballistic;

        if (puVar13 != null)
        {
            oVar12 = Utilities.FUN_2CD78(this);

            if (oVar12 == null)
                oVar12 = this;

            puVar13.vTransform = oVar12.vTransform;
            puVar13.flags = 4;
            puVar13.type = 3;
            puVar13.FUN_305FC();
        }

        puVar9.physics1.X /= 2;
        puVar9.physics1.Z /= 2;
        local_20 = new Vector3Int();
        local_20.x = auStack88.normal2.x << 3;
        local_20.y = auStack88.normal2.y * 8 - 0x8f080;
        local_20.z = auStack88.normal2.z << 3;
        puVar9.FUN_2B1FC(local_20, auStack88.position);
        iVar4 = -25;

        if (puVar9.id < 0)
        {
            UIManager.instance.FUN_4E414(local_70, new Color32(0x00, 0x00, 0xff, 8));
            iVar4 = -25;

            if (puVar9.id < 0)
                iVar4 = -100;
        }

        puVar9.FUN_3A064(iVar4, auStack88.position, false);
        puVar9.state = _VEHICLE_TYPE.Transformer;
        GameManager.instance.FUN_30CB0(puVar9, 15);
        iVar12 = 60;
        tags = 3;
        GameManager.instance.FUN_30CB0(this, iVar12);
        return 0;
    }

    //FUN_CB4 (NUCLEAR.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        sbyte sVar1;
        VigObject oVar3;
        uint uVar6;
        Magnet2 puVar9;
        int iVar12;
        VigObject oVar12;
        Vector3Int local_70;
        Vector3Int local_60;

        switch (arg1)
        {
            case 0:
                sVar1 = tags;

                if (-1 < sVar1)
                {
                    if (sVar1 < 2)
                        return 0;

                    if (sVar1 != 2)
                        return 0;

                    if (arg2 == 0)
                        return 0;

                    if (PDAT_78 == null)
                        return 0;

                    uVar6 = GameManager.FUN_2AC5C();

                    if ((uVar6 & 1) != 0)
                    {
                        puVar9 = LevelManager.instance.xobfList[19].ini.FUN_2C17C(49, typeof(Magnet2), 8) as Magnet2;
                        oVar3 = PDAT_78;
                        iVar12 = (int)GameManager.FUN_2AC5C();
                        puVar9.screen.x = oVar3.vTransform.position.x + iVar12 * 2;
                        iVar12 = (int)GameManager.FUN_2AC5C();
                        puVar9.screen.y = oVar3.vTransform.position.y + iVar12 * 2;
                        iVar12 = (int)GameManager.FUN_2AC5C();
                        puVar9.screen.z = oVar3.vTransform.position.z + iVar12 * 2;
                        local_70 = new Vector3Int();
                        local_70.x = vTransform.position.x - puVar9.screen.x;
                        local_70.y = vTransform.position.y - puVar9.screen.y;
                        local_70.z = vTransform.position.z - puVar9.screen.z;
                        Utilities.FUN_29FC8(local_70, out local_60);
                        iVar12 = local_60.x;

                        if (iVar12 < 0)
                            iVar12 += 7;

                        puVar9.physics1.Z = iVar12 >> 3;
                        iVar12 = local_60.y;

                        if (iVar12 < 0)
                            iVar12 += 7;

                        puVar9.physics1.W = iVar12 >> 3;
                        iVar12 = local_60.z;

                        if (iVar12 < 0)
                            iVar12 += 7;

                        puVar9.physics2.X = iVar12 >> 3;
                        puVar9.flags = 0xb4;
                        puVar9.FUN_3066C();
                        PDAT_78 = null;
                    }
                }

                break;
            case 1:
                oVar12 = child2;

                if (oVar12 != null)
                {
                    do
                    {
                        if (oVar12.id == 1)
                            oVar12.type = 3;
                        else
                        {
                            if (oVar12.id == 2)
                                oVar12.flags |= 2;
                        }

                        oVar12 = oVar12.child;
                    } while (oVar12 != null);

                    oVar12 = child2;
                }

                while (oVar12 != null)
                {
                    if (oVar12.id == -0x5556)
                    {
                        oVar12.FUN_2CCBC();
                        Utilities.FUN_2CC9C(this, oVar12);
                        oVar12.transform.parent = transform;
                        break;
                    }

                    oVar12 = oVar12.child;
                }

                tags = 0;
                iVar12 = (int)GameManager.FUN_2AC5C();
                iVar12 = (iVar12 * 420 >> 15) + 180;
                GameManager.instance.FUN_30CB0(this, iVar12);
                break;
            case 2:
                sVar1 = tags;

                if (sVar1 == 0)
                {
                    oVar12 = child2;

                    while (oVar12 != null)
                    {
                        if (oVar12.id == 2)
                        {
                            oVar12.flags &= 0xfffffffd;
                            break;
                        }

                        oVar12 = oVar12.child;
                    }

                    iVar12 = (int)GameManager.FUN_2AC5C();
                    GameManager.instance.FUN_30CB0(this, (iVar12 * 420 >> 15) + 420);
                    tags = 1;
                }
                else
                {
                    if (-1 < sVar1)
                    {
                        if (3 < sVar1)
                            return 0;

                        oVar12 = child2;

                        while (oVar12 != null)
                        {
                            if (oVar12.id == 2)
                            {
                                oVar12.flags |= 2;
                                break;
                            }

                            oVar12 = oVar12.child;
                        }

                        FUN_30BA8();
                        iVar12 = (int)GameManager.FUN_2AC5C();
                        GameManager.instance.FUN_30CB0(this, (iVar12 * 420 >> 15) + 420);
                        PDAT_78 = null;
                        tags = 0;
                    }
                }

                break;
            case 8:
                FUN_32B90((uint)arg2);
                return 0;
        }

        return 0;
    }
}
