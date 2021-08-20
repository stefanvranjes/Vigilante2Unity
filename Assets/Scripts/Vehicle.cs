using System;
using System.IO;
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

public class Vehicle : VigObject
{
    public struct AI
    {
        public void FUN_51C54(Vector3Int param1, Vector3Int param2, uint param3, uint param4)
        {
            short[] aVar1;
            int iVar2;

            aVar1 = GameManager.instance.FUN_51ED4(param1, param2, param3, param4);
            iVar2 = FUN_51BDC(aVar1);

            if (iVar2 == 0)
            {
                DAT_08 = param2.x;
                DAT_0C = param2.z;
            }
        }

        public int FUN_51CFC(VigObject param1, int param2)
        {
            short sVar1;
            int iVar2;
            long lVar3;
            short sVar4;
            short sVar5;
            int iVar5;
            Vector3Int local_8;

            iVar2 = DAT_08 - param1.vTransform.position.x;

            if (iVar2 < 0)
                iVar2 = -iVar2;

            if (iVar2 < param2)
            {
                iVar2 = DAT_0C - param1.vTransform.position.z;

                if (iVar2 < 0)
                    iVar2 = -iVar2;

                if (iVar2 < param2)
                {
                    if (0 < DAT_00)
                    {
                        sVar1 = DAT_02;
                        DAT_02++;
                        sVar4 = DAT_04[(sVar1 + 1 << 16 >> 14) / 2];
                        sVar5 = DAT_04[(sVar1 + 1 << 16 >> 14) / 2 + 1];

                        if (sVar4 != 0 || sVar5 != 0)
                        {
                            DAT_08 = sVar4 << 16;
                            DAT_0C = sVar5 << 16;
                            goto LAB_51DB0;
                        }
                    }

                    DAT_00 = -1;
                }
            }

            LAB_51DB0:
            local_8 = new Vector3Int();
            local_8.y = 0;
            local_8.x = DAT_08 - param1.vTransform.position.x;
            iVar2 = DAT_0C;
            iVar5 = param1.vTransform.position.z;
            local_8.z = iVar2 - iVar5;
            local_8 = Utilities.FUN_2426C(param1.vTransform.rotation,
                        new Matrix2x4(local_8.x, local_8.y, local_8.z, 0));
            lVar3 = Utilities.Ratan2(local_8.x, local_8.z);
            return (int)(lVar3 << 20) >> 20;
        }

        private int FUN_51BDC(short[] param1)
        {
            DAT_04 = param1;
            DAT_02 = 0;
            DAT_00 = (short)(param1 != null ? 1 : 0);

            if (param1 != null)
            {
                DAT_08 = param1[0] << 16;
                DAT_0C = param1[1] << 16;
            }

            return DAT_00;
        }

        public short DAT_00; //0x00
        public short DAT_02; //0x02
        public short[] DAT_04; //0x04
        public int DAT_08; //0x08
        public int DAT_0C; //0x0C
    }

    public short turning; //0xA8
    public short acceleration; //0xAA
    public _WHEELS wheelsType; //0xAC
    public sbyte direction; //0xAD
    public byte weaponSlot; //0xAE
    public byte DAT_AF; //0xAF
    public sbyte DAT_B0; //0xB0
    public sbyte DAT_B1; //0xB1
    public sbyte DAT_B2; //0xB2
    public byte DAT_B3; //0xB3
    public ushort DAT_B4; //0xB4
    public short DAT_B6; //0xB6
    public short ignition; //0xB8
    public byte DAT_C0; //0xC0
    public sbyte breaking; //0xC1
    public byte DAT_C2; //0xC2
    public byte DAT_C3; //0xC3
    public byte DAT_C4; //0xC4
    public byte DAT_C5; //0xC5
    public short DAT_C6; //0xC6
    public AI ai; //0xCC;
    public _VEHICLE vehicle; //0xDC
    public _CAR_VIEW view; //0xDD
    public byte DAT_DE; //0xDE
    public byte DAT_DF; //0xDF
    public short DAT_E0; //0xE0
    public short DAT_E2; //0xE2
    public int DAT_E4; //0xE4
    public int lightness; //0xE8
    public VigCamera vCamera; //0xEC
    public VigObject target; //0xF0
    public short DAT_F4; //0xF4
    public ushort DAT_F6; //0xF6
    public VigObject[] body; //0xF8
    public VigObject closeViewer; //0x100
    public Wheel[] wheels; //0x104
    public VigObject mgun; //0x11C
    public VigObject[] weapons; //0x120
    public ushort transformation; //0x12C
    public ushort doubleDamage; //0x12E
    public ushort shield; //0x130
    public ushort jammer; //0x132

    private VigConfig config;

    
    void Awake()
    {
        config = GetComponent<VigConfig>();
        weapons = new VigObject[3];
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override uint OnCollision(HitDetection param1)
    {
        int iVar4;
        uint uVar6;
        VigObject ppcVar8;

        ppcVar8 = param1.object1;
        uVar6 = 0;

        if (ppcVar8 != this && !ppcVar8.GetType().IsSubclassOf(typeof(VigObject)))
        {
            iVar4 = (int)ppcVar8.OnCollision(param1);
            uVar6 = (uint)(iVar4 != 0 ? 1 : 0);
        }

        if (uVar6 != 0)
            return uVar6;

        uVar6 = (uint)FUN_3B424(this, param1);
        return uVar6;
    }

    public override uint UpdateW(int arg1, int arg2)
    {
        uint uVar1;
        _WHEELS eVar1;
        int iVar2;
        short sVar2;
        ushort uVar3;
        uint uVar7;

        if (arg1 == 1)
        {
            uVar7 = flags;
            flags = uVar7 & 0xffff | 0x16088;
            iVar2 = screen.y;
            screen.y = iVar2 - 0x8000;
            vTransform.position.y = iVar2 - 0x8000;
            target = GameManager.instance.playerObjects[0];

            if (body[0] == null)
                uVar3 = maxHalfHealth;
            else
                uVar3 = (ushort)(body[0].maxHalfHealth + body[1].maxHalfHealth);

            maxFullHealth = uVar3;
            FUN_3A500(uVar7 | 0x1000000);

            if ((uVar7 & 0x1840000) != 0)
            {
                eVar1 = _WHEELS.Air;

                if ((uVar7 & 0x800000) == 0)
                {
                    uVar1 = 0x40000;

                    if ((uVar7 & 0x1000000) != 0)
                        eVar1 = _WHEELS.Sea;
                }

                FUN_3E32C(eVar1, 500);
            }

            //...
            uVar1 = 0;
            return uVar1;
        }

        if (arg1 == 2) goto LAB_3C380;

        if (arg1 != 0) return 0;

        if (id < 0 && GameManager.instance.gameMode != _GAME_MODE.Demo)
        {
            TileData tile = GameManager.instance.terrain.GetTileByPosition
                ((uint)vTransform.position.x, (uint)vTransform.position.z);

            if (tile.DAT_10[3] == 7)
                FUN_3BFC0();

            if ((flags & 0x20000000) == 0)
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
                if (tile.DAT_10[4] != DAT_DE)
                {
                    //sound effect FUN_1DE78
                    DAT_DE = (byte)tile.DAT_10[4];

                    if ((byte)tile.DAT_10[4] == 0)
                        DAT_DF = 0;
                    else
                    {
                        //function call by register
                    }
                }
            }

            FUN_3D424(InputManager.controllers[~id]);
            FUN_3AC84(InputManager.controllers[~id]);

            if (arg2 != 0)
                FUN_3A844();
        }
        else
        {
            sVar2 = ignition;

            if (sVar2 == 0)
                FUN_34728();
            else
            {
                ignition--;

                if (sVar2 == 1)
                {
                    //sound
                }
                else
                {
                    if (arg2 == 0) goto LAB_3C32C;
                    //sound
                }
            }

            if (arg2 != 0 && id < 0)
                FUN_3A844();
        }

        LAB_3C32C:
        //FUN_3B344
        if (arg2 != 0)
        {
            uVar7 = flags & 0xfeff7fff;

            if ((flags & 0x1000000) != 0)
                uVar7 |= 0x8000;

            flags = uVar7;
        }

        if (id != 0)
        {
            GameManager.instance.FUN_30CB0(this, 0);
            return 0;
        }

        LAB_3C380:
        FUN_41AE8();
        return 0;
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

        flags &= 0x8fffffff;
        iVar4 = Utilities.FUN_29E84(new Vector3Int
            (physics1.X, physics1.Y, physics1.Z));

        if (iVar4 < 0)
            iVar4 += 127;

        iVar5 = physics1.X;
        physics1.W = iVar4 >> 7;

        if (iVar5 < 0)
            iVar5 += 127;

        iVar4 = physics1.Y;

        if (iVar4 < 0)
            iVar4 += 127;

        iVar3 = physics1.Z;

        if (iVar3 < 0)
            iVar3 += 127;

        iVar4 = vTransform.rotation.V02 * (iVar5 >> 7) +
                vTransform.rotation.V12 * (iVar4 >> 7) +
                vTransform.rotation.V22 * (iVar3 >> 7);

        if (iVar4 < 0)
            iVar4 += 4095;

        physics2.W = iVar4 >> 12;
            
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

        if ((flags & 0x8000000) == 0)
        {
            if (jammer == 0)
            {
                if (-1 < id && (DAT_F6 & 2) == 0)
                {
                    screen = vTransform.position;
                    return;
                }

                iVar4 = (vTransform.position.x - screen.x) * DAT_AF;

                if (iVar4 < 0)
                    iVar4 += 255;

                screen.x += iVar4 >> 8;
                iVar4 = (vTransform.position.y - screen.y) * DAT_AF;

                if (iVar4 < 0)
                    iVar4 += 255;

                screen.y += iVar4 >> 8;
                iVar5 = screen.z;
                iVar4 = (vTransform.position.z - iVar5) * DAT_AF;

                if (iVar4 < 0)
                    iVar4 += 255;

                iVar4 = iVar4 >> 8;
            }
            else
            {
                iVar4 = vTransform.position.x - screen.x;

                if (iVar4 < 0)
                    iVar4 += 31;

                screen.x += iVar4 >> 5;
                iVar4 = vTransform.position.y - screen.y;

                if (iVar4 < 0)
                    iVar4 += 31;

                iVar5 = screen.z;
                screen.y += iVar4 >> 5;
                iVar4 = screen.z - iVar5;

                if (iVar4 < 0)
                    iVar4 += 31;

                iVar4 = iVar4 >> 5;
            }

            screen.z = iVar5 + iVar4;
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

        if (vTransform.rotation.V11 < 0)
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

            if (GameManager.instance.DAT_DA0 <= vTransform.position.z)
                return;

            if (vTransform.position.y <= GameManager.instance.DAT_DB0 + 0x5000)
                return;

            //FUN_391AC
        }
        else
        {
            if (GameManager.instance.DAT_DB0 != 0 && vTransform.position.z < GameManager.instance.DAT_DA0)
            {
                if (GameManager.instance.DAT_DB0 < vTransform.position.y)
                {
                    if (GameManager.instance.DAT_DB0 + 0x5000 < vTransform.position.y)
                    {
                        //FUN_391AC
                        return;
                    }

                    bVar3 = (byte)GameManager.FUN_2AC5C();

                    if ((bVar3 & 63) == 0)
                    {
                        acceleration = -120;

                        if (bVar3 == 0 && physics1.W < 1525)
                            ; //FUN_39BC4
                        else
                        {
                            //sound effects
                        }
                    }
                }
            }

            auStack224 = FUN_2AEAC();
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

                    Vector3Int position = Utilities.FUN_24148(vTransform, local_c0);
                    uVar15 = position.y;
                    TileData tile;
                    Vector3Int normal = new Vector3Int();
                    uVar5 = FUN_2CFBC(position, ref normal, out tile);
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

                            if (physics1.W <= iVar12)
                                goto LAB_41360;

                            //FUN_38EF4
                        }

                        iVar9 = 4096;
                    }

