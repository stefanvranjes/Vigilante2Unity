using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum _WHEELS
{
    Ground,
    Air,
    Sea,
    Snow
};

public enum _VEHICLE
{
    Wonderwagon,
    Thunderbolt,
    DakotaCycle,
    SamsonTow,
    Livingston,
    Xanadu,
    Palomino,
    ElGuerrero,
    BlueBurro,
    Excelsior,
    Tsunami,
    Marathon,
    Trekker,
    Loader,
    Stinger,
    Vertigo,
    Goliath,
    Wapiti
};

public enum _CAR_VIEW
{
    Defualt,
    Far,
    Close
}

public class Vehicle : MonoBehaviour
{
    public VigObject vObject;

    public VigObject target;
    public short turning;
    public short acceleration;
    public _WHEELS wheelsType;
    public sbyte direction;
    public byte weaponSlot;
    public byte DAT_AF; //0xAF
    public sbyte DAT_B0; //0xB0
    public sbyte DAT_B1; //0xB1
    public sbyte DAT_B2; //0xB2
    public byte DAT_B3; //0xB3
    public ushort DAT_B4; //0xB4
    public short ignition;
    public byte DAT_C0; //0xC0
    public sbyte breaking;
    public byte DAT_C2; //0xC2
    public byte DAT_C3; //0xC3
    public byte DAT_C4; //0xC4
    public byte DAT_C5; //0xC5
    public _VEHICLE vehicle; //0xDC
    public _CAR_VIEW view; //0xDD
    public byte DAT_DE; //0xDE
    public byte DAT_DF; //0xDF
    public short DAT_E0; //0xE0
    public short DAT_E2; //0xE2
    public int DAT_E4; //0xE4
    public int lightness; //0xE8
    public VigCamera vCamera;
    public ushort DAT_F6; //0xF6
    public VigObject[] body;
    public VigObject closeViewer; //0x100
    public VigObject[] wheels;
    public VigObject[] weapons;
    public ushort doubleDamage;
    public ushort shield;
    public ushort jammer;

    private VehicleConfig config;

    
    void Awake()
    {
        vObject = GetComponent<VigObject>();
        config = GetComponent<VehicleConfig>();
    }

    // Start is called before the first frame update
    void Start()
    {
        VigObject currentObj = config.FUN_2C17C(0, 308, 0); //r20
        int configID = config.dataID;
        int iVar1 = GameManager.vehicleConfigs[configID].unk0xC;

        if ((iVar1 & 240) == 0)
            iVar1 |= 48;

        currentObj.id = 0;
        currentObj.type = 2;
        currentObj.maxHalfHealth = GameManager.vehicleConfigs[configID].maxHalfHealth;
        vehicle = GameManager.vehicleConfigs[configID].vehicleID;
        DAT_E0 = 0x400;
        lightness = GameManager.vehicleConfigs[configID].lightness;
        int iVar2 = iVar1 & 255; //r30

        if (config.pointerUnk1 != 0)
            currentObj.flags |= 4;

        VigObject child2 = currentObj.child2; //r16
        DAT_E4 = -currentObj.screen.y;

        while (child2 != null)
        {
            VigObject child = child2.child; //r17

            if ((ushort)child2.id < 4)
            {
                body[(child2.id << 16 >> 14) / 4] = child2;
                child2.ai = (byte)(child2.FUN_4DCD8() + 1);
                child2.maxHalfHealth = GameManager.vehicleConfigs[configID].maxHalfHealth;
            }

            child2 = child;
        }

        int iVar3 = 0; //r18
        VigObject wheelObject; //r16
        VehicleConfigContainer nextContainer; //r19
        VehicleConfigContainer previousContainer; //r17
        GameManager.instance.commonWheelConfiguration.currentID = 0;
        wheels.CopyTo(GameManager.instance.commonWheelConfiguration.obj, 0);

        for (int i = 0; i < wheels.Length; i++)
        {
            nextContainer = config.FUN_2C590(0, iVar3 - 0x8000 & 0xFFFF);

            if (nextContainer != null)
            {
                previousContainer = config.FUN_2C6D0(nextContainer, 0);

                if (previousContainer == null)
                {
                    int iVar = 12; //r16

                    if ((GameManager.instance.DAT_40 & 1) == 0)
                        iVar = GameManager.vehicleConfigs[configID].unk0x0[(((iVar3 < 2 ? 1 : 0) ^ 1) << 1) / 2];
                    
                    wheelObject = GameManager.instance.commonWheelConfiguration.FUN_2C17C(iVar, 156, 8); //r16
                    int configIndex = (iVar << 3) - iVar << 2;
                    wheelObject.physics2.X = -GameManager.instance.commonWheelConfiguration.configContainers[configIndex / 0x1C].v3_1.y;
                    wheelObject.vr = new Vector3Int((int)GameManager.FUN_2AC5C(), 0, (iVar3 & 1) << 11);
                }
                else
                {
                    wheelObject = config.FUN_2C17C(config.FUN_2C73C(previousContainer) & 0xFFFF, 156, 8);
                    wheelObject.physics2.X = -(currentObj.screen.y + previousContainer.v3_1.y + nextContainer.v3_1.y);
                }

                wheelObject.id = wheelObject.unk2;
                wheelObject.screen = nextContainer.v3_1;
                Utilities.FUN_2CC48(currentObj, wheelObject);
                wheels[i] = wheelObject;
                nextContainer = config.FUN_2C5CC(nextContainer, 0x8000);
                wheelObject.type = 9;

                if (nextContainer == null)
                    wheelObject.physics1.X = 0;
                else
                    wheelObject.physics1.X = nextContainer.v3_1.y;

                wheelObject.physics1.Y = wheelObject.screen.y;
                int index = (iVar3 >> 1 << 1) / 2;
                wheelObject.physics1.M6 = GameManager.vehicleConfigs[configID].unk0x0[index + 2];
                wheelObject.physics1.M7 = GameManager.vehicleConfigs[configID].unk0x0[index + 4];

                if (wheelObject.vMesh != null)
                {
                    if ((wheelObject.flags & 16) == 0)
                    {
                        int iVar5 = wheelObject.physics2.X * 0x6486;

                        if (iVar5 < 0)
                            iVar5 += 0xFFF;

                        wheelObject.physics2.Y = 0x1000000 / (iVar5 >> 12);
                    }
                    else
                    {
                        wheelObject.flags &= 0xffffffef;
                        wheelObject.physics2.Y = 0;
                    }
                }

                if ((GameManager.instance.DAT_40 & 0x40000) != 0)
                    wheelObject.physics1.Y += 0x2800;

                wheelObject.physics1.Z = wheelObject.physics2.X;

                if (wheelObject.unk3 != 0)
                {
                    //...
                }

                iVar1 = (iVar2 >> iVar3 & 1) << 24;
                int iVar4;

                if ((iVar2 & 16 << iVar3) == 0)
                    iVar4 = iVar3 << 28 | 48;
                else
                    iVar4 = iVar3 << 28 | 0x2000020;

                iVar4 = iVar1 | iVar4;
                wheelObject.flags |= (uint)iVar4;
                wheelObject.ApplyTransformation();
            }

            iVar3++;
        }

        currentObj.vectorUnk1 = GameManager.vehicleConfigs[configID].vectorUnk;
        currentObj.unk1 = GameManager.vehicleConfigs[configID].unk0x2A;
        wheelsType = _WHEELS.Ground;
        direction = 1;
        DAT_B3 = GameManager.vehicleConfigs[configID].unk0x13;
        DAT_B1 = GameManager.vehicleConfigs[configID].unk0xE;
        DAT_B2 = GameManager.vehicleConfigs[configID].unk0xF;
        DAT_AF = GameManager.vehicleConfigs[configID].unk0x15;
        DAT_C3 = GameManager.vehicleConfigs[configID].unk0x10;
        DAT_C4 = GameManager.vehicleConfigs[configID].unk0x11;
        DAT_C5 = GameManager.vehicleConfigs[configID].unk0x12;
        byte[] local_28 = new byte[4]; //sp+10h
        Array.Copy(GameManager.DAT_6B204, local_28, 4);
        int iVar6;

        do
        {
            iVar3 = 0;
            iVar6 = iVar3;

            do
            {
                int iVar5 = iVar3 + 1;
                byte bVar1 = GameManager.vehicleConfigs[configID].unk0x2C[local_28[iVar3]];
                byte bVar2 = GameManager.vehicleConfigs[configID].unk0x2C[local_28[iVar5]];
                byte bVar3 = local_28[iVar3];
                byte bVar4 = local_28[iVar5];

                if (bVar1 < bVar2)
                {
                    iVar6 = 1;
                    local_28[iVar3] = bVar4;
                    local_28[iVar5] = bVar3;
                }

                iVar3 = iVar5;
            } while (iVar3 < 3);
        } while (iVar6 != 0);

        DAT_C0 = (byte)(local_28[0] | local_28[1] << 2 | local_28[2] << 4 | local_28[3] << 6);
        currentObj.pointerUnk3 = currentObj.FUN_2CA1C();
    }

    // Update is called once per frame
    private void Update()
    {
        if (vObject.id < 0 && GameManager.instance.gameMode != _GAME_MODE.Demo)
        {
            TileData tile = GameManager.instance.terrain.GetTileByPosition
                ((uint)vObject.vTransform.position.x, (uint)vObject.vTransform.position.z);

            if (tile.unk2[3] == 7)
                vObject.FUN_3BFC0();

            if ((vObject.flags & 0x20000000) == 0)
            {
                if (DAT_DF != 0)
                {
                    //sound effect FUN_1DE78
                    DAT_DE = 0;
                    DAT_DF = 0;
                }
            }
            else
            {
                if (tile.unk2[4] != DAT_DE)
                {
                    //sound effect FUN_1DE78
                    DAT_DE = (byte)tile.unk2[4];

                    if ((byte)tile.unk2[4] == 0)
                        DAT_DF = 0;
                    else
                    {
                        //function call by register
                    }
                }
            }

            FUN_3D424(InputManager.controllers[~vObject.id]);
            //FUN_3AC84(InputManager.controllers[~vObject.id]);
        }
    }

    private void FixedUpdate()
    {
        FUN_41AE8();
    }

    public void FUN_41AE8()
    {
        FUN_41B0C();
    }

    private void FUN_41B0C()
    {
        byte bVar1;
        int iVar3;
        int iVar4;
        int iVar5;
        short sVar6;

        sVar6 = 84;

        if (-1 < acceleration)
        {
            sVar6 = 85;

            if (-1 < direction)
            {
                sVar6 = 0;

                if ((DAT_F6 & 128) != 0)
                    sVar6 = 86;
            }
        }

        if (body[1] != null)
        {
            //...
        }

        vObject.flags &= 0x8fffffff;
        iVar4 = Utilities.FUN_29E84(new Vector3Int
            (vObject.physics1.X, vObject.physics1.Y, vObject.physics1.Z));

        if (iVar4 < 0)
            iVar4 += 127;

        iVar5 = vObject.physics1.X;
        vObject.physics1.W = iVar4 >> 7;

        if (iVar5 < 0)
            iVar5 += 127;

        iVar4 = vObject.physics1.Y;

        if (iVar4 < 0)
            iVar4 += 127;

        iVar3 = vObject.physics1.Z;

        if (iVar3 < 0)
            iVar3 += 127;

        iVar4 = vObject.vTransform.rotation.V02 * (iVar5 >> 7) +
                vObject.vTransform.rotation.V12 * (iVar4 >> 7) +
                vObject.vTransform.rotation.V22 * (iVar3 >> 7);

        if (iVar4 < 0)
            iVar4 += 4095;

        vObject.physics2.W = iVar4 >> 12;
            
        if (DAT_B4 == 0)
        {
            if (wheelsType == _WHEELS.Air)
                PhyAir();
            else
            {
                if (wheelsType < _WHEELS.Sea)
                {
                    iVar5 = 0;

                    if (wheelsType != _WHEELS.Ground)
                        goto LAB_41E08;

                    PhyGround();
                }
                else
                {
                    if (wheelsType == _WHEELS.Sea)
                        PhySea();
                    else
                    {
                        iVar5 = 0;

                        if (wheelsType != _WHEELS.Snow)
                            goto LAB_41E08;

                        PhySnow();
                    }
                }
            }
        }
        else
        {
            //FUN_3E774
            FUN_3E8C0();
        }

        iVar5 = 0;
        LAB_41E08:
        for (; iVar5 < 3; iVar5++)
            if (weapons[iVar5] != null)
                if (weapons[iVar5].id != 0)
                    weapons[iVar5].id -= 1;

        if (doubleDamage != 0)
            doubleDamage -= 1;

        if (shield != 0)
            shield -= 1;

        if (jammer != 0)
            jammer -= 1;

        if ((vObject.flags & 0x8000000) == 0)
        {
            if (jammer == 0)
            {
                if (-1 < vObject.id && (DAT_F6 & 2) == 0)
                {
                    vObject.screen = vObject.vTransform.position;
                    return;
                }

                iVar4 = (vObject.vTransform.position.x - vObject.screen.x) * DAT_AF;

                if (iVar4 < 0)
                    iVar4 += 255;

                vObject.screen.x += iVar4 >> 8;
                iVar4 = (vObject.vTransform.position.y - vObject.screen.y) * DAT_AF;

                if (iVar4 < 0)
                    iVar4 += 255;

                vObject.screen.y += iVar4 >> 8;
                iVar5 = vObject.screen.z;
                iVar4 = (vObject.vTransform.position.z - iVar5) * DAT_AF;

                if (iVar4 < 0)
                    iVar4 += 255;

                iVar4 = iVar4 >> 8;
            }
            else
            {
                iVar4 = vObject.vTransform.position.x - vObject.screen.x;

                if (iVar4 < 0)
                    iVar4 += 31;

                vObject.screen.x += iVar4 >> 5;
                iVar4 = vObject.vTransform.position.y - vObject.screen.y;

                if (iVar4 < 0)
                    iVar4 += 31;

                iVar5 = vObject.screen.z;
                vObject.screen.y += iVar4 >> 5;
                iVar4 = vObject.screen.z - iVar5;

                if (iVar4 < 0)
                    iVar4 += 31;

                iVar4 = iVar4 >> 5;
            }

            vObject.screen.z = iVar5 + iVar4;
        }
    }

