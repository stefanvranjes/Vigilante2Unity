using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngPlant2 : Destructible
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
        bool bVar1;
        MoltenSteel ppcVar2;

        if (hit.self.type == 2)
        {
            bVar1 = LevelManager.instance.FUN_39AF8((Vehicle)hit.self);

            if (bVar1)
            {
                LevelManager.instance.FUN_4DE54(vTransform.position, 33);
                LevelManager.instance.FUN_4DE54(vTransform.position, 29);
                UIManager.instance.FUN_4E414(vTransform.position, new Color32(0x80, 0x00, 0x00, 8));
                return 0;
            }
        }

        bVar1 = FUN_32CF0(hit);

        if (!bVar1) return 0;

        ppcVar2 = LevelManager.instance.xobfList[42].ini.FUN_2C17C(28, typeof(MoltenSteel), 8) as MoltenSteel;
        Utilities.ParentChildren(ppcVar2, ppcVar2);
        ppcVar2.flags |= 4;
        ppcVar2.screen = vTransform.position;
        ppcVar2.FUN_3066C();
        GameManager.instance.FUN_30CB0(ppcVar2, 120);
        LevelManager.instance.FUN_4DE54(vTransform.position, 33);
        LevelManager.instance.FUN_4DE54(vTransform.position, 29);
        GameManager.instance.FUN_309A0(this);
        return 0xffffffff;
    }

    //FUN_1DCC (STEELMIL.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        uint uVar3;

        if (arg1 == 1)
        {
            flags |= 0x180;
            uVar3 = 0;
        }
        else
        {
            if (arg1 == 0)
            {
                GameManager.instance.terrain.FUN_2D16C(vTransform.position.x - 0x17d7, vTransform.position.z, ref vTransform);

                if (0x36dcfff < vTransform.position.x)
                    return 0;
            }
            else
            {
                if (arg1 != 8)
                    return 0;

                FUN_32B90((uint)arg2);
                return 0;
            }

            GameManager.instance.FUN_309A0(this);
            uVar3 = 0xffffffff;
        }

        return uVar3;
    }
}
