using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle5 : VigObject
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

        FUN_30BA8();
        FUN_4DC94();
        uVar1 = 0;

        if (vMesh == null)
        {
            GameManager.instance.FUN_309A0(this);
            uVar1 = 0xffffffff;
        }

        return uVar1;
    }

    //FUN_4D024
    public override uint UpdateW(int arg1, int arg2)
    {
        uint uVar1;
        int iVar2;
        VigCollider psVar3;
        Vector3Int local_10;

        if (arg1 == 0)
        {
            psVar3 = vCollider;

            if (psVar3 == null || psVar3.reader.ReadUInt16(0) != 1)
                local_10 = new Vector3Int(0, -DAT_58, 0);
            else
            {
                local_10 = new Vector3Int(
                    (psVar3.reader.ReadInt32(4) + psVar3.reader.ReadInt32(16)) / 2,
                    psVar3.reader.ReadInt32(8),
                    psVar3.reader.ReadInt32(24));
            }

            FUN_24700((short)vr.x, 0, 0);
            iVar2 = vr.y;

            if (iVar2 < 0)
                iVar2 += 7;

            vr.x += iVar2 >> 3;
            local_10 = Utilities.FUN_24148(vTransform, local_10);
            iVar2 = GameManager.instance.terrain.FUN_1B750((uint)local_10.x, (uint)local_10.z);

            if (local_10.y < iVar2)
                return 0;
        }
        else
            return 0;

        FUN_30BA8();
        FUN_4DC94();
        uVar1 = 0;

        if (vMesh == null)
        {
            GameManager.instance.FUN_309A0(this);
            uVar1 = 0xffffffff;
        }

        return uVar1;
    }
}
