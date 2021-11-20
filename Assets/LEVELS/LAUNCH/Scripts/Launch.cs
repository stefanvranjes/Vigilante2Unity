using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : VigObject
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
        GameManager.instance.FUN_17F34(0x2f800, 0x7fff0000);

        if (instance == null)
        {
            instance = this;
        }
    }

    public static Launch instance;
    public byte DAT_5874; //0x5874 (LAUNCH.DLL)
    public byte DAT_5875; //0x5875 (LAUNCH.DLL)

    //FUN_200 (LAUNCH.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        VigObject oVar2;
        uint uVar3;
        VigObject oVar3;

        switch (arg1)
        {
            case 1:
                GameManager.instance.offsetFactor = 2.5f;
                GameManager.instance.offsetStart = 0;
                GameManager.instance.angleOffset = 0.4f;
                Color32 color = LevelManager.instance.DAT_DE0;
                color.a = 0x80;
                UIManager.instance.underwater.color = color;
                oVar3 = GameManager.instance.FUN_30250(GameManager.instance.DAT_1078, 0x100);
                oVar2 = GameManager.instance.FUN_4AC1C(0xfe000000, oVar3);
                GameManager.instance.DAT_1038 = oVar2 != null ? 1 : 0;
                DAT_5875 = 0;
                DAT_5874 = 0;
                GameManager.instance.DAT_1000 |= 4;
                goto case 2;
            case 2:
                GameManager.instance.FUN_34B34();
                GameManager.instance.FUN_30CB0(this, 240);
                uVar3 = 0;
                break;
            case 17:
                GameManager.instance.FUN_17EB8();
                uVar3 = 0;
                break;
            default:
                uVar3 = 0;
                break;
        }

        return uVar3;
    }

    public override uint UpdateW(VigObject arg1, int arg2, int arg3)
    {
        ushort uVar1;
        int iVar2;
        VigObject oVar2;
        VigCamera cVar2;
        int iVar3;
        ConfigContainer ccVar3;
        VigObject oVar3;
        Vehicle vVar4;

        switch (arg2)
        {
            case 18:
                GameManager.instance.FUN_327CC(arg1);
                break;
            case 19:
                oVar2 = GameManager.instance.FUN_318D0(49);
                uVar1 = (ushort)(oVar2.screen.x < arg1.vTransform.position.x ? 1 : 0);
                ccVar3 = oVar2.FUN_2C5F4((ushort)(uVar1 + 0x8000));
                arg1.vTransform = GameManager.instance.FUN_2CEAC(oVar2, ccVar3);
                iVar2 = arg1.vTransform.rotation.V02 * 0x3b9a;

                if (iVar2 < 0)
                    iVar2 += 31;

                arg1.physics1.X = iVar2 >> 5;
                iVar2 = arg1.vTransform.rotation.V12 * 0x3b9a;

                if (iVar2 < 0)
                    iVar2 += 31;

                arg1.physics1.Y = iVar2 >> 5;
                iVar2 = arg1.vTransform.rotation.V22 * 0x3b9a;

                if (iVar2 < 0)
                    iVar2 += 31;

                arg1.physics1.Z = iVar2 >> 5;
                arg1.physics2.X = 0;
                arg1.physics2.Y = 0;
                arg1.physics2.Z = 0;
                iVar3 = GameManager.instance.FUN_1DD9C();
                GameManager.instance.FUN_1E580(iVar3, GameManager.instance.DAT_C2C, 37, arg1.vTransform.position);
                vVar4 = (Vehicle)arg1;

                if (vVar4.vCamera != null)
                {
                    oVar3 = GameManager.instance.FUN_30250(GameManager.instance.DAT_1078, uVar1 + 0x201);
                    cVar2 = LevelManager.instance.FUN_4B984(vVar4, oVar3);
                    cVar2.maxHalfHealth = 0x100;
                    vVar4.vCamera.flags &= 0xf3ffffff;
                    GameManager.instance.FUN_30CB0(vVar4.vCamera, 90);
                    vVar4.vCamera = cVar2;
                    LevelManager.instance.defaultCamera.transform.SetParent(cVar2.transform, false);
                    cVar2.FUN_30B78();
                }

                vVar4.FUN_41FEC();
                return 0xffffffff;
        }

        return 0;
    }

    //FUN_5730 (LAUNCH.DLL)
    public static void FUN_5730(ref Matrix3x3 param1, Vector3Int param2)
    {
        int iVar1;
        int iVar2;
        int iVar3;

        iVar1 = param2.x;
        iVar2 = param2.y;
        iVar3 = param2.z;
        param1.V00 = (short)(param1.V00 * iVar1 >> 12);
        param1.V01 = (short)(param1.V01 * iVar2 >> 12);
        param1.V02 = (short)(param1.V02 * iVar3 >> 12);
        param1.V10 = (short)(param1.V10 * iVar1 >> 12);
        param1.V11 = (short)(param1.V11 * iVar2 >> 12);
        param1.V12 = (short)(param1.V12 * iVar3 >> 12);
        param1.V20 = (short)(param1.V20 * iVar1 >> 12);
        param1.V21 = (short)(param1.V21 * iVar2 >> 12);
        param1.V22 = (short)(param1.V22 * iVar3 >> 12);
    }
}
