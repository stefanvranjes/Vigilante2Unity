using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle2 : VigObject
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
        Vector3Int local_18;
        Vector3Int local_10;

        oVar2 = hit.self;
        uVar1 = 0;

        if (oVar2.type == 2)
        {
            vVar2 = (Vehicle)oVar2;
            Utilities.FUN_2A168(out local_18, vTransform.position, vVar2.vTransform.position);
            local_10 = new Vector3Int(local_18.x * 12, local_18.y * 6, local_18.z * 12);
            vVar2.FUN_2B370(local_10, vTransform.position);
            uVar1 = 0;

            if (vVar2.id < 0)
            {
                GameManager.instance.FUN_15B00(~vVar2.id, 255, 2, 128);
                uVar1 = 0;
            }
        }

        return uVar1;
    }

    //FUN_4E03C
    public override uint UpdateW(int arg1, VigObject arg2)
    {
        uint uVar1;

        uVar1 = 0;

        if (arg1 == 5)
        {
            GameManager.instance.FUN_309A0(this);
            uVar1 = 0xfffffffe;
        }

        return uVar1;
    }
}