    //FUN_40F10
    public void PhySnow()
    {
        short sVar1;
        long lVar2;
        byte bVar3;
        int uVar5;
        uint uVar6;
        uint uVar8;
        int iVar7;
        int iVar9;
        int iVar10;
        int iVar11;
        int iVar12;
        int iVar13;
        int iVar14;
        int uVar15;
        int iVar16;
        uint uVar19;
        VigTransform auStack224;
        Vector3Int auStack144;
        Vector3Int auStack104;
        Vector3Int local_100;
        Vector3Int local_f0;
        Vector3Int local_c0;
        Vector3Int local_b0;
        Vector3Int local_a0;
        int local_94;
        Vector3Int local_80;
        Vector3Int local_78;
        uint local_20;
        int local_1c;

        if (vObject.vTransform.rotation.V11 < 0)
        {
            FUN_3E8C0();
            
            for (int i = 2; i < 6; i++)
            {
                if (wheels[i] != null)
                {
                    iVar12 = wheels[i].physics2.Z;
                    wheels[i].screen.y = wheels[i].physics1.Y;
                    iVar9 = iVar12;

                    if (iVar12 < 0)
                        iVar9 = iVar12 + 63;

                    iVar12 -= iVar9 >> 6;
                    wheels[i].physics2.Z = iVar12;

                    if (wheels[i].physics2.Y != 0)
                    {
                        if (iVar12 < 0)
                            iVar12 += 4095;

                        iVar9 = (iVar12 >> 12) * wheels[i].physics2.Y;

                        if (iVar9 < 0)
                            iVar9 += 0x7ffff;

                        wheels[i].vr.x -= iVar9 >> 19;
                    }

                    wheels[i].ApplyTransformation();
                }
            }

            if (GameManager.instance.DAT_DB0 == 0)
                return;

            if (GameManager.instance.DAT_DA0 <= vObject.vTransform.position.z)
                return;

            if (vObject.vTransform.position.y <= GameManager.instance.DAT_DB0 + 0x5000)
                return;

            //FUN_391AC
        }
        else
        {
            if (GameManager.instance.DAT_DB0 != 0 && vObject.vTransform.position.z < GameManager.instance.DAT_DA0)
            {
                if (GameManager.instance.DAT_DB0 < vObject.vTransform.position.y)
                {
                    if (GameManager.instance.DAT_DB0 + 0x5000 < vObject.vTransform.position.y)
                    {
                        //FUN_391AC
                        return;
                    }

                    bVar3 = (byte)GameManager.FUN_2AC5C();

                    if ((bVar3 & 63) == 0)
                    {
                        acceleration = -120;

                        if (bVar3 == 0 && vObject.physics1.W < 1525)
                            ; //FUN_39BC4
                        else
                        {
                            //sound effects
                        }
                    }
                }
            }

            auStack224 = vObject.FUN_2AEAC();
            List<Vector3Int> positions = new List<Vector3Int>();
            List<Vector3Int> normals = new List<Vector3Int>();
            List<TileData> tiles = new List<TileData>();
            List<int> heights = new List<int>();
            local_100 = new Vector3Int(0, 0, 0);
            local_f0 = new Vector3Int(0, 0, 0);

            for (int i = 0; i < 6; i++)
            {
                if (wheels[i] != null)
                {
                    if (i < 2)
                        local_c0 = Utilities.FUN_24148(wheels[i].vTransform, wheels[i].child2.vTransform.position);
                    else
                    {
                        local_c0 = new Vector3Int(
                            wheels[i].screen.x,
                            wheels[i].screen.y + wheels[i].physics2.X,
                            wheels[i].screen.z);
                    }

                    Vector3Int position = Utilities.FUN_24148(vObject.vTransform, local_c0);
                    uVar15 = position.y;
                    TileData tile;
                    Vector3Int normal;
                    uVar5 = vObject.FUN_2CFBC(position, out normal, out tile);
                    position.y = uVar5;
                    positions.Add(position);
                    normals.Add(normal);
                    tiles.Add(tile);
                    heights.Add(uVar15);
                }
            }

            for (int i = 0; i < 6; i++)
            {
                if (wheels[i] != null)
                {
                    local_b0 = new Vector3Int(
                        wheels[i].screen.x,
                        wheels[i].screen.y + wheels[i].physics2.X,
                        wheels[i].screen.z);
                    local_78 = Utilities.FUN_24148(auStack224, local_b0);
                    local_a0 = positions[i];
                    local_94 = heights[i];
                    local_80 = normals[i];
                    TileData tile = tiles[i];

                    if (i < 2)
                    {
                        sVar1 = turning;
                        iVar9 = (int)(sVar1 & 0xfffU) * 2;
                        iVar11 = GameManager.DAT_65C90[iVar9];
                        iVar9 = GameManager.DAT_65C90[iVar9 + 1];

                        if (wheels[i].child2 != null)
                        {
                            wheels[i].child2.vr.y = sVar1;

                            if ((i & 1) != 0)
                                wheels[i].child2.vr.y = sVar1 + 2048;

                            wheels[i].child2.ApplyTransformation();
                        }
                    }
                    else
                    {
                        iVar11 = 0;

                        if (GameManager.instance.DAT_DB0 != 0)
                        {
                            iVar9 = 4096;

                            if (GameManager.instance.DAT_DA0 <= local_a0.y || local_94 <= GameManager.instance.DAT_DB0)
                                goto LAB_41360;

                            iVar12 = (int)GameManager.FUN_2AC5C();

                            if (vObject.physics1.W <= iVar12)
                                goto LAB_41360;

                            //FUN_38EF4
                        }

                        iVar9 = 4096;
                    }

                    LAB_41360:
                    auStack144 = Utilities.FUN_24304(vObject.vTransform, local_a0);
                    auStack144.y -= wheels[i].physics2.X;

                    if (auStack144.y < wheels[i].physics1.Y)
                    {
                        if (i < 4)
                        {
                            uVar6 = 0x10000000;

                            if (tile != null)
                                uVar6 = 0x30000000;

                            vObject.flags |= uVar6;
                            auStack104 = Utilities.FUN_24210(vObject.vTransform.rotation, local_80);
                            iVar12 = wheels[i].physics1.X;

                            if (wheels[i].physics1.X < auStack144.y)
                                iVar12 = auStack144.y;

                            iVar7 = wheels[i].physics1.Y;
                            sVar1 = wheels[i].physics1.M6;

                            local_c0 = new Vector3Int();

                            if (wheels[i].physics1.X < auStack144.y || wheels[i].screen.y < auStack144.y)
                            {
                                local_c0.y = (auStack144.y - wheels[i].screen.y) * wheels[i].physics1.M7;

                                if (local_c0.y < 0)
                                    local_c0.y += 31;

                                local_c0.y = local_c0.y >> 5;
                            }
                            else
                            {
                                local_c0.y = (auStack144.y - wheels[i].screen.y) * 16;
                                vObject.flags |= 0x40000000;
                            }

                            local_c0.y = ((iVar7 - iVar12) * sVar1 * 128) / auStack104.y + local_c0.y;
                            wheels[i].screen.y = auStack144.y;

                            if (tile == null)
                                iVar12 = -local_c0.y;
                            else if (tile.unk2[4] != 2)
                                iVar12 = -local_c0.y;
                            else
                                iVar12 = 0x7fffffff;

                            if (iVar11 == 0)
                            {
                                uVar6 = (uint)(local_78.x >> 5);
                                uVar8 = (uint)(local_78.z >> 2);
                            }
                            else
                            {
                                uVar6 = (uint)((long)local_78.x * iVar9);
                                local_20 = (uint)((long)local_78.z * iVar11);
                                local_1c = (int)((ulong)((long)local_78.z * iVar11) >> 32);
                                uVar19 = (uint)((long)local_78.z * iVar9);
                                uVar6 = uVar6 - local_20 >> 17 |
                                        (uint)((int)((ulong)((long)local_78.x * iVar9) >> 32) -
                                        local_1c - (uint)(uVar6 < local_20 ? 1 : 0)) * 0x8000;
                                uVar8 = (uint)(int)((long)local_78.x * iVar11) + uVar19;
                                uVar8 = uVar8 >> 14 |
                                        (uint)((int)((ulong)((long)local_78.x * iVar11) >> 32) +
                                        (int)((ulong)((long)local_78.z * iVar9) >> 32) +
                                        (uint)(uVar8 < uVar19 ? 1 : 0)) * 0x40000;
                            }

                            local_c0.z = 0;

                            if (1 < i)
                            {
                                iVar13 = acceleration;
                                iVar7 = iVar13;

                                if (iVar13 < 0)
                                    iVar7 = -iVar13;

                                iVar7 = iVar7 * DAT_C5;
                                iVar10 = iVar12;

                                if (iVar7 < iVar12)
                                    iVar10 = iVar7;

                                if (iVar13 < 0)
                                {
                                    local_c0.z = -(int)uVar8;

                                    if (0 < (int)uVar8)
                                    {
                                        iVar7 = -iVar10;

                                        if (local_c0.z < iVar7)
                                            local_c0.z = iVar7;
                                    }

                                    iVar7 = local_c0.z;

                                    if (iVar10 < local_c0.z)
                                        iVar7 = iVar10;

                                    local_c0.z = iVar7;
                                }
                                else
                                {
                                    if (direction < 1)
                                        local_c0.z = -iVar10;
                                    else
                                    {
                                        iVar7 = -(int)uVar8 >> 2;
                                        local_c0.z = iVar10;

                                        if (local_c0.z < iVar7)
                                            local_c0.z = iVar7;
                                    }
                                }
                            }

                            local_c0.x = -(int)uVar6;

                            if ((int)uVar6 < 1)
                            {
                                if (iVar12 < local_c0.x)
                                    local_c0.x = iVar12;
                            }
                            else
                            {
                                if (local_c0.x < -iVar12)
                                    local_c0.x = -iVar12;
                            }

                            if (iVar11 != 0)
                            {
                                iVar12 = iVar11 * local_c0.x;
                                local_c0.x = iVar11 * local_c0.z + iVar9 * local_c0.x >> 12;
                                local_c0.z = iVar9 * local_c0.z - iVar12 >> 12;
                            }

                            int cop2r32 = local_b0.x >> 3;
                            int cop2r34 = local_b0.y >> 3;
                            int cop2r36 = local_b0.z >> 3;
                            Coprocessor.rotationMatrix.rt11 = (short)(cop2r32 & 0xFFFF);
                            Coprocessor.rotationMatrix.rt12 = (short)(cop2r32 >> 16);
                            Coprocessor.rotationMatrix.rt22 = (short)(cop2r34 & 0xFFFF);
                            Coprocessor.rotationMatrix.rt23 = (short)(cop2r34 >> 16);
                            Coprocessor.rotationMatrix.rt33 = (short)cop2r36;
                            iVar12 = local_c0.x >> 3;

                            if (iVar12 < -0x8000)
                                iVar7 = -0x8000;
                            else
                            {
                                iVar7 = 0x7fff;

                                if (iVar12 < 0x8000)
                                    iVar7 = iVar12;
                            }

                            iVar12 = local_c0.y >> 3;

                            if (iVar12 < -0x8000)
                                iVar13 = -0x8000;
                            else
                            {
                                iVar13 = 0x7fff;

                                if (iVar12 < 0x8000)
                                    iVar13 = iVar12;
                            }

                            iVar12 = local_c0.z >> 3;

                            if (iVar12 < -0x8000)
                                iVar10 = -0x8000;
                            else
                            {
                                iVar10 = 0x7fff;

                                if (iVar12 < 0x8000)
                                    iVar10 = iVar12;
                            }

                            Coprocessor.accumulator.ir1 = (short)iVar7;
                            Coprocessor.accumulator.ir2 = (short)iVar13;
                            Coprocessor.accumulator.ir3 = (short)iVar10;
                            Coprocessor.ExecuteOP(12, false);
                            local_f0.x += local_c0.x;
                            local_f0.y += local_c0.y;
                            local_f0.z += local_c0.z;
                            iVar12 = Coprocessor.mathsAccumulator.mac1;
                            local_100.x += iVar12;
                            iVar12 = Coprocessor.mathsAccumulator.mac2;
                            local_100.y += iVar12;
                            iVar12 = Coprocessor.mathsAccumulator.mac3;
                            local_100.z += iVar12;

                            if (tile != null)
                            {
                                if (tile.unk2[3] != 0 && tile.unk2[3] != 7)
                                {
                                    //function call by register
                                }

                                if (i < 2) goto LAB_41944;

                                if (tile.unk2[4] == 2)
                                {
                                    //...
                                }
                            }
                        }
                        else
                            wheels[i].screen.y = auStack144.y;
                    }
                    else
                        wheels[i].screen.y = wheels[i].physics1.Y;

                    if (1 < i)
                    {
                        iVar16 = iVar11 * local_78.x + iVar9 * local_78.z;

                        if (iVar16 < 0)
                            iVar16 += 0xfff;

                        iVar11 = (iVar16 >> 12) * wheels[i].physics2.Y;
                        wheels[i].physics2.Z = iVar16 >> 12;

                        if (iVar11 < 0)
                            iVar11 += 0x7ffff;

                        wheels[i].vr.x -= iVar11 >> 19;
                    }
                }

                LAB_41944:
                ;
            }

            for (int i = 0; i < 6; i++)
                wheels[i].ApplyTransformation();

            local_f0 = Utilities.FUN_24094(vObject.vTransform.rotation, local_f0);
            iVar14 = vObject.physics1.W * lightness;
            local_f0.x -= (int)((ulong)((long)vObject.physics1.X * iVar14) >> 32);
            local_f0.y = (local_f0.y + 11520) -
                         (int)((ulong)((long)vObject.physics1.Y * iVar14) >> 32);
            lVar2 = (long)vObject.physics1.Z * iVar14;
            local_20 = (uint)lVar2;
            local_1c = (int)((ulong)lVar2 >> 32);
            local_f0.z -= local_1c;
            vObject.FUN_2AFF8(local_f0, local_100);
            iVar16 = vObject.physics2.X;
            iVar14 = iVar16;

            if (iVar16 < 0)
                iVar14 = iVar16 + 31;

            iVar11 = vObject.physics2.Y;
            vObject.physics2.X = iVar16 - (iVar14 >> 5);
            iVar14 = iVar11;

            if (iVar11 < 0)
                iVar14 = iVar11 + 31;

            iVar16 = vObject.physics2.Z;
            vObject.physics2.Y = iVar11 - (iVar14 >> 5);
            iVar14 = iVar16;

            if (iVar16 < 0)
                iVar14 = iVar16 + 31;

            vObject.physics2.Z = iVar16 - (iVar14 >> 5);
        }
    }

