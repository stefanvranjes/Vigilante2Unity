using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrumpetBallistic : Ballistic
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
        int iVar4;
        Vehicle vVar9;

        if (hit.self.type == 2 && (hit.self.flags & 0x4000000) == 0)
        {
            vVar9 = (Vehicle)hit.self;

            if (vVar9.id < 0)
                GameManager.instance.FUN_15AA8(~vVar9.id, 8, 255, 64, 16);

            vVar9.physics1.X += DAT_90.physics1.Z * 2 - vVar9.DAT_A6;
            vVar9.physics1.Z += DAT_90.physics2.X * 2 - vVar9.DAT_A6;

            if (DAT_94 == null)
            {
                DAT_94 = vVar9;
                DAT_98 = 0x20000 - vVar9.DAT_A6 * 4;
            }
        }

        return 0;
    }

    public Trumpet2 DAT_90;
    public Vehicle DAT_94;
    public int DAT_98;

    public override uint UpdateW(int arg1, int arg2)
    {
        if (arg1 == 0)
        {
            if (DAT_94 != null)
            {
                DAT_94.physics2.X = DAT_98;
                DAT_94.physics2.Y = DAT_98;
                DAT_94.physics2.Z = DAT_98;
            }
        }

        return 0;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        return base.UpdateW(arg1, arg2);
    }
}
