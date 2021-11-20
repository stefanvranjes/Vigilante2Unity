using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkLift3 : VigObject
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
        Vehicle vVar1;
        Vector3Int local_18;
        Vector3Int local_10;

        if (hit.self.type != 2)
            return 0;

        vVar1 = (Vehicle)hit.self;
        Utilities.FUN_2A168(out local_18, vTransform.position, vVar1.vTransform.position);
        local_10 = new Vector3Int();
        local_10.x = local_18.x << 4;
        local_10.z = local_18.z << 4;
        local_10.y = local_18.y << 1;
        vVar1.FUN_2B370(local_10, vTransform.position);

        if (vVar1.id < 0)
            GameManager.instance.FUN_15B00(~vVar1.id, 255, 2, 128);

        FUN_2444(vVar1);
        return 0;
    }

    //FUN_2514 (NUCLEAR.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        if (arg1 == 2)
            flags |= 0x20;
        else
        {
            if (arg1 != 1)
                return 0;

            GameManager.instance.FUN_30CB0(this, 2);
        }

        return 0;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        if (arg1 != 5)
            return 0;

        GameManager.instance.FUN_309A0(this);
        return 0xfffffffe;
    }

    //FUN_2444 (NUCLEAR.DLL)
    private static void FUN_2444(Vehicle param1)
    {
        sbyte sVar1;
        Fire1 fVar2;
        int iVar3;

        fVar2 = LevelManager.instance.FUN_399FC(param1, LevelManager.instance.xobfList[19], 23);

        if (fVar2 != null)
        {
            sVar1 = (sbyte)GameManager.instance.FUN_1DD9C();
            fVar2.DAT_18 = sVar1;
            GameManager.instance.FUN_1E580(sVar1, LevelManager.instance.xobfList[42].sndList, 0, param1.vTransform.position, true);
            UIManager.instance.FUN_4E414(param1.vTransform.position, new Color32(0x00, 0x80, 0x00, 8));
            iVar3 = GameManager.instance.FUN_1DD9C();
            GameManager.instance.FUN_1E580(iVar3, LevelManager.instance.xobfList[42].sndList, 3, param1.vTransform.position);
            param1.physics1.Y -= 0x8f080;

            if (param1.id < 0)
            {
                //FUN_32758
            }
        }
    }
}
