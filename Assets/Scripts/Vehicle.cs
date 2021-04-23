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
    public byte DAT_C3; //0xC3
    public byte DAT_C4; //0xC4
    public byte DAT_C5; //0xC5
    public _VEHICLE vehicle;
    public byte DAT_DE; //0xDE
    public byte DAT_DF; //0xDF
    public short DAT_E0; //0xE0
    public short DAT_E2; //0xE2
    public int DAT_E4; //0xE4
    public int lightness; //0xE8
    public VigCamera vCamera;
    public ushort DAT_F6; //0xF6
    public VigObject[] body;
    public VigObject DAT_100; //0x100
    public VigObject[] wheels;
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
                wheelObject.phy1Unk1 = GameManager.vehicleConfigs[configID].unk0x0[index + 2];
                wheelObject.phy1Unk2 = GameManager.vehicleConfigs[configID].unk0x0[index + 4];

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
        if (vObject.ai < 0 && GameManager.instance.mode != _MODE.Demo)
        {
            TileData tile = GameManager.instance.terrain.GetTileByPosition((uint)vObject.position.x, (uint)vObject.position.z);

            if (tile.unk2[3] == 7)
                ; //FUN_3BFC0

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
        short sVar1 = 84; //r17

        if (-1 < acceleration)
        {
            sVar1 = 85;

            if (-1 < direction)
            {
                sVar1 = 0;

                if ((DAT_F6 & 0x80) != 0)
                {
                    sVar1 = 86;
                }
            }
        }

        if (body[0] != null)
        {
            if (body[0].pointerUnk1 != 0)
            {
                //...
            }

            if (body[0].pointerUnk2 != 0)
            {
                //...
            }

            if (sVar1 != 0)
            {
                //...
            }
        }

        vObject.flags = vObject.flags & 0x8fffffff;
        int iVar = Utilities.FUN_29E84
            (new Vector3Int(vObject.physics1.X, vObject.physics1.Y, vObject.physics1.Z)); //r2

        if (iVar < 0)
            iVar += 127;
        
        vObject.phy1Unk1 = (short)(iVar >> 7);
        vObject.phy1Unk2 = (short)(iVar >> 7 >> 16);

        int x = vObject.physics1.X;
        if (x < 0)
            x += 127;

        int y = vObject.physics1.Y;
        if (y < 0)
            y += 127;

        int z = vObject.physics1.Z;
        if (z < 0)
            z += 127;

        int sum = vObject.rotation.V02 * (x >> 7) +
                  vObject.rotation.V12 * (y >> 7) +
                  vObject.rotation.V22 * (z >> 7);

        if (sum < 0)
            sum += 4095;

        vObject.phy2Unk1 = (short)(sum >> 12);

        if (DAT_B4 == 0)
        {
            if (wheelsType == _WHEELS.Air)
                ; //FUN_3FCC4
            else
            {
                if (wheelsType < _WHEELS.Sea)
                {
                    sVar1 = 0;

                    if (wheelsType != _WHEELS.Ground)
                        goto LAB_41E08;

                    PhyGround();
                }
                else
                {
                    if (wheelsType == _WHEELS.Sea)
                        ; //FUN_40598
                    else
                    {
                        sVar1 = 0;

                        if (wheelsType == _WHEELS.Snow)
                            goto LAB_41E08;

                        //FUN_40F10
                    }
                }
            }
        }
        else
        {
            //FUN_3E774
            //FUN_3E8C0
        }

        sVar1 = 0;

        LAB_41E08:
        ;
    }

    //FUN_3EDC4
    public void PhyGround()
    {
        if (vObject.rotation.V11 < 0)
        {
            FUN_3E8C0();
        }
        else
        {
            if (GameManager.instance.unk2 != 0 && vObject.position.z < GameManager.instance.unk1)
            {

            }
        }

        Matrix3x3 rotation;
        Vector3Int position;
        vObject.FUN_2AEAC(out rotation, out position);

        List<Vector3Int> positions = new List<Vector3Int>();
        List<Vector3Int> normals = new List<Vector3Int>();
        List<int> positions_y = new List<int>();
        List<TileData> tiles = new List<TileData>();

        Vector3Int local_v1 = new Vector3Int(0, 0, 0); //sp+10
        Vector3Int local_v2 = new Vector3Int(0, 0, 0); //sp+20

        for (int i = 0; i < wheels.Length; i++)
        {
            if (wheels[i] != null)
            {
                int wheel_x = wheels[i].screen.x;
                int wheel_y = wheels[i].screen.y + wheels[i].physics2.X;
                int wheel_z = wheels[i].screen.z;
                Vector3Int pos = Utilities.FUN_24148(vObject.rotation, vObject.position, new Vector3Int(wheel_x, wheel_y, wheel_z));
                int pos_y = pos.y; //r18
                TileData tile;
                Vector3Int normal;
                pos.y = vObject.FUN_2CFBC(pos, out normal, out tile);
                positions.Add(pos); //1F800000h
                normals.Add(normal); //1F800014h
                positions_y.Add(pos_y); //1F80000Ch
                tiles.Add(tile); //1F800010h
            }
        }

        int local_4c = 0; //sp+BCh
        int local_48 = 0; //sp+C0h

        for (int i = 0; i < wheels.Length; i++)
        {
            if (wheels[i] != null)
            {
                if ((wheels[i].flags & 0x2000000) == 0)
                {
                    local_4c = 0;
                    local_48 = 0x1000;
                }
                else
                {
                    int turn = turning;

                    if (1 < i)
                        turn = -turn;

                    wheels[i].vr.z = (short)turn;
                    int j = (turn & 0xFFF << 2) / 2;
                    local_4c = GameManager.DAT_65C90[j];
                    local_48 = GameManager.DAT_65C90[j + 1];
                }

                Vector3Int wheel_pos = new Vector3Int
                    (wheels[i].screen.x, wheels[i].screen.y + wheels[i].physics2.X, wheels[i].screen.z); //sp+60
                Vector3Int v1 = Utilities.FUN_24148(rotation, position, wheel_pos); //sp+98
                int pos_x = positions[i].x; //sp+70h
                int pos_y = positions[i].y; //sp+74h
                int pos_z = positions[i].z; //r25 //sp+78h
                int height = positions_y[i]; //r9 //sp+7Ch
                int normal_x = normals[i].x; //sp+90h
                int normal_y = normals[i].y; //sp+92h
                int normal_z = normals[i].z; //sp+94h
                TileData onTile = tiles[i]; //sp+C4h

                if (1 < i && GameManager.instance.unk6 != 0)
                {
                    //...
                }

                Vector3Int v2 = Utilities.FUN_24304(vObject.rotation, vObject.position, new Vector3Int(pos_x, pos_y, pos_z)); //sp+80
                v2.y = v2.y - wheels[i].physics2.X;

                if (v2.y < wheels[i].physics1.Y)
                {
                    if (i < 4)
                    {
                        uint f = 0x10000000;

                        if (onTile != null)
                            f = 0x30000000;

                        vObject.flags = vObject.flags | f;
                        int iVar3 = (ushort)normal_x << 16 >> 16; //r20
                        int iVar4 = (ushort)normal_x << 16 >> 31; //r21
                        int iVar5 = vObject.physics1.X >> 31; //r19
                        uint uVar1 = (uint)vObject.physics1.X;
                        long lVar1 = (long)((ulong)(uint)iVar3 * uVar1); //r14,r15
                        int iVar6 = (ushort)normal_y << 16 >> 16; //sp+D8h
                        int iVar7 = (ushort)normal_y << 16 >> 31; //sp+DCh
                        int iVar8 = vObject.physics1.Y; //r10
                        int iVar9 = vObject.physics1.Y >> 31; //r11
                        long lVar2 = (long)((ulong)(uint)iVar6 * (uint)iVar8); //r16,r17
                        int iVar10 = (ushort)normal_z << 16; //r6
                        int iVar14 = iVar10 >> 16; //r4
                        int iVar16 = iVar10 >> 31; //r5
                        iVar10 = vObject.physics1.Z;
                        long lVar3 = (long)((ulong)(uint)iVar14 * (uint)iVar10); //sp+D0h,sp+D4h
                        iVar10 = iVar10 >> 31; //r3
                        int iVar11 = iVar3 * iVar5; //sp+E8h
                        int iVar12 = vObject.physics1.X * iVar4; //r7
                        int iVar13 = iVar6 * iVar9; //r18
                        iVar8 = iVar8 * iVar7;
                        iVar9 = iVar14 * iVar10;
                        int iVar15 = (int)(lVar2 >> 32); //r17
                        iVar15 += iVar13;
                        int iVar17 = (int)(lVar1 >> 32); //r15
                        iVar17 += iVar11;
                        iVar17 += iVar12;
                        iVar15 += iVar8;
                        int iVar18 = (int)(lVar3 >> 32); //sp+D4h
                        iVar18 += iVar9;
                        iVar18 += vObject.physics1.Z * iVar16;
                        int iVar19 = (int)lVar3; //sp+D0h
                        int iVar20 = (int)lVar1; //r14
                        int iVar21 = (int)lVar2; //r16
                        iVar20 += iVar21;
                        iVar17 += iVar15;
                        if ((uint)iVar20 < (uint)iVar21) iVar17++;
                        iVar20 += iVar19;
                        iVar17 += iVar18;
                        if ((uint)iVar20 < (uint)iVar19) iVar17++;
                        iVar20 = (int)((uint)iVar20 >> 15);
                        iVar20 = iVar20 | (iVar17 << 17);
                        iVar17 = iVar17 >> 15;
                        iVar21 = iVar20;

                        Vector3Int v3 = Utilities.FUN_24210(vObject.rotation, new Vector3Int(normal_x, normal_y, normal_z)); //sp+A8
                        iVar14 = (-v3.x * iVar21);
                        iVar16 = 0;

                        if (iVar14 < 0)
                            iVar14 += 0xFFF;

                        iVar14 = iVar14 >> 12;

                        if (wheel_pos.x - v2.x < 0)
                            iVar16 = wheel_pos.x - v2.x;

                        iVar20 = -v3.z * iVar21;
                        int iVar22 = iVar14 - iVar16; //sp+50h

                        if (iVar20 < 0)
                            iVar20 += 0xFFF;

                        iVar14 = 0;
                        iVar20 = iVar20 >> 12;

                        if (wheel_pos.z - v2.z < 0)
                            iVar14 = wheel_pos.z - v2.z;

                        int iVar23 = iVar20 - iVar14; //sp+58h
                        iVar14 = wheels[i].physics1.X;
                        iVar16 = v2.y;

                        if (iVar14 < iVar16)
                            iVar14 = iVar16;

                        int iVar24 = (wheels[i].physics1.Y - iVar14) * wheels[i].phy1Unk1; //r8
                        int iVar25 = (iVar24 << 7) / v3.y; //sp+54h, r4

                        if (wheels[i].physics1.X < iVar16 || wheels[i].screen.y < iVar16)
                        {
                            int iVar = (iVar16 - wheels[i].screen.y) * wheels[i].phy1Unk2;

                            if (iVar < 0)
                                iVar += 31;

                            iVar25 = iVar25 + (iVar >> 5);
                        }
                        else
                        {
                            int iVar = iVar16 - wheels[i].screen.y;
                            iVar25 = iVar25 + (iVar << 4);
                            vObject.flags = vObject.flags | 0x40000000;
                        }

                        wheels[i].screen.y = v2.y;

                        if ((wheels[i].flags & 0x4000000) == 0)
                        {
                            if (onTile == null || onTile.unk2[0] == 0)
                                iVar9 = -iVar25 << 1;
                            else
                                iVar9 = -iVar25 * (256 - onTile.unk2[0]) >> 7;

                            if (local_4c == 0)
                            {
                                iVar12 = v1.x >> 5;
                                iVar16 = v1.z >> 2;
                            }
                            else
                            {
                                long lVar4 = (long)v1.x * local_48; //r6,r7
                                long lVar5 = (long)v1.z * local_4c; //sp+E0h,sp+E4h
                                long lVar6 = (long)v1.x * local_4c; //sp+D0h,sp+D4h
                                long lVar7 = (long)v1.z * local_48; //r4,r5
                                iVar8 = (uint)lVar4 < (uint)lVar5 ? 1 : 0;
                                int iVar26 = (int)lVar4 - (int)lVar5; //r2
                                int iVar27 = (int)(lVar4 >> 32) - (int)(lVar5 >> 5); //r3
                                iVar27 -= iVar8;
                                iVar26 = (int)((uint)iVar26 >> 11);
                                iVar10 = iVar27 << 15;
                                iVar26 |= iVar10;
                                iVar27 = iVar27 >> 11;
                                iVar12 = iVar26;
                                iVar26 = (int)lVar6 + (int)lVar7;
                                iVar27 = (int)(lVar6 >> 32) + (int)(lVar7 >> 32);
                                iVar10 = (uint)iVar26 < (uint)lVar7 ? 1 : 0;
                                iVar27 += iVar10;
                                iVar26 = iVar26 >> 14;
                                iVar14 = iVar27 << 18;
                                iVar26 |= iVar14;
                                iVar27 = iVar27 >> 14;
                                iVar16 = iVar26;
                            }

                            int a = acceleration;

                            if (acceleration < 0)
                                a = -a;

                            iVar20 = iVar9;

                            if (a << 6 < iVar9)
                                iVar20 = a << 6;

                            iVar21 = iVar20;

                            if (acceleration < 0)
                            {
                                iVar21 = -iVar16;

                                if (iVar16 <= 0)
                                {
                                    if (iVar20 < -iVar16)
                                        iVar21 = iVar20;
                                }
                                else
                                {
                                    iVar20 = -iVar20;

                                    if (iVar20 <= iVar21)
                                        goto LAB_3F738;
                                }

                                iVar21 = iVar20;
                            }
                            else
                            {
                                if ((wheels[i].flags & 0x1000000) == 0)
                                    iVar21 = 0;
                                else
                                {
                                    if (direction <= 0)
                                        iVar21 = -iVar21;
                                    else
                                    {
                                        if (iVar20 < -iVar16 >> 2)
                                            iVar21 = -iVar16 >> 2;
                                    }
                                }
                            }

                            LAB_3F738:
                            if ((wheels[i].flags & 0x1000000) != 0)
                            {
                                if (breaking != 0 && breaking <= 0)
                                    iVar21 -= (breaking << 1) + breaking;
                                else
                                {
                                    iVar21 += breaking << 6;
                                    iVar9 = (iVar9 + (int)((uint)iVar9 >> 31)) >> 1;
                                    iVar25 = iVar25 - (breaking << 7);
                                }
                            }

                            if ((wheels[i].flags & 0x40000000) != 0)
                            {
                                int iVar;

                                if (iVar16 >> 8 < 0)
                                    iVar = (iVar16 >> 8) * -(iVar16 >> 8);
                                else
                                    iVar = (iVar16 >> 8) * (iVar16 >> 8);

                                iVar21 -= iVar >> 5;
                            }

                            if (onTile != null)
                            {
                                if (onTile.unk2[1] != 0)
                                {
                                    int iVar;

                                    if (iVar16 >> 8 < 0)
                                        iVar = (iVar16 >> 8) * -(iVar16 >> 8);
                                    else
                                        iVar = (iVar16 >> 8) * (iVar16 >> 8);

                                    iVar21 -= iVar * onTile.unk2[1] >> 12;
                                }
                            }

                            iVar16 = -iVar12;

                            if (iVar12 <= 0)
                            {
                                if (iVar9 < -iVar12)
                                    iVar16 = iVar9;
                            }
                            else
                            {
                                if (iVar16 < -iVar9)
                                    iVar16 = -iVar9;
                            }

                            if (local_4c != 0)
                            {
                                int iVar = local_4c * iVar21 + local_48 * iVar16 >> 12;
                                iVar22 = iVar22 + iVar;
                                iVar = local_48 * iVar21 - local_4c * iVar16 >> 12;
                                iVar23 = iVar23 + iVar;
                            }
                            else
                            {
                                iVar22 = iVar22 + iVar16;
                                iVar23 = iVar23 + iVar21;
                            }
                        }

                        int wheel_pos_x = wheel_pos.x >> 3;
                        int wheel_pos_y = wheel_pos.y >> 3;
                        int wheel_pos_z = wheel_pos.z >> 3;
                        Coprocessor.rotationMatrix.rt11 = (short)wheel_pos_x;
                        Coprocessor.rotationMatrix.rt12 = (short)(wheel_pos_x >> 16);
                        Coprocessor.rotationMatrix.rt22 = (short)wheel_pos_y;
                        Coprocessor.rotationMatrix.rt23 = (short)(wheel_pos_y >> 16);
                        Coprocessor.rotationMatrix.rt33 = (short)wheel_pos_z;

                        int iVar29 = iVar22 >> 3; //r3

                        if (iVar29 < -0x8000)
                            iVar10 = -0x8000;
                        else
                        {
                            iVar10 = 0x7fff;

                            if (iVar29 < 0x8000)
                                iVar10 = iVar29;
                        }

                        iVar29 = iVar25 >> 3;

                        if (iVar29 < -0x8000)
                            iVar16 = -0x8000;
                        else
                        {
                            iVar16 = 0x7fff;

                            if (iVar29 < 0x8000)
                                iVar16 = iVar29;
                        }

                        iVar29 = iVar23 >> 3;

                        if (iVar29 < -0x8000)
                            iVar14 = -0x8000;
                        else
                        {
                            iVar14 = 0x7fff;

                            if (iVar29 < 0x8000)
                                iVar14 = iVar29;
                        }

                        Coprocessor.accumulator.ir1 = (short)iVar10;
                        Coprocessor.accumulator.ir2 = (short)iVar16;
                        Coprocessor.accumulator.ir3 = (short)iVar14;
                        Coprocessor.ExecuteOP(12, false);
                        local_v2.x += iVar22;
                        local_v2.y += iVar25;
                        local_v2.z += iVar23;
                        local_v1.x += Coprocessor.mathsAccumulator.mac1;
                        local_v1.y += Coprocessor.mathsAccumulator.mac2;
                        local_v1.z += Coprocessor.mathsAccumulator.mac3;

                        if (onTile != null)
                        {
                            if (onTile.unk2[3] != 0 && onTile.unk2[3] != 7)
                            {
                                //function call by register
                            }
                        }
                    }
                    else
                    {
                        wheels[i].screen.y = v2.y;
                    }
                }
                else
                {
                    wheels[i].screen.y = wheels[i].physics1.Y;
                }

                int iVar30 = local_4c * v1.x + local_48 * v1.z; //r2
                int iVar31 = 0; //r5

                if (iVar30 < 0)
                    iVar30 += 0xFFF;

                iVar30 = iVar30 >> 12;
                wheels[i].physics2.Z = iVar30;

                if (wheels[i].physics2.Y != 0)
                {
                    iVar30 = iVar30 * wheels[i].physics2.Y;

                    if (iVar30 < 0)
                        iVar30 += 0x7ffff;

                    iVar31 = iVar30 >> 19;

                    if ((wheels[i].flags & 0x1000000) != 0)
                    {
                        if (0 < breaking)
                        {
                            iVar31 = iVar31 + breaking * 2;
                        }
                    }

                    wheels[i].vr.x = (short)(wheels[i].vr.x - iVar31);
                }
            }
        }

        for (int i = 0; i < wheels.Length; i++)
            if (wheels[i] != null)
                wheels[i].ApplyTransformation();
        
        local_v2 = Utilities.FUN_24094(vObject.rotation, local_v2);
        int iVar32 = (vObject.phy1Unk2 << 16 | (ushort)vObject.phy1Unk1) * lightness; //r18
        local_v2.y += GameManager.instance.gravityFactor;
        long lVar8 = (long)vObject.physics1.X * iVar32; //sp+E0, sp+E4
        local_v2.x -= (int)(lVar8 >> 32 >> 0);
        lVar8 = (long)vObject.physics1.Y * iVar32;
        local_v2.y -= (int)(lVar8 >> 32 >> 0);
        lVar8 = (long)vObject.physics1.Z * iVar32;
        local_v2.z -= (int)(lVar8 >> 32 >> 0);
        vObject.FUN_2AFF8(local_v2, local_v1);
        int iVar33 = vObject.physics2.X; //r2

        if (vObject.physics2.X < 0)
            iVar33 += 31;

        vObject.physics2.X -= iVar33 >> 5;
        iVar33 = vObject.physics2.Y;

        if (vObject.physics2.Y < 0)
            iVar33 += 31;

        vObject.physics2.Y -= iVar33 >> 5;
        iVar33 = vObject.physics2.Z;

        if (vObject.physics2.Z < 0)
            iVar33 += 31;

        vObject.physics2.Z -= iVar33 >> 5;
    }

    private void FUN_3E8C0()
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
                local_v1 = Utilities.FUN_2426C(vObject.rotation, new Matrix2x3(local_v1));
                int iVar3 = (vObject.phy1Unk2 << 16 | (ushort)vObject.phy1Unk1) * lightness; //r5
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
            rect1 = Utilities.FUN_24148(vObject.rotation, vObject.position, rect1);
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

                int cop2r32 = rect1.x - (vObject.position.x >> 3);
                int cop2r34 = rect1.y - (vObject.position.y >> 3);
                int cop2r36 = rect1.z - (vObject.position.z >> 3);
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
        byte bVar2;
        int iVar6;
        int iVar7;

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
                        iVar7 = (vObject.phy1Unk2 << 16 | (ushort)vObject.phy1Unk1) *
                            GameManager.DAT_63F68[direction + 1];

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
                iVar6 = (vObject.phy1Unk2 << 16 | (ushort)vObject.phy1Unk1) / 2;

            iVar6 -= DAT_E0;
            iVar7 = -512;

            if (-513 < iVar6)
            {
                iVar7 = 512;

                if (iVar6 < 513)
                    iVar7 = iVar6;

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
            }
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

                DAT_100.vr.y = vObject.physics2.M1;
                vObject.vectorUnk1.x = -vObject.vectorUnk1.x;
                DAT_100.ApplyRotationMatrix();
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
                        ((uint)vObject.position.x, (uint)vObject.position.z);

                    if ((GameManager.instance.DAT_40 & 0x80000) == 0)
                        iVar4 = -0x32000;
                    else
                        iVar4 = -0x12C000;

                    iVar4 = (iVar3 - vObject.position.y) + iVar4;

                    if (iVar4 < 0)
                    {
                        iVar3 = -0x32000;

                        if (-0x32000 < iVar4)
                            iVar3 = iVar4;

                        if (iVar3 < 0)
                            iVar3 += 1023;

                        iVar3 = iVar3 >> 10;
                        iVar4 = vObject.rotation.V01 * iVar3;

                        if (iVar4 < 0)
                            iVar4 += 31;

                        iVar8 = vObject.rotation.V11 * iVar3;
                        vObject.physics1.X += iVar4 >> 5;

                        if (iVar8 < 0)
                            iVar8 += 31;

                        iVar3 = vObject.rotation.V21 * iVar3;
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
                    if (DAT_B0 < 1 || (8391 < (vObject.phy1Unk2 << 16 | (ushort)vObject.phy1Unk1) || direction < 1))
                    {
                        if (2287 < (vObject.phy1Unk2 << 16 | (ushort)vObject.phy1Unk1))
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
        uVar17 = playerController.actions;
        unaff_s1 = 0; //original code does not contain

        if (playerController.type == _CONTROLLER_TYPE.SteeringWheel)
        {
            turning = (short)((playerController.leftStickX - 128) * 5);
            uVar19 = (uint)(playerController.leftStickY < 129 ? 1 : 0) ^ 1;

            if (-1 < (sbyte)playerController.DAT_15)
                uVar19 |= 0x10000;

            if ((DAT_F6 & 16) == 0)
            {
                if (direction < 0 && 16 < playerController.leftStickY)
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
                    uVar14 = playerController.leftStickY;

                    if (playerController.rightStickX < 241)
                        uVar14 -= playerController.rightStickX;
                    else
                    {
                        iVar8 = turning;

                        if (iVar8 < 0)
                            iVar8 = -iVar8;

                        if (iVar8 < 170)
                            uVar14 -= playerController.rightStickX;
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

            if (vObject.rotation.V11 < 0)
            {
                uVar17 = playerController.leftStickX;

                if ((int)(uVar17 ^ playerController.leftStickX) < 0)
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

            if ((uVar17 & 0x400) == 0 && playerController.rightStickX < 241)
            {
                if (0 < breaking)
                    breaking = (sbyte)-breaking;

                if (cVar1 == _WHEELS.Ground)
                {
                    iVar8 = ((vObject.phy1Unk2 << 16) | (ushort)vObject.phy1Unk1) * DAT_B2;

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
                    if ((vObject.phy1Unk2 << 16 | (ushort)vObject.phy1Unk1) < 2370)
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
                iVar8 = vObject.phy2Unk2 << 16 | (ushort)vObject.phy2Unk1;

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
                    iVar12 = iVar8 * (vObject.phy2Unk2 << 16 | (ushort)vObject.phy2Unk1);

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
                    if ((vObject.phy1Unk2 << 16 | (ushort)vObject.phy1Unk1) < 2370)
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

            if (0 < vObject.rotation.V11)
            {
                uVar7 = 0;

                if (direction < 1)
                {
                    if ((uVar17 & 0x18000000) != 0)
                    {
                        uVar7 = vObject.flags & 0x7fffffff;

                        if (direction < 0 && ((uVar17 & uVar19) != 0 ||
                            (vObject.phy2Unk2 << 16 | (ushort)vObject.phy2Unk1) < -4997120))
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

                    iVar8 = (vObject.phy1Unk2 << 16 | (ushort)vObject.phy1Unk1) * DAT_B2;

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

        uVar19 = playerController.leftStickX;
        uVar7 = uVar19 - 128;
        iVar12 = 128 - playerController.leftStickY;
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
                iVar8 = turning * (vObject.phy1Unk2 << 16 | (ushort)vObject.phy1Unk1);

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
            uVar14 = (uint)(128 - (byte)playerController.DAT_15 < 65 ? 1 : 0);

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
            iVar8 = vObject.phy2Unk2 << 16 | (ushort)vObject.phy2Unk1;

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
                    vObject.rotation.V02 * (iVar12 >> 7) +
                    vObject.rotation.V22 * (iVar15 >> 7))
                    iVar8 = 1;
            }
        }

        if (vObject.rotation.V11 < 0)
        {
            uVar19 -= playerController.DAT_14;

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
                    if ((vObject.phy1Unk2 << 16 | (ushort)vObject.phy1Unk1) < 2370)
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

                iVar12 = (vObject.phy1Unk2 << 16 | (ushort)vObject.phy1Unk1) * DAT_B2;

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

        iVar12 = playerController.rightStickX - 128;
        iVar8 = iVar12;

        if (iVar12 < 0)
            iVar8 = -iVar12;

        if (32 < iVar8)
        {
            if (iVar12 < 0)
                iVar12 = playerController.rightStickX - 125;

            vCamera.DAT_92 -= (short)(iVar12 >> 2);
        }

        iVar12 = playerController.rightStickY - 128;
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
