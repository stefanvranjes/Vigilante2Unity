using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spark : VigObject
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
        int iVar2;
        Vehicle vVar3;

        if (hit.self.type == 2)
        {
            vVar3 = (Vehicle)hit.self;
            vVar3.physics1.Y -= 0x5f580;
            flags |= 0x20;
            LevelManager.instance.FUN_39AF8(vVar3);
            vVar3.FUN_3A064(-25, vTransform.position, true);
            UIManager.instance.FUN_4E414(vTransform.position, new Color32(0x80, 0x00, 0x00, 8));
            iVar2 = GameManager.instance.FUN_1DD9C();
            GameManager.instance.FUN_1E580(iVar2, GameManager.instance.DAT_C2C, 69, vTransform.position);
            LevelManager.instance.FUN_4E8C8(vTransform.position, 48);
        }

        return 0;
    }

    //FUN_370 (STEELMIL.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        short sVar1;
        int iVar2;
        TileData tVar3;
        Vector3Int auStack8;

        if (arg1 < 4)
        {
            if (arg1 != 0)
                return 0;

            if ((flags & 0x20) == 0)
            {
                vTransform.position.x += physics1.X;
                vTransform.position.z += physics1.Z;
                iVar2 = GameManager.instance.terrain.FUN_1B750((uint)vTransform.position.x, (uint)vTransform.position.z);
                vTransform.position.y = iVar2;
                auStack8 = GameManager.instance.terrain.FUN_1B998((uint)vTransform.position.x, (uint)vTransform.position.z);
                auStack8 = Utilities.VectorNormal(auStack8);
                child2.vTransform.rotation = Utilities.FUN_2A5EC(auStack8);
                tVar3 = GameManager.instance.terrain.GetTileByPosition((uint)vTransform.position.x, (uint)vTransform.position.z);

                if (tVar3.DAT_10[3] == 1)
                    return 0;

                flags |= 0x20;
            }
            else
            {
                sVar1 = physics1.M6;
                physics1.M6 = (short)(sVar1 - 136);
                vTransform.rotation.V22 = sVar1;
                vTransform.rotation.V11 = sVar1;
                vTransform.rotation.V00 = sVar1;

                if (sVar1 - 136 < 205)
                {
                    GameManager.instance.FUN_309A0(this);
                    return 0xffffffff;
                }
            }
        }
        else
        {
            if (arg1 != 4)
                return 0;

            GameManager.instance.FUN_1DE78(DAT_18);
        }

        return 0;
    }
}
