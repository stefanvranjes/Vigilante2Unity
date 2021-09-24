using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle4 : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_4E744
    public override uint UpdateW(int arg1, int arg2)
    {
        bool bVar1;
        uint uVar2;
        VigObject oVar3;
        int iVar4;
        int iVar5;
        VigObject oVar5;

        if (arg1 == 1)
        {
            iVar5 = 0;

            do
            {
                oVar3 = LevelManager.instance.xobfList[19].ini.FUN_2C17C((ushort)DAT_1A, typeof(VigObject), 8);
                oVar3.vTransform.position = new Vector3Int(0, 0, 0);
                iVar4 = (int)GameManager.FUN_2AC5C();
                oVar3.physics1.Z = ((iVar4 << 11) >> 15) - 0x400;
                iVar4 = (int)GameManager.FUN_2AC5C();
                oVar3.physics1.W = -((iVar4 << 13) >> 15);
                iVar4 = (int)GameManager.FUN_2AC5C();
                oVar3.physics2.X = ((iVar4 << 11) >> 15) - 0x400;
                oVar3.flags = 0x10;
                Utilities.FUN_2CC48(this, oVar3);
                Utilities.ParentChildren(this, this);
                bVar1 = iVar5 < 8;
                iVar5++;
            } while (bVar1);

            uVar2 = 0;
        }
        else
        {
            if (arg1 == 0)
            {
                oVar5 = child2;
                uVar2 = 0;

                if (oVar5 != null)
                {
                    do
                    {
                        oVar5.vTransform.position.x += oVar5.physics1.Z;
                        oVar5.vTransform.position.y += oVar5.physics1.W;
                        oVar5.vTransform.position.z += oVar5.physics2.X;
                        oVar5.physics1.W += 56;
                        oVar5 = oVar5.child;
                    } while (oVar5 != null);

                    uVar2 = 0;
                }
            }

            uVar2 = 0;
        }

        return uVar2;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        if (arg1 == 5)
        {
            GameManager.instance.FUN_309A0(this);
            return 0xfffffffe;
        }

        return 0;
    }
}
