using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant4 : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //0x120 (ROUTE66.DLL)
    private static Vector3Int DAT_120 = new Vector3Int(0, -0x140000, 0);
    //0x12C (ROUTE66.DLL)
    private static Vector3Int DAT_12C = new Vector3Int(0, 0, 0);

    public override uint OnCollision(HitDetection hit)
    {
        int iVar1;
        VigObject oVar2;
        Vehicle vVar2;
        int iVar3;
        Vector3Int local_20;

        oVar2 = hit.self;

        if (oVar2.type == 2)
        {
            vVar2 = (Vehicle)oVar2;
            local_20 = new Vector3Int();
            iVar1 = (int)GameManager.FUN_2AC5C();
            iVar3 = iVar1 << 2;
            iVar3 += iVar1;
            iVar3 <<= 11;
            iVar3 >>= 15;
            iVar3 -= 0x1400;
            iVar1 = (int)GameManager.FUN_2AC5C();
            local_20.x = vVar2.vTransform.position.x + iVar3;
            local_20.z = (vVar2.vTransform.position.z + ((iVar1 << 2) << 11 >> 15)) - 0x1400;
            local_20.y = vVar2.vTransform.position.y;
            vVar2.FUN_2B370(DAT_120, local_20);
            vVar2.FUN_3A064(500, DAT_12C, true);
        }

        return 0;
    }

    //FUN_2A78 (ROUTE66.DLL)
    public override uint UpdateW(int arg1, VigObject arg2)
    {
        uint uVar1;

        uVar1 = 0;

        if (arg1 == 5)
        {
            GameManager.instance.FUN_309A0(this);
            uVar1 = 0xffffffff;
        }

        return uVar1;
    }
}
