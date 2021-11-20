using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlacierSmall : Destructible
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
        int iVar4;
        Vehicle vVar8;
        
        if (hit.self.type == 2)
        {
            vVar8 = (Vehicle)hit.self;
            iVar4 = physics1.Z / 0x8000;
            vVar8.FUN_3A064(-iVar4 * 4, vTransform.position, true);
            vVar8.physics1.X += physics1.X;
            vVar8.physics1.Z += physics1.Z;
        }
        else
        {
            if (hit.self.type == 0)
            {
                if (hit.self.GetType() == typeof(GlacierSmall))
                {
                    if (physics1.Z > hit.self.physics1.Z)
                    {
                        hit.self.FUN_4DC94();
                        return 0xfffffffe;
                    }
                }
                
                FUN_4DC94();
                return 0xfffffffe;
            }
            else
            {
                if (hit.self.type == 4)
                {
                    hit.self.FUN_32B90(5);
                    FUN_4DC94();
                    return 0xfffffffe;
                }
            }
        }

        bVar1 = FUN_32CF0(hit);

        if (bVar1)
            return 0xfffffffe;

        return 0;
    }

    //FUN_1BCC (OILFIELD.DLL)
    public override uint UpdateW(int arg1, int arg2)
    {
        bool bVar1;
        int iVar2;
        int iVar3;

        switch (arg1)
        {
            case 0:
                iVar2 = physics1.X;

                if (iVar2 < 0)
                    iVar2 += 127;

                iVar3 = physics1.Z;
                vTransform.position.x += iVar2 >> 7;

                if (iVar3 < 0)
                    iVar3 += 127;

                vTransform.position.z += iVar3 >> 7;
                bVar1 = (uint)GameManager.instance.terrain.DAT_DEC < (uint)vTransform.position.z;

                if (bVar1)
                {
                    iVar2 = GameManager.instance.terrain.FUN_1B750((uint)vTransform.position.x, (uint)vTransform.position.z);

                    if (iVar2 < GameManager.instance.DAT_DB0)
                    {
                        physics1.Z = 0;
                        physics1.X = 0;
                        vTransform.position.y = iVar2;
                    }
                    else
                        vTransform.position.y = GameManager.instance.DAT_DB0;
                }
                else
                    vTransform.position.y = GameManager.instance.DAT_DB0;

                break;
            case 1:
                flags |= 0x10000;
                break;
            case 2:
                FUN_4DC94();
                return 0;
            case 8:
                bVar1 = FUN_32B90((uint)arg2);

                /*if (bVar1)
                    return 0xfffffffe;*/

                break;
            case 9:
                if (arg2 == 0)
                    return 0;

                if (tags == 1)
                    Oilfield.instance.DAT_A4--;

                GameManager.instance.FUN_309A0(this);
                return 0;
        }

        return 0;
    }
}
