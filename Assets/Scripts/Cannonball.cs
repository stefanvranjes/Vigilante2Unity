using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_44AF0
    public override uint UpdateW(int arg1, VigObject arg2)
    {
        int iVar5;
        uint uVar6;
        int iVar12;
        Vector3Int v0;
        Vector3Int local_48;

        if (arg1 == 2)
        {
            LevelManager.instance.FUN_4DE54(screen, 39);
            //sound
            LevelManager.instance.FUN_309C8(this, 1);
            uVar6 = 0xffffffff;
        }
        else
        {
            if (arg1 < 3)
            {
                if (arg1 != 0)
                    return 0;

                screen.x += physics1.Z;
                screen.y += physics1.W;
                screen.z += physics2.X;
                vTransform.position = screen;
                physics1.W += 56;
                iVar5 = GameManager.instance.terrain.FUN_1B750((uint)screen.x, (uint)screen.z);

                if (screen.y <= iVar5)
                    return 0;

                if (tags != 2)
                {
                    screen.y = iVar5;
                    LevelManager.instance.FUN_4DE54(screen, 39);
                    //goto LAB_451D4;
                }

                v0 = GameManager.instance.terrain.FUN_1B998((uint)screen.x, (uint)screen.z);
                local_48 = Utilities.VectorNormal(v0);
                screen.y = iVar5;
                iVar5 = local_48.x * physics1.Z +
                        local_48.y * physics1.W +
                        local_48.z * physics2.X;

                if (iVar5 < 0)
                    iVar5 += 2047;

                iVar5 >>= 11;

                if (-1 < iVar5)
                    return 0;

                iVar12 = iVar5 * local_48.x;

                if (iVar12 < 0)
                    iVar12 += 4095;

                physics1.Z -= iVar12 >> 12;
                iVar12 = iVar5 * local_48.y;

                if (iVar12 < 0)
                    iVar12 += 4095;

                physics1.W -= iVar12 >> 12;
                iVar5 *= local_48.z;

                if (iVar5 < 0)
                    iVar5 += 4095;

                physics2.X -= iVar5 >> 12;
            }
            else
            {
                if (arg1 != 3)
                    return 0;
            }

            uVar6 = 0;
        }

        return uVar6;
    }
}