    //FUN_40598
    public void PhySea()
    {
        byte bVar1;
        long lVar2;
        bool bVar3;
        short sVar4;
        int iVar5;
        int iVar6;
        uint uVar7;
        int iVar8;
        int iVar9;
        uint uVar10;
        int iVar12;
        int local_1c;
        int local_20;
        VigTransform local_24;
        TileData local_28;
        Vector3Int local_40;
        Vector3Int local_60;
        Vector3Int local_70;
        Vector3Int auStack48;
        Vector3Int auStack80;

        if (vObject.vTransform.rotation.V11 < 0)
        {
            iVar5 = VigTerrain.instance.FUN_1B750
                ((uint)vObject.vTransform.position.x, (uint)vObject.vTransform.position.z);

            if (iVar5 < GameManager.instance.DAT_DB0)
            {
                FUN_3E8C0();
                return;
            }
        }

        VigTransform auStack144 = vObject.FUN_2AE18();
        Vector3Int local_b0 = new Vector3Int(0, 0, 0);
        Vector3Int local_a0 = new Vector3Int(0, 11520, 0);
        iVar5 = vObject.vTransform.rotation.V11;
        iVar12 = 0;

        if (0 < iVar5)
        {
            iVar12 = acceleration * DAT_C4 * iVar5;

            if (iVar12 < 0)
                iVar12 += 0x1ffff;

            iVar5 = (iVar12 >> 17) * iVar5;

            if (iVar5 < 0)
                iVar5 += 4095;

            iVar5 = iVar5 >> 12;
            iVar12 = iVar5;

            if (iVar5 < 0)
                iVar6 = vObject.physics2.W;
            else
            {
                iVar6 = direction;

                if (0 < iVar6)
                    goto LAB_406D4;

                iVar12 = 0;
            }

            if (iVar6 < 0)
                iVar12 = -iVar5;
        }

        LAB_406D4:
        bVar3 = false;
        local_24 = vObject.vTransform;
        iVar5 = 3;
        uVar10 = 0;

        do
        {
            iVar5 = iVar5 - 3;

            if (wheels[iVar5] != null)
            {
                if (1 < uVar10)
                {
                    if ((uVar10 & 1) == 0)
                        sVar4 = (short)(-turning / 2);
                    else
                        sVar4 = (short)(2048 - (((int)((uint)turning << 16) >> 16) -
                                                ((int)((uint)turning << 16) >> 31) >> 1));

                    vObject.child2.vr.y = sVar4;
                    vObject.child2.ApplyTransformation();
                    vObject.child2.child2.vr.z += iVar12 * 3;
                    vObject.child2.child2.ApplyTransformation();
                }

                local_70 = new Vector3Int(
                    wheels[iVar5].screen.x,
                    wheels[iVar5].screen.y + wheels[iVar5].DAT_78,
                    wheels[iVar5].screen.z);
                local_60 = Utilities.FUN_24148(local_24, local_70);
                iVar6 = vObject.FUN_2CFBC(local_60, out auStack48, out local_28);
                auStack80 = Utilities.FUN_24148(auStack144, local_70);

                if (local_60.z < GameManager.instance.DAT_DA0 && GameManager.instance.DAT_DB0 < iVar6)
                {
                    bVar3 = true;

                    if (GameManager.instance.DAT_DB0 < local_60.y)
                    {
                        if ((int)uVar10 < 2)
                            local_40 = new Vector3Int(0, 0, 0);
                        else
                        {
                            uVar7 = (uint)((-turning - (-turning >> 31)) * 2 & 0x3ffc);
                            local_40 = new Vector3Int();
                            local_40.x = GameManager.DAT_65C90[uVar7 / 2] * iVar12;

                            if (local_40.x < 0)
                                local_40.x += 63;

                            local_40.x = local_40.x >> 6;
                            local_40.z = GameManager.DAT_65C90[uVar7 / 2 + 1] * iVar12;

                            if (local_40.z < 0)
                                local_40.z += 63;

                            local_40.z = local_40.z >> 6;
                            local_40.y = 0;
                            local_40 = Utilities.FUN_24094(local_24.rotation, local_40);
                            iVar9 = (int)GameManager.FUN_2AC5C();

                            if (iVar9 < vObject.physics1.W)
                                ; //FUN_38F38
                        }

                        iVar9 = (iVar6 - local_60.y) * 16;
                        iVar6 = (((GameManager.instance.DAT_DB0 - local_60.y) * 90) / 24576) * 128;

                        if (iVar9 < iVar6)
                            iVar6 = iVar9;

                        local_40.y += iVar6;

                        if (auStack80.y < 1)
                        {
                            iVar6 = auStack80.y;

                            if (auStack80.y < 0)
                                iVar6 = auStack80.y + 7;

                            local_40.y -= iVar6 >> 3;
                            wheels[iVar5].flags &= 0xf7ffffff;
                        }
                        else
                        {
                            if ((int)uVar10 < 2)
                            {
                                if (-local_40.y < 2881)
                                    wheels[iVar5].flags &= 0xf7ffffff;
                                else
                                {
                                    if ((wheels[iVar5].flags & 0x8000000) == 0)
                                    {
                                        //FUN_38EF4
                                        wheels[iVar5].flags |= 0x8000000;
                                        goto LAB_409F0;
                                    }

                                    iVar6 = (int)GameManager.FUN_2AC5C();

                                    if (iVar6 < vObject.physics1.W)
                                    {
                                        //FUN_38EF4
                                        wheels[iVar5].flags |= 0x8000000;
                                    }

                                    LAB_409F0:
                                    ;
                                }
                            }
                        }

                        if (local_28 != null)
                        {
                            uVar7 = vObject.flags | 0x20000000;
                            vObject.flags = uVar7;
                        }

                        FUN_40B3C();
                    }
                }
                else
                {
                    if (iVar6 < local_60.y)
                    {
                        local_40 = new Vector3Int();

                        if ((int)uVar10 < 2)
                        {
                            local_40.z = 0;
                            local_40.x = 0;
                        }
                        else
                        {
                            uVar7 = (uint)((-turning - (-turning >> 31)) * 2 & 0x3ffc);
                            local_40.x = GameManager.DAT_65C90[uVar7 / 2] * iVar12;

                            if (local_40.x < 0)
                                local_40.x += 63;

                            local_40.x = local_40.x >> 6;
                            local_40.z = GameManager.DAT_65C90[uVar7 / 2 + 1] * iVar12;

                            if (local_40.z < 0)
                                local_40.z += 63;

                            local_40.z = local_40.z >> 6;
                            local_40.y = 0;
                            local_40 = Utilities.FUN_24094(vObject.vTransform.rotation, local_40);
                        }

                        iVar5 = auStack80.y;

                        if (auStack80.y < 0)
                            iVar5 = auStack80.y + 3;

                        local_40.y = (iVar6 - local_60.y) * 16 - (iVar5 >> 2);
                        uVar7 = 0x30000000;

                        if (local_28 != null)
                            uVar7 = 0x10000000;

                        uVar7 = vObject.flags | uVar7;
                        vObject.flags = uVar7;
                        FUN_40B3C();
                    }
                }
            }

            uVar7 = uVar10 + 1;
            iVar5 = (int)uVar10 + 4;
            uVar10 = uVar7;

            if (3 < (int)uVar7)
            {
                if (bVar3 || (vObject.flags & 0x10000000) == 0)
                    DAT_C2 = 0;
                else
                {
                    bVar1 = DAT_C2;
                    DAT_C2 = (byte)(bVar1 + 1);

                    if (60 < bVar1)
                        ; //FUN_3E32C
                }

                local_b0 = Utilities.FUN_2426C(
                    vObject.vTransform.rotation, 
                    new Matrix2x4(local_b0.x, local_b0.y, local_b0.z, 0));
                iVar5 = vObject.physics1.W * lightness;
                local_a0.y -= (int)((ulong)((long)vObject.physics1.Y * iVar5) >> 32);
                lVar2 = (long)vObject.physics1.Z * iVar5;
                local_20 = (int)lVar2;
                local_1c = (int)((ulong)lVar2 >> 32);

                if (acceleration == 0)
                {
                    iVar12 = vObject.physics1.X;

                    if (iVar12 < 0)
                        iVar12 += 63;

                    iVar12 = -(iVar12 >> 6);
                    iVar6 = vObject.physics1.Z;

                    if (iVar6 < 0)
                        iVar6 += 63;

                    iVar6 = -(iVar6 >> 6);
                }
                else
                {
                    iVar6 = -vObject.physics1.X;

                    if (0 < vObject.physics1.X)
                        iVar6 += 31;

                    iVar9 = vObject.vTransform.rotation.V02;

                    if (iVar9 < 0)
                        iVar9 = -iVar9;

                    iVar12 = 0;

                    if (0 < 4096 - iVar9)
                        iVar12 = 4096 - iVar9;

                    iVar12 = (iVar5 >> 5) * iVar12;

                    if (iVar12 < 0)
                        iVar12 += 4095;

                    iVar12 = iVar12 >> 12;
                    iVar9 = -vObject.physics1.Z;

                    if (0 < vObject.physics1.Z)
                        iVar9 += 31;

                    iVar8 = vObject.vTransform.rotation.V22;

                    if (iVar8 < 0)
                        iVar8 = -iVar8;

                    iVar6 = 0;

                    if (0 < 4096 - iVar8)
                        iVar6 = 4096 - iVar8;

                    iVar6 = (iVar9 >> 5) * iVar6;

                    if (iVar6 < 0)
                        iVar6 += 4095;

                    iVar6 = iVar6 >> 12;
                }

                local_a0.x = (local_a0.x - (int)((ulong)((long)vObject.physics1.X * iVar5) >> 32)) + iVar12;
                local_a0.z = (local_a0.z - local_1c) + iVar6;
                vObject.FUN_2AFF8(local_a0, local_b0);
                iVar12 = vObject.physics2.X;
                iVar5 = iVar12;

                if (iVar12 < 0)
                    iVar5 = iVar12 + 31;

                iVar6 = vObject.physics2.Y;
                vObject.physics2.X = iVar12 - (iVar5 >> 5);
                iVar5 = iVar6;

                if (iVar6 < 0)
                    iVar5 = iVar6 + 31;

                iVar12 = vObject.physics2.Z;
                vObject.physics2.Y = iVar6 - (iVar5 >> 5);
                iVar5 = iVar12;

                if (iVar12 < 0)
                    iVar5 = iVar12 + 31;

                vObject.physics2.Z = iVar12 - (iVar5 >> 5);
                return;
            }
        } while (true);

        void FUN_40B3C()
        {
            int cop2r32 = local_60.x - vObject.vTransform.position.x >> 3;
            int cop2r34 = local_60.y - vObject.vTransform.position.y >> 3;
            int cop2r36 = local_60.z - vObject.vTransform.position.z >> 3;
            Coprocessor.rotationMatrix.rt11 = (short)(cop2r32 & 0xFFFF);
            Coprocessor.rotationMatrix.rt12 = (short)(cop2r32 >> 16);
            Coprocessor.rotationMatrix.rt22 = (short)(cop2r34 & 0xFFFF);
            Coprocessor.rotationMatrix.rt23 = (short)(cop2r34 >> 16);
            Coprocessor.rotationMatrix.rt33 = (short)cop2r36;
            Coprocessor.accumulator.ir1 = (short)(local_40.x >> 3);
            Coprocessor.accumulator.ir2 = (short)(local_40.y >> 3);
            Coprocessor.accumulator.ir3 = (short)(local_40.z >> 3);
            Coprocessor.ExecuteOP(12, false);
            local_a0.x += local_40.x;
            local_a0.y += local_40.y;
            local_a0.z += local_40.z;
            iVar5 = Coprocessor.mathsAccumulator.mac1;
            local_b0.x += iVar5;
            iVar5 = Coprocessor.mathsAccumulator.mac2;
            local_b0.y += iVar5;
            iVar5 = Coprocessor.mathsAccumulator.mac3;
            local_b0.z += iVar5;

            if (local_28 != null)
                if (local_28.unk2[3] != 0)
                    if (local_28.unk2[3] != 7)
                        ; //function call by register
        }
    }