                    LAB_41360:
                    auStack144 = Utilities.FUN_24304(vTransform, local_a0);
                    auStack144.y -= wheels[i].physics2.X;

                    if (auStack144.y < wheels[i].physics1.Y)
                    {
                        if (i < 4)
                        {
                            uVar6 = 0x10000000;

                            if (tile != null)
                                uVar6 = 0x30000000;

                            flags |= uVar6;
                            auStack104 = Utilities.FUN_24210(vTransform.rotation, local_80);
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
                                flags |= 0x40000000;
                            }

                            local_c0.y = ((iVar7 - iVar12) * sVar1 * 128) / auStack104.y + local_c0.y;
                            wheels[i].screen.y = auStack144.y;

                            if (tile == null)
                                iVar12 = -local_c0.y;
                            else if (tile.DAT_10[4] != 2)
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
                                if (tile.DAT_10[3] != 0 && tile.DAT_10[3] != 7)
                                {
                                    //function call by register
                                }

                                if (i < 2) goto LAB_41944;

                                if (tile.DAT_10[4] == 2)
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

            local_f0 = Utilities.FUN_24094(vTransform.rotation, local_f0);
            iVar14 = physics1.W * lightness;
            local_f0.x -= (int)((ulong)((long)physics1.X * iVar14) >> 32);
            local_f0.y = (local_f0.y + 11520) -
                         (int)((ulong)((long)physics1.Y * iVar14) >> 32);
            lVar2 = (long)physics1.Z * iVar14;
            local_20 = (uint)lVar2;
            local_1c = (int)((ulong)lVar2 >> 32);
            local_f0.z -= local_1c;
            FUN_2AFF8(local_f0, local_100);
            iVar16 = physics2.X;
            iVar14 = iVar16;

            if (iVar16 < 0)
                iVar14 = iVar16 + 31;

            iVar11 = physics2.Y;
            physics2.X = iVar16 - (iVar14 >> 5);
            iVar14 = iVar11;

            if (iVar11 < 0)
                iVar14 = iVar11 + 31;

            iVar16 = physics2.Z;
            physics2.Y = iVar11 - (iVar14 >> 5);
            iVar14 = iVar16;

            if (iVar16 < 0)
                iVar14 = iVar16 + 31;

            physics2.Z = iVar16 - (iVar14 >> 5);
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
        Vector3Int auStack48 = new Vector3Int();
        Vector3Int auStack80;

        if (vTransform.rotation.V11 < 0)
        {
            iVar5 = VigTerrain.instance.FUN_1B750
                ((uint)vTransform.position.x, (uint)vTransform.position.z);

            if (iVar5 < GameManager.instance.DAT_DB0)
            {
                FUN_3E8C0();
                return;
            }
        }

        VigTransform auStack144 = FUN_2AE18();
        Vector3Int local_b0 = new Vector3Int(0, 0, 0);
        Vector3Int local_a0 = new Vector3Int(0, 11520, 0);
        iVar5 = vTransform.rotation.V11;
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
                iVar6 = physics2.W;
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
        local_24 = vTransform;
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

                    child2.vr.y = sVar4;
                    child2.ApplyTransformation();
                    child2.child2.vr.z += iVar12 * 3;
                    child2.child2.ApplyTransformation();
                }

                local_70 = new Vector3Int(
                    wheels[iVar5].screen.x,
                    wheels[iVar5].screen.y + wheels[iVar5].IDAT_78,
                    wheels[iVar5].screen.z);
                local_60 = Utilities.FUN_24148(local_24, local_70);
                iVar6 = FUN_2CFBC(local_60, ref auStack48, out local_28);
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

                            if (iVar9 < physics1.W)
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

