using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGun : VigObject
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    //FUN_42A64
    public override uint UpdateW(int arg1, VigObject arg2)
    {
        sbyte sbVar1;
        short sVar1;
        Ballistic ppcVar2;
        Bullet ppcVar3;
        ushort uVar4;
        int iVar5;
        int iVar6;
        Vector3Int local_18;

        switch (arg1)
        {
            case 1:
                maxHalfHealth = 1280;
                return 0;
            case 4:
                iVar6 = maxHalfHealth - 64;
                iVar5 = 0x500;

                if (0x500 < iVar6)
                    iVar5 = iVar6;

                maxHalfHealth = (ushort)iVar5;
                DAT_18 = 0;

                if (tags != 0)
                    tags--;

                return 0;
            case 12:
                sbVar1 = (sbyte)(tags - 1);
                tags = sbVar1;

                if (sbVar1 != -1)
                    return 0;

                ppcVar2 = LevelManager.instance.xobfList[19].ini.FUN_2C17C(2, typeof(Ballistic), 8) as Ballistic;
                ppcVar3 = LevelManager.instance.xobfList[19].ini.FUN_2C17C(210, typeof(Bullet), 8) as Bullet;
                LevelManager.instance.FUN_42560(arg2, this, ppcVar3, ppcVar2);
                ppcVar3.flags = 0x280;
                uVar4 = 7;

                if (((Vehicle)arg2).doubleDamage != 0)
                    uVar4 = 14;

                ppcVar3.maxHalfHealth = uVar4;
                iVar5 = arg2.physics1.X;

                if (iVar5 < 0)
                    iVar5 += 127;

                ppcVar3.physics1.Z = (iVar5 >> 7) + ppcVar3.vTransform.rotation.V02 * 4;
                iVar5 = arg2.physics1.Y;

                if (iVar5 < 0)
                    iVar5 += 127;

                ppcVar3.physics1.W = (iVar5 >> 7) + ppcVar3.vTransform.rotation.V12 * 4;
                iVar5 = arg2.physics1.Z;

                if (iVar5 < 0)
                    iVar5 += 127;

                ppcVar3.physics2.X = (iVar5 >> 7) + ppcVar3.vTransform.rotation.V22 * 4;
                ppcVar3.physics2.M2 = 45;
                ppcVar3.FUN_305FC();

                if ((arg2.flags & 4) == 0)
                    ppcVar2.FUN_30BF0();

                //sound
                tags = (sbyte)(maxHalfHealth >> 8);
                maxHalfHealth += 32;
                break;
            case 13:
                local_18 = Utilities.FUN_24304(arg2.vTransform, ((Vehicle)arg2).target.vTransform.position);

                if (0x7cffe < local_18.z - 1)
                    return 0;

                if (local_18.x < 0)
                    local_18.x = -local_18.x;

                return (uint)(local_18.x * 6 < local_18.z ? 1 : 0);
        }

        return 0;
    }
}
