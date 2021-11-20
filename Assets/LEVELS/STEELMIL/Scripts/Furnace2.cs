using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace2 : VigObject
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
        bool bVar3;

        if (hit.object2.type == 3)
            return 0;

        if (hit.self.type == 2)
        {
            bVar3 = LevelManager.instance.FUN_39AF8((Vehicle)hit.self);

            if (bVar3)
            {
                LevelManager.instance.FUN_4DE54(vTransform.position, 35);
                UIManager.instance.FUN_4E414(vTransform.position, new Color32(0x80, 0x80, 0x00, 8));
            }
        }

        return 0;
    }

    public XOBF_DB DAT_98; //0x98

    //FUN_19AC (STEELMIL.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        short sVar1;
        uint uVar2;
        Smoke2 puVar3;
        int iVar4;
        VigObject oVar4;

        if (arg1 == 0)
        {
            sVar1 = (short)(physics1.M0 - 1);
            physics1.M0 = sVar1;

            if (sVar1 == -1)
            {
                puVar3 = DAT_98.ini.FUN_2C17C((ushort)physics2.M3, typeof(Smoke2), 8) as Smoke2;
                puVar3.physics1.Z = 0;
                puVar3.physics1.W = 0;
                puVar3.flags |= 0x410;
                iVar4 = (int)GameManager.FUN_2AC5C();
                puVar3.physics2.X = physics1.W + (iVar4 * physics1.W >> 15);
                puVar3.vTransform = GameManager.FUN_2A39C();
                Utilities.FUN_2CC48(this, puVar3);
                Utilities.ParentChildren(this, this);
                physics1.M0 = physics1.M2;
            }

            oVar4 = child2;

            if (oVar4 == null)
            {
                GameManager.instance.FUN_309A0(this);
                uVar2 = 0xffffffff;
            }
            else
            {
                do
                {
                    oVar4.vTransform.position.x += oVar4.physics1.Z;
                    oVar4.vTransform.position.y += oVar4.physics1.W;
                    oVar4.vTransform.position.z += oVar4.physics2.X;
                    oVar4.physics1.W += 90;
                    oVar4.physics2.X -= 32;

                    if (screen.y < oVar4.vTransform.position.y)
                    {
                        oVar4.vTransform.position.y = screen.y;
                        oVar4.physics1.Z = 0;
                        oVar4.physics1.W = 0;
                        oVar4.physics2.X = 0;
                    }

                    oVar4 = oVar4.child;
                    uVar2 = 0;
                } while (oVar4 != null);
            }
        }
        else
        {
            uVar2 = 0;

            if (arg1 == 2)
            {
                physics1.M0 = -1;
                uVar2 = 0;
            }
        }

        return 0;
    }
}
