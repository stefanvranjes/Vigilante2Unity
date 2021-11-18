using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamewall1 : VigObject
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
        int iVar3;
        Color32 cVar3;
        bool bVar7;
        VigObject oVar7;
        Vehicle vVar7;

        oVar7 = hit.self;

        if (oVar7.type == 2)
        {
            vVar7 = (Vehicle)oVar7;
            vVar7.FUN_3A064(-100, vTransform.position, true); //-200
            iVar3 = GameManager.instance.FUN_1DD9C();
            GameManager.instance.FUN_1E628(iVar3, GameManager.instance.DAT_C2C, 63, vTransform.position);
            bVar7 = LevelManager.instance.FUN_39AF8(vVar7);
            LevelManager.instance.FUN_4DE54(vTransform.position, 35);
            cVar3 = new Color32(0x80, 0x80, 0x00, 8);
        }
        else
        {
            LevelManager.instance.FUN_4DE54(vTransform.position, 35);
            iVar3 = GameManager.instance.FUN_1DD9C();
            GameManager.instance.FUN_1E580(iVar3, GameManager.instance.DAT_C2C, 63, vTransform.position);
            cVar3 = new Color32(0x80, 0x00, 0x00, 8);
        }

        UIManager.instance.FUN_4E414(vTransform.position, cVar3);
        GameManager.instance.FUN_309A0(this);
        return 0xffffffff;
    }

    //FUN_48684
    public override uint UpdateW(int arg1, int arg2)
    {
        uint uVar1;
        VigObject ppcVar2;
        int iVar3;
        uint uVar3;
        int iVar6;

        if (arg1 == 0)
        {
            vTransform.position.x += physics1.Z;
            vTransform.position.y += physics1.W;
            iVar6 = GameManager.instance.DAT_DA0;
            vTransform.position.z += physics2.X;
            physics1.W += 56;

            if (vTransform.position.z < iVar6 && GameManager.instance.DAT_DB0 < vTransform.position.y)
            {
                LevelManager.instance.FUN_4DE54(vTransform.position, 138);
                iVar3 = GameManager.instance.FUN_1DD9C();
                GameManager.instance.FUN_1E14C(iVar3, GameManager.instance.DAT_C2C, 70);
            }
            else
            {
                iVar6 = GameManager.instance.terrain.FUN_1B750((uint)vTransform.position.x, (uint)vTransform.position.z);

                if (vTransform.position.y <= iVar6)
                    return 0;

                uVar1 = GameManager.FUN_2AC5C();
                uVar3 = 25;

                if ((uVar1 & 1) != 0)
                    uVar3 = 29;

                ppcVar2 = LevelManager.instance.xobfList[19].ini.FUN_2C17C((ushort)uVar3, typeof(Flamewall2), 8, typeof(VigChild));
                Utilities.ParentChildren(ppcVar2, ppcVar2);
                ppcVar2.type = 8;
                ppcVar2.flags = 0x14;
                ppcVar2.id = id;
                ppcVar2.screen = vTransform.position;
                ppcVar2.maxHalfHealth = 1;
                ppcVar2.FUN_3066C();
                GameManager.instance.FUN_30CB0(ppcVar2, 60);
                iVar3 = GameManager.instance.FUN_1DD9C();
                GameManager.instance.FUN_1E628(iVar3, GameManager.instance.DAT_C2C, 68, ppcVar2.screen);
            }
        }
        else
            return 0;

        GameManager.instance.FUN_309A0(this);
        return 0xffffffff;
    }
}
