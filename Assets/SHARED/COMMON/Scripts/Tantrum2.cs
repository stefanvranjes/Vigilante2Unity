using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tantrum2 : VigObject
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
        short sVar2;
        int iVar3;
        uint uVar3;
        Vector3Int v3Var3;
        uint uVar5;
        int iVar4;
        Particle1 pVar4;
        VigObject oVar4;
        int iVar6;
        VigObject oVar7;
        Vehicle vVar7;
        Vector3Int iStack24;

        uVar3 = 0;
        oVar7 = hit.self;

        if (oVar7.type != 3)
        {
            pVar4 = LevelManager.instance.FUN_4DE54(screen, 34);
            pVar4.flags |= 0x400;
            sVar2 = (short)GameManager.FUN_2AC5C();
            pVar4.vr.z = sVar2;
            pVar4.ApplyTransformation();
            iVar3 = GameManager.instance.FUN_1DD9C();
            GameManager.instance.FUN_1E628(iVar3, GameManager.instance.DAT_C2C, 66, screen);

            if (oVar7.type == 2)
            {
                vVar7 = (Vehicle)oVar7;
                iStack24 = new Vector3Int();
                iStack24.x = physics1.Z << 4;
                iStack24.y = physics1.W << 4;
                iStack24.z = physics2.X << 4;
                vVar7.FUN_2B370(iStack24, vTransform.position);

                if (vVar7.id < 0)
                    GameManager.instance.FUN_15ADC(~vVar7.id, 10);

                uVar5 = GameManager.FUN_2AC5C();

                if ((uVar5 & 7) == 0)
                {
                    iVar4 = (int)GameManager.FUN_2AC5C();
                    iVar6 = iVar4 * 3 >> 15;
                    oVar4 = vVar7.weapons[iVar6];

                    if (oVar4 != null && oVar4.tags < 8)
                    {
                        v3Var3 = GameManager.instance.FUN_2CE50(oVar4);
                        LevelManager.instance.FUN_4DE54(v3Var3, 142);
                        vVar7.FUN_3A280((uint)iVar6);
                    }
                }
            }

            LevelManager.instance.FUN_309C8(this, 0);
        }

        uVar3 = 0xffffffff;
        return uVar3;
    }

    //FUN_70 (WUNDER.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        short sVar1;
        short sVar2;
        int iVar3;
        uint uVar3;
        Particle1 pVar7;

        if (arg1 == 0)
        {
            screen.x += physics1.Z;
            screen.y += physics1.W;
            screen.z += physics2.X;
            vTransform.position.x = screen.x;
            vTransform.position.y = screen.y;
            vTransform.position.z = screen.z;
            sVar1 = (short)(physics2.M2 - 1);
            physics2.M2 = sVar1;

            if (sVar1 == -1 || GameManager.instance.terrain.FUN_1B750((uint)screen.x, (uint)screen.z) < screen.y)
            {
                pVar7 = LevelManager.instance.FUN_4DE54(screen, 34);
                sVar2 = (short)GameManager.FUN_2AC5C();
                pVar7.vr.z = sVar2;
                pVar7.ApplyTransformation();
                iVar3 = GameManager.instance.FUN_1DD9C();
                GameManager.instance.FUN_1E628(iVar3, GameManager.instance.DAT_C2C, 66, screen);
                LevelManager.instance.FUN_309C8(this, 1);
                uVar3 = 0;
            }
            else
                uVar3 = 0;
        }
        else
            uVar3 = 0;

        return uVar3;
    }
}
