using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brearhug : Mine
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
        VigObject oVar2;
        Vehicle vVar2;
        Magnet1 ppcVar3;
        uint uVar4;
        int iVar5;
        ConfigContainer ccVar5;
        Magnet2 ppcVar6;
        Vector3Int local_18;
        Vector3Int local_8;

        oVar2 = hit.self;

        if (oVar2.type == 2)
        {
            if (child2 == null)
            {
                if (0 < LevelManager.instance.DAT_117C)
                    return 0;

                ppcVar3 = vData.ini.FUN_2C17C(192, typeof(Magnet1), 8) as Magnet1;
                ppcVar3.flags = 0x10;
                ppcVar3.screen = new Vector3Int(0, 0, 0);
                Utilities.FUN_2CC48(this, ppcVar3);
                ppcVar3.transform.parent = transform;
                //sound
                LevelManager.instance.DAT_117C++;
            }

            vVar2 = (Vehicle)oVar2;
            local_18 = new Vector3Int(
                vTransform.position.x - vVar2.vTransform.position.x,
                vTransform.position.y - vVar2.vTransform.position.y - 0x5000,
                vTransform.position.x - vVar2.vTransform.position.z);
            Utilities.FUN_29FC8(local_18, out local_8);
            vVar2.physics1.X += local_8.x * 8;
            vVar2.physics1.Y += local_8.y * 8;
            vVar2.physics1.Z += local_8.z * 8;

            if (vVar2.id < 0)
                GameManager.instance.FUN_15ADC(~vVar2.id, 4);

            uVar4 = GameManager.FUN_2AC5C();

            if ((uVar4 & 3) == 0)
            {
                iVar5 = (int)GameManager.FUN_2AC5C();
                ccVar5 = vVar2.FUN_2C5F4((ushort)((iVar5 * 7 >> 15) - 0x7ff0 & 0xffff));

                if (ccVar5 != null)
                {
                    ppcVar6 = LevelManager.instance.xobfList[19].ini.FUN_2C17C(49, typeof(Magnet2), 8) as Magnet2;
                    ppcVar6.vTransform = GameManager.instance.FUN_2CEAC(vVar2, ccVar5);
                    ppcVar6.physics1.Z = local_8.x;
                    ppcVar6.physics1.W = local_8.y;
                    ppcVar6.physics2.X = local_8.z;
                    ppcVar6.flags = 0xB4;
                    ppcVar6.FUN_305FC();
                }
            }
        }

        return 0;
    }

    //FUN_47384
    public override uint UpdateW(int arg1, int arg2)
    {
        int iVar2;

        if (arg1 == 2)
            id = 0;
        else
        {
            if (arg1 < 3)
            {
                if (arg1 == 0)
                {
                    if (tags == 0)
                    {
                        iVar2 = FUN_46D70();

                        if (iVar2 != 0)
                        {
                            if (vTransform.position.z < GameManager.instance.DAT_DA0 &&
                                GameManager.instance.DAT_DB0 <= vTransform.position.y)
                            {
                                tags = 1;
                                physics2.M2 = 0;
                            }
                            else
                                FUN_30BA8();

                            flags |= 0x100;
                            GameManager.instance.FUN_30CB0(this, 120);
                        }
                    }
                    else
                    {
                        iVar2 = GameManager.DAT_65C90[(physics2.M2 & 0xfff) * 2] * 0x1400;

                        if (iVar2 < 0)
                            iVar2 += 0xfff;

                        vTransform.position.y = GameManager.instance.DAT_DB0 + (iVar2 >> 12);
                        physics2.M2 += 34;
                    }
                }
            }
            else
            {
                if (arg1 == 4)
                    ; //sound
            }
        }

        return 0;
    }
}
