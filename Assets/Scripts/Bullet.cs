using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override uint OnCollision(HitDetection hit)
    {
        short sVar2;
        Particle1 pVar3;
        VigObject oVar7;
        Vehicle vVar7;

        if (hit.object2.type == 3)
            return 0;

        oVar7 = hit.self;
        pVar3 = LevelManager.instance.FUN_4DE54(screen, 0);
        pVar3.flags |= 0x400;
        sVar2 = (short)GameManager.FUN_2AC5C();
        pVar3.vr.z = sVar2;
        pVar3.ApplyTransformation();

        if (oVar7.type == 2)
        {
            vVar7 = (Vehicle)oVar7;

            if (vVar7.shield != 0) goto LAB_42A34;
        }

        //sound
        LAB_42A34:
        GameManager.instance.FUN_309A0(this);
        return 0xffffffff;
    }

    //FUN_4282C
    public override uint UpdateW(int arg1, int arg2)
    {
        short sVar1;
        short sVar2;
        int iVar3;
        Particle1 pVar3;
        uint uVar5;

        if (arg1 == 0)
        {
            iVar3 = GameManager.instance.terrain.FUN_1B750((uint)screen.x, (uint)screen.z);

            if (screen.y <= iVar3)
            {
                screen.x += physics1.Z;
                screen.y += physics1.W;
                screen.z += physics2.X;
                vTransform.position = screen;
                sVar1 = physics2.M2;
                physics2.M2 = (short)(sVar1 - 1);

                if (sVar1 != 1)
                    return 0;

                goto LAB_42A34;
            }

            pVar3 = LevelManager.instance.FUN_4DE54(screen, 0);
            sVar2 = (short)GameManager.FUN_2AC5C();
            pVar3.vr.z = sVar2;
            pVar3.ApplyTransformation();
            //sound
        }

        //sound
        LAB_42A34:
        GameManager.instance.FUN_309A0(this);
        return 0xffffffff;
    }
}
