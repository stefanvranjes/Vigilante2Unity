using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bayou : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    private void Awake()
    {
        flags |= 0x2000;
        GameManager.instance.FUN_17F34(0xc800, 0x7fff0000);

        if (instance == null)
        {
            instance = this;
        }
    }

    private static short[] ids = new short[7] { 307, 309, 310, 312, 313, 314, 324 };

    public static Bayou instance;

    //FUN_328 (BAYOU.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        VigObject oVar5;
        int iVar6;
        VigObject oVar6;

        switch (arg1)
        {
            case 1:
                GameManager.instance.offsetFactor = 2.5f;
                GameManager.instance.offsetStart = 0;
                GameManager.instance.angleOffset = 0.4f;
                Color32 color = LevelManager.instance.DAT_DE0;
                color.a = 0x80;
                UIManager.instance.underwater.color = color;
                FUN_7F0(129);
                oVar5 = GameManager.instance.FUN_30250(GameManager.instance.DAT_1078, 0x100);
                oVar6 = GameManager.instance.FUN_4AC1C(0xfe000000, oVar5);
                GameManager.instance.DAT_1038 = oVar6 != null ? 1 : 0;
                iVar6 = (int)GameManager.FUN_2AC5C();
                oVar5 = GameManager.instance.FUN_31EDC((iVar6 * 5 >> 15) + 1200);
                oVar5.FUN_3066C();
                goto case 2;
            case 2:
                GameManager.instance.FUN_34B34();
                GameManager.instance.FUN_30CB0(this, 240);
                iVar6 = 0;
                break;
            default:
                iVar6 = 0;
                break;
            case 17:
                GameManager.instance.FUN_17EB8();
                iVar6 = 0;
                break;
        }

        return (uint)iVar6;
    }

    public override uint UpdateW(VigObject arg1, int arg2, int arg3)
    {
        ConfigContainer ccVar3;
        int iVar4;
        int iVar5;
        VigObject oVar5;
        VigObject oVar6;
        VigCamera cVar6;
        uint uVar7;
        Vehicle vVar8;

        switch (arg2)
        {
            case 18:
                GameManager.instance.FUN_327CC(arg1);
                break;
            case 19:
                uVar7 = 0;
                oVar6 = GameManager.instance.FUN_31994(typeof(Mansion));

                if (oVar6 != null && oVar6.tags == 1)
                {
                    uVar7 = 1;
                    oVar6 = GameManager.instance.FUN_31994(typeof(Mausoleum));
                }

                ccVar3 = oVar6.FUN_2C5F4(0x8002);
                arg1.vTransform = GameManager.instance.FUN_2CEAC(oVar6, ccVar3);
                iVar4 = arg1.vTransform.rotation.V02 * 9155;

                if (iVar4 < 0)
                    iVar4 += 31;

                arg1.physics1.X = iVar4 >> 5;
                iVar4 = arg1.vTransform.rotation.V12 * 9155;

                if (iVar4 < 0)
                    iVar4 += 31;

                arg1.physics1.Y = iVar4 >> 5;
                iVar4 = arg1.vTransform.rotation.V22 * 9155;

                if (iVar4 < 0)
                    iVar4 += 31;

                arg1.physics1.Z = iVar4 >> 5;
                arg1.physics2.X = 0;
                arg1.physics2.Y = 0;
                arg1.physics2.Z = 0;
                arg1.screen = arg1.vTransform.position;
                arg1.vr = ccVar3.v3_2;
                arg1.vr.y += oVar6.vr.y;
                iVar5 = GameManager.instance.FUN_1DD9C();
                GameManager.instance.FUN_1E580(iVar5, GameManager.instance.DAT_C2C, 37, arg1.vTransform.position);
                arg1.ApplyTransformation();
                vVar8 = (Vehicle)arg1;

                if (vVar8.vCamera != null)
                {
                    oVar5 = GameManager.instance.FUN_30250(GameManager.instance.DAT_1078, (int)(uVar7 | 2000));
                    cVar6 = LevelManager.instance.FUN_4B984(vVar8, oVar5);
                    cVar6.maxHalfHealth = 0x100;
                    vVar8.vCamera.flags &= 0xfbffffff;
                    GameManager.instance.FUN_30CB0(vVar8.vCamera, 90);
                    vVar8.vCamera = cVar6;
                    LevelManager.instance.defaultCamera.transform.SetParent(cVar6.transform, false);
                    cVar6.FUN_30B78();
                }

                vVar8.FUN_41FEC();
                return 0xffffffff;
        }

        return 0;
    }

    public override uint UpdateW(VigObject arg1, int arg2, Vector3Int arg3)
    {
        Mud puVar2;
        int iVar4;
        Wheel wVar6;
        Vehicle vVar6;

        switch (arg2)
        {
            case 10:
                if (arg1.type != 9)
                    return 0;

                vVar6 = Utilities.FUN_2CD78(arg1) as Vehicle;

                if (vVar6.physics1.W < 0x11e1)
                {
                    if ((vVar6.flags & 0x20000) == 0)
                    {
                        GameObject obj = new GameObject();
                        puVar2 = obj.AddComponent<Mud>();
                        puVar2.type = 255;
                        puVar2.child = vVar6;
                        puVar2.FUN_30B78();
                        vVar6.flags |= 0x20000;
                    }

                    if (arg1.physics1.Z - arg1.physics2.X < 0x3c00)
                    {
                        iVar4 = -vVar6.physics1.W + 0x11e1;

                        if (iVar4 < 0)
                            iVar4 = -vVar6.physics1.W + 0x11e8;

                        arg1.physics2.X -= iVar4 >> 3;
                    }

                    if (arg1 == vVar6.wheels[2])
                        wVar6 = vVar6.wheels[4];
                    else
                    {
                        if (arg1 != vVar6.wheels[3])
                            return 0;

                        wVar6 = vVar6.wheels[5];
                    }

                    if (wVar6 == null)
                        return 0;

                    wVar6.physics2.X = (wVar6.physics1.Z + arg1.physics1.X) - arg1.physics1.Z;
                }

                break;
        }

        return 0;
    }

    public override sbyte UpdateW(VigObject arg1, int arg2, TileData arg3)
    {
        int iVar6;

        switch (arg2)
        {
            case 12:
                iVar6 = GameManager.instance.FUN_1DD9C();
                GameManager.instance.FUN_1E098(iVar6, LevelManager.instance.xobfList[42].sndList, 4, 0, true);
                return (sbyte)iVar6;
        }

        return 0;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        switch (arg1)
        {
            case 25:
                if (GameManager.instance.gameMode == _GAME_MODE.Survival)
                {
                    if (arg2.DAT_1A == 0 && arg2.type == 3)
                    {
                        if (ids.Contains(arg2.id))
                        {
                            arg2.screen.y -= 0x18000;
                        }
                    }
                }

                break;
        }

        return 0;
    }

    //FUN_7F0 (BAYOU.DLL)
    public static void FUN_7F0(int param1)
    {
        int iVar1;
        VigObject oVar1;
        Cage cVar1;
        ConfigContainer ccVar1;
        int iVar2;
        int iVar3;
        ConfigContainer ccVar3;
        int iVar4;
        Cage cVar5;

        cVar5 = null;
        iVar1 = (int)GameManager.FUN_2AC5C();
        iVar3 = param1 + (iVar1 * 5 >> 15);
        iVar1 = iVar3;

        if (134 < iVar3)
        {
            iVar3 -= 6;
            iVar1 = iVar3;
        }

        do
        {
            if (iVar1 != param1)
            {
                cVar5 = GameManager.instance.FUN_31EDC(iVar1) as Cage;

                if (cVar5 != null) goto LAB_894;
            }

            iVar4 = iVar1 + 1;

            if (134 < iVar4)
                iVar4 = iVar1 - 5;

            iVar1 = iVar4;
        } while (iVar4 != iVar3);

        if (cVar5 == null)
        {
            cVar5 = GameManager.instance.FUN_31EDC(param1) as Cage;
            iVar1 = param1;

            if (cVar5 == null)
                return;
        }

        LAB_894:
        cVar5.id = 100;
        cVar5.type = 4;
        cVar5.flags |= 0x100;
        cVar5.FUN_3066C();
        cVar5.DAT_19 = (byte)iVar1;
        cVar5.tags = 0;
        cVar5.physics2.Z = cVar5.vTransform.position.y;
        cVar1 = GameManager.instance.FUN_30250(GameManager.instance.DAT_1078, iVar1) as Cage;

        if (cVar1 != null)
        {
            cVar1.DAT_8C = cVar5;
            cVar5.DAT_8C = cVar1;
        }

        ccVar3 = cVar5.FUN_2C5F4(0x8000);
        ccVar1 = ccVar3;

        if (ccVar3 != null)
        {
            oVar1 = cVar5.vData.ini.FUN_2C17C(19, typeof(VigObject), 8);
            cVar5.DAT_90 = oVar1;

            if (oVar1 != null)
            {
                oVar1.vTransform = GameManager.instance.FUN_2CEAC(cVar5, ccVar3);
                cVar5.DAT_90.flags = 0x34;
                cVar5.DAT_90.FUN_305FC();
                iVar2 = GameManager.instance.FUN_1DD9C();
                GameManager.instance.FUN_1E580(iVar2, cVar5.vData.sndList, 2, cVar5.DAT_90.vTransform.position);
                GameManager.instance.FUN_1E30C(iVar2, 0x900);
            }
        }
    }
}
