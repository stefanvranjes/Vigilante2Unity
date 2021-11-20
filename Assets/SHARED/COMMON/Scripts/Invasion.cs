using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invasion : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_66C (CARAVLLE.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        int iVar5;

        if (arg1 == 0)
        {
            iVar5 = FUN_42330(arg2);

            if (iVar5 < 1)
                return 0;

            child2.vr.y += 34;

            if (arg2 != 0)
            {
                child2.ApplyRotationMatrix();
                return 0;
            }
        }

        return 0;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        short sVar1;
        int iVar2;
        ConfigContainer ccVar2;
        Invasion2 puVar3;
        int iVar4;
        int iVar5;
        VigObject oVar5;
        int iVar6;
        short sVar7;
        ushort uVar8;
        Saucer puVar9;
        VigTransform local_50;
        Vector3Int local_30;
        Vector3Int local_20;

        if (arg1 == 1)
        {
            FUN_2C344(vData, 1, 0);
            Utilities.ParentChildren(this, this);
            maxHalfHealth = 3;
            flags |= 0x4000;
        }
        else
        {
            if (arg1 != 0)
            {
                if (arg1 == 12)
                {
                    oVar5 = ((Vehicle)arg2).target;

                    if (oVar5 == null)
                        oVar5 = arg2;

                    ccVar2 = child2.FUN_2C5F4(0x8000);
                    uVar8 = 3;

                    if (oVar5.id < 0)
                        uVar8 = 5;

                    iVar6 = 0;
                    sVar7 = 1;
                    local_50 = GameManager.instance.FUN_2CEAC(child2, ccVar2);

                    do
                    {
                        Dictionary<int, Type> dict = new Dictionary<int, Type>();
                        dict.Add(97, typeof(VigChild));
                        puVar3 = vData.ini.FUN_2C17C(3, typeof(Invasion2), 8, dict) as Invasion2;
                        Utilities.ParentChildren(puVar3, puVar3);
                        puVar3.type = 4;
                        puVar3.id = (short)iVar6;
                        puVar3.flags = 0xa2;
                        puVar3.vTransform = local_50;
                        puVar3.physics2.M3 = sVar7;
                        iVar4 = arg2.physics1.X;

                        if (iVar4 < 0)
                            iVar4 += 127;

                        puVar3.physics1.Z = (iVar4 >> 7) + local_50.rotation.V02;
                        iVar4 = arg2.physics1.Y;

                        if (iVar4 < 0)
                            iVar4 += 127;

                        puVar3.physics1.W = (iVar4 >> 7) + local_50.rotation.V12;
                        iVar4 = arg2.physics1.Z;

                        if (iVar4 < 0)
                            iVar4 += 127;

                        sVar7 += 8;
                        iVar6++;
                        puVar3.physics2.X = (iVar4 >> 7) + local_50.rotation.V22;
                        puVar3.FUN_2D1DC();
                        puVar3.FUN_305FC();
                    } while (iVar6 < 4);

                    local_30 = new Vector3Int();
                    local_30.x = oVar5.screen.x - arg2.vTransform.position.x;
                    local_30.y = oVar5.screen.y - arg2.vTransform.position.y;
                    local_30.z = oVar5.screen.z - arg2.vTransform.position.z;
                    iVar6 = Utilities.FUN_29FC8(local_30, out local_20);

                    if (iVar6 < 0x140000)
                        local_30 = oVar5.screen;
                    else
                    {
                        local_30.x = arg2.vTransform.position.x + local_20.x * 320;
                        local_30.y = arg2.vTransform.position.y + local_20.y * 320;
                        local_30.z = arg2.vTransform.position.z + local_20.z * 320;
                    }

                    iVar6 = 0;
                    sVar7 = 360;

                    do
                    {
                        puVar9 = vData.ini.FUN_2C17C(2, typeof(Saucer), 8) as Saucer;
                        puVar9.type = 8;
                        puVar9.flags = 0x60000080;
                        puVar9.screen.x = local_30.x;
                        puVar9.screen.y = local_30.y - 0xc0000;
                        puVar9.maxHalfHealth = uVar8;
                        puVar9.screen.z = local_30.z;
                        iVar4 = ((iVar6 << 12) / 3 & 0xfff) * 2;
                        sVar1 = GameManager.DAT_65C90[iVar4 + 1];
                        puVar9.physics1.W = 0;
                        puVar9.physics1.Z = sVar1;
                        sVar1 = GameManager.DAT_65C90[iVar4];
                        iVar6++;
                        puVar9.physics2.M3 = sVar7;
                        sVar7 += 60;
                        puVar9.DAT_80 = arg2;
                        puVar9.DAT_84 = oVar5;
                        GameManager.instance.DAT_1084++;
                        puVar9.physics2.X = sVar1;
                        puVar9.FUN_3066C();
                    } while (iVar6 < 3);

                    iVar2 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E188(iVar2, vData.sndList, 2);
                    iVar2 = GameManager.instance.FUN_1DD9C();
                    GameManager.instance.FUN_1E580(iVar2, vData.sndList, 4, local_50.position);
                    sVar7 = (short)(maxHalfHealth - 1);
                    maxHalfHealth = (ushort)sVar7;

                    if (sVar7 == 0)
                        FUN_3A368();

                    if (-1 < arg2.id)
                        return 1200;

                    return 840;
                }

                if (arg1 != 13)
                    return 0;

                if (GameManager.instance.DAT_1084 != 0)
                    return 0;

                iVar5 = Utilities.FUN_29F6C(arg2.screen, ((Vehicle)arg2).target.screen);
                return 0x64000 < iVar5 ? 1U : 0;
            }

            iVar5 = FUN_42330(arg2);

            if (iVar5 < 1)
                return 0;

            child2.vr.y += 34;

            if (arg2 != null)
            {
                child2.ApplyRotationMatrix();
                return 0;
            }
        }

        return 0;
    }
}
