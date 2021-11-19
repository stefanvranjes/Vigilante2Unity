using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trumpet : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_17D0 (TRUCk.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        switch (arg1)
        {
            case 0:
                FUN_42330(arg2);
                break;
        }

        return 0;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        short sVar1;
        int iVar2;
        ConfigContainer ccVar2;
        Trumpet2 ppcVar3;
        int iVar4;
        VigObject oVar5;
        uint uVar6;
        int iVar7;
        int iVar8;
        VigObject oVar8;
        Vector3Int local_18;

        switch (arg1)
        {
            case 0:
                FUN_42330(arg2);
                uVar6 = 0;
                break;
            case 1:
                maxHalfHealth = 6;
                ccVar2 = FUN_2C5F4(0x8000);
                oVar5 = vData.ini.FUN_2C17C(2, typeof(VigChild), 0);
                ((VigChild)oVar5).state = _CHILD_TYPE.Default;
                Utilities.FUN_2CA94(this, ccVar2, oVar5);
                ccVar2 = FUN_2C5F4(0x8001);
                oVar5 = vData.ini.FUN_2C17C(2, typeof(VigObject), 0);
                Utilities.FUN_2CA94(this, ccVar2, oVar5);
                Utilities.ParentChildren(this, this);
                goto default;
            default:
                uVar6 = 0;
                break;
            case 5:
                oVar8 = child2;
                oVar8.child.vAnim = null;
                oVar8.vAnim = null;
                FUN_30C20();
                uVar6 = 0xfffffffe;

                if (maxHalfHealth == 0)
                {
                    FUN_3A368();
                    uVar6 = 0xfffffffe;
                }

                break;
            case 12:
                iVar2 = GameManager.instance.FUN_1DD9C();
                GameManager.instance.FUN_1E580(iVar2, vData.sndList, 4, arg2.vTransform.position);
                iVar8 = 0;
                iVar2 = GameManager.instance.FUN_1DD9C();
                GameManager.instance.FUN_1E188(iVar2, vData.sndList, 2);
                child2.FUN_2C05C();
                child2.child.FUN_2C05C();
                ((VigChild)child2).state = _CHILD_TYPE.Child;
                FUN_30BF0();

                do
                {
                    ppcVar3 = vData.ini.FUN_2C17C(1, typeof(Trumpet2), 8) as Trumpet2;
                    Utilities.ParentChildren(ppcVar3, ppcVar3);
                    ppcVar3.DAT_80 = arg2;
                    ppcVar3.flags = 0x60000380;
                    sVar1 = arg2.id;
                    ppcVar3.type = 8;
                    ppcVar3.maxHalfHealth = 8;
                    ppcVar3.id = sVar1;
                    ccVar2 = FUN_2C5F4((ushort)(iVar8 - 0x8000 & 0xffff));
                    ppcVar3.vTransform = GameManager.instance.FUN_2CEAC(this, ccVar2);
                    GameManager.instance.FUN_2FEE8(ppcVar3, GameManager.instance.timer);
                    iVar7 = arg2.physics1.X;

                    if (iVar7 < 0)
                        iVar7 += 127;

                    iVar4 = ppcVar3.vTransform.rotation.V02 * 0x7735;

                    if (iVar4 < 0)
                        iVar4 += 4095;

                    ppcVar3.physics1.Z = (iVar7 >> 7) + (iVar4 >> 12);
                    iVar7 = arg2.physics1.Y;

                    if (iVar7 < 0)
                        iVar7 += 127;

                    iVar4 = ppcVar3.vTransform.rotation.V12 * 0x7735;

                    if (iVar4 < 0)
                        iVar4 += 4095;

                    ppcVar3.physics1.W = (iVar7 >> 7) + (iVar4 >> 12);
                    iVar7 = arg2.physics1.Z;

                    if (iVar7 < 0)
                        iVar7 += 127;

                    iVar4 = ppcVar3.vTransform.rotation.V22 * 0x7735;

                    if (iVar4 < 0)
                        iVar4 += 4095;

                    ppcVar3.physics2.X = (iVar7 >> 7) + (iVar4 >> 12);
                    ppcVar3.FUN_305FC();
                    iVar8++;
                } while (iVar8 < 2);

                uVar6 = 300;
                //GameManager.instance.DAT_1084++;
                maxHalfHealth--;
                break;
            case 13:
                uVar6 = 0;

                //if (GameManager.instance.DAT_1084 == 0)
                //{
                    local_18 = Utilities.FUN_24304(arg2.vTransform, ((Vehicle)arg2).target.vTransform.position);
                    uVar6 = 0;

                    if ((uint)local_18.z - 0x19001 < 0x63fff)
                    {
                        if (local_18.x < 0)
                            local_18.x = -local_18.x;

                        uVar6 = local_18.x * 2 - 0x10000 < local_18.z ? 1U : 0;
                    }
                //}

                break;
        }

        return uVar6;
    }
}
