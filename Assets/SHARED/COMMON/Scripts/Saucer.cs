using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saucer : VigObject
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
        int iVar3;
        int iVar4;
        int iVar8;
        VigObject oVar8;
        Vehicle vVar8;
        Vector3Int iStack24;

        oVar8 = hit.self;

        if (oVar8.type != 3)
        {
            GameManager.instance.FUN_2F798(this, hit);
            iVar3 = physics1.Z * hit.normal1.x +
                    physics1.W * hit.normal1.y +
                    physics2.X * hit.normal1.z;

            if (iVar3 < 0)
                iVar3 += 0x7ff;

            iVar3 >>= 11;

            if (-1 < iVar3)
                return 0;

            if (oVar8.type == 2)
            {
                vVar8 = (Vehicle)oVar8;
                iStack24 = new Vector3Int();
                iStack24.x = physics1.Z << 3;
                iStack24.y = physics1.W << 3;
                iStack24.z = physics2.X << 3;
                vVar8.FUN_2B370(iStack24, screen);

                if ((flags & 0x8000) == 0)
                {
                    iVar4 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E628(iVar4, GameManager.instance.DAT_C2C, 7, screen);
                    LevelManager.instance.FUN_4DE54(screen, 142);
                    UIManager.instance.FUN_4E414(screen, new Color32(0x40, 0x40, 0x40, 8));

                    if (vVar8.id < 0)
                        GameManager.instance.FUN_15B00(~vVar8.id, 192, 0, 64);
                }

                flags |= 0x1008000;
            }

            iVar8 = iVar3 * hit.normal1.x;

            if (iVar8 < 0)
                iVar8 += 4095;

            physics1.Z -= iVar8 >> 12;
            iVar8 = iVar3 * hit.normal1.y;

            if (iVar8 < 0)
                iVar8 += 4095;

            physics1.W -= iVar8 >> 12;
            iVar3 = iVar3 * hit.normal1.z;

            if (iVar3 < 0)
                iVar3 += 4095;

            physics2.X -= iVar3 >> 12;
        }

        return 0;
    }

    //FUN_60 (CARAVLLE.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        sbyte sVar1;
        short sVar2;
        int iVar3;
        uint uVar4;
        int iVar5;
        uint uVar6;
        int iVar7;
        int iVar8;
        VigObject oVar9;
        Vector3Int sStack32;

        switch (arg1)
        {
            case 0:
                sVar2 = (short)(physics2.M3 - 1);
                physics2.M3 = sVar2;

                if (sVar2 < 0)
                {
                    if (sVar2 < -120)
                    {
                        GameManager.instance.FUN_309A0(this);
                        GameManager.instance.DAT_1084--;
                        return 0xffffffff;
                    }

                    sStack32 = new Vector3Int(0, -0x1000, 0);
                }
                else
                    Utilities.FUN_2A168(out sStack32, screen, DAT_84.screen);

                iVar8 = sStack32.x;

                if (iVar8 < 0)
                    iVar8 += 15;

                iVar3 = physics1.Z;
                iVar8 = iVar3 + (iVar8 >> 4);

                if (iVar3 < 0)
                    iVar3 += 127;

                iVar8 -= iVar3 >> 7;
                iVar3 = -15258;

                if (-15259 < iVar8)
                {
                    iVar3 = 18310;

                    if (iVar8 < 18311)
                        iVar3 = iVar8;
                }

                physics1.Z = iVar3;
                iVar8 = sStack32.y;

                if (iVar8 < 0)
                    iVar8 += 15;

                iVar3 = physics1.W;
                iVar8 = iVar3 + (iVar8 >> 4);

                if (iVar3 < 0)
                    iVar3 += 127;

                iVar7 = FUN_2CFBC(screen);
                iVar7 -= screen.y + 0x2800;

                if (iVar7 < 0)
                    iVar7 += 15;

                iVar5 = 0;

                if (iVar7 >> 4 < 0)
                    iVar5 = iVar7 >> 4;

                iVar5 = (iVar8 - (iVar3 >> 7)) + iVar5;
                iVar8 = -3051;

                if (-3052 < iVar5)
                {
                    iVar8 = 18310;

                    if (iVar5 < 18311)
                        iVar8 = iVar5;
                }

                physics1.W = iVar8;
                iVar8 = sStack32.z;

                if (iVar8 < 0)
                    iVar8 += 15;

                iVar3 = physics2.X;
                iVar8 = iVar3 + (iVar8 >> 4);

                if (iVar3 < 0)
                    iVar3 += 127;

                iVar8 -= iVar3 >> 7;
                iVar3 = -15258;

                if (-15259 < iVar8)
                {
                    iVar3 = 18310;

                    if (iVar8 < 18311)
                        iVar3 = iVar8;
                }

                physics2.X = iVar3;
                screen.x += physics1.Z;
                screen.y += physics1.W;
                screen.z += physics2.X;
                vr.y += 136;

                if (arg2 != 0)
                {
                    ApplyTransformation();
                    uVar4 = GameManager.instance.FUN_1E7A8(vTransform.position);
                    GameManager.instance.FUN_1E2C8(DAT_18, uVar4);
                    uVar6 = flags & 0xfeff7fff;

                    if ((flags & 0x1000000) != 0)
                        uVar6 |= 0x8000;

                    flags = uVar6;

                    if ((uVar6 & 0x20000000) != 0)
                    {
                        if (DAT_80.maxHalfHealth != 0)
                            return 0;

                        flags = uVar6 & 0xdfffffff;
                    }
                }

                break;
            case 1:
                sVar1 = (sbyte)GameManager.instance.FUN_1DD9C();
                DAT_18 = sVar1;
                GameManager.instance.FUN_1E098(sVar1, vData.sndList, 3, 0, true);
                iVar3 = (int)GameManager.FUN_2AC5C();
                GameManager.instance.FUN_30CB0(this, ((iVar3 >> 10) * 2) + 200);
                return 0;
            case 2:
                ShootLaser();
                iVar3 = (int)GameManager.FUN_2AC5C();
                GameManager.instance.FUN_30CB0(this, ((iVar3 >> 10) * 4));
                break;
            case 4:
                GameManager.instance.FUN_1DE78(DAT_18);
                break;
        }

        return 0;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        switch (arg1)
        {
            case 10:
                if (DAT_84 != arg2)
                    return 0;

                DAT_84 = DAT_80;
                break;
        }

        return 0;
    }

    private Laser2 ShootLaser()
    {
        short sVar1;
        int iVar2;
        ConfigContainer ccVar2;
        Laser2 ppcVar3;
        Ballistic puVar4;
        int iVar5;
        int iVar6;
        VigObject oVar6;
        VigObject oVar7;
        Vector3Int local_28;
        Vector3Int local_18;

        //oVar7 = DAT_8C.child2;
        //ccVar2 = oVar7.FUN_2C5F4(0x8000);
        ppcVar3 = vData.ini.FUN_2C17C(98, typeof(Laser2), 8) as Laser2;
        sVar1 = id;
        ppcVar3.type = 8;
        ppcVar3.id = sVar1;
        //ppcVar3.vTransform = GameManager.instance.FUN_2CEAC(oVar7, ccVar2);
        ppcVar3.vTransform = vTransform;
        ppcVar3.screen = ppcVar3.vTransform.position;
        oVar6 = DAT_84;
        local_28 = new Vector3Int();
        local_28.x = oVar6.screen.x - ppcVar3.screen.x;
        local_28.y = oVar6.screen.y - ppcVar3.screen.y;
        local_28.z = oVar6.screen.z - ppcVar3.screen.z;
        Utilities.FUN_29FC8(local_28, out local_18);
        iVar6 = -local_18.x;

        if (0 < local_18.x)
            iVar6 += 3;

        iVar5 = ppcVar3.vTransform.rotation.V02;

        if (iVar5 < 0)
            iVar5 += 3;

        local_18.x = local_18.x + (iVar6 >> 2) + (iVar5 >> 2);
        iVar6 = -local_18.y;

        if (0 < local_18.y)
            iVar6 += 3;

        iVar5 = ppcVar3.vTransform.rotation.V12;

        if (iVar5 < 0)
            iVar5 += 3;

        local_18.y = local_18.y + (iVar6 >> 2) + (iVar5 >> 2);
        iVar6 = -local_18.z;

        if (0 < local_18.z)
            iVar6 += 3;

        iVar5 = ppcVar3.vTransform.rotation.V22;

        if (iVar5 < 0)
            iVar5 += 3;

        local_18.z = local_18.z + (iVar6 >> 2) + (iVar5 >> 2);
        local_18 = Utilities.VectorNormal(local_18);
        ppcVar3.vTransform.rotation = Utilities.FUN_2A724(local_18);
        ppcVar3.flags = 0x84;
        ppcVar3.maxHalfHealth = 50;
        ppcVar3.physics2.M3 = 2;
        ppcVar3.physics2.M2 = 8;
        ppcVar3.FUN_305FC();
        iVar2 = GameManager.instance.FUN_1DD9C();
        GameManager.instance.FUN_1E5D4(iVar2, vData.sndList, 6, vTransform.position);
        iVar5 = (int)GameManager.FUN_2AC5C();
        GameManager.instance.FUN_1E30C(iVar2, ((iVar5 << 10) >> 15) + 0xe00);
        return ppcVar3;
    }
}
