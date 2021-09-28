using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireworks : Destructible
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
        Ballistic puVar3;
        int iVar4;
        VigTransform puVar5;
        VigObject oVar6;
        Vehicle vVar6;
        Vector3Int local_50;
        Vector3Int local_20;
        Vector3Int local_10;
        HitDetection auStack136;

        oVar6 = hit.self;
        FUN_32CF0(hit);
        auStack136 = new HitDetection(null);
        GameManager.instance.FUN_2FB70(this, hit, auStack136);
        local_50 = new Vector3Int(auStack136.position.x / 2, auStack136.position.y / 2, auStack136.position.z / 2);
        local_50 = Utilities.FUN_24148(oVar6.vTransform, local_50);
        puVar3 = LevelManager.instance.xobfList[19].ini.FUN_2C17C
            ((ushort)GloryRocket.DAT_1510[DAT_19], typeof(Ballistic), 8, typeof(VigChild)) as Ballistic;
        puVar3.type = 7;
        puVar3.flags = 0x24;
        puVar3.screen = local_50;
        puVar3.FUN_3066C();

        if (oVar6.type == 2)
        {
            vVar6 = (Vehicle)oVar6;
            puVar5 = GameManager.instance.FUN_2CDF4(this);
            local_10 = new Vector3Int(0, 0, 0);
            local_20 = new Vector3Int();
            local_20.x = (puVar5.rotation.V02 << 11) / 96;
            local_20.y = (puVar5.rotation.V12 << 11) / 96;
            local_20.z = (puVar5.rotation.V22 << 11) / 96;
            local_10 = local_20;
            vVar6.FUN_2B370(local_20, local_50);
            vVar6.vTransform.position.y -= 0xa00;
            iVar4 = (int)GameManager.FUN_2AC5C();

            if ((iVar4 << 2) >> 15 == 0 && vVar6.GetPowerup(DAT_19) != 0)
            {
                iVar4 = (int)GameManager.FUN_2AC5C();
                local_10.x = (iVar4 * 3051 >> 15) - 1525;
                local_10.y = -4577;
                iVar4 = (int)GameManager.FUN_2AC5C();
                local_10.z = (iVar4 * 3051 >> 15) - 1525;
                LevelManager.instance.FUN_4AAC0(0x40000U << (DAT_19 + 1 & 31), vVar6.vTransform.position, local_10);
                vVar6.SetPowerup(DAT_19, 0);
            }
        }

        flags |= 0x20;
        return 0;
    }

    //FUN_348 (STNTBIKE.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        ushort uVar1;
        VigChild puVar5;

        if (arg1 == 2)
        {
            puVar5 = child2 as VigChild;
            DAT_4A = GameManager.instance.timer;
            FUN_30BF0();
            flags &= 0xfffffffd;
            uVar1 = GameManager.instance.timer;

            while(puVar5 != null)
            {
                puVar5.state = _CHILD_TYPE.Child;
                puVar5.DAT_4A = uVar1;
                puVar5 = puVar5.child2 as VigChild;
            }
        }

        return 0;
    }

    public override uint UpdateW(int arg1, VigObject arg2)
    {
        VigObject oVar2;

        if (arg1 == 5)
        {
            if (parent != null)
            {
                oVar2 = FUN_2CCBC();
                transform.parent = null;
                GameManager.instance.FUN_307CC(oVar2);
                return 0xffffffff;
            }

            GameManager.instance.FUN_309A0(this);
            return 0xffffffff;
        }

        return 0;
    }
}