    //FUN_3FCC4
    public void PhyAir()
    {
        long lVar1;
        byte bVar2;
        int iVar6;
        int iVar7;
        int iVar8;
        int iVar10;
        Vector3Int local_48;
        Vector3Int auStack48;
        Vector3Int auStack56;
        TileData local_20;
        int local_18;
        int local_14;

        if (vObject.vTransform.rotation.V11 == 0)
        {
            FUN_3E8C0();

            if (GameManager.instance.DAT_DB0 == 0)
                return;

            if (GameManager.instance.DAT_DA0 <= vObject.vTransform.position.z)
                return;

            if (vObject.vTransform.position.y <= GameManager.instance.DAT_DB0 + 0x5000)
                return;

            //sound effect
            return;
        }

        if (GameManager.instance.DAT_DB0 != 0 && vObject.vTransform.position.z < GameManager.instance.DAT_DA0)
        {
            if (GameManager.instance.DAT_DB0 < vObject.vTransform.position.y)
            {
                if (GameManager.instance.DAT_DB0 + 0x5000 < vObject.vTransform.position.y)
                {
                    //sound effect
                    return;
                }

                bVar2 = (byte)GameManager.FUN_2AC5C();

                if ((bVar2 & 63) == 0)
                {
                    acceleration = -120;

                    if (bVar2 == 0 && vObject.physics1.W < 1525)
                        ; //sound effect
                    else
                    {
                        //sound effect
                    }
                }
            }
        }

        VigTransform container = vObject.FUN_2AE18();
        Vector3Int local_88 = new Vector3Int(0, 0, 0);
        Vector3Int local_78 = new Vector3Int(0, 0, 0);
        vObject.flags |= 0x10000000;
        int iVar5 = 0;

        for (int i = 0; i < 4; i++)
        {
            if (wheels[i] != null)
            {
                local_48 = Utilities.FUN_24148(vObject.vTransform, wheels[i].screen);
                iVar6 = vObject.FUN_2CFBC(local_48, out auStack56, out local_20);

                if (local_48.z < GameManager.instance.DAT_DA0 && GameManager.instance.DAT_DB0 < iVar6)
                {
                    if (GameManager.instance.DAT_36 && GameManager.instance.DAT_DB0 < local_48.y + 40960)
                    {
                        iVar7 = (int)GameManager.FUN_2AC5C();

                        if (iVar7 << 1 < vObject.physics1.W)
                            ; //FUN_38EF4
                    }
                }

                iVar6 = iVar6 - local_48.y;
                iVar7 = 409;

                if (409 < iVar6)
                    iVar7 = iVar6;

                if (i < 2)
                {
                    if (turning < 1)
                    {
                        if (i != 0)
                        {
                            iVar10 = turning;

                            if (iVar10 < 0)
                                iVar10 = -iVar10;

                            iVar7 = ((iVar10 * -32 - 10240) * GameManager.instance.gravityFactor) / iVar7;
                            goto LAB_3FFF4;
                        }
                    }
                    else
                    {
                        if (i != 1)
                        {
                            iVar10 = turning;

                            if (iVar10 < 0)
                                iVar10 = -iVar10;

                            iVar7 = ((iVar10 * -32 - 10240) * GameManager.instance.gravityFactor) / iVar7;
                            goto LAB_3FFF4;
                        }
                    }

                    iVar7 = (GameManager.instance.gravityFactor * -10240) / iVar7;
                }
                else
                {
                    iVar10 = 0;

                    if (0 < acceleration)
                        iVar10 = acceleration;

                    iVar7 = ((iVar10 * -32 - 10240) * GameManager.instance.gravityFactor) / iVar7;
                }

                LAB_3FFF4:
                if (10240 < iVar6)
                {
                    auStack48 = Utilities.FUN_24148(container, wheels[i].screen);

                    if (auStack48.y < 0)
                        iVar7 -= auStack48.y + 63 >> 6;
                }

                local_78.y += iVar7;
                iVar6 = (wheels[i].screen.z >> 3) * (iVar7 >> 3);

                if (iVar6 < 0)
                    iVar6 += 4095;

                local_88.x -= iVar6 >> 12;
                iVar6 = (wheels[i].screen.x >> 3) * (iVar7 >> 3);

                if (iVar6 < 0)
                    iVar6 += 4095;

                local_88.z += iVar6 >> 12;

                if (local_20 != null)
                {
                    if (local_20.unk2[3] != 0 && local_20.unk2[3] != 7)
                    {
                        //function jump by register
                    }

                    vObject.flags |= 0x20000000;
                }
            }
        }

        short sVar3 = 0;

        if (0 < acceleration)
            sVar3 = acceleration;

        for (int i = 0; i < 6; i++)
        {
            if (wheels[i] != null)
            {
                if (wheels[i].child2 != null)
                    wheels[i].child2.vTransform.rotation.V11 = (short)(sVar3 * 16 + 4096);
            }
        }

        iVar5 = acceleration * DAT_C3;

        if (iVar5 < 0)
            iVar5 += 31;

        iVar5 = iVar5 >> 5;
        iVar8 = iVar5;

        if (iVar5 < 0)
            iVar6 = vObject.physics2.W;
        else
        {
            iVar6 = direction;

            if (0 < iVar6)
                goto LAB_40194;

            iVar8 = 0;
        }

        if (iVar6 < 0)
            iVar8 = -iVar5;

        LAB_40194:
        local_78 = Utilities.FUN_24094(vObject.vTransform.rotation, local_78);
        iVar5 = vObject.vTransform.rotation.V02;
        iVar7 = iVar5 * iVar8;
        iVar6 = vObject.physics1.X;

        if (iVar6 < 0)
            iVar6 += 63;

        if (iVar5 < 0)
            iVar5 = -iVar5;

        iVar10 = 0;

        if (0 < 4096 - iVar5)
            iVar10 = 4096 - iVar5;

        iVar10 = (iVar6 >> 6) * iVar10;

        if (iVar10 < 0)
            iVar10 += 4095;

        local_78.x += iVar7 / 24 - (iVar10 >> 12);
        iVar5 = vObject.vTransform.rotation.V22;
        iVar7 = iVar5 * iVar8;
        iVar6 = vObject.physics1.Z;

        if (iVar6 < 0)
            iVar6 += 63;

        if (iVar5 < 0)
            iVar5 = -iVar5;

        iVar10 = 0;

        if (0 < 4096 - iVar5)
            iVar10 = 4096 - iVar5;

        iVar10 = (iVar6 >> 6) * iVar10;

        if (iVar10 < 0)
            iVar10 += 4095;

        local_78.z += iVar7 / 24 - (iVar10 >> 12);
        local_78.y += GameManager.instance.gravityFactor;

        if (3051 < vObject.physics1.W)
        {
            iVar5 = (int)(vObject.physics2.W >> 31 & 8U) / 4;
            local_48 = new Vector3Int(0, wheels[iVar5].screen.y, wheels[iVar5].screen.z);
            local_48 = Utilities.FUN_24148(vObject.vTransform, local_48);
            iVar5 = vObject.physics1.X;

            if (iVar5 < 0)
                iVar5 += 3;

            local_48.x += iVar5 >> 2;
            iVar5 = vObject.physics1.Z;

            if (iVar5 < 0)
                iVar5 += 3;

            local_48.z += iVar5 >> 2;
            iVar5 = vObject.FUN_2CFBC(local_48);

            if (iVar5 - 20480 < vObject.vTransform.position.y)
                local_78.y += (vObject.vTransform.rotation.V12 * iVar8) / 12;

            iVar5 = (iVar5 - 20480) - local_48.y;

            if (iVar5 < 0)
            {
                iVar8 = iVar5 + 31;

                if (-1 < vObject.physics2.W)
                {
                    iVar8 = -iVar5;

                    if (0 < iVar5)
                        iVar8 += 31;
                }

                vObject.physics2.X += iVar8 >> 5;
            }
        }

        iVar5 = vObject.physics1.W * lightness;
        local_78.x -= (int)((ulong)((long)vObject.physics1.X * iVar5) >> 32);
        local_78.y -= (int)((ulong)((long)vObject.physics1.Y * iVar5) >> 32);
        lVar1 = (long)vObject.physics1.Z * iVar5;
        local_18 = (int)lVar1;
        local_14 = (int)(lVar1 >> 32);
        local_78.z -= local_14;
        vObject.FUN_2AFF8(local_78, local_88);
        iVar8 = vObject.physics2.X;
        iVar5 = iVar8;

        if (iVar8 < 0)
            iVar5 = iVar8 + 31;

        iVar5 = iVar5 >> 5;
        iVar6 = iVar8;

        if (iVar8 < 0)
            iVar6 = -iVar8;

        if (1024 < iVar6)
        {
            iVar6 = iVar8;

            if (iVar8 < 0)
                iVar6 = iVar8 + 7;

            iVar5 = iVar5 + (iVar6 >> 3);
        }

        iVar6 = vObject.physics2.Z;
        vObject.physics2.X = iVar8 - iVar5;
        iVar5 = iVar6;

        if (iVar6 < 0)
            iVar5 = iVar6 + 31;

        iVar5 = iVar5 >> 5;
        iVar8 = iVar6;

        if (iVar6 < 0)
            iVar8 = -iVar6;

        if (1024 < iVar8)
        {
            iVar8 = iVar6;

            if (iVar6 < 0)
                iVar8 = iVar6 + 7;

            iVar5 += iVar8 >> 3;
        }

        iVar8 = vObject.physics2.Y;
        vObject.physics2.Z = iVar6 - iVar5;
        iVar5 = iVar8;

        if (iVar8 < 0)
            iVar5 = iVar8 + 15;

        vObject.physics2.Y = iVar8 + (turning * 2 - (iVar5 >> 4));
    }

