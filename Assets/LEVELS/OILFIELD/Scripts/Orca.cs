using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orca : VigObject
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
        sbyte sVar2;
        int iVar4;
        Vehicle vVar13;

        if (hit.self.type != 2)
            return 0;

        vVar13 = (Vehicle)hit.self;
        iVar4 = (int)FUN_33798(hit, 7629);

        if (iVar4 != 0)
            vVar13.FUN_3A064(-13, vTransform.position, true); //originally -3

        if (DAT_18 == 0)
        {
            sVar2 = (sbyte)GameManager.instance.FUN_1DD9C();
            DAT_18 = sVar2;
            GameManager.instance.FUN_1E580(sVar2, GameManager.instance.DAT_C2C, 38, vTransform.position);
            GameManager.instance.FUN_30CB0(this, 60);
            return 0;
        }

        return 0;
    }

    public JUNC_DB DAT_84_2; //0x84

    //FUN_2428 (OILFIELD.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        bool bVar1;
        sbyte sVar2;
        uint uVar3;
        RSEG_DB piVar5;
        JUNC_DB puVar6;
        int iVar7;
        JUNC_DB jVar7;
        int iVar8;
        int iVar9;
        int iVar4;
        int iVar10;
        uint uVar12;
        int iVar13;
        Vehicle vVar14;

        switch (arg1)
        {
            case 0:
                if (tags != 3)
                {
                    iVar8 = physics2.X - vTransform.position.z;
                    iVar13 = vTransform.position.y;
                    iVar4 = physics1.Z - vTransform.position.x;

                    if (tags != 0)
                        iVar13 -= 0x32000;

                    iVar13 = GameManager.instance.DAT_DB0 - iVar13;

                    if (DAT_18 == 0 && tags == 0 && 0 < iVar13)
                    {
                        sVar2 = (sbyte)GameManager.instance.FUN_1DD9C();
                        DAT_18 = sVar2;
                        GameManager.instance.FUN_1E580(sVar2, GameManager.instance.DAT_C2C, 38, vTransform.position);
                        GameManager.instance.FUN_30CB0(this, 60);
                    }

                    iVar7 = iVar8;

                    if (iVar8 < 0)
                        iVar7 = -iVar8;

                    iVar9 = iVar4;

                    if (iVar4 < 0)
                        iVar9 = -iVar4;

                    if (iVar7 < iVar9)
                        iVar7 = iVar9;

                    if (iVar7 < 0x10000)
                    {
                        uVar12 = (uint)GameManager.FUN_2AC5C();
                        jVar7 = DAT_84_2;
                        piVar5 = jVar7.DAT_1C[(int)(uVar12 & 1)];
                        bVar1 = jVar7 == piVar5.DAT_00[0];
                        DAT_19 = (byte)(bVar1 ? 1 : 0);
                        puVar6 = piVar5.DAT_00[DAT_19];
                        DAT_84_2 = puVar6;
                        physics1.Z = puVar6.DAT_00.x;
                        physics1.W = puVar6.DAT_00.y;
                        physics2.X = puVar6.DAT_00.z;

                        if ((uVar12 & 3) == 0)
                            tags = (sbyte)(1 - tags);
                    }

                    iVar4 = Utilities.Ratan2(iVar4, iVar8);
                    iVar8 = (iVar4 - (ushort)vr.y) * 0x100000 >> 20;
                    iVar4 = -22;

                    if (-23 < iVar8)
                    {
                        iVar4 = 22;

                        if (iVar8 < 23)
                            iVar4 = iVar8;
                    }

                    vr.y += (short)iVar4;
                    iVar4 = (-iVar13 / 0x5000) * 56;
                    iVar13 = -341;

                    if (-342 < iVar4)
                    {
                        iVar13 = 341;

                        if (iVar4 < 342)
                            iVar13 = iVar4;
                    }

                    iVar13 -= vr.x;
                    iVar4 = -5;

                    if (-6 < iVar13)
                    {
                        iVar4 = 5;

                        if (iVar13 < 6)
                            iVar4 = iVar13;
                    }

                    vr.x += (short)iVar4;
                    ApplyRotationMatrix();
                    iVar13 = vTransform.rotation.V02 * 7629;

                    if (iVar13 < 0)
                        iVar13 += 4095;

                    iVar4 = vTransform.rotation.V12 * 7629;
                    vTransform.position.x += iVar13 >> 12;

                    if (iVar4 < 0)
                        iVar4 += 4095;

                    iVar13 = vTransform.rotation.V22 * 7629;
                    vTransform.position.y += iVar4 >> 12;

                    if (iVar13 < 0)
                        iVar13 += 4095;

                    vTransform.position.z += iVar13 >> 12;
                    return 0;
                }

                uVar12 = maxHalfHealth;
                maxHalfHealth--;

                if (uVar12 == 0)
                {
                    uVar3 = flags;
                    tags = 0;
                    flags = uVar3 & 0xffffffdf;
                }
                else
                {
                    if (uVar12 == 0x10)
                    {
                        iVar10 = GameManager.instance.FUN_1DD9C();
                        GameManager.instance.FUN_1E580(iVar10, GameManager.instance.DAT_C2C, 38, vTransform.position);
                        uVar3 = flags;
                        flags = uVar3 & 0xffffffdf;
                    }
                }

                iVar13 = (int)uVar12 - 16;

                if (iVar13 < 0)
                    iVar13 = -iVar13;

                iVar4 = (16 - iVar13) * 227;

                if (iVar4 < 0)
                    iVar4 += 15;

                vr.x = iVar4 >> 4;
                ApplyRotationMatrix();
                iVar4 = vTransform.rotation.V02 * 3814;

                if (iVar4 < 0)
                    iVar4 += 4095;

                iVar8 = vTransform.rotation.V22 * 3814;
                vTransform.position.x += iVar4 >> 12;

                if (iVar8 < 0)
                    iVar8 += 4095;

                vTransform.position.z += iVar8 >> 12;
                vTransform.position.y = GameManager.instance.DAT_DB0 + (16 - iVar13) * -0x780;
                break;
            case 1:
                piVar5 = LevelManager.instance.FUN_518DC(screen, 16);
                puVar6 = piVar5.DAT_00[0];
                DAT_84_2 = puVar6;
                physics1.Z = puVar6.DAT_00.x;
                physics1.W = puVar6.DAT_00.y;
                physics2.X = puVar6.DAT_00.z;
                flags |= 0x10184;
                break;
            case 2:
                DAT_18 = 0;
                break;
        }

        return 0;
    }
}
