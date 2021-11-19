using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trumpet2 : VigObject
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
        int iVar4;
        Vehicle vVar9;
        VigTransform auStack32;

        if (hit.self.type == 2 && (hit.self.flags & 0x4000000) == 0)
        {
            vVar9 = (Vehicle)hit.self;

            if (vVar9.id < 0)
                GameManager.instance.FUN_15AA8(~vVar9.id, 8, 255, 64, 16);

            vVar9.physics1.X += physics1.Z * 6;
            vVar9.physics1.Z += physics2.X * 6;
            iVar4 = (int)GameManager.FUN_2AC5C();
            vVar9.physics1.Y += (((iVar4 << 12) >> 15) - 0x800) * 0x80;

            if ((flags & 0x1000000) == 0)
            {
                flags |= 0x1000000;
                vVar9.flip = 200;
                iVar2 = GameManager.instance.FUN_1DD9C();
                GameManager.instance.FUN_1E580(iVar2, vData.sndList, 3, vVar9.vTransform.position);
                GameManager.instance.FUN_2F798(this, hit);
                auStack32 = new VigTransform();
                auStack32.position = Utilities.FUN_24148(vTransform, hit.position);
                auStack32.rotation = Utilities.FUN_2A5EC(hit.normal1);
                LevelManager.instance.FUN_4E56C(auStack32, 123);
            }
        }

        return 0;
    }

    //FUN_14A0 (TRUCK.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        short sVar1;
        uint uVar2;
        TrumpetBallistic puVar3;
        int iVar4;
        VigObject oVar4;
        int iVar5;
        short sVar6;
        int iVar9;
        VigObject oVar9;

        if (arg1 == 0)
        {
            vTransform.position.x += physics1.Z;
            vTransform.position.y += physics1.W;
            vTransform.position.z += physics2.X;
            iVar4 = physics1.Z;
            iVar9 = iVar4;

            if (iVar4 < 0)
                iVar9 = iVar4 + 63;

            iVar5 = physics1.W;
            physics1.Z = iVar4 - (iVar9 >> 6);
            iVar9 = iVar5;

            if (iVar5 < 0)
                iVar9 = iVar5 + 63;

            iVar4 = physics2.X;
            physics1.W = iVar5 - (iVar9 >> 6);
            iVar9 = iVar4;

            if (iVar4 < 0)
                iVar9 = iVar4 + 63;

            physics2.X = iVar4 - (iVar9 >> 6);
            oVar9 = child2;
            sVar1 = (short)(physics1.M2 * 0x1e0 + 0x400);
            oVar9.vTransform.rotation.V11 = sVar1;
            oVar9.vTransform.rotation.V00 = sVar1;
            sVar6 = (short)(physics1.M2 + 1);
            physics1.M2 = sVar6;

            if ((sVar6 * 0x10000) >> 0x10 == 0x40)
            {
                GameManager.instance.FUN_309A0(this);
                uVar2 = 0xffffffff;
            }
            else
            {
                uVar2 = 0;

                if ((sVar6 & 1) == 0)
                {
                    Dictionary<int, Type> dict = new Dictionary<int, Type>();
                    dict.Add(152, typeof(VigChild));
                    puVar3 = vData.ini.FUN_2C17C((ushort)DAT_1A, typeof(TrumpetBallistic), 8, dict) as TrumpetBallistic;
                    Utilities.ParentChildren(puVar3, puVar3);
                    puVar3.DAT_90 = this;
                    puVar3.flags = 0x60000384; //0x24
                    puVar3.type = 8;
                    puVar3.maxHalfHealth = 2;
                    puVar3.DAT_80 = DAT_80;
                    puVar3.id = id;
                    puVar3.vTransform = vTransform;
                    oVar4 = puVar3.child2;
                    oVar4.vTransform = oVar9.vTransform;
                    puVar3.FUN_305FC();
                    uVar2 = 0;
                }
            }
        }
        else
            uVar2 = 0;

        return uVar2;
    }
}
