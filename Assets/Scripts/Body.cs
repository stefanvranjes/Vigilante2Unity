using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum _BODY_TYPE
{
    Default, 
    Collector,  //FUN_25C (GRBLDER.DLL)
    Ant //FUN_20F0 (ROUTE66.DLL)
}

public class Body : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public _BODY_TYPE state;

    public override uint UpdateW(int arg1, int arg2)
    {
        short sVar1;
        Vector3Int v3Var2;
        int iVar3;

        switch (state)
        {
            case _BODY_TYPE.Collector:
                if (arg1 == 0)
                {
                    sVar1 = 32;

                    if (tags == 0)
                        sVar1 = -32;

                    vr.x += sVar1;

                    if (arg2 != 0)
                        ApplyTransformation();
                }
                else
                {
                    if (arg1 == 2)
                    {
                        if ((flags & 0x80) == 0)
                        {
                            GameManager.instance.FUN_30CB0(this, 31);
                            FUN_30B78();
                        }
                        else
                        {
                            ApplyTransformation();
                            iVar3 = 1 - tags;
                            tags = (sbyte)iVar3;

                            if (((iVar3 * 0x1000000 >> 24 ^ id) & 1) == 0)
                                FUN_30BA8();
                            else
                                GameManager.instance.FUN_30CB0(this, 31);

                            v3Var2 = GameManager.instance.FUN_2CE50(this);
                            LevelManager.instance.FUN_4DE54(v3Var2, 13);
                        }
                    }
                }

                break;
        }

        return 0;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        short sVar1;
        VigObject oVar2;
        uint uVar3;
        int iVar4;
        VigObject oVar4;

        switch (state)
        {
            case _BODY_TYPE.Ant:
                if (arg1 == 5)
                {
                    oVar2 = Utilities.FUN_2CDB0(this);
                    uVar3 = oVar2.flags;
                    oVar2.flags = uVar3 & 0xffffffdf;

                    if ((uVar3 & 0x1000000) != 0)
                    {
                        oVar2.flags = uVar3 & 0xfeffffdf;

                        if (oVar2.maxHalfHealth < 50)
                        {
                            oVar2.FUN_30BA8();
                            oVar2.FUN_30C68();
                            oVar2.FUN_4DC94();
                            return 0xfffffffe;
                        }

                        oVar2.FUN_2C124(16);
                        oVar4 = oVar2.child2.child2.child2;

                        while (oVar4 != null && oVar4.id != 1)
                            oVar4 = oVar4.child;

                        ((Ant)oVar2).DAT_8C = oVar4;
                        ((Body)oVar4).state = _BODY_TYPE.Ant;
                        iVar4 = oVar2.physics1.X;

                        if (0xffff < iVar4)
                            return 0xfffffffe;

                        oVar2 = oVar2.child2;

                        if (iVar4 < 0)
                            iVar4 += 15;

                        sVar1 = (short)(iVar4 >> 4);
                        oVar2.vTransform.rotation.V22 = sVar1;
                        oVar2.vTransform.rotation.V11 = sVar1;
                        oVar2.vTransform.rotation.V00 = sVar1;
                        return 0xfffffffe;
                    }
                }

                break;
        }

        return 0;
    }
}
