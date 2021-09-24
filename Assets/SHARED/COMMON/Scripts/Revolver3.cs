using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver3 : VigObject
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
        VigObject oVar1;
        Vehicle vVar1;
        Vector3Int local_10;

        if (hit.object2.type != 3)
        {
            GameManager.instance.FUN_2F798(this, hit);
            FUN_60(hit.normal1);
            oVar1 = hit.self;

            if (oVar1.type == 2)
            {
                vVar1 = (Vehicle)oVar1;
                local_10 = new Vector3Int();

                if (tags == 0)
                {
                    local_10.x = physics1.Z << 6;
                    local_10.y = physics1.W << 6;
                    local_10.z = physics2.X << 6;
                }
                else
                {
                    local_10.x = physics1.Z * 0x60;
                    local_10.y = physics1.W * 0x60;
                    local_10.z = physics2.X * 0x60;
                }

                vVar1.FUN_2B370(local_10, vTransform.position);
                vVar1.physics1.Y = -0x47800;

                if (vVar1.id < 0)
                    GameManager.instance.FUN_15B00(~vVar1.id, 255, 8, 32);

                if (tags == 0)
                {
                    vVar1.physics2.Z *= 24;
                    vVar1.physics2.X *= 24;
                }
                else
                {
                    //FUN_4E414
                    vVar1.physics2.Z <<= 4;
                    vVar1.physics2.X <<= 4;
                }
            }

            GameManager.instance.FUN_309A0(this);
            return 0xffffffff;
        }

        return 0;
    }

    //FUN_1B4 (TBOLT.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        int iVar1;
        int iVar2;
        uint uVar2;
        Vector3Int auStack24;

        if (arg1 == 2)
        {
            iVar2 = GameManager.instance.FUN_1DD9C();
            GameManager.instance.FUN_1E628(iVar2, GameManager.instance.DAT_C2C, 63, vTransform.position);
            LevelManager.instance.FUN_4DE54(vTransform.position, 13);
            GameManager.instance.FUN_309A0(this);
            uVar2 = 0xffffffff;
        }
        else
        {
            if (arg1 < 3)
            {
                if (arg1 != 0)
                    return 0;

                vTransform.position.x += physics1.Z;
                vTransform.position.y += physics1.W;
                vTransform.position.z += physics2.X;
                iVar1 = GameManager.instance.terrain.FUN_1B750((uint)vTransform.position.x, (uint)vTransform.position.z);

                if (iVar1 < vTransform.position.y)
                {
                    auStack24 = GameManager.instance.terrain.FUN_1B998((uint)vTransform.position.x, (uint)vTransform.position.z);
                    auStack24 = Utilities.VectorNormal(auStack24);
                    FUN_60(auStack24);
                    LevelManager.instance.FUN_309C8(this, 1);
                    return 0xffffffff;
                }
            }

            uVar2 = 0;
        }

        return uVar2;
    }

    //FUN_60 (TBOLT.DLL)
    private void FUN_60(Vector3Int param1)
    {
        Ballistic puVar1;
        int iVar2;
        List<AudioClip> aVar3;
        int iVar4;

        puVar1 = LevelManager.instance.xobfList[19].ini.FUN_2C17C(141, typeof(Ballistic), 8, typeof(VigChild)) as Ballistic;
        puVar1.flags = 0x34;
        puVar1.screen = vTransform.position;
        puVar1.FUN_3066C();

        if (tags == 0)
        {
            iVar4 = GameManager.instance.FUN_1DD9C();
            iVar2 = 65;
            aVar3 = GameManager.instance.DAT_C2C;
        }
        else
        {
            puVar1 = vData.ini.FUN_2C17C(1, typeof(Ballistic), 8, typeof(VigChild)) as Ballistic;
            puVar1.flags = 0x34;
            puVar1.screen = vTransform.position;
            puVar1.FUN_3066C();
            iVar4 = GameManager.instance.FUN_1DD9C();
            iVar2 = 4;
            aVar3 = vData.sndList;
        }

        GameManager.instance.FUN_1E628(iVar4, aVar3, iVar2, vTransform.position);
        LevelManager.instance.FUN_4EAE8(vTransform.position, param1, 148);
    }
}
