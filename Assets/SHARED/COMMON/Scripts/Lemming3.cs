using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lemming3 : VigObject
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
        uint uVar1;
        VigObject oVar2;
        Vehicle vVar2;
        Vector3Int auStack24;
        Vector3Int uStack16;

        oVar2 = hit.self;
        uVar1 = 0;

        if (oVar2.type == 2)
        {
            vVar2 = (Vehicle)oVar2;
            Utilities.FUN_2A168(out auStack24, vTransform.position, vVar2.vTransform.position);
            uStack16 = new Vector3Int();
            uStack16.x = 0;
            uStack16.y = -0x2000;

            if (auStack24.y < -0x2000)
                uStack16.y = auStack24.y;

            uStack16.z = 0;
            vVar2.FUN_2B370(uStack16, vTransform.position);
            uVar1 = 0;

            if (vVar2.id < 0)
            {
                GameManager.instance.FUN_15B00(~vVar2.id, 255, 2, 128);
                uVar1 = 0;
            }
        }

        return uVar1;
    }

    //FUN_A0 (ELGUERRO.DLL)
    public override uint UpdateW(int arg1, VigObject arg2)
    {
        if (arg1 == 5)
        {
            GameManager.instance.FUN_309A0(this);
            return 0xffffffff;
        }

        return 0;
    }
}