    //FUN_3EDC4
    public void PhyGround()
    {
        long lVar1;
        long lVar2;
        short sVar3;
        byte bVar4;
        uint uVar6;
        int iVar7;
        int iVar8;
        int iVar9;
        uint uVar10;
        uint uVar11;
        int iVar12;
        uint uVar13;
        int iVar14;
        uint uVar17;
        int iVar18;
        uint uVar19;
        Vector3Int local_b8;
        Vector3Int local_a8;
        Vector3Int local_98;
        int local_8c;
        Vector3Int local_88;
        Vector3Int local_78;
        Vector3Int local_70;
        Vector3Int local_60;
        TileData local_44;
        int local_4c;
        int local_48;
        uint local_38;
        int local_34;
        uint local_30;
        int local_2c;
        uint local_28;
        int local_24;
        int local_20;

        if (vObject.vTransform.rotation.V11 < 0)
        {
            FUN_3E8C0();

            for (int i = 0; i < 6; i++)
            {
                if (wheels[i] != null)
                {
                    iVar12 = wheels[i].physics2.Z;
                    wheels[i].screen.y = wheels[i].physics1.Y;
                    iVar7 = iVar12;

                    if (iVar12 < 0)
                        iVar7 = iVar12 + 63;

                    iVar12 -= iVar7 >> 6;
                    wheels[i].physics2.Z = iVar12;

                    if (wheels[i].physics2.Y != 0)
                    {
                        if (iVar12 < 0)
                            iVar12 += 4095;

                        iVar7 = (iVar12 >> 12) * wheels[i].physics2.Y;

                        if (iVar7 < 0)
                            iVar7 += 0x7ffff;

                        wheels[i].vr.x -= iVar7 >> 19;
                    }

                    vObject.ApplyTransformation();
                }
            }

            if (GameManager.instance.DAT_DB0 == 0)
                return;

            if (GameManager.instance.DAT_DA0 <= vObject.vTransform.position.z)
                return;

            if (vObject.vTransform.position.y <= GameManager.instance.DAT_DB0 + 0x5000)
                return;

            //FUN_391AC
        }
        else
        {
            if (GameManager.instance.DAT_DB0 != 0 && vObject.vTransform.position.z < GameManager.instance.DAT_DA0)
            {
                if (GameManager.instance.DAT_DB0 < vObject.vTransform.position.y)
                {
                    if (GameManager.instance.DAT_DB0 + 0x5000 < vObject.vTransform.position.y)
                    {
                        //FUN_391AC
                        return;
                    }

                    bVar4 = (byte)GameManager.FUN_2AC5C();

                    if ((bVar4 & 63) == 0)
                    {
                        acceleration = -120;

                        if (bVar4 == 0 && vObject.physics1.W < 1525)
                            ; //FUN_39BC4
                        else
                        {
                            //sound effects
                        }
                    }

                    //FUN_39B50
                }
            }

            VigTransform auStack216 = vObject.FUN_2AEAC();
            iVar7 = 0;
            iVar9 = 0;
            List<Vector3Int> positions = new List<Vector3Int>();
            List<Vector3Int> normals = new List<Vector3Int>();
            List<int> heights = new List<int>();
            List<TileData> tiles = new List<TileData>();
            Vector3Int local_f8 = new Vector3Int(0, 0, 0);
            Vector3Int local_e8 = new Vector3Int(0, 0, 0);

            for (int i = 0; i < 6; i++)
            {
                if (wheels[i] != null)
                {
                    local_b8 = new Vector3Int
                        (wheels[i].screen.x, wheels[i].screen.y + wheels[i].physics2.X, wheels[i].screen.z);
                    Vector3Int position;
                    Vector3Int normal;
                    TileData tile;
                    position = Utilities.FUN_24148(vObject.vTransform, local_b8);
                    heights.Add(position.y);
                    position.y = vObject.FUN_2CFBC(position, out normal, out tile);
                    positions.Add(position);
                    normals.Add(normal);
                    tiles.Add(tile);
                }
            }

            for (int i = 0; i < 6; i++)
            {
                if (wheels[i] != null)
                {
                    if ((wheels[i].flags & 0x2000000) == 0)
                    {
                        local_4c = 0;
                        local_48 = 4096;
                    }
                    else
                    {
                        uVar10 = (uint)turning;

                        if (1 < i)
                            uVar10 = (uint)-(int)uVar10; //need to test this...

                        iVar9 = (int)(uVar10 & 4095) * 4;
                        wheels[i].vr.y = (short)(int)uVar10;
                        local_4c = GameManager.DAT_65C90[iVar9 / 2];
                        local_48 = GameManager.DAT_65C90[iVar9 / 2 + 1];
                    }

                    local_a8 = new Vector3Int
                        (wheels[i].screen.x, wheels[i].screen.y + wheels[i].physics2.X, wheels[i].screen.z);
                    local_70 = Utilities.FUN_24148(auStack216, local_a8);
                    local_98 = positions[i];
                    local_8c = heights[i];
                    local_78 = normals[i];
                    local_44 = tiles[i];

                    if (1 < i && GameManager.instance.DAT_DB0 != 0 && local_98.z < GameManager.instance.DAT_DA0)
                    {
                        if (GameManager.instance.DAT_DB0 < local_8c)
                        {
                            iVar9 = (int)GameManager.FUN_2AC5C();

                            if (iVar9 < vObject.physics1.W)
                                ; //FUN_38EF4
                        }
                    }

                    local_88 = Utilities.FUN_24304(vObject.vTransform, local_98);
                    local_88.y -= wheels[i].physics2.X;

                    if (local_88.y < wheels[i].physics1.Y)
                    {
                        if (i < 4)
                        {
                            uVar10 = 0x10000000;

                            if (local_44 != null)
                                uVar10 = 0x30000000;

                            vObject.flags |= uVar10;
                            uVar17 = (uint)((local_78.x << 16) >> 16);
                            uVar10 = (uint)vObject.physics1.X;
                            lVar1 = (long)((ulong)uVar17 * uVar10);
                            local_2c = (int)((uint)(ushort)local_78.y << 16);
                            local_30 = (uint)(local_2c >> 16);
                            local_2c = local_2c >> 31;
                            uVar6 = (uint)vObject.physics1.Y;
                            uVar19 = (uint)((ulong)local_30 * uVar6);
                            uVar11 = (uint)((int)((uint)(ushort)local_78.z << 16) >> 16);
                            uVar13 = (uint)vObject.physics1.Z;
                            lVar2 = (long)((ulong)uVar11 * uVar13);
                            local_38 = (uint)lVar2;
                            local_20 = (int)uVar17 * ((int)uVar10 >> 31);
                            local_34 = (int)((ulong)lVar2 >> 32) + (int)uVar11 * ((int)uVar13 >> 31) +
                                       (int)uVar13 * ((int)((uint)(ushort)local_78.z << 16) >> 31);
                            uVar11 = (uint)((int)lVar1 + (int)uVar19);
                            uVar13 = uVar11 + local_38;
                            uVar10 = (uint)(uVar13 >> 15 |
                                     ((int)((ulong)lVar1 >> 32) + local_20 +
                                     (int)uVar10 * ((local_78.x << 16) >> 31) +
                                     (int)((ulong)local_30 * uVar6 >> 32) +
                                     (int)local_30 * ((int)uVar6 >> 31) + (int)uVar6 * local_2c + (uVar11 < uVar19 ? 1 : 0) +
                                     local_34 + (uint)(uVar13 < local_38 ? 1 : 0)) * 0x20000);
                            local_60 = Utilities.FUN_24210(vObject.vTransform.rotation, local_78);
                            iVar9 = -local_60.x * (int)uVar10;

                            if (iVar9 < 0)
                                iVar9 += 4095;

                            local_b8 = new Vector3Int();
                            local_b8.x = 0;

                            if (local_a8.x - local_88.x < 0)
                                local_b8.x = local_a8.x - local_88.x;

                            iVar7 = -local_60.z * (int)uVar10;
                            local_b8.x = (iVar9 >> 12) - local_b8.x;

                            if (iVar7 < 0)
                                iVar7 += 4095;

                            local_b8.z = 0;

                            if (local_a8.z - local_88.z < 0)
                                local_b8.z = local_a8.z - local_88.z;

                            local_b8.z = (iVar7 >> 12) - local_b8.z;
                            iVar9 = wheels[i].physics1.X;

                            if (wheels[i].physics1.X < local_88.y)
                                iVar9 = local_88.y;

                            iVar7 = wheels[i].physics1.Y;
                            sVar3 = wheels[i].physics1.M6;

                            if (wheels[i].physics1.X < local_88.y || wheels[i].screen.y < local_88.y)
                            {
                                local_b8.y = (local_88.y - wheels[i].screen.y) * wheels[i].physics1.M7;

                                if (local_b8.y < 0)
                                    local_b8.y += 31;

                                local_b8.y = local_b8.y >> 5;
                            }
                            else
                            {
                                local_b8.y = (local_88.y - wheels[i].screen.y) * 16;
                                vObject.flags |= 0x40000000;
                            }

                            local_b8.y = ((iVar7 - iVar9) * sVar3 * 128) / local_60.y + local_b8.y;
                            wheels[i].screen.y = local_88.y;

                            if ((wheels[i].flags & 0x4000000) == 0)
                            {
                                if (local_44 == null)
                                    iVar9 = local_b8.y * -2;
                                else if (local_44.unk2[0] == 0)
                                    iVar9 = local_b8.y * -2;
                                else
                                    iVar9 = -local_b8.y * (256 - local_44.unk2[0]) >> 7;

                                if (local_4c == 0)
                                {
                                    uVar10 = (uint)(local_70.x >> 5);
                                    uVar6 = (uint)(local_70.z >> 2);
                                }
                                else
                                {
                                    uVar10 = (uint)((long)local_70.x * local_48);
                                    local_28 = (uint)((long)local_70.z * local_4c);
                                    local_24 = (int)((ulong)((long)local_70.z * local_4c) >> 32);
                                    local_38 = (uint)((long)local_70.x * local_4c);
                                    local_34 = (int)((ulong)((long)local_70.x * local_4c) >> 32);
                                    uVar11 = (uint)((long)local_70.z * local_48);
                                    uVar10 = (uint)(uVar10 - local_28 >> 17 |
                                             (long)(((int)((ulong)((long)local_70.x * local_48) >> 32) -
                                             local_24) - (uVar10 < local_28 ? 1 : 0)) * 0x8000);
                                    uVar6 = local_38 + uVar11;
                                    uVar6 = (uint)(uVar6 >> 14 | (long)(local_34 +
                                            (int)((ulong)((long)local_70.z * local_48) >> 32) +
                                            (uVar6 < uVar11 ? 1 : 0)) * 0x40000);
                                }

                                iVar12 = acceleration;
                                iVar7 = iVar12;

                                if (iVar12 < 0)
                                    iVar7 = -iVar12;

                                iVar8 = iVar9;

                                if ((iVar7 << 6) < iVar9)
                                    iVar8 = iVar7 << 6;

                                if (iVar12 < 0)
                                {
                                    iVar7 = -(int)uVar6;

                                    if ((int)uVar6 < 1)
                                    {
                                        iVar12 = iVar7;

                                        if (iVar8 < iVar7)
                                            iVar12 = iVar8;
                                    }
                                    else
                                    {
                                        iVar12 = -iVar8;

                                        if (iVar12 <= iVar7)
                                            goto LAB_3F738;
                                    }

                                    iVar7 = iVar12;
                                }
                                else
                                {
                                    if ((wheels[i].flags & 0x1000000) == 0)
                                        iVar7 = 0;
                                    else
                                    {
                                        if (direction < 1)
                                            iVar7 = -iVar8;
                                        else
                                        {
                                            iVar7 = iVar8;

                                            if (iVar8 < (-(int)uVar6 >> 2))
                                                iVar7 = -(int)uVar6 >> 2;
                                        }
                                    }
                                }

                                LAB_3F738:
                                if ((wheels[i].flags & 0x1000000) != 0)
                                {
                                    iVar12 = breaking;

                                    if (iVar12 != 0)
                                    {
                                        if (iVar12 < 1)
                                            iVar7 += iVar12 * -384;
                                        else
                                        {
                                            iVar7 += iVar12 * 64;
                                            iVar9 = iVar9 / 2;
                                            local_b8.y += iVar12 * -128;
                                        }
                                    }
                                }

                                iVar12 = (int)uVar6 >> 8;

                                if ((wheels[i].flags & 0x40000000) != 0)
                                {
                                    iVar8 = iVar12;

                                    if (iVar12 < 0)
                                        iVar8 = -iVar12;

                                    iVar7 -= iVar12 * iVar8 >> 5;
                                }

                                if (local_44 != null)
                                {
                                    if (local_44.unk2[1] != 0)
                                    {
                                        iVar8 = iVar12;

                                        if (iVar12 < 0)
                                            iVar8 = -iVar12;

                                        iVar7 -= iVar12 * iVar8 * local_44.unk2[1] >> 12;
                                    }
                                }

                                iVar12 = -(int)uVar10;

                                if ((int)uVar10 < 1)
                                {
                                    if (iVar9 < iVar12)
                                        iVar12 = iVar9;
                                }
                                else
                                {
                                    if (iVar12 < -iVar9)
                                        iVar12 = -iVar9;
                                }

                                if (local_4c != 0)
                                {
                                    iVar9 = local_4c * iVar12;
                                    iVar12 = local_4c * iVar7 + local_48 * iVar12 >> 12;
                                    iVar7 = local_48 * iVar7 - iVar9 >> 12;
                                }

                                local_b8.x += iVar12;
                                local_b8.z += iVar7;
                            }

                            int cop2r32 = local_a8.x >> 3;
                            int cop2r34 = local_a8.y >> 3;
                            int cop2r36 = local_a8.z >> 3;
                            Coprocessor.rotationMatrix.rt11 = (short)(cop2r32 & 0xFFFF);
                            Coprocessor.rotationMatrix.rt12 = (short)(cop2r32 >> 16);
                            Coprocessor.rotationMatrix.rt22 = (short)(cop2r34 & 0xFFFF);
                            Coprocessor.rotationMatrix.rt23 = (short)(cop2r34 >> 16);
                            Coprocessor.rotationMatrix.rt33 = (short)cop2r36;
                            iVar9 = local_b8.x >> 3;

                            if (iVar9 < -0x8000)
                                iVar7 = -0x8000;
                            else
                            {
                                iVar7 = 0x7fff;

                                if (iVar9 < 0x8000)
                                    iVar7 = iVar9;
                            }

                            iVar9 = local_b8.y >> 3;

                            if (iVar9 < -0x8000)
                                iVar12 = -0x8000;
                            else
                            {
                                iVar12 = 0x7fff;

                                if (iVar9 < 0x8000)
                                    iVar12 = iVar9;
                            }

                            iVar9 = local_b8.z >> 3;

                            if (iVar9 < -0x8000)
                                iVar8 = -0x8000;
                            else
                            {
                                iVar8 = 0x7fff;

                                if (iVar9 < 0x8000)
                                    iVar8 = iVar9;
                            }

                            Coprocessor.accumulator.ir1 = (short)iVar7;
                            Coprocessor.accumulator.ir2 = (short)iVar12;
                            Coprocessor.accumulator.ir3 = (short)iVar8;
                            Coprocessor.ExecuteOP(12, false);
                            local_e8.x += local_b8.x;
                            local_e8.y += local_b8.y;
                            local_e8.z += local_b8.z;
                            iVar9 = Coprocessor.mathsAccumulator.mac1;
                            local_f8.x += iVar9;
                            iVar9 = Coprocessor.mathsAccumulator.mac2;
                            local_f8.y += iVar9;
                            iVar9 = Coprocessor.mathsAccumulator.mac3;
                            local_f8.z += iVar9;

                            if (local_44 != null)
                            {
                                if (local_44.unk2[3] != 0 && local_44.unk2[3] != 7)
                                {
                                    //function call by register
                                }
                            }
                        }
                        else
                            wheels[i].screen.y = local_88.y;
                    }
                    else
                        wheels[i].screen.y = wheels[i].physics1.Y;

                    iVar9 = local_4c * local_70.x + local_48 * local_70.z;

                    if (iVar9 < 0)
                        iVar9 += 4095;

                    wheels[i].physics2.Z = iVar9 >> 12;

                    if (wheels[i].physics2.Y != 0)
                    {
                        iVar9 = (iVar9 >> 12) * wheels[i].physics2.Y;

                        if (iVar9 < 0)
                            iVar9 += 0x7ffff;

                        sVar3 = (short)(iVar9 >> 19);

                        if ((wheels[i].flags & 0x1000000) != 0 && 0 < breaking)
                            sVar3 += (sbyte)(breaking * 5);

                        wheels[i].vr.x -= sVar3;
                    }
                }
            }

            for (int i = 0; i < 6; i++)
                if (wheels[i] != null)
                    wheels[i].ApplyTransformation();

            local_e8 = Utilities.FUN_24094(vObject.vTransform.rotation, local_e8);
            iVar14 = vObject.physics1.W * lightness;
            local_e8.x -= (int)((ulong)((long)vObject.physics1.X * iVar14) >> 32);
            local_e8.y = (local_e8.y + 11520) -
                         (int)((ulong)((long)vObject.physics1.Y * iVar14) >> 32);
            lVar1 = (long)vObject.physics1.Z * iVar14;
            local_24 = (int)lVar1;
            local_28 = (uint)((ulong)lVar1 >> 32);
            local_e8.z -= local_24;
            vObject.FUN_2AFF8(local_e8, local_f8);
            iVar18 = vObject.physics2.X;
            iVar14 = iVar18;

            if (iVar18 < 0)
                iVar14 = iVar18 + 31;

            iVar9 = vObject.physics2.Y;
            vObject.physics2.X = iVar18 - (iVar14 >> 5);
            iVar14 = iVar9;

            if (iVar9 < 0)
                iVar14 = iVar9 + 31;

            iVar18 = vObject.physics2.Z;
            vObject.physics2.Y = iVar9 - (iVar14 >> 5);
            iVar14 = iVar18;

            if (iVar18 < 0)
                iVar14 = iVar18 + 31;

            vObject.physics2.Z = iVar18 - (iVar14 >> 5);
        }
    }

