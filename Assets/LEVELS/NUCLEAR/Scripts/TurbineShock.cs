using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurbineShock : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public static VigObject OnInitialize(XOBF_DB arg1, int arg2, uint arg3)
    {
        Dictionary<int, Type> dict = new Dictionary<int, Type>();
        dict.Add(1035, typeof(TurbineShock2));
        dict.Add(1036, typeof(TurbineShock2));
        dict.Add(1037, typeof(TurbineShock2));
        return arg1.ini.FUN_2C17C((ushort)arg2, typeof(TurbineShock), arg3, dict);
    }

    public override uint OnCollision(HitDetection hit)
    {
        short sVar1;
        int iVar3;
        Throwaway puVar4;
        uint uVar5;
        int iVar6;
        Vehicle puVar7;
        VigObject ppcVar10;
        int iVar11;
        Ballistic puVar12;
        Vector3Int local_68;
        HitDetection auStack88;
        Vector3Int local_20;

        if (hit.collider1.ReadUInt16(0) == 1)
        {
            if (hit.collider1.ReadUInt16(2) != 3)
                return 0;

            if (hit.self.type != 2)
                return 0;

            puVar7 = (Vehicle)hit.self;

            if (puVar7.type != 2)
                return 0;

            if (tags == 1)
            {
                if ((flags & 0x80) == 0)
                    FUN_30B78();

                DAT_80 = hit.self;
            }
            else
            {
                ppcVar10 = child2;

                while (ppcVar10 != null)
                {
                    if ((ushort)ppcVar10.id - 1U < 2)
                    {
                        ((TurbineShock2)ppcVar10).state = _TURBINESHOCK2_TYPE.Type1;
                        ppcVar10.flags &= 0xfffffffd;
                        ppcVar10.FUN_2C05C();
                        ppcVar10.FUN_30BF0();
                    }

                    ppcVar10 = ppcVar10.child;
                }

                auStack88 = new HitDetection(null);
                GameManager.instance.FUN_2FB70(this, hit, auStack88);
                iVar11 = 0;
                local_68 = Utilities.FUN_24148(hit.self.vTransform, auStack88.position);
                LevelManager.instance.FUN_4DE54(local_68, 42);
                iVar3 = GameManager.instance.FUN_1DD9C();
                GameManager.instance.FUN_1E580(iVar3, GameManager.instance.DAT_C2C, 75, local_68);

                do
                {
                    puVar4 = LevelManager.instance.xobfList[19].ini.FUN_2C17C(49, typeof(Throwaway), 8) as Throwaway;
                    puVar4.physics1.M0 = 0;
                    puVar4.physics1.M1 = 0;
                    puVar4.physics1.M2 = 0;
                    uVar5 = GameManager.FUN_2AC5C();
                    puVar4.physics1.Z = (int)(uVar5 & 0xfff) - 0x800;
                    iVar6 = (int)GameManager.FUN_2AC5C();

                    if (iVar6 < 0)
                        iVar6 += 15;

                    puVar4.physics1.W = -(iVar6 >> 4);
                    uVar5 = GameManager.FUN_2AC5C();
                    puVar4.physics2.X = (int)(uVar5 & 0xfff) - 0x800;
                    puVar4.type = 7;
                    puVar4.flags |= 0x1b4;
                    sVar1 = id;
                    iVar11++;
                    puVar4.state = _THROWAWAY_TYPE.Type3;
                    puVar4.id = sVar1;
                    puVar4.vTransform = GameManager.FUN_2A39C();
                    puVar4.vTransform.position = local_68;
                    puVar4.FUN_2D1DC();
                    puVar4.DAT_87 = 1;
                    puVar4.FUN_305FC();
                } while (iVar11 < 12);

                puVar12 = vData.ini.FUN_2C17C(15, typeof(Ballistic), 8) as Ballistic;

                if (puVar12 != null)
                {
                    puVar12.vTransform = vTransform;
                    puVar12.flags = 4;
                    puVar12.type = 3;
                    puVar12.FUN_305FC();
                }

                puVar7.physics1.X /= 2;
                puVar7.physics1.Z /= 2;
                local_20 = new Vector3Int();
                local_20.x = auStack88.normal2.x << 3;
                local_20.y = auStack88.normal2.y * 8 - 0x8f080;
                local_20.z = auStack88.normal2.z << 3;
                puVar7.FUN_2B1FC(local_20, auStack88.position);

                if (puVar7.id < 0)
                    UIManager.instance.FUN_4E414(local_68, new Color32(0x00, 0x00, 0xff, 8));

                puVar7.FUN_3A064(-100, auStack88.position, false);
                puVar7.state = _VEHICLE_TYPE.Transformer;
                GameManager.instance.FUN_30CB0(puVar7, 30);
                flags |= 0x20;
            }
        }

        return 0;
    }

    //FUN_1A34 (NUCLEAR.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        byte bVar2;
        int iVar3;
        VigObject oVar11;
        Vehicle vVar11;

        switch (arg1)
        {
            case 0:
                vVar11 = DAT_80 as Vehicle;

                if (vVar11 == null)
                {
                    DAT_19 = 0;
                    FUN_30BA8();
                    return 0;
                }

                bVar2 = (byte)(DAT_19 + 1);
                DAT_19 = bVar2;

                if (29U < bVar2 && child2.tags < 9)
                {
                    DAT_19 = 0;

                    if (vVar11.body[0] != null)
                    {
                        if (vVar11.body[0].maxHalfHealth + vVar11.body[1].maxHalfHealth < vVar11.maxHalfHealth << 1)
                        {
                            vVar11.FUN_3A0C0(60);
                            iVar3 = GameManager.instance.FUN_1DD9C();
                            GameManager.instance.FUN_1E628(iVar3, GameManager.instance.DAT_C2C, 44, vTransform.position);
                            UIManager.instance.FUN_4E414(vTransform.position, new Color32(0x00, 0x80, 0x00, 8));
                            DAT_80 = null;
                            FUN_30BA8();
                            child2.tags++;
                        }
                    }
                    else
                    {
                        if (vVar11.maxHalfHealth < vVar11.maxFullHealth)
                        {
                            vVar11.FUN_3A0C0(60);
                            iVar3 = GameManager.instance.FUN_1DD9C();
                            GameManager.instance.FUN_1E628(iVar3, GameManager.instance.DAT_C2C, 44, vTransform.position);
                            UIManager.instance.FUN_4E414(vTransform.position, new Color32(0x00, 0x80, 0x00, 8));
                            DAT_80 = null;
                            FUN_30BA8();
                            child2.tags++;
                        }
                    }
                }

                PDAT_74 = null;
                break;
            case 1:
                if (arg2 != 0)
                    return 0;

                oVar11 = child2;

                while (oVar11 != null)
                {
                    oVar11.type = 3;

                    if ((ushort)oVar11.id - 1U < 2)
                        oVar11.flags = oVar11.flags & 0xfffffffb | 2;

                    oVar11 = oVar11.child;
                }

                child2.tags = 0;
                FUN_2EC7C();
                break;
            case 2:
                GameManager.instance.FUN_309A0(this);
                return 0xffffffff;
        }

        return 0;
    }
}
