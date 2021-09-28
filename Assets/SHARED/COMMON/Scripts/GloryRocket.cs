using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloryRocket : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //0x30-3C (STNTBIKE.DLL)
    private static int DAT_30, DAT_38, DAT_3C = 0;
    private static int DAT_34 = 0x10000;
    //0x1510 (STNTBIKE.DLL)
    public static int[] DAT_1510 = new int[] { 171, 170, 169, 157, 166, 154, 162, 160, 164 };

    public override uint OnCollision(HitDetection hit)
    {
        GameManager.instance.FUN_2F798(this, hit);

        if (hit.self.type == 3)
            return 0;

        FUN_730();
        GameManager.instance.FUN_309A0(this);
        return 0xffffffff;
    }

    //FUN_960 (STNTBIKE.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        short sVar1;
        long lVar2;
        int iVar3;
        VigObject oVar3;
        int iVar4;
        Vehicle vVar4;
        VigObject oVar5;
        VigTransform puVar6;
        VigObject oVar7;
        ConfigContainer ccVar7;
        int iVar7;
        uint uVar8;
        int iVar9;
        uint uVar9;
        VigObject oVar9;
        uint uVar10;
        int iVar13;
        int iVar14;
        Vector3Int local_50;
        int local_44;
        Vector3Int local_38;
        Vector3Int auStack64;
        VigTransform auStack40;

        if (arg1 == 2)
        {
            FUN_730();
            GameManager.instance.FUN_309A0(this);
            uVar9 = 0xffffffff;
        }
        else
        {
            if (arg1 < 3)
            {
                if (arg1 != 0)
                    return 0;

                sVar1 = physics2.M3;

                if (physics2.M3 != 0)
                {
                    physics2.M3 = (short)(sVar1 - 1);

                    if (sVar1 != 1)
                        return 0;

                    oVar3 = Utilities.FUN_2CD78(this);
                    vVar4 = Utilities.FUN_2CD78(oVar3) as Vehicle;
                    local_50 = new Vector3Int(DAT_30, DAT_34, DAT_38);
                    local_44 = DAT_3C;

                    if (vVar4 != null)
                    {
                        oVar5 = LevelManager.instance.xobfList[19].ini.FUN_2C17C((ushort)DAT_1510[DAT_19 + 3], typeof(VigObject), 8);
                        puVar6 = GameManager.instance.FUN_2CDF4(this);
                        vTransform = puVar6;
                        screen = vTransform.position;
                        id = vVar4.id;
                        oVar7 = vVar4.target;

                        if (vVar4.target == null)
                            oVar7 = vVar4;

                        DAT_84 = oVar7;
                        iVar7 = Utilities.FUN_29F6C(vTransform.position, oVar7.vTransform.position);

                        if (iVar7 < 0)
                            iVar7 += 255;

                        iVar7 >>= 8;
                        iVar14 = 0x1000;

                        if (4095 < iVar7)
                        {
                            iVar14 = 0x3000;

                            if (iVar7 < 0x3001)
                                iVar14 = iVar7;
                        }

                        iVar7 = vVar4.physics1.X;

                        if (iVar7 < 0)
                            iVar7 += 127;

                        iVar13 = vTransform.rotation.V02 * iVar14;

                        if (iVar13 < 0)
                            iVar13 += 4095;

                        physics1.Z = (iVar7 >> 7) + (iVar13 >> 12);
                        iVar7 = vVar4.physics1.Y;

                        if (iVar7 < 0)
                            iVar7 += 127;

                        iVar13 = vTransform.rotation.V12 * iVar14;

                        if (iVar13 < 0)
                            iVar13 += 4095;

                        physics1.W = (iVar7 >> 7) + (iVar13 >> 12);
                        iVar7 = vVar4.physics1.Z;

                        if (iVar7 < 0)
                            iVar7 += 127;

                        iVar14 = vTransform.rotation.V22 * iVar14;

                        if (iVar14 < 0)
                            iVar14 += 4095;

                        physics2.X = (iVar7 >> 7) + (iVar14 >> 12);
                        FUN_2CCBC();
                        transform.parent = null;
                        FUN_2D1DC();
                        oVar5.vTransform = GameManager.FUN_2A39C();
                        ccVar7 = FUN_2C5F4(0x8000);
                        oVar5.vTransform.position = ccVar7.v3_1;
                        Utilities.FUN_2CC9C(this, oVar5);
                        Utilities.ParentChildren(this, this);
                        FUN_30BF0();
                        GameManager.instance.FUN_30080(GameManager.instance.worldObjs, this);

                        if (oVar3.maxHalfHealth == 0 && DAT_19 == 2)
                            oVar3.FUN_3A368();

                        iVar7 = (int)GameManager.FUN_2AC5C();
                        screen.x = (iVar7 * 0x14000 >> 15) - 0xa000;
                        screen.y = 0;
                        iVar7 = (int)GameManager.FUN_2AC5C();
                        screen.z = (iVar7 * 0x14000 >> 15) - 0xa000;
                        iVar9 = GameManager.instance.FUN_1DD9C();
                        GameManager.instance.FUN_1E628(iVar9, oVar3.vData.sndList, 3, vTransform.position);
                        local_50 = Utilities.FUN_24094(oVar3.vTransform.rotation, local_50);
                        vVar4.FUN_2B1FC(local_50, screen);
                        return 0;
                    }

                    oVar9 = FUN_2CCBC();
                    transform.parent = null;
                    GameManager.instance.FUN_308C4(oVar9);
                    return 0xffffffff;
                }

                vTransform.position.x += physics1.Z;
                vTransform.position.y += physics1.W;
                vTransform.position.z += physics2.X;
                oVar3 = DAT_84;
                physics1.W += 42;
                local_38 = new Vector3Int();
                local_38.x = oVar3.screen.x + screen.x;
                local_38.y = oVar3.screen.y + screen.y;
                local_38.z = oVar3.screen.z + screen.z;
                local_50 = new Vector3Int();
                local_50.x = local_38.x - vTransform.position.x;
                local_50.y = local_38.y - vTransform.position.y;
                local_50.z = local_38.z - vTransform.position.z;
                lVar2 = (long)physics1.W * physics1.W;
                uVar8 = (uint)local_50.y * 56;
                uVar10 = (uint)lVar2 + uVar8;
                iVar3 = (int)((ulong)lVar2 >> 32) + ((int)uVar8 >> 31) + (uVar10 < uVar8 ? 1 : 0);

                if (-1 < iVar3)
                {
                    iVar4 = Utilities.FUN_2ABC4(uVar10, iVar3);
                    iVar4 -= physics1.W;
                    iVar3 = iVar4;

                    if (iVar4 < 0)
                        iVar3 = -iVar4;

                    if (0x100 < iVar3)
                    {
                        iVar3 = (local_50.x * 56) / iVar4 - physics1.Z;

                        if (iVar3 < 0)
                            iVar3 += 31;

                        physics1.Z += iVar3 >> 5;
                        iVar3 = (local_50.z * 56) / iVar4 - physics2.X;

                        if (iVar3 < 0)
                            iVar3 += 31;

                        physics2.X += iVar3 >> 5;
                    }
                }

                local_50.x = physics1.Z;
                local_50.y = physics1.W;
                local_50.z = physics2.X;
                Utilities.FUN_29FC8(local_50, out auStack64);
                auStack40 = GameManager.FUN_2A39C();
                iVar3 = (ushort)vr.z + (ushort)vr.x;
                vr.z = iVar3;
                FUN_1370(iVar3 * 0x100000 >> 20, ref auStack40.rotation);
                vTransform.rotation = Utilities.FUN_2A724(auStack64);
                vTransform.rotation = Utilities.FUN_247C4(vTransform.rotation, auStack40.rotation);

                if (-1 < physics1.W)
                {
                    iVar4 = GameManager.instance.terrain.FUN_1B750((uint)vTransform.position.x, (uint)vTransform.position.z);
                    iVar3 = iVar4;

                    if (vTransform.position.z < GameManager.instance.DAT_DA0)
                    {
                        iVar3 = GameManager.instance.DAT_DB0;

                        if (iVar4 < GameManager.instance.DAT_DB0)
                            iVar3 = iVar4;
                    }

                    if (iVar3 < vTransform.position.y + 0xc800)
                    {
                        FUN_730();
                        GameManager.instance.FUN_309A0(this);
                        return 0xffffffff;
                    }
                }

                physics2.M2++;
            }
            else
            {
                if (arg1 != 4)
                    return 0;

                GameManager.instance.DAT_1084--;
            }

            uVar9 = 0;
        }

        return uVar9;
    }

    //FUN_1370 (STNTBIKE.DLL)
    private static void FUN_1370(int param1, ref Matrix3x3 param2)
    {
        short sVar1;
        short sVar2;
        short sVar3;
        int iVar4;
        int iVar5;

        if (param1 < 0)
        {
            iVar5 = GameManager.DAT_65C90[(-param1 & 0xfff) * 2]; //error in the original code? should have a branch?
            iVar4 = -(short)iVar5;
        }
        else
        {
            iVar5 = GameManager.DAT_65C90[(param1 & 0xfff) * 2];
            iVar4 = -(short)iVar5;
        }

        iVar5 >>= 16;
        sVar1 = param2.V00;
        sVar2 = param2.V01;
        sVar3 = param2.V02;
        param2.V00 = (short)(iVar5 * sVar1 - iVar4 * param2.V10 >> 12);
        param2.V01 = (short)(iVar5 * sVar2 - iVar4 * param2.V11 >> 12);
        param2.V02 = (short)(iVar5 * sVar3 - iVar4 * param2.V12 >> 12);
        param2.V10 = (short)(iVar4 * sVar1 + iVar5 * param2.V10 >> 12);
        param2.V11 = (short)(iVar4 * sVar2 + iVar5 * param2.V11 >> 12);
        param2.V12 = (short)(iVar4 * sVar3 + iVar5 * param2.V12 >> 12);
    }

    //FUN_730 (STNTBIKE.DLL)
    private void FUN_730()
    {
        Fireworks puVar1;
        int iVar2;
        int iVar3;
        VigObject oVar3;
        int iVar5;
        int iVar6;
        VigChild puVar7;

        iVar5 = 0;
        iVar6 = 0;

        do
        {
            puVar1 = LevelManager.instance.xobfList[19].ini.FUN_2C17C((ushort)DAT_1510[DAT_19 + 6], typeof(Fireworks), 8, typeof(VigChild)) as Fireworks;
            Utilities.ParentChildren(puVar1, puVar1);
            puVar7 = puVar1.child2 as VigChild;

            while(puVar7 != null)
            {
                puVar7.state = _CHILD_TYPE.Default;
                puVar7 = puVar7.child2 as VigChild;
            }

            oVar3 = DAT_80;
            puVar1.type = 8;
            puVar1.flags = 0x60000002;
            puVar1.DAT_80 = oVar3;
            puVar1.id = id;
            puVar1.DAT_19 = DAT_19;
            puVar1.screen = vTransform.position;

            if (iVar5 != 0)
            {
                iVar2 = (int)GameManager.FUN_2AC5C();
                puVar1.screen.x = puVar1.screen.x - 0x16800 + (iVar2 * 0x2d000 >> 15);
                iVar2 = (int)GameManager.FUN_2AC5C();
                puVar1.screen.y = puVar1.screen.y - (iVar2 * 0x14000 >> 15);
                iVar2 = (int)GameManager.FUN_2AC5C();
                puVar1.screen.z = puVar1.screen.z - 0x16800 + (iVar2 * 0x2d000 >> 15);
            }

            iVar2 = GameManager.instance.terrain.FUN_1B750((uint)puVar1.screen.x, (uint)puVar1.screen.z);

            if (iVar2 - 0x7800 < puVar1.screen.y)
                puVar1.screen.y = iVar2 - 0x7800;

            puVar1.maxHalfHealth = 75;
            puVar1.FUN_3066C();
            GameManager.instance.FUN_30CB0(puVar1, iVar6);
            iVar5++;
            iVar6 += 15;
        } while (iVar5 < 3);

        iVar3 = GameManager.instance.FUN_1DD9C();
        GameManager.instance.FUN_1E5D4(iVar3, vData.sndList, 4, vTransform.position);
        iVar5 = (int)GameManager.FUN_2AC5C();
        GameManager.instance.FUN_1E30C(iVar3, ((iVar5 << 10) >> 15) + 0xe00);
    }
}