    /*private void FUN_3E8C0()
    {
        int iVar1 = 0; //r22

        if (DAT_B4 == 0)
            iVar1 = (weaponSlot == 0 ? 1 : 0) << 2;

        int iVar2 = 0; //r17
        Vector3Int local_v1 = new Vector3Int(0, 0, 0); //sp+10h
        Vector3Int local_v2 = new Vector3Int(0, GameManager.instance.gravityFactor, 0); //sp+20h
        Vector3Int rect1 = new Vector3Int(0, 0, 0); //sp+30h
        Vector3Int rect2 = new Vector3Int(0, 0, 0); //sp+40h
        TileData onTile; //sp+50h

        do
        {
            if ((iVar2 & iVar1) == 0)
            {
                if ((iVar2 & 1) == 0)
                    rect1.x = vObject.vCollider.min.x;
                else
                    rect1.x = vObject.vCollider.max.x;

                if ((iVar2 & 4) == 0)
                    rect1.y = vObject.vCollider.min.y;
                else
                    rect1.y = vObject.vCollider.max.y;

                if ((iVar2 & 2) == 0)
                    rect1.z = vObject.vCollider.min.z;
                else
                    rect1.z = vObject.vCollider.max.z;

                FUN_3EA0C();
            }
            else
            {
                if (wheels[iVar2 - 4] != null)
                {
                    rect1.x = wheels[iVar2 - 4].screen.x;
                    rect1.y = wheels[iVar2 - 4].screen.y + wheels[iVar2 - 4].physics2.X;
                    rect1.z = wheels[iVar2 - 4].screen.z;
                    FUN_3EA0C();
                }
            }

            iVar2++;

            if (7 < iVar2)
            {
                local_v1 = Utilities.FUN_2426C
                    (vObject.vTransform.rotation, 
                    new Matrix2x4(local_v1.x, local_v1.y, local_v1.z, 0));
                int iVar3 = vObject.physics1.W * lightness; //r5
                long lVar1 = (long)vObject.physics1.X * iVar3; //sp+58h, sp+5Ch
                local_v2.x -= (int)(lVar1 >> 32 >> 0);
                lVar1 = (long)vObject.physics1.Y * iVar3;
                local_v2.y -= (int)(lVar1 >> 32 >> 0);
                lVar1 = (long)vObject.physics1.Z * iVar3;
                local_v2.z -= (int)(lVar1 >> 32 >> 0);
                vObject.FUN_2AFF8(local_v2, local_v1);
                int iVar4 = vObject.physics2.X;
                iVar3 = iVar4;

                if (iVar4 < 0)
                    iVar3 = iVar4 + 31;

                int iVar5 = vObject.physics2.Y;
                vObject.physics2.X = iVar4 - (iVar3 >> 5);
                iVar3 = iVar5;

                if (iVar5 < 0)
                    iVar3 = iVar5 + 31;

                iVar4 = vObject.physics2.Z;
                vObject.physics2.Y = iVar5 - (iVar3 >> 5);
                iVar3 = iVar4;

                if (iVar4 < 0)
                    iVar3 = iVar4 + 31;

                vObject.physics2.Z = iVar4 - (iVar3 >> 5);

                if ((vObject.flags & 0x40000000) != 0)
                {
                    long lVar2 = (long)vObject.physics1.Y * 0x6BCA0000;
                    bool isPlayer = 0 < vObject.id;
                    FUN_3A020((int)-((lVar2 >> 32 >> 13) - (vObject.physics1.Y >> 31)), 0, isPlayer);
                }

                return;
            }
        } while(true);

        void FUN_3EA0C()
        {
            rect1 = Utilities.FUN_24148(vObject.vTransform, rect1);
            int h = vObject.FUN_2CFBC(rect1, out onTile);

            if (0 < rect1.y - h)
            {
                int iVar3 = -vObject.physics1.X;

                if (0 < vObject.physics1.X)
                    iVar3 += 3;

                iVar3 = iVar3 >> 2;

                if (iVar3 < -0xB40)
                    rect2.x = -0xB40;
                else
                {
                    rect2.x = 0xB40;

                    if (iVar3 < 0xB41)
                        rect2.x = iVar3;
                }

                iVar3 = -vObject.physics1.Z;

                if (0 < vObject.physics1.Z)
                    iVar3 += 3;

                iVar3 = iVar3 >> 2;

                if (iVar3 < -0xB40)
                    rect2.z = -0xB40;
                else
                {
                    rect2.z = 0xB40;

                    if (iVar3 < 0xB41)
                        rect2.z = iVar3;
                }

                rect2.y = rect1.y - h;

                if (0 < vObject.physics1.Y)
                    rect2.y -= vObject.physics1.Y >> 2;

                int cop2r32 = rect1.x - (vObject.vTransform.position.x >> 3);
                int cop2r34 = rect1.y - (vObject.vTransform.position.y >> 3);
                int cop2r36 = rect1.z - (vObject.vTransform.position.z >> 3);
                Coprocessor.rotationMatrix.rt11 = (short)cop2r32;
                Coprocessor.rotationMatrix.rt12 = (short)(cop2r32 >> 16);
                Coprocessor.rotationMatrix.rt22 = (short)cop2r34;
                Coprocessor.rotationMatrix.rt23 = (short)(cop2r34 >> 16);
                Coprocessor.rotationMatrix.rt33 = (short)cop2r36;
                Coprocessor.accumulator.ir1 = (short)(rect2.x >> 3);
                Coprocessor.accumulator.ir2 = (short)(rect2.y >> 3);
                Coprocessor.accumulator.ir3 = (short)(rect2.z >> 3);
                Coprocessor.ExecuteOP(12, false);
                local_v2.x += rect2.x;
                local_v2.y += rect2.y;
                local_v2.z += rect2.z;
                local_v1.x += Coprocessor.mathsAccumulator.mac1;
                local_v1.y += Coprocessor.mathsAccumulator.mac2;
                local_v1.z += Coprocessor.mathsAccumulator.mac3;

                if (onTile != null)
                {
                    if (onTile.unk2[3] != 0 && onTile.unk2[3] != 7)
                    {
                        //call function by register
                    }
                }

                if (0x4C00 < vObject.physics1.Y)
                    vObject.flags |= 0x40000000;
            }
        }
    }*/

    private void FUN_3E8C0()
    {
        long lVar1;
        int iVar2;
        int iVar3;
        int iVar4;
        uint uVar5;
        uint uVar6;
        Vector3Int local_60;
        Vector3Int local_50;
        Vector3Int local_40;
        Vector3Int local_30;
        TileData local_20;
        int local_18;
        int local_14;

        uVar6 = 0;

        if (DAT_B4 == 0)
            uVar6 = (uint)(wheelsType == _WHEELS.Ground ? 1 : 0) << 2;

        uVar5 = 0;
        local_60 = new Vector3Int(0, 0, 0);
        local_50 = new Vector3Int(0, GameManager.instance.gravityFactor, 0);

        do
        {
            local_40 = new Vector3Int();
            local_30 = new Vector3Int();

            if ((uVar5 & uVar6) == 0)
            {
                if ((uVar5 & 1) == 0)
                    local_40.x = vObject.vCollider.min.x;
                else
                    local_40.x = vObject.vCollider.max.x;

                if ((uVar5 & 4) == 0)
                    local_40.y = vObject.vCollider.min.y;
                else
                    local_40.y = vObject.vCollider.max.y;

                if ((uVar5 & 2) == 0)
                    local_40.z = vObject.vCollider.min.z;
                else
                    local_40.z = vObject.vCollider.max.z;

                FUN_3EA0C();
            }
            else
            {
                iVar3 = (int)uVar5 - 4;

                if (wheels[iVar3] != null)
                {
                    local_40.x = wheels[iVar3].screen.x;
                    local_40.y = wheels[iVar3].screen.y + wheels[iVar3].physics2.X;
                    local_40.z = wheels[iVar3].screen.z;
                    FUN_3EA0C();
                }
            }

            uVar5++;

            if (7 < (int)uVar5)
            {
                local_60 = Utilities.FUN_2426C(
                    vObject.vTransform.rotation,
                    new Matrix2x4(local_60.x, local_60.y, local_60.z, 0));
                iVar2 = vObject.physics1.W * lightness;
                local_50.x -= (int)((ulong)((long)vObject.physics1.X * iVar2) >> 32);
                local_50.y -= (int)((ulong)((long)vObject.physics1.Y * iVar2) >> 32);
                lVar1 = (long)vObject.physics1.Z * iVar2;
                local_18 = (int)lVar1;
                local_14 = (int)((ulong)lVar1 >> 32);
                local_50.z -= local_14; //need to inspect this
                vObject.FUN_2AFF8(local_50, local_60);
                iVar3 = vObject.physics2.X;
                iVar2 = iVar3;

                if (iVar3 < 0)
                    iVar2 = iVar3 + 31;

                iVar4 = vObject.physics2.Y;
                vObject.physics2.X = iVar3 - (iVar2 >> 5);
                iVar2 = iVar4;

                if (iVar4 < 0)
                    iVar2 = iVar4 + 31;

                iVar3 = vObject.physics2.Z;
                vObject.physics2.Y = iVar4 - (iVar2 >> 5);
                iVar2 = iVar3;

                if (iVar3 < 0)
                    iVar2 = iVar3 + 31;

                vObject.physics2.Z = iVar3 - (iVar2 >> 5);

                if ((vObject.flags & 0x40000000) != 0)
                    FUN_3A020(-(vObject.physics1.Y / 19456), 0, 0 < vObject.id); //tmp

                return;
            }
        } while (true);

        void FUN_3EA0C()
        {
            local_40 = Utilities.FUN_24148(vObject.vTransform, local_40);
            iVar3 = vObject.FUN_2CFBC(local_40, out local_20);

            if (0 < local_40.y - iVar3)
            {
                iVar4 = -vObject.physics1.X;

                if (0 < vObject.physics1.X)
                    iVar4 += 3;

                iVar4 = iVar4 >> 2;

                if (iVar4 < -2880)
                    local_30.x = -2880;
                else
                {
                    local_30.x = 2880;

                    if (iVar4 < 2881)
                        local_30.x = iVar4;
                }

                iVar4 = -vObject.physics1.Z;

                if (0 < vObject.physics1.Z)
                    iVar4 += 3;

                iVar4 = iVar4 >> 2;

                if (iVar4 < -2880)
                    local_30.z = -2880;
                else
                {
                    local_30.z = 2880;

                    if (iVar4 < 2881)
                        local_30.z = iVar4;
                }

                local_30.y = -(local_40.y - iVar3);

                if (0 < vObject.physics1.Y)
                    local_30.y -= vObject.physics1.Y >> 2;

                int cop2r32 = local_40.x - vObject.vTransform.position.x >> 3;
                int cop2r34 = local_40.y - vObject.vTransform.position.y >> 3;
                int cop2r36 = local_40.z - vObject.vTransform.position.z >> 3;
                Coprocessor.rotationMatrix.rt11 = (short)(cop2r32 & 0xFFFF);
                Coprocessor.rotationMatrix.rt12 = (short)(cop2r32 >> 16);
                Coprocessor.rotationMatrix.rt22 = (short)(cop2r34 & 0xFFFF);
                Coprocessor.rotationMatrix.rt23 = (short)(cop2r34 >> 16);
                Coprocessor.rotationMatrix.rt33 = (short)cop2r36;
                Coprocessor.accumulator.ir1 = (short)(local_30.x >> 3);
                Coprocessor.accumulator.ir2 = (short)(local_30.y >> 3);
                Coprocessor.accumulator.ir3 = (short)(local_30.z >> 3);
                Coprocessor.ExecuteOP(12, false);
                local_50.x += local_30.x;
                local_50.y += local_30.y;
                local_50.z += local_30.z;
                iVar3 = Coprocessor.mathsAccumulator.mac1;
                local_60.x += iVar3;
                iVar3 = Coprocessor.mathsAccumulator.mac2;
                local_60.y += iVar3;
                iVar3 = Coprocessor.mathsAccumulator.mac3;
                local_60.z += iVar3;

                if (local_20 != null)
                {
                    if (local_20.unk2[3] != 0 && local_20.unk2[3] != 7)
                    {
                        //function call by register
                    }
                }

                if (19456 < vObject.physics1.Y)
                    vObject.flags |= 0x40000000;
            }
        }
    }

    private int FUN_3A020(int pInt1, int pInt2, bool isPlayer)
    {
        int iVar1;

        if (shield == 0)
            iVar1 = FUN_39DCC(pInt1, pInt2, isPlayer);
        else
        {
            FUN_393F8();
            iVar1 = 0;
        }

        return iVar1;
    }

    private int FUN_39DCC(int pInt1, int pInt2, bool isPlayer)
    {
        return 0;
    }

    private void FUN_393F8()
    {

    }

    private void FUN_39CEC(uint param1)
    {
        short sVar2;
        int iVar3;
        short uVar4;

        if ((param1 & 0xffff) == 0)
        {
            //sound effect
        }
        else
        {
            acceleration = 0;
            sVar2 = (short)(ignition - 1);
            ignition = sVar2;

            if (sVar2 == -1)
                ; //sound effect
            else
            {
                if ((param1 & 0xffff0000) != 0)
                {
                    iVar3 = (int)GameManager.FUN_2AC5C();
                    uVar4 = 6;

                    if (iVar3 * 5 >> 15 != 0)
                        uVar4 = 90;

                    ignition = uVar4;
                    //...
                }
            }
        }
    }

