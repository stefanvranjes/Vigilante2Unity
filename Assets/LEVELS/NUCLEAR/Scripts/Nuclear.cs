using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nuclear : VigObject
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

        flags |= 0x2000;
        GameManager.instance.FUN_17F34(0x64000, 0x7ffffff);
    }

    public static Nuclear instance;

    //FUN_230 (NUCLEAR.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        VigObject oVar4;
        int iVar5;
        VigObject oVar5;

        switch (arg1)
        {
            case 1:
                GameManager.instance.offsetFactor = 2.5f;
                GameManager.instance.offsetStart = 0;
                GameManager.instance.angleOffset = 0.4f;
                Color32 color = LevelManager.instance.DAT_DE0;
                color.a = 0x80;
                UIManager.instance.underwater.color = color;
                GameManager.instance.DAT_1000 |= 1;
                oVar4 = GameManager.instance.FUN_30250(GameManager.instance.DAT_1078, 0x100);
                oVar5 = GameManager.instance.FUN_4AC1C(0xfe000000, oVar4);
                GameManager.instance.DAT_1038 = oVar5 != null ? 1 : 0;
                goto case 2;
            case 2:
                GameManager.instance.FUN_34B34();
                GameManager.instance.FUN_30CB0(this, 240);
                iVar5 = 0;
                break;
            default:
                iVar5 = 0;
                break;
            case 17:
                GameManager.instance.FUN_17EB8();
                iVar5 = 0;
                break;
        }

        return (uint)iVar5;
    }

    public override uint UpdateW(VigObject arg1, int arg2, int arg3)
    {
        int iVar1;
        VigTuple2 tVar1;
        VigObject oVar2;
        int iVar3;
        int iVar4;
        VigObject oVar4;
        ConfigContainer ccVar4;
        int iVar5;
        VigObject oVar5;
        VigCamera cVar5;
        int iVar6;
        int iVar7;
        Vehicle vVar8;

        switch (arg2)
        {
            case 18:
                GameManager.instance.FUN_327CC(arg1);
                break;
            case 19:
                iVar6 = 0x7fff0000;
                oVar5 = null;
                iVar7 = 0;
                tVar1 = GameManager.instance.FUN_2FF3C((uint)arg1.vTransform.position.x, (uint)arg1.vTransform.position.z);

                if (tVar1 != null && (ushort)tVar1.id - 1U < 2)
                {
                    iVar7 = (tVar1.id - 1 ^ 1) + 514;
                    oVar5 = GameManager.instance.FUN_31950(iVar7);
                }

                if (oVar5 == null)
                {
                    iVar1 = 0;

                    do
                    {
                        oVar2 = GameManager.instance.FUN_31950(iVar1 + 49);

                        if (oVar2 != null)
                        {
                            iVar3 = Utilities.FUN_29F6C(arg1.vTransform.position, oVar2.vTransform.position);

                            if (iVar3 < iVar6)
                            {
                                iVar7 = iVar1 + 0x200;
                                oVar5 = oVar2;
                                iVar6 = iVar3;
                            }
                        }

                        iVar1++;
                    } while (iVar1 < 2);
                }

                ccVar4 = oVar5.FUN_2C5F4(0x8000);
                arg1.vTransform = GameManager.instance.FUN_2CEAC(oVar5, ccVar4);
                arg1.physics1.X = 0;
                arg1.physics1.Y = 0;
                arg1.physics1.Z = 0;
                iVar5 = arg1.vTransform.rotation.V02 * 7629;

                if (iVar5 < 0)
                    iVar5 += 31;

                arg1.physics1.X = iVar5 >> 5;
                iVar5 = arg1.vTransform.rotation.V12 * 7629;

                if (iVar5 < 0)
                    iVar5 += 31;

                arg1.physics1.Y = iVar5 >> 5;
                iVar5 = arg1.vTransform.rotation.V22 * 7629;

                if (iVar5 < 0)
                    iVar5 += 31;

                arg1.physics1.Z = iVar5 >> 5;
                iVar4 = GameManager.instance.FUN_1DD9C();
                GameManager.instance.FUN_1E580(iVar4, GameManager.instance.DAT_C2C, 37, arg1.vTransform.position);
                vVar8 = (Vehicle)arg1;
                vVar8.state = _VEHICLE_TYPE.NuclearTunnel;
                vVar8.flags |= 0x4000000;
                GameManager.instance.FUN_30CB0(vVar8, 30);
                
                if (vVar8.vCamera != null)
                {
                    oVar4 = GameManager.instance.FUN_30250(GameManager.instance.DAT_1078, iVar7);
                    cVar5 = LevelManager.instance.FUN_4B984(vVar8, oVar4);
                    cVar5.maxHalfHealth = 0x100;
                    vVar8.vCamera.flags &= 0xfbffffff;
                    GameManager.instance.FUN_30CB0(vVar8.vCamera, 90);
                    vVar8.vCamera = cVar5;
                    LevelManager.instance.defaultCamera.transform.SetParent(cVar5.transform, false);
                    cVar5.FUN_30B78();
                }

                return 0xffffffff;
        }

        return 0;
    }
}
