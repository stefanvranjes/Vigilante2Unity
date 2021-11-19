using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellGate2 : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public VigObject DAT_94; //0x94

    //FUN_8DC (HALFTRAK.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        short sVar1;
        sbyte sVar2;
        int iVar3;
        HellGate3 puVar4;
        uint uVar4;
        short sVar5;
        int iVar5;
        uint uVar5;
        int iVar6;
        ushort uVar7;
        int iVar7;
        Vehicle vVar7;
        int iVar10;
        VigObject oVar10;
        Vehicle vVar10;
        VigCamera cVar10;
        Flamewall3 ppcVar10;
        HellGate4 puVar11;
        Quake puVar12;
        ConfigContainer ccVar13;
        int iVar16;
        uint uVar19;
        Vector3Int local_20;
        Vector3Int local_28;
        Vector3Int local_30;
        Vector3Int local_38;
        Vector3Int local_40;
        Vector3Int local_48;
        Vector3Int local_50;
        Vector3Int local_60;
        Vector3Int local_88;
        Matrix3x3 auStack128;

        if (arg1 == 0)
        {
            oVar10 = DAT_80;

            if (tags < 20)
            {
                local_60 = new Vector3Int();
                local_60.x = 0;
                local_60.z = 0;
                local_60.y = (oVar10.vCollider.reader.ReadInt32(8) + oVar10.vCollider.reader.ReadInt32(20)) / 2;
                local_60 = Utilities.FUN_24148(oVar10.vTransform, local_60);
                local_60.x = vTransform.position.x - local_60.x;
                local_60.y = vTransform.position.y - local_60.y;
                local_60.z = vTransform.position.z - local_60.z;
                iVar7 = local_60.x;

                if (local_60.x < 0)
                    iVar7 = local_60.x + 7;

                iVar6 = oVar10.physics1.X;
                iVar3 = iVar6;

                if (iVar6 < 0)
                    iVar3 = iVar6 + 31;

                oVar10.physics1.X = iVar6 + ((iVar7 >> 3) - (iVar3 >> 5));
                iVar7 = local_60.y;

                if (local_60.y < 0)
                    iVar7 = local_60.y + 7;

                iVar6 = oVar10.physics1.Y;
                iVar3 = iVar6;

                if (iVar6 < 0)
                    iVar3 = iVar6 + 31;

                oVar10.physics1.Y = iVar6 + ((iVar7 >> 3) - (iVar3 >> 5));
                iVar7 = local_60.z;

                if (local_60.z < 0)
                    iVar7 = local_60.z + 7;

                iVar6 = oVar10.physics1.Z;
                iVar3 = iVar6;

                if (iVar6 < 0)
                    iVar3 = iVar6 + 31;

                oVar10.physics1.Z = iVar6 + ((iVar7 >> 3) - (iVar3 >> 5));
                Utilities.FUN_248C4(oVar10.vTransform.rotation, vTransform.rotation, out auStack128);
                iVar7 = auStack128.V11 * -4 - oVar10.physics2.X;

                if (iVar7 < 0)
                    iVar7 += 31;

                oVar10.physics2.X += iVar7 >> 5;
                iVar7 = auStack128.V01 * 4 - oVar10.physics2.Y;

                if (iVar7 < 0)
                    iVar7 += 31;

                iVar3 = oVar10.physics2.Z;
                oVar10.physics2.Y += iVar7 >> 5;
                iVar7 = -iVar3;

                if (0 < iVar3)
                    iVar7 += 31;

                oVar10.physics2.Z = iVar3 + (iVar7 >> 5);
            }

            vVar10 = (Vehicle)oVar10;
            vVar10.FUN_2AF20();
            oVar10 = child2;

            if (oVar10 != null)
            {
                do
                {
                    oVar10.vr.y += (short)oVar10.maxHalfHealth;
                    oVar10.screen.y += 455;
                    oVar10.maxHalfHealth += 4;
                    oVar10.maxFullHealth -= 34;
                    oVar10.ApplyTransformation();
                    local_88 = new Vector3Int((short)oVar10.maxFullHealth, (short)oVar10.maxFullHealth, (short)oVar10.maxFullHealth);
                    Utilities.FUN_245AC(ref oVar10.vTransform.rotation, local_88);
                    oVar10 = oVar10.child;
                } while (oVar10 != null);

                return 0;
            }

            uVar5 = 0;
        }
        else
        {
            if (arg1 != 2)
                return 0;

            sVar2 = (sbyte)(tags + 1);
            tags = sVar2;

            if (sVar2 < 20)
            {
                puVar4 = vData.ini.FUN_2C17C(1, typeof(HellGate3), 8) as HellGate3;
                Utilities.ParentChildren(puVar4, puVar4);
                puVar4.maxFullHealth = 0x1000;
                puVar4.vTransform = GameManager.FUN_2A39C();
                Utilities.FUN_2CC9C(this, puVar4);
                puVar4.transform.parent = transform;
                GameManager.instance.FUN_30CB0(puVar4, 90);
                GameManager.instance.FUN_30CB0(this, 7);
            }

            switch(((byte)tags - 20) * 0x1000000 >> 24)
            {
                case 0:
                    vVar7 = (Vehicle)DAT_80;
                    vVar7.physics1.X = vVar7.vTransform.rotation.V02 * 143;
                    vVar7.physics1.Y = vVar7.vTransform.rotation.V12 * 143;
                    vVar7.physics1.Z = vVar7.vTransform.rotation.V22 * 143;
                    cVar10 = vVar7.vCamera;

                    if (cVar10 != null)
                    {
                        cVar10.DAT_84 = new Vector3Int(0, 0, 0);
                        cVar10.flags |= 0x4000000;
                    }

                    if (vVar7.id < 0)
                        GameManager.instance.FUN_15AA8(~vVar7.id, 30, 255, 0, 64);

                    iVar10 = 0;
                    vVar7.physics2.X = 0;
                    vVar7.physics2.Y = 0;
                    vVar7.physics2.Z = 0;
                    vVar7.flags |= 0x20000;
                    GameManager.instance.FUN_30CB0(this, 30);
                    local_30 = new Vector3Int();
                    local_40 = new Vector3Int();
                    local_50 = new Vector3Int();
                    local_30.y = GameManager.instance.terrain.FUN_1B750((uint)vVar7.vTransform.position.x, (uint)vVar7.vTransform.position.z);

                    do
                    {
                        iVar10++;
                        iVar3 = (int)GameManager.FUN_2AC5C();
                        local_40.y = local_30.y - 0x5000;
                        local_40.x = vVar7.vTransform.position.x + (iVar3 * 0x1e000 >> 15) - 0xf000;
                        iVar3 = (int)GameManager.FUN_2AC5C();
                        local_50.z = vVar7.vTransform.position.z + (iVar3 * 0x1e000 >> 15) - 0xf000;
                        local_50.x = local_40.x;
                        local_50.y = local_40.y;
                        local_40.z = local_50.z;
                        LevelManager.instance.FUN_4DE54(local_50, 33);
                    } while (iVar10 < 3);

                    local_40.x = vVar7.vTransform.position.x;
                    local_40.z = vVar7.vTransform.position.z;
                    local_40.y = local_30.y;
                    local_30.x = local_40.x;
                    local_30.z = local_40.z;
                    LevelManager.instance.FUN_4DE54(local_40, 39);
                    uVar5 = 0;
                    break;
                case 1:
                    vVar7 = (Vehicle)DAT_80;
                    UIManager.instance.FUN_4E414(vTransform.position, new Color32(0xff, 0xff, 0xff, 1));
                    oVar10 = DAT_84;
                    vVar7.vTransform.position = oVar10.screen;
                    vVar7.physics1.X = 0;
                    vVar7.flags |= 0x2000002;
                    vVar7.physics1.Y = 0;
                    vVar7.physics1.Z = 0;
                    cVar10 = vVar7.vCamera;

                    if (cVar10 != null)
                    {
                        cVar10.screen.x = vVar7.vTransform.position.x;
                        cVar10.screen.y = vVar7.vTransform.position.y - 0x4b000;
                        cVar10.screen.z = vVar7.vTransform.position.z - 0x4b000;
                    }

                    GameManager.instance.FUN_30CB0(this, 60);
                    uVar5 = 0;
                    break;
                case 2:
                    GameManager.instance.FUN_30CB0(this, 30);
                    uVar5 = 0;
                    break;
                case 3:
                    vVar7 = (Vehicle)DAT_80;
                    iVar3 = 0;
                    puVar11 = vData.ini.FUN_2C17C(3, typeof(HellGate4), 8) as HellGate4;
                    Utilities.ParentChildren(puVar11, puVar11);
                    oVar10 = DAT_84;
                    vTransform.position = oVar10.screen;
                    local_50 = GameManager.instance.terrain.FUN_1B998((uint)vTransform.position.x, (uint)vTransform.position.z);
                    local_50 = Utilities.VectorNormal(local_50);
                    vTransform.rotation = Utilities.FUN_2A5EC(local_50);
                    vVar7.flags |= 0x2000002;
                    vVar7.vTransform.position = vTransform.position;
                    vVar7.vTransform.rotation = Utilities.FUN_2A724(local_50);
                    vVar7.physics1.X = 0;
                    vVar7.physics1.Y = 0;
                    vVar7.physics1.Z = 0;
                    iVar5 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E580(iVar5, vData.sndList, 3, vTransform.position);
                    iVar5 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E580(iVar5, GameManager.instance.DAT_C2C, 67, vTransform.position);
                    puVar11.vTransform = vTransform;
                    puVar11.flags = 0x60000024;
                    puVar11.type = 8;
                    sVar1 = vVar7.id;
                    puVar11.maxHalfHealth = 300;
                    puVar11.DAT_80 = vVar7;
                    puVar11.id = sVar1;
                    puVar11.FUN_305FC();
                    GameManager.instance.FUN_30CB0(puVar11, 45);
                    GameManager.instance.FUN_30CB0(this, 45);
                    local_20 = new Vector3Int();
                    local_38 = new Vector3Int();
                    local_48 = new Vector3Int();
                    local_20.y = GameManager.instance.terrain.FUN_1B750((uint)vVar7.vTransform.position.x, (uint)vVar7.vTransform.position.z);

                    do
                    {
                        iVar3++;
                        iVar10 = (int)GameManager.FUN_2AC5C();
                        local_38.y = local_20.y - 0x5000;
                        local_38.x = vVar7.vTransform.position.x + (iVar10 * 0x1e000 >> 15) - 0xf000;
                        iVar10 = (int)GameManager.FUN_2AC5C();
                        local_48.z = vVar7.vTransform.position.z + (iVar10 * 0x1e000 >> 15) - 0xf000;
                        local_48.x = local_38.x;
                        local_48.y = local_38.y;
                        local_38.z = local_48.z;
                        LevelManager.instance.FUN_4DE54(local_48, 39);
                    } while (iVar3 < 3);

                    local_38.x = vVar7.vTransform.position.x;
                    local_38.z = vVar7.vTransform.position.z;
                    local_38.y = local_20.y;
                    local_20.x = local_38.x;
                    local_20.z = local_38.z;
                    LevelManager.instance.FUN_4DE54(local_38, 39);

                    for (int i = 0; i < 4; i++)
                    {
                        Matrix3x3 m33 = Utilities.RotMatrixYXZ_gte(new Vector3Int(0, i * 0x200, 0));
                        ccVar13 = FUN_2C5F4(0x8000);
                        uVar19 = 0;
                        uVar4 = 0xfe00;
                        GameManager.instance.FUN_2CF00(out local_20, this, ccVar13);
                        local_30 = new Vector3Int(0, 0, 0);
                        local_30.y = 0x800;

                        while (true)
                        {
                            if (uVar19 == 0 || uVar19 == 1) goto LAB_1;

                            ppcVar10 = LevelManager.instance.xobfList[19].ini.FUN_2C17C(113, typeof(Flamewall3), 8) as Flamewall3;
                            ppcVar10.flags = 0x600006b4;
                            sVar5 = vVar7.id;
                            ppcVar10.type = 8;
                            ppcVar10.maxHalfHealth = 100;
                            ppcVar10.DAT_80 = vVar7;
                            ppcVar10.id = sVar5;
                            ppcVar10.vTransform.position = puVar11.vTransform.position;
                            ppcVar10.vTransform.position.y -= 0x20000;

                            if (i != 0)
                                ppcVar10.tags = 1;

                            if ((uVar19 & 1) == 0)
                                local_30.x = (short)uVar4;
                            else
                                local_30.x = (short)uVar19 * 0x400 - 0x200;

                            local_28 = Utilities.ApplyMatrixSV(m33, local_30);
                            iVar16 = 0x100;

                            if (iVar16 < 0)
                                iVar16 += 127;

                            ppcVar10.physics1.Z = (iVar16 >> 7) * local_28.x;
                            iVar16 = -0x10000;

                            if (iVar16 < 0)
                                iVar16 += 127;

                            ppcVar10.physics1.W = (iVar16 >> 7) + local_28.y;
                            iVar16 = 0x100;

                            if (iVar16 < 0)
                                iVar16 += 127;

                            ppcVar10.physics2.X = (iVar16 >> 7) * local_28.z;
                            ppcVar10.FUN_305FC();
                            LAB_1:
                            uVar19++;
                            uVar4 -= 0x400;

                            if (5 < (int)uVar19)
                                break;
                        }
                    }

                    uVar5 = 0;
                    break;
                case 4:
                    vVar10 = (Vehicle)DAT_80;
                    GameObject obj = new GameObject();
                    puVar12 = obj.AddComponent<Quake>();

                    if (vVar10.id < 0)
                        GameManager.instance.FUN_15AA8(~vVar10.id, 30, 255, 0, 64);

                    vVar10.flags &= 0xfdffffdd;
                    vVar10.physics1.X = vVar10.vTransform.rotation.V02 * 143;
                    vVar10.physics1.Y = vVar10.vTransform.rotation.V12 * 143;
                    vVar10.physics1.Z = vVar10.vTransform.rotation.V22 * 143;
                    vVar10.physics2.X = -0x2880;
                    puVar12.screen = vTransform.position;
                    puVar12.flags = 0xa0;
                    puVar12.FUN_3066C();
                    iVar5 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E580(iVar5, vData.sndList, 3, vTransform.position);
                    GameManager.instance.FUN_1E30C(iVar5, 4595);
                    GameManager.instance.FUN_30CB0(this, 45);
                    uVar5 = 0;
                    break;
                case 5:
                    vVar7 = (Vehicle)DAT_80;
                    cVar10 = vVar7.vCamera;
                    iVar5 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E580(iVar5, vData.sndList, 3, vTransform.position);
                    GameManager.instance.FUN_1E30C(iVar5, 3649);

                    if (cVar10 != null)
                        cVar10.flags = cVar10.flags & 0xfbffffff | 0x8000000;

                    if (DAT_94.maxHalfHealth == 0)
                        DAT_94.FUN_3A368();

                    vVar7.FUN_30B78();
                    GameManager.instance.DAT_1084--;
                    vVar7.flags &= 0xf9fdffff;
                    GameManager.instance.FUN_309A0(this);
                    uVar5 = 0xffffffff;
                    break;
                default:
                    uVar5 = 0;
                    break;
            }
        }

        return uVar5;
    }
}