    private void FUN_3A844()
    {
        sbyte cVar1;
        byte bVar2;
        byte uVar3;
        uint uVar4;
        int iVar6;
        int iVar7;
        uint uVar8;
        uint unaff_s1;

        if ((DAT_F6 & 16) == 0)
        {
            if (wheelsType == _WHEELS.Ground)
            {
                if ((vObject.flags & 0x10000000) == 0)
                {
                    iVar6 = 3072;

                    if (0 < acceleration)
                        iVar6 = 8192;
                }
                else
                {
                    iVar7 = breaking << 6;

                    if (breaking < 1)
                    {
                        iVar7 = vObject.physics1.W * GameManager.DAT_63F68[direction + 1];

                        if (iVar7 < 0)
                            iVar7 += 4095;

                        iVar7 = iVar7 >> 12;

                        if (iVar7 < 3072 && 1 < direction)
                            direction -= 1;

                        if (0x2000 < iVar7)
                        {
                            bVar2 = (byte)direction;

                            if (bVar2 < 3 && -1 < bVar2 << 24)
                                direction = (sbyte)(bVar2 + 1);
                        }
                    }

                    iVar6 = 3072;

                    if (3072 < iVar7)
                        iVar6 = iVar7;
                }
            }
            else
                iVar6 = vObject.physics1.W / 2;

            iVar6 -= DAT_E0;
            iVar7 = -512;

            if (-513 < iVar6)
            {
                iVar7 = 512;

                if (iVar6 < 513)
                    iVar7 = iVar6;
            }

            iVar7 = DAT_E0 + iVar7;
            DAT_E0 = (short)iVar7;
            //sound effect
            Controller playerController = InputManager.controllers[~vObject.id];
            if (((playerController.DAT_B << 24 | playerController.DAT_A << 16 | 
                playerController.steering << 8 | playerController.actions) & 0x100) == 0)
            {
                iVar7 = DAT_E2 - 128;
                iVar6 = 2048;

                if (2048 < iVar7)
                    iVar6 = iVar7;

                DAT_E2 = (short)iVar6;
            }
            else
            {
                iVar7 = DAT_E2 + 128;
                iVar6 = 4096;

                if (iVar7 < 4096)
                    iVar6 = iVar7;

                DAT_E2 = (short)iVar6;
            }

            unaff_s1 = GameManager.instance.FUN_1E478(vObject.vTransform.position);
            iVar6 = (int)(unaff_s1 & 0xffff) * DAT_E2;
            cVar1 = vObject.DAT_18;

            if (iVar6 < 0)
                iVar6 += 4095;

            iVar7 = (int)(unaff_s1 >> 16) * DAT_E2;

            if (iVar7 < 0)
                iVar7 += 4095;

            uVar4 = (uint)(iVar6 >> 12 | (iVar7 >> 12) << 16);
        }
        else
        {
            uVar4 = GameManager.instance.FUN_1E478(vObject.vTransform.position);
            cVar1 = vObject.DAT_18;
        }

        //FUN_1E2C8

        if ((vObject.flags & 0x40000000) != 0)
        {
            //...
        }

        if (wheelsType== _WHEELS.Ground && (vObject.flags & 0x10000000) != 0 && 3051 < vObject.physics1.W)
        {
            iVar7 = vObject.physics2.W;
            iVar6 = vObject.physics1.W * 3;

            if (iVar7 < 0)
                iVar7 = -iVar7;

            if (iVar6 < 0)
                iVar6 += 3;

            if (iVar7 < iVar6 >> 2)
            {
                if ((DAT_F6 & 4) == 0)
                {
                    DAT_F6 |= 4;
                    //...
                }

                //FUN_3928C
                goto LAB_3ABC8;
            }
        }

        DAT_F6 &= 0xfffb;

        LAB_3ABC8:
        if (DAT_DF != 0)
        {
            uVar4 = (uint)(vObject.physics1.W / 2);

            if (uVar4 < 768)
            {
                uVar3 = DAT_DF;
                unaff_s1 = 0;
            }
            else
            {
                uVar8 = 3072;

                if (uVar4 < 3072)
                    uVar8 = uVar4;

                //FUN_1E2E8
                uVar3 = DAT_DF;
            }

            //FUN_1E2C8
        }
    }

    public void FUN_3AC84(Controller playerController)
    {
        Vehicle iVar2;
        int iVar3;
        uint uVar5;
        uint uVar8;

        uVar8 = (uint)(playerController.DAT_B << 24 | playerController.DAT_A << 16 |
                playerController.steering << 8 | playerController.actions);

        if ((vObject.flags & 0x2000000) == 0)
        {
            if ((DAT_F6 & 64) != (uVar8 & 64))
            {
                iVar2 = GameManager.instance.players[~vObject.id];
                DAT_F6 ^= DAT_F6;

                if ((uVar8 & 64) == 0)
                    iVar2.vObject.physics2.M1 = 0;
                else
                    iVar2.vObject.physics2.M1 = 2048;

                closeViewer.vr.y = vObject.physics2.M1;
                vObject.vectorUnk1.x = -vObject.vectorUnk1.x;
                closeViewer.ApplyRotationMatrix();
            }
        }

        if ((uVar8 & 0x180000) != 0)
        {
            uVar5 = 0xffffffff;

            if ((uVar8 & 0x80000) != 0)
                uVar5 = 1;

            //...
        }
    }

    private void FUN_3D0F8(uint param1)
    {
        sbyte cVar1;
        sbyte cVar2;
        int iVar3;
        int iVar4;
        int iVar8;

        if (wheelsType == _WHEELS.Air)
        {
            if ((param1 & 0xffff) == 0)
            {
                iVar3 = DAT_B0 - 1;
                cVar1 = 0;

                if (0 < iVar3)
                    cVar1 = (sbyte)iVar3;

                DAT_B0 = cVar1;
                return;
            }

            if ((param1 & 0xffff0000) != 0)
            {
                cVar1 = -120;

                if (DAT_B0 == 0)
                    cVar1 = 15;

                DAT_B0 = cVar1;
            }

            if (DAT_B0 < 0)
            {
                if (0 < direction)
                {
                    iVar3 = GameManager.instance.terrain.FUN_1B750
                        ((uint)vObject.vTransform.position.x, (uint)vObject.vTransform.position.z);

                    if ((GameManager.instance.DAT_40 & 0x80000) == 0)
                        iVar4 = -0x32000;
                    else
                        iVar4 = -0x12C000;

                    iVar4 = (iVar3 - vObject.vTransform.position.y) + iVar4;

                    if (iVar4 < 0)
                    {
                        iVar3 = -0x32000;

                        if (-0x32000 < iVar4)
                            iVar3 = iVar4;

                        if (iVar3 < 0)
                            iVar3 += 1023;

                        iVar3 = iVar3 >> 10;
                        iVar4 = vObject.vTransform.rotation.V01 * iVar3;

                        if (iVar4 < 0)
                            iVar4 += 31;

                        iVar8 = vObject.vTransform.rotation.V11 * iVar3;
                        vObject.physics1.X += iVar4 >> 5;

                        if (iVar8 < 0)
                            iVar8 += 31;

                        iVar3 = vObject.vTransform.rotation.V21 * iVar3;
                        vObject.physics1.Y += (iVar8 >> 5) - GameManager.instance.gravityFactor;

                        if (iVar3 < 0)
                            iVar3 += 31;

                        vObject.physics1.Z += iVar3 >> 5;
                    }
                }

                cVar2 = (sbyte)(DAT_B0 + 1);
            }
            else
            {
                iVar3 = DAT_B0 - 1;
                cVar2 = 0;

                if (0 < iVar3)
                    cVar2 = (sbyte)iVar3;
            }
        }
        else
        {
            if ((param1 & 0xffff) != 0)
            {
                if ((param1 & 0xffff0000) != 0 && (vObject.flags & 0x10000000) != 0)
                {
                    if (DAT_B0 < 1 || (8391 < vObject.physics1.W || direction < 1))
                    {
                        if (2287 < vObject.physics1.W)
                            goto LAB_3D36C;
                    }
                    else
                    {
                        vObject.FUN_2B1FC(GameManager.instance.DAT_A18, GameManager.instance.DAT_A24);
                        DAT_B0 = -39;
                    }

                    //...
                }

                LAB_3D36C:
                cVar2 = (sbyte)(DAT_B0 + 1);

                if (-2 < DAT_B0)
                    cVar2 = 15;

                DAT_B0 = cVar2;

                if (wheelsType != _WHEELS.Ground)
                    return;

                if (breaking == 0)
                    return;

                //...
                return;
            }

            cVar2 = (sbyte)(DAT_B0 - 1);

            if (DAT_B0 < 1)
                return;
        }

        DAT_B0 = cVar2;
    }

