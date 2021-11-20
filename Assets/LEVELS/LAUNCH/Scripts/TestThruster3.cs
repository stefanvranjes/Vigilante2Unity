using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestThruster3 : VigObject
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
        int iVar1;
        VigTransform tVar2;
        bool bVar3;
        VigObject oVar3;
        Vehicle vVar3;
        Vector3Int local_68;
        Vector3Int local_58;
        Vector3Int local_48;
        HitDetection auStack56;

        oVar3 = hit.self;
        uVar1 = 0;

        if (oVar3.type == 2)
        {
            vVar3 = (Vehicle)oVar3;
            tVar2 = GameManager.instance.FUN_2CDF4(this);
            local_68 = new Vector3Int();
            local_68.x = tVar2.rotation.V02 << 3;
            local_68.z = tVar2.rotation.V22 << 3;
            local_68.y = -0x4180;
            local_58 = local_68;
            auStack56 = new HitDetection(null);
            GameManager.instance.FUN_2FB70(this, hit, auStack56);
            local_48 = new Vector3Int();
            local_48.x = auStack56.position.x / 2;
            local_48.y = auStack56.position.y / 2;
            local_48.z = auStack56.position.z / 2;
            local_48 = Utilities.FUN_24148(vVar3.vTransform, local_48);
            vVar3.FUN_2B370(local_68, local_48);

            if (vVar3.id < 0)
                vVar3.FUN_3A064(-1, vTransform.position, true);

            bVar3 = LevelManager.instance.FUN_39AF8(vVar3);
            uVar1 = 0;

            if (bVar3)
            {
                iVar1 = GameManager.instance.FUN_1DD9C();
                GameManager.instance.FUN_1E580(iVar1, GameManager.instance.DAT_C2C, 69, vTransform.position);
                iVar1 = GameManager.instance.FUN_1DD9C();
                GameManager.instance.FUN_1E580(iVar1, GameManager.instance.DAT_C2C, 25, vTransform.position);
                LevelManager.instance.FUN_4DE54(vTransform.position, 35);
                UIManager.instance.FUN_4E414(vTransform.position, new Color32(0x80, 0x80, 0x00, 8));
                uVar1 = 0;
            }
        }

        return uVar1;
    }

    //FUN_494 (LAUNCH.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        uint uVar1;
        Vector3Int local_78;

        if (arg1 == 2)
        {
            if (tags == 0)
            {
                tags = 1;
                GameManager.instance.FUN_30CB0(this, 15);
                screen.x = 0x1000;
                FUN_30B78();
                uVar1 = 0;
            }
            else
            {
                if (tags == 1)
                {
                    DAT_80.tags = 0;
                    GameManager.instance.FUN_309A0(this);
                    uVar1 = 0xffffffff;
                }
                else
                    uVar1 = 0;
            }
        }
        else
        {
            if (arg1 < 3)
            {
                uVar1 = 0;

                if (arg1 == 0)
                {
                    screen.x -= 245;
                    local_78 = new Vector3Int(screen.x, screen.x, screen.x);
                    Launch.FUN_5730(ref vTransform.rotation, local_78);
                    uVar1 = 0;
                }
            }
            else
                uVar1 = 0;
        }

        return uVar1;
    }
}
