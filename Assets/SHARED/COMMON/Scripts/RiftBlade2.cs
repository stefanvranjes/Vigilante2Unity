using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiftBlade2 : VigObject
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
        Vehicle vVar3;
        int iVar8;
        VigObject oVar9;
        Vector3Int uStack176;
        Vector3Int uStack192;

        oVar9 = DAT_80;

        if (hit.self.type == 3)
            return 0;

        if (hit.self.type == 8)
            return 0;

        DAT_A8 = DAT_A4;

        if (hit.self == DAT_B8)
            return 0;

        uStack176 = new Vector3Int(0, -0x1000, 0);
        DAT_B8 = hit.self;
        DAT_A8 = DAT_A4;
        GameManager.instance.FUN_2F798(this, hit);
        uStack192 = Utilities.FUN_24148(vTransform, hit.position);
        LevelManager.instance.FUN_4EAE8(uStack192, hit.normal1, 148);
        UIManager.instance.FUN_4E414(uStack192, new Color32(0x80, 0x80, 0x80, 8));
        iVar8 = GameManager.instance.FUN_1DD9C();
        GameManager.instance.FUN_1E580(iVar8, oVar9.vData.sndList, 3, uStack192);

        if (hit.self.type != 2)
            return 0;

        vVar3 = (Vehicle)hit.self;

        if (DAT_BC == vVar3)
            return 0;

        DAT_BC = vVar3;
        vVar3.FUN_3A064(-500, uStack192, true);

        if (1 < DAT_19 - 1U)
            return 0;

        if (vVar3 != ((Vehicle)oVar9).target)
            return 0;

        DAT_19 = 4;
        iVar8 = 6;
        GameManager.instance.FUN_30CB0(this, iVar8);
        return 0;
    }

    public VigTransform DAT_84_2; //0x84
    public int DAT_A4; //0xA4
    public int DAT_A8; //0xA8
    public int DAT_AC; //0xAC
    public int DAT_B0; //0xB0
    public int DAT_B4; //0xB4
    public VigObject DAT_B8; //0xB8
    public Vehicle DAT_BC; //0xBC

    //FUN_80 (TSUNAMI.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        byte bVar1;
        Ballistic puVar2;
        int iVar3;
        TileData tVar3;
        VigObject oVar3;
        int iVar4;
        int iVar6;
        int iVar7;
        int iVar8;
        int iVar9;
        VigObject oVar9;
        Vehicle vVar9;
        Vector3Int uStack192;
        Vector3Int uStack176;
        Vector3Int iStack160;
        Vector3Int iStack144;
        Vector3Int iStack128;
        Matrix3x3 uStack112;
        Vector3Int iStack80;
        Vector3Int sStack64;
        Vector3Int uStack56;
        Vector3Int uStack48;
        Vector3Int uStack40;

        oVar9 = DAT_80;

        if (arg1 != 2)
        {
            if (arg1 < 3)
            {
                if (arg1 != 0)
                    return 0;

                bVar1 = DAT_19;
                DAT_A4++;

                if (4 < bVar1)
                {
                    if (bVar1 != 5)
                        return 0;

                    iStack144 = new Vector3Int(DAT_AC, DAT_AC, DAT_AC);
                    iStack128 = new Vector3Int(DAT_B0, DAT_B0, DAT_B0);
                    Utilities.FUN_2449C(DAT_84_2.rotation, iStack144, ref vTransform.rotation);
                    Utilities.FUN_2449C(DAT_84_2.rotation, iStack128, ref oVar9.vTransform.rotation);
                    iVar9 = DAT_AC - 245;
                    iVar3 = DAT_B0 + 245;
                    DAT_AC = iVar9;
                    DAT_B0 = iVar3;
                    return 0;
                }

                if (bVar1 == 0)
                {
                    uStack192 = new Vector3Int(DAT_AC, DAT_AC, DAT_AC);
                    uStack176 = new Vector3Int(DAT_B0, DAT_B0, DAT_B0);
                    Utilities.FUN_2449C(DAT_84_2.rotation, uStack192, ref vTransform.rotation);
                    Utilities.FUN_2449C(DAT_84_2.rotation, uStack176, ref oVar9.vTransform.rotation);
                    iVar9 = DAT_AC + 122;
                    iVar3 = DAT_B0 - 122;
                    DAT_AC = iVar9;
                    DAT_B0 = iVar3;
                }

                iStack144 = new Vector3Int(vTransform.rotation.V02, 0, vTransform.rotation.V22);

                if (DAT_19 == 1)
                    iVar3 = DAT_B4 + 1017;
                else
                {
                    if (DAT_19 != 3) goto LAB_25C;

                    iVar3 = DAT_B4 - 2034;
                }

                DAT_B4 = iVar3;
                LAB_25C:
                iStack160 = new Vector3Int();
                iStack160.x = iStack144.x * DAT_B4 >> 12;
                iStack160.z = iStack144.z * DAT_B4 >> 12;
                iStack160.y = 0x10000;
                iStack128 = new Vector3Int();
                iStack128.x = vTransform.position.x + iStack160.x;
                iStack128.y = vTransform.position.y + 0x10000;
                iStack128.z = vTransform.position.z + iStack160.z;
                tVar3 = GameManager.instance.terrain.GetTileByPosition((uint)iStack128.x, (uint)iStack128.z);

                if (tVar3.DAT_10[3] != 7)
                    vTransform.position = iStack128;

                iVar3 = FUN_2CFBC(vTransform.position);

                if (iVar3 - 0x5000 < vTransform.position.y)
                    vTransform.position.y = iVar3 - 0x5000;

                if (GameManager.instance.DAT_DB0 != 0)
                {
                    if (iVar3 > GameManager.instance.DAT_DB0)
                        vTransform.position.y = GameManager.instance.DAT_DB0;
                }

                oVar9.vTransform.position = Utilities.FUN_24148(vTransform, screen);
                vVar9 = (Vehicle)oVar9;
                oVar3 = vVar9.target;

                if (oVar3 != null)
                {
                    iStack80 = new Vector3Int();
                    iStack80.x = oVar3.screen.x - vTransform.position.x;
                    iStack80.y = oVar3.screen.y - vTransform.position.y;
                    iStack80.z = oVar3.screen.z - vTransform.position.z;
                    Utilities.FUN_29FC8(iStack80, out uStack48);
                    sStack64 = new Vector3Int(uStack48.z, 0, -uStack48.x);
                    iVar3 = uStack48.y * sStack64.z;

                    if (iVar3 < 0)
                        iVar3 += 4095;

                    iVar6 = uStack48.z;
                    iVar4 = iVar6 * iVar6 - uStack48.x * sStack64.z;
                    uStack56 = new Vector3Int();
                    uStack56.x = iVar3 >> 12;

                    if (iVar4 < 0)
                        iVar4 += 4095;

                    iVar6 = -uStack48.y * iVar6;
                    uStack56.y = iVar4 >> 12;

                    if (iVar6 < 0)
                        iVar6 += 4095;

                    uStack56.z = iVar6 >> 12;
                    uStack112 = new Matrix3x3();
                    uStack112.V00 = (short)sStack64.x;
                    uStack112.V01 = (short)uStack56.x;
                    uStack112.V02 = (short)uStack48.x;
                    uStack112.V10 = (short)sStack64.y;
                    uStack112.V11 = (short)uStack56.y;
                    uStack112.V12 = (short)uStack48.y;
                    uStack112.V20 = (short)sStack64.z;
                    uStack112.V21 = (short)uStack56.z;
                    uStack112.V22 = (short)uStack48.z;
                    uStack112 = Utilities.MatrixNormal(uStack112);
                    Utilities.FUN_248C4(vTransform.rotation, uStack112, out uStack112);
                    iVar3 = 64;

                    if (DAT_19 == 2)
                        iVar3 = 256;

                    iVar6 = uStack112.V02;
                    iVar4 = -iVar3;

                    if (-iVar3 <= iVar6)
                    {
                        iVar4 = iVar3;

                        if (iVar6 <= iVar3)
                            iVar4 = iVar6;
                    }

                    iVar6 = -uStack112.V01;
                    iVar7 = -iVar3;

                    if (-iVar3 <= iVar6)
                    {
                        iVar7 = iVar3;

                        if (iVar6 <= iVar3)
                            iVar7 = iVar6;
                    }

                    FUN_24700(0, (short)iVar4, (short)iVar7);
                    vTransform.rotation = Utilities.MatrixNormal(vTransform.rotation);
                }

                uStack40 = new Vector3Int(DAT_B0, DAT_B0, DAT_B0);
                Utilities.FUN_2449C(vTransform.rotation, uStack40, ref oVar9.vTransform.rotation);

                if ((DAT_A4 & 3) != 0)
                    return 0;

                puVar2 = vData.ini.FUN_2C17C(1, typeof(Ballistic), 8) as Ballistic;
                puVar2.type = 7;
                puVar2.flags = 0x24;
                puVar2.screen = vTransform.position;
                puVar2.FUN_3066C();
                puVar2.vTransform = vTransform;
                return 0;
            }

            return 0;
        }

        switch (DAT_19)
        {
            case 0:
                DAT_19++;
                oVar9.flags |= 2;
                GameManager.instance.FUN_30CB0(this, 50);
                flags &= 0xffffffdf;
                return 0;
            case 1:
                iVar8 = 30;
                DAT_19++;
                break;
            case 2:
                iVar8 = 15;
                DAT_19++;
                break;
            case 3:
            case 4:
                if (DAT_A4 - DAT_A8 < 5)
                {
                    iVar8 = 5;
                    DAT_19 = 4;
                    DAT_B4 = 4577;
                }
                else
                {
                    iVar8 = 15;
                    DAT_19 = 5;
                    DAT_AC = 0x1000;
                    DAT_B0 = 0x199;
                    DAT_84_2 = vTransform;
                    oVar9.flags &= 0xfffffffd;
                }

                break;
            case 5:
                oVar9.FUN_30B78();
                oVar9.flags &= 0xf9ffffdf;
                GameManager.instance.FUN_309A0(this);
                GameManager.instance.DAT_1084--;
                goto default;
            default:
                return 0;
        }

        LAB_994:
        GameManager.instance.FUN_30CB0(this, iVar8);
        return 0;
    }
}
