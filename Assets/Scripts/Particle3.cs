using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle3 : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_4E460
    public override uint UpdateW(int arg1, int arg2)
    {
        int iVar1;
        VigObject oVar2;
        VigObject oVar3;

        if (arg1 == 0)
        {
            oVar2 = child2;

            if (child2 == null)
                GameManager.instance.FUN_309A0(this);
            else
            {
                do
                {
                    oVar3 = oVar2.child;
                    iVar1 = oVar2.physics2.Y - 1;
                    oVar2.physics2.Y = iVar1;

                    if (iVar1 == 0)
                    {
                        oVar2.FUN_2CCBC();
                        GameManager.instance.FUN_2C4B4(oVar2);
                        Utilities.ParentChildren(this, this);
                    }
                    else
                    {
                        oVar2.screen.x += oVar2.physics1.Z;
                        oVar2.screen.y += oVar2.physics1.W;
                        oVar2.screen.z += oVar2.physics2.X;
                        oVar2.vr.x += oVar2.physics1.M0;
                        oVar2.vr.y += oVar2.physics1.M1;
                        oVar2.vr.z += oVar2.physics1.M2;
                        oVar2.ApplyTransformation();
                        oVar2.physics1.W += 90;
                    }

                    oVar2 = oVar3;
                } while (oVar3 != null);
            }
        }

        return 0;
    }
}
