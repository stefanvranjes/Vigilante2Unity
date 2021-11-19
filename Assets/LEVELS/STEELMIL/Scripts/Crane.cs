using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crane : VigObject
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
        int iVar4;
        ConfigContainer ccVar4;
        int iVar5;
        int iVar6;
        int iVar7;
        Vehicle ppcVar8;
        int iVar9;
        BufferedBinaryReader pcVar9;
        int iVar10;
        VigObject oVar10;
        Vector3Int local_28;
        Vector3Int local_30;
        Vector3Int local_40;
        VigTransform auStack128;

        if (hit.self.type != 2)
            return 0;

        if (1U < (byte)tags)
            return 0;

        oVar10 = child2;

        if (oVar10 == null)
            return 0;

        ppcVar8 = (Vehicle)hit.self;
        pcVar9 = ppcVar8.vCollider.reader;
        ccVar4 = oVar10.FUN_2C5F4(0x8000);
        auStack128 = GameManager.instance.FUN_2CEAC(oVar10, ccVar4);
        local_28 = new Vector3Int();
        local_28.x = (pcVar9.ReadInt32(4) + pcVar9.ReadInt32(16)) / 2;
        local_28.y = pcVar9.ReadInt32(8);
        local_28.z = (pcVar9.ReadInt32(12) + pcVar9.ReadInt32(24)) / 2;
        local_28 = Utilities.FUN_24148(hit.self.vTransform, local_28);
        local_40 = new Vector3Int();
        local_40.x = auStack128.position.x - local_28.x;
        local_40.y = auStack128.position.y - local_28.y;
        local_40.z = auStack128.position.z - local_28.z;
        iVar10 = Utilities.FUN_29FC8(local_40, out local_30);
        iVar5 = -iVar10 + 0x40000;

        if (iVar10 < 0x40001)
        {
            if (iVar5 < 0)
                iVar5 = -iVar10 + 0x40003;

            iVar5 >>= 2;
            local_40.x = local_30.x * iVar5 >> 12;
            local_40.y = local_30.y * iVar5 >> 12;
            local_40.z = local_30.z * iVar5 >> 12;
            iVar6 = ppcVar8.physics1.X;
            iVar9 = iVar6;

            if (iVar6 < 0)
                iVar9 = iVar6 + 31;

            iVar7 = ppcVar8.physics1.Y;
            ppcVar8.physics1.X = iVar6 + (local_40.x - (iVar9 >> 5));
            iVar9 = iVar7;

            if (iVar7 < 0)
                iVar9 = iVar6 + 31;

            iVar6 = ppcVar8.physics1.Z;
            ppcVar8.physics1.Y = iVar7 + (local_40.y - (iVar9 >> 5));
            iVar9 = iVar6;

            if (iVar6 < 0)
                iVar9 = iVar6 + 31;

            ppcVar8.physics1.Z = iVar6 + (local_40.z - (iVar9 >> 5));

            if (tags == 0)
            {
                sVar1 = (sbyte)GameManager.instance.FUN_1DD9C();
                DAT_18 = sVar1;
                GameManager.instance.FUN_1E628(sVar1, GameManager.instance.DAT_C2C, 56, auStack128.position, true);
                tags = 1;
                FUN_30B78();
                DAT_80 = ppcVar8;
            }
            else
            {
                if (iVar10 < 0x2801 && ppcVar8.physics1.W < 0x5f5)
                {
                    tags = 2;
                    ppcVar8.vTransform.position.y += auStack128.position.y - local_28.y;
                    DAT_AC = vTransform.position.z;
                    iVar4 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E628(iVar4, vData.sndList, 1, vTransform.position);
                    ppcVar8.state = _VEHICLE_TYPE.Crane;
                    ppcVar8.physics1.X = 0;
                    ppcVar8.flags |= 0x4000000;
                    ppcVar8.physics1.Y = 0;
                    ppcVar8.physics1.Z = 0;

                    if (DAT_18 != 0)
                    {
                        GameManager.instance.FUN_1DE78(DAT_18);
                        DAT_18 = 0;
                    }

                    iVar4 = GameManager.instance.FUN_1DD9C();
                    DAT_A4 = iVar4;
                    GameManager.instance.FUN_1E628(iVar4, vData.sndList, 6, vTransform.position, true);
                    DAT_B4 = -91;
                    DAT_A8 = 0;
                    DAT_B0 = 6;
                    Utilities.FUN_2A454(auStack128, ppcVar8.vTransform, out DAT_84_2);
                }

                DAT_80 = ppcVar8;
            }
        }

        return 0;
    }

    public VigTransform DAT_84_2; //0x84
    public int DAT_A4; //0xA4
    public int DAT_A8; //0xA8
    public int DAT_AC; //0xAC
    public int DAT_B0; //0xB0
    public int DAT_B4; //0xB4

    //FUN_2C4C (STEELMIL.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        uint uVar2;
        Magnet2 puVar3;
        int iVar4;
        ConfigContainer ccVar4;
        VigObject oVar5;
        int iVar10;
        BufferedBinaryReader brVar10;
        VigObject oVar11;
        Vector3Int local_60;
        Vector3Int local_50;
        Vector3Int local_40;
        VigTransform local_38;
        VigTransform auStack128;

        switch (arg1)
        {
            case 0:
                switch (tags)
                {
                    case 1:
                        if (DAT_80 == null)
                        {
                            FUN_30BA8();
                            tags = 0;

                            if (DAT_18 != 0)
                            {
                                GameManager.instance.FUN_1DE78(DAT_18);
                                DAT_18 = 0;
                            }
                        }
                        else
                        {
                            if (arg2 == 0)
                                return 0;

                            uVar2 = GameManager.FUN_2AC5C();

                            if ((uVar2 & 1) != 0)
                            {
                                oVar11 = child2;
                                puVar3 = LevelManager.instance.xobfList[19].ini.FUN_2C17C(49, typeof(Magnet2), 8) as Magnet2;
                                brVar10 = DAT_80.vCollider.reader;
                                local_60 = new Vector3Int();
                                local_60.x = (brVar10.ReadInt32(4) + brVar10.ReadInt32(16)) / 2;
                                local_60.y = brVar10.ReadInt32(8);
                                local_60.z = (brVar10.ReadInt32(12) + brVar10.ReadInt32(24)) / 2;
                                local_60 = Utilities.FUN_24148(DAT_80.vTransform, local_60);
                                iVar10 = (int)GameManager.FUN_2AC5C();
                                puVar3.vTransform.position.x = local_60.x + iVar10;
                                iVar10 = (int)GameManager.FUN_2AC5C();
                                puVar3.vTransform.position.y = local_60.y + iVar10;
                                iVar10 = (int)GameManager.FUN_2AC5C();
                                puVar3.vTransform.position.z = local_60.z + iVar10;
                                ccVar4 = oVar11.FUN_2C5F4(0x8000);
                                auStack128 = GameManager.instance.FUN_2CEAC(oVar11, ccVar4);
                                local_50 = new Vector3Int();
                                local_50.x = auStack128.position.x - local_60.x;
                                local_50.y = auStack128.position.y - local_60.y;
                                local_50.z = auStack128.position.z - local_60.z;
                                Utilities.FUN_29FC8(local_50, out local_40);
                                iVar10 = local_40.x;

                                if (iVar10 < 0)
                                    iVar10 += 15;

                                puVar3.physics1.Z = iVar10 >> 4;
                                iVar10 = local_40.y;

                                if (iVar10 < 0)
                                    iVar10 += 15;

                                puVar3.physics1.W = iVar10 >> 4;
                                iVar10 = local_40.z;

                                if (iVar10 < 0)
                                    iVar10 += 15;

                                puVar3.physics2.X = iVar10 >> 4;
                                puVar3.flags = 0xb4;
                                puVar3.vTransform.rotation.V00 = 0x1000;
                                puVar3.vTransform.rotation.V11 = 0x1000;
                                puVar3.vTransform.rotation.V22 = 0x1000;
                                puVar3.FUN_305FC();
                            }

                            DAT_80 = null;
                        }

                        break;
                    case 2:
                        oVar5 = child2;
                        oVar5.FUN_24700((short)DAT_B4, 0, 0);
                        oVar5.vTransform.rotation = Utilities.MatrixNormal(oVar5.vTransform.rotation);
                        iVar10 = DAT_B4;

                        if (iVar10 < 0)
                            iVar10 = -iVar10;

                        if (91 < iVar10)
                            DAT_B0 = -DAT_B0;

                        DAT_B4 += DAT_B0;

                        if (DAT_AC - vTransform.position.z < 0x190001 && 
                            ((Vehicle)DAT_80).state == _VEHICLE_TYPE.Crane)
                        {
                            if (DAT_A8 < 0x2666)
                                DAT_A8 += 163;

                            vTransform.position.z -= DAT_A8;
                            ccVar4 = oVar5.FUN_2C5F4(0x8000);
                            local_38 = GameManager.instance.FUN_2CEAC(oVar5, ccVar4);
                            DAT_80.vTransform = Utilities.CompMatrixLV(local_38, DAT_84_2);
                            return 0;
                        }

                        iVar4 = GameManager.instance.FUN_1DD9C();
                        GameManager.instance.FUN_1E628(iVar4, vData.sndList, 1, vTransform.position);
                        ((Vehicle)DAT_80).FUN_41FEC();
                        DAT_80 = null;
                        tags = 3;
                        break;
                    case 3:
                        vTransform.position.z += 0x2666;
                        child2.FUN_24700((short)DAT_B4, 0, 0);
                        child2.vTransform.rotation = Utilities.MatrixNormal(child2.vTransform.rotation);
                        iVar10 = DAT_B4;

                        if (iVar10 < 0)
                            iVar10 = -iVar10;

                        if (91 < iVar10)
                            DAT_B0 = -DAT_B0;

                        DAT_B4 += DAT_B0;

                        if (vTransform.position.z < DAT_AC)
                            return 0;

                        vTransform.position.z = DAT_AC;
                        tags = 4;
                        iVar4 = GameManager.instance.FUN_1DD9C();
                        GameManager.instance.FUN_1E628(iVar4, vData.sndList, 1, vTransform.position);

                        if (DAT_A4 == 0)
                            return 0;

                        GameManager.instance.FUN_1DE78(DAT_A4);
                        DAT_A4 = 0;
                        break;
                    case 4:
                        oVar5 = child2;
                        oVar5.FUN_24700((short)DAT_B4, 0, 0);
                        oVar5.vTransform.rotation = Utilities.MatrixNormal(oVar5.vTransform.rotation);

                        if (oVar5.vTransform.rotation.V11 < 4092)
                        {
                            iVar10 = DAT_B4;

                            if (iVar10 < 0)
                                iVar10 = -iVar10;

                            if (91 < iVar10)
                                DAT_B0 = -DAT_B0;
                        }
                        else
                        {
                            FUN_30BA8();
                            tags = 0;
                        }

                        DAT_B4 += DAT_B0;
                        break;
                }

                break;
            case 1:
                type = 3;
                flags |= 0x100;
                break;
            case 8:
                FUN_32B90((uint)arg2);
                return 0;
        }

        return 0;
    }
}
