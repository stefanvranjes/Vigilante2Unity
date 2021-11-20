using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneSmall : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public VigTransform DAT_84_2; //0x84
    public int DAT_A4; //0xA4
    public short DAT_A8; //0xA8
    public short DAT_AA; //0xAA
    public int DAT_B4; //0xB4
    public int DAT_B8; //0xB8
    public int DAT_BC; //0xBC

    public static VigObject OnInitialize(XOBF_DB arg1, int arg2, uint arg3)
    {
        Dictionary<int, Type> dict = new Dictionary<int, Type>();
        dict.Add(210, typeof(VigChild));
        dict.Add(211, typeof(VigChild));
        dict.Add(213, typeof(VigChild));
        dict.Add(215, typeof(VigChild));
        VigObject obj = arg1.ini.FUN_2C17C((ushort)arg2, typeof(CraneSmall), arg3, dict);
        FUN_14C0((VigChild)obj.child2, _CHILD_TYPE.Default);
        return obj;
    }

    public override uint OnCollision(HitDetection hit)
    {
        if (hit.self.type != 8)
            return 0;

        return FUN_32B90(hit.self.maxHalfHealth) ? 1U : 0;
    }

    //FUN_1570 (HARBOR.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        short sVar1;
        sbyte sVar2;
        VigObject unaff_s2;
        VigObject unaff_s3;
        short sVar3;
        short sVar4;
        VigObject unaff_s5;
        int iVar5;
        VigObject unaff_s6;
        int iVar6;
        int iVar7;
        uint uVar7;
        ConfigContainer ccVar7;
        int iVar8;
        BufferedBinaryReader pcVar9;
        int iVar10;
        VigObject oVar10;
        VigObject oVar12;
        Vehicle ppcVar11;
        Vector3Int local_48;
        Vector3Int local_38;
        Vector3Int local_28;
        VigTransform auStack136;
        VigTransform auStack104;

        switch (arg1)
        {
            case 0:
                break;
            case 1:
                child2.type = 3;
                FUN_14C0((VigChild)child2, _CHILD_TYPE.Child);
                FUN_1518(child2, 3);
                sVar1 = id;

                if (sVar1 == 114)
                {
                    DAT_A8 = 0x200;
                    child2.vr.y = 0x200;
                    sVar4 = -0x200;
                }
                else
                {
                    if (114 < sVar1)
                    {
                        if (sVar1 == 115)
                        {
                            DAT_A8 = 0;
                            DAT_AA = -0x755;
                            return 0;
                        }

                        return 0;
                    }

                    if (sVar1 != 113)
                        return 0;

                    DAT_A8 = -0x200;
                    child2.vr.y = -0x200;
                    sVar4 = 0x400;
                }

                DAT_AA = sVar4;
                child2.ApplyTransformation();
                return 0;
            default:
                return 0;
            case 8:
                uVar7 = FUN_32B90((uint)arg2) ? 1U : 0;
                return uVar7;
        }

        switch (tags)
        {
            case 1:
                oVar12 = child2;
                ppcVar11 = DAT_80 as Vehicle;
                oVar10 = oVar12.child2;
                unaff_s3 = oVar10.child2;

                if (oVar10 == null || unaff_s3 == null)
                    return 0; //FUN_15D9C

                DAT_A4--;
                oVar10.vr.x += DAT_B4;
                unaff_s3.vr.x -= DAT_B4;
                oVar10.ApplyTransformation();
                unaff_s3.ApplyTransformation();

                if (DAT_80 == null)
                    iVar10 = DAT_A4;
                else
                {
                    pcVar9 = ppcVar11.vCollider.reader;
                    ccVar7 = unaff_s3.FUN_2C5F4(0x8000);
                    auStack136 = GameManager.instance.FUN_2CEAC(unaff_s3, ccVar7);
                    local_48 = new Vector3Int();
                    local_48.x = (pcVar9.ReadInt32(4) + pcVar9.ReadInt32(16)) / 2;
                    local_48.z = (pcVar9.ReadInt32(12) + pcVar9.ReadInt32(24)) / 2;
                    local_48.y = pcVar9.ReadInt32(8);
                    local_38 = Utilities.FUN_24148(ppcVar11.vTransform, local_48);
                    local_28 = new Vector3Int();
                    local_28.x = auStack136.position.x - local_38.x;
                    local_28.y = auStack136.position.y - local_38.y;
                    local_28.z = auStack136.position.z - local_38.z;
                    iVar10 = Utilities.FUN_29E84(local_28);

                    if (iVar10 < 0x50001)
                    {
                        iVar8 = ppcVar11.vTransform.rotation.V12;
                        ppcVar11.physics1.X = local_28.x * 4 + ppcVar11.physics1.X / 2;
                        ppcVar11.physics1.Y = local_28.y + ppcVar11.physics1.Y / 2;
                        ppcVar11.physics2.X = 0;
                        ppcVar11.physics2.Y = 0;
                        ppcVar11.physics2.Z = 0;
                        ppcVar11.physics1.Z = local_28.z * 4 + ppcVar11.physics1.Z / 2;

                        if (iVar8 < 0)
                            iVar8 += 7;

                        iVar5 = -ppcVar11.vTransform.rotation.V10;

                        if (0 < ppcVar11.vTransform.rotation.V10)
                            iVar5 += 7;

                        ppcVar11.FUN_24700((short)(iVar8 >> 3), 0, (short)(iVar5 >> 3));
                        ppcVar11.vTransform.rotation = Utilities.MatrixNormal(ppcVar11.vTransform.rotation);

                        if (DAT_A4 == 0)
                        {
                            if (iVar10 < 0x18001)
                            {
                                tags = 3;
                                iVar7 = GameManager.instance.FUN_1DD9C();
                                GameManager.instance.FUN_1E628(iVar7, vData.sndList, 2, vTransform.position);
                                LevelManager.instance.FUN_4DD54(ppcVar11, local_48, 142);
                                ppcVar11.vTransform.position.x += auStack136.position.x - local_38.x;
                                ppcVar11.vTransform.position.y += auStack136.position.y - local_38.y;
                                ppcVar11.vTransform.position.z += auStack136.position.z - local_38.z;
                                sVar1 = (short)oVar12.vr.y;
                                DAT_BC = 0;
                                DAT_B8 = (DAT_AA - sVar1) / 120;
                                auStack104 = Utilities.FUN_2A3EC(auStack136);
                                DAT_84_2 = Utilities.CompMatrixLV(auStack104, ppcVar11.vTransform);
                                ppcVar11.physics1.X = 0;
                                ppcVar11.physics1.Y = 0;
                                ppcVar11.physics1.Z = 0;
                                ppcVar11.state = _VEHICLE_TYPE.CraneSmall;
                            }
                            else
                                tags = 2;

                            DAT_B4 = -4;
                            return 0;
                        }

                        return 0;
                    }

                    iVar10 = DAT_A4;
                    DAT_80 = null;
                }

                if (iVar10 != 0)
                    return 0;

                tags = 2;
                DAT_B4 = -1;
                break;
            case 2:
                oVar10 = child2.child2;
                unaff_s2 = oVar10.child2;

                if (oVar10 == null || unaff_s2 == null)
                    return 0; //FUN_15D9C

                unaff_s2.vr.x -= DAT_B4;
                sVar3 = (short)(oVar10.vr.x + DAT_B4);
                oVar10.vr.x = sVar3;

                if (sVar3 << 16 < 1)
                {
                    oVar10.vr.x = 0;
                    unaff_s2.vr.x = 0;
                    tags = 0;
                    FUN_30BA8();

                    if (DAT_18 != 0)
                    {
                        GameManager.instance.FUN_1DE78(DAT_18);
                        DAT_18 = 0;
                    }
                }

                unaff_s2.ApplyTransformation();
                oVar10.ApplyTransformation();
                return 0;
            case 3:
                oVar12 = child2;
                oVar10 = oVar12.child2;
                unaff_s5 = oVar10.child2;

                if (oVar10 == null || unaff_s5 == null)
                    return 0; //FUN_15D9C

                if (-0x200 < oVar10.vr.x)
                {
                    oVar10.vr.x += DAT_B4;
                    unaff_s5.vr.x -= DAT_B4;
                    unaff_s5.ApplyTransformation();
                    oVar10.ApplyTransformation();
                }

                iVar10 = DAT_B8;

                if (iVar10 < 1)
                {
                    if (iVar10 < DAT_BC)
                    {
                        iVar10 = DAT_BC - 1;
                        DAT_BC = iVar10;
                    }
                }
                else
                {
                    if (DAT_BC < iVar10)
                    {
                        iVar10 = DAT_BC + 1;
                        DAT_BC = iVar10;
                    }
                }

                oVar12.vr.y += DAT_BC;
                oVar12.ApplyTransformation();
                iVar10 = DAT_AA - oVar12.vr.y;

                if (DAT_BC < 0)
                    iVar10 = -iVar10;

                if (10 < iVar10)
                {
                    ccVar7 = unaff_s5.FUN_2C5F4(0x8000);
                    auStack136 = GameManager.instance.FUN_2CEAC(unaff_s5, ccVar7);
                    DAT_80.vTransform = Utilities.CompMatrixLV(auStack136, DAT_84_2);
                    return 0;
                }

                tags = 4;

                if (DAT_18 != 0)
                {
                    GameManager.instance.FUN_1DE78(DAT_18);
                    DAT_18 = 0;
                }

                iVar7 = GameManager.instance.FUN_1DD9C();
                GameManager.instance.FUN_1E628(iVar7, vData.sndList, 2, vTransform.position);
                DAT_A4 = 60;
                DAT_B4 = 11;
                DAT_BC = 0;
                DAT_B8 = -DAT_B8;
                ((Vehicle)DAT_80).FUN_41FEC();
                DAT_80 = null;
                break;
            case 4:
                iVar10 = DAT_A4 - 1;
                DAT_A4 = iVar10;

                if (0 < iVar10)
                    return 0;

                tags = 5;
                sVar2 = (sbyte)GameManager.instance.FUN_1DD9C();
                GameManager.instance.FUN_1E628(sVar2, vData.sndList, 1, vTransform.position);
                goto case 5;
            case 5:
                oVar12 = child2;
                oVar10 = oVar12.child2;
                unaff_s6 = oVar10.child2;

                if (oVar10 == null || unaff_s6 == null)
                    return 0; //FUN_15D9C

                if (oVar10.vr.x < 5)
                {
                    oVar10.vr.x += DAT_B4;
                    unaff_s6.vr.x -= DAT_B4;
                    unaff_s6.ApplyTransformation();
                    oVar10.ApplyTransformation();
                }

                iVar10 = DAT_B8;

                if (iVar10 < 1)
                {
                    if (iVar10 < DAT_BC)
                    {
                        iVar10 = DAT_BC - 1;
                        DAT_BC = iVar10;
                    }
                }
                else
                {
                    if (DAT_BC < iVar10)
                    {
                        iVar10 = DAT_BC + 1;
                        DAT_BC = iVar10;
                    }
                }

                oVar12.vr.y += DAT_BC;
                oVar12.ApplyTransformation();
                iVar6 = DAT_A8 - oVar12.vr.y;

                if (DAT_BC < 0)
                    iVar6 = -iVar6;

                if ((iVar6 & 0xfff) < 11)
                {
                    tags = 0;
                    FUN_30BA8();

                    if (DAT_18 == 0)
                        return 0;

                    GameManager.instance.FUN_1DE78(DAT_18);
                    DAT_18 = 0;
                }

                break;
        }

        return 0;
    }
    
    //FUN_14C0 (HARBOR.DLL)
    private static void FUN_14C0(VigChild param1, _CHILD_TYPE param2)
    {
        if (param1 != null)
        {
            do
            {
                param1.state = param2;

                if (param1.child2 != null)
                    FUN_14C0((VigChild)param1.child2, param2);

                param1 = (VigChild)param1.child;
            } while (param1 != null);
        }
    }

    //FUN_1518 (HARBOR.DLL)
    private static void FUN_1518(VigObject param1, int param2)
    {
        if (param1 != null)
        {
            do
            {
                param1.type = (byte)param2;

                if (param1.child2 != null)
                    FUN_1518(param1.child2, param2);

                param1 = param1.child;
            } while (param1 != null);
        }
    }
}
