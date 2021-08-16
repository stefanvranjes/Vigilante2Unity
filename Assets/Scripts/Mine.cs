using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_46F1C
    public override uint UpdateW(int arg1, int arg2)
    {
        int iVar1;
        VigObject pcVar2;

        if (arg1 == 2)
        {
            if (DAT_80.maxHalfHealth == 0)
            {
                flags &= 0xdfffffff;
                return 0;
            }
        }
        else
        {
            if (arg1 != 0)
                return 0;

            if (tags != 0)
            {
                iVar1 = GameManager.DAT_65C90[((ushort)physics2.M2 & 0xfff) * 2] * 0x1400;

                if (iVar1 < 0)
                    iVar1 += 4095;

                vTransform.position.y = GameManager.instance.DAT_DB0 + (iVar1 >> 12);
                physics2.M2 += 34;
                return 0;
            }

            iVar1 = FUN_46D70();

            if (iVar1 == 0)
                return 0;

            if (vTransform.position.z < GameManager.instance.DAT_DA0 &&
                GameManager.instance.DAT_DB0 <= vTransform.position.y)
            {
                tags = 1;
                physics2.M2 = 0;
            }
            else
                FUN_30BA8();

            id = 0;
            flags |= 0x300;
            GameManager.instance.FUN_30080(GameManager.instance.DAT_10C8, this);
            iVar1 = Utilities.FUN_29A9C(GameManager.instance.DAT_10C8);

            if (15 < iVar1)
            {
                pcVar2 = GameManager.instance.DAT_10C8[0].vObject;

                if (pcVar2.GetType().IsSubclassOf(typeof(VigObject)))
                    pcVar2.UpdateW(2, 0);
            }

            if ((flags & 0x20000000) == 0)
                return 0;
        }

        GameManager.instance.FUN_30CB0(this, 120);
        return 0;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        if (arg1 != 2)
        {
            if (2 < arg1)
            {
                if (arg1 != 3)
                {
                    if (arg1 != 8)
                        return 0;

                    GameManager.instance.FUN_300B8(GameManager.instance.DAT_10C8, this);
                    //sound
                    LevelManager.instance.FUN_4DE54(screen, 39);
                    LevelManager.instance.FUN_309C8(this, 0);
                    return 0xffffffff;
                }
            }
        }

        GameManager.instance.FUN_30CB0(this, 120);
        return 0;
    }

    public int FUN_46D70()
    {
        int iVar1;
        int iVar2;
        Vector3Int auStack8;

        screen.x += physics1.Z;
        screen.y += physics1.W;
        screen.z += physics2.X;
        physics1.Z -= physics1.Z >> 4;
        physics1.W -= physics1.W >> 4;
        physics2.X -= physics2.X >> 4;
        vTransform.position.x = screen.x;
        vTransform.position.y = screen.y;
        vTransform.position.z = screen.z;
        physics1.W += 56;
        auStack8 = new Vector3Int();
        iVar1 = FUN_2CFBC(screen, ref auStack8);

        if (vTransform.position.z < GameManager.instance.DAT_DA0 &&
            GameManager.instance.DAT_DB0 < iVar1)
            iVar1 = GameManager.instance.DAT_DB0;

        iVar2 = 0;

        if (iVar1 < screen.y)
        {
            screen.y = iVar1;
            //sound
            physics2.M2++;
            vTransform.rotation = Utilities.FUN_2A5EC(auStack8);
            iVar2 = 1;

            if (761 < physics1.W)
            {
                physics1.W = -physics1.W / 2;
                physics1.Z >>= 2;
                physics2.X >>= 2;
                iVar2 = 0;
            }
        }

        return iVar2;
    }
}
