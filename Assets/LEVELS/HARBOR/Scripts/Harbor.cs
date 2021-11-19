using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harbor : VigObject
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
        if (instance == null)
        {
            instance = this;
        }

        GameManager.instance.FUN_17F34(0x1e000, 0x7fff0000);
    }

    public static Harbor instance;

    //FUN_2E0 (HARBOR.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        uint uVar4;
        VigObject oVar4;
        VigObject oVar7;

        switch (arg1)
        {
            case 1:
                GameManager.instance.offsetFactor = 2.5f;
                GameManager.instance.offsetStart = 0;
                GameManager.instance.angleOffset = 0.4f;
                Color32 color = LevelManager.instance.DAT_DE0;
                color.a = 0x80;
                UIManager.instance.underwater.color = color;
                oVar4 = GameManager.instance.FUN_30250(GameManager.instance.DAT_1078, 0x100);
                oVar7 = GameManager.instance.FUN_4AC1C(0xfe000000, oVar4);
                GameManager.instance.DAT_1038 = oVar7 != null ? 1 : 0;
                goto case 2;
            case 2:
                GameManager.instance.FUN_34B34();
                GameManager.instance.FUN_30CB0(this, 240);
                uVar4 = 0;
                break;
            case 17:
                GameManager.instance.FUN_17EB8();
                uVar4 = 0;
                break;
            default:
                uVar4 = 0;
                break;
        }

        return uVar4;
    }

    public override uint UpdateW(VigObject arg1, int arg2, int arg3)
    {
        sbyte sVar1;
        CraneSmall oVar2;
        int iVar3;
        int iVar4;
        ConfigContainer ccVar4;
        VigObject oVar5;
        int iVar6;
        int iVar7;
        CraneSmall oVar7;
        int iVar8;
        Vehicle vVar9;

        switch (arg2)
        {
            case 18:
                GameManager.instance.FUN_327CC(arg1);
                break;
            case 19:
                iVar8 = 0x7fff0000;
                oVar7 = null;
                iVar6 = 113;

                do
                {
                    oVar2 = GameManager.instance.FUN_30250(GameManager.instance.worldObjs, iVar6) as CraneSmall;

                    if (oVar2 != null && oVar2.tags == 0)
                    {
                        iVar3 = Utilities.FUN_29F6C(arg1.vTransform.position, oVar2.vTransform.position);

                        if (iVar3 < iVar8)
                        {
                            oVar7 = oVar2;
                            iVar8 = iVar3;
                        }
                    }

                    iVar6++;
                } while (iVar6 < 116);

                if (oVar7 == null)
                    oVar7 = GameManager.instance.FUN_30250(GameManager.instance.worldObjs, 113) as CraneSmall;
                else
                {
                    oVar7.DAT_A4 = 30;
                    oVar7.tags = 1;
                    oVar7.DAT_80 = arg1;
                    oVar7.DAT_B4 = 7;
                    oVar7.FUN_30B78();
                    sVar1 = (sbyte)GameManager.instance.FUN_1DD9C();
                    oVar7.DAT_18 = sVar1;
                    GameManager.instance.FUN_1E628(sVar1, oVar7.vData.sndList, 1, oVar7.vTransform.position, true);
                }

                oVar5 = oVar7.child2.child2.child2;
                ccVar4 = oVar5.FUN_2C5F4(0x8000);
                arg1.vTransform = GameManager.instance.FUN_2CEAC(oVar5, ccVar4);
                iVar6 = GameManager.instance.DAT_DB0 + 0xf000;
                iVar8 = GameManager.instance.terrain.FUN_1B750((uint)arg1.vTransform.position.x, (uint)arg1.vTransform.position.z);
                iVar7 = iVar8 - 0xa000;

                if (iVar6 < iVar8 - 0xa000)
                    iVar7 = iVar6;

                arg1.vTransform.position.y = iVar7;
                arg1.physics1.X = 0;
                arg1.physics1.Y = 0;
                arg1.physics1.Z = 0;
                arg1.physics2.X = 0;
                arg1.physics2.Y = 0;
                arg1.physics2.Z = 0;
                iVar4 = GameManager.instance.FUN_1DD9C();
                GameManager.instance.FUN_1E580(iVar4, GameManager.instance.DAT_C2C, 37, arg1.vTransform.position);
                vVar9 = (Vehicle)arg1;
                vVar9.state = _VEHICLE_TYPE.Harbor;
                GameManager.instance.FUN_30CB0(vVar9, 60);
                vVar9.flags &= 0xfdffffff;

                if (vVar9.vCamera != null)
                    GameManager.instance.FUN_30CB0(vVar9.vCamera, 1);

                return 0xffffffff;
        }

        return 0;
    }
}