    public void FUN_3D424(Controller playerController)
    {
        _WHEELS cVar1;
        bool bVar2;
        bool bVar3;
        short sVar4;
        short sVar5;
        ushort uVar6;
        uint uVar7;
        int iVar8;
        uint uVar9;
        sbyte uVar10;
        uint uVar11;
        int iVar12;
        uint uVar14;
        int iVar15;
        uint unaff_s1;
        uint uVar17;
        bool bVar18;
        uint uVar19;

        cVar1 = wheelsType;
        uVar17 = (uint)(playerController.DAT_B << 24 | playerController.DAT_A << 16 |
                        playerController.steering << 8 | playerController.actions);
        unaff_s1 = 0; //original code does not contain

        if (playerController.type == _CONTROLLER_TYPE.SteeringWheel)
        {
            turning = (short)((playerController.stick[0] - 128) * 5);
            uVar19 = (uint)(playerController.stick[1] < 129 ? 1 : 0) ^ 1;

            if (-1 < (sbyte)playerController.DAT_14[1])
                uVar19 |= 0x10000;

            if ((DAT_F6 & 16) == 0)
            {
                if (direction < 0 && 16 < playerController.stick[1])
                {
                    uVar10 = 1;
                    direction = uVar10;
                }
                else
                {
                    uVar10 = -1;

                    if ((playerController.actions & 0x100) != 0)
                        direction = uVar10;
                }

                uVar7 = DAT_B3;

                if (direction < 0)
                {
                    if ((playerController.actions & 0x100) == 0)
                        uVar7 = 0;
                }
                else
                {
                    uVar14 = playerController.stick[1];

                    if (playerController.stick[2] < 241)
                        uVar14 -= playerController.stick[2];
                    else
                    {
                        iVar8 = turning;

                        if (iVar8 < 0)
                            iVar8 = -iVar8;

                        if (iVar8 < 170)
                            uVar14 -= playerController.stick[2];
                    }

                    uVar14 = uVar14 * uVar7;
                    uVar7 = uVar14 >> 8;

                    if ((int)uVar14 < 0)
                        uVar7 = uVar14 + 255 >> 8;
                }

                acceleration = (short)uVar7;
                FUN_3D0F8(uVar19);
            }
            else
            {
                FUN_39CEC(uVar19);
            }

            if (vObject.vTransform.rotation.V11 < 0)
            {
                uVar17 = playerController.stick[0];

                if ((int)(uVar17 ^ playerController.stick[0]) < 0)
                    uVar17 = 0;

                iVar12 = vObject.physics2.Z;
                iVar8 = (int)(uVar17 << 2);

                if (cVar1 != _WHEELS.Sea)
                {
                    vObject.physics2.Z = iVar12 + (int)uVar17;
                    return;
                }

                vObject.physics2.Z = iVar12 + iVar8;
                return;
            }

            if ((uVar17 & 0x400) == 0 && playerController.stick[0] < 241)
            {
                if (0 < breaking)
                    breaking = (sbyte)-breaking;

                if (cVar1 == _WHEELS.Ground)
                {
                    iVar8 = vObject.physics1.W * DAT_B2;

                    if (iVar8 < 0)
                        iVar8 += 4095;

                    iVar12 = DAT_B1 + (iVar8 >> 12);
                    iVar8 = 0;

                    if (0 < iVar12)
                        iVar8 = iVar12;

                    if (direction < 0)
                        iVar8 = -iVar8;

                    iVar8 = turning * iVar8;

                    if (iVar8 < 0)
                        iVar8 += 15;

                    iVar8 = vObject.physics2.Y + (iVar8 >> 4);
                    vObject.physics2.Y = iVar8;
                }
            }
            else
            {
                iVar8 = turning;

                if (iVar8 < 0)
                    iVar8 = -iVar8;

                if (iVar8 < 170)
                {
                    if (vObject.physics1.W < 2370)
                    {
                        uVar10 = 127;

                        if (uVar19 == 0)
                        {
                            uVar10 = 0;
                            iVar8 = breaking - 3;
                            bVar2 = 0 < iVar8;
                        }
                        else
                        {
                            iVar8 = breaking + 2;
                            bVar2 = iVar8 < 127;
                        }

                        if (bVar2)
                            uVar10 = (sbyte)iVar8;

                        breaking = uVar10;
                    }

                    acceleration = (short)(DAT_B3 * -2);
                }
                if (cVar1 != _WHEELS.Air)
                {
                    if (direction < 0)
                        iVar8 = vObject.physics2.Y + turning * -2;
                    else
                        iVar8 = vObject.physics2.Y + turning * 2;

                    vObject.physics2.Y = iVar8;
                }
            }

            iVar8 = breaking + 4;

            if (-1 < breaking)
                return;

            uVar10 = 0;

            if (iVar8 < 0)
                uVar10 = (sbyte)iVar8;

            breaking = uVar10;
            return;
        }

        if (playerController.type < _CONTROLLER_TYPE.JoystickAnalog)
        {
            if (playerController.type != _CONTROLLER_TYPE.JoypadDigital)
                return;

            uVar19 = 0x100;

            if (direction < 0)
            {
                uVar19 = 0x200;
                uVar7 = 0x100;
            }
            else
                uVar7 = 0x200;

            if ((uVar17 & uVar7) == 0)
            {
                unaff_s1 = uVar17 & (uVar19 | uVar19 << 16);

                if ((DAT_F6 & 16) == 0)
                {
                    uVar7 = unaff_s1;

                    if (((playerController.DAT_F << 24 | playerController.DAT_E << 16
                        | playerController.dpad << 8 | playerController.buttons << 0)
                        & 0xf0000000) != 0)
                        uVar7 = unaff_s1 & 0xffff;

                    FUN_3D0F8(uVar7);

                    if ((unaff_s1 & 0xffff) == 0)
                    {
                        iVar8 = acceleration - 4;
                        uVar11 = 0;

                        if (0 < iVar8)
                            uVar11 = (uint)iVar8;

                        acceleration = (short)uVar11;
                    }
                    else
                        acceleration = DAT_B3;
                }
                else
                    FUN_39CEC(unaff_s1);
            }
            else
            {
                iVar8 = vObject.physics2.W;

                if (iVar8 < 0)
                    iVar8 = -iVar8;

                if (iVar8 < 474)
                {
                    uVar10 = 1;

                    if (-1 < direction)
                        uVar10 = -1;

                    direction = uVar10;
                }
                else
                    acceleration = (short)-DAT_B3;
            }

            if ((uVar17 & 0x1800) == 0)
            {
                sVar5 = turning;
                iVar8 = sVar5;

                if (iVar8 != 0)
                {
                    iVar12 = iVar8 * vObject.physics2.W;

                    if (iVar12 < 0)
                        iVar12 += 0x7fff;

                    sVar4 = (short)(sVar5 - (iVar12 >> 15));

                    if (iVar12 >> 15 == 0)
                    {
                        sVar4 = (short)(sVar5 - 1);

                        if (iVar8 < 0)
                            sVar4 = (short)(sVar5 + 1);
                    }

                    turning = sVar4;
                }

                if ((uVar17 & 0x400) == 0)
                {
                    if (0 < breaking)
                        breaking = (sbyte)-breaking;
                }
                else
                {
                    if (vObject.physics1.W < 2370)
                    {
                        uVar10 = 127;

                        if (unaff_s1 == 0)
                        {
                            uVar10 = 0;
                            iVar8 = breaking - 3;
                            bVar2 = 0 < iVar8;
                        }
                        else
                        {
                            iVar8 = breaking + 2;
                            bVar2 = iVar8 < 127;
                        }

                        if (bVar2)
                            uVar10 = (sbyte)iVar8;

                        breaking = uVar10;
                    }

                    acceleration = (short)(DAT_B3 * -2);
                }

                iVar8 = breaking + 4;

                if (-1 < breaking)
                    return;

                uVar10 = 0;

                if (iVar8 < 0)
                    uVar10 = (sbyte)iVar8;

                breaking = uVar10;
                return;
            }

            if (0 < vObject.vTransform.rotation.V11)
            {
                uVar7 = 0;

                if (direction < 1)
                {
                    if ((uVar17 & 0x18000000) != 0)
                    {
                        uVar7 = vObject.flags & 0x7fffffff;

                        if (direction < 0 && ((uVar17 & uVar19) != 0 ||
                            vObject.physics2.W < -4997120))
                            uVar7 |= 0x80000000;

                        vObject.flags = uVar7;
                    }

                    uVar7 = vObject.flags >> 31;
                }
                else
                    vObject.flags &= 0x7fffffff;

                if ((uVar17 & 0x400) == 0 || wheelsType == _WHEELS.Air)
                {
                    if ((uVar17 & 0x800) == 0)
                    {
                        iVar12 = turning;
                        iVar8 = 0;

                        if (0 < iVar12)
                            iVar8 = iVar12;

                        if (iVar8 < 0)
                            iVar8 += 63;

                        iVar15 = (iVar12 + 16) - (iVar8 >> 6);
                        iVar12 = 0x2aa0000;
                        iVar8 = 0x2aa;

                        if (iVar15 < 0x2aa)
                        {
                            iVar12 = iVar15 * 0x10000;
                            iVar8 = iVar15;
                        }

                        iVar12 = iVar12 >> 16;
                        turning = (short)iVar8;

                        if (iVar12 < 1)
                            return;
                    }
                    else
                    {
                        iVar12 = turning;
                        iVar8 = 0;

                        if (iVar12 < 0)
                            iVar8 = iVar12;

                        if (iVar8 < 0)
                            iVar8 += 63;

                        iVar15 = (iVar12 - 16) - (iVar8 >> 6);
                        iVar12 = -0x2aa0000;
                        iVar8 = -0x2aa;

                        if (-0x2aa < iVar15)
                        {
                            iVar12 = iVar15 * 0x10000;
                            iVar8 = iVar15;
                        }

                        iVar12 = iVar12 >> 16;
                        turning = (short)iVar8;

                        if (-1 < iVar12)
                            return;
                    }

                    if (wheelsType != _WHEELS.Ground)
                        return;

                    iVar8 = vObject.physics1.W * DAT_B2;

                    if (iVar8 < 0)
                        iVar8 += 4095;

                    iVar15 = DAT_B1 + (iVar8 >> 12);
                    iVar8 = 0;

                    if (0 < iVar15)
                        iVar8 = iVar15;

                    iVar8 = (int)(uVar7 * 2 - 1) * iVar12 * iVar8;

                    if (iVar8 < 0)
                        iVar8 += 15;

                    vObject.physics2.Y -= iVar8 >> 4;
                    return;
                }

                if ((uVar17 & 0x800) == 0)
                {
                    iVar8 = 0x2aa;

                    if (turning + 32 < 0x2aa)
                        iVar8 = turning + 32;

                    turning = (short)iVar8;
                    iVar8 = vObject.physics2.Y;

                    if (uVar7 == 0)
                    {
                        vObject.physics2.Y = iVar8 + 1280;
                        return;
                    }
                }
                else
                {
                    iVar8 = -0x2aa;

                    if (-0x2aa < turning - 32)
                        iVar8 = turning - 32;

                    turning = (short)iVar8;
                    iVar8 = vObject.physics2.Y;

                    if (uVar7 != 0)
                    {
                        vObject.physics2.Y = iVar8 + 1280;
                        return;
                    }
                }

                vObject.physics2.Y = iVar8 - 1280;
                return;
            }

            if ((uVar17 & 0x8000000) == 0)
            {
                if ((uVar17 & 0x10000000) == 0)
                    return;

                iVar12 = vObject.physics2.Z;
                iVar8 = 0x8000;

                if (wheelsType == _WHEELS.Sea)
                    iVar8 = 0x10000;
            }
            else
            {
                iVar12 = vObject.physics2.Z;
                iVar8 = -0x8000;

                if (wheelsType == _WHEELS.Sea)
                    iVar8 = -0x10000;
            }

            vObject.physics2.Z = iVar12 + iVar8;
            return;
        }

        if (_CONTROLLER_TYPE.JoypadAnalog < playerController.type)
            return;

        uVar19 = playerController.stick[0];
        uVar7 = uVar19 - 128;
        iVar12 = 128 - playerController.stick[0];
        iVar8 = iVar12;

        if (iVar12 < 0)
            iVar8 = -iVar12;

        iVar15 = 0;

        if (8 < iVar8)
            iVar15 = iVar12;

        bVar2 = false;

        if (((uVar17 & 0x400) != 0 && iVar15 <= -64) || (uVar17 & 0x200) != 0)
            bVar2 = true;

        bVar18 = false;

        if ((uVar17 & 0x400) != 0 && (-1 < direction || -64 < iVar15))
            bVar18 = true;

        bVar3 = false;

        if ((uVar17 & 0x1800) == 0)
        {
            uVar14 = uVar7;

            if ((int)uVar7 < 0)
                uVar14 = (uint)-(int)uVar7;

            if (7 < (int)uVar14)
                bVar3 = true;
        }
        else
            bVar3 = true;

        if ((uVar17 & 0x1800) == 0)
        {
            uVar14 = uVar7;

            if ((int)uVar7 < 0)
                uVar14 = (uint)-(int)uVar7;

            if ((int)uVar14 < 8)
            {
                iVar8 = turning * vObject.physics1.W;

                if (iVar8 < 0)
                    iVar8 += 0x7fff;

                sVar5 = (short)(turning - (iVar8 >> 15));
            }
            else
            {
                iVar8 = 16;

                if (bVar18)
                    iVar8 = 32;

                uVar14 = (uint)(int)turning;

                sVar5 = 0; //tmp
                //...
            }

            turning = sVar5;
        }
        else
        {
            if (bVar18)
            {
                if ((uVar17 & 0x800) == 0)
                {
                    iVar8 = turning + 32;
                    iVar12 = 0x2aa;

                    if (iVar8 < 0x2aa)
                        iVar12 = iVar8;

                    turning = (short)iVar12;
                    goto LAB_3DE3C;
                }

                iVar8 = turning * -32;
            }
            else
            {
                if ((uVar17 & 0x800) == 0)
                {
                    iVar12 = turning;
                    iVar8 = 0;

                    if (0 < iVar12)
                        iVar8 = iVar12;

                    if (iVar8 < 0)
                        iVar8 += 63;

                    iVar8 = (iVar12 + 16) - (iVar8 >> 6);
                    iVar12 = 0x2aa;

                    if (iVar8 < 0x2aa)
                        iVar12 = iVar8;

                    turning = (short)iVar12;
                    goto LAB_3DE3C;
                }

                iVar12 = turning;
                iVar8 = 0;

                if (iVar12 < 0)
                    iVar8 = iVar12;

                if (iVar8 < 0)
                    iVar8 += 63;

                iVar8 = (iVar12 - 16) - (iVar8 >> 6);
            }

            iVar12 = -0x2aa;

            if (-0x2aa < iVar8)
                iVar12 = iVar8;

            turning = (short)iVar12;
        }

        LAB_3DE3C:
        uVar14 = 0;

        if (64 < iVar15)
            uVar14 = (uint)(128 - (byte)playerController.DAT_14[1] < 65 ? 1 : 0);

        uVar9 = uVar17 & 0x1000100;

        if (64 < iVar15)
            uVar9 |= 1;

        uVar9 = uVar14 << 16 | uVar9;

        if ((DAT_F6 & 16) == 0)
        {
            sVar5 = -1;

            if (-1 < direction)
                sVar5 = 1;

            if ((uVar17 & 0x100) == 0)
            {
                if (bVar2)
                    uVar6 = (ushort)-DAT_B3;
                else
                {
                    iVar15 = iVar15 * DAT_B3;

                    if (iVar15 < 0)
                        iVar15 += 127;

                    uVar6 = (ushort)(iVar15 >> 7);
                }
            }
            else
                uVar6 = DAT_B3;

            acceleration = (short)(sVar5 * uVar6);
            iVar8 = vObject.physics2.W;

            if (iVar8 < 0)
                iVar8 = -iVar8;

            if (iVar8 < 474 && acceleration < -16)
            {
                if (direction < 0)
                    direction = 1;
                else if (bVar2)
                    direction = -1;
            }

            FUN_3D0F8(uVar9);
        }
        else
            FUN_39CEC(uVar9);

        iVar8 = 1;

        if (direction < 0)
        {
            iVar8 = -1;

            if (!bVar2)
            {
                iVar12 = vObject.physics2.X;

                if (iVar12 < 0)
                    iVar12 += 127;

                iVar15 = vObject.physics2.Z;

                if (iVar15 < 0)
                    iVar15 += 127;

                iVar8 = -1;

                if (-1941505 <
                    vObject.vTransform.rotation.V02 * (iVar12 >> 7) +
                    vObject.vTransform.rotation.V22 * (iVar15 >> 7))
                    iVar8 = 1;
            }
        }

        if (vObject.vTransform.rotation.V11 < 0)
        {
            uVar19 -= playerController.DAT_14[0];

            if ((uVar19 ^ uVar7) < 0)
                uVar19 = 0;

            uVar7 = uVar17 >> 28 & 1;

            if ((uVar17 & 0x8000000) == 0)
                iVar8 = (int)(uVar7 << 15);
            else
                iVar8 = (int)(uVar7 - 1) * 0x8000;

            iVar8 = (int)(uVar19 * 512) + iVar8;

            if (wheelsType == _WHEELS.Sea)
                vObject.physics2.Z += iVar8 * 4;
            else
                vObject.physics2.Z += iVar8;
        }
        else
        {
            if (bVar18)
            {
                iVar12 = turning;

                if (iVar12 < 0)
                    iVar12 = -iVar12;

                if (iVar12 < 170)
                {
                    if (vObject.physics1.W < 2370)
                    {
                        if (uVar9 == 0)
                        {
                            iVar12 = breaking - 3;
                            uVar10 = 0;

                            if (0 < iVar12)
                                uVar10 = (sbyte)iVar12;

                            breaking = uVar10;
                        }
                        else
                        {
                            iVar15 = breaking + 2;
                            iVar12 = 127;

                            if (iVar15 < 127)
                                iVar12 = iVar15;

                            breaking = (sbyte)iVar12;
                        }
                    }

                    acceleration = (short)(DAT_B3 * -2);
                }

                if (!bVar3 || wheelsType == _WHEELS.Air)
                    goto LAB_3E1C8;

                iVar12 = vObject.physics2.Y;
                iVar8 = iVar8 * turning * 2;
            }
            else
            {
                if (0 < breaking)
                    breaking = (sbyte)-breaking;

                if (!bVar3 || wheelsType != _WHEELS.Ground)
                    goto LAB_3E1C8;

                iVar12 = vObject.physics1.W * DAT_B2;

                if (iVar12 < 0)
                    iVar12 += 4095;

                iVar15 = DAT_B1 * (iVar12 >> 12);
                iVar12 = 0;

                if (0 < iVar15)
                    iVar12 = iVar15;

                iVar8 = (iVar8 * turning * iVar12) / 14;
                iVar12 = vObject.physics2.Y;
            }

            vObject.physics2.Y = iVar12 + iVar8;
        }

        LAB_3E1C8:
        iVar8 = breaking + 4;

        if (breaking < 0)
        {
            uVar10 = 0;

            if (iVar8 < 0)
                uVar10 = (sbyte)iVar8;

            breaking = uVar10;
        }

        iVar12 = playerController.stick[0] - 128;
        iVar8 = iVar12;

        if (iVar12 < 0)
            iVar8 = -iVar12;

        if (32 < iVar8)
        {
            if (iVar12 < 0)
                iVar12 = playerController.stick[0] - 125;

            vCamera.DAT_92 -= (short)(iVar12 >> 2);
        }

        iVar12 = playerController.stick[1] - 128;
        iVar8 = iVar12;

        if (32 < iVar8)
        {
            iVar12 = iVar12 * 3051;

            if (iVar12 < 0)
                iVar12 += 127;

            iVar12 = vCamera.DAT_9C + (iVar12 >> 7);
            iVar8 = vObject.DAT_58 << 1;

            if (iVar8 <= iVar12)
            {
                iVar8 = 0x140000;

                if (iVar12 < 0x140001)
                    iVar8 = iVar12;
            }

            vCamera.DAT_9C = iVar8;
        }

        if ((uVar17 & 0x20000000) == 0)
            return;

        if ((vObject.flags & 0x2000000) != 0)
            return;

        if (GameManager.instance.DAT_C74 != 0)
            return;

        vCamera.FUN_4B898();
    }
}