                                    if (iVar6 < physics1.W)
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
                            uVar7 = flags | 0x20000000;
                            flags = uVar7;
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
                            local_40 = Utilities.FUN_24094(vTransform.rotation, local_40);
                        }

                        iVar5 = auStack80.y;

                        if (auStack80.y < 0)
                            iVar5 = auStack80.y + 3;

                        local_40.y = (iVar6 - local_60.y) * 16 - (iVar5 >> 2);
                        uVar7 = 0x30000000;

                        if (local_28 != null)
                            uVar7 = 0x10000000;

                        uVar7 = flags | uVar7;
                        flags = uVar7;
                        FUN_40B3C();
                    }
                }
            }

            uVar7 = uVar10 + 1;
            iVar5 = (int)uVar10 + 4;
            uVar10 = uVar7;

            if (3 < (int)uVar7)
            {
                if (bVar3 || (flags & 0x10000000) == 0)
                    DAT_C2 = 0;
                else
                {
                    bVar1 = DAT_C2;
                    DAT_C2 = (byte)(bVar1 + 1);

                    if (60 < bVar1)
                        ; //FUN_3E32C
                }

                local_b0 = Utilities.FUN_2426C(
                    vTransform.rotation, 
                    new Matrix2x4(local_b0.x, local_b0.y, local_b0.z, 0));
                iVar5 = physics1.W * lightness;
                local_a0.y -= (int)((ulong)((long)physics1.Y * iVar5) >> 32);
                lVar2 = (long)physics1.Z * iVar5;
                local_20 = (int)lVar2;
                local_1c = (int)((ulong)lVar2 >> 32);

                if (acceleration == 0)
                {
                    iVar12 = physics1.X;

                    if (iVar12 < 0)
                        iVar12 += 63;

                    iVar12 = -(iVar12 >> 6);
                    iVar6 = physics1.Z;

                    if (iVar6 < 0)
                        iVar6 += 63;

                    iVar6 = -(iVar6 >> 6);
                }
                else
                {
                    iVar6 = -physics1.X;

                    if (0 < physics1.X)
                        iVar6 += 31;

                    iVar9 = vTransform.rotation.V02;

                    if (iVar9 < 0)
                        iVar9 = -iVar9;

                    iVar12 = 0;

                    if (0 < 4096 - iVar9)
                        iVar12 = 4096 - iVar9;

                    iVar12 = (iVar5 >> 5) * iVar12;

                    if (iVar12 < 0)
                        iVar12 += 4095;

                    iVar12 = iVar12 >> 12;
                    iVar9 = -physics1.Z;

                    if (0 < physics1.Z)
                        iVar9 += 31;

                    iVar8 = vTransform.rotation.V22;

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

                local_a0.x = (local_a0.x - (int)((ulong)((long)physics1.X * iVar5) >> 32)) + iVar12;
                local_a0.z = (local_a0.z - local_1c) + iVar6;
                FUN_2AFF8(local_a0, local_b0);
                iVar12 = physics2.X;
                iVar5 = iVar12;

                if (iVar12 < 0)
                    iVar5 = iVar12 + 31;

                iVar6 = physics2.Y;
                physics2.X = iVar12 - (iVar5 >> 5);
                iVar5 = iVar6;

                if (iVar6 < 0)
                    iVar5 = iVar6 + 31;

                iVar12 = physics2.Z;
                physics2.Y = iVar6 - (iVar5 >> 5);
                iVar5 = iVar12;

                if (iVar12 < 0)
                    iVar5 = iVar12 + 31;

                physics2.Z = iVar12 - (iVar5 >> 5);
                return;
            }
        } while (true);

        void FUN_40B3C()
        {
            int cop2r32 = local_60.x - vTransform.position.x >> 3;
            int cop2r34 = local_60.y - vTransform.position.y >> 3;
            int cop2r36 = local_60.z - vTransform.position.z >> 3;
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
                if (local_28.DAT_10[3] != 0)
                    if (local_28.DAT_10[3] != 7)
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
        Vector3Int auStack56 = new Vector3Int();
        TileData local_20;
        int local_18;
        int local_14;

        if (vTransform.rotation.V11 == 0)
        {
            FUN_3E8C0();

            if (GameManager.instance.DAT_DB0 == 0)
                return;

            if (GameManager.instance.DAT_DA0 <= vTransform.position.z)
                return;

            if (vTransform.position.y <= GameManager.instance.DAT_DB0 + 0x5000)
                return;

            //sound effect
            return;
        }

        if (GameManager.instance.DAT_DB0 != 0 && vTransform.position.z < GameManager.instance.DAT_DA0)
        {
            if (GameManager.instance.DAT_DB0 < vTransform.position.y)
            {
                if (GameManager.instance.DAT_DB0 + 0x5000 < vTransform.position.y)
                {
                    //sound effect
                    return;
                }

                bVar2 = (byte)GameManager.FUN_2AC5C();

                if ((bVar2 & 63) == 0)
                {
                    acceleration = -120;

                    if (bVar2 == 0 && physics1.W < 1525)
                        ; //sound effect
                    else
                    {
                        //sound effect
                    }
                }
            }
        }

        VigTransform container = FUN_2AE18();
        Vector3Int local_88 = new Vector3Int(0, 0, 0);
        Vector3Int local_78 = new Vector3Int(0, 0, 0);
        flags |= 0x10000000;
        int iVar5 = 0;

        for (int i = 0; i < 4; i++)
        {
            if (wheels[i] != null)
            {
                local_48 = Utilities.FUN_24148(vTransform, wheels[i].screen);
                iVar6 = FUN_2CFBC(local_48, ref auStack56, out local_20);

                if (local_48.z < GameManager.instance.DAT_DA0 && GameManager.instance.DAT_DB0 < iVar6)
                {
                    if (GameManager.instance.DAT_36 && GameManager.instance.DAT_DB0 < local_48.y + 40960)
                    {
                        iVar7 = (int)GameManager.FUN_2AC5C();

                        if (iVar7 << 1 < physics1.W)
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
                    if (local_20.DAT_10[3] != 0 && local_20.DAT_10[3] != 7)
                    {
                        //function jump by register
                    }

                    flags |= 0x20000000;
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
            iVar6 = physics2.W;
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
        local_78 = Utilities.FUN_24094(vTransform.rotation, local_78);
        iVar5 = vTransform.rotation.V02;
        iVar7 = iVar5 * iVar8;
        iVar6 = physics1.X;

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
        iVar5 = vTransform.rotation.V22;
        iVar7 = iVar5 * iVar8;
        iVar6 = physics1.Z;

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

        if (3051 < physics1.W)
        {
            iVar5 = (int)(physics2.W >> 31 & 8U) / 4;
            local_48 = new Vector3Int(0, wheels[iVar5].screen.y, wheels[iVar5].screen.z);
            local_48 = Utilities.FUN_24148(vTransform, local_48);
            iVar5 = physics1.X;

            if (iVar5 < 0)
                iVar5 += 3;

            local_48.x += iVar5 >> 2;
            iVar5 = physics1.Z;

            if (iVar5 < 0)
                iVar5 += 3;

            local_48.z += iVar5 >> 2;
            iVar5 = FUN_2CFBC(local_48);

            if (iVar5 - 20480 < vTransform.position.y)
                local_78.y += (vTransform.rotation.V12 * iVar8) / 12;

            iVar5 = (iVar5 - 20480) - local_48.y;

            if (iVar5 < 0)
            {
                iVar8 = iVar5 + 31;

                if (-1 < physics2.W)
                {
                    iVar8 = -iVar5;

                    if (0 < iVar5)
                        iVar8 += 31;
                }

                physics2.X += iVar8 >> 5;
            }
        }

        iVar5 = physics1.W * lightness;
        local_78.x -= (int)((ulong)((long)physics1.X * iVar5) >> 32);
        local_78.y -= (int)((ulong)((long)physics1.Y * iVar5) >> 32);
        lVar1 = (long)physics1.Z * iVar5;
        local_18 = (int)lVar1;
        local_14 = (int)(lVar1 >> 32);
        local_78.z -= local_14;
        FUN_2AFF8(local_78, local_88);
        iVar8 = physics2.X;
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

        iVar6 = physics2.Z;
        physics2.X = iVar8 - iVar5;
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

        iVar8 = physics2.Y;
        physics2.Z = iVar6 - iVar5;
        iVar5 = iVar8;

        if (iVar8 < 0)
            iVar5 = iVar8 + 15;

        physics2.Y = iVar8 + (turning * 2 - (iVar5 >> 4));
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

        if (vTransform.rotation.V11 < 0)
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

                    wheels[i].ApplyTransformation();
                }
            }

            if (GameManager.instance.DAT_DB0 == 0)
                return;

            if (GameManager.instance.DAT_DA0 <= vTransform.position.z)
                return;

            if (vTransform.position.y <= GameManager.instance.DAT_DB0 + 0x5000)
                return;
                
            //FUN_391AC
        }
        else
        {
            if (GameManager.instance.DAT_DB0 != 0 && vTransform.position.z < GameManager.instance.DAT_DA0)
            {
                if (GameManager.instance.DAT_DB0 < vTransform.position.y)
                {
                    if (GameManager.instance.DAT_DB0 + 0x5000 < vTransform.position.y)
                    {
                        //FUN_391AC
                        return;
                    }

                    bVar4 = (byte)GameManager.FUN_2AC5C();

                    if ((bVar4 & 63) == 0)
                    {
                        acceleration = -120;

                        if (bVar4 == 0 && physics1.W < 1525)
                            ; //FUN_39BC4
                        else
                        {
                            //sound effects
                        }
                    }

                    //FUN_39B50
                }
            }

            VigTransform auStack216 = FUN_2AEAC();
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
                    Vector3Int normal = new Vector3Int();
                    TileData tile;
                    position = Utilities.FUN_24148(vTransform, local_b8);
                    heights.Add(position.y);
                    position.y = FUN_2CFBC(position, ref normal, out tile);
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

                            if (iVar9 < physics1.W)
                                ; //FUN_38EF4
                        }
                    }

                    local_88 = Utilities.FUN_24304(vTransform, local_98);
                    local_88.y -= wheels[i].physics2.X;

                    if (local_88.y < wheels[i].physics1.Y)
                    {
                        if (i < 4)
                        {
                            uVar10 = 0x10000000;

                            if (local_44 != null)
                                uVar10 = 0x30000000;

                            flags |= uVar10;
                            uVar17 = (uint)((local_78.x << 16) >> 16);
                            uVar10 = (uint)physics1.X;
                            lVar1 = (long)((ulong)uVar17 * uVar10);
                            local_2c = (int)((uint)(ushort)local_78.y << 16);
                            local_30 = (uint)(local_2c >> 16);
                            local_2c = local_2c >> 31;
                            uVar6 = (uint)physics1.Y;
                            uVar19 = (uint)((ulong)local_30 * uVar6);
                            uVar11 = (uint)((int)((uint)(ushort)local_78.z << 16) >> 16);
                            uVar13 = (uint)physics1.Z;
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
                            local_60 = Utilities.FUN_24210(vTransform.rotation, local_78);
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
                                flags |= 0x40000000;
                            }

                            local_b8.y = ((iVar7 - iVar9) * sVar3 * 128) / local_60.y + local_b8.y;
                            wheels[i].screen.y = local_88.y;

                            if ((wheels[i].flags & 0x4000000) == 0)
                            {
                                if (local_44 == null)
                                    iVar9 = local_b8.y * -2;
                                else if (local_44.DAT_10[0] == 0)
                                    iVar9 = local_b8.y * -2;
                                else
                                    iVar9 = -local_b8.y * (256 - local_44.DAT_10[0]) >> 7;

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
                                    if (local_44.DAT_10[1] != 0)
                                    {
                                        iVar8 = iVar12;

                                        if (iVar12 < 0)
                                            iVar8 = -iVar12;

                                        iVar7 -= iVar12 * iVar8 * local_44.DAT_10[1] >> 12;
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
                                if (local_44.DAT_10[3] != 0 && local_44.DAT_10[3] != 7)
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

            local_e8 = Utilities.FUN_24094(vTransform.rotation, local_e8);
            local_e8.y = GameManager.instance.gravityFactor + local_e8.y;
            iVar14 = physics1.W * lightness;
            local_e8.x -= (int)((ulong)((long)physics1.X * iVar14) >> 32);
            local_e8.y -= (int)((ulong)((long)physics1.Y * iVar14) >> 32);
            lVar1 = (long)physics1.Z * iVar14;
            local_28 = (uint)lVar1;
            local_24 = (int)((ulong)lVar1 >> 32);
            local_e8.z -= local_24;
            FUN_2AFF8(local_e8, local_f8);
            iVar18 = physics2.X;
            iVar14 = iVar18;

            if (iVar18 < 0)
                iVar14 = iVar18 + 31;

            iVar9 = physics2.Y;
            physics2.X = iVar18 - (iVar14 >> 5);
            iVar14 = iVar9;

            if (iVar9 < 0)
                iVar14 = iVar9 + 31;

            iVar18 = physics2.Z;
            physics2.Y = iVar9 - (iVar14 >> 5);
            iVar14 = iVar18;

            if (iVar18 < 0)
                iVar14 = iVar18 + 31;

            physics2.Z = iVar18 - (iVar14 >> 5);
        }
    }

    public void FUN_3CCD4(int param1)
    {
        sbyte sVar1;
        int ppiVar2;
        VigTuple ppiVar3;
        int ppiVar4;
        int iVar5;
        uint uVar6;
        int iVar7;
        VigObject oVar7;
        int iVar8;
        VigObject oVar10;
        VigObject oVar11;
        uint uVar12;
        VigObject oVar13;
        VigObject oVar14;
        Vector3Int local_40;
        int unaff_s5;
        int unaff_s6;
        VigTerrain terrain = VigTerrain.instance;

        oVar13 = null;
        oVar11 = null;
        unaff_s5 = 0; //not in the original code
        unaff_s6 = 0; //not in the original code
        uVar12 = 0xffffffff;
        uVar6 = (uint)~id;
        sVar1 = GameManager.instance.DAT_1128[uVar6];
        Coprocessor.rotationMatrix.rt11 = terrain.DAT_BDFF0[uVar6].rotation.V00;
        Coprocessor.rotationMatrix.rt12 = terrain.DAT_BDFF0[uVar6].rotation.V01;
        Coprocessor.rotationMatrix.rt13 = terrain.DAT_BDFF0[uVar6].rotation.V02;
        Coprocessor.rotationMatrix.rt21 = terrain.DAT_BDFF0[uVar6].rotation.V10;
        Coprocessor.rotationMatrix.rt22 = terrain.DAT_BDFF0[uVar6].rotation.V11;
        Coprocessor.rotationMatrix.rt23 = terrain.DAT_BDFF0[uVar6].rotation.V12;
        Coprocessor.rotationMatrix.rt31 = terrain.DAT_BDFF0[uVar6].rotation.V20;
        Coprocessor.rotationMatrix.rt32 = terrain.DAT_BDFF0[uVar6].rotation.V21;
        Coprocessor.rotationMatrix.rt33 = terrain.DAT_BDFF0[uVar6].rotation.V22;
        Coprocessor.translationVector._trx = terrain.DAT_BDFF0[uVar6].position.x;
        Coprocessor.translationVector._try = terrain.DAT_BDFF0[uVar6].position.y;
        Coprocessor.translationVector._trz = terrain.DAT_BDFF0[uVar6].position.z;

        for (int i = 0; i < GameManager.instance.worldObjs.Count; i++)
        {
            ppiVar3 = GameManager.instance.worldObjs[i];
            oVar10 = ppiVar3.vObject;
            oVar7 = oVar11;
            uVar6 = uVar12;
            iVar8 = unaff_s5;
            iVar5 = unaff_s6;
            oVar14 = oVar13;

            if (oVar10 == this || oVar10.type == 3)
            {
                oVar11 = oVar7;
                uVar12 = uVar6;
                unaff_s5 = iVar8;
                unaff_s6 = iVar5;
                oVar13 = oVar14;
            }
            else
            {
                if ((oVar10.flags & 0x4000) != 0 && 
                    (0 < oVar10.id || 
                    sVar1 != GameManager.instance.DAT_1128[~oVar10.id]))
                {
                    local_40 = Utilities.FUN_24008(oVar10.screen);
                    iVar5 = local_40.y >> 10;

                    if (iVar5 < 0)
                        iVar5 = -iVar5;

                    iVar7 = local_40.x >> 10;

                    if (iVar7 < 0)
                        iVar7 = -iVar7;

                    if (iVar5 < iVar7)
                        iVar5 = iVar7;

                    iVar8 = local_40.z >> 10;

                    if (iVar5 < iVar8)
                    {
                        oVar7 = oVar10;

                        if (oVar11 == null || unaff_s5 * iVar5 < iVar8 * unaff_s6)
                        {
                            oVar11 = oVar7;
                            uVar12 = uVar6;
                            unaff_s5 = iVar8;
                            unaff_s6 = iVar5;
                            oVar13 = oVar14;
                            continue;
                        }
                    }
                    
                    if (oVar11 == null)
                    {
                        uVar6 = (uint)Utilities.FUN_29F6C(vTransform.position, oVar10.screen);
                        oVar7 = oVar11;
                        iVar8 = unaff_s5;
                        iVar5 = unaff_s6;
                        oVar14 = oVar10;

                        if (uVar6 < uVar12)
                        {
                            oVar11 = oVar7;
                            uVar12 = uVar6;
                            unaff_s5 = iVar8;
                            unaff_s6 = iVar5;
                            oVar13 = oVar14;
                        }
                    }
                }
            }
        }

        if (oVar11 == null)
            oVar11 = oVar13;

        if (oVar11 != target && (oVar11 != null || param1 != 0))
        {
            target = oVar11;
            DAT_C6 = 0;
            //weapon stuff...
        }
    }

    public void FUN_3C9C4(int param1)
    {
        ushort uVar1;
        uint uVar2;
        int iVar6;
        ConfigContainer ccVar7;
        XOBF_DB dbVar8;

        dbVar8 = vData;
        uVar1 = (ushort)dbVar8.ini.configContainers[DAT_1A].next;
        iVar6 = param1 * 0xb4 + (int)vehicle * 10;

        for (uVar2 = uVar1; uVar2 != 0xffffU; uVar2 = uVar1)
        {
            ccVar7 = dbVar8.ini.configContainers[(int)uVar2];

            if ((ushort)ccVar7.objID - 256 < 4 && 
                49 < 0)
            {
                //salvage points
            }

            uVar1 = (ushort)ccVar7.previous;
        }

        if (49 < 0)
        {
            //hotrod stuff
        }

        DAT_B3 = GameManager.vehicleConfigs[(int)vehicle].DAT_13; //tmp
        lightness = GameManager.vehicleConfigs[(int)vehicle].lightness; //tmp
        DAT_AF = GameManager.vehicleConfigs[(int)vehicle].DAT_15; //tmp
        //FUN_3C404
    }

    public int FUN_3B424(VigObject param1, HitDetection param2)
    {
        sbyte sVar1;
        uint uVar2;
        int iVar7;
        int pcVar7;
        uint uVar8;
        int iVar9;
        VigObject ppcVar11;
        int iVar13;
        ulong uVar14;
        Vector3Int local_70;
        int local_30;
        int local_38;

        ppcVar11 = param2.self;

        if (param2.object2.type != 3)
        {
            if (param2.object2.type == 10)
                return 0;

            GameManager.instance.FUN_2F798(param1, param2);
            sVar1 = (sbyte)ppcVar11.type;

            if (sVar1 == 8)
            {
                if (param2.object1.type == 3)
                    return 0;

                if (shield != 0)
                {
                    //FUN_393F8
                    return 0;
                }

                if (param1.type == 2)
                {
                    if ((ppcVar11.flags & 0x20000000) == 0)
                    {
                        Vehicle v = (Vehicle)param1;
                        v.FUN_39DCC(-ppcVar11.maxHalfHealth, param2.position, true);
                        return 0;
                    }

                    //...
                }
                else
                {
                    //...
                }
            }
            else
            {
                if (sVar1 == 7)
                {
                    //...
                }
                else
                {
                    if (sVar1 == 2)
                    {
                        //...
                        return 0;
                    }

                    if ((PDAT_74 == ppcVar11 || PDAT_78 == ppcVar11)
            && param2.normal1.y < -2048)
                        return 0;

                    uVar14 = (ulong)Utilities.FUN_2AD3C(new Vector3Int(param1.physics1.X, param1.physics1.Y, param1.physics1.Z), param2.normal1);
                    uVar8 = (uint)uVar14 >> 0xd | (uint)(uVar14 >> 0x20 << 0x13);
                    iVar13 = 0;

                    if ((int)uVar8 < 0)
                    {
                        iVar13 = -(int)uVar8;

                        if (0 < (int)uVar8)
                            iVar13 += 0x3fff;

                        iVar9 = (iVar13 >> 14) * param1.DAT_A6;

                        if (iVar9 < 0)
                            iVar9 += 4095;

                        iVar13 = 0;

                        if (ppcVar11.GetType().IsSubclassOf(typeof(VigObject)))
                            ; //...

                        if (iVar13 == 0)
                        {
                            local_70 = Utilities.FUN_24210(param1.vTransform.rotation, param2.normal1);
                            uVar2 = (uint)-(param2.distance + (int)uVar8);
                            iVar9 = (int)uVar2 >> 31;
                            local_30 = local_70.x * iVar9;
                            local_38 = local_70.z * iVar9;
                            local_70.x = (int)((uint)((ulong)(uint)local_70.x * uVar2) >> 12 |
                                         (uint)(((int)((ulong)(uint)local_70.x * uVar2 >> 32) + local_30 +
                                         (int)uVar2 * (local_70.x >> 31)) * 0x100000));
                            local_70.y = (int)((uint)((ulong)(uint)local_70.y * uVar2) >> 12 |
                                         (uint)(((int)((ulong)(uint)local_70.y * uVar2 >> 32) + local_70.y * iVar9 +
                                         (int)uVar2 * (local_70.y >> 31)) * 0x100000));
                            local_70.z = (int)((uint)((ulong)(uint)local_70.z * uVar2) >> 12 |
                                         (uint)(((int)((ulong)(uint)local_70.z * uVar2 >> 32) + local_38 +
                                         (int)uVar2 * (local_70.z >> 31)) * 0x100000));
                            param1.FUN_2B1FC(local_70, param2.position);

                            if ((flags & 0x8000) == 0)
                            {
                                iVar7 = (int)(uVar8 + 0x3fff) >> 14;
                                iVar9 = -ppcVar11.maxHalfHealth;

                                if (-ppcVar11.maxHalfHealth < iVar7)
                                    iVar9 = iVar7;

                                if (param1.type == 2)
                                    ; //FUN_3A020
                                else
                                    if (param1.GetType().IsSubclassOf(typeof(VigObject)))
                                    ; //...

                                if (id < 0)
                                    GameManager.instance.FUN_15B00(~id, 0xff, 0, 0x40);

                                if (457 < param1.physics1.W)
                                {
                                    uVar8 = (uint)(ushort)ppcVar11.id >> 4;
                                    pcVar7 = 11;

                                    if (uVar8 < 11)
                                        pcVar7 = (int)uVar8;

                                    sVar1 = Utilities.DAT_106E8[pcVar7];

                                    if (sVar1 != -1)
                                    {
                                        //sounds
                                    }
                                }

                                if (1525 < param1.physics1.W && -32 < iVar9 / param1.physics1.W)
                                {
                                    //particles
                                }
                            }

                            flags |= 0x1008000;
                        }
                    }

                    if (iVar13 < 1)
                        return iVar13;
                }
            }
        }

        return 0;
    }

    public void FUN_3A500(uint param1)
    {
        bool bVar1;
        ConfigContainer ccVar2;
        VigObject oVar3;
        VigObject oVar4;
        int iVar5;
        uint uVar6;

        uVar6 = 0;

        do
        {
            if ((param1 & 0x1000000 << (int)(uVar6 & 31)) != 0)
            {
                ccVar2 = FUN_4AE5C((int)uVar6);

                if (ccVar2 != null)
                {
                    oVar3 = FUN_4AE94((int)uVar6);
                    iVar5 = -1;

                    if (oVar3 != null)
                    {
                        Utilities.FUN_2CA94(this, ccVar2, oVar3);
                        oVar3.transform.parent = transform;
                        bVar1 = true;

                        if (uVar6 != 0)
                        {
                            oVar4 = weapons[0];
                            iVar5 = 0;

                            while (oVar4 != null)
                            {
                                if (2 < iVar5) goto LAB_3A5C8;

                                oVar4 = weapons[iVar5 + 1];
                                iVar5++;
                            }

                            bVar1 = iVar5 < 3;
                        }

                        if (bVar1)
                        {
                            if (iVar5 == -1)
                                mgun = oVar3;
                            else
                                weapons[iVar5] = oVar3;
                        }
                    }
                }
            }

            LAB_3A5C8:
            uVar6++;

            if (7 < (int)uVar6)
                return;
        } while (true);
    }

    public void FUN_3A3D4(VigObject param1)
    {
        int iVar2;
        uint uVar3;
        uint uVar4;

        param1.id = 0;
        Utilities.FUN_2CC48(this, param1);
        param1.transform.parent = transform;
        uVar3 = 0;

        if (weapons[0] != null)
        {
            iVar2 = 0;

            do
            {
                if (2 < (int)uVar3) break;

                iVar2++;

                if (weapons[uVar3].tags == param1.tags)
                {
                    param1.tags = (sbyte)-param1.tags;
                    return;
                }

                uVar3++;

                if (iVar2 > 2) break;
            } while (weapons[iVar2] != null);
        }

        if (uVar3 != 3) goto LAB_3A4DC;

        uVar4 = weaponSlot;
        uVar3 = uVar4;

        if (id < 1)
        {
            if (7 < weapons[uVar4].tags)
            {
                uVar3 = 2;

                if (uVar4 != 0)
                    uVar3 = uVar4 - 1;
            }
        }
        else
        {
            if (weapons[uVar4].tags == 7)
            {
                uVar3 = 2;

                if (uVar4 != 0)
                    uVar3 = uVar4 - 1;
            }
        }

        FUN_3A148((int)uVar3);
        LAB_3A4DC:
        weapons[uVar3] = param1;
    }

    private void FUN_3A148(int param1)
    {
        Throwaway ppcVar1;
        VigObject oVar3;
        int iVar3;

        oVar3 = weapons[param1];

        if (oVar3.tags < 8)
        {
            ppcVar1 = oVar3.FUN_4ECA0();
            ppcVar1.id = id;
            iVar3 = physics1.X;

            if (iVar3 < 0)
                iVar3 += 127;

            ppcVar1.physics1.Z += iVar3 >> 7;
            iVar3 = physics1.Y;

            if (iVar3 < 0)
                iVar3 += 127;

            ppcVar1.physics1.W += iVar3 >> 7;
            iVar3 = physics1.Z;

            if (iVar3 < 0)
                iVar3 += 127;

            ppcVar1.physics2.X += iVar3 >> 7;
            //sound

            if (ppcVar1.maxHalfHealth != 0 && ppcVar1.tags != 7)
                ppcVar1.state = _THROWAWAY_TYPE.Spawnable;
        }
        else
        {
            oVar3.FUN_2CCBC();
            GameManager.instance.FUN_307CC(oVar3);
            flags &= 0xfffbffff;
        }
    }

    public void FUN_3A0C0(int param1)
    {
        ushort uVar1;
        uint uVar2;
        uint uVar3;
        uint uVar4;
        int iVar5;
        uint uVar6;

        uVar6 = maxHalfHealth;

        if (body[0] == null)
        {
            uVar1 = maxFullHealth;

            if ((int)uVar6 + param1 < uVar1)
                uVar1 = (ushort)((int)uVar6 + param1);

            maxHalfHealth = uVar1;
        }

        iVar5 = 0;

        if (param1 != 0)
        {
            do
            {
                if (1 < iVar5)
                    return;

                uVar4 = body[iVar5].maxHalfHealth;
                uVar3 = uVar4 + (uint)param1;
                uVar2 = uVar6;

                if ((int)uVar3 < (int)uVar6)
                    uVar2 = uVar3;

                body[iVar5].maxHalfHealth = (ushort)uVar2;
                param1 -= (int)(uVar2 - uVar4);
                iVar5++;
            } while (param1 != 0);
        }
    }

    public bool FUN_39DCC(int param1, Vector3Int param2, bool param3)
    {
        ushort uVar1;
        ushort uVar2;
        VigObject oVar3;
        uint uVar4;
        uint uVar5;
        uint uVar6;

        uVar1 = maxHalfHealth;
        uVar4 = uVar1;

        if (param1 < 0)
        {
            if (uVar4 == 0)
                return false;

            uVar2 = transformation;

            if (uVar2 != 0)
            {
                if (-param1 < uVar2)
                    transformation = (ushort)(uVar2 + param1);
                else
                    ; //FUN_3E32C
            }

            if (body[0] == null)
            {
                if ((int)uVar4 + param1 < 1)
                {
                    if (param3)
                    {
                        if (param1 < -20)
                        {
                            //FUN_38C40
                            return true;
                        }

                        //FUN_38DA8
                        return false;
                    }
                }
                else
                    maxHalfHealth = (ushort)((int)uVar4 + param1);
            }
            else
            {
                uVar5 = (uint)(param2.z < 1 ? 1 : 0);
                oVar3 = body[(int)uVar5];
                uVar6 = (uint)uVar1 >> 1;

                while(oVar3 != null)
                {
                    param1 = oVar3.maxHalfHealth + param1;

                    if ((oVar3.maxHalfHealth * oVar3.tags + (int)uVar6) / (int)uVar4 
                        != (param1 * oVar3.tags + (int)uVar6) / (int)uVar4)
                    {
                        //FUN_4DC94
                        oVar3.IDAT_78 = 0;
                        oVar3.IDAT_74 = 0;
                        oVar3.PDAT_78 = null;
                        oVar3.PDAT_74 = null;

                        if (oVar3 == body[0] && (DAT_F6 & 0x80) != 0)
                            ; //FUN_36634
                    }

                    if (-1 < param1)
                    {
                        oVar3.maxHalfHealth = (ushort)param1;
                        return false;
                    }

                    oVar3.maxHalfHealth = 0;
                    uVar5 = 1 - uVar5;

                    if (body[0].maxHalfHealth == 0 && 
                        body[1].maxHalfHealth == 0)
                    {
                        if (param3)
                        {
                            if (param1 < -20)
                            {
                                //FUN_38C40
                                return true;
                            }

                            //FUN_38DA8
                            return false;
                        }

                        return false;
                    }

                    oVar3 = body[(int)uVar5];
                }
            }
        }

        return false;
    }

    public void FUN_3968C(Wheel param1)
    {
        Vector3Int v3;

        if (wheelsType == _WHEELS.Ground)
        {
            param1.state = _WHEEL_TYPE.Flatten;
            param1.flags |= 0x40000000;
            param1.physics2.X -= 0xc00;
            flags |= 0x20000;
            GameManager.instance.FUN_30CB0(param1, 300);
            v3 = GameManager.instance.FUN_2CE50(param1);
            LevelManager.instance.FUN_4DE54(v3, 13);
        }
    }

    public bool FUN_39714(Vector3Int param1)
    {
        bool bVar1;
        Vector3Int v3;
        int iVar2;
        Wheel ppcVar3;
        Vector3Int local_10;

        bVar1 = false;

        if (wheelsType == _WHEELS.Ground)
        {
            local_10 = Utilities.FUN_24304(vTransform, param1);
            iVar2 = 0 < local_10.x ? 1 : 0;

            if (local_10.z < 0)
                iVar2 |= 2;

            ppcVar3 = wheels[iVar2];

            if (ppcVar3 == null)
            {
                ppcVar3 = wheels[iVar2 & 2];

                if (ppcVar3 == null)
                    return false;
            }

            if ((ppcVar3.flags & 0x40000000) == 0)
            {
                ppcVar3.state = _WHEEL_TYPE.Flatten;
                ppcVar3.flags |= 0x40000000;
                ppcVar3.physics2.X -= 0xc00;
                flags |= 0x20000;
                GameManager.instance.FUN_30CB0(ppcVar3, 300);
                v3 = GameManager.instance.FUN_2CE50(ppcVar3);
                LevelManager.instance.FUN_4DE54(v3, 13);
                //sound
                bVar1 = true;
            }
            else
                bVar1 = false;
        }

        return bVar1;
    }

    public void FUN_38408()
    {
        sbyte sVar1;

        int iVar4;

        if (DAT_18 == 0)
        {
            //sounds
        }

        if (wheelsType == _WHEELS.Ground)
        {
            //sounds
        }

        //sounds
    }

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
            vCollider.reader.Seek(0, SeekOrigin.Begin);

            if ((uVar5 & uVar6) == 0)
            {
                if ((uVar5 & 1) == 0)
                    local_40.x = vCollider.reader.ReadInt32(4);
                else
                    local_40.x = vCollider.reader.ReadInt32(16);

                if ((uVar5 & 4) == 0)
                    local_40.y = vCollider.reader.ReadInt32(8);
                else
                    local_40.y = vCollider.reader.ReadInt32(20);

                if ((uVar5 & 2) == 0)
                    local_40.z = vCollider.reader.ReadInt32(12);
                else
                    local_40.z = vCollider.reader.ReadInt32(24);

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
                    vTransform.rotation,
                    new Matrix2x4(local_60.x, local_60.y, local_60.z, 0));
                iVar2 = physics1.W * lightness;
                local_50.x -= (int)((ulong)((long)physics1.X * iVar2) >> 32);
                local_50.y -= (int)((ulong)((long)physics1.Y * iVar2) >> 32);
                lVar1 = (long)physics1.Z * iVar2;
                local_18 = (int)lVar1;
                local_14 = (int)((ulong)lVar1 >> 32);
                local_50.z -= local_14;
                FUN_2AFF8(local_50, local_60);
                iVar3 = physics2.X;
                iVar2 = iVar3;

                if (iVar3 < 0)
                    iVar2 = iVar3 + 31;

                iVar4 = physics2.Y;
                physics2.X = iVar3 - (iVar2 >> 5);
                iVar2 = iVar4;

                if (iVar4 < 0)
                    iVar2 = iVar4 + 31;

                iVar3 = physics2.Z;
                physics2.Y = iVar4 - (iVar2 >> 5);
                iVar2 = iVar3;

                if (iVar3 < 0)
                    iVar2 = iVar3 + 31;

                physics2.Z = iVar3 - (iVar2 >> 5);

                if ((flags & 0x40000000) != 0)
                    ; //FUN_3A020(-(physics1.Y / 19456), 0, 0 < id); //tmp

                return;
            }
        } while (true);

        void FUN_3EA0C()
        {
            local_40 = Utilities.FUN_24148(vTransform, local_40);
            iVar3 = FUN_2CFBC(local_40, out local_20);

            if (0 < local_40.y - iVar3)
            {
                iVar4 = -physics1.X;

                if (0 < physics1.X)
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

                iVar4 = -physics1.Z;

                if (0 < physics1.Z)
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

                if (0 < physics1.Y)
                    local_30.y -= physics1.Y >> 2;

                int cop2r32 = local_40.x - vTransform.position.x >> 3;
                int cop2r34 = local_40.y - vTransform.position.y >> 3;
                int cop2r36 = local_40.z - vTransform.position.z >> 3;
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
                    if (local_20.DAT_10[3] != 0 && local_20.DAT_10[3] != 7)
                    {
                        //function call by register
                    }
                }

                if (19456 < physics1.Y)
                    flags |= 0x40000000;
            }
        }
    }

    public int FUN_3A064(int param1, Vector3Int param2, bool param3)
    {
        Vector3Int auStack16;

        auStack16 = Utilities.FUN_24304(vTransform, param2);
        return FUN_3A020(param1, auStack16, param3);
    }

    public int FUN_3A020(int param1, Vector3Int param2, bool param3)
    {
        int iVar1;

        if (shield == 0)
            iVar1=0;//iVar1 = FUN_39DCC(pInt1, pInt2, isPlayer);
        else
        {
            FUN_393F8();
            iVar1 = 0;
        }

        return iVar1;
    }

    private void FUN_33AF8()
    {
        short sVar1;
        int iVar2;
        int iVar3;
        VigObject ppcVar4;

        sVar1 = (short)LevelManager.instance.FUN_35778(vTransform.position.x, vTransform.position.z);

        if (sVar1 != 0)
        {
            ai.FUN_51C54(vTransform.position, target.vTransform.position, 0x22740, 0);
            tags = 3;
            flags &= 0xffffffdf;
        }

        ppcVar4 = mgun;
        direction = 1;
        turning = 0;
        acceleration = 60;
        iVar3 = 12;

        if (ppcVar4.tags == 0)
        {
            if (!ppcVar4.GetType().IsSubclassOf(typeof(VigObject)))
                iVar2 = 0;
            else
                iVar2 = (int)ppcVar4.UpdateW(13, this);

            iVar3 = 12;

            if (iVar2 == 0)
                iVar3 = 4;
        }

        if (ppcVar4.GetType().IsSubclassOf(typeof(VigObject)))
            ppcVar4.UpdateW(iVar3, this);
    }

    private int FUN_33C10()
    {
        if (GameManager.instance.gameMode != _GAME_MODE.Quest &&
            GameManager.instance.gameMode != _GAME_MODE.Quest2)
            return 0;

        if ((GameManager.instance.DAT_1002 & 0x80) == 0)
            return 0;

        return 0; //tmp
        //quest...
    }

    private void FUN_33DE8()
    {
        bool bVar1;
        short sVar2;
        int iVar3;
        uint uVar4;
        short sVar5;
        int iVar6;
        int iVar7;
        VigObject ppcVar8;
        int iVar9;

        if ((GameManager.instance.DAT_28 - DAT_19 & 0x7f) == 0 &&
            (ai.DAT_00 < 1 || (flags & 0x20000000) != 0))
            ai.FUN_51C54(vTransform.position, target.vTransform.position, 0x22740, 0);

        direction = 1;
        sVar2 = (short)ai.FUN_51CFC(this, physics1.W * 32 + 0x10000);
        iVar6 = sVar2;
        iVar9 = -682;

        if (-683 < iVar6)
        {
            iVar9 = 682;

            if (iVar6 < 683)
                iVar9 = iVar6;
        }

        turning = (short)iVar9;
        iVar6 = physics1.W * DAT_B2;

        if (iVar6 < 0)
            iVar6 += 4095;

        iVar3 = DAT_B1 + (iVar6 >> 12);
        iVar6 = 0;

        if (0 < iVar3)
            iVar6 = iVar3;

        iVar9 *= iVar6;

        if (iVar9 < 0)
            iVar9 += 15;

        physics2.Y += iVar9 >> 4;
        iVar9 = sVar2;

        if (iVar9 < 0)
            iVar9 = -iVar9;

        if (iVar9 < 342 || physics1.W < 3052)
        {
            if (physics1.W < 6867)
            {
                iVar9 = 0;

                if (0 < acceleration)
                    iVar9 = acceleration;

                uVar4 = DAT_B3;
                iVar9++;
                bVar1 = iVar9 < (int)uVar4;
                sVar5 = (short)uVar4;

                if (bVar1)
                    sVar5 = (short)iVar9;

                acceleration = sVar5;
            }
            else
            {
                iVar6 = acceleration - 1;
                iVar9 = -DAT_B3;

                if (-DAT_B3 < iVar6)
                    iVar9 = iVar6;

                acceleration = (short)iVar9;
            }
        }
        else
        {
            iVar9 = 0;

            if (acceleration < 0)
                iVar9 = acceleration;

            iVar9--;
            uVar4 = (uint)-DAT_B3;
            bVar1 = (int)uVar4 < iVar9;
            sVar5 = (short)uVar4;

            if (bVar1)
                sVar5 = (short)iVar9;

            acceleration = sVar5;
        }

        iVar9 = 0;
        ppcVar8 = weapons[weaponSlot];
        bVar1 = false;

        if (ppcVar8 != null)
            bVar1 = ppcVar8.id == 0;

        if (bVar1)
        {
            iVar6 = FUN_33C10();

            if (iVar6 == 0)
            {
                if (!ppcVar8.GetType().IsSubclassOf(typeof(VigObject)))
                    iVar6 = 0;
                else
                    iVar6 = (int)ppcVar8.UpdateW(13, this);

                if (iVar6 != 0)
                    iVar9 = 1;
            }
        }

        FUN_3A5FC(iVar9);
        ppcVar8 = mgun;

        if (ppcVar8.tags == 0)
        {
            if (iVar9 != 0)
            {
                iVar7 = 4;
                goto LAB_340C0;
            }

            if (!ppcVar8.GetType().IsSubclassOf(typeof(VigObject)))
                iVar6 = 0;
            else
                iVar6 = (int)ppcVar8.UpdateW(13, this);

            iVar7 = 4;

            if (iVar6 == 0) goto LAB_340C0;
        }

        iVar7 = 12;
        LAB_340C0:
        if (ppcVar8.GetType().IsSubclassOf(typeof(VigObject)))
            ppcVar8.UpdateW(iVar7, this);

        if (bVar1)
        {
            if (iVar9 == 0)
                FUN_3A734(1);
            else
            {
                uVar4 = GameManager.FUN_2AC5C();

                if ((uVar4 & 7) == 0)
                    FUN_3A734(1);
            }
        }
    }

    private void FUN_33BE4()
    {
        if (60 < GameManager.instance.DAT_28)
            tags = 1;

        turning = 0;
        acceleration = 60;
    }

    private void FUN_341F8()
    {
        bool bVar1;
        short sVar2;
        uint uVar3;
        int iVar4;
        short sVar6;
        uint uVar7;
        int iVar8;
        int iVar9;
        VigObject oVar9;
        int iVar10;
        VigObject ppcVar10;
        int iVar11;
        VigObject ppcVar11;
        int iVar12;

        if ((GameManager.instance.DAT_28 - DAT_19 & 0x7f) == 0 && 
            (ai.DAT_00 < 1 || (flags & 0x20000000) != 0))
        {
            oVar9 = null;

            if (DAT_F4 != 0)
                oVar9 = GameManager.instance.FUN_30250(GameManager.instance.worldObjs, DAT_F4);

            if (DAT_F4 == 0 || oVar9 == null)
            {
                if (body[0] == null)
                {
                    if (maxFullHealth <= maxHalfHealth * 3)
                    {
                        uVar7 = 0xfe000000;

                        if (weapons[1] == null)
                            oVar9 = GameManager.instance.FUN_34120(GameManager.instance.worldObjs, uVar7, vTransform.position);

                        if (oVar9 == null)
                        {
                            oVar9 = GameManager.instance.FUN_34120(GameManager.instance.worldObjs, uVar7, vTransform.position);

                            if (oVar9 != null)
                                goto LAB_34388;
                            else if (wheelsType == _WHEELS.Ground)
                            {
                                uVar3 = GameManager.FUN_2AC5C();

                                if ((uVar3 & 1) == 0)
                                {
                                    oVar9 = GameManager.instance.FUN_34120(GameManager.instance.worldObjs, 0x840000, vTransform.position);

                                    if (oVar9 != null) goto LAB_34388;

                                    oVar9 = GameManager.instance.FUN_34120(GameManager.instance.worldObjs, 0xfe780000, vTransform.position);

                                    if (oVar9 != null) goto LAB_34388;
                                }
                            }

                            iVar10 = (int)GameManager.FUN_2AC5C();
                            iVar11 = GameManager.instance.FUN_30428(GameManager.instance.DAT_1078, 0); //s3?
                            oVar9 = GameManager.instance.FUN_30498(GameManager.instance.DAT_1078, 0, iVar10 * iVar11 >> 15);
                        }

                        goto LAB_343C8;
                    }
                    else
                    {
                        oVar9 = GameManager.instance.FUN_34120(GameManager.instance.worldObjs, 0x100000, vTransform.position);
                        uVar7 = 0x400000;

                        if (oVar9 == null)
                        {
                            oVar9 = GameManager.instance.FUN_34120(GameManager.instance.worldObjs, uVar7, vTransform.position);

                            if (oVar9 != null)
                                goto LAB_34388;
                            else if (wheelsType == _WHEELS.Ground)
                            {
                                uVar3 = GameManager.FUN_2AC5C();

                                if ((uVar3 & 1) == 0)
                                {
                                    oVar9 = GameManager.instance.FUN_34120(GameManager.instance.worldObjs, 0x840000, vTransform.position);

                                    if (oVar9 != null) goto LAB_34388;

                                    oVar9 = GameManager.instance.FUN_34120(GameManager.instance.worldObjs, 0xfe780000, vTransform.position);

                                    if (oVar9 != null) goto LAB_34388;
                                }
                            }

                            iVar10 = (int)GameManager.FUN_2AC5C();
                            iVar11 = GameManager.instance.FUN_30428(GameManager.instance.DAT_1078, 0); //s3?
                            oVar9 = GameManager.instance.FUN_30498(GameManager.instance.DAT_1078, 0, iVar10 * iVar11 >> 15);
                        }
                    }

                    LAB_34388:
                    DAT_F4 = oVar9.id;
                }
                else
                {
                    if ((body[0].maxHalfHealth + body[1].maxHalfHealth) * 3 < maxFullHealth)
                    {
                        oVar9 = GameManager.instance.FUN_34120(GameManager.instance.worldObjs, 0x100000, vTransform.position);
                        uVar7 = 0x400000;

                        if (oVar9 == null)
                        {
                            oVar9 = GameManager.instance.FUN_34120(GameManager.instance.worldObjs, uVar7, vTransform.position);

                            if (oVar9 != null)
                                goto LAB_34388;
                            else if (wheelsType == _WHEELS.Ground)
                            {
                                uVar3 = GameManager.FUN_2AC5C();

                                if ((uVar3 & 1) == 0)
                                {
                                    oVar9 = GameManager.instance.FUN_34120(GameManager.instance.worldObjs, 0x840000, vTransform.position);

                                    if (oVar9 != null) goto LAB_34388;

                                    oVar9 = GameManager.instance.FUN_34120(GameManager.instance.worldObjs, 0xfe780000, vTransform.position);

                                    if (oVar9 != null) goto LAB_34388;
                                }
                            }

                            iVar10 = (int)GameManager.FUN_2AC5C();
                            iVar11 = GameManager.instance.FUN_30428(GameManager.instance.DAT_1078, 0); //s3?
                            oVar9 = GameManager.instance.FUN_30498(GameManager.instance.DAT_1078, 0, iVar10 * iVar11 >> 15);
                        }
                    }
                    else
                    {
                        uVar7 = 0xfe000000;

                        if (weapons[1] == null)
                            oVar9 = GameManager.instance.FUN_34120(GameManager.instance.worldObjs, uVar7, vTransform.position);

                        if (oVar9 == null)
                        {
                            oVar9 = GameManager.instance.FUN_34120(GameManager.instance.worldObjs, uVar7, vTransform.position);

                            if (oVar9 != null)
                                goto LAB_34388;
                            else if (wheelsType == _WHEELS.Ground)
                            {
                                uVar3 = GameManager.FUN_2AC5C();

                                if ((uVar3 & 1) == 0)
                                {
                                    oVar9 = GameManager.instance.FUN_34120(GameManager.instance.worldObjs, 0x840000, vTransform.position);

                                    if (oVar9 != null) goto LAB_34388;

                                    oVar9 = GameManager.instance.FUN_34120(GameManager.instance.worldObjs, 0xfe780000, vTransform.position);

                                    if (oVar9 != null) goto LAB_34388;
                                }
                            }

                            iVar10 = (int)GameManager.FUN_2AC5C();
                            iVar11 = GameManager.instance.FUN_30428(GameManager.instance.DAT_1078, 0); //s3?
                            oVar9 = GameManager.instance.FUN_30498(GameManager.instance.DAT_1078, 0, iVar10 * iVar11 >> 15);
                        }

                        goto LAB_343C8;
                    }

                    LAB_34388:
                    DAT_F4 = oVar9.id;
                }

                LAB_343C8:
                if (oVar9 == null)
                    oVar9 = GameManager.instance.playerObjects[0];
            }

            ai.FUN_51C54(vTransform.position, oVar9.screen, 0x22740, 0);
        }

        direction = 1;
        sVar2 = (short)ai.FUN_51CFC(this, physics1.W * 32 + 0x10000);
        iVar9 = sVar2;
        iVar12 = -682;

        if (-682 < iVar9)
            iVar12 = iVar9;

        iVar8 = 682;

        if (iVar12 < 682)
            iVar8 = iVar12;

        turning = (short)iVar8;
        iVar12 = physics1.W * DAT_B2;

        if (iVar12 < 0)
            iVar12 += 4095;

        iVar4 = DAT_B1 + (iVar12 >> 12);
        iVar12 = 0;

        if (0 < iVar4)
            iVar12 = iVar4;

        iVar12 = (short)iVar8 * iVar12;

        if (iVar12 < 0)
            iVar12 += 15;

        physics2.Y += iVar12 >> 4;

        if (iVar9 < 0)
            iVar9 = -iVar9;

        if (iVar9 < 342 || physics1.W < 3052)
        {
            if (physics1.W < 6867)
            {
                iVar9 = 0;

                if (0 < acceleration)
                    iVar9 = acceleration;

                uVar3 = DAT_B3;
                iVar9++;
                bVar1 = iVar9 < (int)uVar3;
                sVar6 = (short)uVar3;

                if (bVar1)
                    sVar6 = (short)iVar9;

                acceleration = sVar6;
            }
            else
            {
                iVar12 = acceleration - 1;
                iVar9 = -DAT_B3;

                if (-DAT_B3 < iVar12)
                    iVar9 = iVar12;

                acceleration = (short)iVar9;
            }
        }
        else
        {
            iVar9 = 0;

            if (acceleration < 0)
                iVar9 = acceleration;

            iVar9--;
            uVar3 = (uint)-DAT_B3;
            bVar1 = (int)uVar3 < iVar9;
            sVar6 = (short)uVar3;

            if (bVar1)
                sVar6 = (short)iVar9;

            acceleration = sVar6;
        }

        ppcVar10 = weapons[weaponSlot];
        ppcVar11 = mgun;

        if (ppcVar10 != null)
        {
            if (ppcVar10.id == 0)
            {
                iVar9 = FUN_33C10();

                if (iVar9 == 0)
                {
                    oVar9 = target;

                    if (oVar9 != null)
                    {
                        iVar12 = vTransform.position.x - oVar9.vTransform.position.x;

                        if (iVar12 < 0)
                            iVar12 = -iVar12;

                        if (iVar12 < 0x12c000)
                        {
                            iVar12 = vTransform.position.y - oVar9.vTransform.position.y;

                            if (iVar12 < 0)
                                iVar12 = -iVar12;

                            if (iVar12 < 0x12c000)
                            {
                                iVar9 = vTransform.position.z - oVar9.vTransform.position.z;

                                if (iVar9 < 0)
                                    iVar9 = -iVar9;

                                if (iVar9 < 0x12c000)
                                {
                                    if (!ppcVar10.GetType().IsSubclassOf(typeof(VigObject)))
                                        iVar9 = 0;
                                    else
                                        iVar9 = (int)ppcVar10.UpdateW(13, this);

                                    FUN_3A5FC(iVar9);

                                    if (iVar9 == 0)
                                        FUN_3A734(1);
                                    else
                                    {
                                        uVar3 = GameManager.FUN_2AC5C();

                                        if ((uVar3 & 7) == 0)
                                            FUN_3A734(1);
                                    }

                                    if (ppcVar11.tags == 0)
                                    {
                                        if (iVar9 == 0)
                                        {
                                            if (!ppcVar11.GetType().IsSubclassOf(typeof(VigObject)))
                                                iVar9 = 0;
                                            else
                                                iVar9 = (int)ppcVar11.UpdateW(13, this);

                                            uVar7 = 4;

                                            if (iVar9 != 0)
                                                uVar7 = 12;
                                        }
                                        else
                                            uVar7 = 4;
                                    }
                                    else
                                        uVar7 = 12;

                                    if (!ppcVar11.GetType().IsSubclassOf(typeof(VigObject)))
                                        return;

                                    goto LAB_34704;
                                }
                            }
                        }
                    }
                }
            }
        }

        if (!ppcVar11.GetType().IsSubclassOf(typeof(VigObject)))
            return;

        uVar7 = 4;
        LAB_34704:
        ppcVar11.UpdateW((int)uVar7, this);
    }

    private void FUN_34728()
    {
        switch (tags)
        {
            case 1:
                FUN_33AF8();
                break;
            case 2:
                FUN_33DE8();
                break;
            case 3:
                FUN_341F8();
                break;
            case 4:
                FUN_33BE4();
                break;
        }
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
                if ((flags & 0x10000000) == 0)
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
                        iVar7 = physics1.W * GameManager.DAT_63F68[direction + 1];

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
                iVar6 = physics1.W / 2;

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
            Controller playerController = InputManager.controllers[~id];
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

            unaff_s1 = GameManager.instance.FUN_1E478(vTransform.position);
            iVar6 = (int)(unaff_s1 & 0xffff) * DAT_E2;
            cVar1 = DAT_18;

            if (iVar6 < 0)
                iVar6 += 4095;

            iVar7 = (int)(unaff_s1 >> 16) * DAT_E2;

            if (iVar7 < 0)
                iVar7 += 4095;

            uVar4 = (uint)(iVar6 >> 12 | (iVar7 >> 12) << 16);
        }
        else
        {
            uVar4 = GameManager.instance.FUN_1E478(vTransform.position);
            cVar1 = DAT_18;
        }

        //FUN_1E2C8

        if ((flags & 0x40000000) != 0)
        {
            //...
        }

        if (wheelsType== _WHEELS.Ground && (flags & 0x10000000) != 0 && 3051 < physics1.W)
        {
            iVar7 = physics2.W;
            iVar6 = physics1.W * 3;

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
            uVar4 = (uint)(physics1.W / 2);

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
        VigObject pcVar1;
        bool bVar2;
        int iVar2;
        VigObject oVar2;
        Vehicle vVar2;
        int iVar3;
        VigObject ppcVar4;
        uint uVar5;
        VigObject oVar5;
        int iVar7;
        uint uVar8;

        uVar8 = (uint)(playerController.DAT_B << 24 | playerController.DAT_A << 16 |
                playerController.steering << 8 | playerController.actions);

        if ((flags & 0x2000000) == 0)
        {
            if ((DAT_F6 & 64) != (uVar8 & 64))
            {
                vVar2 = GameManager.instance.playerObjects[~id];
                DAT_F6 ^= DAT_F6;

                if ((uVar8 & 64) == 0)
                    vVar2.physics2.M1 = 0;
                else
                    vVar2.physics2.M1 = 2048;

                closeViewer.vr.y = physics2.M1;
                DAT_A0.x = -DAT_A0.x;
                closeViewer.ApplyRotationMatrix();
            }
        }

        if ((uVar8 & 0x180000) != 0)
        {
            uVar5 = 0xffffffff;

            if ((uVar8 & 0x80000) != 0)
                uVar5 = 1;

            bVar2 = FUN_3A734((int)uVar5);
            DAT_F6 &= 0xffdf;
            //sound

            if (bVar2)
            {
                ppcVar4 = weapons[weaponSlot];

                if (ppcVar4.GetType().IsSubclassOf(typeof(VigObject)))
                    ppcVar4.UpdateW(11, this);
            }

            if ((uVar8 & 0x18) == 0x18)
                FUN_3E32C(_WHEELS.Ground, 0);
        }

        if ((DAT_F6 & 0x20) == 0 &&
            (GameManager.instance.DAT_28 - DAT_19 & 0x3f) == 0)
            FUN_3CCD4(0);

        if ((uVar8 & 0x200000) != 0)
        {
            oVar5 = null;

            if ((DAT_F6 & 0x20) != 0 && DAT_C6 < 0x100)
                oVar5 = target;

            oVar5 = FUN_3CF7C(oVar5);
            target = oVar5;
            DAT_C6 = 0;
            DAT_F6 |= 0x20;
            //sound
        }

        if ((uVar8 & 0x4000) == 0) goto LAB_3AF04;

        oVar2 = weapons[weaponSlot];

        if (oVar2 != null)
        {
            if (oVar2.id == 0) goto LAB_3AF04;

            if ((GameManager.instance.DAT_40 & 0x800) != 0)
            {
                oVar2.id = 0;
                goto LAB_3AF04;
            }
        }

        //sound
        LAB_3AF04:
        FUN_3A5FC((int)(uVar8 & 4));

        if (DAT_B6 == 0)
        {
            int seq = playerController.sequence[0] | playerController.sequence[1] << 8 |
                      playerController.sequence[2] << 16 | playerController.sequence[3] << 24;

            if ((uVar8 & 0x20000) != 0 && 0x100 < seq)
            {
                iVar2 = 0;

                if ((seq & 0xffff) == 0x1324)
                    FUN_3E32C(_WHEELS.Ground, 0);
                else
                {
                    iVar3 = 40;

                    do
                    {
                        ppcVar4 = weapons[iVar2];

                        if (ppcVar4 != null && ppcVar4.maxHalfHealth != 0)
                        {
                            iVar7 = 0;

                            if (ppcVar4.GetType().IsSubclassOf(typeof(VigObject)))
                                iVar7 = (int)ppcVar4.UpdateW(10, seq);

                            if (iVar7 != 0)
                            {
                                //sound

                                if (0 < iVar7)
                                    DAT_B6 = (short)iVar7;

                                playerController.sequence[0] = 0;
                                playerController.sequence[1] = 0;
                                playerController.sequence[2] = 0;
                                playerController.sequence[3] = 0;
                                break;
                            }
                        }

                        iVar2++;
                        iVar3 += 4;
                    } while (iVar2 < 3);
                }
            }
        }
        else
            DAT_B6--;

        uVar5 = 4;

        if ((uVar8 & 2) != 0)
            uVar5 = 12;

        pcVar1 = mgun;

        if (pcVar1.GetType().IsSubclassOf(typeof(VigObject)))
            pcVar1.UpdateW((int)uVar5, this);

        if (DAT_C6 < 0x100)
            DAT_C6 += 8;
    }

    public void FUN_3A5FC(int param1)
    {
        short sVar1;
        int iVar2;
        int iVar3;
        VigObject ppcVar4;

        ppcVar4 = weapons[weaponSlot];

        if (ppcVar4 != null)
        {
            if (param1 == 0 || ppcVar4.maxHalfHealth == 0 || ppcVar4.id != 0)
            {
                if (ppcVar4.GetType().IsSubclassOf(typeof(VigObject)))
                    ppcVar4.UpdateW(0, this);
            }
            else
            {
                if (!ppcVar4.GetType().IsSubclassOf(typeof(VigObject)))
                    sVar1 = 0;
                else
                    sVar1 = (short)ppcVar4.UpdateW(12, this);

                if (ppcVar4.maxHalfHealth != 0)
                {
                    if (0 < id && (ppcVar4.flags & 0x4000000) == 0)
                    {
                        if ((DAT_F6 & 2) == 0)
                            iVar3 = (2 - GameManager.instance.DAT_C6E) * 64;
                        else
                            iVar3 = (2 - GameManager.instance.DAT_C6E) * 32;

                        iVar2 = (int)GameManager.FUN_2AC5C();
                        sVar1 = (short)(sVar1 + iVar3 + (iVar2 * iVar3 >> 15));
                    }

                    ppcVar4.id = sVar1;
                }
            }
        }
    }

    public void FUN_3A280(uint param1)
    {
        byte bVar1;
        short sVar2;
        int iVar3;
        VigObject oVar3;
        uint uVar4;

        bVar1 = weaponSlot;
        uVar4 = bVar1;
        FUN_3A148((int)param1);
        iVar3 = (int)param1;

        for (int i = 0; i < 2 - param1; i++)
            weapons[iVar3 + i] = weapons[iVar3 + i + 1];

        weapons[2] = null;

        if ((int)uVar4 <= (int)param1)
        {
            if (param1 == 0) goto LAB_3A304;

            if (uVar4 != param1)
                return;

            if (weapons[iVar3] != null) goto LAB_3A304;
        }

        weaponSlot--;
        LAB_3A304:
        if (uVar4 == param1)
        {
            oVar3 = weapons[weaponSlot];

            if (oVar3 != null)
            {
                sVar2 = 30;

                if (30 < oVar3.id)
                    sVar2 = oVar3.id;

                oVar3.id = sVar2;
            }
        }
    }

    public bool FUN_3A734(int param1)
    {
        int iVar1;
        bool bVar2;
        uint uVar4;
        int iVar5;
        VigObject ppcVar6;

        ppcVar6 = weapons[weaponSlot];
        uVar4 = (uint)(weaponSlot + param1 + 3) % 3;
        weaponSlot = (byte)uVar4;
        iVar5 = 0;

        if (weapons[uVar4 & 0xff] == null)
        {
            iVar1 = 1;

            do
            {
                iVar5 = iVar1;

                if (2 < iVar5) break;

                uVar4 = (uint)(weaponSlot + param1 + 3) % 3;
                weaponSlot = (byte)uVar4;
                iVar1 = iVar5 + 1;
            } while (weapons[uVar4 & 0xff] == null);
        }

        bVar2 = false;

        if (iVar5 < 2)
        {
            if (ppcVar6.GetType().IsSubclassOf(typeof(VigObject)))
                ppcVar6.UpdateW(11, 0);

            bVar2 = true;
        }

        return bVar2;
    }

    public VigObject FUN_3CF7C(VigObject param1)
    {
        sbyte sVar1;
        VigTuple ppiVar3;
        List<VigTuple> ppiVar4;
        uint uVar5;
        uint uVar6;
        VigObject oVar7;
        uint uVar8;
        uint uVar9;
        uint uVar10;
        uint uVar11;
        VigObject oVar12;
        VigObject oVar13;
        VigObject oVar14;
        VigObject oVar15;

        oVar14 = null;
        oVar12 = null;
        uVar10 = 0xffffffff;
        uVar8 = 0xffffffff;

        if (param1 == null)
            uVar5 = 0;
        else
            uVar5 = (uint)Utilities.FUN_29F6C(vTransform.position, param1.screen);

        sVar1 = GameManager.instance.DAT_1128[~id];
        ppiVar4 = GameManager.instance.worldObjs;

        for (int i = 0; i < ppiVar4.Count; i++)
        {
            ppiVar3 = ppiVar4[i];
            oVar7 = ppiVar3.vObject;
            uVar9 = uVar8;
            uVar11 = uVar10;
            oVar13 = oVar12;
            oVar15 = oVar14;

            if (oVar7 == this || oVar7.type == 3 || 
                (oVar7.flags & 0x4000) == 0)
            {
                uVar8 = uVar9;
                uVar10 = uVar11;
                oVar12 = oVar13;
                oVar14 = oVar15;
            }
            else
            {
                if (0 < oVar7.id || sVar1 != GameManager.instance.DAT_1128[~oVar7.id])
                {
                    uVar6 = (uint)Utilities.FUN_29F6C(vTransform.position, oVar7.screen);

                    if (uVar5 < uVar6)
                    {
                        uVar9 = uVar6;
                        oVar13 = oVar7;

                        if (uVar6 < uVar8)
                        {
                            uVar8 = uVar9;
                            uVar10 = uVar11;
                            oVar12 = oVar13;
                            oVar14 = oVar15;
                            continue;
                        }
                    }

                    uVar9 = uVar8;
                    uVar11 = uVar6;
                    oVar13 = oVar12;
                    oVar15 = oVar7;

                    if (uVar6 < uVar10)
                    {
                        uVar8 = uVar9;
                        uVar10 = uVar11;
                        oVar12 = oVar13;
                        oVar14 = oVar15;
                    }
                }
            }
        }

        if (oVar12 == null)
            oVar12 = oVar14;

        return oVar14;
    }

    public void FUN_3E32C(_WHEELS param1, ushort param2)
    {
        VigObject oVar1;
        VigObject oVar4;
        int iVar5;
        int iVar6;

        if (DAT_B4 != 0)
            return;

        iVar6 = 0;

        if (wheelsType == param1)
        {
            if (param1 == _WHEELS.Ground)
            {
                transformation = 0;
                goto LAB_3E47C;
            }
        }
        else
        {
            iVar5 = 12;

            do
            {
                oVar4 = wheels[iVar6];

                if (oVar4 != null)
                {
                    GameObject obj = new GameObject();
                    oVar1 = obj.AddComponent<VigObject>();
                    oVar1.screen = oVar4.screen;
                    oVar1.ApplyTransformation();
                    oVar4.vTransform.position = new Vector3Int(0, 0, 0);
                    oVar4.FUN_2CCBC();
                    Utilities.FUN_2CC9C(oVar1, oVar4);
                    Utilities.FUN_2CC9C(this, oVar1);
                    oVar1.transform.parent = transform;
                }

                iVar6++;
                iVar5 += 4;
            } while (iVar6 < 6);

            wheelsType = param1;
            DAT_B4 = 32;
            physics1.Y -= 195200;
            //sound

            if (param1 == _WHEELS.Ground)
            {
                if (0 < id && transformation == 2)
                    tags = 1;

                if (param1 == _WHEELS.Ground)
                {
                    transformation = 0;
                    goto LAB_3E47C;
                }
            }
        }

        transformation = param2;
        LAB_3E47C:
        DAT_C2 = 0;
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
                        ((uint)vTransform.position.x, (uint)vTransform.position.z);

                    if ((GameManager.instance.DAT_40 & 0x80000) == 0)
                        iVar4 = -0x32000;
                    else
                        iVar4 = -0x12C000;

                    iVar4 = (iVar3 - vTransform.position.y) + iVar4;

                    if (iVar4 < 0)
                    {
                        iVar3 = -0x32000;

                        if (-0x32000 < iVar4)
                            iVar3 = iVar4;

                        if (iVar3 < 0)
                            iVar3 += 1023;

                        iVar3 = iVar3 >> 10;
                        iVar4 = vTransform.rotation.V01 * iVar3;

                        if (iVar4 < 0)
                            iVar4 += 31;

                        iVar8 = vTransform.rotation.V11 * iVar3;
                        physics1.X += iVar4 >> 5;

                        if (iVar8 < 0)
                            iVar8 += 31;

                        iVar3 = vTransform.rotation.V21 * iVar3;
                        physics1.Y += (iVar8 >> 5) - GameManager.instance.gravityFactor;

                        if (iVar3 < 0)
                            iVar3 += 31;

                        physics1.Z += iVar3 >> 5;
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
                if ((param1 & 0xffff0000) != 0 && (flags & 0x10000000) != 0)
                {
                    if (DAT_B0 < 1 || (8391 < physics1.W || direction < 1))
                    {
                        if (2287 < physics1.W)
                            goto LAB_3D36C;
                    }
                    else
                    {
                        FUN_2B1FC(GameManager.DAT_A18, GameManager.DAT_A24);
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
        int iVar13;
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

            if (vTransform.rotation.V11 < 0)
            {
                uVar17 = playerController.stick[0];

                if ((int)(uVar17 ^ playerController.stick[0]) < 0)
                    uVar17 = 0;

                iVar12 = physics2.Z;
                iVar8 = (int)(uVar17 << 2);

                if (cVar1 != _WHEELS.Sea)
                {
                    physics2.Z = iVar12 + (int)uVar17;
                    return;
                }

                physics2.Z = iVar12 + iVar8;
                return;
            }

            if ((uVar17 & 0x400) == 0 && playerController.stick[0] < 241)
            {
                if (0 < breaking)
                    breaking = (sbyte)-breaking;

                if (cVar1 == _WHEELS.Ground)
                {
                    iVar8 = physics1.W * DAT_B2;

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

                    iVar8 = physics2.Y + (iVar8 >> 4);
                    physics2.Y = iVar8;
                }
            }
            else
            {
                iVar8 = turning;

                if (iVar8 < 0)
                    iVar8 = -iVar8;

                if (iVar8 < 170)
                {
                    if (physics1.W < 2370)
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
                        iVar8 = physics2.Y + turning * -2;
                    else
                        iVar8 = physics2.Y + turning * 2;

                    physics2.Y = iVar8;
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
                iVar8 = physics2.W;

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
                    iVar12 = iVar8 * physics1.W;

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
                    if (physics1.W < 2370)
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

            if (0 < vTransform.rotation.V11)
            {
                uVar7 = 0;

                if (direction < 1)
                {
                    if ((uVar17 & 0x18000000) != 0)
                    {
                        uVar7 = flags & 0x7fffffff;

                        if (direction < 0 && ((uVar17 & uVar19) != 0 ||
                            physics2.W < -4997120))
                            uVar7 |= 0x80000000;

                        flags = uVar7;
                    }

                    uVar7 = flags >> 31;
                }
                else
                    flags &= 0x7fffffff;

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

                    iVar8 = physics1.W * DAT_B2;

                    if (iVar8 < 0)
                        iVar8 += 4095;

                    iVar15 = DAT_B1 + (iVar8 >> 12);
                    iVar8 = 0;

                    if (0 < iVar15)
                        iVar8 = iVar15;

                    iVar8 = (int)(uVar7 * 2 - 1) * iVar12 * iVar8;

                    if (iVar8 < 0)
                        iVar8 += 15;

                    physics2.Y -= iVar8 >> 4;
                    return;
                }

                if ((uVar17 & 0x800) == 0)
                {
                    iVar8 = 0x2aa;

                    if (turning + 32 < 0x2aa)
                        iVar8 = turning + 32;

                    turning = (short)iVar8;
                    iVar8 = physics2.Y;

                    if (uVar7 == 0)
                    {
                        physics2.Y = iVar8 + 1280;
                        return;
                    }
                }
                else
                {
                    iVar8 = -0x2aa;

                    if (-0x2aa < turning - 32)
                        iVar8 = turning - 32;

                    turning = (short)iVar8;
                    iVar8 = physics2.Y;

                    if (uVar7 != 0)
                    {
                        physics2.Y = iVar8 + 1280;
                        return;
                    }
                }

                physics2.Y = iVar8 - 1280;
                return;
            }

            if ((uVar17 & 0x8000000) == 0)
            {
                if ((uVar17 & 0x10000000) == 0)
                    return;

                iVar12 = physics2.Z;
                iVar8 = 0x8000;

                if (wheelsType == _WHEELS.Sea)
                    iVar8 = 0x10000;
            }
            else
            {
                iVar12 = physics2.Z;
                iVar8 = -0x8000;

                if (wheelsType == _WHEELS.Sea)
                    iVar8 = -0x10000;
            }

            physics2.Z = iVar12 + iVar8;
            return;
        }

        if (_CONTROLLER_TYPE.JoypadAnalog < playerController.type)
            return;

        uVar19 = playerController.stick[0];
        uVar7 = uVar19 - 128;
        iVar12 = 128 - playerController.stick[1];
        iVar8 = iVar12;

        if (iVar12 < 0)
            iVar8 = -iVar12;

        iVar15 = 0;

        if (8 < iVar8)
            iVar15 = iVar12;

        bVar2 = false;

        if (((uVar17 & 0x400) != 0 && iVar15 < -64) || (uVar17 & 0x200) != 0)
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
                iVar8 = turning * physics1.W;

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

                if ((InputManager.turnRadius[uVar19] ^ (int)uVar14) < 0)
                {
                    sVar5 = (short)iVar8;

                    if (-1 < (int)uVar14)
                        sVar5 = (short)-sVar5;
                }
                else
                {
                    iVar13 = InputManager.turnRadius[uVar19] - (int)uVar14;
                    iVar12 = -iVar8;

                    if (-iVar8 <= iVar13)
                    {
                        iVar12 = iVar8;

                        if (iVar13 <= iVar8)
                            iVar12 = iVar13;
                    }

                    sVar5 = (short)(turning + iVar12);
                }
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

                iVar8 = turning - 32;
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
            iVar8 = physics2.W;

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
                iVar12 = physics1.X;

                if (iVar12 < 0)
                    iVar12 += 127;

                iVar15 = physics1.Z;

                if (iVar15 < 0)
                    iVar15 += 127;

                iVar8 = -1;

                if (-1941505 <
                    vTransform.rotation.V02 * (iVar12 >> 7) +
                    vTransform.rotation.V22 * (iVar15 >> 7))
                    iVar8 = 1;
            }
        }

        if (vTransform.rotation.V11 < 0)
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
                physics2.Z += iVar8 * 4;
            else
                physics2.Z += iVar8;
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
                    if (physics1.W < 2370)
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

                iVar12 = physics2.Y;
                iVar8 = iVar8 * turning * 2;
            }
            else
            {
                if (0 < breaking)
                    breaking = (sbyte)-breaking;

                if (!bVar3 || wheelsType != _WHEELS.Ground)
                    goto LAB_3E1C8;

                iVar12 = physics1.W * DAT_B2;

                if (iVar12 < 0)
                    iVar12 += 4095;

                iVar15 = DAT_B1 + (iVar12 >> 12);
                iVar12 = 0;

                if (0 < iVar15)
                    iVar12 = iVar15;

                iVar8 = (iVar8 * turning * iVar12) / 14;
                iVar12 = physics2.Y;
            }

            physics2.Y = iVar12 + iVar8;
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

        iVar12 = playerController.stick[2] - 128;
        iVar8 = iVar12;

        if (iVar12 < 0)
            iVar8 = -iVar12;

        if (32 < iVar8)
        {
            if (iVar12 < 0)
                iVar12 = playerController.stick[2] - 125;

            vCamera.DAT_92 -= (short)(iVar12 >> 2);
        }

        iVar12 = playerController.stick[3] - 128;
        iVar8 = iVar12;

        if (iVar12 < 0)
            iVar8 = -iVar12;

        if (32 < iVar8)
        {
            iVar12 = iVar12 * 3051;

            if (iVar12 < 0)
                iVar12 += 127;

            iVar12 = vCamera.DAT_9C + (iVar12 >> 7);
            iVar8 = DAT_58 << 1;

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

        if ((flags & 0x2000000) != 0)
            return;

        if (GameManager.instance.DAT_C74 != 0)
            return;

        vCamera.FUN_4B898();
    }
}
